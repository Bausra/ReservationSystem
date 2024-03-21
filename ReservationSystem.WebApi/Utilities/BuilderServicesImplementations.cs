using Microsoft.AspNetCore.Mvc;
using Quartz;
using ReservationSystem.WebApi.Jobs.Interfaces;
using ReservationSystem.WebApi.Models;

namespace ReservationSystem.WebApi.Helpers
{
    public static class BuilderServicesImplementations
    {
        public static void QuartzServicesInfrastructure(this IServiceCollection services)
        {
            services.AddQuartz(options =>
            {
                options.UseMicrosoftDependencyInjectionJobFactory();

                var jobKey = JobKey.Create(nameof(IReservationStatusChangerJob));
                options
                    .AddJob<IReservationStatusChangerJob>(jobKey)
                    .AddTrigger(trigger => 
                        trigger
                            .ForJob(jobKey)
                            .WithCronSchedule("5 0/30 * * * ?"));             
            });

            services.AddQuartzHostedService(options =>
            {
                options.WaitForJobsToComplete = true;
            });
        }

        public static void InvalidModelStateResponseInfrastructure(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options => 
            {
                options.InvalidModelStateResponseFactory = actionContext =>
                {
                    var errors = actionContext.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .Select(e => e.Value.Errors.First().ErrorMessage).ToList();

                    var combinedErrors = string.Join(" ", errors);

                    return new BadRequestObjectResult(
                        new ErrorInformation
                        {
                            StatusCode = 400,
                            ErrorMessage = combinedErrors
                        }
                    );
                };
            });
        }
    }
}
