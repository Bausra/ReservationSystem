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
    public class ReservationsController : ControllerBase
    {
        private readonly IReservationsService _reservationsService;
        private readonly IMapper _mapper;
        public ReservationsController(IReservationsService reservationsService, IMapper mapper)
        {
            _reservationsService = reservationsService;
            _mapper = mapper;
        }

        /// <summary>Will retrieve all reservations.</summary>
        /// <remarks>Important! This endpoint supports OData functionality: filter, sort, order by.</remarks>
        /// <response code="200">Sucess. Returns JSON array containing all reservations.</response>
        [HttpGet]
        [EnableQuery]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<ReservationDto>> GetReservations()
        {
            List<Reservation> reservations = _reservationsService.GetAllReservations();

            var result = _mapper.Map<List<ReservationDto>>(reservations);

            return Ok(result);
        }

        /// <summary>Will retrieve single reservation item.</summary>
        /// <response code="200">Sucess. Returns JSON containing reservation details.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Not found.</response>
        [HttpGet("{reservationId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<ReservationDto> GetReservation(int reservationId)
        {
            if (!_reservationsService.Exists(reservationId))
                return NotFound();

            Reservation reservation = _reservationsService.GetReservation(reservationId);

            var result = _mapper.Map<ReservationDto>(reservation);

            return Ok(result);
        }

        /// <summary>Will create reservation item.</summary>
        /// <remarks>Important! Reservation id and status is generated automatically, thus, this value should not be provided in the request.</remarks>
        /// <response code="201">Created. Returns created reservation item with automatically generated reservation ID.</response>
        /// <response code="400">Bad request.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<ReservationDto> AddReservation([FromBody] ReservationDto reservation)
        {
            try
            {
                Reservation newReservation = _mapper.Map<Reservation>(reservation);

                newReservation = _reservationsService.ExecuteAddReservation(newReservation);

                var result = _mapper.Map<ReservationDto>(newReservation);

                return StatusCode(StatusCodes.Status201Created, result);
            }
            catch(BadHttpRequestException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>Will cancel single reservation item.</summary>
        /// <response code="204">Sucess. No content will be returned.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Not found.</response>
        [HttpPost("{reservationId}/actions/cancel")] 
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult CancelReservation(int reservationId)
        {
            if (!_reservationsService.Exists(reservationId))
                return NotFound();

            if (_reservationsService.IsCancelled(reservationId) || _reservationsService.IsCompleted(reservationId))
                return BadRequest($"Item with reservation id '{reservationId}' is already cancelled or completed!");

            _reservationsService.ExecuteUpdateReservationStatus(reservationId, Status.CANCELLED);

            return NoContent();
        }
    }
}
