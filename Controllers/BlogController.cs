﻿using Microsoft.AspNetCore.Mvc;

namespace RestaurantMVC.Controllers
{
    public class BlogController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
