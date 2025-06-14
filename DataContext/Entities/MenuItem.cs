﻿using System.ComponentModel.DataAnnotations.Schema;

namespace RestaurantMVC.DataContext.Entities
{
    public class MenuItem
    {
        public int Id { get; set; }
        public required string Name { get; set; }
        public required string Description { get; set; }
        public required decimal Price { get; set; }
        public required string ImgUrl { get; set; }
        [NotMapped]
        public IFormFile? Img{ get; set; }
        public required int CategoryId { get; set; }
        public Category? Category { get; set; }
        public bool IsAvailable { get; set; }
    }
}
