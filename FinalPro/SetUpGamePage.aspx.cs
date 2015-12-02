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
            int numPlayers = 0;
            if (PlayerOneDD.SelectedValue != "1")
            {
                numPlayers++;
                Globals.GlobalAnalysis.addPlayerUsername(PlayerOneDD.SelectedItem.Text);
            }
            if (PlayerTwoDD.SelectedValue != "1")
            {
                numPlayers++;
                Globals.GlobalAnalysis.addPlayerUsername(PlayerTwoDD.SelectedItem.Text);
            }
            if (PlayerThreeDD.SelectedValue != "1")
            {
                numPlayers++;
                Globals.GlobalAnalysis.addPlayerUsername(PlayerThreeDD.SelectedItem.Text);
            }
            if (PlayerFourDD.SelectedValue != "1")
            {
                numPlayers++;
                Globals.GlobalAnalysis.addPlayerUsername(PlayerFourDD.SelectedItem.Text);
            }
            if (PlayerFiveDD.SelectedValue != "1")
            {
                numPlayers++;
                Globals.GlobalAnalysis.addPlayerUsername(PlayerFiveDD.SelectedItem.Text);
            }

            if (numPlayers > 0)
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
                conn.Close();

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