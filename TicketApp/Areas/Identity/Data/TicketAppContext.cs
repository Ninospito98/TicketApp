using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using TicketApp.Areas.Identity.Data;
using TicketApp.Models;

namespace TicketApp.Areas.Identity.Data;

public class TicketAppContext : IdentityDbContext<TicketAppUser>
{
    public TicketAppContext(DbContextOptions<TicketAppContext> options)
        : base(options)
    {
    }
    public DbSet<Event> Events { get; set; }

    public DbSet<Booking> Bookings { get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);
    }
}
