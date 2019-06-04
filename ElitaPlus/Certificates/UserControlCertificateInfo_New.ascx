<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlCertificateInfo_New.ascx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Certificates.UserControlCertificateInfo_New"
    TargetSchema="http://schemas.microsoft.com/intellisense/ie5" %>
<tr runat="server" id="SubscriberStatusTR">
    <td align="right">
        <asp:Label ID="CustomerNameLabelLabel" runat="server" SkinID="SummaryLabel">CUSTOMER_NAME</asp:Label>:
    </td>
    <td align="left" class="bor" style="font-weight: bold" colspan="2" runat="server" id="CustomerNameTD">
        <strong></strong>
    </td>
    <td align="right">
        <asp:Label ID="WarrantySalesDateLabel" runat="server" SkinID="SummaryLabel">WARRANTY_SALES_DATE</asp:Label>:
    </td>
    <td align="left" class="bor" runat="server" id="WarrantySalesDateTD"></td>
    <td align="right">
        <asp:Label ID="ScrutinyRequiredLabel" runat="server" SkinID="SummaryLabel">SCRUTINY_REQUIRED</asp:Label>:
    </td>
    <td align="left" class="bor" runat="server" id="ScrutinyRequiredLabelTD"></td>
    <td align="right" runat="server" id="SubscriberStatusLabelTD">
        <asp:Label ID="SubscriberStatusLabel" runat="server" SkinID="SummaryLabel">SUBSCRIBER_STATUS</asp:Label>:
    </td>
    <td align="left" runat="server" id="SubscriberStatusTD"></td>
    <td colspan="3" align="right" class="StatClosed" style="text-align: right;">
        <asp:Label ID="moRestrictedStatus" Visible="False" runat="server">RESTRICTED</asp:Label>
    </td>
</tr>
<tr>
    <td align="right">
        <asp:Label ID="CertificateNumberLabel" runat="server" SkinID="SummaryLabel">CERTIFICATE</asp:Label>:
    </td>
    <td align="left" class="bor" runat="server" id="CertificateNumberTD"></td>
    <td align="right">
        <asp:Label ID="StatusLabel" runat="server" SkinID="SummaryLabel">PROTECTION_STATUS</asp:Label>:
    </td>
    <td align="left" class="bor" runat="server" id="StatusTD"></td>
    <td align="left" class="bor" runat="server" id="CustCancelDateTD"></td>
    <td align="right">
        <asp:Label ID="ProductTotalPaidAmountLabel" runat="server" SkinID="SummaryLabel">PRODUCT_TOTAL_PAID_AMOUNT</asp:Label>:
    </td>
    <td align="left" runat="server" id="ProductTotalPaidAmountTD"></td>

</tr>
<tr>
    <td align="right">
        <asp:Label ID="DealerNameLabel" runat="server" SkinID="SummaryLabel">DEALER_NAME</asp:Label>:
    </td>
    <td align="left" class="bor" runat="server" id="DealerNameTD"></td>
    <td align="right">
        <asp:Label ID="DealerGroupLabel" runat="server" SkinID="SummaryLabel">DEALER_GROUP</asp:Label>:
    </td>
    <td align="left" class="bor" runat="server" id="DealerGroupTD"></td>
    <td align="right">
        <asp:Label ID="CompanyCodeLabel" runat="server" SkinID="SummaryLabel">COMPANY_CODE</asp:Label>:
    </td>
    <td align="left" class="bor" runat="server" id="CompanyCodeTD"></td>
    <td align="right">
        <asp:Label ID="ProductRemainLiabilityLimitLabel" runat="server" SkinID="SummaryLabel">PRODUCT_REMAIN_LIABILITY_LIMIT</asp:Label>:
    </td>
    <td align="left" runat="server" id="ProductRemainLiabilityLimitTD"></td>

</tr>
