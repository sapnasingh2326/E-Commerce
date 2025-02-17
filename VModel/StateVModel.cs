namespace ECommerceWeb.VModel
{
    public class StateVModel
    {
        public int Id { get; set; }

        public string? CityName { get; set; }
        public string? StateName { get; set; }
        public string? CountryName { get; set; }

        public int? CountryId { get; set; }
    }
}
