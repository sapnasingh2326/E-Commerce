using ECommerceWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Text;
using OfficeOpenXml; // Ensure you have this package installed
using System.Collections.Generic;
using System.Linq;
using System.ComponentModel;

namespace ECommerceWeb.Pages.Admin
{
    public class OrderReportModel : PageModel
    {
        private readonly DemoProjectContext _context;

        public OrderReportModel(DemoProjectContext context)
        {
            _context = context;
        }

        public List<Order> OrderList { get; set; } = new List<Order>();

        [BindProperty]
        public Order CurrentOrder { get; set; } = new Order();
        public IEnumerable<Order> datalist { get; set; } //this is for list


        #region----GET METHOD-----
        public IActionResult OnGet(int? id, string searchQuery)
        {
            datalist = _context.Orders.Where(e => e.IsDeleted == false).ToList();
            IQueryable<Order> query = _context.Orders.Where(e => e.IsDeleted == false);

            // Apply search filter if provided
            if (!string.IsNullOrEmpty(searchQuery))
            {
                query = query.Where(e => e.OrderNumber.Contains(searchQuery));
            }

            // Fetch filtered data
            OrderList = query.ToList();

            // Handle specific order by ID if provided
            if (id.HasValue)
            {
                var order = _context.Orders.FirstOrDefault(e => e.Id == id && e.IsDeleted == false);
                if (order == null)
                {
                    return NotFound();
                }
                CurrentOrder = order;
            }

            return Page();
        }
        #endregion

        #region----DOWNLOAD CSV METHOD-----
        public IActionResult OnPostDownload()
        {
            // Generate CSV content
            var csv = new StringBuilder();
            csv.AppendLine("ID,Customer ID,Order Number,Order Date,Order Status");

            var orders = _context.Orders.Where(o => o.IsDeleted == false).ToList();
            foreach (var order in orders)
            {
                csv.AppendLine($"{order.Id},{order.CustomerId},{order.OrderNumber},{order.OrderDate:yyyy-MM-dd},{order.OrderStatus}");
                
            }

            // Convert CSV content to a file
            var fileName = $"OrderReport_{DateTime.Now:yyyyMMddHHmmss}.csv";
            var fileBytes = Encoding.UTF8.GetBytes(csv.ToString());
            return File(fileBytes, "text/csv", fileName);
        }
        #endregion

        #region----EXPORT TO EXCEL METHOD-----
        public IActionResult OnGetExportToExcel()
        {
            var orders = _context.Orders.Where(o => o.IsDeleted == false).ToList();

            using var package = new ExcelPackage();
            var worksheet = package.Workbook.Worksheets.Add("Orders");

            // Add Header
            worksheet.Cells[1, 1].Value = "Id";
            worksheet.Cells[1, 2].Value = "CustomerId";
            worksheet.Cells[1, 3].Value = "OrderNumber";
            worksheet.Cells[1, 4].Value = "OrderDate";
            worksheet.Cells[1, 5].Value = "OrderStatus";

            // Add Data
            for (int i = 0; i < orders.Count; i++)
            {
                worksheet.Cells[i + 2, 1].Value = orders[i].Id;
                worksheet.Cells[i + 2, 2].Value = orders[i].CustomerId;
                worksheet.Cells[i + 2, 3].Value = orders[i].OrderNumber;
                worksheet.Cells[i + 2, 4].Value = orders[i].OrderDate.HasValue? orders[i].OrderDate.Value.ToString("yyyy-MM-dd"): string.Empty;
                worksheet.Cells[i + 2, 5].Value = orders[i].OrderStatus;
            }

            // Format Table
            worksheet.Cells[worksheet.Dimension.Address].AutoFitColumns();

            var excelData = package.GetAsByteArray();
            return File(excelData, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "Orders.xlsx");
        }


        #endregion
    }
}
