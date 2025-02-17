using ECommerceWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceWeb.Pages.Shared
{
    public class PrivacyPolicyModel : PageModel
    {
        private readonly ILogger<PrivacyPolicyModel> _logger;
        private readonly DemoProjectContext _context;

        public PrivacyPolicyModel(ILogger<PrivacyPolicyModel> logger, DemoProjectContext context)
        {
            _logger = logger;
            _context = context;
        }

        public DynamicPage? PrivacyPolicydynamicPage { get; set; }  // Updated variable name

        public void OnGet(int? id)
        {
            // Removed the redundant call and fixed the logical error
            PrivacyPolicydynamicPage = _context.DynamicPages.Where(x => x.IsDeleted == false && x.PageId == id).Select(x => new DynamicPage
            {
                PageId = x.PageId,
                PageName = x.PageName,
                PageContent = x.PageContent,
                PageUrl = x.PageUrl,
            }).FirstOrDefault();
        }
    }
}
