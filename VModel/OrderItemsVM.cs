namespace ECommerceWeb.VModel
{
    public class OrderItemsVM
    {

        public int Id { get; set; }
        public int? ProductId { get; set; }
        public string? ProductKey { get; set; }
        public string? CatgProductName { get; set; }
        public string? ProductName { get; set; }
        public string? ShortDescription {  get; set; }
        public decimal? Price { get; set; } = default(decimal?);
        public int? Quantity { get; set; }
        public decimal? SubPrice { get; set; } = default(decimal?);
        public int? OrderNumber { get; set; }
        public decimal? TotalAmount { get; set; }

        //Address
        public int? RegistrationId { get; set; }
        public string? RegAddress {  get; set; }
        public decimal? Tax { get; set; }
        public bool IsDeleted { get; set; } // Add this line



    }
    
}
