using Microsoft.AspNetCore.Mvc;

namespace RestaurantMVC.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AdminController : Controller
    {
    }

    public class RequestModel
    {
        public int Id { get; set; }
        public int StartFrom { get; set; }
        public string? ImageName { get; set; }
    }

    public class RequestModelString
    {
        public string? Id { get; set; }
    }
}
