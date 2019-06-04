<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="UPRExtractForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.UPRExtractForm" MasterPageFile="~/Reports/ElitaReportExtractBase.Master"
    Theme="Default" %>

<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl_New" Src="../Common/MultipleColumnDDLabelControl_new.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <style>
        .btnZone input.altBtn[disabled] { visibility:visible !important; display:block !important;}
    </style>  
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server"></asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js" type="text/javascript"></script>
    <div class="dataContainer">
        <div class="stepformZone">
            <table class="formGrid" border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px;" width="100%">
                <tr runat="server" id="trYearMonth">
                    <td align="left" colspan="2">&nbsp;&nbsp;*
                            <asp:Label ID="MonthYearLabel" runat="server">SELECT MONTH AND YEAR</asp:Label>
                        <asp:DropDownList ID="MonthDropDownList" runat="server" AutoPostBack="false"></asp:DropDownList>
                        <asp:DropDownList ID="YearDropDownList" runat="server" AutoPostBack="false"></asp:DropDownList>
                    </td>
                </tr>
                <tr runat="server" id="trCompanies">
                    <td runat="server" colspan="2">
                        <table>
                            <tbody>
                                <uc1:MultipleColumnDDLabelControl_New ID="moUserCompanyMultipleDrop" runat="server"></uc1:MultipleColumnDDLabelControl_New>
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr runat="server" id="trAllDealers" style="height: 30px;">
                    <td colspan="2">
                        <table border="0" style="padding-left: 1px;" cellpadding="1">
                            <tr runat="server">
                                <td style="vertical-align: bottom" colspan="2">*
                                            <asp:Label ID="AllDealersLabel" runat="server">SELECT_ALL_DEALERS</asp:Label>:
                                            <asp:RadioButton ID="rdealer" onclick=" document.all.item('ctl00_BodyPlaceHolder_moUserDealerMultipleDrop_lb_DropDown').style.color = ''; ToggleExt(this, arrDealerCtr);"
                                                AutoPostBack="false" type="radio" GroupName="Dealer" Text="" runat="server" Checked="False"></asp:RadioButton>
                                </td>
                                <td align="left" runat="server">
                                    <table>
                                        <uc1:MultipleColumnDDLabelControl_New ID="moUserDealerMultipleDrop" runat="server"></uc1:MultipleColumnDDLabelControl_New>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
                <tr runat="server" id="trOnlyDealersWith">
                    <td colspan="2" style="padding-left: 5cm;">&nbsp;&nbsp;
                        <asp:Label ID="moProductLabel" runat="server">Or only Dealers With</asp:Label>:&nbsp;&nbsp;
                        <asp:DropDownList ID="ddlDealerCurrency" runat="server" AutoPostBack="false"
                                            onchange="javascript:return ToggleDropdownsforCurrency();" Width="212px">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr><td>&nbsp;</td></tr> 
                <tr runat="server" id="trAllGroups">
                    <td colspan="2">&nbsp;&nbsp;
                        <asp:Label ID="AllGroupsLabel" runat="server">SELECT_ALL_GROUPS</asp:Label>:
                        <asp:RadioButton ID="rGroup" onclick="ToggleExt(this, arrDealerGroupCtr);"
                            AutoPostBack="false" type="radio" GroupName="Dealer" Text="" runat="server" Checked="False"></asp:RadioButton>
                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        <asp:Label ID="GroupLabel" runat="server">OR_A_SINGLE_GROUP</asp:Label>:
                        <asp:DropDownList ID="cboDealerGroup" runat="server" type="DropDown" onchange="ToggleExt(this, arrDealerGroupCtr);  document.all.item('ctl00_BodyPlaceHolder_moUserDealerMultipleDrop_lb_DropDown').style.color = '';">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr><td>&nbsp;</td></tr> 
                <tr runat="server" id="trCurrency">
                    <td align="left" colspan="2">&nbsp;&nbsp;
                            <asp:Label ID="lblCurrency" runat="server">Currency</asp:Label>:
                                &nbsp;&nbsp;<asp:DropDownList ID="ddlCurrency" runat="server" AutoPostBack="false"
                                    Width="212px">
                                </asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="btnZone">
        <asp:Button ID="btnGenRpt" runat="server" SkinID="AlternateLeftButton" Text="Generate Report Request" Width="200px" />
    </div>
    <script type="text/javascript">

        $(document).ready(function () {
            $("form > *").change(function () {
                enableReport();
            });
        });


        function enableReport() {
            var btnGenReport = document.getElementById("<%=btnGenRpt.ClientID%>");
            btnGenReport.disabled = false;
        }

        var arrDealerGroupCtr = [[['ctl00_BodyPlaceHolder_moUserDealerMultipleDrop_moMultipleColumnDropDesc'], ['ctl00_BodyPlaceHolder_moUserDealerMultipleDrop_moMultipleColumnDrop']], ['ctl00_BodyPlaceHolder_rdealer'], ['ctl00_BodyPlaceHolder_rGroup'], ['ctl00_BodyPlaceHolder_cboDealerGroup'], ['ctl00_BodyPlaceHolder_ddlDealerCurrency']]
        var arrDealerCtr = [[['ctl00_BodyPlaceHolder_moUserDealerMultipleDrop_moMultipleColumnDropDesc'], ['ctl00_BodyPlaceHolder_moUserDealerMultipleDrop_moMultipleColumnDrop']], ['ctl00_BodyPlaceHolder_rdealer'], ['ctl00_BodyPlaceHolder_rGroup'], ['ctl00_BodyPlaceHolder_cboDealerGroup'], ['ctl00_BodyPlaceHolder_ddlDealerCurrency']]

        function ToggleDropdownsforCurrency() {
            var curdropdownValue = document.getElementById("ctl00_BodyPlaceHolder_ddlDealerCurrency").value;

            //if only dealers with is chosen, then select all dealer and individual dealers have to be cleared
            if (curdropdownValue != '00000000-0000-0000-0000-000000000000') {
                document.getElementById("ctl00_BodyPlaceHolder_rdealer").checked = false;
                document.getElementById('ctl00_BodyPlaceHolder_rGroup').checked = false;
                document.getElementById("ctl00_BodyPlaceHolder_cboDealerGroup").selectedIndex = 0;
                document.getElementById('ctl00_BodyPlaceHolder_cboDealerGroup').selectedIndex = -1;
                document.getElementById("ctl00_BodyPlaceHolder_moUserDealerMultipleDrop_moMultipleColumnDrop").selectedIndex = 0;
                document.getElementById("ctl00_BodyPlaceHolder_moUserDealerMultipleDrop_moMultipleColumnDropDesc").selectedIndex = 0;

            }
            else {
                document.getElementById("ctl00_BodyPlaceHolder_rdealer").checked = true;
                document.getElementById("ctl00_BodyPlaceHolder_cboDealerGroup").selectedIndex = 0;
                document.getElementById("ctl00_BodyPlaceHolder_moUserDealerMultipleDrop_moMultipleColumnDrop").selectedIndex = 0;
                document.getElementById("ctl00_BodyPlaceHolder_moUserDealerMultipleDrop_moMultipleColumnDropDesc").selectedIndex = 0;
            }
        }
    </script>
</asp:Content>
