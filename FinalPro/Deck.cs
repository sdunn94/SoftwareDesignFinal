using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Security.Cryptography;

namespace FinalPro
{
    public class Deck
    {
        private List <Card> cards = new List <Card>();
        
        public Deck()
        {
            for(int i = 0; i < 3; i++)
            {
                cards.Add(new Card("Assassin"));
                cards.Add(new Card("Contessa"));
                cards.Add(new Card("Duke"));
                cards.Add(new Card("Captain"));
                cards.Add(new Card("Ambassador"));
            }
        }

        public List <Card> getCards()
        {
            return cards;
        }

        public void shuffle()
        {
            RNGCryptoServiceProvider random = new RNGCryptoServiceProvider();
            Card[] templist = cards.OrderBy(x => GetNextInt32(random)).ToArray();
            int i = 0;
            foreach(Card c in templist)
            {
                cards[i] = c;
                i++;
            }
        }

        private int GetNextInt32(RNGCryptoServiceProvider rnd)
        {
            byte[] randomInt = new byte[4];
            rnd.GetBytes(randomInt);
            return Convert.ToInt32(randomInt[0]);
        }

        public void dealHand(List<Card> hand, int numCards)
        {
            if (cards.Count >= 2)
            {
                if (numCards == 2)
                {
                    hand.Add(cards[0]);
                    hand.Add(cards[1]);

                    cards.RemoveAt(0);
                    cards.RemoveAt(0);
                }
                else if(numCards == 1)
                {
                    hand.Add(cards[0]);

                    cards.RemoveAt(0);
                }
            }
        }

    }
}