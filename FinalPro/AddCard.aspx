<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddCard.aspx.cs" Inherits="FinalPro.AddCard" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Choose A Card<br />
        <asp:DropDownList ID="DeadCardListDD" runat="server">
            <asp:ListItem>Duke</asp:ListItem>
            <asp:ListItem>Assassin</asp:ListItem>
            <asp:ListItem>Captain</asp:ListItem>
            <asp:ListItem>Contessa</asp:ListItem>
            <asp:ListItem>Ambassador</asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
        <asp:Button ID="AddCardButton" runat="server" OnClick="AddCardButton_Click" Text="Add Card" />
    
    </div>
    </form>
</body>
</html>
