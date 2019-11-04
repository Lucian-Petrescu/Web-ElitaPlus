<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ProductCodeForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.ProductCodeForm" EnableSessionState="True" Theme="Default"
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
                    <td id="TRPlanCode" runat="server" colspan="2">
                        <table>
                            <tbody>
                                <Elita:MultipleColumnDDLabelControl runat="server" ID="ProductDropControl" />
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>
                        <table class="formGrid">
                            <tr id="TRPrdCode" runat="server">
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moProductCodeLabel" runat="server">Product_Code</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="moProductCodeText" runat="server" SkinID="MediumTextBox"></asp:TextBox>
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
                                    <asp:Label ID="moRiskGroupLabel" runat="server">Risk_Group</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="moRiskGroupDrop" runat="server" SkinID="MediumDropDown">
                                    </asp:DropDownList>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="LabelWaitingPeriod" runat="server">WAITING_PERIOD_IN_DAYS</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="moWaitingPeriod" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moMethodOfRepairLabel" runat="server">Method_of_Repair</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="moMethodOfRepairDrop" AutoPostBack="true" runat="server" SkinID="MediumDropDown">
                                    </asp:DropDownList>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="LabelDaysToCancel" runat="server">DAYS_TO_CANCEL</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="moDaysToCancel" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <asp:Panel ID="pnlMethdOfRepair_Replacement" runat="server" Visible="false">
                                <tr>
                                    <td align="right" nowrap="nowrap">
                                        <asp:Label ID="moNumOfReplacementsLabel" runat="server">NUMBER_OF_CLAIMS</asp:Label>
                                    </td>
                                    <td align="left" nowrap="nowrap">
                                        <asp:TextBox ID="moNumOfReplacementsText" runat="server" type="number" SkinID="MediumTextBox"
                                            MaxLength="4"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td align="right" nowrap="nowrap">
                                        <asp:Label ID="moNumOfRepairClaimsLabel" runat="server">NUMBER_OF_REPAIR_CLAIMS</asp:Label>
                                    </td>
                                    <td align="left" nowrap="nowrap">
                                        <asp:TextBox ID="moNumOfRepairClaimsText" runat="server" type="number" SkinID="MediumTextBox"
                                            MaxLength="4"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" nowrap="nowrap">
                                        <asp:Label ID="moNumOfReplClaimsLabel" runat="server">NUMBER_OF_REPLACEMENT_CLAIMS</asp:Label>
                                    </td>
                                    <td align="left" nowrap="nowrap">
                                        <asp:TextBox ID="moNumOfReplClaimsText" runat="server" type="number" SkinID="MediumTextBox"
                                            MaxLength="4"></asp:TextBox>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moTypeOfEquipmentLabel" runat="server">Type_Of_Equipment</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="moTypeOfEquipmentDrop" runat="server" SkinID="MediumDropDown">
                                    </asp:DropDownList>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="LabelApplyWaitingPeriod" runat="server">ALWAYS_APPLY_WAITING_PERIOD</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="cboIgnoreWaitingPeriodWhen_WSD_Equal_PSD" TabIndex="146" runat="server" Width="179px" SkinID="SmallDropDown">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moPriceMatrixLabel" runat="server">Coverage_Pricing</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="moPriceMatrixDrop" runat="server" SkinID="MediumDropDown">
                                    </asp:DropDownList>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moProdLiabilityLimitBasedOnLabel" runat="server">PROD_CLAIMLIABILITY_LIMIT_BASED_ON</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="moProdLiabilityLimitBasedOnDrop" AutoPostBack="true" runat="server" SkinID="MediumDropDown">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moUserDescriptionLabel" runat="server">USE_DEPRECIATION</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="moUseDepreciationDrop" runat="server" SkinID="MediumDropDown">
                                    </asp:DropDownList>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moProdLimitApplicableToXCDLabel" runat="server">PROD_LIMIT_APPLICABLE_TO_XCD</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="moProdLimitApplicableToXCDDrop" AutoPostBack="true" runat="server" SkinID="MediumDropDown">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moBundledItemLabel" runat="server">BUNDLED_ITEM</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="moBundledItemId" runat="server" SkinID="MediumDropDown">
                                    </asp:DropDownList>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moProdLiabilityLimitPolicyLabel" runat="server">PROD_CLAIMLIABILITY_LIMIT_POLICY</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="moProdLiabilityLimitPolicyDrop" AutoPostBack="true" runat="server" SkinID="MediumDropDown">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moPercentOfRetailLabel" runat="server">Percent_Of_Retail</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="moPercentOfRetailText" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moProdLiabilityLimitLabel" runat="server" Font-Bold="false">PROD_LIABILITY_LIMIT</asp:Label>&nbsp;
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="moProdLiabilityLimitText" TabIndex="12" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td></td>
                                <td></td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moProdLiabilityLimitPercentLabel" runat="server" Font-Bold="false">PROD_LIABILITY_LIMIT_PERCENT</asp:Label>&nbsp;
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="moProdLiabilityLimitPercentText" TabIndex="12" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <asp:Panel ID="pnlInstallment" runat="server" Visible="false">
                                <tr>
                                    <td align="right" nowrap="nowrap">
                                        <asp:Label ID="moBillingFrequencyLabel" runat="server">BILLING_FREQUENCY</asp:Label>
                                    </td>
                                    <td align="left" nowrap="nowrap">
                                        <asp:DropDownList ID="moBillingFrequencyId" runat="server" SkinID="MediumDropDown">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" nowrap="nowrap">
                                        <asp:Label ID="moInstallmentsLabel" runat="server">NUMBER_OF_INSTALLMENTS</asp:Label>
                                    </td>
                                    <td align="left" nowrap="nowrap">
                                        <asp:TextBox ID="moInstallmentText" runat="server" type="number" SkinID="MediumTextBox"
                                            MaxLength="2"></asp:TextBox>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <asp:Panel ID="pnlMethodOfRepair_Repair" runat="server">
                                <tr>
                                    <td align="right" nowrap="nowrap">
                                        <asp:Label ID="moMethodOfRepairByPriceLabel" runat="server">Method_Of_Repair_By_Price</asp:Label>
                                    </td>
                                    <td align="left" nowrap="nowrap">
                                        <asp:DropDownList ID="moMethodOfRepairByPriceDrop" AutoPostBack="false" runat="server"
                                            SkinID="MediumDropDown">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moSplitWarrantyLabel" runat="server">Split_Warranty</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="moSplitWarrantyDrop" runat="server" SkinID="MediumDropDown">
                                    </asp:DropDownList>
                                </td>
                                <td align="right" nowrap="nowrap" runat="server" id="lblAutoApprovePSP_TD" visible="false">
                                    <asp:Label ID="lblAutoApprovePSP" runat="server">AUTO_APPROVE_PSP</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap" runat="server" id="txtAutoApprovePSP_TD" visible="false">
                                    <asp:TextBox ID="txtAutoApprovePSP" MaxLength="5" runat="server" Width="55px"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moCommentsLabel" runat="server">Comments</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="moCommentsText" runat="server" TextMode="MultiLine" Rows="6" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moReInsuredLabel" runat="server">ReInsured</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="moReInsuredDrop" AutoPostBack="true" runat="server" SkinID="MediumDropDown"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moSpecialServiceLabel" runat="server">Special_Service</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="moSpecialServiceText" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moInstallmentRepricableLabel" runat="server">INSTALLMENT_REPRICABLE</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="moInstallmentRepricableDrop" AutoPostBack="true" runat="server" SkinID="MediumDropDown"></asp:DropDownList>
                                </td>
                            </tr>
                            <asp:Panel ID="pnlUPGFields" runat="server">
                                <tr>
                                    <td align="right" nowrap="nowrap">
                                        <asp:Label ID="moUpgradeProgramLabel" runat="server">Upgrade_Program</asp:Label>
                                    </td>
                                    <td align="left" nowrap="nowrap">
                                        <asp:DropDownList ID="moUpgradeProgramDrop" AutoPostBack="true" runat="server" SkinID="MediumDropDown"></asp:DropDownList>
                                    </td>
                                    <td align="right" nowrap="nowrap">
                                        <asp:Label ID="moUPGFinanceBalCompMethLabel" runat="server">UPG_Finance_Bal_Comp_Meth</asp:Label>
                                    </td>
                                    <td align="left" nowrap="nowrap">
                                        <asp:DropDownList ID="moUPGFinanceBalCompMethDrop" runat="server" SkinID="MediumDropDown"></asp:DropDownList>
                                    </td>
                                </tr>
                            </asp:Panel>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moupgFinanceInfoRequireLabel" runat="server">UPG_FINANCE_INFO_REQUIRE</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="moupgFinanceInfoRequireDrop" runat="server" SkinID="MediumDropDown">
                                    </asp:DropDownList>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moUpgFeeLabel" runat="server">Upgrade_Fee</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="moUpgFeeText" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moUpgTermUOMLabel" runat="server">Upgrade_Term_Unit_Of_Measure</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="moUpgTermUOMDrop" AutoPostBack="true" runat="server" SkinID="MediumDropDown">
                                    </asp:DropDownList>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moUpgradeTermLabel" runat="server">Upgrade_Term</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="moUpgradeTermText" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="moUpgradeTermFromLabel" runat="server">UPGRADE_TERM_FROM</asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="moUpgradeTermFROMText" runat="server" SkinID="SmallTextBox" />
                                </td>
                                <td align="right">
                                    <asp:Label ID="moUpgradeTermToLabel" runat="server">UPGRADE_TERM_TO</asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="moUpgradeTermToText" runat="server" SkinID="SmallTextBox" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moIsNewProductCodeLabel" runat="server" Visible="False"></asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:Label ID="moProductCodeIdLabel" runat="server" Visible="False"></asp:Label>
                                    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
                                        runat="server" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="moBillingCriteriaLabel" runat="server">BILLING_CRITERIA</asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="moBillingCriteriaDrop" AutoPostBack="false" runat="server" SkinID="MediumDropDown"></asp:DropDownList>
                                </td>
                                <td align="right">
                                    <asp:Label ID="moCancellationDependencyLabel" runat="server">CANCELLATION_DEPENDENCY</asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="moCancellationDependencyDrop" AutoPostBack="false" runat="server" SkinID="MediumDropDown"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="moPostPrePaidLabel" runat="server">POST_PRE_PAID</asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="moPostPrePaidDrop" AutoPostBack="false" runat="server" SkinID="MediumDropDown"></asp:DropDownList>
                                </td>
                                <td align="right">
                                    <asp:Label ID="moCnlLumpsumBillingLabel" runat="server">CANCELLATION_LUMPSUM_BILLING</asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="moCnlLumpsumBillingDrop" AutoPostBack="false" runat="server" SkinID="MediumDropDown"></asp:DropDownList>
                                </td>
                            </tr>

                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblProductEquipmentValidation" runat="server">PRODUCT_EQUIPMENT_VALIDATION</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="cboProductEquipmentValidation" runat="server" SkinID="MediumDropDown"></asp:DropDownList>
                                </td>

                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblAllowRegisteredItems" runat="server">ALLOW_REGISTERED_ITEMS</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="cboAllowRegisteredItems" runat="server" SkinID="MediumDropDown" AutoPostBack="true"></asp:DropDownList>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblListForDeviceGroup" runat="server">LIST_FOR_DEVICE_GROUP</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="cboListForDeviceGroup" runat="server" SkinID="MediumDropDown" AutoPostBack="true"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moInitialQuestionSetLabel" runat="server">INITIAL_QUESTION_SET</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="cboInitialQuestionSet" runat="server" SkinID="MediumDropDown"></asp:DropDownList>
                                </td>
                                <td align="right" nowrap="nowrap"></td>
                                <td align="left" nowrap="nowrap"></td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="moMaxAgeOfRegisteredItemLabel" runat="server">MAX_AGE_OF_REGISTERED_ITEM</asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="moMaxAgeOfRegisteredItemText" runat="server" SkinID="SmallTextBox" />
                                </td>
                                <td align="right">
                                    <asp:Label ID="moMaxRegistrationsAllowedLabel" runat="server">MAX_REGISTERED_ALLOWED</asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="moMaxRegistrationsAllowedText" runat="server" SkinID="SmallTextBox" />
                                </td>
                            </tr>
                            <tr runat="server" id="trUpdateReplaceRegItemsId">
                                <td align="right" nowrap="nowrap">
                                    <asp:Label runat="server" ID="lblUpdateReplaceRegItemsId">UPDATE_REPLACE_REG_ITEMS_ID</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="cboUpdateReplaceRegItemsId" runat="server" SkinID="MediumDropDown"></asp:DropDownList>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label runat="server" ID="lblRegisteredItemsLimit">REGISTERED_ITEMS_LIMIT</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="txtRegisteredItemsLimit" runat="server" SkinID="SmallTextBox" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="moClaimLimitPerRegisteredItemlabel" runat="server">CLAIM_LIMIT_PER_REG_ITEM</asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="moClaimLimitPerRegisteredItemText" runat="server" SkinID="SmallTextBox" />
                                </td>
                                <td align="right">
                                    <asp:Label ID="moCancellationWithinTermLabel" runat="server">CANCELLATION_WITHIN_TERM</asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="cboCancellationWithinTerm" runat="server" SkinID="MediumDropDown"></asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="moExpNotificationDaysLabel" runat="server">EXPIRATION_NOTIFICATION_DAYS</asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="moExpNotificationDaysText" runat="server" SkinID="SmallTextBox" />
                                </td>
                                <td align="right">
                                    <asp:Label ID="labelFulfillmentReimThresholdValue" runat="server">FULFILLMENT_REIM_THRESHOLD_VALUE</asp:Label>
                                </td>
                                <td align="left">
                                    <asp:TextBox ID="TextBoxFulfillmentReimThresholdValue" runat="server" SkinID="SmallTextBox" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="LabelDepSchCashReimbursement" runat="server">DEP_SCH_CASH_REIMBURSEMENT</asp:Label>:
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="ddlDepSchCashReimbursement" runat="server" SkinID="MediumDropDown"></asp:DropDownList>
                                </td>
                                <td align="right"></td>
                                <td align="left"></td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="moBenefitsEligibleFlagLabel" runat="server">BENEFITS_ELIGIBLE</asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="moBenefitsEligibleFlagDropDownList" runat="server" SkinID="MediumDropDown" AutoPostBack="true" OnSelectedIndexChanged="moBenefitsEligibleFlagDropDownList_SelectedIndexChanged" />
                                </td>
                                <td align="right">
                                    <asp:Label ID="moBenefitsEligibleActionLabel" runat="server">BENEFITS_ELIGIBLE_ACTION</asp:Label>
                                </td>
                                <td align="left">
                                    <asp:DropDownList ID="moBenefitsEligibleActionDropDownList" runat="server" SkinID="MediumDropDown" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblClaimProfile" runat="server">CLAIM_PROFILE</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="ddlClaimProfile" runat="server" SkinID="MediumDropDown"></asp:DropDownList>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblCalcCovgEndDateBasedOn" runat="server">CALC_COVG_END_DATE_BASED_ON</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="ddlCalcCovgEndDateBasedOn" runat="server" SkinID="MediumDropDown"></asp:DropDownList>
                                </td>
                            </tr>
                             <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moPerIncidentLiabilityLimitCapLabel" runat="server" Font-Bold="false">PER_INCIDENT_LIABILITY_LIMIT_CAP</asp:Label>&nbsp;
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="moPerIncidentLiabilityLimitCapText" TabIndex="12" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                </td>
                                 <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblPriceMatrixUsesWpXcd" runat="server" Font-Bold="false">PRICE_MATRIX_USES_WP_XCD</asp:Label>&nbsp;
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="cboPriceMatrixUsesWpXcd" runat="server" SkinID="MediumDropDown"></asp:DropDownList>
                                </td>
                            </tr>
                             <tr>
                                 <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblExpectedPremiumIsWpXcd" runat="server" Font-Bold="false">EXPECTED_PREMIUM_IS_WP_XCD</asp:Label>&nbsp;
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:DropDownList ID="cboExpectedPremiumIsWpXcd" runat="server" SkinID="MediumDropDown"></asp:DropDownList>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>

    <div class="dataContainer">
        <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0" />
        <asp:HiddenField ID="hdnDisabledTab" runat="server" />
        <%--This anchor is used to scroll to the Tab content--%>
        <a ID="topTabs" runat="server" />

        <div id="tabs" class="style-tabs">
            <ul>
                <li><a href="#tab_RegionDetail">
                    <asp:Label ID="Label4" runat="server" CssClass="tabHeaderText">Regions</asp:Label></a></li>
                <li><a href="#tab_ExtProdCode_Policy_WRITE">
                    <asp:Label ID="Label6" runat="server" CssClass="tabHeaderText">Policy</asp:Label></a></li>
                <li><a href="#tab_Accounting_Codes">
                    <asp:Label ID="Label8" runat="server" CssClass="tabHeaderText">Accounting Codes</asp:Label></a></li>
                <li><a href="#tab_moAttributes_WRITE">
                    <asp:Label ID="Label1" runat="server" CssClass="tabHeaderText">ATTRIBUTES</asp:Label></a></li>
                <li><a href="#tab_ExtendedAttributes">
                    <asp:Label ID="Label2" runat="server" CssClass="tabHeaderText">EXTENDED ATTRIBUTES</asp:Label></a></li>
                <li><a href="#tab_moProductEquipment">
                    <asp:Label ID="Label3" runat="server" CssClass="tabHeaderText">PRODUCT_EQUIPMENT</asp:Label></a></li>
                <li><a href="#tab_moProductRewards">
                    <asp:Label ID="Label5" runat="server" CssClass="tabHeaderText">PRODUCT_REWARDS</asp:Label></a></li>
                <li><a href="#tab_DeviceTypeDetails">
                    <asp:Label ID="Label7" runat="server" CssClass="tabHeaderText">DEVICE_TYPES</asp:Label></a></li>
                <li><a href="#tab_moProductBenefits">
                    <asp:Label ID="Label9" runat="server" CssClass="tabHeaderText">EQUIPMENT_BENEFITS</asp:Label></a></li>
            </ul>

            <div id="tab_RegionDetail">
                <div class="Page" runat="server" id="moRegionDetail1" style="height: 100%; overflow: auto">
                    <Elita:UserControlAvailableSelected tabindex="12" ID="UserControlAvailableSelectedRegions"
                        runat="server"></Elita:UserControlAvailableSelected>
                </div>
            </div>
            <div id="tab_ExtProdCode_Policy_WRITE">
                <div class="Page" runat="server" id="moExtProdCode_Policy_WRITE1" style="height: 100%; overflow: auto">
                    <asp:GridView ID="moProductPolicyDatagrid" runat="server" Width="100%" OnRowCreated="RowCreated"
                        OnRowCommand="RowCommand" AllowPaging="True" AllowSorting="False" CellPadding="1"
                        AutoGenerateColumns="False" SkinID="DetailPageGridView">
                        <SelectedRowStyle Wrap="False"></SelectedRowStyle>
                        <EditRowStyle Wrap="False"></EditRowStyle>
                        <AlternatingRowStyle Wrap="False"></AlternatingRowStyle>
                        <RowStyle Wrap="False"></RowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="Type_Of_Equipment">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblTypeofEquipment" runat="server" Visible="True">
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="cboTypeOfEquipmentInGrid" runat="server">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="External_Product_Code">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblExtProdCode" runat="server" Visible="True">
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="cboExtProdCodeInGrid" runat="server"></asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Policy">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblPolicyNum" runat="server" Visible="True">
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtPolicyNum" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="False" HeaderText="Product_Policy_Id">
                                <ItemTemplate>
                                    <asp:Label ID="IdLabel" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="30px" Wrap="False"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                <ItemTemplate>
                                    <asp:ImageButton ID="EditButton_WRITE" Style="cursor: pointer; cursor: hand;" runat="server" ImageUrl="../Navigation/images/edit.png"
                                        CommandName="EditRecord" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="BtnCancel" runat="server" CommandName="CancelRecord" CommandArgument="<%#Container.DisplayIndex %>"
                                        Text="Cancel"></asp:LinkButton>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="30px" Wrap="False"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                <ItemTemplate>
                                    <asp:ImageButton Style="cursor: pointer; cursor: hand;" ID="DeleteButton_WRITE" ImageUrl="../Navigation/images/icon_delete.png"
                                        runat="server" CommandName="DeleteRecord" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Button ID="BtnSave_WRITE" runat="server" CommandName="SaveRecord" CommandArgument="<%#Container.DisplayIndex %>"
                                        Text="Save" SkinID="PrimaryRightButton"></asp:Button>
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                        <PagerStyle />
                    </asp:GridView>

                    <table class="tabBtnAreaZone" cellpadding="0" cellspacing="0" width="100%">
                        <tbody>
                            <tr>
                                <td />
                            </tr>
                            <tr>
                                <td>
                                    <input id="HiddenDeletePromptResponse" type="hidden" runat="server" designtimedragdrop="261" />
                                    <asp:Button runat="server" ID="btnNewProductPolicy_WRITE" SkinID="PrimaryLeftButton"
                                        Text="ADD_NEW" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <div id="tab_Accounting_Codes">
                <table border="0" cellpadding="0" cellspacing="0" width="100%" class="searchGrid">
                    <tr>
                        <td colspan="7">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; white-space: nowrap; vertical-align: bottom;">
                            <asp:Label ID="moAnalysisCode1Label" runat="server">Analysis_Code_1</asp:Label>
                        </td>
                        <td width="20%">
                            <asp:TextBox ID="moAnalysisCode1Text" runat="server" Width="185px"></asp:TextBox>
                        </td>
                        <td style="text-align: right; white-space: nowrap; vertical-align: bottom;">
                            <asp:Label ID="moAnalysisCode6Label" runat="server">Analysis_Code_6</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="moAnalysisCode6Text" runat="server" Width="185px"></asp:TextBox>
                        </td>
                    </tr>

                    <tr>
                        <td style="text-align: right; white-space: nowrap; vertical-align: bottom;">
                            <asp:Label ID="moAnalysisCode2Label" runat="server">Analysis_Code_2</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="moAnalysisCode2Text" runat="server" Width="185px"></asp:TextBox>
                        </td>
                        <td style="text-align: right; white-space: nowrap; vertical-align: bottom;">
                            <asp:Label ID="moAnalysisCode7Label" runat="server">Analysis_Code_7</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="moAnalysisCode7Text" runat="server" Width="185px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; white-space: nowrap; vertical-align: bottom;">
                            <asp:Label ID="moAnalysisCode3Label" runat="server">Analysis_Code_3</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="moAnalysisCode3Text" runat="server" Width="185px"></asp:TextBox>
                        </td>
                        <td style="text-align: right; white-space: nowrap; vertical-align: bottom;">
                            <asp:Label ID="moAnalysisCode8Label" runat="server">Analysis_Code_8</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="moAnalysisCode8Text" runat="server" Width="185px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; white-space: nowrap; vertical-align: bottom;">
                            <asp:Label ID="moAnalysisCode4Label" runat="server">Analysis_Code_4</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="moAnalysisCode4Text" runat="server" Width="185px"></asp:TextBox>
                        </td>
                        <td style="text-align: right; white-space: nowrap; vertical-align: bottom;">
                            <asp:Label ID="moAnalysisCode9Label" runat="server">Analysis_Code_9</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="moAnalysisCode9Text" runat="server" Width="185px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; white-space: nowrap; vertical-align: bottom;">
                            <asp:Label ID="moAnalysisCode5Label" runat="server">Analysis_Code_5</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="moAnalysisCode5Text" runat="server" Width="185px"></asp:TextBox>
                        </td>
                        <td style="text-align: right; white-space: nowrap; vertical-align: bottom;">
                            <asp:Label ID="moAnalysisCode10Label" runat="server">Analysis_Code_10</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="moAnalysisCode10Text" runat="server" Width="185px"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="tab_moAttributes_WRITE">
                <Elita:UserControlAttrtibutes runat="server" ID="AttributeValues" />
            </div>
            <div id="tab_ExtendedAttributes">
                <input id="hdnOperationType" type="hidden" runat="server" />
                <div class="Page" runat="server" id="mo_ExtendedAttributes" style="height: 100%; overflow: auto">
                    <asp:GridView ID="mo_ParentsGrid" runat="server" Width="100%" OnRowCreated="RowCreated"
                            OnRowDataBound="mo_ParentsGrid_RowDataBound" OnRowCommand="mo_ParentsGrid_OnRowCommand"
                            AllowPaging="True" AllowSorting="False" CellPadding="1"
                            AutoGenerateColumns="False" SkinID="DetailPageGridView" DataKeyNames="product_code_parent_id">
                        <SelectedRowStyle Wrap="False"></SelectedRowStyle>
                        <EditRowStyle Wrap="False"></EditRowStyle>
                        <AlternatingRowStyle Wrap="False"></AlternatingRowStyle>
                        <RowStyle Wrap="False"></RowStyle>
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <img alt="" style="cursor: pointer; cursor: hand;" src="..\\App_Themes\\Default\\Images\\sort_indicator_des.png" />
                                    <asp:Panel ID="pnlChild" runat="server" Style="display: none">
                                        <asp:GridView ID="gvChilds" runat="server" AutoGenerateColumns="false" SkinID="DetailPageGridView" OnRowDataBound="gvChilds_RowDataBound"
                                            DataKeyNames="product_code_detail_id">
                                            <Columns>
                                                <asp:BoundField ItemStyle-Width="150px" DataField="company_code" HeaderText="Company" />
                                                <asp:BoundField ItemStyle-Width="150px" DataField="dealer_code" HeaderText="Dealer" />
                                                <asp:BoundField ItemStyle-Width="150px" DataField="child_product_code" HeaderText="Child Product Code" />
                                                <asp:BoundField ItemStyle-Width="150px" DataField="Effective" HeaderText="Effective Date" />
                                                <asp:BoundField ItemStyle-Width="150px" DataField="Expiration" HeaderText="Expiration Date" />
                                            </Columns>
                                        </asp:GridView>
                                    </asp:Panel>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="product_code" SortExpression="product_code"
                                ReadOnly="true" HeaderText="PARENT CODE" HeaderStyle-HorizontalAlign="Center"
                                HtmlEncode="false" />
                            <asp:BoundField DataField="effective" SortExpression="effective"
                                ReadOnly="true" HeaderText="EFFECTIVE" HeaderStyle-HorizontalAlign="Center"
                                HtmlEncode="false" />
                            <asp:BoundField DataField="expiration" SortExpression="expiration"
                                ReadOnly="true" HeaderText="EXPIRATION" HeaderStyle-HorizontalAlign="Center"
                                HtmlEncode="false" />
                            <asp:BoundField DataField="smart_bundle_flat_amt" SortExpression="smart_bundle_flat_amt" ReadOnly="true"
                                HeaderText="SMART BUNDLE AMOUNT" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                            <asp:BoundField DataField="smart_bundle_flat_amt_currency" SortExpression="smart_bundle_flat_amt_currency" ReadOnly="true"
                                HeaderText="SMART BUNDLE CURRENCY" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                             <asp:TemplateField HeaderText="PAYMENT_SPLIT_RULE">
                                <ItemTemplate>
                                    <asp:Label ID="lblPaymentSplitRule" runat="server"></asp:Label>
                                </ItemTemplate>
                                 <EditItemTemplate>
                                     <asp:DropDownList ID="ddlPaymentSplitRule" runat="server" SkinID="MediumDropDown" AutoPostBack="True"></asp:DropDownList>
                                 </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="30px" Wrap="False"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                <ItemTemplate>
                                    <asp:ImageButton ID="BtnEditRecordParent" Style="cursor: pointer; cursor: hand;" runat="server" ImageUrl="../Navigation/images/edit.png"
                                       CommandName="EditRecord" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="BtnCancelRecordParent" runat="server" CommandName="CancelRecord" CommandArgument="<%#Container.DisplayIndex %>" 
                                                    Text="Cancel"></asp:LinkButton>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="30px" Wrap="False"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                <ItemTemplate></ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Button ID="BtnSaveRecordParent" runat="server" CommandName="SaveRecord" CommandArgument="<%#Container.DisplayIndex %>"
                                        Text="Save" SkinID="PrimaryRightButton"></asp:Button>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="product_code_parent_id" ReadOnly="true" Visible="false" />
                        </Columns>
                        <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                        <PagerStyle />
                    </asp:GridView>
                </div>
            </div>

            <div id="tab_moProductEquipment">
                <table id="tblProductEquipment" class="dataGrid" border="0" rules="cols" width="100%">
                    <tr>
                        <td colspan="2">
                            <ElitaUC:ErrorController ID="ErrorControllerProductEquipment" runat="server"></ElitaUC:ErrorController>

                        </td>
                    </tr>
                    <tr>
                        <td align="Center" colspan="2">
                            <div id="scroller" style="overflow: auto; width: 96%; height: 125px" align="center">
                                <asp:GridView ID="ProductEquipmentGridView" runat="server" OnRowCommand="ItemCommand"
                                    AllowPaging="False" AllowSorting="False" CellPadding="1" AutoGenerateColumns="False"
                                    SkinID="DetailPageGridView" Width="100%">
                                    <SelectedRowStyle Wrap="False"></SelectedRowStyle>
                                    <EditRowStyle Wrap="False"></EditRowStyle>
                                    <AlternatingRowStyle Wrap="False"></AlternatingRowStyle>
                                    <RowStyle Wrap="False"></RowStyle>
                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                    <Columns>
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="EditButton_WRITE" Style="cursor: pointer; cursor: hand;" runat="server" ImageUrl="~/Navigation/images/icons/edit2.gif"
                                                    CommandName="EditRecord" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="lblProductEquipmentID" Text='<%# GetGuidStringFromByteArray(Container.DataItem("PROD_ITEM_MANUF_EQUIP_ID"))%>'
                                                    runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="True" HeaderText="Product_Equipment_Make">
                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblEquipmentMake" Text='<%# Container.DataItem("EQUIPMENT_MAKE")%>'
                                                    runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="True" HeaderText="Product_Equipment_Model">
                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblEquipmentModel" Text='<%# Container.DataItem("EQUIPMENT_MODEL")%>'
                                                    runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="True" HeaderText="Product_Equipment_Effective_Date">
                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblProdEquipmentEffectiveDate" Text='<%# GetDateFormattedStringNullable(Container.DataItem("EFFECTIVE_DATE_PRODUCT_EQUIP"))%>'
                                                    runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="True" HeaderText="Product_Equipment_Expiration_Date">
                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblProdEquipmentExpirationDate" Text='<%# GetDateFormattedStringNullable(Container.DataItem("EXPIRATION_DATE_PRODUCT_EQUIP"))%>'
                                                    runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtProdEquipmentExpirationDate" Text='<%# GetDateFormattedStringNullable(Container.DataItem("EXPIRATION_DATE_PRODUCT_EQUIP"))%>'
                                                    runat="server">
                                                </asp:TextBox>
                                                <asp:ImageButton ID="ProdEquipmentExpirationDateImageButton" runat="server" Style="vertical-align: bottom"
                                                    ImageUrl="~/App_Themes/Default/Images/calendar.png" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="True" HeaderText="Product_Equipment_ModifyCreate_Date">
                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblProdEquipmentCreateDate" Text='<%# GetDateFormattedStringNullable(Container.DataItem("CREATE_DATE_PRODUCT_EQUIP"))%>'
                                                    runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" width="96%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="BtnSaveProdEquip_WRITE" runat="server" SkinID="PrimaryRightButton" Text="Save"></asp:Button>&nbsp;
                            <asp:Button ID="BtnCancelProdEquip" runat="server" SkinID="AlternateLeftButton" Text="Cancel"></asp:Button>
                        </td>
                    </tr>
                </table>
            </div>

            <div id="tab_moProductRewards">
                <div class="Page" runat="server" id="Div1" style="height: 100%; overflow: auto">
                    <asp:GridView ID="ProductRewardsGridView" runat="server" Width="100%" OnRowCreated="ProductRewardsGridView_RowCreated"
                        OnRowCommand="ProductRewardsGridView_RowCommand" AllowPaging="True" AllowSorting="False" CellPadding="1"
                        AutoGenerateColumns="False" SkinID="DetailPageGridView">
                        <SelectedRowStyle Wrap="False"></SelectedRowStyle>
                        <EditRowStyle Wrap="False"></EditRowStyle>
                        <AlternatingRowStyle Wrap="False"></AlternatingRowStyle>
                        <RowStyle Wrap="False"></RowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="Reward_Name">
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblRewardName" runat="server" Visible="True">
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtRewardName" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reward_Type">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblRewardType" runat="server" Visible="True">
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="cboRewardType" runat="server"></asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Reward_Amount">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblRewardAmount" runat="server" Visible="True">
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtRewardAmount" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Min_Purchase_Price">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblMinPurchasePrice" runat="server" Visible="True">
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtMinPurchasePrice" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Days_To_Redeem">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblDaysToRedeem" runat="server" Visible="True">
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDaysToRedeem" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="From_Renewal">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblFromRenewal" runat="server" Visible="True">
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtFromRenewal" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="To_Renewal">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblToRenewal" runat="server" Visible="True">
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtToRenewal" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="True" HeaderText="Start_Date" ItemStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblStartDate" runat="server"> </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtStartDate" runat="server" Visible="True" SkinID="exSmallTextBox"></asp:TextBox>
                                    <asp:ImageButton ID="btnStartDate" runat="server" align="AbsMiddle" ImageUrl="~/App_Themes/Default/Images/calendar.png"></asp:ImageButton>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="True" HeaderText="End_Date" ItemStyle-Wrap="false"
                                ItemStyle-HorizontalAlign="Left">
                                <ItemTemplate>
                                    <asp:Label ID="lblEndDate" runat="server"> </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtEndDate" runat="server" Visible="True" SkinID="exSmallTextBox"></asp:TextBox>
                                    <asp:ImageButton ID="btnEndDate" runat="server" align="AbsMiddle" ImageUrl="~/App_Themes/Default/Images/calendar.png"></asp:ImageButton>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="False" HeaderText="Prod_Reward_Id">
                                <ItemTemplate>
                                    <asp:Label ID="IdLabel" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="30px" Wrap="False"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                <ItemTemplate>
                                    <asp:ImageButton ID="EditButton_WRITE" Style="cursor: pointer; cursor: hand;" runat="server" ImageUrl="../Navigation/images/edit.png"
                                        CommandName="EditRecord" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="BtnCancel" runat="server" CommandName="CancelRecord" CommandArgument="<%#Container.DisplayIndex %>"
                                        Text="Cancel"></asp:LinkButton>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="30px" Wrap="False"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                <ItemTemplate>
                                    <asp:ImageButton Style="cursor: pointer; cursor: hand;" ID="DeleteButton_WRITE" ImageUrl="../Navigation/images/icon_delete.png"
                                        runat="server" CommandName="DeleteRecord" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Button ID="BtnSave_WRITE" runat="server" CommandName="SaveRecord" CommandArgument="<%#Container.DisplayIndex %>"
                                        Text="Save" SkinID="PrimaryRightButton"></asp:Button>
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                        <PagerStyle />
                    </asp:GridView>

                    <table class="tabBtnAreaZone" cellpadding="0" cellspacing="0" width="100%">
                        <tbody>
                            <tr>
                                <td />
                            </tr>
                            <tr>
                                <td>
                                    <input id="Hidden1" type="hidden" runat="server" designtimedragdrop="261" />
                                    <asp:Button runat="server" ID="btnNewProductRewards_WRITE" SkinID="PrimaryLeftButton"
                                        Text="ADD_NEW" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <div id="tab_DeviceTypeDetails">
                <table id="Table7" align="center" border="0" class="dataGrid" rules="cols" width="98%">
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label13" runat="server">Available</asp:Label>:
                        </td>
                        <td colspan="2">
                            <asp:DropDownList ID="ddlDeviceTypeAvailable" Width="200px" runat="server" SkinID="SmallDropDown">
                            </asp:DropDownList>
                            &nbsp;
                            <asp:Button ID="btnAddDeviceType" runat="server" Width="41px" Text="Add_Device_Type" SkinID="AlternateLeftButton"></asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="3">
                            <div id="DeviceTypesDetails" style="overflow: auto; width: 96%; height: 125px" align="center">
                                <asp:GridView ID="GridViewDeviceTypesDetails" runat="server"
                                    AllowPaging="false" AllowSorting="False" CellPadding="1" PageSize="50"
                                    AutoGenerateColumns="False" SkinID="DetailPageGridView" Width="100%">
                                    <SelectedRowStyle Wrap="True"></SelectedRowStyle>
                                    <EditRowStyle Wrap="True"></EditRowStyle>
                                    <AlternatingRowStyle Wrap="True"></AlternatingRowStyle>
                                    <RowStyle Wrap="True" HorizontalAlign="Left"></RowStyle>
                                    <Columns>
                                        <asp:TemplateField HeaderText="DEVICE_TYPES">
                                            <HeaderStyle></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="labelDeviceType"
                                                    runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="Method_Of_Repair">
                                            <HeaderStyle></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="labelMethodOfRepair" runat="server" Visible="True" />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlMethodOfRepair" runat="server" Visible="true" Width="220px"></asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="30px">
                                            <HeaderStyle ForeColor="#12135B" Wrap="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="EditButton_WRITE" Style="cursor: pointer; cursor: hand;" runat="server" ImageUrl="~/Navigation/images/icons/edit2.gif"
                                                    CommandName="EditRecord" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DeviceTypeId") %>'></asp:ImageButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="BtnCancel" runat="server" CommandName="CancelRecord" CommandArgument="<%#Container.DisplayIndex %>"
                                                    Text="Cancel"></asp:LinkButton>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="30px">
                                            <HeaderStyle ForeColor="#12135B" Wrap="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:ImageButton Style="cursor: pointer; cursor: hand;" ID="DeleteButton_WRITE" ImageUrl="~/Navigation/images/icons/trash.gif"
                                                    runat="server" CommandName="DeleteRecord" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "DeviceTypeId") %>'></asp:ImageButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Button ID="BtnSave_WRITE" runat="server" CommandName="SaveRecord" CommandArgument="<%#Container.DisplayIndex %>"
                                                    Text="Save" SkinID="PrimaryRightButton"></asp:Button>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="tab_moProductBenefits">
                <div class="Page" runat="server" id="Div2" style="height: 100%; overflow: auto">
                    <asp:GridView ID="ProductBenefitsGridView" runat="server" Width="100%" OnRowCreated="ProductBenefitsGridView_RowCreated"
                        OnRowCommand="ProductBenefitsGridView_RowCommand" AllowPaging="True" AllowSorting="False" CellPadding="1" 
                        AutoGenerateColumns="False"  SkinID="DetailPageGridView" >
                        <SelectedRowStyle Wrap="False"></SelectedRowStyle>
                        <EditRowStyle Wrap="False"></EditRowStyle>
                        <AlternatingRowStyle Wrap="False"></AlternatingRowStyle>
                        <RowStyle Wrap="False"></RowStyle>
                        <HeaderStyle Wrap="False"></HeaderStyle>
                        <Columns>
                            <asp:TemplateField Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblProductBenefitsID" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="True" HeaderText="Product_Equipment_Make">
                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblEquipmentMake" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="cboEquipmentMakeModel" runat="server"></asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="True" HeaderText="Product_Equipment_Model">
                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblEquipmentModel" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="True" HeaderText="Product_Equipment_Effective_Date">
                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label runat="server" ID="lblProdBenefitsEffectiveDate" ></asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtProdBenefitsEffectiveDate" runat="server" Visible="True" SkinID="exSmallTextBox"></asp:TextBox>
                                    <asp:ImageButton ID="btnProdBenefitsEffectiveDate" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png"></asp:ImageButton>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="True" HeaderText="Product_Equipment_Expiration_Date">
                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblProdBenefitsExpirationDate" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtProdBenefitsExpirationDate" runat="server" Visible="True" SkinID="exSmallTextBox"></asp:TextBox>
                                    <asp:ImageButton ID="btnProdBenefitsExpirationDate" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png"></asp:ImageButton>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="True" HeaderText="Product_Equipment_ModifyCreate_Date">
                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblProdBenefitsCreateDate" runat="server"></asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="30px" Wrap="False"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                <ItemTemplate>
                                    <asp:ImageButton ID="EditButton_WRITE" Style="cursor: pointer; cursor: hand;" runat="server" ImageUrl="../Navigation/images/edit.png"
                                        CommandName="EditRecord" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="BtnCancel" runat="server" CommandName="CancelRecord" CommandArgument="<%#Container.DisplayIndex %>"
                                        Text="Cancel"></asp:LinkButton>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="30px" Wrap="False"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                <ItemTemplate>
                                    <asp:ImageButton Style="cursor: pointer; cursor: hand;" ID="DeleteButton_WRITE" ImageUrl="../Navigation/images/icon_delete.png"
                                        runat="server" CommandName="DeleteRecord" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Button ID="BtnSave_WRITE" runat="server" CommandName="SaveRecord" CommandArgument="<%#Container.DisplayIndex %>"
                                        Text="Save" SkinID="PrimaryRightButton"></asp:Button>
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                        <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
                    </asp:GridView>
                    <table class="tabBtnAreaZone" cellpadding="0" cellspacing="0" width="100%">
                        <tbody>
                            <tr>
                                <td />
                            </tr>
                            <tr>
                                <td>
                                    <input id="Hidden2" type="hidden" runat="server" designtimedragdrop="261" />
                                    <asp:Button runat="server" ID="btnNewProductBenefits_WRITE" SkinID="PrimaryLeftButton"
                                        Text="ADD_NEW" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>

            </div>
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
            SkinID="AlternateLeftButton"></asp:Button>
        <asp:Button ID="btnDelete_WRITE" runat="server" CausesValidation="False" Text="Delete"
            SkinID="CenterButton"></asp:Button>
        <asp:Button ID="btnProdRepairPrice_WRITE" runat="server" Text="Method_of_repair_by_price"
            SkinID="CenterButton"></asp:Button>
    </div>

</asp:Content>
