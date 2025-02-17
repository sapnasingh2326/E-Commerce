using ECommerceWeb.Model;
using ECommerceWeb.VModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ECommerceWeb.Pages
{
    public class LoginModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [BindProperty]
        public AppInfo appInfo { get; set; }


        private readonly DemoProjectContext _context;

        public LoginModel(DemoProjectContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            appInfo = _context.AppInfos.Where(e => e.Id == 2 && e.IsDeleted == false).FirstOrDefault();


            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var result = _context.Registrations
                .Where(s => s.Email == Username && s.Password == Password && s.IsDeleted == false)
                .FirstOrDefault();

            if (result != null)
            {

                HttpContext.Session.SetString("RegId", result.Id!.ToString());
                HttpContext.Session.SetString("RoleId", result!.RoleId!.ToString());
                HttpContext.Session.SetString("UserName", result.Name);

                // Debugging - Log session values
                int? regidDebug = HttpContext.Session.GetInt32("RegId");
                int? roleidDebug = HttpContext.Session.GetInt32("RoleId");

                Console.WriteLine($"RegId: {regidDebug}, RoleId: {roleidDebug}");

                return RedirectToPage("/admin/Dashboard");
            }
            else
            {
                TempData["errorMsg"] = "Invalid Credentials";
                return RedirectToPage("/Login");
            }
        }

     // -----Clear Session for Logout-------------

        public IActionResult OnPostLogout()
        {
            // Clear session variables
            HttpContext.Session.Clear();

            // Redirect to login page
            return RedirectToPage("/Login");
        }
    }
}
