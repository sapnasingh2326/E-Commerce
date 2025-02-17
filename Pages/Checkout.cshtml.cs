using ECommerceWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text.Json;

namespace ECommerceWeb.Pages
{
    public class CheckoutModel : PageModel
    {
        private readonly DemoProjectContext _context;


        [BindProperty]
        public string CartItems { get; set; }

        public List<CartItem> OrderItems { get; set; }


        public CheckoutModel(DemoProjectContext context)
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



            Order order = new Order();
            order.OrderStatus = 1;
            order.OrderDate = DateTime.Now;
            order.OrderNumber = GenerateOrderNumber(10); 
            order.CustomerId = 1;
            order.IsDeleted = false;
            _context.Orders.Add(order);
            _context.SaveChanges();


            var OrderId = _context.Orders.Where(x=>x.OrderNumber== order.OrderNumber).Select(x => x.Id).FirstOrDefault();   

            foreach(var itm in OrderItems)
            {
                OrderItem orderitem = new OrderItem();
                orderitem.ItemQuantity = itm.quantity;
                orderitem.ProductId = itm.productid;
                orderitem.OrderId = OrderId;
                orderitem.IsDeleted = false;
                _context.OrderItems.Add(orderitem);
                 _context.SaveChanges(); 


            }

            // Process the order (e.g., save to database)
            // ...

            // Clear the cart after successful order processing
            CartItems = string.Empty;

            return RedirectToPage("OrderConfirmation");
        }



        static string GenerateOrderNumber(int length)
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
            public int? productid { get; set;}
            public int? customerId { get; set;}

        }


    }
}
