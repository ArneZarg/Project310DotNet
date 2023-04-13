using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace _310NutritionAPI.Models
{
    public class Product
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        public string Title { get; set; }
        [Required]
        public string Handle { get; set; }
        public string FeaturedImageSrc { get; set; }
        public double Price { get; set; }
        public double? CompareAtPrice { get; set; }
        public List<Variant>? Variants { get; set; }
        [ForeignKey("Collection")]
        public int? CollectionId { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        
    }
}
