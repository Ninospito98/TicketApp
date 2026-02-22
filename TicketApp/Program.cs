using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using TicketApp.Areas.Identity.Data;
using TicketApp.Services;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("TicketAppContextConnection") ?? throw new InvalidOperationException("Connection string 'TicketAppContextConnection' not found.");;

builder.Services.AddDbContext<TicketAppContext>(options => options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<TicketAppUser>(options => options.SignIn.RequireConfirmedAccount = false)
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<TicketAppContext>()
        .AddDefaultTokenProviders()
        .AddDefaultUI();

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages(); //This comes with app.MapRazorPages(); and they help with the login register UI

builder.Services.AddScoped<EventService>();
builder.Services.AddScoped<BookingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

using (var scope = app.Services.CreateScope())
{
    await SeedData.SeedRolesAndUserAsync(scope.ServiceProvider);
    await SeedData.SeedEventsAsync(scope.ServiceProvider.GetRequiredService<TicketAppContext>());
}


app.MapRazorPages();

app.Run();
