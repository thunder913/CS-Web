using Git.Data;
using Git.Data.Models;
using System.Linq;

namespace Git.Services
{
    public class UsersService : IUsersService
    {

        private readonly ApplicationDbContext db = new ApplicationDbContext();
        public UsersService(ApplicationDbContext database)
        {
            db = database;
        }


        public void CreateUser(string username, string email, string password)
        {
            var user = new User()
            {
                Username = username,
                Email = email,
                Password = password
            };
            db.Users.Add(user);
            db.SaveChanges();
        }

        public string GetUserId(string username, string password)
        {
            var user =  db
                .Users
                .Where(x => x.Username == username && x.Password == password)
                .FirstOrDefault();
            return user.Id;
        }

        public bool IsEmailAvailable(string email)
        {
            return !db
                .Users
                .Any(x => x.Email == email);
        }

        public bool IsUsernameAvailable(string username)
        {
            return !db
                .Users
                .Any(x => x.Username == username);
        }
    }
}
