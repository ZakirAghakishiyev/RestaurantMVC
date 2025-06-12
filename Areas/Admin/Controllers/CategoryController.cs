using AdminPanelPractice.Areas.Admin.Data;
using AdminPanelPractice.Areas.Admin.Extentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RestaurantMVC.Areas.Admin.Models;
using RestaurantMVC.DataContext;
using RestaurantMVC.DataContext.Entities;

namespace RestaurantMVC.Areas.Admin.Controllers
{
    public class CategoryController : AdminController
    {
        private readonly AppDbContext _dbContext;
        public CategoryController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            var categories = await _dbContext.Categories
                .ToListAsync();
            return View(categories);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateViewModel createView)
        {
            var categories = await _dbContext.Categories.ToListAsync();
            var categoryListItems = categories.Select(x => new SelectListItem { Text = x.Name, Value = x.Id.ToString() }).ToList();

            if (!ModelState.IsValid)
            {
                return View(createView);
            }

            if (!createView.CoverImageFile.IsImage())
            {
                ModelState.AddModelError("CoverImageFile", "Şəkil seçilməlidir!");
                return View(createView);
            }


            if (await _dbContext.Categories.AnyAsync(t => t.Name == createView.Name))
            {
                ModelState.AddModelError("Name", "Bu adda category artiq movcuddur!");
                return View(createView);
            }


            var unicalCoverImageFileName = await createView.CoverImageFile.GenerateFile(FilePathConstants.CategoryPath);


            await _dbContext.Categories.AddAsync(new Category { Name = createView.Name, CoverImgUrl = unicalCoverImageFileName });
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] RequestModel requestModel)
        {
            var category = await _dbContext.Categories.FindAsync(requestModel.Id);

            if (category == null) return NotFound();

            var removedCategory = _dbContext.Categories.Remove(category);
            await _dbContext.SaveChangesAsync();

            if (removedCategory != null)
            {
                System.IO.File.Delete(Path.Combine(FilePathConstants.CategoryPath, category.CoverImgUrl));
            }

            return Json(removedCategory.Entity);
        }

        public async Task<IActionResult> Update(int id)
        {
            var category = await _dbContext.Categories.FindAsync(id);
            if (category == null) return NotFound();

            var viewModel = new CategoryUpdateViewModel
            {
                Id = category.Id,
                Name = category.Name,
                CoverImgUrl = category.CoverImgUrl
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(CategoryUpdateViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var category = await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (category == null) return NotFound();

            category.Name = model.Name != null ? model.Name : category.Name;

            if (model.CoverImageFile != null)
            {
                if (!model.CoverImageFile.IsImage())
                {
                    ModelState.AddModelError("CoverImageFile", "Sekil secilmelidir!");
                    return View(model);
                }

                if (!model.CoverImageFile.IsAllowedSize(1))
                {
                    ModelState.AddModelError("CoverImageFile", "Sekil hecmi 1mb-dan cox ola bilmez");
                    return View(model);
                }

                var uniqueFileName = await model.CoverImageFile.GenerateFile(FilePathConstants.CategoryPath);

                if (!string.IsNullOrWhiteSpace(category.CoverImgUrl))
                {
                    var oldImagePath = Path.Combine(FilePathConstants.CategoryPath, category.CoverImgUrl);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                category.CoverImgUrl = uniqueFileName;
            }

            _dbContext.Categories.Update(category);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
