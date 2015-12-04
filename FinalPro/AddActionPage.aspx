<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="AddActionPage.aspx.cs" Inherits="FinalPro.AddActionPage" %>

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
    
        New Action<br />
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RegistrationConnectionString %>" SelectCommand="SELECT * FROM [UserDataTable]"></asp:SqlDataSource>
        <br />
        <table class="auto-style1">
            <tr>
                <td>Player</td>
                <td>Action Type</td>
                <td>Action</td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td>
                    <asp:DropDownList ID="PlayerDD" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="ActionTypeDD" runat="server" OnSelectedIndexChanged="ActionTypeDD_SelectedIndexChanged" AutoPostBack="True">
                        <asp:ListItem>Select Action Type</asp:ListItem>
                        <asp:ListItem>Action</asp:ListItem>
                        <asp:ListItem>Counteraction</asp:ListItem>
                        <asp:ListItem>Challenge</asp:ListItem>
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:DropDownList ID="ActionDD" runat="server" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>
        </table>
    
        <br />
        <asp:Button ID="AddButton" runat="server" OnClick="AddButton_Click" Text="Add" />
        <br />
    
    </div>
        <asp:Panel ID="Panel1" runat="server" Height="211px">
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
            <br />
            <asp:Button ID="NoButton" runat="server" OnClick="NoButton_Click" Text="No" Visible="False" Width="37px" />
            &nbsp;<br />
            <asp:Button ID="YesButtonForAssassination" runat="server" OnClick="YesButtonForAssassination_Click" Text="Yes" Visible="False" />
            &nbsp;<asp:Button ID="NoButtonForAssassination" runat="server" OnClick="NoButtonForAssassination_Click" Text="No" Visible="False" />
        </asp:Panel>
    </form>
</body>
</html>
