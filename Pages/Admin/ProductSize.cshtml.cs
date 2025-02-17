using ECommerceWeb.Model;
using ECommerceWeb.Pages.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ECommerceWeb.Pages.Admin
{
    public class ProductSizeModel : PageModel
    {
        private readonly DemoProjectContext _context;

        public ProductSizeModel(DemoProjectContext context)
        {
            _context = context;
        }


        public IEnumerable<ProductSize> datalist { get; set; } //this is for list



        [BindProperty]
        public ProductSize productSize { get; set; }



        #region----GET METHOD-----
        public IActionResult OnGet(int? id)
        {
            datalist = _context.ProductSizes.Where(e => e.IsDeleted == false).ToList();
            productSize = new ProductSize();
            if (id.HasValue)
            {
                var co = _context.ProductSizes.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
                if (co == null)
                {
                    return NotFound();
                }
                productSize = co;
            }


            return Page();

        }
        #endregion


        #region--POST METHOD--
        public IActionResult OnPost()
        {
            if (productSize.Id > 0)//Update
            {
                var co = _context.ProductSizes.AsNoTracking().Where(e => e.Id == productSize.Id && e.IsDeleted == false).FirstOrDefault();
                if (co == null)
                {
                    return NotFound();
                }


                productSize.IsDeleted = co.IsDeleted;
                _context.ProductSizes.Update(productSize);
                _context.SaveChanges();
                TempData["info"] = "Your Data update Successfully";

            }
            else
            {
                try
                {

                    productSize.IsDeleted = false;
                    _context.ProductSizes.Add(productSize);
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
            var co = _context.ProductSizes.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
            if (co == null)
            {
                return NotFound();
            }
            co.IsDeleted = true;
            _context.ProductSizes.Update(co);
            _context.SaveChanges();
            TempData["success"] = "data deleted";
            return RedirectToPage();

        }

        #endregion



    }
}
