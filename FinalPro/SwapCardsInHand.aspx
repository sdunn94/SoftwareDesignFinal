<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SwapCardsInHand.aspx.cs" Inherits="FinalPro.SwapCardsInHand" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        Your Cards<br />
        <table class="auto-style1">
            <tr>
                <td>
                    <asp:Image ID="Image1" runat="server" />
                </td>
                <td>
                    <asp:Image ID="Image2" runat="server" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:RadioButton ID="CardOne" runat="server" />
                </td>
                <td>
                    <asp:RadioButton ID="CardTwo" runat="server" />
                </td>
            </tr>
        </table>
    
    </div>
        <p>
            New Card</p>
        <p>
            <asp:DropDownList ID="DropDownList1" runat="server">
            </asp:DropDownList>
        </p>
        <p>
            <asp:Button ID="SwapButton" runat="server" Text="Swap" OnClick="SwapButton_Click" />
        </p>
    </form>
</body>
</html>
