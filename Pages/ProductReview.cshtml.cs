using ECommerceWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWeb.Pages
{
    public class ProductReviewModel : PageModel
    {
        private readonly DemoProjectContext _context;

        public ProductReviewModel(DemoProjectContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ProductReview productReview {  get; set; }
        public Product Product { get; set; }
        public Registration Registration { get; set; }
        public List<ProductReview> ProductReviews { get; set; } = new List<ProductReview>();
        public IEnumerable<AppInfo> datalist { get; set; }

        public IActionResult OnGet(int? productId = 13)
        {
            if (!productId.HasValue || productId <= 0)
            {
                return NotFound("Invalid Product ID");
            }
            datalist = _context.AppInfos.Where(e => e.IsDeleted == false).ToList();
            // Fetch the product without reviews
            Product = _context.Products.FirstOrDefault(p => p.Id == productId.Value);
            

            // Check if the product was found 
            if (Product == null)
            {
                return NotFound("Product not found");
            }

            Registration = _context.Registrations.FirstOrDefault(e => e.IsDeleted == false);
            // Retrieve product reviews for the specified product
            ProductReviews = _context.ProductReviews
                .Where(review => review.ProductId == productId.Value && (review.IsDeleted == false || review.IsDeleted == null))
                .ToList();

            return Page();
        }

        public IActionResult OnPost(int? productId)
        {
            if (!ModelState.IsValid)
            {
                var errors = ModelState.Values.SelectMany(v => v.Errors).Select(e => e.ErrorMessage);
                TempData["error"] = string.Join(", ", errors);
                return Page();
            }

            if (!productId.HasValue || productId <= 0)
            {
                return NotFound("Invalid Product ID");
            }

            // Initialize variables for file paths
            var Img1 = "";
            var Img2 = "";

            // Loop through each file in the form
            foreach (var file in HttpContext.Request.Form.Files)
            {
                if (file != null && file.Length > 0)
                {
                    // Save the file to a directory on the server
                    var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    var filePath = Path.Combine(uploads, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    // Determine which file is being uploaded based on input name
                    if (file.Name == "productReview.ImageOne")
                    {
                        Img1 = "/images/" + uniqueFileName;
                    }
                    else if (file.Name == "productReview.ImageTwo")
                    {
                        Img2 = "/images/" + uniqueFileName;
                    }
                }
            }

            // Update the respective properties if a new file was uploaded
            if (!string.IsNullOrEmpty(Img1))
            {
                productReview.ImageOne = Img1;
            }
            if (!string.IsNullOrEmpty(Img2))
            {
                productReview.ImageTwo = Img2;
            }

            // Set the product ID and registration ID before saving
            productReview.ProductId = productId.Value;
            productReview.RegistrationId = Registration?.Id ?? 0; // Make sure Registration is not null
            productReview.CreatedDate = DateTime.Now;
            productReview.IsDeleted = false;

            try
            {
                // Add or update the product review in the database
                if (productReview.Id > 0) // Update existing ProductReview 
                {
                    _context.ProductReviews.Update(productReview);
                    TempData["info"] = "Your review has been updated successfully.";
                }
                else // Add new ProductReview
                {
                    _context.ProductReviews.Add(productReview);
                    TempData["success"] = "Your review has been saved successfully.";
                }

                _context.SaveChanges();
            }
            catch (Exception ex)
            {
                TempData["error"] = "An error occurred while saving the review.";
                // Optionally log the error here
            }

            return RedirectToPage(new { productId = productReview.ProductId });
        }


    }
}

