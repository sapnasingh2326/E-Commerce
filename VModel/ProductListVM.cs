using System.ComponentModel.DataAnnotations;

namespace ECommerceWeb.VModel
{
    public class ProductListVM
    {
        public int id { get; set; }


        [Required(ErrorMessage = "Product Name is required.")]
        [StringLength(20, ErrorMessage = "Product Name cannot be longer than 20 characters.")]
        public string? ProductName { get; set; }

        [Required(ErrorMessage = "Category Name is required.")]
        [StringLength(50, ErrorMessage = "Category Name cannot be longer than 50 characters.")]
        public string? CategoryName { get; set; }

        [StringLength(50, ErrorMessage = "SubCategory Name cannot be longer than 50 characters.")]
        public string? SubCategoryName { get; set; }

        [Range(100, 10000, ErrorMessage = "The Price must be between 100 and 10,000.")]
        public decimal? ProductPrice { get; set; }

        [StringLength(500, ErrorMessage = "Product Description cannot be longer than 400 characters.")]
        public string? ProductDescription { get; set; }

        [Url(ErrorMessage = "Product Image must be a valid URL.")]
        public string? ProductImage { get; set; }

        [StringLength(20, ErrorMessage = "Product Size cannot be longer than 20 characters.")]
        public string? ProductSize { get; set; }

        [StringLength(20, ErrorMessage = "Product Color cannot be longer than 20 characters.")]
        public string? ProductColor { get; set; }

        [Required(ErrorMessage = "Product Code is required.")]
        [StringLength(20, ErrorMessage = "Product Code cannot be longer than 20 characters.")]
        public string? ProductCode { get; set; }
        




    }
}
