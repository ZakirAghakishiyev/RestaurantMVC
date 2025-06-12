using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantMVC.DataContext.Entities
{
    public class Chef
    {
        public int Id { get; set; }
        public required string Fullname { get; set; }
        public required string Role { get; set; }
        public required string CoverImgUrl { get; set; }
        [NotMapped]
        public IFormFile? CoverImageFile { get; set; }
    }

}
