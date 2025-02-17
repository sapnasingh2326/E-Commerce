using ECommerceWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWeb.Pages
{
    public class ProductDetailsModel : PageModel
    {
        private readonly DemoProjectContext _context;
        public ProductDetailsModel(DemoProjectContext context)
        {
            _context = context;
        }


        public IEnumerable<SubCategory> datalist { get; set; }



        [BindProperty]
        public Product product { get; set; }

        [BindProperty] 
        public IEnumerable<ProductImage> productImages { get; set; }
        public IEnumerable<ProductSize> productSize { get; set; } //this is for list
        public IEnumerable<ProductColor> productColor { get; set; } //this is for list
        public IEnumerable <ProductDetailsModel> productDetails { get; set; }
        public IEnumerable<SubCategory> subCategories { get; set; }
         
        #region-----GET METHOD------
        public IActionResult OnGet(int? id)
        {
            
            productSize = _context.ProductSizes.Where(e => e.IsDeleted == false).ToList();

            productColor = _context.ProductColors.Where(e => e.IsDeleted == false).ToList();

            subCategories = _context.SubCategories.Where(e => e.IsDeleted==false).ToList();

            productImages = _context.ProductImages.Where(e => e.IsDeleted == false).ToList();
          
         

            product = new Product();
            if (id.HasValue)
            {
                var co = _context.Products.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();

                productImages = _context.ProductImages.Where(x => x.Product == id  && x.IsDeleted == false).Select(x => x).ToList();
                
                
                if (co == null)
                {
                    return NotFound();
                }
                product = co;
            }


            return Page();

        }
        #endregion


        #region--------POST METHOD---------
        public IActionResult OnPost()
        {
            if (product.Id > 0)//Update
            {
                var co = _context.Products.AsNoTracking().Where(e => e.Id == product.Id && e.IsDeleted == false).FirstOrDefault();
                if (co == null)
                {
                    return NotFound();
                }


                product.IsDeleted = co.IsDeleted;
                _context.Products.Update(product);
                _context.SaveChanges();
                TempData["info"] = "Your Data update Successfully";

            }
            else
            {
                try
                {

                    product.IsDeleted = false;
                    _context.Products.Add(product);
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
            var co = _context.Products.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
            if (co == null)
            {
                return NotFound();
            }
            co.IsDeleted = true;
            _context.Products.Update(co);
            _context.SaveChanges();
            TempData["success"] = "data deleted";
            return RedirectToPage();

        }

        #endregion

        public IActionResult AddToCart(int productId)
        {
            // Logic to add the product to the cart
            // For example, fetching the product by ID and adding it to the user's session/cart

            return RedirectToAction("Index", "Cart");
        }


    }
}
