using Common;
using Models;

namespace Game.CardSystem
{
    public class Card : IPrototype<Card>
    {
        public string CardId { get; private set; }
        public string SkillId { get; private set; }
        public int Cost { get; set; }
        public int CardInstanceId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public CardTypeEnum CardType { get; private set; }
        public string UpgradedCardID { get; private set; }
        public Card(CardData cardData)
        {
            CardId = cardData.ID;
            SkillId = cardData.SkillID;
            Cost = cardData.Cost;
            Name = cardData.Name;
            Description = cardData.Description;
            CardType = cardData.GetCardTypeEnum();
            UpgradedCardID = cardData.UpgradedCardID;
            CardInstanceId = IoCContainer.GetService<IInstanceIdService>().Next(IdCategoryEnum.Card);
        }

        protected Card(string cardId, string skillId, int cost, string name, string upgradedID, string des, CardTypeEnum cardType)
        {
            CardId = cardId;
            SkillId = skillId;
            CardInstanceId = IoCContainer.GetService<IInstanceIdService>().Next(IdCategoryEnum.Card);
            Name = name;
            Description = des;
            CardType = cardType;
            Cost = cost;
            UpgradedCardID = upgradedID;
        }

        protected Card(Card other)
        {
            CardId = other.CardId;
            SkillId = other.SkillId;
            CardInstanceId = IoCContainer.GetService<IInstanceIdService>().Next(IdCategoryEnum.Card);
            Name = other.Name;
            Description = other.Description;
            CardType = other.CardType;
            Cost = other.Cost;
        }

        public Card Clone()
        {
            return new Card(this);
        }
    }


}