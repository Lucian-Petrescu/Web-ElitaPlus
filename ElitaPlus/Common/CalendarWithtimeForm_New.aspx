<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CalendarWithtimeForm_New.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.CalendarWithtimeForm_New" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head id="Head1" runat="server">
    <title>Choose a date & time</title>
    <asp:literal id="Literal1" runat="server"></asp:literal>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR"/>
	<meta content="Visual Basic 7.0" name="CODE_LANGUAGE"/>
	<meta content="JavaScript" name="vs_defaultClientScript"/>
	<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
    <link rel="stylesheet" type="text/css" href="../App_Themes/Default/Default.css"/>
    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>
</head>
<body>
    <form id="Form1" method="post" runat="server">
    <table id="Table2" border="0" cellpadding="0" cellspacing="0" class="EcalDate" width="250">
        <tr>
            <td nowrap align="right">
                <asp:Label ID="LabelMonth" runat="server" Width="40px">Month</asp:Label>:
            </td>
            <td>
                <asp:DropDownList ID="cboMonthList" runat="server" Width="55px" AutoPostBack="True"  CssClass="date">
                </asp:DropDownList>
            </td>
            <td nowrap align="right">
                <asp:Label ID="LabelYear" runat="server" Width="40px">Year</asp:Label>:
            </td>
            <td>
                <asp:DropDownList ID="cboYearList" runat="server" Width="55px" AutoPostBack="True"  CssClass="date">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    <table id="Table1" cellspacing="0" cellpadding="0" width="250" border="0" >
        <tr>
            <td>
                <asp:Calendar ID="MyCalendar" runat="server" Width="250px" Height="120px" ShowGridLines="True" CssClass="Ecal"
                    NextMonthText="<img runat='server' id='btnNextMonth' width='12' Height='10' src='../App_Themes/Default/Images/right.gif' border='0'>"
                    PrevMonthText="<img runat='server' id='btnPrevMonth' width='12' Height='10' src='../App_Themes/Default/Images/left.gif' border='0'>">
                    <TitleStyle CssClass="TitleStyle"/>
                    <OtherMonthDayStyle CssClass="OtherMonthDayStyle"/> 
                    <WeekendDayStyle CssClass="WeekendDayStyle" />  
                    <DayStyle CssClass="DayStyle"/>    
                    <TodayDayStyle CssClass="TodayDayStyle"/>   
                    <SelectedDayStyle CssClass="SelectedDayStyle" />    
                </asp:Calendar>
            </td>
        </tr>
    </table>
    <table id="Table3" cellspacing="0" cellpadding="0" width="250" class="EcalTime">
        <tr>
            <td align="center">
                <asp:Label ID="lblhour" Width="100%" runat="server">HOUR</asp:Label>
            </td>
            <td align="center">
                <asp:Label ID="lblminute" Width="100%" runat="server">MINUTE</asp:Label>
            </td>
            <td align="center">
                <asp:Label ID="lblSecond" Width="100%" runat="server">SECOND</asp:Label>
            </td>
            <td align="center">
            </td>
        </tr>
        <tr>
            <td align="center">
                <asp:DropDownList ID="ddlhour" runat="server" Width="50px" SkinID="SmallDropDown">
                </asp:DropDownList>
            </td>
            <td align="center">
                <asp:DropDownList ID="ddlMinutes" runat="server" Width="50px" SkinID="SmallDropDown">
                </asp:DropDownList>
            </td>
            <td align="center">
                <asp:DropDownList ID="ddlSeconds" runat="server" Width="50px" SkinID="SmallDropDown">
                </asp:DropDownList>
            </td>
            <td align="center">
                <asp:DropDownList ID="ddlampm" runat="server" Width="50px" SkinID="SmallDropDown">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>
