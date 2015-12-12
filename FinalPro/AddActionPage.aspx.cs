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
            if (!(PlayerDD.SelectedItem.Text.Contains("Select Player") || ActionTypeDD.SelectedItem.Text.Contains("Select Action Type") || ActionDD.Items.Count == 0 || ActionDD.SelectedItem.Text.Contains("Select Action")))
            {
                string playerUsername = PlayerDD.SelectedItem.Text.ToString();
                int playerId = getUserId(playerUsername);
                string[] id = Session["New"].ToString().Split(':');
                insertAction(playerId, ActionTypeDD.SelectedItem.Text, ActionDD.SelectedItem.Text, id[1]);

                Globals.GlobalAnalysis.addActions(playerUsername + ": " + ActionDD.SelectedItem.Text);

                if (ActionDD.SelectedItem.Text == "Foreign Aid")
                {
                    //is there a block
                    Label1.Visible = true;
                    Label1.Text = "Did someone block foreign aid?";
                    YesButton.Visible = true;
                    NoButton.Visible = true;
                }
                else if (ActionDD.SelectedItem.Text == "Coup")
                {
                    //which card did they get rid of
                    Label1.Visible = true;
                    Label1.Text = "Who loses a card and what card did they lose?";
                    HiddenDDOne.Visible = true;
                    HiddenDDOne.Items.Clear();
                    HiddenDDOne.Items.Add("Select Player");
                    string[] id1 = Session["New"].ToString().Split(':');
                    HiddenDDOne.Items.Add(id1[0]);
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
                    Globals.GlobalAnalysis.addPossibleCard("Duke", false, PlayerDD.SelectedItem.Text);
                    Globals.GlobalAnalysis.calculateStatistics();

                    //did someone challenge this
                    Label1.Visible = true;
                    Label1.Text = "Did someone challenge tax?";
                    YesButton.Visible = true;
                    NoButton.Visible = true;
                }
                else if (ActionDD.SelectedItem.Text == "Block Foreign Aid")
                {
                    Globals.GlobalAnalysis.addPossibleCard("Duke", false, PlayerDD.SelectedItem.Text);
                    Globals.GlobalAnalysis.calculateStatistics();

                    //did someone challenge this
                    Label1.Visible = true;
                    Label1.Text = "Did someone challenge blocking of foreign aid?";
                    YesButton.Visible = true;
                    NoButton.Visible = true;
                }
                else if (ActionDD.SelectedItem.Text == "Assassinate")
                {
                    Globals.GlobalAnalysis.addPossibleCard("Assassin", false, PlayerDD.SelectedItem.Text);
                    Globals.GlobalAnalysis.calculateStatistics();

                    //did someone block or challenge (if no one challenges the person being assassinated loses a card)
                    Globals.PlayerAssassinating = PlayerDD.SelectedItem.Text;
                    Label1.Visible = true;
                    Label1.Text = "Did someone challenge or block assassination?";
                    YesButton.Visible = true;
                    NoButton.Visible = true;
                }
                else if (ActionDD.SelectedItem.Text == "Exchange")
                {
                    Globals.GlobalAnalysis.addPossibleCard("Ambassador", false, PlayerDD.SelectedItem.Text);
                    Globals.GlobalAnalysis.calculateStatistics();

                    //did someone challenge this
                    Label1.Visible = true;
                    Label1.Text = "Did someone challenge the exchange?";
                    YesButton.Visible = true;
                    NoButton.Visible = true;
                }
                else if (ActionDD.SelectedItem.Text == "Steal")
                {
                    Globals.GlobalAnalysis.addPossibleCard("Captain", false, PlayerDD.SelectedItem.Text);
                    Globals.GlobalAnalysis.calculateStatistics();

                    //did someone challenge this
                    Label1.Visible = true;
                    Label1.Text = "Did someone challenge or block the Steal?";
                    YesButton.Visible = true;
                    NoButton.Visible = true;
                }
                else if (ActionDD.SelectedItem.Text == "Block Stealing")
                {
                    Globals.GlobalAnalysis.addPossibleCard("Captain", true, PlayerDD.SelectedItem.Text);
                    Globals.GlobalAnalysis.addPossibleCard("Ambassador", true, PlayerDD.SelectedItem.Text);
                    Globals.GlobalAnalysis.calculateStatistics();

                    //did someone challenge this
                    Label1.Visible = true;
                    Label1.Text = "Did someone challenge blocking a steal?";
                    YesButton.Visible = true;
                    NoButton.Visible = true;
                }
                else if (ActionDD.SelectedItem.Text == "Block Assassination")
                {
                    Globals.GlobalAnalysis.addPossibleCard("Contessa", false, PlayerDD.SelectedItem.Text);
                    Globals.GlobalAnalysis.calculateStatistics();

                    //did someone challenge this
                    Globals.PlayerBlockingAssassination = PlayerDD.SelectedItem.Text;
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
                    Label1.Visible = true;
                    Label1.Text = "Did the challenger win?";
                    YesButton.Visible = true;
                    NoButton.Visible = true;
                    //if challenger wins opponent loses a card
                    //if opponent wins challenger loses a card
                }
                else if (ActionDD.SelectedItem.Text == "Challenge Stealing")
                {
                    Label1.Visible = true;
                    Label1.Text = "Did the challenger win?";
                    YesButton.Visible = true;
                    NoButton.Visible = true;
                    //if challenger wins opponent loses a card
                    //if opponent wins challenger loses a card
                }
                else if (ActionDD.SelectedItem.Text == "Challenge Block Assassination")
                {
                    //Globals.PlayerAssassinating = Player being blocked
                    //Globals.PlayerBlockingAssassination
                    //ask who is being assassinated
                    //PlayerDD.SelectedItem.Text is person challenging the block on the assassination
                    //ask if the challenger won
                    Label2.Visible = true;
                    Label3.Visible = true;
                    Label4.Visible = true;
                    Label2.Text = Globals.PlayerAssassinating + " is being blocked by " + Globals.PlayerBlockingAssassination;
                    Label3.Text = "Who is being assassinated?";
                    DropDownList1.Visible = true;
                    DropDownList1.Items.Add("Select Player");
                    foreach (string un in Globals.GlobalAnalysis.getPlayerUsernames())
                    {
                        DropDownList1.Items.Add(un);
                    }
                    Label4.Text = "Did " + PlayerDD.SelectedItem.Text + " win the challenge?";
                    Button1.Visible = true;
                    Button2.Visible = true;

                    //if challenger wins opponent loses a card and person being assassinated loses a card
                    //if opponent wins challenger loses a card
                }
                else
                {
                    Response.Redirect("GamePlayPage.aspx");
                }
            }
        } 

        protected void YesButton_Click(object sender, EventArgs e)
        {
            Globals.ChallengeStatus = "Won";
            string[] id = Session["New"].ToString().Split(':');
            if (ActionDD.SelectedItem.Text == "Challenge Tax" || ActionDD.SelectedItem.Text == "Challenge Block Foreign Aid"
                || ActionDD.SelectedItem.Text == "Challenge Exchange" || ActionDD.SelectedItem.Text == "Challenge Assassinate"
                || ActionDD.SelectedItem.Text == "Challenge Block Stealing" || ActionDD.SelectedItem.Text == "Challenge Stealing")
            {
                //should this be added to database or just the stream
                Globals.GlobalAnalysis.addActions(PlayerDD.SelectedItem.Text + ": wins the challenge");

                Label1.Text = "What card did " + Globals.CorBPlayer + " lose?";

                HiddenDDOne.Visible = true;
                HiddenDDOne.Items.Clear();
                HiddenDDOne.Items.Add(Globals.CorBPlayer);

                HiddenDDTwo.Visible = true;
                HiddenDDTwo.Items.Clear();
                if (Globals.CorBPlayer == id[0])
                {
                    foreach(Card c in Globals.GlobalAnalysis.getCardsInHand())
                    {
                        HiddenDDTwo.Items.Add(c.getCardType());
                    }
                }
                else
                {
                    populateCardsLeftDropDownList(HiddenDDTwo);
                }

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
            string[] id = Session["New"].ToString().Split(':');
            Globals.ChallengeStatus = "Lost";
            if(ActionDD.SelectedItem.Text == "Exchange" && PlayerDD.SelectedItem.Text != id[0])
            {
                Globals.GlobalAnalysis.getPossibleCard(PlayerDD.SelectedItem.Text).Clear();
                Globals.GlobalAnalysis.calculateStatistics();
                Response.Redirect("GamePlayPage.aspx");
            }
            else if (ActionDD.SelectedItem.Text == "Exchange" && PlayerDD.SelectedItem.Text == id[0])
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
                YesButton.Visible = false;
                NoButton.Visible = false;
            }
            else if(ActionDD.SelectedItem.Text == "Assassinate")
            {
                //show drop down for who loses a card and what card
                Label1.Text = "Who was assassinated and what card did they lose?";

                HiddenDDOne.Visible = true;
                HiddenDDOne.Items.Clear();
                HiddenDDOne.Items.Add("Select Player");
                foreach (string username in Globals.GlobalAnalysis.getPlayerUsernames())
                {
                    HiddenDDOne.Items.Add(username);
                }

                HiddenDDTwo.Visible = true;
                HiddenDDTwo.Items.Clear();

                populateCardsLeftDropDownList(HiddenDDTwo); //possibly filter to your cards if you are the one being assassinated
                
                SubmitButton.Visible = true;
                YesButton.Visible = false;
                NoButton.Visible = false;
            }
            else if(ActionDD.SelectedItem.Text == "Challenge Tax" || ActionDD.SelectedItem.Text == "Challenge Block Foreign Aid"
                || ActionDD.SelectedItem.Text == "Challenge Exchange" || ActionDD.SelectedItem.Text == "Challenge Block Stealing"
                || ActionDD.SelectedItem.Text == "Challenge Stealing")
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
            }
            else
            {
                Response.Redirect("GamePlayPage.aspx");
            }
        }

        private void populateCardsLeftDropDownList(DropDownList list)
        {
            Card c1 = null; 
            Card c2 = null;
            if (Globals.GlobalAnalysis.getCardsInHand().Count != 0)
            {
                c1 = Globals.GlobalAnalysis.getCardsInHand()[0];
            }
            if (Globals.GlobalAnalysis.getCardsInHand().Count == 2)
            {
                c2 = Globals.GlobalAnalysis.getCardsInHand()[1];
            }
                       
            if (!(Globals.DukeCounter == 2 && ((c1 != null && c1.getCardType().ToString() == "Duke") || (c2 != null && c2.getCardType().ToString() == "Duke"))))
            {
                list.Items.Add("Duke");
            }
            else if (!(Globals.DukeCounter == 1 && ((c1 != null && c1.getCardType().ToString() == "Duke") && (c2 != null && c2.getCardType().ToString() == "Duke"))))
            {
                list.Items.Add("Duke");
            }
            else if(!(Globals.DukeCounter == 3))
            {
                list.Items.Add("Duke");
            }

            if (!(Globals.CaptainCounter == 2 && ((c1 != null && c1.getCardType().ToString() == "Captain") || (c2 != null && c2.getCardType().ToString() == "Captain"))))
            {
                list.Items.Add("Captain");
            }
            else if (!(Globals.CaptainCounter == 1 && ((c1 != null && c1.getCardType().ToString() == "Captain") && (c2 != null && c2.getCardType().ToString() == "Captain"))))
            {
                list.Items.Add("Captain");
            }
            else if (!(Globals.CaptainCounter == 3))
            {
                list.Items.Add("Captain");
            }

            if (!(Globals.AssassinCounter == 2 && ((c1 != null && c1.getCardType().ToString() == "Assassin") || (c2 != null && c2.getCardType().ToString() == "Assassin"))))
            {
                list.Items.Add("Assassin");
            }
            else if (!(Globals.AssassinCounter == 1 && ((c1 != null && c1.getCardType().ToString() == "Assassin") && (c2 != null && c2.getCardType().ToString() == "Assassin"))))
            {
                list.Items.Add("Assassin");
            }
            else if (!(Globals.AssassinCounter == 3))
            {
                list.Items.Add("Assassin");
            }

            if (!(Globals.ContessaCounter == 2 && ((c1 != null && c1.getCardType().ToString() == "Contessa") || (c2 != null && c2.getCardType().ToString() == "Contessa"))))
            {
                list.Items.Add("Contessa");
            }
            else if (!(Globals.ContessaCounter == 1 && ((c1 != null && c1.getCardType().ToString() == "Contessa") && (c2 != null && c2.getCardType().ToString() == "Contessa"))))
            {
                list.Items.Add("Contessa");
            }
            else if (!(Globals.ContessaCounter == 3))
            {
                list.Items.Add("Contessa");
            }

            if (!(Globals.AmbassadorCounter == 2 && ((c1 != null && c1.getCardType().ToString() == "Ambassador") || (c2 != null && c2.getCardType().ToString() == "Ambassador"))))
            {
                list.Items.Add("Ambassador");
            }
            else if (!(Globals.AmbassadorCounter == 1 && ((c1 != null && c1.getCardType().ToString() == "Ambassador") && (c2 != null && c2.getCardType().ToString() == "Ambassador"))))
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
            string[] id = Session["New"].ToString().Split(':');
            ErrorLabel.Visible = false;
            if (ActionDD.SelectedItem.Text == "Coup" || ActionDD.SelectedItem.Text == "Assassinate" || ActionDD.SelectedItem.Text.Contains("Challenge"))
            {
                if(ActionDD.SelectedItem.Text == "Challenge Assassinate" && Globals.ChallengeStatus == "Lost")
                {
                    handleChallengeAssassinate();
                }
                else if(ActionDD.SelectedItem.Text == "Challenge Block Assassination")
                {
                    handleChallengeBlockAssassination();
                }
                else 
                {
                    if (HiddenDDOne.SelectedItem.Text == id[0] && isCardInHand(HiddenDDTwo.SelectedItem.Text) || HiddenDDOne.SelectedItem.Text != id[0])
                    {
                        string card = HiddenDDTwo.SelectedItem.Text;
                        string playerUsername = HiddenDDOne.SelectedItem.Text.ToString();
                        Globals.GlobalAnalysis.updatePlayerCardCount(playerUsername, Globals.GlobalAnalysis.getPlayerCardCounts()[Globals.GlobalAnalysis.getPlayerUsernames().IndexOf(playerUsername)] - 1);
                        //if new number of cards is zero do something to kick them out of the game

                        int playerId = getUserId(playerUsername);
                        insertAction(playerId, "Lose Card", "Lost a " + card, id[1]);

                        Globals.GlobalAnalysis.addActions(playerUsername + ": Lost the " + card);

                        if (ActionDD.SelectedItem.Text.Contains("Challenge") && Globals.ChallengeStatus == "Lost" && Globals.CorBPlayer != id[0])
                        {
                            Globals.GlobalAnalysis.addActions(Globals.CorBPlayer + ": Gets new card");
                        }

                        for (int i = 0; i < Globals.GlobalAnalysis.getPossibleCard(playerUsername).Count; i++)
                        {
                            if (Globals.GlobalAnalysis.getPossibleCard(playerUsername)[i].T == card)
                            {
                                Globals.GlobalAnalysis.getPossibleCard(playerUsername).RemoveAt(i);
                                break;
                            }
                        }

                        if (ActionDD.SelectedItem.Text.Contains("Challenge") && Globals.ChallengeStatus == "Lost")
                        {
                            for (int i = 0; i < Globals.GlobalAnalysis.getPossibleCard(Globals.CorBPlayer).Count; i++)
                            {
                                if (ActionDD.SelectedItem.Text == "Challenge Tax" && Globals.GlobalAnalysis.getPossibleCard(Globals.CorBPlayer)[i].T == "Duke")
                                {
                                    Globals.GlobalAnalysis.getPossibleCard(Globals.CorBPlayer).RemoveAt(i);
                                    break;
                                }
                                else if (ActionDD.SelectedItem.Text == "Challenge Block Foreign Aid" && Globals.GlobalAnalysis.getPossibleCard(Globals.CorBPlayer)[i].T == "Duke")
                                {
                                    Globals.GlobalAnalysis.getPossibleCard(Globals.CorBPlayer).RemoveAt(i);
                                    break;
                                }
                                else if (ActionDD.SelectedItem.Text == "Challenge Stealing" && Globals.GlobalAnalysis.getPossibleCard(Globals.CorBPlayer)[i].T == "Captain")
                                {
                                    Globals.GlobalAnalysis.getPossibleCard(Globals.CorBPlayer).RemoveAt(i);
                                    break;
                                }
                                else if (ActionDD.SelectedItem.Text == "Challenge Exchange")
                                {
                                    Globals.GlobalAnalysis.getPossibleCard(Globals.CorBPlayer).Clear();
                                    break;
                                }
                                else if (ActionDD.SelectedItem.Text == "Challenge Block Stealing")
                                {
                                    if (Globals.GlobalAnalysis.getPossibleCard(Globals.CorBPlayer)[i].T == "Captain" && Globals.GlobalAnalysis.getPossibleCard(Globals.CorBPlayer)[i].B)
                                    {
                                        Globals.GlobalAnalysis.getPossibleCard(Globals.CorBPlayer).RemoveAt(i); //remove captain
                                        Globals.GlobalAnalysis.getPossibleCard(Globals.CorBPlayer).RemoveAt(i); //remove ambassador both of which were added becuase the player blocked a steal
                                        break;
                                    }
                                }
                            }
                        }
                        Globals.GlobalAnalysis.calculateStatistics();

                        updateDeadCardCounters(card);

                        if (HiddenDDOne.SelectedItem.Text == id[0])
                        {
                            Card c = new Card("Duke");
                            for (int i = 0; i < Globals.GlobalAnalysis.getCardsInHand().Count; i++)
                            {
                                if (Globals.GlobalAnalysis.getCardsInHand()[i].getCardType() == HiddenDDTwo.SelectedItem.Text)
                                    c = Globals.GlobalAnalysis.getCardsInHand()[i];
                            }
                            Globals.GlobalAnalysis.getCardsInHand().Remove(c);
                        }
                        if (ActionDD.SelectedItem.Text.Contains("Challenge") && Globals.CorBPlayer == id[0] && Globals.ChallengeStatus == "Lost")
                        {
                            string cardToRemove = "";
                            if (ActionDD.SelectedItem.Text == "Challenge Tax")
                                cardToRemove = "Duke";
                            else if (ActionDD.SelectedItem.Text == "Challenge Block Foreign Aid")
                                cardToRemove = "Duke";
                            else if (ActionDD.SelectedItem.Text == "Challenge Exchange")
                                cardToRemove = "Ambassador";
                            //else if (ActionDD.SelectedItem.Text == "Challenge Block Stealing")
                            //either lose Ambassador or Captain
                            else if (ActionDD.SelectedItem.Text == "Challenge Stealing")
                                cardToRemove = "Captain";
                            else if (ActionDD.SelectedItem.Text == "Challenge Assassinate")
                                cardToRemove = "Assassin";

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
                    else
                    {
                        ErrorLabel.Visible = true;
                        ErrorLabel.Text = "That Card is not in your hand!";
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

        private bool isCardInHand(string p)
        {
            bool retVal = false;
            foreach(Card c in Globals.GlobalAnalysis.getCardsInHand())
            {
                if(c.getCardType() == p)
                {
                    retVal = true;
                }
            }
            return retVal;
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
            //int index = Globals.AssassinatedPlayer.IndexOf(':');
            string playerUsername = Globals.AssassinatedPlayer;
            string[] id = Session["New"].ToString().Split(':');
            bool isError = false;
            //if the challenger lost and the challenger was the one being assassinated, and if they have two cards they lose both otherwise they lose the one
            if (playerUsername == PlayerDD.SelectedItem.Text)  
            {
                if(Globals.GlobalAnalysis.getPlayerCardCounts()[Globals.GlobalAnalysis.getPlayerUsernames().IndexOf(playerUsername)] == 2) //if they have two cards
                {
                    if ((playerUsername == id[0] && isBothCardsInHand(HiddenDDOne.SelectedItem.Text, HiddenDDTwo.SelectedItem.Text)) || playerUsername != id[0])
                    {
                        string card = HiddenDDOne.SelectedItem.Text;
                        string card2 = HiddenDDTwo.SelectedItem.Text;

                        Globals.GlobalAnalysis.updatePlayerCardCount(playerUsername, Globals.GlobalAnalysis.getPlayerCardCounts()[Globals.GlobalAnalysis.getPlayerUsernames().IndexOf(playerUsername)] - 2);

                        //if new number of cards is zero do something to kick them out of the game

                        int playerId = getUserId(playerUsername);
                        insertAction(playerId, "Lose Card", "Lost a " + card + " and a " + card2, id[1]);

                        Globals.GlobalAnalysis.addActions(playerUsername + ": Lost the " + card + " and the " + card2);

                        updateDeadCardCounters(card);
                        updateDeadCardCounters(card2);
                    }
                    else
                    {
                        ErrorLabel.Visible = true;
                        ErrorLabel.Text = "One or both of thoses cards are not in your hand!";
                        isError = true;
                    }
                }
                else if (Globals.GlobalAnalysis.getPlayerCardCounts()[Globals.GlobalAnalysis.getPlayerUsernames().IndexOf(playerUsername)] == 1)  //if they only have one card
                {
                    if ((playerUsername == id[0] && isCardInHand(HiddenDDOne.SelectedItem.Text)) || playerUsername != id[0])
                    {
                        string card = HiddenDDOne.SelectedItem.Text;

                        Globals.GlobalAnalysis.updatePlayerCardCount(playerUsername, Globals.GlobalAnalysis.getPlayerCardCounts()[Globals.GlobalAnalysis.getPlayerUsernames().IndexOf(playerUsername)] - 1);

                        //if new number of cards is zero do something to kick them out of the game
                        int playerId = getUserId(playerUsername);
                        insertAction(playerId, "Lose Card", "Lost a " + card, id[1]);

                        Globals.GlobalAnalysis.addActions(playerUsername + ": Lost the " + card);

                        updateDeadCardCounters(card);
                    }
                    else
                    {
                        ErrorLabel.Visible = true;
                        ErrorLabel.Text = "That card is not in your hand!";
                        isError = true;
                    }
                }
                if (!isError)
                {
                    if (playerUsername == id[0]) //if that player was the user, clear their hand
                    {
                        Globals.GlobalAnalysis.getCardsInHand().Clear();
                    }
                    Globals.GlobalAnalysis.getPossibleCard(playerUsername).Clear();
                    Globals.GlobalAnalysis.calculateStatistics();
                }
            }
            else  //if the challenger lost and the challenger is not the player being assassinated, they both lose one card
            {
                string card = HiddenDDOne.SelectedItem.Text;
                string card2 = HiddenDDTwo.SelectedItem.Text;
                playerUsername = HiddenDDThree.SelectedItem.Text;

                if ((playerUsername == id[0] && isCardInHand(card2)) || (PlayerDD.SelectedItem.Text == id[0] && isCardInHand(card)) || (playerUsername != id[0] && PlayerDD.SelectedItem.Text != id[0]))
                {
                    Globals.GlobalAnalysis.updatePlayerCardCount(playerUsername, Globals.GlobalAnalysis.getPlayerCardCounts()[Globals.GlobalAnalysis.getPlayerUsernames().IndexOf(playerUsername)] - 1);
                    Globals.GlobalAnalysis.updatePlayerCardCount(PlayerDD.SelectedItem.Text, Globals.GlobalAnalysis.getPlayerCardCounts()[Globals.GlobalAnalysis.getPlayerUsernames().IndexOf(PlayerDD.SelectedItem.Text)] - 1);

                    //if new number of cards is zero do something to kick them out of the game

                    int playerIdOne = getUserId(playerUsername);
                    insertAction(playerIdOne, "Lose Card", "Lost a " + card2, id[1]);

                    int playerIdTwo = getUserId(PlayerDD.SelectedItem.Text);
                    insertAction(playerIdTwo, "Lose Card", "Lost a " + card, id[1]);

                    Globals.GlobalAnalysis.addActions(playerUsername + ": was Assassinated and lost the " + card2);
                    Globals.GlobalAnalysis.addActions(PlayerDD.SelectedItem.Text + ": Lost the " + card);

                    if (playerUsername == id[0])
                    {
                        Card c = new Card("Duke");
                        for (int i = 0; i < Globals.GlobalAnalysis.getCardsInHand().Count; i++)
                        {
                            if (Globals.GlobalAnalysis.getCardsInHand()[i].getCardType() == card2)
                                c = Globals.GlobalAnalysis.getCardsInHand()[i];
                        }
                        Globals.GlobalAnalysis.getCardsInHand().Remove(c);
                    }
                    else if (PlayerDD.SelectedItem.Text == id[0])
                    {
                        Card c = new Card("Duke");
                        for (int i = 0; i < Globals.GlobalAnalysis.getCardsInHand().Count; i++)
                        {
                            if (Globals.GlobalAnalysis.getCardsInHand()[i].getCardType() == card)
                                c = Globals.GlobalAnalysis.getCardsInHand()[i];
                        }
                        Globals.GlobalAnalysis.getCardsInHand().Remove(c);
                    }

                    for (int i = 0; i < Globals.GlobalAnalysis.getPossibleCard(playerUsername).Count; i++)
                    {
                        if (Globals.GlobalAnalysis.getPossibleCard(playerUsername)[i].T == card)
                        {
                            Globals.GlobalAnalysis.getPossibleCard(playerUsername).RemoveAt(i);
                            break;
                        }
                    }
                    for (int i = 0; i < Globals.GlobalAnalysis.getPossibleCard(PlayerDD.SelectedItem.Text).Count; i++)
                    {
                        if (Globals.GlobalAnalysis.getPossibleCard(PlayerDD.SelectedItem.Text)[i].T == card2)
                        {
                            Globals.GlobalAnalysis.getPossibleCard(PlayerDD.SelectedItem.Text).RemoveAt(i);
                            break;
                        }
                    }
                    Globals.GlobalAnalysis.calculateStatistics();

                    updateDeadCardCounters(card);
                    updateDeadCardCounters(card2);
                }
                else
                {
                    ErrorLabel.Visible = true;
                    ErrorLabel.Text = "That card is not in your hand!";
                    isError = true;
                }
            }

            if (Globals.CorBPlayer == id[0])
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
                if(!isError)
                    Response.Redirect("GamePlayPage.aspx");
            }
        }

        private bool isBothCardsInHand(string p1, string p2)
        {
            if((Globals.GlobalAnalysis.getCardsInHand()[0].getCardType() == p1 && Globals.GlobalAnalysis.getCardsInHand()[1].getCardType() == p2) || (Globals.GlobalAnalysis.getCardsInHand()[0].getCardType() == p2 && Globals.GlobalAnalysis.getCardsInHand()[1].getCardType() == p1))
            {
                return true;
            }
            else
            {
                return false;
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

        private void insertAction(int playerId, string actionType, string action, string gameId)
        {
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString);
            conn.Open();
            string insertActionOne = "INSERT INTO ActionDataTable (Player, ActionType, Action, GameId) VALUES (@id, @at, @a, @gid)";
            SqlCommand com = new SqlCommand(insertActionOne, conn);
            com.Parameters.AddWithValue("@id", playerId);
            com.Parameters.AddWithValue("@at", actionType);
            com.Parameters.AddWithValue("@a", action);
            com.Parameters.AddWithValue("@gid", gameId);
            com.ExecuteNonQuery();
            conn.Close();
        }

        protected void Button1_Click(object sender, EventArgs e) //the person challenging the person blocking an assassination wins
        {
            Label2.Visible = false;
            Label3.Visible = false;
            Label4.Visible = false;
            DropDownList1.Visible = false;
            Button1.Visible = false;
            Button2.Visible = false;

            Globals.GlobalAnalysis.addActions(PlayerDD.SelectedItem.Text + ": wins the challenge");
            //person assassinating -> p1, person being assassinated -> p2, person blocking -> p3, person challenging -> p4
            //p1 -> Globals.PlayerAssassinating
            //p2 -> DropDownList1.SelectedItem.Text
            //p3 -> Globals.PlayerBlockingAssassination
            //p4 -> PlayerDD.SelectedItem.Text

            //if p2 is p3 and p1 is p4 - Two players total involoved or if p2 is p3 and the p1 is not p4 - Three players total involoved
            if((DropDownList1.SelectedItem.Text == Globals.PlayerBlockingAssassination && Globals.PlayerAssassinating == PlayerDD.SelectedItem.Text)
                || (DropDownList1.SelectedItem.Text == Globals.PlayerBlockingAssassination && Globals.PlayerAssassinating != PlayerDD.SelectedItem.Text))
            {
                //person being assassinated loses a card for the assassination and loses a card for losing the challenge (p2 loses 2 cards)
                Label1.Visible = true;
                Label1.Text = "What cards did " + DropDownList1.SelectedItem.Text + " lose?";
                HiddenDDOne.Visible = true;
                populateCardsLeftDropDownList(HiddenDDOne);
                //show second drop down if player has two cards
                if (Globals.GlobalAnalysis.getPlayerCardCounts()[Globals.GlobalAnalysis.getPlayerUsernames().IndexOf(DropDownList1.SelectedItem.Text)] == 2)
                {
                    HiddenDDTwo.Visible = true;
                    populateCardsLeftDropDownList(HiddenDDTwo);
                }
                SubmitButton.Visible = true;
            }
            //if p2 is not p3 and p1 is p4 - Three players total involoved or if p2 is not p3 and p1 is not p4 - Four players total involoved
            else if ((DropDownList1.SelectedItem.Text != Globals.PlayerBlockingAssassination && Globals.PlayerAssassinating == PlayerDD.SelectedItem.Text)
                || (DropDownList1.SelectedItem.Text != Globals.PlayerBlockingAssassination && Globals.PlayerAssassinating != PlayerDD.SelectedItem.Text))
            {
                //person being assassinated loses a card for the assassination (p2 loses a card)
                //person attempting to block the assassination loses a card for losing the challenge (p3 loses a card)
                Label1.Visible = true;
                Label1.Text = "What card did " + DropDownList1.SelectedItem.Text + " lose?";
                AssassinatedPlayerLB.Visible = true;
                AssassinatedPlayerLB.Text = "What card did " + Globals.PlayerBlockingAssassination + " lose?";

                HiddenDDOne.Visible = true;
                populateCardsLeftDropDownList(HiddenDDOne);
                HiddenDDThree.Visible = true;
                populateCardsLeftDropDownList(HiddenDDThree);
                SubmitButton.Visible = true;
            }

            Globals.PlayerAssassinating = Globals.PlayerAssassinating + ":Won";
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            //the person challenging the person blocking an assassination loses
            Label2.Visible = false;
            Label3.Visible = false;
            Label4.Visible = false;
            DropDownList1.Visible = false;
            Button1.Visible = false;
            Button2.Visible = false;
            Globals.GlobalAnalysis.addActions(PlayerDD.SelectedItem.Text + ": lost the challenge");
            //no matter who is doing what, the block holds and only the person challenging loses a card
            //and the person being challenged swaps out their contessa

            Label1.Visible = true;
            Label1.Text = "What card did " + PlayerDD.SelectedItem.Text + " lose?";
            HiddenDDOne.Visible = true;
            populateCardsLeftDropDownList(HiddenDDOne);
            SubmitButton.Visible = true;

            Globals.PlayerAssassinating = Globals.PlayerAssassinating + ":Lost";
        }
    
        private void handleChallengeBlockAssassination()
        {
            //person assassinating -> p1, person being assassinated -> p2, person blocking -> p3, person challenging -> p4
            //p1 -> Globals.PlayerAssassinating
            //p2 -> DropDownList1.SelectedItem.Text
            //p3 -> Globals.PlayerBlockingAssassination
            //p4 -> PlayerDD.SelectedItem.Text
            string[] id = Session["New"].ToString().Split(':');
            string[] p1 = Globals.PlayerAssassinating.Split(':');
            string p2 = DropDownList1.SelectedItem.Text;
            string p3 = Globals.PlayerBlockingAssassination;
            string p4 = PlayerDD.SelectedItem.Text;
            bool isError = false;

            if (((p2 == p3 && p1[0] == p4) || (p2 == p3 && p1[0] != p4)) && Globals.ChallengeStatus == "Won") //case one, challenger won and person being assassinated loses 2 cards
            {
                if (Globals.GlobalAnalysis.getPlayerCardCounts()[Globals.GlobalAnalysis.getPlayerUsernames().IndexOf(p2)] == 2) //if they have two cards
                {
                    string card = HiddenDDOne.SelectedItem.Text;
                    string card2 = HiddenDDTwo.SelectedItem.Text;

                    if((p2.Contains(id[0]) && isBothCardsInHand(card, card2)) || !p2.Contains(id[0]))
                    {
                        Globals.GlobalAnalysis.updatePlayerCardCount(p2, Globals.GlobalAnalysis.getPlayerCardCounts()[Globals.GlobalAnalysis.getPlayerUsernames().IndexOf(p2)] - 2);

                        //if new number of cards is zero do something to kick them out of the game

                        int playerId = getUserId(p2);
                        insertAction(playerId, "Lose Card", "Lost a " + card + " and a " + card2, id[1]);

                        Globals.GlobalAnalysis.addActions(p2 + ": Lost the " + card + " and the " + card2);

                        updateDeadCardCounters(card);
                        updateDeadCardCounters(card2);
                    }
                    else
                    {
                        ErrorLabel.Visible = true;
                        ErrorLabel.Text = "One or both of thoses cards are not in your hand!";
                        isError = true;
                    }
                }
                else if (Globals.GlobalAnalysis.getPlayerCardCounts()[Globals.GlobalAnalysis.getPlayerUsernames().IndexOf(p2)] == 1)  //if they only have one card
                {
                    string card = HiddenDDOne.SelectedItem.Text;
                    if(p2.Contains(id[0]) && isCardInHand(card) || !p2.Contains(id[0]))
                    {
                        Globals.GlobalAnalysis.updatePlayerCardCount(p2, Globals.GlobalAnalysis.getPlayerCardCounts()[Globals.GlobalAnalysis.getPlayerUsernames().IndexOf(p2)] - 1);

                        //if new number of cards is zero do something to kick them out of the game
                        int playerId = getUserId(p2);
                        insertAction(playerId, "Lose Card", "Lost a " + card, id[1]);

                        Globals.GlobalAnalysis.addActions(p2 + ": Lost the " + card);

                        updateDeadCardCounters(card);
                    }
                    else
                    {
                        ErrorLabel.Visible = true;
                        ErrorLabel.Text = "That card is are not in your hand!";
                        isError = true;
                    }
                }
                if(!isError)
                {
                    if(p2 == id[0])
                    {
                        Globals.GlobalAnalysis.getCardsInHand().Clear();
                    }
                    Globals.GlobalAnalysis.getPossibleCard(p2).Clear();
                    Globals.GlobalAnalysis.calculateStatistics();
                    Response.Redirect("GamePlayPage.aspx");
                }
            }
            else if (((p2 != p3 && p1[0] == p4) || (p2 != p3 && p1[0] != p4)) && Globals.ChallengeStatus == "Won") //second case, challenger won and two players loses one card
            {
                string card = HiddenDDOne.SelectedItem.Text;
                string card2 = HiddenDDThree.SelectedItem.Text;
                if ((p2.Contains(id[0]) && isCardInHand(card)) || (p3.Contains(id[0]) && isCardInHand(card2)) || (!p2.Contains(id[0]) && !p3.Contains(id[0])))
                {
                    Globals.GlobalAnalysis.updatePlayerCardCount(p2, Globals.GlobalAnalysis.getPlayerCardCounts()[Globals.GlobalAnalysis.getPlayerUsernames().IndexOf(p2)] - 1);
                    Globals.GlobalAnalysis.updatePlayerCardCount(p3, Globals.GlobalAnalysis.getPlayerCardCounts()[Globals.GlobalAnalysis.getPlayerUsernames().IndexOf(p3)] - 1);

                    //if new number of cards is zero do something to kick them out of the game

                    int playerId = getUserId(p2);
                    insertAction(playerId, "Lose Card", "Lost a " + card, id[1]);

                    int playerIdTwo = getUserId(p3);
                    insertAction(playerIdTwo, "Lose Card", "Lost a " + card2, id[1]);

                    Globals.GlobalAnalysis.addActions(p2 + ": was assassinated and lost the " + card);
                    Globals.GlobalAnalysis.addActions(p3 + ": Lost the " + card2);

                    for (int i = 0; i < Globals.GlobalAnalysis.getPossibleCard(p2).Count; i++)
                    {
                        if (Globals.GlobalAnalysis.getPossibleCard(p2)[i].T == card)
                        {
                            Globals.GlobalAnalysis.getPossibleCard(p2).RemoveAt(i);
                            break;
                        }
                    }

                    for (int i = 0; i < Globals.GlobalAnalysis.getPossibleCard(p3).Count; i++)
                    {
                        if (Globals.GlobalAnalysis.getPossibleCard(p3)[i].T == card)
                        {
                            Globals.GlobalAnalysis.getPossibleCard(p3).RemoveAt(i);
                            break;
                        }
                    }
                    Globals.GlobalAnalysis.calculateStatistics();

                    updateDeadCardCounters(card);
                    updateDeadCardCounters(card2);

                    if (p2 == id[0])
                    {
                        Card c = new Card("Duke");
                        for (int i = 0; i < Globals.GlobalAnalysis.getCardsInHand().Count; i++)
                        {
                            if (Globals.GlobalAnalysis.getCardsInHand()[i].getCardType() == card)
                                c = Globals.GlobalAnalysis.getCardsInHand()[i];
                        }
                        Globals.GlobalAnalysis.getCardsInHand().Remove(c);
                    }
                    else if (p3 == id[0])
                    {
                        Card c = new Card("Duke");
                        for (int i = 0; i < Globals.GlobalAnalysis.getCardsInHand().Count; i++)
                        {
                            if (Globals.GlobalAnalysis.getCardsInHand()[i].getCardType() == card2)
                                c = Globals.GlobalAnalysis.getCardsInHand()[i];
                        }
                        Globals.GlobalAnalysis.getCardsInHand().Remove(c);
                    }
                    Response.Redirect("GamePlayPage.aspx");
                }
                else
                {
                    ErrorLabel.Visible = true;
                    ErrorLabel.Text = "That card is are not in your hand!";
                    isError = true;
                }
            }
            else //third case, challenger loses a card, and other player swaps out contessa
            {
                string card = HiddenDDOne.SelectedItem.Text;
                if ((p4.Contains(id[0]) && isCardInHand(card)) || !p4.Contains(id[0]))
                {
                    Globals.GlobalAnalysis.updatePlayerCardCount(p4, Globals.GlobalAnalysis.getPlayerCardCounts()[Globals.GlobalAnalysis.getPlayerUsernames().IndexOf(p4)] - 1);

                    //if new number of cards is zero do something to kick them out of the game
                    int playerId = getUserId(p4);
                    insertAction(playerId, "Lose Card", "Lost a " + card, id[1]);

                    Globals.GlobalAnalysis.addActions(p4 + ": Lost the " + card);

                    for (int i = 0; i < Globals.GlobalAnalysis.getPossibleCard(p4).Count; i++)
                    {
                        if (Globals.GlobalAnalysis.getPossibleCard(p4)[i].T == card)
                        {
                            Globals.GlobalAnalysis.getPossibleCard(p4).RemoveAt(i);
                            break;
                        }
                    }
                    for (int i = 0; i < Globals.GlobalAnalysis.getPossibleCard(p3).Count; i++)
                    {
                        if (Globals.GlobalAnalysis.getPossibleCard(p3)[i].T == "Contessa")
                        {
                            Globals.GlobalAnalysis.getPossibleCard(p3).RemoveAt(i);
                            break;
                        }
                    }
                    Globals.GlobalAnalysis.calculateStatistics();

                    updateDeadCardCounters(card);
                    if (p1[0] == id[0]) //lose a card
                    {
                        Card c = new Card("Duke");
                        for (int i = 0; i < Globals.GlobalAnalysis.getCardsInHand().Count; i++)
                        {
                            if (Globals.GlobalAnalysis.getCardsInHand()[i].getCardType() == card)
                                c = Globals.GlobalAnalysis.getCardsInHand()[i];
                        }
                        Globals.GlobalAnalysis.getCardsInHand().Remove(c);
                        Response.Redirect("GamePlayPage.aspx");
                    }
                    else if (p3 == id[0]) //swap out the contessa
                    {
                        for (int i = 0; i < Globals.GlobalAnalysis.getCardsInHand().Count; i++)
                        {
                            if (Globals.GlobalAnalysis.getCardsInHand()[i].getCardType() == "Contessa")
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
                        Globals.GlobalAnalysis.addActions(Globals.CorBPlayer + ": Gets new card");
                        Response.Redirect("GamePlayPage.aspx");
                    }
                }
                else
                {
                    ErrorLabel.Visible = true;
                    ErrorLabel.Text = "That card is are not in your hand!";
                    isError = true;
                }
            }
        }
    }
}