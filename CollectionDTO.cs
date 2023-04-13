namespace _310NutritionAPI.Models.DTO
{
    public class CollectionDTO
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Handle { get; set; }
        public string? Excerpt { get; set; }
        public string? BannerImgSrc { get; set; }
        public List<ProductDTO> CollectionProducts { get; set; }
    }
}
