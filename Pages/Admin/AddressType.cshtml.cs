using ECommerceWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceWeb.Pages.Admin
{
    public class AddressTypeModel : PageModel
    {
        private readonly DemoProjectContext _context;

        public AddressTypeModel(DemoProjectContext context)
        {
            _context = context;
        }
        [BindProperty]
        public AddressType addressType { get; set; }

        public List<AddressType> addressTypes { get; set; }

        public IActionResult OnGet()
        {
            return Page();
        }
        public IActionResult OnPostCreate()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }
            return RedirectToPage("./AddressType");
        }
        public IActionResult OnPostEdit(int id)
        {
            return RedirectToPage("./AddressType");

        }
        public IActionResult OnPostDelete(int id)
        {
            return RedirectToPage("./AddressType");
        }
    }
}

