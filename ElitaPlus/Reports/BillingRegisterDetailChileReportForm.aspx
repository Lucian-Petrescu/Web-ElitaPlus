<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Reports/content_Report.Master"
    CodeBehind="BillingRegisterDetailChileReportForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.BillingRegisterDetailChileReportForm" %>

<%@ Register TagPrefix="uc3" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <table cellpadding="2" cellspacing="2" border="0" style="vertical-align: top;">
        <tr>
            <td class="BLANKROW" colspan="3" style="vertical-align: top;">
            </td>
        </tr>
        <tr id="trcomp" runat="server">
            <td nowrap align="right" colspan="1" style="width: 25%">
            </td>
            <td class="LABELCOLUMN" style="vertical-align: bottom;">
                *&nbsp;<asp:Label ID="lblCompany" runat="server">SELECT_COMPANY:</asp:Label>
            </td>
            <td>
                <uc3:MultipleColumnDDLabelControl ID="CompanyDropControl" runat="server"></uc3:MultipleColumnDDLabelControl>
            </td>
        </tr>
        <tr>
            <td class="BLANKROW" colspan="3">
            </td>
        </tr>
        <tr>
            <td class="BLANKROW" colspan="3">
                <hr />
            </td>
        </tr>
        <tr>
            <td nowrap align="right" colspan="1" style="width: 25%">
            </td>
            <td class="LABELCOLUMN">
                *&nbsp;<asp:Label ID="lblMonthYear" runat="server" Text="SELECT MONTH AND YEAR:"></asp:Label>
            </td>
            <td>
                &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="moMonthDropDownList" runat="server" Width="25%"
                    AutoPostBack="false">
                </asp:DropDownList>
                &nbsp;&nbsp;&nbsp;
                <asp:DropDownList ID="moYearDropDownList" runat="server" Width="15%" AutoPostBack="false">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="BLANKROW" colspan="3">
                <hr />
            </td>
        </tr>
        <tr>
            <td nowrap align="right" colspan="1" style="width: 25%">
            </td>
            <td class="LABELCOLUMN" style="vertical-align: bottom;" height="10px">
                <asp:RadioButton ID="rdealer" AutoPostBack="false" Text="SELECT_ALL_DEALERS" TextAlign="left"
                    runat="server" Checked="True"></asp:RadioButton>
            </td>
            <td>
                &nbsp;&nbsp;<uc3:MultipleColumnDDLabelControl ID="DealerDropControl" runat="server">
                </uc3:MultipleColumnDDLabelControl>
            </td>
        </tr>
        <tr>
            <td align="left" width="30%">
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
            <td align="left">
                &nbsp;&nbsp;&nbsp;<asp:DropDownList ID="ddlDealerCurrency" runat="server" AutoPostBack="false"
                    onchange="javascript:return ToggleDropdownsforCurrency();" Width="212px">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="3">
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
                document.getElementById("ctl00_ContentPanelMainContentBody_DealerDropControl_moMultipleColumnDrop").selectedIndex = 0;
                document.getElementById("ctl00_ContentPanelMainContentBody_DealerDropControl_moMultipleColumnDropDesc").selectedIndex = 0;

            }
            else {
                document.getElementById("ctl00_ContentPanelMainContentBody_rdealer").checked = true;
                document.getElementById("ctl00_ContentPanelMainContentBody_DealerDropControl_moMultipleColumnDrop").selectedIndex = 0;
                document.getElementById("ctl00_ContentPanelMainContentBody_DealerDropControl_moMultipleColumnDropDesc").selectedIndex = 0;
            }
        }
              
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <asp:Button ID="btnGenRpt" Style="background-image: url(../Navigation/images/viewIcon2.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Text="View" Width="100px"
        CssClass="FLATBUTTON" Height="20px" CausesValidation="False"></asp:Button>
</asp:Content>

