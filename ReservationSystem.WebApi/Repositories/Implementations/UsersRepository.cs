using ReservationSystem.WebApi.Data;
using ReservationSystem.WebApi.Models;
using ReservationSystem.WebApi.Repositories.Interfaces;

namespace ReservationSystem.WebApi.Repositories.Implementations
{
    public class UsersRepository : IUsersRepository
    {
        private readonly ReservationSystemDbContext _context;
        public UsersRepository(ReservationSystemDbContext context)
        {
            _context = context;
        }

        public User AddUser(User user)
        {
            _context.Users.Add(user);
            return user;
        }

        public User DeleteUser(int id)
        {
            User user = GetUser(id);
            user.Status = Status.DELETED;
            return user;
        }

        public bool Exists(int id)
        {
            return _context.Users.Any(u => u.Id == id && u.Status != Status.DELETED);
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.Where(z => z.Status != Status.DELETED).ToList();
        }

        public User GetUser(int id)
        {
            return _context.Users.Where(x => x.Id == id).First();
        }

        public void UpdateUser(int id, User updatedUser)
        {
            User existingUser = GetUser(id);
            _context.Entry(existingUser).CurrentValues.SetValues(updatedUser);
        }

        public int SaveChangesToDatabase()
        {
            return _context.SaveChanges();
        }
    }
}
