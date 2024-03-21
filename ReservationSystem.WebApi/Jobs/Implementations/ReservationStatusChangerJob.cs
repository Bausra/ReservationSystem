using Quartz;
using ReservationSystem.WebApi.Jobs.Interfaces;
using ReservationSystem.WebApi.Models;
using ReservationSystem.WebApi.Repositories.Interfaces;

namespace ReservationSystem.WebApi.Jobs.Implementations
{
    [DisallowConcurrentExecution]
    public class ReservationStatusChangerJob : IJob, IReservationStatusChangerJob
    {
        private readonly IReservationsRepository _reservationsRepository;

        public ReservationStatusChangerJob(IReservationsRepository reservationsRepository)
        {
            _reservationsRepository = reservationsRepository;
        }

        public Task Execute(IJobExecutionContext context)
        {
            List<Reservation> reservations = _reservationsRepository.GetActiveReservations();

            foreach (Reservation reservation in reservations)
            {
                if (reservation.ReservationEnd < DateTime.Now)
                    _reservationsRepository.UpdateReservationStatus(reservation.Id, Status.COMPLETED);
                _reservationsRepository.SaveChangesToDatabase();
            }

            return Task.CompletedTask;
        }
    }
}
