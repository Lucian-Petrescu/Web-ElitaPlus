<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="OpenedClaimsReportForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.OpenedClaimsReportForm" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title id="rptWindowTitle" runat="server">ClaimsByDateOpened</title>
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
								<TD height="20"><asp:Label ID="LabelReports"  CssClass="TITLELABEL" runat="server">Reports</asp:label>:&nbsp;<asp:Label ID="Label7" runat="server"  CssClass="TITLELABELTEXT">Claim Detail By Date Opened</asp:label></TD>
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
											<TR>
												<TD align="center" colSpan="4">
													<TABLE cellSpacing="2" cellPadding="0" width="75%" border="0">
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
																			<asp:textbox id="moEndDateText" tabIndex="1" runat="server" CssClass="FLATTEXTBOX" width="125px"></asp:textbox>&nbsp;</TD>
																		<TD>
																			<asp:imagebutton id="BtnEndDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:imagebutton></TD>
																		<TD width="50%">&nbsp;</TD>
																	</TR>
																</TABLE>
															</TD>
														</TR>
														<TR>
															<TD colSpan="4">
																<HR style="HEIGHT: 1px">
															</TD>
														</TR>
														<TR>
															<TD noWrap align="right" width="25%" colSpan="2">
																<asp:RadioButton id="rDealer" onclick="toggleAllDealersSelection(false);" AutoPostBack="false" Runat="server"
																	Text="SELECT_ALL_DEALERS" TextAlign="left" Checked="False"></asp:RadioButton></TD>
															<TD noWrap align="left" width="25%">&nbsp;&nbsp;
																<asp:label id="moDealerLabel" runat="server">OR A SINGLE DEALER</asp:label>&nbsp;&nbsp;
															</TD>
															<TD noWrap align="left" width="40%">
																<asp:dropdownlist id="cboDealer" runat="server" width="100%" AutoPostBack="false" onchange="toggleAllDealersSelection(true);"></asp:dropdownlist></TD>
														</TR> <!--<TR>
															<TD colSpan="4"><IMG height="3" src="../Navigation/images/trans_spacer.gif"></TD>
														</TR>-->
														<TR>
															<TD colSpan="4">
																<HR style="HEIGHT: 1px">
															</TD>
														</TR>
														<TR id="ddHideRow">
															<TD noWrap align="right" width="25%" colSpan="2">
																<asp:RadioButton id="rCountry" onclick="toggleAllCountiesSelection(false);" AutoPostBack="True" Runat="server"
																	Text="PLEASE_SELECT_ALL_COUNTRIES" TextAlign="left" Checked="False"></asp:RadioButton></TD>
															<TD noWrap align="left" width="25%">&nbsp;&nbsp;
																<asp:label id="moCountryLabel" runat="server">OR_SELECT_SINGLE_COUNTRY</asp:label>&nbsp;&nbsp;
															</TD>
															<TD noWrap align="left" width="40%">
																<asp:dropdownlist id="cboCountry" runat="server" width="100%" AutoPostBack="True" onchange="toggleAllCountiesSelection(true);"></asp:dropdownlist></TD>
														</TR> <!--<TR>
															<TD colSpan="4"><IMG height="3" src="../Navigation/images/trans_spacer.gif"></TD>
														</TR>-->
														<TR>
															<TD noWrap align="right" width="25%" colSpan="2">
																<asp:RadioButton id="rSvcCtr" onclick="toggleAllSvcCtrsSelection(false);" AutoPostBack="false" Runat="server"
																	Text="PLEASE SELECT ALL SERVICE CENTERS" TextAlign="left" Checked="False"></asp:RadioButton></TD>
															<TD noWrap align="left" width="25%">&nbsp;&nbsp;
																<asp:label id="SvcCtrLabel" runat="server">OR A SINGLE SERVICE CENTER</asp:label>&nbsp;&nbsp;</TD>
															<TD noWrap align="left" width="40%">
																<asp:dropdownlist id="cboSvcCtr" runat="server" width="100%" AutoPostBack="false" onchange="toggleAllSvcCtrsSelection(true);"></asp:dropdownlist></TD>
														</TR> <!--<TR>
															<TD colSpan="4"><IMG height="3" src="../Navigation/images/trans_spacer.gif"></TD>
														</TR>-->
														<TR>
															<TD colSpan="4">
																<HR style="HEIGHT: 1px">
															</TD>
														</TR>
														<TR>
															<TD noWrap align="right" width="25%" colSpan="2">
																<asp:RadioButton id="rRiskType" onclick="toggleAllRiskTypesSelection(false);" AutoPostBack="false"
																	Runat="server" Text="PLEASE SELECT ALL RISK TYPES" TextAlign="left" Checked="False"></asp:RadioButton></TD>
															<TD noWrap align="left" width="25%">&nbsp;&nbsp;
																<asp:label id="RiskTypeLabel" runat="server">OR A SINGLE RISK TYPE</asp:label>&nbsp;&nbsp;</TD>
															<TD noWrap align="left" width="25%">
																<asp:dropdownlist id="cboRiskType" runat="server" width="100%" AutoPostBack="false" onchange="toggleAllRiskTypesSelection(true);"></asp:dropdownlist></TD>
														</TR>
														<TR>
															<TD colSpan="4"><IMG height="3" src="../Navigation/images/trans_spacer.gif"></TD>
														</TR>
                                                        <tr>
                                                            <td nowrap align="right" width="25%" colspan="2">
                                                                <asp:RadioButton ID="rMethodofRepair" onclick="toggleAllRepairSelection(false);"
                                                                    Checked="False" TextAlign="left" Text="PLEASE_SELECT_ALL_METHOD_OF_REPAIR" runat="server"
                                                                    AutoPostBack="false"></asp:RadioButton></td>
                                                            <td nowrap align="left" width="25%">
                                                                &nbsp;&nbsp;
                                                                <asp:Label ID="lblMethodofRepair" runat="server">OR_A_SINGLE_METHOD_OF_REPAIR</asp:Label>&nbsp;&nbsp;</td>
                                                            <td nowrap align="left" width="25%">
                                                                <asp:DropDownList ID="cboMethodofRepair" runat="server" Width="100%" AutoPostBack="false"
                                                                    onchange="toggleAllRepairSelection(true);">
                                                                </asp:DropDownList></td>
                                                        </tr>
														<TR>
															<TD colSpan="4"><IMG height="3" src="../Navigation/images/trans_spacer.gif"></TD>
														</TR>
														<tr>
                                                            <td nowrap align="right" width="25%" colspan="2">
                                                                <asp:RadioButton ID="rCoverageType" onclick="toggleAllCovergeTypesSelection(false);" Checked="False"
                                                                    TextAlign="left" Text="PLEASE_SELECT_ALL_COVERAGE_TYPES" runat="server" AutoPostBack="false">
                                                                </asp:RadioButton></td>
                                                            <td nowrap align="left" width="25%">
                                                                &nbsp;&nbsp;
                                                                <asp:Label ID="lblCoverageType" runat="server">OR_A_SINGLE_COVERAGE_TYPE</asp:Label>&nbsp;&nbsp;</td>
                                                            <td nowrap align="left" width="25%">
                                                                <asp:DropDownList ID="cboCoverageType" runat="server" Width="100%" AutoPostBack="false" 
                                                                    onchange="toggleAllCovergeTypesSelection(true);">
                                                                </asp:DropDownList></td>
                                                    </tr>
                                                    <TR>
															<TD colSpan="4"><IMG height="3" src="../Navigation/images/trans_spacer.gif"></TD>
														</TR>
														<TR>
															<TD noWrap align="right" width="25%" colSpan="2">
																<asp:RadioButton id="rAllUsers" onclick="toggleAllUsersSelection(false);" AutoPostBack="false" Runat="server"
																	Text="PLEASE_SELECT_ALL_USERS" TextAlign="left" Checked="False"></asp:RadioButton></TD>
															<TD noWrap align="left" width="25%">&nbsp;&nbsp;
																<asp:label id="lblUserId" runat="server">OR_ENTER_A_USER_ID</asp:label>&nbsp;&nbsp;</TD>
															<TD noWrap align="left" width="25%">
																<asp:TextBox id="txtUserId" runat="server" CssClass="FLATTEXTBOX" width="100%" AutoPostBack="false"
																	onFocus="toggleAllUsersSelection(true);"></asp:TextBox></TD>
														</TR>
														<TR>
															<TD colSpan="4"></TD>
														</TR>
														<TR>
															<TD colSpan="4">
																<HR style="HEIGHT: 1px">
															</TD>
														</TR>
														<TR>
															<TD vAlign="top" noWrap align="center" width="25%" colSpan="4"><TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
																	<TR>
																		<TD vAlign="top" noWrap align="right" width="25%">
																			<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
																				<TR>
																					<TD><IMG height="7" src="../Navigation/images/trans_spacer.gif" width="1"></TD>
																				</TR>
																				<TR>
																					<TD noWrap align="right">
																						<asp:label id="Label5" runat="server">SELECT_REPORT_SORT_ORDER</asp:label>:</TD>
																				</TR>
																			</TABLE>
																		</TD>
																		<TD vAlign="top" width="55%" colSpan="2">
																			<asp:RadioButtonList id="rdReportSortOrder" runat="server" RepeatDirection="Vertical">
																				<asp:ListItem Value="1" Selected="True">Claim_Number</asp:ListItem>
																				<asp:ListItem Value="3">Risk_Type</asp:ListItem>
																				<asp:ListItem Value="2">Service_Center_Name</asp:ListItem>
																				<asp:ListItem Value="4">Date Claim Opened</asp:ListItem>
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
									<TD style="HEIGHT: 3px"></TD>
								</TR>
								<TR>
									<TD>
										<HR>
									</TD>
								</TR>
								<TR>
									<TD align="left">&nbsp;
										<asp:button id="btnGenRpt" style="BACKGROUND-IMAGE: url(../Navigation/images/viewIcon2.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server"  CssClass="FLATBUTTON" Text="View" Width="100px" height="20px"></asp:button></TD>
								</TR>
							</TABLE>
						</asp:panel></td>
				</tr>
			</TABLE>
		</form>
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/ReportCeMainScripts.js"></SCRIPT>
		<script>

		    function toggleAllUsersSelection(isSingleUser) {
		        //debugger;
		        if (isSingleUser) {
		            document.forms[0].rAllUsers.checked = false;
		        }
		        else {
		            document.forms[0].txtUserId.value = "";
		        }
		    }

		    function toggleAllCountiesSelection(isSingleCountry) {
		        //debugger;
		        if (isSingleCountry) {
		            document.forms[0].rCountry.checked = false;
		        }
		        else {
		            document.forms[0].cboCountry.selectedIndex = -1;
		        }
		    }

		    function toggleAllDealersSelection(isSingleDealer) {
		        //debugger;
		        if (isSingleDealer) {
		            document.forms[0].rDealer.checked = false;
		        }
		        else {
		            document.forms[0].cboDealer.selectedIndex = -1;
		        }
		    }

		    function toggleAllSvcCtrsSelection(isSingleSvcCtr) {
		        //debugger;
		        if (isSingleSvcCtr) {
		            document.forms[0].rSvcCtr.checked = false;
		        }
		        else {
		            document.forms[0].cboSvcCtr.selectedIndex = -1;
		        }
		    }

		    function toggleAllRiskTypesSelection(isSingleRiskType) {
		        //debugger;
		        if (isSingleRiskType) {
		            document.forms[0].rRiskType.checked = false;
		        }
		        else {
		            document.forms[0].cboRiskType.selectedIndex = -1;
		        }
		    }

		    function toggleAllRepairSelection(isSingleRepairType) {
		        //debugger;
		        if (isSingleRepairType) {
		            document.forms[0].rMethodofRepair.checked = false;
		        }
		        else {
		            document.forms[0].cboMethodofRepair.selectedIndex = -1;
		        }
		    }
		    function toggleAllCovergeTypesSelection(isSingleCoverageType) {
		        //debugger;
		        if (isSingleCoverageType) {
		            document.forms[0].rCoverageType.checked = false;
		        }
		        else {
		            document.forms[0].cboCoverageType.selectedIndex = -1;
		        }
		    }
		</script>
	</body>
</HTML>
