namespace RestaurantMVC.Areas.Admin.Models
{
    public class ChefUpdateViewModel
    {
        public int Id { get; set; }
        public string? Fullname { get; set; }
        public string? Role { get; set; }
        public string? CoverImgUrl { get; set; }
        public IFormFile? CoverImageFile { get; set; }
    }
}
