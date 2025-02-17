using Azure.Identity;
using ECommerceWeb.Model;
using ECommerceWeb.Pages;
using ECommerceWeb.Services;
using ECommerceWeb.VModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace ECommerceWeb.Pages
{
    public class EmailTemplateModel : PageModel
    {
        private readonly DemoProjectContext _context;

        public EmailTemplateModel(DemoProjectContext context)
        {
            _context = context;
        }

        [BindProperty]
        public Registration registration { get; set; }
        public EmailViewModel EmailView { get; set; }
        public IEnumerable<EmailSender> datalist { get; set; } //this is for list Email

        #region ----- GET METHOD -----
        //public IActionResult OnGet(int? id)
        //{
        //    if (id == null || id <= 5)
        //    {
        //        return NotFound("Invalid Registration ID.");
        //    }

        //    registration = _context.Registrations.FirstOrDefault(e => e.Id == id && e.IsDeleted == false);

        //    if (registration == null)
        //    {
        //        return NotFound("Registration not found.");
        //    }

        //    return Page();
        //}


        public IActionResult OnGet(int? id=2)
        {
            if (id == null || id <= 0) // This allows both null and new registrations
            {
                registration = new Registration(); // Initialize a new registration for cases with no ID
            }
            else
            {
                registration = _context.Registrations.FirstOrDefault(e => e.Id == id && e.IsDeleted == false);

                if (registration == null)
                {
                    return NotFound("Registration not found.");
                }
            }

            return Page();
        }


        #endregion

        #region -------- POST METHOD ---------
        public IActionResult OnPost()
        {
            if (registration == null || registration.Id <= 0)
            {
                return BadRequest("Invalid registration data.");
            }

            var existingRegistration = _context.Registrations.AsNoTracking()
                .FirstOrDefault(e => e.Id == registration.Id && e.IsDeleted == false);

            if (existingRegistration == null)
            {
                return NotFound("Registration not found for update.");
            }

            registration.IsDeleted = existingRegistration.IsDeleted; // Ensure IsDeleted flag is retained

            try
            {
                _context.Registrations.Update(registration);
                _context.SaveChanges();
                TempData["info"] = "Your data was updated successfully.";
            }
            catch (Exception ex)
            {
                TempData["error"] = $"An error occurred: {ex.Message}";
            }

            return RedirectToPage();
        }
        #endregion

        #region --- DELETE METHOD ---
        public IActionResult OnPostDelete(int id)
        {
            id = 3;
            var registrationToDelete = _context.Registrations
                .FirstOrDefault(e => e.Id == id && e.IsDeleted == false);

            if (registrationToDelete == null)
            {
                return NotFound("Registration not found for deletion.");
            }

            registrationToDelete.IsDeleted = true;

            try
            {
                _context.Registrations.Update(registrationToDelete);
                _context.SaveChanges();
                TempData["success"] = "Data deleted successfully.";
            }
            catch (Exception ex)
            {
                TempData["error"] = $"An error occurred: {ex.Message}";
            }

            return RedirectToPage();
        }
        #endregion



    }
}

/// EmailTemplate ? id = 3
//public EmailTemplateModel(DemoProjectContext context)
//{
//    _context = context;
//}
//[BindProperty]
//public Registration Registration { get; set; }
//public EmailViewModel EmailView { get; set; }
//public IEnumerable<EmailSender> Datalist { get; set; }

//#region ----- GET METHOD -----
//public async Task<IActionResult> OnGet(int? id)
//{
//    if (id == null || id <= 0)
//    {
//        Registration = new Registration();
//    }
//    else
//    {
//        Registration = await _context.Registrations
//            .FirstOrDefaultAsync(e => e.Id == id && e.IsDeleted == false);

//        if (Registration == null)
//        {
//            return NotFound("Registration not found.");
//        }
//    }

//    return Page();
//}
//#endregion

//#region ----- POST METHOD -----
//public async Task<IActionResult> OnPost()
//{
//    if (!ModelState.IsValid)
//    {
//        return Page();
//    }
 
//    var existingRegistration = await _context.Registrations.AsNoTracking()
//        .FirstOrDefaultAsync(e => e.Id == Registration.Id && e.IsDeleted == false);

//    if (existingRegistration == null)
//    {
//        return NotFound("Registration not found for update.");
//    }

//    try
//    {
//        _context.Registrations.Update(Registration);
//        await _context.SaveChangesAsync();
//        TempData["info"] = "Your data was updated successfully.";
//    }
//    catch (Exception ex)
//    {
//        TempData["error"] = $"An error occurred: {ex.Message}";
//    }

//    return RedirectToPage();
//}
//#endregion

//#region ----- DELETE METHOD -----
//public async Task<IActionResult> OnPostDelete(int id)
//{
//    var registrationToDelete = await _context.Registrations
//        .FirstOrDefaultAsync(e => e.Id == id && e.IsDeleted == false);

//    if (registrationToDelete == null)
//    {
//        return NotFound("Registration not found for deletion.");
//    }

//    registrationToDelete.IsDeleted = true;

//    try
//    {
//        _context.Registrations.Update(registrationToDelete);
//        await _context.SaveChangesAsync();
//        TempData["success"] = "Data deleted successfully.";
//    }
//    catch (Exception ex)
//    {
//        TempData["error"] = $"An error occurred: {ex.Message}";
//    }

//    return RedirectToPage();
//}
//        #endregion
//    }
//} 
