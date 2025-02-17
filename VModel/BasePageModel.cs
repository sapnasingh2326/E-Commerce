using ECommerceWeb.Model;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceWeb.VModel
{
    public class BasePageModel : PageModel
    {
        private readonly DemoProjectContext _context;

        public BasePageModel(DemoProjectContext context)
        {
            _context = context;
        }



        public IEnumerable<ProductListVM> NewProducts { get; set; }

        public override void OnPageHandlerExecuting(PageHandlerExecutingContext context)
        {

            
            NewProducts = _context.Products.Where(x => x.IsDeleted == false && x.IsNewarrivals == true).Select(x => new ProductListVM
            {
                id = x.Id,
                ProductName = x.ProductName,
                ProductPrice = x.Price,
                ProductImage = x.ProductDisplayImage
            }).ToList();

             
            base.OnPageHandlerExecuting(context);
        }
    }
}
