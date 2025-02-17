using ECommerceWeb.Model;

namespace ECommerceWeb.VModel
{
    public class CatSubCatListVModel
    {
       public Category Category { get; set; }
        public IEnumerable<SubCategory> SubCategories { get; set; }
    }
}
