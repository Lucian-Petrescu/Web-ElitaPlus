<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="IssuedCancelledRegister.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.IssuedCancelledRegister"%>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Issued / Cancelled Register</title>
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
								<TD height="20">
									<asp:label id="LabelReports" runat="server" CssClass="TITLELABEL">Reports</asp:label>:
									<asp:label id="Label7" runat="server" CssClass="TITLELABELTEXT">ISSUED_CANCELLED_REGISTER</asp:label></TD>
								<TD align="right" height="20">*&nbsp;
									<asp:label id="moIndicatesLabel" runat="server"  EnableViewState="true">INDICATES_REQUIRED_FIELDS</asp:label></TD>
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
													<TABLE cellSpacing="0" cellPadding="0" width="75%" border="0">
														<TR>
															<TD style="WIDTH: 100%; HEIGHT: 72px" align="left" colSpan="4">
																<TABLE id="Table2" cellSpacing="1" cellPadding="1" align="center" border="0">
																	<TR>
																		<TD align="right"></TD>
																		<TD style="WIDTH: 274px" align="right">
																			<asp:Label id="Label8" runat="server" Width="2px">MONTH</asp:Label></TD>
																		<TD align="right">
																			<asp:Label id="Label9" runat="server" Width="2px">YEAR</asp:Label></TD>
																	</TR>
																	<TR>
																		<TD align="right" colSpan="2">*
																			<asp:label id="MonthYearLabel" runat="server" >SELECT_MONTH_AND_YEAR</asp:label>:
																			<asp:dropdownlist id="MonthDropDownList" runat="server" Width="128px" AutoPostBack="false"></asp:dropdownlist></TD>
																		<TD align="right">
																			<asp:dropdownlist id="YearDropDownList" runat="server" Width="84px" AutoPostBack="false"></asp:dropdownlist></TD>
																	</TR>
																</TABLE>
															</TD>
														</TR>
														<TR>
															<TD noWrap align="right" width="100%" colSpan="4">
															<hr style="width: 100%; height: 1px"></TD>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4" align="center">
                                                                <table border="0" cellpadding="0" cellspacing="0" width="65%">
                                                                    <tr>
                                                                        <td align="left" colspan="2" nowrap valign="top">
                                                                            <uc1:MultipleColumnDDLabelControl ID="moUserCompanyMultipleDrop" runat="server" />
                                                                        </td>
                                                                        <td align="right" colspan="2">
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td id="ddSeparator" align="center" colspan="4" width="100%">
                                                                <hr style="width: 100%; height: 1px">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td nowrap align="center"
                                                                width="100%" colspan="4">
																<TABLE id="Table1" cellSpacing="2" cellPadding="0" width="75%" align="center" border="0">
																	<TR>
																		<TD noWrap align="center" colSpan="4"></TD>
																	</TR>
																	<TR>
																		<TD noWrap align="right" valign=bottom>*&nbsp;
																			<asp:RadioButton id="rdealer" onclick="ToggleDualDropDownsSelection('multipleDropControl_moMultipleColumnDrop', 'multipleDropControl_moMultipleColumnDropDesc', 'rdealer', false, ''); document.all.item('multipleDropControl_lb_DropDown').style.color = '';"
																				AutoPostBack="false" Checked="False" Runat="server" TextAlign="left" Text="SELECT_ALL_DEALERS"></asp:RadioButton></TD>
																		<TD noWrap align="left">
																			<uc1:MultipleColumnDDLabelControl id="multipleDropControl" runat="server"></uc1:MultipleColumnDDLabelControl></TD>
																	</TR>
																</TABLE>
															</TD>
														</TR>
														<TR>
															<TD style="WIDTH: 100%" noWrap align="right" width="100%"></TD>
															<TD style="WIDTH: 100%" noWrap align="right" width="100%"></TD>
															<TD noWrap align="left" width="25%" colSpan="2"></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 100%" colSpan="4">&nbsp;</TD>
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
