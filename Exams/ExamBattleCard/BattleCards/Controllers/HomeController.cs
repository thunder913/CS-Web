using SUS.HTTP;
using SUS.MvcFramework;

namespace BattleCards.Controllers
{

    public class HomeController : Controller
    { 
        [HttpGet("/")]
        public HttpResponse Index()
        {
            if (IsUserSignedIn())
            {
                return this.Redirect("/Cards/All");
            }

            return this.View();
        }

        [HttpGet("/Logout")]
        public HttpResponse Logout()
        {
            if (!IsUserSignedIn())
            {
                return this.Error("You must be logged in to access this page!");
            }
            SignOut();
            return this.Redirect("/");
        }
    }
}