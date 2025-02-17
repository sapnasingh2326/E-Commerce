using ECommerceWeb.Model;
using ECommerceWeb.Pages.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ECommerceWeb.Pages.Admin
{
    public class BrandModel : PageModel
    {
        private readonly DemoProjectContext _context;

        public BrandModel(DemoProjectContext context)
        {
            _context = context;
        }


        public IEnumerable<Brand> datalist { get; set; } //this is for list



        [BindProperty]
        public Brand brand { get; set; }



        #region----GET METHOD-----
        public IActionResult OnGet(int? id)
        {
            datalist = _context.Brands.Where(e => e.IsDeleted == false).ToList();
            brand = new Brand();
            if (id.HasValue)
            {
                var co = _context.Brands.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
                if (co == null)
                {
                    return NotFound();
                }
                brand = co;
            }


            return Page();

        }
        #endregion


        #region------POST METHOD---

        public IActionResult OnPost()
        {
            var BrandLogoStr = "";
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

                    BrandLogoStr = "/images/" + uniqueFileName;

                }
            }

            if (BrandLogoStr != "")
            {
                brand.BrandLogo = BrandLogoStr;
            }
            if (brand.Id > 0)//Update
            {
                var co = _context.Banners.AsNoTracking().Where(e => e.Id == brand.Id && e.IsDeleted == false).FirstOrDefault();
                if (co == null)
                {
                    return NotFound();
                }
                brand.IsDeleted = co.IsDeleted;
                _context.Brands.Update(brand);
                _context.SaveChanges();
                TempData["info"] = "Your Data update Successfully";
            }
            else
            {
                try
                {
                    brand.IsDeleted = false;
                    _context.Brands.Add(brand);
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
            var co = _context.Brands.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
            if (co == null)
            {
                return NotFound();
            }
            co.IsDeleted = true;
            _context.Brands.Update(co);
            _context.SaveChanges();
            TempData["success"] = "data deleted";
            return RedirectToPage();

        }

        #endregion



    }
}