using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace ReservationSystem.WebApi.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Locations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locations", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Surname = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    PersonalCode = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Address = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Phone = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CarRegistrationPlate = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    CountryAbbreviation = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "LocationSpots",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    LocationId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LocationSpots", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LocationSpots_Locations_LocationId",
                        column: x => x.LocationId,
                        principalTable: "Locations",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Reservations",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ReservationStart = table.Column<DateTime>(type: "datetime2", nullable: false),
                    ReservationEnd = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<int>(type: "int", nullable: false),
                    LocationSpotId = table.Column<int>(type: "int", nullable: false),
                    UserId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Reservations", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Reservations_LocationSpots_LocationSpotId",
                        column: x => x.LocationSpotId,
                        principalTable: "LocationSpots",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Reservations_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Locations",
                columns: new[] { "Id", "Name", "Status" },
                values: new object[,]
                {
                    { 1, "Parking", 0 },
                    { 2, "Desk", 0 }
                });

            migrationBuilder.InsertData(
                table: "Users",
                columns: new[] { "Id", "Address", "CarRegistrationPlate", "CountryAbbreviation", "Email", "Name", "PersonalCode", "Phone", "Status", "Surname" },
                values: new object[,]
                {
                    { 1, "Rukolos g. 12-2, Kaunas", "AMU111", "LT", "jonas.jonaitis@gmail.com", "Jonas", "43614578542", "86-878-4596", 0, "Jonaitis" },
                    { 2, "Kalpoko g. 3, Kaunas", null, "LT", null, "Rima", "49547578542", "37068784596", 0, "Ramune" },
                    { 3, "Suur-Ameerika 1, Tallinn", "441AUI", "EE", "bob.marley@gmail.com", "Bob", "38254789564", "+372 799 2222", 0, "Marley" }
                });

            migrationBuilder.InsertData(
                table: "LocationSpots",
                columns: new[] { "Id", "LocationId", "Name", "Status" },
                values: new object[,]
                {
                    { 1, 1, "A1", 0 },
                    { 2, 1, "A2", 0 },
                    { 3, 1, "A3", 3 },
                    { 4, 1, "B1", 0 },
                    { 5, 1, "B2", 0 },
                    { 6, 2, "Python", 0 },
                    { 7, 2, "PHP", 0 },
                    { 8, 2, "C#", 0 },
                    { 9, 2, "Java", 3 }
                });

            migrationBuilder.InsertData(
                table: "Reservations",
                columns: new[] { "Id", "LocationSpotId", "ReservationEnd", "ReservationStart", "Status", "UserId" },
                values: new object[,]
                {
                    { 1, 1, new DateTime(2023, 1, 1, 17, 30, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 1, 1, 12, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 2, 2, new DateTime(2023, 5, 24, 18, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 2, 8, 0, 0, 0, DateTimeKind.Unspecified), 2, 1 },
                    { 3, 2, new DateTime(2023, 5, 30, 18, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 5, 10, 8, 0, 0, 0, DateTimeKind.Unspecified), 1, 1 },
                    { 4, 4, new DateTime(2023, 12, 22, 18, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 18, 13, 0, 0, 0, DateTimeKind.Unspecified), 0, 3 },
                    { 5, 1, new DateTime(2023, 12, 29, 18, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 29, 8, 0, 0, 0, DateTimeKind.Unspecified), 0, 2 },
                    { 6, 1, new DateTime(2023, 12, 28, 18, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 12, 26, 8, 0, 0, 0, DateTimeKind.Unspecified), 0, 2 },
                    { 7, 8, new DateTime(2023, 12, 29, 18, 0, 0, 0, DateTimeKind.Unspecified), new DateTime(2023, 11, 1, 8, 0, 0, 0, DateTimeKind.Unspecified), 2, 3 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_LocationSpots_LocationId",
                table: "LocationSpots",
                column: "LocationId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_LocationSpotId",
                table: "Reservations",
                column: "LocationSpotId");

            migrationBuilder.CreateIndex(
                name: "IX_Reservations_UserId",
                table: "Reservations",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Reservations");

            migrationBuilder.DropTable(
                name: "LocationSpots");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "Locations");
        }
    }
}
