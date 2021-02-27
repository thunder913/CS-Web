using BattleCards.Data;
using BattleCards.Models;
using BattleCards.ViewModels;
using System.Collections.Generic;
using System.Linq;

namespace BattleCards.Services
{
    public class CardsService : ICardsService
    {
        private ApplicationDbContext db;

        public CardsService(ApplicationDbContext dbContext)
        {
            this.db = dbContext;
        }

        public void AddCard(string name, string url, string keyword, int attack, int health, string description)
        {
            db.Cards.Add(new Card()
            {
                Name = name,
                ImageUrl = url,
                Keyword = keyword,
                Attack = attack,
                Health = health,
                Description = description
            });
            db.SaveChanges();

        }

        public void AddToCollection(string userId, string cardId)
        {
            db.UsersCards.Add(new UserCard() 
            {
                UserId=userId,
                CardId=cardId
            });
            db.SaveChanges();
        }

        public bool AlreadyInCollection(string userId, string cardId)
        {
            return db.UsersCards.Any(x => x.CardId == cardId && x.UserId == userId);
        }

        public ICollection<CardViewModel> GetCards(string userId = null)
        {
            if (userId==null)
            {
                return this.db
                            .Cards
                            .Select(x => new CardViewModel()
                            {
                                CardId = x.Id,
                                Attack = x.Attack,
                                Health = x.Health,
                                Description = x.Description,
                                ImageUrl = x.ImageUrl,
                                Keyword = x.Keyword,
                                Name = x.Name
                            })
                            .ToArray();
            }
            return this.db
                .UsersCards
                .Where(x=>x.UserId==userId)
                .Select(x => new CardViewModel()
                {
                    CardId = x.Card.Id,
                    Attack = x.Card.Attack,
                    Health = x.Card.Health,
                    Description = x.Card.Description,
                    ImageUrl = x.Card.ImageUrl,
                    Keyword = x.Card.Keyword,
                    Name = x.Card.Name
                })
                .ToArray();
        }

        public void RemoveFromCollection(string userId, string cardId)
        {
            var userCard = db.UsersCards.Where(x => x.UserId == userId && x.CardId == cardId).First();
            db.UsersCards.Remove(userCard);
            db.SaveChanges();
        }
    }
}
