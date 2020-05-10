<%@ Control Language="vb" AutoEventWireup="false" Codebehind="RegionTaxUserControl.ascx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.RegionTaxUserControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<link href="../App_Themes/Default/Default.css" rel="stylesheet" />
<table id="Table3" border="0">
	<tr>
		<td noWrap align="right">
		    <asp:label id="Label2" runat="server">DESCRIPTION</asp:label>:</td>
		<td><asp:textbox id="txtTaxDescription"  runat="server" CssClass="small" Columns="30" maxlength="30"
				Enabled="False"></asp:textbox></td>
	</tr>
	<tr>
		<td noWrap align="right">
		    <asp:label id="lblPercent" runat="server">PERCENT:</asp:label></td>
		<td><asp:textbox id="txtTaxPercent"  runat="server" CssClass="small" Columns="18" maxlength="18"
				Enabled="False"></asp:textbox></td>
	</tr>
	<tr>
		<td noWrap align="right" colspan="1" rowSpan="1">
		    <asp:label id="lblNontaxable" runat="server">NON_TAXABLE</asp:label>:</td>
		<td><asp:textbox id="txtNonTaxable"  runat="server" CssClass="small"  Columns="18" maxlength="18"
				Enabled="False"></asp:textbox></td>
	</tr>
	<tr>
		<td noWrap align="right" colspan="1" rowSpan="1">
		    <asp:label id="lblMinTax" runat="server">MINIMUM_TAX</asp:label>:</td>
		<td><asp:textbox id="txtMinimumTax"  runat="server" CssClass="small" Columns="18" maxlength="18"
				Enabled="False"></asp:textbox></td>
	</tr>
	<tr>
		<td noWrap align="right" colSpan="1" rowspan="1" >
			<asp:label id="Label4" runat="server">GL_ACCOUNT_NUMBER:</asp:label></td>
		<td>
			<asp:TextBox id="txtGLAccount" runat="server" CssClass="small" Enabled="False"></asp:TextBox></td>
	</tr>
</table>
