namespace RestaurantMVC.Areas.Admin.Models
{
    public class ChefCreateViewModel
    {
        public required string Fullname { get; set; }
        public required string Role { get; set; }
        public required IFormFile CoverImageFile { get; set; }
    }
}
