using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TicketApp.Areas.Identity.Data;
using TicketApp.Models;
using TicketApp.Services;

namespace TicketApp.Controllers
{
    public class EventController : Controller
    {
        private readonly EventService _eventService;

        public EventController(EventService eventService)
        {
            _eventService = eventService;
        }
        public async Task<IActionResult> Index()
        {
            var events = await _eventService.GetAllEvents();
            return View(events);
        }

        public async Task<IActionResult> Details(int id)
        {
            var ev = await _eventService.GetEventById(id);

            if (ev == null)
            {
                return NotFound();
            }
            return View(ev);
        }

        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Create(Event model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }

            await _eventService.CreateEvent(model);
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id)
        {
            var ev = await _eventService.GetEventById(id);
            if (ev == null) return NotFound();
            return View(ev);
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int id, Event model)
        {
            if (!ModelState.IsValid) return View(model);
            await _eventService.UpdateEvent(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            await _eventService.DeleteEvent(id);
            return RedirectToAction("Index");
        }
    }
}
