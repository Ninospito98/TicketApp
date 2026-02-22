using Microsoft.EntityFrameworkCore;
using QRCoder;
using TicketApp.Areas.Identity.Data;
using TicketApp.Models;

namespace TicketApp.Services
{
    public class BookingService
    {
        private readonly TicketAppContext _context;

        public BookingService(TicketAppContext context)
        {
            _context = context;
        }

        public async Task<Booking> CreateBooking(string userId, int eventId, int quantity)
        {
            var ev = await _context.Events.FindAsync(eventId);

            if (ev == null)
                throw new Exception("Event not found");

            if (ev.TicketsRemaining < quantity)
                throw new Exception("Not enough tickets remaining");

            var booking = new Booking
            {
                UserId = userId,
                EventId = eventId,
                Quantity = quantity,
                TotalPrice = ev.Price * quantity,
                TicketCode = Guid.NewGuid().ToString(),
                CreatedAt = DateTime.UtcNow
            };

            ev.TicketsRemaining -= quantity;

            _context.Bookings.Add(booking);
            await _context.SaveChangesAsync();

            return booking;
        }

        public async Task<List<Booking>> GetUserBookings(string userId)
        {
            return await _context.Bookings
                .Include(b => b.Event)
                .Where(b => b.UserId == userId)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }

        public async Task<Booking> GetBookingByCode(string ticketCode)
        {
            return await _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.TicketCode == ticketCode);
        }

        public async Task<Booking> GetBookingById(int id)
        {
            return await _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.User)
                .FirstOrDefaultAsync(b => b.Id == id);
        }

        public async Task<List<Booking>> GetAllBookings()
        {
            return await _context.Bookings
                .Include(b => b.Event)
                .Include(b => b.User)
                .OrderByDescending(b => b.CreatedAt)
                .ToListAsync();
        }
        public string GenerateQrCode(string ticketCode)
        {
            var qrGenerator = new QRCodeGenerator();
            var qrData = qrGenerator.CreateQrCode(ticketCode, QRCodeGenerator.ECCLevel.Q);
            var qrCode = new PngByteQRCode(qrData);
            byte[] qrBytes = qrCode.GetGraphic(20);
            return Convert.ToBase64String(qrBytes);
        }

        public async Task<bool> MarkAsUsed(string ticketCode)
        {
            var booking = await _context.Bookings
                .FirstOrDefaultAsync(b => b.TicketCode == ticketCode);

            if (booking == null) return false;
            if (booking.IsUsed) return false;

            booking.IsUsed = true;
            booking.UsedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return true;
        }


    }
}