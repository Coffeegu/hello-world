<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CalendarForm3.aspx.cs" Inherits="WebApplication1.CalendarForm3" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
<asp:calendar id="Calendar1" style="Z-INDEX: 101; LEFT: 32px; POSITION: absolute; TOP: 16px" runat="server" 
    Height="390px" Width="440px" BorderWidth="1px" BackColor="#FFFFCC" DayNameFormat="Full" ForeColor="#663399" 
    Font-Size="8pt" Font-Names="Verdana" BorderColor="#FFCC66" ShowGridLines="True" PrevMonthText="上个月&amp;lt;&amp;lt;" 
    NextMonthText="下个月&amp;gt;&amp;gt;" OnSelectionChanged="Calendar1_SelectionChanged"> 
    <TodayDayStyle ForeColor="#00C000" BackColor="Khaki"></TodayDayStyle> 
    <SelectorStyle BackColor="#FFCC66"></SelectorStyle> 
    <NextPrevStyle Font-Size="9pt" ForeColor="#FFFFCC"></NextPrevStyle> 
    <DayHeaderStyle Height="1px" BackColor="#FFCC66"></DayHeaderStyle> 
    <SelectedDayStyle Font-Bold="True" BackColor="MediumPurple"></SelectedDayStyle> 
    <TitleStyle Font-Size="9pt" Font-Bold="True" ForeColor="#FFFFCC" BackColor="#990000"></TitleStyle> 
    <OtherMonthDayStyle ForeColor="#CC9966"></OtherMonthDayStyle> 
   </asp:calendar> 
   <asp:TextBox id="TextBox1" style="Z-INDEX: 102; LEFT: 32px; POSITION: absolute; TOP: 416px" runat="server" 
    Visible="False"></asp:TextBox> 
   <asp:Button id="Button1" style="Z-INDEX: 103; LEFT: 216px; POSITION: absolute; TOP: 412px" runat="server" 
    Text="确 定" BorderColor="SteelBlue" ForeColor="White" BackColor="SteelBlue" Width="81px" 
    Height="30px"></asp:Button>
    </form>
</body>
</html>
