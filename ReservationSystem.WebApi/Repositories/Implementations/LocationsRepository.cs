using Microsoft.EntityFrameworkCore;
using ReservationSystem.WebApi.Data;
using ReservationSystem.WebApi.Models;
using ReservationSystem.WebApi.Repositories.Interfaces;

namespace ReservationSystem.WebApi.Repositories.Implementations
{
    public class LocationsRepository : ILocationsRepository
    {
        private readonly ReservationSystemDbContext _context;
        public LocationsRepository(ReservationSystemDbContext context)
        {
            _context = context;
        }

        public Location AddLocation(Location newLocation)
        {
            _context.Locations.Add(newLocation);
            return newLocation;
        }

        public Location DeleteLocation(int id)
        {
            Location location = GetLocation(id);
            location.Status = Status.DELETED;
            return location;
        }

        public bool Exists(int id)
        {
            return _context.Locations.Any(u => u.Id == id && u.Status != Status.DELETED);
        }

        public List<Location> GetAllLocations()
        {
            return _context.Locations.Where(z => z.Status != Status.DELETED).
                Include(x => x.LocationSpots.Where(y => y.Status != Status.DELETED)).
                ToList();
        }

        public List<string> GetLocationNames()
        {
            return _context.Locations.Where(z => z.Status != Status.DELETED).Select(x => x.Name).ToList();
        }

        public Location GetLocation(int id)
        {
            return _context.Locations.Where(x => x.Id == id).
                Include(x => x.LocationSpots.Where(y => y.Status != Status.DELETED)).
                First();
        }

        public int SaveChangesToDatabase()
        {
            return _context.SaveChanges();
        }

        public void UpdateLocation(int id, Location updatedLocation)
        {
            Location existingLocation = _context.Locations.Where(x => x.Id == id).First();
            existingLocation.Name = updatedLocation.Name;
        }
    }
}
