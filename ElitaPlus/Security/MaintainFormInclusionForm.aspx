<%@ Page Language="vb" AutoEventWireup="false" Codebehind="MaintainFormInclusionForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Security.MaintainFormInclusionForm" %>
<%@ Register TagPrefix="uc1" TagName="TreeController" Src="../Common/TreeController.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>MaintainFormInclusionForm</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="STYLESHEET">
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>
	</HEAD>
	<body leftMargin="0" topMargin="0" onload="changeScrollbarColor();" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<input id="scrollCount" type="hidden" name="scrollCount" runat="server"/> 
			<input id="scrollPos" type="hidden" name="scrollPos" runat="server"/>
			<!--Start Header-->
			<table style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				cellspacing="0" cellpadding="0" width="98%" bgcolor="#d5d6e4" border="0">
				<tr>
					<TD vAlign="top">
						<table width="100%" border="0">
							<tr>
								<TD height="20"><asp:label id="moTitleLabel1" runat="server" CssClass="TITLELABEL">ADMIN</asp:label>:
									<asp:label id="moTitleLabel2" runat="server" CssClass="TITLELABELTEXT">MAINTAIN FORM INCLUSIONS</asp:label></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<table id="tblOuter2" style="border: 1px solid black; MARGIN: 5px; height:93%;"
				cellspacing="0" cellpadding="0" width="98%" bgColor="#d5d6e4"
				border="0" frame="void">
				<tr>
					<td style="HEIGHT: 3px">&nbsp;</td>
				</tr>
				<tr>
					<td valign="top">
						<table id="moOutTable" style="border: 1px solid #999999; height:98%;"
							cellspacing="0" cellpadding="4" width="98%" align="center"
							bgcolor="#fef9ea" border="0">
							<!--<TABLE id="moOutTable2" cellSpacing="1" cellPadding="1" width="95%" border="1">-->
							<tr>
								<td valign="top" colspan="2"><font color="#12135b"><i><b>*</b> Click on +/- to expand or 
											collapse nodes and toggle form access and edit rights for each user role.</i></font></td>
							</tr>
							<tr>
								<td colspan="2">
									<hr>
								</td>
							</tr>
							<tr>
								<td valign="top" width="35%" height="98%">
								<td>
								    <asp:Panel ID="panelTree" ScrollBars="Auto" runat="server">
								        <asp:TreeView ID="tvFormList" runat="server" ShowExpandCollapse="true" ShowLines="true" NodeStyle-HorizontalPadding="5" NodeStyle-VerticalPadding="0"></asp:TreeView>
								    </asp:Panel>
								</td>
							</tr>
							<tr>
								<td colspan="2">
									<hr>
								</td>
							</tr>
							<tr>
								<TD align="left" colSpan="2">&nbsp;<asp:button id="BtnSave_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/save_trans_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
										runat="server" ToolTip="The Checked Roles will be in the DataBase form inclusion table" Text="Save" Width="90px" CssClass="FLATBUTTON"
										height="18px"></asp:button>&nbsp;
									<asp:button id="BtnReset" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/reset_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
										runat="server" ToolTip="Load The Tabs, Forms and Roles from the DataBase" Text="Reset" Width="90px"
										CssClass="FLATBUTTON" height="18px"></asp:button></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
		</form>
		<script type="text/javascript">
		    SetScrollAreaCtrTreeV("panelTree", "100%", "100%");		    
		</script>
	</body>
</HTML>
