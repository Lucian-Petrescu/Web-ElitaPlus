<%@ Control Language="vb" AutoEventWireup="false" Codebehind="InterfaceProgressControl.ascx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Interfaces.InterfaceProgressControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<SCRIPT language="JavaScript" src="../Navigation/Scripts/InterfaceScripts.js"></SCRIPT>
<asp:button id="btnViewHidden" style="display:none" runat="server" Width="0" Height="0"></asp:button>
<asp:Button id="btnErrorHidden" style="display:none" runat="server" Height="0"
	Width="0" Text=""></asp:Button>
<input id="moInterfaceStatus" type="hidden" name="moInterfaceStatus" runat="server"/>
<input id="moInterfaceErrorMsg" type="hidden" name="moInterfaceErrorMsg" runat="server"/>
