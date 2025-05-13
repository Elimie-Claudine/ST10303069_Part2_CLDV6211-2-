using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace EventEaseProject.Models;

public partial class EventEaseDbContext : DbContext
{
    public EventEaseDbContext()
    {
    }

    public EventEaseDbContext(DbContextOptions<EventEaseDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Booking> Bookings { get; set; }

    public virtual DbSet<Eventss> Eventsses { get; set; }

    public virtual DbSet<Venue> Venues { get; set; }


    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Booking>(entity =>
        {
            entity.HasKey(e => e.BookingId).HasName("PK__Bookings__C6D03BCD2EDD09F4");

            entity.Property(e => e.BookingId).HasColumnName("bookingId");
            entity.Property(e => e.BookingDate)
                .HasDefaultValueSql("(getdate())")
                .HasColumnType("datetime");
            entity.Property(e => e.CustomerEmail)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.VenueId).HasMaxLength(100);

            entity.HasOne(d => d.Event).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.EventId)
                .HasConstraintName("FK__Bookings__EventI__4F7CD00D");

            entity.HasOne(d => d.Venue).WithMany(p => p.Bookings)
                .HasForeignKey(d => d.VenueId)
                .HasConstraintName("FK__Bookings__VenueI__4E88ABD4");
        });

        modelBuilder.Entity<Eventss>(entity =>
        {
            entity.HasKey(e => e.EventId).HasName("PK__Eventss__7944C810C118156E");

            entity.ToTable("Eventss");

            entity.Property(e => e.EventDateTime).HasColumnName("eventDate");
            entity.Property(e => e.EventDescription)
                .HasColumnType("text")
                .HasColumnName("eventDescription");
            entity.Property(e => e.EventName)
                .HasMaxLength(100)
                .HasColumnName("eventName");
            entity.Property(e => e.ImageUrl)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("ImageURL");

            entity.HasOne(d => d.Venue).WithMany(p => p.Events)
                .HasForeignKey(d => d.VenueId)
                .HasConstraintName("FK__Eventss__VenueId__4BAC3F29");
        });

        modelBuilder.Entity<Venue>(entity =>
        {
            entity.ToTable("Venues");

            entity.HasKey(e => e.VenueId).HasName("PK__Venues__3C57E5F263EF9BB5");

            entity.Property(e => e.ImageUrl)
                .HasMaxLength(200)
                .IsUnicode(false)
                .HasColumnName("ImageURL");
            entity.Property(e => e.VenueLocation)
                .HasMaxLength(200)
                .HasColumnName("venueLocation");
            entity.Property(e => e.VenueName)
                .HasMaxLength(100)
                .HasColumnName("venueName");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
