using ECommerceWeb.Model;
using ECommerceWeb.VModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ECommerceWeb.Pages.Admin
{
    public class SmtpconfigModel : PageModel
    {
        private readonly DemoProjectContext _context;

        public SmtpconfigModel(DemoProjectContext context)
        {
            _context = context;
        }

        public IEnumerable<Smtpconfig> datalist { get; set; }
        public IEnumerable<RegistrationVM> registrations { get; set; }
        [BindProperty]
        public Smtpconfig smtpConfig { get; set; }
        public Registration Registration { get; set; }



        #region----------GET METHOD----------
        public IActionResult OnGet(int? id)
        {
            datalist = _context.Smtpconfigs.Where(e => e.IsDeleted == false).ToList();
            Registration = _context.Registrations.Where(e => e.IsDeleted == false && e.Id == id).FirstOrDefault();

            smtpConfig = new Smtpconfig();
            if (id.HasValue)
            {
                var co = _context.Smtpconfigs.Where(e => e.SmtpId == id && e.IsDeleted == false).FirstOrDefault();
                if (co == null)
                {
                    return NotFound();
                }
                smtpConfig = co;
            }


            return Page();

        }
        #endregion

        #region-----POST METHOD-----
        public IActionResult OnPost()
        {
            if (smtpConfig.SmtpId > 0)//Update
            {
                var co = _context.Smtpconfigs.AsNoTracking().Where(e => e.SmtpId == smtpConfig.SmtpId && e.IsDeleted == false).FirstOrDefault();
                if (co == null)
                {
                    return NotFound();
                }


                smtpConfig.IsDeleted = co.IsDeleted;
                _context.Smtpconfigs.Update(smtpConfig);
                _context.SaveChanges();
                TempData["info"] = "Your Data update Successfully";

            }
            else
            {
                try
                {

                    smtpConfig.IsDeleted = false;
                    _context.Smtpconfigs.Add(smtpConfig);
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
            var co = _context.Smtpconfigs.Where(e => e.SmtpId == id && e.IsDeleted == false).FirstOrDefault();
            if (co == null)
            {
                return NotFound();
            }
            co.IsDeleted = true;
            _context.Smtpconfigs.Update(co);
            _context.SaveChanges();
            TempData["success"] = "data deleted";
            return RedirectToPage();

        }

        #endregion


    }
}
















//#region GET METHOD
//public IActionResult OnGet(int? id)

//{
//    {
//        Datalist = _context.Smtpconfigs.Where(e => e.IsDeleted == false).ToList();

//       smtpConfig = new Smtpconfig();

//        if (id.HasValue)
//        {
//            var co = _context.Smtpconfigs.Where(e => e.SmtpId == id && e.IsDeleted == false).FirstOrDefault();
//            if (co == null)
//            {
//                return NotFound();
//            }
//            smtpConfig = co;
//        }

//        return Page();

//    }

//}

////{
////    Datalist = _context.Smtpconfigs.Where(e => e.IsDeleted == false).ToList();
////    SmtpConfig = id.HasValue
////        ? _context.Smtpconfigs.FirstOrDefault(e => e.SmtpId == id && e.IsDeleted == false) ?? new Smtpconfig()
////        : new Smtpconfig();
////    return Page();
////}
//#endregion

//#region POST METHOD
//public IActionResult OnPost()
//{
//    if (SmtpConfig.SmtpId>0)// Update
//    {
//        var existing = _context.Smtpconfigs.FirstOrDefault(e => e.SmtpId == SmtpConfig.SmtpId && e.IsDeleted == false);
//        if (existing == null)
//        {
//            return NotFound();
//        }

//        existing.SmtpUser = SmtpConfig.SmtpUser;
//        existing.SmtpPassword = SmtpConfig.SmtpPassword;
//        existing.Host = SmtpConfig.Host;
//        existing.Port = SmtpConfig.Port;
//        existing.IsEnableSsl = SmtpConfig.IsEnableSsl;

//        _context.Smtpconfigs.Update(existing);
//        _context.SaveChanges();
//        TempData["info"] = "Your data updated successfully.";
//    }
//    else // Add
//    {
//        try
//        {
//            SmtpConfig.IsDeleted = false;
//            _context.Smtpconfigs.Add(SmtpConfig);
//            _context.SaveChanges();
//            TempData["success"] = "Data saved successfully.";
//        }
//        catch (Exception ex)
//        {
//            TempData["error"] = $"Error: {ex.Message}";
//        }
//    }
//    return RedirectToPage();
//}
//#endregion


//#region-----POST METHOD-----
//public IActionResult OnPost()
//{
//    if (!ModelState.IsValid)
//    {
//        Datalist = _context.Smtpconfigs.Where(e => e.IsDeleted == false).ToList();
//        return Page();
//    }

//    try
//    {

//        if (smtpConfig.SmtpId > 0) // Update
//        {
//            var existing = _context.Smtpconfigs.FirstOrDefault(e => e.SmtpId == smtpConfig.SmtpId && e.IsDeleted == false);
//            if (existing == null) return NotFound();

//            // Update the existing record
//            existing.SmtpUser = smtpConfig.SmtpUser;
//            existing.SmtpPassword = smtpConfig.SmtpPassword;
//            existing.Host = smtpConfig.Host;
//            existing.Port = smtpConfig.Port;
//            existing.IsEnableSsl = smtpConfig.IsEnableSsl;

//            _context.Smtpconfigs.Update(existing);
//            TempData["info"] = "SMTP configuration updated successfully.";
//        }
//        else // Add new record
//        {
//            smtpConfig.IsDeleted = false;
//            _context.Smtpconfigs.Add(smtpConfig);
//            TempData["success"] = "SMTP configuration added successfully.";
//        }

//        _context.SaveChanges();
//    }
//    catch (Exception ex)
//    {
//        TempData["error"] = $"Error occurred: {ex.Message}";
//    }

//    return RedirectToPage();
//}

//#endregion

//#region DELETE METHOD
//public IActionResult OnPostDelete(int id)
//{
//    try
//    {
//        var existing = _context.Smtpconfigs.FirstOrDefault(e => e.SmtpId == id && e.IsDeleted == false);
//        if (existing == null)
//        {
//            return NotFound();
//        }

//        existing.IsDeleted = true;
//        _context.Smtpconfigs.Update(existing);
//        _context.SaveChanges();
//        TempData["success"] = "SMTP configuration deleted successfully.";
//    }
//    catch (Exception ex)
//    {
//        TempData["error"] = $"Error occurred: {ex.Message}";
//    }

//    return RedirectToPage();
//}
//#endregion

