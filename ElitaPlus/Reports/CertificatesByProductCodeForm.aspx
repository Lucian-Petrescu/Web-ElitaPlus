<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CertificatesByProductCodeForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.CertificatesByProductCodeForm" %>
<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Certificate By Product Code</title>
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
			<TABLE style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid; HEIGHT: 22px"
				cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE width="100%" border="0">
							<TR>
								<TD height="20"><asp:label id="LabelReports" runat="server" CssClass="TITLELABEL">Reports</asp:label>:
									<asp:label id="Label7" runat="server" CssClass="TITLELABELTEXT">CERTIFICATES BY PRODUCT CODE REPORT CRITERIA</asp:label></TD>
								<TD align="right" height="20">*&nbsp;
									<asp:label id="moIndicatesLabel" runat="server"  EnableViewState="true">INDICATES_REQUIRED_FIELDS</asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="tblOuter2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				height="93%" cellSpacing="0" cellPadding="0" rules="none" width="98%" bgColor="#d5d6e4"
				border="0"> <!--d5d6e4-->
				<TR>
					<TD>&nbsp;</TD>
				</TR>
				<TR>
					<TD vAlign="top" align="center" height="100%"><asp:panel id="WorkingPanel" runat="server">
							<TABLE id="tblMain1" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid"
								cellSpacing="0" cellPadding="6" rules="cols" width="98%" align="center" bgColor="#fef9ea"
								border="0">
								<TR>
									<TD vAlign="top" height="1"></TD>
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
																		<TD noWrap align="center"><uc1:errorcontroller id="ErrorCtrl" runat="server"></uc1:errorcontroller>&nbsp;
																		</TD>
																	</TR>
																</TABLE>
																<uc1:reportceinputcontrol id="moReportCeInputControl" runat="server"></uc1:reportceinputcontrol></TD>
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
															<TD colSpan="3"></TD>
														</TR>
														<TR>
															<TD vAlign="bottom" noWrap align="right">*
																<asp:radiobutton id="rdealer" onclick="ToggleDualDropDownsSelection('multipleDropControl_moMultipleColumnDrop', 'multipleDropControl_moMultipleColumnDropDesc', 'rdealer', false, ''); document.all.item('multipleDropControl_lb_DropDown').style.color = '';"
																	TextAlign="left" Text="SELECT_ALL_DEALERS" Runat="server" Checked="False" AutoPostBack="false"></asp:radiobutton>&nbsp;&nbsp;&nbsp;&nbsp;</TD>
															<TD style="WIDTH: 20px; HEIGHT: 18px" width="20"></TD>
															<TD noWrap align="left">
																<uc1:multiplecolumnddlabelcontrol id="multipleDropControl" runat="server"></uc1:multiplecolumnddlabelcontrol></TD>
														</TR>
														<TR>
															<TD style="HEIGHT: 18px" noWrap align="center" width="50%" colSpan="3">
																<HR style="WIDTH: 95%; HEIGHT: 1px">
																&nbsp;</TD>
														</TR>
														<TR>
															<TD style="HEIGHT: 18px" noWrap align="center" width="50%" colSpan="3">
																<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="90%" border="0">
																	<TR>
																		<TD align="right">*
																			<asp:radiobutton id="rbProduct" onclick="toggleAllProductsSelection(false); document.all.item('moProductLabel').style.color = '';"
																				AutoPostBack="false" Checked="False" Runat="server" Text="SELECT_ALL_PRODUCT_CODES" TextAlign="left"></asp:radiobutton></TD>
																		<TD style="WIDTH: 20px; HEIGHT: 18px"></TD>
																		<TD>
																			<asp:label id="moProductLabel" runat="server">OR A SINGLE PRODUCT CODE</asp:label>:
																			<asp:dropdownlist id="cboProduct" runat="server" AutoPostBack="false" onchange="toggleAllProductsSelection(true); document.all.item('moProductLabel').style.color = '';"
																				Width="212px"></asp:dropdownlist></TD>
																	</TR>
																</TABLE>
															</TD>
														</TR>
														<TR>
															<TD style="HEIGHT: 14px" colSpan="3">
																<HR style="WIDTH: 95%; HEIGHT: 1px">
															</TD>
														</TR>
		                                                <TR>
															<TD style="HEIGHT: 18px" noWrap align="center" width="50%" colSpan="3">
																<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="90%" border="0">
																	<TR>
																		<TD align="right">*
																			<asp:radiobutton id="rbCampaignNumber" onclick="toggleAllCampainNoSelection(false); document.all.item('moCampaignNumberLabel').style.color = '';"
																				AutoPostBack="false" Checked="False" Runat="server" Text="SELECT_ALL_CAMPAIGN_NUMBERS" TextAlign="left"></asp:radiobutton></TD>
																		<TD style="WIDTH: 20px; HEIGHT: 18px"></TD>
																		<TD>
																			<asp:label id="moCampaignNumberLabel" runat="server">OR A SINGLE CAMPAIGN NUMBER</asp:label>:
																			<asp:dropdownlist id="cboCampaignNumber" runat="server" AutoPostBack="false" onchange="toggleAllCampainNoSelection(true); document.all.item('moCampaignNumberLabel').style.color = '';"
																				Width="212px"></asp:dropdownlist></TD>
																	</TR>
																</TABLE>
															</TD>
														</TR>														
														<TR>
															<TD style="HEIGHT: 14px" colSpan="3">
																<HR style="WIDTH: 95%; HEIGHT: 1px">
															</TD>
														</TR>
														<TR>
															<TD style="HEIGHT: 14px" colSpan="3">
																<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="90%" align="center" border="0">
																	<TR>
																		<TD vAlign="top">
																			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
																				<TBODY>
																					<TR> <!--<TD style="WIDTH: 274px"><IMG height="7" src="../images/trans_spacer.gif" width="1"></TD>-->
																						<TD><IMG height="7" src="../Navigation/images/trans_spacer.gif" width="1"></TD>
																					</TR>
																					<TR>
																						<TD noWrap align="right"></TD>
																						<TD noWrap align="right">*
																							<asp:label id="Label5" runat="server">SELECT_REPORT_SORT_ORDER</asp:label>:</TD>
																		</TD>
																	</TR>
																</TABLE>
															</TD>
															<TD>
																<asp:radiobuttonlist id="rdReportSortOrder" runat="server" Width="224px" Height="40px" RepeatDirection="VERTICAL">
																	<asp:ListItem Value="0" Selected="True">DEALER CODE</asp:ListItem>
																	<asp:ListItem Value="1">PRODUCT CODE</asp:ListItem>
																</asp:radiobuttonlist></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
											<TR>
												<TD vAlign="top" noWrap align="left">
												</TD>
												<TD style="WIDTH: 20px" vAlign="top"></TD>
												<TD vAlign="top">
											<TR>
												<TD style="HEIGHT: 25px" colSpan="3">
													<HR style="WIDTH: 95%; HEIGHT: 1px">
												</TD>
											</TR>
											<TR>
												<TD style="HEIGHT: 25px" align="center" colSpan="3">
													<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="50%" border="0">
														<TR>
															<TD>*
																<asp:radiobutton id="RadiobuttonTotalsOnly" onclick="toggleDetailSelection(false);" AutoPostBack="false"
																	Runat="server" Text="SHOW TOTALS ONLY" TextAlign="left"></asp:radiobutton></TD>
															<TD><asp:radiobutton id="RadiobuttonDetail" onclick="toggleDetailSelection(true);" AutoPostBack="false"
																	Runat="server" Text="OR SHOW DETAIL WITH TOTALS" TextAlign="left"></asp:radiobutton></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
							</TABLE></TD>
				</TR>
				<TR>
					<TD style="HEIGHT: 24px"></TD>
				</TR>
				<TR>
					<TD>
						<HR style="HEIGHT: 1px">
					</TD>
				</TR>
				<TR>
					<TD align="left">&nbsp;
						<asp:button id="btnGenRpt" style="BACKGROUND-IMAGE: url(../Navigation/images/viewIcon2.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
							runat="server"  Text="View" Width="100px" height="20px" CssClass="FLATBUTTON"></asp:button></TD>
				</TR>
			</TABLE>
			</asp:panel></TD></TR></TBODY></TABLE></form>
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/ReportCeMainScripts.js"></SCRIPT>

	</body>
</HTML>

