<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="InvoicesToBePaidFreeZoneForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.InvoicesToBePaidFreeZoneForm" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
	    <title id="rptWindowTitle" runat="server">INVOICES TO BE PAID INCLUDING FREE ZONE</title>
	    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
        <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
        <meta content="JavaScript" name="vs_defaultClientScript">
        <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
        <link href="../Styles.css" type="text/css" rel="STYLESHEET">
            

		<SCRIPT language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>
		<script>
		    function DisplayPayeeRow() {
		        document.getElementById("PayeeRow").style.display = 'block';
		    }

		    function toggleOptionSelection(isByReportingPeriod) {
		        //debugger;
		        if (isByReportingPeriod) {
		            document.getElementById("RadiobuttonByInvoiceNumber").checked = false;
		            document.getElementById("InvoiceRow").style.display = 'none';
		            document.getElementById("PayeeRow").style.display = 'none';
		            document.getElementById("DateRow").style.display = 'block';
		        }
		        else {
		            document.getElementById("RadiobuttonByReportingPeriod").checked = false;
		            document.getElementById("InvoiceNumberTextbox").innerText = "";
		            document.getElementById("InvoiceRow").style.display = 'block';
		            document.getElementById("DateRow").style.display = 'none';
		            document.getElementById("PayeeRow").style.display = 'none';
		        }
		    }
		</script>
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
								<TD height="20"><asp:label id="LabelReports"  CssClass="TITLELABEL" runat="server">Reports</asp:label>:&nbsp;<asp:label id="Label7" runat="server" CssClass="TITLELABELTEXT">INVOICES_TO_BE_PAID_INCLUDING_FREE_ZONE</asp:label></TD>
								<td height="20" align="right">
                                *&nbsp;
                                <asp:Label ID="moIndicatesLabel" runat="server"  EnableViewState="true">INDICATES_REQUIRED_FIELDS</asp:Label></td>
                        </tr>
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
								cellSpacing="0" cellPadding="6" rules="cols" width="98%" height= "95%" align="center" bgColor="#fef9ea"
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
													<TABLE cellSpacing="2" cellPadding="0" width="75%" border="0">
														<TR>														
															<TD noWrap align="right">
																<asp:RadioButton id="RadiobuttonByReportingPeriod" onclick="toggleOptionSelection(true);" TextAlign="left"
																	Text="Base Report On Reporting Period" Runat="server" AutoPostBack="false"></asp:RadioButton></TD>
															<TD noWrap align="left"  >&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																<asp:RadioButton id="RadiobuttonByInvoiceNumber" onclick="toggleOptionSelection(false);" TextAlign="left"
																	Text="Base Report On An Invoice Number" Runat="server" AutoPostBack="false"></asp:RadioButton></TD>
															<td></td>		
																	
														</TR>
														<TR>
															<TD>&nbsp;</TD>
															<TD>&nbsp;</TD>
															<TD>&nbsp;</TD>
														</TR>
														<TR>
															<TD align="center" width="50%" colSpan="3">
																<TABLE cellSpacing="0" cellPadding="0" width="75%" border="0">
																	<TR id=DateRow <%=toggleDisplay("period")%>>
																		<TD width="50%">&nbsp;</TD>
																		<TD vAlign="middle" noWrap align="right">
																			<asp:label id="BeginDateLabel" runat="server" >BEGIN_DATE</asp:label>:</TD>
																		<TD noWrap>&nbsp;
																			<asp:textbox id="BeginDateText" tabIndex="1" runat="server" CssClass="FLATTEXTBOX"></asp:textbox>&nbsp;</TD>
																		<TD>
																			<asp:imagebutton id="BtnBeginDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" Height="17px"
																				Width="20px"></asp:imagebutton></TD>
																		<TD vAlign="middle" noWrap align="right">&nbsp;&nbsp;
																			<asp:label id="EndDateLabel" runat="server" >END_DATE</asp:label>:</TD>
																		<TD noWrap>&nbsp;
																			<asp:textbox id="EndDateText" tabIndex="1" runat="server" CssClass="FLATTEXTBOX" width="125px"></asp:textbox>&nbsp;
																		</TD>
																		<TD>
																			<asp:imagebutton id="BtnEndDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" Height="17px"
																				Width="20px"></asp:imagebutton></TD>
																		<TD width="50%">&nbsp;</TD>
																	</TR>
																	<TR id=InvoiceRow <%=toggleDisplay("invoice")%>>
																		<TD width="50%">&nbsp;</TD>	
																		<TD vAlign="middle" noWrap align="right">
																			<asp:label id="InvoiceNumberLabel" runat="server" >INVOICE_NUMBER</asp:label>:</TD>
																		<TD noWrap>&nbsp;
																			<asp:textbox id="InvoiceNumberTextbox" tabIndex="1" runat="server" AutoPostBack="True" CssClass="FLATTEXTBOX"
																				onblur="DisplayPayeeRow()"></asp:textbox>&nbsp;</TD>
																		<TD width="50%">&nbsp;</TD>
																	</TR>
																	<TR>
																	<TD>&nbsp;</TD>
																	</TR>
																	<TR id= PayeeRow <%=toggleDisplay("payee")%>>
																		<TD width="50%">&nbsp;</TD>
																	  	<TD vAlign="middle" noWrap align="right">
																			<asp:label id="PayeeLabel" runat="server" >Payee</asp:label>:</TD>
																		<TD>&nbsp;
																			<asp:DropDownList id="cboPayee" runat="server" width="300px"></asp:DropDownList></TD>
																		<TD width="50%">&nbsp;</TD>
																	</TR>
																	<TR>
																		<TD width="50%">&nbsp;</TD>
																	  	<TD vAlign="middle" noWrap align="right" colspan =2>
																			 <asp:CheckBox ID="chkSvcCode" Text="INCLUDE_SERVICE_CENTER_CODE" AutoPostBack="false"
                                                                        runat="server" TextAlign="Left"></asp:CheckBox>                                                     																		
																		<TD width="50%">&nbsp;</TD>
																	</TR>
																	<TR>
																		<TD width="50%">&nbsp;</TD>
																	  	<TD vAlign="middle" noWrap align="right" colspan =2>
																			 <asp:CheckBox ID="chkCustomerAddress" Text="INCLUDE_CUSTOMER_ADDRESS" AutoPostBack="false"
                                                                        runat="server" TextAlign="Left"></asp:CheckBox>                                                     																		
																		<TD width="50%">&nbsp;</TD>
																	</TR>	
                                                                    
                                                    <tr>
                                                        <td nowrap align="right" width="25%" colspan="2">
                                                            <asp:RadioButton ID="rSvcCtr" Checked="False"
                                                                TextAlign="left" Text="PLEASE SELECT ALL SERVICE CENTERS" runat="server" AutoPostBack="false">
                                                            </asp:RadioButton></td>
                                                        <td nowrap align="left" width="25%">
                                                            &nbsp;&nbsp;
                                                        </td>
                                                        <td nowrap align="left" width="40%"> &nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                            <TD width="50%">&nbsp;</TD>
                                                            <td  vAlign="middle" noWrap align="right" colspan =2>
                                                                <asp:CheckBox ID="CheckBoxNoFreeZone" onclick="toggleCheckBox('CheckBoxNoFreeZone','CheckBoxFreeZone');" Text="NO_FREE_ZONE" AutoPostBack="false" Checked="False" TextAlign="left" runat="server"></asp:CheckBox>
                                                                <asp:CheckBox ID="CheckBoxFreeZone" onclick="toggleCheckBox('CheckBoxFreeZone','CheckBoxNoFreeZone');" Text="FREE_ZONE" AutoPostBack="false" Checked="False" TextAlign="left" runat="server"></asp:CheckBox>
                                                            </td>
                                                            <TD width="50%">&nbsp;</TD>
                                                    </tr>																
												</TABLE>
															</TD>
														</TR>
														<TR>
															<TD>&nbsp;</TD>
															<TD>&nbsp;</TD>
															<TD>&nbsp;</TD>
														</TR>
														<TR>
															<TD colSpan="3">
																<HR>
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
											runat="server"  Text="View" CssClass="FLATBUTTON" Width="100px" height="20px"></asp:button></TD>
								</TR>
							</TABLE>
						</asp:panel></td>
				</tr>
			</TABLE>
		</form>
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/ReportCeMainScripts.js"></SCRIPT>
	</body>
</HTML>
