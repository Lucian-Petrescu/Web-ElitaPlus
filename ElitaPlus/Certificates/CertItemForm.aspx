<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CertItemForm.aspx.vb"
    Theme="Default" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Certificates.CertItemForm" 
    EnableSessionState="True" MasterPageFile="../Navigation/masters/ElitaBase.Master" %>

<%@ Register TagPrefix="mytab" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="Elita" TagName="ProtectionAndEventDetails" Src="~/Common/ProtectionAndEventDetails.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlWizard" Src="~/Common/UserControlWizard.ascx" %>
<%@ Register TagPrefix="Elita" Assembly="Assurant.ElitaPlus.WebApp" Namespace="Assurant.ElitaPlus.ElitaPlusWebApp" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <style type="text/css">
        .FLATTEXTBOX
        {
        }
    </style>
    <style type="text/css">
        .ModalBackground
        {
            background-color: Gray;
            filter: alpha(opacity=50);
            opacity: 0.5; 
        }
        .black_show
        {
            cursor: wait;
            position: absolute;
            top: 0%;
            left: 0%;
            background-color: #777777;
            z-index: 1001;
            -moz-opacity: 0.8;
            opacity: .75;
            filter: alpha(opacity=90);
        }
    </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <Elita:ProtectionAndEventDetails ID="moProtectionAndEventDetails" runat="server" />
</asp:Content>
<asp:Content ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.6.1.min.js" />
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto">
        <Scripts>
            <asp:ScriptReference Path="~/Navigation/scripts/ComunaSuggest.js" />
        </Scripts>
    </asp:ScriptManager>
    <div class="dataContainer">
        <div class="stepWizBox">
            <Elita:UserControlWizard runat="server" ID="WizardControl">
                <Steps>
                    <Elita:StepDefinition StepNumber="1" StepName="DATE_OF_INCIDENT" />
                    <Elita:StepDefinition StepNumber="2" StepName="COVERAGE_DETAILS" IsSelected="true" />
                    <Elita:StepDefinition StepNumber="3" StepName="LOCATE_SERVICE_CENTER" />
                    <Elita:StepDefinition StepNumber="4" StepName="CLAIM_DETAILS" />
                    <Elita:StepDefinition StepNumber="5" StepName="SUBMIT_CLAIM" />
                </Steps>
            </Elita:UserControlWizard>
        </div>
    </div>
    <div id="ModalCancel" class="overlay">
        <div id="light" class="overlay_message_content">
            <p class="modalTitle">
            <table width="525"><tr><td align="left">
                <asp:Label ID="lblModalTitle" runat="server" Text="CONFIRM"></asp:Label></td><td align="right">
                <a href="javascript:void(0)" onclick="hideModal('ModalCancel');">
                    <img id="Img1" src="~/App_Themes/Default/Images/icon_modalClose.png" runat="server"
                        width="16" height="18" align="right"/></a></td></tr></table></p>
            <table class="formGrid" cellspacing="0" cellpadding="0" border="0" width="525">
                <tbody>
                    <tr>
                        <td align="right">
                            <img id="imgMsgIcon" name="imgMsgIcon" width="28" runat="server" src="~/App_Themes/Default/Images/dialogue_confirm.png"
                                height="28" />
                        </td>
                        <td id="tdModalMessage" colspan="2" runat="server">
                            <asp:Label ID="lblCancelMessage" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td id="tdBtnArea" nowrap="nowrap" runat="server" colspan="2">
                            <input id="btnModalCancelYes" class="primaryBtn floatR" runat="server" type="button"
                                value="Yes" />
                            <input id="Button1" class="popWindowAltbtn floatR" runat="server" type="button" value="No"
                                onclick="hideModal('ModalCancel');" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div id="fade" class="black_overlay">
        </div>
    </div>
    <div class="container">
        <div id="contentZoneHome">
            <div class="dataContainer">
                <h2 class="dataGridHeader">
                    Coverage Details</h2>
                <asp:Panel ID="EditPanel_WRITE" runat="server" Width="100%">
                    <div class="stepformZone">
                        <table border="0" class="formGrid" cellpadding="0" cellspacing="0">
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelBeginDate" runat="server" Font-Bold="False">Begin_Date</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxBeginDate" TabIndex="2" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelInvNum" runat="server" Font-Bold="False">Invoice_Number</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextboxInvNum" runat="server" MaxLength="30" SkinID="MediumTextBox"
                                        TabIndex="15"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelEndDate" runat="server" Font-Bold="False">End_Date</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxEndDate" TabIndex="5" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelDeductibleBasedOn" runat="server" Font-Bold="False">COMPUTE_DEDUCTIBLE_BASED_ON</asp:Label>:
                                </td>
                                <td>
                                    <asp:TextBox ID="TextboxDeductibleBasedOn" runat="server" TabIndex="17" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelDateAdded" runat="server" Font-Bold="False">Date_Added</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxDateAdded" TabIndex="7" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelDeductible" runat="server" Font-Bold="False">DEDUCTIBLE</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextboxDeductible" runat="server" SkinID="MediumTextBox" TabIndex="17"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelRiskTypeId" runat="server" Font-Bold="False">Risk_Type</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:DropDownList ID="cboRiskTypeId" runat="server" SkinID="MediumDropDown" TabIndex="9">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="TextboxRiskType" runat="server" SkinID="MediumTextBox" TabIndex="30"></asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelDeductiblePercent" runat="server" Font-Bold="False">DEDUCTIBLE_PERCENT</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextboxDeductiblePercent" runat="server" SkinID="MediumTextBox"
                                        TabIndex="20"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelCoverageType" runat="server" Font-Bold="False">Coverage_Type</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxCoverageType" runat="server" SkinID="MediumTextBox" TabIndex="11"></asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="labelSKU" runat="server" Font-Bold="False">SKU_NUMBER</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextboxSKU" runat="server" SkinID="MediumTextBox" TabIndex="20"
                                        Width="75%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelMethodOfRepair" runat="server" Font-Bold="False">METHOD_OF_REPAIR</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:DropDownList ID="cboMethodOfRepair" runat="server" AutoPostBack="true" TabIndex="13"
                                        SkinID="MediumDropDown">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="TextboxMethodOfRepair" runat="server" TabIndex="14" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelReinsuranceStatus" runat="server" Font-Bold="False">REINSURANCE_STATUS</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxReinsuranceStatus" runat="server" SkinID="MediumTextBox" TabIndex="14"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelProductCode" runat="server" Font-Bold="False">Product_Code</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxProductCode" runat="server" SkinID="MediumTextBox" TabIndex="16"></asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right">
                                   <asp:Label ID="LabelReinsRejectReason" runat="server" Font-Bold="False">REINS_REJECT_REASON</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextboxReinsRejectReason" runat="server" SkinID="MediumTextBox" TabIndex="16"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelLiabilityLimit" runat="server" Font-Bold="False">LIABILITY_LIMIT</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxLiabilityLimit" runat="server" SkinID="MediumTextBox" TabIndex="19"></asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="labelRepairDiscountPct" runat="server" Font-Bold="False">REPAIR_DISCOUNT_PCT</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxRepairDiscountPct" runat="server" SkinID="SmallTextBox" TabIndex="19"></asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="labelReplacementDiscountPct" runat="server" Font-Bold="False">REPLACEMENT_DISCOUNT_PCT</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxReplacementDiscountPct" runat="server" SkinID="SmallTextBox"
                                        TabIndex="19"></asp:TextBox>
                                </td>
                                <td nowrap="nowrap" align="right">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </asp:Panel>
            </div>
            <asp:Panel ID="pnlDeviceInfo" runat="server">
                <div class="dataContainer">
                    <h2 class="dataGridHeader" runat="server" id="headerDeviceInfo">
                        DEVICE_INFORMATION</h2>
                    <div class="stepformZone">
                        <table border="0" class="formGrid" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    &nbsp;
                                </td>
                                <td nowrap="nowrap">
                                    <asp:Label ID="lblEnrolledDeviceInfo" runat="server" Text="ENROLLED_DEVICE_INFO"
                                        Visible="false" Font-Bold="true"></asp:Label>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    &nbsp;
                                </td>
                                <td nowrap="nowrap">
                                    <asp:Label ID="lblClaimedEquipment" runat="server" Text="CLAIMED_DEVICE_INFO" Visible="false"
                                        Font-Bold="true"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelMakeId" runat="server" Text="MAKE" Font-Bold="False"></asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:DropDownList ID="cboManufacturerId" TabIndex="3" runat="server" SkinID="MediumDropDown">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="TextboxManufacturer" TabIndex="4" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblClaimedMake" runat="server" Text="MAKE" Visible="False"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlClaimedManuf" Visible="false" TabIndex="3" runat="server"
                                        SkinID="MediumDropDown">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtClaimedmake" runat="server" SkinID="SmallTextBox" TabIndex="17"
                                        Visible="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelModel" runat="server" Font-Bold="False" Text="MODEL" EnableTheming="True"></asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxModel" TabIndex="6" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblClaimedModel" runat="server" Text="MODEL" Visible="False"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtClaimedModel" runat="server" SkinID="SmallTextBox" TabIndex="20"
                                        Visible="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelDealerItemDesc" runat="server" Text="DESCRIPTION" Font-Bold="False"></asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxDealerItemDesc" TabIndex="8" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    &nbsp;
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="LabelClaimDesc" runat="server" Text="Description" Visible="False"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtClaimedDescription" TabIndex="8" runat="server" SkinID="MediumTextBox"
                                        Visible="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="LabelYear" runat="server" Text="YEAR" Font-Bold="False"></asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxYear" runat="server" SkinID="MediumTextBox" TabIndex="10"></asp:TextBox>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblClaimedSKU" runat="server" Text="SKU_NUMBER" Visible="False"></asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="ddlClaimedSkuNumber" Visible="false" TabIndex="3" runat="server"
                                        SkinID="MediumDropDown">
                                    </asp:DropDownList>
                                    <asp:TextBox ID="txtClaimedSKu" TabIndex="8" runat="server" SkinID="MediumTextBox"
                                        Visible="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelSerialNumberIMEI" runat="server">SERIAL_NUMBER</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxSerialNumber" TabIndex="12" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="LabelClaimSerialNumber" runat="server" Text="SERIAL_NUMBER" Visible="False"></asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="txtClaimSerialNumber" TabIndex="12" runat="server" SkinID="MediumTextBox"
                                        Visible="False"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelIMEINumber" runat="server" Text="IMEI_NUMBER"> </asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxIMEINumber" TabIndex="12" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>

                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelClaimIMEINumber" runat="server" Text="IMEI_NUMBER" Font-Bold="False" Width="124px"></asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="txtClaimIMEINumber" TabIndex="12" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                                <td align="right" nowrap="nowrap">
                                </td>
                                <td>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlVehicleInfo" runat="server">
                <div class="dataContainer">
                    <h2 class="dataGridHeader">
                        Vehicle Information</h2>
                    <div class="stepformZone">
                        <table border="0" class="formGrid" cellpadding="0" cellspacing="0" width="100%">
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelOdometer" runat="server" Font-Bold="False">ODOMETER:</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxOdometer" runat="server" SkinID="MediumTextBox" TabIndex="20"></asp:TextBox>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="LabelApplyDiscount0" runat="server" Font-Bold="false">Apply_Discount:</asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="cboApplyDiscount" runat="server" SkinID="MediumDropDown" TabIndex="24">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelClassCode0" runat="server" Font-Bold="false">Class_Code:</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="TextboxClassCode" runat="server" SkinID="MediumTextBox" TabIndex="22"></asp:TextBox>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="LabelDiscountAmt0" runat="server" Font-Bold="false">Discount_Amount:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextboxDiscountAmt" runat="server" SkinID="MediumTextBox" TabIndex="21"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="LabelClaimAllowed0" runat="server" Font-Bold="false">Claim_Allowed:</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:DropDownList ID="cboCalimAllowed" runat="server" SkinID="MediumDropDown" TabIndex="24">
                                    </asp:DropDownList>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="LabelDiscountPercent0" runat="server" Font-Bold="false">Discount_percentage:</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="TextboxDiscountPercent" runat="server" SkinID="MediumTextBox" TabIndex="23"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    &nbsp;
                                </td>
                                <td nowrap="nowrap">
                                    &nbsp;
                                </td>
                                <td align="right" nowrap="nowrap">
                                    &nbsp;
                                </td>
                                <td>
                                    &nbsp;
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </asp:Panel>
            <div class="btnZone">
                <div class="">
                    <!--START   DEF-2539-->
                    <asp:Button ID="ButtonLocateCenter" TabIndex="185" runat="server" Font-Bold="false"
                        Text="CONTINUE" SkinID="PrimaryRightButton" />
                    <asp:Button ID="btnSave_WRITE" TabIndex="190" runat="server" Font-Bold="false" Text="SAVE"
                        SkinID="PrimaryRightButton" />
                    <asp:LinkButton ID="btnCancel" runat="server" SkinID="AlternateRightButton" Text="CANCEL"
                        OnClientClick="return revealModal('ModalCancel');" />
                    <asp:Button ID="btnDenyClaim" runat="server" Font-Bold="false" TabIndex="195" Text="DENY_CLAIM"
                        SkinID="AlternateRightButton" />
                    <asp:Button ID="btnBack" TabIndex="185" runat="server" Font-Bold="false" Text="BACK"
                        SkinID="AlternateLeftButton" />
                    <asp:Button ID="btnUndo_Write" TabIndex="195" runat="server" Font-Bold="false" Text="UNDO"
                        SkinID="AlternateLeftButton" />
                    <asp:Button ID="btnEdit_WRITE" TabIndex="200" runat="server" Text="EDIT" CausesValidation="False"
                        SkinID="AlternateLeftButton" />
                    <asp:Button ID="ButtonSoftQuestions" TabIndex="190" runat="server" Font-Bold="false"
                        Text="SOFT_QUESTIONS" SkinID="AlternateLeftButton" />
                    <!--START   DEF-2539-->
                </div>
            </div>
        </div>
    </div>
    <div id="divSoftQuestions">
        <asp:Button ID="hiddenTargetControlForModalPopup" runat="server" Style="display: none" />
        <ajaxToolkit:ModalPopupExtender runat="server" ID="mdlPopup" TargetControlID="hiddenTargetControlForModalPopup"
            PopupControlID="pnlPopup" BackgroundCssClass="black_show" DropShadow="True" PopupDragHandleControlID="programmaticPopupDragHandle"
            RepositionMode="RepositionOnWindowScroll">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlPopup" runat="server" Style="width: 880px; height: 550px; display: none;">
            <asp:Panel runat="Server" ID="programmaticPopupDragHandle" Style="cursor: move; background-color: #DDDDDD;
                border: solid 1px Gray; color: Black; text-align: center; z-index: 1001;">
            </asp:Panel>
            <!-- ../Tables/SoftQuestionsList.aspx --> 
            <iframe runat="server" id="frameSoftQuestions" name="frameSoftQuestions" src="" style="width: 880px;height: 550px;"></iframe>
            <br />
            <%--<asp:Button ID="btnClose" runat="server" Text="Back" runat="server" Font-Bold="false"
                    Width="90px" Height="20px" OnClick="btnClose_Click" 
                SkinID="AlternateLeftButton" />--%>
        </asp:Panel>
    </div>
    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
        runat="server" />
    <input id="hdnDealerId" type="hidden" name="hdnDealerId" runat="server" />
    <input id="hdnSelectedClaimedSku" type="hidden" name="hdnSelectedClaimedSku" runat="server" />
    <script type="text/javascript" language="javascript">
        var ddlClaimedManufacturer = '<%=ddlClaimedManuf.ClientId%>';
        var txtClaimedModel = '<%=txtClaimedModel.ClientId%>';
        var ddlClaimedSku = '<%=ddlClaimedSkuNumber.ClientId%>';
        var hdnDealerId = '<%=hdnDealerId.ClientId%>';
        var hdnSelectedClaimedSku = '<%=hdnSelectedClaimedSku.ClientId%>';


        function LoadSKU()
        {

            var claimedManufacturerId = $('#' + ddlClaimedManufacturer).val();
            var claimedmodel = $('#' + txtClaimedModel).val();
            var dealerId = $('#' + hdnDealerId).val();

            if (claimedManufacturerId.length > 0 && claimedManufacturerId != '00000000-0000-0000-0000-000000000000' && claimedmodel.length > 0)
            {
                $.ajax({
                    type: "POST",
                    url: "CertItemForm.aspx/LoadSku",
                    data: '{ manufacturerId: "' + claimedManufacturerId + '",model:"' + claimedmodel + '",dealerId:"' + dealerId + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg)
                    {
                        $('#' + ddlClaimedSku).empty();
                        if (msg.d == null) { return }

                        var jsonArray = jQuery.parseJSON(msg.d);
                        
                        var listItems = "";
                        $.each(jsonArray, function ()
                        {
                            listItems += "<option value='" + this + "'>" + this + "</option>";

                        });
                        $('#' + ddlClaimedSku).html(listItems);
                        $('#' + ddlClaimedSku).selectedIndex = 0;
                        var hdnSelectedIssue = document.getElementById(hdnSelectedClaimedSku);
                        hdnSelectedIssue.value = $('#' + ddlClaimedSku).val();
                    }
                });
            }
        }

        function FillHiddenField(sourceDropDownClientId,destinationControlClientId)
        {
            var hdnSelectedIssue = document.getElementById(destinationControlClientId);
            var objDecDropDown = document.getElementById(sourceDropDownClientId);
            hdnSelectedIssue.value = objDecDropDown.options[objDecDropDown.selectedIndex].value;
        }
    </script>
</asp:Content>
