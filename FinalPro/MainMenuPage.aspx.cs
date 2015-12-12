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
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if(Session["New"] != null)
            {
                string[] id = Session["New"].ToString().Split(':');
                Session["New"] = id[0];
                Label1.Text = "Welcome " + Session["New"].ToString();

                Globals.Stats = 0.0f;
                Globals.AmbassadorCounter = 0;
                Globals.AssassinCounter = 0;
                Globals.CaptainCounter = 0;
                Globals.ContessaCounter = 0;
                Globals.DukeCounter = 0;
            }
            else
            {
                Response.Redirect("LoginPage.aspx");
            }
        }

        protected void LogOutButton_Click(object sender, EventArgs e)
        {
            Session["New"] = null;
            Response.Redirect("LoginPage.aspx");
        }

        protected void StartGameButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("SetUpGamePage.aspx");
        }

        protected void StatisticsButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("GameStatistics.aspx");
        }
    }
}