using ECommerceWeb.Model;
using ECommerceWeb.VModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWeb.Pages.Admin
{
    public class SubCategoryModel : PageModel
    {
        private readonly DemoProjectContext _context;

        public SubCategoryModel(DemoProjectContext context)
        {
            _context = context;
        }

        public IEnumerable<SubCategoryListVM> datalist { get; set; } //this is for list
        public IEnumerable<Category> category { get; set; } //this is for  category


        [BindProperty]
        public SubCategory subCategory { get; set; }



        #region----GET METHOD-----
        public IActionResult OnGet(int? id)
        {
            category = _context.Categories.Where(e => e.IsDeleted == false).ToList();

            datalist = _context.SubCategories.Where(e => e.IsDeleted == false).Select(e => new SubCategoryListVM
           
            {
                Id = e.Id,
                SubCategoryName = e.SubCategoryName,
                CategoryName = e.Category.CategoryName,                
            }).ToList();


             

            subCategory = new SubCategory();
            if (id.HasValue)
            {
                var co = _context.SubCategories.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
                if (co == null)
                {
                    return NotFound();
                }
                subCategory = co;
            }


            return Page();

        }
        #endregion


        #region--POST METHOD--
        public IActionResult OnPost()
        {
            if (subCategory.Id > 0)//Update
            {
                var co = _context.SubCategories.AsNoTracking().Where(e => e.Id == subCategory.Id && e.IsDeleted == false).FirstOrDefault();
                if (co == null)
                {
                    return NotFound();
                }


                subCategory.IsDeleted = co.IsDeleted;
                _context.SubCategories.Update(subCategory);
                _context.SaveChanges();
                TempData["info"] = "Your Data update Successfully";

            }
            else
            {
                try
                {

                    subCategory.IsDeleted = false;
                    _context.SubCategories.Add(subCategory);
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
            var co = _context.SubCategories.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
            if (co == null)
            {
                return NotFound();
            }
            co.IsDeleted = true;
            _context.SubCategories.Update(co);
            _context.SaveChanges();
            TempData["success"] = "data deleted";
            return RedirectToPage();

        }

        #endregion



    }
}








//        [BindProperty]
//        public IEnumerable<Category> category { get; set; }


//        [BindProperty]
//        public SubCategory SubCategory { get; set; }

//        [BindProperty]
//        public IEnumerable<SubCategory> subCategories { get; set; }



//        // Use async Task for asynchronous methods
//        public async Task<IActionResult> OnGet(int? id)
//        {
//            category = _context.Categories.Where(x => x.IsDeleted == false).Select(x => new Category
//            {
//                CategoryName = x.CategoryName,
//                Id = x.Id
//            }).OrderByDescending(x => x.CategoryName).ToList();



//            if(id!=null)
//            {
//                SubCategory = _context.SubCategories.Where(x=>x.Id == id).Select(x=>x).FirstOrDefault();
//            }
//            subCategories = await _context.SubCategories.ToListAsync();
//            return Page();
//        }

//        // Use async Task for asynchronous methods
//        public async Task<IActionResult> OnPost()
//         {


//            if (SubCategory.Id == null || SubCategory.Id == 0)
//            {
//                SubCategory.IsDeleted = false;
//                _context.SubCategories.Add(SubCategory);
//                TempData["Message"] = "SubCategory created successfully.";
//            }
//            else
//            {
//                var existingSubCategory = await _context.SubCategories.FindAsync(SubCategory.Id);
//                if (existingSubCategory != null)
//                {
//                    existingSubCategory.SubCategoryName = SubCategory.SubCategoryName;
//                    _context.SubCategories.Update(existingSubCategory);
//                    TempData["Message"] = "SubCategory updated successfully.";
//                }
//            }

//            await _context.SaveChangesAsync();
//            return RedirectToPage("./SubCategory");
//        }


//        // Use async Task for asynchronous methods
//        public async Task<IActionResult> OnPostEdit(int id)
//        {
//            var existingSubCategory = await _context.SubCategories.FindAsync(id);

//            if (existingSubCategory == null)
//            {
//                return NotFound();
//            }

//            _context.Entry(existingSubCategory).CurrentValues.SetValues(SubCategory);
//            await _context.SaveChangesAsync();

//            return RedirectToPage("./SubCategory");
//        }

//        // Use async Task for asynchronous methods
//        public async Task<IActionResult> OnPostDelete()
//        {
//            var id = SubCategory.Id;
//            var subCategoryToDelete = await _context.SubCategories.FindAsync(id);
//            subCategoryToDelete.IsDeleted = true;
//            _context.SubCategories.Update(subCategoryToDelete);
//            await _context.SaveChangesAsync();

//            return RedirectToPage("./SubCategory");
//        }

//        // Property to hold subCategories retrieved from the database
//        public List<SubCategory> SubCategories { get; set; }
//    }
//}