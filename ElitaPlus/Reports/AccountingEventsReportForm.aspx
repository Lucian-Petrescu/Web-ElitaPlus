<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AccountingEventsReportForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.AccountingEventsReportForm" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html >
<HEAD>
	<title id="rptWindowTitle" runat="server">ACCOUNTING_EVENTS</title>
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
							<tr>
                            <td height="20">
                                <asp:Label ID="LabelReports" Font-Bold="false" CssClass="TITLELABEL" runat="server">Reports</asp:Label>:
                                <asp:Label ID="Label7" runat="server" Font-Bold="false" CssClass="TITLELABELTEXT">ACCOUNTING_EVENTS</asp:Label></td>
                            <td height="20" align="right">
                                *&nbsp;
                                <asp:Label ID="moIndicatesLabel" runat="server" Font-Bold="false" EnableViewState="true">INDICATES_REQUIRED_FIELDS</asp:Label></td>
                        </tr>
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
								cellSpacing="0" cellPadding="6" rules="cols" width="98%" align="center" bgColor="#fef9ea" height = "95%"
								border="0">
								<TR>
									<TD height="1"></TD>
								</TR>
								<TR>
									<TD vAlign="top">
										<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0" height = "95%">
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
													<TABLE style="WIDTH: 706px; HEIGHT: 50px" cellSpacing="2" cellPadding="0" width="706" border="0">														
														<TR>
															<TD vAlign="bottom" align="center" width="100%" colSpan="2">
																<uc1:MultipleColumnDDLabelControl id="multipleDropControl" runat="server"></uc1:MultipleColumnDDLabelControl></TD>
														</TR>
														<TR>
															<TD align="center" width="100%" colSpan="2">
																<HR id="ddSeparator1" style="WIDTH: 100%; HEIGHT: 1px">
															</TD>
														</TR>
														<TR>
															<TD noWrap align="right" width="35%">*
																<asp:RadioButton id="rEventType" onclick="ToggleSingleDropDownSelection('cboEventType','rEventType',false);" AutoPostBack="false" Checked="False"
																	Runat="server" TextAlign="left" Text="SELECT_ALL_EVENTS"></asp:RadioButton></TD>
															<TD style="HEIGHT: 24px" noWrap align="center" width="65%">
																<asp:label id="EventTypeLabel" runat="server">OR A SINGLE EVENT</asp:label>:
																<asp:dropdownlist id="cboEventType" runat="server" AutoPostBack="false" width="150px" onchange="ToggleSingleDropDownSelection('cboEventType','rEventType',true);"></asp:dropdownlist></TD>
														</TR>
														<TR>
															<TD align="center" width="100%" colSpan="2">
																<HR style="WIDTH: 100%; HEIGHT: 1px">
															</TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD style="HEIGHT: 85%; width: 100%;"></TD>
								</TR>
								<TR>
									<TD width="100%">
										<HR>
									</TD>
								</TR>
								<TR>
									<TD align="left">&nbsp;
										<asp:button id="btnGenRpt" style="BACKGROUND-IMAGE: url(../Navigation/images/viewIcon2.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server" Font-Bold="false" Width="100px" Text="View" height="20px" CssClass="FLATBUTTON"></asp:button></TD>
								</TR>
							</TABLE>
						</asp:panel></td>
				</tr>
			</TABLE>
		</form>
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/ReportCeMainScripts.js"></SCRIPT>
	</body>
</html>
