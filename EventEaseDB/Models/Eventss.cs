namespace EventEaseDB.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using System.Data.Entity.Spatial;

    [Table("Eventss")]
    public partial class Eventss
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Eventss()
        {
            Bookings = new HashSet<Booking>();
        }

        [Key]
        public int EventId { get; set; }

        [Required]
        [StringLength(100)]
        public string eventName { get; set; }

        [Column(TypeName = "date")]
        public DateTime? eventDate { get; set; }

        public TimeSpan? eventTime { get; set; }

        [Column(TypeName = "text")]
        public string eventDescription { get; set; }

        [StringLength(200)]
        public string ImageURL { get; set; }

        public int? VenueId { get; set; }

        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Booking> Bookings { get; set; }

        public virtual Venue Venue { get; set; }
    }
}
