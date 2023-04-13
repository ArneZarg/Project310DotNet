using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace _310NutritionAPI.Models
{
    public class Collection
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        [Required]
        [MaxLength(80)]
        public string Title { get; set; }
        [Required]
        [MaxLength(100)]
        public string Handle { get; set; }
        public string? Excerpt { get; set; }
        public string? BannerImgSrc { get; set; }
        public List<Product> CollectionProducts { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
