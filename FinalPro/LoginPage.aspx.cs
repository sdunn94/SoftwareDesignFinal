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
    public partial class LoginPage1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            
        }

        protected void RegisterButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("RegistrationPage.aspx");
        }

        protected void LoginButton_Click(object sender, EventArgs e)
        {
            PasswordErrorLB.Text = "";
            UserNameErrorLB.Text = "";
            SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString);
            conn.Open();
            string checkUser = "SELECT count(*) FROM UserDataTable WHERE Username ='" + UserNameTB.Text + "'";
            SqlCommand com = new SqlCommand(checkUser, conn);
            int numUsers = Convert.ToInt32(com.ExecuteScalar().ToString());
            conn.Close();
            if (numUsers == 1)
            {
                conn.Open();
                string checkPassword = "SELECT Password FROM UserDataTable WHERE Username ='" + UserNameTB.Text + "'";
                SqlCommand com2 = new SqlCommand(checkPassword, conn);
                string password = com2.ExecuteScalar().ToString().Replace(" ", "");
                conn.Close();

                if(password == PasswordTB.Text)
                {
                    Session["New"] = UserNameTB.Text;
                    Response.Redirect("MainMenuPage.aspx");
                }
                else
                {
                    PasswordErrorLB.Text = "You Must Enter a Correct Password";
                }
            }
            else
            {
                UserNameErrorLB.Text = "You Must Enter a Correct Username";
            }
        }
    }
}