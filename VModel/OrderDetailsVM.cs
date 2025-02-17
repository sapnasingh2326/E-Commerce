namespace ECommerceWeb.VModel
{
    public class OrderDetailsVM
    {

        public int Id { get; set; }

        public int? CustomerId { get; set; }

        public string? OrderNumber { get; set; }

        public DateTime? OrderDate { get; set; }

        public int? OrderStatus { get; set; }

        public IEnumerable<OrderItemsList>? orderItems { get; set; }
    }


    public class OrderItemsList
    {
        public int Id { get; set; }
        public int? ProductId { get; set; }
        public string? ProductName { get; set; }
        public decimal? Price { get; set; } = default(decimal?);
        public int? Quantity { get; set; }

    }
}