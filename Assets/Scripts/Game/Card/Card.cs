using Common;
using Models;

namespace Game.Card
{
    public class Card : IPrototype<Card>
    {
        public string CardId { get; protected set; }
        public string SkillId { get; protected set; }
        public int CardInstanceId { get; protected set; }
        public string CardName { get; protected set; }
        public string Description { get; protected set; }
        public CardTypeEnum CardType { get; protected set; }
        public string UpgradedCardID { get; set; }
        public Card(CardData cardData)
        {
            CardId = cardData.ID;
            SkillId = cardData.SkillID;
            CardName = cardData.Name;
            Description = cardData.Description;
            CardType = cardData.GetCardTypeEnum();
            UpgradedCardID = cardData.UpgradedCardID;
            CardInstanceId = IoCContainer.GetService<IInstanceIdService>().Next(IdCategoryEnum.Card);
        }
        protected Card(string cardId, string skillId, string name, string des, CardTypeEnum cardType)
        {
            CardId = cardId;
            SkillId = skillId;
            CardInstanceId = IoCContainer.GetService<IInstanceIdService>().Next(IdCategoryEnum.Card);
            CardName = name;
            Description = des;
            CardType = cardType;
        }

        protected Card(Card other)
        {
            CardId = other.CardId;
            SkillId = other.SkillId;
            CardInstanceId = IoCContainer.GetService<IInstanceIdService>().Next(IdCategoryEnum.Card);
            CardName = other.CardName;
            Description = other.Description;
            CardType = other.CardType;
        }

        public Card Clone()
        {
            return new Card(this);
        }
    }


}