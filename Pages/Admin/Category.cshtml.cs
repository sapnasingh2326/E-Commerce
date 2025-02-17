using ECommerceWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWeb.Pages.Admin
{
    public class CategoryModel : PageModel
    {
        private readonly DemoProjectContext _context;

        public CategoryModel (DemoProjectContext context)
        {
            _context = context;
        }


        public IEnumerable<Category> datalist { get; set; } //this is for list



        [BindProperty]
        public Category category { get; set; }



        #region----GET METHOD-----
        public IActionResult OnGet(int? id)
        {
            datalist = _context.Categories.Where(e => e.IsDeleted == false).ToList();
            category = new Category();
            if (id.HasValue)
            {
                var co = _context.Categories.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
                if (co == null)
                {
                    return NotFound();
                }
                category = co;
            }


            return Page();

        }
        #endregion


        #region------POST METHOD---

        public IActionResult OnPost()
        {
            var CategoryImageUrlStr = "";
            foreach (var file in HttpContext.Request.Form.Files)
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

                    CategoryImageUrlStr = "/images/" + uniqueFileName;

                }
            }

            if (CategoryImageUrlStr != "")
            {
                category.CategoryImageUrl = CategoryImageUrlStr;
            }
            if (category.Id > 0)//Update
            {
                var co = _context.Banners.AsNoTracking().Where(e => e.Id == category.Id && e.IsDeleted == false).FirstOrDefault();
                if (co == null)
                {
                    return NotFound();
                }
                category.IsDeleted = co.IsDeleted;
                _context.Categories.Update(category);
                _context.SaveChanges();
                TempData["info"] = "Your Data update Successfully";
            }
            else
            {
                try
                {
                    category.IsDeleted = false;
                    _context.Categories.Add(category);
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
            var co = _context.Categories.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
            if (co == null)
            {
                return NotFound();
            }
            co.IsDeleted = true;
            _context.Categories.Update(co);
            _context.SaveChanges();
            TempData["success"] = "data deleted";
            return RedirectToPage();

        }

        #endregion



    }
}