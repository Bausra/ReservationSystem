using ReservationSystem.WebApi.Models;
using ReservationSystem.WebApi.Repositories.Interfaces;
using ReservationSystem.WebApi.Services.Interfaces;

namespace ReservationSystem.WebApi.Services.Implementations
{
    public class UsersService : IUsersService
    {
        private readonly IUsersRepository _usersRepository;
        private readonly IReservationsService _reservationsService;

        public UsersService(IUsersRepository usersRepository, IReservationsService reservationsService)
        {
            _usersRepository = usersRepository;
            _reservationsService = reservationsService;
        }

        public User AddUser(User newUser)
        {
            newUser.Id = 0;
            newUser.Status = Status.ACTIVE;

            newUser = _usersRepository.AddUser(newUser);
            _usersRepository.SaveChangesToDatabase();
            return newUser;
        }

        public User DeleteUser(int id)
        {
            return _usersRepository.DeleteUser(id);
        }

        public bool Exists(int userId)
        {
            return _usersRepository.Exists(userId);
        }

        public List<User> GetAllUsers()
        {
            return _usersRepository.GetAllUsers();
        }

        public User GetUser(int userId)
        {
            return _usersRepository.GetUser(userId);
        }

        public void UpdateUser(int userId, User updatedUser)
        {
            _usersRepository.UpdateUser(userId, updatedUser);
        }

        public void ExecuteUpdateUser(int userId, User updatedUser)
        {
            updatedUser.Id = userId;

            UpdateUser(userId, updatedUser);
            _usersRepository.SaveChangesToDatabase();
        }

        public void ExecuteDeleteUser(int userId)
        {
            List<Reservation> userReservations = _reservationsService.GetUserReservations(userId);
            if (userReservations?.Any() ?? false)
                userReservations.ForEach(reservation => _reservationsService.UpdateReservationStatus(reservation.Id, Status.DELETED));

            DeleteUser(userId);
            _usersRepository.SaveChangesToDatabase();
        }
    }
}
