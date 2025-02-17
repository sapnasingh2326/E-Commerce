using ECommerceWeb.Model;
using ECommerceWeb.VModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWeb.Pages.Admin
{
    public class ProductModel : PageModel

    {
        private readonly DemoProjectContext _context;

        public ProductModel(DemoProjectContext context)
        {
            _context = context;
        }
        public IEnumerable<Product> datalist { get; set; } //this is for Product list
        public IEnumerable<ProductSize> productSize { get; set; } //this is for ProductSize list
        public IEnumerable<ProductColor> productColor{ get; set; } //this is for ProductColor list
        public IEnumerable<SubCategory> subcategory{ get; set; } //this is for SubCategory list
        public IEnumerable<Category> category { get; set; } //this is for Category list



        [BindProperty]
        public Product product { get; set; }

        [BindProperty]
        public List<Category> Categories { get; set; }

        [BindProperty]
        public List<SubCategory> SubCategories { get; set; }

        [BindProperty]
        public int SelectedCategoryId { get; set; }

        [BindProperty]
        public int SelectedSubCategoryId { get; set; }

        #region----GET METHOD-----

        public IActionResult OnGet(int? id)
        {
            datalist = _context.Products.Where(e => e.IsDeleted == false).ToList();

            productSize = _context.ProductSizes.Where(e => e.IsDeleted == false).ToList();
           
            productColor = _context.ProductColors.Where(e => e.IsDeleted == false).ToList();

            subcategory= _context.SubCategories.Where(e => e.IsDeleted == false).ToList();

            category = _context.Categories.Where(e => e.IsDeleted == false).ToList();

           
            product = new Product();
            if (id.HasValue)
            {
                var co = _context.Products.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
                if (co == null)
                {
                    return NotFound();
                }
                product = co;
            }


            return Page();
        }
       

    #endregion


    #region------POST METHOD--
    public IActionResult OnPost()
           {
            var ImageUrlStr1 = "";
            foreach (var file in HttpContext.Request.Form.Files)
            {
                // Check if file is not null and has content
                if (file != null && file.Length > 0)
                {
                    // Save the file to a directory on the server
                    var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images/ProductImage"); // Adjust path as needed
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    var filePath = Path.Combine(uploads, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    var ImageUrlStr = "/images/ProductImage/" + uniqueFileName;

                    product.ProductDisplayImage = ImageUrlStr;
                }
            }

            if (product.Id > 0)//Update
            {
                var co = _context.Products.AsNoTracking().Where(e => e.Id == product.Id && e.IsDeleted == false).FirstOrDefault();
                if (co == null)
                {
                    return NotFound();
                }

                product.IsDeleted = false;
                _context.Products.Update(product);
                _context.SaveChanges();
                TempData["info"] = "Your Data update Successfully";

            }
            else
            {
                try
                {
                    product.CreatedDate = DateTime.Now;
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

    #region-----------DELETE METHOD---------------

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