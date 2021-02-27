using BattleCards.Data;
using BattleCards.Models;
using System.Linq;

namespace BattleCards.Services
{
    public class UsersService : IUsersService
    {
        private ApplicationDbContext db;

        public UsersService(ApplicationDbContext dbContext)
        {
            this.db = dbContext;
        }
        public void AddUser(string username, string email, string password)
        {
            var user = new User()
            {
                Email = email,
                Username = username,
                Password = password
            };
            db.Users.Add(user);
            db.SaveChanges();

        }

        public bool IsEmailTaken(string email)
        {
            return db.Users.Any(x => x.Email == email);
        }

        public bool IsUsernameTaken(string username)
        {
            return db.Users.Any(x => x.Username == username);
        }

        public string UserExists(string username, string password)
        {
            return db.Users
                        .Where(u => u.Username == username && u.Password == password)
                        .Select(x=>x.Id)
                        .FirstOrDefault();
        }
    }
}
