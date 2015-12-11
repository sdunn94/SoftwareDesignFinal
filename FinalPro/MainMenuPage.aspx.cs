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