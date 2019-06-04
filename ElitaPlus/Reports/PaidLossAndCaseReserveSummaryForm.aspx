<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PaidLossAndCaseReserveSummaryForm.aspx.vb"
    MasterPageFile="~/Reports/content_Report.Master" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.PaidLossAndCaseReserveSummaryForm" %>

<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <table cellpadding="2" cellspacing="2" border="0" style="vertical-align: top; width: 100%;
        text-align: center;">
        <tr>
            <td align="right" colspan="1" style="width: 30%">
            </td>
            <td style="height: 12px" align="center" colspan="2">
                <table id="Table1" cellspacing="1" cellpadding="1" border="0">
                    <tr>
                        <td align="right" style="white-space: nowrap;">
                            <asp:Label ID="Label1" runat="server" Font-Bold="false" Width="222px">PLEASE SELECT REPORTING PERIOD</asp:Label>:&nbsp;
                        </td>
                        <td>
                            <asp:RadioButton ID="rdoMTD" runat="server" Width="87px" Text="M-T-D" GroupName="PeriodGroup"
                                Checked="True"></asp:RadioButton>
                        </td>
                        <td>
                            <asp:RadioButton ID="rdoQTD" runat="server" Width="87px" Text="Q-T-D" GroupName="PeriodGroup">
                            </asp:RadioButton>
                        </td>
                        <td>
                            <asp:RadioButton ID="rdoYTD" runat="server" Width="87px" Text="Y-T-D" GroupName="PeriodGroup">
                            </asp:RadioButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="3">
                <hr />
            </td>
        </tr>
        <tr>
            <td align="right" colspan="1" style="width: 30%">
            </td>
            <td align="left" colspan="2">
                <asp:Label ID="MonthYearLabel" runat="server">SELECT_MONTH_AND_YEAR</asp:Label>:&nbsp;
                <asp:DropDownList ID="MonthDropDownList" runat="server" Width="128px" AutoPostBack="false">
                </asp:DropDownList>
                &nbsp;
                <asp:DropDownList ID="YearDropDownList" runat="server" Width="84px" AutoPostBack="false">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="1" style="width: 30%">
            </td>
            <td align="center" colspan="2">
                <uc1:MultipleColumnDDLabelControl ID="moUserCompanyMultipleDrop" runat="server">
                </uc1:MultipleColumnDDLabelControl>
            </td>
        </tr>
        <tr runat="server" id="trSelectAllDealers">
            <td align="right" colspan="1" style="width: 30%">
            </td>
            <td class="LABELCOLUMN" style="vertical-align: bottom; white-space: nowrap;">
                <asp:RadioButton ID="rdealer" AutoPostBack="false" Text="SELECT_ALL_DEALERS" TextAlign="left"
                    runat="server" Checked="True"></asp:RadioButton>
            </td>
            <td>
                <uc1:MultipleColumnDDLabelControl ID="multipleDealerDropControl" runat="server">
                </uc1:MultipleColumnDDLabelControl>
            </td>
        </tr>
        <tr runat="server" id="trHrAfterSelectAllDealersRow">
            <td align="right" colspan="1" style="width: 30%">
            </td>
            <td align="left" width="90%" colspan="2">
            </td>
        </tr>
        <tr runat="server" id="trOnlyDealersWith">
            <td align="right" colspan="1" style="width: 30%">
            </td>
            <td style="white-space: nowrap;">
                <asp:Label ID="moProductLabel" runat="server">Or only Dealers With</asp:Label>:
            </td>
            <td align="left" colspan="3">
                &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlDealerCurrency" runat="server" AutoPostBack="false"
                    onchange="javascript:return ToggleDropdownsforCurrency();" Width="212px">
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
            <td class="LABELCOLUMN" style="vertical-align: bottom; white-space: nowrap;">
                <asp:Label ID="lblCurrency" runat="server">Currency</asp:Label>:
            </td>
            <td align="left">
                &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlCurrency" runat="server" AutoPostBack="false"
                    Width="212px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr runat="server" id="trHrAfterCurrencyRow">
            <td align="right" colspan="3">
                <hr />
            </td>
        </tr>
    </table>
    <script language="javascript" type="text/javascript">
        function ToggleDropdownsforCurrency() {
            var curdropdownValue = document.getElementById("ctl00_ContentPanelMainContentBody_ddlDealerCurrency").value;

            //if only dealers with is chosen, then select all dealer and individual dealers have to be cleared
            if (curdropdownValue != '00000000-0000-0000-0000-000000000000') {
                document.getElementById("ctl00_ContentPanelMainContentBody_rdealer").checked = false;
                document.getElementById("ctl00_ContentPanelMainContentBody_multipleDealerDropControl_moMultipleColumnDrop").selectedIndex = 0;
                document.getElementById("ctl00_ContentPanelMainContentBody_multipleDealerDropControl_moMultipleColumnDropDesc").selectedIndex = 0;

            }
            else {
                document.getElementById("ctl00_ContentPanelMainContentBody_rdealer").checked = true;
                document.getElementById("ctl00_ContentPanelMainContentBody_multipleDealerDropControl_moMultipleColumnDrop").selectedIndex = 0;
                document.getElementById("ctl00_ContentPanelMainContentBody_multipleDealerDropControl_moMultipleColumnDropDesc").selectedIndex = 0;
            }
        }

  
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <asp:Button ID="btnGenRpt" Style="background-image: url(../Navigation/images/viewIcon2.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Text="View" Width="100px"
        CssClass="FLATBUTTON" Height="20px" CausesValidation="False"></asp:Button>
</asp:Content>


















