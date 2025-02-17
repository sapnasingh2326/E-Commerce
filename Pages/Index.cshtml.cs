using ECommerceWeb.Model;
using ECommerceWeb.VModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Threading;

namespace ECommerceWeb.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        private readonly DemoProjectContext _context;

        public IndexModel(ILogger<IndexModel> logger, DemoProjectContext context)
        {
            _logger = logger;
            _context = context;
        }



        [BindProperty]
        public IEnumerable<Banner> banner { get; set; }
        public IEnumerable<Brand> brand { get; set; }
        public IEnumerable<Cart> cart { get; set; }

        //public IEnumerable <Wishlist> wishlist { get; set; } = Enumerable.Empty<Wishlist>();
        public IEnumerable<CatSubCatListVModel> categorys { get; set; }

        //----------virtual Model for Product List------------//

        public IEnumerable<ProductListVM> products { get; set; }
        public IEnumerable<ProductListVM> NewArrivalproducts { get; set; }
        public IEnumerable<ProductListVM> ThreeItemsproducts { get; set; } // Declare the ThreeItemsproducts collection

        public IEnumerable<ProductListVM> TwoItemsproducts { get; set; }


        //-----------ThreeItems-----------
        //public IEnumerable<ProductListVM> ThreeItemsproducts { get; set; }
        public void OnGet()
        {
            banner = _context.Banners.Where(x => x.IsDeleted == false).Select(x => x).ToList();
            brand = _context.Brands.Where(x => x.IsDeleted == false).Select(x => x).ToList();
            //cart = _context.Carts.Where(x => x.IsDeleted==false).Select(x=> x).ToList();
            //wishlist = _context.Wishlists.Where(x => x.IsDeleted == false).Select(x => x).ToList(); 

            categorys = _context.Categories.Where(x => x.IsDeleted == false).Select(x => new CatSubCatListVModel
            {
                Category = x,
                SubCategories = x.SubCategories
            }).ToList();

            products = _context.Products.Where(x => x.IsDeleted == false).Select(x => new ProductListVM
            {
                id = x.Id,
                ProductName = x.ProductName,
                ProductPrice = x.Price,
                ProductImage = x.ProductImages.Where(y => y.IsDeleted == false).Select(x => x.ImageUrl).FirstOrDefault(),
            }).ToList();

            NewArrivalproducts = _context.Products.Where(x => x.IsDeleted == false && x.IsNewarrivals == true).Select(x => new ProductListVM
            {
                id = x.Id,
                ProductName = x.ProductName,
                ProductPrice = x.Price,
                ProductImage = x.ProductImages.Where(y => y.IsDeleted == false).Select(x => x.ImageUrl).FirstOrDefault(),
            }).ToList();



            TwoItemsproducts = _context.Products.Where(x => x.IsDeleted == false && x.IsTwoitems == true).Select(x => new ProductListVM
            {
                id = x.Id,
                ProductName = x.ProductName,
                ProductPrice = x.Price,
                ProductImage = x.ProductImages.Where(y => y.IsDeleted == false).Select(x => x.ImageUrl).FirstOrDefault(),
            }).ToList();

            // Populate the ThreeItemsproducts collection
            ThreeItemsproducts = _context.Products.Where(x => x.IsDeleted == false && x.IsThreeitems == true).Select(x => new ProductListVM
            {
                id = x.Id,
                ProductName = x.ProductName,
                ProductPrice = x.Price,
                ProductImage = x.ProductImages.Where(y => y.IsDeleted == false).Select(x => x.ImageUrl).FirstOrDefault(),
            }).ToList();
        }

            



    }
}
