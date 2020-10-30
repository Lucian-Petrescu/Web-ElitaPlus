<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ClaimWizardForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ClaimWizardForm" MasterPageFile="~/Navigation/masters/ElitaBase.Master"
    Theme="Default" EnableSessionState="True" %>

<%@ Register TagPrefix="Elita" TagName="ProtectionAndEventDetails" Src="~/Common/ProtectionAndEventDetails.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlWizard" Src="~/Common/UserControlWizard.ascx" %>
<%@ Register TagPrefix="Elita" TagName="MultipleColumnDDLabelControl" Src="~/Common/MultipleColumnDDLabelControl_New.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlContactInfo" Src="../Common/UserControlContactInfo_New.ascx" %>
<%@ Register TagPrefix="Elita" Assembly="Assurant.ElitaPlus.WebApp" Namespace="Assurant.ElitaPlus.ElitaPlusWebApp" %>
<%@ Register TagPrefix="Elita" TagName="MessageController" Src="~/Common/MessageController.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlPoliceReport_New" Src="~/Common/UserControlPoliceReport_New.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlCertificateInfo" Src="~/Certificates/UserControlCertificateInfo_New.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register TagPrefix="Elita" TagName="BestReplacementOption" Src="~/Interfaces/ReplacementOptions.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlConsequentialDamage" Src="UserControlConsequentialDamage.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlClaimDeviceInfo" Src="~/Interfaces/ClaimDeviceInformationController.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlAddressInfo" Src="~/Common/UserControlLogisticStageAddresses.ascx" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <style type="text/css">
        .ModalBackground {
            background-color: Gray;
            filter: alpha(opacity=50);
            opacity: 0.5;
        }

        .black_show {
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
<asp:Content ID="Content2" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
    <Elita:MessageController runat="server" ID="moMessageController" Visible="false" />
    <Elita:MessageController runat="server" ID="mcIssueStatus" Visible="false" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <Elita:ProtectionAndEventDetails ID="moProtectionAndEventDetails" runat="server"
        align="center" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto">
        <Scripts>
            <asp:ScriptReference Path="~/Navigation/scripts/ComunaSuggest.js" />
        </Scripts>
    </asp:ScriptManager>
    <div id="ModalCancel" class="overlay">
        <div id="light" class="overlay_message_content">
            <p class="modalTitle">
                <asp:Label ID="lblModalTitle" runat="server" Text="CONFIRM"></asp:Label>
                <a href="javascript:void(0)" onclick="hideModal('ModalCancel');">
                    <img id="Img1" src="~/App_Themes/Default/Images/icon_modalClose.png" runat="server"
                        width="16" height="18" align="absmiddle" class="floatR" /></a>
            </p>
            <table class="formGrid" width="98%" cellspacing="0" cellpadding="0" border="0">
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
                        <td>&nbsp;
                        </td>
                        <td id="tdBtnArea" nowrap="nowrap" runat="server" colspan="2">
                            <input id="btnModalCancelYes" class="primaryBtn floatR" runat="server" type="button"
                                value="Yes" />
                            <input id="btnModalCancelNo" class="popWindowAltbtn floatR" runat="server" type="button"
                                value="No" onclick="hideModal('ModalCancel');" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div id="fade" class="black_overlay">
        </div>
    </div>
    <div id="ModalServiceWarranty" class="overlay">
        <div id="light" class="overlay_message_content">
            <p class="modalTitle">
                <asp:Label ID="lblModalTitleServiceWarranty" runat="server" Text="CONFIRM"></asp:Label>
                <a href="javascript:void(0)" onclick="hideModal('ModalServiceWarranty');">
                    <img id="Img11" src="~/App_Themes/Default/Images/icon_modalClose.png" runat="server"
                        width="16" height="18" align="absmiddle" class="floatR" /></a>
            </p>
            <table class="formGrid" width="98%" cellspacing="0" cellpadding="0" border="0">
                <tbody>
                    <tr>
                        <td align="right">
                            <img id="img12" name="imgMsgIcon" width="28" runat="server" src="~/App_Themes/Default/Images/dialogue_confirm.png"
                                height="28" />
                        </td>
                        <td id="tdModalMessageServiceWarranty" colspan="2" runat="server">
                            <asp:Label ID="lblServiceWarrantyMessage" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                        <td id="td4" nowrap="nowrap" runat="server" colspan="2">
                            <asp:Button ID="btnModalServiceWarrantyYes" runat="server" SkinID="PrimaryRightButton" Text="Yes" />
                            <input id="btnModalServiceWarrantyNo" class="popWindowAltbtn floatR" runat="server" type="button"
                                value="No" onclick="hideModal('ModalServiceWarranty');" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div id="Div3" class="black_overlay">
        </div>
    </div>
    <div id="ModalClaimCancel" class="overlay">
        <div id="light" class="overlay_message_content">
            <p class="modalTitle">
                <asp:Label ID="lblModalClaimCancelTitle" runat="server" Text="CONFIRM"></asp:Label>
                <a href="javascript:void(0)" onclick="hideModal('ModalClaimCancel');">
                    <img id="Img7" src="~/App_Themes/Default/Images/icon_modalClose.png" runat="server"
                        width="16" height="18" align="absmiddle" class="floatR" /></a>
            </p>
            <table class="formGrid" width="98%" cellspacing="0" cellpadding="0" border="0">
                <tbody>
                    <tr>
                        <td align="right">
                            <img id="img8" name="imgMsgIcon" width="28" runat="server" src="~/App_Themes/Default/Images/dialogue_confirm.png"
                                height="28" />
                        </td>
                        <td id="td1" colspan="2" runat="server">
                            <asp:Label ID="lblModalClaimCancel" runat="server"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                        <td id="td2" nowrap="nowrap" runat="server" colspan="2">
                            <input id="btnModalCancelClaimYes" class="primaryBtn floatR" runat="server" type="button"
                                value="Yes" />
                            <input id="btnModalClaimCancelNo" class="popWindowAltbtn floatR" runat="server" type="button"
                                value="No" onclick="hideModal('ModalClaimCancel');" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div id="fade" class="black_overlay">
        </div>
    </div>
    <div id="ModalMasterClaim" class="overlay">
        <div id="Div1" class="overlay_message_content" style="width: 650px; height: auto">
            <p class="modalTitle">
                <table width="100%">
                    <tr>
                        <td align="left">
                            <asp:Label ID="modalHeaderLabel" Text="SELECT_MASTER_CLAIM_NUMBER" runat="server"></asp:Label></td>
                        <td align="right">
                            <a href="javascript:void(0)" onclick="hideModal('ModalMasterClaim');">
                                <img id="Img6" src="~/App_Themes/Default/Images/icon_modalClose.png" runat="server"
                                    width="16" height="18" align="top" /></a>
            </p>
            </td></tr></table>
            <div class="Page">
                <asp:GridView ID="grdMasterClaim" runat="server" Width="100%" AutoGenerateColumns="False"
                    AllowPaging="True" SkinID="DetailPageGridView" AllowSorting="true">
                    <SelectedRowStyle Wrap="True" />
                    <EditRowStyle Wrap="True" />
                    <AlternatingRowStyle Wrap="True" />
                    <RowStyle Wrap="True" />
                    <HeaderStyle />
                    <Columns>
                        <asp:TemplateField HeaderText="MASTER_CLAIM_#" SortExpression="MASTER_CLAIM_NUMBER">
                            <ItemTemplate>
                                <asp:LinkButton runat="server" ID="btnSelectClaim" CommandName="SelectAction" CausesValidation="False" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="CLAIM_#" DataField="CLAIM_NUMBER" SortExpression="CLAIM_NUMBER"
                            ReadOnly="true" HtmlEncode="false" HeaderStyle-HorizontalAlign="Center" />
                        <asp:BoundField HeaderText="DATE_OF_LOSS" DataField="LOSS_DATE" SortExpression="LOSS_DATE"
                            ReadOnly="true" HtmlEncode="false" HeaderStyle-HorizontalAlign="Center" />
                    </Columns>
                    <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                    <PagerStyle />
                </asp:GridView>
            </div>
            <div class="btnZone">
                <asp:Button ID="btnAddNewMasterClaim" runat="server" Text="NEW_MASTER" SkinID="PrimaryRightButton"></asp:Button>
            </div>
        </div>
        <div id="fade" class="black_overlay">
        </div>
    </div>
    <div id="ModalDenyClaim" class="overlay">
        <div id="light" class="overlay_message_content" style="width: 470px">
            <p class="modalTitle">
                <asp:Label ID="Label2" Text="Deny_Claim" runat="server"></asp:Label>
                <a href="javascript:void(0)" onclick="hideModal('ModalDenyClaim');">
                    <img id="Img9" src="~/App_Themes/Default/Images/icon_modalClose.png" runat="server"
                        width="16" height="18" align="absmiddle" class="floatR" /></a>
            </p>
            <table width="100%" border="0" class="formGrid" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                        <span class="mandatory">*</span><asp:Label ID="step3_lblDeniedReason" runat="server">Denied_Reason</asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="step3_cboDeniedReason" runat="server" SkinID="MediumDropDown"
                            AutoPostBack="false">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="step3_lblPotFraudulent" runat="server">Potentially_Fraudulent</asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="step3_cboFraudulent" runat="server" SkinID="MediumDropDown"
                            AutoPostBack="false">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Label ID="step3_lblComplaint" runat="server">Complaint</asp:Label>
                    </td>
                    <td>
                        <asp:DropDownList ID="step3_cboComplaint" runat="server" SkinID="MediumDropDown"
                            AutoPostBack="false">
                        </asp:DropDownList>
                    </td>
                </tr>
            </table>
            <div class="btnZone">
                <asp:Button ID="btnDenyClaimSave" runat="server" Text="SAVE" SkinID="PrimaryRightButton"></asp:Button>
            </div>
        </div>
        <div id="fade" class="black_overlay">
        </div>
    </div>
    <div id="ModalBypassDoL" class="overlay">
        <div class="overlay_message_content">
            <p class="modalTitle">
                <asp:Label ID="lblTitle" runat="server" Text="CONFIRM"></asp:Label>
                <a href="javascript:void(0)" onclick="hideModal('ModalBypassDoL');">
                    <img id="Img13" src="~/App_Themes/Default/Images/icon_modalClose.png" runat="server" width="16" height="18" align="absmiddle" class="floatR" alt="" />
                </a>
            </p>
            <table class="formGrid" width="98%">
                <tbody>
                    <tr>
                        <td>
                            <img id="img14" width="28" runat="server" src="~/App_Themes/Default/Images/dialogue_confirm.png" height="28" />
                        </td>
                        <td id="tdModalBbDolMessage" colspan="2" runat="server">
                            <asp:Label ID="lblInvalidDoLMessage" runat="server">Bypass_Invalid_DoL</asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>&nbsp;
                        </td>
                        <td id="td5" runat="server" colspan="2">
                            <asp:Button ID="btnModalBbDolYes" class="primaryBtn floatR" runat="server" Text="Yes"></asp:Button>
                            <input id="btnModalBbDolNo" class="popWindowAltbtn floatR" runat="server" type="button"
                                value="No" onclick="hideModal('ModalBypassDoL');" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div id="fade" class="black_overlay">
        </div>
    </div>
    <div id="ModalSoftQuestions" class="overlay">
        <div id="light" class="overlay_message_content" style="left: 10%; right: 5%; top: 5%; max-height: 80%">
            <p class="modalTitle">
                <asp:Label ID="lbl" runat="server">Soft Questions</asp:Label>
                <a href="javascript:void(0)" onclick="hideModal('ModalSoftQuestions');">
                    <img id="Img10" src="~/App_Themes/Default/Images/icon_modalClose.png" runat="server"
                        width="16" height="18" align="absmiddle" class="floatR" /></a>
            </p>
            <div class="dataContainer">
                <table id="TableFixed" cellspacing="0" cellpadding="0" border="0" width="80%" class="summaryGrid">
                    <tbody>
                        <Elita:UserControlCertificateInfo ID="moCertificateInfoController" runat="server"></Elita:UserControlCertificateInfo>
                    </tbody>
                </table>
            </div>
            <div class="dataContainer">
                <asp:TreeView ID="tvQuestion" runat="server" Target="_self" ShowExpandCollapse="true"
                    ShowLines="true" NodeStyle-HorizontalPadding="5" NodeStyle-VerticalPadding="0">
                    <SelectedNodeStyle BackColor="LightGray" />
                    <RootNodeStyle Font-Bold="true" />
                </asp:TreeView>

            </div>
        </div>
        <div class="black_overlay"></div>
    </div>
    <div class="container">
        <div class="contentZoneHome">
            <div class="dataContainer">
                <div class="stepWizBox">
                    <Elita:UserControlWizard runat="server" ID="WizardControl">
                        <Steps>
                            <Elita:StepDefinition StepNumber="1" StepName="DATE_OF_INCIDENT" />
                            <Elita:StepDefinition StepNumber="2" StepName="COVERAGE_DETAILS" />
                            <Elita:StepDefinition StepNumber="3" StepName="CLAIM_DETAILS" />
                            <Elita:StepDefinition StepNumber="4" StepName="LOCATE_SERVICE_CENTER" />
                            <Elita:StepDefinition StepNumber="5" StepName="SUBMIT_CLAIM" />
                        </Steps>
                    </Elita:UserControlWizard>
                </div>
            </div>
            <div id="dvstep1" runat="server">
                <div class="dataContainer">
                    <h2 class="dataGridHeader">Incident Information</h2>
                    <div class="stepformZone">
                        <table class="formGrid" cellspacing="0" cellpadding="0" border="0">
                            <tbody>
                                <tr>
                                    <td nowrap="nowrap" align="right">
                                        <span class="mandate">*</span><asp:Label ID="step1_lblDateOfLoss" runat="server">DATE_OF_INCIDENT</asp:Label>:
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:TextBox ID="step1_moDateOfLossText" TabIndex="1" runat="server" SkinID="smallTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="step1_btnDateOfLoss" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png"
                                            valign="bottom"></asp:ImageButton>
                                    </td>
                                    <td nowrap="nowrap" align="right">
                                        <span class="mandate">*</span><asp:Label ID="step1_lblDateReported" runat="server">DATE_REPORTED</asp:Label>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="step1_txtDateReported" TabIndex="2" runat="server" SkinID="smallTextBox"></asp:TextBox>
                                    </td>
                                    <td>
                                        <asp:ImageButton ID="step1_btnDateReported" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png"
                                            valign="bottom"></asp:ImageButton>
                                    </td>
                                    <td>
                                        <asp:Button ID="step1_btnSearch" runat="server" SkinID="SearchButton" Text="Search"></asp:Button>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <asp:Panel ID="step1_searchResultPanel" runat="server">
                    <div class="dataContainer">
                        <h2 class="dataGridHeader">Select Eligible Coverage</h2>
                        <div class="stepformZone">
                            <table class="formGrid" cellspacing="0" cellpadding="0" border="0">
                                <tbody>
                                    <tr id="step1_riskTypeTR" runat="server">
                                        <td nowrap="nowrap" align="right">
                                            <span class="mandate">*</span><asp:Label ID="step1_riskTypeLabel" runat="server"
                                                Text="RISK_TYPE"></asp:Label>:
                                        </td>
                                        <td nowrap="nowrap">
                                            <asp:DropDownList ID="step1_cboRiskType" runat="server" SkinID="MediumDropDown" AutoPostBack="True">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap="nowrap" align="right">
                                            <span class="mandate">*</span><asp:Label ID="step1_coverageTypeLabel" runat="server"
                                                Text="COVERAGE_TYPE"></asp:Label>:
                                        </td>
                                        <td nowrap="nowrap">
                                            <asp:DropDownList ID="step1_cboCoverageType" runat="server" SkinID="MediumDropDown">
                                            </asp:DropDownList>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap="nowrap" align="right">
                                            <span class="mandate">*</span><asp:Label ID="step1_callerNameLabel" runat="server"
                                                Text="NAME_OF_CALLER"></asp:Label>:
                                        </td>
                                        <td nowrap="nowrap">
                                            <asp:TextBox ID="step1_textCallerName" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap="nowrap" align="right">
                                            <span class="mandate">*</span><asp:Label ID="step1_problemDescriptionLabel" runat="server"
                                                Text="PROBLEM_DESCRIPTION"></asp:Label>:
                                        </td>
                                        <td nowrap="nowrap">
                                            <asp:TextBox ID="step1_textProblemDescription" runat="server" TextMode="MultiLine"></asp:TextBox>
                                        </td>
                                    </tr>
                                </tbody>
                            </table>
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <div id="dvStep2" runat="server">
                <div class="dataContainer">
                    <h2 class="dataGridHeader">Coverage Details</h2>
                    <asp:Panel ID="EditPanel_WRITE" runat="server" Width="100%">
                        <div class="stepformZone">
                            <table border="0" class="formGrid" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td nowrap="nowrap" align="right">
                                        <asp:Label ID="step2_LabelBeginDate" runat="server" Font-Bold="False">Begin_Date</asp:Label>
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:TextBox ID="step2_TextboxBeginDate" TabIndex="2" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td nowrap="nowrap" align="right">
                                        <asp:Label ID="step2_LabelInvNum" runat="server" Font-Bold="False">Invoice_Number</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="step2_TextboxInvNum" runat="server" MaxLength="30" SkinID="MediumTextBox"
                                            TabIndex="15"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap" align="right">
                                        <asp:Label ID="step2_LabelEndDate" runat="server" Font-Bold="False">End_Date</asp:Label>
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:TextBox ID="step2_TextboxEndDate" TabIndex="5" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td nowrap="nowrap" align="right">
                                        <asp:Label ID="step2_LabelDeductibleBasedOn" runat="server" Font-Bold="False">COMPUTE_DEDUCTIBLE_BASED_ON</asp:Label>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="step2_TextboxDeductibleBasedOn" runat="server" TabIndex="17" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap" align="right">
                                        <asp:Label ID="step2_LabelDateAdded" runat="server" Font-Bold="False">Date_Added</asp:Label>
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:TextBox ID="step2_TextboxDateAdded" TabIndex="7" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td nowrap="nowrap" align="right">
                                        <asp:Label ID="step2_LabelDeductible" runat="server" Font-Bold="False">DEDUCTIBLE</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="step2_TextboxDeductible" runat="server" SkinID="MediumTextBox" TabIndex="17"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap" align="right">
                                        <asp:Label ID="step2_LabelRiskTypeId" runat="server" Font-Bold="False">Risk_Type</asp:Label>
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:DropDownList ID="step2_cboRiskTypeId" runat="server" SkinID="MediumDropDown"
                                            TabIndex="9">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="step2_TextboxRiskType" runat="server" SkinID="MediumTextBox" TabIndex="30"></asp:TextBox>
                                    </td>
                                    <td nowrap="nowrap" align="right">
                                        <asp:Label ID="step2_LabelDeductiblePercent" runat="server" Font-Bold="False">DEDUCTIBLE_PERCENT</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="step2_TextboxDeductiblePercent" runat="server" SkinID="MediumTextBox"
                                            TabIndex="20"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap" align="right">
                                        <asp:Label ID="step2_LabelCoverageType" runat="server" Font-Bold="False">Coverage_Type</asp:Label>
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:TextBox ID="step2_TextboxCoverageType" runat="server" SkinID="MediumTextBox"
                                            TabIndex="11"></asp:TextBox>
                                    </td>
                                    <td nowrap="nowrap" align="right">
                                        <asp:Label ID="step2_labelSKU" runat="server" Font-Bold="False">SKU_NUMBER</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="step2_TextboxSKU" runat="server" SkinID="MediumTextBox" TabIndex="20"
                                            Width="75%"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap" align="right">
                                        <asp:Label ID="step2_LabelMethodOfRepair" runat="server" Font-Bold="False">METHOD_OF_REPAIR</asp:Label>
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:DropDownList ID="step2_cboMethodOfRepair" runat="server" AutoPostBack="true"
                                            TabIndex="13" SkinID="MediumDropDown">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="step2_TextboxMethodOfRepair" runat="server" TabIndex="14" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td nowrap="nowrap" align="right">&nbsp;
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap" align="right">
                                        <asp:Label ID="step2_LabelProductCode" runat="server" Font-Bold="False">Product_Code</asp:Label>
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:TextBox ID="step2_TextboxProductCode" runat="server" SkinID="MediumTextBox"
                                            TabIndex="16"></asp:TextBox>
                                    </td>
                                    <td nowrap="nowrap" align="right">&nbsp;
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap" align="right">
                                        <asp:Label ID="step2_LabelLiabilityLimit" runat="server" Font-Bold="False">LIABILITY_LIMIT</asp:Label>
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:TextBox ID="step2_TextboxLiabilityLimit" runat="server" SkinID="MediumTextBox"
                                            TabIndex="19"></asp:TextBox>
                                    </td>
                                    <td nowrap="nowrap" align="right">&nbsp;
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap" align="right">
                                        <asp:Label ID="step2_labelRepairDiscountPct" runat="server" Font-Bold="False">REPAIR_DISCOUNT_PCT</asp:Label>:
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:TextBox ID="step2_TextboxRepairDiscountPct" runat="server" SkinID="SmallTextBox"
                                            TabIndex="19"></asp:TextBox>
                                    </td>
                                    <td nowrap="nowrap" align="right">&nbsp;
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap" align="right">
                                        <asp:Label ID="step2_labelReplacementDiscountPct" runat="server" Font-Bold="False">REPLACEMENT_DISCOUNT_PCT</asp:Label>:
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:TextBox ID="step2_TextboxReplacementDiscountPct" runat="server" SkinID="SmallTextBox"
                                            TabIndex="19"></asp:TextBox>
                                    </td>
                                    <td nowrap="nowrap" align="right">&nbsp;
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </asp:Panel>
                </div>
                <asp:Panel ID="pnlDeviceInfo" runat="server">
                    <div class="dataContainer">
                        <h2 id="headerDeviceInfo" runat="server" class="dataGridHeader">DEVICE_INFORMATION</h2>
                        <div class="stepformZone">
                            <table border="0" class="formGrid" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td nowrap="nowrap" align="right">&nbsp;
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:Label ID="step2_lblEnrolledDeviceInfo" runat="server" Text="ENROLLED_DEVICE_INFO"
                                            Visible="false" Font-Bold="true"></asp:Label>
                                    </td>
                                    <td align="right" nowrap="nowrap">&nbsp;
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:Label ID="step2_lblClaimedEquipment" runat="server" Text="CLAIMED_DEVICE_INFO"
                                            Visible="false" Font-Bold="true"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap" align="right">
                                        <asp:Label ID="step2_LabelMakeId" runat="server" Font-Bold="False" Text="Make"></asp:Label>
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:DropDownList ID="step2_cboManufacturerId" TabIndex="3" runat="server" SkinID="MediumDropDown">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="step2_TextboxManufacturer" TabIndex="4" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td align="right" nowrap="nowrap">
                                        <asp:Label ID="step2_lblClaimedMake" runat="server" Text="MAKE" Visible="False"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="step2_ddlClaimedManuf" Visible="false" TabIndex="3" runat="server"
                                            SkinID="MediumDropDown">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="step2_txtClaimedmake" runat="server" SkinID="SmallTextBox" TabIndex="17"
                                            Visible="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap" align="right">
                                        <asp:Label ID="step2_LabelModel" runat="server" Font-Bold="False" EnableTheming="True" Text="Model"></asp:Label>
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:TextBox ID="step2_TextboxModel" TabIndex="6" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td align="right" nowrap="nowrap">
                                        <asp:Label ID="step2_lblClaimedModel" runat="server" Text="MODEL" Visible="False"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="step2_txtClaimedModel" runat="server" SkinID="SmallTextBox" TabIndex="20"
                                            Visible="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap" align="right">
                                        <asp:Label ID="step2_LabelDealerItemDesc" runat="server" Font-Bold="False" Text="Description"></asp:Label>
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:TextBox ID="step2_TextboxDealerItemDesc" TabIndex="8" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                        &nbsp;
                                    </td>
                                    <td align="right" nowrap="nowrap">
                                        <asp:Label ID="step2_LabelClaimDesc" runat="server" Text="Description" Visible="False"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="step2_txtClaimedDescription" TabIndex="8" runat="server" SkinID="MediumTextBox"
                                            Visible="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" nowrap="nowrap">
                                        <asp:Label ID="step2_LabelYear" runat="server" Font-Bold="False">Year:</asp:Label>
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:TextBox ID="step2_TextboxYear" runat="server" SkinID="MediumTextBox" TabIndex="10"></asp:TextBox>
                                    </td>
                                    <td align="right" nowrap="nowrap">
                                        <asp:Label ID="step2_lblClaimedSKU" runat="server" Text="SKU_NUMBER" Visible="False"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="step2_ddlClaimedSku" Visible="false" TabIndex="3" runat="server"
                                            SkinID="MediumDropDown">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="step2_txtClaimedSKu" TabIndex="8" runat="server" SkinID="MediumTextBox"
                                            Visible="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap" align="right">
                                        <asp:Label ID="step2_LabelSerialNumber" runat="server" Font-Bold="False" Width="124px">SERIAL_NUMBER</asp:Label>
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:TextBox ID="step2_TextboxSerialNumber" TabIndex="12" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td align="right" nowrap="nowrap">
                                        <asp:Label ID="step2_LabelClaimSerialNumber" runat="server" Text="SERIAL_NUMBER"
                                            Visible="False"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="step2_txtClaimedSerialNumber" TabIndex="12" runat="server" SkinID="MediumTextBox"
                                            Visible="False"></asp:TextBox>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlVehicleInfo" runat="server">
                    <div class="dataContainer">
                        <h2 class="dataGridHeader">Vehicle Information</h2>
                        <div class="stepformZone">
                            <table border="0" class="formGrid" cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td nowrap="nowrap" align="right">
                                        <asp:Label ID="step2_LabelOdometer" runat="server" Font-Bold="False">ODOMETER:</asp:Label>
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:TextBox ID="step2_TextboxOdometer" runat="server" SkinID="MediumTextBox" TabIndex="20"></asp:TextBox>
                                    </td>
                                    <td align="right" nowrap="nowrap">
                                        <asp:Label ID="step2_LabelApplyDiscount0" runat="server" Font-Bold="false">Apply_Discount:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="step2_cboApplyDiscount" runat="server" SkinID="MediumDropDown"
                                            TabIndex="24">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap" align="right">
                                        <asp:Label ID="step2_LabelClassCode0" runat="server" Font-Bold="false">Class_Code:</asp:Label>
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:TextBox ID="step2_TextboxClassCode" runat="server" SkinID="MediumTextBox" TabIndex="22"></asp:TextBox>
                                    </td>
                                    <td align="right" nowrap="nowrap">
                                        <asp:Label ID="step2_LabelDiscountAmt0" runat="server" Font-Bold="false">Discount_Amount:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="step2_TextboxDiscountAmt" runat="server" SkinID="MediumTextBox"
                                            TabIndex="21"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap" align="right">
                                        <asp:Label ID="step2_LabelClaimAllowed0" runat="server" Font-Bold="false">Claim_Allowed:</asp:Label>
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:DropDownList ID="step2_cboCalimAllowed" runat="server" SkinID="MediumDropDown"
                                            TabIndex="24">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right" nowrap="nowrap">
                                        <asp:Label ID="step2_LabelDiscountPercent0" runat="server" Font-Bold="false">Discount_percentage:</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="step2_TextboxDiscountPercent" runat="server" SkinID="MediumTextBox"
                                            TabIndex="23"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap" align="right">&nbsp;
                                    </td>
                                    <td nowrap="nowrap">&nbsp;
                                    </td>
                                    <td align="right" nowrap="nowrap">&nbsp;
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </asp:Panel>
            </div>
            <div id="dvStep3" runat="server">
                <div class="dataContainer">
                    <h2 class="dataGridHeader">
                        <asp:Label runat="server" ID="LabelNewClaimDtl">NEW_CLAIM_DETAILS</asp:Label>
                    </h2>
                    <div class="stepformZone">
                        <table class="formGrid" cellspacing="0" cellpadding="0" border="0">
                            <tbody>
                                <tr>
                                    <td nowrap="nowrap" align="right">
                                        <asp:Label ID="step3_LabelCertificateNumber" runat="server" Font-Bold="false">Certificate</asp:Label>
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:TextBox ID="step3_TextboxCertificateNumber" TabIndex="200" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap" align="right">
                                        <asp:Label ID="step3_LabelClaimNumber" runat="server">Claim_Number</asp:Label>
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:TextBox ID="step3_TextboxClaimNumber" TabIndex="171" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap" align="right">
                                        <asp:Label ID="step3_LabelContactName" runat="server">Contact_Name</asp:Label>
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:DropDownList ID="step3_cboContactSalutationId" TabIndex="64" runat="server">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="step3_TextboxContactName" TabIndex="65" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap" align="right">
                                        <asp:Label ID="step3_LabelCallerName" runat="server">NAME_OF_CALLER</asp:Label>
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:DropDownList ID="step3_cboCallerSalutationId" TabIndex="64" runat="server">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="step3_TextboxCallerName" TabIndex="70" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td nowrap="nowrap" align="right">
                                        <asp:Label ID="step3_LabelCALLER_TAX_NUMBER" runat="server">CALLER_TAX_NUMBER</asp:Label>
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:TextBox ID="step3_TextboxCALLER_TAX_NUMBER" TabIndex="71" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap" align="right">
                                        <asp:Label ID="step3_LabelLossDate" runat="server">Date_Of_Loss</asp:Label>
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:TextBox ID="step3_TextboxLossDate" TabIndex="73" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                        <asp:ImageButton ID="step3_ImageButtonLossDate" TabIndex="75" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                            ImageAlign="AbsMiddle"></asp:ImageButton>
                                    </td>
                                    <td nowrap="nowrap" align="right">
                                        <asp:Label ID="step3_LabelLiabilityLimit" runat="server">Liability_Limit</asp:Label>
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:TextBox ID="step3_TextboxLiabilityLimit" TabIndex="76" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap" align="right">
                                        <asp:Label ID="step3_LabelReportDate" runat="server">DATE_REPORTED</asp:Label>
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:TextBox ID="step3_TextboxReportDate" TabIndex="73" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                        <asp:ImageButton ID="step3_ImageButtonReportDate" TabIndex="75" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                            ImageAlign="AbsMiddle"></asp:ImageButton>
                                    </td>
                                    <td nowrap="nowrap" align="right">
                                        <asp:Label ID="step3_LabelDeductible" runat="server">Deductible</asp:Label>
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:TextBox ID="step3_TextboxDeductible_WRITE" TabIndex="78" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap" align="right">
                                        <asp:Label ID="step3_LabelOutstandingPremAmt" runat="server">OUTSTANDING_PREMIUM_AMOUNT</asp:Label>
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:TextBox ID="step3_TextboxOutstandingPremAmt" TabIndex="75" runat="server" ReadOnly="true"
                                            SkinID="MediumTextBox" ForeColor="Red" Font-Bold="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap" align="right">
                                        <asp:Label ID="step3_LabelCauseOfLossId" runat="server">Cause_Of_Loss</asp:Label>
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:DropDownList ID="step3_cboCauseOfLossId" TabIndex="77" runat="server" SkinID="MediumDropDown"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                    <td nowrap="nowrap" align="right">
                                        <asp:Label ID="step3_LabelDiscount" runat="server">DISCOUNT</asp:Label>
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:TextBox ID="step3_TextBoxDiscount" TabIndex="78" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap" align="right">
                                        <asp:Label ID="step3_LabelIsLawsuitId" runat="server" Font-Bold="false">Lawsuit</asp:Label>
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:DropDownList ID="step3_cboLawsuitId" TabIndex="79" runat="server" SkinID="SmallDropDown">
                                        </asp:DropDownList>
                                    </td>
                                    <td nowrap="nowrap" align="right">
                                        <asp:Label ID="step3_LabelPolicyNumber" runat="server">Policy_Number</asp:Label>
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:TextBox ID="step3_TextboxPolicyNumber" TabIndex="80" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap" align="right">
                                        <asp:Label ID="step3_LabelUseShipAddress" runat="server" Visible="false">USE_SHIP_ADDRESS</asp:Label>
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:DropDownList ID="step3_cboUseShipAddress" TabIndex="77" runat="server" SkinID="MediumDropDown"
                                            AutoPostBack="True" Visible="false">
                                        </asp:DropDownList>
                                    </td>
                                    <td nowrap="nowrap" align="right">
                                        <asp:Label ID="step3_lblNewDeviceSKU" runat="server">NEW_DEVICE_SKU:</asp:Label>
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:TextBox ID="step3_txtNewDeviceSKU" TabIndex="86" runat="server" AutoPostBack="true"
                                            SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="step3_lblPickupDate" runat="server">PickUp_Date</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="step3_txtPickupDate" TabIndex="76" runat="server" AutoPostBack="true"
                                                 SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td nowrap="nowrap" align="right">
                                    <asp:Label ID="step3_lblVisitDate" runat="server">Visit_Date</asp:Label>
                                </td>
                                <td nowrap="nowrap">
                                    <asp:TextBox ID="step3_txtVisitDate" TabIndex="88" runat="server" AutoPostBack="true"
                                                 SkinID="MediumTextBox"></asp:TextBox>   
                                </td>
                            </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <Elita:BestReplacementOption ID="step3_ReplacementOption" runat="server" Visible="False"></Elita:BestReplacementOption>
                <div class="dataContainer">
                    <h2 class="dataGridHeader">
                        <asp:Label runat="server" ID="lblGrdHdr"></asp:Label>
                        <span class=""><a onclick="RevealModalWithMessage('ModalIssue');" href="javascript:void(0)">
                            <asp:Label ID="lblFileNewIssue" runat="server"></asp:Label>
                        </a></span>
                    </h2>
                    <!--
    -->
                    <div style="width: 100%;">
                        <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0" />
                        <div id="tabs" class="style-tabs">
                            <ul>
                                <li><a href="#tabClaimIssues">
                                    <asp:Label ID="Label4" runat="server" CssClass="tabHeaderText">CLAIM_ISSUES</asp:Label></a></li>
                                <li><a href="#tabClaimImages">
                                    <asp:Label ID="Label6" runat="server" CssClass="tabHeaderText">CLAIM_IMAGES</asp:Label></a></li>
                                <li><a href="#tabClaimAuthorization">
                                    <asp:Label ID="Label8" runat="server" CssClass="tabHeaderText">CLAIM_AUTHORIZATIONS</asp:Label></a></li>
                                <li><a href="#tabDeviceInformation">
                                    <asp:Label ID="Label3" runat="server" CssClass="tabHeaderText">DEVICE_INFORMATION</asp:Label></a></li>
                                <li><a href="#tabsQuestionAnswerInfo">
                                    <asp:Label ID="QuestionAnswerLabel" runat="server" CssClass="tabHeaderText">CASE_QUESTION_ANSWER</asp:Label></a></li>
                                <li><a href="#tabsActionInfo">
                                    <asp:Label ID="ActionInfoLabel" runat="server" CssClass="tabHeaderText">CASE_ACTION</asp:Label></a></li>
                                <li><a href="#tbClaimConsequentialDamage">
                                    <asp:Label ID="Label5" runat="server" CssClass="tabHeaderText">CLAIM_CONSEQUENTIAL_DAMAGE</asp:Label></a></li>                        
                            </ul>

                            <div id="tabClaimIssues">
                                <div class="Page">
                                    <div id="dvGridPager" runat="server">
                                        <table width="100%" class="dataGrid">
                                            <tr id="trPageSize" runat="server">
                                                <td class="bor" align="left">
                                                    <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label><asp:Label ID="colonSepertor"
                                                        runat="server">:</asp:Label>
                                                    &nbsp;
                                                            <asp:DropDownList ID="cboPageSize" runat="server" Width="50px" AutoPostBack="true"
                                                                SkinID="SmallDropDown">
                                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                                <asp:ListItem Value="15">15</asp:ListItem>
                                                                <asp:ListItem Value="20">20</asp:ListItem>
                                                                <asp:ListItem Value="25">25</asp:ListItem>
                                                            </asp:DropDownList>
                                                </td>
                                                <td class="bor" align="right">
                                                    <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <asp:GridView ID="Grid" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
                                        SkinID="DetailPageGridView" AllowSorting="true">
                                        <SelectedRowStyle Wrap="True" />
                                        <EditRowStyle Wrap="True" />
                                        <AlternatingRowStyle Wrap="True" />
                                        <RowStyle Wrap="True" />
                                        <HeaderStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Issue" SortExpression="ISSUE_DESCRIPTION">
                                                <ItemTemplate>
                                                    <asp:LinkButton CommandName="Select" ID="EditButton_WRITE" runat="server" CausesValidation="False"
                                                        Text=""></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="CREATED_DATE" SortExpression="CREATED_DATE" ReadOnly="true"
                                                HtmlEncode="false" HeaderText="CREATED_DATE" HeaderStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="CREATED_BY" SortExpression="CREATED_BY" ReadOnly="true"
                                                HtmlEncode="false" HeaderText="CREATED_BY" HeaderStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="PROCESSED_DATE" SortExpression="PROCESSED_DATE" ReadOnly="true"
                                                HtmlEncode="false" HeaderText="PROCESSED_DATE" HeaderStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="PROCESSED_BY" SortExpression="PROCESSED_BY" ReadOnly="true"
                                                HtmlEncode="false" HeaderText="PROCESSED_BY" HeaderStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="STATUS_CODE" ReadOnly="true" HeaderText="Status" SortExpression="Status"
                                                HtmlEncode="false" />
                                        </Columns>
                                        <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                                        <PagerStyle />
                                    </asp:GridView>
                                </div>
                            </div>

                            <div id="tabClaimImages">
                                <asp:Panel runat="server" ID="AddImagePanel" CssClass="dataContainer">
                                    <h2 class="dataGridHeader">
                                        <asp:Label runat="server" ID="AddImageHealder">ADD_IMAGE</asp:Label>
                                    </h2>
                                    <div class="stepformZone">
                                        <table width="100%" class="formGrid" border="0" cellpadding="0" cellspacing="0">
                                            <tbody>
                                                <tr>
                                                    <td align="right" nowrap="nowrap">
                                                        <asp:Label runat="server" ID="DocumentTypeLabel" Text="DOCUMENT_TYPE"></asp:Label>
                                                    </td>
                                                    <td nowrap="nowrap">
                                                        <asp:DropDownList runat="server" ID="DocumentTypeDropDown" SkinID="MediumDropDown" />
                                                    </td>
                                                    <td align="right" nowrap="nowrap">
                                                        <asp:Label runat="server" ID="ScanDateLabel" Text="SCAN_DATE"></asp:Label>
                                                    </td>
                                                    <td nowrap="nowrap">
                                                        <asp:TextBox runat="server" ID="ScanDateTextBox" ReadOnly="true" SkinID="MediumTextBox" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" nowrap="nowrap">
                                                        <asp:Label runat="server" ID="FileNameLabel" Text="FileName"></asp:Label>
                                                    </td>
                                                    <td colspan="3" nowrap="nowrap">
                                                        <input id="ImageFileUpload" style="width: 80%" type="file" name="ImageFileUpload"
                                                            runat="server" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" nowrap="nowrap">
                                                        <asp:Label runat="server" ID="CommentLabel" Text="COMMENT"></asp:Label>
                                                    </td>
                                                    <td colspan="3" nowrap="nowrap">
                                                        <asp:TextBox runat="server" ID="CommentTextBox" Width="80%" Rows="4" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <div class="btnZone">
                                                            <asp:LinkButton ID="ClearButton" runat="server" SkinID="AlternateRightButton" Text="Cancel"></asp:LinkButton>
                                                            <asp:Button ID="AddImageButton" runat="server" SkinID="PrimaryLeftButton" Text="Add_Image"></asp:Button>
                                                        </div>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </asp:Panel>
                                <div class="Page">
                                    <asp:GridView ID="GridClaimImages" runat="server" Width="100%" AutoGenerateColumns="False"
                                        AllowPaging="True" SkinID="DetailPageGridView" AllowSorting="true">
                                        <SelectedRowStyle Wrap="True" />
                                        <EditRowStyle Wrap="True" />
                                        <AlternatingRowStyle Wrap="True" />
                                        <RowStyle Wrap="True" />
                                        <HeaderStyle />
                                        <Columns>
                                            <asp:TemplateField HeaderText="IMAGE_ID" SortExpression="IMAGE_ID">
                                                <ItemTemplate>
                                                    <asp:LinkButton runat="server" ID="btnImageLink" CommandName="SelectActionImage"
                                                        Text="Image Link"></asp:LinkButton>
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:BoundField DataField="SCAN_DATE" SortExpression="SCAN_DATE" ReadOnly="true"
                                                HtmlEncode="false" HeaderText="SCAN_DATE" HeaderStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="DOCUMENT_TYPE" SortExpression="DOCUMENT_TYPE" ReadOnly="true"
                                                HtmlEncode="false" HeaderText="DOCUMENT_TYPE" HeaderStyle-HorizontalAlign="Center" />
                                            <asp:BoundField DataField="STATUS" SortExpression="SCAN_DATE" ReadOnly="true" HtmlEncode="false"
                                                HeaderText="STATUS" HeaderStyle-HorizontalAlign="Center" />
                                        </Columns>
                                        <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                                        <PagerStyle />
                                    </asp:GridView>
                                </div>
                            </div>

                            <div id="tabClaimAuthorization">
                                <div class="Page">
                                    <div class="dataContainer" id="dvClaimAuthorizationDetails" runat="server">
                                        <div style="width: 100%">
                                            <asp:GridView ID="GridClaimAuthorization" runat="server" Width="100%" AutoGenerateColumns="False"
                                                SkinID="DetailPageGridView" AllowSorting="true">
                                                <SelectedRowStyle Wrap="True" />
                                                <EditRowStyle Wrap="True" />
                                                <AlternatingRowStyle Wrap="True" />
                                                <RowStyle Wrap="True" />
                                                <HeaderStyle />
                                                <Columns>
                                                    <asp:BoundField DataField="AuthorizationNumber" SortExpression="AuthorizationNumber"
                                                        ReadOnly="true" HtmlEncode="false" HeaderText="CLAIM_AUTHORIZATION_NUMBER" HeaderStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="ServiceCenterName" SortExpression="ServiceCenterName"
                                                        ReadOnly="true" HtmlEncode="false" HeaderText="SERVICE_CENTER_NAME" HeaderStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="AuthorizedAmount" SortExpression="AuthorizedAmount" ReadOnly="true"
                                                        HtmlEncode="false" HeaderText="AUTHORIZED_AMOUNT" HeaderStyle-HorizontalAlign="Center" ItemStyle-HorizontalAlign="Right" />
                                                    <asp:BoundField DataField="AuthorizationType" SortExpression="AuthorizationType" ReadOnly="true"
                                                                    HtmlEncode="false" HeaderText="AUTHORIZATION_TYPE" HeaderStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="RefundMethod" SortExpression="RefundMethod" ReadOnly="true"
                                                                    HtmlEncode="false" HeaderText="REFUND_METHOD" HeaderStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="CreatedBy" SortExpression="CreatedBy" ReadOnly="true"
                                                                    HtmlEncode="false" HeaderText="CREATED_BY" HeaderStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="CreatedDate" SortExpression="CreatedDate" ReadOnly="true"
                                                        HtmlEncode="false" HeaderText="CREATED_DATE" HeaderStyle-HorizontalAlign="Center" />
                                                    <asp:BoundField DataField="ClaimAuthStatus" ReadOnly="true" HeaderText="Status" SortExpression="ClaimAuthStatus"
                                                        HtmlEncode="false" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </div>
                                </div>
                            </div>
                            <div id="tabDeviceInformation">
                                <div class="Page">
                                    <table border="0" width="100%">
                                        <tr>
                                            <td width="100%" align="left">
                                                <Elita:UserControlClaimDeviceInfo ID="ucClaimDeviceInfo" runat="server"></Elita:UserControlClaimDeviceInfo>
                                            </td>
                                        </tr>
                                    </table>
                                </div>
                            </div>
                            <div id="tabsQuestionAnswerInfo">
                                <table class="dataGrid" border="0" width="100%">
                                    <tr>
                                        <td width="40%" align="right">
                                            <asp:Label ID="lblQuestionRecordFound" class="bor" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <div class="Page" runat="server" id="CaseQuestionAnswerTabPanel" style="display: block; height: 300px; overflow: auto">
                                    <asp:GridView ID="CaseQuestionAnswerGrid" runat="server" Width="100%" AllowPaging="false" AllowSorting="false"
                                        SkinID="DetailPageGridView">
                                        <Columns>
                                            <asp:BoundField HeaderText="case_number" DataField="case_number" SortExpression="case_number" HtmlEncode="false"></asp:BoundField>
                                            <asp:BoundField HeaderText="interaction_number" DataField="interaction_number" SortExpression="interaction_number" HtmlEncode="false"></asp:BoundField>
                                            <asp:BoundField HeaderText="Question" DataField="Question" SortExpression="Question" HtmlEncode="false"></asp:BoundField>
                                            <asp:BoundField HeaderText="answer" DataField="answer" SortExpression="answer" HtmlEncode="false"></asp:BoundField>
                                            <asp:BoundField HeaderText="created_date" DataField="created_date" SortExpression="created_date" HtmlEncode="false"></asp:BoundField>
                                        </Columns>
                                        <PagerSettings PageButtonCount="30" Mode="Numeric" />
                                    </asp:GridView>
                                </div>
                            </div>

                            <div id="tabsActionInfo">
                                <table class="dataGrid" border="0" width="100%">
                                    <tr>
                                        <td width="40%" align="right">
                                            <asp:Label ID="lblClaimActionRecordFound" class="bor" runat="server"></asp:Label>
                                        </td>
                                    </tr>
                                </table>
                                <div class="Page" runat="server" id="ClaimActionTabPanel" style="display: block; height: 300px; overflow: auto">

                                    <asp:GridView ID="ClaimActionGrid" runat="server" Width="100%" AllowPaging="false" AllowSorting="false"
                                        SkinID="DetailPageGridView">
                                        <Columns>
                                            <asp:BoundField HeaderText="action_owner" DataField="action_owner" SortExpression="action_owner" HtmlEncode="false"></asp:BoundField>
                                            <asp:BoundField HeaderText="action_type" DataField="action_type" SortExpression="action_type" HtmlEncode="false"></asp:BoundField>
                                            <asp:BoundField HeaderText="document_type_descr" DataField="document_type_descr" SortExpression="document_type_descr" HtmlEncode="false"></asp:BoundField>
                                            <asp:BoundField HeaderText="status" DataField="status" SortExpression="status" HtmlEncode="false"></asp:BoundField>
                                            <asp:BoundField HeaderText="created_date" DataField="created_date" SortExpression="created_date" HtmlEncode="false"></asp:BoundField>
                                        </Columns>
                                        <PagerSettings PageButtonCount="30" Mode="Numeric" />
                                    </asp:GridView>
                                </div>
                            </div>

                            <div id="tbClaimConsequentialDamage">
                                <Elita:UserControlConsequentialDamage ID="ucClaimConsequentialDamage" runat="server"></Elita:UserControlConsequentialDamage>
                            </div>
                        </div>

                    </div>
                    <!-- -->
                </div>
                <asp:Panel ID="PanelPoliceReport" Width="100%" runat="server">
                    <div class="dataContainer">
                        <h2 class="dataGridHeader">
                            <asp:Label ID="LabelPoliceRptDtl" runat="server">POLICE_REPORT_DETAILS</asp:Label>
                        </h2>
                        <Elita:UserControlPoliceReport_New ID="mcUserControlPoliceReport" runat="server" />
                    </div>
                </asp:Panel>
                <div class="dataContainer">
                    <table width="70%" cellspacing="0" cellpadding="0" border="0">
                        <tbody>
                            <tr>
                                <td>
                                    <h2 class="dataGridHeader">
                                        <asp:Label ID="step3_LabelProblemDescription" runat="server">Problem_Description</asp:Label>
                                    </h2>
                                    <div class="stepformZone">
                                        <table class="formGrid" cellspacing="0" cellpadding="0" border="0">
                                            <tbody>
                                                <tr>
                                                    <td nowrap="nowrap" align="right">
                                                        <asp:TextBox ID="step3_TextboxProblemDescription" runat="server" Rows="5" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                             <tr>
                                <td>
                                    <h2 class="dataGridHeader">
                                        <asp:Label ID="step3_LabelSpecialInstruction" runat="server">Special_Instruction</asp:Label>
                                    </h2>
                                    <div class="stepformZone">
                                        <table class="formGrid" cellspacing="0" cellpadding="0" border="0">
                                            <tbody>
                                                <tr>
                                                    <td nowrap="nowrap" align="right">
                                                        <asp:TextBox ID="step3_TxtSpecialInstruction" runat="server" Rows="5" TextMode="MultiLine"></asp:TextBox>
                                                    </td>
                                                </tr>
                                            </tbody>
                                        </table>
                                    </div>
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
                <div class="dataContainer">
                    <div class="stepformZone">
                        <table>
                            <tbody>
                                <Elita:UserControlContactInfo ID="moUserControlContactInfo" runat="server" Visible="false"></Elita:UserControlContactInfo>
                            </tbody>
                        </table>
                        <table border="0"></table>
                    </div>
                </div>
                 <div class="dataContainer">
                     <table style="width: 100%; border-collapse: collapse; border: 0;">
                         <tr>
                             <td>
                                 <h2 class="dataGridHeader">
                                     <asp:Label ID="lblLogisticStageAddress" runat="server">LOGISTIC_STAGE_ADDRESSES</asp:Label>
                                 </h2>
                                 <div class="stepformZone">
                                     <Elita:UserControlAddressInfo ID="repAddress" runat="server" Visible="True"></Elita:UserControlAddressInfo>
                                 </div>
                             </td>
                         </tr>
                     </table>
                        
                </div>
             
                <div id="modalClaimImages" class="overlay">
                    <div id="light" class="overlay_message_content" style="width: 1100px; left: 8%">
                        <p class="modalTitle">
                            <asp:Label ID="lblClaimImage" runat="server" Text="CLAIM_IMAGE"></asp:Label>
                            <a href="javascript:void(0)" onclick="hideModal('modalClaimImages');">
                                <img id="img3" src="~/App_Themes/Default/Images/icon_modalClose.png" runat="server"
                                    width="16" height="18" align="absmiddle" class="floatR" /></a>
                        </p>
                        <iframe class="pdfContainer" align="left" runat="server" id="pdfIframe"></iframe>
                    </div>
                    <div id="fade" class="black_overlay">
                    </div>
                </div>
                <div id="ModalIssue" class="overlay">
                    <div id="light" class="overlay_message_content" style="width: 500px">
                        <p class="modalTitle">
                            <asp:Label ID="Label1" runat="server" Text="NEW_CLAIM_ISSUE"></asp:Label>
                            <a href="javascript:void(0)" onclick="HideErrorAndModal('ModalIssue');">
                                <img id="img2" src="~/App_Themes/Default/Images/icon_modalClose.png" runat="server"
                                    width="16" height="18" align="absmiddle" class="floatR" /></a>
                        </p>
                        <div class="dataContainer">
                            <div runat="server" id="modalMessageBox" class="errorMsg" style="display: none">
                                <p>
                                    <img id="imgIssueMsg" width="16" height="13" align="middle" runat="server" src="~/App_Themes/Default/Images/icon_error.png" />
                                    <asp:Literal runat="server" ID="MessageLiteral" />
                                </p>
                            </div>
                        </div>
                        <table width="100%" border="0" class="formGrid" cellpadding="0" cellspacing="0">
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblIssueCode" runat="server" Text="ISSUE_CODE"></asp:Label>:
                                </td>
                                <td colspan="2">
                                    <asp:DropDownList ID="ddlIssueCode" runat="server" SkinID="MediumDropDown" AutoPostBack="false">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblIssueDescription" runat="server" Text="ISSUE_DESCRIPTION"></asp:Label>:
                                </td>
                                <td colspan="2">
                                    <asp:DropDownList ID="ddlIssueDescription" runat="server" SkinID="MediumDropDown"
                                        AutoPostBack="false">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" class="seperator">
                                    <img id="Img4" src="~/App_Themes/Default/Images/icon_dash.png" runat="server" width="6"
                                        height="5" />
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblCreatedDate" runat="server" Text="CREATED_DATE"></asp:Label>:
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtCreatedDate" runat="server" SkinID="MediumTextBox" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right">
                                    <asp:Label ID="lblCreatedBy" runat="server" Text="CREATED_BY"></asp:Label>:
                                </td>
                                <td colspan="2">
                                    <asp:TextBox ID="txtCreatedBy" runat="server" SkinID="MediumTextBox" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="3" align="right">&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2" align="right">&nbsp;
                                </td>
                                <td>
                                    <asp:Button ID="step3_modalClaimIssue_btnSave" runat="server" SkinID="PrimaryRightButton"
                                        Text="SAVE" OnClientClick="return validate();" />
                                    <input id='step3_modalClaimIssue_btnCancel' runat="server" type="button" name="Cancel"
                                        value="Cancel" onclick="HideErrorAndModal('ModalIssue');" class='popWindowCancelbtn
                                        floatR' />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="fade" class="black_overlay">
                    </div>
                </div>
                <div id="modalCollectDeductible" class="overlay">
                    <div id="light" class="overlay_message_content" style="width: 45%; top: 50px; overflow: hidden;">
                        <p class="modalTitle">
                            <asp:Label ID="lblCollectDeductible" runat="server" Text="COLLECT_DEDUCTIBLE"></asp:Label>
                            <a href="javascript:void(0)" onclick="hideModal('modalCollectDeductible');">
                                <img id="img5" src="~/App_Themes/Default/Images/icon_modalClose.png" runat="server"
                                    width="16" height="18" align="middle" class="floatR" /></a>
                        </p>
                        <Elita:MessageController runat="server" ID="moModalCollectDivMsgController" />
                        <table width="100%" border="0" class="formGrid" cellpadding="0" cellspacing="0">
                            <tr>
                                <td>
                                    <span class="mandatory">*</span><asp:Label ID="step3_lblDedCollMethod" runat="server">DED_COLL_METHOD</asp:Label>
                                </td>
                                <td>
                                    <asp:DropDownList ID="step3_cboDedCollMethod" runat="server" SkinID="MediumDropDown"
                                        AutoPostBack="True">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="step3_lblDedCollAuthCode" runat="server">CC_AUTH_CODE</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="step3_txtDedCollAuthCode" runat="server" SkinID="MediumTextBox"
                                        Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
                                </td>
                                <td>
                                    <asp:Button ID="btnDedCollContinue" runat="server" CssClass="primaryBtn
                                            floatR"
                                        Text="CONTINUE" CausesValidation="false" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="fade" class="black_overlay">
                    </div>
                </div>
            </div>
            <div id="dvStep4" runat="server">
                <div class="dataContainer">
                    <h2 class="dataGridHeader" runat="server" id="searchServiceCenterH2">
                        <asp:Label runat="server" ID="moSearchServiceCenterLabel" Text="SEARCH_SERVICE_CENTER" /></h2>
                    <div class="stepformZone">
                        <table class="formGrid" border="0" cellpadding="0" cellspacing="0">
                            <tbody>
                                <tr>
                                    <td align="right" nowrap="nowrap">
                                        <asp:Label runat="server" ID="step4_moSearchByLabel" Text="SEARCH_BY" />
                                        :
                                    </td>
                                    <td align="left" nowrap="nowrap">
                                        <asp:RadioButton ID="step4_RadioButtonByZip" runat="server" AutoPostBack="True" Text="BY_ZIP"
                                            GroupName="SEARCH_TYPE"></asp:RadioButton>
                                    </td>
                                    <td align="left" nowrap="nowrap">
                                        <asp:RadioButton ID="step4_RadioButtonByCity" runat="server" AutoPostBack="True"
                                            Text="BY_CITY" GroupName="SEARCH_TYPE"></asp:RadioButton>
                                    </td>
                                    <td align="left" nowrap="nowrap">
                                        <asp:RadioButton ID="step4_RadioButtonAll" runat="server" AutoPostBack="True" Text="ALL"
                                            GroupName="SEARCH_TYPE"></asp:RadioButton>
                                    </td>
                                    <td align="left" nowrap="nowrap">
                                        <asp:RadioButton ID="step4_RadioButtonNO_SVC_OPTION" runat="server" AutoPostBack="True" Text="NO_SVC_OPTION" GroupName="SEARCH_TYPE"></asp:RadioButton>
                                    </td>
                                    <td class="padLeft60" nowrap="nowrap" runat="server" id="step4_tdCountryLabel">
                                        <asp:Label runat="server" ID="moCountryLabel" Text="COUNTRY" />
                                        :
                                    </td>
                                    <td nowrap="nowrap">
                                        <asp:DropDownList ID="step4_moCountryDrop" runat="server" SkinID="SmallDropDown"
                                            AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                    <td nowrap="nowrap" runat="server" id="step4_tdCityLabel">
                                        <asp:Label runat="server" ID="step4_moCityLabel" Text="CITY" />
                                        :
                                    </td>
                                    <td nowrap="nowrap" runat="server" id="step4_tdCityTextBox">
                                        <asp:TextBox ID="step4_TextboxCity" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>

                                    <td runat="server" id="step4_tdClearButton">
                                        <asp:Button ID="step4_btnClearSearch" runat="server" SkinID="AlternateLeftButton"
                                            Text="Clear"></asp:Button>
                                    </td>
                                    <td runat="server" id="tdSearchButton">
                                        <asp:Button ID="step4_btnSearch" runat="server" Text="Search" SkinID="SearchButton"></asp:Button>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" runat="server" id="step4_tdServiceCenterLabel" colspan="11">
                                        <table>
                                            <tbody>
                                                <Elita:MultipleColumnDDLabelControl runat="server" ID="step4_moMultipleColumnDrop" />
                                            </tbody>
                                        </table>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div class="dataContainer" runat="server" id="step4_DataGridPlaceHolder">
                    <asp:Xml ID="xmlSource" runat="server"></asp:Xml>
                </div>
            </div>
            <div id="dvStep5" runat="server">
                <div class="dataContainer">
                    <div class="stepformZone">
                        <table class="formGrid" border="0" cellspacing="0" cellpadding="0">
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="step5_lblCertificate" runat="server" Font-Bold="false">CERTIFICATE</asp:Label>:
                                </td>
                                <td nowrap="nowrap" colspan="2">
                                    <asp:TextBox ID="step5_TextboxCertificate" runat="server" SkinID="MediumTextBox"
                                        ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="step5_LabelDealer" runat="server" Font-Bold="false">DEALER</asp:Label>:
                                </td>
                                <td nowrap="nowrap" colspan="2">
                                    <asp:TextBox ID="step5_TextboxDealer" runat="server" SkinID="MediumTextBox" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="step5_LabelDateTime" runat="server" Font-Bold="false">DATE_TIME</asp:Label>
                                </td>
                                <td nowrap="nowrap" colspan="2">
                                    <asp:TextBox ID="step5_TextboxDateTime" runat="server" SkinID="MediumTextBox" ReadOnly="true"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="step5_LabelCallerName" runat="server" Font-Bold="false">NAME_OF_CALLER</asp:Label>
                                </td>
                                <td nowrap="nowrap" colspan="2">
                                    <asp:TextBox ID="step5_TextboxCallerName" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap" colspan="3" />
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="step5_LabelCommentType" runat="server" Font-Bold="false">COMMENT_TYPE</asp:Label>
                                </td>
                                <td nowrap="nowrap" colspan="2">
                                    <asp:DropDownList ID="step5_cboCommentType" runat="server" SkinID="MediumDropDown">
                                    </asp:DropDownList>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="step5_LabelComment" runat="server" Font-Bold="false">COMMENT</asp:Label>:
                                </td>
                                <td nowrap="nowrap" colspan="2">
                                    <asp:TextBox ID="step5_TextboxCommentText" runat="server" TextMode="MultiLine" Rows="5"
                                        Columns="45" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </div>
            <div class="btnZone">
                <asp:Button ID="btnContinue" runat="server" SkinID="PrimaryRightButton" Text="Continue" />
                <asp:Button ID="btnSave_WRITE" TabIndex="190" runat="server" Text="Save" SkinID="PrimaryRightButton" />
                <asp:LinkButton ID="btnCancel" runat="server" SkinID="AlternateRightButton" Text="Cancel"
                    OnClientClick="return revealModal('ModalCancel');" />
                <asp:Button ID="btnBack" runat="server" SkinID="AlternateLeftButton" Text="Back" />
                <asp:Button ID="btnUndo_Write" TabIndex="195" runat="server" Font-Bold="false" Text="Undo"
                    SkinID="AlternateLeftButton" />
                <asp:Button ID="btnEdit_WRITE" TabIndex="200" runat="server" Text="Edit" CausesValidation="False"
                    SkinID="AlternateLeftButton" />
                <asp:Button ID="btnSoftQuestions" TabIndex="190" runat="server" Font-Bold="false"
                    Text="Soft Questions" SkinID="AlternateLeftButton" />
                <asp:Button ID="btnClaimOverride_Write" TabIndex="190" runat="server" Text="Override"
                    SkinID="AlternateLeftButton" />
                <asp:Button ID="btnComment" TabIndex="190" runat="server" Text="Comments" SkinID="AlternateLeftButton" />
                <asp:Button ID="btnClaimDeductibleRefund" TabIndex="190"  runat="server" Text="CLAIM_DEDUCTIBLE_REFUND" SkinID="AlternateLeftButton" Visible="false" />
               <asp:Button ID="btnDenyClaim" runat="server" Font-Bold="false" TabIndex="195" Text="Deny_Claim"
                    SkinID="AlternateLeftButton" />
                <asp:LinkButton ID="btnCancel_Write" TabIndex="190" runat="server" Text="Cancel"
                    OnClientClick="return revealModal('ModalCancel');" SkinID="AlternateRightButton"
                    Visible="false" />
                <asp:LinkButton ID="btnCancelClaim" TabIndex="190" runat="server" Text="Cancel" OnClientClick="return revealModal('ModalClaimCancel');"
                    SkinID="AlternateRightButton" Visible="false" />
                <%-- <asp:Button ID="BtnVerifyEquipment_WRITE"
    Visible="False" TabIndex="190" runat="server" Font-Bold="false" Text="VERIFY_EQUIPMENT"
    SkinID="PrimaryRightButton" />--%>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="hdnSelectedIssueCode" runat="server" />
    <asp:HiddenField ID="HiddenSaveChangesPromptResponse" runat="server" />
    <asp:HiddenField ID="hdnSelectedServiceCenterId" runat="server" Value="XXXX" />
    <asp:HiddenField ID="CurrentPage" runat="server" Value="1" />
    <input id="hdnDealerId" type="hidden" name="hdnDealerId" runat="server" />
    <input id="hdnSelectedClaimedSku" type="hidden" name="hdnSelectedClaimedSku" runat="server" />
    <input id="hdnSelectedEnrolledSku" type="hidden" name="hdnSelectedEnrolledSku" runat="server" />
    <script language="jscript" type="text/jscript">
        function SelectServiceCenter(theID) {
            var selectedServiceCenterId = '<%=hdnSelectedServiceCenterId.ClientID%>';;
            document.getElementById(selectedServiceCenterId).value = theID;
        }

        function openClose(theID) {
            if (document.getElementById(theID).style.display == "block") {
                document.getElementById(theID).style.display = "none";
                document.getElementById("tick_" + theID).innerHTML = "+";
            }
            else {
                document.getElementById(theID).style.display = "block";
                document.getElementById("tick_" + theID).innerHTML = "-";
            }
        }

        function validate() {
            var ddlIssueCode = document.getElementById('<%=ddlIssueCode.ClientID %>');
            if (ddlIssueCode.options[ddlIssueCode.selectedIndex].value == '00000000-0000-0000-0000-000000000000') {
                var msgBox = document.getElementById('<%=modalMessageBox.ClientID %>');
                msgBox.style.display = 'block';
                return false;
            }
            return true;
        }

        function RefreshDropDownsAndSelect(ctlCodeDropDown, ctlDecDropDown, isSingleSelection, change_Dec_Or_Code) {
            RefreshDualDropDownsSelection(ctlCodeDropDown, ctlDecDropDown, isSingleSelection, change_Dec_Or_Code);
            var objCodeDropDown = document.getElementById(ctlCodeDropDown); // "By Code" DropDown control
            var objDecDropDown = document.getElementById(ctlDecDropDown);
            var hdnSelectedIssue = document.getElementById('<%=hdnSelectedIssueCode.ClientID %>');
            hdnSelectedIssue.value = objDecDropDown.options[objDecDropDown.selectedIndex].value;
            var ddlIssueCode = document.getElementById('<%=ddlIssueCode.ClientID %>');
            if (ddlIssueCode.options[ddlIssueCode.selectedIndex].value != '00000000-0000-0000-0000-000000000000') {
                var msgBox = document.getElementById('<%=modalMessageBox.ClientID %>');
                msgBox.style.display = 'none';
            }
        }

        function HideErrorAndModal(divId) {
            var msgBox = document.getElementById('<%=modalMessageBox.ClientID %>');
            msgBox.style.display = 'none';
            var objCodeDropDown = document.getElementById('<%=ddlIssueCode.ClientID %>'); // "By Code" DropDown control
            var objDecDropDown = document.getElementById('<%=ddlIssueDescription.ClientID %>');
            objCodeDropDown.selectedIndex = 0;
            objDecDropDown.selectedIndex = 0;
            hideModal(divId);
        }

        function RevealModalWithMessage(divId) {
            var msgBox = document.getElementById('<%=modalMessageBox.ClientID %>');
            msgBox.style.display = 'block';
            revealModal(divId);
        }

        function showHidePage(newPageNumber, recordCount, pageSize) {
            var newPage = parseInt(newPageNumber);
            var currentPageId = '<%=CurrentPage.ClientID%>';
            var currentPage = parseInt(document.getElementById(currentPageId).value);
            var tempLocation;
            if (newPageNumber == document.getElementById(currentPageId).value) { return; }
            document.getElementById('pg1_' + document.getElementById(currentPageId).value).style.cursor = 'pointer';
            document.getElementById('pg1_' + document.getElementById(currentPageId).value).setAttribute('class', '');
            document.getElementById('pg2_' + document.getElementById(currentPageId).value).style.cursor = 'pointer';
            document.getElementById('pg2_' + document.getElementById(currentPageId).value).setAttribute('class', '');
            document.getElementById(currentPageId).value = newPageNumber;
            document.getElementById('pg1_' + document.getElementById(currentPageId).value).style.cursor = 'text';
            document.getElementById('pg1_' + document.getElementById(currentPageId).value).setAttribute('class', 'selected_page');
            document.getElementById('pg2_' + document.getElementById(currentPageId).value).style.cursor = 'text';
            document.getElementById('pg2_' + document.getElementById(currentPageId).value).setAttribute('class', 'selected_page');
            for (i = 1; i <= pageSize; i++) {
                tempLocation = (((currentPage - 1) * pageSize) + i);
                if (tempLocation < recordCount) {
                    document.getElementById('tr_' + tempLocation).style.display = "none";
                    document.getElementById('trd_' + tempLocation).style.display = "none";
                }
                tempLocation = (((newPage - 1) * pageSize) + i);
                if (tempLocation < recordCount) {
                    document.getElementById('tr_' + tempLocation).style.display = "block";
                    document.getElementById('trd_' + tempLocation).style.display = "block";
                }
            }
            SelectServiceCenter('XXXX');
        }

        if (document.getElementById('pg1_1') != null) {
            document.getElementById('pg1_1').style.cursor = 'text';
            document.getElementById('pg1_1').setAttribute('class', 'selected_page');
        }

        if (document.getElementById('pg2_1') != null) {
            document.getElementById('pg2_1').style.cursor = 'text';
            document.getElementById('pg2_1').setAttribute('class', 'selected_page');
        }

        var hdnDealerId = '<%=hdnDealerId.ClientId%>';
        function LoadSKU(ctrlManufaturer, ctrlModel, ctrlSKU, ctrlHdnField) {

            var claimedManufacturerId = $('#' + ctrlManufaturer).val();
            var claimedmodel = $('#' + ctrlModel).val();
            var dealerId = $('#' + hdnDealerId).val();

            if (claimedManufacturerId.length > 0 && claimedManufacturerId != '00000000-0000-0000-0000-000000000000' && claimedmodel.length > 0) {
                $.ajax({
                    type: "POST",
                    url: "ClaimWizardForm.aspx/LoadSku",
                    data: '{ manufacturerId: "' + claimedManufacturerId + '",model:"' + claimedmodel + '",dealerId:"' + dealerId + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        $('#' + ctrlSKU).empty();
                        if (msg.d == null) { return }

                        var jsonArray = jQuery.parseJSON(msg.d);

                        var listItems = "";
                        $.each(jsonArray, function () {
                            listItems += "<option value='" + this + "'>" + this + "</option>";

                        });
                        $('#' + ctrlSKU).html(listItems);
                        $('#' + ctrlSKU).selectedIndex = 0;
                        var hdnSelectedIssue = document.getElementById(ctrlHdnField);
                        hdnSelectedIssue.value = $('#' + ctrlSKU).val();
                    }
                });
            }
        }

        function FillHiddenField(sourceDropDownClientId, destinationControlClientId) {
            var hdnSelectedIssue = document.getElementById(destinationControlClientId);
            var objDecDropDown = document.getElementById(sourceDropDownClientId);
            hdnSelectedIssue.value = objDecDropDown.options[objDecDropDown.selectedIndex].value;
        }


    </script>
</asp:Content>
