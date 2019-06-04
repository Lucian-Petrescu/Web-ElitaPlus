<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ReplacementForm.aspx.vb"
    Theme="Default" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Claims.ReplacementForm"
    EnableSessionState="True" MasterPageFile="~/Navigation/masters/ElitaBase.Master" %>

<%@ Register TagPrefix="Elita" TagName="MessageController" Src="~/Common/MessageController.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlClaimInfo" Src="UserControlClaimInfo.ascx" %>

<asp:Content ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <Elita:UserControlClaimInfo ID="moClaimInfoController" runat="server" align="center"></Elita:UserControlClaimInfo>
</asp:Content>
<asp:Content ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse" runat="server" />

    <div class="dataContainer">
        <div class="stepformZone">
            <table width="100%" class="formGrid" border="0" cellspacing="0" cellpadding="0">
                <thead>
                    <tr>
                        <td nowrap="nowrap" align="right">&nbsp;
                        </td>
                        <td nowrap="nowrap">
                            <asp:Label ID="OldEquipmentInfoLabel" runat="server" Text="Old_Item" Font-Bold="true"></asp:Label>
                        </td>
                        <td align="right" nowrap="nowrap">&nbsp;
                        </td>
                        <td nowrap="nowrap">
                            <asp:Label ID="NewEquipmentInfoLabel" runat="server" Text="New_Item" Font-Bold="true"></asp:Label>
                        </td>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="OldManufacturerLabel" runat="server" Text="Manufacturer"></asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:TextBox ID="OldManufacturerTextBox" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td align="right" nowrap="nowrap">
                            <asp:Label ID="NewManufacturerLabel" runat="server" Text="Manufacturer"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="NewManufacturerDropDown" runat="server" SkinID="MediumDropDown">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="OldModelLabel" runat="server" Text="Model"></asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:TextBox ID="OldModelTextBox" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td align="right" nowrap="nowrap">
                            <asp:Label ID="NewModelLabel" runat="server" Text="Model"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="NewModelTextBox" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="OldRiskTypeLabel" runat="server" Text="Risk_Type"></asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:TextBox ID="OldRiskTypeTextBox" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td align="right" nowrap="nowrap">
                            <asp:Label ID="NewRiskTypeLabel" runat="server" Text="Risk_Type"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="NewRiskTypeTextBox" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="OldSerialNumberLabel" runat="server" Text="Serial_Number"></asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:TextBox ID="OldSerialNumberTextBox" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td align="right" nowrap="nowrap">
                            <asp:Label ID="NewSerialNumberLabel" runat="server" Text="Serial_Number"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="NewSerialNumberTextBox" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="OldImeiNumberLabel" runat="server" Text="IMEI_NUMBER"></asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:TextBox ID="OldImeiNumberTextBox" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td align="right" nowrap="nowrap">
                            <asp:Label ID="NewImeiNumberLabel" runat="server" Text="IMEI_NUMBER"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="NewImeiNumberTextBox" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap" align="right">&nbsp;
                        </td>
                        <td nowrap="nowrap">&nbsp;
                        </td>
                        <td align="right" nowrap="nowrap">
                            <asp:Label ID="DeviceTypeLabel" runat="server" Text="Device_Type"></asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="DeviceTypeDropDown" runat="server" SkinID="MediumDropDown">
                            </asp:DropDownList>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    <div class="dataContainer">
        <h2 id="headerDeviceInfo" runat="server" class="dataGridHeader">Comments</h2>
        <div class="stepformZone">
            <table width="100%" class="formGrid" border="0" cellspacing="0" cellpadding="0">
                <tbody>
                    <tr>
                        <td nowrap="nowrap">
                            <asp:TextBox ID="CommentsTextBox" runat="server" Width="87%" TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    <div class="btnZone">
        <asp:Button runat="server" ID="btnBack" Text="Back" SkinID="AlternateLeftButton" />
        <asp:Button runat="server" ID="btnSave_WRITE" Text="Save" SkinID="PrimaryRightButton" />
        <asp:Button runat="server" ID="BtnEdit_WRITE" Text="Edit" SkinID="PrimaryRightButton" />
        <asp:Button runat="server" ID="btnUndo_WRITE" Text="Undo" SkinID="AlternateRightButton" />
        <asp:Button runat="server" ID="btnDelete_WRITE" Text="Delete" SkinID="CenterButton" />
    </div>
</asp:Content>
