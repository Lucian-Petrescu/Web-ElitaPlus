<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CalendarForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.CalendarForm"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Choose a date</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../styles.css" type="text/css" rel="STYLESHEET">
		<SCRIPT language="JavaScript" src="../Navigation/scripts/GlobalHeader.js"></SCRIPT>
        <asp:literal id="Literal1" runat="server"></asp:literal>		
	</HEAD>
	<body oncontextmenu="return false" bgColor="#f0f0fa" leftMargin="0" topMargin="0">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" bgColor="#fef4e8" border="0">
				<TR>
					<TD noWrap align="right"><asp:label id="LabelMonth" runat="server" Width="53px" >Month</asp:label><STRONG>:</STRONG></TD>
					<td><asp:dropdownlist id="cboMonthList" runat="server" Width="55px"  AutoPostBack="True"></asp:dropdownlist></td>
					<td noWrap align="right"><asp:label id="LabelYear" runat="server" Width="27px" >Year</asp:label><STRONG>:</STRONG></td>
					<td><asp:dropdownlist id="cboYearList" runat="server" Width="57px"  AutoPostBack="True"></asp:dropdownlist></td>
				</TR>
			</TABLE>
			<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="250" border="1">
				<TR>
					<TD><asp:calendar id="MyCalendar" runat="server" Width="250px" Height="148px" ShowGridLines="True"
							BackColor="#f0f0fa" NextMonthText="<img runat='server' id='btnNextMonth' width='12' Height='10' src='./images/calendar_foward.gif' border='0'>"
							PrevMonthText="<img runat='server' id='btnPrevMonth' width='12' Height='10' src='./images/calendar_back.gif' border='0'>">
							<TodayDayStyle Font-Bold="True"  ForeColor="#12135B"></TodayDayStyle>
							<DayStyle ></DayStyle>
							<NextPrevStyle  ForeColor="#FBE6A5"></NextPrevStyle>
							<DayHeaderStyle   ForeColor="#12135B"></DayHeaderStyle>
							<SelectedDayStyle BackColor="Orange"></SelectedDayStyle>
							<TitleStyle  ForeColor="#FBE6A5"
								BorderStyle="Groove" BackColor="#A1BD08"></TitleStyle>
							<OtherMonthDayStyle ForeColor="#FBE6A5" BackColor="#F0F0FA"></OtherMonthDayStyle>
						</asp:calendar></TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
