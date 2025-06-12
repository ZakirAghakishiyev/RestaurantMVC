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
    public class ChefController : AdminController
    {
        private readonly AppDbContext _dbContext;
        public ChefController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            var chefs = await _dbContext.Chefs
                .ToListAsync();
            return View(chefs);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ChefCreateViewModel createView)
        {
            var chefs = await _dbContext.Chefs.ToListAsync();
            var chefsListItems = chefs.Select(x => new SelectListItem { Text = x.Fullname, Value = x.Id.ToString() }).ToList();

            if (!ModelState.IsValid)
            {
                return View(createView);
            }

            if (!createView.CoverImageFile.IsImage())
            {
                ModelState.AddModelError("CoverImageFile", "Şəkil seçilməlidir!");
                return View(createView);
            }

            var unicalCoverImageFileName = await createView.CoverImageFile.GenerateFile(FilePathConstants.ChefPath);


            await _dbContext.Chefs.AddAsync(new Chef { Fullname = createView.Fullname,Role=createView.Role, CoverImgUrl = unicalCoverImageFileName });
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] RequestModel requestModel)
        {
            var chef = await _dbContext.Chefs.FindAsync(requestModel.Id);

            if (chef == null) return NotFound();

            var removedChef = _dbContext.Chefs.Remove(chef);
            await _dbContext.SaveChangesAsync();

            if (removedChef != null)
            {
                System.IO.File.Delete(Path.Combine(FilePathConstants.ChefPath, chef.CoverImgUrl));
            }

            return Json(removedChef.Entity);
        }

        public async Task<IActionResult> Update(int id)
        {
            var chef = await _dbContext.Chefs.FindAsync(id);
            if (chef == null) return NotFound();

            var viewModel = new ChefUpdateViewModel
            {
                Id = chef.Id,
                Fullname = chef.Fullname,
                Role = chef.Role,
                CoverImgUrl = chef.CoverImgUrl
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(ChefUpdateViewModel model)
        {
            if (!ModelState.IsValid) return View(model);

            var chef = await _dbContext.Chefs.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (chef == null) return NotFound();

            chef.Fullname = model.Fullname!=null?model.Fullname:chef.Fullname;
            chef.Role=model.Role!=null?model.Role:chef.Role;

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

                var uniqueFileName = await model.CoverImageFile.GenerateFile(FilePathConstants.ChefPath);

                if (!string.IsNullOrWhiteSpace(chef.CoverImgUrl))
                {
                    var oldImagePath = Path.Combine(FilePathConstants.ChefPath, chef.CoverImgUrl);
                    if (System.IO.File.Exists(oldImagePath))
                    {
                        System.IO.File.Delete(oldImagePath);
                    }
                }

                chef.CoverImgUrl = uniqueFileName;
            }

            _dbContext.Chefs.Update(chef);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

    }
}
