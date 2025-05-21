using Common;
using Common.Factories;
using Game.CardSystem;

namespace Managers
{
    // using 

    /// <summary>
    /// 该类是管理战斗外的卡牌的类，战斗类的卡牌是需要从这里克隆数据的
    /// </summary>

    [AutoRegisterManager]
    public class CardManager : Singleton<CardManager>
    {
        private readonly ICardFactory _cardFactory;

        public CardManager()
        {
            _cardFactory = IoCContainer.GetService<ICardFactory>();
        }
        public Card CreateCard(string cardId)
        {
            return _cardFactory.CreateCard(cardId);
        }

        public Card CloneCard(Card original)
        {
            return _cardFactory.CloneCard(original);
        }
    }
}