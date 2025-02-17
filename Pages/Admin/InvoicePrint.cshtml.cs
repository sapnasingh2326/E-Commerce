using ECommerceWeb.Model;
using ECommerceWeb.VModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWeb.Pages.Admin
{
    public class InvoicePrintModel : PageModel
    {

        private readonly DemoProjectContext _context;
        public InvoicePrintModel(DemoProjectContext context)
        {
            _context = context;
        }


        //public IEnumerable<Registration> datalist { get; set; }

        [BindProperty]
        public Product product { get; set; }

        [BindProperty]
        public Order orderinfo { get; set; }
        public Registration registration { get; set; }

        [BindProperty]
        public IEnumerable<OrderItemsVM> orderProductList { get; set; }

        public IEnumerable <OrderSummaryVM> orderSummary { get; set; }

        public IEnumerable<SubCategory> subCategories { get; set; }
        public Registration  registrationsAD { get; set; }

        #region-----GET METHOD------
        public IActionResult OnGet(int? id)
        {
            //datalist = _context.Products.Where(e => e.IsDeleted == false).ToList();

            orderinfo = _context.Orders.Where(e => e.IsDeleted == false && e.Id == id).FirstOrDefault();

            registrationsAD = _context.Registrations.Where(e => e.IsDeleted == false && e.Id == orderinfo!.CustomerId).FirstOrDefault();
             
            orderProductList = _context.OrderItems.Where(x => x.OrderId == id).Select(x => new OrderItemsVM
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


            //registration = _context.Registrations.Where(e => e.IsDeleted == false && e.Id == id) .FirstOrDefault();

            //if (registration == null)
            //{
            //    return NotFound();
            //}


            //var address = _context.Registrations
            //    .Where(x => x.RoleId== id && x.IsDeleted == false)
            //    .Select(x => x.Address) 
            //    .FirstOrDefault();

            //if (address == null)
            //{

            //    return NotFound();
            //}


            //ViewData["Address"] = address;

            //return Page();




        }
        #endregion


        #region--------POST METHOD---------
        public IActionResult OnPost()
        {
            if (product.Id > 0)//Update
            {
                var co = _context.Products.AsNoTracking().Where(e => e.Id == product.Id && e.IsDeleted == false).FirstOrDefault();
                if (co == null)
                {
                    return NotFound();
                }


                product.IsDeleted = co.IsDeleted;
                _context.Products.Update(product);
                _context.SaveChanges();
                TempData["info"] = "Your Data update Successfully";

            }
            else
            {
                try
                {

                    product.IsDeleted = false;
                    _context.Products.Add(product);
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
            var co = _context.Products.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
            if (co == null)
            {
                return NotFound();
            }
            co.IsDeleted = true;
            _context.Products.Update(co);
            _context.SaveChanges();
            TempData["success"] = "data deleted";
            return RedirectToPage();

        }

        #endregion




    }
}


