using BattleCards.Services;
using SUS.HTTP;
using SUS.MvcFramework;

namespace BattleCards.Controllers
{
    public class CardsController : Controller
    {
        private ICardsService cardsService;

        public CardsController(ICardsService cards)
        { 
            this.cardsService = cards;
        }
        public HttpResponse All()
        {
            if (!IsUserSignedIn())
            {
                return this.Error("You must be logged in to access this page!");
            }
            var users = cardsService.GetCards();
            return this.View(users);
        }
        public HttpResponse Collection()
        {
            if (!IsUserSignedIn())
            {
                return this.Error("You must be logged in to access this page!");
            }
            var userId = GetUserId();
            var cards = cardsService.GetCards(userId);
            return this.View(cards);
        }

        public HttpResponse Add()
        {
            if (!IsUserSignedIn())
            {
                return this.Error("You must be logged in to access this page!");
            }
            return this.View();
        }

        [HttpPost]
        public HttpResponse Add(string name, string image, string keyword, int attack, int health, string description)
        {
            if (!IsUserSignedIn())
            {
                return this.Error("You must be logged in to access this page!");
            }
            if (name.Length<5||name.Length>15)
            {
                return this.Error("The card name must be between 5 and 15 characters long!");
            }
            if (attack<0)
            {
                return this.Error("The attack must be a positive number!");
            }
            if (health<0)
            {
                return this.Error("The health must be a positive number!");
            }
            if (description.Length>200)
            {
                return this.Error("The description length must be under 200 characters long!");
            }

            cardsService.AddCard(name, image, keyword, attack, health, description);
            return this.Redirect("/Cards/All");
        }

        public HttpResponse AddToCollection(string cardId)
        {
            if (!IsUserSignedIn())
            {
                return this.Error("You must be logged in to access this page!");
            }
            if (cardsService.AlreadyInCollection(GetUserId(),cardId))
            {
                return this.Error("Item is already in collection!");
            }

            cardsService.AddToCollection(GetUserId(), cardId);
            return this.Redirect("/Cards/All");
        }

        public HttpResponse RemoveFromCollection(string cardId)
        {
            if (!IsUserSignedIn())
            {
                return this.Error("You must be logged in to access this page!");
            }
            cardsService.RemoveFromCollection(GetUserId(), cardId);
            return this.Redirect("/Cards/Collection");
        }
    }
}
