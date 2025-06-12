using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Pb304PetShop.DataContext.Entities;
using RestaurantMVC.DataContext.Entities;

namespace RestaurantMVC.DataContext
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Chef> Chefs { get; set; }
        public DbSet<LocalOrder> LocalOrders { get; set; }
        public DbSet<OnlineOrder> OnlineOrders { get; set; }
        public DbSet<OnlineOrderItem> OnlineOrderItems { get; set; }
        public DbSet<LocalOrderItem> LocalOrderItems { get; set; }
        public DbSet<Table> Tables { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }   
        public DbSet<Reservation> Reservations { get; set; }


    }
}
