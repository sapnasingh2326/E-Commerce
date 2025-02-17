using ECommerceWeb.Model;
using ECommerceWeb.VModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWeb.Pages
{
    public class MyWishlistModel : PageModel
    {
        private readonly DemoProjectContext _context;

        public MyWishlistModel(DemoProjectContext context)
        {
            _context = context;
        }

        //        public IEnumerable<WishlistVM> Datalist { get; set; } // This is for list
        //        public IEnumerable<Product> Products { get; set; } // This is for Product

        //        [BindProperty]
        //        public Wishlist Wishlist { get; set; }

        //        #region----GET METHOD-----
        //        public IActionResult OnGet(int? id)
        //        {
        //            Products = _context.Products.Where(e => e.IsDeleted == false).ToList();

        //            Datalist = _context.Wishlists.Where(e => e.IsDeleted == false).Select(e => new WishlistVM
        //            {
        //                Id = e.Id,
        //                ProductName = e.Product.ProductName,
        //                Price = e.Product.Price,
        //                Quantity = e.Product.Quality,
        //                RegistrationName = e.Registration.Name,
        //            }).ToList();

        //            Wishlist = new Wishlist();
        //            if (id.HasValue)
        //            {
        //                var wishlistItem = _context.Wishlists.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
        //                if (wishlistItem == null)
        //                {
        //                    return NotFound();
        //                }
        //                Wishlist = wishlistItem;
        //            }

        //            return Page();
        //        }
        //        #endregion

        //        #region--POST METHOD--
        //        public IActionResult OnPost()
        //        {
        //            if (Wishlist.Id > 0) // Update
        //            {
        //                var existingWishlistItem = _context.Wishlists.AsNoTracking().Where(e => e.Id == Wishlist.Id && e.IsDeleted == false).FirstOrDefault();
        //                if (existingWishlistItem == null)
        //                {
        //                    return NotFound();
        //                }

        //                Wishlist.IsDeleted = existingWishlistItem.IsDeleted;
        //                _context.Wishlists.Update(Wishlist);
        //                _context.SaveChanges();
        //                TempData["info"] = "Your data updated successfully.";
        //            }
        //            else
        //            {
        //                try
        //                {
        //                    Wishlist.IsDeleted = false;
        //                    _context.Wishlists.Add(Wishlist);
        //                    _context.SaveChanges();
        //                    TempData["success"] = "Data saved.";
        //                }
        //                catch (Exception ex)
        //                {
        //                    // Log the exception
        //                    TempData["error"] = "An error occurred while saving data.";
        //                }
        //            }
        //            return RedirectToPage();
        //        }
        //        #endregion

        //        #region---DELETE METHOD---
        //        public IActionResult OnPostDelete(int id)
        //        {
        //            var wishlistItem = _context.Wishlists.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
        //            if (wishlistItem == null)
        //            {
        //                return NotFound();
        //            }
        //            wishlistItem.IsDeleted = true;
        //            _context.Wishlists.Update(wishlistItem);
        //            _context.SaveChanges();
        //            TempData["success"] = "Data deleted.";
        //            return RedirectToPage();
        //        }
        //        #endregion
        //    }
        //}


        public IEnumerable<WishlistVM> Datalist { get; set; }

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
                    ProductColor = p.ProductCode,
                    SubCategoryName = p.ProductName,
                   


                })
                .ToList();

              Products = _context.Products.Where(e => e.IsDeleted == false).ToList();

                       Datalist = _context.Wishlists.Where(e => e.IsDeleted == false).Select(e => new WishlistVM
                       {
                          Id = e.Id,
                          ProductName = e.Product.ProductName,
                          Price = e.Product.Price,
                          Quantity = e.Product.Quality,
                          RegistrationName = e.Registration.Name,
                          
                       }).ToList();

            // Fetch all non-deleted WishList items
            WishListItems = _context.Wishlists.Where(w => w.IsDeleted == false).ToList();

            // Fetch all non-deleted products for the dropdown
            Products = _context.Products.Where(p => p.IsDeleted == false).ToList();

            if (id.HasValue)
            {
                var wishlistItem = _context.Wishlists.FirstOrDefault(e => e.Id == id.Value && e.IsDeleted == false);
                if (wishlistItem == null)
                {
                    return NotFound(); // Ensure this properly handles 404s
                }
                WishListItem = wishlistItem;
            }
            else
            {
                WishListItem = new Wishlist(); // New item for adding to wishlist
            }

            // Populate Products and WishListItems for the page
            ProductsList = _context.Products.Where(p => p.IsDeleted == false).Select(p => new ProductListVM
            {
                ProductCode = p.ProductCode,
                ProductName = p.ProductName,
                ProductPrice = p.Price,
                ProductImage = p.ProductDisplayImage,
            }).ToList();

            WishListItems = _context.Wishlists.Where(w => w.IsDeleted == false).ToList();

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