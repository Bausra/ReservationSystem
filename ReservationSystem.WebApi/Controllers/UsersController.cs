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
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;
        private readonly IMapper _mapper;
        public UsersController(IUsersService usersService,IMapper mapper)
        {
            _usersService = usersService;
            _mapper = mapper;
        }

        /// <summary>Will retrieve all users.</summary>
        /// <remarks>Important! This endpoint supports OData functionality: filter, sort, order by.</remarks>
        /// <response code="200">Sucess. Returns JSON array containing all users.</response>
        [HttpGet]
        [EnableQuery]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<List<UserDto>> GetUsers()
        {
            List<User> users = _usersService.GetAllUsers();

            var result = _mapper.Map<List<UserDto>>(users);

            return Ok(result);
        }

        /// <summary>Will retrieve single user item.</summary>
        /// <response code="200">Sucess. Returns JSON containing user details.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Not found.</response>
        [HttpGet("{userId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserDto> GetUser(int userId)
        {
            if (!_usersService.Exists(userId))
                return NotFound();

            User user = _usersService.GetUser(userId);

            var result = _mapper.Map<UserDto>(user);

            return Ok(result);
        }

        /// <summary>Will create user item.</summary>
        /// <remarks>Important! User id is generated automatically, thus, this value should not be provided in the request.</remarks>
        /// <response code="201">Created. Returns created user item with automatically generated user ID.</response>
        /// <response code="400">Bad request.</response>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        public ActionResult<UserDto> AddUser([FromBody] UserDto user)
        {
            User newUser = _mapper.Map<User>(user);

            newUser = _usersService.AddUser(newUser);

            var result = _mapper.Map<UserDto>(newUser);

            return StatusCode(StatusCodes.Status201Created, result);
        }

        /// <summary>Will delete single user item.</summary>
        /// <response code="204">Sucess. No content will be returned.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Not found.</response>
        [HttpDelete("{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult DeleteUser(int userId)
        {
            if (!_usersService.Exists(userId))
                return NotFound();

            _usersService.ExecuteDeleteUser(userId);

            return NoContent();
        }

        /// <summary>Will update single user item.</summary>
        /// <response code="204">Sucess. No content will be returned.</response>
        /// <response code="400">Bad request.</response>
        /// <response code="404">Not found.</response>
        [HttpPut("{userId}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<UserDto> UpdateUser(int userId, [FromBody] UserDto user)
        {
            if (!_usersService.Exists(userId))
                return NotFound();

            User updatedUser = _mapper.Map<User>(user);

            _usersService.ExecuteUpdateUser(userId, updatedUser);

            return NoContent();
        }
    }
}
