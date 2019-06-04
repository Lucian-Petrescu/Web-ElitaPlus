<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ErrorForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ErrorForm" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Error Form</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="STYLESHEET">
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>
	</HEAD>
	<body style="BACKGROUND-REPEAT: no-repeat" bgColor="#d3d5e2" background="../navigation/images/error_splash.jpg" leftMargin="0" topMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:TextBox id="txtError" CssClass="FLATTEXTBOX" style="Z-INDEX: 102; LEFT: 156px; POSITION: absolute; TOP: 278px" runat="server" Width="302px" ReadOnly="True"  Rows="15" TextMode="MultiLine"></asp:TextBox>
		</form>
	</body>
</HTML>
