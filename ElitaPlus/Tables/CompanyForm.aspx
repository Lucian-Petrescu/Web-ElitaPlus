<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UserControlAddress" Src="../Common/UserControlAddress.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UserControlAvailableSelected" Src="../Common/UserControlAvailableSelected.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlClaimCloseRules" Src="~/Common/UserControlClaimCloseRules.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlAttrtibutes" Src="~/Common/UserControlAttrtibutes.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CompanyForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.CompanyForm"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" Theme="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js">        function TABLE1_onclick() {

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePageMethods="true" ScriptMode="Auto">
        <Scripts>
            <asp:ScriptReference Path="~/Navigation/scripts/ComunaSuggest.js" />
        </Scripts>
    </asp:ScriptManager>
    <!--Start Header-->
    <table id="moTableOuter" class="searchGrid" border="0">
        <tr>
            <td valign="top" align="center">
                <asp:Panel ID="WorkingPanel" runat="server">
                    <table id="tblMain1" border="0">
                        <tr>
                            <td valign="top">
                                <asp:Panel ID="EditPanel_WRITE" runat="server">
                                    <table valign="top">
                                        <tr>
                                            <td colspan="4">
                                                <input id="HiddenSaveChangesPromptResponse" type="hidden" size="14" name="HiddenSaveChangesPromptResponse"
                                                    runat="server" />
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="LabelCode" runat="server" Font-Bold="false">Code</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="TextboxCode" TabIndex="1" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                            </td>
                                            <td align="right" colspan="1">
                                                <asp:Label ID="LabelUniqueCertificateNumber" runat="server" Font-Bold="False">UNIQUE_CERTIFICATE_NUMBERS</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="CboUniqueCertificateNumberID" runat="server" SkinID="SmallDropDown"
                                                    TabIndex="6" AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="LabelDescription" runat="server" Font-Bold="false">Description</asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextboxDescription" TabIndex="2" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                            </td>
                                            <td align="right" colspan="1">
                                                <asp:Label ID="LabelUniqueCertNumberEffDate" runat="server" Font-Bold="False">UNIQUE_CERT_NUMBER_EFFECTIVE_DATE</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="TextboxUniqueCertNumberEffDate" TabIndex="6" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                                <asp:ImageButton ID="ImgUniqueCertNumberEffDate" ImageAlign="AbsMiddle" runat="server"
                                                    ImageUrl="~/App_Themes/Default/Images/calendar.png"></asp:ImageButton>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="LabelTaxIdNumber" runat="server" Font-Bold="false">Tax_Id_Number</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="TextboxTaxIdNumber" TabIndex="3" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="LabelBusinessCountry" runat="server" Font-Bold="false">Business_Country</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="cboBusinessCountryId" TabIndex="6" runat="server" SkinID="SmallDropDown">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="LabelCompanyGroup" runat="server" Font-Bold="false">COMPANY_GROUP</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="cboCompanyGrpID" TabIndex="4" runat="server" SkinID="SmallDropDown">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="LabelLanguage" runat="server" Font-Bold="false">Language</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="cboLanguageId" TabIndex="7" runat="server" SkinID="SmallDropDown">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="LabelAcctCompany" runat="server">ACCT_COMPANY</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="cboAcctCompany" TabIndex="5" runat="server" SkinID="SmallDropDown">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblEUMember" runat="server">EU_MEMBER</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="cboEUMemberId" runat="server" TabIndex="8" SkinID="SmallDropDown">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Labeltime_zone_name" runat="server" Font-Bold="False">TIME_ZONE_NAME</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="cboTimeZoneName" runat="server" TabIndex="5" SkinID="SmallDropDown">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right" colspan="1" rowspan="1" style="width: 218px; height: 1px">
                                                <asp:Label ID="LabelComapnyType" runat="server" Font-Bold="false">COMPANY_TYPE</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="cboCompanyType" TabIndex="9" runat="server" SkinID="SmallDropDown">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" colspan="1">
                                                <asp:Label ID="LabelBILLING_BY_DEALER" runat="server" Font-Bold="False">BILLING_BY_DEALER</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="cboBilling_by_dealer" runat="server" TabIndex="5" SkinID="SmallDropDown">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblFtpSite" runat="server" Font-Bold="False">FTP_SITE</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlFtpSiteList" runat="server" TabIndex="9" SkinID="SmallDropDown">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="Address1Label" runat="server" Font-Bold="false">Address1</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="Address1Text" TabIndex="10" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="Address2Label" runat="server" Font-Bold="false">Address2</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="Address2Text" TabIndex="15" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="CityLabel" runat="server" Font-Bold="false">City</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="CityText" TabIndex="11" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="StateLabel" runat="server" Font-Bold="false">State_Province</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="moRegionDrop_WRITE" TabIndex="16" runat="server" SkinID="SmallDropDown">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="ZipLabel" runat="server" Font-Bold="false">Zip</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="ZipText" TabIndex="12" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="CountryLabel" runat="server" Font-Bold="false">Country</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="moCountryDrop_WRITE" TabIndex="17" runat="server" SkinID="SmallDropDown"
                                                    AutoPostBack="True">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="LabelPhone1" runat="server" Font-Bold="false">Phone</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="TextboxPhone" TabIndex="13" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="LabelEmail" runat="server" Font-Bold="false">Email</asp:Label>
                                            </td>
                                            <td align="right">
                                                <asp:TextBox ID="TextboxEmail" TabIndex="18" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="LabelFax" runat="server" Font-Bold="false">Fax</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="TextboxFax" TabIndex="14" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                            </td>
                                            <td align="right"></td>
                                            <td></td>
                                        </tr>
                                        <tr>
                                            <td align="right" colspan="1" rowspan="1">
                                                <asp:Label ID="LabelUseZipDistrict" runat="server" Text="USE_ZIP_DISTRICTS"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="cboUseZipDistictId" TabIndex="19" runat="server" SkinID="SmallDropDown">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="LabelClaimNumbFormatId" runat="server" Font-Bold="false">Claim_Number_Format</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="cboClaimNumbFormatId" TabIndex="24" runat="server" SkinID="SmallDropDown">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="LabelUPR_USES_WP" runat="server" Font-Bold="false">UPR_USES_WP</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="cboUPR_USES_WP" TabIndex="20" runat="server" SkinID="SmallDropDown">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="labelAddlDAC" runat="server" Font-Bold="false">ADDL_DAC</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="cboAddlDAC" TabIndex="25" runat="server" SkinID="SmallDropDown">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" colspan="1" rowspan="1">
                                                <asp:Label ID="LabelSalutation" runat="server" Font-Bold="false">Salutation</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="cboSalutationId" TabIndex="21" runat="server" SkinID="SmallDropDown">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right" colspan="1" rowspan="1">
                                                <asp:Label ID="LabelInvoiceMethod" runat="server" Font-Bold="false">Invoice_method</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="cboInvoiceMethodId" TabIndex="26" runat="server" SkinID="SmallDropDown">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" colspan="1" rowspan="1">
                                                <asp:Label ID="LabelAutDetailRqrd" runat="server">AUTH_DETAIL_RQRD</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="cboAuthDetailRqrdId" TabIndex="22" runat="server" SkinID="SmallDropDown">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right" colspan="1" rowspan="1">
                                                <asp:Label ID="Label4" runat="server" Font-Bold="false">CLIP_METHOD:</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="cboCLIPMethod" TabIndex="27" runat="server" SkinID="SmallDropDown">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" colspan="1" rowspan="1">
                                                <asp:Label ID="labelMasterClaim" runat="server" Font-Bold="false">MASTER_CLAIM_PROCESSING</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="cboMasterClaimID" TabIndex="23" runat="server" SkinID="SmallDropDown">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="LabelAutoProcessId" runat="server" Font-Bold="false">AUTO_PROCESS_FILES</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="cboAutoprocessid" TabIndex="27" runat="server" SkinID="SmallDropDown">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right" colspan="1" rowspan="1">
                                                <asp:Label ID="lblSvcOrdersByDealer" runat="server" Font-Bold="False">SVC_ORDERS_BY_DEALER</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="cboSvcOrdersByDealerId" TabIndex="23" runat="server" SkinID="SmallDropDown">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right" colspan="1" rowspan="1">
                                                <asp:Label ID="LabelUseRecoveries" runat="server" Text="Use_Recoveries"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="cboUseRecoveries" TabIndex="27" runat="server" SkinID="SmallDropDown">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblReportCommTax" runat="server">REPORT_COMMISSION_TAX</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="ddlReportCommTax" TabIndex="23" runat="server" SkinID="SmallDropDown">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblRequireItemDescription" runat="server" Text="REQUIRES_ITEM_DESCRIPTION"></asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="cboRequireItemDescription" TabIndex="27" runat="server" SkinID="SmallDropDown">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblComputeTaxBased" runat="server">COMPUTE_TAX_BASED</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="cboComputeTaxBased" runat="server" TabIndex="23" SkinID="SmallDropDown">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblPoliceRptForLoss" runat="server">POLICE_RPT_FOR_LOSS:</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="cboPoliceRptForLoss" TabIndex="27" runat="server" SkinID="SmallDropDown">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblTransferOfOwnership" runat="server">TRANSFER_OF_OWNERSHIP</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="cboTransferOfOwnership" runat="server" TabIndex="23" SkinID="SmallDropDown">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="lblRequiresAgntCd" runat="server">Requires_Agent_Code</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="cboReqAgentCode" runat="server" TabIndex="27" SkinID="SmallDropDown"
                                                    AutoPostBack="true">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="LabelRefundToleranceAmt" runat="server" Font-Bold="false">Refund_Tolerance_Amt</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="TextboxRefundToleranceAmount" TabIndex="23" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="LabelMaxFollowupDays" runat="server" Font-Bold="false">Max_FollowUp_Days</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:TextBox ID="TextboxMaxFollowupDays" TabIndex="27" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="LabelDaysToCloseClaim" runat="server" Font-Bold="false">Days_To_Close_Claim</asp:Label>
                                            </td>
                                            <td align="left" valign="top">
                                                <asp:TextBox ID="TextboxDaysToCloseClaim" TabIndex="23" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                            </td>
                                            <td valign="middle" align="right" colspan="1" rowspan="1">
                                                <asp:Label ID="LabelDefaultFollowupDays" runat="server" Font-Bold="false">Default_FollowUp_days</asp:Label>
                                            </td>
                                            <td valign="middle" align="left" colspan="1" rowspan="1">
                                                <asp:TextBox ID="TextboxDefaultFollowupDays" TabIndex="27" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="LabelCLAIM_NUMBER_OFFSET" runat="server" Font-Bold="false">CLAIM_NUMBER_OFFSET</asp:Label>
                                            </td>
                                            <td align="left" valign="top">
                                                <asp:TextBox ID="TextboxClaimNumberOffset" TabIndex="23" runat="server" SkinID="SmallTextBox"
                                                    MaxLength="2"></asp:TextBox>
                                            </td>
                                            <td align="right">
                                                <asp:Label ID="LabelOverride_WarrantyPriceid" runat="server" Font-Bold="false">OVERRIDE_WARRANTYPRICE_CHECK</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="cboOverride_WarrantyPriceid" TabIndex="27" runat="server" SkinID="SmallDropDown">
                                                </asp:DropDownList>
                                            </td>
                                            <td colspan="2"></td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="lblREQ_CUSTOMER_LEGAL_INFO_ID" runat="server" Text="REQ_CUSTOMER_LEGAL_INFO_ID"></asp:Label>
                                            </td>
                                            <td align="left" valign="top">
                                                <asp:DropDownList ID="cboREQ_CUSTOMER_LEGAL_INFO_ID" runat="server" TabIndex="23"
                                                    SkinID="MediumDropDown">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right" nowrap="nowrap">
                                                <asp:Label ID="lblCertNumLookUpBy" runat="server" Font-Bold="false">CERT_NUMBER_LOOKUP_BY</asp:Label>
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:DropDownList ID="cboCertNumLookUpBy" runat="server" SkinID="SmallDropDown" TabIndex="27">
                                                </asp:DropDownList>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="LabelUsePreInvProcess" runat="server" Font-Bold="false">USE_PRE_INVOICE_PROCESS</asp:Label>
                                            </td>
                                            <td align="left" valign="top">
                                                <asp:DropDownList ID="ddlUsePreInvProcess" runat="server" TabIndex="23" SkinID="MediumDropDown"
                                                    onChange="return enablePreInvControls();">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right" nowrap="nowrap">
                                                <asp:Label ID="LabelSCPreInvWP" runat="server" Font-Bold="false">SC_PRE_INV_WAITING_PERIOD</asp:Label>
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <asp:TextBox ID="txtSCPreINVWP" TabIndex="27" runat="server" SkinID="SmallTextBox"
                                                    MaxLength="2"></asp:TextBox>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="right">
                                                <asp:Label ID="LabelCertNumberFormat" runat="server" Font-Bold="false">Certificate_Number_Format</asp:Label>
                                            </td>
                                            <td align="left">
                                                <asp:DropDownList ID="cboCertNumberFormat" TabIndex="24" runat="server" SkinID="SmallDropDown">
                                                </asp:DropDownList>
                                            </td>
                                            <td align="right" nowrap="nowrap">
                                                <%--<asp:Label ID="lblAllowPeriodMileageValidation" runat="server">Allow_Period_&_Mileage_Validation</asp:Label>--%>
                                            </td>
                                            <td align="left" nowrap="nowrap">
                                                <%--<asp:DropDownList ID="ddlPeriodMileageVal" runat="server"  SkinID="MediumDropDown">
                                                </asp:DropDownList>--%>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 10px" valign="top" align="left" colspan="4">
                                                <uc1:UserControlAvailableSelected ID="UserControlAvailableSelectedCountries" runat="server"></uc1:UserControlAvailableSelected>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 10px" valign="top" align="left" colspan="4"></td>
                                        </tr>
                                        <tr>
                                            <td colspan="4">
                                                <table border="0" width="100%">
                                                    <tr>
                                                        <td>
                                                            <asp:Panel ID="Panel1" runat="server">
                                                                <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0" />
                                                                <div id="tabs" class="style-tabs" style="border: none;">
                                                                    <ul style="text-align: center; background-color: white; border: none;">
                                                                        <li><a href="#tabsClaimCloseRules">
                                                                            <asp:Label ID="Label1" runat="server" CssClass="tabHeaderText">CLAIM_CLOSE_RULES</asp:Label></a></li>
                                                                        <li><a href="#tabsAttributes">
                                                                            <asp:Label ID="Label6" runat="server" CssClass="tabHeaderText">ATTRIBUTES</asp:Label></a></li>

                                                                    </ul>

                                                                    <div id="tabsClaimCloseRules" style="text-align: center; background: #d5d6e4;">
                                                                        <asp:Panel ID="moClaimCloseRulesTabPanel_WRITE" runat="server" Width="100%">
                                                                            <Elita:UserControlClaimCloseRules EntityType="Company" runat="server" ID="ClaimCloseRules"
                                                                                RequestCloseClaimData="ClaimCloseRules_RequestCloseClaimData"></Elita:UserControlClaimCloseRules>
                                                                        </asp:Panel>
                                                                    </div>

                                                                    <div id="tabsAttributes" style="text-align: center; background: #d5d6e4;">
                                                                        <Elita:UserControlAttrtibutes runat="server" ID="AttributeValues" />
                                                                    </div>
                                                                </div>
                                                            </asp:Panel>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 20px" valign="top" align="left" colspan="4"></td>
                                        </tr>
                                        <tr>
                                            <td valign="top" align="left" colspan="4">&nbsp;
                                                <asp:Label ID="LabelLegalDisclaimer" runat="server" Font-Bold="false">Legal_Disclaimer</asp:Label>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="4">
                                                <asp:TextBox ID="TextboxLegalDisclaimer" TabIndex="180" runat="server" Width="98%"
                                                    Height="60px" TextMode="MultiLine" Rows="8"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 1px" valign="top">&nbsp;
                                <asp:Button ID="btnAccCloseDates" TabIndex="210" runat="server" SkinID="AlternateRightButton"
                                    Text="ACCOUNTING_CLOSING_DATES" Visible="False"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <div class="btnZone" width="70%">
        <table width="100%">
            <tr>
                <td width="50%">
                    <asp:Button ID="btnBack" TabIndex="185" runat="server" SkinID="AlternateLeftButton"
                        Text="Back"></asp:Button>
                    <asp:Button ID="btnNew_WRITE" TabIndex="200" runat="server" Text="New" SkinID="AlternateLeftButton"></asp:Button>
                    <asp:Button ID="btnCopy_WRITE" TabIndex="205" runat="server" Text="NEW_WITH_COPY"
                        CausesValidation="False" SkinID="AlternateLeftButton"></asp:Button>
                    <asp:Button ID="btnUndo_Write" TabIndex="195" runat="server" Text="Undo" SkinID="AlternateLeftButton" />
                    <asp:Button ID="btnDelete_WRITE" TabIndex="210" runat="server" Text="Delete" SkinID="AlternateLeftButton"></asp:Button>
                </td>
                <td align="right" width="30%">
                    <asp:Button ID="btnSAVE_WRITE" TabIndex="190" runat="server" Text="Save" SkinID="PrimaryRightButton" />
                </td>
                <td width="10%"></td>
            </tr>
        </table>
    </div>
    <script>
        function resizeForm(item) {
            var browseWidth, browseHeight;

            if (document.layers) {
                browseWidth = window.outerWidth;
                browseHeight = window.outerHeight;
            }
            if (document.all) {
                browseWidth = document.body.clientWidth;
                browseHeight = document.body.clientHeight;
            }

            if (screen.width == "800" && screen.height == "600") {
                newHeight = browseHeight - 350;
            }
            else {
                newHeight = browseHeight - 370;
            }

            item.style.height = String(newHeight) + "px";

            item.style.width = String(browseWidth - 80) + "px";

        }

        function enablePreInvControls() {
            var ddl = document.getElementById("ctl00_SummaryPlaceHolder_ddlUsePreInvProcess");
            if (ddl.options[ddl.selectedIndex].text == "Yes") {

                document.getElementById("ctl00_SummaryPlaceHolder_LabelSCPreInvWP").style.display = "block";
                document.getElementById("ctl00_SummaryPlaceHolder_txtSCPreINVWP").style.display = "block";
            }
            else {

                document.getElementById("ctl00_SummaryPlaceHolder_LabelSCPreInvWP").style.display = "none";
                document.getElementById("ctl00_SummaryPlaceHolder_txtSCPreINVWP").style.display = "none";
            }

            return false;
        };

    </script>
</asp:Content>
