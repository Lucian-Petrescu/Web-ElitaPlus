<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlContactInfo.ascx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.UserControlContactInfo" %>
<%@ Register TagPrefix="uc1" TagName="UserControlAddress" Src="UserControlAddress.ascx" %>

<style type="text/css">
    .style1
    {
        height: 31px;
    }
</style>

<table border="0"> 
<tr>
    <td valign="bottom" nowrap align="left" colspan="4" height="10">
        <hr style="width: 10%; height: 1px" size="1">
    </td>
</tr>
<tr>
    <td width="10%" align="right"  style="white-space:nowrap;" >
        <asp:Label ID="Label1" runat="server" Width="80px">Address_Type</asp:Label>
    </td>
    <td align="left" width="45%">
        &nbsp;
        <asp:TextBox ID="moAddressTypeText" runat="server" ReadOnly="True" Width="30%" 
            TabIndex="5"></asp:TextBox>
        <asp:DropDownList ID="moAddressTypeDrop_WRITE" TabIndex="6" runat="server" Width="30%">
        </asp:DropDownList>
    </td>
     
    <td width="10%" align="right"  style="white-space:nowrap;" >
        &nbsp;</td>
    <td align="left" width="45%">
        &nbsp;
        </td>
</tr>
<tr>
    <td width="10%" align="right"  style="white-space:nowrap;" >
        <asp:Label ID="moSalutationLabel" runat="server">SALUTATION</asp:Label>
    </td>
    <td width="45%" align="left" class="style1">
        &nbsp;
        <asp:TextBox ID="moSalutationText" TabIndex="1" runat="server" CssClass="FLATTEXTBOX_TAB"
            Width="20%"></asp:TextBox>
        <asp:DropDownList ID="cboSalutationId" TabIndex="1" runat="server" Width="30%">
        </asp:DropDownList>
    </td>
    <td width="10%" align="right"  style="white-space:nowrap;" >
        <asp:Label ID="moHomePhoneLabel" runat="server">HOME_PHONE</asp:Label>
    </td>
    <td align="left" width="45%" class="style1">
        &nbsp;
        <asp:TextBox ID="moHomePhoneText" TabIndex="1" runat="server" CssClass="FLATTEXTBOX_TAB"></asp:TextBox>
    </td>
</tr>
<tr>
    <td width="10%" align="right"  style="white-space:nowrap;" >
        <asp:Label ID="moContactNameLabel" runat="server">NAME</asp:Label>
    </td>
    <td width="45%" align="left">
        &nbsp;
        <asp:TextBox ID="moContactNameText" TabIndex="1" runat="server" CssClass="FLATTEXTBOX_TAB"
            Width="90%"></asp:TextBox>
    </td>
    <td width="10%" align="right"  style="white-space:nowrap;" >
        <asp:Label ID="moWorkPhoneLabel"  runat="server">WORK_PHONE</asp:Label>
    </td>
    <td align="left" width="45%">
        &nbsp;
        <asp:TextBox ID="moWorkPhoneText" TabIndex="1" runat="server" CssClass="FLATTEXTBOX_TAB"></asp:TextBox>
    </td>
</tr>
<tr>
    <td width="10%" align="right"  style="white-space:nowrap;" >
        <asp:Label ID="moEmailAddressLabel" runat="server">EMAIL</asp:Label>
    </td>
    <td align="left" width="45%">
        &nbsp;
        <asp:TextBox ID="moEmailAddressText" TabIndex="1" runat="server" CssClass="FLATTEXTBOX_TAB"
            Width="90%"></asp:TextBox>
    </td>
    <td width="10%" align="right" style="white-space:nowrap;">
        <asp:Label ID="moCellPhoneLabel"  runat="server">CELL_PHONE</asp:Label>
    </td>
    <td align="left" width="45%">
        &nbsp;
        <asp:TextBox ID="moCellPhoneText" runat="server" TabIndex="1"  CssClass="FLATTEXTBOX_TAB"></asp:TextBox>
    </td>
</tr>
<tr>
    <td colspan="4">
        <uc1:UserControlAddress ID="moAddressController" runat="server"></uc1:UserControlAddress>
    </td>
</tr>
</table>