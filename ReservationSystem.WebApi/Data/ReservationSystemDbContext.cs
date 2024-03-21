using Microsoft.EntityFrameworkCore;
using ReservationSystem.WebApi.Models;

namespace ReservationSystem.WebApi.Data
{
    public class ReservationSystemDbContext : DbContext
    {
        public ReservationSystemDbContext(DbContextOptions options) : base(options) { }

        public DbSet<Location> Locations { get; set; }
        public DbSet<LocationSpot> LocationSpots { get; set; }
        public DbSet<Reservation> Reservations { get; set; }
        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<Location>().HasData(
                new Location { Id = 1, Name = "Parking", Status = Status.ACTIVE },
                new Location { Id = 2, Name = "Desk", Status = Status.ACTIVE }
                );

            modelBuilder.Entity<LocationSpot>().HasData(
                new LocationSpot { Id = 1, Name = "A1", LocationId = 1, Status = Status.ACTIVE },
                new LocationSpot { Id = 2, Name = "A2", LocationId = 1, Status = Status.ACTIVE },
                new LocationSpot { Id = 3, Name = "A3", LocationId = 1, Status = Status.DELETED },
                new LocationSpot { Id = 4, Name = "B1", LocationId = 1, Status = Status.ACTIVE },
                new LocationSpot { Id = 5, Name = "B2", LocationId = 1, Status = Status.ACTIVE },
                new LocationSpot { Id = 6, Name = "Python", LocationId = 2, Status = Status.ACTIVE },
                new LocationSpot { Id = 7, Name = "PHP", LocationId = 2, Status = Status.ACTIVE },
                new LocationSpot { Id = 8, Name = "C#", LocationId = 2, Status = Status.ACTIVE },
                new LocationSpot { Id = 9, Name = "Java", LocationId = 2, Status = Status.DELETED }
                );

            modelBuilder.Entity<User>().HasData(
                new User
                {
                    Id = 1,
                    Name = "Jonas",
                    Surname = "Jonaitis",
                    PersonalCode = "43614578542",
                    Address = "Rukolos g. 12-2, Kaunas",
                    Phone = "86-878-4596",
                    CarRegistrationPlate = "AMU111",
                    Email = "jonas.jonaitis@gmail.com",
                    CountryAbbreviation = "LT",
                    Status = Status.ACTIVE
                },
                new User
                {
                    Id = 2,
                    Name = "Rima",
                    Surname = "Ramune",
                    PersonalCode = "49547578542",
                    Address = "Kalpoko g. 3, Kaunas",
                    Phone = "37068784596",
                    CarRegistrationPlate = null,
                    Email = null,
                    CountryAbbreviation = "LT",
                    Status = Status.ACTIVE
                },
                new User
                {
                    Id = 3,
                    Name = "Bob",
                    Surname = "Marley",
                    PersonalCode = "38254789564",
                    Address = "Suur-Ameerika 1, Tallinn",
                    Phone = "+372 799 2222",
                    CarRegistrationPlate = "441AUI",
                    Email = "bob.marley@gmail.com",
                    CountryAbbreviation = "EE",
                    Status = Status.ACTIVE
                }
                );

            modelBuilder.Entity<Reservation>().HasData(
                new Reservation
                {
                    Id = 1,
                    ReservationStart = DateTime.Parse("2023-01-01 12:00:00"),
                    ReservationEnd = DateTime.Parse("2023-01-01 17:30:00"),
                    LocationSpotId = 1,
                    UserId = 1,
                    Status = Status.COMPLETED
                },
                new Reservation
                {
                    Id = 2,
                    ReservationStart = DateTime.Parse("2023-05-02 08:00:00"),
                    ReservationEnd = DateTime.Parse("2023-05-24 18:00:00"),
                    LocationSpotId = 2,
                    UserId = 1,
                    Status = Status.CANCELLED
                },
                new Reservation
                {
                    Id = 3,
                    ReservationStart = DateTime.Parse("2023-05-10 08:00:00"),
                    ReservationEnd = DateTime.Parse("2023-05-30 18:00:00"),
                    LocationSpotId = 2,
                    UserId = 1,
                    Status = Status.COMPLETED
                },
                new Reservation
                {
                    Id = 4,
                    ReservationStart = DateTime.Parse("2023-12-18 13:00:00"),
                    ReservationEnd = DateTime.Parse("2023-12-22 18:00:00"),
                    LocationSpotId = 4,
                    UserId = 3,
                    Status = Status.ACTIVE
                },
                new Reservation
                {
                    Id = 5,
                    ReservationStart = DateTime.Parse("2023-12-29 08:00:00"),
                    ReservationEnd = DateTime.Parse("2023-12-29 18:00:00"),
                    LocationSpotId = 1,
                    UserId = 2,
                    Status = Status.ACTIVE
                },
                new Reservation
                {
                    Id = 6,
                    ReservationStart = DateTime.Parse("2023-12-26 08:00:00"),
                    ReservationEnd = DateTime.Parse("2023-12-28 18:00:00"),
                    LocationSpotId = 1,
                    UserId = 2,
                    Status = Status.ACTIVE
                },
                new Reservation
                {
                    Id = 7,
                    ReservationStart = DateTime.Parse("2023-11-01 08:00:00"),
                    ReservationEnd = DateTime.Parse("2023-12-29 18:00:00"),
                    LocationSpotId = 8,
                    UserId = 3,
                    Status = Status.CANCELLED
                }
                );
        }
    }
}
   