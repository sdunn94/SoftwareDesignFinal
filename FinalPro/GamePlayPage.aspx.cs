using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace FinalPro
{
    public partial class GamePlayPage : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void SwapCardsButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("SwapCardsInHand.aspx");
        }

        protected void AddDeckCardButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddCard.aspx");
        }

        protected void AddDeadCardButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("AddCard.aspx");
        }

        protected void RemoveDeckCardButton_Click(object sender, EventArgs e)
        {
            Response.Redirect("RemoveCard.aspx");
        }
    }
}