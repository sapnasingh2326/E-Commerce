using ECommerceWeb.Model;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace ECommerceWeb.VModel
{
    public class AddressBookVM
    {
        public AddressBookVM()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public int? CityId { get; set; }
        public string? CustmerAddress { get; set; }
        public bool? IsDeleted { get; set; }
        public int? UserId { get; set; }
        public string DeliverLocation { get; set; }

        public string City { get; set; }
        public string User { get; set; }

        public AddressBook addressBook { get; set; } // Add property for a single address book
        public IEnumerable<AddressBook> addressBookList { get; set; } // Add property for list of addresses
        public IEnumerable<SelectListItem> CitySelectList { get; set; } // For the dropdown

        public virtual ICollection<Order> Orders { get; set; }
    }
}
