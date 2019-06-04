<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ClaimsPaidReportForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.ClaimsPaidReportForm" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title id="rptWindowTitle" runat="server">ClaimsPaid</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">

	<link href="../Styles.css" type="text/css" rel="STYLESHEET" />
    <SCRIPT language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>
    <script language="JavaScript">
        function toggleOptionSelection(ReportBy) {
            //debugger;
            if (ReportBy == 'D') {
                document.getElementById("RadiobuttonByInvoiceNumber").checked = false;
                document.getElementById("BeginDateLabel").style.display = '';
                document.getElementById("BeginDateText").style.display = '';
                document.getElementById("BtnBeginDate").style.display = '';
                document.getElementById("EndDateLabel").style.display = '';
                document.getElementById("EndDateText").style.display = '';
                document.getElementById("BtnEndDate").style.display = '';                
                document.getElementById("InvoiceNumberLabel").style.display = 'none';
                document.getElementById("InvoiceNumberTextbox").style.display = 'none';
                document.getElementById("PayeeLabel").style.display = 'none';
                document.getElementById("cboPayee").style.display = 'none';
            }
            else {
                document.getElementById("RadiobuttonByReportingPeriod").checked = false;
                document.getElementById("BeginDateLabel").style.display = 'none';
                document.getElementById("BeginDateText").style.display = 'none';
                document.getElementById("BtnBeginDate").style.display = 'none';
                document.getElementById("EndDateLabel").style.display = 'none';
                document.getElementById("EndDateText").style.display = 'none';
                document.getElementById("BtnEndDate").style.display = 'none';
                document.getElementById("InvoiceNumberLabel").style.display = '';
                document.getElementById("InvoiceNumberTextbox").style.display = '';
                document.getElementById("PayeeLabel").style.display = '';
                document.getElementById("cboPayee").style.display = '';
            }
        }
	</script>
</head>
<body leftMargin="0" topMargin="0" onload="changeScrollbarColor();" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--Start Header--><input id="rptTitle" type="hidden" name="rptTitle"> <input id="rptSrc" type="hidden" name="rptSrc">
			<table style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<tr>
					<td vAlign="top">
						<table width="100%" border="0">
							<tr>
								<td height="20"><asp:label id="LabelReports"  CssClass="TITLELABEL" runat="server">Reports</asp:label>:&nbsp;<asp:label id="Label7" runat="server" CssClass="TITLELABELTEXT">CLAIMS_PAID</asp:label></td>
								<td height="20" align="right">
                                *&nbsp;
                                <asp:Label ID="moIndicatesLabel" runat="server"  EnableViewState="true">INDICATES_REQUIRED_FIELDS</asp:Label></td>
                        </tr>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<table id="tblOuter2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				height="93%" cellSpacing="0" cellPadding="0" rules="none" width="98%" bgColor="#d5d6e4"
				border="0"> <!--ededd5-->
				<tr>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td vAlign="top" align="center" height="100%"><asp:panel id="WorkingPanel" runat="server">
							<table id="tblMain1" style="BORDER: #999999 1px solid;height:95%;" cellSpacing="0" cellPadding="6" rules="cols" width="98%" height="95%" align="center" bgColor="#fef9ea"
								border="0">
								<tr>
									<td height="1"></td>
								</tr>
								<tr>
									<td>
										<table cellSpacing="0" cellPadding="0" width="100%" border="0">
											<tr>
												<td colSpan="3">
													<table id="tblSearch" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; WIDTH: 100%; BORDER-BOTTOM: #999999 1px solid; HEIGHT: 64px"
														cellSpacing="2" cellPadding="8" rules="cols" width="100%" align="center" bgColor="#fef9ea"
														border="0">
														<tr>
															<td>
																<table cellSpacing="0" cellPadding="0" width="100%" border="0">
																	<tr>
																		<td align="center" colSpan="3">
																			<uc1:ErrorController id="ErrorCtrl" runat="server"></uc1:ErrorController>
                                                                        </td>
																	</tr>
																</table>
																<uc1:ReportCeInputControl id="moReportCeInputControl" runat="server"></uc1:ReportCeInputControl>
                                                            </td>
														</tr>
													</table>
												</td>
											</tr>
											<tr>
												<td colSpan="3"><IMG height="15" src="../Navigation/images/trans_spacer.gif"></td>
											</tr>
											<tr>
												<td align="center" colspan="3">
													<table cellspacing="2" cellpadding="0" width="75%" border="0">
														<tr>														
															<td nowrap align="right">
																<asp:RadioButton id="RadiobuttonByReportingPeriod" onclick="toggleOptionSelection('D');" TextAlign="left"
																	Text="Base Report On Reporting Period" Runat="server" AutoPostBack="false"></asp:RadioButton>
                                                            </td>
															<td nowrap align="left"  >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																<asp:RadioButton id="RadiobuttonByInvoiceNumber" onclick="toggleOptionSelection('I');" TextAlign="left"
																	Text="Base Report On An Invoice Number" Runat="server" AutoPostBack="false"></asp:RadioButton>
                                                            </td>
														</tr>
														<tr>
															<td colspan="2">&nbsp;</td>
														</tr>
														<tr>
															<td align="center" width="50%" colspan="2">
																<table cellspacing="0" cellpadding="0" width="75%" border="0">
																	<tr>
																		<td vAlign="middle" nowrap align="right">
																			<asp:label id="BeginDateLabel" runat="server">BEGIN_DATE:</asp:label>
                                                                            <asp:label id="InvoiceNumberLabel" runat="server" >INVOICE_NUMBER:</asp:label>&nbsp;
                                                                        </td>
																		<td nowrap  align="left">
																			<asp:textbox id="BeginDateText" tabIndex="1" runat="server" CssClass="FLATTEXTBOX" Width="125px"></asp:textbox>
                                                                            <asp:imagebutton id="BtnBeginDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" Height="17px" Width="20px"></asp:imagebutton>
                                                                            <asp:textbox id="InvoiceNumberTextbox" tabIndex="1" runat="server" AutoPostBack="True" CssClass="FLATTEXTBOX"></asp:textbox>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td valign="middle" nowrap align="right">&nbsp;
																		    <asp:label id="EndDateLabel" runat="server">END_DATE:</asp:label>
                                                                            <asp:label id="PayeeLabel" runat="server" >Payee:</asp:label>&nbsp;
																		</td>
																		<td nowrap align="left" style="padding-top:3px;">
                                                                            <asp:textbox id="EndDateText" tabIndex="1" runat="server" CssClass="FLATTEXTBOX" width="125px"></asp:textbox>
																			<asp:imagebutton id="BtnEndDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" Height="17px" Width="20px"></asp:imagebutton>
                                                                            <asp:DropDownList id="cboPayee" runat="server" width="300px"></asp:DropDownList>                                                                            
                                                                        </td>																		
																	</tr>																																											
																</table>
                                                             </td>															
														</tr>
														<tr>
															<td colspan="3">&nbsp;</td>
														</tr>
														<tr>
															<td colspan="3"><hr /></td>
														</tr>
													</table>
												</td>
											</tr>
										</table>
									</td>
								</tr>
								<tr>
									<td style="HEIGHT:85%; width:100%;"></td>
								</tr>
								<tr>
									<td width="100%"><hr /></td>
								</tr>
								<tr>
									<td align="left">&nbsp;
										<asp:button id="btnGenRpt" style="BACKGROUND-IMAGE: url(../Navigation/images/viewIcon2.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat;"
											runat="server"  Text="View" CssClass="FLATBUTTON" Width="100px" height="20px" EnableTheming="False"></asp:button></td>
								</tr>
							</table>
						</asp:panel>
                    </td>
				</tr>
			</table>
		</form>
		<script language="JavaScript" src="../Navigation/Scripts/ReportCeMainScripts.js" />
	</body>

</html>
