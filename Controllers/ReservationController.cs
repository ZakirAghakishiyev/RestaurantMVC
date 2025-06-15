﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using RestaurantMVC.DataContext;
using RestaurantMVC.DataContext.Entities;

namespace RestaurantMVC.Controllers
{
    public class ReservationController : Controller
    {
        private readonly AppDbContext _context;

        public ReservationController(AppDbContext context)
        {
            _context = context;
        }

        [HttpPost]
        public async Task<IActionResult> Create(int people, DateTime day, string time, string firstName, string lastName, string phoneNumber)
        {
            Console.WriteLine("Reservation Create POST method called");
            if (people <= 0 || string.IsNullOrEmpty(time) || string.IsNullOrEmpty(firstName) || string.IsNullOrEmpty(lastName) || string.IsNullOrEmpty(phoneNumber))
            {
                ModelState.AddModelError("", "Please fill in all required fields.");
                return View(); 
            }

            DateTime reservationStartTime;
            try
            {
                reservationStartTime = DateTime.Parse($"{day.ToShortDateString()} {time}");
            }
            catch
            {
                ModelState.AddModelError("", "Invalid date or time.");
                return View();
            }

            var availableTable = await _context.Tables
                .Where(t => t.Capacity >= people)
                .OrderBy(t => t.Capacity)
                .FirstOrDefaultAsync(t =>
                    !_context.Reservations.Any(r =>
                        r.TableId == t.Id &&
                        r.ReservationStartTime == reservationStartTime));

            if (availableTable == null)
            {
                ModelState.AddModelError("", "No available table for the selected number of people.");
                return View();
            }

            var reservation = new Reservation
            {
                GuestFirstName = firstName,
                GuestLastName = lastName,
                GuestPhoneNumber = phoneNumber,
                TableId = availableTable.Id,
                ReservationStartTime = reservationStartTime,
                ReservationEndTime = reservationStartTime.AddHours(2)
            };

            _context.Reservations.Add(reservation);
            Console.WriteLine($"Reservation created: {reservation.GuestFirstName} {reservation.ReservationStartTime}");

            await _context.SaveChangesAsync();

            TempData["SuccessMessage"] = "Reservation successfully completed!";
            return Json(new { success = true, message = "Reservation successfully completed!" });
        }

    }
}
