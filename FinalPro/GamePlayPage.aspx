<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GamePlayPage.aspx.cs" Inherits="FinalPro.GamePlayPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            width: 639px;
        }
        .auto-style3 {
            text-decoration: underline;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Panel ID="Panel1" runat="server" Height="143px">
            Player Probabilities<br />
            <table class="auto-style1">
                <tr>
                    <td>
                        <asp:Label ID="PlayerOneLB" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="PlayerTwoLB" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="PlayerThreeLB" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="PlayerFourLB" runat="server"></asp:Label>
                    </td>
                    <td>
                        <asp:Label ID="PlayerFiveLB" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
        </asp:Panel>
        <table class="auto-style1">
            <tr>
                <td class="auto-style2">Actions                 <br />
        <asp:ListBox ID="ActionListBox" runat="server" Height="356px" Width="637px"></asp:ListBox>
                </td>
                <td>Your Cards
            <asp:Button ID="SwapCardsButton" runat="server" Text="Swap" OnClick="SwapCardsButton_Click" />
                    <br />
                    <asp:Label ID="NumYourCardsLB" runat="server" Text="Label"></asp:Label>
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <br />
                    <span class="auto-style3"><strong>Dead Cards</strong></span><br />
        Duke:
        <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
        <br />
        Contessa:
        <asp:Label ID="Label2" runat="server" Text="Label"></asp:Label>
        <br />
        Captain:
        <asp:Label ID="Label3" runat="server" Text="Label"></asp:Label>
        <br />
        Assassin:
        <asp:Label ID="Label4" runat="server" Text="Label"></asp:Label>
        <br />
        Ambassador:
        <asp:Label ID="Label5" runat="server" Text="Label"></asp:Label>
                </td>
            </tr>
        </table>
        <br />
        <asp:Button ID="AddActionButton" runat="server" OnClick="AddActionButton_Click" Text="New Action" />
        <br />
        </div>
    </form>
</body>
</html>
