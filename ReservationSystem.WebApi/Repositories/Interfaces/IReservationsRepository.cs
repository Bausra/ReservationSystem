using ReservationSystem.WebApi.Models;

namespace ReservationSystem.WebApi.Repositories.Interfaces
{
    public interface IReservationsRepository
    {
        List<Reservation> GetAllReservations();
        List<Reservation> GetActiveReservations();
        Reservation GetReservation(int id);
        Reservation AddReservation(Reservation newReservation);
        Reservation UpdateReservationStatus(int id, Status status);
        bool Exists(int id);
        bool IsCancelled(int id);
        bool IsCompleted(int id);
        int SaveChangesToDatabase();
        List<Reservation> GetUserReservations(int userId);
        List<Reservation> GetReservationsForLocation(int locationId);
        List<Reservation> GetReservationsForLocationSpot(int locationSpotId);
    }
}
