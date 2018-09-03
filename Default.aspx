<%@ Page Title="Home Page" Language="C#" AutoEventWireup="true" CodeBehind="Default.aspx.cs" Inherits="WebApplication1._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Memeber Mannagement</title>
    <link rel="Stylesheet" href="mystyle.css" type="text/css" />
    <style type="text/css">
        .auto-style1 {
            margin: 10px;
            padding: 10px;
            font-size: 14px;
            display: block;
            float: inherit;
            background-image: url('pic/bglogin.png');
            background-position: center;
            background-repeat: repeat-x;
            border-style: solid;
            border-width: 3px;
            width: 47%;
            height: 90%;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
            <div class="auto-style1" align="center">
                Team:
                <asp:DropDownList ID="ddlTeam" runat="server" AutoPostBack="True" OnSelectedIndexChanged="ddlTeam_SelectedIndexChanged"></asp:DropDownList>
                &nbsp;
                <br />
                Members:
                <asp:DropDownList ID="ddlMember" runat="server" OnSelectedIndexChanged="ddlMember_SelectedIndexChanged">
                </asp:DropDownList>
                <br />
                <br />
                <p>
            <asp:Label ID="Results" runat="server" EnableViewState="False"
                Visible="False"></asp:Label>
        </p>
               <asp:Button ID="SelectPerson" runat="server" OnClick="btnSel_Click" Text="Select"/>
                &nbsp;
               <asp:Button ID="AddNewPerson" runat="server" Text="Add" OnClick="btnAdd_Click" Visible="false" OnClientClick="return confirm('Confirm Registration?')" />
                &nbsp;
               <asp:Button ID="DeleteSelectedPerson" runat="server" Text="Delete" Visible="false" OnClick="btnDel_Click"/>
        &nbsp;<br />
                <asp:Panel ID="Panel1" runat="server" Height="127px" Visible="false">
                    Member Mannagement<br />
                    <br />
                    username<asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
                    <br />
                    password<asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                    <br />
                    <br />
                    <asp:Button ID="Button1" runat="server" OnClick="Button1_Click" Text="submit" />

                    <asp:Button ID="Button2" runat="server" Text="Register" OnClick="Button2_Click" />
                </asp:Panel>
                <asp:Panel ID="Panel2" runat="server" Visible="False">
                    <asp:Label ID="Label1" runat="server" Text="Label"></asp:Label>
                    <br />
                    <asp:Button ID="Button3" runat="server" OnClick="Button3_Click" Text="Modify password" />
                    <br />
                    <br />
                    <input type="button" onclick="window.location = 'Register.aspx'" value="Register new user" />
                </asp:Panel>
            </div>

            <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None">
            <AlternatingRowStyle BackColor="White" />
            <EditRowStyle BackColor="#7C6F57" />
            <FooterStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <HeaderStyle BackColor="#1C5E55" Font-Bold="True" ForeColor="White" />
            <PagerStyle BackColor="#666666" ForeColor="White" HorizontalAlign="Center" />
            <RowStyle BackColor="#E3EAEB" />
            <SelectedRowStyle BackColor="#C5BBAF" Font-Bold="True" ForeColor="#333333" />
            <SortedAscendingCellStyle BackColor="#F8FAFA" />
            <SortedAscendingHeaderStyle BackColor="#246B61" />
            <SortedDescendingCellStyle BackColor="#D4DFE1" />
            <SortedDescendingHeaderStyle BackColor="#15524A" />
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        Select  
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="CheckItem" runat="server" />
                        <asp:HiddenField ID="HidID" runat="server" Value='<%# Eval("UserID") %> ' />
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>
    </form>
</body>
</html>

