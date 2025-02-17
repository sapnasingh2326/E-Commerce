using ECommerceWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWeb.Pages.Admin
{
    public class NewsletterSubscriptionsModel : PageModel
    {
        private DemoProjectContext _context;

        public NewsletterSubscriptionsModel(DemoProjectContext context)
        {
            _context = context;
        }


        public IEnumerable<NewsletterSubscription> datalist { get; set; } //this is for list



        [BindProperty]
        public NewsletterSubscription newsletterSubscription { get; set; }



        #region-----------------GET METHOD--------------------

        public IActionResult OnGet(int? id)
        {
            datalist = _context.NewsletterSubscriptions.Where(e => e.IsDeleted == false).ToList();
            newsletterSubscription = new NewsletterSubscription();
            if (id.HasValue)
            {
                var co = _context.NewsletterSubscriptions.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
                if (co == null)
                {
                    return NotFound();
                }
                newsletterSubscription = co;
            }


            return Page();

        }
        #endregion


        #region----------------POST METHOD------------------------
        public IActionResult OnPost()
        {
            if (newsletterSubscription.Id > 0)//Update
            {
                var co = _context.NewsletterSubscriptions.AsNoTracking().Where(e => e.Id == newsletterSubscription.Id && e.IsDeleted == false).FirstOrDefault();
                if (co == null)
                {
                    return NotFound();
                }


                newsletterSubscription.IsDeleted = co.IsDeleted;
                _context.NewsletterSubscriptions.Update(newsletterSubscription);
                _context.SaveChanges();
                TempData["info"] = "Your Data update Successfully";

            }
            else
            {
                try
                {

                    newsletterSubscription.IsDeleted = false;
                    _context.NewsletterSubscriptions.Add(newsletterSubscription);
                    _context.SaveChanges();
                    TempData["success"] = "data saved";
                }
                catch (Exception ex) { }
            }
            return RedirectToPage();

        }

        #endregion

        #region----------------DELETE METHOD-----------------------

        public IActionResult OnPostDelete(int id)
        {
            var co = _context.NewsletterSubscriptions.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
            if (co == null)
            {
                return NotFound();
            }
            co.IsDeleted = true;
            _context.NewsletterSubscriptions.Update(co);
            _context.SaveChanges();
            TempData["success"] = "data deleted";
            return RedirectToPage();

        }

        #endregion



    }
}
