using ECommerceWeb.Model;
using ECommerceWeb.VModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWeb.Pages.Metronic_Shop
{
    public class OrderModel : PageModel
    {
        private readonly DemoProjectContext _context;

        public OrderModel(DemoProjectContext context)
        {
            _context = context;
        }

        public IEnumerable<Order> OrderItems { get; set; } // This is for list of order items
        public IEnumerable<Product> Products { get; set; } // This is for Product

        [BindProperty]
        public Order Order { get; set; }

        #region----GET METHOD-----
        public IActionResult OnGet(int? id)
        {
            Products = _context.Products.Where(e => e.IsDeleted == false).ToList();

            OrderItems = _context.Orders.Where(e => e.IsDeleted == false).Select(e => new Order
            {
                Id = e.Id,
                CustomerId = e.CustomerId,
                OrderNumber = e.OrderNumber, 
                OrderDate = e.OrderDate,
                OrderStatus = e.OrderStatus,
            }).ToList();

            Order = new Order();
            if (id.HasValue)
            {
                var orderItem = _context.Orders.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
                if (orderItem == null)
                {
                    return NotFound();
                }
                Order = orderItem;
            }

            return Page();
        }
        #endregion

        #region--POST METHOD--
        public IActionResult OnPost()
        {
            if (Order.Id > 0) // Update
            {
                var existingOrderItem = _context.Orders.AsNoTracking().Where(e => e.Id == Order.Id && e.IsDeleted == false).FirstOrDefault();
                if (existingOrderItem == null)
                {
                    return NotFound();
                }

                Order.IsDeleted = existingOrderItem.IsDeleted;
                _context.Orders.Update(Order);
                _context.SaveChanges();
                TempData["info"] = "Your data updated successfully.";
            }
            else
            {
                try
                {
                    Order.IsDeleted = false;
                    _context.Orders.Add(Order);
                    _context.SaveChanges();
                    TempData["success"] = "Data saved.";
                }
                catch (Exception ex)
                {
                    // Log the exception
                    TempData["error"] = "An error occurred while saving data.";
                }
            }
            return RedirectToPage();
        }
        #endregion

        #region---DELETE METHOD---
        public IActionResult OnPostDelete(int id)
        {
            var orderItem = _context.Orders.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
            if (orderItem == null)
            {
                return NotFound();
            }
            orderItem.IsDeleted = true;
            _context.Orders.Update(orderItem);
            _context.SaveChanges();
            TempData["success"] = "Data deleted.";
            return RedirectToPage();
        }
        #endregion
    }
}
