using System.ComponentModel.DataAnnotations;
using TicketApp.Areas.Identity.Data;

namespace TicketApp.Models
{
    public class Booking
    {
        public int Id { get; set; }

        public string UserId { get; set; }
        public TicketAppUser User { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }

        [Required]
        public string TicketCode { get; set; }

        public int Quantity { get; set; }

        public decimal TotalPrice { get; set; }

        public bool IsUsed { get; set; }

        public DateTime? UsedAt { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}