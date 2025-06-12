using AdminPanelPractice.Areas.Admin.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace RestaurantMVC.Areas.Admin.Controllers
{
    public class RoleController : AdminController
    {
        private readonly RoleManager<IdentityRole> _roleManager;

        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            var roles = _roleManager.Roles.ToList();
            return View(roles);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(string roleName)
        {
            if (string.IsNullOrWhiteSpace(roleName))
            {
                ModelState.AddModelError("", "Role name is required.");
                return View();
            }

            var roleExists = await _roleManager.RoleExistsAsync(roleName);
            if (roleExists)
            {
                ModelState.AddModelError("", "Role already exists.");
                return View();
            }

            var result = await _roleManager.CreateAsync(new IdentityRole(roleName));
            if (result.Succeeded)
                return RedirectToAction(nameof(Index));

            foreach (var error in result.Errors)
                ModelState.AddModelError("", error.Description);

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] RequestModelString requestModel)
        {
            var role = await _roleManager.FindByIdAsync(requestModel.Id);

            if (role == null)
                return NotFound();

            var result = await _roleManager.DeleteAsync(role);

            if (result.Succeeded)
                return Json(new { success = true, deletedRoleId = role.Id });

            return BadRequest(new { success = false, errors = result.Errors.Select(e => e.Description) });
        }

    }
}

