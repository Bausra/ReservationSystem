using ReservationSystem.WebApi.Models;
using ReservationSystem.WebApi.Repositories.Interfaces;
using ReservationSystem.WebApi.Services.Interfaces;

namespace ReservationSystem.WebApi.Services.Implementations
{
    public class LocationsService : ILocationsService
    {
        private readonly ILocationsRepository _locationsRepository;
        private readonly IReservationsService _reservationsService;
        private readonly ILocationSpotsService _locationSpotsService;

        public LocationsService(ILocationsRepository locationsRepository, IReservationsService reservationsService, ILocationSpotsService locationSpotsService)
        {
            _locationsRepository = locationsRepository;
            _reservationsService = reservationsService;
            _locationSpotsService = locationSpotsService;
        }

        public Location AddLocation(Location newLocation)
        {
            return _locationsRepository.AddLocation(newLocation);
        }

        public Location DeleteLocation(int id)
        {
            return _locationsRepository.DeleteLocation(id);
        }

        public bool Exists(int locationId)
        {
            return _locationsRepository.Exists(locationId);
        }

        public List<Location> GetAllLocations()
        {
            return _locationsRepository.GetAllLocations();
        }

        public List<string> GetLocationNames()
        {
            return _locationsRepository.GetLocationNames();
        }

        public Location GetLocation(int locationId)
        {
            return _locationsRepository.GetLocation(locationId);
        }

        public void UpdateLocation(int id, Location updatedLocation)
        {
            _locationsRepository.UpdateLocation(id, updatedLocation);
        }

        public Location ExecuteAddLocation(Location newLocation)
        {
            newLocation.Id = 0;
            newLocation.Status = Status.ACTIVE;

            foreach (var item in newLocation.LocationSpots)
            {
                item.Id = 0;
                item.Status = Status.ACTIVE;
            }

            newLocation = AddLocation(newLocation);
            _locationsRepository.SaveChangesToDatabase();

            return newLocation;
        }

        public bool LocationNameExists(string locationName, bool toLowerCase = false)
        {
            List<string> locationNames = GetLocationNames();

            if (toLowerCase)
            {
                var convertedLocationsList = locationNames.ConvertAll(x => x.ToLower());
                return convertedLocationsList.Contains(locationName.ToLower());
            }
            return locationNames.Contains(locationName);
        }

        public bool ExistingLocationNameChanged(int id, string nameToChangeTo, bool toLowerCase = false)
        {
            Location location = GetLocation(id);

            if (toLowerCase)
                return location.Name.ToLower() != nameToChangeTo.ToLower();

            return location.Name != nameToChangeTo;
        }

        public void ExecuteDeleteLocation(int locationId)
        {
            List<Reservation> reservationsForLocation = _reservationsService.GetReservationsForLocation(locationId);
            if (reservationsForLocation?.Any() ?? false)
                reservationsForLocation.ForEach(reservation => _reservationsService.UpdateReservationStatus(reservation.Id, Status.DELETED));

            List<LocationSpot> locationSpotsForLocationId = _locationSpotsService.GetLocationSpotsForLocation(locationId);
            if (locationSpotsForLocationId?.Any() ?? false)
                locationSpotsForLocationId.ForEach(spot => _locationSpotsService.DeleteLocationSpot(spot.Id));

            DeleteLocation(locationId);
            _locationsRepository.SaveChangesToDatabase();
        }

        public void ExacuteUpdateLocation(int locationId, Location updatedLocation)
        {
            updatedLocation.Id = locationId;
            foreach (var item in updatedLocation.LocationSpots)
            {
                item.LocationId = locationId;
            }

            bool locationNameChanged = ExistingLocationNameChanged(locationId, updatedLocation.Name, true);
            bool nameAlreadyAssigned = LocationNameExists(updatedLocation.Name, true);

            if (locationNameChanged)
            {
                if (nameAlreadyAssigned)
                {
                    throw new BadHttpRequestException($"Location with name '{updatedLocation.Name}' already exists! If it should be updated, please provide correct Id.");
                }
                else
                {
                    UpdateLocation(locationId, updatedLocation);
                }
            }

            List<int> notProvidedLocationSpots = _locationSpotsService.GetNotProvidedLocationSpotsForLocation(locationId, updatedLocation.LocationSpots);
            if (notProvidedLocationSpots.Any())
            {
                foreach (var item in notProvidedLocationSpots)
                {
                    List<Reservation> reservationsForLocationSpot = _reservationsService.GetReservationsForLocationSpot(item);
                    if (reservationsForLocationSpot?.Any() ?? false)
                        reservationsForLocationSpot.ForEach(reservation => _reservationsService.UpdateReservationStatus(reservation.Id, Status.DELETED));

                    _locationSpotsService.DeleteLocationSpot(item);
                }
            }

            foreach (var item in updatedLocation.LocationSpots)
            {
                if (_locationSpotsService.LocationSpotCanBeUpdated(locationId, item.Id, item.Name, true))
                {
                    _locationSpotsService.UpdateLocationSpot(item.Id, item);
                }
                else if (_locationSpotsService.LocationSpotCanBeAdded(locationId, item.Id, item.Name, true))
                {
                    item.Id = 0;
                    item.Status = Status.ACTIVE;
                    _locationSpotsService.AddLocationSpot(item);
                }
                else if (_locationSpotsService.LocationSpotCannotBeAddedOrUpdated(locationId, item.Id, item.Name, true))
                {
                    throw new BadHttpRequestException($"Location spot with name '{item.Name}' already exists! No creation or change is possible!");
                }
            }
            _locationsRepository.SaveChangesToDatabase();
        }
    }
}
