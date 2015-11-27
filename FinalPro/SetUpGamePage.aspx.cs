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

        }

        protected void StartGameButton_Click(object sender, EventArgs e)
        {
            int numPlayers = 0;
            if (PlayerOneDD.SelectedValue != "1")
                numPlayers++;
            if (PlayerTwoDD.SelectedValue != "1") 
                numPlayers++;
            if (PlayerThreeDD.SelectedValue != "1") 
                numPlayers++;
            if (PlayerFourDD.SelectedValue != "1") 
                numPlayers++;
            if (PlayerFiveDD.SelectedValue != "1") 
                numPlayers++;

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

            Response.Redirect("GamePlayPage.aspx");
        }
    }
}