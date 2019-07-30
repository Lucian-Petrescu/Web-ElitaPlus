<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Reports/ElitaReportBase.Master" CodeBehind="LossSummaryArgForm.aspx.vb" Theme="Default" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.LossSummaryArgForm" %>

<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SummaryPlaceHolder" runat="server"></asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server"></asp:Content>
<asp:Content ID="Content5" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
        <div class="stepformZone">
            <table class="formGrid" border="0" cellspacing="0" cellpadding="" style="padding-left: 15px;"
                width="55%">
                <tr>
                    <td style="white-space: nowrap;">
                        <asp:Label ID="lblReporting" runat="server">PLEASE SELECT REPORTING PERIOD</asp:Label></td>
                    <td>
                        <asp:RadioButton ID="rdoMTD" runat="server" Width="87px" Text="M-T-D" GroupName="PeriodGroup" Checked="True"></asp:RadioButton>
                    </td>
                    <td>
                        <asp:RadioButton ID="rdoQTD" runat="server" Width="87px" Text="Q-T-D" GroupName="PeriodGroup"></asp:RadioButton>
                    </td>
                    <td>
                        <asp:RadioButton ID="rdoYTD" runat="server" Width="87px" Text="Y-T-D" GroupName="PeriodGroup"></asp:RadioButton>
                    </td>
                </tr>
            </table>
            <table width="100%">
                <tr>
                    <td>
                        <hr />
                    </td>
                </tr>
            </table>
            <table class="formGrid" border="0" cellspacing="0" cellpadding="0" width="35%">
                <tr id="trcomp" runat="server">
                    <td align="left" colspan="2">
                        <asp:Label ID="MonthYearLabel" runat="server">SELECT_MONTH_AND_YEAR</asp:Label>:&nbsp;
                        <asp:DropDownList ID="MonthDropDownList" runat="server" Width="128px" AutoPostBack="false"></asp:DropDownList>
                        &nbsp;
                        <asp:DropDownList ID="YearDropDownList" runat="server" Width="84px" AutoPostBack="false">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right" colspan="1" style="width: 30%"></td>
                    <td align="center" colspan="2">
                        <uc1:MultipleColumnDDLabelControl ID="moUserCompanyMultipleDrop" runat="server"></uc1:MultipleColumnDDLabelControl>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="btnZone">
        <asp:Button ID="btnGenRpt" runat="server" CausesValidation="False" Text="View" SkinID="AlternateLeftButton"></asp:Button>
    </div>
</asp:Content>
