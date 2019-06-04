<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ClaimsByManufacturerEnglishUSAFreeZoneReportForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.ClaimsByManufacturerEnglishUSAFreeZoneReportForm" %>
<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Claims By Manufacturer including Free Zone</title>
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
								<asp:label id="Label7" runat="server" CssClass="TITLELABELTEXT">CLAIMS_BY_MANUFACTURER_INCLUDING_FREE_ZONE</asp:label></TD>
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
												<TD align="center" colSpan="3">
													<TABLE cellSpacing="2" cellPadding="0" width="75%" border="0">
														<TR>
															<TD align="center" width="50%" colSpan="3">
																<TABLE cellSpacing="0" cellPadding="0" width="75%" border="0">
																	<TR>
																		<TD width="50%">&nbsp;</TD>
																		<TD noWrap align="right">
																			<asp:label id="moBeginDateLabel" runat="server" >BEGIN_DATE</asp:label>:</TD>
																		<TD vAlign="middle" noWrap>&nbsp;
																			<asp:textbox id="moBeginDateText" tabIndex="1" runat="server" CssClass="FLATTEXTBOX"></asp:textbox>&nbsp;
																		</TD>
																		<TD>
																			<asp:imagebutton id="BtnBeginDate" runat="server" Width="20px" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:imagebutton></TD>
																		<TD noWrap align="right">&nbsp;&nbsp;
																			<asp:label id="moEndDateLabel" runat="server" >END_DATE</asp:label>:</TD>
																		<TD vAlign="middle" noWrap>&nbsp;
																			<asp:textbox id="moEndDateText" tabIndex="1" runat="server" CssClass="FLATTEXTBOX" width="125px"></asp:textbox>&nbsp;</TD>
																		<TD>
																			<asp:imagebutton id="BtnEndDate" runat="server" Width="20px" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:imagebutton></TD>
																		<TD width="50%">&nbsp;</TD>
																	</TR>
																</TABLE>
															</TD>
														</TR>
														<TR>
															<TD colSpan="3">&nbsp;</TD>
														</TR>
														<TR>
															<TD noWrap align="right" width="25%" colSpan="2">
																<asp:RadioButton id="rmanufacturer" onclick="toggleAllManufacturesSelection(false);" AutoPostBack="false"
																	Checked="False" Runat="server" Text="PLEASE CHOOSE ALL MANUFACTURERS" TextAlign="left"></asp:RadioButton></TD>
															<TD noWrap align="left" width="25%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																<asp:label id="moManufacturerLabel" runat="server">OR A SINGLE MANUFACTURER</asp:label>&nbsp;&nbsp;
																<asp:dropdownlist id="cboManufacturer" runat="server" AutoPostBack="false" onchange="toggleAllManufacturesSelection(true);"></asp:dropdownlist></TD>
														</TR>

                                                                                                            <tr>
                                                        <td nowrap align="right" width="25%" colspan="2">
                                                            <asp:RadioButton ID="rSvcCtr" Checked="True"
                                                                TextAlign="left" Text="PLEASE SELECT ALL SERVICE CENTERS" runat="server" AutoPostBack="false">
                                                            </asp:RadioButton></td>
                                                        <td nowrap align="left" width="25%">
                                                            &nbsp;&nbsp;
                                                        </td>
                                                        <td nowrap align="left" width="40%"> &nbsp;&nbsp;
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                            <td nowrap align="right" width="25%">
                                                                <asp:CheckBox ID="CheckBoxNoFreeZone" onclick="toggleCheckBox('CheckBoxNoFreeZone','CheckBoxFreeZone');" Text="NO_FREE_ZONE" AutoPostBack="false" Checked="False" TextAlign="left" runat="server"></asp:CheckBox>
                                                                <asp:CheckBox ID="CheckBoxFreeZone" onclick="toggleCheckBox('CheckBoxFreeZone','CheckBoxNoFreeZone');" Text="FREE_ZONE" AutoPostBack="false" Checked="False" TextAlign="left" runat="server"></asp:CheckBox>
                                                            </td>
                                                            <td width="15">
                                                                <img src="../Navigation/images/trans_spacer.gif" width="15">
                                                            </td>
                                                            <td nowrap align="left" width="50%">
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
											runat="server"  CssClass="FLATBUTTON" Width="100px" Text="View" height="20px"></asp:button></TD>
								</TR>
							</TABLE>
						</asp:panel></td>
				</tr>
			</TABLE>
		</form>
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/ReportCeMainScripts.js"></SCRIPT>
		<script>
		    function toggleAllManufacturesSelection(isSingleManufacturer) {
		        //debugger;
		        if (isSingleManufacturer) {
		            document.forms[0].rmanufacturer.checked = false;
		        }
		        else {
		            document.forms[0].cboManufacturer.selectedIndex = -1;
		        }
		    }
		</script>
	</body>
</HTML>
