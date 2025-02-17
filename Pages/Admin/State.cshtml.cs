using ECommerceWeb.Model;
using ECommerceWeb.VModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace ECommerceWeb.Pages.Admin
{
    public class StateModel : PageModel
    {
        private readonly DemoProjectContext _context;

        public StateModel(DemoProjectContext context)
        {
            _context = context;
        }

        public IEnumerable<State> datalist { get; set; } // This is for list
        public IEnumerable<Country> country { get; set; } // This is for list
        public IEnumerable<City> city { get; set; } // This is for list

        [BindProperty]
        public StateVModel state { get; set; }

        [BindProperty]
        public IList<StateVModel> States { get; set; }

        public IList<Country> Countries { get; set; }
        public IList<City> Cities { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            datalist = await _context.States.Where(e => e.IsDeleted == false).ToListAsync();
            city = await _context.Cities.Where(e => e.IsDeleted == false).ToListAsync();
            country = await _context.Countries.Where(e => e.IsDeleted == false).ToListAsync();

            States = await _context.States.Where(x => x.IsDeleted == false).Select(x => new StateVModel
            {
                CountryName = x.Country.CountryName,
                StateName = x.StateName,
                CityName = x.StateName,
                Id = x.Id,
                CountryId = x.CountryId,
            }).OrderByDescending(x => x.StateName).ToListAsync();

            Countries = await _context.Countries.ToListAsync();

            if (id != null)
            {
                state = await _context.States.Where(x => x.Id == id).Select(x => new StateVModel
                {
                    CountryName = x.Country.CountryName,
                    StateName = x.StateName,
                    CityName = x.StateName,
                    Id = x.Id,
                    CountryId = x.CountryId,
                }).FirstOrDefaultAsync();

                if (state == null)
                {
                    return NotFound();
                }
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            State _state = new State
            {
                CountryId = state.CountryId,
                StateName = state.StateName,
                IsDeleted = false
            };

            if (state.Id > 0) // Update
            {
                var existingState = await _context.States.AsNoTracking().FirstOrDefaultAsync(e => e.Id == state.Id && e.IsDeleted == false);
                if (existingState == null)
                {
                    return NotFound();
                }

                _state.Id = state.Id;
                _context.States.Update(_state);
                TempData["info"] = "Your Data update Successfully";
            }
            else
            {
                try
                {
                    _context.States.Add(_state);
                    TempData["success"] = "Data saved";
                }
                catch (Exception ex)
                {
                    TempData["error"] = "Error saving data: " + ex.Message;
                    return Page();
                }
            }

            await _context.SaveChangesAsync();
            return RedirectToPage();
        }

        #region DELETE METHOD
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var state = await _context.States.FirstOrDefaultAsync(e => e.Id == id && e.IsDeleted == false);
            if (state == null)
            {
                return NotFound();
            }

            state.IsDeleted = true; // Mark the state as deleted
            _context.States.Update(state);
            await _context.SaveChangesAsync();

            TempData["success"] = "State deleted successfully.";
            return RedirectToPage("./State");
        }
        #endregion
    }
}
