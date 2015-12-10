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

        public struct card
        {
            string type;
            bool blockSteal;

            public string T
            {
                get
                {
                    return type;
                }
                set
                {
                    type = value;
                }
            }

            public bool B
            {
                get
                {
                    return blockSteal;
                }
                set
                {
                    blockSteal = value;
                }
            }
        }

        private List<card> pOnePossibleCards = new List<card>();
        private List<card> pTwoPossibleCards = new List<card>();
        private List<card> pThreePossibleCards = new List<card>();
        private List<card> pFourPossibleCards = new List<card>();
        private List<card> pFivePossibleCards = new List<card>();

        public Analyze()
        {
            
        }

        public void addPossibleCard(string c, bool bSteal, string user)
        {
            card currCard = new card();
            currCard.T = c;
            currCard.B = bSteal;

            int index = getPlayerUsernames().IndexOf(user);

            if(index == 0 && !pOnePossibleCards.Contains(currCard))
            {
                addCard(currCard, pOnePossibleCards);
            }
            else if(index == 1 && !pTwoPossibleCards.Contains(currCard))
            {
                addCard(currCard, pTwoPossibleCards);
            }
            else if (index == 2 && !pThreePossibleCards.Contains(currCard))
            {
                addCard(currCard, pThreePossibleCards);
            }
            else if (index == 3 && !pFourPossibleCards.Contains(currCard))
            {
                addCard(currCard, pFourPossibleCards);
            }
            else if (index == 4 && !pFivePossibleCards.Contains(currCard))
            {
                addCard(currCard, pFivePossibleCards);
            }
        }

        private void addCard(card currCard, List<card> possibleCards)
        {
            if(possibleCards.Count == 0 || possibleCards.Count == 1)
            {
                possibleCards.Add(currCard);
            }
            else if(possibleCards.Count == 2)
            {
                if((possibleCards[0].B && possibleCards[1].B) || (possibleCards[1].B && currCard.B))
                {
                    possibleCards.Add(currCard);
                }
                else
                {
                    //must be lying about something!!! don't really know what we want to do here.
                    //say that there is a zero percent chance that every one is telling the truth because at the point we know that at least one person is lying
                }
            }
        }

        public List<card> getPossibleCard(string user)
        {
            if (user == getPlayerUsernames()[0])
            {
                return pOnePossibleCards;
            }
            else if (getPlayerUsernames().Count > 1 && user == getPlayerUsernames()[1])
            {
                return pTwoPossibleCards;
            }
            else if (getPlayerUsernames().Count > 2 && user == getPlayerUsernames()[2])
            {
                return pThreePossibleCards;
            }
            else if (getPlayerUsernames().Count > 3 && user == getPlayerUsernames()[3])
            {
                return pFourPossibleCards;
            }
            else if (getPlayerUsernames().Count > 4 && user == getPlayerUsernames()[4])
            {
                return pFivePossibleCards;
            }
            else
                return null;
        }

        public void calculateStatistics() 
        {
            int total = 2000;
            int count = 0;

            for (int j = 0; j < 2000; j++)
            {
                List<List<string>> hands = new List<List<string>>();
                Deck deck = new Deck();
                deleteDeadCard(deck);
                deleteUserCards(deck);
                deck.shuffle();

                for (int i = 0; i < playerUsernames.Count; i++) //only deal one card if corresponding user only has one card.
                {
                    List<Card> tempHand = new List<Card>();
                    deck.dealHand(tempHand);
                    //translate to a list of strings
                    List<string> stringHand = new List<string>();
                    stringHand.Add(tempHand[0].getCardType());
                    stringHand.Add(tempHand[1].getCardType());
                    hands.Add(stringHand);
                }
                 
                if(checkIfCardsMatch(hands))
                {
                    count++;
                }
            }

            Globals.Stats = ((float)count / (float)total) * 100.0f;
        }

        private void deleteUserCards(Deck d)
        {
            foreach(Card c in getCardsInHand())
            {
                foreach(Card c1 in d.getCards())
                {
                    if(c.getCardType() == c1.getCardType())
                    {
                        d.getCards().Remove(c1);
                        break;
                    }
                }
            }
        }

        private void deleteDeadCard(Deck d)
        {
            foreach(Card c in getDeadCards())
            {
                foreach(Card c1 in d.getCards())
                {
                    if(c.getCardType() == c1.getCardType())
                    {
                        d.getCards().Remove(c1);
                        break;
                    }
                }
            }
        }

        private bool checkIfCardsMatch(List<List<string>> hands)
        {
            bool retVal = false;
            bool player1 = false;
            bool player2 = false;
            bool player3 = false;
            bool player4 = false;
            bool player5 = false;

            player1 = checkIfHandIsGood(pOnePossibleCards, hands[0]);
            if (hands.Count > 1)
            {
                player2 = checkIfHandIsGood(pTwoPossibleCards, hands[1]);
            }
            else
            {
                player2 = true;
                player3 = true;
                player4 = true;
                player5 = true;
            }
            if(hands.Count > 2)
            { 
                player3 = checkIfHandIsGood(pThreePossibleCards, hands[2]);
            }
            else
            {
                player3 = true;
                player4 = true;
                player5 = true;
            }
            if(hands.Count > 3)
            { 
                player4 = checkIfHandIsGood(pFourPossibleCards, hands[3]);
            }
            else
            {
                player4 = true;
                player5 = true;
            }
            if(hands.Count > 4)
            { 
                player5 = checkIfHandIsGood(pFivePossibleCards, hands[4]);
            }
            else
            {
                player5 = true;
            }

            if(player1 && player2 && player3 && player4 && player5)
            {
                retVal = true;
            }

            return retVal;
        }

        private bool checkIfHandIsGood(List<card> possibleCards, List<string> list)
        {
            bool retVal = false;

            if(possibleCards.Count == 0)
            {
                retVal = true;
            }
            else if (possibleCards.Count == 1)
            {
                if (list.Contains(possibleCards[0].T))
                {
                    retVal = true;
                }
            }
            else if(possibleCards.Count == 2)
            {
                if(possibleCards[0].B && possibleCards[1].B)
                {
                    if(list.Contains(possibleCards[0].T) || list.Contains(possibleCards[1].T))
                    {
                        retVal = true;
                    }
                }
                else
                {
                    if(list.Contains(possibleCards[0].T) && list.Contains(possibleCards[1].T))
                    {
                        retVal = true;
                    }
                }
            }
            else if(possibleCards.Count == 3)
            {
                if ((possibleCards[0].B && possibleCards[1].B) || (possibleCards[1].B && possibleCards[2].B))
                {
                    if ((possibleCards[0].B && possibleCards[1].B) && ((list.Contains(possibleCards[0].T) || list.Contains(possibleCards[1].T)) && list.Contains(possibleCards[2].T))) 
                    {
                        retVal = true;
                    }
                    else if((possibleCards[1].B && possibleCards[2].B) && ((list.Contains(possibleCards[1].T) || list.Contains(possibleCards[2].T)) && list.Contains(possibleCards[0].T)))
                    {
                        retVal = true;
                    }
                }
                else
                {
                    //player is lying about something for sure, not really sure what to do here
                }
            }

            return retVal;
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

    }
}