using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HighLow.BL
{
    internal class Hand
    {
        List<Card> cards = new List<Card>();

        public void clear()
        {
            cards.Clear();
        }
        public void addCard(Card card)
        {
            cards.Add(card);
        }

        public void removeCard(Card card)
        {
            cards.Remove(card);
        }

        public void removeCard(int position)
        {
            cards.RemoveAt(position);
        }

        public int getCardCount()
        {
            return cards.Count;
        }

        public Card getCard(int position)
        {
            return cards[position];
        }
    }
}
