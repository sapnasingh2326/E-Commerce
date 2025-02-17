using ECommerceWeb.Model;
using ECommerceWeb.VModel;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace ECommerceWeb.Pages.Admin
{
    public class DashboardModel : PageModel
    {

        private readonly DemoProjectContext _context;

        public DashboardModel(DemoProjectContext context)
        {
            _context = context;
            dashboardVM = new DashboardVM();
        }

        public IEnumerable<Order> OrderItems { get; set; } 
        public IEnumerable<Product> Products { get; set; } 
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<SubCategory> SubCategories { get; set; }
        public IEnumerable<Brand> Brands { get; set; }



        public DashboardVM dashboardVM { get; set; }

        public void OnGet()
        {
            dashboardVM.Product = _context.Products.Where(x => x.IsDeleted == false).Count();
            dashboardVM.Order = _context.OrderLists.Where(x => x.IsDeleted == false).Count();
            dashboardVM.Order = _context.OrderItems.Where(x => x.IsDeleted == false).Count();
            dashboardVM.Coupons = _context.Carts.Where(x => x.IsDeleted == false).Count();
            dashboardVM.City = _context.Cities.Where(x => x.IsDeleted == false).Count();
            dashboardVM.Helpers = _context.Roles.Where(x => x.IsDeleted == false).Count();
            dashboardVM.Brand = _context.Brands.Where(x => x.IsDeleted == false).Count();
            dashboardVM.Category = _context.Categories.Where(x => x.IsDeleted == false).Count();
            dashboardVM.SubCategory = _context.SubCategories.Where(x => x.IsDeleted == false).Count();
            dashboardVM.Price = _context.Products.Where(x => x.IsDeleted == false).Count();
            dashboardVM.Role = _context.Roles.Where(x => x.IsDeleted != false).Count();
            dashboardVM.MonthlyOrder = _context.OrderLists.Where(x =>x.IsDeleted == false).Count();
            dashboardVM.DayOfWeek = _context.NewsletterSubscriptions.Where(x =>x.IsDeleted==false).Count();
            dashboardVM.CouponsBook = _context.Carts.Where(x => x.IsDeleted == false).Count();
            dashboardVM.PhysicalCoupons =_context.Carts.Where(x =>x.IsDeleted == false).Count();
            dashboardVM.VirtualCoupons =_context.Carts.Where(x => x.IsDeleted == false).Count();
            dashboardVM.Order = _context.OrderDetails.Where(x => x.IsDeleted==false).Count();
            





        }
    }
}