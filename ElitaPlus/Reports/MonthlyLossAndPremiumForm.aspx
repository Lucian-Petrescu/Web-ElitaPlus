<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="MonthlyLossAndPremiumForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.MonthlyLossAndPremiumForm" %>
<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>MonthlyLossAndPremium</title>
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
								<asp:label id="Label7" runat="server"  CssClass="TITLELABELTEXT">Monthly Loss And Premium</asp:label></TD>
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
										<TABLE cellSpacing="0" cellPadding="0" width="98%" border="0">
											<TR>
												<TD colSpan="3">
													<TABLE id="tblSearch" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; WIDTH: 628px; BORDER-BOTTOM: #999999 1px solid; HEIGHT: 64px"
														cellSpacing="2" cellPadding="8" rules="cols" width="628" align="center" bgColor="#fef9ea"
														border="0">
														<TR>
															<TD>
																<TABLE cellSpacing="0" cellPadding="0" width="100%" align="center" border="0">
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
												<TD style="WIDTH: 740px" colSpan="3"><IMG height="15" src="../Navigation/images/trans_spacer.gif"></TD>
											</TR>
											<TR>
												<TD align="center" colSpan="3">
													<TABLE style="WIDTH: 698px; HEIGHT: 50px" cellSpacing="2" cellPadding="0" width="698" align="center"
														border="0">
														<TR>
															<TD align="center" colSpan="2">
																<asp:label id="MonthYearLabel" runat="server" Font-Bold="false">SELECT MONTH AND YEAR</asp:label>:
																<asp:dropdownlist id="MonthDropDownList" runat="server" Width="128px" AutoPostBack="false"></asp:dropdownlist>&nbsp;
																<asp:dropdownlist id="YearDropDownList" runat="server" Width="84px" AutoPostBack="false"></asp:dropdownlist></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 100%" align="center" width="100%" colSpan="2">
																<HR style="WIDTH: 100%; HEIGHT: 1px" id="ddSeparator">
															</TD>
														</TR> 
														<TR>
															<TD style="HEIGHT: 22px" align="center" colSpan="2">
																<uc1:MultipleColumnDDLabelControl id="moUserCompanyMultipleDrop" runat="server"></uc1:MultipleColumnDDLabelControl></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 100%" align="center" width="100%" colSpan="2">
																<DIV id="HRLine" runat="server">
																	<HR style="WIDTH: 100%; HEIGHT: 1px">
																</DIV>
															</TD>
														</TR>
														<TR>
															<TD align="center" colSpan="2">
																<TABLE style="WIDTH: 494px; HEIGHT: 50px" cellSpacing="2" cellPadding="0" width="494" align="center"
																	border="0">
																	<TR>
																		<TD style="HEIGHT: 14px" noWrap align="right" width="50%" colSpan="2">
																			<asp:RadioButton id="rdealer" onclick="toggleAllDealersSelection(false,'DealerLabel');" AutoPostBack="false" Text="SELECT_ALL_DEALERS"
																				TextAlign="left" Runat="server" Checked="False"></asp:RadioButton></TD>
																		<TD style="HEIGHT: 14px" noWrap align="left" width="50%">&nbsp;&nbsp;&nbsp;&nbsp;
																			<asp:label id="DealerLabel" runat="server">OR A SINGLE DEALER</asp:label>:
																			<asp:dropdownlist id="cboDealer" runat="server" AutoPostBack="false" onchange="toggleAllDealersSelection(true);"
																				width="272px"></asp:dropdownlist></TD>
																	</TR>
																</TABLE>
															</TD>
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
											runat="server" Font-Bold="false" Width="100px" Text="View" CssClass="FLATBUTTON" height="20px"></asp:button></TD>
								</TR>
							</TABLE>
						</asp:panel></td>
				</tr>
			</TABLE>
		</form>
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/ReportCeMainScripts.js"></SCRIPT>
	</body>
</HTML>
