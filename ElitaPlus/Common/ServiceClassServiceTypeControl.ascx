<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ServiceClassServiceTypeControl.ascx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Common.ServiceClassServiceTypeControl"
    TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<div>
    <table border="0" cellpadding="0" cellspacing="0" style="position: relative; height: 10px; margin-bottom:0PX; padding-bottom:0px;">
        <tr>
            <td nowrap="nowrap" align="left" style="width: 20px;">
                <asp:Label ID="ServiceClassLabel" runat="server">SERVICE_CLASS</asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:DropDownList ID="cboServiceClass" runat="server" SkinID="SmallDropDown">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td nowrap="nowrap" align="left" colspan="2">
                <asp:Label ID="ServiceTypeLabel" runat="server">SERVICE_TYPE</asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left" colspan="2">
                <asp:DropDownList ID="cboServiceType" TabIndex="1" runat="server" SkinID="smallDropDown">
                </asp:DropDownList>
            </td>
        </tr>
    </table>
</div>
