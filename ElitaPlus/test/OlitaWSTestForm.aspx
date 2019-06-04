<%@ Page Language="vb" AutoEventWireup="false" Codebehind="OlitaWSTestForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.OlitaWSTestForm" validateRequest="false" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>OlitaWSTestForm</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:label id="lblDealer" style="Z-INDEX: 100; LEFT: 16px; POSITION: absolute; TOP: 8px" runat="server"
				>Dealer</asp:label>
			<asp:button id="btnGetQuote" style="Z-INDEX: 112; LEFT: 888px; POSITION: absolute; TOP: 8px"
				tabIndex="4" runat="server" Width="96px" Text="Get Quote"></asp:button><asp:textbox id="xmlUpdateOut" style="Z-INDEX: 111; LEFT: 16px; POSITION: absolute; TOP: 304px"
				tabIndex="20" runat="server" Width="1168px" TextMode="MultiLine" Height="208px"></asp:textbox><asp:button id="btnUpdate" style="Z-INDEX: 110; LEFT: 16px; POSITION: absolute; TOP: 272px"
				tabIndex="5" runat="server" Text="Update"></asp:button><asp:textbox id="txtCountryCode" style="Z-INDEX: 109; LEFT: 584px; POSITION: absolute; TOP: 8px"
				tabIndex="3" runat="server" Width="120px"></asp:textbox><asp:label id="Label1" style="Z-INDEX: 108; LEFT: 488px; POSITION: absolute; TOP: 8px" runat="server"></asp:label>
				
				tabIndex="4" runat="server" Text="Get Regions" Width="96px"></asp:button><asp:button id="Button1" style="Z-INDEX: 104; LEFT: 384px; POSITION: absolute; TOP: 8px" tabIndex="2"
				runat="server" Text="Get Cert"></asp:button><asp:textbox id="txtCertNumber" style="Z-INDEX: 103; LEFT: 288px; POSITION: absolute; TOP: 8px"
				tabIndex="1" runat="server" Width="88px"></asp:textbox><asp:textbox id="txtDealer" style="Z-INDEX: 102; LEFT: 72px; POSITION: absolute; TOP: 8px" runat="server"
				Width="120px"></asp:textbox><asp:label id="Label2" style="Z-INDEX: 101; LEFT: 200px; POSITION: absolute; TOP: 8px" runat="server"
				>Cert Number</asp:label>
			<asp:TextBox id="xmlDataOut" style="Z-INDEX: 105; LEFT: 16px; POSITION: absolute; TOP: 40px"
				runat="server" Width="1168px" Height="208px" TextMode="MultiLine" tabIndex="15"></asp:TextBox></form>
	</body>
</HTML>
