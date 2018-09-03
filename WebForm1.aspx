<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm1.aspx.cs" Inherits="WebApplication1.WebForm1" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>在Web应用程序中执行计划任务的例子</title>
    <style type="text/css">
        .auto-style1 {
            height: 26px;
        }
    </style>
</head>
<body style="background-color: #e5eecc; text-align: center;">
    <script type="text/javascript" src="js/calendar.js"></script>
    <form id="form1" runat="server" method="post">
        <div>
            <asp:Label ID="label1" runat="server">Welcome , </asp:Label>
            <asp:Label ID="label2" runat="server"></asp:Label>
            <br />
            <br />
            <asp:Panel runat="server">
                Name:
            <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
                &nbsp;Content:
            <asp:TextBox ID="TextBox1" runat="server" OnClick="this,value=''" Text="Replied"></asp:TextBox>
            </asp:Panel>

            <br />
            StartTime:   
            <asp:TextBox ID="TextBox3" runat="server" onfocus="this.blur()" onclick="SelectDate(this)"></asp:TextBox>


            &nbsp;EndTime:
             <asp:TextBox ID="TextBox4" runat="server" onfocus="this.blur()" onclick="SelectDate(this)"></asp:TextBox>
            <br />
            <br />
            <asp:Button ID="Button1" runat="server" Text="Select" OnClick="btnSelect_Click" BackColor="#CCFFFF" BorderStyle="Outset" BorderWidth="0px" Font-Names="Tahoma" Font-Size="Large" Height="48px" Width="102px" />
            &nbsp;
            <asp:Button ID="Button2" runat="server" Text="Reset" OnClick="btnReset_Click" BackColor="#CCFFFF" BorderStyle="Outset" BorderWidth="0px" Font-Names="Tahoma" Font-Size="Large" Height="48px" Width="102px" />
            &nbsp;
            <asp:Button ID="btn_return" runat="server" Text="Return" OnClick="btnReturn_Click" BackColor="#CCFFFF" BorderStyle="Outset" BorderWidth="0px" Font-Names="Tahoma" Font-Size="Large" Height="48px" Width="102px" />
             <h3>&nbsp;Calendar SelectionChanged&nbsp;&nbsp; </h3>

            Select a day, week, or month on the Calendar control.<br />
            <asp:RadioButton ID="RadioButton_Month" runat="server" Text="Month" GroupName="rbd" />
            <asp:RadioButton ID="RadioButton_Week" runat="server" Text="Week" GroupName="rbd" />
            <asp:RadioButton ID="RadioButton_Day" runat="server" Text="Day" GroupName="rbd" />
            <br />

            <asp:Calendar ID="Calendar1" runat="server" OnSelectionChanged="Calendar_SelectionChanged" SelectionMode="DayWeekMonth" BackColor="White" BorderColor="White" BorderWidth="1px" Font-Names="Verdana" Font-Size="9pt" ForeColor="Black" Height="190px" NextPrevFormat="FullMonth" Width="350px" align="center">
                <DayHeaderStyle Font-Bold="True" Font-Size="8pt" />
                <NextPrevStyle Font-Bold="True" Font-Size="8pt" ForeColor="#333333" VerticalAlign="Bottom" />
                <OtherMonthDayStyle ForeColor="#999999" />
                <SelectedDayStyle BackColor="#333399" ForeColor="White" />
                <TitleStyle BackColor="White" BorderColor="Black" BorderWidth="4px" Font-Bold="True" Font-Size="12pt" ForeColor="#333399" />
                <TodayDayStyle BackColor="#CCCCCC" />
            </asp:Calendar>
            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
            <hr />
            <table border="1" align="center">

                <tr style="background-color: silver">

                    <th class="auto-style1">Selected Dates:

                    </th>
                </tr>

                <tr>

                    <td>

                        <asp:Label ID="Message"
                            Text="No dates selected."
                            runat="server" />

                    </td>

                </tr>

            </table>

        </div>
        <asp:GridView ID="GridView1" runat="server" CellPadding="4" ForeColor="#333333" GridLines="None" OnSelectedIndexChanged="GridView1_SelectedIndexChanged" OnRowDataBound="GridView1_RowDataBound">
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

        <p>
            <asp:Label ID="DeleteResults" runat="server" EnableViewState="False"
                Visible="False"></asp:Label>
        </p>
        <asp:Button ID="Button3" runat="server" Text="Add" OnClick="btnAdd_Click" Enabled="false" OnClientClick="return confirm('Confirm Registration?')" BackColor="#CCFFFF" BorderStyle="Outset" BorderWidth="0px" Font-Names="Tahoma" Font-Size="Large" Height="48px" Width="102px" Visible="false" />
        &nbsp;
            <asp:Button ID="DeleteSelectedPerson" runat="server" Text="Delete" OnClick="btnDel_Click" BackColor="#CCFFFF" BorderStyle="Outset" BorderWidth="0px" Font-Names="Tahoma" Font-Size="Large" Height="48px" Width="102px" />
    </form>
</body>
</html>

