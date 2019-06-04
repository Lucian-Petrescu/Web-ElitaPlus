<%@ Control Language="vb" AutoEventWireup="false" Codebehind="CountryTaxUserControl.ascx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.CountryTaxUserControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table id="Table3" border="0">
	<tr>
		<td align="right" ><asp:label id="lblRequire" runat="server" Font-Bold="false" ForeColor="black" Visible="False">*</asp:label><asp:label id="Label2" runat="server" Font-Bold="false">DESCRIPTION</asp:label>:</td>
		<td>&nbsp;<asp:textbox id="txtTaxDescription" runat="server" SkinID="SmallTextBox" maxlength="30" Columns="30"></asp:textbox></td>
	</tr>
	<tr>
		<td align="right"><asp:label id="lblTaxComputeMeth" runat="server" Font-Bold="false">COMPUTE_METHOD</asp:label>:</td>
		<td>&nbsp;<asp:dropdownlist id="dlstTaxComputeMethod" runat="server" SkinID="SmallDropDown" AutoPostBack="True"></asp:dropdownlist></td>
	</tr>
	<tr>
		<td align="right"><asp:label id="lblTaxPercent" runat="server" Font-Bold="false">PERCENT_FLAG:</asp:label></td>
		<td>&nbsp;<asp:dropdownlist id="dlstTaxPercentFlag" runat="server" SkinID="SmallDropDown" AutoPostBack="True"></asp:dropdownlist></td>
	</tr>
	<tr>
		<td align="right"><asp:label id="lblPercent" runat="server" Font-Bold="false">PERCENT:</asp:label></td>
		<td>&nbsp;<asp:textbox id="txtTaxPercent" runat="server" SkinID="SmallTextBox" maxlength="18" Columns="18"></asp:textbox></td>
	</tr>
</table>
