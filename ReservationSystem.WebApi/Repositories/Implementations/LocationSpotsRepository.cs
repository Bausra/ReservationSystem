using ReservationSystem.WebApi.Data;
using ReservationSystem.WebApi.Models;
using ReservationSystem.WebApi.Repositories.Interfaces;

namespace ReservationSystem.WebApi.Repositories.Implementations
{
    public class LocationSpotsRepository : ILocationSpotsRepository
    {
        private readonly ReservationSystemDbContext _context;
        public LocationSpotsRepository(ReservationSystemDbContext context)
        {
            _context = context;
        }

        public LocationSpot AddLocationSpot(LocationSpot newlocationSpot)
        {
            _context.LocationSpots.Add(newlocationSpot);
            return newlocationSpot;
        }

        public LocationSpot DeleteLocationSpot(int id)
        {
            LocationSpot locationSpot = GetLocationSpot(id);
            locationSpot.Status = Status.DELETED;
            return locationSpot;
        }

        public bool ExistsForLocation(int id, int locationId)
        {
            return _context.LocationSpots.Any(u => u.Id == id && u.LocationId == locationId && u.Status != Status.DELETED);
        }

        public List<LocationSpot> GetAllLocationSpots()
        {
            return _context.LocationSpots.Where(z => z.Status != Status.DELETED).ToList();
        }

        public List<LocationSpot> GetLocationSpotsForLocation(int locationId)
        {
            return _context.LocationSpots.Where(x => x.LocationId == locationId).ToList();
        }

        public List<string> GetLocationSpotNamesForLocation(int locationId)
        {
            List<LocationSpot> locationSpotsForlocation = GetLocationSpotsForLocation(locationId);
            return locationSpotsForlocation.Select(x => x.Name).ToList();
        }

        public LocationSpot GetLocationSpot(int id)
        {
            return _context.LocationSpots.Where(x => x.Id == id).FirstOrDefault();
        }

        public int SaveChangesToDatabase()
        {
            return _context.SaveChanges();
        }

        public void UpdateLocationSpot(int id, LocationSpot updatedLocationSpot)
        {
            LocationSpot existingLocationSpot = GetLocationSpot(id);
            _context.Entry(existingLocationSpot).CurrentValues.SetValues(updatedLocationSpot);
        }

        public List<int> GetLocationSpotIdsForLocation(int locationId)
        {
            return _context.LocationSpots.Where(x => x.Status != Status.DELETED && x.LocationId == locationId).Select(o => o.Id).ToList();
        }
    }
}
