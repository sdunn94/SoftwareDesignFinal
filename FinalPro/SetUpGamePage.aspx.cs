using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data.SqlClient;
using System.Configuration;

namespace FinalPro
{
    public partial class SetUpGamePage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            ListItem item = PlayerOneDD.Items.FindByText(Session["New"].ToString());
            PlayerOneDD.Items.Remove(item);
            PlayerTwoDD.Items.Remove(Session["New"].ToString());
            PlayerThreeDD.Items.Remove(Session["New"].ToString());
            PlayerFourDD.Items.Remove(Session["New"].ToString());
            PlayerFiveDD.Items.Remove(Session["New"].ToString());
        }

        protected void StartGameButton_Click(object sender, EventArgs e)
        {
            Globals.GlobalAnalysis.getCardsInHand().Clear();
            Globals.GlobalAnalysis.getDeadCards().Clear();
            Globals.GlobalAnalysis.getPlayerUsernames().Clear();
            Globals.GlobalAnalysis.getActions().Clear();
            Globals.GlobalAnalysis.getPlayerCardCounts().Clear();
            Globals.GlobalAnalysis.clearAllPossibleCards();

            int numPlayers = 0;
            if (PlayerOneDD.SelectedValue != "1")
            {
                numPlayers++;
                Globals.GlobalAnalysis.addPlayerUsername(PlayerOneDD.SelectedItem.Text);
                Globals.GlobalAnalysis.getPlayerCardCounts().Add(2);
            }
            if (PlayerTwoDD.SelectedValue != "1")
            {
                if (!Globals.GlobalAnalysis.getPlayerUsernames().Contains(PlayerTwoDD.SelectedItem.Text))
                {
                    numPlayers++;
                    Globals.GlobalAnalysis.addPlayerUsername(PlayerTwoDD.SelectedItem.Text);
                    Globals.GlobalAnalysis.getPlayerCardCounts().Add(2);
                }
            }
            if (PlayerThreeDD.SelectedValue != "1")
            {
                if (!Globals.GlobalAnalysis.getPlayerUsernames().Contains(PlayerThreeDD.SelectedItem.Text))
                {
                    numPlayers++;
                    Globals.GlobalAnalysis.addPlayerUsername(PlayerThreeDD.SelectedItem.Text);
                    Globals.GlobalAnalysis.getPlayerCardCounts().Add(2);
                }
            }
            if (PlayerFourDD.SelectedValue != "1")
            {
                if (!Globals.GlobalAnalysis.getPlayerUsernames().Contains(PlayerFourDD.SelectedItem.Text))
                {
                    numPlayers++;
                    Globals.GlobalAnalysis.addPlayerUsername(PlayerFourDD.SelectedItem.Text);
                    Globals.GlobalAnalysis.getPlayerCardCounts().Add(2);
                }
            }
            if (PlayerFiveDD.SelectedValue != "1")
            {
                if (!Globals.GlobalAnalysis.getPlayerUsernames().Contains(PlayerFiveDD.SelectedItem.Text))
                {
                    numPlayers++;
                    Globals.GlobalAnalysis.addPlayerUsername(PlayerFiveDD.SelectedItem.Text);
                    Globals.GlobalAnalysis.getPlayerCardCounts().Add(2);
                }
            }

            Globals.GlobalAnalysis.addPlayerUsername(Session["New"].ToString());
            Globals.GlobalAnalysis.getPlayerCardCounts().Add(2);

            if (numPlayers > 0 && !CardOneDD.SelectedItem.Text.Contains("Select Card") && !CardTwoDD.SelectedItem.Text.Contains("Select Card"))
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString);
                conn.Open();
                string getUserId = "SELECT Id FROM UserDataTable WHERE Username = '" + Session["New"].ToString() + "'";
                SqlCommand com1 = new SqlCommand(getUserId, conn);
                int userId = Convert.ToInt32(com1.ExecuteScalar().ToString());

                string insertGame = "INSERT INTO GameDataTable (UserId, NumberOfPlayers) VALUES (@id, @np)";
                SqlCommand com = new SqlCommand(insertGame, conn);
                com.Parameters.AddWithValue("@id", userId);
                com.Parameters.AddWithValue("@np", numPlayers);
                com.ExecuteNonQuery();

                string getGameId = "SELECT MAX(Id) FROM GameDataTable";
                com = new SqlCommand(getGameId, conn);
                int gameId = Convert.ToInt32(com.ExecuteScalar().ToString());
                conn.Close();
                //add game id to session username:gameid
                Session["New"] = Session["New"].ToString() + ":" + gameId.ToString();

                Globals.GlobalAnalysis.addCardsToHand(CardOneDD.SelectedValue.ToString());
                Globals.GlobalAnalysis.addCardsToHand(CardTwoDD.SelectedValue.ToString());

                Response.Redirect("GamePlayPage.aspx");
            }
        }

        protected void AddNewPlayerButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistrationPage.aspx");
        }
    }
}