<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ProductConversionExtendedForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.ProductConversionExtendedForm" EnableSessionState="True" Theme="Default"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" %>

<%@ Register TagPrefix="Elita" TagName="MultipleColumnDDLabelControl"  Src="../Common/MultipleColumnDDLabelControl_new.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto">
        <Scripts>
            <asp:ScriptReference Path="~/Navigation/scripts/ComunaSuggest.js" />
        </Scripts>
    </asp:ScriptManager>

    <div class="dataContainer">
        <div class="stepformZone">
            <table class="formGrid" border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px;"
                width="100%">
                <tr>
                    <td runat="server" colspan="2">
                        <table>
                            <tbody>
                                <Elita:MultipleColumnDDLabelControl runat="server" ID="DealerDropControl" />
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="formGrid">
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblProdCode" runat="server">ASSURANT_PRODUCT_CODE</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="moProductCode" runat="server" SkinID="MediumDropDown"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lbldealerProdCode" runat="server">DEALER_PRODUCT_CODE</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="txtdealerProdCode" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id ="trmfgdurationrow" runat ="server">
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblCertduration" runat="server">Certificate_Duration</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="txtCertDuration" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id ="trmfgwarrrow" runat ="server">
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblMfgwarranty" runat="server">Manufacturer_Warranty</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="txtMfgwarranty" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id ="trAmountRow" runat ="server">
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblGrossAmt" runat="server">Gross_Amt</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="txtGrossAmt" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                            </tr>
                             <tr id ="trManufacturerRow" runat ="server">
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblManufacturer" runat="server">Manufacturer</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="moManufacturer" runat="server" SkinID="MediumDropDown"></asp:DropDownList>
                                </td>
                            </tr>
                             <tr id ="trModelRow" runat ="server">
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblModel" runat="server">Model</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="txtModel" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr id ="trSalesPriceRow" runat ="server">
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblSalesPrice" runat="server">Sales_Price</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="txtSalesPrice" runat="server" SkinID="MediumTextBox"></asp:TextBox>                                    
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

        </div>
        <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse" runat="server" />
        <input id="HiddenDeletePromptResponse" type="hidden" runat="server" />

        <div class="btnZone">
            <asp:Button ID="btnApply_WRITE" runat="server" CausesValidation="False" Text="SAVE" SkinID="PrimaryRightButton"></asp:Button>
            <asp:Button ID="btnUndo_WRITE" runat="server" CausesValidation="False" Text="UNDO" SkinID="AlternateRightButton"></asp:Button>
            <asp:Button ID="btnBack" runat="server" CausesValidation="False" Text="BACK" SkinID="AlternateLeftButton"></asp:Button>
            <asp:Button ID="btnNew_WRITE" runat="server" CausesValidation="False" Text="New"
                SkinID="AlternateLeftButton"></asp:Button>
            <asp:Button ID="btnCopy_WRITE" runat="server" CausesValidation="False" Text="New_With_Copy"
                SkinID="AlternateRightButton"></asp:Button>
            <asp:Button ID="btnDelete_WRITE" runat="server" CausesValidation="False" Text="Delete"
                SkinID="CenterButton"></asp:Button>           
        </div>
    </div>

</asp:Content>
