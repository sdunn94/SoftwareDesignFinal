<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="LoginPage.aspx.cs" Inherits="FinalPro.LoginPage1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="StyleSheetTest.css" rel="stylesheet"  type="text/css" />

    <title></title>
    <style type="text/css">
        .auto-style3 {
            width: 100%;
        }
        .auto-style4 {
            text-align: right;
            width: 69px;
        }
        .auto-style5 {
            width: 241px;
        }
    </style>
</head>
<body>

    <form id="form1" runat="server">
    
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RegistrationConnectionString %>" SelectCommand="SELECT * FROM [UserDataTable]"></asp:SqlDataSource>
       <div id ="wrapper" style ="text-align:center">  <div id ="myDiv" style="display: inline-block;">
            <strong><h1 class="loginTitle">Login <br />
            </h1></strong>
            <table class="auto-style3">
                <tr>
                    <td class="auto-style4">Username:</td>
                    
                    <td class="auto-style5" style="text-align: left">
                        <asp:TextBox ID="UserNameTB" runat="server" Width="229px"></asp:TextBox>
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="UserNameErrorLB" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4">Pasword:</td>
                    <td class="auto-style5" style="text-align: left">
                        <asp:TextBox ID="PasswordTB" runat="server" TextMode="Password" Width="226px"></asp:TextBox>
                    </td>
                    <td style="text-align: left">
                        <asp:Label ID="PasswordErrorLB" runat="server" ForeColor="Red"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td class="auto-style4">&nbsp;</td>
                    <td class="auto-style5" style="text-align: left">
                        <asp:button ID="LoginButton" class="button" runat="server" OnClick="LoginButton_Click" Text="Login" Height="45px" Width="92px" />
                        &nbsp;<asp:button ID="RegisterButton" class="button" runat="server" OnClick="RegisterButton_Click" Text="Register" Height="43px" Width="117px" />
                    </td>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td class="auto-style4">&nbsp;</td>
                    <td class="auto-style5">&nbsp;</td>
                    <td>&nbsp;</td>
                </tr>
            </table>
            
        </div>
           </div>
    
    </form>
</body>
</html>
