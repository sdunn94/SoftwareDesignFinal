<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="GameStatistics.aspx.cs" Inherits="FinalPro.GameStatistics" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label2" runat="server" Text="Label" Visible="False"></asp:Label>
        <br />
        <asp:Label ID="Label1" runat="server" Text="Choose a game to see the history of: "></asp:Label>
&nbsp;
        <asp:DropDownList ID="DropDownList1" runat="server" AutoPostBack="True" DataSourceID="SqlDataSource1" DataTextField="Id" DataValueField="Id" OnSelectedIndexChanged="DropDownList1_SelectedIndexChanged">
        </asp:DropDownList>
        <asp:SqlDataSource ID="SqlDataSource1" runat="server" ConnectionString="<%$ ConnectionStrings:RegistrationConnectionString %>" SelectCommand="SELECT [Id] FROM [GameDataTable] WHERE ([UserId] = @UserId)">
            <SelectParameters>
                <asp:ControlParameter ControlID="Label2" Name="UserId" PropertyName="Text" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        <br />
        <br />
        <asp:Label ID="Label3" runat="server" Text="The Winner of the game was: "></asp:Label>
        <br />
        <br />
        <asp:ListBox ID="ListBox1" runat="server" DataSourceID="SqlDataSource2" DataTextField="Player" DataValueField="Player" Height="446px" Width="78px"></asp:ListBox>
        <asp:ListBox ID="ListBox2" runat="server" DataSourceID="SqlDataSource2" DataTextField="ActionType" DataValueField="ActionType" Height="446px" style="margin-top: 0px" Width="207px"></asp:ListBox>
        <asp:ListBox ID="ListBox3" runat="server" DataSourceID="SqlDataSource2" DataTextField="Action" DataValueField="Action" Height="446px" Width="212px"></asp:ListBox>
&nbsp;<asp:SqlDataSource ID="SqlDataSource2" runat="server" ConnectionString="<%$ ConnectionStrings:RegistrationConnectionString %>" SelectCommand="SELECT [Player], [Action], [ActionType] FROM [ActionDataTable] WHERE ([GameId] = @GameId)">
            <SelectParameters>
                <asp:ControlParameter ControlID="DropDownList1" Name="GameId" PropertyName="SelectedValue" Type="Int32" />
            </SelectParameters>
        </asp:SqlDataSource>
        <br />
        <br />
        <asp:Button ID="Button1" runat="server" Text="Return To Main Menu" />
    
    </div>
    </form>
    <p>
        &nbsp;</p>
</body>
</html>
