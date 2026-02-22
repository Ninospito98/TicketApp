using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TicketApp.Areas.Identity.Data;
using TicketApp.Models;
using TicketApp.Services;

namespace TicketApp.Controllers
{
    [Authorize]
    public class BookingController : Controller
    {
        private readonly BookingService _bookingService;
        private readonly EventService _eventService;

        public BookingController(BookingService bookingService, EventService eventService)
        {
            _bookingService = bookingService;
            _eventService = eventService;
        }

        // GET - show checkout page
        public async Task<IActionResult> Checkout(int eventId)
        {
            var ev = await _eventService.GetEventById(eventId);
            if (ev == null) return NotFound();
            return View(ev);
        }

        // POST - confirm booking
        [HttpPost]
        public async Task<IActionResult> Checkout(int eventId, int quantity)
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            try
            {
                var booking = await _bookingService.CreateBooking(userId, eventId, quantity);
                return RedirectToAction("Confirmation", new { id = booking.Id });
            }
            catch (Exception ex)
            {
                TempData["Error"] = ex.Message;
                return RedirectToAction("Checkout", new { eventId });
            }
        }

        // GET - booking confirmation with QR
        public async Task<IActionResult> Confirmation(int id)
        {
            var booking = await _bookingService.GetBookingById(id);
            if (booking == null) return NotFound();

            ViewBag.QrCode = _bookingService.GenerateQrCode(booking.TicketCode);

            return View(booking);
        }

        // GET - my tickets
        public async Task<IActionResult> MyTickets()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            var bookings = await _bookingService.GetUserBookings(userId);
            return View(bookings);
        }


    }
}