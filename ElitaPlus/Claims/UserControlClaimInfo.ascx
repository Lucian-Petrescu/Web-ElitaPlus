<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlClaimInfo.ascx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.UserControlClaimInfo" %>
<div>
    <table class="summaryGrid" cellspacing="0" cellpadding="0" border="0">
        <tbody>
            <tr>
                <td align="right">
                    <asp:Label ID="CertificateNumberLabel" runat="server" SkinID="SummaryLabel">CERTIFICATE NUMBER</asp:Label>:
                </td>
                <td class="bor padRight" align="left" runat="server" id="CertificateNumberTD"></td>

                <td class="padLeft" align="right" runat="server" id="ServiceCenterLabelTD">
                    <asp:Label ID="ServiceCenterNameLabel" runat="server" SkinID="SummaryLabel">SERVICE CENTER NAME</asp:Label>:
                </td>
                <td class="bor padRight" align="left" runat="server" id="ServiceCenterTD"></td>

                <td class="padLeft" align="left">
                    <asp:Label ID="StatusLabel" runat="server" SkinID="SummaryLabel">CLAIM STATUS</asp:Label>:
                </td>
                <td align="left" runat="server" id="CLAIMStatusTD"></td>

                <td class="padLeft" align="left" id="SubscriberStatusLabelTD" runat="server">
                    <asp:Label ID="SubscriberStatusLabel" runat="server" SkinID="SummaryLabel">SUBSCRIBER_STATUS</asp:Label>:
                </td>
                <td align="left" runat="server" id="SubscriberStatus"></td>

                <td class="padLeft" align="right">
                    <asp:Label ID="ScrutinyRequiredLabel" runat="server" SkinID="SummaryLabel">SCRUTINY_REQUIRED</asp:Label>:
                </td>
                <td class="bor padRight" align="left" runat="server" id="ScrutinyRequiredLabelTD"></td>
            </tr>
        </tbody>
    </table>
</div>












