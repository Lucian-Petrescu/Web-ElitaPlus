<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SoftQuestionForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.SoftQuestionForm" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Soft Questions</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="STYLESHEET">
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>
	</HEAD>
	<body oncontextmenu="return false" style="WIDTH: 100%; BACKGROUND-REPEAT: repeat; HEIGHT: 100%"
		bottomMargin="0" leftMargin="0" background="../Common/images/back_spacer.jpg" topMargin="0"
		scroll="no" onload="changeScrollbarColor();" rightMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:panel id="panelForm" runat="server" Width="53.43%">
				<TABLE style="border: 1px groove;WIDTH: 100%; HEIGHT: 100%" cellSpacing="2" cellPadding="2" bgColor="#f4f3f8"
					border="0">
					<TR>
						<TD style="WIDTH: 301px;" vAlign="top">
							<asp:panel id="ListPanel" runat="server" Height="100%" Width="100%">
								<TABLE id="ListTabel" cellSpacing="2" cellPadding="2" border="0">
									<TR>
										<TD style="HEIGHT: 1px" vAlign="bottom">
											<asp:label id="Label2" runat="server" Width="152px">Soft Questions Group</asp:label></TD>
										<TD style="HEIGHT: 1px" vAlign="bottom">
											<asp:label id="Label1" runat="server">Soft Questions Description</asp:label></TD>
									</TR>
									<TR>
										<TD style="WIDTH: 94px; HEIGHT: 1px" vAlign="top" align="center">
											<asp:DropDownList id="cboSoftQuestionGroup" runat="server" Width="224px"></asp:DropDownList></TD>
										<TD style="HEIGHT: 1px" vAlign="top" align="right">
											<asp:textbox id="txtSoftQuestion" runat="server" Height="68px" Width="368px"></asp:textbox>
											<asp:RequiredFieldValidator id="RequiredFieldValidator1" runat="server" ErrorMessage="SoftQuestion Required"
												ControlToValidate="txtSoftQuestion"></asp:RequiredFieldValidator></TD>
									</TR>
									<TR>
										<TD vAlign="bottom" noWrap align="left" colSpan="2" height="25">
											<asp:button id="btnSave_WRITE" style="BACKGROUND-POSITION: left center; BACKGROUND-IMAGE: url(../Navigation/images/icons/save_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
												runat="server" Height="21" Font-Bold="false" ForeColor="#000000" CssClass="FLATBUTTON" Text="&nbsp;&nbsp;Save"
												ToolTip="Save changes to database" width="100"></asp:button>&nbsp; <INPUT class="FLATBUTTON" id="btnUndo_Write" title="Cancel" style="BACKGROUND-POSITION: left center; BACKGROUND-IMAGE: url(../Navigation/images/icons/cancel_icon.gif); WIDTH: 100px; CURSOR: hand; COLOR: #000000; BACKGROUND-REPEAT: no-repeat; HEIGHT: 21px"
												onclick="parent.CloseSoftQuestion();" type="button" value=" Cancel" Height="21" width="100">
										</TD>
									</TR>
								</TABLE>
							</asp:panel></TD>
					</TR>
				</TABLE>
			</asp:panel>
		</form>
	</body>
</HTML>
