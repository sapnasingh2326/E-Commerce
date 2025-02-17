 using ECommerceWeb.Model;
using ECommerceWeb.Pages.Admin;
using ECommerceWeb.VModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Text.Json;

namespace ECommerceWeb.Pages.Metronic_Shop
{
    public class YourOrdersModel : PageModel
    {
        private readonly DemoProjectContext _context;
         
        public YourOrdersModel(DemoProjectContext context)
        {
            _context = context;
        }
        public IEnumerable<Order> datalist { get; set; } //this is for list


        [BindProperty]
        public Order order { get; set; }
        public OrderDetail detail { get; set; }
        public OrderItem item { get; set; }

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



































//        public IEnumerable<Order> datalist { get; set; }

//        [BindProperty]
//        public Order order { get; set; }

//        #region----GET METHOD-----
//        public IActionResult OnGet(int? id)
//        {
//            datalist = _context.Orders.Where(e => e.IsDeleted == false).ToList();
//            order = new Order();
//            if (id.HasValue)
//            {
//                var co = _context.Orders.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
//                if (co == null)
//                {
//                    return NotFound();
//                }
//                order = co;
//            }

//            return Page();
//        }
//        #endregion

//        #region--POST METHOD--
//        public IActionResult OnPost()
//        {
//            if (order.Id > 0)
//            {
//                var co = _context.Orders.AsNoTracking().Where(e => e.Id == order.Id && e.IsDeleted == false).FirstOrDefault();
//                if (co == null)
//                {
//                    return NotFound();
//                }

//                order.IsDeleted = co.IsDeleted;
//                _context.Orders.Update(order);
//                _context.SaveChanges();
//                TempData["info"] = "Your Data update Successfully";
//            }
//            else
//            {
//                try
//                {
//                    order.IsDeleted = false;
//                    _context.Orders.Add(order);
//                    _context.SaveChanges();
//                    TempData["success"] = "data saved";
//                }
//                catch (Exception ex)
//                {
//                    // Handle exception
//                }
//            }
//            return RedirectToPage();
//        }
//        #endregion

//        #region---DELETE METHOD---
//        public IActionResult OnPostDelete(int id)
//        {
//            var co = _context.Orders.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
//            if (co == null)
//            {
//                return NotFound();
//            }
//            co.IsDeleted = true;
//            _context.Orders.Update(co);
//            _context.SaveChanges();
//            TempData["success"] = "data deleted";
//            return RedirectToPage();
//        }
//        #endregion

//        #region---API CALL FOR ORDER DETAILS-----
//        [Route("Api/GetYourOrdersForPopup")]
//        public JsonResult GetYourOrdersForPopup(int YourOrderId)
//        {
//            var result = _context.Orders
//                .Where(x => x.Id == YourOrderId)
//                .Select(x => new OrderDetailsVM
//                {
//                    CustomerId = x.CustomerId,
//                    Id = x.Id,
//                    OrderDate = x.OrderDate,
//                    OrderNumber = x.OrderNumber,
//                    OrderStatus = x.OrderStatus,
//                    orderItems = x.OrderItems.Select(y => new OrderItemsList
//                    {
//                        Id = y.Id,
//                        Price = y.Product.Price,
//                        ProductId = y.ProductId,
//                        ProductName = y.Product.ProductName,
//                        Quantity = y.ItemQuantity
//                    }).ToList()
//                })
//                .FirstOrDefault();

//            return new JsonResult(result);
//        }

//        #endregion
//    }
//}













//[BindProperty]
//public string Orders { get; set; }

//public List<Order> OrderItems { get; set; }




//public void OnGet()
//{
//}


//public IActionResult OnPost()
//{
//    if (string.IsNullOrEmpty(Orders))
//    {
//        return Page();
//    }


//    OrderItems = JsonSerializer.Deserialize<List<Order>>(Orders);



//    Order order = new Order();
//    order.OrderStatus = 1;
//    order.OrderDate = DateTime.Now;
//    order.OrderNumber = GenerateOrderNumber(10);
//    order.CustomerId = 1;
//    order.IsDeleted = false;
//    _context.Orders.Add(order);
//    _context.SaveChanges();


//    var OrderId = _context.Orders.Where(x => x.OrderNumber == order.OrderNumber).Select(x => x.Id).FirstOrDefault();

//    foreach (var itm in OrderItems)
//    {
//        OrderItem orderitem = new OrderItem();
//        order.Id = order.Id;
//        //order.CustomerId += itm.Id;
//        //order.OrderNumber += itm.Id;
//        //order.OrderDate orderDate = DateTime.Now;
//        //order.OrderDate = orderDate;
//        //order.OrderNumber = order.OrderNumber;
//        order.CustomerId = order.CustomerId;
//        order.OrderNumber = order.OrderNumber;
//        order.OrderDate = order.OrderDate;
//        order.OrderStatus = order.OrderStatus;
//        order.OrderItems.Add(orderitem);

//        orderitem.IsDeleted = false;
//        _context.OrderItems.Add(orderitem);
//        _context.SaveChanges();


//    }

//    // Process the order (e.g., save to database)
//    // ...

//    // Clear the cart after successful order processing
//    Orders = string.Empty;

//    return RedirectToPage("OrderConfirmation");
//}



//static string GenerateOrderNumber(int length)
//{
//    const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
//    Random random = new Random();
//    return new string(Enumerable.Repeat(chars, length)
//        .Select(s => s[random.Next(s.Length)]).ToArray());
//}

//public class Order
//{
//    public int? Id { get; set; }
//    public string? CustomerName { get; set; }
//    public string? OrderNumber { get; set; }
//    public string? OrderDate { get; set; }
//    public string? OrderStatus { get; set; }


//}

















