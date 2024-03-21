using ReservationSystem.WebApi.Models;

namespace ReservationSystem.WebApi.Services.Interfaces
{
    public interface IUsersService
    {
        User AddUser(User newUser);
        User DeleteUser(int id);
        bool Exists(int userId);
        List<User> GetAllUsers();
        User GetUser(int userId);
        void UpdateUser(int userId, User updatedUser);

        void ExecuteUpdateUser(int userId, User updatedUser);
        void ExecuteDeleteUser(int userId);
    }
}
