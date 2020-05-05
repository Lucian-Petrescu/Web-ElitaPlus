<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ExchangeRateForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.ExchangeRateForm"%>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Exchange Rate</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="STYLESHEET">
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>
	</HEAD>
	<body onresize="resizeForm(document.getElementById('scroller'));" leftMargin="0" topMargin="0"
		onload="changeScrollbarColor();" MS_POSITIONING="GridLayout" border="0">
		<form id="Form1" method="post" runat="server">
			<!--Start Header-->
			<TABLE style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE width="100%" border="0">
							<TR>
								<TD height="20"><asp:label id="moTitleLabel1" runat="server" Cssclass="TITLELABEL">Tables</asp:label>:
									<asp:label id="moTitleLabel2" runat="server" Cssclass="TITLELABELTEXT">EXCHANGE_RATE</asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="tblOuter2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				height="93%" cellSpacing="0" cellPadding="0" rules="none" width="98%" bgColor="#d5d6e4"
				border="0">
				<tr>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td vAlign="top" align="center">
						<TABLE id="moOutTable" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid"
							height="98%" cellSpacing="0" cellPadding="6" width="98%" align="center" bgColor="#fef9ea"
							border="0">
							<tr>
								<td style="WIDTH: 97.56%; HEIGHT: 1px" align="center" colSpan="2">&nbsp;&nbsp;
									<uc1:errorcontroller id="ErrorCtrl" runat="server"></uc1:errorcontroller></td>
							</tr>
							<TR>
								<TD style="WIDTH: 95%; HEIGHT: 200px" align="center" colSpan="1" rowSpan="1">
									<DIV id="scroller" style="OVERFLOW: auto; WIDTH: 98.14%; HEIGHT: 100%" align="center">
										<asp:panel id="EditPanel_WRITE" runat="server" Width="98.93%" Height="100%">
											<TABLE id="Table1" style="WIDTH: 703px " cellSpacing="1" cellPadding="0"
												width="703" border="0">
												<TR>
													<TD width="50%" colSpan="4">&nbsp;
													</TD>
												</TR>
												<TR>
                                                    <td valign="top" align="left" colspan="4">
                                                       <table cellspacing="0" cellpadding="0" border="0">
                                                         <tr>
                                                             <td nowrap align="center" valign=top colspan="4" style="padding-bottom:5px;">
                                                             <uc1:MultipleColumnDDLabelControl ID="multipleDealerDropControl" runat="server"></uc1:MultipleColumnDDLabelControl>                                                             
                                                             </td>
                                                          </tr>
                                                        </table>
                                                    </td>
                                                </tr>												
												<tr>
												    <td colspan =4>
												        <hr style="height:1px"/></td>
												</tr>
												<TR>
													<TD style="WIDTH: 129px; HEIGHT: 20px" noWrap align="right" width="129">&nbsp;
														<asp:label id="lblFromCurrencyId" runat="server">From_Currency</asp:label></TD>
													<TD style="WIDTH: 251px; HEIGHT: 20px" width="251">&nbsp;
														<asp:dropdownlist id="cboFromCurrencyId" runat="server" Width="205px"></asp:dropdownlist></TD>
													<TD style="WIDTH: 113px; HEIGHT: 20px" noWrap align="right" width="113">
														<asp:label id="lblExchangeFrom" runat="server">Exchange_Rate</asp:label></TD>
													<TD style="HEIGHT: 20px" width="50%">&nbsp;
														<asp:textbox id="txtFromCurrency" runat="server" Wrap="False"></asp:textbox>&nbsp;
														<asp:Button id="btnGetRate_WRITE" runat="server" Text="GET_RATE" Visible="False"></asp:Button></TD>
												</TR>
												<TR>
													<TD style="WIDTH: 129px; HEIGHT: 11px" noWrap align="right" width="129">&nbsp;
														<asp:label id="lblToCurrencyId" runat="server">To_Currency</asp:label></TD>
													<TD style="WIDTH: 251px; HEIGHT: 11px" width="251">&nbsp;
														<asp:dropdownlist id="cboToCurrencyID" runat="server" Width="205px"></asp:dropdownlist></TD>
													<TD style="WIDTH: 113px; HEIGHT: 11px" noWrap align="right" width="113">
														<asp:label id="lblExchangeTo" runat="server">Exchange_Rate</asp:label></TD>
													<TD style="HEIGHT: 11px" width="50%">&nbsp;
														<asp:textbox id="txtToCurrency" runat="server" Wrap="False"></asp:textbox>&nbsp;</TD>
												</TR>
												<TR>
													<TD style="WIDTH: 129px; HEIGHT: 15px" noWrap align="right" width="129">&nbsp;
														<asp:label id="lblEffectiveDate" runat="server">Effective_Date</asp:label></TD>
													<TD style="WIDTH: 251px; HEIGHT: 15px" width="251">&nbsp;
														<asp:textbox id="txtEffectiveDate" runat="server" Width="88px" Wrap="False"></asp:textbox>
														<asp:imagebutton id="btnEffectiveDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:imagebutton></TD>
													<TD style="WIDTH: 113px; HEIGHT: 15px" noWrap align="right" width="113"></TD>
													<TD style="HEIGHT: 15px" width="50%">&nbsp;&nbsp;
													</TD>
												</TR>
												<TR>
													<TD style="WIDTH: 129px; HEIGHT: 2px" noWrap align="right" width="129">&nbsp;
														<asp:label id="lblExpirationDate" runat="server">Expiration_Date</asp:label>
													</TD>
													<TD style="WIDTH: 251px; HEIGHT: 2px" align="left" width="251" colSpan="1" rowSpan="1">&nbsp;
														<asp:textbox id="txtExpirationDate" runat="server" Width="88px"></asp:textbox>
														<asp:imagebutton id="btnExpirationDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:imagebutton></TD>
													<TD style="WIDTH: 113px; HEIGHT: 2px" noWrap align="right" width="113"></TD>
													<TD style="HEIGHT: 2px" width="50%">&nbsp;
													</TD>
												</TR>
												<TR>
													<TD colSpan="4">
														<asp:Label id="lblCurrencyID" runat="server" Visible="False"></asp:Label>
                                                        <asp:Label id="moIsNewCurrencyLabel" runat="server" Visible="False"></asp:Label><INPUT id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
                                                                                                                                               runat="server"/>
													</TD>
												</TR>
												<TR>
													<TD style="WIDTH: 129px; HEIGHT: 18px" noWrap align="right" width="129">&nbsp;
													</TD>
													<TD style="WIDTH: 251px; HEIGHT: 18px" width="251">&nbsp;
													</TD>
													<TD style="WIDTH: 113px; HEIGHT: 18px" noWrap align="right" width="113"></TD>
													<TD style="HEIGHT: 18px" width="50%">&nbsp;
													</TD>
												</TR>
												<TR>
													<TD style="HEIGHT: 12px" colSpan="4"></TD>
												</TR>
												<TR>
													<TD colSpan="4">&nbsp;</TD>
												</TR>
											</TABLE>
										</asp:panel></DIV>
								</TD>
							<TR>
								<TD style="HEIGHT: 12px" align="center" colSpan="2"><HR style="WIDTH: 95%; HEIGHT: 2px" SIZE="2">
								</TD>
							</TR>
							<TR>
								<TD align="center" colSpan="2" rowSpan="1">
									<TABLE id="Table2" style="WIDTH: 542px; HEIGHT: 24px" cellSpacing="1" cellPadding="1" width="542"
										align="left" border="0">
										<TR>
											<TD><asp:button id="btnBack" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/back_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
													runat="server" Width="90px" Text="BACK" CssClass="FLATBUTTON" height="20px" CausesValidation="False"></asp:button></TD>
											<TD><asp:button id="btnApply_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/save_trans_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
													runat="server" Width="100px" Text="SAVE" CssClass="FLATBUTTON" height="20px" CausesValidation="False"></asp:button></TD>
											<TD><asp:button id="btnUndo_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/cancel_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
													runat="server" Width="90px" Text="UNDO" CssClass="FLATBUTTON" height="20px" CausesValidation="False"></asp:button></TD>
											<TD><asp:button id="btnNew_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
													runat="server" Width="100px" Text="New" CssClass="FLATBUTTON" height="20px" CausesValidation="False"></asp:button></TD>
											<TD><asp:button id="btnCopy_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
													runat="server" Width="135px" Text="New_With_Copy" CssClass="FLATBUTTON" height="20px" CausesValidation="False"></asp:button></TD>
											<TD><asp:button id="btnDelete_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/clear_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
													runat="server" Width="100px" Text="Delete" CssClass="FLATBUTTON" height="20px" CausesValidation="False"></asp:button></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</TABLE>
					</td>
				</tr>
				<TR>
					<TD vAlign="top" align="center"></TD>
				</TR>
			</TABLE>
			<script>
			  resizeForm(document.getElementById("scroller"));
			</script>
		</form>
	</body>
</HTML>
