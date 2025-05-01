using Game.Card;
using Models;

namespace Common
{
    public interface ICardFactory
    {
        Card CreateCard(CardData cardData);
        Card CloneCard(CardData cardData);
    }

    [AutoRegisterFactory(typeof(ICardFactory))]
    public class CardFactory : ICardFactory
    {
        public Card CloneCard(CardData cardData)
        {
            throw new System.NotImplementedException();
        }

        public Card CreateCard(CardData cardData)
        {
            return new Card(cardData);
        }
    }
}