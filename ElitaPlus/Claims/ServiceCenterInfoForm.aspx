<%@ Register TagPrefix="uc1" TagName="UserControlServiceCenterInfo" Src="../Certificates/UserControlServiceCenterInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ServiceCenterInfoForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ServiceCenterInfoForm"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>TemplateForm</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="STYLESHEET">
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>
	</HEAD>
	<body leftMargin="0" topMargin="0" onload="changeScrollbarColor();" MS_POSITIONING="GridLayout"
		border="0">
		<form id="Form1" method="post" runat="server">
			<!--Start Header-->
			
			<TABLE style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; WIDTH: 98%; BORDER-BOTTOM: black 1px solid; HEIGHT: 20px"
				cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE width="100%" border="0">
							<TR>
								<TD height="20">
									<asp:label id="LabelTables" runat="server"  CssClass="TITLELABEL">CLAIMS</asp:label>:
									<asp:label id="Label1" runat="server"  CssClass="TITLELABELTEXT">Service_Center</asp:label>
								</TD>
								<TD align="right" height="20"><STRONG>*</STRONG>
									<asp:label id="moIndicatesLabel" runat="server" Font-Bold="false" EnableViewState="true">INDICATES_REQUIRED_FIELDS</asp:label>
								</TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="tblOuter2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				cellSpacing="0" cellPadding="0" rules="none" width="98%" height="93%" bgColor="#d5d6e4"
				border="0">  <!--d5d6e4-->
				<tr>
					<td height="5"></td>
				</tr>
				<tr>
					<td vAlign="top" align="center"><asp:panel id="WorkingPanel" runat="server">
							<TABLE id="tblMain1" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid"
								height="98%" cellSpacing="0" cellPadding="6" width="98%" align="center"
								bgColor="#fef9ea" border="0">
								
								<TR>
										<TD vAlign="top" align="center" height="1">
											<uc1:ErrorController id="ErrorCtrl" runat="server"></uc1:ErrorController>
										</TD>
								</TR>
								
								<TR>
									<TD vAlign="top">
											<asp:Panel id="EditPanel_WRITE" runat="server">
												<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0"
													<TR>
														<TD style="WIDTH: 35%;" vAlign="middle" align="center" colSpan="4"></TD>
													</TR>
													<TR>
														<TD style="WIDTH: 35%;" vAlign="middle" align="center" colSpan="4">
															<uc1:UserControlServiceCenterInfo id="UserControlServiceCenterInfo" runat="server"></uc1:UserControlServiceCenterInfo></TD>
													</TR>
													<TR>
														<TD style="WIDTH: 35%;" vAlign="middle" align="center" colSpan="4"></TD>
													</TR>
												</TABLE>
											</asp:Panel>
									</TD>
								</TR>
								<TR>
									<TD>
										<HR style="WIDTH: 100%; HEIGHT: 1px" SIZE="1">
									</TD>
								</TR>
								<TR>
									<TD vAlign="top" noWrap align="left" height="20">&nbsp;
										<asp:button id="btnBack" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/back_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											tabIndex="185" runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" Text="Back"
											height="20px"></asp:button>&nbsp;
									</TD>
								</TR>
								<TR>
									<TD height="5"></TD>
								</TR>
							</TABLE>
							<INPUT id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
								runat="server" DESIGNTIMEDRAGDROP="261">
						</asp:panel></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
