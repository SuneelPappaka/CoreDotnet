using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoreDotnet.Models
{
    public class Product
    {
        [Key]
        public int? Id { get; set; }
        public string ProductName { get; set; }
        public string Description { get; set; }
        public decimal Price { get; set; }
        public string ImagePath { get; set; }
        public DateTime CreatedAt { get; set; }
        [NotMapped]
        public IFormFile? ImageFile { get; set; }
    }
}
