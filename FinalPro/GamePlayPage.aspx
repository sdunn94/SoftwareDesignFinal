<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GamePlayPage.aspx.cs" Inherits="FinalPro.GamePlayPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Panel ID="Panel1" runat="server" Height="143px">
            Player Probabilities</asp:Panel>
        <br />
        Actions</div>
        <asp:ListBox ID="ActionListBox" runat="server" Height="356px" Width="637px"></asp:ListBox>
        <asp:Panel ID="Panel2" runat="server" Height="216px">
            Your Cards&nbsp;
            <asp:Button ID="SwapCardsButton" runat="server" Text="Swap" OnClick="SwapCardsButton_Click" />
            <br />
            <br />
            <br />
            <br />
            Deck Cards&nbsp;
            <asp:Button ID="AddDeckCardButton" runat="server" Text="Add" OnClick="AddDeckCardButton_Click" />
            &nbsp;
            <asp:Button ID="RemoveDeckCardButton" runat="server" Text="Remove" OnClick="RemoveDeckCardButton_Click" />
            <br />
            <br />
            <br />
            <br />
            Dead Cards&nbsp;
            <asp:Button ID="AddDeadCardButton" runat="server" Text="Add" OnClick="AddDeadCardButton_Click" />
            <br />
            <br />
        </asp:Panel>
    </form>
</body>
</html>
