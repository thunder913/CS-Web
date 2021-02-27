using Git.Services;
using SUS.HTTP;
using SUS.MvcFramework;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.ComponentModel.DataAnnotations;
namespace Git.Controllers
{
    public class UsersController : Controller
    {
        public IUsersService usersService;
        public UsersController(IUsersService usersService)
        {
            this.usersService = usersService;
        }
        public HttpResponse Login()
        {
            if (IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            return this.View();
        }

        public HttpResponse Register()
        {
            if (IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Login(string username, string password)
        {
            
            var encryptedPass = Encrypt(password);
            var id = usersService.GetUserId(username, encryptedPass);
            if (id != null)
            {
                SignIn(id);
            }
            else
            {
                return Error("Invalid username or password!");
            }
            return this.Redirect("/Repositories/All");
        }

        [HttpPost]
        public HttpResponse Register(string username, string email, string password, string confirmPassword)
        {
            var emailvalidator = new EmailAddressAttribute();
            if (password.Length<6 || password.Length>20)
            {
                return this.Error("The password must be between 6 and 20 characters long!");
            }
            if (username.Length<5||username.Length>20)
            {
                return this.Error("The username must be between 5 and 20 characters long!");
            }
            if (password!=confirmPassword)
            {
                return this.Error("The passwords must match!");
            }
            if (!usersService.IsUsernameAvailable(username))
            {
                return this.Error("There is already a user with that username!");
            }
            if (!usersService.IsEmailAvailable(email))
            {
                return this.Error("There is already a user with this email!");
            }
            if (!emailvalidator.IsValid(email))
            {
                return this.Error("Invalid email!");
            }
            var encryptedPass = Encrypt(password);
            usersService.CreateUser(username, email, encryptedPass);
            return this.Redirect("/Users/Login");
        }

        public HttpResponse Logout()
        {
            if (!IsUserSignedIn())
            {
                return this.Redirect("/");
            }
            SignOut();
            return this.Redirect("/");
        }


        private static string Encrypt(string inputString)
        {
            using (SHA512 shaM = new SHA512Managed())
            {
                return Convert.ToBase64String(shaM.ComputeHash(Encoding.UTF8.GetBytes(inputString)));
            }
        }
    }
}
