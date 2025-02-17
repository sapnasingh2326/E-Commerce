using ECommerceWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceWeb.Pages
{
    public class DynamicPageDetailsModel : PageModel
    {
        private readonly DemoProjectContext _context;

        public DynamicPageDetailsModel(DemoProjectContext context)
        {
            _context = context;
        }

        public DynamicPage DynamicPage { get; set; }

        public IActionResult OnGet(int id)
        {
            DynamicPage = _context.DynamicPages.FirstOrDefault(p => p.PageId == id && p.IsDeleted == false);

            if (DynamicPage == null)
            {
                return NotFound();
            }

            return Page();
        }
    }
}
