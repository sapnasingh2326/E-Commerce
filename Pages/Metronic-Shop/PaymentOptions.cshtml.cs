using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ECommerceWeb.Pages.Metronic_Shop
{
    public class PaymentOptionsModel : PageModel
    {
        [BindProperty]
        public List<PaymentMethod> PaymentList { get; set; } = new List<PaymentMethod>();

        [BindProperty]
        public PaymentMethod NewPayment { get; set; }

        public void OnGet()
        {
            // Sample data, replace with actual data retrieval logic
            PaymentList = new List<PaymentMethod>
            {
                new PaymentMethod { Id = 1, CardHolderName = "Sidhart Malhotra", Last4Digits = "1234", ExpiryMonth = "12", ExpiryYear = "2024" },
                new PaymentMethod { Id = 2, CardHolderName = "Aanand Shroff", Last4Digits = "5678", ExpiryMonth = "06", ExpiryYear = "2025" },
                new PaymentMethod { Id = 3, CardHolderName = "Honey Bee", Last4Digits = "4233", ExpiryMonth = "26", ExpiryYear = "2021"},
                new PaymentMethod { Id = 4, CardHolderName = "Parmish Verma", Last4Digits = "8654", ExpiryMonth = "27", ExpiryYear = "2028"},
                new PaymentMethod { Id = 5, CardHolderName = "NeerajChopra", Last4Digits = "3113", ExpiryMonth = "26", ExpiryYear = "2020"},
                new PaymentMethod { Id = 6, CardHolderName = "Jennifer Winget", Last4Digits = "6533", ExpiryMonth = "54", ExpiryYear = "2020"},
                new PaymentMethod { Id = 7, CardHolderName = "Zain Imam", Last4Digits = "9773", ExpiryMonth = "22", ExpiryYear = "2024"},
                new PaymentMethod { Id = 8, CardHolderName = "Roshani Jha", Last4Digits = "0965", ExpiryMonth = "23", ExpiryYear = "2097"},
                new PaymentMethod { Id = 9, CardHolderName = "Geni Gony", Last4Digits = "4334", ExpiryMonth = "01", ExpiryYear = "2027"},
                new PaymentMethod { Id =10, CardHolderName = "Manisha Jha", Last4Digits = "5532", ExpiryMonth = "15", ExpiryYear = "2098"}


            };
        }

        public IActionResult OnPostAdd()
        {
            if (ModelState.IsValid)
            {
                // Logic to add the new payment method to the database or in-memory list
                PaymentList.Add(new PaymentMethod
                {
                    Id = PaymentList.Count + 1, // Sample ID generation logic
                    CardHolderName = NewPayment.CardHolderName,
                    Last4Digits = NewPayment.CardNumber[^4..],
                    ExpiryMonth = NewPayment.ExpiryMonth,
                    ExpiryYear = NewPayment.ExpiryYear
                });

                // Reset the NewPayment property to clear the form
                NewPayment = new PaymentMethod();
            }

            return Page();
        }

        public IActionResult OnPostDelete(int id)
        {
            // Logic to delete the payment method from the database or in-memory list
            var paymentMethod = PaymentList.Find(p => p.Id == id);
            if (paymentMethod != null)
            {
                PaymentList.Remove(paymentMethod);
            }

            return Page();
        }
    }

    public class PaymentMethod
    {
        public int Id { get; set; }

        [Required]
        public string CardHolderName { get; set; }

        [Required]
        [CreditCard]
        public string CardNumber { get; set; }

        [Required]
        [Range(1, 12)]
        public string ExpiryMonth { get; set; }

        [Required]
        [Range(2022, 2030)]
        public string ExpiryYear { get; set; }

        [Required]
        [StringLength(4, MinimumLength = 3)]
        public string Cvv { get; set; }

        // Additional properties to display masked card number
        public string Last4Digits { get; set; }
    }
}
