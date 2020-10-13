<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BillingCycleForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.BillingCycleForm" EnableSessionState="True" Theme="Default"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" %>

<%@ Register TagPrefix="Elita" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl_New.ascx" %>
<%@ Register TagPrefix="mytab" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register Assembly="Microsoft.Web.UI.WebControls" Namespace="Microsoft.Web.UI.WebControls" TagPrefix="iewc" %>
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
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moBillingCycleCodeLabel" runat="server">Billing_Cycle_Code</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="moBillingCycleCodeText" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moStartDayLabel" runat="server">START_DAY</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="moStartDayText" AutoPostBack="true" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moEndDayLabel" runat="server">END_DAY</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="moEndDayText" AutoPostBack="true" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moBillingRunDateOffsetDaysLabel" runat="server">BILLING_RUN_DATE_OFFSET_DAYS</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="moBillingRunDateOffsetDaysText" AutoPostBack="true" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moNumberOfDigitsRoundoffLabel" runat="server">NUMBER_OF_DIGITS_ROUNDOFF</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="moNumberOfDigitsRoundoffDrop" runat="server" SkinID="MediumDropDown">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moDateOfPaymentOptionLabel" runat="server">DATE_OF_PAYMENT_OPTION</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="moDateOfPaymentOptionDrop" AutoPostBack="true" runat="server" SkinID="MediumDropDown">
                                    </asp:DropDownList>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moDateOfPaymentOffsetDaysLabel" runat="server">DATE_OF_PAYMENT_OFFSET_DAYS</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="moDateOfPaymentOffsetDaysText" AutoPostBack="true" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moNextRunDateLabel" runat="server">NEXT_RUN_DATE</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="moNextRunDateText" TabIndex="3" runat="server" SkinID="MediumTextBox" Width="150px" ReadOnly="True"></asp:TextBox>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moBillingCoolOffDaysLabel" runat="server">BILLING_COOL_OFF_DAYS</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="moBillingCoolOffDaysText" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="4" style="border: solid">
                                    <table border="0">
                                        <tr>
                                            <td align="right" nowrap="nowrap">
                                                <asp:Label ID="moPrePaidDateOfPaymentLabel" runat="server">PREPAID_DATE_OF_PAYMENT</asp:Label>
                                            </td>
                                            <td style="width: 23%;">
                                                <asp:TextBox ID="moPrePaidDateOfPaymentText" TabIndex="3" runat="server" SkinID="MediumTextBox" Width="150px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td align="right" nowrap="nowrap">&nbsp;
                                                <asp:Label ID="moPrePaidFromDateLabel" runat="server">PREPAID_FROM_DATE</asp:Label>
                                            </td>
                                            <td style="width: 23%;">
                                                <asp:TextBox ID="moPrePaidFromDateText" TabIndex="3" runat="server" SkinID="MediumTextBox" Width="150px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td align="right" nowrap="nowrap">&nbsp;
                                                <asp:Label ID="moPrePaidToDateLabel" runat="server" Font-Bold="false">PREPAID_TO_DATE</asp:Label>&nbsp;
                                            </td>
                                            <td style="width: 23%;">
                                                <asp:TextBox ID="moPrePaidToDateText" TabIndex="3" runat="server" SkinID="MediumTextBox" Width="150px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" nowrap="nowrap">
                                                <asp:Label ID="moPostPaidDateOfPaymentLabel" runat="server">POSTPAID_DATE_OF_PAYMENT</asp:Label>
                                            </td>
                                            <td style="width: 23%;">
                                                <asp:TextBox ID="moPostPaidDateOfPaymentText" TabIndex="3" runat="server" SkinID="MediumTextBox" Width="150px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td align="right" nowrap="nowrap">&nbsp;
                                                <asp:Label ID="moPostPaidFromDateLabel" runat="server">POSTPAID_FROM_DATE</asp:Label>
                                            </td>
                                            <td style="width: 23%;">
                                                <asp:TextBox ID="moPostPaidFromDateText" TabIndex="3" runat="server" SkinID="MediumTextBox" Width="150px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                            <td align="right" nowrap="nowrap">&nbsp;
                                                <asp:Label ID="moPostPaidToDateLabel" runat="server" Font-Bold="false">POSTPAID_TO_DATE</asp:Label>&nbsp;
                                            </td>
                                            <td style="width: 23%;">
                                                <asp:TextBox ID="moPostPaidToDateText" TabIndex="3" runat="server" SkinID="MediumTextBox" Width="150px" ReadOnly="True"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moIsNewBillingCycleLabel" runat="server" Visible="False"></asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:Label ID="moBillingCycleIdLabel" runat="server" Visible="False"></asp:Label>
                                    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
                                        runat="server" />
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>

    </div>

    <div class="btnZone">
        <asp:Button ID="btnApply_WRITE" runat="server" CausesValidation="False" Text="SAVE"
            SkinID="PrimaryRightButton"></asp:Button>
        <asp:Button ID="btnUndo_WRITE" runat="server" CausesValidation="False" Text="UNDO"
            SkinID="AlternateRightButton"></asp:Button>
        <asp:Button ID="btnBack" runat="server" CausesValidation="False" Text="BACK" SkinID="AlternateLeftButton"></asp:Button>
        <asp:Button ID="btnNew_WRITE" runat="server" CausesValidation="False" Text="New"
            SkinID="AlternateLeftButton"></asp:Button>
        <asp:Button ID="btnCopy_WRITE" runat="server" CausesValidation="False" Text="New_With_Copy"
            SkinID="AlternateRightButton"></asp:Button>
        <asp:Button ID="btnDelete_WRITE" runat="server" CausesValidation="False" Text="Delete"
            SkinID="CenterButton"></asp:Button>
    </div>
</asp:Content>
