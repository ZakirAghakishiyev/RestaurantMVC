using Microsoft.AspNetCore.Mvc;

namespace RestaurantMVC.Areas.Admin.Controllers
{
    public class DashboardController : AdminController
    {
        public IActionResult Index()
        {
            return View();
        }
    } 
}
