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
    public partial class AddActionPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                PlayerDD.Items.Clear();
                PlayerDD.Items.Add("Select Player");
                PlayerDD.Items.Add(Session["New"].ToString());
                foreach (string username in Globals.GlobalAnalysis.getPlayerUsernames())
                {
                    PlayerDD.Items.Add(username);
                }
            }
        }

        protected void ActionTypeDD_SelectedIndexChanged(object sender, EventArgs e)
        {
            ActionDD.Items.Clear();
            if (ActionTypeDD.SelectedItem.Text == "Action")
            {
                ActionDD.Items.Add("Select Action");
                ActionDD.Items.Add("Income");
                ActionDD.Items.Add("Foreign Aid");
                ActionDD.Items.Add("Coup");
                ActionDD.Items.Add("Tax");
                ActionDD.Items.Add("Assassinate");
                ActionDD.Items.Add("Exchange");
                ActionDD.Items.Add("Steal");
            }
            else if (ActionTypeDD.SelectedItem.Text == "Counteraction")
            {
                ActionDD.Items.Add("Select Action");
                ActionDD.Items.Add("Block Foreign Aid");
                ActionDD.Items.Add("Block Stealing");
                ActionDD.Items.Add("Block Assassination");
            }
            else
            {
                ActionDD.Items.Add("Select Action");
                ActionDD.Items.Add("Challenge Tax");
                ActionDD.Items.Add("Challenge Block Foreign Aid");
                ActionDD.Items.Add("Challenge Assassinate");
                ActionDD.Items.Add("Challenge Exchange");
                ActionDD.Items.Add("Challenge Block Stealing");
                ActionDD.Items.Add("Challenge Stealing");
                ActionDD.Items.Add("Challenge Block Assassination");
            }
        }

        protected void AddButton_Click(object sender, EventArgs e)
        {
            string playerUsername = PlayerDD.SelectedItem.Text.ToString();
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString);
            conn.Open();
            string getUserId = "SELECT Id FROM UserDataTable WHERE Username = '" + playerUsername + "'";
            SqlCommand com1 = new SqlCommand(getUserId, conn);
            int playerId = Convert.ToInt32(com1.ExecuteScalar().ToString());

            string insertAction = "INSERT INTO ActionDataTable (Player, ActionType, Action) VALUES (@id, @at, @a)";
            SqlCommand com = new SqlCommand(insertAction, conn);
            com.Parameters.AddWithValue("@id", playerId);
            com.Parameters.AddWithValue("@at", ActionTypeDD.SelectedItem.Text);
            com.Parameters.AddWithValue("@a", ActionDD.SelectedItem.Text);
            com.ExecuteNonQuery();
            conn.Close();

            if(ActionDD.SelectedItem.Text == "Foreign Aid")
            {
                //is there a block
                Label1.Visible = true;
                Label1.Text = "Did someone block foreign aid?";
                YesButton.Visible = true;
                NoButton.Visible = true;
            }
            if(ActionDD.SelectedItem.Text == "Coup")
            {
                //which card did they get rid of
            }
            if(ActionDD.SelectedItem.Text == "Tax")
            {
                //did someone challenge this
                Label1.Visible = true;
                Label1.Text = "Did someone challenge tax?";
                YesButton.Visible = true;
                NoButton.Visible = true;
            }
            if(ActionDD.SelectedItem.Text == "Block Foreign Aid")
            {
                //did someone challenge this
                Label1.Visible = true;
                Label1.Text = "Did someone challenge blocking of foreign aid?";
                YesButton.Visible = true;
                NoButton.Visible = true;
            }
            if(ActionDD.SelectedItem.Text == "Assassinate")
            {
                //did someone block or challenge (if no one challenges the person being assassinated loses a card)
                Label1.Visible = true;
                Label1.Text = "Did someone challenge or block assassination?";
                YesButton.Visible = true;
                AssassinationNoButton.Visible = true;
                //NoButton.Visible = true; different button because different functionality
            }
            if(ActionDD.SelectedItem.Text == "Exchange")
            {
                //did someone challenge this
                Label1.Visible = true;
                Label1.Text = "Did someone challenge the exchange?";
                YesButton.Visible = true;
                NoButton.Visible = true;
            }
            if(ActionDD.SelectedItem.Text == "Block Stealing")
            {
                //did someone challenge this
                Label1.Visible = true;
                Label1.Text = "Did someone challenge blocking a steal?";
                YesButton.Visible = true;
                NoButton.Visible = true;
            }
            if(ActionDD.SelectedItem.Text == "Block Assassination")
            {
                //did someone challenge this
                Label1.Visible = true;
                Label1.Text = "Did someone challenge blocking the assassination?";
                YesButton.Visible = true;
                NoButton.Visible = true;
            }
            if(ActionDD.SelectedItem.Text == "Challenge Tax")
            {
                //if challenger wins opponent loses a card
                //if opponent wins challenger loses a card
            }
            if (ActionDD.SelectedItem.Text == "Challenge Block Foreign Aid")
            {
                //if challenger wins opponent loses a card
                //if opponent wins challenger loses a card
            }
            if (ActionDD.SelectedItem.Text == "Challenge Assassinate")
            {
                //if challenger wins opponent loses a card unless it is the person being assassinated they lose two cards (if they have 2)
                //if opponent wins challenger loses a card
            }
            if (ActionDD.SelectedItem.Text == "Challenge Exchange")
            {
                //if challenger wins opponent loses a card
                //if opponent wins challenger loses a card
            }
            if (ActionDD.SelectedItem.Text == "Challenge Block Stealing")
            {
                //if challenger wins opponent loses a card
                //if opponent wins challenger loses a card
            }
            if (ActionDD.SelectedItem.Text == "Challenge Stealing")
            {
                //if challenger wins opponent loses a card
                //if opponent wins challenger loses a card
            }
            if (ActionDD.SelectedItem.Text == "Challenge Block Assassination")
            {
                //if challenger wins opponent loses a card and person being assassinated loses a card
                //if opponent wins challenger loses a card
            }
            
            //Response.Redirect("GamePlayPage.aspx");
        } 

        protected void YesButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddActionPage.aspx"); //is there a good way to automatically select counteraction and block foreign aid
        }

        protected void NoButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("GamePlayPage.aspx");
        }

        protected void AssassinationNoButton_Click(object sender, EventArgs e)
        {
            //show drop down for who loses a card and what card
        }
    }
}