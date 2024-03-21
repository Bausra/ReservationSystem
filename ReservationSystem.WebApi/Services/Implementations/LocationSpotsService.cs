using ReservationSystem.WebApi.Models;
using ReservationSystem.WebApi.Repositories.Interfaces;
using ReservationSystem.WebApi.Services.Interfaces;

namespace ReservationSystem.WebApi.Services.Implementations
{
    public class LocationSpotsService : ILocationSpotsService
    {
        private readonly ILocationSpotsRepository _locationSpotsRepository;

        public LocationSpotsService(ILocationSpotsRepository locationSpotsRepository)
        {
            _locationSpotsRepository = locationSpotsRepository;
        }


        public LocationSpot AddLocationSpot(LocationSpot newLocationSpot)
        {
            return _locationSpotsRepository.AddLocationSpot(newLocationSpot);
        }

        public LocationSpot DeleteLocationSpot(int id)
        {
            return _locationSpotsRepository.DeleteLocationSpot(id);
        }

        public bool ExistsForLocation(int id, int locationId)
        {
            return _locationSpotsRepository.ExistsForLocation(id, locationId);
        }

        public List<LocationSpot> GetAllLocationSpots()
        {
            return _locationSpotsRepository.GetAllLocationSpots();
        }

        public List<LocationSpot> GetLocationSpotsForLocation(int locationId)
        {
            return _locationSpotsRepository.GetLocationSpotsForLocation(locationId);
        }

        public List<string> GetLocationSpotNamesForLocation(int locationId)
        {
            return _locationSpotsRepository.GetLocationSpotNamesForLocation(locationId);
        }

        public LocationSpot GetLocationSpot(int id)
        {
            return _locationSpotsRepository.GetLocationSpot(id);
        }

        public void UpdateLocationSpot(int id, LocationSpot updatedLocationSpot)
        {
            _locationSpotsRepository.UpdateLocationSpot(id, updatedLocationSpot);
        }

        public List<int> GetLocationSpotIdsForLocation(int locationId)
        {
            return _locationSpotsRepository.GetLocationSpotIdsForLocation(locationId);
        }

        public bool ExistingLocationSpotNameChanged(int id, string nameToChangeTo, bool toLowerCase = false)
        {
            LocationSpot locationSpot = GetLocationSpot(id);

            if (toLowerCase)
                return locationSpot.Name.ToLower() != nameToChangeTo.ToLower();

            return locationSpot.Name != nameToChangeTo;
        }

        public bool LocationSpotNameForLocationExists(string locationSpotName, int locationId, bool toLowerCase = false)
        {
            var locationSpotNamesForLocation = GetLocationSpotNamesForLocation(locationId);

            if (toLowerCase)
            {
                var convertedLocationSpotList = locationSpotNamesForLocation.ConvertAll(x => x.ToLower());
                return convertedLocationSpotList.Contains(locationSpotName.ToLower());
            }
            return locationSpotNamesForLocation.Contains(locationSpotName);
        }

        public List<int> GetNotProvidedLocationSpotsForLocation(int locationId, List<LocationSpot> providedLocationSpots)
        {
            List<int> locationSpotIdsInDb = GetLocationSpotIdsForLocation(locationId);
            List<int> providedLocationSpotsIds = providedLocationSpots.Select(o => o.Id).ToList();

            return locationSpotIdsInDb.Except(providedLocationSpotsIds).ToList();
        }

        public bool LocationSpotCanBeUpdated(int locationId, int locationSpotId, string locationSpotNameToUpdate, bool toLowerCase = false)
        {
            return ExistsForLocation(locationSpotId, locationId)
                    && ExistingLocationSpotNameChanged(locationSpotId, locationSpotNameToUpdate, toLowerCase)
                    && !LocationSpotNameForLocationExists(locationSpotNameToUpdate, locationId, toLowerCase);
        }
        public bool LocationSpotCanBeAdded(int locationId, int locationSpotId, string locationSpotNameToAdd, bool toLowerCase = false)
        {
            return !ExistsForLocation(locationSpotId, locationId)
                    && !LocationSpotNameForLocationExists(locationSpotNameToAdd, locationId, toLowerCase);
        }

        public bool LocationSpotCannotBeAddedOrUpdated(int locationId, int locationSpotId, string locationSpotName, bool toLowerCase = false)
        {
            return !ExistsForLocation(locationSpotId, locationId)
                    && LocationSpotNameForLocationExists(locationSpotName, locationId, toLowerCase)
                    || ExistsForLocation(locationSpotId, locationId)
                    && ExistingLocationSpotNameChanged(locationSpotId, locationSpotName, toLowerCase)
                    && LocationSpotNameForLocationExists(locationSpotName, locationId, toLowerCase);
        }
    }
}
