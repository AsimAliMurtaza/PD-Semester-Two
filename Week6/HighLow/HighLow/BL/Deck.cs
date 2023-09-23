using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace HighLow.BL
{
    internal class Deck
    {
        private int count;
        private Random random = new Random();
        List<Card> cards = new List<Card>();

        public Deck()
        {
            for (int x = 1; x <= 13; x++)
            {
                for (int y = 1; y <= 4; y++)
                {
                    Card card = new Card(x, y);
                    cards.Add(card);
                }
            }
        }

        public void popOffCard()
        {
            cards.RemoveAt(0);
        }
        public void shuffle()
        {
            Card temp;
            for(int i = 0; i < cards.Count; i++) 
            {
                int num = random.Next(0, cards.Count);
                temp = cards[num];
                cards[num] = cards[i];
                cards[i] = temp;
            }
            count = 52;
        }

        public int cardsLeft()
        {
            return cards.Count;
        }

        public Card dealCard()
        {
            if(cards.Count > 0)
            {
                count--;
                return cards[0];
            }
            else
            {
                return null; 
            }
        }
        public string check()
        {
            if (cards[0].getValue() <= cards[1].getValue())
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }
    }
}
