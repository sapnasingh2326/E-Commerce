using ECommerceWeb.Model;
using ECommerceWeb.Pages.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ECommerceWeb.Pages.Admin
{
    public class ProductImageModel : PageModel
    {
        private DemoProjectContext _context;

        public ProductImageModel(DemoProjectContext context)
        {
            _context = context;
        }
        public IEnumerable<ProductImage> datalist { get; set; } //this is for list

        public IEnumerable<Product> product { get; set; } //this is for list


        [BindProperty]
        public ProductImage productImage { get; set; }



        #region----GET METHOD-----
        public IActionResult OnGet(int? id)
        {
            datalist = _context.ProductImages.Where(e => e.IsDeleted == false).ToList();

            product = _context.Products.Where(e => e.IsDeleted == false).ToList();

            productImage = new ProductImage();
            if (id.HasValue)
            {
                var co = _context.ProductImages.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
                if (co == null)
                {
                    return NotFound();
                }
                productImage = co;
            }


            return Page();

        }
        #endregion


        #region------POST METHOD---

        public IActionResult OnPost()
        {
            var ImageUrlStr1 = "";
            foreach (var file in HttpContext.Request.Form.Files)
            {
                // Check if file is not null and has content
                if (file != null && file.Length > 0)
                {
                    // Save the file to a directory on the server
                    var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images/ProductImage"); // Adjust path as needed
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    var filePath = Path.Combine(uploads, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                   var ImageUrlStr = "/images/ProductImage/" + uniqueFileName;

                    productImage.ImageUrl = ImageUrlStr;

                    if (productImage.Id > 0)//Update
                    {
                        var co = _context.ProductImages.AsNoTracking().Where(e => e.Id == productImage.Id && e.IsDeleted == false).FirstOrDefault();
                        if (co == null)
                        {
                            return NotFound();
                        }
                        productImage.IsDeleted = co.IsDeleted;
                        _context.ProductImages.Update(productImage);
                        _context.SaveChanges();
                        TempData["info"] = "Your Data update Successfully";
                    }
                    else
                    {
                        try
                        {
                            productImage.IsDeleted = false;
                            _context.ProductImages.Add(productImage);
                            _context.SaveChanges();

                            productImage.Id = 0;
                            TempData["success"] = "data saved";
                        }
                        catch (Exception ex) { }
                    }

                }
            }

            
            return RedirectToPage();
        }

        #endregion

        #region---DELETE METHOD--

        public IActionResult OnPostDelete(int id)
        {
            var co = _context.ProductImages.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
            if (co == null)
            {
                return NotFound();
            }
            co.IsDeleted = true;
            _context.ProductImages.Update(co);
            _context.SaveChanges();
            TempData["success"] = "data deleted";
            return RedirectToPage();

        }

        #endregion



    }
}