using RestaurantMVC.DataContext.Entities;

namespace RestaurantMVC.Areas.Admin.Models
{
    public class MenuItemCreateViewModel
    {
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required decimal Price { get; set; }
        public IFormFile? Img { get; set; }
        public required int CategoryId { get; set; }
        public Category? Category { get; set; }
        public bool IsAvailable { get; set; }
    }
}
