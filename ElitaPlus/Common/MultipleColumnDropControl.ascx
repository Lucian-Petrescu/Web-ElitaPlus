<%@ Control Language="vb" AutoEventWireup="false" Codebehind="MultipleColumnDropControl.ascx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Common.MultipleColumnDropControl"
    TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<table id="Table1" cellspacing="0" cellpadding="0" border="0" style="width: 0.01%;
    height: 44px" align="center">
    <tr>
        <td nowrap align="right" style="height: 14px">
            <asp:Label ID="lb_DropDown" runat="server" Width="1%"></asp:Label>&nbsp;&nbsp;</td>
        <td style="width: 86px; height: 14px" align="left">
            <asp:Label ID="lblCode" runat="server" Width="83px">By_Code</asp:Label></td>
        <td align="left" style="width: 10%; height: 14px">
            &nbsp;<asp:DropDownList ID="moMultipleColumnDrop" Width="128px" runat="server" AutoPostBack="true"
                Height="1000px">
            </asp:DropDownList></td>
    </tr>
    <tr>
        <td style="width: 105px">
        </td>
        <td style="width: 86px" align="left">
            <asp:Label ID="lblDescription" runat="server" Width="83px" Height="8px">By_Description</asp:Label></td>
        <td align="left" style="width: 10%">
            &nbsp;<asp:DropDownList ID="moMultipleColumnDropDesc" Width="328px" runat="server"
                AutoPostBack="true" Height="1000px"  SkinID="SmallDropDown">
            </asp:DropDownList></td>
    </tr>
</table>
