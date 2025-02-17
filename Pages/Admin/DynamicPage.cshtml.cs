using ECommerceWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
namespace ECommerceWeb.Pages.Admin
{
    public class DynamicPageModel : PageModel
    {

        private readonly DemoProjectContext _context;

        public DynamicPageModel(DemoProjectContext context)
        {
            _context = context;
        }


        public IEnumerable<DynamicPage> datalist { get; set; } //this is for list DynamicPage



        [BindProperty]
        public DynamicPage dynamicPage { get; set; }

        #region----GET METHOD-----
        public IActionResult OnGet(int? id)
        {
           datalist = _context.DynamicPages.Where(e => e.IsDeleted == false).ToList();

            dynamicPage = new DynamicPage();

            if (id.HasValue)
            {
                var co  = _context.DynamicPages.Where(e => e.PageId == id && e.IsDeleted == false).FirstOrDefault();
                if (co == null)
                {
                    return NotFound();
                }
                dynamicPage = co;
            }
     
            return Page();

        }
        #endregion
         

        #region------POST METHOD---

        public IActionResult OnPost()
        {
            var DynamicPageImgStr = "";
            foreach (var file  in HttpContext.Request.Form.Files)
            {
                // Check if file is not null and has content
                if (file != null && file.Length > 0)
                {
                    // Save the file to a directory on the server
                    var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images"); // Adjust path as needed
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    var filePath = Path.Combine(uploads, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    DynamicPageImgStr = "/images/" + uniqueFileName;

                }
            }

            if (DynamicPageImgStr != "")
            {
                dynamicPage.PageUrl = DynamicPageImgStr;
            }
            if (dynamicPage.PageId > 0)//Update
            {
                //var co = _context.Banners.AsNoTracking().Where(e => e.Id == dynamicPage.PageId && e.IsDeleted == false).FirstOrDefault();
                //if (co == null)
                //{
                //    return NotFound();
                //}
                dynamicPage.IsDeleted = false;
                _context.DynamicPages.Update(dynamicPage);
                _context.SaveChanges();
                TempData["info"] = "Your Data update Successfully";
            }
            else
            {
                try
                {
                    dynamicPage.IsDeleted = false;
                    _context.DynamicPages.Add(dynamicPage);
                    _context.SaveChanges();
                    TempData["success"] = "data saved";
                }
                catch (Exception ex) { }
            }
            return RedirectToPage();
        }
         
        #endregion

        #region---DELETE METHOD--

        public IActionResult OnPostDelete(int id)
        {
            var co = _context.DynamicPages.Where(e => e.PageId == id && e.IsDeleted == false).FirstOrDefault();
            if (co == null)
            {
                return NotFound();
            }
            co.IsDeleted = true;
            _context.DynamicPages.Update(co);
            _context.SaveChanges();
            TempData["success"] = "data deleted";
            return RedirectToPage();

        }

        #endregion




    }
}
