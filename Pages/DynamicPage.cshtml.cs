using ECommerceWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceWeb.Pages
{
    public class DynamicPageModel : PageModel
    {
        private readonly ILogger<DynamicPageModel> _logger;
        private readonly DemoProjectContext _context;

        public DynamicPageModel(ILogger<DynamicPageModel> logger, DemoProjectContext context)
        {
            _logger = logger;
            _context = context;
        }
        public DynamicPage? CustomerServicedynamicPage { get; set; }
        public void OnGet(int? id)
        {
            //------------CustomerService DynamicPage-------------

            
            CustomerServicedynamicPage = _context.DynamicPages.Where(x => x.IsDeleted == false && x.PageId == id).Select(x => new DynamicPage
            {
                PageId = x.PageId,
                PageName = x.PageName,
                PageContent = x.PageContent,
                PageUrl = x.PageUrl,
            }).FirstOrDefault();
        }
    }
}
