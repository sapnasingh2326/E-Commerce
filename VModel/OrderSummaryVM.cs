namespace ECommerceWeb.VModel
{
    public class OrderSummaryVM
    {
        public decimal? TotalAmount { get; set; }
        public decimal? Price { get; set; } = default(decimal?);
        public decimal? SubPrice { get; set; } = default(decimal?);
        public decimal? Subtotal { get; set; }
        public decimal? Tax { get; set; }
        public decimal? Shipping { get; set; }
        public decimal? Total { get; set; }
    }
}
