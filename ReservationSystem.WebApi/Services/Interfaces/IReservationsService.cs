using ReservationSystem.WebApi.Models;

namespace ReservationSystem.WebApi.Services.Interfaces
{
    public interface IReservationsService
    {
        Reservation AddReservation(Reservation newReservation);
        Reservation UpdateReservationStatus(int id, Status status);
        bool Exists(int reservationId);
        bool IsCancelled(int reservationId);
        bool IsCompleted(int reservationId);
        List<Reservation> GetAllReservations();
        List<Reservation> GetActiveReservations();
        Reservation GetReservation(int reservationId);
        List<Reservation> GetUserReservations(int userId);
        List<Reservation> GetReservationsForLocation(int locationId);
        List<Reservation> GetReservationsForLocationSpot(int locationSpotId);


        bool LocationSpotIsOccupied(Reservation reservation);
        Reservation ExecuteAddReservation(Reservation newReservation);
        void ExecuteUpdateReservationStatus(int reservationId, Status status);
    }
}
