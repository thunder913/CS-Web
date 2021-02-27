using BattleCards.Services;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.ComponentModel.DataAnnotations;
using System.Security.Cryptography;
using System.Text;

namespace BattleCards.Controllers
{
    public class UsersController : Controller
    {
        private IUsersService usersService;

        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }
        public HttpResponse Register()
        {
            if (IsUserSignedIn())
            {
                return this.Error("You must be logged in to access this page!");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Register(string username, string email, string password, string confirmPassword)
        {
            if (IsUserSignedIn())
            {
                return this.Error("You must be logged in to access this page!");
            }
            var emailValidator = new EmailAddressAttribute();
            if (password!=confirmPassword)
            {
                return this.Error("The passwords must match!");
            }
            if (password.Length<6||password.Length>20)
            {
                return this.Error("The password must be between 6 and 20 characters long!");
            }
            if (!emailValidator.IsValid(email))
            {
                return this.Error("You have entered an invalid email!");
            }
            if (usersService.IsUsernameTaken(username))
            {
                return this.Error("This username is taken!");
            }
            if (usersService.IsEmailTaken(username))
            {
                return this.Error("This email is taken!");
            }

            var hashedPass = Encrypt(password);
            usersService.AddUser(username, email, hashedPass);
            return this.Redirect("/Users/Login");

        }

        public HttpResponse Login()
        {
            if (IsUserSignedIn())
            {
                return this.Error("You must be logged in to access this page!");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(string username, string password)
        {
            if (IsUserSignedIn())
            {
                return this.Error("You must be logged in to access this page!");
            }
            var userId = usersService.UserExists(username, Encrypt(password));
            if (userId != null)
            {
                SignIn(userId);
            }
            else
            {
                return this.Error("Invalid credentials!");
            }

            return this.Redirect("/Cards/All");
        }

        public string Encrypt(string inputString)
        {
            using (SHA512 shaM = new SHA512Managed())
            {
                return Convert.ToBase64String(shaM.ComputeHash(Encoding.UTF8.GetBytes(inputString)));
            }
        }
    }
}
