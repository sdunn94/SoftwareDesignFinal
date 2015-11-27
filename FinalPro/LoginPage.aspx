<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="FinalPro.LoginPage1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .auto-style1 {
            text-align: center;
        }
        .auto-style2 {
            font-size: xx-large;
            text-align: left;
        }
        .auto-style3 {
            width: 100%;
        }
        .auto-style4 {
            text-align: right;
            width: 202px;
        }
        .auto-style5 {
            width: 127px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="height: 402px; width: 627px" class="auto-style1">
    
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RegistrationConnectionString %>" SelectCommand="SELECT * FROM [UserDataTable]"></asp:SqlDataSource>
        <div class="auto-style1">
            <strong><span class="auto-style2">Login<br />
            </span></strong>
            <table class="auto-style3">
                <tr>
                    <td class="auto-style4">Username:</td>
                    <strong>
                    <td class="auto-style5" style="text-align: left">
                        <asp:TextBox ID="UserNameTB" runat="server"></asp:TextBox>
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="UserNameErrorLB" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4">Pasword:</td>
                    <td class="auto-style5" style="text-align: left">
                        <asp:TextBox ID="PasswordTB" runat="server" TextMode="Password"></asp:TextBox>
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="PasswordErrorLB" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4">&nbsp;</td>
                    <td class="auto-style5" style="text-align: left">
                        <asp:Button ID="LoginButton" runat="server" OnClick="LoginButton_Click" Text="Login" />
                        <br />
                        <br />
                        <asp:Button ID="RegisterButton" runat="server" OnClick="RegisterButton_Click" Text="Register" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style4">&nbsp;</td>
                    <td class="auto-style5">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            </strong>
        </div>
    
    </div>
    </form>
</body>
</html>
