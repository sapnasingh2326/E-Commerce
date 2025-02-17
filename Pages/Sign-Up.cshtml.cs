using ECommerceWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Reflection;

namespace ECommerceWeb.Pages
{
    public class Sign_UpModel : PageModel
    {
        [BindProperty]
        public string Name { get; set; }

        [BindProperty]
        public string Email { get; set; }

        [BindProperty]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [BindProperty]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        [BindProperty]
        public string Mobile { get; set; }

        [BindProperty]
        public string Address { get; set; }

        [BindProperty]
        public string PostCode { get; set; }

        private readonly DemoProjectContext _context;

        public Sign_UpModel(DemoProjectContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
            // This method is executed when the page is loaded via a GET request.
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                TempData["errorMsg"] = "Invalid input.";
                return RedirectToPage("/SignUp");
            }

            if (Password != ConfirmPassword)
            {
                TempData["errorMsg"] = "Passwords do not match.";
                return RedirectToPage("/SignUp");
            }

            var existingUser = _context.Registrations
                .Where(s => s.Email == Email)
                .FirstOrDefault();

            if (existingUser != null)
            {
                TempData["errorMsg"] = "Email is already registered.";
                return RedirectToPage("/Sign-Up");
            }

            var newUser = new Registration
            {
                Name = Name,
                Email = Email,
                Password = Password,
                Mobile = Mobile,
                Address = Address,
                PostCode = PostCode,
                IsDeleted = false,
                CreatedDate = DateTime.Now,
                RoleId = 8 // Assuming 8 is the role ID for customers
            };

            _context.Registrations.Add(newUser);
            await _context.SaveChangesAsync();

            HttpContext.Session.SetString("RegId", newUser.Id!.ToString());
            HttpContext.Session.SetString("RoleId", newUser!.RoleId!.ToString());
            HttpContext.Session.SetString("UserName", newUser.Name);
              
            TempData["successMsg"] = "Successfully registered.";
            return RedirectToPage("/Sign-Up");
        }

        // -----Clear Session for Logout---------------
        public IActionResult OnPostLogout()
        {
            HttpContext.Session.Clear();
            return RedirectToPage("/Login");
        }
    }
}
