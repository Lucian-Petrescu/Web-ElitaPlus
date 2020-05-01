<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ChangeCompany.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ChangeCompany" %>
<%@ Register TagPrefix="uc1" TagName="UserControlAvailableSelected" Src="../Common/UserControlAvailableSelected.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ChangeCompany</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="STYLESHEET">
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>
		</HEAD>
	<body leftMargin="0" topMargin="0" onload="changeScrollbarColor();" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
            <INPUT id="txtNextPageID" type="hidden" name="Hidden1" runat="server"/> 
			<!--Start Header-->
			<TABLE style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE width="100%" border="0">
							<TR>
								<TD height="20">
									<asp:label id="LabelTables" runat="server"  CssClass="TITLELABEL">Admin</asp:label>:
									<asp:label id="Label7" runat="server" CssClass="TITLELABELTEXT">CHANGE_COMPANY</asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="tblOuter2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				height="93%" cellSpacing="0" cellPadding="0" rules="none" width="98%" bgColor="#d5d6e4"
				border="0" frame="void">
				<tr>
					<td>&nbsp;
						<uc1:ErrorController id="moErrorController" runat="server"></uc1:ErrorController></td>
				</tr>
				<tr>
					<td vAlign="top" align="center">
						<TABLE id="moOutTable" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid"
							height="100" cellSpacing="0" cellPadding="4" rules="cols" width="510" align="center"
							bgColor="#fef9ea" border="0">
							<tr>
								<td align="right" style="WIDTH: 19px; HEIGHT: 20px"></td>
								<td align="left" style="HEIGHT: 20px"></td>
							</tr>
							<TR>
								<TD align="center" colspan="2">
									<uc1:MultipleColumnDDLabelControl id="moGroupCompanyMultipleDrop" runat="server"></uc1:MultipleColumnDDLabelControl></TD>
							</TR>
							<TR>
								<TD align="center" colspan="2">
									<uc1:UserControlAvailableSelected id="UserControlAvailableSelectedCompanies" runat="server"></uc1:UserControlAvailableSelected></TD>
							</TR>
							<tr>
								<td align="center" colSpan="2"><asp:button id="btnSave_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/save_trans_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
										runat="server" Width="100px" Text="Save" ToolTip="Save modifications" CssClass="FLATBUTTON" height="18px"></asp:button>&nbsp;&nbsp;</td>
							</tr>
						</TABLE>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
