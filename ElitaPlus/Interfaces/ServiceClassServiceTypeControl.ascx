﻿<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ServiceClassServiceTypeControl.ascx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Interfaces.ServiceClassServiceTypeControl"
    TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<link href="../App_Themes/Default/Default.css" rel="stylesheet" />
<div>
    <tr>
        <td nowrap="nowrap" align="right">
            <asp:Label ID="ServiceClassLabel" runat="server">SERVICE_CLASS</asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="cboServiceClass" runat="server" CssClass="small">
            </asp:DropDownList>
            <asp:TextBox ID="moTextBoxServiceClass" runat="server" Enabled="False" ReadOnly="True"
                         CssClass="small"></asp:TextBox>
        </td>
        <td nowrap="nowrap" align="right">
            <asp:Label ID="ServiceTypeLabel" runat="server">SERVICE_TYPE</asp:Label>
        </td>
        <td>
            <asp:DropDownList ID="cboServiceType" TabIndex="1" runat="server" CssClass="small">
            </asp:DropDownList>
            <asp:TextBox ID="moTextBoxServiceType" runat="server" Enabled="False" ReadOnly="True"
                         CssClass="small"></asp:TextBox>
        </td>
    </tr>
</div>
