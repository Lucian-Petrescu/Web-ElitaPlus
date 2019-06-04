<%@ Control Language="vb" AutoEventWireup="false" Codebehind="UserControlAddress.ascx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.UserControlAddress" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
  
<TR>
	<TD vAlign="bottom" noWrap align="left" colSpan="4" height="10">
		<HR style="WIDTH: 98%; HEIGHT: 1px" SIZE="1">
	</TD>
</TR>
<TR>
	<td width="1%" nowrap align="right"><asp:label id="moAddress1Label" runat="server">Address1</asp:label></td>
	<TD width="49%" align="left">&nbsp;
		<asp:textbox id="moAddress1Text" TABINDEX="2" runat="server" Width="90%"></asp:textbox></TD>
	<TD width="1%" align="right"><asp:label id="moAddress2Label" runat="server">Address2</asp:label></TD>
	<TD align="left" width="49%">&nbsp;
		<asp:textbox id="moAddress2Text" TABINDEX="3" runat="server" Width="90%"></asp:textbox></TD>
</TR>
<TR>
	<td width="1%" nowrap align="right"><asp:label id="moAddress3Label" runat="server">Address3</asp:label></td>
	<TD width="49%" align="left">&nbsp;
		<asp:textbox id="moAddress3Text" TABINDEX="4" runat="server" Width="90%"></asp:textbox></TD>
	<TD width="1%" align="right"></TD>
	<TD align="left" width="49%">&nbsp;
		</TD>
</TR>
<TR>
	<td width="1%" align="right"><asp:label id="moCityLabel" runat="server">City</asp:label></td>
	<TD width="49%" align="left">&nbsp;
		<asp:textbox id="moCityText" TABINDEX="5" runat="server" Width="90%"></asp:textbox></TD>
	<TD width="1%" align="right"><asp:label id="moCountryLabel" runat="server" Width="80px">Country</asp:label></TD>
	<TD align="left" width="49%">&nbsp;
		<asp:TextBox id="moCountryText" runat="server" ReadOnly="True" Width="90%" TabIndex="5"></asp:TextBox>
		<asp:dropdownlist id="moCountryDrop_WRITE" tabIndex="6" runat="server" Width="90%" AutoPostBack="True"></asp:dropdownlist>
	</TD>
</TR>
<TR>
	<td width="1%" align="right"><asp:label id="moPostalLabel" runat="server">Zip</asp:label></td>
	<TD width="49%" align="left">&nbsp;
		<asp:textbox id="moPostalText" TABINDEX="7" runat="server" Width="55%"></asp:textbox></TD>
	<TD width="1%" align="right"><asp:label id="moRegionLabel" runat="server" Width="120px">State_Province</asp:label></TD>
	<TD align="left" width="49%">&nbsp;
		<asp:textbox id="moRegionText" runat="server" Width="90%" ReadOnly="True" TabIndex="8"></asp:textbox>
		<asp:dropdownlist id="moRegionDrop_WRITE"  TABINDEX="8" runat="server" Width="90%"></asp:dropdownlist>			
	</TD>
</TR>
<TR>
	<TD vAlign="bottom" noWrap align="left" colSpan="4" height="10">
		<HR style="WIDTH: 98%; HEIGHT: 1px" SIZE="1">
	</TD>
</TR>
