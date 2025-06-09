using Microsoft.EntityFrameworkCore;
using RestaurantMVC.DataContext.Entities;

namespace RestaurantMVC.DataContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Chef> Chefs { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<MenuItem> MenuItem { get; set; }   
        public DbSet<Reservation> Reservations { get; set; }

    }
}
