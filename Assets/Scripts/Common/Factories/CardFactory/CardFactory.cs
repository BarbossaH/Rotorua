

namespace Common.Factories
{
    using Game.CardSystem;
    using Models;
    using UnityEngine;
    public interface ICardFactory
    {
        Card CreateCard(string cardId);
        Card CloneCard(Card card);
    }

    [AutoRegisterFactory(typeof(ICardFactory))]
    public class CardFactory : ICardFactory
    {
        public Card CloneCard(Card original)
        {
            return original.Clone();
        }

        public Card CreateCard(string cardId)
        {
            var repo = IoCContainer.GetService<CardRepository>();
            if (repo == null)
            {
                Debug.LogError("card repo is null");
                return null;
            }
            CardData cardData = repo.GetById(cardId);
            return new Card(cardData);
        }
    }
}