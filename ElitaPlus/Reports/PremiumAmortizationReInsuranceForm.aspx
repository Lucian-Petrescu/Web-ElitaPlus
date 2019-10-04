<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PremiumAmortizationReInsuranceForm.aspx.vb"
    MasterPageFile="~/Reports/content_Report.Master" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.PremiumAmortizationReInsuranceForm" %>


<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js" type="text/javascript"></script>
    <table cellpadding="2" cellspacing="2" border="0" style="vertical-align: top; width: 100%;
        text-align: center;">
        <tr>
            <td align="left" width="30%">
            </td>
            <td align="left" colspan="3">
                &nbsp;*
                <asp:Label ID="MonthYearLabel" runat="server">SELECT MONTH AND YEAR</asp:Label>
                <asp:DropDownList ID="MonthDropDownList" runat="server" Height="16px" Width="126px"
                    AutoPostBack="false">
                </asp:DropDownList>
                <asp:DropDownList ID="YearDropDownList" runat="server" AutoPostBack="false">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="4">
                <hr />
            </td>
        </tr>
        <tr>
            <td align="left" width="30%">
            </td>
            <td style="width: 100%; height: 0.01%" valign="top" nowrap align="left" colspan="3"
                rowspan="1">
                <uc1:MultipleColumnDDLabelControl ID="moUserCompanyMultipleDrop" runat="server">
                </uc1:MultipleColumnDDLabelControl>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="4">
                <hr />
            </td>
        </tr>
        <tr>
            <td align="left" width="30%">
            </td>
            <td style="white-space:nowrap;" valign="middle" align="left">
                *
                <asp:RadioButton ID="rdealer" onclick=" document.all.item('ctl00_ContentPanelMainContentBody_moUserDealerMultipleDrop_lb_DropDown').style.color = ''; ToggleExt(this, arrDealerCtr); fncEnable(2); "
                    AutoPostBack="false" type="radio" GroupName="Dealer" Text="SELECT_ALL_DEALERS"
                    TextAlign="left" runat="server" Checked="False"></asp:RadioButton>
            </td>
            <td align="left" colspan="2">
                 &nbsp;&nbsp;&nbsp;<uc1:MultipleColumnDDLabelControl ID="moUserDealerMultipleDrop" runat="server"></uc1:MultipleColumnDDLabelControl>
            </td>
        </tr>
        <tr>
            <td align="left" width="30%">
            </td>
            
        </tr>
        <tr>
            <td align="left" width="30%">
            </td>
            <td align="left" width="90%" colspan="3">
            </td>
        </tr>
        <tr runat="server" id="trOnlyDealersWith">
            <td align="right" colspan="1" style="width: 30%">
            </td>
            <td style="white-space: nowrap;">
                <asp:Label ID="moProductLabel" runat="server">Or only Dealers With</asp:Label>:
            </td>
            <td align="left" colspan="2" style="width: 1100px;">
                &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlDealerCurrency" runat="server" AutoPostBack="false"
                    onchange="javascript:return ToggleDropdownsforCurrency();" Width="212px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr runat="server" id="trHrAfterCurrencyRow">
            <td align="right" colspan="4">
                <hr />
            </td>
        </tr>
        <tr>
            <td align="left" width="30%">
            </td>
            <td align="left" colspan="3">
                &nbsp;<asp:RadioButton ID="rGroup" onclick="ToggleExt(this, arrDealerGroupCtr); fncEnable(1);"
                    AutoPostBack="false" type="radio" GroupName="Dealer" Text="SELECT_ALL_GROUPS"
                    TextAlign="left" runat="server" Checked="False"></asp:RadioButton>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="GroupLabel" runat="server">OR_A_SINGLE_GROUP</asp:Label>
                <asp:DropDownList ID="cboDealerGroup" runat="server" type="DropDown" onchange="ToggleExt(this, arrDealerGroupCtr); fncEnable(1);  document.all.item('ctl00_ContentPanelMainContentBody_moUserDealerMultipleDrop_lb_DropDown').style.color = '';">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="4">
                <hr />
            </td>
        </tr>
        <tr runat="server" id="trCurrency">
            <td align="right" colspan="1" style="width: 30%">
            </td>
            <td style="white-space: nowrap; text-align:right">
                <asp:Label ID="lblCurrency" runat="server">Currency</asp:Label>:
            </td>
            <td align="left" colspan="2" >
                &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlCurrency" runat="server" AutoPostBack="false"
                    Width="212px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr runat="server" id="trTransactionDay">
            <td align="right" colspan="1" style="width: 30%">
            </td>
            <td style="white-space: nowrap; text-align:right">
                <asp:Label ID="lblExchangeRateDate" runat="server">Exchange_Rate_Date</asp:Label>:
            </td>
            <td colspan="2">
               &nbsp;&nbsp;&nbsp; <asp:RadioButtonList ID="rblAccountingPeriod" runat="server" AppendDataBoundItems="false"
                    RepeatDirection="Horizontal">
                    <asp:ListItem Text="LAST_DAY_OF_THE_MONTH" Value="EOM" Selected="True"></asp:ListItem>
                    <asp:ListItem Text="LAST_DAY_OF_THE_ACCOUNTING_PERIOD" Value="EOP"></asp:ListItem>
                </asp:RadioButtonList>
            </td>
        </tr>
        
        <tr>
            <td align="right" colspan="4">
                <hr />
            </td>
        </tr>
        <tr>
            <td align="left" width="30%">
            </td>
            <td align="left" colspan="3">
                <asp:RadioButton ID="RadiobuttonTotalsOnly" GroupName="Detail" AutoPostBack="false"
                    Text="SHOW TOTALS ONLY" TextAlign="left" runat="server"></asp:RadioButton>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:RadioButton ID="RadiobuttonDetail" GroupName="Detail" AutoPostBack="false" Text="OR SHOW DETAIL WITH TOTALS"
                    TextAlign="left" runat="server"></asp:RadioButton>
            </td>
        </tr>
        <tr>
            <td align="left" width="30%">
            </td>
            <td style="height: 22px" colspan="3">
            </td>
        </tr>
        <tr runat="server" id="trCoverage">
            <td align="left" width="30%">
            </td>
            <td nowrap align="left" width="25%" colspan="3">
                <asp:Label ID="lblTotalsByCov" runat="server">SHOW_TOTALS_PAGE_BY_COVERAGE</asp:Label>
                <asp:CheckBox ID="chkTotalsPageByCov" runat="server" TextAlign="Left" AutoPostBack="false">
                </asp:CheckBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <input type="hidden" id="hidden_dacvalue" runat="server" name="hidden_dacvalue" />
                <input type="hidden" id="hdnQuerystring" runat="server" />
            </td>
        </tr>
    </table>
    <script language="JavaScript" type="text/javascript">
        //var arrDealerGroupCtr = [[['cboDealer'],['cboDealerCode']],['rdealer'],['rGroup'],['cboDealerGroup']]
        var arrDealerGroupCtr = [[['ctl00_ContentPanelMainContentBody_moUserDealerMultipleDrop_moMultipleColumnDropDesc'], ['ctl00_ContentPanelMainContentBody_moUserDealerMultipleDrop_moMultipleColumnDrop']], ['ctl00_ContentPanelMainContentBody_rdealer'], ['ctl00_ContentPanelMainContentBody_rGroup'], ['ctl00_ContentPanelMainContentBody_cboDealerGroup'], ['ctl00_ContentPanelMainContentBody_ddlDealerCurrency']]
        var arrDealerCtr = [[['ctl00_ContentPanelMainContentBody_moUserDealerMultipleDrop_moMultipleColumnDropDesc'], ['ctl00_ContentPanelMainContentBody_moUserDealerMultipleDrop_moMultipleColumnDrop']], ['ctl00_ContentPanelMainContentBody_rdealer'], ['ctl00_ContentPanelMainContentBody_rGroup'], ['ctl00_ContentPanelMainContentBody_cboDealerGroup'], ['ctl00_ContentPanelMainContentBody_ddlDealerCurrency']]

        function fncEnable(chknum) {
            if (chknum == 1)  // for Dealer Group
            {
                //debugger;
                if (document.getElementById('ctl00_ContentPanelMainContentBody_hidden_dacvalue').value != "NONE") {
                    document.getElementById('ctl00_ContentPanelMainContentBody_chkTotalsPageByCov').disabled = false;
                    document.getElementById('ctl00_ContentPanelMainContentBody_chkTotalsPageByCov').parentElement.removeAttribute('disabled');
                    document.getElementById('ctl00_ContentPanelMainContentBody_lblTotalsByCov').disabled = false;
                    return true;
                }
                else {
                    return false;
                }
            }
            else    // for Dealer
            {
                //debugger;
                if (document.getElementById('ctl00_ContentPanelMainContentBody_hidden_dacvalue').value != "NONE") {
                    document.getElementById('ctl00_ContentPanelMainContentBody_chkTotalsPageByCov').parentElement.setAttribute('disabled', 'true');
                    document.getElementById('ctl00_ContentPanelMainContentBody_chkTotalsPageByCov').checked = false;
                    document.getElementById('ctl00_ContentPanelMainContentBody_lblTotalsByCov').disabled = true;
                    return true;
                }
                else {
                    return false;
                }
            }
        }

        function ToggleDropdownsforCurrency() {
            var curdropdownValue = document.getElementById("ctl00_ContentPanelMainContentBody_ddlDealerCurrency").value;

            //if only dealers with is chosen, then select all dealer and individual dealers have to be cleared
            if (curdropdownValue != '00000000-0000-0000-0000-000000000000') {
                document.getElementById("ctl00_ContentPanelMainContentBody_rdealer").checked = false;
                document.getElementById('ctl00_ContentPanelMainContentBody_rGroup').checked = false;

                if (document.getElementById('ctl00_ContentPanelMainContentBody_hidden_dacvalue').value = "ADMMKT") {
                    document.getElementById('ctl00_ContentPanelMainContentBody_chkTotalsPageByCov').parentElement.setAttribute('disabled', 'true');
                }

                document.getElementById("ctl00_ContentPanelMainContentBody_cboDealerGroup").selectedIndex = 0;
                document.getElementById('ctl00_ContentPanelMainContentBody_cboDealerGroup').selectedIndex = -1;
                document.getElementById("ctl00_ContentPanelMainContentBody_moUserDealerMultipleDrop_moMultipleColumnDrop").selectedIndex = 0;
                document.getElementById("ctl00_ContentPanelMainContentBody_moUserDealerMultipleDrop_moMultipleColumnDropDesc").selectedIndex = 0;

            }
            else {

                document.getElementById("ctl00_ContentPanelMainContentBody_rdealer").checked = true;
                document.getElementById("ctl00_ContentPanelMainContentBody_cboDealerGroup").selectedIndex = 0;
                document.getElementById("ctl00_ContentPanelMainContentBody_moUserDealerMultipleDrop_moMultipleColumnDrop").selectedIndex = 0;
                document.getElementById("ctl00_ContentPanelMainContentBody_moUserDealerMultipleDrop_moMultipleColumnDropDesc").selectedIndex = 0;

            }
        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <asp:Button ID="btnGenRpt" Style="background-image: url(../Navigation/images/viewIcon2.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Text="View" Width="100px"
        CssClass="FLATBUTTON" Height="20px" CausesValidation="False"></asp:Button>
</asp:Content>