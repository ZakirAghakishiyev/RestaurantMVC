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
    public class TableController : AdminController
    {
        private readonly AppDbContext _dbContext;
        public TableController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<IActionResult> Index()
        {
            var tables=await _dbContext.Tables
                .OrderBy(t => t.Number)
                .ToListAsync();
            return View(tables);
        }

        public async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(TableCreateViewModel createView)
        {
            
            var tables = await _dbContext.Tables.ToListAsync();
            var tableListItems = tables.Select(x => new SelectListItem { Value = x.Id.ToString() }).ToList();

            var existingTable = await _dbContext.Tables
            .FirstOrDefaultAsync(t => t.Number == createView.Number);

            if (existingTable != null)
            {
                ModelState.AddModelError("Number", "Masa Artıq Mövcuddur.");
                return View(createView);
            }
            if (!ModelState.IsValid)
            {
                return View(createView);
            }

            await _dbContext.Tables.AddAsync(new Table { Capacity = createView.Capacity, Number=createView.Number });
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] RequestModel requestModel)
        {
            var table = await _dbContext.Tables.FindAsync(requestModel.Id);

            if (table == null) return NotFound();

            var removedTable = _dbContext.Tables.Remove(table);
            await _dbContext.SaveChangesAsync();
            return Json(removedTable.Entity);
        }

        public async Task<IActionResult> Update(int id)
        {
            var table = await _dbContext.Tables.FindAsync(id);
            if (table == null) return NotFound();

            var viewModel = new Table
            {
                Id = table.Id,
                Number = table.Number,
                Capacity = table.Capacity
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Update(Table model)
        {
            if (!ModelState.IsValid) return View(model);

            var table = await _dbContext.Tables.FirstOrDefaultAsync(x => x.Id == model.Id);
            if (table == null) return NotFound();

            var duplicate = await _dbContext.Tables
            .FirstOrDefaultAsync(x => x.Number == model.Number && x.Id != model.Id);

            if (duplicate != null)
            {
                ModelState.AddModelError("Number", "Masa Artıq Mövcuddur.");
                return View(model);
            }

            table.Number = model.Number != null ? model.Number : table.Number;
            table.Capacity = model.Capacity != null ? model.Capacity : table.Capacity;


            _dbContext.Tables.Update(table);
            await _dbContext.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }
    }
}
