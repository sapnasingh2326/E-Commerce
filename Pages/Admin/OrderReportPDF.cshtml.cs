using ECommerceWeb.Model;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OfficeOpenXml;
using System.Text;

namespace ECommerceWeb.Pages.Admin
{
    public class OrderReportPDFModel : PageModel
    {
        private readonly DemoProjectContext _context;

        public OrderReportPDFModel(DemoProjectContext context)
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

        public IActionResult OnGetExportToPDF()
        {
            // Retrieve orders from the database
            var orders = _context.Orders.Where(o => o.IsDeleted == false).ToList();

            // Create a memory stream
            using (var stream = new MemoryStream())
            {
                try
                {
                    // Initialize the PdfWriter with the memory stream
                    var writer = new iText.Kernel.Pdf.PdfWriter(stream);
                    var pdfDoc = new iText.Kernel.Pdf.PdfDocument(writer);
                    var document = new iText.Layout.Document(pdfDoc);

                    // Add a title
                    var title = new iText.Layout.Element.Paragraph("Order Report")
                        .SetTextAlignment(iText.Layout.Properties.TextAlignment.CENTER)
                        .SetFontSize(18)
                        ;
                    document.Add(title);

                    // Create a table with 5 columns
                    var table = new iText.Layout.Element.Table(5).UseAllAvailableWidth();
                    table.AddHeaderCell("ID");
                    table.AddHeaderCell("Customer ID");
                    table.AddHeaderCell("Order Number");
                    table.AddHeaderCell("Order Date");
                    table.AddHeaderCell("Order Status");

                    // Populate table with data
                    foreach (var order in orders)
                    {
                        table.AddCell(order.Id.ToString() ?? "N/A");
                        table.AddCell(order.CustomerId?.ToString() ?? "N/A");
                        table.AddCell(order.OrderNumber ?? "N/A");
                        //table.AddCell(order.OrderDate.ToString("yyyy-MM-dd"));
                        //table.AddCell(order.OrderStatus.ToString ?? "N/A");
                    }

                    document.Add(table);
                    document.Close();

                    // Reset the position of the memory stream
                    stream.Position = 0;

                    // Return the PDF file as a downloadable response
                    return File(stream.ToArray(), "application/pdf", "OrderReport.pdf");
                }
                catch (Exception ex)
                {
                    // Log or handle the error as needed
                    return BadRequest($"An error occurred while generating the PDF: {ex.Message}");
                }
            }
        }





    }
}
