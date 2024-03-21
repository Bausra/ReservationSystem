using ReservationSystem.WebApi.Models;
using ReservationSystem.WebApi.Repositories.Interfaces;
using ReservationSystem.WebApi.Services.Interfaces;

namespace ReservationSystem.WebApi.Services.Implementations
{
    public class ReservationsService : IReservationsService
    {
        private readonly IReservationsRepository _reservationsRepository;

        public ReservationsService(IReservationsRepository reservationsRepository)
        {
            _reservationsRepository = reservationsRepository;
        }

        public bool LocationSpotIsOccupied(Reservation reservation)
        {
            List<Reservation> allReservations = GetAllReservations();

            return allReservations
                .Where(r =>
                    r.LocationSpotId == reservation.LocationSpotId &&
                    r.Status == Status.ACTIVE &&
                    (
                        r.ReservationStart >= reservation.ReservationStart && r.ReservationStart < reservation.ReservationEnd ||
                        r.ReservationEnd > reservation.ReservationStart && r.ReservationEnd <= reservation.ReservationEnd ||
                        r.ReservationStart <= reservation.ReservationStart && r.ReservationEnd >= reservation.ReservationEnd
                    )
                )
                .Any();
        }

        public List<Reservation> GetReservationsForLocation(int locationId)
        {
            return _reservationsRepository.GetReservationsForLocation(locationId);
        }

        public List<Reservation> GetAllReservations()
        {
            return _reservationsRepository.GetAllReservations();
        }

        public bool Exists(int reservationId)
        {
            return _reservationsRepository.Exists(reservationId);
        }

        public Reservation GetReservation(int reservationId)
        {
            return _reservationsRepository.GetReservation(reservationId);
        }

        public Reservation ExecuteAddReservation(Reservation newReservation)
        {
            bool locationSpotOccupied = LocationSpotIsOccupied(newReservation);
            if (locationSpotOccupied)
                throw new BadHttpRequestException($"Location spot is not available for provided date-time period!");

            newReservation.Id = 0;
            newReservation.Status = Status.ACTIVE;

            newReservation = AddReservation(newReservation);
            _reservationsRepository.SaveChangesToDatabase();
            return newReservation;
        }

        public bool IsCancelled(int reservationId)
        {
            return _reservationsRepository.IsCancelled(reservationId);
        }

        public bool IsCompleted(int reservationId)
        {
            return _reservationsRepository.IsCompleted(reservationId);
        }

        public void ExecuteUpdateReservationStatus(int reservationId, Status status)
        {
            UpdateReservationStatus(reservationId, Status.CANCELLED);
            _reservationsRepository.SaveChangesToDatabase();
        }

        public List<Reservation> GetActiveReservations()
        {
            return _reservationsRepository.GetActiveReservations();
        }

        public List<Reservation> GetUserReservations(int userId)
        {
            return _reservationsRepository.GetUserReservations(userId);
        }

        public List<Reservation> GetReservationsForLocationSpot(int locationSpotId)
        {
            return _reservationsRepository.GetReservationsForLocationSpot(locationSpotId);
        }

        public Reservation AddReservation(Reservation newReservation)
        {
            return _reservationsRepository.AddReservation(newReservation);
        }

        public Reservation UpdateReservationStatus(int id, Status status)
        {
            return _reservationsRepository.UpdateReservationStatus(id, status);
        }
    }
}
