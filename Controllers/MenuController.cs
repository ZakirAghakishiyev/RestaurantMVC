using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantMVC.DataContext;
using RestaurantMVC.Models;

namespace RestaurantMVC.Controllers
{
    public class MenuController : Controller
    {

        private readonly AppDbContext _dbContext;

        public MenuController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            var menuItems = await _dbContext.MenuItems
                .Where(x => x.IsAvailable)
                .Include(x => x.Category)
                .ToListAsync();

            var categories = await _dbContext.Categories.ToListAsync();

            var viewModel = new ShopViewModel
            {
                MenuItems = menuItems,
                Categories = categories,
                CurrentPage = 1,
                TotalPages = 1
            };

            return View(viewModel);
        }

    }
}
