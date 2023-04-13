using System.ComponentModel.DataAnnotations;

namespace _310NutritionAPI.Models.DTO
{
    public class ProductDTO
    {
        public int Id { get; set; }
        [Required]
        [MaxLength(100)]
        public string Title { get; set; }
        [Required]
        [MaxLength(100)]
        public string Handle { get; set; }
        public string FeaturedImageSrc { get; set; }
        public float Price { get; set; }
        public float CompareAtPrice { get; set; }
        public List<Variant>? Variants { get; set; }
        public int CollectionId { get; set; }
    }
}
