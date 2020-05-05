<%@ Register TagPrefix="uc1" TagName="UserControlAvailableSelected" Src="../Common/UserControlAvailableSelected.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SoftQuestionGroupForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.SoftQuestionGroupForm" %>
<%@ Register TagPrefix="mytab" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Soft Questions Edit Form</title> <!-- ************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BOEditingWebForm.cst (11/12/2004)  ******************** -->
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
					<TD style="vertical-align:top;">
						<TABLE width="100%" border="0">
							<TR>
								<TD height="20">
									<P>&nbsp;
										<asp:label id="LabelTables" runat="server" Cssclass="TITLELABEL">Tables</asp:label>:
										<asp:label id="Label40" runat="server"  Cssclass="TITLELABELTEXT">Soft Questions Group</asp:label></P>
								</TD>
								<TD align="right" height="20"><STRONG>*</STRONG>
									<asp:label id="Label9" runat="server" Font-Bold="false">INDICATES_REQUIRED_FIELDS</asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="tblOuter2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				height="93%" cellSpacing="0" cellPadding="0" rules="none" width="98%" bgColor="#d5d6e4"
				border="0"><!--d5d6e4-->
				<tr>
					<td>&nbsp;</td>
				</tr>
				<TR>
					<TD style="vertical-align:middle;text-align:center;">
						<uc1:ErrorController id="ErrorCtrl" runat="server"></uc1:ErrorController>&nbsp;</TD>
				</TR>
				<tr>
					<td style="vertical-align:top;text-align:center;"><asp:panel id="WorkingPanel" runat="server" Width="99.41%" Height="98%">
							<TABLE id="tblMain1" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid"
								height="98%" cellSpacing="0" cellPadding="6" rules="cols" width="98%" align="center"
								bgColor="#fef9ea" border="0">
								
								<TR>
									<TD height="1" style="vertical-align:top;">
										<DIV id="scroller" style="OVERFLOW: auto; WIDTH: 99.7%; HEIGHT: 235px" align="center">
											<asp:Panel id="EditPanel_WRITE" runat="server">
												<TABLE id="Table1" style="WIDTH: 100%" cellSpacing="1" cellPadding="0" width="710" align="center" border="0">
													<TR>
														<TD style="WIDTH: 25%;vertical-align:middle;text-align:right;">
															<asp:label id="LabelDescription" runat="server" Font-Bold="false">Description</asp:label></TD>
														<TD style="WIDTH: 75%;vertical-align:middle;text-align:left;">&nbsp;
															<asp:textbox id="TextboxDescription" tabIndex="10" runat="server" Width="289px" CssClass="FLATTEXTBOX"></asp:textbox></TD>
													</TR>
													<TR>
														<TD colSpan="2"></TD>
													</TR>
													<TR>
														<TD style="HEIGHT: 14px" colSpan="2">
															<HR style="WIDTH: 98%; HEIGHT: 1px" SIZE="1">
														</TD>
													</TR>
													<TR>
														<TD style="vertical-align:middle;text-align:center;" colSpan="2">
															<uc1:UserControlAvailableSelected id="UserControlAvailableSelectedRiskType" runat="server"></uc1:UserControlAvailableSelected></TD>
													</TR>
												</TABLE>
											</asp:Panel></DIV>
									</TD>
								</TR>
								<TR>
									<TD style="HEIGHT: 39px" align="left" colSpan="4">
										<HR style="WIDTH: 100%; HEIGHT: 1px" SIZE="1">
									</TD>
								</TR>
								<TR>
									<TD style="vertical-align:bottom;white-space:nowrap; text-align:left;" height="20">
										<asp:button id="btnBack" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/back_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											tabIndex="185" runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" height="20px"
											Text="Back"></asp:button>&nbsp;
										<asp:button id="btnSave_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/save_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											tabIndex="190" runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" height="20px"
											Text="Save"></asp:button>&nbsp;
										<asp:button id="btnUndo_Write" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/cancel_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											tabIndex="195" runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" height="20px"
											Text="Undo"></asp:button>&nbsp;&nbsp;
										<asp:button id="btnDelete_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/clear_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											tabIndex="210" runat="server" Font-Bold="false" Width="100px" CssClass="FLATBUTTON" height="20px"
											Text="Delete"></asp:button></TD>
								</TR>
							</TABLE>
                        <INPUT id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
                               runat="server" DESIGNTIMEDRAGDROP="261"/>
						</asp:panel></td>
				</tr>
			</TABLE>
		<script>
			resizeForm(document.getElementById("scroller"));
		</script>
			</form>
</body>
</HTML>
