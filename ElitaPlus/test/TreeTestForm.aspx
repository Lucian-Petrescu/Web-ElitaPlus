<%@ Register TagPrefix="uc1" TagName="TreeController" Src="../Common/TreeController.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="TreeTestForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.TreeTestForm"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>TreeTestForm</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<uc1:TreeController id="moTree" runat="server"></uc1:TreeController>
			<asp:Button id="BtnAdd" style="Z-INDEX: 101; LEFT: 86px; POSITION: absolute; TOP: 233px" runat="server"
				Text="Add"></asp:Button>
			<asp:Button id="BtnEdit" style="Z-INDEX: 102; LEFT: 205px; POSITION: absolute; TOP: 234px" runat="server"
				Text="Edit"></asp:Button>
			<asp:Button id="BtnDelete" style="Z-INDEX: 103; LEFT: 330px; POSITION: absolute; TOP: 234px"
				runat="server" Text="Delete"></asp:Button>
		</form>
	</body>
</HTML>
