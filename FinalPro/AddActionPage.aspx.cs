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

            Globals.GlobalAnalysis.addActions(playerUsername + ": " + ActionDD.SelectedItem.Text);

            if(ActionDD.SelectedItem.Text == "Foreign Aid")
            {
                //is there a block
                Label1.Visible = true;
                Label1.Text = "Did someone block foreign aid?";
                YesButton.Visible = true;
                NoButton.Visible = true;
            }
            else if(ActionDD.SelectedItem.Text == "Coup")
            {
                //which card did they get rid of
                Label1.Visible = true;
                Label1.Text = "Who loses a card and what card did they lose?";
                SecondPlayerDD.Visible = true;
                SecondPlayerDD.Items.Clear();
                SecondPlayerDD.Items.Add("Select Player");
                SecondPlayerDD.Items.Add(Session["New"].ToString());
                foreach (string username in Globals.GlobalAnalysis.getPlayerUsernames())
                {
                    SecondPlayerDD.Items.Add(username);
                }

                CardToLoseDD.Visible = true;
                CardToLoseDD.Items.Clear();
                populateCardsLeftDropDownList(CardToLoseDD);

                SubmitButton.Visible = true;
            }
            else if (ActionDD.SelectedItem.Text == "Tax")
            {
                //did someone challenge this
                Label1.Visible = true;
                Label1.Text = "Did someone challenge tax?";
                YesButton.Visible = true;
                NoButton.Visible = true;
            }
            else if (ActionDD.SelectedItem.Text == "Block Foreign Aid")
            {
                //did someone challenge this
                Label1.Visible = true;
                Label1.Text = "Did someone challenge blocking of foreign aid?";
                YesButton.Visible = true;
                NoButton.Visible = true;
            }
            else if (ActionDD.SelectedItem.Text == "Assassinate")
            {
                //did someone block or challenge (if no one challenges the person being assassinated loses a card)
                Label1.Visible = true;
                Label1.Text = "Did someone challenge or block assassination?";
                YesButton.Visible = true;
                NoButton.Visible = true;
            }
            else if (ActionDD.SelectedItem.Text == "Exchange")
            {
                //did someone challenge this
                Label1.Visible = true;
                Label1.Text = "Did someone challenge the exchange?";
                YesButton.Visible = true;
                NoButton.Visible = true;
            }
            else if (ActionDD.SelectedItem.Text == "Block Stealing")
            {
                //did someone challenge this
                Label1.Visible = true;
                Label1.Text = "Did someone challenge blocking a steal?";
                YesButton.Visible = true;
                NoButton.Visible = true;
            }
            else if (ActionDD.SelectedItem.Text == "Block Assassination")
            {
                //did someone challenge this
                Label1.Visible = true;
                Label1.Text = "Did someone challenge blocking the assassination?";
                YesButton.Visible = true;
                NoButton.Visible = true;
            }
            else if (ActionDD.SelectedItem.Text == "Challenge Tax")
            {
                Label1.Visible = true;
                Label1.Text = "Did the challenger win?";
                YesButton.Visible = true;
                NoButton.Visible = true;
                //if challenger wins opponent loses a card
                //if opponent wins challenger loses a card
            }
            else if (ActionDD.SelectedItem.Text == "Challenge Block Foreign Aid")
            {
                //if challenger wins opponent loses a card
                //if opponent wins challenger loses a card
            }
            else if (ActionDD.SelectedItem.Text == "Challenge Assassinate")
            {
                //if challenger wins opponent loses a card unless it is the person being assassinated they lose two cards (if they have 2)
                //if opponent wins challenger loses a card
            }
            else if (ActionDD.SelectedItem.Text == "Challenge Exchange")
            {
                //if challenger wins opponent loses a card
                //if opponent wins challenger loses a card
            }
            else if (ActionDD.SelectedItem.Text == "Challenge Block Stealing")
            {
                //if challenger wins opponent loses a card
                //if opponent wins challenger loses a card
            }
            else if (ActionDD.SelectedItem.Text == "Challenge Stealing")
            {
                //if challenger wins opponent loses a card
                //if opponent wins challenger loses a card
            }
            else if (ActionDD.SelectedItem.Text == "Challenge Block Assassination")
            {
                //if challenger wins opponent loses a card and person being assassinated loses a card
                //if opponent wins challenger loses a card
            }
            else
            {
                Response.Redirect("GamePlayPage.aspx");
            }
            
            //Response.Redirect("GamePlayPage.aspx");
        } 

        protected void YesButton_Click(object sender, EventArgs e)
        {
            if (ActionDD.SelectedItem.Text == "Challenge Tax")
            {
                //should this be added to database or just the stream
                Globals.GlobalAnalysis.addActions(PlayerDD.SelectedItem.Text + ": wins the challenge");

                Label1.Text = "What card did " + Globals.CorBPlayer + " lose?";

                SecondPlayerDD.Visible = true;
                SecondPlayerDD.Items.Clear();
                SecondPlayerDD.Items.Add(Globals.CorBPlayer);

                CardToLoseDD.Visible = true;
                CardToLoseDD.Items.Clear();
                populateCardsLeftDropDownList(CardToLoseDD);

                SubmitButton.Visible = true;
                YesButton.Visible = false;
                NoButton.Visible = false;
            }
            else
            {
                Globals.CorBPlayer = PlayerDD.SelectedItem.Text;
                Response.Redirect("AddActionPage.aspx"); //is there a good way to automatically select counteraction and block foreign aid
            }
        }

        protected void NoButton_Click(object sender, EventArgs e)
        {
            if (ActionDD.SelectedItem.Text == "Exchange" && PlayerDD.SelectedItem.Text == Session["New"].ToString())
            {
                SecondPlayerDD.Visible = true;
                SecondPlayerDD.Items.Clear();
                if(Globals.DukeCounter < 3)
                    SecondPlayerDD.Items.Add("Duke");
                if(Globals.ContessaCounter < 3)
                    SecondPlayerDD.Items.Add("Contessa");
                if(Globals.CaptainCounter < 3)
                    SecondPlayerDD.Items.Add("Captain");
                if(Globals.AssassinCounter < 3)
                    SecondPlayerDD.Items.Add("Assassin");
                if(Globals.AmbassadorCounter < 3)
                    SecondPlayerDD.Items.Add("Ambassador");

                if(Globals.GlobalAnalysis.getCardsInHand().Count == 2)
                {
                    CardToLoseDD.Visible = true;
                    CardToLoseDD.Items.Clear();
                    if (Globals.DukeCounter < 3)
                        CardToLoseDD.Items.Add("Duke");
                    if (Globals.ContessaCounter < 3)
                        CardToLoseDD.Items.Add("Contessa");
                    if (Globals.CaptainCounter < 3)
                        CardToLoseDD.Items.Add("Captain");
                    if (Globals.AssassinCounter < 3)
                        CardToLoseDD.Items.Add("Assassin");
                    if (Globals.AmbassadorCounter < 3)
                        CardToLoseDD.Items.Add("Ambassador");
                }

                SubmitButton.Visible = true;
            }
            else if(ActionDD.SelectedItem.Text == "Assassinate")
            {
                //show drop down for who loses a card and what card
                SecondPlayerDD.Visible = true;
                SecondPlayerDD.Items.Clear();
                SecondPlayerDD.Items.Add("Select Player");
                SecondPlayerDD.Items.Add(Session["New"].ToString());
                foreach (string username in Globals.GlobalAnalysis.getPlayerUsernames())
                {
                    SecondPlayerDD.Items.Add(username);
                }

                CardToLoseDD.Visible = true;
                CardToLoseDD.Items.Clear();
                populateCardsLeftDropDownList(CardToLoseDD); //possibly filter to your cards if you are the one being assassinated
                
                SubmitButton.Visible = true;
            }
            else if(ActionDD.SelectedItem.Text == "Challenge Tax")
            {
                //should this be added to the database?
                Globals.GlobalAnalysis.addActions(PlayerDD.SelectedItem.Text + ": Lost the challenge");
                Globals.GlobalAnalysis.addActions(Globals.CorBPlayer + ": Gets new card");

                Label1.Text = "What card did " + PlayerDD.SelectedItem.Text + " lose?";

                SecondPlayerDD.Visible = true;
                SecondPlayerDD.Items.Clear();
                SecondPlayerDD.Items.Add(PlayerDD.SelectedItem.Text);

                CardToLoseDD.Visible = true;
                CardToLoseDD.Items.Clear();
                populateCardsLeftDropDownList(CardToLoseDD);

                SubmitButton.Visible = true;
                YesButton.Visible = false;
                NoButton.Visible = false;
            }
            else
            {
                Response.Redirect("GamePlayPage.aspx");
            }
        }

        private void populateCardsLeftDropDownList(DropDownList list)
        {
            Card c1 = Globals.GlobalAnalysis.getCardsInHand()[0];
            Card c2 = null;
            if (Globals.GlobalAnalysis.getCardsInHand().Count == 2)
            {
                c2 = Globals.GlobalAnalysis.getCardsInHand()[1];
            }
                       
            if (!(Globals.DukeCounter == 2 && (c1.getCardType().ToString() == "Duke" || (c2 != null && c2.getCardType().ToString() == "Duke"))))
            {
                list.Items.Add("Duke");
            }
            else if (!(Globals.DukeCounter == 1 && (c1.getCardType().ToString() == "Duke" && (c2 != null && c2.getCardType().ToString() == "Duke"))))
            {
                list.Items.Add("Duke");
            }
            else if(!(Globals.DukeCounter == 3))
            {
                list.Items.Add("Duke");
            }

            if (!(Globals.CaptainCounter == 2 && (c1.getCardType().ToString() == "Captain" || (c2 != null && c2.getCardType().ToString() == "Captain"))))
            {
                list.Items.Add("Captain");
            }
            else if (!(Globals.CaptainCounter == 1 && (c1.getCardType().ToString() == "Captain" && (c2 != null && c2.getCardType().ToString() == "Captain"))))
            {
                list.Items.Add("Captain");
            }
            else if (!(Globals.CaptainCounter == 3))
            {
                list.Items.Add("Captain");
            }

            if (!(Globals.AssassinCounter == 2 && (c1.getCardType().ToString() == "Assassin" || (c2 != null && c2.getCardType().ToString() == "Assassin"))))
            {
                list.Items.Add("Assassin");
            }
            else if (!(Globals.AssassinCounter == 1 && (c1.getCardType().ToString() == "Assassin" && (c2 != null && c2.getCardType().ToString() == "Assassin"))))
            {
                list.Items.Add("Assassin");
            }
            else if (!(Globals.AssassinCounter == 3))
            {
                list.Items.Add("Assassin");
            }

            if (!(Globals.ContessaCounter == 2 && (c1.getCardType().ToString() == "Contessa" || (c2 != null && c2.getCardType().ToString() == "Contessa"))))
            {
                list.Items.Add("Contessa");
            }
            else if (!(Globals.ContessaCounter == 1 && (c1.getCardType().ToString() == "Contessa" && (c2 != null && c2.getCardType().ToString() == "Contessa"))))
            {
                list.Items.Add("Contessa");
            }
            else if (!(Globals.ContessaCounter == 3))
            {
                list.Items.Add("Contessa");
            }

            if (!(Globals.AmbassadorCounter == 2 && (c1.getCardType().ToString() == "Ambassador" || (c2 != null && c2.getCardType().ToString() == "Ambassador"))))
            {
                list.Items.Add("Ambassador");
            }
            else if (!(Globals.AmbassadorCounter == 1 && (c1.getCardType().ToString() == "Ambassador" && (c2 != null && c2.getCardType().ToString() == "Ambassador"))))
            {
                list.Items.Add("Ambassador");
            }
            else if (!(Globals.AmbassadorCounter == 3))
            {
                list.Items.Add("Ambassador");
            }  
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            if (ActionDD.SelectedItem.Text == "Coup" || ActionDD.SelectedItem.Text == "Assassinate" || ActionDD.SelectedItem.Text == "Challenge Tax")
            {
                string card = CardToLoseDD.SelectedItem.Text;
                string playerUsername = SecondPlayerDD.SelectedItem.Text.ToString();
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString);
                conn.Open();
                string getUserId = "SELECT Id FROM UserDataTable WHERE Username = '" + playerUsername + "'";
                SqlCommand com1 = new SqlCommand(getUserId, conn);
                int playerId = Convert.ToInt32(com1.ExecuteScalar().ToString());

                string insertAction = "INSERT INTO ActionDataTable (Player, ActionType, Action) VALUES (@id, @at, @a)";
                SqlCommand com = new SqlCommand(insertAction, conn);
                com.Parameters.AddWithValue("@id", playerId);
                com.Parameters.AddWithValue("@at", "Lose Card");
                com.Parameters.AddWithValue("@a", "Lost a " + card);
                com.ExecuteNonQuery();
                conn.Close();

                Globals.GlobalAnalysis.addActions(playerUsername + ": Lost the " + card);

                if (card == "Duke")
                {
                    Globals.DukeCounter++;
                }
                else if (card == "Assassin")
                {
                    Globals.AssassinCounter++;
                }
                else if (card == "Contessa")
                {
                    Globals.ContessaCounter++;
                }
                else if (card == "Captain")
                {
                    Globals.CaptainCounter++;
                }
                else if (card == "Ambassador")
                {
                    Globals.AmbassadorCounter++;
                }

                if(SecondPlayerDD.SelectedItem.Text == Session["New"].ToString())
                {
                    Card c = new Card("Duke");
                    for (int i = 0; i < Globals.GlobalAnalysis.getCardsInHand().Count; i++)
                    {
                        if (Globals.GlobalAnalysis.getCardsInHand()[i].getCardType() == CardToLoseDD.SelectedItem.Text)
                            c = Globals.GlobalAnalysis.getCardsInHand()[i];
                    }
                    Globals.GlobalAnalysis.getCardsInHand().Remove(c);
                }
                if(ActionDD.SelectedItem.Text.Contains("Challenge") && Globals.CorBPlayer == Session["New"].ToString())
                {
                    string cardToRemove = "";
                    if (ActionDD.SelectedItem.Text == "Challenge Tax")
                        cardToRemove = "Duke";
                    //else if (ActionDD.SelectedItem.Text == "Challenge Block Foreign Aid")
                    //    cardToRemove = "Duke";
                    //else if (ActionDD.SelectedItem.Text == "Challenge Assassinate")


                    //ActionDD.Items.Add("Challenge Block Foreign Aid");
                    //ActionDD.Items.Add("Challenge Assassinate");
                    //ActionDD.Items.Add("Challenge Exchange");
                    //ActionDD.Items.Add("Challenge Block Stealing");
                    //ActionDD.Items.Add("Challenge Stealing");
                    //ActionDD.Items.Add("Challenge Block Assassination");

                    for (int i = 0; i < Globals.GlobalAnalysis.getCardsInHand().Count; i++)
                    {
                        if (Globals.GlobalAnalysis.getCardsInHand()[i].getCardType() == cardToRemove)
                            Globals.GlobalAnalysis.getCardsInHand().RemoveAt(i);
                    }

                    Label1.Text = "What is your new card?";
                    SecondPlayerDD.Visible = true;
                    CardToLoseDD.Visible = false;
                    SubmitButton.Visible = false;

                    SecondPlayerDD.Items.Clear();
                    populateCardsLeftDropDownList(SecondPlayerDD);
                    
                    ChallengeSubmitButton.Visible = true;
                }

                Response.Redirect("GamePlayPage.aspx");
            }
            else if(ActionDD.SelectedItem.Text == "Exchange")
            {
                Globals.GlobalAnalysis.getCardsInHand().Clear();
                Globals.GlobalAnalysis.addCardsToHand(SecondPlayerDD.SelectedItem.Text);
                if (CardToLoseDD.Visible == true)
                    Globals.GlobalAnalysis.addCardsToHand(CardToLoseDD.SelectedItem.Text);

                Response.Redirect("GamePlayPage.aspx");
            }
        }

        protected void ChallengeSubmitButton_Click(object sender, EventArgs e)
        {
            Globals.GlobalAnalysis.addCardsToHand(SecondPlayerDD.SelectedItem.Text); //second player DD is being used to hold the new card you got after you won the challenge

        }
    }
}