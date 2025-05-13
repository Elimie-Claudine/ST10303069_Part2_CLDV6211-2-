namespace EventEaseDB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    public partial class Venue
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Venue()
        {
            Bookings = new HashSet<Booking>();
            Eventsses = new HashSet<Eventss>();
        }

        public int VenueId { get; set; }

        [Required]
        [StringLength(100)]
        public string venueName { get; set; }

        [StringLength(200)]
        public string venueLocation { get; set; }

        public int? Capacity { get; set; }

        [StringLength(200)]
        public string ImageURL { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Booking> Bookings { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Eventss> Eventsses { get; set; }
    }
}
