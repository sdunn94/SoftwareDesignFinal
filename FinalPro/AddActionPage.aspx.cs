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
            int playerId = getUserId(playerUsername);

            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString);
            conn.Open();
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
                HiddenDDOne.Visible = true;
                HiddenDDOne.Items.Clear();
                HiddenDDOne.Items.Add("Select Player");
                HiddenDDOne.Items.Add(Session["New"].ToString());
                foreach (string username in Globals.GlobalAnalysis.getPlayerUsernames())
                {
                    HiddenDDOne.Items.Add(username);
                }

                HiddenDDTwo.Visible = true;
                HiddenDDTwo.Items.Clear();
                populateCardsLeftDropDownList(HiddenDDTwo);

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
                Label1.Visible = true;
                Label1.Text = "Did the challenger win?";
                YesButton.Visible = true;
                NoButton.Visible = true;
                //if challenger wins opponent loses a card
                //if opponent wins challenger loses a card
            }
            else if (ActionDD.SelectedItem.Text == "Challenge Assassinate")
            {
                Label1.Visible = true;
                Label1.Text = "Is the challenger the person being assassinated?";
                YesButtonForAssassination.Visible = true;
                NoButtonForAssassination.Visible = true;
                //if challenger wins opponent loses a card unless it is the person being assassinated they lose two cards (if they have 2)
                //if opponent wins challenger loses a card
            }
            else if (ActionDD.SelectedItem.Text == "Challenge Exchange")
            {
                Label1.Visible = true;
                Label1.Text = "Did the challenger win?";
                YesButton.Visible = true;
                NoButton.Visible = true;
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
            if (ActionDD.SelectedItem.Text == "Challenge Tax" || ActionDD.SelectedItem.Text == "Challenge Block Foreign Aid"
                || ActionDD.SelectedItem.Text == "Challenge Exchange" || ActionDD.SelectedItem.Text == "Challenge Assassinate")
            {
                //should this be added to database or just the stream
                Globals.GlobalAnalysis.addActions(PlayerDD.SelectedItem.Text + ": wins the challenge");

                Label1.Text = "What card did " + Globals.CorBPlayer + " lose?";

                HiddenDDOne.Visible = true;
                HiddenDDOne.Items.Clear();
                HiddenDDOne.Items.Add(Globals.CorBPlayer);

                HiddenDDTwo.Visible = true;
                HiddenDDTwo.Items.Clear();
                populateCardsLeftDropDownList(HiddenDDTwo);

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
                HiddenDDOne.Visible = true;
                HiddenDDOne.Items.Clear();
                if(Globals.DukeCounter < 3)
                    HiddenDDOne.Items.Add("Duke");
                if(Globals.ContessaCounter < 3)
                    HiddenDDOne.Items.Add("Contessa");
                if(Globals.CaptainCounter < 3)
                    HiddenDDOne.Items.Add("Captain");
                if(Globals.AssassinCounter < 3)
                    HiddenDDOne.Items.Add("Assassin");
                if(Globals.AmbassadorCounter < 3)
                    HiddenDDOne.Items.Add("Ambassador");

                if(Globals.GlobalAnalysis.getCardsInHand().Count == 2)
                {
                    HiddenDDTwo.Visible = true;
                    HiddenDDTwo.Items.Clear();
                    if (Globals.DukeCounter < 3)
                        HiddenDDTwo.Items.Add("Duke");
                    if (Globals.ContessaCounter < 3)
                        HiddenDDTwo.Items.Add("Contessa");
                    if (Globals.CaptainCounter < 3)
                        HiddenDDTwo.Items.Add("Captain");
                    if (Globals.AssassinCounter < 3)
                        HiddenDDTwo.Items.Add("Assassin");
                    if (Globals.AmbassadorCounter < 3)
                        HiddenDDTwo.Items.Add("Ambassador");
                }

                SubmitButton.Visible = true;
            }
            else if(ActionDD.SelectedItem.Text == "Assassinate")
            {
                //show drop down for who loses a card and what card
                Label1.Text = "Who was assassinated and what card did they lose?";

                HiddenDDOne.Visible = true;
                HiddenDDOne.Items.Clear();
                HiddenDDOne.Items.Add("Select Player");
                HiddenDDOne.Items.Add(Session["New"].ToString());
                foreach (string username in Globals.GlobalAnalysis.getPlayerUsernames())
                {
                    HiddenDDOne.Items.Add(username);
                }

                HiddenDDTwo.Visible = true;
                HiddenDDTwo.Items.Clear();
                populateCardsLeftDropDownList(HiddenDDTwo); //possibly filter to your cards if you are the one being assassinated
                
                SubmitButton.Visible = true;
            }
            else if(ActionDD.SelectedItem.Text == "Challenge Tax" || ActionDD.SelectedItem.Text == "Challenge Block Foreign Aid"
                || ActionDD.SelectedItem.Text == "Challenge Exchange")
            {
                //should this be added to the database?
                Globals.GlobalAnalysis.addActions(PlayerDD.SelectedItem.Text + ": Lost the challenge");

                Label1.Text = "What card did " + PlayerDD.SelectedItem.Text + " lose?";

                HiddenDDOne.Visible = true;
                HiddenDDOne.Items.Clear();
                HiddenDDOne.Items.Add(PlayerDD.SelectedItem.Text);

                HiddenDDTwo.Visible = true;
                HiddenDDTwo.Items.Clear();
                populateCardsLeftDropDownList(HiddenDDTwo);

                SubmitButton.Visible = true;
                YesButton.Visible = false;
                NoButton.Visible = false;
            }
            else if (ActionDD.SelectedItem.Text == "Challenge Assassinate")
            {
                Globals.GlobalAnalysis.addActions(PlayerDD.SelectedItem.Text + ": Lost the challenge");
                if(Globals.AssassinatedPlayer == PlayerDD.SelectedItem.Text)
                {
                    //loses two cards
                    //if player has two cards ask what they are
                    //else if they only have one card ask what that card is
                    if(Globals.GlobalAnalysis.getPlayerCardCounts()[Globals.GlobalAnalysis.getPlayerUsernames().IndexOf(Globals.AssassinatedPlayer)] == 2)
                    {
                        Label1.Text = "What cards did " + Globals.AssassinatedPlayer + " lose?";
                        HiddenDDOne.Visible = true;
                        HiddenDDOne.Items.Clear();
                        populateCardsLeftDropDownList(HiddenDDOne);

                        HiddenDDTwo.Visible = true;
                        HiddenDDTwo.Items.Clear();
                        populateCardsLeftDropDownList(HiddenDDTwo);

                        SubmitButton.Visible = true;
                        YesButton.Visible = false;
                        NoButton.Visible = false;
                    }
                    else if (Globals.GlobalAnalysis.getPlayerCardCounts()[Globals.GlobalAnalysis.getPlayerUsernames().IndexOf(Globals.AssassinatedPlayer)] == 1)
                    {
                        Label1.Text = "What card did " + Globals.AssassinatedPlayer + " lose?";
                        HiddenDDOne.Visible = true;
                        HiddenDDOne.Items.Clear();
                        populateCardsLeftDropDownList(HiddenDDOne);

                        HiddenDDTwo.Visible = false;

                        SubmitButton.Visible = true;
                        YesButton.Visible = false;
                        NoButton.Visible = false;
                    }
                }
                else
                {
                    //loses one card and assassinated loses one card
                    //ask which cards they both lose
                    Label1.Text = "What cards did " + PlayerDD.SelectedItem.Text + " lose?";
                    AssassinatedPlayerLB.Text = "Who was assassinated and what card did they lose?";
                    HiddenDDOne.Visible = true;
                    HiddenDDOne.Items.Clear();
                    populateCardsLeftDropDownList(HiddenDDOne);

                    HiddenDDTwo.Visible = true;
                    HiddenDDTwo.Items.Clear();
                    populateCardsLeftDropDownList(HiddenDDTwo);

                    HiddenDDThree.Visible = true;
                    HiddenDDThree.Items.Clear();
                    HiddenDDThree.Items.Add("Select Player");
                    foreach (string username in Globals.GlobalAnalysis.getPlayerUsernames())
                    {
                        HiddenDDThree.Items.Add(username);
                    }

                    SubmitButton.Visible = true;
                    YesButton.Visible = false;
                    NoButton.Visible = false;
                }
                Globals.AssassinatedPlayer = Globals.AssassinatedPlayer + ":Challenger lost";
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
            if (ActionDD.SelectedItem.Text == "Coup" || ActionDD.SelectedItem.Text == "Assassinate" || ActionDD.SelectedItem.Text.Contains("Challenge"))
            {
                if(ActionDD.SelectedItem.Text == "Challenge Assassinate" && Globals.AssassinatedPlayer.Contains(':'))
                {
                    handleChallengeAssassinate();
                }
                else 
                {
                    string card = HiddenDDTwo.SelectedItem.Text;
                    string playerUsername = HiddenDDOne.SelectedItem.Text.ToString();
                    Globals.GlobalAnalysis.updatePlayerCardCount(playerUsername, Globals.GlobalAnalysis.getPlayerCardCounts()[Globals.GlobalAnalysis.getPlayerUsernames().IndexOf(playerUsername)] - 1);
                    //if new number of cards is zero do something to kick them out of the game

                    int playerId = getUserId(playerUsername);

                    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString);
                    conn.Open();
                    string insertAction = "INSERT INTO ActionDataTable (Player, ActionType, Action) VALUES (@id, @at, @a)";
                    SqlCommand com = new SqlCommand(insertAction, conn);
                    com.Parameters.AddWithValue("@id", playerId);
                    com.Parameters.AddWithValue("@at", "Lose Card");
                    com.Parameters.AddWithValue("@a", "Lost a " + card);
                    com.ExecuteNonQuery();
                    conn.Close();

                    Globals.GlobalAnalysis.addActions(playerUsername + ": Lost the " + card);

                    updateDeadCardCounters(card);


                    if (HiddenDDOne.SelectedItem.Text == Session["New"].ToString())
                    {
                        Card c = new Card("Duke");
                        for (int i = 0; i < Globals.GlobalAnalysis.getCardsInHand().Count; i++)
                        {
                            if (Globals.GlobalAnalysis.getCardsInHand()[i].getCardType() == HiddenDDTwo.SelectedItem.Text)
                                c = Globals.GlobalAnalysis.getCardsInHand()[i];
                        }
                        Globals.GlobalAnalysis.getCardsInHand().Remove(c);
                    }
                    if (ActionDD.SelectedItem.Text.Contains("Challenge") && Globals.CorBPlayer == Session["New"].ToString())
                    {
                        string cardToRemove = "";
                        if (ActionDD.SelectedItem.Text == "Challenge Tax")
                            cardToRemove = "Duke";
                        else if (ActionDD.SelectedItem.Text == "Challenge Block Foreign Aid")
                            cardToRemove = "Duke";
                        else if (ActionDD.SelectedItem.Text == "Challenge Exchange")
                            cardToRemove = "Ambassador";



                        for (int i = 0; i < Globals.GlobalAnalysis.getCardsInHand().Count; i++)
                        {
                            if (Globals.GlobalAnalysis.getCardsInHand()[i].getCardType() == cardToRemove)
                                Globals.GlobalAnalysis.getCardsInHand().RemoveAt(i);
                        }

                        Label1.Text = "What is your new card?";
                        HiddenDDOne.Visible = true;
                        HiddenDDTwo.Visible = false;
                        SubmitButton.Visible = false;

                        HiddenDDOne.Items.Clear();
                        populateCardsLeftDropDownList(HiddenDDOne);

                        ChallengeSubmitButton.Visible = true;
                    }
                    else 
                    {
                        Response.Redirect("GamePlayPage.aspx");
                    }
                }
                
            }
            else if(ActionDD.SelectedItem.Text == "Exchange")
            {
                Globals.GlobalAnalysis.getCardsInHand().Clear();
                Globals.GlobalAnalysis.addCardsToHand(HiddenDDOne.SelectedItem.Text);
                if (HiddenDDTwo.Visible == true)
                    Globals.GlobalAnalysis.addCardsToHand(HiddenDDTwo.SelectedItem.Text);

                Response.Redirect("GamePlayPage.aspx");
            }
        }

        protected void ChallengeSubmitButton_Click(object sender, EventArgs e)
        {
            Globals.GlobalAnalysis.addCardsToHand(HiddenDDOne.SelectedItem.Text);
            Globals.GlobalAnalysis.addActions(Globals.CorBPlayer + ": Got new card");
            Response.Redirect("GamePlayPage.aspx");
        }

        protected void YesButtonForAssassination_Click(object sender, EventArgs e)
        {
            Globals.AssassinatedPlayer = PlayerDD.SelectedItem.Text;

            Label1.Text = "Did the challenger win?";
            YesButton.Visible = true;
            NoButton.Visible = true;
            YesButtonForAssassination.Visible = false;
            NoButtonForAssassination.Visible = false;
        }

        protected void NoButtonForAssassination_Click(object sender, EventArgs e)
        {
            Label1.Text = "Did the challenger win?";
            YesButton.Visible = true;
            NoButton.Visible = true;
            YesButtonForAssassination.Visible = false;
            NoButtonForAssassination.Visible = false;
        }

        private void handleChallengeAssassinate()
        {
            int index = Globals.AssassinatedPlayer.IndexOf(':');
            string playerUsername = Globals.AssassinatedPlayer.Substring(0, index);

            //if the challenger lost and the challenger was the one being assassinated, and if they have two cards they lose both otherwise they lose the one
            if (playerUsername == PlayerDD.SelectedItem.Text)  
            {
                if(Globals.GlobalAnalysis.getPlayerCardCounts()[Globals.GlobalAnalysis.getPlayerUsernames().IndexOf(playerUsername)] == 2) //if they have two cards
                {
                    string card = HiddenDDOne.SelectedItem.Text;
                    string card2 = HiddenDDTwo.SelectedItem.Text;

                    Globals.GlobalAnalysis.updatePlayerCardCount(playerUsername, Globals.GlobalAnalysis.getPlayerCardCounts()[Globals.GlobalAnalysis.getPlayerUsernames().IndexOf(playerUsername)] - 2);

                    //if new number of cards is zero do something to kick them out of the game
                    
                    int playerId = getUserId(playerUsername);

                    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString);
                    conn.Open();
                    string insertAction = "INSERT INTO ActionDataTable (Player, ActionType, Action) VALUES (@id, @at, @a)";
                    SqlCommand com = new SqlCommand(insertAction, conn);
                    com.Parameters.AddWithValue("@id", playerId);
                    com.Parameters.AddWithValue("@at", "Lose Card");
                    com.Parameters.AddWithValue("@a", "Lost a " + card + " and a " + card2);
                    com.ExecuteNonQuery();
                    conn.Close();

                    Globals.GlobalAnalysis.addActions(playerUsername + ": Lost the " + card + " and the " + card2);

                    updateDeadCardCounters(card);
                    updateDeadCardCounters(card2);
                }
                else if (Globals.GlobalAnalysis.getPlayerCardCounts()[Globals.GlobalAnalysis.getPlayerUsernames().IndexOf(playerUsername)] == 1)  //if they only have one card
                {
                    string card = HiddenDDOne.SelectedItem.Text;

                    Globals.GlobalAnalysis.updatePlayerCardCount(playerUsername, Globals.GlobalAnalysis.getPlayerCardCounts()[Globals.GlobalAnalysis.getPlayerUsernames().IndexOf(playerUsername)] - 1);

                    //if new number of cards is zero do something to kick them out of the game
                    int playerId = getUserId(playerUsername);

                    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString);
                    conn.Open();
                    string insertAction = "INSERT INTO ActionDataTable (Player, ActionType, Action) VALUES (@id, @at, @a)";
                    SqlCommand com = new SqlCommand(insertAction, conn);
                    com.Parameters.AddWithValue("@id", playerId);
                    com.Parameters.AddWithValue("@at", "Lose Card");
                    com.Parameters.AddWithValue("@a", "Lost a " + card);
                    com.ExecuteNonQuery();
                    conn.Close();

                    Globals.GlobalAnalysis.addActions(playerUsername + ": Lost the " + card);

                    updateDeadCardCounters(card);
                }

                if (playerUsername == Session["New"].ToString()) //if that player was the user, clear their hand
                {
                    Globals.GlobalAnalysis.getCardsInHand().Clear();
                }
            }
            else  //if the challenger lost and the challenger is not the player being assassinated, they both lose one card
            {
                string card = HiddenDDOne.SelectedItem.Text;
                string card2 = HiddenDDTwo.SelectedItem.Text;
                playerUsername = HiddenDDThree.SelectedItem.Text;

                Globals.GlobalAnalysis.updatePlayerCardCount(playerUsername, Globals.GlobalAnalysis.getPlayerCardCounts()[Globals.GlobalAnalysis.getPlayerUsernames().IndexOf(playerUsername)] - 1);
                Globals.GlobalAnalysis.updatePlayerCardCount(PlayerDD.SelectedItem.Text, Globals.GlobalAnalysis.getPlayerCardCounts()[Globals.GlobalAnalysis.getPlayerUsernames().IndexOf(PlayerDD.SelectedItem.Text)] - 1);

                //if new number of cards is zero do something to kick them out of the game
       
                int playerIdOne = getUserId(playerUsername);

                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString);
                conn.Open();
                string insertActionOne = "INSERT INTO ActionDataTable (Player, ActionType, Action) VALUES (@id, @at, @a)";
                SqlCommand com = new SqlCommand(insertActionOne, conn);
                com.Parameters.AddWithValue("@id", playerIdOne);
                com.Parameters.AddWithValue("@at", "Lose Card");
                com.Parameters.AddWithValue("@a", "Lost a " + card);
                com.ExecuteNonQuery();
                //----------------------------------------
                int playerIdTwo = getUserId(PlayerDD.SelectedItem.Text);

                string insertActionTwo = "INSERT INTO ActionDataTable (Player, ActionType, Action) VALUES (@id, @at, @a)";
                com = new SqlCommand(insertActionTwo, conn);
                com.Parameters.AddWithValue("@id", playerIdTwo);
                com.Parameters.AddWithValue("@at", "Lose Card");
                com.Parameters.AddWithValue("@a", "Lost a " + card2);
                com.ExecuteNonQuery();
                conn.Close();

                Globals.GlobalAnalysis.addActions(playerUsername + ": was Assassinated and lost the " + card2);
                Globals.GlobalAnalysis.addActions(PlayerDD.SelectedItem.Text + ": Lost the " + card);

                if (playerUsername == Session["New"].ToString())
                {
                    Card c = new Card("Duke");
                    for (int i = 0; i < Globals.GlobalAnalysis.getCardsInHand().Count; i++)
                    {
                        if (Globals.GlobalAnalysis.getCardsInHand()[i].getCardType() == card2)
                            c = Globals.GlobalAnalysis.getCardsInHand()[i];
                    }
                    Globals.GlobalAnalysis.getCardsInHand().Remove(c);
                }
                else if(PlayerDD.SelectedItem.Text == Session["New"].ToString())
                {
                    Card c = new Card("Duke");
                    for (int i = 0; i < Globals.GlobalAnalysis.getCardsInHand().Count; i++)
                    {
                        if (Globals.GlobalAnalysis.getCardsInHand()[i].getCardType() == card)
                            c = Globals.GlobalAnalysis.getCardsInHand()[i];
                    }
                    Globals.GlobalAnalysis.getCardsInHand().Remove(c);
                }

                updateDeadCardCounters(card);
                updateDeadCardCounters(card2);
            }

            if (Globals.CorBPlayer == Session["New"].ToString())
            {
                string cardToRemove = "Assassin";

                for (int i = 0; i < Globals.GlobalAnalysis.getCardsInHand().Count; i++)
                {
                    if (Globals.GlobalAnalysis.getCardsInHand()[i].getCardType() == cardToRemove)
                        Globals.GlobalAnalysis.getCardsInHand().RemoveAt(i);
                }

                Label1.Text = "What is your new card?";
                HiddenDDOne.Visible = true;
                HiddenDDTwo.Visible = false;
                HiddenDDThree.Visible = false;
                AssassinatedPlayerLB.Visible = false;
                SubmitButton.Visible = false;

                HiddenDDOne.Items.Clear();
                populateCardsLeftDropDownList(HiddenDDOne);

                ChallengeSubmitButton.Visible = true;
            }
            else
            {
                Response.Redirect("GamePlayPage.aspx");
            }
        }

        private void updateDeadCardCounters(string card)
        {
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
        }
    
        private int getUserId(string username)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString);
            conn.Open();
            string id = "SELECT Id FROM UserDataTable WHERE Username = '" + username + "'";
            SqlCommand com1 = new SqlCommand(id, conn);
            int playerId = Convert.ToInt32(com1.ExecuteScalar().ToString());
            conn.Close();

            return playerId;
        }
    }
}