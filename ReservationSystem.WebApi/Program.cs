using Microsoft.AspNetCore.OData;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using NLog;
using NLog.Web;
using Quartz;
using ReservationSystem.WebApi.Data;
using ReservationSystem.WebApi.Helpers;
using ReservationSystem.WebApi.Jobs.Implementations;
using ReservationSystem.WebApi.Jobs.Interfaces;
using ReservationSystem.WebApi.Mappers;
using ReservationSystem.WebApi.Repositories.Implementations;
using ReservationSystem.WebApi.Repositories.Interfaces;
using ReservationSystem.WebApi.Services.Implementations;
using ReservationSystem.WebApi.Services.Interfaces;
using ReservationSystem.WebApi.Utilities;
using System.Reflection;
using System.Text.Json.Serialization;

var logger = LogManager.Setup().LoadConfigurationFromAppSettings().GetCurrentClassLogger();

try
{
    var builder = WebApplication.CreateBuilder(args);

    // Add services to the container.

    //Database
    var connectionString = builder.Configuration.GetConnectionString("ReservationSystemDbConnectionString");
    builder.Services.AddDbContext<ReservationSystemDbContext>(options =>
    {
        options.UseSqlServer(connectionString);
    });

    //Nlog
    builder.Logging.ClearProviders();
    builder.Host.UseNLog();

    //Controllers
    builder.Services.AddControllers()
        .AddOData(options => options.Select().Filter().OrderBy())
        .ConfigureApiBehaviorOptions(x => { x.SuppressMapClientErrors = true; })
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });

    //Endpoints
    builder.Services.AddEndpointsApiExplorer();

    //Swagger
    builder.Services.AddSwaggerGen(options =>
    {
        options
        .SwaggerDoc("v1", new OpenApiInfo
        {
            Title = "Reservation System API",
            Description = "An ASP.NET Core Web API for managing reservations",
        });

        options.SchemaFilter<EnumSchemaFilter>();

        var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
        options.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
    });

    //Mapper
    builder.Services.AddAutoMapper(typeof(ReservationSystemMapper));

    //ScopedServices
    builder.Services.AddScoped<IUsersRepository, UsersRepository>();
    builder.Services.AddScoped<ILocationsRepository, LocationsRepository>();
    builder.Services.AddScoped<ILocationSpotsRepository, LocationSpotsRepository>();
    builder.Services.AddScoped<IReservationsRepository, ReservationsRepository>();
    builder.Services.AddScoped<IReservationStatusChangerJob, ReservationStatusChangerJob>();
    builder.Services.AddScoped<ILocationsService, LocationsService>();
    builder.Services.AddScoped<ILocationSpotsService, LocationSpotsService>();
    builder.Services.AddScoped<IUsersService, UsersService>();
    builder.Services.AddScoped<IReservationsService, ReservationsService>();

    //Quartz jobs
    builder.Services.QuartzServicesInfrastructure();

    //Invalid model state
    builder.Services.InvalidModelStateResponseInfrastructure();

    var app = builder.Build();



    // Configure the HTTP request pipeline.
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseMiddleware<ExceptionMiddleware>();

    app.UseHttpsRedirection();

    app.UseAuthorization();

    app.MapControllers();

    app.Run();


    // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle

}
catch (Exception ex)
{
    logger.Error(ex);
}
finally
{
    LogManager.Shutdown();
}


