using ECommerceWeb.Model;
using ECommerceWeb.VModel;
using System.Collections.Generic;
using System.Linq;

public interface INewArrivalProductService
{
    IEnumerable<ProductListVM> GetNewArrivalProducts();
}

public class NewArrivalProductService : INewArrivalProductService
{
    private readonly DemoProjectContext _context; 

    public NewArrivalProductService(DemoProjectContext context)
    {
        _context = context;
    }

    public IEnumerable<ProductListVM> GetNewArrivalProducts()
    {
        return _context.Products
            .Where(x => x.IsDeleted == false && x.IsNewarrivals == true )
            .Select(x => new ProductListVM
            {
                id = x.Id,
                ProductName = x.ProductName,
                ProductPrice = x.Price,
                ProductImage = x.ProductDisplayImage
            })
            .ToList();
    }
}
