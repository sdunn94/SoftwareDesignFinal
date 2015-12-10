using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;
using System.Data.OleDb;

namespace FinalPro
{
    public partial class GamePlayPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < Globals.GlobalAnalysis.getPlayerUsernames().Count(); i++)
            {
                if (Globals.GlobalAnalysis.getPlayerCardCounts()[i] == 0)
                {
                    Globals.GlobalAnalysis.addActions(Globals.GlobalAnalysis.getPlayerUsernames()[i] + " : removed from game");
                    Globals.GlobalAnalysis.getPlayerUsernames().RemoveAt(i);
                    Globals.GlobalAnalysis.getPlayerCardCounts().RemoveAt(i);
                    i--;
                }
            }

            string[] id = Session["New"].ToString().Split(':');

            if (Globals.GlobalAnalysis.getPlayerUsernames().Count() == 1)
            {
                //that player is the winner
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString);
                conn.Open();
                string updateWinner = "UPDATE GameDataTable SET winner = '" + Globals.GlobalAnalysis.getPlayerUsernames()[0] + "' WHERE Id = '" + Int32.Parse(id[1]) + "';";
                SqlCommand com = new SqlCommand(updateWinner, conn);
                com.ExecuteNonQuery();
                conn.Close();
                //end game
                AddActionButton.Enabled = false;
                Label6.Visible = true;
                Label6.Text = "The Game is Over and the winner is: " + Globals.GlobalAnalysis.getPlayerUsernames()[0];
                Button1.Visible = true;
            }

            if (Globals.GlobalAnalysis.getPlayerUsernames().Count >= 1 && Globals.GlobalAnalysis.getPlayerUsernames()[0] != id[0])
                PlayerOneLB.Text = Globals.GlobalAnalysis.getPlayerUsernames()[0] + " " + Globals.GlobalAnalysis.getPlayerCardCounts()[0];
            if (Globals.GlobalAnalysis.getPlayerUsernames().Count >= 2 && Globals.GlobalAnalysis.getPlayerUsernames()[1] != id[0])
                PlayerTwoLB.Text = Globals.GlobalAnalysis.getPlayerUsernames()[1] + " " + Globals.GlobalAnalysis.getPlayerCardCounts()[1];
            if (Globals.GlobalAnalysis.getPlayerUsernames().Count >= 3 && Globals.GlobalAnalysis.getPlayerUsernames()[2] != id[0])
                PlayerThreeLB.Text = Globals.GlobalAnalysis.getPlayerUsernames()[2] + " " + Globals.GlobalAnalysis.getPlayerCardCounts()[2];
            if (Globals.GlobalAnalysis.getPlayerUsernames().Count >= 4 && Globals.GlobalAnalysis.getPlayerUsernames()[3] != id[0])
                PlayerFourLB.Text = Globals.GlobalAnalysis.getPlayerUsernames()[3] + " " + Globals.GlobalAnalysis.getPlayerCardCounts()[3];
            if (Globals.GlobalAnalysis.getPlayerUsernames().Count >= 5 && Globals.GlobalAnalysis.getPlayerUsernames()[4] != id[0])
                PlayerFiveLB.Text = Globals.GlobalAnalysis.getPlayerUsernames()[4] + " " + Globals.GlobalAnalysis.getPlayerCardCounts()[4];

            ActionListBox.Items.Clear();
            foreach(string action in Globals.GlobalAnalysis.getActions())
            {
                ActionListBox.Items.Add(action);
            }

            Label1.Text = Globals.DukeCounter.ToString();
            Label2.Text = Globals.ContessaCounter.ToString();
            Label3.Text = Globals.CaptainCounter.ToString();
            Label4.Text = Globals.AssassinCounter.ToString();
            Label5.Text = Globals.AmbassadorCounter.ToString();
            NumYourCardsLB.Text = "";
            foreach(Card card in Globals.GlobalAnalysis.getCardsInHand())
            {
                NumYourCardsLB.Text += card.getCardType() + " ";
            }

            StatsLabel.Text = "Chance that everyone is telling the truth: " + Globals.Stats.ToString() + "%";

            if(Globals.GlobalAnalysis.getPlayerUsernames().Count > 0)
            {
                for(int i = 0; i < Globals.GlobalAnalysis.getPossibleCard(Globals.GlobalAnalysis.getPlayerUsernames()[0]).Count; i++)
                {
                    PotentialCards1.Text += Globals.GlobalAnalysis.getPossibleCard(Globals.GlobalAnalysis.getPlayerUsernames()[0])[i].T + " ";
                }
            }
            if (Globals.GlobalAnalysis.getPlayerUsernames().Count > 1)
            {
                for (int i = 0; i < Globals.GlobalAnalysis.getPossibleCard(Globals.GlobalAnalysis.getPlayerUsernames()[1]).Count; i++)
                {
                    PotentialCards2.Text += Globals.GlobalAnalysis.getPossibleCard(Globals.GlobalAnalysis.getPlayerUsernames()[1])[i].T + " ";
                }
            }
            if (Globals.GlobalAnalysis.getPlayerUsernames().Count > 2)
            {
                for (int i = 0; i < Globals.GlobalAnalysis.getPossibleCard(Globals.GlobalAnalysis.getPlayerUsernames()[2]).Count; i++)
                {
                    PotentialCards3.Text += Globals.GlobalAnalysis.getPossibleCard(Globals.GlobalAnalysis.getPlayerUsernames()[2])[i].T + " ";
                }
            }
            if (Globals.GlobalAnalysis.getPlayerUsernames().Count > 3)
            {
                for (int i = 0; i < Globals.GlobalAnalysis.getPossibleCard(Globals.GlobalAnalysis.getPlayerUsernames()[3]).Count; i++)
                {
                    PotentialCards4.Text += Globals.GlobalAnalysis.getPossibleCard(Globals.GlobalAnalysis.getPlayerUsernames()[3])[i].T + " ";
                }
            }
            if (Globals.GlobalAnalysis.getPlayerUsernames().Count > 4)
            {
                for (int i = 0; i < Globals.GlobalAnalysis.getPossibleCard(Globals.GlobalAnalysis.getPlayerUsernames()[4]).Count; i++)
                {
                    PotentialCards5.Text += Globals.GlobalAnalysis.getPossibleCard(Globals.GlobalAnalysis.getPlayerUsernames()[4])[i].T + " ";
                }
            }
        }

        protected void AddActionButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddActionPage.aspx");
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Response.Redirect("MainMenuPage.aspx");
        }
    }
}