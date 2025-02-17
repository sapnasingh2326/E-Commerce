using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceWeb.Pages.Metronic_Shop
{
    public class YourAddressesModel : PageModel
    {

        [BindProperty]
        public List<Address> AddressList { get; set; }

        [BindProperty]
        public Address NewAddress { get; set; }

        public void OnGet()
        {
            // Simulate fetching addresses from a database
            AddressList = new List<Address>
            {
                new Address { Id = 1, Name = "Home", Street = "123 Main St", City = "Whitefield", State = "KA", Zip = "560067" },
                new Address { Id = 2, Name = "Work", Street = "456 Elm St", City = "Chikkabanavara", State = "KA", Zip = "560090" },
                new Address { Id = 3, Name = "Default", Street = "123 Main St", City = "KantiPark", State = "MA", Zip = "426001" }
               
            };
        }

        public async Task<IActionResult> OnPostAddAsync()
        {
            // Simulate adding the new address to the database
            // In a real application, you would add the address to the database here

            AddressList.Add(NewAddress);
            return RedirectToPage();
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            // Simulate deleting the address from the database
            // In a real application, you would delete the address from the database here

            var address = AddressList.Find(a => a.Id == id);
            if (address != null)
            {
                AddressList.Remove(address);
            }
            return RedirectToPage();
        }

        public class Address
        {
            public int Id { get; set; }
            public string? Name { get; set; }
            public string? Street { get; set; }
            public string? City { get; set; }
            public string? State { get; set; }
            public string? Zip { get; set; }
        }
    }
}
