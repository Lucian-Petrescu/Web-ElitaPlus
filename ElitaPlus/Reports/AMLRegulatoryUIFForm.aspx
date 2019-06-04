<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Reports/ElitaReportBase.Master"
    CodeBehind="AMLRegulatoryUIFForm.aspx.vb" Theme="Default"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.AMLRegulatoryUIFForm" %>

<%@ Register TagPrefix="uc3" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <style type="text/css">
            .btnZone input.altBtn[disabled] { visibility:visible !important; display:block !important;}
    </style>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SummaryPlaceHolder" runat="server"></asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server"></asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
        <div class="stepformZone">
            <table class="formGrid" border="0" cellspacing="0" cellpadding="" style="padding-left:15px;"
                width="100%">
                <tr>
                    <td style="white-space:nowrap;">
                        <asp:Label ID="lblReportBy" runat="server">BASE_REPORT_ON:</asp:Label></td>
                    <td align="left" style="white-space:nowrap;">
                        <asp:RadioButtonList ID="rblReportBy" runat="server" CssClass="formGrid">
                            <asp:ListItem Text="CLAIM_CREATED_DATE" Value="CR" Selected="True"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td align="left">*
                        <asp:Label ID="BeginDateLabel" runat="server">CREATED_FROM:</asp:Label>
                        <asp:TextBox ID="BeginDateText" TabIndex="1" runat="server" SkinID="SmallTextBox" onchange="DateChanged();"></asp:TextBox>
                        <asp:ImageButton ID="BtnBeginDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:ImageButton>
                    </td>
                    <td align="left">*
                        <asp:Label ID="EndDateLabel" runat="server">CREATED_TO:</asp:Label>
                        <asp:TextBox ID="EndDateText" TabIndex="1" runat="server" SkinID="SmallTextBox" onchange="DateChanged();"></asp:TextBox>
                        <asp:ImageButton ID="BtnEndDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:ImageButton>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td style="white-space:nowrap;">
                        <asp:Label ID="lblReportType" runat="server">REPORT_TYPE:</asp:Label></td>
                    <td align="left" style="white-space:nowrap;">
                        <asp:RadioButtonList ID="rblReportType" runat="server" RepeatDirection="vertical" onchange="DateChanged();" BackColor="Transparent">
                            <asp:ListItem Text="CLAIMS_COMPLIANCE_ISSUE" Value="CCI"></asp:ListItem>
                            <asp:ListItem Text="CLAIMS_NO_COMPLIANCE_ISSUE" Value="CNCI"></asp:ListItem>
                            <asp:ListItem Text="ALL_CLAIMS" Value="ALL" Selected="True"></asp:ListItem>
                        </asp:RadioButtonList>
                    </td>
                    <td></td>
                    <td></td>
                </tr>
            </table>
        </div>
    </div>
    <div class="btnZone">
        <asp:Button ID="btnGenRpt" runat="server" CausesValidation="False" Text="GENERATE_REPORT_REQUEST" SkinID="AlternateLeftButton"></asp:Button>
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