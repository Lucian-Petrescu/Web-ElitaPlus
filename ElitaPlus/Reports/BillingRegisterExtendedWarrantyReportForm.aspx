<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="BillingRegisterExtendedWarrantyReportForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.BillingRegisterExtendedWarrantyReportForm" %>
<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Billing Register</title>
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
								<asp:label id="Label7" runat="server" CssClass="TITLELABELTEXT">BILLING_REGISTER</asp:label></TD>
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
												<TD style="HEIGHT: 59px" align="center" colSpan="4">
													<TABLE  style="WIDTH: 608px; HEIGHT: 34px" cellSpacing="2" cellPadding="0" width="608" border="0">
													
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
														<TR>
															<TD align="center" width="50%" colSpan="4">
																<TABLE cellSpacing="0" cellPadding="0" width="75%" border="0">
																	<TR>
																		<TD width="50%">&nbsp;</TD>
																		<TD vAlign="middle" noWrap align="right">
																			<asp:label id="moBeginDateLabel" runat="server" >BEGIN_DATE</asp:label>:</TD>
																		<TD noWrap>&nbsp;
																			<asp:textbox id="moBeginDateText" tabIndex="1" runat="server" CssClass="FLATTEXTBOX"></asp:textbox>&nbsp;</TD>
																		<TD>
																			<asp:imagebutton id="BtnBeginDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:imagebutton>&nbsp;&nbsp;</TD>
																		<TD vAlign="middle" noWrap align="right">&nbsp;&nbsp;
																			<asp:label id="moEndDateLabel" runat="server" >END_DATE</asp:label>:</TD>
																		<TD noWrap>&nbsp;
																			<asp:textbox id="moEndDateText" tabIndex="1" runat="server" width="125px" CssClass="FLATTEXTBOX"></asp:textbox>&nbsp;</TD>
																		<TD>
																			<asp:imagebutton id="BtnEndDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:imagebutton></TD>
																		<TD width="50%">&nbsp;</TD>
																	</TR> 
																	</TABLE>
															</TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
											<TR>
												<TD style="HEIGHT: 19px" vAlign="middle" align="center" colSpan="3" rowSpan="1">
													<HR style="WIDTH: 571px; HEIGHT: 1px" SIZE="1">
													&nbsp;</TD>
											</TR>
											<TR>
												<TD align="center" colSpan="3">
													<TABLE id="Table2" style="WIDTH: 571px; HEIGHT: 60px" cellSpacing="1" cellPadding="1" width="571"
														border="0">
														<TR>
															<TD style="WIDTH: 791px; HEIGHT: 10px" vAlign="middle" align="right">
																<asp:RadioButton id="rdealer" onclick="ToggleDualDropDownsSelection('cboDealerCode', 'cboDealer', 'rdealer', false, '');"
																	AutoPostBack="false" Text="SELECT_ALL_DEALERS" TextAlign="left" Runat="server" Checked="True"
																	Height="10px"></asp:RadioButton></TD>
															<TD style="WIDTH: 68px; HEIGHT: 10px" vAlign="baseline"></TD>
															<TD style="WIDTH: 68px; HEIGHT: 10px" vAlign="baseline" colSpan="2">
																<asp:label id="DealerLabel" runat="server" Width="152px" Height="4px">OR A SINGLE DEALER</asp:label></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 791px; HEIGHT: 10px"></TD>
															<TD style="WIDTH: 68px; HEIGHT: 10px"></TD>
															<TD style="WIDTH: 68px; HEIGHT: 10px">
																<asp:label id="lblCode" runat="server" Width="70px">By_Code</asp:label></TD>
															<TD style="HEIGHT: 10px">
																<asp:dropdownlist id="cboDealerCode" runat="server" AutoPostBack="false" onchange="ToggleDualDropDownsSelection('cboDealerCode', 'cboDealer', 'rdealer', true,'D');"
																	width="84px"></asp:dropdownlist></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 791px"></TD>
															<TD style="WIDTH: 68px"></TD>
															<TD style="WIDTH: 68px">
																<asp:label id="lblDescription" runat="server" Width="84px">By_Description</asp:label></TD>
															<TD>
																<asp:dropdownlist id="cboDealer" runat="server" AutoPostBack="false" onchange="ToggleDualDropDownsSelection('cboDealerCode', 'cboDealer', 'rdealer', true, 'C');"
																	width="248px"></asp:dropdownlist></TD>
														</TR>
														<TR>
															<TD style="HEIGHT: 10px" vAlign="middle" align="center" colSpan="4" rowSpan="1">
																<HR style="WIDTH: 571px; HEIGHT: 1px" SIZE="1">
																&nbsp;</TD>
														</TR>																												
														<TR>
															<TD style="WIDTH: 791px" align="right">
																<asp:RadioButton id="RadiobuttonTotalsOnly" onclick="toggleDetailSelection(false);" AutoPostBack="false"
																	Text="SHOW TOTALS ONLY" TextAlign="left" Runat="server"></asp:RadioButton></TD>
															<TD style="WIDTH: 68px"></TD>
															<TD align="left" colSpan="2">
																<asp:RadioButton id="RadiobuttonDetail" onclick="toggleDetailSelection(true);" AutoPostBack="false"
																	Text="OR SHOW DETAIL WITH TOTALS" TextAlign="left" Runat="server"></asp:RadioButton></TD>
														</TR>
														<TR>
																<TD style="HEIGHT: 10px" vAlign="middle" align="center" colSpan="4" rowSpan="1"></TD>
														</TR>
														<TR>
																<TD style="WIDTH: 791px" align="right" valign="top">
																	<asp:label id="moSortByLabel" runat="server">SELECT_REPORT_SORT_ORDER</asp:label>:&nbsp;</TD>
																<TD style="WIDTH: 68px"></TD>
																<TD vAlign="bottom" align="left" colSpan="2">
																	<asp:RadioButtonList id="rdReportSortOrder" runat="server" RepeatDirection="VERTICAL">
																		<asp:ListItem Value="0" Selected="True">DEALER_CODE</asp:ListItem>
																		<asp:ListItem Value="1">DEALER_NAME</asp:ListItem>
																	</asp:RadioButtonList></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
											<TR>
												<TD align="center" colSpan="3"></TD>
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
		<script>
		function toggleCustomerRefunds(isIncluded)
		{
		//debugger;
		if(!isIncluded)
		{
			document.forms[0].rIncludeCustomerRefunds.checked = false;
		}
		else
		{
			document.forms[0].rExcludeCustomerRefunds.checked = false;
		}
		}
		</script>
	</body>
</HTML>
