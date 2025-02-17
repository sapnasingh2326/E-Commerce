using ECommerceWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ECommerceWeb.Pages.Admin
{
    public class RoleModel : PageModel
    {
        private readonly DemoProjectContext _context;

        public RoleModel(DemoProjectContext context)
        {
            _context = context;
        }
        public IEnumerable<Role> datalist { get; set; } //this is for list

        [BindProperty]
        public Role role { get; set; }

        #region----------GET METHOD----------
        public IActionResult OnGet(int? id)
        {
            datalist = _context.Roles.Where(e => e.IsDeleted == false).ToList();
            role = new Role();
            if (id.HasValue)
            {
                var co = _context.Roles.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
                if (co == null)
                {
                    return NotFound();
                }
                role = co;
            }


            return Page();

        }
        #endregion

        #region-----POST METHOD-----
        public IActionResult OnPost()
        {
            if (role.Id > 0)//Update
            {
                var co = _context.Roles.AsNoTracking().Where(e => e.Id == role.Id && e.IsDeleted == false).FirstOrDefault();
                if (co == null)
                {
                    return NotFound();
                }


                role.IsDeleted = co.IsDeleted;
                _context.Roles.Update(role);
                _context.SaveChanges();
                TempData["info"] = "Your Data update Successfully";

            }
            else
            {
                try
                {

                    role.IsDeleted = false;
                    _context.Roles.Add(role);
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
            var co = _context.Roles.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
            if (co == null)
            {
                return NotFound();
            }
            co.IsDeleted = true;
            _context.Roles.Update(co);
            _context.SaveChanges();
            TempData["success"] = "data deleted";
            return RedirectToPage();

        }

        #endregion

    }
}
