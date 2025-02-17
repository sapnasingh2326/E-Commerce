using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceWeb.Pages.Metronic_Shop
{
    public class PrimeModel : PageModel
    {
        [BindProperty]
        public bool IsPrimeMember { get; set; }

        public void OnGet()
        {
            // Simulate fetching user details from a database
            // For demonstration, let's assume the user is not a Prime member
            IsPrimeMember = false;
        }

        public async Task<IActionResult> OnPostJoinPrimeAsync()
        {
            // Simulate updating the user's Prime membership status in the database
            // For demonstration, let's assume the operation is successful

            IsPrimeMember = true;

            // You would typically update the database here
            // e.g., await _database.UpdateUserPrimeStatusAsync(UserId, true);

            return RedirectToPage();
        }
    }
}

