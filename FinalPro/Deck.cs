using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalPro
{
    public class Deck
    {
        private List <Card> cards = new List <Card>();

        public Deck()
        {
            for(int i = 0; i < 3; i++)
            {
                cards.Add(new Card("Duke"));
                cards.Add(new Card("Assassin"));
                cards.Add(new Card("Contessa"));
                cards.Add(new Card("Captian"));
                cards.Add(new Card("Ambassador"));
            }
        }

        public List <Card> getCards()
        {
            return cards;
        }

        public void shuffle()
        {
            for(int i = 0; i < 15; i++)
            {
                Random rnd = new Random();
                int randIndex = rnd.Next(15);

                Card temp = cards[i];
                cards[i] = cards[randIndex];
                cards[randIndex] = temp;
            }
        }
    }
}