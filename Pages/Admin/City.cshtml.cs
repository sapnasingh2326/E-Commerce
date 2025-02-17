using ECommerceWeb.Model;
using ECommerceWeb.VModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics.Metrics;

namespace ECommerceWeb.Pages.Admin
{
    public class CityModel : PageModel
    {

        private readonly DemoProjectContext _context;


        public CityModel(DemoProjectContext context)
        {
            _context = context;
        }
        public IEnumerable<City> datalist { get; set; } // this is for list
        public IEnumerable<State> state { get; set; } // this is for list
        public IEnumerable<Country> country { get; set; } // this is for list

        [BindProperty]
        public CityVModel City { get; set; }

        [BindProperty]
        public IList<CityVModel> Cities { get; set; }

        public IList<Country> Countries { get; set; }
        public IList<State> States { get; set; }

        public async Task<IActionResult> OnGet(int? id)
        {
            await LoadDataAsync(id);
            return Page();
        }

        //#region----GET METHOD-----

        //public async Task<IActionResult> OnGetAsync(int? id)
        //{
        //    datalist = await _context.Cities.Where(e => e.IsDeleted == false).ToListAsync();
        //    state = await _context.States.Where(e => e.IsDeleted == false).ToListAsync();
        //    country = await _context.Countries.Where(e => e.IsDeleted == false).ToListAsync();

        //    City = new CityVModel();
        //    if (id.HasValue)
        //    {
        //        City = await _context.Cities.Where(e => e.Id == id && e.IsDeleted == false)
        //                                    .Select(x => new CityVModel
        //                                    {
        //                                        CityName = x.CityName,
        //                                        CountryName = x.State.Country.CountryName,
        //                                        StateName = x.State.StateName,
        //                                        Id = x.Id,
        //                                        StateId = x.StateId,
        //                                        CountryId = x.State.CountryId
        //                                    }).FirstOrDefaultAsync();

        //        if (City == null)
        //        {
        //            return NotFound();
        //        }
        //    }

        //    return Page();
        //}

        //#endregion

        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                await LoadDataAsync();
                return Page();
            }

            City _city;

            if (City.Id > 0) // Update
            {
                _city = await _context.Cities.FindAsync(City.Id);

                if (_city == null)
                {
                    return NotFound();
                }

                _city.StateId = City.StateId;
                _city.CityName = City.CityName;
                _city.IsDeleted = false;

                _context.Cities.Update(_city);
            }
            else // Create
            {
                _city = new City
                {
                    StateId = City.StateId,
                    CityName = City.CityName,
                    IsDeleted = false
                };

                _context.Cities.Add(_city);
            }

            try
            {
                await _context.SaveChangesAsync();
                TempData["success"] = City.Id > 0 ? "Data updated successfully" : "Data saved successfully";
            }
            catch (Exception ex)
            {
                // Log the exception (ex) here
                TempData["error"] = "An error occurred while saving data.";
            }

            return RedirectToPage();
        }

        #region --------DELETE METHOD---------

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var city = await _context.Cities.FirstOrDefaultAsync(e => e.Id == id && e.IsDeleted == false);
            if (city == null)
            {
                return NotFound();
            }

            city.IsDeleted = true; // Mark as deleted
            _context.Cities.Update(city);
            await _context.SaveChangesAsync();

            TempData["success"] = "City deleted successfully.";
            return RedirectToPage("./City");
        }

        #endregion

        private async Task LoadDataAsync(int? id = null)
        {
            
            state = _context.States.Where(e => e.IsDeleted == false).ToList();
            country = _context.Countries.Where(e => e.IsDeleted == false).ToList();
            datalist = _context.Cities.Where(e => e.IsDeleted == false).ToList();
            Cities = await _context.Cities.Where(x => x.IsDeleted == false).Select(x => new CityVModel
            {
                CityName = x.CityName,
                CountryName = x.State.Country.CountryName,
                StateName = x.State.StateName,
                Id = x.Id,
                StateId = x.StateId
            }).OrderByDescending(x => x.CityName).ToListAsync();

            Countries = await _context.Countries.ToListAsync();
            States = await _context.States.ToListAsync();

            if (id != null)
            {
                City = await _context.Cities.Where(x => x.Id == id.Value).Select(x => new CityVModel
                {
                    CityName = x.CityName,
                    CountryName = x.State.Country.CountryName,
                    StateName = x.State.StateName,
                    Id = x.Id,
                    StateId = x.StateId,
                    CountryId = x.State.CountryId,

                }).FirstOrDefaultAsync();

                if (City == null)
                {
                    City = new CityVModel(); // Ensure City is not null to avoid model binding issues
                }
            }
        }
    }
}
