<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PrintDealerLoadRejectForm.aspx.vb" Theme="Default"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.PrintDealerLoadRejectForm" MasterPageFile="~/Reports/ElitaReportExtractBase.Master" %>

<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <!--Start Header-->
    <input id="rptTitle" type="hidden" name="rptTitle">
    <input id="rptSrc" type="hidden" name="rptSrc">
    <div class="dataContainer">
        <div class="stepformZone">

            <table class="formGrid" border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px;"
                width="100%">
                <tr>
                    <td>
                        <asp:Label ID="lblEntireRecord" runat="server" Visible="true" Style="text-align: right">ENTIRE_RECORD</asp:Label>
                        <asp:CheckBox ID="moEntireRecordCheck" runat="server" Visible="true" BorderWidth="0" />
                        &nbsp;&nbsp;&nbsp;
                        <asp:Label ID="lblIncludeBypassedRecords" runat="server" Visible="true" Style="text-align: right">INCLUDE_BYPASSED_RECORDS</asp:Label>
                        <asp:CheckBox ID="moInclBypassedRecCheck" runat="server" Visible="true" Checked="true" BorderWidth="0" />
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="btnZone">
        <asp:Button ID="btnGenRpt" runat="server" CausesValidation="False" Text="Generate Report Request" SkinID="AlternateLeftButton"></asp:Button>
        <asp:Button ID="btnBack" SkinID="AlternateLeftButton" Text="BACK" runat="server"></asp:Button>
    </div>
</asp:Content>
