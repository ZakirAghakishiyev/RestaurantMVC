using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantMVC.DataContext;

namespace RestaurantMVC.Controllers
{
    public class HomeController : Controller
    {
        private readonly AppDbContext _dbContext;

        public HomeController(AppDbContext context)
        {
            _dbContext = context;
        }

        public async Task<IActionResult> Index()
        {
            var chefs=await _dbContext.Chefs.ToListAsync();
            return View(chefs);
        }
    }
}
