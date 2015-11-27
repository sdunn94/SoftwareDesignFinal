using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace FinalPro
{
    public class Card
    {
        private string cardType;
        private bool isDead;

        public Card(string t)
        {
            setCardType(t);
        }

        public void setCardType(string t)
        {
            if(t == "Duke" || t == "Assassin" || t == "Contessa" || t == "Captian" || t == "Ambassador")
            {
                cardType = t;
            }
            else
            {
                //bad things happen
            }
        }

        public string getCardType()
        {
            return cardType;
        }

        public void setIsDead(bool d)
        {
            isDead = d;
        }

        public bool getIsDead()
        {
            return isDead;
        }
    }
}