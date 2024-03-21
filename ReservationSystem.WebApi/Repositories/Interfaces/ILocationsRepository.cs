using ReservationSystem.WebApi.Models;

namespace ReservationSystem.WebApi.Repositories.Interfaces
{
    public interface ILocationsRepository
    {
        List<Location> GetAllLocations();
        Location GetLocation(int id);
        Location AddLocation(Location newLocation);
        void UpdateLocation(int id, Location updatedLocation);
        Location DeleteLocation(int id);
        bool Exists(int id);
        int SaveChangesToDatabase();
        List<string> GetLocationNames();
    }
}
