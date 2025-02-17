namespace ECommerceWeb.VModel
{
    public class RegistrationVM
    {
        public int Id { get; set; }
        public string? RoleName { get; set; }
        public string? Name { get; set; }
        public string? UserName{ get; set; }
        public string? Mobile { get; set; }
        public string? Email { get; set; }
        public string? Address {  get; set; }
        public string? Password { get; set; }
        public string? PostCode { get; set; }
        public DateTime? CreatedDate { get; set;}
        public string? ImageUrl { get; set; }

    }
}
