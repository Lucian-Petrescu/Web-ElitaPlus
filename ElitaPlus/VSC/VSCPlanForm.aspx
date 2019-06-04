<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="VSCPlanForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.VSCPlanForm" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>VSC_Plan</title>
		<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="STYLESHEET">
		
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>
	</HEAD>
	<body onresize="resizeForm(document.getElementById('scroller'));" leftMargin="0" topMargin="0"
		onload="changeScrollbarColor();" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--Start Header-->
			<TABLE style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE width="100%" border="0">
							<TR>
								<TD height="20"><asp:label id="moTitleLabel1" runat="server" CssClass="titlelabel">Tables</asp:label>:
									<asp:label id="moTitleLabel2" runat="server" CssClass="titlelabeltext">VSC_Plan</asp:label></TD>
								<TD align="right" height="20"><STRONG>*</STRONG>
									<asp:label id="moIndicatesLabel" runat="server" Font-Bold="false" EnableViewState="true">INDICATES_REQUIRED_FIELDS</asp:label></TD>
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
								<td style="HEIGHT: 36px" align="center" colSpan="2">&nbsp;&nbsp;
									<uc1:errorcontroller id="ErrorCtrl" runat="server"></uc1:errorcontroller></td>
								<TD style="HEIGHT: 36px" align="center"></TD>
							</tr>
							<TR>
								<TD>
									<DIV id="scroller" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 365px" align="center"><asp:panel id="EditPanel_WRITE" runat="server" Height="320px">
											<TABLE id="Table1" style="WIDTH: 98%; HEIGHT: 110px" cellSpacing="1" cellPadding="0" width="100%"
												border="0">
												<TR>
													<TD noWrap align="right" width="*">&nbsp;
														<asp:label id="lblCode" runat="server">Code</asp:label></TD>
													<TD>&nbsp;
														<asp:textbox id="txtCode" tabIndex="1" runat="server" Width="200px"></asp:textbox></TD>
												</TR>
												<TR>
													<TD noWrap align="right" width="*">
														<asp:label id="lblDesc" runat="server">Description</asp:label></TD>
													<TD>&nbsp;
														<asp:textbox id="txtDesc" tabIndex="3" runat="server" Width="299px"></asp:textbox></TD>
												</TR>
												<TR>
													<TD noWrap align="right">&nbsp;
														<asp:label id="lblRiskGroup" runat="server">Risk_Group</asp:label></TD>
													<TD>&nbsp;
														<asp:dropdownlist id="cboRiskGroup" runat="server" Width="300px" AutoPostBack="true"></asp:dropdownlist></TD>
												</TR>
												<TR>
													<TD align="right">&nbsp;
														<asp:label id="lblRiskType" runat="server">Risk_Type</asp:label></TD>
													<TD>&nbsp;
														<asp:dropdownlist id="cboRiskType" runat="server" Width="300px" AutoPostBack="False"></asp:dropdownlist></TD>
												</TR>
												<TR>
													<TD noWrap align="right">
														<asp:label id="lblWrapPlan" runat="server">Wrap_Plan</asp:label></TD>
													<TD> 
														<asp:CheckBox id="chkWrapPlan" runat="server" style="padding-left:4px"></asp:CheckBox></TD>
												</TR>
												<TR>
													<TD colSpan="4"></TD>
												</TR>
											</TABLE>
										</asp:panel>
										<DIV></DIV>
										<DIV>&nbsp;</DIV>
									</DIV>
								</TD>
							</TR>
							<TR>
								<TD align="left" colSpan="2">
									<HR style="WIDTH: 100%; HEIGHT: 1px" SIZE="1">
								</TD>
							</TR>
							<TR>
								<TD colSpan="4"><asp:label id="lblEntityId" runat="server" Visible="False"></asp:label><asp:label id="moIsNewEntityLabel" runat="server" Visible="False"></asp:label><INPUT id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
										runat="server">
								</TD>
							</TR>
							<TR>
								<TD align="right" colSpan="2">
									<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="300" align="left" border="0">
										<TR>
											<TD><asp:button id="btnBack" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/back_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
													tabIndex="14" runat="server" Width="90px" CausesValidation="False" height="20px" CssClass="FLATBUTTON"
													Text="BACK"></asp:button></TD>
											<TD><asp:button id="btnApply_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/save_trans_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
													tabIndex="15" runat="server" Width="100px" CausesValidation="False" height="20px" CssClass="FLATBUTTON"
													Text="SAVE"></asp:button></TD>
											<TD><asp:button id="btnUndo_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/cancel_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
													tabIndex="16" runat="server" Width="90px" CausesValidation="False" height="20px" CssClass="FLATBUTTON"
													Text="UNDO"></asp:button></TD>
											<TD><asp:button id="btnNew_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
													tabIndex="17" runat="server" Width="100px" CausesValidation="False" height="20px" CssClass="FLATBUTTON"
													Text="New"></asp:button></TD>
											<TD><asp:button id="btnCopy_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
													tabIndex="18" runat="server" Width="135px" CausesValidation="False" height="20px" CssClass="FLATBUTTON"
													Text="New_With_Copy"></asp:button></TD>
											<TD><asp:button id="btnDelete_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/clear_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
													tabIndex="19" runat="server" Width="100px" CausesValidation="False" height="20px" CssClass="FLATBUTTON"
													Text="Delete"></asp:button></TD>
										</TR>
									</TABLE>
								</TD>
								<TD align="right"></TD>
							</TR>
						</TABLE>
						<DIV></DIV>
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
		</TR></TBODY></TABLE>
	</body>
</HTML>
