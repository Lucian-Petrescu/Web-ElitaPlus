<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<%@ Register TagPrefix="Elita" TagName="MessageController" Src="~/Common/MessageController.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlAttrtibutes" Src="~/Common/UserControlAttrtibutes.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CoverageForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.CoverageForm" Theme="Default"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js">        function TABLE1_onclick() {

        }

    </script>
    <script type="text/javascript" src="https://code.jquery.com/jquery-1.11.3.js"></script>

    <script type="text/javascript">

        function GetData(obj, type) {

            var row = obj.parentNode.parentNode;
            var rowIndex = row.rowIndex;
            //var grid = document.getElementById('<%=moGridView.ClientID%>')

             var count = row.cells.length;

             if (count == 7) {
                 var grossAmount = row.cells[4].getElementsByTagName("input")[0].value;
                 var grossPercent = row.cells[5].getElementsByTagName("input")[0].value;

                 if (obj.id.indexOf("moGross_AmtText") > 0) {
                     if (grossAmount == "") {
                         row.cells[5].getElementsByTagName("input")[0].disabled = false;
                     }
                     else {
                         row.cells[5].getElementsByTagName("input")[0].value = 0;
                         row.cells[5].getElementsByTagName("input")[0].disabled = true;
                         document.getElementById('<%=hdnGrossAmtOrPercent.ClientID %>').value = "Percent";
                        document.getElementById('<%=hdnGrossAmtOrPercentValue.ClientID %>').value = grossAmount;

                    }
                }
                else if (obj.id.indexOf("moGross_Amount_PercentText") > 0) {
                    if (grossPercent == "") {
                        row.cells[4].getElementsByTagName("input")[0].disabled = false;
                    }
                    else {
                        row.cells[4].getElementsByTagName("input")[0].value = 0;
                        row.cells[4].getElementsByTagName("input")[0].disabled = true;
                        document.getElementById('<%=hdnGrossAmtOrPercent.ClientID %>').value = "Amount";
                        document.getElementById('<%=hdnGrossAmtOrPercentValue.ClientID %>').value = grossPercent;

                    }
                }
        }

        else if (count == 12) {
            var grossAmount = row.cells[4].getElementsByTagName("input")[0].value;
            var grossPercent = row.cells[10].getElementsByTagName("input")[0].value;

            if (obj.id.indexOf("moGross_AmtText") > 0) {
                if (grossAmount == "") {
                    row.cells[10].getElementsByTagName("input")[0].disabled = false;
                }
                else {
                    row.cells[10].getElementsByTagName("input")[0].value = 0;
                    row.cells[10].getElementsByTagName("input")[0].disabled = true;
                    document.getElementById('<%=hdnGrossAmtOrPercent.ClientID %>').value = "Percent";
                        document.getElementById('<%=hdnGrossAmtOrPercentValue.ClientID %>').value = grossAmount;

                    }
                }
                else if (obj.id.indexOf("moGross_Amount_PercentText") > 0) {
                    if (grossPercent == "") {
                        row.cells[4].getElementsByTagName("input")[0].disabled = false;
                    }
                    else {
                        row.cells[4].getElementsByTagName("input")[0].value = 0;
                        row.cells[4].getElementsByTagName("input")[0].disabled = true;
                        document.getElementById('<%=hdnGrossAmtOrPercent.ClientID %>').value = "Amount";
                        document.getElementById('<%=hdnGrossAmtOrPercentValue.ClientID %>').value = grossPercent;

                    }
                }
        }
}


    </script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
    <Elita:MessageController runat="server" ID="moMessageController" Visible="false" />
</asp:Content>
<asp:Content ID="SummaryContent" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table id="moTableOuter" border="0" align="left">
        <tr>
            <td>
                <asp:Panel ID="EditPanel" runat="server">
                    <table class="searchGrid" border="0">
                        <asp:Panel ID="moCoverageEditPanel_WRITE" runat="server" Height="98%" Width="100%">
                            <tbody>
                                <tr>
                                    <td style="width: 1px"></td>
                                    <td nowrap align="center" colspan="5">
                                        <div style="width: 93.3%;" align="center">
                                            <uc1:MultipleColumnDDLabelControl ID="multipleDropControl" runat="server"></uc1:MultipleColumnDDLabelControl>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 1px" width="1"></td>
                                    <td nowrap align="right" colspan="1">&nbsp;
                                        <asp:Label ID="moProductLabel" runat="server" Font-Bold="false">Product_Code</asp:Label>&nbsp;
                                    </td>
                                    <td colspan="1" rowspan="1" align="left">
                                        <asp:DropDownList ID="moProductDrop" TabIndex="4" runat="server" SkinID="SmallDropDown"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right" colspan="1">&nbsp;
                                        <asp:Label ID="moCoverageTypeLabel" runat="server" Font-Bold="false">Coverage_Type</asp:Label>&nbsp;
                                    </td>
                                    <td colspan="1" align="left">
                                        <asp:DropDownList ID="moCoverageTypeDrop" TabIndex="5" runat="server" SkinID="SmallDropDown"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 1px" width="1"></td>
                                    <td align="right" colspan="1">&nbsp;
                                        <asp:Label ID="moRiskLabel" runat="server" Font-Bold="false">Risk_Type</asp:Label>&nbsp;
                                    </td>
                                    <td colspan="1" align="left">
                                        <asp:DropDownList ID="moRiskDrop" TabIndex="6" runat="server" SkinID="SmallDropDown"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right" colspan="1">&nbsp;
                                        <asp:Label ID="moCertificateDurationLabel" runat="server" Font-Bold="false">Certificate_Duration</asp:Label>&nbsp;
                                    </td>
                                    <td colspan="1" align="left">
                                        <p>
                                            <asp:TextBox ID="moCertificateDurationText" TabIndex="7" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 1px" width="1"></td>
                                    <td align="right" colspan="1">&nbsp;
                                        <asp:Label ID="moCoverageDurationLabel" runat="server" Font-Bold="false">Coverage_Duration</asp:Label>&nbsp;
                                    </td>
                                    <td colspan="1" align="left">
                                        <asp:TextBox ID="moCoverageDurationText" TabIndex="8" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td align="right" colspan="1">&nbsp;
                                        <asp:Label ID="moItemNumberLabel" runat="server" Font-Bold="false">ITEM_NUMBER</asp:Label>:&nbsp;
                                    </td>
                                    <td colspan="1" align="left">
                                        <asp:TextBox ID="moItemNumberText" TabIndex="9" runat="server" SkinID="SmallTextBox"
                                            Enabled="False" ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 1px; height: 17px" width="1"></td>
                                    <td style="height: 17px" align="right" colspan="1">
                                        <asp:Label ID="Label1" runat="server" Font-Bold="false">Earning_Pattern:</asp:Label>&nbsp;
                                    </td>
                                    <td style="height: 17px" colspan="1" align="left">
                                        <asp:DropDownList ID="moEarningCodeDrop" TabIndex="10" runat="server" SkinID="SmallDropDown">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="height: 17px" align="right" colspan="1">
                                        <asp:Label ID="lblTaxType" runat="server" Font-Bold="false">TAX_TYPE</asp:Label>
                                    </td>
                                    <td style="height: 17px"  align="left" colspan="1">
                                        <asp:DropDownList ID="moTaxTypeDrop" TabIndex="10" runat="server" SkinID="SmallDropDown">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="5">
                                        <hr />
                                        <asp:Label ID="moCoverageIdLabel" runat="server" Visible="False"></asp:Label>
                                        <asp:Label ID="moIsNewCoverageLabel" runat="server" Visible="False"></asp:Label>
                                        <asp:Label ID="moIsNewRateLabel" runat="server" Visible="False"></asp:Label><input
                                            id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
                                            runat="server">
                                        <asp:Label ID="moCoverageRateIdLabel" runat="server" Visible="False"></asp:Label>
                                        <asp:Label ID="moIsNewCoverageConseqDamageLabel" runat="server" Visible="False"></asp:Label>
                                        <asp:Label ID="moCoverageConseqDamageIdLabel" runat="server" Visible="False"></asp:Label>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="width: 1px" width="1"></td>
                                    <td align="right" colspan="1">&nbsp;
                                        <asp:Label ID="moOffsetMethodLabel" runat="server" Font-Bold="false">Offset_Method</asp:Label>&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="moOffsetMethodDrop" TabIndex="11" runat="server" SkinID="SmallDropDown" AutoPostBack="false" OnChange="OffsetMethodChange();">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right" colspan="1">&nbsp;
                                        <asp:Label ID="lblMarkupDistPercent" runat="server" Font-Bold="false">Markup_Distribution</asp:Label>&nbsp;
                                    </td>
                                    <td colspan="1" align="left">
                                        <asp:TextBox ID="txtMarkupDistPercent" TabIndex="12" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td style="width: 1px" width="1"></td>
                                    <td align="right" colspan="1">&nbsp;
                                        <asp:Label ID="moOffsetLabel" runat="server" Font-Bold="false">Offset_to_Start</asp:Label>&nbsp;
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moOffsetText" TabIndex="11" runat="server" SkinID="exSmallTextBox"
                                            MaxLength="2"></asp:TextBox>
                                        <asp:Label ID="lblOffsetMonths" runat="server">MONTH(S)</asp:Label>
                                        <asp:TextBox ID="txtOffsetDays" TabIndex="40" runat="server" SkinID="exSmallTextBox"
                                            MaxLength="4"></asp:TextBox>
                                        <asp:Label ID="lblOffsetDays" runat="server">DAY(S)</asp:Label>
                                    </td>
                                    <td align="right" colspan="1">&nbsp;
                                        <asp:Label ID="moLiabilityLabel" runat="server" Font-Bold="false">Claim_Liability_Limit</asp:Label>&nbsp;
                                    </td>
                                    <td colspan="1" align="left">
                                        <asp:TextBox ID="moLiabilityText" TabIndex="12" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 1px" width="1"></td>
                                    <td align="right" colspan="1">&nbsp;
                                        <asp:Label ID="moEffectiveLabel" runat="server" Font-Bold="false">Effective</asp:Label>&nbsp;
                                    </td>
                                    <td nowrap colspan="1" align="left">
                                        <asp:TextBox ID="moEffectiveText" TabIndex="13" runat="server" AutoPostBack="true"
                                            SkinID="SmallTextBox"></asp:TextBox>
                                        <asp:ImageButton ID="BtnEffectiveDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:ImageButton>
                                    </td>
                                    <td align="right" colspan="1">&nbsp;
                                        <asp:Label ID="moLiabilityLimitPercentLabel" runat="server" Font-Bold="false">Claim_Liability_Limit_Percent</asp:Label>&nbsp;
                                    </td>
                                    <td colspan="1" align="left">
                                        <asp:TextBox ID="moLiabilityLimitPercentText" TabIndex="12" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 1px" width="1"></td>
                                    <td></td>
                                    <td></td>
                                    <td style="height: 12px" align="right" colspan="1">&nbsp;
                                        <asp:Label ID="moClaimLimitCountLabel" runat="server" Font-Bold="false">Claim_Limit_Count</asp:Label>&nbsp;
                                    </td>
                                    <td nowrap colspan="1" align="left">
                                        <asp:TextBox ID="moClaimLimitCountText" TabIndex="15" AutoPostBack="true" runat="server"
                                            SkinID="SmallTextBox" Enabled="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 1px" width="1"></td>
                                    <td style="height: 12px" align="right" colspan="1">&nbsp;
                                        <asp:Label ID="moExpirationLabel" runat="server" Font-Bold="false">Expiration</asp:Label>&nbsp;
                                    </td>
                                    <td nowrap colspan="1" align="left">
                                        <asp:TextBox ID="moExpirationText" TabIndex="17" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                        <asp:ImageButton ID="BtnExpirationDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:ImageButton>
                                    </td>
                                    <td style="height: 12px" align="right" colspan="1">&nbsp;
                                        <asp:Label ID="moCovDeductibleLabel" runat="server" Font-Bold="false">Coverage_Deductible</asp:Label>:&nbsp;
                                    </td>
                                    <td nowrap colspan="1" align="left">
                                        <asp:TextBox ID="moCovDeductibleText" TabIndex="15" AutoPostBack="true" runat="server"
                                            SkinID="SmallTextBox" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 1px" width="1"></td>
                                    <td style="height: 17px" align="right">
                                        <asp:Label ID="moProductItemLabel" runat="server" Font-Bold="false">Product_Item</asp:Label>&nbsp;
                                    </td>
                                    <td style="height: 17px" nowrap align="left">
                                        <p>
                                            <asp:DropDownList ID="moProductItemDrop" TabIndex="18" runat="server" SkinID="SmallDropDown">
                                            </asp:DropDownList>
                                        </p>
                                    </td>
                                    <td style="height: 17px" align="right">
                                        <asp:Label ID="moDeductibleBasedOnLabel" runat="server" Font-Bold="false">Compute_Deductible_Based_On</asp:Label>&nbsp;
                                    </td>
                                    <td style="height: 17px" nowrap align="left">
                                        <p>
                                            <asp:DropDownList ID="cboDeductibleBasedOn" TabIndex="18" runat="server" AutoPostBack='true'
                                                SkinID="SmallDropDown">
                                            </asp:DropDownList>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 1px" width="1"></td>
                                    <td style="height: 17px" align="right" colspan="1">
                                        <p>
                                            <asp:Label ID="moOptionalLabel" runat="server" Font-Bold="false">Optional_Coverage</asp:Label>&nbsp;
                                        </p>
                                    </td>
                                    <td style="height: 17px" colspan="1" align="left">
                                        <p>
                                            <asp:DropDownList ID="moOptionalDrop" TabIndex="19" runat="server" SkinID="SmallDropDown">
                                            </asp:DropDownList>
                                        </p>
                                    </td>
                                    <td align="right" colspan="1">&nbsp;
                                        <asp:Label ID="moDeductibleLabel" runat="server" Font-Bold="false">Deductible</asp:Label>&nbsp;
                                    </td>
                                    <td colspan="1" align="left">
                                        <asp:TextBox ID="moDeductibleText" TabIndex="14" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 1px" width="1"></td>
                                    <td style="height: 12px" align="right" colspan="1">
                                        <asp:Label ID="moRepairDiscountPctLabel" runat="server" Font-Bold="false">REPAIR_DISCOUNT_PCT</asp:Label>&nbsp;
                                    </td>
                                    <td colspan="1" align="left">
                                        <asp:TextBox ID="moRepairDiscountPctText" TabIndex="27" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td style="height: 12px" align="right" colspan="1">
                                        <asp:Label ID="moDeductiblePercentLabel" runat="server" Font-Bold="false">Deductible_Percent</asp:Label>&nbsp;
                                    </td>
                                    <td colspan="1" align="left">
                                        <asp:TextBox ID="moDeductiblePercentText" TabIndex="16" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 1px" width="1"></td>
                                    <td style="height: 12px" align="right" colspan="1">
                                        <asp:Label ID="moReplacementDiscountPrcLabel" runat="server" Font-Bold="false">REPLACEMENT_DISCOUNT_PCT</asp:Label>&nbsp;
                                    </td>
                                    <td colspan="1" align="left">
                                        <asp:TextBox ID="moReplacementDiscountPctText" TabIndex="27" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td style="height: 12px" align="right" colspan="1">
                                        <asp:Label ID="moCoveragePricingLabel" runat="server" Font-Bold="false">Coverage_Pricing</asp:Label>:&nbsp;
                                    </td>
                                    <td colspan="1" align="left">
                                        <asp:TextBox ID="moCoveragePricingText" runat="server" SkinID="SmallTextBox" Enabled="False"
                                            ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 1px" width="1"></td>
                                    <td style="height: 12px" align="right" colspan="1">&nbsp;
                                        <asp:Label ID="moIsClaimAllowedLabel" runat="server" Font-Bold="false">IS_CLAIM_ALLOWED</asp:Label>&nbsp;
                                    </td>
                                    <td nowrap colspan="1" align="left">
                                        <asp:DropDownList ID="moIsClaimAllowedDrop" TabIndex="19" runat="server" SkinID="SmallDropDown">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="height: 17px" align="right">
                                        <div id="currLabelDiv" runat="server">
                                            <asp:Label ID="LabelCURRENCY_OF_COVERAGE" runat="server" Font-Bold="false">CURRENCY_OF_COVERAGE</asp:Label>:&nbsp;
                                        </div>
                                    </td>
                                    <td style="height: 17px" align="left">
                                        <div id="currTextBoxDiv" runat="server">
                                            <asp:TextBox ID="TextBoxCurrencyOfCoverage" runat="server" SkinID="SmallTextBox"
                                                Enabled="False" ReadOnly="True"></asp:TextBox>
                                        </div>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 1px" width="1"></td>
                                    <td style="height: 12px" align="right" colspan="1">&nbsp;
                                        <asp:Label ID="moUseCoverageStartDateLable" runat="server" Font-Bold="false">USE_COVERAGE_START_DATE</asp:Label>&nbsp;
                                    </td>
                                    <td nowrap colspan="1" align="left">
                                        <asp:DropDownList ID="UseCoverageStartDateId" TabIndex="19" runat="server" SkinID="SmallDropDown">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="height: 17px" align="right" colspan="1">
                                        <p>
                                            <asp:Label ID="moRetailLabel" runat="server" Font-Bold="false">Percent_Of_Retail</asp:Label>:&nbsp;
                                        </p>
                                    </td>
                                    <td style="height: 17px" colspan="1" align="left">
                                        <p>
                                            <asp:TextBox ID="moRetailText" TabIndex="20" runat="server" SkinID="SmallTextBox"
                                                Enabled="False" ReadOnly="True"></asp:TextBox>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 1px" width="1"></td>
                                    <td style="height: 12px" align="right" colspan="1">&nbsp;
                                        <asp:Label ID="moMethodOfRepairLabel" runat="server" Font-Bold="false">Method_of_Repair</asp:Label>&nbsp;
                                    </td>
                                    <td nowrap colspan="1" align="left">
                                        <asp:DropDownList ID="moMethodOfRepairDrop" TabIndex="19" runat="server" SkinID="SmallDropDown">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="height: 17px" align="right" colspan="1">
                                        <p>
                                            <asp:Label ID="moAgentCodeLabel" runat="server" Font-Bold="false">Agent_Code</asp:Label>&nbsp;
                                        </p>
                                    </td>
                                    <td style="height: 17px" colspan="1" align="left">
                                        <p>
                                            <asp:TextBox ID="moAgentcodeText" TabIndex="20" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 1px" width="1"></td>
                                    <td style="height: 12px" align="right" colspan="1">&nbsp;
                                        <asp:Label ID="moCoverageLiabilityLimitLabel" runat="server" Font-Bold="false">COV_LIABILITY_LIMIT</asp:Label>&nbsp;
                                    </td>
                                    <td nowrap colspan="1" align="left">
                                        <asp:TextBox ID="moCoverageLiabilityLimitText" TabIndex="12" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td style="height: 17px" align="right" colspan="1">
                                        <p>
                                            <asp:Label ID="moCoverageLiabilityLimitPercentLabel" runat="server" Font-Bold="false">COV_LIABILITY_LIMIT_PERCENT</asp:Label>&nbsp;
                                        </p>
                                    </td>
                                    <td style="height: 17px" colspan="1" align="left">
                                        <p>
                                            <asp:TextBox ID="moCoverageLiabilityLimitPercentText" TabIndex="12" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                        </p>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="width: 1px" width="1"></td>
                                    <td style="height: 12px" align="right" colspan="1">&nbsp;
                                        <asp:Label ID="moRecoverDeciveLabel" runat="server" Font-Bold="false">RECOVER_DEVICE</asp:Label>&nbsp;
                                    </td>
                                    <td nowrap colspan="1" align="left">
                                        <asp:DropDownList ID="moRecoverDeciveDrop" TabIndex="19" runat="server" SkinID="med">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="height: 12px" align="right" colspan="1">&nbsp;
                                        <asp:Label ID="moReInsuredLabel" runat="server" Font-Bold="false">ReInsured</asp:Label>&nbsp;
                                    </td>
                                    <td nowrap colspan="1" align="left">
                                        <asp:DropDownList ID="moReInsuredDrop" AutoPostBack="true" runat="server" SkinID="MediumDropDown"></asp:DropDownList>
                                    </td>
                                </tr>
                                 <tr>
                                     <td style="width: 1px" width="1"></td>
                                <td style="height: 12px" align="right" colspan="1">&nbsp;
                                    <asp:Label ID="LabelDepSchCashReimbursement" runat="server">DEP_SCH_CASH_REIMBURSEMENT</asp:Label>:
                                </td>
                                <td nowrap colspan="1" align="left">
                                    <asp:DropDownList ID="ddlDepSchCashReimbursement" runat="server" SkinID="MediumDropDown"></asp:DropDownList>
                                </td>
                                <td align="right"></td>
                                <td align="left"></td>
                            </tr>
                                <tr>
                                 <td style="width: 1px" width="1"></td>
                                <td style="height: 12px" align="right" colspan="1">&nbsp;
                                    <asp:Label ID="moPerIncidentLiabilityLimitCapLabel" runat="server" Font-Bold="false">PER_INCIDENT_LIABILITY_LIMIT_CAP</asp:Label>&nbsp;
                                </td>
                                <td nowrap colspan="1" align="left">
                                    <asp:TextBox ID="moPerIncidentLiabilityLimitCapText" TabIndex="12" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            </tbody>
                        </asp:Panel>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>

    <script type="text/javascript" language="javascript">
        //Hide field

        OffsetMethodChange();

        function OffsetMethodChange() {
            var objOffsetMethod = document.getElementById('<%=moOffsetMethodDrop.ClientID%>');

            if (objOffsetMethod) {
                //alert(objOffsetMethod.options[objOffsetMethod.selectedIndex].text);

                var objOffsetMonths = document.getElementById('<%=moOffsetText.ClientID%>');
                var objOffsetDays = document.getElementById('<%=txtOffsetDays.ClientID%>');

                if (objOffsetMethod.options.length > 0 && objOffsetMethod.options[objOffsetMethod.selectedIndex].value == 'FIXED') {
                    //  objOffsetMonths.readOnly = false;
                    //objOffsetDays.readOnly = false;
                    objOffsetDays.disabled = false;
                    objOffsetMonths.disabled = false;
                }
                else {
                    objOffsetMonths.value = '0';
                    objOffsetDays.value = '0';
                    objOffsetMonths.disabled = true;
                    objOffsetDays.disabled = true;
                }
            }
        }
    </script>
</asp:Content>

<asp:Content ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
        <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0" />
        <asp:HiddenField ID="hdnDisabledTab" runat="server" />
        <div id="tabs" class="style-tabs">
            <ul>
                <li><a href="#tabsCoverageRate">
                    <asp:Label ID="Label4" runat="server" CssClass="tabHeaderText">Coverage Rate</asp:Label></a></li>
                <li><a href="#tabsDeductible">
                    <asp:Label ID="Label6" runat="server" CssClass="tabHeaderText">Deductible</asp:Label></a></li>
                <li><a href="#tabsATTRIBUTES">
                    <asp:Label ID="Label8" runat="server" CssClass="tabHeaderText">ATTRIBUTES</asp:Label></a></li>
                <li><a href="#tabsCoverageConseqDamage">
                    <asp:Label ID="Label2" runat="server" CssClass="tabHeaderText">Coverage_Conseq_Damage</asp:Label></a></li>

            </ul>

            <div id="tabsCoverageRate">
                <table id="tblOpportunities" class="dataGrid" border="0" rules="cols" width="98%">
                    <tr>
                        <td colspan="2">
                            <Elita:MessageController runat="server" ID="moMsgControllerRate" Visible="false" />
                        </td>
                    </tr>
                    <tr>
                        <td align="Center" colspan="2">
                            <div id="scroller" style="overflow: auto; width: 96%; height: 125px" align="center">
                                <asp:GridView ID="moGridView" runat="server" OnRowCreated="ItemCreated" OnRowCommand="ItemCommand"
                                    AllowPaging="False" PageSize="50" AllowSorting="True" CellPadding="1" AutoGenerateColumns="False"
                                    SkinID="DetailPageGridView">
                                    <SelectedRowStyle Wrap="False"></SelectedRowStyle>
                                    <EditRowStyle Wrap="False"></EditRowStyle>
                                    <AlternatingRowStyle Wrap="False"></AlternatingRowStyle>
                                    <RowStyle Wrap="False"></RowStyle>
                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                    <Columns>
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="EditButton_WRITE" Style="cursor: hand" runat="server" ImageUrl="~/Navigation/images/icons/edit2.gif"
                                                    CommandName="EditRecord" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:ImageButton Style="cursor: hand;" ID="DeleteButton_WRITE" ImageUrl="~/Navigation/images/icons/trash.gif"
                                                    runat="server" CommandName="DeleteRecord" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="moCOVERAGE_RATE_ID" Text='<%# GetGuidStringFromByteArray(Container.DataItem("COVERAGE_RATE_ID"))%>'
                                                    runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="True" HeaderText="Low_Price">
                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="moLowPriceLabel" Text='<%# GetAmountFormattedToVariableString(Container.DataItem("LOW_PRICE"))%>'
                                                    runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="moLowPriceText" runat="server" Visible="True" Width="75"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="True" HeaderText="High_Price">
                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="moHigh_PriceLabel" Text='<%# GetAmountFormattedToVariableString(Container.DataItem("HIGH_PRICE"))%>'
                                                    runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="moHigh_PriceText" runat="server" Visible="True" Width="75"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="True" HeaderText="GROSS_AMT_NET">
                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="moGross_AmtLabel" Text='<%# GetAmountFormattedToVariableString(Container.DataItem("GROSS_AMT"))%>'
                                                    runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="moGross_AmtText" onchange="javascript:GetData(this);" runat="server" Width="75"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="True" HeaderText="Commission_Percent">
                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="moCommission_PercentLabel" Text='<%# GetAmountFormattedDoubleString(Container.DataItem("COMMISSION_PERCENT"), "N4")%>'
                                                    runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="moCommission_PercentText" runat="server" Visible="True" Width="75"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="True" HeaderText="Marketing_Percent">
                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="moMarketing_PercentLabel" Text='<%# GetAmountFormattedDoubleString(Container.DataItem("MARKETING_PERCENT"), "N4")%>'
                                                    runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="moMarketing_PercentText" runat="server" Visible="True" Width="75"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="True" HeaderText="ADMINISTRATIVE_EXPENSE">
                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="moAdmin_ExpenseLabel" Text='<%# GetAmountFormattedDoubleString(Container.DataItem("ADMIN_EXPENSE"), "N4")%>'
                                                    runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="moAdmin_ExpenseText" runat="server" Visible="True" Width="75"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="True" HeaderText="Profit_Percent">
                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="moProfit_ExpenseLabel" Text='<%# GetAmountFormattedDoubleString(Container.DataItem("PROFIT_EXPENSE"), "N4")%>'
                                                    runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="moProfit_ExpenseText" runat="server" Visible="True" Width="75"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="True" HeaderText="Loss_Cost_Percent">
                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="moLoss_Cost_PercentLabel" Text='<%# GetAmountFormattedDoubleString(Container.DataItem("LOSS_COST_PERCENT"), "N4")%>'
                                                    runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="moLoss_Cost_PercentText" runat="server" Visible="True" Width="75"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="True" HeaderText="GROSS_AMOUNT_PERCENTAGE">
                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="moGross_Amount_PercentLabel" Text='<%# GetAmountFormattedDoubleString(Container.DataItem("GROSS_AMOUNT_PERCENT"), "N4")%>'
                                                    runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="moGross_Amount_PercentText" onchange="javascript:GetData(this);" runat="server" Width="75"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="True" HeaderText="RENEWAL_NUMBER">
                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="moRenewal_NumberLabel" Text='<%# GetAmountFormattedToString(Container.DataItem("RENEWAL_NUMBER"))%>'
                                                    runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="moRenewal_NumberText" runat="server" Visible="True" Width="75" MaxLength="3" Text="0"></asp:TextBox>
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
                            <asp:Button ID="BtnNewRate_WRITE" runat="server" SkinID="AlternateLeftButton" Text="New"></asp:Button>&nbsp;
                            <asp:Button ID="BtnSaveRate_WRITE" runat="server" SkinID="PrimaryRightButton" Text="Save"></asp:Button>&nbsp;
                            <asp:Button ID="BtnCancelRate" runat="server" SkinID="AlternateLeftButton" Text="Cancel"></asp:Button>
                        </td>
                    </tr>
                </table>
            </div>

            <div id="tabsDeductible">
                <table id="tblDeduct" border="0" class="dataGrid" rules="cols" width="98%">
                    <tr>
                        <td colspan="2">
                            <Elita:MessageController runat="server" ID="moMsgControllerDeductible" Visible="false" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <asp:Label ID="moCoverageDeductibleIdLabel" runat="server" Visible="False"></asp:Label>
                            <asp:Label ID="IsNewDeductibleLabel" runat="server" Visible="False"></asp:Label>
                            <div id="scroller1" style="overflow: auto; width: 98%; height: 125px" align="center"
                                runat="server">
                                <asp:GridView ID="dedGridView" runat="server" Width="97%" OnRowCreated="DeductibleItemCreated"
                                    OnRowCommand="DeductibleItemCommand" AllowPaging="False" PageSize="50" AllowSorting="True"
                                    CellPadding="1" AutoGenerateColumns="False" SkinID="DetailPageGridView">
                                    <SelectedRowStyle Wrap="false"></SelectedRowStyle>
                                    <EditRowStyle Wrap="false"></EditRowStyle>
                                    <AlternatingRowStyle Wrap="False"></AlternatingRowStyle>
                                    <RowStyle Wrap="false"></RowStyle>
                                    <HeaderStyle Wrap="false"></HeaderStyle>
                                    <Columns>
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="EditButton_WRITE" Style="cursor: hand" runat="server" ImageUrl="~/Navigation/images/icons/edit2.gif"
                                                    CommandName="EditRecord" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:ImageButton Style="cursor: hand;" ID="DeleteButton_WRITE" ImageUrl="~/Navigation/images/icons/trash.gif"
                                                    runat="server" CommandName="DeleteRecord" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="moCOVERAGE_DED_ID" Text='<%# GetGuidStringFromByteArray(Container.DataItem("COVERAGE_DED_ID"))%>'
                                                    runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="moMETHOD_OF_REPAIR_ID" Text='<%# GetGuidStringFromByteArray(Container.DataItem("METHOD_OF_REPAIR_ID"))%>'
                                                    runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="moDEDUCTIBLE_BASED_ON_ID" Text='<%# GetGuidStringFromByteArray(Container.DataItem("DEDUCTIBLE_BASED_ON_ID"))%>'
                                                    runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="True" HeaderText="Method_of_Repair">
                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="moMethodOfRepair_Label" Text='<%# Container.DataItem("METHOD_OF_REPAIR")%>'
                                                    runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="moddl_MethodOfRepair" runat="server" Visible="True" Width="200">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="True" HeaderText="Compute_Deductible_Based_On">
                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="moDeductibleBasedOn_Label" Text='<%# Container.DataItem("DEDUCTIBLE_BASED_ON")%>'
                                                    runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="moddl_DeductibleBasedOn" runat="server" Visible="True" Width="150" AutoPostBack='true' OnSelectedIndexChanged="moddl_DeductibleBasedOn_SelectedIndexChanged">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="True" HeaderText="Deductible">
                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="moLabelDeductible" Text='<%# GetAmountFormattedDoubleString(Container.DataItem("DEDUCTIBLE"))%>'
                                                    runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="motxt_Deductible" runat="server" Visible="True" Width="75"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnnew_Deductible" runat="server" SkinID="PrimaryRightButton" Text="New"></asp:Button>&nbsp;
                            <asp:Button ID="btnSave_Deductible" runat="server" SkinID="PrimaryRightButton" Text="Save"></asp:Button>&nbsp;
                            <asp:Button ID="btnCancel_Deductible" runat="server" SkinID="AlternateLeftButton"
                                Text="Cancel"></asp:Button>
                        </td>
                    </tr>
                </table>
            </div>

            <div id="tabsATTRIBUTES">
                <Elita:UserControlAttrtibutes runat="server" ID="AttributeValues" />
            </div>

            <div id="tabsCoverageConseqDamage">
                <table id="tblCovConseqDamage" class="dataGrid" border="0" rules="cols" width="98%">
                    <tr>
                        <td colspan="2">
                            <Elita:MessageController runat="server" ID="moMsgControllerConseqDamage" Visible="false" />
                        </td>
                    </tr>
                    <tr>
                        <td align="Center" colspan="2">
                            <div id="scrollerCovConseqDamage" style="overflow: auto; width: 96%; height: 125px" align="center">
                                <asp:GridView ID="moGridViewConseqDamage" runat="server" OnRowCommand="ConseqDamageRowCommand" 
                                    AllowPaging="False" PageSize="50" AllowSorting="True" CellPadding="1" AutoGenerateColumns="False"
                                    SkinID="DetailPageGridView">
                                    <SelectedRowStyle Wrap="False"></SelectedRowStyle>
                                    <EditRowStyle Wrap="False"></EditRowStyle>
                                    <AlternatingRowStyle Wrap="False"></AlternatingRowStyle>
                                    <RowStyle Wrap="False"></RowStyle>
                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                    <Columns>
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="EditButton_WRITE" Style="cursor: hand" runat="server" ImageUrl="~/Navigation/images/icons/edit2.gif"
                                                    CommandName="EditRecord" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:ImageButton Style="cursor: hand;" ID="DeleteButton_WRITE" ImageUrl="~/Navigation/images/icons/trash.gif"
                                                    runat="server" CommandName="DeleteRecord" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="moCoverageConseqDamageIdLabel" Text='<%# GetGuidStringFromByteArray(Container.DataItem("coverage_conseq_damage_id"))%>'
                                                    runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="True" HeaderText="CONSEQ_DAMAGE_TYPE">
                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="moConseqDamageTypeLabel" runat="server" Text='<%# Container.DataItem("conseq_damage_type_desc")%>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="moConseqDamageTypeDropdown" runat="server" Visible="True" Width="200">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="True" HeaderText="LIABILITY_LIMIT_BASED_ON">
                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="moLiabilityLimitBasedOnLabel" runat="server" Text='<%# Container.DataItem("liability_limit_base_desc")%>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="moLiabilityLimitBasedOnDropdown" runat="server" Visible="True" Width="150">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="True" HeaderText="LIABILITY_LIMIT_PER_INCIDENT">
                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="moLiabilityLimitPerIncidentLabel" Text='<%# GetAmountFormattedToVariableString(Container.DataItem("liability_limit_per_incident"))%>'
                                                    runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="moLiabilityLimitPerIncidentText" runat="server" Visible="True" Width="75"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="True" HeaderText="LIABILITY_LIMIT_CUMULATIVE">
                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="moLiabilityLimitCumulativeLabel" Text='<%# GetAmountFormattedToVariableString(Container.DataItem("LIABILITY_LIMIT_CUMULATIVE"))%>'
                                                    runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="moLiabilityLimitCumulativeText" runat="server" Visible="True" Width="75"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="True" HeaderText="Effective_Date">
                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="moConseqDamageEffectiveDateLabel" runat="server" Text='<%# Container.DataItem("effective")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="moConseqDamageEffectiveDateText" runat="server" Visible="True" SkinID="SmallTextBox" />
                                                <asp:ImageButton ID="btnConseqDamageEffectiveDate" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="True" HeaderText="Expiration_Date">
                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="moConseqDamageExpirationDateLabel" runat="server" Text='<%# Container.DataItem("expiration")%>'></asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="moConseqDamageExpirationDateText" runat="server" Visible="True" SkinID="SmallTextBox" />
                                                <asp:ImageButton ID="btnConseqDamageExpirationDate" runat="server" Visible="True" ImageUrl="~/App_Themes/Default/Images/calendar.png" />
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="True" HeaderText="FULFILMENT_METHOD">
                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="moFulfilmentMethodLabel" runat="server" Text='<%# Container.DataItem("fulfilment_method_desc")%>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:DropDownList ID="moFulfilmentMethodDropdown" runat="server" Visible="True" Width="150">
                                                </asp:DropDownList>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="moConseqDamageTypeXcdLabel" runat="server" Text='<%# Container.DataItem("conseq_damage_type_xcd")%>'>
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="moLiabilityLimitBasedOnXcdLabel" runat="server" Text='<%# Container.DataItem("liability_limit_base_xcd")%>'>
                                                </asp:Label>
                                            </ItemTemplate>

                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="false">
                                            <ItemStyle HorizontalAlign="center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="moFulfilmentMethodXcdLabel" runat="server" Text='<%# Container.DataItem("fulfilment_method_xcd")%>'>
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
                            <asp:Button ID="btnNewConseqDamage_WRITE" runat="server" SkinID="AlternateLeftButton" Text="New" />&nbsp
                            <asp:Button ID="btnSaveConseqDamage_WRITE" runat="server" SkinID="PrimaryRightButton" Text="Save"></asp:Button>&nbsp;
                            <asp:Button ID="btnCancelConseqDamage_WRITE" runat="server" SkinID="AlternateLeftButton" Text="Cancel"></asp:Button>
                        </td>
                    </tr>
                </table>
            </div>



        </div>
    </div>
    <div class="btnZone" width="70%">
        <table width="100%">
            <tr>
                <td width="60%">
                    <asp:Button ID="btnBack" runat="server" SkinID="AlternateLeftButton" Text="BACK"></asp:Button>
                    <asp:Button ID="btnNew_WRITE" runat="server" SkinID="AlternateLeftButton" Text="New"></asp:Button>
                    <asp:Button ID="btnCopy_WRITE" SkinID="AlternateLeftButton" runat="server" Text="New_With_Copy"></asp:Button>
                    <asp:Button ID="btnUndo_WRITE" SkinID="AlternateLeftButton" runat="server" Text="UNDO" />
                    <asp:Button ID="btnDelete_WRITE" runat="server" SkinID="AlternateLeftButton" Text="Delete"></asp:Button>
                </td>
                <td width="30%" align="right">
                    <asp:Button ID="btnApply_WRITE" runat="server" SkinID="PrimaryLeftButton" Text="SAVE" />
                </td>
                <td width="10%"></td>
            </tr>
        </table>
    </div>
    <asp:HiddenField ID="hdnGrossAmtOrPercent" runat="server" />
    <asp:HiddenField ID="hdnGrossAmtOrPercentValue" runat="server" />
</asp:Content>



