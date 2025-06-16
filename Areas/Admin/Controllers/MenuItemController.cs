using AdminPanelPractice.Areas.Admin.Data;
using AdminPanelPractice.Areas.Admin.Extentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantMVC.Areas.Admin.Models;
using RestaurantMVC.DataContext;
using RestaurantMVC.DataContext.Entities;
using System.Reflection.Metadata;

namespace RestaurantMVC.Areas.Admin.Controllers
{
    public class MenuItemController : AdminController
    {
        private readonly AppDbContext _dbContext;
        public MenuItemController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        //public async Task<IActionResult> Index()
        //{
        //    var menuItems = await _dbContext.MenuItems.ToListAsync();
        //    return View(menuItems);
        //}
        public async Task<IActionResult> Index()
        {
            var menuItems = await _dbContext.MenuItems
                .Include(c => c.Category)
                .ToListAsync();
            return View(menuItems);
        }

        public async Task<IActionResult> Create()
        {
            var categories = await _dbContext.Categories.ToListAsync();
            ViewBag.Categories = categories
                .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
                .ToList();

            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(MenuItemCreateViewModel createView)
        {
            var categories = await _dbContext.Categories.ToListAsync();
            var categoryListItems = categories
                .Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() })
                .ToList();

            ViewBag.Categories = categoryListItems;

            if (!ModelState.IsValid)
            {
                return View(createView);
            }

            if (createView.Img == null || !createView.Img.IsImage())
            {
                ModelState.AddModelError("Img", "Sekil olmalidir.");
                return View(createView);
            }

            var uniqueImgFileName = await createView.Img.GenerateFile(FilePathConstants.MenuItemPath);

            var menuItem = new MenuItem
            {
                Name = createView.Name,
                Description = createView.Description,
                Price = createView.Price,
                ImgUrl = uniqueImgFileName,
                CategoryId = createView.CategoryId,
                IsAvailable = createView.IsAvailable
            };

            await _dbContext.MenuItems.AddAsync(menuItem);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Update(int id)
        {
            var menuItem = await _dbContext.MenuItems.FindAsync(id);
            if (menuItem == null) return NotFound();

            var categories = await _dbContext.Categories.ToListAsync();
            ViewBag.Categories = categories.Select(x => new SelectListItem
            {
                Text = x.Name,
                Value = x.Id.ToString()
            }).ToList();

            return View(menuItem);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        
        public async Task<IActionResult> Update(MenuItem model)
        {
            Console.WriteLine("Entered");
            if (!ModelState.IsValid)
            {
                var categories = await _dbContext.Categories.ToListAsync();
                ViewBag.Categories = categories.Select(x => new SelectListItem
                {
                    Text = x.Name,
                    Value = x.Id.ToString()
                }).ToList();
                return View(model);
            }

            var menuItem = await _dbContext.MenuItems.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (menuItem == null) return NotFound();

            menuItem.Name = model.Name ?? menuItem.Name;
            menuItem.Description = model.Description ?? menuItem.Description;
            menuItem.Price = model.Price;
            menuItem.CategoryId = model.CategoryId;
            menuItem.IsAvailable = model.IsAvailable;

            if (model.Img != null)
            {
                if (!model.Img.IsImage())
                {
                    ModelState.AddModelError("Img", "Sekil olmalidir.");
                    var categories = await _dbContext.Categories.ToListAsync();
                    ViewBag.Categories = categories.Select(x => new SelectListItem
                    {
                        Text = x.Name,
                        Value = x.Id.ToString()
                    }).ToList();
                    return View(model);
                }

                var uniqueFileName = await model.Img.GenerateFile(FilePathConstants.MenuItemPath);

                if (!string.IsNullOrWhiteSpace(menuItem.ImgUrl))
                {
                    var oldImagePath = Path.Combine(FilePathConstants.MenuItemPath, menuItem.ImgUrl);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                menuItem.ImgUrl = uniqueFileName;
            }

            _dbContext.MenuItems.Update(menuItem);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Details(int id)
        {
            var menuItem= await _dbContext.MenuItems
                .Include(x => x.Category)
                .SingleOrDefaultAsync(x => x.Id == id);  
            return View(menuItem);
        }
    }
}
