using ECommerceWeb.Model;
using ECommerceWeb.Pages.Admin;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace ECommerceWeb.Pages.Admin
{
    public class BannerModel : PageModel
    {
      private readonly DemoProjectContext _context;

        public BannerModel (DemoProjectContext context)
        {
            _context = context;
        }


        public IEnumerable<Banner> datalist { get; set; } //this is for list



        [BindProperty]
        public Banner banner { get; set; }



        #region----GET METHOD-----
        public IActionResult OnGet(int? id)
        {
            datalist = _context.Banners.Where(e => e.IsDeleted == false).ToList();
            banner = new Banner();
            if (id.HasValue)
            {
                var co = _context.Banners.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
                if (co == null)
                {
                    return NotFound();
                }
                banner = co;
            }


            return Page();

        }
        #endregion


        #region------POST METHOD---

        public IActionResult OnPost()
        {
            var BannerImgStr = "";
            foreach (var file in HttpContext.Request.Form.Files)
            {
                // Check if file is not null and has content
                if (file != null && file.Length > 0)
                {
                    // Save the file to a directory on the server
                    var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images"); // Adjust path as needed
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    var filePath = Path.Combine(uploads, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    BannerImgStr = "/images/" + uniqueFileName;

                }
            }

            if (BannerImgStr != "")
            {
                banner.BannerImg = BannerImgStr;
            }
            if (banner.Id > 0)//Update
            {
                var co = _context.Banners.AsNoTracking().Where(e => e.Id == banner.Id && e.IsDeleted == false).FirstOrDefault();
                if (co == null)
                {
                    return NotFound();
                }
                banner.IsDeleted = co.IsDeleted;
                _context.Banners.Update(banner);
                _context.SaveChanges();
                TempData["info"] = "Your Data update Successfully";
            }
            else
            {
                try
                {
                    banner.IsDeleted = false;
                    _context.Banners.Add(banner);
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
            var co = _context.Banners.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
            if (co == null)
            {
                return NotFound();
            }
            co.IsDeleted = true;
            _context.Banners.Update(co);
            _context.SaveChanges();
            TempData["success"] = "data deleted";
            return RedirectToPage();

        }

        #endregion



    }
}  