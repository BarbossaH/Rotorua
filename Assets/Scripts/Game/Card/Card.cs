using Common;
using Game.Character;
using Game.SkillSystem;
using Managers;
using Models;

namespace Game.CardSystem
{
    public class Card : IPrototype<Card>
    {
        public string CardId { get; private set; }
        public string SkillId { get; private set; }
        public int CardLevel { get; private set; }
        public int Cost { get; set; }
        public int CardInstanceId { get; private set; }
        public string Name { get; private set; }
        public string Description { get; private set; }
        public CardTypeEnum CardType { get; private set; }
        public CardSourceEnum CardSource { get; private set; }
        public CardLifecycleEnum CardLifecycle { get; private set; }
        public CardOwnershipEnum CardOwnership { get; private set; }
        public string UpgradedCardID { get; private set; }
        public Card(CardData cardData)
        {
            CardId = cardData.ID;
            SkillId = cardData.SkillID;
            Cost = cardData.Cost;
            Name = cardData.Name;
            Description = cardData.Description;
            CardType = cardData.GetCardType();
            CardSource = cardData.GetCardSource();
            CardLifecycle = cardData.GetCardLifecycle();
            CardOwnership = cardData.GetCardOwnership();
            UpgradedCardID = cardData.UpgradedCardID;
            CardInstanceId = IoCContainer.GetService<IInstanceIdService>().Next(IdCategoryEnum.Card);
        }

        protected Card(string cardId, string skillId, int cost, string name, string upgradedID, string des,
        CardTypeEnum cardType, CardOwnershipEnum cardOwnership, CardLifecycleEnum cardLifecycle, CardSourceEnum cardSource)
        {
            CardId = cardId;
            SkillId = skillId;
            CardInstanceId = IoCContainer.GetService<IInstanceIdService>().Next(IdCategoryEnum.Card);
            Name = name;
            Description = des;
            CardType = cardType;
            Cost = cost;
            UpgradedCardID = upgradedID;
            CardOwnership = cardOwnership;
            CardLifecycle = cardLifecycle;
            CardSource = cardSource;
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
            UpgradedCardID = other.UpgradedCardID;
            CardOwnership = other.CardOwnership;
            CardLifecycle = other.CardLifecycle;
            CardSource = other.CardSource;
        }

        public Card Clone()
        {
            return new Card(this);
        }

        public void ApplySkill(CharacterBase caster, CharacterBase target)
        {
            var skill = SkillManager.Instance.CreateSkill(CardId, CardLevel);
            skill.ApplySkill(caster, target);
        }
    }


}