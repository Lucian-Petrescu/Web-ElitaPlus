<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ClaimsOpenedAtTheBeginningOfESC.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.ClaimsOpenedAtTheBeginningOfESC"%>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Claims Opened At The Beginning Of ESC</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="../Styles.css" type="text/css" rel="STYLESHEET">
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>
	</HEAD>
	<body leftMargin="0" topMargin="0" onload="changeScrollbarColor();" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--Start Header--><input id="rptTitle" type="hidden" name="rptTitle"> <input id="rptSrc" type="hidden" name="rptSrc">
			<TABLE style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE width="100%" border="0">
							<TR>
								<TD height="20"><asp:label id="LabelReports" runat="server" CssClass="TITLELABEL">Reports</asp:label>:&nbsp;<asp:label id="Label7" runat="server"  CssClass="TITLELABELTEXT">CLAIMS_OPENED_AT_THE_BEGINNING_OF_ESC</asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="tblOuter2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				height="93%" cellSpacing="0" cellPadding="0" rules="none" width="98%" bgColor="#d5d6e4"
				border="0"> <!--ededd5-->
				<tr>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td vAlign="top" align="center" height="100%"><asp:panel id="WorkingPanel" runat="server">
							<TABLE id="tblMain1" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid"
								cellSpacing="0" cellPadding="6" rules="cols" width="98%" align="center" bgColor="#fef9ea"
								border="0">
								<TR>
									<TD height="1"></TD>
								</TR>
								<TR>
									<TD>
										<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
											<TR>
												<TD colSpan="3">
													<TABLE id="tblSearch" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; WIDTH: 628px; BORDER-BOTTOM: #999999 1px solid; HEIGHT: 64px"
														cellSpacing="2" cellPadding="8" rules="cols" width="628" align="center" bgColor="#fef9ea"
														border="0">
														<TR>
															<TD>
																<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
																	<TR>
																		<TD align="center" colSpan="3">
																			<uc1:ErrorController id="ErrorCtrl" runat="server"></uc1:ErrorController></TD>
																	</TR>
																</TABLE>
																<uc1:ReportCeInputControl id="moReportCeInputControl" runat="server"></uc1:ReportCeInputControl></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
											<TR>
												<TD colSpan="3"><IMG height="15" src="../Navigation/images/trans_spacer.gif"></TD>
											</TR>
											<TR>
												<TD align="center" colSpan="3">
													<TABLE cellSpacing="2" cellPadding="0" width="75%" border="0">
														<TR>
															<TD noWrap align="center">
																<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="75%" border="0">
																	<TR>
																		<TD width="50%">&nbsp;</TD>
																		<TD vAlign="middle" noWrap align="right">
																			<asp:label id="moBeginDateLabel" runat="server" >BEGIN_DATE</asp:label>:</TD>
																		<TD noWrap>&nbsp;
																			<asp:textbox id="moBeginDateText" tabIndex="1" runat="server" CssClass="FLATTEXTBOX"></asp:textbox>&nbsp;</TD>
																		<TD>
																			<asp:imagebutton id="BtnBeginDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:imagebutton>&nbsp;&nbsp;</TD>
																		<TD vAlign="middle" noWrap align="right">&nbsp;&nbsp;
																			<asp:label id="moEndDateLabel" runat="server" >END_DATE</asp:label>:</TD>
																		<TD noWrap>&nbsp;
																			<asp:textbox id="moEndDateText" tabIndex="1" runat="server" CssClass="FLATTEXTBOX" width="125px"></asp:textbox>&nbsp;</TD>
																		<TD>
																			<asp:imagebutton id="BtnEndDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:imagebutton></TD>
																		<TD width="50%">&nbsp;</TD>
																	</TR>
																</TABLE>
															</TD>
														</TR>
														<TR>
															<TD noWrap align="center">
																<HR style="HEIGHT: 1px">
																&nbsp;</TD>
														</TR>
														<TR>
															<TD noWrap align="center">
																<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" border="0">
																	<TR>
																		<TD align="center">
																			<asp:label id="numberOfDaysLabel" runat="server" Width="263px">Number of days since start of coverage</asp:label>
																			<asp:TextBox id="txtNumberOfDaysSinceStartOfCoverage" CssClass="FLATTEXTBOX" Width="40" MaxLength="3"
																				Runat="server"></asp:TextBox></TD>
																	</TR>
																	<TR>
																		<TD align="center" colSpan="2">
																			<HR style="HEIGHT: 1px">
																			&nbsp;</TD>
																	</TR>
																</TABLE>
																<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="100%" border="0">
																	<TR>
																		<TD vAlign="top" align="right">
																			<asp:label id="Label4" runat="server">SELECT_REPORT_SORT_ORDER</asp:label></TD>
																		<TD vAlign="top">
																			<asp:RadioButtonList id="rdReportSortOrder" runat="server" Width="154px" CellSpacing="0" CellPadding="0"
																				RepeatDirection="Vertical">
																				<asp:ListItem Value="0">Claim_number</asp:ListItem>
																				<asp:ListItem Value="1">Claim_date</asp:ListItem>
																				<asp:ListItem Value="2">Service_center_name</asp:ListItem>
																			</asp:RadioButtonList></TD>
																	</TR>
																</TABLE>
															</TD>
														</TR>
														<TR>
															<TD noWrap align="center"></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD style="HEIGHT: 11px"></TD>
								</TR>
								<TR>
									<TD>
										<HR>
									</TD>
								</TR>
								<TR>
									<TD align="left">&nbsp;
										<asp:button id="btnGenRpt" style="BACKGROUND-IMAGE: url(../Navigation/images/viewIcon2.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server"  CssClass="FLATBUTTON" Width="100px" height="20px" Text="View"></asp:button></TD>
								</TR>
							</TABLE>
						</asp:panel></td>
				</tr>
			</TABLE>
		</form>
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/ReportCeMainScripts.js"></SCRIPT>
	</body>
</HTML>
