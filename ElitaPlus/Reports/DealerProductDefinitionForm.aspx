<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="DealerProductDefinitionForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.DealerProductDefinitionForm" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Dealer Product Definition Report</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="../Styles.css" type="text/css" rel="STYLESHEET">
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>
	</HEAD>
	<body leftMargin="0" topMargin="0" onload="changeScrollbarColor();" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--Start Header-->
			<TABLE style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE width="100%" border="0">
							<TR>
								<TD height="20"><asp:label id="LabelReports" runat="server"  CssClass="TITLELABEL">Reports</asp:label>:<asp:label id="Label7" runat="server" CssClass="TITLELABELTEXT">Dealer_Product_Definition</asp:label></TD>
								<TD height="20" align="right">*&nbsp;
									<asp:label id="moIndicatesLabel" runat="server"  EnableViewState="true">INDICATES_REQUIRED_FIELDS</asp:label></TD>
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
												<TD>
													<TABLE id="tblSearch" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; WIDTH: 628px; BORDER-BOTTOM: #999999 1px solid; HEIGHT: 64px"
														cellSpacing="2" cellPadding="8" rules="cols" width="628" align="center" bgColor="#fef9ea"
														border="0">
														<TBODY>
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
																		</TR> <!--<TR>
																		<TD noWrap width="450" colSpan="2">&nbsp;</TD>
																		<TD noWrap align="right">
																			<asp:label id="lblDistChannel" runat="server">Sort By:</asp:label>&nbsp;
																			<asp:dropdownlist id="cboDistChannel" runat="server" Width="154px" AutoPostBack="False"></asp:dropdownlist></TD>
																	</TR>--></TABLE>
																	<uc1:ReportCeInputControl id="moReportCeInputControl" runat="server"></uc1:ReportCeInputControl></TD>
												</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD>&nbsp;
										<TABLE id="Table1" cellSpacing="2" cellPadding="0" width="75%" border="0" align="center">
											<TR>
												<TD noWrap align="center">
													<uc1:MultipleColumnDDLabelControl id="multipleDropControl" runat="server"></uc1:MultipleColumnDDLabelControl></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD>
										<HR>
										&nbsp;</TD>
								</TR>
							</TABLE>
					</td>
				</tr>
				<TR>
					<TD style="HEIGHT: 11px"></TD>
				</TR>
				<TR>
					<TD align="left">&nbsp;
						<asp:button id="btnGenRpt" style="BACKGROUND-IMAGE: url(../Navigation/images/viewIcon2.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
							runat="server"  Width="100px" Text="View" height="20px" CssClass="FLATBUTTON"></asp:button></TD>
				</TR>
			</TABLE>
			</asp:panel></TD></TR></TBODY></TABLE>
		</form>
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/ReportCeMainScripts.js"></SCRIPT>
	</body>
</HTML>
