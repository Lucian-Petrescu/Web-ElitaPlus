<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UserControlPoliceReport" Src="../Common/UserControlPoliceReport.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PoliceReportForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.PoliceReportForm"%>
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
								<TD height="20"><asp:label id="LabelTables" runat="server" CssClass="TITLELABEL">CLAIMS</asp:label>:
									<asp:label id="Label1" runat="server"   CssClass="TITLELABELTEXT">Police_Report</asp:label></TD>
								<TD align="right" height="20"><STRONG>*</STRONG>
									<asp:label id="moIndicatesLabel" runat="server" Font-Bold="false" EnableViewState="true">INDICATES_REQUIRED_FIELDS</asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="tblOuter2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				height="93%" cellSpacing="0" cellPadding="0" rules="none" width="98%" bgColor="#d5d6e4"
				border="4"> <!--d5d6e4-->
				<tr>
					<td height="5"></td>
				</tr>
				<tr>
					<td vAlign="top" align="center"><asp:panel id="WorkingPanel" runat="server">
							<TABLE id="tblMain1" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid"
								height="98%" cellSpacing="0" cellPadding="6" width="98%" align="center" bgColor="#fef9ea"
								border="0">
								<TR>
									<TD vAlign="top" align="center" height="1">
										<uc1:ErrorController id="ErrorCtrl" runat="server"></uc1:ErrorController></TD>
								</TR>
								<TR>
									<TD vAlign="top">
										<asp:Panel id="EditPanel_WRITE" runat="server">
											<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
												<TR>
													<TD noWrap align="left" colSpan="3"></TD>
												</TR>
												<TR>
													<TD noWrap align="left" colSpan="3"></TD>
												</TR>
												<TR>
													<TD noWrap align="left" colSpan="3"></TD>
												</TR>
												<TR>
													<TD noWrap align="left" colSpan="3"></TD>
												</TR>
												<TR>
													<TD width="4%"></TD>
													<TD noWrap align="right" width="13%">
														<asp:label id="LabelClaimNumber" runat="server" Font-Bold="false">CLAIM_NUMBER</asp:label>:</TD>
													<TD align="left" width="33%">&nbsp;<asp:textbox id="TextboxClaimNumber" tabIndex="1" runat="server" CssClass="FLATTEXTBOX" Width="150px"></asp:textbox></TD>
													<TD noWrap align="right" width="23%">
														<asp:label id="LabelCertNumber" runat="server" Font-Bold="false">Certificate</asp:label>:</TD>
													<TD align="left" width="27%">&nbsp;<asp:textbox id="TextboxCertNumber" tabIndex="1" runat="server" CssClass="FLATTEXTBOX" Width="150px"></asp:textbox></TD>
												</TR>
												<TR>
													<TD width="4%"></TD>
													<TD noWrap align="right" width="13%">
														<asp:label id="LabelDealer" runat="server" Font-Bold="false">Dealer</asp:label>:</TD>
													<TD align="left">&nbsp;<asp:textbox id="TextboxDealer" tabIndex="1" runat="server" CssClass="FLATTEXTBOX" Width="150px"></asp:textbox></TD>
												</tr>
												<TR>
													<TD colSpan=5>&nbsp;
														<HR style="WIDTH: 100%; HEIGHT: 1px" SIZE="1">&nbsp;
													</TD>
												</TR>
												<TR>
													<TD noWrap align="left" colSpan="3"></TD>
												</TR>
												<TR>
													<TD noWrap align="left" colSpan="3"></TD>
												</TR>
												<uc1:UserControlPoliceReport id="mcUserControlPoliceReport" runat="server"></uc1:UserControlPoliceReport><%--<TR>
													<TD style="WIDTH: 35%" vAlign=middle align=center ></TD>
												</TR>
												<TR>
													<TD style="WIDTH: 100%" vAlign=middle align=left >
														<uc1:UserControlPoliceReport id=mcUserControlPoliceReport runat="server"></uc1:UserControlPoliceReport></TD>
												</TR>
												<TR>
													<TD style="WIDTH: 35%" vAlign=middle align=left ></TD>
												</TR>--%></TABLE>
										</asp:Panel></TD>
								</TR>
								<TR>
									<TD>
										<HR style="WIDTH: 100%; HEIGHT: 1px" SIZE="1">
									</TD>
								</TR>
								<TR>
									<TD vAlign="top" noWrap align="left" height="20">&nbsp;
										<asp:button id="btnBack" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/back_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server" Font-Bold="false" height="20px" Text="Back" CssClass="FLATBUTTON" Width="100px"></asp:button>&nbsp;
										<asp:button id="btnEdit_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											tabIndex="5" runat="server" Font-Bold="false" height="20px" Text="Edit" CssClass="FLATBUTTON"
											Width="100px"></asp:button>&nbsp;
										<asp:button id="btnSave_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/save_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server" Font-Bold="false" height="20px" Text="Save" CssClass="FLATBUTTON" Width="100px"></asp:button>&nbsp;
										<asp:button id="btnUndo_Write" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/cancel_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server" Font-Bold="false" height="20px" Text="Undo" CssClass="FLATBUTTON" Width="100px"></asp:button>&nbsp;
									</TD>
								</TR>
								<TR>
									<TD height="5"></TD>
								</TR>
							</TABLE>
                        <INPUT id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
                               runat="server" DESIGNTIMEDRAGDROP="261"/>
						</asp:panel></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
