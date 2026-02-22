# TicketApp
🎫 TicketApp
A full-stack ticket booking web application built with ASP.NET Core MVC. Users can browse events, book tickets, and receive a scannable QR code. Staff can scan QR codes to check in attendees, and admins can manage events, users, and bookings through a dedicated dashboard.

🚀 Features
Public

Browse upcoming events (concerts, sports, comedy, gaming, and more)
View event details including date, venue, price, and tickets remaining
Register and log in securely

Users

Book tickets with quantity selection
Dynamic checkout with live price calculation
Receive a unique QR code upon booking confirmation
View all bookings in My Tickets page

Staff

Dedicated check-in page with QR code scanner
Manual ticket code entry as fallback
View ticket details before confirming check-in
Tickets marked as used after check-in — prevents duplicate entry

Admin

Full event management — create, edit, delete
User management — view, edit roles, delete users
Bookings overview with status (Valid / Used)


🛠️ Built With

ASP.NET Core MVC — web framework
Entity Framework Core — ORM and database management
ASP.NET Core Identity — authentication and role-based authorization
SQL Server (MSSQL) — database
QRCoder — QR code generation
html5-qrcode — browser-based QR code scanning
Bootstrap — base styling
Custom CSS — dark themed UI


📁 Project Structure
TicketApp/
├── Controllers/
│   ├── HomeController.cs
│   ├── EventController.cs
│   ├── BookingController.cs
│   ├── CheckInController.cs
│   └── AdminController.cs
├── Models/
│   ├── Event.cs
│   ├── Booking.cs
│   └── UserViewModel.cs
├── Services/
│   ├── EventService.cs
│   └── BookingService.cs
├── Data/
│   ├── TicketAppContext.cs
│   └── SeedData.cs
├── Areas/Identity/
│   └── Data/TicketAppUser.cs
└── Views/
    ├── Home/
    ├── Event/
    ├── Booking/
    ├── CheckIn/
    └── Admin/

👤 Roles
Admin: Full access — events, users, bookings
Staff: Check-in page only
User: Browse events, book tickets, view own tickets
