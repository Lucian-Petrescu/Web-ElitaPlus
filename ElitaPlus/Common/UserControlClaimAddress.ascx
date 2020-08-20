<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlClaimAddress.ascx.vb" 
Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.UserControlClaimAddress"
TargetSchema="http://schemas.microsoft.com/intellisense/ie5"%>
<tr>
    <td nowrap="nowrap" align="right">
        <asp:Label ID="moAddress1Label" runat="server">Address1</asp:Label>
    </td>
    <td nowrap="nowrap">
        <asp:TextBox ID="moAddress1Text" TabIndex="1" runat="server" SkinID="LargeTextBox" TextMode="MultiLine" Rows="2" Style="white-space: pre-wrap; width: 540px;" Columns="50"></asp:TextBox>
    </td>
    <td nowrap="nowrap" align="right">
        <asp:Label ID="moCountryLabel" runat="server">Country</asp:Label>
    </td>
    <td nowrap="nowrap">
        <asp:TextBox ID="moCountryText" runat="server" ReadOnly="True"  SkinID="SmallTextBox" Visible="False"></asp:TextBox>
        <asp:DropDownList ID="moCountryDrop_WRITE" TabIndex="4" runat="server" AutoPostBack="True">
        </asp:DropDownList>
    </td>
</tr>
<tr>
    <td nowrap="nowrap" align="right">
        <asp:Label ID="moAddress2Label" runat="server">Address2</asp:Label>
    </td>
    <td nowrap="nowrap">
        <asp:TextBox ID="moAddress2Text" TabIndex="2" runat="server" SkinID="LargeTextBox" TextMode="MultiLine" Rows="2" Style="white-space: pre-wrap; width: 540px;" Columns="50"></asp:TextBox>
    </td>
    <td nowrap="nowrap" align="right">
        <asp:Label ID="moRegionLabel" runat="server">State_Province</asp:Label>
    </td>
    <td nowrap="nowrap">
        <asp:TextBox ID="moRegionText" runat="server" ReadOnly="True"  SkinID="SmallTextBox" Visible="False"></asp:TextBox>
        <asp:DropDownList ID="moRegionDrop_WRITE" TabIndex="6" runat="server">
        </asp:DropDownList>        
    </td>
</tr>
<tr>
    <td nowrap="nowrap" align="right">
        <asp:Label ID="moAddress3Label" runat="server">Address3</asp:Label>
    </td>
    <td nowrap="nowrap">
        <asp:TextBox ID="moAddress3Text" TabIndex="3" runat="server" SkinID="LargeTextBox" TextMode="MultiLine" Rows="2" Style="white-space: pre-wrap; width: 540px;" Columns="50"></asp:TextBox>
    </td>
    <td nowrap="nowrap" align="right">
        <asp:Label ID="moCityLabel" runat="server">City</asp:Label>
    </td>
    <td nowrap="nowrap">
        <asp:TextBox ID="moCityText" TabIndex="8" runat="server" SkinID="SmallTextBox"></asp:TextBox>
    </td>
</tr>
<tr>
    <td nowrap="nowrap" align="right"></td>
    <td nowrap="nowrap"></td>
    <td nowrap="nowrap" align="right">
        <asp:Label ID="moPostalLabel" runat="server">Zip</asp:Label>
    </td>
    <td nowrap="nowrap">
        <asp:TextBox ID="moPostalText" TabIndex="9" runat="server" SkinID="SmallTextBox"></asp:TextBox>
    </td>

</tr>





