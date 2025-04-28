namespace Game.Card
{
    public abstract class CardBase
    {
        public string CardId { get; }
        public int CardInstanceId { get; }
        public string CardName { get; }
        public string Description { get; }
        public CardTypeEnum cardType { get; }
    }
}