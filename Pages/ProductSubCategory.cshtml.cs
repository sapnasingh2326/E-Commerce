using ECommerceWeb.Model;
using ECommerceWeb.VModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWeb.Pages
{
    public class ProductSubCategoryModel : PageModel
    {
        private readonly DemoProjectContext _context;

        public ProductSubCategoryModel (DemoProjectContext context)
        {
            _context = context;
        }

        public IEnumerable<Product> datalist { get; set; } //this is for list



        [BindProperty]
        public Product product { get; set; }

        [BindProperty]
        public IEnumerable<ProductListVM> products { get; set; }

        #region--------GET METHOD---
        public void OnGet(int id)
        {
            products = _context.Products.Where(x => x.IsDeleted == false && x.SubCategoryId == id).Select(x => new ProductListVM
            {
                id = x.Id,
                ProductName = x.ProductName,
                ProductPrice = x.Price,
                ProductImage = x.ProductImages.Where(y => y.IsDeleted == false).Select(x => x.ImageUrl).FirstOrDefault(),
                ProductDescription = x.LongDescription,
            }).ToList();
        }
        #endregion
    }
}




