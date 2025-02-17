using ECommerceWeb.Model;
using ECommerceWeb.Pages.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;


namespace ECommerceWeb.Pages.Admin
{
    public class OrderItemModel : PageModel
    {
        private readonly DemoProjectContext _context;

        public OrderItemModel(DemoProjectContext context)
        {
            _context = context;
        }

        public IEnumerable<OrderItem> datalist { get; set; } //this is for list
        public IEnumerable<Product> product { get; set; } //this is for list

        [BindProperty]
        public OrderItem orderItem { get; set; }


        #region----GET METHOD-----
        public IActionResult OnGet(int? id)
        {
            datalist = _context.OrderItems.Where(e => e.IsDeleted == false).ToList();
            product = _context.Products.Where(e => e.IsDeleted == false).ToList();
            orderItem = new OrderItem();
            if (id.HasValue)
            {
                var co = _context.OrderItems.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
                if (co == null)
                {
                    return NotFound();
                }
                orderItem = co;
            }


            return Page();

        }
        #endregion


        #region----POST METHOD----
        public IActionResult OnPost()
        {
            if (orderItem.Id > 0)//Update
            {
                var co = _context.OrderItems.AsNoTracking().Where(e => e.Id == orderItem.Id && e.IsDeleted == false).FirstOrDefault();
                if (co == null)
                {
                    return NotFound();
                }


                orderItem.IsDeleted = co.IsDeleted;
                _context.OrderItems.Update(orderItem);
                _context.SaveChanges();
                TempData["info"] = "Your Data update Successfully";

            }
            else
            {
                try
                {
                     
                    orderItem.IsDeleted = false;
                    _context.OrderItems.Add(orderItem);
                    _context.SaveChanges();
                    TempData["success"] = "data saved";
                }
                catch (Exception ex) { }
            }
            return RedirectToPage();

        }

        #endregion

        #region---DELETE METHOD----

        public IActionResult OnPostDelete(int id)
        {
            var co = _context.OrderItems.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
            if (co == null)
            {
                return NotFound();
            }
            co.IsDeleted = true;
            _context.OrderItems.Update(co);
            _context.SaveChanges();
            TempData["success"] = "data deleted";
            return RedirectToPage();

        }

        #endregion
    }
}
