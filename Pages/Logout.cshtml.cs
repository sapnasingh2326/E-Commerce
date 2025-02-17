using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceWeb.Pages
{
    public class LogoutModel : PageModel
    {
        public class AccountController : Controller
        {
            [HttpPost]
            [ValidateAntiForgeryToken]
            public async Task<IActionResult> Logout(string userType)
            {
                if (userType == "Admin" || userType == "Customer")
                {
                    await HttpContext.SignOutAsync(); // Sign out the user
                    return Ok(); // Return a 200 OK response
                }
                return BadRequest(); // Return a 400 Bad Request response if userType is invalid
            }
        }
    }
}