using ECommerceWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWeb.Pages.Admin
{
    public class ProductColorModel : PageModel
    {
        private readonly DemoProjectContext _context;

        public ProductColorModel (DemoProjectContext context)
        {
            _context = context;
        }


        public IEnumerable<ProductColor> datalist { get; set; } //this is for list



        [BindProperty]
        public ProductColor productColor { get; set; }



        #region----GET METHOD-----
        public IActionResult OnGet(int? id)
        {
            datalist = _context.ProductColors.Where(e => e.IsDeleted == false).ToList();
            productColor = new ProductColor();
            if (id.HasValue)
            {
                var co = _context.ProductColors.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
                if (co == null)
                {
                    return NotFound();
                }
                productColor = co;
            }


            return Page();

        }
        #endregion


        #region--POST METHOD--
        public IActionResult OnPost()
        {
            if (productColor.Id > 0)//Update
            {
                var co = _context.ProductSizes.AsNoTracking().Where(e => e.Id == productColor.Id && e.IsDeleted == false).FirstOrDefault();
                if (co == null)
                {
                    return NotFound();
                }


                productColor.IsDeleted = co.IsDeleted;
                _context.ProductColors.Update(productColor);
                _context.SaveChanges();
                TempData["info"] = "Your Data update Successfully";

            }
            else
            {
                try
                {

                    productColor.IsDeleted = false;
                    _context.ProductColors.Add(productColor);
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
            var co = _context.ProductColors.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
            if (co == null)
            {
                return NotFound();
            }
            co.IsDeleted = true;
            _context.ProductColors.Update(co);
            _context.SaveChanges();
            TempData["success"] = "data deleted";
            return RedirectToPage();

        }

        #endregion



    }
}