using System.Collections.Generic;
using Game.CardSystem;

namespace Game.Character
{
    /// <summary>
    /// 这个类是战斗里卡牌的管理器（与战斗外的不同）
    /// </summary>
    public class BattleCardManager
    {
        public List<Card> DrawPile { get; } = new();
        public List<Card> HandCards { get; } = new();
        public List<Card> DiscardPile { get; } = new();

        //add a card to draw pile
        public void AddToDrawPile(Card card)
        {
            DrawPile.Add(card);
        }

        public void DrawCard()
        {
            AddToHandCards(RemoveFromDrawPile());
        }

        public void AddToDiscardPile(Card card)
        {
            DiscardPile.Add(card);
        }

        //remove a card from draw pile
        private Card RemoveFromDrawPile()
        {
            Card card = DrawPile[0];
            DrawPile.RemoveAt(0);
            return card;
        }

        private void AddToHandCards(Card card)
        {
            HandCards.Add(card);
        }

        public Card RemoveFromDiscardPile()
        {
            Card card = DiscardPile[0];
            DiscardPile.RemoveAt(0);
            return card;
        }

        public void ResetDrawPile()
        {
            DrawPile.AddRange(DiscardPile);
            DiscardPile.Clear();
            UtilTools.Shuffle<Card>(DrawPile);
        }
    }
}