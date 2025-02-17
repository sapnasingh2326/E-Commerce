using ECommerceWeb.Model;
using ECommerceWeb.VModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWeb.Pages.Admin
{
    public class CartModel : PageModel
    {
        private readonly DemoProjectContext _context;

        public CartModel (DemoProjectContext context)
        {
            _context = context;
        }

        public IEnumerable<WishlistVM> CartItems { get; set; } // This is for list of cart items
        public IEnumerable<Product> Products { get; set; } // This is for Product

        [BindProperty]
        public Cart Cart { get; set; }

        #region----GET METHOD-----
        public IActionResult OnGet(int? id)
        {
            Products = _context.Products.Where(e => e.IsDeleted == false).ToList();

            CartItems = _context.Carts.Where(e => e.IsDeleted == false).Select(e => new WishlistVM
            {
                Id = e.Id,
                ProductName = e.Product.ProductName,
                Price = e.Product.Price,
                Quantity = e.Quality,
                RegistrationName = e.Registration.Name,
            }).ToList();

            Cart = new Cart();
            if (id.HasValue)
            {
                var cartItem = _context.Carts.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
                if (cartItem == null)
                {
                    return NotFound();
                }
                Cart = cartItem;
            }

            return Page();
        }
        #endregion

        #region--POST METHOD--
        public IActionResult OnPost()
        {
            if (Cart.Id > 0) // Update
            {
                var existingCartItem = _context.Carts.AsNoTracking().Where(e => e.Id == Cart.Id && e.IsDeleted == false).FirstOrDefault();
                if (existingCartItem == null)
                {
                    return NotFound();
                }

                Cart.IsDeleted = existingCartItem.IsDeleted;
                _context.Carts.Update(Cart);
                _context.SaveChanges();
                TempData["info"] = "Your data updated successfully.";
            }
            else
            {
                try
                {
                    Cart.IsDeleted = false;
                    _context.Carts.Add(Cart);
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
            var cartItem = _context.Carts.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
            if (cartItem == null)
            {
                return NotFound();
            }
            cartItem.IsDeleted = true;
            _context.Carts.Update(cartItem);
            _context.SaveChanges();
            TempData["success"] = "Data deleted.";
            return RedirectToPage();
        }
        #endregion
    }
}
