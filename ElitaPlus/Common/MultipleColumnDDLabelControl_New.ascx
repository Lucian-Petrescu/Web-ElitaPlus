<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="MultipleColumnDDLabelControl_New.ascx.vb" ClassName="MultipleColumnDDLabelControl"  
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDDLabelControl_New"
    TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<link href="../App_Themes/Default/Default.css" rel="stylesheet" />
<tr>
    <td nowrap="nowrap" align="right">
        &nbsp;
    </td>
    <td nowrap="nowrap">
        <asp:Label ID="lblCode" runat="server">Code</asp:Label>
    </td>
    <td nowrap="nowrap" align="left">
        <asp:Label ID="lblDescription" runat="server">Description</asp:Label>
    </td>
</tr>
<tr>
    <td nowrap="nowrap" align="right">
        <asp:Label ID="lb_DropDown" runat="server"></asp:Label>
    </td>
    <td nowrap="nowrap">
        <asp:DropDownList ID="moMultipleColumnDrop" runat="server"  CssClass="small" ></asp:DropDownList>
        <asp:TextBox ID="moMultipleColumnTextBoxCode" runat="server" Enabled="False" ReadOnly="True" Visible="False"  CssClass="small" ></asp:TextBox>
    </td>
    <td nowrap="nowrap" align="left">
        <asp:DropDownList ID="moMultipleColumnDropDesc" TabIndex="1" runat="server" CssClass="medium">
        </asp:DropDownList>
        <asp:TextBox ID="moMultipleColumnTextBoxDesc" runat="server" Enabled="False" ReadOnly="True" Visible="False"  CssClass="small"></asp:TextBox>
    </td>
</tr>
