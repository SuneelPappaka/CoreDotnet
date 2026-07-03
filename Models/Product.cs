using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreDotnet.Models
{
    public class Product
    {
        [Key]
        public int? Id { get; set; }
        [Required(ErrorMessage ="Product Name Is Required")]
        public string ProductName { get; set; }
        [Required(ErrorMessage = "Description is required.")]
        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Price must be greater than zero.")]
        public decimal Price { get; set; }
        
        public string ImagePath { get; set; }
        public DateTime CreatedAt { get; set; }
        [NotMapped]
        [Required(ErrorMessage = "Image File is required.")]
        //[FileExtensions(Extensions = "jpg,jpeg,png,gif", ErrorMessage = "Please upload a valid image file (jpg, jpeg, png, gif).")]
        public IFormFile? ImageFile { get; set; }
    }
}
