using ReservationSystem.WebApi.Models;

namespace ReservationSystem.WebApi.Repositories.Interfaces
{
    public interface IUsersRepository
    {
        List<User> GetAllUsers();
        User GetUser(int id);
        User AddUser(User newUser);
        void UpdateUser(int id, User updatedUser);
        User DeleteUser(int id);
        bool Exists(int id);
        int SaveChangesToDatabase();
    }
}
