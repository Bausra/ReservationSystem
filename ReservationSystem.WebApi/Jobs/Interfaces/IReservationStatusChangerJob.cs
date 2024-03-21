using Quartz;

namespace ReservationSystem.WebApi.Jobs.Interfaces
{
    public interface IReservationStatusChangerJob : IJob
    {
        Task Execute(IJobExecutionContext context);
    }
}
