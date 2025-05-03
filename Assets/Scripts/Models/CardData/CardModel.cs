using System;
using System.Collections.Generic;

namespace Models
{
    public class CardJsonConfig : IGameData
    {
        public List<CardData> Cards;

        public string GetKey()
        {
            return typeof(CardJsonConfig).Name;
        }
    }
    public class CardData
    {
        public string ID { get; set; }
        public string SkillID { get; set; }
        public int Cost { get; set; }
        public string UpgradedCardID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Type { get; set; }
        public string Source { get; set; }
        public CardTypeEnum GetCardTypeEnum() => Enum.TryParse(Type, out CardTypeEnum result) ? result : CardTypeEnum.None;
        public CardSourceEnum GetCardSourceEnum() => Enum.TryParse(Source, out CardSourceEnum result) ? result : CardSourceEnum.None;
    }
}