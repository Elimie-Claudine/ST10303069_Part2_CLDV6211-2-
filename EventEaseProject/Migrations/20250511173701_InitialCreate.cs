using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EventEaseProject.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Venues",
                columns: table => new
                {
                    VenueId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    venueName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    venueLocation = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Capacity = table.Column<int>(type: "int", nullable: true),
                    ImageURL = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Venues__3C57E5F263EF9BB5", x => x.VenueId);
                });

            migrationBuilder.CreateTable(
                name: "Eventss",
                columns: table => new
                {
                    EventId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    eventName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    eventDate = table.Column<DateOnly>(type: "date", nullable: true),
                    eventTime = table.Column<TimeOnly>(type: "time", nullable: true),
                    eventDescription = table.Column<string>(type: "text", nullable: true),
                    ImageURL = table.Column<string>(type: "varchar(200)", unicode: false, maxLength: 200, nullable: true),
                    VenueId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Eventss__7944C810C118156E", x => x.EventId);
                    table.ForeignKey(
                        name: "FK__Eventss__VenueId__4BAC3F29",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "VenueId");
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    bookingId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    VenueId = table.Column<int>(type: "int", nullable: true),
                    EventId = table.Column<int>(type: "int", nullable: true),
                    CustomerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    CustomerEmail = table.Column<string>(type: "varchar(100)", unicode: false, maxLength: 100, nullable: false),
                    BookingDate = table.Column<DateTime>(type: "datetime", nullable: true, defaultValueSql: "(getdate())")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK__Bookings__C6D03BCD2EDD09F4", x => x.bookingId);
                    table.ForeignKey(
                        name: "FK__Bookings__EventI__4F7CD00D",
                        column: x => x.EventId,
                        principalTable: "Eventss",
                        principalColumn: "EventId");
                    table.ForeignKey(
                        name: "FK__Bookings__VenueI__4E88ABD4",
                        column: x => x.VenueId,
                        principalTable: "Venues",
                        principalColumn: "VenueId");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_EventId",
                table: "Bookings",
                column: "EventId");

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_VenueId",
                table: "Bookings",
                column: "VenueId");

            migrationBuilder.CreateIndex(
                name: "IX_Eventss_VenueId",
                table: "Eventss",
                column: "VenueId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Eventss");

            migrationBuilder.DropTable(
                name: "Venues");
        }
    }
}
