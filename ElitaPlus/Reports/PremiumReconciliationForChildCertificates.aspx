<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl_New" Src="../Common/MultipleColumnDDLabelControl_new.ascx" %>
<%@ Register TagPrefix="uc2" TagName="UserControlAvailableSelected" Src="../Common/UserControlAvailableSelected_New.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PremiumReconciliationForChildCertificates.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.PremiumReconciliationForChildCertificates" MasterPageFile="~/Reports/ElitaReportExtractBase.Master"
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
                <tr>
                    <td align="right" style="vertical-align: middle" width="20%">*
                        <asp:Label ID="lblReportBasedOn" runat="server">REPORT_BASED_ON:</asp:Label>
                    </td>
                    <td align="left" colspan="2">
                        <asp:RadioButtonList ID="ReportBasedOn" runat="server" RepeatDirection="Horizontal" onclick = "enableReport();">
                            <asp:ListItem Text="CREATED_DATE" Value="CREATED" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="WARRANTY_SALES_DATE" Value="WARRANTY"></asp:ListItem>
                        </asp:RadioButtonList>
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
                    <td align="right" style="vertical-align: middle" width="20%">*
                        <asp:Label ID="lblReportPeriodBasedOn" runat="server">REPORT_PERIOD_BASED_ON:</asp:Label>
                    </td>
                    <td align="left" colspan="2">
                        <asp:RadioButtonList ID="ReportPeriodBasedOn" runat="server" RepeatDirection="Horizontal" onclick="enableDisablePeriod();">
                            <asp:ListItem Text="RANGE" Value="R" Selected="True"></asp:ListItem>
                            <asp:ListItem Text="MONTH_AND_YEAR" Value="MY"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                </tr>
                <tr id="RangeRow">
                    <td align="right" rowspan="1" style="vertical-align: middle" width="20%">*
                        <asp:Label ID="lblRange" runat="server">RANGE:</asp:Label>
                    </td>
                    <td align="right" width="10%">
                        <asp:Label ID="lblBeginDate" runat="server">BEGIN_DATE:</asp:Label>
                    </td>
                    <td align="left" width="15%">
                        <asp:TextBox runat="server" ID="BeginDate" SkinID="SmallTextBox" onchange="DateChanged();"/>
                        <asp:ImageButton ID="BeginDateCal" runat="server" Style="vertical-align: bottom"
                            ImageUrl="~/App_Themes/Default/Images/calendar.png" />
                    </td>
                    <td align="right" width="10%">
                        <asp:Label ID="lblEndDate" runat="server">END_DATE:</asp:Label>
                    </td>
                    <td align="left">
                        <asp:TextBox runat="server" ID="EndDate" SkinID="SmallTextBox" onchange="DateChanged();"/>
                        <asp:ImageButton ID="EndDateCal" runat="server" Style="vertical-align: bottom"
                            ImageUrl="~/App_Themes/Default/Images/calendar.png" />
                    </td>
                </tr>
                <tr id="MonthAndYearRow">
                    <td align="right" style="vertical-align: middle" width="20%">*
                        <asp:Label ID="MonthYearLabel" runat="server">MONTH_AND_YEAR:</asp:Label>
                    </td>
                    <td align="right" width="10%">
                        <asp:Label ID="MonthLabel" runat="server">MONTH:</asp:Label>
                    </td>
                    <td align="left" width="10%">
                        <asp:DropDownList ID="MonthDropDown" runat="server" SkinID="SmallDropDown" onchange="enableReport();"></asp:DropDownList>
                    </td>
                    <td align="right" width="10%">
                        <asp:Label ID="YearLabel" runat="server">YEAR:</asp:Label>
                    </td>
                    <td align="left">
                        <asp:DropDownList ID="YearDropDown" runat="server" SkinID="SmallDropDown" onchange="enableReport();"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="1" style="vertical-align: middle" width="20%">*
                        <asp:Label ID="lbldealers" runat="server">PARENT_DEALERS:</asp:Label>
                    </td>
                    <td align="left" colspan="3">
                        <uc2:UserControlAvailableSelected ID="UsercontrolAvailableSelectedDealers" runat="server" onclick ="enableReport();"></uc2:UserControlAvailableSelected>
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

        function enableDisablePeriod() {

            var flag = document.getElementById("ctl00_BodyPlaceHolder_ReportPeriodBasedOn_0").checked;

            document.getElementById('<%= BeginDate.ClientID %>').disabled = !flag;
            document.getElementById('<%=BeginDateCal.ClientID%>').disabled = !flag;
            document.getElementById('<%=EndDate.ClientID%>').disabled = !flag;
            document.getElementById('<%=EndDateCal.ClientID%>').disabled = !flag;
            document.getElementById('<%=MonthDropDown.ClientID%>').disabled = flag;
            document.getElementById('<%=YearDropDown.ClientID%>').disabled = flag;
        }
    </script>
</asp:Content>