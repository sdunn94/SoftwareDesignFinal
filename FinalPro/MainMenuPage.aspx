<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="MainMenuPage.aspx.cs" Inherits="FinalPro.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        &nbsp;<asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
        <asp:Button ID="LogOutButton" runat="server" OnClick="LogOutButton_Click" Text="Logout" />
        <br />
        <br />
        What would you like to do?<br />
        <br />
        Start A New Game:<br />
        <asp:Button ID="StartGameButton" runat="server" Text="Start Game" OnClick="StartGameButton_Click" />
        <br />
        <br />
        Review Stats for Previous Games:<br />
        <asp:Button ID="StatisticsButton" runat="server" Text="Statistics" OnClick="StatisticsButton_Click" />
    
    </div>
    </form>
</body>
</html>
