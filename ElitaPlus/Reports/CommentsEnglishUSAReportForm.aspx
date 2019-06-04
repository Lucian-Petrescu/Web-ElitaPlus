<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CommentsEnglishUSAReportForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.CommentsEnglishUSAReportForm" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>CommentsEnglish_USA</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="../Styles.css" type="text/css" rel="STYLESHEET">
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>
	</HEAD>
	<body leftMargin="0" topMargin="0" onload="changeScrollbarColor();" MS_POSITIONING="GridLayout"
		border="0">
		<form id="Form1" method="post" runat="server">
			<!--Start Header--><input id="rptTitle" type="hidden" name="rptTitle"> <input id="rptSrc" type="hidden" name="rptSrc">
			<TABLE style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE width="100%" border="0">
							<TR>
								<TD height="20"><asp:label id="LabelReports" runat="server" CssClass="TITLELABEL">Reports</asp:label>:
									<asp:label id="Label7" runat="server" CssClass="TITLELABELTEXT">Comments</asp:label></TD>
								<TD align="right" height="20">*&nbsp;
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
															<TD align="center" width="50%" colSpan="3">
																<TABLE cellSpacing="0" cellPadding="0" width="75%" border="0">
																	<TR>
																		<TD vAlign="middle" noWrap align="right">*
																			<asp:label id="moBeginDateLabel" runat="server" >BEGIN_DATE</asp:label>:</TD>
																		<TD noWrap>&nbsp;
																			<asp:textbox id="moBeginDateText" tabIndex="1" runat="server" CssClass="FLATTEXTBOX"></asp:textbox>
																			<asp:imagebutton id="BtnBeginDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg"
																				Width="20px"></asp:imagebutton></TD>
																		<TD>&nbsp;&nbsp;</TD>
																		<TD vAlign="middle" noWrap align="right">*
																			<asp:label id="moEndDateLabel" runat="server" >END_DATE</asp:label>:</TD>
																		<TD noWrap>&nbsp;
																			<asp:textbox id="moEndDateText" tabIndex="2" runat="server" CssClass="FLATTEXTBOX" width="125px"></asp:textbox>
																			<asp:imagebutton id="BtnEndDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg"
																				Width="20px"></asp:imagebutton></TD>
																	</TR>
																</TABLE>
															</TD>
														</TR>
														<TR>
															<TD colSpan="3">&nbsp;</TD>
														</TR>
														<TR>
															<TD style="HEIGHT: 13px" noWrap align="right" width="20%">*
																<asp:RadioButton id="rcerts" onclick="toggleAllCertsSelection(false);" tabIndex="3" AutoPostBack="false"
																	Checked="False" Runat="server" Text="ALL CERTIFICATES" TextAlign="left"></asp:RadioButton></TD>
															<TD style="HEIGHT: 13px" width="2%"><IMG src="../Navigation/images/trans_spacer.gif" width="10"></TD>
															<TD style="HEIGHT: 13px" align="left" width="78%">
																<asp:label id="moCertNumberLabel" runat="server">OR A SINGLE CERTIFICATE NUMBER</asp:label>:
																<asp:TextBox id="moCertNumberTextbox" tabIndex="4" CssClass="FLATTEXTBOX" Width="252px" AutoPostBack="false"
																	Runat="server" onchange="toggleAllCertsSelection(true);"></asp:TextBox></TD>
														</TR>
														<TR>
															<TD style="HEIGHT: 13px" noWrap align="right" width="25%" colSpan="3"></TD>
														</TR>
														<TR>
															<TD vAlign="bottom" noWrap align="right" width="20%">*
																<asp:RadioButton id="rdealer" onclick="ToggleDualDropDownsSelection('multipleDropControl_moMultipleColumnDrop', 'multipleDropControl_moMultipleColumnDropDesc', 'rdealer', false, ''); document.all.item('multipleDropControl_lb_DropDown').style.color = '';"
																	tabIndex="5" AutoPostBack="false" Checked="False" Runat="server" Text="ALL DEALERS" TextAlign="Left"></asp:RadioButton></TD>
															<TD align="left" width="2%"></TD>
															<TD align="left" width="78%">
																<uc1:MultipleColumnDDLabelControl id="multipleDropControl" runat="server"></uc1:MultipleColumnDDLabelControl></TD>
														</TR>
														<TR>
															<TD noWrap align="center" width="25%" colSpan="3"><IMG src="../Navigation/images/trans_spacer.gif" width="10"></TD>
														</TR>
														<TR>
															<TD style="HEIGHT: 17px" noWrap align="right" width="20%">*
																<asp:RadioButton id="rcommentType" onclick="toggleAllCommentTypesSelection(false);" tabIndex="8"
																	Width="128px" AutoPostBack="false" Checked="False" Runat="server" Text="ALL COMMENT TYPES" TextAlign="left"></asp:RadioButton></TD>
															<TD style="HEIGHT: 17px" width="2%"><IMG src="../Navigation/images/trans_spacer.gif" width="10"></TD>
															<TD style="HEIGHT: 17px" noWrap align="left" width="78%">
																<asp:label id="moCommentTypeLabel" runat="server">OR A SINGLE COMMENT TYPE</asp:label>:
																<asp:dropdownlist id="cboCommentType" tabIndex="9" runat="server" Width="292px" AutoPostBack="false"
																	onchange="toggleAllCommentTypesSelection(true);"></asp:dropdownlist></TD>
														</TR>
														<TR>
															<TD colSpan="3">&nbsp;</TD>
														</TR>
														<TR>
															<TD noWrap align="right" width="20%">*
																<asp:RadioButton id="RadiobuttonAllRecords" onclick="toggleRecordsSelection(false);" tabIndex="10"
																	AutoPostBack="false" Runat="server" Text="INCLUDE ALL RECORDS" TextAlign="left"></asp:RadioButton></TD>
															<TD width="2%"><IMG src="../Navigation/images/trans_spacer.gif" width="10"></TD>
															<TD noWrap align="left" width="78%">
																<asp:RadioButton id="RadiobuttonExcludeClosedClaims" onclick="toggleRecordsSelection(true);" tabIndex="11"
																	AutoPostBack="false" Runat="server" Text="OR EXCLUDE CLOSED CLAIMS" TextAlign="left"></asp:RadioButton></TD>
														</TR>
														<TR>
															<TD colSpan="3">&nbsp;</TD>
														</TR>
														<TR>
															<TD noWrap align="right" width="20%">*
																<asp:RadioButton id="RadiobuttonAllComments" onclick="toggleCommentsSelection(false);" tabIndex="12"
																	AutoPostBack="false" Runat="server" Text="INCLUDE ALL COMMENTS" TextAlign="left"></asp:RadioButton></TD>
															<TD width="2%"><IMG src="../Navigation/images/trans_spacer.gif" width="10"></TD>
															<TD noWrap align="left" width="78%">
																<asp:RadioButton id="RadiobuttonClaimComments" onclick="toggleCommentsSelection(true);" tabIndex="13"
																	AutoPostBack="false" Runat="server" Text="OR CLAIMS COMMENTS ONLY" TextAlign="left"></asp:RadioButton></TD>
														</TR>
														<TR>
															<TD colSpan="3"><IMG height="3" src="../Navigation/images/trans_spacer.gif">
																<HR style="HEIGHT: 1px">
															</TD>
														</TR>
														<TR>
															<TD width="2%" colSpan="3">
																<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="100%" border="0">
																	<TR>
																		<TD vAlign="top" align="right" width="40%">*
																			<asp:label id="moSortByLabel" runat="server">SELECT_REPORT_SORT_ORDER</asp:label>:</TD>
																		<TD width="2%"></TD>
																		<TD width="58%">
																			<asp:RadioButtonList id="rdReportSortOrder" tabIndex="12" runat="server" RepeatDirection="VERTICAL">
																				<asp:ListItem Value="0" Selected="True">DEALER CODE</asp:ListItem>
																				<asp:ListItem Value="1">CERTIFICATE NUMBER</asp:ListItem>
																				<asp:ListItem Value="2">DATE COMMENT ADDED</asp:ListItem>
																				<asp:ListItem Value="3">COMMENT TYPE</asp:ListItem>
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
									<TD style="HEIGHT: 1px"></TD>
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
function toggleAllCertsSelection(isAllCertificates)
	{
		//debugger;
		if(isAllCertificates)
		{
			document.forms[0].rcerts.checked = false;
			document.all.item('moCertNumberLabel').style.color = '';
		}
		else
		{
			document.forms[0].moCertNumberTextbox.value = "";
			document.all.item('moCertNumberLabel').style.color = '';
		}
	}		
function toggleAllCommentTypesSelection(isCommentType)
	{
		//debugger;
		if(isCommentType)
		{
			document.forms[0].rcommentType.checked = false;
		}
		else
		{
			document.forms[0].cboCommentType.selectedIndex = -1;
		}
	}
	function toggleRecordsSelection(isAllRecords)
	{
		//debugger;
		if(!isAllRecords)
		{
			document.forms[0].RadiobuttonExcludeClosedClaims.checked = false;
		}
		else
		{
			document.forms[0].RadiobuttonAllRecords.checked = false;
		}
	}
	
	
	function toggleCommentsSelection(isAllComments)
	{
		//debugger;
		if(!isAllComments)
		{
			document.forms[0].RadiobuttonClaimComments.checked = false;
		}
		else
		{
			document.forms[0].RadiobuttonAllComments.checked = false;
		}
	}
	
		</script>
	</body>
</HTML>
