using System;
using System.Collections.Generic;

namespace EventEaseProject.Models;

public partial class Booking
{
    public int BookingId { get; set; }

    public int? VenueId { get; set; }

    public int? EventId { get; set; }

    public string CustomerEmail { get; set; } = null!;

    public DateTime BookingDate { get; set; }

    public virtual Eventss? Event { get; set; }

    public virtual Venue? Venue { get; set; }

    public string VenueName { get; set; } = null!;
}
