<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ProRatedPremiumAnalysisForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.ProRatedPremiumAnalysis" %>

<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ProRated Premium Analysis Report</title>
		<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="STYLESHEET">
		
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
									<asp:label id="LabelReports" runat="server" cssclass="TITLELABEL">Reports</asp:label>:
									<asp:label id="LabelTitle" runat="server"  cssclass="TITLELABELTEXT">PRO_RATED_PREMIUM_ANALYSIS_REPORT</asp:label></TD>
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
											<TR>
												<TD style="WIDTH: 99.43%; HEIGHT: 1px"></TD>
											</TR>
											<TR>
												<TD align="center" colSpan="3">
													<TABLE cellSpacing="2" cellPadding="0" width="95%" border="0">
														<TR>
															<TD style="HEIGHT: 10px" vAlign="middle" align="center" width="50%" colSpan="3" rowSpan="1">
																<TABLE style="WIDTH: 484px; HEIGHT: 18px" cellSpacing="0" cellPadding="0" width="484" border="0">
																	<TR>
																		<TD noWrap align="right" width="40%" colSpan="2">*
																			<asp:label id="moMonthYearLabel" runat="server">SELECT_MONTH_AND_YEAR</asp:label>:</TD>
																		<TD noWrap align="left" width="40%">&nbsp;
                                                                            <asp:DropDownList ID="cboMonth" runat="server" TabIndex="1" Width="90px"></asp:DropDownList>&nbsp;
                                                                            <asp:TextBox ID="moYearText" runat="server" TabIndex="2" Width="60px" MaxLength="4"></asp:TextBox>
																		</TD>
																	</TR>
																</TABLE>
															</TD>
														</TR>
														<TR>
															<TD style="HEIGHT: 94px" align="center" colSpan="3">
																<HR style="WIDTH: 99.43%; HEIGHT: 1px">
																<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" border="0">
																	<TR>
																		<TD vAlign="bottom" align="right" width="23%">*
																		</TD>
																		<TD vAlign="baseline" noWrap width="75%">
																			<uc1:MultipleColumnDDLabelControl id="multipleDropControl" runat="server"></uc1:MultipleColumnDDLabelControl></TD>
																	</TR>
																</TABLE>
															</TD>
														</TR>
														<TR>
															<TD style="HEIGHT: 27px" colSpan="3">
																<HR style="WIDTH: 99.43%; HEIGHT: 1px">
															</TD>
														</TR>
														<TR>
															<TD noWrap align="right" width="25%" colSpan="2">*
																<asp:RadioButton id="RadiobuttonTotalsOnly" onclick="toggleDetailSelection(false);" TextAlign="left"
																	Runat="server" AutoPostBack="false" Text="SHOW TOTALS ONLY"></asp:RadioButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
															</TD>
															<TD noWrap align="left" width="25%">&nbsp;
																<asp:RadioButton id="RadiobuttonDetail" onclick="toggleDetailSelection(true);" TextAlign="left" Runat="server"
																	AutoPostBack="false" Text="OR SHOW DETAIL WITH TOTALS"></asp:RadioButton></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD style="HEIGHT: 20px"></TD>
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

