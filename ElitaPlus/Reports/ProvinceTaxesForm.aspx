<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ProvinceTaxesForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.ProvinceTaxesForm"%>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDropControl" Src="../Common/MultipleColumnDropControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ProvinceTaxesForm</title>
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
								<asp:label id="lblProvTaxes" runat="server" CssClass="TITLELABELTEXT">Export Province Taxes</asp:label></TD>
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
												<TD style="WIDTH: 741px" colSpan="3">
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
															<TD style="WIDTH: 100%" align="center" width="100%" colSpan="4">
																<asp:label id="MonthYearLabel" runat="server"  Width="160px">SELECT MONTH AND YEAR</asp:label>
																<asp:dropdownlist id="MonthDropDownList" runat="server" Width="128px" AutoPostBack="false"></asp:dropdownlist>
																<asp:dropdownlist id="YearDropDownList" runat="server" Width="84px" AutoPostBack="false"></asp:dropdownlist></TD>
														</TR>
														<TR>
															<TD align="center" width="100%" colSpan="4">
																<HR style="WIDTH: 100%; HEIGHT: 1px">
															</TD>
														</TR>
														<TR>
															<TD style="WIDTH: 100%; HEIGHT: 0.01%" vAlign="bottom" align="center" width="100%" colSpan="4">
																<uc1:MultipleColumnDropControl id="moUserCompanyMultipleDrop" runat="server"></uc1:MultipleColumnDropControl></TD>
														</TR>
														<TR>
															<TD id="ddSeparator" align="center" width="100%" colSpan="4">
																<HR style="WIDTH: 100%; HEIGHT: 1px">
															</TD>
														</TR>
														<TR>
															<TD style="HEIGHT: 14px" noWrap align="right" width="50%" colSpan="2">
																<asp:RadioButton id="rdealer" onclick="toggleAllDealersSelection(false);" AutoPostBack="false" Checked="False"
																	Runat="server" TextAlign="left" Text="SELECT_ALL_DEALERS"></asp:RadioButton></TD>
															<TD style="HEIGHT: 14px" noWrap align="left" width="50%">&nbsp;&nbsp;&nbsp;&nbsp;
																<asp:label id="DealerLabel" runat="server">OR A SINGLE DEALER</asp:label>
																<asp:dropdownlist id="cboDealer" runat="server" AutoPostBack="false" width="272px" onchange="toggleAllDealersSelection(true);"></asp:dropdownlist></TD>
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
											runat="server"  Width="100px" Text="View" height="20px" CssClass="FLATBUTTON"></asp:button></TD>
								</TR>
							</TABLE>
						</asp:panel></td>
				</tr>
			</TABLE>
		</form>
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/ReportCeMainScripts.js"></SCRIPT>
	</body>
</HTML>
