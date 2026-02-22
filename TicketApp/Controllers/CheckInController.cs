using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TicketApp.Services;

namespace TicketApp.Controllers
{
    [Authorize(Roles = "Staff,Admin")]
    public class CheckInController : Controller
    {
        private readonly BookingService _bookingService;

        public CheckInController(BookingService bookingService)
        {
            _bookingService = bookingService;
        }

        // GET - scanner page
        public IActionResult Index()
        {
            return View();
        }

        // GET - verify ticket
        public async Task<IActionResult> Verify(string code)
        {
            if (string.IsNullOrEmpty(code))
                return RedirectToAction("Index");

            var booking = await _bookingService.GetBookingByCode(code);

            if (booking == null)
                return View("Invalid");

            return View(booking);
        }

        // POST - mark as used
        [HttpPost]
        public async Task<IActionResult> MarkUsed(string ticketCode)
        {
            var success = await _bookingService.MarkAsUsed(ticketCode);

            if (!success)
                return View("Invalid");

            return RedirectToAction("Success");
        }

        public IActionResult Success()
        {
            return View();
        }
    }
}