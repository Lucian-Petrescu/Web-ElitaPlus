<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DealerForm.aspx.vb" Theme="Default"
Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.DealerForm" EnableSessionState="True"
MasterPageFile="../Navigation/masters/ElitaBase.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register TagPrefix="Elita" TagName="UserControlAddress" Src="../Common/UserControlAddress_New.ascx" %>
<%@ Register TagPrefix="Elita" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl_New.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UserControlAvailableSelected" Src="../Common/UserControlAvailableSelected_New.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlBankInfo" Src="../Common/UserControlBankInfo_New.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlClaimCloseRules" Src="~/Common/UserControlClaimCloseRules.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlAttrtibutes" Src="~/Common/UserControlAttrtibutes.ascx" %>
<%@ Register assembly="Microsoft.Web.UI.WebControls" namespace="Microsoft.Web.UI.WebControls" tagprefix="iewc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">   
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto">
    <Scripts>
        <asp:ScriptReference Path="~/Navigation/scripts/ComunaSuggest.js" />
    </Scripts>
</asp:ScriptManager>
<div class="dataContainer">
<table class="formGrid" border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px;"
       width="100%">
<tr>
    <td align="left" nowrap="nowrap" colspan="4" class="borderLeft" width="100%">
        <table class="formGrid" width="100%">
            <tr>
                <td>
                    <Elita:MultipleColumnDDLabelControl runat="server" ID="moMultipleColumnDrop" />
                </td>
            </tr>
        </table>
    </td>
</tr>
<tr style="height: 1px;">
    <td colspan="4" style="height: 1px; background-color: Silver;">
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="lblDealerCode" runat="server">Dealer_Code</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:TextBox ID="txtDealerCode" runat="server" Width="205px"></asp:TextBox>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblDealerName" runat="server">Dealer_Name</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:TextBox ID="txtDealerName" runat="server" Width="170px"></asp:TextBox>&nbsp;
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="lblClientDealerCode" runat="server">Client_Dealer_Code</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:TextBox ID="txtClientDealerCode" runat="server" Width="205px"></asp:TextBox>
    </td>
    <td align="right" nowrap="nowrap">
                   
    </td>
    <td align="left" nowrap="nowrap">
                   
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="lblDealerGroupel" runat="server">Dealer_Group</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moDealerGroupDrop" runat="server" Width="205px" AutoPostBack="True">
        </asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblTaxId" runat="server">Tax_Id</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:TextBox ID="txtTaxIdNumber" runat="server" Width="170px"></asp:TextBox>
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="lblDealerType" runat="server">Dealer_Type</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moDealerTypeDrop" runat="server" Width="205px" AutoPostBack="True">
        </asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblServiceNetwork" runat="server">Service_Network</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moServiceNetworkDrop" runat="server" Width="170px">
        </asp:DropDownList>
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        &nbsp;
        <asp:Label ID="lblDealerIsRetailer" runat="server">Dealer_Is_Retailer</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="dlstRetailerID" runat="server" Width="205px">
        </asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblPriceMatrix" runat="server">PRICE_MATRIX_USES_WP</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moPriceMatrixDrop" runat="server" Width="170px">
        </asp:DropDownList>
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="lblManualEnrollmentAllowed" runat="server">MANUAL_ENROLLMENT_ALLOWED</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moManualEnrollmentAllowedId" runat="server" Width="205px">
        </asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblExpectedPremiumIsWP" runat="server">EXPECTED_PREMIUM_IS_WP</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moExpectedPremiumIsWPDrop" runat="server" Width="170px">
        </asp:DropDownList>
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="lblClaimSystem" runat="server">CLAIM_SYSTEM</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moClaimSystemDrop" runat="server" AutoPostBack="true" Width="205px">
        </asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblAObligor" runat="server">ASSURANT_IS_OBLIGOR</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moAObligorDrop" runat="server" SkinID="MediumDropDown">
        </asp:DropDownList>
    </td>
</tr>
<tr id="trClaimRec" runat="server">
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="lblClaimRecording" runat="server">CLAIM_RECORDING</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moClaimRecordingDrop" runat="server" SkinID="MediumDropDown">
        </asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblUseFraudMonitoring" runat="server">USE_FRAUD_MONITORING</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="ddlUseFraudMonitoring" runat="server" Width="170px"></asp:DropDownList>
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        &nbsp;
        <asp:Label ID="lblUseEquipment" runat="server">USER_EQUIPMENT</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moUseEquipment" runat="server" SkinID="MediumDropDown" AutoPostBack="true">
        </asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblValidateBillingCycleId" runat="server">VALIDATE_BILLING_CYCLE</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moValidateBillingCycleIdDrop" runat="server" SkinID="MediumDropDown">
        </asp:DropDownList>
    </td>
</tr>
         
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        &nbsp;
        <asp:Label ID="lblValidateSerialNumber" runat="server">VALIDATE_SERIAL_NUMBER</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moValidateSerialNumberDrop" runat="server" SkinID="MediumDropDown">
        </asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblBestReplacement" runat="server">BEST_REPLACEMENT</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moBestReplacementDrop" runat="server" SkinID="MediumDropDown">
        </asp:DropDownList>
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="lblskunumber" runat="server">VALIDATE_SKU</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moSkuNumberDrop" runat="server" Width="205px" AutoPostBack="true">
        </asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblEquipmentList" runat="server">EQUIPMENT_LIST</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moEquipmentListDrop" runat="server" Width="170px">
        </asp:DropDownList>
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="lblCertificatesAutonumberId" runat="server">CERT_NUMBER_AUTO_GENERATE</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="cboCertificatesAutonumberId" runat="server" AutoPostBack="true" SkinID="MediumDropDown">
        </asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblCertificatesAutonumberPrefix" runat="server">CERT_NUMBER_AUTO_GENERATE_PREFIX</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:TextBox ID="txtCertificatesAutonumberPrefix" runat="server" SkinID="MediumTextBox"></asp:TextBox>
    </td>
</tr>

<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="lblMaxCertNumLengthAlwd" runat="server">MAX_CERT_NUMBER_LENGTH_ALLOWED</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:TextBox ID="txtMaxCertNumLengthAlwd" runat="server" SkinID="MediumTextBox"></asp:TextBox>
    </td>
    <td align="right" nowrap="nowrap">                    
    </td>
    <td align="left" nowrap="nowrap">                    
    </td>
</tr>

<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="lblFileLoadNotificationEmail" runat="server">FILE_LOAD_NOTIFICATION_EMAIL</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:TextBox ID="txtFileLoadNotificationEmail" runat="server" SkinID="MediumTextBox"></asp:TextBox>
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="lblClaim_Extended_Status_Entry" runat="server">Claim_Extended_Status_Entry</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moClaim_Extended_Status_Entry" runat="server" AutoPostBack="true"
                          Width="205px">
        </asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblQuestionList" runat="server">QUESTION_LIST</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moQuestionListDrop" runat="server" Width="170px">
        </asp:DropDownList>
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        &nbsp;
        <asp:Label ID="lblDEALER_SUPPORT_WEB_CLAIMS" runat="server">DEALER_SUPPORT_WEB_CLAIMS</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moDEALER_SUPPORT_WEB_CLAIMS" runat="server" AutoPostBack="true"
                          Width="205px">
        </asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblCLAIM_STATUS_FOR_EXT_SYSTEM" runat="server">CLAIM_STATUS_FOR_EXT_SYSTEM</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moExtSystemClaimStatus" runat="server" Width="170px">
        </asp:DropDownList>
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="lblGracePeriod" runat="server">GRACE_PERIOD</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:TextBox ID="txtGracePeriodMonths" runat="server" />
        <asp:Label ID="lblMonths" runat="server">MONTHS</asp:Label>
                  
        <asp:TextBox ID="txtGracePeriodDays" runat="server" />
        <asp:Label ID="lblDays" runat="server">DAYS</asp:Label>                        
    </td>

</tr>
            
<tr style="height: 1px;">
    <td colspan="4" style="height: 1px; background-color: Silver;">
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="lblContactName" runat="server">Contact_Name</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:TextBox ID="txtContactName" runat="server" Width="205px"></asp:TextBox>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblContactPhone" runat="server">Contact_Phone</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:TextBox ID="txtContactPhone" runat="server" Width="170px"></asp:TextBox>
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="lblContactPhoneExt" runat="server">Contact_Phone_Ext</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:TextBox ID="txtContactExt" runat="server" Width="205px"></asp:TextBox>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblContactFax" runat="server">Contact_Fax</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:TextBox ID="txtContactFax" runat="server" Width="170px"></asp:TextBox>
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="lblContactEmail" runat="server">Contact_Email</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:TextBox ID="txtContactEmail" runat="server" Width="205px"></asp:TextBox>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblWebAddress" runat="server">DEALER_WEB_ADDRESS</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:TextBox ID="txtWebAddress" runat="server" Width="170px"></asp:TextBox>
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="lblServLineEmail" runat="server">SERVICE_LINE_EMAIL:</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:TextBox ID="txtServLineEmail" runat="server" Width="205px" MaxLength="50"></asp:TextBox>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblServLinePhoneNum" runat="server">SERVICE_LINE_PHONE_NUMBER:</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:TextBox ID="txtServLinePhoneNum" runat="server" Width="170px" MaxLength="15"></asp:TextBox>
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="lblESCInsuranceLable" runat="server">ESC_INSURANCE_LABEL:</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:TextBox ID="txtESCInsuranceLable" runat="server" Width="205px" MaxLength="50"></asp:TextBox>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblServLineFax" runat="server">SERVICE_LINE_FAX:</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:TextBox ID="txtServLineFax" runat="server" Width="170px" MaxLength="15"></asp:TextBox>
    </td>
</tr>
<tr style="height: 1px;">
    <td colspan="4" style="height: 1px; background-color: Silver;">
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="lblIBNR_COMPUTATION_METHOD" runat="server">IBNR_COMPUTATION</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moIBNR_COMPUTATION_METHODDropd" runat="server" Width="205px">
        </asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblIBNR_FACTOR" runat="server">IBNR_FACTOR</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:TextBox ID="txtIBNR_Factor" runat="server" Width="170px"></asp:TextBox>
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="lblSTATIBNR_COMPUT_MTHD" runat="server">STAT_IBNR_COMPUTATION</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moSTATIBNR_COMPUT_MTHDDropd" runat="server" Width="205px">
        </asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblSTATIBNR_FACTOR" runat="server">STAT_IBNR_FACTOR</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:TextBox ID="txtSTATIBNR_Factor" runat="server" Width="170px"></asp:TextBox>
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="lblLAEIBNR_COMPUT_MTHD" runat="server">LAE_IBNR_COMPUTATION</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moLAEIBNR_COMPUT_MTHDDropd" runat="server" Width="205px">
        </asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblLAEIBNR_FACTOR" runat="server">LAE_IBNR_FACTOR</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:TextBox ID="txtLAEIBNR_Factor" runat="server" Width="170px"></asp:TextBox>
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="lblPayDeductible" runat="server">PAY_DEDUCTIBLE</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moPayDeductible" runat="server" Width="205px">
        </asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblAutoSelectSvcCenter" runat="server">AUTO_SELECT_SERVICE_CENTER</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moAutoSelectServiceCenter" runat="server" Width="170px">
        </asp:DropDownList>
    </td>
    <%-- Tab begin --%>
</tr>
<tr>
    <td style="height: 6px; width: 1%;" nowrap="nowrap" align="right" colspan="1" class="borderLeft">&nbsp;
    </td>
    <td style="height: 6px" width="20%">
        &nbsp;
    </td>
    <td style="height: 6px" nowrap align="right" width="1%">
        <asp:Label ID="lblUseClaimAutorization" runat="server">USE_CLAIM_AUTHORIZATION:</asp:Label>
    </td>
    <td style="height: 6px" width="20%">
        <asp:DropDownList ID="moUseClaimAutorization" runat="server" Width="170px">
        </asp:DropDownList>
    </td>
</tr>                                        
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="lblCollectDeductible" runat="server">COLLECT_DEDUCTIBLE</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moCollectDeductible" runat="server" Width="205px">
        </asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
    </td>
    <td align="left" nowrap="nowrap">
        &nbsp;
    </td>
</tr>
<tr style="height: 1px;">
    <td colspan="4" style="height: 1px; background-color: Silver;">
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="LabelConvertProdCode" runat="server" Font-Bold="false">CONVERT_PRODUCT_CODE</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="cboConvertProdCode" runat="server" Width="205px">
        </asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblBranchValidation" runat="server">Branch_Validation</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moBranchValidationDrop" runat="server" Width="170px">
        </asp:DropDownList>
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="LabelDelayFactor" runat="server">DELAY_FACTOR</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moDelayFactorDrop" runat="server" Width="205px">
        </asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblEditBranch" runat="server">EDIT_BRANCH</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moEditBranchDrop" runat="server" Width="170px">
        </asp:DropDownList>
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="LabelInstallmentFactor" runat="server">INSTALLMENT_FACTOR</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moInstallmentFactorDrop" runat="server" Width="205px">
        </asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="moRegistrationProcessLabel" runat="server" Font-Bold="false">REGISTRATION_PROCESS</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moRegistrationProcessDrop" runat="server" Width="170px">
        </asp:DropDownList>
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="moRegistrationEmailFromLabel" runat="server">REGISTRATION_EMAIL_FROM</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:TextBox ID="moRegistrationEmailFromText" runat="server" Width="205px"></asp:TextBox>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="moUseWarrantyMasterLabel" runat="server" Font-Bold="false">AUTO_PROCESS_FILES</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moAutoProcessFileDrop" runat="server" AutoPostBack="true" Width="170px">
        </asp:DropDownList>
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">&nbsp;
        <asp:Label ID="moAcceptPaymentByCheck" runat="server" Font-Bold="false">ACCEPT_PAYMENT_BY_CHECK</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moAcceptPaymentByCheckDrop" runat="server" Width="205px">
        </asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="moAutoProcessPymtFileLabel" runat="server" Font-Bold="false">AUTO_PROCESS_PAYMENT_FILES</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moAutoProcessPymtFiledrop" runat="server" Width="170px">
        </asp:DropDownList>
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="moUseIncomingSalesTaxLabel" runat="server">USE_INCOMING_SALES_TAX</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moUseIncomingSalesTaxDrop" runat="server" Width="205px">
        </asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblEnrFilePreProcess" runat="server" Font-Bold="false">ENROLLMENT_PRE_PROCESSING</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="ddlEnrFilePreProcess" runat="server" Width="170px">
        </asp:DropDownList>
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="Label3" runat="server" Font-Bold="false">USE_WARRANTY_MASTER:</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moUseWarrantyMasterDrop" runat="server" Width="205px">
        </asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblCertNumLookUpBy" runat="server" Font-Bold="false">CERT_NUMBER_LOOKUP_BY</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="ddlCertNumLookUpBy" runat="server" Width="170px">
        </asp:DropDownList>
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="LabelInsertIfMakeNotExists" runat="server" Font-Bold="false">INSERT_MAKE_IF_NOT_EXIST:</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moInsertIfMakeNotExists" runat="server" Width="205px">
        </asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblFullfileDealer" runat="server" Font-Bold="false" Visible="false">USE_FULLFILE_PROCESS</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="ddlFullfileProcess" runat="server" Width="170px" AutoPostBack="true" Visible="false">
        </asp:DropDownList>
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="lblAutoRejErrType" runat="server" Font-Bold="false">AUTO_REJ_ERR_TYPE:</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="ddlAutoRejErrType" runat="server" Width="205px" >
        </asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblReconRejRecType" runat="server" Font-Bold="false">RECON_REJ_REC_TYPE:</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="ddlReconRejRecType" runat="server" Width="205px" >
        </asp:DropDownList>
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="lblAutoGenRejPymtFile" runat="server" Font-Bold="false">AUTO_GEN_REJ_PYMT_FILE:</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="ddlAutoGenRejPymtFile" runat="server" Width="205px" >
        </asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblPymtRejRecRecon" runat="server" Font-Bold="false">PYMT_REJ_REC_RECON:</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="ddlPymtRejRecRecon" runat="server" Width="205px" >
        </asp:DropDownList>
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="lblDealerExtractPeriod" runat="server" Font-Bold="false">DEALER_EXTRACT_PERIOD:</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="ddlDealerExtractPeriod" runat="server" Width="205px">
        </asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblMaxNCRecords" runat="server" Font-Bold="false" Visible="false">MAX_NC_RECORDS</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:TextBox ID="txtMaxNCRecords" runat="server" Width="170px" Visible="false"></asp:TextBox>
    </td>
</tr>
<tr style="height: 1px;">
    <td colspan="4" style="height: 1px; background-color: Silver;">
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="Label1" runat="server" Font-Bold="false">INVOICE_BY_BRANCH</asp:Label>:
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="ddlInvByBranch" runat="server" Width="205px">
        </asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="Label2" runat="server">SEPARATED_CREDIT_NOTES</asp:Label>:
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="ddlSeparatedCN" runat="server" Width="170px">
        </asp:DropDownList>
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="moRoundCommLabel" runat="server" Font-Bold="false">ROUND_COMMISSION</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moRoundCommId" runat="server" Width="205px">
        </asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="moCancelByLabel" runat="server" Font-Bold="false">CERT_CANCEL_BY</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="DDCancelBy" runat="server" Width="170px">
        </asp:DropDownList>
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="moUseInstallmentDefnLabel" runat="server" Font-Bold="false">USER_INSTALLMENT_DEFN</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moUseInstallmentDefnId" runat="server" Width="205px">
        </asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="moProgramNameLabel" runat="server">PROGRAM_NAME</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:TextBox ID="txtProgramName" runat="server" Width="170px"></asp:TextBox>
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="lblOlitaSearch" runat="server">OLITA_SEARCH</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moOlitaSearchDrop" runat="server" Width="205px" AutoPostBack="True">
        </asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblMaxManWarr" runat="server">MAX_MANUFACTURER_WARRANTY</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:TextBox ID="txtMaxManWarr" runat="server" Width="170px"></asp:TextBox>
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="lblCancelRequestFlag" runat="server">CANCELLATION_REQUEST_FLAG</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moCancelRequestFlagDrop" runat="server" Width="205px">
        </asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblMinManWarr" runat="server">MIN_MANUFACTURER_WARRANTY</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:TextBox ID="txtMinManWarr" runat="server" Width="170px"></asp:TextBox>
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap" style="height: 7px" colspan="4">
        <asp:Label ID="lblDealerID" runat="server" Visible="False"></asp:Label>
        <asp:Label ID="moIsNewDealerLabel" runat="server" Visible="False"></asp:Label>
        <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
               runat="server" />
    </td>
</tr>
<tr style="height: 1px;">
    <td colspan="4" style="height: 1px; background-color: Silver;">
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="lblRepClaimDedTolerancePct" runat="server">REPLACEMENT_CLAIM_DEDUCTIBLE_TOLERANCE_PCT</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:TextBox ID="txtRepClaimDedTolerancePct" MaxLength="6" runat="server" Width="200px"></asp:TextBox>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblBankInfoMandatory" runat="server" Font-Bold="false">BANK_INFO_MANDATORY</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moBankInfoMandatory" runat="server" Width="170px">
        </asp:DropDownList>
    </td>
</tr>
<tr id="trVscLicenseTag" runat="server">
    <td align="right" class="borderLeft" nowrap="nowrap">
        &nbsp;<asp:Label ID="lblLicenseTagMandatory" runat="server" Font-Bold="false">LICENSE_TAG_MANDATORY</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moLicenseTagMandatory" runat="server" Width="170px">
        </asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap" style="margin-left: 80px">
        <asp:Label ID="lblplancodeinquote" runat="server" Font-Bold="False">PLAN_CODE_IN_QUOTE</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moplancodeinquote" runat="server" Width="170px">
        </asp:DropDownList>
    </td>

</tr>
            
<tr id="trVscVinRestrict" runat="server">
    <td align="right" class="borderLeft" nowrap="nowrap">
        &nbsp;<asp:Label ID="lblVinrestrictMandatory" runat="server" Font-Bold="false">VSC_VIN_RESTRICT</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moVinrestrictMandatory" runat="server" Width="170px">
        </asp:DropDownList>
    </td>
</tr>


<tr id="trLineHid1" runat="server" style="height: 1px;">
    <td colspan="4" style="height: 1px; background-color: Silver;">
    </td>
</tr>
<tr id="trHid1" runat="server">
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="lblBusinessName" runat="server" Font-Bold="false">BUSINESS_NAME</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:TextBox ID="txtBusinessName" runat="server"></asp:TextBox>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblStateTaxIdNumber" runat="server">STATE_TAX_ID_NUMBER</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:TextBox ID="txtStateTaxIdNumber" runat="server"></asp:TextBox>
    </td>
</tr>
<tr id="trHid2" runat="server">
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="lblNumbOfOtherLocations" runat="server">NUMBER_OF_OTHER_LOCATIONS</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:TextBox ID="txtNumbOfOtherLocations" runat="server" Width="55px"></asp:TextBox>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblCityTaxIdNumber" runat="server">CITY_TAX_ID_NUMBER</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:TextBox ID="txtCityTaxIdNumber" runat="server" Width="170px"></asp:TextBox>
    </td>
</tr>
<tr id="tr1" runat="server">
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="lblProdByRegion" runat="server">PRODUCT_BY_REGION</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moProdByRegion" runat="server" Width="205px">
        </asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblClaimVerfificationNumLength" runat="server">CLAIM_VERIFICATION_#_LENGTH</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:TextBox ID="txtClaimVerfificationNumLength" MaxLength="2" runat="server" Width="170px"></asp:TextBox>
    </td>
</tr>
<tr id="tr2" runat="server">
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="lblAlwupdateCancel" runat="server">ALLOW_UPDATE_CANCELLATION</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moAlwupdateCancel" runat="server" Width="205px">
        </asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblRejectaftercancel" runat="server">REJECT_AFTER_CANCELLATION</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moRejectaftercancel" runat="server" Width="170px">
        </asp:DropDownList>
    </td>
</tr>
<tr id="tr3" runat="server">
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="lblAllowfuturecancel" runat="server">ALLOW_FUTURE_CANCELLATION</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moAllowfuturecancel" runat="server" Width="205px">
        </asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblReplacementSKURequired" runat="server">REPLACEMENT_SKU_REQUIRED</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="ddlReplacementSKURequired" runat="server" Width="170px">
        </asp:DropDownList>
    </td>
</tr>
<tr id="tr4" runat="server">
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="lblIsLawsuitMandatory" runat="server">MAKE_LAWSUIT_MANDATORY</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moIsLawsuitMandatory" runat="server" Width="205px">
        </asp:DropDownList>
    </td>
    <td align="right" nowrap="noWrap">
        <asp:Label ID="moDefaultSalvageCenterLabel" runat="server">DEFAULT_SALVAGE_CENTER</asp:Label>
    </td>
    <td nowrap="noWrap">
        <input id="inputServiceCenterId" type="hidden" name="inputServiceCenterId" runat="server" />
        <input id="inputServiceCenterDesc" type="hidden" name="inputServiceCenterDesc" runat="server" />
        <asp:TextBox runat="server" ID="moDefaultSalvageCenter" SkinID="MediumTextBox" />
        <Ajax:AutoCompleteExtender ID="aCompSalvageCenter" runat="server" TargetControlID="moDefaultSalvageCenter"
                                   OnClientItemSelected="comboSelectedServiceCenter" ServiceMethod="PopulateSalvageCenterDrop"
                                   MinimumPrefixLength='1' CompletionListCssClass="completionList" CompletionListHighlightedItemCssClass="itemHighlighted"
                                   CompletionListItemCssClass="listItem">
        </Ajax:AutoCompleteExtender>
    </td> 
</tr>
<tr style="height: 1px;">
    <td colspan="4" style="height: 1px; background-color: Silver;">
    </td>
</tr>
<tr>
    <td align="right" nowrap="nowrap" class="borderLeft">&nbsp;
        <asp:Label ID="lblBillingProcessCode" runat="server">BILLING_PROCESS_CODE</asp:Label>
    </td>
    <td align="left">
        <asp:DropDownList ID="ddlBillingProcessCode" runat="server" Width="205px"></asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblBillResultExpFTPSite" runat="server">BILLING_RESULT_EXCEPTION_FILE_FTP_SITE</asp:Label>
    </td>
    <td align="left">
        <asp:DropDownList ID="ddlBillResultExpFTPSite" runat="server" Width="170px"></asp:DropDownList>
    </td>
</tr>
<tr>
    <td align="right" nowrap="nowrap" class="borderLeft">&nbsp;
        <asp:Label ID="lblBillResultNotifyEmail" runat="server">BILLING_RESULT_NOTIFICATION_EMAIL</asp:Label>
    </td>
    <td align="left">
        <asp:TextBox ID="txtBillResultNotifyEmail" MaxLength="250" runat="server" Width="200px"></asp:TextBox>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblPolicyEventNotifyEmail" runat="server">POLICY_EVENT_NOTIFICATION_EMAIL</asp:Label>
    </td>
    <td align="left">
        <asp:TextBox ID ="txtPolicyEventNotifiyEmail" MaxLength="250" runat="server" Width="200px"></asp:TextBox>
    </td>              
</tr>
<tr>
    <td align="right" nowrap="nowrap" class="borderLeft">&nbsp;
        <asp:Label ID="lblClaimAutoApprove" runat="server">CLAIM_AUTO_APPROVE</asp:Label>
    </td>
    <td align="left">
        <asp:DropDownList ID="moClaimAutoApproveDrop" runat="server" Width="205px" 
                          AutoPostBack="True">
        </asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblReuseSerialNumber" runat="server">REUSE_SERIAL_NUMBER</asp:Label>
    </td>
    <td align="left">
        <asp:DropDownList ID="moReuseSerialNumberDrop" runat="server" SkinID="MediumDropDown">
        </asp:DropDownList>
    </td>            
</tr>
<asp:Panel ID="pnlTypesRow" runat="server">
    <tr>
        <td colspan="4" align="left" valign="top" class="borderLeft">&nbsp;
            <table class="formGrid" border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px;" width="100%">
                <tr>
                    <td align="right" nowrap="nowrap" valign="top" style="padding-left: 0px;" width="10%">
                        <asp:Label ID="lblClaimTypes" runat="server">CLAIM_TYPES</asp:Label>:
                    </td> 
                    <td align="left" valign="top" style="padding-left: 0px;" width="90%">
                        <uc1:UserControlAvailableSelected ID="UserControlAvailableSelectedClaimTypes" runat="server"></uc1:UserControlAvailableSelected>
                    </td>
                </tr>
                <tr>
                    <td align="right" nowrap="nowrap" valign="top" style="padding-left: 0px;" width="10%">
                        <asp:Label ID="lblCoverageTypes" runat="server">COVERAGE_TYPES</asp:Label>:
                    </td>
                    <td align="left" valign="top" style="padding-left: 0px;" width="90%">
                        <uc1:UserControlAvailableSelected id="UserControlAvailableSelectedCoverageTypes" runat="server"></uc1:UserControlAvailableSelected>
                    </td>
                </tr>
            </table>
        </td>
    </tr>
</asp:Panel>
<tr>
    <td align="right" nowrap="nowrap" class="borderLeft">&nbsp;
        <asp:Label ID="lblRequireCustomerAMLInfo" runat="server" Font-Bold="False">REQUIRE_CUSTOMER_AML_INFO</asp:Label>
    </td>
    <td align="left">
        <asp:DropDownList ID="DDRequireCustomerAMLInfo" runat="server" Width="205px">
        </asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblMaxCommissionPercent" runat="server" Font-Bold="False">MAX_COMMISSION_PERCENT</asp:Label>
    </td>
    <td align="left">
        <asp:TextBox ID="txtMaxCommissionPercent" runat="server" Width="170px"></asp:TextBox>
    </td>           
</tr>

<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="lblUseNewBillPay" runat="server">USE_NEWBILLCOLL_SCREEN</asp:Label>
    </td>
    <td align="left">
        <asp:DropDownList ID="cboUseNewBillPay" runat="server" SkinID="SmallDropDown" Width="205px"></asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblShareCustomers" runat="server">Share_Customers</asp:Label>
    </td>
    <td align="left">
        <asp:DropDownList ID="cboShareCustomers" runat="server" SkinID="SmallDropDown" Width="205px" AutoPostBack="true"></asp:DropDownList>
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="lblCustomerIdLookUpBy" runat="server">Customer_Identity_Lookup_By</asp:Label>
    </td>
    <td align="left">
        <asp:DropDownList ID="cboCustomerIdLookUpBy" runat="server" SkinID="SmallDropDown" Width="205px"></asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblIdentificationNumberType" runat="server">Identification_Number_Type</asp:Label>
    </td>
    <td align="left">
        <asp:DropDownList ID="cboIdentificationNumberType" runat="server" SkinID="SmallDropDown" Width="205px"></asp:DropDownList>
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="lblUseQuote" runat="server">use_quote</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="cboUseQuote" runat="server" SkinID="SmallDropDown" Width="205px"></asp:DropDownList>
    </td>
</tr>
<tr>              
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="lblImeiNoUse" runat="server">USE_IMEI_NO</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="cboImeiNoUse" runat="server" Enabled="false" SkinID="MediumDropDown"></asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblContractManualVerification" runat="server">Contract_Manual_Verification</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="cboContractManualVerification" runat="server" SkinID="SmallDropDown" Width="205px"></asp:DropDownList>
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="lblClaimRecordingCheckInventory" runat="server">CLAIM_RECORDING_CHECK_INVENTORY</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="ddlClaimRecordingCheckInventory" TabIndex="39" runat="server" AutoPostBack="False"
                          SkinID="MediumDropDown">
        </asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">                   
    </td>
    <td align="left">
                   
    </td>          
</tr>
<tr id="trBenefitDlrTypeCtls1" runat="server">
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="lblSuspenseApplies" runat="server">SUSPENSE_APPLIES</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="ddlSuspenseApplies" TabIndex="39" runat="server"
                          SkinID="MediumDropDown">
        </asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblSuspensePeriod" runat="server" Font-Bold="False">SUSPENSE_PERIOD</asp:Label>
    </td>
    <td align="left">
        <asp:TextBox ID="txtSuspensePeriod" runat="server" Width="170px" onkeypress="return isNumber(event)"></asp:TextBox>
    </td>          
</tr>
<tr id="trBenefitDlrTypeCtls2" runat="server">
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="lblInvoiceCutOffDay" runat="server">INVOICE_CUTOFF_DAY</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:TextBox ID="txtInvCutOffDay" runat="server" Width="170px" onkeypress="return isNumber(event)"></asp:TextBox>                    
    </td>
    <td align="right" nowrap="nowrap">  
        <asp:Label ID="lblVoidDuration" runat="server" Font-Bold="False">VOID_DURATION</asp:Label>                  
    </td>
    <td align="left">
        <asp:TextBox ID="txtVoidDuration" runat="server" Width="170px" onkeypress="return isNumber(event)"></asp:TextBox>
    </td>          
</tr>
<tr id="trBenefitDlrTypeCtls3" runat="server">
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="lblSourceSystem" runat="server">SOURCE_SYSTEM</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="ddlSourceSystem" TabIndex="39" runat="server"
                          SkinID="MediumDropDown">
        </asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lbleBenefitCarrierCode" runat="server" Font-Bold="False">BENEFIT_CARRIER_CODE</asp:Label>
    </td>
    <td align="left">
        <asp:TextBox ID="txtBenefitCarrierCode" runat="server" Width="170px" ></asp:TextBox>
    </td>          
</tr>
<tr id="trBenefitDlrTypeCtls4" runat="server">
    <td align="right" class="borderLeft" nowrap="nowrap">
        <asp:Label ID="lblBenefitSoldToAccount" runat="server">BENEFIT_SOLD_TO_ACCOUNT</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:TextBox ID="txtBenefitSoldToAccount" runat="server" Width="170px" ></asp:TextBox>
    </td>
    <td align="right"  nowrap="nowrap">
                  
        <asp:Label ID="lblIsShipmentAllowed" runat="server">IS_RESHIPMENT_ALLOWED</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="reshipmentAllowedDrop" runat="server" 
                          Width="205px">
        </asp:DropDownList>
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        &nbsp;
        <asp:Label ID="lblIsCancelShipmentAllowed" runat="server">IS_CANCEL_SHIPMENT_ALLOWED</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="cancelShipmentAllowedDrop" runat="server"
                          Width="205px">
        </asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblCancelShipmentGracePeriod" runat="server">CANCEL_SHIPMENT_GRACE_PERIOD</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:TextBox ID="txtCancelShipmentGracePeriod" runat="server" Width="205px"></asp:TextBox>
    </td>
</tr>
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        &nbsp;
        <asp:Label ID="LabelValidateAddress" runat="server">VALIDATE_ADDRESS</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moValidateAddress" runat="server"
                          Width="205px">
        </asp:DropDownList>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblCaseProfile" runat="server">CASE_PROFILE</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="ddlCaseProfile" runat="server" SkinID="MediumDropDown"></asp:DropDownList>
    </td>
</tr>            
<tr>
    <td align="right" class="borderLeft" nowrap="nowrap">
        &nbsp;
        <asp:Label ID="lblClosecaseperiod" runat="server">CLOSE_CASE_GRACE_PERIOD</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:TextBox ID="txtClosecaseperiod" runat="server" Width="170px" onkeypress="return isNumber(event)"></asp:TextBox>
    </td>
    <td align="right" nowrap="nowrap">
        <asp:Label ID="lblShowPrevCallerInfo" runat="server">SHOW_PREV_CALLER_INFO</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="moShowPrevCallerInfo" runat="server" SkinID="MediumDropDown"></asp:DropDownList>
    </td>
    </tr>

    <tr>
        <td align="right" nowrap="nowrap">
        <asp:Label ID="lblDealerNameFlag" runat="server">DISPLAY_MASK_DOB</asp:Label>
    </td>
    <td align="left" nowrap="nowrap">
        <asp:DropDownList ID="ddlDealerNameFlag" runat="server" SkinID="MediumDropDown"></asp:DropDownList>
    </td>
    </tr>
           
<tr> <td colspan="4" class="borderLeft"> 
    <div class="dataContainer">            
        <asp:Panel ID="EditPanel_WRITE" runat="server">
            <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0" />
            <asp:HiddenField ID="hdnDisabledTab" runat="server" />
            <div id="tabs" class="style-tabs">
                <ul>
                    <li><a href="#tabmoAddressTabPanelWRITE"><asp:Label ID="Label4" runat="server" CssClass="tabHeaderText">Address</asp:Label></a></li>
                    <li><a href="#tabmoMailingAddressTabPanelWRITE"><asp:Label ID="Label6" runat="server" CssClass="tabHeaderText">MAILING_ADDRESS</asp:Label></a></li>
                    <li><a href="#tabmoServiceOrderAddressTabPanalWRITE"><asp:Label ID="Label8" runat="server" CssClass="tabHeaderText">SVC_ORDER_ADDRESS</asp:Label></a></li>
                    <li><a href="#tabmoBnakInfoTabPanelWRITE"><asp:Label ID="Label5" runat="server" CssClass="tabHeaderText">BANK_INFO</asp:Label></a></li>
                    <li><a href="#tabmoMerchantCodeTabPanelWRITE"><asp:Label ID="Label7" runat="server" CssClass="tabHeaderText">Merchant_Code</asp:Label></a></li>
                    <li><a href="#tabmoClaimCloseRulesTabPanelWRITE"><asp:Label ID="Label9" runat="server" CssClass="tabHeaderText">CLAIM_CLOSE_RULES</asp:Label></a></li>
                    <li><a href="#tabmoAttributesWRITE"><asp:Label ID="Label10" runat="server" CssClass="tabHeaderText">ATTRIBUTES</asp:Label></a></li>
                    <li><a href="#tabDealerInflationWrite"><asp:Label ID="lblDealerInflationTab" runat="server"  CssClass="tabHeaderText">DEALER_INFLATION</asp:Label></a></li>
                </ul>

                <div id="tabmoAddressTabPanelWRITE">
                    <asp:Panel ID="moAddressTabPanel_WRITE" runat="server" Width="100%">
                        <table id="tblAddress" cellspacing="4" cellpadding="4" rules="cols" width="100%"
                               height="100%" background="" border="0" class="formGrid">
                            <tr>
                                <td align="left" height="100%">
                                    <Elita:UserControlAddress ID="moaddressController" runat="server"></Elita:UserControlAddress>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>
          
                <div id="tabmoMailingAddressTabPanelWRITE">
                    <asp:Panel ID="moMailingAddressTabPanel_WRITE" runat="server" Width="100%">
                        <div id="scroller1" style="overflow: auto; width: 99.53%; height: 90%" align="center">
                            <table id="tblMAddress" height="100%" cellspacing="4" cellpadding="4" rules="cols"
                                   width="100%" background="" border="0" class="formGrid">
                                <tr>
                                    <td align="left" height="100%">
                                        <Elita:UserControlAddress ID="moMailingaddressController" runat="server"></Elita:UserControlAddress>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <%-- Tab end --%></asp:Panel>
                </div>
          
                <div id="tabmoServiceOrderAddressTabPanalWRITE">
                    <asp:Panel ID="moServiceOrderAddressTabPanal_WRITE" runat="server" Width="100%">
                        <div id="DIV1" style="overflow: auto; width: 99.53%; height: 90%" align="center">
                            <table id="TABLE4" cellspacing="4" cellpadding="4" rules="cols" width="100%" background=""
                                   height="100%" border="0" class="formGrid">
                                <tr>
                                    <td style="height: 5px" nowrap align="right" width="1%" colspan="1" rowspan="1">
                                        <asp:Label ID="lblName" runat="server" Font-Bold="false" Width="13px">NAME</asp:Label>:
                                    </td>
                                    <td width="20%">
                                        &nbsp;
                                        <asp:TextBox ID="txtName" runat="server" Width="90%"></asp:TextBox>
                                    </td>
                                    <td style="height: 5px" nowrap align="right" width="1%" colspan="1" rowspan="1">
                                        <asp:Label ID="lblOtherTaxId" runat="server" Font-Bold="false" Width="13px">TAX_ID_NUMBER</asp:Label>:
                                    </td>
                                    <td width="170px">
                                        &nbsp;
                                        <asp:TextBox ID="txtTaxId" runat="server" Width="90%"></asp:TextBox>
                                    </td>
                                    <td align="left" colspan="1" height="100%">
                                        <Elita:UserControlAddress ID="moSvcOrderAddressController" runat="server"></Elita:UserControlAddress>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <%--Tab end --%></asp:Panel>
                </div>

                <div id="tabmoBnakInfoTabPanelWRITE">
                    <asp:Panel ID="moBankInfoTabPanel_WRITE" runat="server" Width="100%">
                        <table id="Table5" height="100%" cellspacing="4" cellpadding="4" rules="cols" width="100%"
                               background="" border="0" class="formGrid">
                            <tr>
                                <td align="left" colspan="1" height="100%">
                                    <Elita:UserControlBankInfo ID="moBankInfo" runat="server" />
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </div>

                <div id="tabmoMerchantCodeTabPanelWRITE">
                    <asp:Panel ID="moMerchantCodesTabPanel_WRITE" runat="server" Width="100%">                        
                        <asp:GridView ID="moMerchantCodesDatagrid" runat="server" Width="100%" OnRowCreated="RowCreated"
                                      OnRowCommand="RowCommand" AllowPaging="True" AllowSorting="False" CellPadding="1"
                                      AutoGenerateColumns="False" SkinID="DetailPageGridView">
                            <SelectedRowStyle Wrap="True"></SelectedRowStyle>
                            <EditRowStyle Wrap="True"></EditRowStyle>
                            <AlternatingRowStyle Wrap="True"></AlternatingRowStyle>
                            <RowStyle Wrap="True"></RowStyle>
                            <Columns>
                                <asp:TemplateField HeaderText="Company_Credit_Card">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="CompanyCreditCardTypeLabel" runat="server" Text='<%# Container.DataItem("company_credit_card_type")%>'
                                                   Visible="True">
                                        </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="cboCompanyCreditCardInGrid" runat="server">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="Merchant_Code">
                                    <ItemStyle HorizontalAlign="Left" Width="60%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="MerchantCodeLabel" runat="server" Visible="True" Text='<%# Container.DataItem("merchant_code")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="MerchantCodeTextBox" runat="server" Visible="True"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="False" HeaderText="MERCHANT_CODE_ID">
                                    <ItemTemplate>
                                        <asp:Label ID="IdLabel" Text='<%# GetGuidStringFromByteArray(Container.DataItem("merchant_code_id"))%>'
                                                   runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="False" HeaderText="company_credit_card_id">
                                    <ItemTemplate>
                                        <asp:Label ID="CompanyCreditCardIDLabel" Text='<%# GetGuidStringFromByteArray(Container.DataItem("company_credit_card_id"))%>'
                                                   runat="server">
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="30px" Wrap="False">
                                    </HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="EditButton_WRITE" Style="cursor:  hand" runat="server" ImageUrl="../Navigation/images/edit.png"
                                                         CommandName="EditRecord" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:LinkButton ID="BtnCancel" runat="server" CommandName="CancelRecord" CommandArgument="<%#Container.DisplayIndex %>"
                                                        Text="Cancel"></asp:LinkButton>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField>
                                    <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="30px" Wrap="False">
                                    </HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:ImageButton Style="cursor: hand;" ID="DeleteButton_WRITE" ImageUrl="../Navigation/images/icon_delete.png"
                                                         runat="server" CommandName="DeleteRecord" CommandArgument="<%#Container.DisplayIndex %>">
                                        </asp:ImageButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Button ID="BtnSave_WRITE" runat="server" CommandName="SaveRecord" CommandArgument="<%#Container.DisplayIndex %>"
                                                    Text="Save" SkinID="PrimaryRightButton"></asp:Button>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                            <PagerStyle />
                        </asp:GridView>                                 
                    </asp:Panel>
                    <div class="btnZone">
                        <input id="HiddenDeletePromptResponse" type="hidden" runat="server" designtimedragdrop="261" />
                        <asp:Button ID="btnNewMerchantCode_WRITE" runat="server" CausesValidation="False"
                                    Text="New" SkinID="AlternateLeftButton"></asp:Button>
                    </div>
                </div>

                <div id="tabmoClaimCloseRulesTabPanelWRITE">
                    <Elita:UserControlClaimCloseRules EntityType="Dealer" runat="server" ID="ClaimCloseRules"
                                                      RequestCloseClaimData="ClaimCloseRules_RequestCloseClaimData"></Elita:UserControlClaimCloseRules>
                </div>

                <div id="tabmoAttributesWRITE">
                    <Elita:UserControlAttrtibutes runat="server" ID="AttributeValues" />
                </div>
                
                <div id ="tabDealerInflationWrite">
                  
                </div>
            </div>
        </asp:Panel>
    </div>
</td></tr>
</table>
</div>
<div class="btnZone">
    <asp:Button ID="btnApply_WRITE" runat="server" CausesValidation="False" Text="SAVE"
                SkinID="PrimaryRightButton"></asp:Button>
    <asp:Button ID="btnCopy_WRITE" runat="server" CausesValidation="False" Text="New_With_Copy"
                SkinID="AlternateRightButton"></asp:Button>
    <asp:Button ID="btnUndo_WRITE" runat="server" CausesValidation="False" Text="UNDO"
                SkinID="AlternateRightButton"></asp:Button>
    <asp:Button ID="btnBack" runat="server" CausesValidation="False" Text="BACK" SkinID="AlternateLeftButton">
    </asp:Button>
    <asp:Button ID="btnNew_WRITE" runat="server" CausesValidation="False" Text="New"
                SkinID="AlternateLeftButton"></asp:Button>
    <asp:Button ID="btnDelete_WRITE" runat="server" CausesValidation="False" Text="Delete"
                SkinID="CenterButton"></asp:Button>
    <input id = "HiddenDelDeletePromptResponse" type ="Hidden" runat="server" designtimedragdrop="261" />
     
</div>
<script type="text/javascript" language="javascript">
    function comboSelectedServiceCenter(source, eventArgs) {
        var inpId = document.getElementById('<%=inputServiceCenterId.ClientID%>');
        var inpDesc = document.getElementById('<%=inputServiceCenterDesc.ClientID%>');
        inpId.value = eventArgs.get_value();
        inpDesc.value = eventArgs.get_text();
        //                alert(" Key : " + eventArgs.get_text() + "  Value :  " + eventArgs.get_value());
    }

    $(document).ready(function () {
            
        $('#<%= ddlSuspenseApplies.ClientId%>').change(function () {
            var rbvalue = $('#ctl00_BodyPlaceHolder_ddlSuspenseApplies').val();
            // alert(rbvalue);

            if (rbvalue.toUpperCase() == "YESNO-Y") {
                $("#ctl00_BodyPlaceHolder_txtSuspensePeriod").show(); 
                $("#ctl00_BodyPlaceHolder_lblSuspensePeriod").show();

            }
            else {
                $("#ctl00_BodyPlaceHolder_txtSuspensePeriod").hide();
                $("#ctl00_BodyPlaceHolder_lblSuspensePeriod").hide();

            }


        });

        <%-- Used for Use Claim Authorization--%>
        $.fn.validateUseClaimAuthorization = function () {
            var rbvalue = $('#ctl00_BodyPlaceHolder_moUseClaimAutorization').val();
            var cancelShipmentAllowedDropvalue = $('#ctl00_BodyPlaceHolder_cancelShipmentAllowedDrop').val();
            if (rbvalue == "20b65efa-de52-e654-e034-0003ba5d2202") {
                $("#ctl00_BodyPlaceHolder_cancelShipmentAllowedDrop").attr('disabled', false);
                $("#ctl00_BodyPlaceHolder_reshipmentAllowedDrop").attr('disabled', false);
                var cancelShipmentAllowedDropvalue = $('#ctl00_BodyPlaceHolder_cancelShipmentAllowedDrop').val();

                if (cancelShipmentAllowedDropvalue.toUpperCase() == "YESNO-Y") {
                    $("#ctl00_BodyPlaceHolder_txtCancelShipmentGracePeriod").attr('disabled', false);;
                }
                else {
                    $("#ctl00_BodyPlaceHolder_txtCancelShipmentGracePeriod").attr('disabled', true);
                }

            }
            else {
                $("#ctl00_BodyPlaceHolder_cancelShipmentAllowedDrop").attr('disabled', true);
                $("#ctl00_BodyPlaceHolder_reshipmentAllowedDrop").attr('disabled', true);
                $("#ctl00_BodyPlaceHolder_cancelShipmentAllowedDrop").prop('selectedIndex', 0);
                $("#ctl00_BodyPlaceHolder_reshipmentAllowedDrop").prop('selectedIndex', 0);
                $("#ctl00_BodyPlaceHolder_txtCancelShipmentGracePeriod").attr('disabled', true);

            }
        }
        <%-- Call on load--%>  
        $.fn.validateUseClaimAuthorization();

        <%--  Use Claim Authorization Change Event--%> 
        $('#<%= moUseClaimAutorization.ClientId%>').change(function () {   
            $.fn.validateUseClaimAuthorization();
        });



        <%-- Used for Cancel Shipment logic--%>       
        $.fn.validateCancelShipment = function () {
            var rbvalue = $('#ctl00_BodyPlaceHolder_cancelShipmentAllowedDrop').val();
               
            if (rbvalue.toUpperCase() == "YESNO-Y") {
                   
                $("#ctl00_BodyPlaceHolder_txtCancelShipmentGracePeriod").attr('disabled', false);
            }
            else {
                $("#ctl00_BodyPlaceHolder_txtCancelShipmentGracePeriod").val("");
                $("#ctl00_BodyPlaceHolder_txtCancelShipmentGracePeriod").attr('disabled', true);

            }

        }
        <%-- Call on load--%>  
        $.fn.validateCancelShipment();
        <%-- Used for Cancel Shipment Change event logic--%>  
           
        $('#<%=cancelShipmentAllowedDrop.ClientId%>').change(function () {
            $.fn.validateCancelShipment();
        });


    });

    function isNumber(evt) {
        evt = (evt) ? evt : window.event;
        var charCode = (evt.which) ? evt.which : evt.keyCode;
        if (charCode > 31 && (charCode < 48 || charCode > 57)) {
            return false;
        }

        return true;
    }
    <%--function EnableDisableSuspensePeriod()
        {
            var isSuspenseAllowed = document.get('<%=ddlSuspenseApplies.ClientID%>');
            alert(isSuspenseAllowed.value());
            if(isSuspenseAllowed)
            {
                $("#ctl00_BodyPlaceHolder_txtDealerCode").style.visibility = 'visible';
            }
            else
            {
                $("#ctl00_BodyPlaceHolder_txtDealerCode").style.visibility = 'hidden';
            }
        }--%>
        
</script>
</asp:Content>
