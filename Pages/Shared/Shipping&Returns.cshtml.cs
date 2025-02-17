using ECommerceWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceWeb.Pages.Shared
{
    public class Shipping_ReturnsModel : PageModel 
    {
        private readonly ILogger<Shipping_ReturnsModel> _logger;
        private readonly DemoProjectContext _context;

        public Shipping_ReturnsModel(ILogger<Shipping_ReturnsModel> logger, DemoProjectContext context)
        {
            _logger = logger;
            _context = context;
        }
        public DynamicPage? ShippingReturnPage { get; set; }
        public void OnGet(int? id)
        {
            if (id == null)
            {
                // Handle the case when id is not provided
                // You could either redirect to a not found page or set a default value
                ShippingReturnPage = null;
            }
            else
            {
                ShippingReturnPage = _context.DynamicPages
                    .Where(x => x.IsDeleted == false && x.PageId == id)
                    .Select(x => new DynamicPage
                    {
                        PageId = x.PageId,
                        PageName = x.PageName,
                        PageContent = x.PageContent,
                        PageUrl = x.PageUrl,
                    })
                    .FirstOrDefault();

                if (ShippingReturnPage == null)
                {
                    // Handle the case when no page is found
                    // You could redirect or set an error message
                }
            }
        }

    }
}
