using Common;
using Game.Battle;
using Game.Character;
using Game.CardSystem;
using Managers;
using Models;
using Common.Factories;
using System.Collections.Generic;

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
        public List<string> EffectIds { get; set; } = new();
        public List<string> BuffIds { get; set; } = new();
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
            BuffIds = cardData.BuffIDs;
            EffectIds = cardData.EffectIDs;
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

        public void PlayCard(BattleContext context)
        {

            var effectFactory = IoCContainer.GetService<IEffectFactory>();
            List<EffectBase> effects = effectFactory.CreateSkillEffects(EffectIds);
            if (effects != null)
            {
                foreach (var e in effects)
                {
                    e.ApplyEffect(null, null);
                }
            }

            var buffFactory = IoCContainer.GetService<IBuffFactory>();
            List<BuffBase> buffs = buffFactory.CreateBuff(BuffIds);

            if (buffs != null)
            {
                foreach (var b in buffs)
                {
                    // b.
                }
            }

        }
    }


}