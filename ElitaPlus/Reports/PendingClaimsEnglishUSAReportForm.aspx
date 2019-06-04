<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PendingClaimsEnglishUSAReportForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.PendingClaimsEnglishUSAReportForm" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>PendingClaims</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
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
								<TD height="20"><asp:label id="LabelReports" runat="server" CssClass="TITLELABEL">Reports</asp:label>:&nbsp;
								<asp:label id="Label7" runat="server" CssClass="TITLELABELTEXT">Pending Claims</asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="tblOuter2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				height="93%" cellSpacing="0" cellPadding="0" rules="none" width="98%" bgColor="#d5d6e4"
				border="0"> <!--d5d6e4-->
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
													<TABLE id="tblSearch" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; WIDTH: 100%; BORDER-BOTTOM: #999999 1px solid; HEIGHT: 64px"
														cellSpacing="2" cellPadding="8" rules="cols" width="100%" align="center" bgColor="#fef9ea"
														border="0">
														<TR>
															<TD>
																<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
																	<TR>
																		<TD align="center" colSpan="3">
																			<uc1:ErrorController id="ErrorCtrl" runat="server"></uc1:ErrorController></TD>
																	</TR>
																	<TR>
																		<TD noWrap align="right">&nbsp;
																		</TD>
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
															<TD colSpan="3">&nbsp;</TD>
														</TR>
														<TR>
															<TD noWrap align="right" width="25%">
																<asp:RadioButton id="rdealer" onclick="toggleAllDealersSelection(false);" AutoPostBack="false" Checked="False"
																	Runat="server" Text="SELECT_ALL_DEALERS" TextAlign="left"></asp:RadioButton></TD>
															<TD width="15"><IMG src="../Navigation/images/trans_spacer.gif" width="15"></TD>
															<TD align="right" width="50%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																<asp:label id="moDealerLabel" runat="server">OR CHOOSE SINGLE DEALER</asp:label>&nbsp;&nbsp;
																<asp:dropdownlist id="cboDealer" runat="server" AutoPostBack="false" Width="250px" onchange="toggleAllDealersSelection(true);"></asp:dropdownlist></TD>
														</TR>
														<TR>
															<TD colSpan="3">
																<HR style="HEIGHT: 1px">
															</TD>
														</TR>
														<TR>
															<TD noWrap align="right" width="25%">
																<asp:RadioButton id="rsvccenter" onclick="toggleAllServiceCentersSelection(false);" AutoPostBack="false"
																	Checked="False" Runat="server" Text="PLEASE SELECT ALL SERVICE CENTERS" TextAlign="left"></asp:RadioButton></TD>
															<TD width="15"><IMG src="../Navigation/images/trans_spacer.gif" width="15"></TD>
															<TD noWrap align="right" width="50%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																<asp:label id="moServiceCenterLabel" runat="server">OR A SINGLE SERVICE CENTER</asp:label>&nbsp;&nbsp;
																<asp:dropdownlist id="cboSvcCenter" runat="server" AutoPostBack="false" Width="250px" onchange="toggleAllServiceCentersSelection(true);"></asp:dropdownlist></TD>
														</TR>
														<TR>
															<TD colSpan="3">
																<HR style="HEIGHT: 1px">
															</TD>
														</TR>
														<TR>
															<TD style="HEIGHT: 17px" colSpan="2"></TD>
															<TD style="HEIGHT: 17px" noWrap align="right">
															<asp:label id="moDaysActiveLabel" runat="server">PLEASE ENTER MINIMUM NUMBER OF DAYS PENDING (0 TO 999)</asp:label>&nbsp;&nbsp;
															<asp:TextBox id="txtActiveDays" Runat="server" Width="40" CssClass="FLATTEXTBOX" MaxLength="3"></asp:TextBox>
															</TD>
														</TR>
														<TR>
															<TD colSpan="3">
																<HR style="HEIGHT: 1px">
															</TD>
														</TR>
														<TR>
														<TR>
															<TD>&nbsp;</TD>
															<TD>&nbsp;</TD>
															<TD>&nbsp;</TD>
														</TR>
														<TR>
															<TD vAlign="top" noWrap align="right" width="25%">
																<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
																	<TR>
																		<TD height="4"></TD>
																	</TR>
																	<TR>
																		<TD align="right">
																			<asp:label id="moSortByLabel" runat="server">SELECT_REPORT_SORT_ORDER</asp:label>:</TD>
																	</TR>
																</TABLE>
															</TD>
															<TD vAlign="top" align="center" colSpan="2">
																<asp:RadioButtonList id="rdReportSortOrder" runat="server" Width="420px" RepeatDirection="VERTICAL">
																	<asp:ListItem Value="0" Selected="True">NUMBER OF ACTIVE DAYS</asp:ListItem>
																	<asp:ListItem Value="1">CLAIM_NUMBER</asp:ListItem>
																	<asp:ListItem Value="2">SERVICE_CENTER_NAME</asp:ListItem>
																</asp:RadioButtonList></TD>
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
										<HR style="HEIGHT: 1px">
									</TD>
								</TR>
								<TR>
									<TD align="left">&nbsp;
										<asp:button id="btnGenRpt" style="BACKGROUND-IMAGE: url(../Navigation/images/viewIcon2.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server"  Text="View" Width="100px" CssClass="FLATBUTTON" height="20px"></asp:button></TD>
								</TR>
							</TABLE>
						</asp:panel></td>
				</tr>
			</TABLE>
		</form>
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/ReportCeMainScripts.js"></SCRIPT>
		<script>
function toggleAllServiceCentersSelection(isSingleServiceCenter)
	{
		//debugger;
		if(isSingleServiceCenter)
		{
			document.forms[0].rsvccenter.checked = false;
		}
		else
		{
			document.forms[0].cboSvcCenter.selectedIndex = -1;
		}
	}
	function toggleClaimsSelection(isAllClaims)
	{
		//debugger;
		if(!isAllClaims)
		{
			document.forms[0].RadiobuttonExcludeRepairedClaims.checked = false;
		}
		else
		{
			document.forms[0].RadiobuttonAllClaims.checked = false;
		}
	}
		</script>
	</body>
</HTML>
