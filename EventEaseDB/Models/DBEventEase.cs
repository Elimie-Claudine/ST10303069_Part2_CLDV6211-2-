using System;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Linq;

namespace EventEaseDB.Models
{
    public partial class DBEventEase : DbContext
    {
        public DBEventEase()
            : base("name=DBEventEase")
        {
        }

        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Eventss> Eventsses { get; set; }
        public virtual DbSet<Venue> Venues { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Booking>()
                .Property(e => e.CustomerEmail)
                .IsUnicode(false);

            modelBuilder.Entity<Eventss>()
                .Property(e => e.eventDescription)
                .IsUnicode(false);

            modelBuilder.Entity<Eventss>()
                .Property(e => e.ImageURL)
                .IsUnicode(false);

            modelBuilder.Entity<Venue>()
                .Property(e => e.ImageURL)
                .IsUnicode(false);
        }
    }
}
