using ECommerceWeb.Model;
using ECommerceWeb.VModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.ViewEngines;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using ECommerceWeb.Services;
using Microsoft.AspNetCore.Mvc.Razor;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerceWeb.Pages.Admin
{
    public class RegistrationModel : PageModel
    {
        private readonly DemoProjectContext _context;
        private readonly EmailSender _emailSender; // Inject EmailSender

        public RegistrationModel(DemoProjectContext context, EmailSender emailSender)
        {
            _context = context;
            _emailSender = emailSender; // Initialize the EmailSender
        }

        public IEnumerable<RegistrationVM> datalist { get; set; } //this is for list
        public IEnumerable<Role> role { get; set; } //this is for list

        [BindProperty]
        public Registration registration { get; set; }

        #region----GET METHOD-----
        public IActionResult OnGet(int? id)
        {
            role = _context.Roles.Where(e => e.IsDeleted == false).ToList();

            datalist = _context.Registrations.Where(e => e.IsDeleted == false).Select(e => new RegistrationVM
            {
                Id = e.Id,
                RoleName = e.Role.RoleName,
                Name = e.Name,
                UserName = e.UserName,
                Mobile = e.Mobile,
                Email = e.Email,
                Address = e.Address,
                Password = e.Password,
                PostCode = e.PostCode,
                CreatedDate = e.CreatedDate,
                ImageUrl = e.ImageUrl,
            }).ToList();

            registration = new Registration();
            if (id.HasValue)
            {
                var co = _context.Registrations.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
                if (co == null)
                {
                    return NotFound();
                }
                registration = co;
            }

            return Page();
        }
        #endregion

        #region--POST METHOD--
        public async Task<IActionResult> OnPostAsync() // Make OnPost method async
        {
            foreach (var file in HttpContext.Request.Form.Files)
            {
                if (file != null && file.Length > 0)
                {
                    var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images/ProductImage");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    var filePath = Path.Combine(uploads, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }
                    registration.ImageUrl = "/images/ProductImage/" + uniqueFileName;
                }
            }

            if (registration.Id > 0) // Update
            {
                var co = _context.Registrations.AsNoTracking().Where(e => e.Id == registration.Id && e.IsDeleted == false).FirstOrDefault();
                if (co == null) return NotFound();

                registration.IsDeleted = false;
                _context.Registrations.Update(registration);
                _context.SaveChanges();
                TempData["info"] = "Your Data update Successfully";
            }
            else // New Registration
            {
                try
                {
                    registration.CreatedDate = DateTime.Now;
                    registration.IsDeleted = false;
                    _context.Registrations.Add(registration);
                    _context.SaveChanges();

                    TempData["success"] = "data saved";

                    // Send registration email
                    var emailViewModel = new EmailViewModel
                    {
                        UserName = registration.Name,
                        Email = registration.Email
                    };


                    await _emailSender.SendRegistrationEmailAsync(registration.Email, registration.Name);

                    var emailBody = await RenderViewToStringAsync("~/Pages/Shared/Emailtemp.cshtml", emailViewModel);
                }
                catch (Exception ex)
                {
                    // Handle exception
                }
            }

            return RedirectToPage();
        }
        #endregion

        // Render Razor view to string (for email template)
        private async Task<string> RenderViewToStringAsync<TModel>(string viewName, TModel model)
        {
            var viewEngine = HttpContext.RequestServices.GetService(typeof(IRazorViewEngine)) as IRazorViewEngine;
            var tempDataProvider = HttpContext.RequestServices.GetService(typeof(ITempDataProvider)) as ITempDataProvider;

            using var writer = new StringWriter();
            var viewResult = viewEngine.FindView(PageContext, viewName, false); // Use PageContext instead of ActionContext

            if (viewResult.View == null)
                throw new ArgumentNullException($"View {viewName} not found");

            var viewContext = new ViewContext(
                PageContext, // Use PageContext
                viewResult.View,
                new ViewDataDictionary<TModel>(ViewData, model),
                new TempDataDictionary(HttpContext, tempDataProvider),
                writer,
                new HtmlHelperOptions()
            );

            await viewResult.View.RenderAsync(viewContext);
            return writer.ToString();
        }

    }
}
