using ECommerceWeb.Model;
using ECommerceWeb.VModel;
using System.Collections.Generic;
using System.Linq;

public interface INewInformationDynamicPageService
{
    IEnumerable<DynamicpageVM> GetInformationDynamicPage();
}

public class NewInformationDynamicPageService : INewInformationDynamicPageService
{
    private readonly DemoProjectContext _context;

    public NewInformationDynamicPageService(DemoProjectContext context)
    {
        _context = context;
    }

    public IEnumerable<DynamicpageVM> GetInformationDynamicPage()
    {
        return _context.DynamicPages
            .Where(x => x.IsDeleted == false) // Filtering on IsDeleted flag
            .Select(x => new DynamicpageVM
            {
                PageId = x.PageId,
                PageName = x.PageName,
                PageUrl = x.PageUrl,

            }).ToList(); // Returning the list of DynamicPages
    }
}
