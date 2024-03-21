# About

API for Reservation System to excersise API skills

# Endpoints created:
- GET /Locations
- POST /Locations
- GET /Locations/{locationId}
- DELETE /Locations/{locationId}
- PUT /Locations/{locationId}
- GET /Locations/{locationId}/reservations


- GET /Reservations
- POST /Reservations
- GET /Reservations/{reservationId}
- POST /Reservations/{reservationId}/actions/cancel

- GET /Users
- POST /Users
- GET /Users/{userId}
- DELETE /Users/{userId}
- PUT /Users/{userId}

# Stack

- .NET
- C#
- MSSQL
- ASP.NET Core Web API

# Build

After cloning project from repository:

- Rebuild project
- In Package Manager Console run: PM> update-database
- Run ReservationSystem.WebApi

Important> Project is configured to be run on MSSQL database, Server=localhost. In case your server is different:

- Change server name in ConnectionStrings (ReservationSystem.WebApi => appsettings.json file)
- Delete Migrations folder (ReservationSystem.WebApi => Migrations)
- In Package Manager Console run: PM> add-migration Init
- In Package Manager Console run: PM> update-database
