using ECommerceWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceWeb.Pages.Admin
{
    public class ComposeModel : PageModel
    {
        private readonly DemoProjectContext _context; 

        public ComposeModel(DemoProjectContext context)
        {
            _context = context;
        
        }
        [BindProperty]
        public  Registration registration { get; set; }

        [BindProperty]
        public MailItem mailItem { get; set; }


        public IEnumerable<MailItem> datalist { get; set; } //this is for list DynamicPage




        #region----GET METHOD-----
        public IActionResult OnGet(int? id)
        {
            datalist = _context.MailItems.Where(e => e.IsDeleted == false).ToList();

            mailItem = new MailItem();

            if (id.HasValue)
            {
                var co = _context.MailItems.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
                if (co == null)
                {
                    return NotFound();
                }
                mailItem = co;
            }

            return Page();

        }
        #endregion


        //#region------POST METHOD---

        //public IActionResult OnPost()
        //{
        //    var MailImgStr = "";
        //    foreach (var file in HttpContext.Request.Form.Files)
        //    {
        //        // Check if file is not null and has content
        //        if (file != null && file.Length > 0)
        //        {
        //            // Save the file to a directory on the server
        //            var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images"); // Adjust path as needed
        //            var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
        //            var filePath = Path.Combine(uploads, uniqueFileName);
        //            using (var fileStream = new FileStream(filePath, FileMode.Create))
        //            {
        //                file.CopyTo(fileStream);
        //            }

        //           MailImgStr = "/images/" + uniqueFileName;

        //        }
        //    }

        //    if (MailImgStr != "")
        //    {
        //        mailItem.MailImg = MailImgStr;
        //    }
        //    if (mailItem.Id > 0)//Update
        //    {
        //        //var co = _context.Banners.AsNoTracking().Where(e => e.Id == Mail.Id && e.IsDeleted == false).FirstOrDefault();
        //        //if (co == null)
        //        //{
        //        //    return NotFound();
        //        //}
        //        mailItem.IsDeleted = false;
        //        _context.MailItems.Update(mailItem);
        //        _context.SaveChanges();
        //        TempData["info"] = "Your Data update Successfully";
        //    }
        //    else
        //    {
        //        try
        //        {
        //            mailItem.IsDeleted = false;
        //            _context.MailItems.Add(mailItem);
        //            _context.SaveChanges();
        //            TempData["success"] = "data saved";
        //        }
        //        catch (Exception ex) { }
        //    }
        //    return RedirectToPage();
        //}

        //#endregion

        public IActionResult OnPost()
        {
            // Process the uploaded files
            var MailImgStr = "";
            foreach (var file in HttpContext.Request.Form.Files)
            {
                if (file != null && file.Length > 0)
                {
                    var uploads = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "images");
                    var uniqueFileName = Guid.NewGuid().ToString() + "_" + file.FileName;
                    var filePath = Path.Combine(uploads, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        file.CopyTo(fileStream);
                    }

                    MailImgStr = "/images/" + uniqueFileName;
                }
            }

            // Update the MailImg property if a file was uploaded
            if (MailImgStr != "")
            {
                mailItem.MailImg = MailImgStr;
            }

            if (mailItem.Id > 0) // Update existing mail
            {
                mailItem.IsDeleted = false;
                _context.MailItems.Update(mailItem);
                _context.SaveChanges();
                TempData["info"] = "Your Data updated Successfully";
            }
            else // Create new mail
            {
                try
                {
                    mailItem.IsDeleted = false;
                    _context.MailItems.Add(mailItem);
                    _context.SaveChanges();
                    TempData["success"] = "Data saved successfully";
                }
                catch (Exception ex)
                {
                    // Handle exceptions
                }
            }
            return RedirectToPage();
        }


        #region---DELETE METHOD--

        public IActionResult OnPostDelete(int id)
        {
            var co = _context.MailItems.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
            if (co == null)
            {
                return NotFound();
            }
            co.IsDeleted = true;
            _context.MailItems.Update(co);
            _context.SaveChanges();
            TempData["success"] = "data deleted";
            return RedirectToPage();

        }

        #endregion
    }
}
