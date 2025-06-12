using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantMVC.DataContext.Entities
{
    public class Category
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string CoverImgUrl {  get; set; }
        [NotMapped]
        public IFormFile? CoverImageFile { get; set; }
    }

}
