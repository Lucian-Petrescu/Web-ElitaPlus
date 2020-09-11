<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ContractForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ContractForm" EnableSessionState="True"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" Theme="Default" %>

<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>

<%@ Register Assembly="Microsoft.Web.UI.WebControls" Namespace="Microsoft.Web.UI.WebControls" TagPrefix="iewc" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="JavaScript" type="text/javascript" src="../Navigation/Scripts/GlobalHeader.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0" class="formGrid" style="padding: 0px; margin: 0px">
        <tr>
            <td align="center" colspan="4" class="borderLeft">
                <div style="width: 78%" align="right">
                    <uc1:MultipleColumnDDLabelControl ID="multipleDropControl" runat="server"></uc1:MultipleColumnDDLabelControl>
                </div>
            </td>
        </tr>
        <tr>
            <td style="width: 30%;" align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="LabelStartDate" runat="server">Start_Date</asp:Label>
            </td>
            <td style="width: 23%;">&nbsp;
                    <asp:TextBox ID="TextboxStartDate_WRITE" TabIndex="3" runat="server" SkinID="MediumTextBox" Width="150px"></asp:TextBox>
                <asp:ImageButton ID="ImageButtonStartDate" ImageAlign="AbsMiddle" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png"></asp:ImageButton>
            </td>
            <td style="width: 22%" align="right">
                <asp:Label ID="LabelEndDate" runat="server">End_Date</asp:Label>
            </td>
            <td style="width: 25%;">&nbsp;
                    <asp:TextBox ID="TextboxEndDate_WRITE" TabIndex="9" runat="server" SkinID="MediumTextBox" Width="150px"></asp:TextBox>
                <asp:ImageButton ID="ImageButtonEndDate" ImageAlign="AbsMiddle" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png"></asp:ImageButton>
            </td>
        </tr>
        <tr>
            <td style="width: 30%;" align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="LabelContractType" runat="server">Contract_Type</asp:Label>
            </td>
            <td style="width: 23%;">&nbsp;
                    <asp:DropDownList ID="cboContractType" TabIndex="4" runat="server" SkinID="SmallDropDown" Width="179px">
                    </asp:DropDownList>
            </td>

            <td style="width: 22%;" align="right">

                <asp:Label ID="LabelPolicyType" runat="server">Policy_Type</asp:Label>
            </td>
            <td style="width: 25%;">&nbsp;
                    <asp:DropDownList ID="cboPolicyType" TabIndex="4" runat="server" SkinID="ExtraSmallDropDown" AutoPostBack="True" Width="179px">
                    </asp:DropDownList>
            </td>
        </tr>

         <tr>
             <td></td>
             <td></td>
             
             <td style="width: 22%;" align="right">

                <asp:Label ID="LabelCollectivePolicyGeneration" runat="server">POLICY_GENERATION</asp:Label>
            </td>
            <td style="width: 25%;">&nbsp;


                    <asp:DropDownList ID="cboCollectivePolicyGeneration" TabIndex="4" runat="server" SkinID="SmallDropDown" AutoPostBack="True" Width="179px">
                    </asp:DropDownList>
            </td>
          </tr>

        <tr>
            <td align="right" class="borderLeft">&nbsp;</td>
            <td>&nbsp;</td>

              <td style="width: 22%;" align="right">
                <asp:Label ID="LabelLineOfBusiness" runat="server">Line_Of_Business</asp:Label>
            </td>
            <td style="width: 25%;">&nbsp;
                    <asp:DropDownList ID="cboLineOfBusiness" TabIndex="4" runat="server" SkinID="SmallDropDown" AutoPostBack="True" Width="179px">
                    </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="LabelREPLACEMENT_POLICY" runat="server">REPLACEMENT_POLICY</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:DropDownList ID="cboReplacementPolicy" TabIndex="5" runat="server" Width="179px" SkinID="SmallDropDown" AutoPostBack="false" onchange="showRepPolicyClaimCnt();">
                    </asp:DropDownList>
            </td>

              <td align="right">
                <asp:Label ID="LabelPolicy" runat="server">CONTRACT_POLICY</asp:Label>
            </td>
            <td>&nbsp;&nbsp;<asp:TextBox ID="TextboxPolicyNumber" TabIndex="10" runat="server" SkinID="MediumTextBox" Width="178px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="lblReplacementPolicyClaimCount" runat="server">REPLACEMENT_POLICY_CLAIM_COUNT</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:TextBox ID="txtReplacementPolicyCliamCount" runat="server" SkinID="MediumTextBox" Width="178px" MaxLength="2"></asp:TextBox>
            </td>
             <td align="right">
                <asp:Label ID="LabelLayout" runat="server">LAYOUT</asp:Label>

            </td>

            <td>&nbsp;
                    <asp:TextBox ID="TextboxLayout" TabIndex="11" runat="server" SkinID="MediumTextBox" Width="178px"></asp:TextBox>
            </td>

        </tr>
        <tr>
            <td align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="LabelCancellationReason" runat="server">CANCELLATION_CODE</asp:Label>
            </td>


            <td>&nbsp;
                    <asp:DropDownList ID="cboCancellationReason" TabIndex="6" runat="server" Width="179px" SkinID="SmallDropDown" AutoPostBack="True">
                    </asp:DropDownList>
            </td>

            <td align="right">
                <asp:Label ID="LabelFullRefundDays" runat="server">FULL_REFUND_DAYS</asp:Label>

            </td>
            <td>&nbsp;
                    <asp:TextBox ID="TextboxFullRefundDays" TabIndex="12" runat="server" SkinID="MediumTextBox" Width="178px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="LabelAcctBusinessUnit" runat="server">ACCOUNTING_BUSINESS_UNIT</asp:Label>
            </td>


            <td>&nbsp;
                    <asp:DropDownList ID="cboAcctBusinessUnit" TabIndex="6" runat="server" Width="179px" SkinID="SmallDropDown" AutoPostBack="False">
                    </asp:DropDownList>
            </td>
            <td align="right">
                <asp:Label ID="lblAuthorizedAmountMaxUpdates" runat="server">AUTHORIZED_AMOUNT_MAX_#</asp:Label>

            </td>
            <td>&nbsp;
                    <asp:TextBox ID="txtAuthorizedAmountMaxUpdates" TabIndex="12" runat="server" SkinID="MediumTextBox" Width="178px"></asp:TextBox>

            </td>
        </tr>
        <tr>
            <td align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="lblDaysToReportClaim" runat="server">DAYS_TO_REPORT_CLAIM:</asp:Label>
            </td>


            <td>&nbsp;
                    <asp:TextBox ID="txtDaysToReportClaim" TabIndex="7" runat="server" SkinID="MediumTextBox" Width="178px" MaxLength="3"></asp:TextBox>
            </td>
            <td align="right">
                <asp:Label ID="LabelNumOfClaims" runat="server">NUMBER_OF_CLAIMS</asp:Label>

            </td>
            <td>&nbsp;
                    <asp:TextBox ID="txtNumOfClaims" TabIndex="12" runat="server" SkinID="MediumTextBox" Width="178px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="LabelPenaltyPct" runat="server">PENALTY_PCT</asp:Label>
            </td>


            <td>&nbsp;
                    <asp:TextBox ID="txtPenaltyPct" runat="server" SkinID="MediumTextBox" TabIndex="8" Width="178px"></asp:TextBox>
            </td>
            <td align="right">
                <asp:Label ID="LabelNumOfRepairClaims" runat="server">NUMBER_OF_REPAIR_CLAIMS</asp:Label>

            </td>
            <td>&nbsp;
                    <asp:TextBox ID="txtNumOfRepairClaims" TabIndex="12" runat="server" SkinID="MediumTextBox" Width="178px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="lblDaysToReactivate" runat="server">DAYS_TO_REACTIVATE</asp:Label>
            </td>
            <td>&nbsp

                    <asp:TextBox ID="txtbxDaysToReactivate" TabIndex="12" runat="server" SkinID="MediumTextBox" Width="178px"></asp:TextBox>
            </td>
            <td align="right">
                <asp:Label ID="LabelNumOfReplClaims" runat="server">NUMBER_OF_REPLACEMENT_CLAIMS</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:TextBox ID="txtNumOfReplClaims" TabIndex="12" runat="server" SkinID="MediumTextBox" Width="178px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" class="borderLeft">&nbsp;</td>
            <td></td>
            <td align="right">
                <asp:Label ID="LabelReplacementBasedOn" runat="server">CLAIM_LIMIT_BASED_ON</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:DropDownList ID="cboReplacementBasedOn" TabIndex="13" runat="server" Width="179px" SkinID="SmallDropDown">
                    </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="LabelCurrency" runat="server">Currency</asp:Label>
            </td>

            <td>&nbsp;
                    <asp:DropDownList ID="cboCurrency" TabIndex="21" runat="server" Width="179px" SkinID="SmallDropDown">


                    </asp:DropDownList>
            </td>

            <td align="right">
                <asp:Label ID="LabelCovDeductible" runat="server">Coverage_Deductible</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:DropDownList ID="cboCovDeductible" TabIndex="139" runat="server" Width="179px" SkinID="SmallDropDown" AutoPostBack="True">
                    </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td valign="middle" align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="LabelRepairDiscountPct" runat="server">REPAIR_DISCOUNT_PCT</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:TextBox ID="TextboxRepairDiscountPct" TabIndex="22" runat="server" SkinID="MediumTextBox" Width="178px"></asp:TextBox>
            </td>

            <td valign="middle" align="right">
                <asp:Label ID="LabelDeductibleBasedOn" runat="server">Compute_Deductible_Based_On</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:DropDownList ID="cboDeductibleBasedOn" TabIndex="140" runat="server" Width="179px" SkinID="SmallDropDown" AutoPostBack="True">
                    </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td valign="middle" align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="LabelReplacementDiscountPrc" runat="server">REPLACEMENT_DISCOUNT_PCT</asp:Label>

            </td>
            <td>&nbsp;
                    <asp:TextBox ID="TextboxReplacementDiscountPct" TabIndex="23" runat="server" SkinID="MediumTextBox" Width="178px"></asp:TextBox>
            </td>
            <td valign="middle" align="right">
                <asp:Label ID="LabelDeductible" runat="server">Deductible</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:TextBox ID="TextboxDeductible" TabIndex="141" runat="server" SkinID="MediumTextBox" Width="178px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td valign="middle" align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="LabelCURRENCY_OF_COVERAGES" runat="server">CURRENCY_OF_COVERAGES</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:DropDownList ID="cboCURRENCY_OF_COVERAGES" TabIndex="24" runat="server" Width="179px" SkinID="SmallDropDown">

                    </asp:DropDownList>
            </td>
            <td valign="middle" align="right">
                <asp:Label ID="LabelDeductiblePercent" runat="server">Deductible_Percent</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:TextBox ID="TextboxDeductiblePercent" TabIndex="142" runat="server" SkinID="MediumTextBox" Width="178px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td valign="middle" align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="Label_CURRENCY_CONVERSION" runat="server">CURRENCY_CONVERSION</asp:Label>
            </td>

            <td>&nbsp;
                    <asp:DropDownList ID="cboCURRENCY_CONVERSION" TabIndex="25" runat="server" Width="179px" SkinID="SmallDropDown" AutoPostBack="True">
                    </asp:DropDownList>
            </td>
            <td valign="middle" align="right">
                <asp:Label ID="LabelDeductibleByMfg" runat="server">DEDUCTIBLE_BY_MFG</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:DropDownList ID="cboDeductible_By_Manufacturer" TabIndex="143" runat="server" SkinID="SmallDropDown" Width="179px">
                    </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td valign="middle" align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="LabelEditModel" runat="server">Edit_Model</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:DropDownList ID="cboEditModel" TabIndex="26" runat="server" Width="179px" SkinID="SmallDropDown">
                    </asp:DropDownList>
            </td>
            <td valign="middle" align="right">
                <asp:Label ID="LabelMinReplCost" runat="server">Min_Replacement_Cost</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:TextBox ID="TextboxMinRepCost" TabIndex="144" runat="server" SkinID="MediumTextBox" Width="178px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td valign="middle" align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="LabelAutoMFG" runat="server">Auto_MFG_Coverage</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:DropDownList ID="cboAutoMFG" TabIndex="27" runat="server" Width="179px" SkinID="SmallDropDown">
                    </asp:DropDownList>
            </td>
            <td align="right">
                <asp:Label ID="LabelWaitingPeriod" runat="server">WAITING_PERIOD_IN_DAYS</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:TextBox ID="TextboxWaitingPeriod" TabIndex="145" runat="server" SkinID="MediumTextBox" Width="178px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td valign="middle" align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="LabelFixedEscDurationFlag" runat="server">FIXED_ESC_DURATION</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:DropDownList ID="cboFixedEscDurationFlag" TabIndex="28" runat="server" Width="179px" SkinID="SmallDropDown">
                    </asp:DropDownList>
            </td>
            <td align="right">
                <asp:Label ID="LabelApplyWaitingPeriod" runat="server">ALWAYS_APPLY_WAITING_PERIOD</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:DropDownList ID="cboIgnoreWaitingPeriodWhen_WSD_Equal_PSD" TabIndex="146" runat="server" Width="179px" SkinID="SmallDropDown">
                    </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2" valign="middle" align="right" class="borderLeft">&nbsp;</td>
            <tb></tb>
            <td align="right">
                <asp:Label ID="LabelWarrantyMaxDelay" runat="server">Warranty_Max Delay_in_Days</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:TextBox ID="TextboxWarrantyMaxDelay" TabIndex="147" runat="server" SkinID="MediumTextBox" Width="178px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td valign="middle" align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="LabelDealerMarkup" runat="server">Dealer_Markup</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:DropDownList ID="cboDealerMarkup_WRITE" TabIndex="30" runat="server" Width="179px" SkinID="SmallDropDown" AutoPostBack="True">
                    </asp:DropDownList>
            </td>
            <td valign="middle" align="right">
                <asp:Label ID="LabelRemainingMFGDays" runat="server">Remaining_MFG_Days</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:TextBox ID="TextBoxRemainingMFGDays" TabIndex="148" runat="server" SkinID="MediumTextBox" Width="179px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td valign="middle" align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="LabelRestrictMarkup_WRITE" runat="server">Restrict_Markup</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:DropDownList ID="cboRestrictMarkup_WRITE" TabIndex="31" runat="server" Width="179px" SkinID="SmallDropDown">
                    </asp:DropDownList>
            </td>
            <td valign="middle" align="right">
                <asp:Label ID="LabelFundingSource" runat="server">Funding_Source</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:DropDownList ID="cboFundingSource" TabIndex="149" runat="server" Width="179px" SkinID="SmallDropDown">
                    </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" class="borderLeft">&nbsp;
                <asp:Label ID="lblAllowCoverageMarkupDistribution" runat="server">ALLOW_COVERAGE_MARKUP_DISTRIBUTION</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:DropDownList ID="ddlAllowCoverageMarkupDistribution" TabIndex="171" runat="server" Width="179px" SkinID="SmallDropDown">
                    </asp:DropDownList>
            </td>
            <td valign="middle" align="right" nowrap="nowrap"></td>
            <td>&nbsp;
            </td>
        </tr>
        <tr>
            <td valign="middle" align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="LabelClaimControl" runat="server">CLAIM_CONTROL</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:DropDownList ID="cboClaimControlID" TabIndex="32" runat="server" Width="179px" SkinID="SmallDropDown" AutoPostBack="True">
                    </asp:DropDownList>
            </td>
            <td valign="middle" align="right">
                <asp:Label ID="LabelTypeOfIns" runat="server">Type_of_Insurance</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:DropDownList ID="cboTypeOfIns" TabIndex="150" runat="server" Width="179px" SkinID="SmallDropDown">
                    </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td valign="middle" align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="LabelRecurringPremium" runat="server">PERIODIC_BILLING</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:DropDownList ID="cboRecurringPremium" TabIndex="33" runat="server" Width="179px" SkinID="SmallDropDown" AutoPostBack="True">
                    </asp:DropDownList>
            </td>
            <td valign="middle" align="right">
                <asp:Label ID="LabelTypeOfMarketing" runat="server">Type_of_Marketing</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:DropDownList ID="cboTypeOfMarketing" TabIndex="151" runat="server" Width="179px" SkinID="SmallDropDown">
                    </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td valign="middle" align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="LabelIncludeFirstPmt" runat="server">INCLUDE_FIRST_PYMT</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:DropDownList ID="cboIncludeFirstPmt" TabIndex="34" runat="server" Width="179px" AutoPostBack="True" SkinID="SmallDropDown">
                    </asp:DropDownList>
            </td>
            <td valign="middle" align="right">
                <asp:Label ID="LabelTypeOfEquipment" runat="server">Type_of_Equipment</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:DropDownList ID="cboTypeOfEquipment" TabIndex="152" runat="server" Width="179px" SkinID="SmallDropDown">
                    </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td valign="middle" align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="labelFirstPymtMonths" runat="server">FIRST_PYMT_MONTHS</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:TextBox ID="txtFirstPaymentMonths" TabIndex="35" runat="server" SkinID="MediumTextBox" Width="178px"></asp:TextBox>
            </td>
            <td valign="middle" align="right">
                <asp:Label ID="lblPeridiocBillingWarntyPeriod" runat="server">PERIDIOC_BILLING_WARNTY_PERIOD</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:TextBox ID="txtPeridiocBillingWarntyPeriod" TabIndex="35" runat="server" SkinID="MediumTextBox" Width="178px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td valign="middle" align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="LabelDaysToSuspend" runat="server">DAYS_TO_SUSPEND</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:TextBox ID="TextboxDaysToSuspend" TabIndex="36" runat="server" SkinID="MediumTextBox" Width="178px"></asp:TextBox>
            </td>
            <td valign="middle" align="right">
                <asp:Label ID="LabelNetTaxes" runat="server">Net_Taxes</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:DropDownList ID="cboNetTaxes" TabIndex="154" runat="server" Width="179px" SkinID="SmallDropDown">
                    </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td valign="middle" align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="LabelDaysToCancel" runat="server">DAYS_TO_CANCEL</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:TextBox ID="TextboxDaysToCancel" TabIndex="37" runat="server" SkinID="MediumTextBox" Width="178px"></asp:TextBox>
            </td>
            <td valign="middle" align="right">
                <asp:Label ID="LabelNetCommission" runat="server">Net_Commissions</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:DropDownList ID="cboNetCommission" TabIndex="155" runat="server" Width="179px" SkinID="SmallDropDown">
                    </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td valign="middle" align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="lblExtendCoverage" runat="server">EXTEND_COVERAGE:</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:DropDownList ID="cboExtendCoverage" TabIndex="38" SkinID="SmallDropDown" runat="server" Width="179px" onchange="showExtendCovField();">

                    </asp:DropDownList>
            </td>
            <td valign="middle" align="right">
                <asp:Label ID="LabelNetMarketing" runat="server">Net_Marketing</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:DropDownList ID="cboNetMarketing" TabIndex="156" runat="server" Width="179px" SkinID="SmallDropDown">
                    </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="height: 17px" valign="middle" align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="lblExtendCoverageBy" runat="server">EXTEND_COVERAGE_BY_EXTRA:</asp:Label>
            </td>
            <td style="white-space: nowrap">&nbsp;
                    <asp:TextBox ID="txtExtendCoverageByExtraMonths" TabIndex="39" runat="server" SkinID="MediumTextBox" MaxLength="2" Width="35px"></asp:TextBox>
                <asp:Label ID="lblExtendCoverageByExtraMonths" runat="server">MONTH(S)</asp:Label>
                <asp:TextBox ID="txtExtendCoverageByExtraDays" TabIndex="40" runat="server" SkinID="MediumTextBox" MaxLength="4" Width="35px"></asp:TextBox>
                <asp:Label ID="lblExtendCoverageByExtraDays" runat="server">DAY(S)</asp:Label>
            </td>
            <td valign="middle" align="right">
                <asp:Label ID="LabelIsCommPCodeId" runat="server">COMMISSION_BY_PRODUCT_CODE</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:DropDownList ID="cboIsCommPCodeId" TabIndex="157" runat="server" Width="179px" SkinID="SmallDropDown">
                    </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="LabelPayOutstandingAmount" runat="server">PAY_OUTSTANDING_PREMIUM</asp:Label>
                <asp:Label ID="lblAllowPymtSkipMonths" runat="server">ALLOW_PYMT_SKIP_MONTHS:</asp:Label>
            </td>

            <td>&nbsp;
                    <asp:DropDownList ID="cboPayOutstandingAmount" TabIndex="39" runat="server" Width="179px" SkinID="SmallDropDown">
                    </asp:DropDownList>
                <asp:DropDownList ID="ddlAllowPymtSkipMonths" TabIndex="39" runat="server" Width="179px" SkinID="SmallDropDown">
                </asp:DropDownList>
            </td>
            <td valign="middle" align="right">
                <asp:Label ID="lblBillingCycleType" runat="server">BILLING_CYCLE_TYPE</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:DropDownList ID="ddlBillingCycleType" runat="server" Width="179px" SkinID="SmallDropDown">
                    </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="width: 25%; height: 8px" valign="middle" align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="LabelAutoSetLiability" runat="server">AUTO_SET_LIABILITY</asp:Label>
            </td>

            <td style="width: 25%; height: 8px">&nbsp;
                    <asp:DropDownList ID="cboAutoSetLiability" TabIndex="41" runat="server" Width="179px" SkinID="SmallDropDown">


                    </asp:DropDownList>
            </td>
            <td valign="middle" align="right">
                <asp:Label ID="LabelID_VALIDATION" runat="server">ID_VALIDATION</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:DropDownList ID="cboID_VALIDATION" TabIndex="157" runat="server" Width="179px" SkinID="SmallDropDown" AutoPostBack="True">
                    </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td valign="middle" align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="LabelIgnorePremium" runat="server">IGNORE_INCOMING_PREMIUM</asp:Label>
            </td>

            <td>&nbsp;
                    <asp:DropDownList ID="cboIgnorePremium" TabIndex="42" runat="server" Width="179px" SkinID="SmallDropDown">
                    </asp:DropDownList>


            </td>
            <td valign="middle" align="right">
                <asp:Label ID="LabelAcselProdCode" runat="server">ACSEL_PROD_CODE</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:DropDownList ID="cboAcselProdCode" TabIndex="158" runat="server" Width="179px" SkinID="SmallDropDown" AutoPostBack="True">
                    </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="LabelIgnoreCovAmt" runat="server">IGNORE_COVERAGE_AMOUNT</asp:Label>
            </td>

            <td>&nbsp;
                    <asp:DropDownList ID="cboIgnoreCovAmt" TabIndex="43" runat="server" Width="179px" SkinID="SmallDropDown">
                    </asp:DropDownList>
            </td>
            <td valign="middle" align="right">
                <asp:Label ID="LabelBackEndClaimAllowed" runat="server">BACKEND_CLAIMS_ALLOWED</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:DropDownList ID="cboBackEndClaimAllowed" TabIndex="159" runat="server" Width="179px" SkinID="SmallDropDown">
                    </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" class="borderLeft">&nbsp;
            </td>
            <td>&nbsp;
            </td>
            <td valign="middle" align="right" nowrap="nowrap">
                <asp:Label ID="labelAllowDifferentCoverage" runat="server">ALLOW_DIFFERENT_COVERAGE</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:DropDownList ID="cboAllowDifferentCoverage" TabIndex="159" runat="server" Width="179px" SkinID="SmallDropDown">
                    </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="LabelInstallmentPayment" runat="server">INSTALLMENT_PAYMENT</asp:Label>
            </td>

            <td>&nbsp;
                    <asp:DropDownList ID="cboInstallmentPayment" TabIndex="45" runat="server" Width="179px" SkinID="SmallDropDown" AutoPostBack="true">
                    </asp:DropDownList>
            </td>
            <td valign="middle" align="right">
                <asp:Label ID="LabelDaysOfFirstPymt" runat="server">DAYS_OF_FIRST_PAYMENT</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:TextBox ID="TextboxDaysOfFirstPymt" TabIndex="160" runat="server" SkinID="MediumTextBox" Width="178px"></asp:TextBox>
            </td>
        </tr>
        <tr id="moInstallmentBillingInformation0" runat="server">
            <td align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="LabelDaysToSendLetter" runat="server">DAYS_TO_SEND_LETTER</asp:Label>
            </td>

            <td>&nbsp;
                    <asp:TextBox ID="TextboxDaysToSendLetter" TabIndex="46" runat="server" SkinID="MediumTextBox" Width="178px"></asp:TextBox>
            </td>
            <td valign="middle" align="right">
                <asp:Label ID="LabelDaysToCancelCert" runat="server">DAYS_TO_CANCEL_CERT</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:TextBox ID="TextboxDaysToCancelCert" TabIndex="161" runat="server" SkinID="MediumTextBox" Width="178px"></asp:TextBox>
            </td>
        </tr>
        <tr id="moInstallmentBillingInformation1" runat="server">
            <td align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="LabelBaseInstallments" runat="server">Base_Installments</asp:Label>
            </td>

            <td>&nbsp;
                    <asp:DropDownList ID="cboBaseInstallments" TabIndex="47" runat="server" Width="179px" SkinID="SmallDropDown">
                    </asp:DropDownList>
            </td>
            <td valign="middle" align="right">
                <asp:Label ID="LabelMaxNumofInstallments" runat="server">Max_Num_of_Installments</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:TextBox ID="TextboxMaxNumofInstallments" TabIndex="162" runat="server" SkinID="MediumTextBox" Width="178px"></asp:TextBox>
            </td>
        </tr>
        <tr id="moInstallmentBillingInformation2" runat="server">
            <td align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="LabelBillingCycleFrequency" runat="server">Billing_Cycle_Frequency</asp:Label>
            </td>

            <td>&nbsp;
                    <asp:DropDownList ID="cboBillingCycleFrequency" TabIndex="48" runat="server" Width="179px" SkinID="SmallDropDown">
                    </asp:DropDownList>
            </td>
            <td valign="middle" align="right">
                <asp:Label ID="LabelInstallmentsBaseReducer" runat="server">Installments_Base_Reducer</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:TextBox ID="TextboxInstallmentsBaseReducer" TabIndex="163" runat="server" SkinID="MediumTextBox" Width="178px"></asp:TextBox>
            </td>
        </tr>
        <tr id="moInstallmentBillingInformation3" runat="server">
            <td valign="middle" align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="LabelCollectionReAttempts" runat="server">COLLECTION_RE_ATTEMPTS</asp:Label>
            </td>

            <td>&nbsp;
                    <asp:TextBox ID="txtCollectionReAttempts" TabIndex="49" runat="server" SkinID="MediumTextBox" Width="178px"></asp:TextBox>
            </td>
            <td valign="middle" align="right">
                <asp:Label ID="lblPastDueMonthsAllowed" runat="server">PAST_DUE_MONTHS_ALLOWED:</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:TextBox ID="txtPastDueMonthsAllowed" TabIndex="164" runat="server" SkinID="MediumTextBox" Width="178px"></asp:TextBox>
            </td>
        </tr>
        <tr id="moInstallmentBillingInformation4" runat="server">
            <td align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="LabelCollectionCycleType" runat="server">Collection_Cycle_Type</asp:Label>
            </td>

            <td>&nbsp;
                    <asp:DropDownList ID="cboCollectionCycleType" TabIndex="50" runat="server" Width="179px" SkinID="SmallDropDown">
                    </asp:DropDownList>
            </td>
            <td valign="middle" align="right">
                <asp:Label ID="LabelOffsetBeforeDueDate" runat="server">Offset_Before_Due_Date</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:TextBox ID="TextboxOffsetBeforeDueDate" TabIndex="165" runat="server" SkinID="MediumTextBox" Width="178px"></asp:TextBox>
            </td>
        </tr>
        <tr id="moInstallmentBillingInformation5" runat="server">
            <td valign="middle" align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="LabelCycleDay" runat="server">Cycle_Day</asp:Label>
            </td>

            <td>&nbsp;
                    <asp:TextBox ID="TextboxCycleDay" TabIndex="51" runat="server" SkinID="MediumTextBox" Width="178px"></asp:TextBox>

            </td>
            <td colspan="2"></td>
        </tr>
        <tr>
            <td colspan="2" class="borderLeft">&nbsp;
            </td>
            <td valign="middle" align="right">
                <asp:Label ID="lblCLIPct" runat="server">CLIP_PERCENT:</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:TextBox ID="txtCLIPPct" TabIndex="166" runat="server" SkinID="MediumTextBox" Width="178px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="LabelEditMfgTerm" runat="server">EDIT_MFG_TERM</asp:Label>
            </td>

            <td>&nbsp;
                    <asp:DropDownList ID="cboEDIT_MFG_TERM" TabIndex="52" runat="server" Width="179px" SkinID="SmallDropDown" AutoPostBack="true">
                    </asp:DropDownList>
            </td>
            <td valign="middle" align="right">
                <asp:Label ID="lblInsPremFactor" runat="server">INSURANCE_PREMIUM_FACTOR:</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:TextBox ID="txtInsPremFactor" TabIndex="168" runat="server" SkinID="MediumTextBox" Width="178px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="LabelOverrideEditMfgTerm" runat="server">OVERRIDE_EDIT_MFG_TERM</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:DropDownList ID="cboOverrideEditMfgTerm" TabIndex="53" runat="server" Width="179px" SkinID="SmallDropDown">
                    </asp:DropDownList>
            </td>
            <td valign="middle" align="right">
                <asp:Label ID="lblMarketingPromo" runat="server">MARKETING_PROMO</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:DropDownList ID="cboMarketingPromo" TabIndex="170" runat="server" SkinID="SmallDropDown" Width="179px">
                    </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="lblAllowNoExtended" runat="server">ALLOW_NO_EXTENDED</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:DropDownList ID="cboAllowNoExtended" TabIndex="54" runat="server" Width="179px" SkinID="SmallDropDown" AutoPostBack="true">
                    </asp:DropDownList>


            </td>
            <td valign="middle" align="right">
                <asp:Label ID="lblAllowMultipleRejections" runat="server">ALLOW_CC_REJECTIONS</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:DropDownList ID="cboAllowMultipleRejections" TabIndex="170" runat="server" SkinID="SmallDropDown" Width="179px">
                    </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" class="borderLeft">&nbsp;</td>
            <td></td>
            <td valign="middle" align="right">
                <asp:Label ID="LabelProRataMethodId" runat="server">PRO_RATA_METHOD</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:DropDownList ID="cboProRataMethodId" TabIndex="171" runat="server" AutoPostBack="true" Width="179px" SkinID="SmallDropDown">
                    </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="labelCustAddressRequired" runat="server">CUSTOMER_ADDRESS_REQUIRED_TO_OPEN_CLAIMS</asp:Label>
            </td>

            <td>&nbsp;
                    <asp:DropDownList ID="cboCustAddressRequired" TabIndex="56" runat="server" Width="179px" SkinID="SmallDropDown">
                    </asp:DropDownList>


            </td>
            <td valign="middle" align="right">
                <asp:Label ID="lblDailyRateBasedOn" runat="server">DAILY_RATE_BASED_ON</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:DropDownList ID="ddlDailyRateBasedOn" TabIndex="56" runat="server" Width="179px" SkinID="SmallDropDown">
                    </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="lblFutureDateAllowFor" runat="server">FUTURE_DATE_ALLOW_FOR</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:DropDownList ID="cboFutureDateAllowFor" TabIndex="57" runat="server" Width="179px" SkinID="SmallDropDown">
                    </asp:DropDownList>
            </td>
            <td valign="middle" align="right">
                <asp:Label ID="lblAllowBillingAfterCancellation" runat="server">ALLOW_BILLING_AFTER_CANCELLATION</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:DropDownList ID="ddlAllowBillingAfterCancellation" TabIndex="171" runat="server" Width="179px" SkinID="SmallDropDown">
                    </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" class="borderLeft">&nbsp;
                 <asp:Label ID="lblProducer" runat="server">PRODUCER</asp:Label>
           </td>
           <td>&nbsp;
                  <asp:DropDownList ID="ddlProducer" TabIndex="57" runat="server" Width="179px" SkinID="SmallDropDown">
                  </asp:DropDownList>            
            </td>
            <td valign="middle" align="right" nowrap="nowrap">
                <asp:Label ID="lblAllowCollectionAfterCancellation" runat="server">ALLOW_COLLECTION_AFTER_CANCELLATION</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:DropDownList ID="ddlAllowCollectionAfterCancellation" TabIndex="171" runat="server" Width="179px" SkinID="SmallDropDown">
                    </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="lblRdoName" runat="server">RDO_NAME</asp:Label>
            </td>

            <td>&nbsp
                    <asp:TextBox ID="txtRdoName" runat="server" SkinID="MediumTextBox" Width="178px"></asp:TextBox>
            </td>

            <td valign="middle" align="right" nowrap="nowrap">
                <asp:Label ID="lblPaymentProcessingTypeId" runat="server">PAYMENT_PROCESSING_TYPE_ID</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:DropDownList ID="ddlPaymentProcessingTypeId" runat="server" AutoPostBack="true" SkinID="SmallDropDown">
                    </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td align="right" class="borderLeft">&nbsp;
                    <asp:Label ID="lblRdoPercent" runat="server">RDO_PERCENT</asp:Label>
            </td>

            <td>&nbsp
                     <asp:TextBox ID="txtRdoPercent" runat="server" SkinID="MediumTextBox" Width="178px"></asp:TextBox>
            </td>

            <td align="right"><%--class="borderLeft">&nbsp;--%>
                <asp:Label ID="lblThirdPartyName" runat="server" Visible="False">THIRD_PARTY_NAME</asp:Label>
            </td>
            <td>&nbsp
                    <asp:TextBox ID="txtThirdPartyName" runat="server" SkinID="MediumTextBox" Visible="False" Width="178px"></asp:TextBox>
            </td>
        </tr>

        <tr>
            <td align="right" class="borderLeft">&nbsp;
                   <asp:Label ID="lblRdoTaxId" runat="server">RDO_TAX_ID</asp:Label>
            </td>

            <td>&nbsp
                   <asp:TextBox ID="txtRdoTaxId" runat="server" SkinID="MediumTextBox" Width="178px"></asp:TextBox>
            </td>

            <td align="right">
                <asp:Label ID="lblThirdPartyTaxId" runat="server" Visible="False">THIRD_PARTY_TAX_ID</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:TextBox ID="txtThirdPartyTaxId" runat="server" SkinID="MediumTextBox" Visible="False" Width="178px"></asp:TextBox>
            </td>
        </tr>
    </table>

    <table id="Table3" cellspacing="0" cellpadding="0" width="100%" border="0" class="formGrid" style="padding: 0px; margin: 0px">
        <tr>
            <td style="height: 19px" align="right" width="16.66%" class="borderLeft">
                <asp:Label ID="LabelLossCostPercent" runat="server">Loss_Cost_Percent</asp:Label>
            </td>
            <td style="height: 19px" width="16.66%">&nbsp;
                    <asp:TextBox ID="TextboxLossCostPercent" TabIndex="270" runat="server" Width="60px" SkinID="MediumTextBox"></asp:TextBox>&nbsp;
                <asp:DropDownList ID="cboLossCostPercentSourceXcd" TabIndex="290" runat="server" Width="100px" SkinID="SmallDropDown" Visible="False" onchange="javascript:DisableLossTextForDiffSelect();">
                </asp:DropDownList>
            </td>
            <td style="height: 19px" align="right" width="16.66%">
                <asp:Label ID="LabelProfitExpense" runat="server">PROFIT_PERCENT</asp:Label>
            </td>
            <td style="height: 19px" width="16.66%">&nbsp;
                    <asp:TextBox ID="TextboxProfitExpense" TabIndex="271" runat="server" Width="60px" SkinID="MediumTextBox"></asp:TextBox>&nbsp;
                <asp:DropDownList ID="cboProfitExpenseSourceXcd" TabIndex="292" runat="server" Width="100px" SkinID="SmallDropDown" Visible="False" onchange="javascript:DisableProfitTextForDiffSelect();">
                </asp:DropDownList>
            </td>
            <td style="height: 19px" align="right" width="16.66%">
                <asp:Label ID="LabelAdminExpense" runat="server">ADMINISTRATIVE_EXPENSE</asp:Label>
            </td>
            <td style="height: 19px" width="16.66%">&nbsp;
                    <asp:TextBox ID="TextboxAdminExpense" TabIndex="272" runat="server" Width="60px" SkinID="MediumTextBox"></asp:TextBox>&nbsp;
                <asp:DropDownList ID="cboAdminExpenseSourceXcd" TabIndex="293" runat="server" Width="100px" SkinID="SmallDropDown" Visible="False" onchange="javascript:DisableAdminTextForDiffSelect();">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" class="borderLeft">
                <asp:Label ID="LabelMarketingExpense" runat="server">MARKETING_PERCENT</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:TextBox ID="TextboxMarketingExpense" TabIndex="273" runat="server" Width="60px" SkinID="MediumTextBox"></asp:TextBox>&nbsp;
                <asp:DropDownList ID="cboMarketingExpenseSourceXcd" TabIndex="294" runat="server" Width="100px" SkinID="SmallDropDown" Visible="False" onchange="javascript:DisableMarketingTextForDiffSelect();">
                </asp:DropDownList>
            </td>
            <td align="right">
                <asp:Label ID="LabelCommPercent" runat="server">Comm. Percent</asp:Label>
            </td>
            <td>&nbsp;
                    <asp:TextBox ID="TextboxCommPercent" TabIndex="274" runat="server" Width="60px" SkinID="MediumTextBox"></asp:TextBox>&nbsp;
                <asp:DropDownList ID="cboCommPercentSourceXcd" TabIndex="291" runat="server" Width="100px" SkinID="SmallDropDown" Visible="False" onchange="javascript:DisableCommTextForDiffSelect();">
                </asp:DropDownList>
            </td>
            <td align="right"></td>
            <td></td>
        </tr>
        <tr>
            <td style="height: 19px" align="right" width="16.66%" class="borderLeft">
                <asp:Label ID="LabelRatingPlan" runat="server">RATING_PLAN</asp:Label>
            </td>
            <td style="height: 19px" width="16.66%">&nbsp;
                    <asp:TextBox ID="TextboxRatingPlan" TabIndex="275" runat="server" Width="60px" SkinID="MediumTextBox" MaxLength="4"></asp:TextBox>
            </td>
            <td style="height: 19px" align="right" width="16.66%">
                <asp:Label ID="LabelCOINSURANCE" runat="server">COINSURANCE</asp:Label>
            </td>
            <td style="height: 19px" width="16.66%">&nbsp;
                    <asp:DropDownList ID="cboCOINSURANCE" TabIndex="276" runat="server" Width="70px" SkinID="SmallDropDown" onchange="SetCodeDropDownValue();">
                    </asp:DropDownList>
            </td>
            <td style="height: 19px" align="right" width="16.66%">
                <asp:Label ID="LabelPARTICIPATION_PERCENT" runat="server">PARTICIPATION_PERCENT</asp:Label>
            </td>
            <td style="height: 19px" width="16.66%">&nbsp;
                    <asp:TextBox ID="TextboxPARTICIPATION_PERCENT" TabIndex="277" runat="server" Width="60px" SkinID="MediumTextBox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="4" class="borderLeft">
                <asp:DropDownList ID="cboCOINSURANCE_Code" TabIndex="278" runat="server" Width="64px" SkinID="SmallDropDown">
                </asp:DropDownList>
            </td>
        </tr>

        <tr>
            <td colspan="6" class="borderLeft"></td>
        </tr>
    </table>

    </asp:Content>
        <asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">

    <div class="dataContainer">
        <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0"></asp:HiddenField>
        <div id="tabs" class="style-tabs">
            <ul>
                <li><a href="#tabsReplacementPolicy" rel="noopener noreferrer">
                    <asp:Label ID="Label4" runat="server" CssClass="tabHeaderText">Replacement_Policy</asp:Label></a></li>
                <li><a href="#tabsDepreciationSchedule" rel="noopener noreferrer">
                    <asp:Label ID="Label1" runat="server" CssClass="tabHeaderText">Depreciation_Schedule</asp:Label></a></li>
            </ul>
            <div id="tabsReplacementPolicy">
                <div id="TabContainer">
                    <table id="tblReplacementPolicy" class="formGrid" border="0" rules="cols" width="98%">
                        <tr>
                            <td align="center" colspan="2">
                                <div id="scrollerReplacementPolicy" style="overflow: auto; width: 96%; height: 125px" align="center">
                                    <asp:GridView ID="moGridView" runat="server" OnRowCreated="ItemCreated" OnRowCommand="ItemCommand" AllowPaging="False" AllowSorting="false" CellPadding="1" AutoGenerateColumns="False" SkinID="DetailPageGridView" Width="100%">
                                        <SelectedRowStyle Wrap="False"></SelectedRowStyle>
                                        <EditRowStyle Wrap="False"></EditRowStyle>
                                        <AlternatingRowStyle Wrap="False"></AlternatingRowStyle>
                                        <RowStyle Wrap="False"></RowStyle>
                                        <HeaderStyle Wrap="False"></HeaderStyle>
                                        <Columns>
                                            <asp:TemplateField Visible="True" HeaderText="Product_CODE" ItemStyle-Width="20%">
                                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblProductCode" Text='<%# DataBinder.Eval(Container.DataItem, "ProductCode")%>' runat="server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlProductCode" runat="server" Visible="true" Width="220px"></asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="True" HeaderText="COVERAGE_TYPE" ItemStyle-Width="20%">
                                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCoverageType" Text='<%# DataBinder.Eval(Container.DataItem, "CoverageTypeDescription")%>' runat="server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlCoverageTYPE" runat="server" Visible="true" Width="220px"></asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="True" HeaderText="CERTIFICATE_DURATION" ItemStyle-Width="20%">
                                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblCertDuration" Text='<%# DataBinder.Eval(Container.DataItem, "CertDuration")%>' runat="server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="ddlCertDuration" runat="server" Visible="true" Width="150px"></asp:DropDownList>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField Visible="True" HeaderText="REPLACEMENT_POLICY_CLAIM_COUNT" ItemStyle-Width="20%">
                                                <ItemStyle HorizontalAlign="center"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="lblClaimCount" Text='<%# DataBinder.Eval(Container.DataItem, "ReplacementPolicyClaimCount")%>' runat="server">
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:TextBox ID="txtClaimCount" runat="server" Visible="True" Width="75" MaxLength="2"></asp:TextBox>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="30px" Wrap="False"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:ImageButton ID="EditButton_WRITE" Style="cursor: hand" runat="server" ImageUrl="../Navigation/images/edit.png" CommandName="EditRecord" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id") %>'></asp:ImageButton>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:LinkButton ID="BtnCancel" runat="server" CommandName="CancelRecord" CommandArgument="<%#Container.DisplayIndex %>" Text="Cancel"></asp:LinkButton>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField>
                                                <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="30px" Wrap="False"></HeaderStyle>
                                                <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:ImageButton Style="cursor: hand;" ID="DeleteButton_WRITE" ImageUrl="../Navigation/images/icon_delete.png" runat="server" CommandName="DeleteRecord" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id") %>'></asp:ImageButton>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:Button ID="BtnSave_WRITE" runat="server" CommandName="SaveRecord" CommandArgument="<%#Container.DisplayIndex %>" Text="Save" SkinID="PrimaryRightButton"></asp:Button>
                                                </EditItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                        <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
                                    </asp:GridView>
                                </div>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2" width="96%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Button ID="BtnNewReplacementPolicy_WRITE" runat="server" SkinID="AlternateLeftButton" Text="New"></asp:Button>&nbsp;                            
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div id="tabsDepreciationSchedule">
                <table id="tblDepreciationSchedule" class="dataGrid" border="0" rules="cols" width="98%">
                    <tr>
                        <td colspan="2">
                            <uc1:ErrorController ID="ErrorControllerDS" runat="server"></uc1:ErrorController>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <div id="scrollerDepreciationSchedule" style="overflow: auto; width: 96%; height: 125px" align="center">
                                <asp:GridView ID="GridViewDepreciationSchedule" runat="server" OnRowCreated="GridViewDepreciationSchedule_RowCreated" OnRowCommand="GridViewDepreciationSchedule_RowCommand" AllowPaging="False" AllowSorting="false" PageSize="50" CellPadding="1" AutoGenerateColumns="False" SkinID="DetailPageGridView" Width="100%">
                                    <SelectedRowStyle Wrap="False"></SelectedRowStyle>
                                    <EditRowStyle Wrap="False"></EditRowStyle>
                                    <AlternatingRowStyle Wrap="False"></AlternatingRowStyle>
                                    <RowStyle Wrap="False"></RowStyle>
                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                    <Columns>
                                        <asp:TemplateField Visible="True" HeaderText="DEPRECIATION_SCHEDULE">
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDepreciationSchedule" Text='<%# DataBinder.Eval(Container.DataItem, "DepreciationScheduleCode")%>' runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlDepreciationSchedule" runat="server" Visible="true" Width="220px"></asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="True" HeaderText="EFFECTIVE_DATE">
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblEffectiveDate" runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtEffectiveDate" runat="server" Visible="True" SkinID="SmallTextBox"></asp:TextBox>
                                                <asp:ImageButton ID="btnEffectiveDate" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png"></asp:ImageButton>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="True" HeaderText="EXPIRATION_DATE">
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblExpirationDate" runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtExpirationDate" runat="server" Visible="True" SkinID="SmallTextBox"></asp:TextBox>
                                                <asp:ImageButton ID="btnExpirationDate" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png"></asp:ImageButton>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="True" HeaderText="DEPRECIATION_SCHEDULE_USAGE">
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblDepreciationScheduleUsage" Text='<%# DataBinder.Eval(Container.DataItem, "DepreciationScheduleUsage")%>' runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="ddlDepreciationScheduleUsage" runat="server" Visible="true" Width="220px"></asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="30px">
                                            <HeaderStyle ForeColor="#12135B" Wrap="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="EditButton_WRITE" Style="cursor: hand" runat="server" ImageUrl="~/Navigation/images/icons/edit2.gif" CommandName="EditRecord" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id") %>'></asp:ImageButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="BtnCancel" runat="server" CommandName="CancelRecord" CommandArgument="<%#Container.DisplayIndex %>" Text="Cancel"></asp:LinkButton>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="30px">
                                            <HeaderStyle ForeColor="#12135B" Wrap="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:ImageButton Style="cursor: hand;" ID="DeleteButton_WRITE" ImageUrl="~/Navigation/images/icons/trash.gif" runat="server" CommandName="DeleteRecord" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "id") %>'></asp:ImageButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Button ID="BtnSave_WRITE" runat="server" CommandName="SaveRecord" CommandArgument="<%#Container.DisplayIndex %>" Text="Save" SkinID="PrimaryRightButton"></asp:Button>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" width="96%">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                <asp:Button ID="BtnNewDepreciationSchedule" runat="server" SkinID="AlternateLeftButton" Text="New"></asp:Button>&nbsp;                            
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>

    <div class="btnZone">
        <asp:Button ID="btnSave_WRITE" runat="server" SkinID="PrimaryRightButton" Text="Save"></asp:Button>

        <asp:Button ID="btnUndo_Write" runat="server" SkinID="AlternateRightButton" Text="Undo"></asp:Button>

        <asp:Button ID="btnBack" runat="server" SkinID="AlternateLeftButton" Text="Back"></asp:Button>
        <asp:Button ID="btnCopy_WRITE" runat="server" SkinID="AlternateLeftButton" Text="NEW_WITH_COPY" CausesValidation="False"></asp:Button>
        <asp:Button ID="btnNew_WRITE" runat="server" SkinID="AlternateLeftButton" Text="New"></asp:Button>
        <asp:Button ID="btnDelete_WRITE" runat="server" SkinID="CenterButton" Text="Delete"></asp:Button>
        <asp:Button ID="btnTNC" runat="server" SkinID="AlternateLeftButton" Text="TERMANDCONDITION"></asp:Button>
    </div>
    <input id="HiddenSaveChangesPromptResponse" type="hidden" runat="server" designtimedragdrop="261">
    <input id="HiddenPARTICIPATION_PERCENTAG" type="hidden" runat="server" designtimedragdrop="261">
    <input id="HiddenCertificateAuoNumGenConfirmation" type="hidden" runat="server" designtimedragdrop="261">

    <script type="text/javascript" language="javascript">

        function DisableLossTextForDiffSelect() {
            var dList = document.getElementById('<%=cboLossCostPercentSourceXcd.ClientID%>');
            var tBox = document.getElementById('<%=TextboxLossCostPercent.ClientID%>');
            var selectedValue = dList.options[dList.selectedIndex].value;

            if (selectedValue == 'ACCTBUCKETSOURCE-D') {
                tBox.value = '0.0000';
                tBox.disabled = true;
            }
            else {
                tBox.disabled = false;
            }
        }

        function DisableProfitTextForDiffSelect() {
            var dList = document.getElementById('<%=cboProfitExpenseSourceXcd.ClientID%>');
            var tBox = document.getElementById('<%=TextboxProfitExpense.ClientID%>');
            var selectedValue = dList.options[dList.selectedIndex].value;

            if (selectedValue == 'ACCTBUCKETSOURCE-D') {
                tBox.value = '0.0000';
                tBox.disabled = true;
            }
            else {
                tBox.disabled = false;
            }
        }

        function DisableAdminTextForDiffSelect() {
            var dList = document.getElementById('<%=cboAdminExpenseSourceXcd.ClientID%>');
            var tBox = document.getElementById('<%=TextboxAdminExpense.ClientID%>');
            var selectedValue = dList.options[dList.selectedIndex].value;

            if (selectedValue == 'ACCTBUCKETSOURCE-D') {
                tBox.value = '0.0000';
                tBox.disabled = true;
            }
            else {
                tBox.disabled = false;
            }
        }

        function DisableMarketingTextForDiffSelect() {
            var dList = document.getElementById('<%=cboMarketingExpenseSourceXcd.ClientID%>');
            var tBox = document.getElementById('<%=TextboxMarketingExpense.ClientID%>');
            var selectedValue = dList.options[dList.selectedIndex].value;

            if (selectedValue == 'ACCTBUCKETSOURCE-D') {
                tBox.value = '0.0000';
                tBox.disabled = true;
            }
            else {
                tBox.disabled = false;
            }
        }

        function DisableCommTextForDiffSelect() {
            var dList = document.getElementById('<%=cboCommPercentSourceXcd.ClientID%>');
            var tBox = document.getElementById('<%=TextboxCommPercent.ClientID%>');
            var selectedValue = dList.options[dList.selectedIndex].value;

            if (selectedValue == 'ACCTBUCKETSOURCE-D') {
                tBox.value = '0.0000';
                tBox.disabled = true;
            }
            else {
                tBox.disabled = false;
            }
        }

        //Hide field
        if (document.getElementById('<%=cboCOINSURANCE_Code.ClientID%>')) {
            document.getElementById('<%=cboCOINSURANCE_Code.ClientID%>').style.display = 'none';
        }

        function SetCodeDropDownValue() {
            var objDecDropDown = document.getElementById('<%=cboCOINSURANCE.ClientID%>');   // "Description" DropDown control 
            var objPart_Percet = document.getElementById('<%=TextboxPARTICIPATION_PERCENT.ClientID%>');
            var objCodeDropDown = document.getElementById('<%=cboCOINSURANCE_Code.ClientID%>'); // "Code" DropDown control 

            //Select Code drop down
            objCodeDropDown.value = objDecDropDown.options[objDecDropDown.selectedIndex].value;

            if (objCodeDropDown.options[objCodeDropDown.selectedIndex].text == '3') {
                objPart_Percet.value = setCultureFormat(100.00);
                document.getElementById('<%=HiddenPARTICIPATION_PERCENTAG.ClientID%>').value = setCultureFormat(100.00);
                objPart_Percet.disabled = true;
            }
            else {
                objPart_Percet.disabled = false;
                document.getElementById('<%=HiddenPARTICIPATION_PERCENTAG.ClientID%>').value = "";
            }
        }


        function showExtendCovField() {
            var objcboExtendCoverage = document.getElementById('<%=cboExtendCoverage.ClientID%>');
            if (objcboExtendCoverage.options[objcboExtendCoverage.selectedIndex].value == "<% =GetExtCovebyPaymentId() %>") {
                document.getElementById('<%=lblExtendCoverageBy.ClientID%>').style.display = "";
                document.getElementById('<%=lblExtendCoverageByExtraMonths.ClientID%>').style.display = "";
                document.getElementById('<%=lblExtendCoverageByExtraDays.ClientID%>').style.display = "";
                document.getElementById('<%=txtExtendCoverageByExtraMonths.ClientID%>').style.display = "";
                document.getElementById('<%=txtExtendCoverageByExtraDays.ClientID%>').style.display = "";
            } else {
                document.getElementById('<%=lblExtendCoverageBy.ClientID%>').style.display = "none";;
                document.getElementById('<%=lblExtendCoverageByExtraMonths.ClientID%>').style.display = "none";
                document.getElementById('<%=lblExtendCoverageByExtraDays.ClientID%>').style.display = "none";
                document.getElementById('<%=txtExtendCoverageByExtraMonths.ClientID%>').style.display = "none";
                document.getElementById('<%=txtExtendCoverageByExtraDays.ClientID%>').style.display = "none";
            }
        }

        if (document.getElementById('<%=cboExtendCoverage.ClientID%>')) {
            showExtendCovField();
        }

        function showProrateRateBasedOn() {
            var objcbo = document.getElementById('<%=cboProRataMethodId.ClientID%>');
            //GetListItemIDFromCode
            if (objcbo.options[objcbo.selectedIndex].value == '<% =GetListItemIDFromCode("PRMETHOD", "NPR") %>' ||
                objcbo.options[objcbo.selectedIndex].value == '<% =Guid.Empty %>') {
                document.getElementById('<%=ddlDailyRateBasedOn.ClientID%>').style.display = "none";
                document.getElementById('<%=lblDailyRateBasedOn.ClientID%>').style.display = "none";
            } else {
                document.getElementById('<%=ddlDailyRateBasedOn.ClientID%>').style.display = "";
                document.getElementById('<%=lblDailyRateBasedOn.ClientID%>').style.display = "";
            }
        }

        if (document.getElementById('<%=cboProRataMethodId.ClientID%>')) {
            showProrateRateBasedOn();
        }

        function StringTrim(str) {
            s = str.replace(/^(\s)*/, '');
            s = s.replace(/(\s)*$/, '');
            return s;
        }

        function showRepPolicyClaimCnt() {
            var objcbo = document.getElementById('<%=cboReplacementPolicy.ClientID%>');
            var objtxt = document.getElementById('<%=txtReplacementPolicyCliamCount.ClientID%>');
            var objlbl = document.getElementById('<%=lblReplacementPolicyClaimCount.ClientID%>');

            if (objcbo.options[objcbo.selectedIndex].value == '<% =GetListItemIDFromCode("REPP", "CNCL") %>' ||
                objcbo.options[objcbo.selectedIndex].value == '<% =GetListItemIDFromCode("REPP", "CNCLAF") %>') {
                objtxt.style.display = "";
                document.getElementById('<%=lblReplacementPolicyClaimCount.ClientID%>').style.display = "";
                var strTxt = StringTrim(objtxt.value);
                //alert("ClaimCount" + strTxt + "end");
                if (strTxt == "") {
                    objtxt.value = "1";
                }
                document.getElementById("TabContainer").style.display = "";
            } else {
                objtxt.style.display = "none";
                objlbl.style.display = "none";
                objtxt.value = "1";
                document.getElementById("TabContainer").style.display = "none";
            }
        }

        if (document.getElementById('<%=cboReplacementPolicy.ClientID%>')) {
            showRepPolicyClaimCnt();
        }

    </script>
</asp:Content>
            document.getElementById('<%=lblExtendCoverageByExtraDays.ClientID%>').style.display = "";
                document.getElementById('<%=txtExtendCoverageByExtraMonths.ClientID%>').style.display = "";
                document.getElementById('<%=txtExtendCoverageByExtraDays.ClientID%>').style.display = "";
            } else {
                document.getElementById('<%=lblExtendCoverageBy.ClientID%>').style.display = "none";;
                document.getElementById('<%=lblExtendCoverageByExtraMonths.ClientID%>').style.display = "none";
                document.getElementById('<%=lblExtendCoverageByExtraDays.ClientID%>').style.display = "none";
                document.getElementById('<%=txtExtendCoverageByExtraMonths.ClientID%>').style.display = "none";
                document.getElementById('<%=txtExtendCoverageByExtraDays.ClientID%>').style.display = "none";
            }
        }

        if (document.getElementById('<%=cboExtendCoverage.ClientID%>')) {
            showExtendCovField();
        }

        function showProrateRateBasedOn() {
            var objcbo = document.getElementById('<%=cboProRataMethodId.ClientID%>');
            //GetListItemIDFromCode
            if (objcbo.options[objcbo.selectedIndex].value == '<% =GetListItemIDFromCode("PRMETHOD", "NPR") %>' ||
                objcbo.options[objcbo.selectedIndex].value == '<% =Guid.Empty %>') {
                document.getElementById('<%=ddlDailyRateBasedOn.ClientID%>').style.display = "none";
                document.getElementById('<%=lblDailyRateBasedOn.ClientID%>').style.display = "none";
            } else {
                document.getElementById('<%=ddlDailyRateBasedOn.ClientID%>').style.display = "";
                document.getElementById('<%=lblDailyRateBasedOn.ClientID%>').style.display = "";
            }
        }

        if (document.getElementById('<%=cboProRataMethodId.ClientID%>')) {
            showProrateRateBasedOn();
        }

        function StringTrim(str) {
            s = str.replace(/^(\s)*/, '');
            s = s.replace(/(\s)*$/, '');
            return s;
        }

        function showRepPolicyClaimCnt() {
            var objcbo = document.getElementById('<%=cboReplacementPolicy.ClientID%>');
            var objtxt = document.getElementById('<%=txtReplacementPolicyCliamCount.ClientID%>');
            var objlbl = document.getElementById('<%=lblReplacementPolicyClaimCount.ClientID%>');

            if (objcbo.options[objcbo.selectedIndex].value == '<% =GetListItemIDFromCode("REPP", "CNCL") %>' ||
                objcbo.options[objcbo.selectedIndex].value == '<% =GetListItemIDFromCode("REPP", "CNCLAF") %>') {
                objtxt.style.display = "";
                document.getElementById('<%=lblReplacementPolicyClaimCount.ClientID%>').style.display = "";
                var strTxt = StringTrim(objtxt.value);
                //alert("ClaimCount" + strTxt + "end");
                if (strTxt == "") {
                    objtxt.value = "1";
                }
                document.getElementById("TabContainer").style.display = "";
            } else {
                objtxt.style.display = "none";
                objlbl.style.display = "none";
                objtxt.value = "1";
                document.getElementById("TabContainer").style.display = "none";
            }
        }

        if (document.getElementById('<%=cboReplacementPolicy.ClientID%>')) {
            showRepPolicyClaimCnt();
        }

    </script>
</asp:Content>
