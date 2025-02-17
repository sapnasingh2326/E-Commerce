using ECommerceWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Text.Json;

namespace ECommerceWeb.Pages.Metronic_Shop
{
    public class MyAccountModel : PageModel
    {
        [BindProperty]
        public string Username { get; set; }

        [BindProperty]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [BindProperty]
        public string CartItems { get; set; }

        public List<CartItem> OrderItems { get; set; }

        private readonly DemoProjectContext _context;

        public MyAccountModel (DemoProjectContext context)
        {
            _context = context;
        }

        public void OnGet()
        {
        }

        public IActionResult OnPost()
        {
            if (string.IsNullOrEmpty(CartItems))
            {
                return Page();
            }

            OrderItems = JsonSerializer.Deserialize<List<CartItem>>(CartItems);

            Order order = new Order
            {
                OrderStatus = 1,
                OrderDate = DateTime.Now,
                OrderNumber = GenerateOrderNumber(10),
                CustomerId = 1,
                IsDeleted = false
            };
            _context.Orders.Add(order);
            _context.SaveChanges();

            var OrderId = _context.Orders
                                  .Where(x => x.OrderNumber == order.OrderNumber)
                                  .Select(x => x.Id)
                                  .FirstOrDefault();

            foreach (var itm in OrderItems)
            {
                OrderItem orderitem = new OrderItem
                {
                    ItemQuantity = itm.quantity,
                    ProductId = itm.productid,
                    OrderId = OrderId,
                    IsDeleted = false
                };
                _context.OrderItems.Add(orderitem);
                _context.SaveChanges();
            }

            CartItems = string.Empty;

            return RedirectToPage("OrderConfirmation");
        }

        private static string GenerateOrderNumber(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            Random random = new Random();
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        }

        public class CartItem
        {
            public string? image { get; set; }
            public string? name { get; set; }
            public string? price { get; set; }
            public int? quantity { get; set; }
            public int? productid { get; set; }
            public int? customerId { get; set; }
        }
    }
}
