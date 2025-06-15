using Microsoft.AspNetCore.Mvc;
using RestaurantMVC.DataContext.Entities;
using RestaurantMVC.DataContext;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using RestaurantMVC.Areas.Admin.Models;

namespace RestaurantMVC.Areas.Admin.Controllers;

public class LocalOrderController : AdminController
{
    private readonly AppDbContext _DbContext;

    public LocalOrderController(AppDbContext context)
    {
        _DbContext = context;
    }

    public async Task<IActionResult> Index()
    {
        var orders = await _DbContext.LocalOrders
            .Include(o => o.Reservation)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
            .ToListAsync();
        return View(orders);
    }


    public IActionResult Create()
    {
        ViewBag.Reservations = new SelectList(_DbContext.Reservations
            .Select(r => new { r.Id, Name = $"{r.GuestFirstName} {r.GuestLastName}" }), "Id", "Name");

        ViewBag.MenuItems = new MultiSelectList(_DbContext.MenuItems
            .Where(m => m.IsAvailable), "Id", "Name");

        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create(LocalOrderCreateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Reservations = new SelectList(_DbContext.Reservations
                .Select(r => new { r.Id, Name = $"{r.GuestFirstName} {r.GuestLastName}" }), "Id", "Name");

            ViewBag.MenuItems = new SelectList(_DbContext.MenuItems.Where(m => m.IsAvailable), "Id", "Name");
            return View(model);
        }

        var order = new LocalOrder
        {
            TableNo = model.TableNo,
            ReservationID = model.ReservationID,
            OrderDate = DateTime.Now,
            OrderItems = model.Items.Select(i => new LocalOrderItem
            {
                MenuItemId = i.MenuItemId,
                Count = i.Count
            }).ToList()
        };

        _DbContext.LocalOrders.Add(order);
        await _DbContext.SaveChangesAsync();

        return RedirectToAction("Index");
    }
    public async Task<IActionResult> Update(int id)
    {
        var order = await _DbContext.LocalOrders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null) return NotFound();

        var viewModel = new LocalOrderUpdateViewModel
        {
            Id = order.Id,
            TableNo = order.TableNo,
            ReservationID = order.ReservationID,
            Items = order.OrderItems.Select(oi => new OrderItemInput
            {
                MenuItemId = oi.MenuItemId,
                Count = oi.Count
            }).ToList()
        };

        ViewBag.Reservations = new SelectList(
            _DbContext.Reservations
                .Select(r => new { r.Id, Name = $"{r.GuestFirstName} {r.GuestLastName}" }),
            "Id", "Name", viewModel.ReservationID
        );

        ViewBag.MenuItems = new SelectList(
            _DbContext.MenuItems
                .Where(m => m.IsAvailable),
            "Id", "Name"
        );

        return View(viewModel);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Update(LocalOrderUpdateViewModel model)
    {
        if (!ModelState.IsValid)
        {
            ViewBag.Reservations = new SelectList(_DbContext.Reservations
                .Select(r => new { r.Id, Name = $"{r.GuestFirstName} {r.GuestLastName}" }),
                "Id", "Name", model.ReservationID);

            ViewBag.MenuItems = new SelectList(_DbContext.MenuItems.Where(m => m.IsAvailable), "Id", "Name");
            return View(model);
        }

        var order = await _DbContext.LocalOrders
            .Include(o => o.OrderItems)
            .FirstOrDefaultAsync(o => o.Id == model.Id);

        if (order == null) return NotFound();

        order.TableNo = model.TableNo;
        order.ReservationID = model.ReservationID;

        _DbContext.LocalOrderItems.RemoveRange(order.OrderItems);

        order.OrderItems = model.Items.Select(i => new LocalOrderItem
        {
            MenuItemId = i.MenuItemId,
            Count = i.Count,
            OrderId = order.Id
        }).ToList();

        await _DbContext.SaveChangesAsync();
        return RedirectToAction("Index");
    }

    public async Task<IActionResult> Details(int id)
    {
        var order = await _DbContext.LocalOrders
            .Include(o => o.Reservation)
            .Include(o => o.OrderItems)
                .ThenInclude(oi => oi.MenuItem)
            .FirstOrDefaultAsync(o => o.Id == id);

        if (order == null) return NotFound();

        var viewModel = new LocalOrderViewModel
        {
            Id = order.Id,
            TableNo = order.TableNo,
            OrderDate = order.OrderDate,
            OrderItems = order.OrderItems.Select(oi => new LocalOrderItemViewModel
            {
                Id = oi.Id,
                MenuItemId = oi.MenuItemId,
                MenuItemName = oi.MenuItem?.Name ?? "Unknown",
                Count = oi.Count
            }).ToList()
        };

        ViewBag.Reservation = order.Reservation != null
            ? $"{order.Reservation.GuestFirstName} {order.Reservation.GuestLastName}"
            : null;

        return View(viewModel);
    }


}
