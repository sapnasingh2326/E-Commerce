using ECommerceWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceWeb.Pages.Shared
{
    public class DeliveryInformationModel : PageModel
    {
        private readonly ILogger<DeliveryInformationModel> _logger;
        private readonly DemoProjectContext _context;

        public DeliveryInformationModel(ILogger<DeliveryInformationModel> logger, DemoProjectContext context)
        {
            _logger = logger;
            _context = context;
        }
        public DynamicPage? DeliveryInformationPage { get; set; }
        public void OnGet(int? id)
        {
            //------------DeliveryInformation DynamicPage-------------


            DeliveryInformationPage = _context.DynamicPages.Where(x => x.IsDeleted == false && x.PageId == id).Select(x => new DynamicPage
            {
                PageId = x.PageId,
                PageName = x.PageName,
                PageContent = x.PageContent,
                PageUrl = x.PageUrl,
            }).FirstOrDefault();
        }
    }
}
