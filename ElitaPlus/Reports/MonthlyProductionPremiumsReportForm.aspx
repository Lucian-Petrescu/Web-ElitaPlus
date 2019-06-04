<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="MonthlyProductionPremiumsReportForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.MonthlyProductionPremiumsReportForm"%>
<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>MonthlyProductionPremiums</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="../Styles.css" type="text/css" rel="STYLESHEET">
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>
		<SCRIPT language="JavaScript">
	     //var arrDealerGroupCtr = [[['cboDealerCode'],['cboDealerDescription']],['rbdealer'],['rDealerGroup'],['moDealerGroupDrop']]
	     var arrDealerGroupCtr = [[['multipleDropControl_moMultipleColumnDropDesc'],['multipleDropControl_moMultipleColumnDrop']],['rdealer'],['rGroup'],['cboDealerGroup']]	     
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
								<TD height="20"><asp:label id="LabelReports" runat="server" CssClass="TITLELABEL">Reports</asp:label>:
								<asp:label id="Label7" runat="server" CssClass="TITLELABELTEXT">MONTHLY PRODUCTION - PREMIUMS REPORT CRITERIA</asp:label></TD>
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
															<TD noWrap align="right" width="25%" colSpan="2">*
																<asp:label id="moYearLabel" runat="server" >SELECT REPORTING YEAR</asp:label>:</TD>
															<TD noWrap align="left" width="25%">&nbsp;
																<asp:DropDownList id="moYearDrop" runat="server"></asp:DropDownList></TD>
														</TR>
														<TR>
															<TD style="HEIGHT: 30px" colSpan="3">&nbsp;
																<HR>
															</TD>
														</TR>
														<TR>
															<TD vAlign="top" colSpan="3">
																<TABLE id="Table2" style="HEIGHT: 106px" cellSpacing="1" cellPadding="1" width="100%" border="0">
																	<TR>
																		<TD vAlign="bottom" noWrap align="right">*
																			<asp:RadioButton id="rdealer" onclick=" document.all.item('multipleDropControl_lb_DropDown').style.color = ''; ToggleExt(this, arrDealerGroupCtr);"
																				type="radio" GroupName="Dealer" AutoPostBack="false" Checked="True" Runat="server" Text="SELECT_ALL_DEALERS"
																				TextAlign="left"></asp:RadioButton></TD>
																		<TD vAlign="top" noWrap></TD>
																		<TD vAlign="bottom" noWrap colSpan="2">
																			<uc1:MultipleColumnDDLabelControl id="multipleDropControl" runat="server"></uc1:MultipleColumnDDLabelControl></TD>
																	</TR>
																	<TR>
																		<TD style="HEIGHT: 14px" align="left" colSpan="4"></TD>
																	</TR>
																	<TR>
																		<TD style="WIDTH: 233px; HEIGHT: 14px"></TD>
																		<TD style="WIDTH: 24px; HEIGHT: 14px"></TD>
																		<TD style="WIDTH: 118px; HEIGHT: 14px"></TD>
																		<TD style="HEIGHT: 14px"></TD>
																	</TR>
																	<TR>
																		<TD noWrap align="right">
																			<asp:RadioButton id="rGroup" onclick="ToggleExt(this, arrDealerGroupCtr); document.all.item('multipleDropControl_lb_DropDown').style.color = '';"
																				tabIndex="1" type="radio" AutoPostBack="false" Runat="server" Text="SELECT_ALL_GROUPS" TextAlign="left"></asp:RadioButton></TD>
																		<TD></TD>
																		<TD noWrap align="left" colSpan="2">
																			<asp:label id="moDealerGroupLabel" runat="server">OR_A_SINGLE_GROUP</asp:label>:
																			<asp:dropdownlist id="cboDealerGroup" runat="server" type="DropDown" Width="248px" AutoPostBack="false"
																				onchange="ToggleExt(this, arrDealerGroupCtr); document.all.item('multipleDropControl_lb_DropDown').style.color = '';"></asp:dropdownlist></TD>
																	</TR>
																</TABLE>
															</TD>
														</TR>
														<TR>
															<TD style="HEIGHT: 7px" colSpan="3">
																<HR>
															</TD>
														</TR>
														<TR>
															<TD noWrap align="right" width="25%" colSpan="2">*
																<asp:RadioButton id="RadiobuttonTotalsOnly" onclick="toggleDetailSelection(false);" AutoPostBack="false"
																	Runat="server" Text="SHOW TOTALS ONLY" TextAlign="left"></asp:RadioButton></TD>
															<TD noWrap align="left" width="25%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																<asp:RadioButton id="RadiobuttonDetail" onclick="toggleDetailSelection(true);" AutoPostBack="false"
																	Runat="server" Text="OR SHOW DETAIL WITH TOTALS" TextAlign="left"></asp:RadioButton></TD>
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
											runat="server"  Width="100px" Text="View" CssClass="FLATBUTTON" height="20px"></asp:button></TD>
								</TR>
							</TABLE>
						</asp:panel></td>
				</tr>
			</TABLE>
		</form>
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/ReportCeMainScripts.js"></SCRIPT>
		<SCRIPT language="JavaScript">
	   
	 
		function ToggleMultDropDownsSelection(ctlCodeDropDown, ctlDecDropDown, ctlRadioButton, isSingleSelection, change_Dec_Or_Code,Selector)
	    {
	       if (selector = "G" ){ 
	          ToggleDualDropDownsSelection('cboDealerCode', 'cboDealer', 'rdealer', false, '');
	          ToggleDualDropDownsSelection('cboDealerGroup', 'moDealerGroupDrop', 'rGroup', false, '');
	       } 
	  	}
	  	
	  	function ToggleDealerGroupList()
	    {
	     	document.getElementById('cboDealerCode').selectedIndex = -1;
		    document.getElementById('cboDealer').selectedIndex = -1;
	  	}
	  	
	  	
	    function ToggleDealerList()
	    {
	       document.getElementById('cboDealerGroup').selectedIndex = -1;
	  	}
	
		</SCRIPT>
	</body>
</HTML>
