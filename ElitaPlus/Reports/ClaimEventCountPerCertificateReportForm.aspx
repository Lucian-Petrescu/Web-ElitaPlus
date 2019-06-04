<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ClaimEventCountPerCertificateReportForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.ClaimEventCountPerCertificateReportForm"
    MasterPageFile="~/Reports/ElitaReportExtractBase.Master" Theme="Default" %>

<%@ Register TagPrefix="Elita" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl_New.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlAvailableSelected" Src="../common/UserControlAvailableSelected_New.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <style>
        .btnZone input.altBtn[disabled] {
            visibility: visible !important;
            display: block !important;
        }
    </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="SummaryPlaceHolder" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="MessagePlaceHolder" runat="server"></asp:Content>
<asp:Content ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
        <div class="stepformZone">
            <table class="formGrid" border="0" cellspacing="0" cellpadding="0" style="padding-left: 0%;"
                width="100%">
                <tr>
                    <td align="right">
                        <span class="mandatory">*</span><asp:Label ID="lblDealerGroup" runat="server" vertical-align="middle">DEALER_GROUP</asp:Label>&nbsp;:
                    </td>
                    <td colspan="2" align="center">
                        <table width="100%">
                            <Elita:MultipleColumnDDLabelControl ID="ddlcDealerGroup" runat="server"></Elita:MultipleColumnDDLabelControl>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <span class="mandatory">*</span><asp:Label ID="lblDealers" runat="server" vertical-align="middle">SELECT_DEALERS</asp:Label>&nbsp;:
                    </td>
                    <td colspan="2">
                        <Elita:UserControlAvailableSelected ID="ucDealerAvaSel" runat="server" ShowDownButton="false" ShowUpButton="false" AvailableDesc="AVAILABLE_DEALERS" SelectedDesc="SELECTED_DEALERS"></Elita:UserControlAvailableSelected>
                    </td>
                </tr>
                <tr>
                    <td style="width: 30%" align="right">
                        <span class="mandatory">*</span><asp:Label ID="lblDate" runat="server">DATE</asp:Label>&nbsp;:
                    </td>
                    <td colspan="2">
                        <asp:TextBox runat="server" ID="moDateText" SkinID="SmallTextBox" />
                        <asp:ImageButton ID="BtnDate" runat="server" Style="vertical-align: bottom"
                            ImageUrl="~/App_Themes/Default/Images/calendar.png" />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblRepairedClaims" runat="server">NUMBER_OF_REPAIRED_CLAIMS</asp:Label>&nbsp;:
                    </td>
                    <td>
                        <asp:TextBox ID="moRepairedClaimsText" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <span class="mandatory">*</span><asp:Label ID="lblAndOr" runat="server">AND_OR</asp:Label>
                    </td>
                    <td></td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="lblReplacedClaims" runat="server">NUMBER_OF_REPLACED_CLAIMS</asp:Label>&nbsp;:
                    </td>
                    <td>
                        <asp:TextBox ID="moReplacedClaimsText" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="btnZone">
        <asp:Button ID="btnGenRpt" runat="server" SkinID="AlternateLeftButton" Text="Generate Report Request" Width="200px"></asp:Button>
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


    </script>
</asp:Content>
