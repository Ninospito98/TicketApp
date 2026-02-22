using System.ComponentModel.DataAnnotations;

namespace TicketApp.Models
{
    public class Event
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        [StringLength(100)]
        public string Subtitle { get; set; }

        [Required]
        [StringLength(100)]
        public string Category { get; set; }

        [Required]
        public DateTime Date { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        [Required]
        [StringLength(200)]
        public string Venue { get; set; }

        [StringLength(500)]
        public string ImageUrl { get; set; }

        [Required]
        [Range(0, int.MaxValue)]
        public decimal Price { get; set; }

        [StringLength(2000)]
        public string Description { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int TotalTickets { get; set; }

        public int TicketsRemaining { get; set; }

        public ICollection<Booking> Bookings { get; set; }
    }
}
