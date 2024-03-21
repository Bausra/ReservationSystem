using ReservationSystem.WebApi.Models;

namespace ReservationSystem.WebApi.Services.Interfaces
{
    public interface ILocationsService
    {
        Location AddLocation(Location newLocation);
        Location DeleteLocation(int id);
        bool Exists(int locationId);
        List<Location> GetAllLocations();
        List<string> GetLocationNames();
        Location GetLocation(int locationId);
        void UpdateLocation(int locationId, Location updatedLocation);

        Location ExecuteAddLocation(Location newLocation);
        bool LocationNameExists(string locationName, bool toLowerCase = false);
        bool ExistingLocationNameChanged(int id, string nameToChangeTo, bool toLowerCase = false);
        void ExecuteDeleteLocation(int locationId);
        void ExacuteUpdateLocation(int locationId, Location updatedLocation);
    }
}
