using ReservationSystem.WebApi.Models;

namespace ReservationSystem.WebApi.Repositories.Interfaces
{
    public interface ILocationSpotsRepository
    {
        List<LocationSpot> GetAllLocationSpots();
        LocationSpot GetLocationSpot(int id);
        LocationSpot AddLocationSpot(LocationSpot newLocationSpot);
        void UpdateLocationSpot(int id, LocationSpot updatedLocationSpot);
        LocationSpot DeleteLocationSpot(int id);
        bool ExistsForLocation(int id, int locationId);
        int SaveChangesToDatabase();
        List<int> GetLocationSpotIdsForLocation(int locationId);
        List<LocationSpot> GetLocationSpotsForLocation(int locationId);
        List<string> GetLocationSpotNamesForLocation(int locationId);
    }
}
