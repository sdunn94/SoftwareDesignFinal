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
            string[] id = Session["New"].ToString().Split(':');
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
        }

        protected void SwapCardsButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("SwapCardsInHand.aspx");
        }

        protected void AddDeadCardButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddCard.aspx");
        }

        protected void AddActionButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddActionPage.aspx");
        }
    }
}