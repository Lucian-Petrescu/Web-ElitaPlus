
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" Codebehind="DealerReconciliationReportForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.DealerReconciliationReportForm"%>
<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		 <title id="rptWindowTitle" runat="server">Dealer Reconciliation</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="../Styles.css" type="text/css" rel="STYLESHEET">
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>
	</HEAD>
	<body leftMargin="0" topMargin="0" onload="changeScrollbarColor();" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
		 <asp:ScriptManager ID="ScriptManager1" runat="server" />
			<!--Start Header--><input id="rptTitle" type="hidden" name="rptTitle"> <input id="rptSrc" type="hidden" name="rptSrc">
			<TABLE style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE width="100%" border="0">
							<TR>
								<TD height="20"><asp:label id="LabelReports" runat="server" CssClass="TITLELABEL">Reports</asp:label>:&nbsp;
								<asp:label id="Label7" runat="server" CssClass="TITLELABELTEXT">Dealer Reconciliation</asp:label></TD>
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
																			<UC1:ERRORCONTROLLER id="ErrorCtrl" runat="server"></UC1:ERRORCONTROLLER></TD>
																	</TR>
																	<TR>
																		<TD noWrap align="right">&nbsp;
																		</TD>
																	</TR>
																</TABLE>
																<UC1:REPORTCEINPUTCONTROL id="moReportCeInputControl" runat="server"></UC1:REPORTCEINPUTCONTROL></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
											<TR>
												<TD colSpan="3"><IMG height="15" src="../Navigation/images/trans_spacer.gif"></TD>
											</TR>
											<TR>
												<TD align="center" colSpan="3">
													<TABLE cellSpacing="2" cellPadding="0" width="75%" border="0">
														<TR>
															<TD style="HEIGHT: 12px" align="center" width="50%" colSpan="3"></TD>
														</TR>
														<TR>
															<TD colSpan="3">&nbsp;</TD>
														</TR>
														 <tr>
                                                            <td align="center" colspan="3">
                                                                <table id="Table1" border="0" cellpadding="1" cellspacing="1" width="100%">
                                                                    <tr>
                                                                        <td align="right" valign="bottom" width="23%">
                                                                        </td>
                                                                        <td nowrap valign="baseline" width="75%">
                                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                <ContentTemplate>
                                                                                    <uc1:MultipleColumnDDLabelControl ID="UserCompanyMultiDrop" runat="server" />
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                         <tr id="trsep" runat="server">
                                                    <td colspan="3">
                                                        <hr style="width: 99.43%; height: 1px"></hr>
                                                    </td>
                                                </tr>
                                               
                                                        <tr>
                                                            <td align="center" colspan="3">
                                                                <table id="Table2" border="0" cellpadding="1" cellspacing="1" width="100%">
                                                                    <tr>
                                                                        <td align="right" valign="bottom" width="23%">
                                                                        </td>
                                                                        <td nowrap valign="baseline" width="75%">
                                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                                                <ContentTemplate>
                                                                                    <uc1:MultipleColumnDDLabelControl ID="multipleDropControl" runat="server" />
                                                                                </ContentTemplate>
                                                                                 <Triggers>
                                                                                <asp:AsyncPostBackTrigger ControlID="UserCompanyMultiDrop" />
                                                                                     <asj:AsyncPostBackTrigger ControlID="UserCompanyMultiDrop" />
                                                                                </Triggers>
                                                                            </asp:UpdatePanel>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>												
														<TR>
															<TD>&nbsp;</TD>
															<TD>&nbsp;</TD>
															<TD>&nbsp;</TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
										</TABLE>
									</TD>
                                </tr>
                                <tr>
                                    <td style="height: 85%; width: 100%;">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
										<HR>
									</TD>
								</TR>
								<TR>
									<TD align="left">&nbsp;
										<asp:button id="btnGenRpt" style="BACKGROUND-IMAGE: url(../Navigation/images/viewIcon2.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server"  CssClass="FLATBUTTON" height="20px" Text="View" Width="100px"></asp:button></TD>
								</TR>
							</TABLE>
						</asp:panel></td>
				</tr>
			</TABLE>
		</form>
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/ReportCeMainScripts.js"></SCRIPT>
	</body>
</HTML>
