namespace ECommerceWeb.VModel
{
    public class CityVModel
    {
        public int Id { get; set; }

        public string? CityName { get; set; }
        public string? StateName { get; set; }
        public string? CountryName { get; set; }

        public int? StateId { get; set; }
        public int? CountryId { get; set;}
    }
}
