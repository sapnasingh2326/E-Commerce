using ECommerceWeb.Model;
using ECommerceWeb.VModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWeb.Pages.Admin
{
    public class CreateInvoiceModel : PageModel
    {
        private readonly DemoProjectContext _context;

        public CreateInvoiceModel (DemoProjectContext context)
        {
            _context = context;
        }


        [BindProperty]
        public Product Product { get; set; }

        [BindProperty]
        public Order Order { get; set; }

        public Registration Registration { get; set; }

        [BindProperty]
        public IEnumerable<OrderItemsVM> OrderProductList { get; set; }

        public IEnumerable<OrderSummaryVM> OrderSummary { get; set; }

        public IEnumerable<SubCategory> SubCategories { get; set; }
        public IEnumerable<RegistrationVM> Registrations { get; set; }

        public IActionResult OnGet(int? id)
        {
            //if (id == null)
            //{
            //    return NotFound();
            //}

            Order = _context.Orders.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
            if (Order == null)
            {
                return NotFound();
            }

            Registration = _context.Registrations.FirstOrDefault(e => e.IsDeleted == false && e.Id == Order.CustomerId);

            OrderProductList = _context.OrderItems
                .Where(x => x.OrderId == id)
                .Select(x => new OrderItemsVM
                {
                    Id = x.Id,
                    ProductId = x.ProductId,
                    ProductName = x.Product!.ProductName,
                    CatgProductName = x.Product!.SubCategory.SubCategoryName,
                    Price = x.Product.Price,
                    SubPrice = x.Product.Price * x.ItemQuantity,
                    ProductKey = x.Product.ProductCode,
                    Quantity = x.ItemQuantity,
                    ShortDescription = x.Product.ShortDescription
                }).ToList();

            return Page();
        }

        public IActionResult OnPost()
        {
            if (Product != null && Product.Id > 0)
            {
                var existingProduct = _context.Products.AsNoTracking().FirstOrDefault(e => e.Id == Product.Id && e.IsDeleted == false);
                if (existingProduct == null)
                {
                    return NotFound();
                }

                Product.IsDeleted = existingProduct.IsDeleted;
                _context.Products.Update(Product);
                _context.SaveChanges();
                TempData["info"] = "Your Data was updated successfully";
            }
            else
            {
                try
                {
                    Product.IsDeleted = false;
                    _context.Products.Add(Product);
                    _context.SaveChanges();
                    TempData["success"] = "Data saved successfully";
                }
                catch
                {
                    TempData["error"] = "An error occurred while saving the data";
                }
            }
            return RedirectToPage();
        }

        public IActionResult OnPostDelete(int id)
        {
            var product = _context.Products.FirstOrDefault(e => e.Id == id && e.IsDeleted == false);
            if (product == null)
            {
                return NotFound();
            }
            product.IsDeleted = true;
            _context.Products.Update(product);
            _context.SaveChanges();
            TempData["success"] = "Data deleted successfully";
            return RedirectToPage();
        }
    }
}
