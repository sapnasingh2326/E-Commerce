using ECommerceWeb.Model;
using ECommerceWeb.Pages.Admin;
using ECommerceWeb.Pages.Metronic_Shop;
using ECommerceWeb.VModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace ECommerceWeb.Pages.Admin
{
    public class PaymentModel : PageModel
    {
        private readonly DemoProjectContext _context;

        public PaymentModel(DemoProjectContext context)
        {
            _context = context;
        }

        public IEnumerable<Payment> datalist { get; set; }

        public IEnumerable<Order> order { get; set; }
        public IEnumerable<OrderItemsVM> Orderitem { get; set; }
        public IEnumerable<OrderDetailsVM> OrderDetails { get; set; }
        
        [BindProperty]
        public Payment payment { get; set; }

        #region----GET METHOD-----
        public IActionResult OnGet(int? id)
        {
            // Fetching orders where IsDeleted is false
            var orders = _context.Orders.Where(e => e.IsDeleted == false).ToList();
            var orderDetails = _context.OrderDetails.Where(e => e.IsDeleted == false).ToList();

            Orderitem = _context.OrderItems.Where(x => x.OrderId == id).Select(x => new OrderItemsVM
            {
                Id = x.Id,
                ProductId = x.ProductId,
                
                ProductName = x.Product!.ProductName,
                CatgProductName = x.Product!.SubCategory.SubCategoryName,
                Price = x.Product.Price,
                SubPrice = (x.Product.Price) * (x.ItemQuantity),
                ProductKey = x.Product.ProductCode,
                Quantity = x.ItemQuantity,
                ShortDescription = x.Product.ShortDescription,
                //RegistrationId = id,
                //RegAddress =registration.Address,


            }).ToList();


            return Page();
        }

        #endregion

        #region--POST METHOD--
        public IActionResult OnPost()
        {
            if (payment.Id > 0)//Update
            {
                var co = _context.Payments.AsNoTracking().Where(e => e.Id == payment.Id && e.IsDeleted == false).FirstOrDefault();
                if (co == null)
                {
                    return NotFound();
                }


                payment.IsDeleted = co.IsDeleted;
                _context.Payments.Update(payment);
                _context.SaveChanges();
                TempData["info"] = "Your Data update Successfully";

            }
            else
            {
                try
                {

                    payment.IsDeleted = false;
                    _context.Payments.Add(payment);
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
            var co = _context.Payments.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
            if (co == null)
            {
                return NotFound();
            }
            co.IsDeleted = true;
            _context.Payments.Update(co);
            _context.SaveChanges();
            TempData["success"] = "data deleted";
            return RedirectToPage();

        }

        #endregion




























        //[BindProperty]
        //public Payment Payment { get; set; }

        //[BindProperty]
        //public IEnumerable<OrderItemsVM> OrderItems { get; set; }
        //public IEnumerable<Order> orders { get; set; }

        //#region----GET METHOD-----
        //public void OnGet(int? id)
        //{
        //    // Fetch order items related to the specific order
        //    if (id.HasValue)
        //    {
        //        orders = _context.OrderItems
        //                             .Where(o => o.OrderId == id && o.IsDeleted == false)
        //                             .Select(o => new OrderItemsVM
        //                             {
        //                                 Id = o.Id,
        //                                 OrderNumber = o.OrderId,
        //                                 Price = o.Product.Price,
        //                                 //TotalAmount = o.Order.OrderItems,
        //                                 Tax = o.Product.ToString,
        //                                 //ProductId = o.ProductId,
        //                                 //ProductKey = o.ProductPrice,
        //                                 //CatgProductName = o.CatgProductName,
        //                                 //ProductName = o.ProductName,
        //                                 //ShortDescription = o.ShortDescription,
        //                                 //Price = o.Price,
        //                                 //Quantity = o.Quantity,
        //                                 //SubPrice = o.Price * o.Quantity,
        //                                 //OrderNumber = o.OrderNumber,
        //                                 //TotalAmount = o.TotalAmount,
        //                                 //RegistrationId = o.RegistrationId,
        //                                 //RegAddress = o.RegAddress,
        //                                 //Tax = o.Tax
        //                             })
        //                             .ToList();

        //        Payment = new Payment();
        //    }
        //}
        //#endregion

        //public async Task<IActionResult> OnPostAsync()
        //{
        //    if (!ModelState.IsValid)
        //    {
        //        return Page();
        //    }

        //    Payment.PaymentDate = DateTime.Now;
        //    Payment.TransactionId = Guid.NewGuid().ToString();

        //    _context.Payments.Add(Payment);
        //    await _context.SaveChangesAsync();

        //    return RedirectToPage("/PaymentSuccess", new { id = Payment.TransactionId });
        //}
    }
}


    



