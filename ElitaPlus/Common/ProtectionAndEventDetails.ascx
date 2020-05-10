<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ProtectionAndEventDetails.ascx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ProtectionAndEventDetails" %>

<!----Start Here---->
<table border="0" class="summaryGrid" cellpadding="0" cellspacing="0">
    <tr>
        <td align="right" nowrap="nowrap" ><asp:Label ID="CustomerNameLabel" runat="server">CUSTOMER_NAME</asp:Label>:</td>
        <td align="left" nowrap="nowrap" colspan="5"><asp:Label ID="CustomerNameText" runat="server" Font-Bold="true"></asp:Label></td>       
    </tr>
    <tr id="custAddress" runat="server" visible="false">
       <td align="right" nowrap="nowrap"><asp:Label ID="CustomerAddressLabel" runat="server" >CUSTOMER_ADDRESS</asp:Label>:</td>
        <td align="left" nowrap="nowrap" colspan="5"><asp:Label ID="CustomerAddressText" runat="server" ></asp:Label></td>
    </tr>
    <tr>
       <td align="right" nowrap="nowrap"><asp:Label ID="CallerNameLabel" runat="server" >NAME_OF_CALLER</asp:Label>:</td>
        <td align="left" nowrap="nowrap" colspan="5"><asp:Label ID="CallerNameText" runat="server" ></asp:Label></td>
    </tr>
    <tr>
      <td align="right" nowrap="nowrap"><asp:Label ID="DealerNameLabel" runat="server" >DEALER_NAME</asp:Label>:</td>
        <td align="left" nowrap="nowrap" class="bor padRight"><asp:Label ID="DealerNameText" runat="server" ></asp:Label></td>
        <td align="right" nowrap="nowrap" class="padLeft"><asp:Label ID="EnrolledMakeLabel" runat="server" >ENROLLED_MAKE</asp:Label>:</td>
        <td align="left" nowrap="nowrap" class="bor padRight"><asp:Label ID="EnrolledMakeText" runat="server" ></asp:Label></td>
        <td align="right" nowrap="nowrap" class="padLeft"><asp:Label ID="ClaimNumberLabel" runat="server" >CLAIM_#</asp:Label>:</td>
        <td align="left" nowrap="nowrap" class="padRight"><asp:Label ID="ClaimNumberText" runat="server" ></asp:Label></td>
    </tr>
    <tr>
         <td  align="right" nowrap="nowrap" ><asp:Label ID="ProtectionStatusLabel" runat="server">PROTECTION_STATUS</asp:Label>:</td>
        <td id="ProtectionStatusTD" align="left" style="white-space: nowrap" class="bor padRight" runat="server"><asp:Label ID="ProtectionStatusText" runat="server" ></asp:Label></td>
        <td align="right" nowrap="nowrap" class="padLeft"><asp:Label ID="EnrolledModeLabel" runat="server" >ENROLLED_MODEL</asp:Label>:</td>
        <td align="left" nowrap="nowrap" class="bor padRight"><asp:Label ID="EnrolledModeText" runat="server" ></asp:Label></td> 
        <td align="right" nowrap="nowrap" class="padLeft"><asp:Label ID="ClaimStatusLabel" runat="server" >CLAIM_STATUS</asp:Label>:</td>
        <td id="ClaimStatusTD" align="left" style="white-space: nowrap" class="padRight"  runat="server"><asp:Label ID="ClaimStatusText" runat="server" ></asp:Label></td>       
       
    </tr>
    <tr>
       <td colspan="2" class="bor padRight"/>
        <td align="right" nowrap="nowrap" class="padLeft"><asp:Label ID="ClaimedMakeLabel" runat="server" >CLAIMED_MAKE</asp:Label>:</td>
        <td align="left" nowrap="nowrap" class="bor padRight"><asp:Label ID="ClaimedMakeText" runat="server" ></asp:Label></td>
        <td align="right" nowrap="nowrap" class="padLeft"><asp:Label ID="TypeOfLossLabel" runat="server" >TYPE_OF_LOSS</asp:Label>:</td>
        <td align="left" nowrap="nowrap" class="padRight"><asp:Label ID="TypeOfLossText" runat="server" ></asp:Label></td>
    </tr>
    <tr>
        <td colspan="2" class="bor padRight"/>
        <td align="right" nowrap="nowrap" class="padLeft"><asp:Label ID="ClaimedModelLabel" runat="server" >CLAIMED_MODEL</asp:Label>:</td>
        <td align="left" nowrap="nowrap" class="bor padRight"><asp:Label ID="ClaimedModelText" runat="server" ></asp:Label></td>
        <td align="right" nowrap="nowrap" class="padLeft"><asp:Label ID="DateOfLossLabel" runat="server">DATE_OF_LOSS</asp:Label>:</td>
        <td align="left" nowrap="nowrap" class="padRight"><asp:Label ID="DateOfLossText" runat="server" ></asp:Label></td>
    </tr>
</table>

