<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ClaimDeductibleRefundForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ClaimDeductibleRefundForm" Theme="Default" MasterPageFile="../Navigation/masters/ElitaBase.Master" %>
<%@ Register TagPrefix="Elita" TagName="MessageController" Src="~/Common/MessageController.ascx" %>

<asp:Content ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="SummaryPlaceHolder" runat="server"> 
    <table border="0" class="summaryGrid" cellpadding="0" cellspacing="0">
    <tr>
        <td align="right" nowrap="nowrap" ><asp:Label ID="lblCustomerName" runat="server" SkinID="SummaryLabel">CUSTOMER_NAME</asp:Label>:</td>
        <td align="left" nowrap="nowrap" colspan="5"><asp:Label ID="lblCustomerNameValue" runat="server" Font-Bold="true"><%#NO_DATA%></asp:Label></td>       
    </tr>   
    <tr>
      <td align="right" nowrap="nowrap"><asp:Label ID="lblClaimNumber" runat="server" SkinID="SummaryLabel">CLAIM_#</asp:Label>:</td>
        <td align="left" nowrap="nowrap" class="bor padRight"><asp:Label ID="lblClaimNumberValue" runat="server" SkinID="SummaryLabel"><%#NO_DATA%></asp:Label></td>
        <td align="right" nowrap="nowrap" class="padLeft"><asp:Label ID="lblDealerName" runat="server" SkinID="SummaryLabel">DEALER_NAME</asp:Label>:</td>
        <td align="left" nowrap="nowrap" class="bor padRight"><asp:Label ID="lblDealerNameValue" runat="server" SkinID="SummaryLabel"><%#NO_DATA%></asp:Label></td>
        <td align="right" nowrap="nowrap" class="padLeft"><asp:Label ID="lblClaimStatus" runat="server" SkinID="SummaryLabel">CLAIM_STATUS</asp:Label>:</td>
        <td id="ClaimStatusTD" runat="server" align="left" nowrap="nowrap" class="padRight"><asp:Label ID="lblClaimStatusValue" runat="server" SkinID="SummaryLabel"><%#NO_DATA%></asp:Label></td>
    </tr>
    <tr>
        <td align="right" nowrap="nowrap" ><asp:Label ID="lblWorkPhoneNumber" runat="server" SkinID="SummaryLabel">WORK_CELL_NUMBER</asp:Label>:</td>
        <td align="left" nowrap="nowrap" class="bor padRight" ><asp:Label ID="lblWorkPhoneNumberValue" runat="server" SkinID="SummaryLabel"><%#NO_DATA%></asp:Label></td>
        <td align="right" nowrap="nowrap" class="padLeft"><asp:Label ID="lblDealerGroup" runat="server" SkinID="SummaryLabel">DEALER_GROUP</asp:Label>:</td>
        <td align="left" nowrap="nowrap" class="bor padRight"><asp:Label ID="lblDealerGroupValue" runat="server" SkinID="SummaryLabel"><%#NO_DATA%></asp:Label></td> 
        <td align="right" nowrap="nowrap" class="padLeft"><asp:Label ID="lblSubscriberStatus" runat="server" SkinID="SummaryLabel">SUBSCRIBER_STATUS</asp:Label>:</td>
        <td id="SubStatusTD" align="left" nowrap="nowrap" class="padRight"  runat="server"><asp:Label ID="lblSubscriberStatusValue" runat="server" SkinID="SummaryLabel"><%#NO_DATA%></asp:Label></td>       
       
    </tr>
    <tr>
        <td align="right" nowrap="nowrap" class="padLeft"><asp:Label ID="lblCertificateNumber" runat="server" SkinID="SummaryLabel">CERTIFICATE_NUMBER</asp:Label>:</td>
        <td align="left" nowrap="nowrap" class="bor padRight"><asp:Label ID="lblCertificateNumberValue" runat="server" SkinID="SummaryLabel"><%#NO_DATA%></asp:Label></td>
        <td align="right" nowrap="nowrap" class="padLeft"><asp:Label ID="lblSerialNumberImei" runat="server" SkinID="SummaryLabel">SERIAL_NUMBER_IMEI</asp:Label>:</td>
        <td align="left" nowrap="nowrap" class="bor padRight"><asp:Label ID="lblSerialNumberImeiValue" runat="server" SkinID="SummaryLabel"><%#NO_DATA%></asp:Label></td>
        <td align="right" nowrap="nowrap" class="padLeft"><asp:Label ID="lblDateOfLoss" runat="server" SkinID="SummaryLabel">DATE_OF_LOSS</asp:Label>:</td>
        <td align="left" nowrap="nowrap" class="padRight"><asp:Label ID="lblDateOfLossValue" runat="server" SkinID="SummaryLabel"><%#NO_DATA%></asp:Label></td>
    </tr>  
</table>  
</asp:Content>
<asp:Content ContentPlaceHolderID="MessagePlaceHolder" runat="server"> 
    <Elita:MessageController runat="server" ID="mcIssueStatus" Visible="false"/>   
</asp:Content>
<asp:Content ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <script type="text/javascript">
    function validate()
    {
        var ddlRefundType = document.getElementById('<%=ddlRefundType.ClientID %>');
        if (ddlRefundType.options[ddlRefundType.selectedIndex].value == '00000000-0000-0000-0000-000000000000')
        {
           
            //msgBox.style.display = 'block';
            return false;
        }
        

        return true;
    }

    

    </script>
<div class="dataContainer">
    <h2 class="dataGridHeader">
        <asp:label runat="server" ID="lblGrdHdr" ></asp:label>
    </h2>
  
    <div style="width: 100%">
    </div>
</div>
<asp:Panel runat="server" ID="AddImagePanel" CssClass="dataContainer">
       <table width="100%" border="0" class="formGrid" cellpadding="0" cellspacing="0">
            <tr>
                <td align="right">
                    <asp:Label ID="lblClmDedRefundAmount" runat="server" Text="DEDUCTIBLE_REFUND_AMOUNT"></asp:Label>:
                </td>
                <td colspan="2">
                   <asp:TextBox ID="txtClmDedRefundAmount" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right">
                    <asp:Label ID="lblRefundType" runat="server" Text="REFUND_METHOD"></asp:Label>:
                </td>
                <td colspan="2">
                    <asp:DropDownList ID="ddlRefundType" runat="server" SkinID="MediumDropDown"
                        AutoPostBack="false">
                    </asp:DropDownList>
                </td>
            </tr>
          <tr>
                <td colspan="3" align="right">
                    &nbsp;
                </td>
            </tr>
            <tr>
                <td colspan="2" align="right">
                    &nbsp;
                </td>
                 <td align="right">
                    <asp:Button ID="btnSubmit" runat="server" SkinID="PrimaryRightButton" Text="SUBMIT_FOR_APPROVAL" OnClientClick="return validate();" />
                </td>
            </tr>
        </table>
</asp:Panel>

<div class="btnZone">
<asp:Button ID="btnBack" runat="server" SkinID="AlternateLeftButton" Text="BACK"  />
</div>
<asp:HiddenField ID="hdnSelectedIssueCode" runat="server" />  
</asp:Content>


