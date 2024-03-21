using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using ReservationSystem.WebApi.DTOs;
using ReservationSystem.WebApi.Models;
using ReservationSystem.WebApi.Services.Interfaces;

namespace ReservationSystem.WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    [Consumes("application/json")]
    public class LocationsController : ControllerBase
    {
        private readonly ILocationsService _locationsService;
        private readonly IReservationsService _reservationsService;
        private readonly IMapper _mapper;
        public LocationsController(ILocationsService locationsService, IReservationsService reservationsService, IMapper mapper)
        {
            _locationsService = locationsService;
            _reservationsService = reservationsService;
            _mapper = mapper;
        }

        /// <summary>Will retrieve all location items.</summary>
        /// <remarks>Important! This endpoint supports OData functionality: filter, sort, order by.</remarks>
        /// <response code="200">Sucess. Returns JSON array that includes location items along with their corresponding location spots.</response>
        [HttpGet]
        [EnableQuery]
        [ProducesResponseType(typeof(LocationDto), StatusCodes.Status200OK)]
        public ActionResult<List<LocationDto>> GetLocations()
        {
            List<Location> locations = _locationsService.GetAllLocations();

            var result = _mapper.Map<List<LocationDto>>(locations);

            return Ok(result);
        }

        /// <summary>Will retrieve single location item.</summary>
        /// <response code="200">Sucess. Returns JSON containing location details and an array of its associated location spots.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Not found.</response>
        [HttpGet("{locationId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<LocationDto> GetLocation(int locationId)
        {
            if (!_locationsService.Exists(locationId))
                return NotFound();

            Location location = _locationsService.GetLocation(locationId);

            var result = _mapper.Map<LocationDto>(location);

            return Ok(result);
        }

        /// <summary>Will retrieve reservation items for location.</summary>
        /// <response code="200">Sucess. Returns JSON containing reservation details for provided location ID.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Not found.</response>
        [HttpGet("{locationId}/reservations")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<List<ReservationDto>> GetReservationsForLocation(int locationId)
        {
            if (!_locationsService.Exists(locationId))
                return NotFound();

            List<Reservation> reservations = _reservationsService.GetReservationsForLocation(locationId);

            var result = _mapper.Map<List<ReservationDto>>(reservations);

            return Ok(result);
        }

        /// <summary>Will create location item with it's corresponding location spots.</summary>
        /// <remarks>Important! Location id, as well as Location spot Id is generated automatically, thus, these values should not be provided in the request.</remarks>
        /// <response code="201">Created. Returns created location item with automatically generated location ID.</response>
        /// <response code="400">Bad request.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<LocationDto> AddLocation([FromBody] LocationDto location)
        {
            Location newLocation = _mapper.Map<Location>(location);

            if (_locationsService.LocationNameExists(newLocation.Name, true))
                return BadRequest($"Location with name '{newLocation.Name}' already exists!");

            newLocation = _locationsService.ExecuteAddLocation(newLocation);

            var result = _mapper.Map<LocationDto>(newLocation);

            return StatusCode(StatusCodes.Status201Created, result);
        }

        /// <summary>Will delete location item with it's corresponding location spots.</summary>
        /// <response code="204">Sucess. No content will be returned.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Not found.</response>
        [HttpDelete("{locationId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteLocation(int locationId)
        {
            if (!_locationsService.Exists(locationId))
                return NotFound();

            _locationsService.ExecuteDeleteLocation(locationId);

            return NoContent();
        }

        /// <summary>Will update location item with it's corresponding location spots.</summary>
        /// <remarks>
        /// Important! 
        /// 
        /// 1) Updatable location should be provided along with corresponding location spots data:
        /// { "id": 1, "name": "Parking lot", "locationSpots": [ { "id": 1000, "name": "A1" }, { "id": 1001, "name": "A2" } ] }
        /// 2) Location Id as well as corresponding Location spot Id's should match existing ones.
        /// 3) Adjustments:
        ///      - To update location or corresponding location spot name/s, adjust current ones, while keeping id's unchanged.
        ///      { "id": 1, "name": "Parking", "locationSpots": [ { "id": 1000, "name": "A1" }, { "id": 1001, "name": "A2" } ] }
        ///      Or:
        ///      { "id": 1, "name": "Parking lot", "locationSpots": [ { "id": 1000, "name": "A1" }, { "id": 1001, "name": "A3" } ] }
        ///      - To delete location spot, exclude location spot item from Location spots array.
        ///      { "id": 1, "name": "Parking lot", "locationSpots": [ { "id": 1001, "name": "A3" } ] }
        ///      - To add location spot for location, add only location spot name to "LocationSpots" array.
        ///      { "id": 1, "name": "Parking lot", "locationSpots": [ { "id": 1001, "name": "A3" }, { "name": "A4" } ] }
        /// </remarks>
        /// <response code="204">Sucess. No content will be returned.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Not found.</response>
        [HttpPut("{locationId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserDto> UpdateLocation(int locationId, [FromBody] LocationDto location)
        {
            if (!_locationsService.Exists(locationId))
                return NotFound();

            Location updatedLocation = _mapper.Map<Location>(location);

            try
            {
                _locationsService.ExacuteUpdateLocation(locationId, updatedLocation);

                return NoContent();
            }
            catch (BadHttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
