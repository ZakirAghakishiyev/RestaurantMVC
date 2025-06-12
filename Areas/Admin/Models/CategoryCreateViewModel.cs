using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantMVC.Areas.Admin.Models;

public class CategoryCreateViewModel
{
    public required string Name { get; set; }
    public required IFormFile CoverImageFile { get; set; }
}
