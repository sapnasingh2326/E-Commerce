using ECommerceWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWeb.Pages.Admin
{
    public class ProductReviewModel : PageModel
    {
        private readonly DemoProjectContext _context;

        public ProductReviewModel(DemoProjectContext context)
        {
            _context = context;
        }

        [BindProperty]
        public ProductReview NewReview { get; set; }
        public IEnumerable<ProductReview> datalist { get; set; } //this is for list

        public List<ProductReview> Reviews { get; set; } = new List<ProductReview>();

        public async Task OnGetAsync(int productId)
        {
            Reviews = await _context.ProductReviews
                .Include(r => r.Registration)
                .Include(r => r.Product)
                .Where(r => r.ProductId == productId && r.IsDeleted == false)
                .ToListAsync();
        }

        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody] ProductReview review)
        {
            _context.ProductReviews.Add(review);
            await _context.SaveChangesAsync();
            return Page();
        }

    }
}
