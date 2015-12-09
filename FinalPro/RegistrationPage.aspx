<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="RegistrationPage.aspx.cs" Inherits="FinalPro.LoginPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="StyleSheetTest.css" rel="stylesheet" />
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
            height: 145px;
            margin-bottom: 0px;
        }
        .auto-style2 {
            width: 466px;
        }
        #Reset1 {
            width: 106px;
        }
        .auto-style5 {
            text-align: center;
        }
        .auto-style6 {
            font-size: large;
        }
        .auto-style7 {
            width: 218px;
        }
        .auto-style8 {
            width: 218px;
            text-align: center;
        }
        .auto-style9 {
            width: 466px;
            text-align: right;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>

        <div class="auto-style5">
            <strong><span class="auto-style6">Registration Page</span></strong><br />
        <br />
        </div>
        <table class="auto-style1">
            <tr>
                <td class="auto-style9">Username:</td>
                <td class="auto-style7">
                    <asp:TextBox ID="UserNameTB" runat="server" Width="178px"></asp:TextBox>
                </td>
                <td class="auto-style5">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="UserNameTB" ErrorMessage="Username Required" ForeColor="Red" style="text-align: left"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style9">Password:</td>
                <td class="auto-style7">
                    <asp:TextBox ID="PasswordTB" runat="server" TextMode="Password" Width="180px"></asp:TextBox>
                </td>
                <td class="auto-style5">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" ControlToValidate="PasswordTB" ErrorMessage="Password Required" ForeColor="Red"></asp:RequiredFieldValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style9">Confirm Password:</td>
                <td class="auto-style7">
                    <asp:TextBox ID="ConfirmPasswordTB" runat="server" TextMode="Password" Width="180px"></asp:TextBox>
                </td>
                <td class="auto-style5">
                    <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="ConfirmPasswordTB" ErrorMessage="Confirm Password Required" ForeColor="Red"></asp:RequiredFieldValidator>
                    <br />
                    <asp:CompareValidator ID="CompareValidator1" runat="server" ControlToCompare="PasswordTB" ControlToValidate="ConfirmPasswordTB" ErrorMessage="Both Password must be same" ForeColor="Red"></asp:CompareValidator>
                </td>
            </tr>
            <tr>
                <td class="auto-style9">&nbsp;</td>
                <td class="auto-style8">
                    <asp:Button ID="SubmitButton" class="button" runat="server" OnClick="SubmitButton_Click" Text="Submit" />
                    <br />
                    <br />
                    <input id="Reset1" class =" button" type="reset" value="reset" /></td>
                <td class="auto-style5">&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style7">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style7">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td class="auto-style2">&nbsp;</td>
                <td class="auto-style7">&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    
    </div>
    </form>
</body>
</html>
