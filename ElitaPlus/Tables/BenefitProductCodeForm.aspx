<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BenefitProductCodeForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.BenefitProductCodeForm"
    EnableSessionState="True"
    Theme="Default"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" %>

<%@ Register TagPrefix="Elita" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl_new.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlAvailableSelected" Src="../Common/UserControlAvailableSelected_new.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlAttrtibutes" Src="~/Common/UserControlAttrtibutes.ascx" %>
<%@ Register TagPrefix="mytab" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>

<%@ Register TagPrefix="ElitaUC" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register Assembly="Microsoft.Web.UI.WebControls" Namespace="Microsoft.Web.UI.WebControls" TagPrefix="iewc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.6.1.min.js"> </script>
    <script type="text/javascript">
        $("[src*=\sort_indicator_des]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "..\\App_Themes\\Default\\Images\\sort_indicator_asc.png");
        });
        $("[src*=sort_indicator_asc]").live("click", function () {
            $(this).attr("src", "..\\App_Themes\\Default\\Images\\sort_indicator_des.png");
            $(this).closest("tr").next().remove();
        });
    </script>
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
            <table class="formGrid" border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px;" width="100%">
                <tr>
                    <td id="Td1" runat="server" colspan="2">
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
                            <tr id="TRPrdCode" runat="server">
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moBenefitProductCodeLabel" runat="server">Benefit_Product_Code</asp:Label>
                                    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse" runat="server" />
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="moBenefitProductCodeText" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moDescriptionLabel" runat="server">Description</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="moDescriptionText" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moVendorLabel" runat="server">Benefit_Product_Vendor</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="moVendorDrop" runat="server" SkinID="MediumDropDown" />
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moCurrencyLabel" runat="server">Currency</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="moCurrencyDrop" runat="server" SkinID="MediumDropDown" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moTaxTypeXcdLabel" runat="server">Tax_Type</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="moTaxTypeXcdDrop" runat="server" SkinID="MediumDropDown" />
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moUnitOfMeasureLabel" runat="server">Unit_Of_Measure</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="moUnitOfMeasureXcdDrop" runat="server" SkinID="MediumDropDown" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moNetPriceLabel" runat="server">Net_Price</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="moNetPriceText" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moDurationInMonthLabel" runat="server">Duration_In_Month</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="moDurationInMonthText" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moBillablePartNumberLabel" runat="server">Billable_Part_Number</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="moBillablePartNumberText" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moDaysToExpireAfterEndDateLabel" runat="server">Days_To_Expire_After_End_Date_Label</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="moDaysToExpireAfterEndDateText" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moEffectiveDateLabel" runat="server">Effective_Date</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="moEffectiveDateText" runat="server" SkinID="MediumTextBox" />
                                    <asp:ImageButton ID="moEffectiveDateImageButton" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png" />
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moExpirationDateLabel" runat="server">Expiration_Date</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="moExpirationDateText" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    <asp:ImageButton ID="moExpirationDateImageButton" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <div class="btnZone">
        <asp:Button ID="btnApply_WRITE" runat="server" CausesValidation="False" Text="SAVE" SkinID="PrimaryRightButton" />
        <asp:Button ID="btnUndo_WRITE" runat="server" CausesValidation="False" Text="UNDO" SkinID="AlternateRightButton" />
        <asp:Button ID="btnBack" runat="server" CausesValidation="False" Text="BACK" SkinID="AlternateLeftButton" />
        <asp:Button ID="btnNew_WRITE" runat="server" CausesValidation="False" Text="New" SkinID="AlternateLeftButton" />
        <asp:Button ID="btnCopy_WRITE" runat="server" CausesValidation="False" Text="New_With_Copy" SkinID="AlternateLeftButton" />
        <asp:Button ID="btnDelete_WRITE" runat="server" CausesValidation="False" Text="Delete" SkinID="CenterButton" />
    </div>
</asp:Content>
