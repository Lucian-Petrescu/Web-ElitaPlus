<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="DealerPaymentReportForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.DealerPaymentReportForm"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title id="rptWindowTitle" runat="server">DealerPayment</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="../Styles.css" type="text/css" rel="STYLESHEET">
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>
	    <style type="text/css">
            .style1
            {
                width: 211px;
                height: 75px;
            }
            .style2
            {
                height: 75px;
            }
            .style3
            {
                width: 667px;
            }
        </style>
	</HEAD>
	<body leftMargin="0" topMargin="0" onload="changeScrollbarColor();" border="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--Start Header--><input id="rptTitle" type="hidden" name="rptTitle"> <input id="rptSrc" type="hidden" name="rptSrc">
			<TABLE style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE width="100%" border="0">
							<TR>
								<TD height="20"><asp:label id="LabelReports" runat="server" CssClass="TITLELABEL">Reports</asp:label>:&nbsp;
								<asp:label id="Label7" runat="server"  CssClass="TITLELABELTEXT">DEALER PAYMENT</asp:label></TD>
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
								cellSpacing="0" cellPadding="6" rules="cols" width="98%" align="center" bgColor="#fef9ea" height="98%"
								border="0">
								<TR>
									<TD height="1"></TD>
								</TR>
								<TR>
									<TD>
										<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
											<TR>
												<TD colSpan="3">
													 <table id="tblSearch" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                                                border-left: #999999 1px solid; width: 100%; border-bottom: #999999 1px solid;
                                                height: 76px" cellspacing="2" cellpadding="8" rules="cols" width="100%" align="center"
                                                bgcolor="#fef9ea" border="0">
                                                <tr>
                                                    <td>
                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                            <tr>
                                                                <td align="center" colspan="3">
                                                                    <uc1:ErrorController ID="ErrorCtrl" runat="server"></uc1:ErrorController>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <uc1:ReportCeInputControl ID="moReportCeInputControl" runat="server"></uc1:ReportCeInputControl>
                                                    </td>
                                                </tr>
                                            </table>
												</TD>
											</TR>
											<TR>
												<TD colSpan="3"><IMG height="15" src="../Navigation/images/trans_spacer.gif"></TD>
											</TR>
											<TR>
												<TD align="center" colSpan="3">
													<TABLE cellSpacing="2" cellPadding="0" width="85%" border="0">
														<TR>
															<TD colSpan="2">&nbsp;</TD>
														</TR>
														<TR>
															<TD style="WIDTH: 211px; HEIGHT: 32px" noWrap align="right" width="211">
																<asp:label id="moDealerLabel" runat="server">SELECT_DEALER</asp:label>:</TD>
															<TD style="HEIGHT: 32px" align="left">
																<asp:dropdownlist id="moDealerDrop" runat="server" AutoPostBack="true" Width="250px"></asp:dropdownlist></TD>
														</TR>
														<TR>
															<TD style="WIDTH: 211px">&nbsp;</TD>
															<TD>&nbsp;</TD>
														</TR>
														<TR>
															<TD style="WIDTH: 211px; HEIGHT: 18px" noWrap align="right" width="211">
																<asp:label id="moPaymentFileLabel" runat="server">SELECT PAYMENT FILE</asp:label>:</TD>
															<TD style="HEIGHT: 18px" align="left">
																<asp:dropdownlist id="moPaymentFileDrop" runat="server" AutoPostBack="false" Width="250px"></asp:dropdownlist></TD>
														</TR>
														<TR>
															<TD colSpan="2">
																<HR>
															</TD>
														</TR>
														<TR>
															<TD width="211" class="style1"></TD>
															<TD vAlign="top" align="left" width="90%" colSpan="2" class="style2">
																<asp:RadioButtonList id="moCriteriaRadioList" runat="server" RepeatDirection="VERTICAL">
																	<asp:ListItem Value="0" Selected="True">SHOW TOTALS ONLY</asp:ListItem>
																	<asp:ListItem Value="1">REJECTS ONLY</asp:ListItem>
																	<asp:ListItem Value="2">OR SHOW DETAIL WITH TOTALS</asp:ListItem>
																</asp:RadioButtonList></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
										</TABLE>
									</TD>
                                </tr>
                                <tr>
                                    <td style="height: 95%; width: 100%;">
                                    </td>
                                </tr>
                                <tr>
									<TD>
										<HR>
									</TD>
								</TR>
								<TR>
									<TD align="left">&nbsp;
										<asp:button id="btnGenRpt" style="BACKGROUND-IMAGE: url(../Navigation/images/viewIcon2.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server"  Width="100px" Text="View" CssClass="FLATBUTTON" height="20px"></asp:button></TD>
								</TR>
							</TABLE>
						</asp:panel></td>
				</tr>
			</TABLE>
		</form>
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/ReportCeMainScripts.js"></SCRIPT>
	</body>
</HTML>
