<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddActionPage.aspx.cs" Inherits="FinalPro.AddActionPage" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <link href="StyleSheetTest.css" rel="stylesheet" />
    <title></title>
    <style type="text/css">
        .auto-style1 {
            width: 100%;
        }
        .auto-style2 {
            text-align: center;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div style="text-align: center">
    
        New Action<br />
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RegistrationConnectionString %>" SelectCommand="SELECT * FROM [UserDataTable]"></asp:SqlDataSource>
        <br />
        <table class="auto-style1">
            <tr>
                <td style="text-align: center">Player</td>
                <td style="text-align: center">Action Type</td>
                <td style="text-align: center">Action</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td style="text-align: center">
                    <asp:DropDownList ID="PlayerDD" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td style="text-align: center">
                    <asp:DropDownList ID="ActionTypeDD" runat="server" OnSelectedIndexChanged="ActionTypeDD_SelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem>Select Action Type</asp:ListItem>
                        <asp:ListItem>Action</asp:ListItem>
                        <asp:ListItem>Counteraction</asp:ListItem>
                        <asp:ListItem>Challenge</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td style="text-align: center">
                    <asp:DropDownList ID="ActionDD" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    
        <br />
        <asp:Button ID="AddButton" class="button" runat="server" OnClick="AddButton_Click" Text="Add" />
        <br />
    
    </div>
        <asp:Panel ID="Panel1" runat="server" Height="211px">
            <div class="auto-style2">
                <asp:Label ID="Label1" runat="server" Text="Did someone counteract or challenge?" Visible="False"></asp:Label>
                &nbsp;<asp:DropDownList ID="HiddenDDOne" runat="server" Visible="False">
                </asp:DropDownList>
                &nbsp;<asp:Label ID="AssassinatedPlayerLB" runat="server"></asp:Label>
                &nbsp;<asp:DropDownList ID="HiddenDDThree" runat="server" Visible="False">
                </asp:DropDownList>
                <asp:DropDownList ID="HiddenDDTwo" runat="server" Visible="False">
                </asp:DropDownList>
                &nbsp;
                <asp:Button ID="SubmitButton" runat="server" OnClick="SubmitButton_Click" Text="Submit" Visible="False" />
                <br />
                <asp:Button ID="YesButton" runat="server" OnClick="YesButton_Click" Text="Yes" Visible="False" />
                &nbsp;<asp:Button ID="ChallengeSubmitButton" runat="server" OnClick="ChallengeSubmitButton_Click" Text="Submit" Visible="False" />
                <asp:Label ID="ErrorLabel" runat="server" ForeColor="Red" Visible="False"></asp:Label>
                <asp:Button ID="SubmitChallengeBlockSteal" runat="server" OnClick="SubmitChallengeBlockSteal_Click" Text="Submit" Visible="False" />
                <br />
                <asp:Button ID="NoButton" runat="server" OnClick="NoButton_Click" Text="No" Visible="False" Width="37px" />
                &nbsp;<br />
                <asp:Button ID="YesButtonForAssassination" runat="server" OnClick="YesButtonForAssassination_Click" Text="Yes" Visible="False" />
                &nbsp;<asp:Button ID="NoButtonForAssassination" runat="server" OnClick="NoButtonForAssassination_Click" Text="No" Visible="False" />
                <br />
                <asp:Label ID="Label2" runat="server" Text="Label" Visible="False"></asp:Label>
                <br />
                <asp:Label ID="Label3" runat="server" Text="Label" Visible="False"></asp:Label>
                &nbsp;<asp:DropDownList ID="DropDownList1" runat="server" Visible="False">
                </asp:DropDownList>
                <br />
                <asp:Label ID="Label4" runat="server" Text="Label" Visible="False"></asp:Label>
                &nbsp;<asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="Yes" Visible="False" />
                &nbsp;<asp:Button ID="Button2" runat="server" OnClick="Button2_Click" Text="No" Visible="False" />
            </div>
        </asp:Panel>
    </form>
</body>
</html>
