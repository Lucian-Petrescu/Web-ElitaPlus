<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl_New" Src="../Common/MultipleColumnDDLabelControl_new.ascx" %>
<%@ Register TagPrefix="uc2" TagName="UserControlAvailableSelected" Src="../Common/UserControlAvailableSelected_New.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SummaryPaymentByCertificate.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.SummaryPaymentByCertificate" MasterPageFile="~/Reports/ElitaReportExtractBase.Master"
    Theme="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
<style>
            .btnZone input.altBtn[disabled] { visibility:visible !important; display:block !important;}
        </style>  
    </asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server"></asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server"></asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
        <div class="stepformZone">
            <table class="formGrid" border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px;" width="100%">               
                <tr id="RangeRow">
                    <td align="right" rowspan="1" style="vertical-align: middle" width="20%">
                        <asp:Label ID="lblRange" runat="server">RANGE:</asp:Label>
                    </td>
                    <td align="right" width="10%">
                        <asp:Label ID="lblBeginDate" runat="server">BEGIN_DATE:</asp:Label>
                    </td>
                    <td align="left" width="10%">
                        <asp:TextBox runat="server" ID="BeginDate" SkinID="SmallTextBox" onchange="DateChanged();"/>
                    </td>
                    <td align="left" width="10%">
                        <asp:ImageButton ID="BeginDateCal" runat="server" Style="vertical-align: bottom"
                            ImageUrl="~/App_Themes/Default/Images/calendar.png" />
                    </td>
                    <td align="right" width="10%">
                        <asp:Label ID="lblEndDate" runat="server">END_DATE:</asp:Label>
                    </td>
                    <td align="left" width="10%">
                        <asp:TextBox runat="server" ID="EndDate" SkinID="SmallTextBox" onchange="DateChanged();"/>
                    </td>
                    <td align="left" width="10%"><asp:ImageButton ID="EndDateCal" runat="server" Style="vertical-align: bottom"
                            ImageUrl="~/App_Themes/Default/Images/calendar.png" />
                    </td>
                </tr>
                <tr>
                    <td align="right" style="vertical-align: middle" width="20%">*
                        <asp:Label ID="CompanyLabel" runat="server">COMPANY:</asp:Label>
                    </td>
                    <td align="left" colspan="2">
                        <asp:DropDownList ID="CompanyDropDown" runat="server" SkinID="MediumDropDown" AutoPostBack="true" OnSelectedIndexChanged="CompanyDropDown_SelectedIndexChanged"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="1" style="vertical-align: middle" width="20%">*
                        <asp:Label ID="lbldealers" runat="server">DEALERS:</asp:Label>
                    </td>
                    <td align="left" colspan="3">
                        <uc2:UserControlAvailableSelected ID="UsercontrolAvailableSelectedDealers" runat="server" onclick ="enableReport();"></uc2:UserControlAvailableSelected>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="vertical-align: middle" width="20%">*
                        <asp:Label ID="CertificateStatus" runat="server">STATUS:</asp:Label>
                    </td>
                    <td align="left" colspan="2">
                        <asp:DropDownList ID="CertStatusDropDown" runat="server" SkinID="MediumDropDown" AutoPostBack="false"></asp:DropDownList>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="btnZone">
        <asp:Button ID="btnGenRpt" runat="server" CausesValidation="False" Text="Generate Report Request" SkinID="AlternateLeftButton"></asp:Button>
    </div>

<script type='text/javascript'>
        $(document).ready(function () {
            $("form > *").change(function () {
                enableReport();
            });
        });
        
        function DateChanged() {
            enableReport();
        }

        function enableReport() {
            var btnGenReport = document.getElementById("<%=btnGenRpt.ClientID%>");
            btnGenReport.disabled = false;
        }        
    </script>
</asp:Content>