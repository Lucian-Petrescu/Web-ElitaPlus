<%@ Page Language="vb" AutoEventWireup="false" Codebehind="vscWSTestForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.vscWSTestForm"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>vscWSTestForm</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:Button id="GetVscButton" style="Z-INDEX: 101; LEFT: 440px; POSITION: absolute; TOP: 192px"
				runat="server" Text="GetVsc"></asp:Button>
			<asp:TextBox id="userTextBox" style="Z-INDEX: 102; LEFT: 424px; POSITION: absolute; TOP: 48px"
				runat="server"></asp:TextBox>
			<asp:Label id="userLabel" style="Z-INDEX: 103; LEFT: 288px; POSITION: absolute; TOP: 48px"
				runat="server">networkId</asp:Label>
			<asp:Label id="passwordLabel" style="Z-INDEX: 104; LEFT: 296px; POSITION: absolute; TOP: 80px"
				runat="server">password</asp:Label>
			<asp:TextBox id="passwordTextBox" style="Z-INDEX: 105; LEFT: 424px; POSITION: absolute; TOP: 88px"
				runat="server" TextMode="Password"></asp:TextBox>
			<asp:Button id="LoginButton" style="Z-INDEX: 106; LEFT: 448px; POSITION: absolute; TOP: 128px"
				runat="server" Text="Login"></asp:Button>
			<asp:Label id="LabelError" style="Z-INDEX: 107; LEFT: 280px; POSITION: absolute; TOP: 280px"
				runat="server" Width="408px" ForeColor="Red"   ></asp:Label>
			<asp:Button id="HelloBtn" style="Z-INDEX: 108; LEFT: 744px; POSITION: absolute; TOP: 120px"
				runat="server" Text="Hello"></asp:Button>
			<asp:Button id="LoginBodyButton" style="Z-INDEX: 109; LEFT: 528px; POSITION: absolute; TOP: 128px"
				runat="server" Text="Login Body"></asp:Button>
		</form>
	</body>
</HTML>
