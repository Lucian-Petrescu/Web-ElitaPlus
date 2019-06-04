<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlContactInfo_New.ascx.vb" EnableTheming="true"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.UserControlContactInfo_New" %>
<%@ Register TagPrefix="uc1" TagName="UserControlAddress" Src="UserControlAddress_New.ascx" %>

<tr>
    <td nowrap="nowrap" align="right">
        <asp:Label ID="Label1" runat="server">Address_Type:</asp:Label>
    </td>
    <td nowrap="nowrap">
        <asp:TextBox ID="moAddressTypeText" runat="server" ReadOnly="True" TabIndex="5" SkinID="MediumTextBox"></asp:TextBox>
        <asp:DropDownList ID="moAddressTypeDrop_WRITE" TabIndex="6" runat="server" SkinID="MediumDropDown">
        </asp:DropDownList>
    </td>
</tr>
<tr>
    <td nowrap="nowrap" align="right">
        <asp:Label ID="moSalutationLabel" runat="server">SALUTATION</asp:Label>
    </td>
    <td nowrap="nowrap">
        <asp:TextBox ID="moSalutationText" TabIndex="1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
        <asp:DropDownList ID="cboSalutationId" TabIndex="1" runat="server" SkinID="MediumDropDown">
        </asp:DropDownList>
    </td>
    <td nowrap="nowrap" align="right">
        <asp:Label ID="moHomePhoneLabel" runat="server">HOME_PHONE</asp:Label>
    </td>
    <td nowrap="nowrap">
        <asp:TextBox ID="moHomePhoneText" TabIndex="1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
    </td>
</tr>
<tr>
    <td nowrap="nowrap" align="right">
        <asp:Label ID="moContactNameLabel" runat="server">NAME</asp:Label>
    </td>
    <td nowrap="nowrap">
        <asp:TextBox ID="moContactNameText" TabIndex="1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
    </td>
    <td nowrap="nowrap" align="right">
        <asp:Label ID="moWorkPhoneLabel" runat="server">WORK_PHONE</asp:Label>
    </td>
    <td nowrap="nowrap">
        <asp:TextBox ID="moWorkPhoneText" TabIndex="1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
    </td>
</tr>
<tr>
    <td nowrap="nowrap" align="right">
        <asp:Label ID="moEmailAddressLabel" runat="server">EMAIL</asp:Label>
    </td>
    <td nowrap="nowrap">
        <asp:TextBox ID="moEmailAddressText" TabIndex="1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
    </td>
    <td nowrap="nowrap" align="right">
        <asp:Label ID="moCellPhoneLabel" runat="server">CELL_PHONE</asp:Label>
    </td>
    <td nowrap="nowrap">
        <asp:TextBox ID="moCellPhoneText" runat="server" TabIndex="1" SkinID="MediumTextBox"></asp:TextBox>
    </td>
</tr>
<tr>
    <td nowrap="nowrap" align="right">
        <asp:Label ID="moCompanyLabel" runat="server">COMPANY</asp:Label>
    </td>
    <td nowrap="nowrap">
        <asp:TextBox ID="moCompanyText" TabIndex="1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
    </td>
    <td nowrap="nowrap" align="right">
        <asp:Label ID="moJobTitleLabel" runat="server">JOB_TITLE</asp:Label>
    </td>
    <td nowrap="nowrap">
        <asp:TextBox ID="moJobTitleText" runat="server" TabIndex="1" SkinID="MediumTextBox"></asp:TextBox>
    </td>
</tr>
<uc1:UserControlAddress ID="moAddressController" runat="server" />