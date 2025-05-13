namespace EventEaseDB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Booking
    {
        public int bookingId { get; set; }

        public int? VenueId { get; set; }

        public int? EventId { get; set; }

        [Required]
        [StringLength(100)]
        public string CustomerName { get; set; }

        [Required]
        [StringLength(100)]
        public string CustomerEmail { get; set; }

        public DateTime? BookingDate { get; set; }

        public virtual Eventss Eventss { get; set; }

        public virtual Venue Venue { get; set; }
    }
}
