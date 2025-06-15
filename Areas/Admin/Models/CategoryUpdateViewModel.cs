namespace RestaurantMVC.Areas.Admin.Models;

public class CategoryUpdateViewModel
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? CoverImgUrl { get; set; }
    public IFormFile? CoverImageFile { get; set; }
}

