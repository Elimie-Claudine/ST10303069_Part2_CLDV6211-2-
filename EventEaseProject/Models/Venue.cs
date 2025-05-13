using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace EventEaseProject.Models;

public partial class Venue
{
    public int VenueId { get; set; }



    public string VenueName { get; set; } = null!;

    public string? VenueLocation { get; set; }

    public int? Capacity { get; set; }

    public string? ImageUrl { get; set; }

    [NotMapped]
    public IFormFile? ImageFile { get; set; }

    public virtual ICollection<Booking> Bookings { get; set; } = new List<Booking>();

    public virtual ICollection<Eventss> Events { get; set; } = new List<Eventss>();
}
