<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CommentForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.CommentForm"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" Theme="Default" %>

<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlCertificateInfo" Src="UserControlCertificateInfo_New.ascx" %>
<%@ Register TagPrefix="uc2" TagName="ProtectionAndEventDetails" Src="~/Common/ProtectionAndEventDetails.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlWizard" Src="~/Common/UserControlWizard.ascx" %>
<%@ Register TagPrefix="Elita" Assembly="Assurant.ElitaPlus.WebApp" Namespace="Assurant.ElitaPlus.ElitaPlusWebApp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <uc2:ProtectionAndEventDetails ID="moProtectionEvtDtl" runat="server" align="center" />
    <table id="TableFixed" cellspacing="0" cellpadding="0" border="0" width="80%" class="summaryGrid">
        <tbody>
            <Elita:UserControlCertificateInfo ID="moCertificateInfoController" runat="server" align="center" />
        </tbody>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto">
        <Scripts>
            <asp:ScriptReference Path="~/Navigation/scripts/GlobalHeader.js" />
        </Scripts>
    </asp:ScriptManager>
    <div class="container">
        <div class="contentZoneHome">
            <div class="dataContainer">
                <div class="stepWizBox" id="stepWizBox" runat="server">
                    <Elita:UserControlWizard runat="server" ID="WizardControl">
                        <Steps>
                            <Elita:StepDefinition StepNumber="1" StepName="DATE_OF_INCIDENT" />
                            <Elita:StepDefinition StepNumber="2" StepName="COVERAGE_DETAILS" />
                            <Elita:StepDefinition StepNumber="3" StepName="LOCATE_SERVICE_CENTER" />
                            <Elita:StepDefinition StepNumber="4" StepName="CLAIM_DETAILS" />
                            <Elita:StepDefinition StepNumber="5" StepName="SUBMIT_CLAIM" IsSelected="true" />
                        </Steps>
                    </Elita:UserControlWizard>
                </div>
            </div>
            <div class="dataContainer">
                <h2 class="dataGridHeader">
                    <asp:Label ID="LabelTitle" runat="server">NEW_CLAIM_SUMMARY</asp:Label>
                    <span class="floatR">Added by :
                        <asp:Label ID="LabelAddedBy" runat="server"></asp:Label></span></h2>
                <div class="stepformZone">
                    <table class="formGrid" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="Certificate" runat="server" Font-Bold="false">CERTIFICATE</asp:Label>:
                            </td>
                            <td nowrap="nowrap" colspan="2">
                                <asp:TextBox ID="TextboxCertificate" runat="server" SkinID="MediumTextBox" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="LabelDealer" runat="server" Font-Bold="false">DEALER</asp:Label>:
                            </td>
                            <td nowrap="nowrap" colspan="2">
                                <asp:TextBox ID="TextboxDealer" runat="server" SkinID="MediumTextBox" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="LabelDateTime" runat="server" Font-Bold="false">DATE_TIME</asp:Label>
                            </td>
                            <td nowrap="nowrap" colspan="2">
                                <asp:TextBox ID="TextboxDateTime" runat="server" SkinID="MediumTextBox" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="LabelCallerName" runat="server" Font-Bold="false">NAME_OF_CALLER</asp:Label>
                            </td>
                            <td nowrap="nowrap" colspan="2">
                                <asp:TextBox ID="TextboxCallerName" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap="nowrap" colspan="3" />
                        </tr>
                        <tr>
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="LabelCommentType" runat="server" Font-Bold="false">COMMENT_TYPE</asp:Label>
                            </td>
                            <td nowrap="nowrap" colspan="2">
                                <asp:DropDownList ID="cboCommentType" runat="server" SkinID="MediumDropDown">
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="LabelComment" runat="server" Font-Bold="false">COMMENT</asp:Label>:
                            </td>
                            <td nowrap="nowrap" colspan="2">
                                <asp:TextBox ID="TextboxCommentText" runat="server" TextMode="MultiLine" Rows="5"
                                    Columns="45" SkinID="MediumTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="TREmailToCustomer" runat="server">
                            <td>
                            </td>
                            <td align="left" nowrap="nowrap">
                                <asp:CheckBox ID="cbEmailToCustomer" runat="server" CssClass="disabled" />
                            </td>
                            <td nowrap="nowrap">
                                <asp:Label ID="LabelEmialToCustomer" runat="server" Text="SEND_AN_EMAIL_TO_CUSTOMER"></asp:Label>
                            </td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="EmailToCustomerText" runat="server" SkinID="MediumTextBox" ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="TREmailToServiceCenter" runat="server">
                            <td>
                            </td>
                            <td align="left" nowrap="nowrap">
                                <asp:CheckBox ID="cbEmailToServiceCenter" runat="server" CssClass="disabled" />
                            </td>
                            <td nowrap="nowrap">
                                <asp:Label ID="LabelEmailToServiceCenter" runat="server" Text="SEND_AN_EMAIL_TO_SERVICE_CENTER"></asp:Label>
                            </td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="EmailToServiceCenterText" runat="server" SkinID="MediumTextBox"
                                    ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                        <tr id="TREmailToSalvageCenter" runat="server" Visible="false">
                            <td>
                            </td>
                            <td align="left" nowrap="nowrap">
                                <asp:CheckBox ID="cbEmailToSalvageCenter" runat="server" CssClass="disabled" />
                            </td>
                            <td nowrap="nowrap">
                                <asp:Label ID="LabelEmailToSalvageCenter" runat="server" Text="SEND_AN_EMAIL_TO_SALVAGE_CENTER"></asp:Label>
                            </td>
                            <td nowrap="nowrap">
                                <asp:TextBox ID="EmailToSalvageCenterText" runat="server" SkinID="MediumTextBox"
                                    ReadOnly="true"></asp:TextBox>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
            <div class="btnZone">
                <!--START   DEF-2539-->
                <asp:Button ID="btnAdd_WRITE" runat="server" SkinID="PrimaryRightButton" Text="SUBMIT_CLAIM" />
                <asp:Button ID="btnBack" runat="server" SkinID="AlternateLeftButton" Text="Back" />
                <!--END   DEF-2539-->
            </div>
        </div>
    </div>
    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
        runat="server" designtimedragdrop="261" />
</asp:Content>
