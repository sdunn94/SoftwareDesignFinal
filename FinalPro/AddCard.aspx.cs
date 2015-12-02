using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalPro
{
    public partial class AddCard : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Globals.DukeCounter == 3)
            {
                DeadCardListDD.Items.Remove("Duke");
            }
            else if(Globals.AssassinCounter == 3)
            {
                DeadCardListDD.Items.Remove("Assassin");
            }
            else if(Globals.ContessaCounter == 3)
            {
                DeadCardListDD.Items.Remove("Contessa");
            }
            else if(Globals.CaptainCounter == 3)
            {
                DeadCardListDD.Items.Remove("Captain");
            }
            else if(Globals.AmbassadorCounter == 3)
            {
                DeadCardListDD.Items.Remove("Ambassador");
            }
        }

        protected void AddCardButton_Click(object sender, EventArgs e)
        {
            Card c1 = Globals.GlobalAnalysis.getCardsInHand()[0];
            Card c2 = null;
            if(Globals.GlobalAnalysis.getCardsInHand().Count == 2)
            {
                c2 = Globals.GlobalAnalysis.getCardsInHand()[1];
            }
            
            string cardName = DeadCardListDD.SelectedValue.ToString();
            if (cardName == "Duke")
            {
                if (Globals.DukeCounter == 2 && (c1.getCardType().ToString() == "Duke" || (c2 != null && c2.getCardType().ToString() == "Duke")))
                {
                    Globals.GlobalAnalysis.getCardsInHand().Remove(c1.getCardType().ToString() == "Duke" ? c1 : c2);
                }
                else if (Globals.DukeCounter == 1 && (c1.getCardType().ToString() == "Duke" && (c2 != null && c2.getCardType().ToString() == "Duke")))
                {
                    Globals.GlobalAnalysis.getCardsInHand().Remove(c1);
                }
                Globals.DukeCounter++;
            }
            else if (cardName == "Assassin")
            {
                if (Globals.AssassinCounter == 2 && (c1.getCardType().ToString() == "Assassin" || (c2 != null && c2.getCardType().ToString() == "Assassin")))
                {
                    Globals.GlobalAnalysis.getCardsInHand().Remove(c1.getCardType().ToString() == "Assassin" ? c1 : c2);
                }
                else if (Globals.AssassinCounter == 1 && (c1.getCardType().ToString() == "Assassin" && (c2 != null && c2.getCardType().ToString() == "Assassin")))
                {
                    Globals.GlobalAnalysis.getCardsInHand().Remove(c1);
                }
                Globals.AssassinCounter++;
            }
            else if (cardName == "Captain")
            {
                if (Globals.CaptainCounter == 2 && (c1.getCardType().ToString() == "Captain" || (c2 != null && c2.getCardType().ToString() == "Captain")))
                {
                    Globals.GlobalAnalysis.getCardsInHand().Remove(c1.getCardType().ToString() == "Captain" ? c1 : c2);
                }
                else if (Globals.CaptainCounter == 1 && (c1.getCardType().ToString() == "Captain" && (c2 != null && c2.getCardType().ToString() == "Captain")))
                {
                    Globals.GlobalAnalysis.getCardsInHand().Remove(c1);
                }
                Globals.CaptainCounter++;
            }
            else if (cardName == "Contessa")
            {
                if (Globals.ContessaCounter == 2 && (c1.getCardType().ToString() == "Contessa" || (c2 != null && c2.getCardType().ToString() == "Contessa")))
                {
                    Globals.GlobalAnalysis.getCardsInHand().Remove(c1.getCardType().ToString() == "Contessa" ? c1 : c2);
                }
                else if (Globals.ContessaCounter == 1 && (c1.getCardType().ToString() == "Contessa" && (c2 != null && c2.getCardType().ToString() == "Contessa")))
                {
                    Globals.GlobalAnalysis.getCardsInHand().Remove(c1);
                }
                Globals.ContessaCounter++;
            }
            else if (cardName == "Ambassador")
            {
                if (Globals.AmbassadorCounter == 2 && (c1.getCardType().ToString() == "Ambassador" || (c2 != null && c2.getCardType().ToString() == "Ambassador")))
                {
                    Globals.GlobalAnalysis.getCardsInHand().Remove(c1.getCardType().ToString() == "Ambassador" ? c1 : c2);
                }
                else if (Globals.AmbassadorCounter == 1 && (c1.getCardType().ToString() == "Ambassador" && (c2 != null && c2.getCardType().ToString() == "Ambassador")))
                {
                    Globals.GlobalAnalysis.getCardsInHand().Remove(c1);
                }
                Globals.AmbassadorCounter++;
            }
            Globals.GlobalAnalysis.addDeadCard(cardName);
            Response.Redirect("GamePlayPage.aspx");
   
        }
    }
}