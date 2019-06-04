<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DKNewCertificatesbyCoverageForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.DKNewCertificatesbyCoverageForm" %>
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
									<asp:Label ID="Label7" runat="server"  CssClass="TITLELABELTEXT">DK_NEW_CERTIFICATES_BY_COVERAGE</asp:label></TD>
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
												<td style="WIDTH: 99.43%; HEIGHT: 1px"></td>
											
											<tr>
												<td align="center" colspan="3" valign="top">
													<table border="0" cellpadding="0" cellspacing="2" 
                                    width="95%">
														<tr>
															<td align="center" colspan="3" rowspan="1" 
                                    valign="middle" width="50%">
																<table border="0" cellpadding="0" 
                                    cellspacing="0" width="484">
																	<tr>
																		<td align="right" nowrap 
                                    valign="middle">*
																			<asp:Label ID="moBeginDateLabel" 
                                    runat="server">BEGIN_DATE</asp:Label>:</td>
																		<td nowrap style="WIDTH: 110px">&#160;
																			<asp:TextBox ID="moBeginDateText" 
                                    runat="server" CssClass="FLATTEXTBOX" tabIndex="1" width="95px"></asp:TextBox></td>
																		<td align="center" style="WIDTH: 15px" 
                                    valign="middle">
																			<asp:ImageButton ID="BtnBeginDate" 
                                    runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg" 
                                    Width="20px">
                                </asp:ImageButton></td>
																		<td align="right" colspan="1" nowrap 
                                    rowspan="1" valign="middle" width="15"></td>
																		<td align="right" nowrap 
                                    valign="middle">*
																			<asp:Label ID="moEndDateLabel" 
                                    runat="server">END_DATE</asp:Label>:</td>
																		<td nowrap style="WIDTH: 110px">&#160;
																			<asp:TextBox ID="moEndDateText" 
                                    runat="server" CssClass="FLATTEXTBOX" tabIndex="1" width="95px"></asp:TextBox></td>
																		<td align="center" style="WIDTH: 15px" 
                                    valign="middle">
																			<asp:ImageButton ID="BtnEndDate" 
                                    runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg" 
                                    Width="20px">
                                </asp:ImageButton></td>
																	</tr>
																</table>
															</td>
														</tr>
														<tr>
															<td align="center" colspan="3">
																<hr style="WIDTH: 99.43%; HEIGHT: 1px">
																<table id="Table2" border="0" cellpadding="1" 
                                    cellspacing="1" width="100%">
																	<tr>
																		<td align="right" valign="bottom" 
                                    width="23%">*
																			<asp:RadioButton ID="rdealer" 
                                    Runat="server" AutoPostBack="false" Checked="False" 
                                    onclick="ToggleDualDropDownsSelection('multipleDropControl_moMultipleColumnDrop', 'multipleDropControl_moMultipleColumnDropDesc', 'rdealer', false, ''); document.all.item('multipleDropControl_lb_DropDown').style.color = '';" 
                                    Text="SELECT_ALL_DEALERS" TextAlign="left">
                                </asp:RadioButton>&#160;
																		</td>
																		<td nowrap valign="baseline" 
                                    width="75%">
																			<uc1:MultipleColumnDDLabelControl 
                                    ID="multipleDropControl" runat="server"></uc1:MultipleColumnDDLabelControl></td>
																	</tr>
																</table>
															</hr></td>
														</tr>
														<tr>
															<td colspan="3">
																<hr style="WIDTH: 99.43%; HEIGHT: 1px">
															</hr></td>
														</tr>
														<tr>
															<td align="right" colspan="1" nowrap width="25%">*
																<asp:RadioButton ID="RadiobuttonTotalsOnly" 
                                    Runat="server" AutoPostBack="false" onclick="toggleDetailSelection(false);" 
                                    Text="SHOW TOTALS ONLY" TextAlign="left">
                                </asp:RadioButton>&#160;&#160;&#160;&#160;&#160;&#160;
															</td>
																<td align="right" colspan="1" nowrap 
                                    valign="middle" width="5%"></td>
															<td align="left" nowrap width="25%">&#160;
																<asp:RadioButton ID="RadiobuttonDetail" 
                                    Runat="server" AutoPostBack="false" onclick="toggleDetailSelection(true);" 
                                    Text="OR SHOW DETAIL WITH TOTALS" TextAlign="left">
                                </asp:RadioButton></td>
														</tr>
														<tr>
												            <td colspan="3"></td>
											            </tr>
														<tr>
															<td align="center" colspan="3" nowrap width="25%">
																&#160;&#160;&#160;&#160;&#160;&#160;
															</td>															
														</tr>
													</table>
												</td>
											</tr>
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

