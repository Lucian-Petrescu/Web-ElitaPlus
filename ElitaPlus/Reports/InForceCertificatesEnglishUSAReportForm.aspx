<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="InForceCertificatesEnglishUSAReportForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.InForceCertificatesEnglishUSAReportForm" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title id="rptWindowTitle" runat="server">InForceCertificatesEnglish_USA</title>
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
								<asp:label id="Label7" runat="server" CssClass="TITLELABELTEXT">In-Force Certificates</asp:label></TD>
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
															<TD align="center" width="50%" colSpan="3">
																<TABLE cellSpacing="0" cellPadding="0" width="75%" border="0">
																	<TR>
																		<TD vAlign="middle" noWrap align="right">
																			<asp:label id="moAsOfDateLabel" runat="server" >IN-FORCE AS OF</asp:label>:</TD>
																		<TD noWrap>&nbsp;
																			<asp:textbox id="moAsOfDateText" tabIndex="1" runat="server" CssClass="FLATTEXTBOX"></asp:textbox>
																			<asp:imagebutton id="BtnAsOfDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg"
																				Width="20px"></asp:imagebutton></TD>
																	</TR>
																</TABLE>
															</TD>
														</TR>
														<TR>
															<TD colSpan="3">&nbsp;</TD>
														</TR>
														<TR>
															<TD noWrap align="right" width="25%" colSpan="2">
																<asp:RadioButton id="rdealer" onclick="toggleAllDealersSelection(false);" TextAlign="left" Text="SELECT_ALL_DEALERS"
																	Runat="server" Checked="False" AutoPostBack="false"></asp:RadioButton></TD>
															<TD noWrap align="left" width="25%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																<asp:label id="moDealerLabel" runat="server">OR CHOOSE SINGLE DEALER</asp:label>&nbsp;&nbsp;
																<asp:dropdownlist id="cboDealer" runat="server" AutoPostBack="false" onchange="toggleAllDealersSelection(true);"></asp:dropdownlist></TD>
														</TR>
														<TR>
															<TD colSpan="3">&nbsp;</TD>
														</TR>
			                                            <TR>
														    <TD noWrap align="right" width="25%" colSpan="2">
															    <asp:radiobutton id="rbCampaignNumber" onclick="toggleAllCampainNoSelection(false); document.all.item('moCampaignNumberLabel').style.color = '';"
																AutoPostBack="false" Runat="server" Text="SELECT_ALL_CAMPAIGN_NUMBERS" TextAlign="left"></asp:radiobutton></TD>
															<TD noWrap align="left" width="25%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																<asp:label id="moCampaignNumberLabel" runat="server">OR A SINGLE CAMPAIGN NUMBER</asp:label>:
																<asp:dropdownlist id="cboCampaignNumber" runat="server" AutoPostBack="false" onchange="toggleAllCampainNoSelection(true); document.all.item('moCampaignNumberLabel').style.color = '';"
																Width="212px"></asp:dropdownlist></TD>
														</TR>
														<TR>
															<TD colSpan="3">&nbsp;</TD>
														</TR>														
														<TR>
															<TD noWrap align="right" colSpan="2">
																<asp:CheckBox id="chkINCLUDE_MFG_COVERAGE" runat="server" TextAlign="Left" Text="INCLUDE_MFG_COVERAGE"></asp:CheckBox></TD>
															<TD></TD>
														</TR>
														<TR>
															<TD colSpan="3">&nbsp;</TD>
														</TR>
														<TR>
															<TD noWrap align="right" width="25%" colSpan="2">
																<asp:RadioButton id="RadiobuttonTotalsOnly" onclick="toggleDetailSelection(false);" TextAlign="left"
																	Text="SHOW TOTALS ONLY" Runat="server" AutoPostBack="false"></asp:RadioButton></TD>
															<TD noWrap align="left" width="25%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																<asp:RadioButton id="RadiobuttonDetail" onclick="toggleDetailSelection(true);" TextAlign="left" Text="OR SHOW DETAIL WITH TOTALS"
																	Runat="server" AutoPostBack="false"></asp:RadioButton></TD>
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
											runat="server"  CssClass="FLATBUTTON" Width="100px" Text="View" height="20px"></asp:button></TD>
								</TR>
							</TABLE>
						</asp:panel></td>
				</tr>
			</TABLE>
		</form>
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/ReportCeMainScripts.js"></SCRIPT>
	</body>
</HTML>
