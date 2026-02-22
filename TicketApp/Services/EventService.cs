using Microsoft.EntityFrameworkCore;
using TicketApp.Areas.Identity.Data;
using TicketApp.Models;

namespace TicketApp.Services
{
    public class EventService
    {
        private readonly TicketAppContext _context;

        public EventService(TicketAppContext context)
        {
            _context = context;
        }

        public async Task<List<Event>> GetAllEvents()
        {
            return await _context.Events.ToListAsync();
        }

        public async Task<Event> GetEventById(int id)
        {
            return await _context.Events.FindAsync(id);
        }

        public async Task CreateEvent(Event model)
        {
            model.TicketsRemaining = model.TotalTickets;
            _context.Events.Add(model);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateEvent(Event model)
        {
            _context.Events.Update(model);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteEvent(int id)
        {
            var ev = await _context.Events.FindAsync(id);
            if (ev == null) return;

            _context.Events.Remove(ev);
            await _context.SaveChangesAsync();
        }
    }
}
