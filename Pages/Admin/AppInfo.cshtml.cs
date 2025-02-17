using ECommerceWeb.Model;
using ECommerceWeb.VModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace ECommerceWeb.Pages.Admin
{
    public class AppInfoModel : PageModel
    {
        private readonly DemoProjectContext _context; 

        public AppInfoModel(DemoProjectContext context)
        {
            _context = context;
        }
        public IEnumerable<AppInfo> datalist { get; set; } //this is for list AppInfoPage

        [BindProperty]
        public AppInfo appInfo { get; set; }
        public LoginModel login { get; set; }

        #region----GET METHOD-----
        public IActionResult OnGet(int? id)
        {
           
            datalist = _context.AppInfos.Where(e => e.IsDeleted == false).ToList();
             
            appInfo = new AppInfo();

            if (id.HasValue)
            {
                var co = _context.AppInfos.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
                if (co == null)
                {
                    return NotFound();
                }
                appInfo = co;
            }

            return Page();

        }
        #endregion


        #region------POST METHOD---

        public IActionResult OnPost()
            
        { 
           
            //var result = _context.Login
            //   .Where(s => s.Logo == u && s.Password == Password && s.IsDeleted == false)
            //   .FirstOrDefault();

            // Initialize variables for file paths
            var favIconPath = "";
            var logoPath = "";
            var logoBgPath = "";

            // Loop through each file in the form
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

                    // Determine which file is being uploaded based on input name
                    if (file.Name == "appInfo.FavIcon")
                    {
                        favIconPath = "/images/" + uniqueFileName;
                    }
                    else if (file.Name == "appInfo.Logo")
                    {
                        logoPath = "/images/" + uniqueFileName;
                    }
                    else if (file.Name == "appInfo.LogoBackGroundImage")
                    {
                        logoBgPath = "/images/" + uniqueFileName;
                    }
                }
            }

            // Update the respective properties if a new file was uploaded
            if (!string.IsNullOrEmpty(favIconPath))
            {
                appInfo.FavIcon = favIconPath;
            }
            if (!string.IsNullOrEmpty(logoPath))
            {
                appInfo.Logo = logoPath;
            }
            if (!string.IsNullOrEmpty(logoBgPath))
            {
                appInfo.LogoBackGroundImage = logoBgPath;
               
            }

            if (appInfo.Id > 0) // Update existing AppInfo
            {
                appInfo.IsDeleted = false;
                _context.AppInfos.Update(appInfo);
                _context.SaveChanges();
                TempData["info"] = "Your data has been updated successfully.";
            }
            else // Create new AppInfo
            {
                try
                {
                    appInfo.IsDeleted = false;
                    _context.AppInfos.Add(appInfo);
                    _context.SaveChanges();
                    TempData["success"] = "Data saved successfully.";
                }
                catch (Exception ex)
                {
                    // Handle exception
                }
            }

            return RedirectToPage();
        }


        #endregion

        #region---DELETE METHOD--

        public IActionResult OnPostDelete(int id)
        {
            var co = _context.AppInfos.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
            if (co == null)
            {
                return NotFound();
            }
            co.IsDeleted = true;
            _context.AppInfos.Update(co);
            _context.SaveChanges();
            TempData["success"] = "data deleted";
            return RedirectToPage("appInfo");

        }

        #endregion


    }
}
