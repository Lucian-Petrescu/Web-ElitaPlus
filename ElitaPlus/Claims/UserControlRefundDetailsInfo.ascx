<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlRefundDetailsInfo.ascx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.UserControlRefundDetailsInfo" %>


<div>
    <table width="100%" class="dataGrid">
        <tr>
            <td class="bor" align="left">
            </td>
            <td class="bor" align="right">
                <asp:Label ID="lblRefundRecordCount" runat="server"></asp:Label>
            </td>
        </tr>
    </table>
</div>
<div style="overflow: auto; height: 200px;">
    <asp:GridView ID="GridViewRefundDetails" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="true"
        SkinID="DetailPageGridView" AllowSorting="false" PageSize="50">
        <Columns>
            <asp:BoundField DataField="refund_method_xcd" HeaderText="REFUND_METHOD" ItemStyle-Width="20%" />
            <asp:BoundField DataField="refund_amount" HeaderText="REFUND_AMOUNT" ItemStyle-Width="15%" />
            <asp:BoundField DataField="refund_requested_date" HeaderText="REQUESTED_DATE" ItemStyle-Width="17%" />
            <asp:BoundField DataField="refund_requested_by" HeaderText="REQUESTED_BY" ItemStyle-Width="17%" />
             <asp:BoundField DataField="refund_authorization_status" HeaderText="AUTHORIZATION_STATUS" ItemStyle-Width="10%" />
        </Columns>
        <PagerStyle />
    </asp:GridView>
</div>
