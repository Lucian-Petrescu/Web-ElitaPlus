<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LocateEligibleCoverageListForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Certificates.LocateEligibleCoverageListForm"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" Theme="Default" %>

<%@ Register TagPrefix="Elita" TagName="UserControlCertificateInfo" Src="UserControlCertificateInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="~/Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc2" TagName="ProtectionAndEventDetails" Src="~/Common/ProtectionAndEventDetails.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlWizard" Src="~/Common/UserControlWizard.ascx" %>
<%@ Register TagPrefix="Elita" Assembly="Assurant.ElitaPlus.WebApp" Namespace="Assurant.ElitaPlus.ElitaPlusWebApp" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <uc2:ProtectionAndEventDetails ID="moProtectionAndEventDetails" runat="server" align="center" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto">
        <Scripts>
            <asp:ScriptReference Path="~/Navigation/scripts/ComunaSuggest.js" />
        </Scripts>
    </asp:ScriptManager>
    <!--Start Header-->
    <div class="container">
        <div class="contentZoneHome">
            <div class="dataContainer">
                <div class="stepWizBox">
                   <Elita:UserControlWizard runat="server" ID="WizardControl">
                        <Steps>
                            <Elita:StepDefinition StepNumber="1" StepName="DATE_OF_INCIDENT" IsSelected="true"/>
                            <Elita:StepDefinition StepNumber="2" StepName="COVERAGE_DETAILS" />
                            <Elita:StepDefinition StepNumber="3" StepName="LOCATE_SERVICE_CENTER"  />
                            <Elita:StepDefinition StepNumber="4" StepName="CLAIM_DETAILS" />
                            <Elita:StepDefinition StepNumber="5" StepName="SUBMIT_CLAIM" />
                        </Steps>
                    </Elita:UserControlWizard>
                </div>
            </div>
            <div class="dataContainer">
                <h2 class="dataGridHeader">Incident Information</h2>
                <div class="stepformZone">
                    <table class="formGrid" cellspacing="0" cellpadding="0" border="0">
                        <tbody>
                            <tr>
                                <td nowrap="nowrap" align="right"><span class="mandate">*</span><asp:Label ID="lblDateOfLoss" runat="server">DATE_OF_INCIDENT</asp:Label>:</td>
                                <td nowrap="nowrap"><asp:TextBox ID="moDateOfLossText" TabIndex="1" runat="server" SkinID="smallTextBox"></asp:TextBox></td>
                                <td><asp:ImageButton ID="BtnDateOfLoss" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png" valign="bottom"></asp:ImageButton></td>    
                                <td nowrap="nowrap" align="right"><span class="mandate">*</span><asp:Label ID="lblDateReported" runat="server">DATE_REPORTED</asp:Label>:</td>
                                <td><asp:TextBox ID="txtDateReported" TabIndex="2"  runat="server" SkinID="smallTextBox"></asp:TextBox></td>
                                <td><asp:ImageButton ID="BtnDateReported" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png" valign="bottom"></asp:ImageButton></td>            
                                <td><asp:Button ID="btnSearch" runat="server" SkinID="SearchButton" Text="Search"></asp:Button></td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
            <asp:panel id="SearchResultPanel" runat="server">
                <div class="dataContainer">
                    <h2 class="dataGridHeader">Select Eligible Coverage</h2>
                    <div class="stepformZone">
                        <table class="formGrid" cellspacing="0" cellpadding="0" border="0">
                            <tbody>
                                <tr id="RiskTypeTR" runat="server">
                                    <td nowrap="nowrap" align="right"><span class="mandate">*</span><asp:label ID="RiskTypeLabel" runat="server" Text="RISK_TYPE"></asp:label>:</td>
                                    <td nowrap="nowrap"><asp:DropDownList ID="cboRiskType" runat="server" SkinID="MediumDropDown" AutoPostBack="True"></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap" align="right"><span class="mandate">*</span><asp:label ID="CoverageTypeLabel" runat="server" Text="COVERAGE_TYPE"></asp:label>:</td>
                                    <td nowrap="nowrap"><asp:DropDownList ID="cboCoverageType" runat="server" SkinID="MediumDropDown" ></asp:DropDownList></td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap" align="right"><span class="mandate">*</span><asp:label ID="CallerNameLabel" runat="server" Text="NAME_OF_CALLER"></asp:label>:</td>    
                                    <td nowrap="nowrap"><asp:TextBox ID="TextCallerName" runat="server" SkinID="MediumTextBox"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap" align="right"><span class="mandate">*</span><asp:label ID="ProblemDescriptionLabel" runat="server" Text="PROBLEM_DESCRIPTION"></asp:label>:</td>
                                    <td nowrap="nowrap"><asp:TextBox ID="TextProblemDescription" runat="server" TextMode="MultiLine"></asp:TextBox></td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
            </asp:panel>
            <div class="btnZone">
                <!--START DEF-2539 -->
                <asp:Button ID="btnContinue" runat="server" SkinID="PrimaryRightButton" Text="Continue"/>
                <asp:LinkButton ID="btnCancel" runat="server" SkinID="AlternateRightButton" Text="Cancel"/> 
                <asp:Button ID="btnBack" runat="server" SkinID="AlternateLeftButton" Text="Back"/>
            <!--END DEF-2539-->
            </div>
        </div>
    </div>
</asp:Content>
