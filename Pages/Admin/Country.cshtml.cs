using ECommerceWeb.Model;
using ECommerceWeb.Pages.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ECommerceWeb.Pages.Admin
{
    public class CountryModel : PageModel
    {
        private readonly DemoProjectContext _context;

        public CountryModel(DemoProjectContext context)
        {
            _context = context;
        }
        public IEnumerable<Country> datalist { get; set; } //this is for list



        [BindProperty]
        public Country country { get; set; }



        #region----GET METHOD-----
        public IActionResult OnGet(int? id)
        {
            datalist = _context.Countries.Where(e => e.IsDeleted == false).ToList();
            country = new Country();
            if (id.HasValue)
            {
                var co = _context.Countries.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();

                country = co;
            }


            return Page();

        }
        #endregion


        #region--POST METHOD--
        public IActionResult OnPost()
        {
            if (country.Id > 0)//Update
            {
                var co = _context.Countries.AsNoTracking().Where(e => e.Id == country.Id && e.IsDeleted == false).FirstOrDefault();
                if (co == null)
                {
                    return NotFound();
                }


                country.IsDeleted = co.IsDeleted;
                _context.Countries.Update(country);
                _context.SaveChanges();
                TempData["info"] = "Your Data update Successfully";

            }
            else
            {
                try
                {

                    country.IsDeleted = false;
                    _context.Countries.Add(country);
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
            var co = _context.Countries.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
            if (co == null)
            {
                return NotFound();
            }
            co.IsDeleted = true;
            _context.Countries.Update(co);
            _context.SaveChanges();
            TempData["success"] = "data deleted";
            return RedirectToPage();

        }

        #endregion



    }
}
