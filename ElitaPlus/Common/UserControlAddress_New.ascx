<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlAddress_New.ascx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.UserControlAddress_New" TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
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
        <asp:TextBox ID="moCountryText" runat="server" ReadOnly="True" TabIndex="4" SkinID="SmallTextBox"></asp:TextBox>
        <asp:DropDownList ID="moCountryDrop_WRITE" TabIndex="5" runat="server" AutoPostBack="True">
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
        <asp:TextBox ID="moRegionText" runat="server" ReadOnly="True" TabIndex="6" SkinID="SmallTextBox"></asp:TextBox>
        <asp:DropDownList ID="moRegionDrop_WRITE" TabIndex="7" runat="server">
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
        <asp:button id="btnValidate_Address" Visible="false" style="CURSOR:pointer; BACKGROUND-REPEAT: no-repeat"
									runat="server" Text="VALIDATE_ADDRESS" CssClass="FLATBUTTON" height="30px" CausesValidation="False"
									Width="160px" SkinID="PrimaryRightButton"></asp:button>
    </td>

</tr>
<tr id ="ValidatedAddress" runat="server" visible="false">
    <td colspan ="5">
        <div>
            <table style="width:100%">
                <tr>
                    <td nowrap="nowrap" align="left" colspan ="4">
                        <asp:Label ID="LabelPopupHeader" Font-Bold="true" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="LabelAddress1" runat="server">Address1</asp:Label>:
                    </td>
                    <td colspan ="2" align="left">
                        <asp:Label ID="LabelAddress1Text" runat="server" Style="width: 540px;"></asp:Label>
                    </td>
                    <td align="right">
                        <asp:Label ID="LabelCity" runat="server">City</asp:Label>:
                    </td>
                    <td align="left">
                        <asp:Label ID="LabelCityText" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="LabelAddress2" runat="server">Address2</asp:Label>:
                    </td>
                    <td colspan ="2" align="left">
                        <asp:Label ID="LabelAddress2Text" runat="server"  Style="width: 540px;"></asp:Label>
                    </td>
                    <td align="right">
                        <asp:Label ID="LabelState" runat="server">State</asp:Label>:
                    </td>
                    <td align="left">
                        <asp:Label ID="LabelStateText" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="LabelAddress3" runat="server">Address3</asp:Label>:
                    </td>
                    <td colspan ="2" align="left">
                        <asp:Label ID="LabelAddress3Text" runat="server"  Style="width: 540px;"></asp:Label>
                    </td>
                    <td align="right">
                        <asp:Label ID="LabelZip" runat="server">Zip</asp:Label>:
                    </td>
                    <td align="left">
                        <asp:Label ID="LabelZipText" runat="server"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="Label1Country" runat="server">Country</asp:Label>:
                    </td>
                    <td align="left">
                        <asp:Label ID="Label1CountryText" runat="server"></asp:Label>
                    </td>
                </tr>
                <td colspan="4">
                    <div id="validateAddressButton" runat="server">
                        <asp:Button ID="btnAccept" CausesValidation="false" CssClass="FLATBUTTON" style="CURSOR:pointer;" runat="server" Text="ACCEPT_ADDRESS"></asp:Button>&nbsp;
                        <asp:Button ID="btnDecline" CausesValidation="false" CssClass="FLATBUTTON" style="CURSOR:pointer;" runat="server" Text="DECLINE_ADDRESS"></asp:Button>&nbsp;&nbsp;
                        <br />
                    </div>
                </td>
            </table>
        </div>
    </td>
</tr>