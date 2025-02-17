 using ECommerceWeb.Model;
using ECommerceWeb.VModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
namespace ECommerceWeb.Pages.Admin
{
    public class WishlistModel : PageModel
    {
        private readonly DemoProjectContext _context;

        public WishlistModel(DemoProjectContext context)
        {
            _context = context;
        }

        // For displaying the list of WishList items
        public IEnumerable<Wishlist> WishListItems { get; set; }

        // List of products for the dropdown
        public IEnumerable<Product> Products { get; set; }

        // List of products to bind in the dropdown
        public IEnumerable<ProductListVM> ProductsList { get; set; }

        // For binding WishList form data
        [BindProperty]
        public Wishlist WishListItem { get; set; }

        #region ---- GET METHOD -----
        public IActionResult OnGet(int? id)
        {
            // Retrieve and map products into ProductListVM
            ProductsList = _context.Products
                .Where(p => p.IsDeleted == false)
                .Select(p => new ProductListVM
                {
                    ProductCode = p.ProductCode,
                    ProductName = p.ProductName,
                    ProductPrice = p.Price,
                    
                })
                .ToList();

            // Fetch all non-deleted WishList items
            WishListItems = _context.Wishlists.Where(w => w.IsDeleted == false).ToList();

            // Fetch all non-deleted products for the dropdown
            Products = _context.Products.Where(p => p.IsDeleted == false).ToList();

            WishListItem = new Wishlist();

            // If id is passed, fetch the specific WishList item for editing
            if (id.HasValue)
            {
                var item = _context.Wishlists.Where(w => w.Id == id && w.IsDeleted == false).FirstOrDefault();
                if (item == null)
                {
                    return NotFound();
                }
                WishListItem = item;
            }

            return Page();
        }
        #endregion

        #region ---- POST METHOD -----
        public IActionResult OnPost()
        {
            // Check if we are updating an existing WishList item
            if (WishListItem.Id > 0)
            {
                _context.Wishlists.Update(WishListItem);
                _context.SaveChanges();
                TempData["info"] = "Your wishlist item has been updated successfully.";
            }
            else // Creating a new WishList item
            {
                try
                {
                    WishListItem.IsDeleted = false; // Ensure it's not marked as deleted
                    _context.Wishlists.Add(WishListItem);
                    _context.SaveChanges();
                    TempData["success"] = "Wishlist item saved successfully.";
                }
                catch (Exception ex)
                {
                    // Handle exception if any
                }
            }

            return RedirectToPage();
        }
        #endregion

        #region ---- DELETE METHOD -----
        public IActionResult OnPostDelete(int id)
        {
            var item = _context.Wishlists.Where(w => w.Id == id && w.IsDeleted == false).FirstOrDefault();
            if (item == null)
            {
                return NotFound();
            }
            item.IsDeleted = true; // Soft delete
            _context.Wishlists.Update(item);
            _context.SaveChanges();
            TempData["success"] = "Wishlist item deleted successfully.";
            return RedirectToPage();
        }
        #endregion
    }
}