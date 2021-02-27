using BattleCards.Models;
using BattleCards.ViewModels;
using System.Collections.Generic;

namespace BattleCards.Services
{
    public interface ICardsService
    {
        public void AddCard(string name, string url, string keyword,int attack, int health, string description);

        public ICollection<CardViewModel> GetCards(string userId=null);

        public bool AlreadyInCollection(string userId, string cardId);

        public void AddToCollection(string userId, string cardId);

        public void RemoveFromCollection(string userId, string cardId);
    }
}
