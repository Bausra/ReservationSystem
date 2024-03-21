using ReservationSystem.WebApi.Data;
using ReservationSystem.WebApi.DTOs;
using ReservationSystem.WebApi.Models;
using System.ComponentModel.DataAnnotations;

namespace ReservationSystem.WebApi.Helpers
{
    public class DataAnnotations
    {
        [AttributeUsage(AttributeTargets.Property, AllowMultiple = false)]
        public class StartDateBeforeEndDateAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                var reservationDto = (ReservationDto)validationContext.ObjectInstance;

                if (reservationDto.ReservationStart >= reservationDto.ReservationEnd)
                {
                    return new ValidationResult("Start date must be earlier than end date.");
                }

                return ValidationResult.Success;
            }
        }

        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
        public class DateTimeNotInThePastAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value is DateTime dateTime)
                {
                    if (dateTime < DateTime.Now)
                    {
                        return new ValidationResult($"The {validationContext.DisplayName} cannot be in the past.");
                    }
                }

                return ValidationResult.Success;
            }
        }


        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
        public class ValidEntityIdAttribute : ValidationAttribute
        {
            private readonly Type _entityType;

            public ValidEntityIdAttribute(Type entityType)
            {
                _entityType = entityType;
            }

            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value != null)
                {
                    var serviceProvider = validationContext.GetService<IServiceProvider>();
                    using (var scope = serviceProvider.CreateScope())
                    {
                        var dbContext = scope.ServiceProvider.GetRequiredService<ReservationSystemDbContext>();

                        var id = (int)value;

                        var entityType = dbContext.Model.FindEntityType(_entityType);
                        var primaryKey = entityType.FindPrimaryKey().Properties.First();

                        var entity = dbContext.Find(_entityType, id);

                        if (entity == null || (int)entityType.FindProperty("Status").GetGetter().GetClrValue(entity) != (int)Status.ACTIVE)
                        {
                            return new ValidationResult($"The provided {validationContext.DisplayName} is not valid.");
                        }
                    }
                }

                return ValidationResult.Success;
            }
        }

        [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = false)]
        public class CustomMinutesAndSecondsAttribute : ValidationAttribute
        {
            protected override ValidationResult IsValid(object value, ValidationContext validationContext)
            {
                if (value != null)
                {
                    DateTime dateTime = (DateTime)value;

                    if (dateTime.Minute != 0 && dateTime.Minute != 30)
                    {
                        return new ValidationResult($"The provided {validationContext.DisplayName} must have minutes set to either 00 or 30.");
                    }

                    if (dateTime.Second != 0)
                    {
                        return new ValidationResult($"The provided {validationContext.DisplayName} must not have seconds set.");
                    }

                    return ValidationResult.Success;
                }

                return new ValidationResult($"The provided {validationContext.DisplayName} does not match the expected format ('2023-01-01T10:00:00') or (('2023-01-01T10:30:00')).");
            }
        }
    }
}
