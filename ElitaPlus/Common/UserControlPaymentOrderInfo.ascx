<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlPaymentOrderInfo.ascx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.UserControlPaymentOrderInfo"  %>
    
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>


 <tr id="bankInfo_hr1">
            <td align="right">
                <asp:Label ID="lblBankName" runat="server">BANK_NAME:</asp:Label>
            </td>
            <td align="left">
                <asp:TextBox ID="txtBankName" runat="server" SkinID="MediumTextBox" MaxLength="50"></asp:TextBox>
                <asp:DropDownList ID="moBankName" runat="server" SkinID="MediumDropDown" AutoPostBack="true">
                </asp:DropDownList>
            </td>
             <td align="right">
                <asp:Label ID="labelBankID" runat="server">BANK_ID:</asp:Label>
            </td>
            <td align="left">                
                <asp:TextBox ID="textboxBankID" runat="server" SkinID="MediumTextBox" MaxLength="10"></asp:TextBox>
            </td>
</tr>     
              
 <tr id="bankInfo_hr2">
    <td align="right"  class="<%= HiddenClassName %>">
        <asp:Label ID="lblNameonAccount" runat="server" Visible="false">NAME_ON_ACCOUNT:</asp:Label>
    </td>
    <td align="left">
        <asp:TextBox ID="txtNameAccount" runat="server" Visible="false" MaxLength="50" SkinID="MediumTextBox" />
    </td>
    <td align="right">
        <asp:Label ID="lblCountryOfBank" Visible="false" runat="server">COUNTRY_OF_BANK:</asp:Label>
    </td>
    <td align="left">
        <asp:DropDownList ID="moCountryDrop_WRITE" runat="server" Visible="false" AutoPostBack="True" SkinID="MediumDropDown">
        </asp:DropDownList>
    </td>
</tr>
 <tr id="bankInfo_hr10">   
    <td colspan="4">
    <asp:HiddenField  id = "HiddenClassName"  runat="server"/>
    </td>
</tr>
