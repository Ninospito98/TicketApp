using Microsoft.AspNetCore.Identity;
using TicketApp.Models;

namespace TicketApp.Areas.Identity.Data
{
    public class SeedData
    {
        public static async Task SeedRolesAndUserAsync(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = serviceProvider.GetRequiredService<UserManager<TicketAppUser>>();

            string[] roles = { "Admin", "Staff", "User" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole(role));
                }
            }

            var adminEmail = "admin@ticket.com";
            var adminUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                var newAdmin = new TicketAppUser
                {
                    UserName = adminEmail,
                    Email = adminEmail,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(newAdmin, "@Admin123");
                await userManager.AddToRoleAsync(newAdmin, "Admin");
            }

            var staffEmail = "staff@ticket.com";
            var staffUser = await userManager.FindByEmailAsync(adminEmail);

            if (adminUser == null)
            {
                var newAdmin = new TicketAppUser
                {
                    UserName = staffEmail,
                    Email = staffEmail,
                    EmailConfirmed = true
                };

                await userManager.CreateAsync(newAdmin, "@Staff123");
                await userManager.AddToRoleAsync(newAdmin, "Staff");
            }
        }

        public static async Task SeedEventsAsync(TicketAppContext context)
        {
            if (context.Events.Any()) return; // already seeded

            var events = new List<Event>
        {
        new Event
        {
            Title = "Stockholm Music Festival",
            Subtitle = "Drake, Travis Scott, 21 Savage",
            Category = "Concert",
            Date = new DateTime(2026, 6, 15, 20, 0, 0),
            Venue = "Tele2 Arena, Stockholm",
            Price = 899,
            Description = "Stockholm Music Festival is one of Scandinavia's most anticipated live music events, bringing together the biggest names in hip hop, R&B, and pop culture for an unforgettable night under the open sky. Held at the iconic Tele2 Arena, this year's lineup features Drake, Travis Scott, and 21 Savage — three of the most dominant artists of their generation sharing one stage for the very first time. Doors open at 18:00 with supporting acts kicking off the night, building up to the headline performances that will run well into the early hours. Whether you're a longtime fan or just looking for the event of the year, this is a night you won't forget.",
            ImageUrl = "https://images.unsplash.com/photo-1493225457124-a3eb161ffa5f?w=800",
            TotalTickets = 5000,
            TicketsRemaining = 5000
        },
        new Event
        {
            Title = "Champions League Final",
            Subtitle = "Real Madrid vs Barcelona",
            Category = "Sports",
            Date = new DateTime(2026, 5, 28, 20, 45, 0),
            Venue = "Friends Arena, Stockholm",
            Price = 1499,
            Description = "Stockholm Music Festival is one of Scandinavia's most anticipated live music events, bringing together the biggest names in hip hop, R&B, and pop culture for an unforgettable night under the open sky. Held at the iconic Tele2 Arena, this year's lineup features Drake, Travis Scott, and 21 Savage — three of the most dominant artists of their generation sharing one stage for the very first time. Doors open at 18:00 with supporting acts kicking off the night, building up to the headline performances that will run well into the early hours. Whether you're a longtime fan or just looking for the event of the year, this is a night you won't forget.",
            
            ImageUrl = "https://images.unsplash.com/photo-1431324155629-1a6deb1dec8d?w=800",
            TotalTickets = 10000,
            TicketsRemaining = 10000
        },
        new Event
        {
            Title = "Comedy Night Stockholm",
            Subtitle = "Dave Chappelle",
            Category = "Comedy",
            Date = new DateTime(2026, 4, 10, 19, 0, 0),
            Venue = "Annexet, Stockholm",
            Price = 599,
            Description = "An unforgettable night of stand up comedy.",
            ImageUrl = "https://images.unsplash.com/photo-1527224538127-2104bb71c51b?w=800",
            TotalTickets = 2000,
            TicketsRemaining = 2000
        },
        new Event
        {
            Title = "Välkommen till Norrköping",
            Subtitle = "Prova på Curling",
            Category = "Sport",
            Date = new DateTime(2026, 2, 10, 12, 0, 0),
            Venue = "Norrköpings Curlinghall",
            Price = 0,
            Description = "Testa curling gratis i curlinghallen under OS och sportlovet",
            ImageUrl = "https://ichef.bbci.co.uk/images/ic/480xn/p0n0ylfg.jpg.webp",
            TotalTickets = 1000,
            TicketsRemaining = 1000
        },
        new Event
        {
            Title = "Välkommen till Norrköping",
            Subtitle = "När gaming möter science",
            Category = "Gaming",
            Date = new DateTime(2026, 4, 10, 19, 0, 0),
            Venue = "Visualiseringscenter C, Norrköping",
            Price = 0,
            Description = "Spellov där spel möter vetenskap, teknik och kreativt skapande",
            ImageUrl = "https://www.theupcoming.co.uk/wp-content/uploads/2022/11/generic-pexels-gaming-event-alena-darmel-7862655-1024x620.jpg",
            TotalTickets = 2000,
            TicketsRemaining = 2000
        }
    };

            context.Events.AddRange(events);
            await context.SaveChangesAsync();
        }
    }
}
