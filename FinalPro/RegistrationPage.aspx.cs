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
    public partial class LoginPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            //if(IsPostBack)
            //{
            //    SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString);
            //    conn.Open();
            //    string checkUser = "SELECT count(*) FROM UserDataTable WHERE Username ='" + UserNameTB.Text + "'";
            //    SqlCommand com = new SqlCommand(checkUser, conn);
            //    int numUsers = Convert.ToInt32(com.ExecuteScalar().ToString());

            //    if(numUsers > 0)
            //    {
            //        Response.Write("User already exists");
            //    }
            //    conn.Close();
            //}
        }

        protected void SubmitButton_Click(object sender, EventArgs e)
        {
            try
            {
                SqlConnection conn = new SqlConnection(ConfigurationManager.ConnectionStrings["RegistrationConnectionString"].ConnectionString);
                conn.Open();

                string checkUser = "SELECT count(*) FROM UserDataTable WHERE Username ='" + UserNameTB.Text + "'";
                SqlCommand com1 = new SqlCommand(checkUser, conn);
                int numUsers = Convert.ToInt32(com1.ExecuteScalar().ToString());

                if (numUsers > 0)
                {
                    Response.Write("User already exists");
                }
                else
                {
                    string insertUser = "INSERT INTO UserDataTable (Username, Password) VALUES (@un, @pw)";
                    SqlCommand com = new SqlCommand(insertUser, conn);
                    com.Parameters.AddWithValue("@un", UserNameTB.Text);
                    com.Parameters.AddWithValue("@pw", PasswordTB.Text);
                    com.ExecuteNonQuery();
                    Session["New"] = UserNameTB.Text;
                    Response.Redirect("MainMenuPage.aspx");
                }
                conn.Close();
            }
            catch(Exception ex)
            {
                Response.Write("Error:" + ex.ToString());
            }
        }

    }
}