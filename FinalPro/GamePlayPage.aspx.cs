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
            if (Globals.GlobalAnalysis.getPlayerUsernames().Count >= 1)
                PlayerOneLB.Text = Globals.GlobalAnalysis.getPlayerUsernames()[0];
            if (Globals.GlobalAnalysis.getPlayerUsernames().Count >= 2)
                PlayerTwoLB.Text = Globals.GlobalAnalysis.getPlayerUsernames()[1];
            if (Globals.GlobalAnalysis.getPlayerUsernames().Count >= 3)
                PlayerThreeLB.Text = Globals.GlobalAnalysis.getPlayerUsernames()[2];
            if (Globals.GlobalAnalysis.getPlayerUsernames().Count >= 4)
                PlayerFourLB.Text = Globals.GlobalAnalysis.getPlayerUsernames()[3];
            if (Globals.GlobalAnalysis.getPlayerUsernames().Count >= 5)
                PlayerFiveLB.Text = Globals.GlobalAnalysis.getPlayerUsernames()[4];

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString);
            SqlDataReader myReader;
            conn.Open();
            string getActions = "SELECT Player, ActionType, Action FROM ActionDataTable";
            SqlCommand com = new SqlCommand(getActions, conn);
            myReader = com.ExecuteReader();

            if (myReader.HasRows)
            {
                while (myReader.Read())
                {
                    int playerId = myReader.GetInt32(0);
                    string getUsername = "SELECT Username FROM UserDataTable WHERE Id = " + playerId;
                    SqlCommand com1 = new SqlCommand(getUsername, conn);
                    string playerUsername = Convert.ToString(com1.ExecuteScalar().ToString());
                    ListItem item = new ListItem();
                    item.Text = playerUsername + ", " + myReader.GetString(1) + ", " + myReader.GetString(2);
                    ActionListBox.Items.Add(item);
                }
            }
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