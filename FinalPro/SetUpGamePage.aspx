<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SetUpGamePage.aspx.cs" Inherits="FinalPro.SetUpGamePage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Set Up New Game<br />
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RegistrationConnectionString %>" SelectCommand="SELECT * FROM [UserDataTable]"></asp:SqlDataSource>
        <br />
        Players<br />
        <asp:DropDownList ID="PlayerOneDD" runat="server" DataSourceID="SqlDataSource1" DataTextField="Username" DataValueField="Id">
        </asp:DropDownList>
&nbsp;&nbsp;
        <asp:DropDownList ID="PlayerTwoDD" runat="server" DataSourceID="SqlDataSource1" DataTextField="Username" DataValueField="Id">
        </asp:DropDownList>
&nbsp;&nbsp;
        <asp:DropDownList ID="PlayerThreeDD" runat="server" DataSourceID="SqlDataSource1" DataTextField="Username" DataValueField="Id">
        </asp:DropDownList>
&nbsp;&nbsp;
        <asp:DropDownList ID="PlayerFourDD" runat="server" DataSourceID="SqlDataSource1" DataTextField="Username" DataValueField="Id">
        </asp:DropDownList>
&nbsp;&nbsp;
        <asp:DropDownList ID="PlayerFiveDD" runat="server" DataSourceID="SqlDataSource1" DataTextField="Username" DataValueField="Id">
        </asp:DropDownList>
        <br />
        <br />
        <asp:Button ID="AddNewPlayerButton" runat="server" Text="Add New Player" OnClick="AddNewPlayerButton_Click" />
        <br />
        <br />
        Your Cards<br />
        <asp:DropDownList ID="CardOneDD" runat="server">
            <asp:ListItem>Select Card</asp:ListItem>
            <asp:ListItem>Duke</asp:ListItem>
            <asp:ListItem>Assassin</asp:ListItem>
            <asp:ListItem>Ambassador</asp:ListItem>
            <asp:ListItem>Captain</asp:ListItem>
            <asp:ListItem>Contessa</asp:ListItem>
        </asp:DropDownList>
&nbsp;&nbsp;
        <asp:DropDownList ID="CardTwoDD" runat="server">
            <asp:ListItem>Select Card</asp:ListItem>
            <asp:ListItem>Duke</asp:ListItem>
            <asp:ListItem>Assassin</asp:ListItem>
            <asp:ListItem>Ambassador</asp:ListItem>
            <asp:ListItem>Captain</asp:ListItem>
            <asp:ListItem>Contessa</asp:ListItem>
        </asp:DropDownList>
        <br />
        <br />
        <asp:Button ID="Button2" runat="server" Text="Start Game" OnClick="StartGameButton_Click" />
    
    </div>
    </form>
</body>
</html>
