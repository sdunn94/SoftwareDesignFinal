using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalPro
{
    public class Analyze
    {
        private List<Card> cardsInHand = new List<Card>();
        private List<Card> deadCards = new List<Card>();
        private List<string> playerUsernames = new List<string>();
        private List<int> playerCardCounts = new List<int>();
        private List<string> actions = new List<string>();
        //private List<Card> cardsKnownInDeck = new List<Card>();

        public Analyze()
        {

        }

        public List<Card> getCardsInHand()
        {
            return cardsInHand;
        }

        public List<Card> getDeadCards()
        {
            return deadCards;
        }

        public List<string> getPlayerUsernames()
        {
            return playerUsernames;
        }

        public List<string> getActions()
        {
            return actions;
        }

        public List<int> getPlayerCardCounts()
        {
            return playerCardCounts;
        }

        public void updatePlayerCardCount(string playerUsername, int numCards)
        {
            playerCardCounts[getPlayerUsernames().IndexOf(playerUsername)] = numCards;
        }

        public void addActions(string action)
        {
            actions.Add(action);
        }

        public void addPlayerUsername(string un)
        {
            playerUsernames.Add(un);
        }

        //public List<Card> getCardsKnownInDeck()
        //{
        //    return cardsKnownInDeck;
        //}

        public void addCardsToHand(string card)
        {
            if (cardsInHand.Count < 2)
            {
                Card c = new Card(card);
                cardsInHand.Add(c);
            }
        }

        public void addDeadCard(string card)
        {
            Card c = new Card(card);
            deadCards.Add(c);
        }

        //public void addKnownCardInDeck(string card)
        //{
        //    Card c = new Card(card);
        //    cardsKnownInDeck.Add(c);
        //}
    }
}