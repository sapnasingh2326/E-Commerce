using ECommerceWeb.Model;
using ECommerceWeb.VModel;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceWeb.Pages.Shared
{
    public class LayoutNewProductModel : PageModel
    {
        private readonly DemoProjectContext _context;
        public IEnumerable<ProductListVM> NewProducts { get; set; }

        public LayoutNewProductModel(DemoProjectContext context)
        {
            _context = context;
        }

        public void OnGet()
        {

        }
    }

}
