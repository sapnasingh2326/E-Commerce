using ECommerceWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWeb.Pages.Admin
{
    public class OrderModel : PageModel
    {
        private readonly DemoProjectContext _context;

        public OrderModel (DemoProjectContext context)
        {
            _context = context;
        }

        public IEnumerable<Order> datalist { get; set; } //this is for list


        [BindProperty]
        public Order order { get; set; }


        #region----GET METHOD-----
        public IActionResult OnGet(int? id)
        {
            datalist = _context.Orders.Where(e => e.IsDeleted == false).ToList();
            order = new Order();
            if (id.HasValue)
            {
                var co = _context.Orders.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
                if (co == null)
                {
                    return NotFound();
                }
                order = co;
            }


            return Page();

        }
        #endregion


        #region--POST METHOD--
        public IActionResult OnPost()
        {
            if (order.Id > 0)//Update
            {
                var co = _context.Orders.AsNoTracking().Where(e => e.Id == order.Id && e.IsDeleted == false).FirstOrDefault();
                if (co == null)
                {
                    return NotFound();
                }


                order.IsDeleted = co.IsDeleted;
                _context.Orders.Update(order);
                _context.SaveChanges();
                TempData["info"] = "Your Data update Successfully";

            }
            else
            {
                try
                {

                    order.IsDeleted = false;
                    _context.Orders.Add(order);
                    _context.SaveChanges();
                    TempData["success"] = "data saved";
                }
                catch (Exception ex) { }
            }
            return RedirectToPage();

        }

        #endregion

        #region---DELETE METHOD--

        public IActionResult OnPostDelete(int id)
        {
            var co = _context.Orders.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
            if (co == null)
            {
                return NotFound();
            }
            co.IsDeleted = true;
            _context.Orders.Update(co);
            _context.SaveChanges();
            TempData["success"] = "data deleted";
            return RedirectToPage();

        }

        #endregion



    }
}


