<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlCaseHeaderInfo.ascx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.UserControlCaseHeader" %>

<table border="0" class="summaryGrid" cellpadding="0" cellspacing="0">
    <tr>
        <td align="right" nowrap="nowrap">
            <asp:Label ID="LabelCustomerName" runat="server" >CUSTOMER_NAME</asp:Label>:</td>
        <td align="left" nowrap="nowrap" colspan="5">
            <asp:Label ID="LabelCustomerNameValue" runat="server" Font-Bold="true"><%#NoData%></asp:Label></td>
    </tr>
    <tr>
        <td align="right" nowrap="nowrap">
            <asp:Label ID="LabelCallerName" runat="server" >NAME_OF_CALLER</asp:Label>:</td>
        <td align="left" nowrap="nowrap" colspan="5">
            <asp:Label ID="LabelCallerNameValue" runat="server" ><%#NoData%></asp:Label></td>
    </tr>
    <tr>
        <td align="right" nowrap="nowrap">
            <asp:Label ID="LabelCompany" runat="server">COMPANY</asp:Label>:
        </td>
        <td align="left" nowrap="nowrap" class="bor padRight">
            <asp:Label ID="LabelCompanyValue" runat="server"><%#NoData%></asp:Label>
        </td>
        <td align="right" nowrap="nowrap">
            <asp:Label ID="LabelDealerName" runat="server">DEALER</asp:Label>:
        </td>
        <td align="left" nowrap="nowrap" class="padRight">
            <asp:Label ID="LabelDealerNameValue" runat="server"><%#NoData%></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="right" nowrap="nowrap">
            <asp:Label ID="LabelCaseNumber" runat="server">CASE_NUMBER</asp:Label>:
        </td>
        <td align="left" nowrap="nowrap" class="bor padRight">
            <asp:Label ID="LabelCaseNumberValue" runat="server"><%#NoData%></asp:Label>
        </td>
        <td align="right" nowrap="nowrap">
            <asp:Label ID="LabelCaseOpenDate" runat="server">CASE_OPEN_DATE</asp:Label>:
        </td>
        <td align="left" nowrap="nowrap" class="padRight">
            <asp:Label ID="LabelCaseOpenDateValue" runat="server"><%#NoData%></asp:Label>
        </td>
    </tr>
    <tr>

        <td align="right" nowrap="nowrap">
            <asp:Label ID="LabelCasePurpose" runat="server">CASE_PURPOSE</asp:Label>:
        </td>
        <td align="left" nowrap="nowrap" class="bor padRight">
            <asp:Label ID="LabelCasePurposeValue" runat="server"><%#NoData%></asp:Label>
        </td>
        <td align="right" nowrap="nowrap">
            <asp:Label ID="LabelCaseStatus" runat="server">CASE_STATUS</asp:Label>:
        </td>
        <td id="CaseStatusTD" runat="server" align="left" style="white-space: nowrap"  class="padRight">
            <asp:Label ID="LabelCaseStatusValue" runat="server"><%#NoData%></asp:Label>
        </td>
    </tr>
    <tr id="caseCloseInfo" runat="server" visible="false">
        <td align="right" nowrap="nowrap">
            <asp:Label ID="LabelCaseCloseDate" runat="server">CASE_CLOSE_DATE</asp:Label>:
        </td>
        <td align="left" nowrap="nowrap" class="bor padRight">
            <asp:Label ID="LabelCaseCloseDateValue" runat="server"><%#NoData%></asp:Label>
        </td>
        <td align="right" nowrap="nowrap">
            <asp:Label ID="LabelCaseClose" runat="server">CASE_CLOSE_REASON</asp:Label>:
        </td>
        <td align="left" nowrap="nowrap" class="padRight">
            <asp:Label ID="LabelCaseCloseValue" runat="server"><%#NoData%></asp:Label>
        </td>
    </tr>
    <tr>
        <td align="right" nowrap="nowrap">
            <asp:Label ID="LabelCertificateNumber" runat="server">CERTIFICATE_NUMBER</asp:Label>:
        </td>
        <td align="left" nowrap="nowrap" class="bor padRight">
            <asp:Label ID="LabelCertificateNumberValue" runat="server"><%#NoData%></asp:Label>
        </td>
        <td align="right" nowrap="nowrap">
            <asp:Label ID="LabelClaimNumber" runat="server">CLAIM_NUMBER</asp:Label>:
        </td>
        <td align="left" nowrap="nowrap" class="padRight">
            <asp:Label ID="LabelClaimNumberValue" runat="server"><%#NoData%></asp:Label>
        </td>
    </tr>
</table>
