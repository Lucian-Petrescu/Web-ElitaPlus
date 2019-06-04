<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="NewCertificatesbyCoverageForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.NewCertificatesbyCoverageForm" %>
<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html> 
<HEAD>
		<title id="rptWindowTitle" runat="server">NewCertificatesByCoverage</title>
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
								<TD height="20">
									<asp:Label ID="LabelReports"  CssClass="TITLELABEL" runat="server">Reports</asp:label>:
									<asp:Label ID="Label7" runat="server"  CssClass="TITLELABELTEXT">NEW_CERTIFICATES_BY_COVERAGE</asp:label></TD>
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
								cellSpacing="0" cellPadding="6" rules="cols" width="98%" align="center" bgColor="#fef9ea" height = "95%"
								border="0">
								<TR>
									<TD height="1"></TD>
								</TR>
								<TR>
									<TD>
										<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
											<TR>
												<TD colSpan="3">
													<TABLE id="tblSearch" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid; HEIGHT: 64px"
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
											<TR>
												<TD style="WIDTH: 99.43%; HEIGHT: 1px"></TD>
											</TR>
											<TR>
												<TD align="center" colSpan="3" valign = top>
													<TABLE cellSpacing="2" cellPadding="0" width="95%" border="0">
														<TR>
															<TD  vAlign="middle" align="center" width="50%" colSpan="3" rowSpan="1">
																<TABLE  cellSpacing="0" cellPadding="0" width="484" border="0">
																	<TR>
																		<TD vAlign="middle" noWrap align="right">*
																			<asp:label id="moBeginDateLabel" runat="server" >BEGIN_DATE</asp:label>:</TD>
																		<TD style="WIDTH: 110px" noWrap>&nbsp;
																			<asp:textbox id="moBeginDateText" tabIndex="1" runat="server" width="95px" CssClass="FLATTEXTBOX"></asp:textbox></TD>
																		<TD style="WIDTH: 15px" vAlign="middle" align="center">
																			<asp:imagebutton id="BtnBeginDate" runat="server" Width="20px" ImageUrl="../Common/Images/calendarIcon2.jpg"
																				Height="17px"></asp:imagebutton></TD>
																		<TD vAlign="middle" noWrap align="right" width="15" colSpan="1" rowSpan="1"></TD>
																		<TD vAlign="middle" noWrap align="right">*
																			<asp:label id="moEndDateLabel" runat="server" >END_DATE</asp:label>:</TD>
																		<TD style="WIDTH: 110px" noWrap>&nbsp;
																			<asp:textbox id="moEndDateText" tabIndex="1" runat="server" width="95px" CssClass="FLATTEXTBOX"></asp:textbox></TD>
																		<TD style="WIDTH: 15px" vAlign="middle" align="center">
																			<asp:imagebutton id="BtnEndDate" runat="server" Width="20px" ImageUrl="../Common/Images/calendarIcon2.jpg"
																				Height="17px"></asp:imagebutton></TD>
																	</TR>
																</TABLE>
															</TD>
														</TR>
														<TR>
															<TD align="center" colSpan="3">
																<HR style="WIDTH: 99.43%; HEIGHT: 1px">
																<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" border="0">
																	<TR>
																		<TD vAlign="bottom" align="right" width="23%">*
																			<asp:RadioButton id="rdealer" onclick="ToggleDualDropDownsSelection('multipleDropControl_moMultipleColumnDrop', 'multipleDropControl_moMultipleColumnDropDesc', 'rdealer', false, ''); document.all.item('multipleDropControl_lb_DropDown').style.color = '';"
																				Text="SELECT_ALL_DEALERS" AutoPostBack="false" Checked="False" Runat="server" TextAlign="left"></asp:RadioButton>&nbsp;
																		</TD>
																		<TD vAlign="baseline" noWrap width="75%">
																			<uc1:MultipleColumnDDLabelControl id="multipleDropControl" runat="server"></uc1:MultipleColumnDDLabelControl></TD>
																	</TR>
																</TABLE>
															</TD>
														</TR>
														<TR>
															<TD colSpan="3">
																<HR style="WIDTH: 99.43%; HEIGHT: 1px">
															</TD>
														</TR>
														<TR>
															<TD noWrap align="right" width="25%" colSpan="1">*
																<asp:RadioButton id="RadiobuttonTotalsOnly" onclick="toggleDetailSelection(false);" Text="SHOW TOTALS ONLY"
																	AutoPostBack="false" Runat="server" TextAlign="left"></asp:RadioButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
															</TD>
																<TD vAlign="middle" noWrap align="right" width="5%" colSpan="1"></TD>
															<TD noWrap align="left" width="25%">&nbsp;
																<asp:RadioButton id="RadiobuttonDetail" onclick="toggleDetailSelection(true);" Text="OR SHOW DETAIL WITH TOTALS"
																	AutoPostBack="false" Runat="server" TextAlign="left"></asp:RadioButton></TD>
														</TR>
														<TR>
												            <TD colSpan="3"></TD>
											            </TR>
														<TR>
															<TD noWrap align="center" width="25%" colSpan="3">
																&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
									<TD>
										<HR>
									</TD>
								</TR>
								<TR>
									<TD align="left">
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
