<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CalendarWithTimeForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.CalendarWithtimeForm"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<head>
		<title>Choose a date</title>
		<asp:literal id="Literal1" runat="server"></asp:literal>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR"/>
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		<link href="../styles.css" type="text/css" rel="STYLESHEET"/>
		<script language="JavaScript" src="../Navigation/scripts/GlobalHeader.js" type="text/javascript"></script>
	</head>
	<body oncontextmenu="return false" bgcolor="#f0f0fa" leftmargin="0" topmargin="0">
		<form id="Form1" method="post" runat="server">
			<table id="Table2" cellspacing="1" cellpadding="1" width="100%" bgcolor="#fef4e8" border="0">
				<TR>
					<td nowrap align="right"><asp:label id="LabelMonth" runat="server" Width="53px" >Month</asp:label><strong>:</strong></td>
					<td><asp:dropdownlist id="cboMonthList" runat="server" Width="55px"  AutoPostBack="True"></asp:dropdownlist></td>
					<td noWrap align="right"><asp:label id="LabelYear" runat="server" Width="27px" >Year</asp:label><strong>:</strong></td>
					<td><asp:dropdownlist id="cboYearList" runat="server" Width="57px"  AutoPostBack="True"></asp:dropdownlist></td>
				</TR>
			</table>
			<table id="Table1" cellSpacing="1" cellPadding="1" width="250" border="1">
				<tr>
					<td><asp:calendar id="MyCalendar" runat="server" Width="250px" Height="148px" ShowGridLines="True"
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
						</asp:calendar></td>
				</tr>
                <tr>
					<td>
                        <table id="Table3" cellspacing="1" cellpadding="1" width="100%" border="1">
                            <tr>
                                <td align="center" >
                                    <asp:Label ID="lblhour" Width="100%" runat="server" >HOUR</asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lblminute"  Width="100%" runat="server" >MINUTE</asp:Label>
                                </td>
                                <td align="center">
                                    <asp:Label ID="lblSecond"  Width="100%" runat="server" >SECOND</asp:Label>
                                </td>
                                <td align="center">
                                </td>
                            </tr>
                            <tr>
                                <td align="center">
                                    <asp:DropDownList ID="ddlhour" runat="server" Width="50px">
                                    </asp:DropDownList>
                                </td>
                                <td align="center">
                                    <asp:DropDownList ID="ddlMinutes" runat="server" Width="50px">
                                    </asp:DropDownList>
                                </td>
                                <td align="center">
                                <asp:DropDownList ID="ddlSeconds" runat="server" Width="50px">
                                    </asp:DropDownList>
                                </td>
                                <td align="center">
                                <asp:DropDownList ID="ddlampm" runat="server" Width="50px">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
			</table>
		</form>
	</body>
</HTML>
