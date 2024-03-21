using ReservationSystem.WebApi.Models;

namespace ReservationSystem.WebApi.Services.Interfaces
{
    public interface ILocationSpotsService
    {
        LocationSpot AddLocationSpot(LocationSpot newLocationSpot);
        LocationSpot DeleteLocationSpot(int id);
        bool ExistsForLocation(int id, int locationId);
        List<LocationSpot> GetAllLocationSpots();
        List<LocationSpot> GetLocationSpotsForLocation(int locationId);
        List<string> GetLocationSpotNamesForLocation(int locationId);
        LocationSpot GetLocationSpot(int id);
        void UpdateLocationSpot(int id, LocationSpot updatedLocationSpot);
        List<int> GetLocationSpotIdsForLocation(int locationId);

        bool ExistingLocationSpotNameChanged(int id, string nameToChangeTo, bool toLowerCase = false);
        bool LocationSpotNameForLocationExists(string locationSpotName, int locationId, bool toLowerCase = false);
        List<int> GetNotProvidedLocationSpotsForLocation(int locationId, List<LocationSpot> providedLocationSpots);
        bool LocationSpotCanBeUpdated(int locationId, int locationSpotId, string locationSpotNameToUpdate, bool toLowerCase = false);
        bool LocationSpotCanBeAdded(int locationId, int locationSpotId, string locationSpotNameToAdd, bool toLowerCase = false);
        bool LocationSpotCannotBeAddedOrUpdated(int locationId, int locationSpotId, string locationSpotName, bool toLowerCase = false);
    }
}
