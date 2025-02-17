using ECommerceWeb.Model;
using ECommerceWeb.VModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ECommerceWeb.Pages.Admin
{
    public class EmailTempModel : PageModel
    {
        private readonly DemoProjectContext _context;

        public EmailTempModel (DemoProjectContext context)
        {
            _context = context;
        }

        public IEnumerable<EmailTemp> datalist { get; set; }
        [BindProperty]
        public EmailTemp emailTemp { get; set; }
        public Registration registration { get; set; }
        public IActionResult OnGet(int? id) 
        {
            datalist = _context.EmailTemps.Where(e => e.IsDeleted == false).ToList();
            registration =_context.Registrations.Where(e => e.Id == id && e.IsDeleted== false).FirstOrDefault(registration=>registration.Id==id);
            

            emailTemp = id.HasValue ? _context.EmailTemps.FirstOrDefault(e => e.Id == id && e.IsDeleted == false) : new EmailTemp();
            if (id.HasValue && emailTemp == null) return NotFound();
            return Page();
        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid) return Page();

            string uniqueFileName = null;
            if (HttpContext.Request.Form.Files.Count > 0)
            {
                var file = HttpContext.Request.Form.Files[0];
                if (file.Length > 0)
                {
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");
                    uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using var stream = new FileStream(filePath, FileMode.Create);
                    file.CopyTo(stream);
                    emailTemp.EmailUrl = "/images/" + uniqueFileName;
                }
            }

            if (emailTemp.Id > 0)
            {
                emailTemp.IsDeleted = false;
                _context.EmailTemps.Update(emailTemp);
            }
            else
            {
                emailTemp.IsDeleted = false;
                _context.EmailTemps.Add(emailTemp);
            }
            _context.SaveChanges();
            TempData["success"] = "Template saved successfully.";
            return RedirectToPage();
        }

        //public IActionResult OnPostDelete(int id)
        //{
        //    var template = _context.EmailTemps.FirstOrDefault(e => e.Id == id && e.IsDeleted == false);
        //    if (template == null) return NotFound();
        //    template.IsDeleted = true;
        //    _context.EmailTemps.Update(template);
        //    _context.SaveChanges();
        //    TempData["success"] = "Template deleted successfully.";
        //    return RedirectToPage();
        //}


        #region---DELETE METHOD--

        public IActionResult OnPostDelete(int id)
        {
            var template = _context.EmailTemps.FirstOrDefault(e => e.Id == id && e.IsDeleted == false);
            if (template == null)
            {
                return NotFound();
            }

            template.IsDeleted = true;
            _context.EmailTemps.Update(template);
            _context.SaveChanges();
            TempData["success"] = "Template deleted successfully.";
            return RedirectToPage();
        }


        #endregion



    }
}
