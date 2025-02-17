using ECommerceWeb.Model;
using ECommerceWeb.VModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using static ECommerceWeb.Pages.Metronic_Shop.YourAddressesModel;

namespace ECommerceWeb.Pages.Admin
{
    public class AddressBookModel : PageModel
    {
        //private readonly DemoProjectContext _context;

        //public AddressBookModel(DemoProjectContext context)
        //{
        //    _context = context;
        //}

        //[BindProperty]
        //public AddressBookVM AddressBookVM { get; set; }

        //public IEnumerable<AddressBook> addressBookList { get; set; }

        //public IActionResult OnGet(int? id)
        //{
        //    if (id == null)
        //    {
        //        id = Convert.ToInt32(HttpContext.Session.GetString("CustomerID"));
        //    }

        //    AddressBookVM = new AddressBookVM
        //    {
        //        addressBookList = _context.AddressBooks
        //            .Where(x => x.Id == id && x.IsDeleted == false).ToList()
        //    };

        //    if (AddressBookVM.addressBookList == null || !AddressBookVM.addressBookList.Any())
        //    {
        //        return RedirectToPage("UpsertAddress");
        //    }

        //    return Page();
        //}

        //public async Task<IActionResult> OnPostDeleteAddressAsync(int id)
        //{
        //    var addrFromDb = await _context.AddressBooks.FindAsync(id);
        //    if (addrFromDb == null)
        //    {
        //        return NotFound();
        //    }

        //    addrFromDb.IsDeleted = true;
        //    await _context.SaveChangesAsync();

        //    return RedirectToPage("Address");
        //}

        //public IActionResult OnGetUpsertAddress(int? id)
        //{
        //    AddressBookVM = new AddressBookVM
        //    {
        //        addressBook = id == null ? new AddressBook() : _context.AddressBooks.Find(id),
        //        CitySelectList = _context.Cities.Where(x => x.Id == id && x.IsDeleted == false)
        //            .Select(i => new SelectListItem
        //            {
        //                Text = i.CityName,
        //                Value = i.Id.ToString()
        //            }).ToList()
        //    }; 

        //    if (AddressBookVM.addressBook == null && id != null)
        //    {
        //        return NotFound();
        //    }

        //    return Page();
        //}

        //public IActionResult OnPostUpsertAddress()
        //{
        //    if (ModelState.IsValid)
        //    {
        //        AddressBookVM.addressBook.IsDeleted = false;
        //        AddressBookVM.addressBook.Id = Convert.ToInt32(HttpContext.Session.GetString("CustomerID"));

        //        if (AddressBookVM.addressBook.Id == 0)
        //        {
        //            // Create new address
        //            _context.AddressBooks.Add(AddressBookVM.addressBook);
        //        }
        //        else
        //        {
        //            // Update existing address
        //            _context.AddressBooks.Update(AddressBookVM.addressBook);
        //        }

        //        _context.SaveChanges();
        //        return RedirectToPage("Address");
        //    }

        //    AddressBookVM.CitySelectList = _context.Cities.Where(x => x.State == id && x.IsDeleted == false)

        //         .Select(i => new SelectListItem
        //        {
        //            Text = i.Name,
        //            Value = i.Id.ToString()
        //        }).ToList();

        //    return Page();
        //}



    }
}





























        //public IEnumerable<AddressBook> datalist { get; set; } //this is for list



        //[BindProperty]
        //public AddressBook addressBook { get; set; }



        //#region----GET METHOD-----
        //public IActionResult OnGet(int? id)
        //{
        //    datalist = _context.AddressBooks.Where(e => e.IsDeleted == false).ToList();
        //    addressBook = new AddressBook();
        //    if (id.HasValue)
        //    {
        //        var co = _context.AddressBooks.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();

        //        addressBook = co;
        //    }


        //    return Page();

        //}
        //#endregion


        //#region--POST METHOD--
        //public IActionResult OnPost()
        //{
        //    if (addressBook.Id > 0)//Update
        //    {
        //        var co = _context.AddressBooks.AsNoTracking().Where(e => e.Id == addressBook.Id && e.IsDeleted == false).FirstOrDefault();
        //        if (co == null)
        //        {
        //            return NotFound();
        //        }


        //        addressBook.IsDeleted = co.IsDeleted;
        //        _context.AddressBooks.Update(addressBook);
        //        _context.SaveChanges();
        //        TempData["info"] = "Your Data update Successfully";

        //    }
        //    else
        //    {
        //        try
        //        {

        //            addressBook.IsDeleted = false;
        //            _context.AddressBooks.Add(addressBook);
        //            _context.SaveChanges();
        //            TempData["success"] = "data saved";
        //        }
        //        catch (Exception ex) { }
        //    }
        //    return RedirectToPage();

        //}

        //#endregion

        //#region---DELETE METHOD--

        //public IActionResult OnPostDelete(int id)
        //{
        //    var co = _context.AddressBooks.Where(e => e.Id == id && e.IsDeleted == false).FirstOrDefault();
        //    if (co == null)
        //    {
        //        return NotFound();
        //    }
        //    co.IsDeleted = true;
        //    _context.AddressBooks.Update(co);
        //    _context.SaveChanges();
        //    TempData["success"] = "data deleted";
        //    return RedirectToPage();

        //}

        //#endregion

    