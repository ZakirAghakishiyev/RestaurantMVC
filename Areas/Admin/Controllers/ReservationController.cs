using AdminPanelPractice.Areas.Admin.Data;
using AdminPanelPractice.Areas.Admin.Extentions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantMVC.DataContext;
using RestaurantMVC.DataContext.Entities;
using RestaurantMVC.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace RestaurantMVC.Areas.Admin.Controllers
{
    public class ReservationController : AdminController
    {
        private readonly AppDbContext _dbContext;

        public ReservationController(AppDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<IActionResult> Index(string filter)
        {
            var query = _dbContext.Reservations
                .Include(r => r.Table)
                .AsQueryable();

            if (filter == "upcoming")
            {
                query = query.Where(r => r.ReservationStartTime >= DateTime.Now);
            }
            else if (filter == "past")
            {
                query = query.Where(r => r.ReservationEndTime < DateTime.Now);
            }

            var reservations = await query.OrderByDescending(r => r.ReservationStartTime).ToListAsync();
            return View(reservations);
        }

        //public async Task<IActionResult> Create()
        //{
        //    ViewBag.Tables = await _dbContext.Tables
        //        .Select(t => new SelectListItem
        //        {
        //            Text = $"Table #{t.Id} - Capacity: {t.Capacity}",
        //            Value = t.Id.ToString()
        //        }).ToListAsync();

        //    return View();
        //}

        //[HttpPost]
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> Create(ReservationCreateViewModel model)
        //{
        //    ViewBag.Tables = await _dbContext.Tables
        //        .Select(t => new SelectListItem
        //        {
        //            Text = $"Table #{t.Id} - Capacity: {t.Capacity}",
        //            Value = t.Id.ToString()
        //        }).ToListAsync();

        //    if (!ModelState.IsValid) return View(model);

        //    var table = await _dbContext.Tables.FindAsync(model.TableId);
        //    if (table == null)
        //    {
        //        ModelState.AddModelError("TableId", "Selected table does not exist.");
        //        return View(model);
        //    }

        //    var overlappingReservation = await _dbContext.Reservations.AnyAsync(r =>
        //        r.TableId == model.TableId &&
        //        r.ReservationStartTime == model.ReservationStartTime);

        //    if (overlappingReservation)
        //    {
        //        ModelState.AddModelError("", "This table is already reserved at the selected time.");
        //        return View(model);
        //    }

        //    var reservation = new Reservation
        //    {
        //        GuestFirstName = model.GuestFirstName,
        //        GuestLastName = model.GuestLastName,
        //        GuestPhoneNumber = model.GuestPhoneNumber,
        //        TableId = model.TableId,
        //        ReservationStartTime = model.ReservationStartTime,
        //        ReservationEndTime = model.ReservationStartTime.AddHours(2)
        //    };

        //    await _dbContext.Reservations.AddAsync(reservation);
        //    await _dbContext.SaveChangesAsync();

        //    return RedirectToAction(nameof(Index));
        //}

        [HttpPost]
        public async Task<IActionResult> Delete([FromBody] RequestModel requestModel)
        {
            var reservation = await _dbContext.Reservations.FindAsync(requestModel.Id);
            if (reservation == null) return NotFound();

            _dbContext.Reservations.Remove(reservation);
            await _dbContext.SaveChangesAsync();

            return Json(reservation);
        }
        public async Task<IActionResult> Details(int id)
        {
            var reservation = await _dbContext.Reservations
                .Include(r => r.Table)
                .FirstOrDefaultAsync(r => r.Id == id);

            if (reservation == null) return NotFound();

            var orders = await _dbContext.LocalOrders
                .Where(o => o.ReservationID == id)
                .Include(o => o.OrderItems)
                    .ThenInclude(oi => oi.MenuItem)
                .ToListAsync();

            var viewModel = new ReservationDetailsViewModel
            {
                Id = reservation.Id,
                GuestFirstName = reservation.GuestFirstName,
                GuestLastName = reservation.GuestLastName,
                GuestPhoneNumber = reservation.GuestPhoneNumber,
                TableNo = reservation.Table?.Number ?? 0,
                ReservationStartTime = reservation.ReservationStartTime,
                ReservationEndTime = reservation.ReservationEndTime,
                LocalOrders = orders.Select(o => new LocalOrderViewModel
                {
                    Id = o.Id,
                    TableNo = o.TableNo,
                    OrderDate = o.OrderDate,
                    OrderItems = o.OrderItems.Select(i => new LocalOrderItemViewModel
                    {
                        Id = i.Id,
                        MenuItemId = i.MenuItemId,
                        MenuItemName = i.MenuItem?.Name ?? "Unknown"
                    }).ToList()
                }).ToList()
            };

            return View(viewModel);
        }
    }
}

