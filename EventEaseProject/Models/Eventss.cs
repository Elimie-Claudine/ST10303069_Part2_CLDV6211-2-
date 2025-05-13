using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventEaseProject.Models;

public partial class Eventss
{
    public int EventId { get; set; }

    public string EventName { get; set; } = null!;

    public DateTime? EventDateTime { get; set; }

    public string? EventDescription { get; set; }

    public string? ImageUrl { get; set; }

    [NotMapped]
    public IFormFile? ImageFile { get; set; }

    public int? VenueId { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual Venue? Venue { get; set; }
}
