using Microsoft.EntityFrameworkCore;
using ReservationSystem.WebApi.Data;
using ReservationSystem.WebApi.Models;
using ReservationSystem.WebApi.Repositories.Interfaces;

namespace ReservationSystem.WebApi.Repositories.Implementations
{
    public class ReservationsRepository : IReservationsRepository
    {
        private readonly ReservationSystemDbContext _context;
        public ReservationsRepository(ReservationSystemDbContext context)
        {
            _context = context;
        }

        public Reservation AddReservation(Reservation newReservation)
        {
            _context.Reservations.Add(newReservation);
            return newReservation;
        }

        public Reservation UpdateReservationStatus(int id, Status status)
        {
            Reservation reservation = _context.Reservations.Where(x => x.Id == id).First();
            reservation.Status = status;
            return reservation;
        }

        public bool Exists(int id)
        {
            return _context.Reservations
                .Any(u => u.Id == id && u.Status != Status.DELETED);
        }

        public bool IsCancelled(int id)
        {
            return _context.Reservations
                .Any(u => u.Id == id && u.Status == Status.CANCELLED);
        }

        public bool IsCompleted(int id)
        {
            return _context.Reservations
                .Any(u => u.Id == id && u.Status == Status.COMPLETED);
        }

        public List<Reservation> GetAllReservations()
        {
            return _context.Reservations
                 .Where(r => r.Status != Status.DELETED)
                 .ToList();
        }

        public List<Reservation> GetActiveReservations()
        {
            return _context.Reservations
                 .Where(r => r.Status == Status.ACTIVE)
                 .ToList();
        }

        public Reservation GetReservation(int id)
        {
            return _context.Reservations
                .Where(r => r.Id == id && r.Status != Status.DELETED)
                .First();
        }

        public int SaveChangesToDatabase()
        {
            return _context.SaveChanges();
        }

        public List<Reservation> GetUserReservations(int userId)
        {
            return _context.Reservations.Where(x => x.UserId == userId && x.Status != Status.DELETED).ToList();
        }

        public List<Reservation> GetReservationsForLocation(int locationId)
        {
            return _context.Reservations
                     .Include(r => r.LocationSpot)
                     .Where(r => r.LocationSpot.LocationId == locationId && r.Status != Status.DELETED)
                     .ToList();
        }

        public List<Reservation> GetReservationsForLocationSpot(int locationSpotId)
        {
            return _context.Reservations
                     .Where(r => r.LocationSpotId == locationSpotId && r.Status != Status.DELETED)
                     .ToList();
        }
    }
}
