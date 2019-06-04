<%@ Control Language="vb" AutoEventWireup="false" Codebehind="MultipleColumnDDLabelControl.ascx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<TABLE id="TableMultipleDD" style="WIDTH: 80%" cellSpacing="0" cellPadding="0" border="0">
	<TR>
		<TD style="width:196px;">&nbsp;</TD>
		<TD align="left">&nbsp;<asp:label id="lblCode" runat="server">Code</asp:label></TD>
		<TD align="left">&nbsp;<asp:label id="lblDescription" runat="server">Description</asp:label></TD>
	</TR>
	<TR>
		<TD style="vertical-align:middle;"  noWrap align="center" width="1%"><asp:label id="lb_DropDown" runat="server"></asp:label></TD>
		<TD  style="vertical-align:bottom;" align="left">&nbsp;<asp:dropdownlist 
                id="moMultipleColumnDrop" Width="100px" runat="server" ></asp:dropdownlist><asp:textbox id="moMultipleColumnTextBoxCode" Width="100px" runat="server" Enabled="False" ReadOnly="True"
				Visible="False" cssclass="FLATTEXTBOX"></asp:textbox></TD>
		<TD  style="vertical-align:bottom;"  align="left">&nbsp;<asp:dropdownlist id="moMultipleColumnDropDesc" tabIndex="1" Width="300px" runat="server" ></asp:dropdownlist>
            <asp:textbox id="moMultipleColumnTextBoxDesc" Width="300px" runat="server" 
                Enabled="False" ReadOnly="True"
				Visible="False" cssclass="FLATTEXTBOX"></asp:textbox></TD>
	</TR>
</TABLE>
 