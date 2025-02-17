using ECommerceWeb.Model;
using ECommerceWeb.VModel;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Linq;

namespace ECommerceWeb.Pages.Shared
{
    public class DynamicPageListModel : PageModel
    {
        private readonly DemoProjectContext _context;
        public IEnumerable<DynamicpageVM> dynamicPages { get; set; }

        public DynamicPageListModel(DemoProjectContext context)
        {
            _context = context;
        }

        public void OnGet()
        {

        }
    }

}
