<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDropControl" Src="../Common/MultipleColumnDropControl.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ServiceCenterLocatorReportForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.ServiceCenterLocatorReportForm"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ServiceCenterLocator</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="../Styles.css" type="text/css" rel="STYLESHEET">
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>
        <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.6.1.min.js"></script>
		<SCRIPT language="JavaScript">
		    $(document).ready(function () {		       
		        $('.fromToInput').find('select').css({ height: '' });
		    });

			function MultipleColumnChanged()
			{
				ToggleDualDropDownsSelection('moFromMultipleColumnDropControl_moMultipleColumnDrop', 'moToMultipleColumnDropControl_moMultipleColumnDrop', 'moAllServiceCentersRadio', false)
				ToggleDualDropDownsSelection('moFromMultipleColumnDropControl_moMultipleColumnDropDesc', 'moToMultipleColumnDropControl_moMultipleColumnDropDesc', 'moAllServiceCentersRadio', false)
				var objFromCodeDropDown = document.getElementById('moFromMultipleColumnDropControl_moMultipleColumnDrop'); // "By Code" DropDown control
				var objFromDecDropDown = document.getElementById('moFromMultipleColumnDropControl_moMultipleColumnDropDesc');   // "By Description" DropDown control 
				var objToCodeDropDown = document.getElementById('moToMultipleColumnDropControl_moMultipleColumnDrop'); // "By Code" DropDown control
				var objToDecDropDown = document.getElementById('moToMultipleColumnDropControl_moMultipleColumnDropDesc');   // "By Description" DropDown control 
				objFromCodeDropDown.selectedIndex = 0;
				objFromDecDropDown.selectedIndex = 0;
				objToCodeDropDown.selectedIndex = 0;
				objToDecDropDown.selectedIndex = 0;				
			}										
		</SCRIPT>
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
								<TD height="20"><asp:label id="LabelReports" runat="server" cssclass="TITLELABEL">Reports</asp:label>:&nbsp;
								<asp:label id="moTitleLabel" runat="server" cssclass="TITLELABELTEXT">Service_Center_Locator</asp:label></TD>
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
																</TABLE>
																<uc1:ReportCeInputControl id="moReportCeInputControl" runat="server"></uc1:ReportCeInputControl></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
											<TR>
												<TD colSpan="3"><IMG height="15" src="../Navigation/images/trans_spacer.gif"></TD>
											</TR>
											<TR id="ddSeparator">
												<TD align="center" colSpan="4">
													<TABLE id="tblCountry" cellSpacing="2" cellPadding="0" width="75%" border="0">
														<TR>
															<TD noWrap align="left" width="15%">&nbsp;&nbsp;
																<asp:label id="moCountryLabel" runat="server">SELECT_SINGLE_COUNTRY</asp:label>&nbsp;&nbsp;</TD>
															<TD noWrap align="left" width="35%">
																<asp:dropdownlist id="cboCountry" runat="server" AutoPostBack="True" width="100%"></asp:dropdownlist></TD>
															<TD noWrap align="left" width="50%"></TD>
														</TR>
														<TR>
															<TD colSpan="4">
																<HR style="HEIGHT: 1px">
																<IMG height="1" src="../Navigation/images/trans_spacer.gif"></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
											<TR>
												<TD>
													<TABLE id="tblSericeCenter" cellSpacing="2" cellPadding="8" rules="cols" width="100%" align="center"
														bgColor="#fef9ea" border="0">
														<TR>
															<TD align="center" colSpan="4">
																<TABLE cellSpacing="2" cellPadding="0" width="75%" border="0">
                                                                    <tr>
                                                                        <td nowrap align="right" width="40%" colspan="2">
                                                                            <asp:RadioButton ID="rSvcNetwork" onclick="ToggleSingleDropDownSelection('dpSvcNetwork', 'rSvcNetwork',false);" runat="server"
                                                                                Text="PLEASE_SELECT_ALL_SERVICE_NETWORKS" AutoPostBack="true" TextAlign="left" Checked="False"></asp:RadioButton>
                                                                        </td>
                                                                        <td nowrap align="left" width="25%">
                                                                            <asp:Label ID="lblSvcNetwork" runat="server">or_a_single_service_network</asp:Label>&nbsp;&nbsp;
                                                                        </td>
                                                            </td>
                                                            <td nowrap align="left" width="40%">
                                                                <asp:DropDownList ID="dpSvcNetwork" runat="server" AutoPostBack="true" onchange="ToggleSingleDropDownSelection('dpSvcNetwork', 'rSvcNetwork', true);"
                                                                    Width="230px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">
                                                                <hr style="height: 1px">
                                                                <img height="1" src="../Navigation/images/trans_spacer.gif">
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td nowrap align="right" width="40%" colspan="2">
                                                                <asp:RadioButton ID="moAllServiceCentersRadio" onclick="MultipleColumnChanged();"
                                                                    runat="server" Text="PLEASE SELECT ALL SERVICE CENTERS" TextAlign="left" Checked="False"></asp:RadioButton></TD>
																		<TD noWrap align="left" width="25%">&nbsp;&nbsp; &nbsp;&nbsp;</TD>
																		<TD noWrap align="left" width="40%"></TD>
																	</TR>
																	<TR>
																		<TD colSpan="4"><IMG height="3" src="../Navigation/images/trans_spacer.gif"></TD>
																	</TR>
																	<TR>
																		<TD style="HEIGHT: 2px" noWrap align="right" width="50%" colSpan="2">
																			<asp:label id="moRangeServiceCentersLabel" runat="server">or a range of service centers</asp:label>:&nbsp;&nbsp;</TD>
																		<TD style="HEIGHT: 2px" noWrap align="left" width="25%"></TD>
																		<TD style="HEIGHT: 2px" noWrap align="left" width="25%"></TD>
																	</TR>
																</TABLE>
																<TABLE cellSpacing="2" cellPadding="0" width="75%" border="0" class="fromToInput">
																	<TR>
																		<TD colSpan="4"><IMG height="3" src="../Navigation/images/trans_spacer.gif"></TD>
																	</TR>
																	<TR>
																		<TD style="HEIGHT: 2px" noWrap align="right" width="25%" colSpan="1">
																			<asp:label id="moFromLabel" runat="server">FROM</asp:label>:&nbsp;&nbsp;</TD>
																		<TD style="HEIGHT: 2px" noWrap align="left" width="75%" colSpan="3">
																			<uc1:MultipleColumnDropControl id="moFromMultipleColumnDropControl" runat="server"></uc1:MultipleColumnDropControl></TD>
																	</TR>
																	<TR>
																		<TD colSpan="4"><IMG height="3" src="../Navigation/images/trans_spacer.gif"></TD>
																	</TR>
																	<TR>
																		<TD style="HEIGHT: 2px" noWrap align="right" width="25%" colSpan="1">
																			<asp:label id="moToLabel" runat="server">TO</asp:label>:&nbsp;&nbsp;</TD>
																		<TD style="HEIGHT: 2px" noWrap align="left" width="75%" colSpan="3">
																			<uc1:MultipleColumnDropControl id="moToMultipleColumnDropControl" runat="server"></uc1:MultipleColumnDropControl></TD>
																	</TR>
																</TABLE>
																<TABLE cellSpacing="2" cellPadding="0" width="75%" border="0">
																	<TR>
																		<TD colSpan="4">
																			<HR style="HEIGHT: 1px">
																		</TD>
																	</TR>
																	<TR>
																		<TD noWrap align="right" width="25%" colSpan="2">
																			<asp:RadioButton id="moAllServiceGroupsRadio" onclick="ToggleSingleDropDownSelection('moServiceGroupDrop', 'moAllServiceGroupsRadio',false);"
																				AutoPostBack="false" Runat="server" Text="Please select all service groups" TextAlign="left"
																				Checked="False"></asp:RadioButton></TD>
																		<TD noWrap align="left" width="25%">&nbsp;&nbsp;
																			<asp:label id="moServiceGroupLabel" runat="server">or a single service group</asp:label>&nbsp;&nbsp;</TD>
																		<TD noWrap align="left" width="40%">
																			<asp:dropdownlist id="moServiceGroupDrop" runat="server" AutoPostBack="false" onchange="ToggleSingleDropDownSelection('moServiceGroupDrop', 'moAllServiceGroupsRadio', true);"
																				Width="230px"></asp:dropdownlist></TD>
																	</TR>
																	<TR>
																		<TD colSpan="4"><IMG height="3" src="../Navigation/images/trans_spacer.gif"></TD>
																	</TR>
																	<TR>
																		<TD noWrap align="right" width="25%" colSpan="2">
																			<asp:RadioButton id="moAllPriceListsRadio" onclick="ToggleSingleDropDownSelection('moPriceListDrop', 'moAllPriceListsRadio',false);"
																				AutoPostBack="false" Runat="server" Text="Please select all price lists" TextAlign="left" Checked="False"></asp:RadioButton></TD>
																		<TD noWrap align="left" width="25%">&nbsp;&nbsp;
																			<asp:label id="moPriceListLabel" runat="server">or a single price list</asp:label>&nbsp;&nbsp;</TD>
																		<TD noWrap align="left" width="25%">
																			<asp:dropdownlist id="moPriceListDrop" runat="server" AutoPostBack="false" onchange="ToggleSingleDropDownSelection('moPriceListDrop', 'moAllPriceListsRadio', true);"
																				Width="230px"></asp:dropdownlist></TD>
																	</TR>
																	<TR>
																		<TD colSpan="4"><IMG height="3" src="../Navigation/images/trans_spacer.gif"></TD>
																	</TR>
																	<TR>
																		<TD noWrap align="right" width="25%" colSpan="2">
																			<asp:RadioButton id="moAllMethodofRepairRadio" onclick="ToggleSingleDropDownSelection('moMethodofRepairDrop', 'moAllMethodofRepairRadio',false);"
																				AutoPostBack="false" Runat="server" Text="Please_select_all_method_of_repair" TextAlign="left"
																				Checked="False"></asp:RadioButton></TD>
																		<TD noWrap align="left" width="25%">&nbsp;&nbsp;
																			<asp:label id="moMethodofRepairLabel" runat="server">OR_A_SINGLE_METHOD_OF_REPAIR</asp:label>&nbsp;&nbsp;</TD>
																		<TD noWrap align="left" width="25%">
																			<asp:dropdownlist id="moMethodofRepairDrop" runat="server" AutoPostBack="false" onchange="ToggleSingleDropDownSelection('moMethodofRepairDrop', 'moAllMethodofRepairRadio', true);"
																				Width="230px"></asp:dropdownlist></TD>
																	</TR>
																	<TR>
																		<TD colSpan="4"><IMG height="1" src="../Navigation/images/trans_spacer.gif"></TD>
																	</TR>
																	<TR>
																		<TD colSpan="4">
																			<HR style="HEIGHT: 1px">
																		</TD>
																	</TR>
																	<TR>
																		<TD vAlign="top" noWrap align="center" width="25%" colSpan="4">
																			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
																				<TR>
																					<TD vAlign="top" noWrap align="right" width="25%">
																						<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
																							<TR>
																								<TD noWrap align="right">
																									<asp:label id="moSortOrderLabel" runat="server">SELECT_REPORT_SORT_ORDER</asp:label>:</TD>
																							</TR>
																						</TABLE>
																					</TD>
																					<TD vAlign="top" width="65%" colSpan="2">
																						<asp:RadioButtonList id="moSortOrderRadioList" runat="server" RepeatDirection="Vertical">
																							<asp:ListItem Value="1" Selected="True">State</asp:ListItem>
																							<asp:ListItem Value="2">City</asp:ListItem>
																							<asp:ListItem Value="3">Service Group Code</asp:ListItem>
																							<asp:ListItem Value="4">METHOD_OF_REPAIR</asp:ListItem>
																						</asp:RadioButtonList></TD>
																				</TR>
																			</TABLE>
																		</TD>
																	</TR>
																</TABLE>
															</TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
											<TR>
												<TD>
													<HR>
												</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD align="left">&nbsp;
										<asp:button id="btnGenRpt" style="BACKGROUND-IMAGE: url(../Navigation/images/viewIcon2.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server" Font-Bold="false" Text="View" Width="100px" height="20px" CssClass="FLATBUTTON"></asp:button></TD>
								</TR>
							</TABLE>
						</asp:panel></td>
				</tr>
			</TABLE>
		</form>
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/ReportCeMainScripts.js"></SCRIPT>
	</body>
</HTML>
