<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LocateServiceCenterForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.LocateServiceCenterForm" Theme="Default"
    EnableSessionState="True" MasterPageFile="../Navigation/masters/ElitaBase.Master" %>

<%@ Register TagPrefix="Elita" TagName="MultipleColumnDDLabelControl" Src="~/Common/MultipleColumnDDLabelControl_New.ascx" %>
<%@ Register TagPrefix="Elita" TagName="ProtectionAndEventDetails" Src="~/Common/ProtectionAndEventDetails.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlWizard" Src="~/Common/UserControlWizard.ascx" %>
<%@ Register TagPrefix="Elita" Assembly="Assurant.ElitaPlus.WebApp" Namespace="Assurant.ElitaPlus.ElitaPlusWebApp" %>
<asp:Content ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <Elita:ProtectionAndEventDetails ID="moProtectionAndEventDetails" runat="server"></Elita:ProtectionAndEventDetails>
</asp:Content>
<asp:Content ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div id="ModalCancel" class="overlay">
        <div id="light" class="overlay_message_content">
            <p class="modalTitle">
            <table width="525"><tr><td align="left">
                <asp:Label ID="lblModalTitle" runat="server" Text="CONFIRM"></asp:Label></td><td align="right">
                <a href="javascript:void(0)" onclick="hideModal('ModalCancel');" rel="noopener noreferrer">
                    <img id="Img1" src="~/App_Themes/Default/Images/icon_modalClose.png" runat="server" width="16" height="18" align="right"></a></td></tr></table>
            <table class="formGrid" cellspacing="0" cellpadding="0" border="0" width="525">
                <tbody>
                    <tr>
                        <td align="right">
                            <img id="imgMsgIcon" name="imgMsgIcon" width="28" runat="server" src="~/App_Themes/Default/Images/dialogue_confirm.png" height="28">
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
                            <input id="btnModalCancelYes" class="primaryBtn floatR" runat="server" type="button" value="Yes">
                            <input id="Button1" class="popWindowAltbtn floatR" runat="server" type="button" value="No" onclick="hideModal('ModalCancel');">
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
        <div id="fade" class="black_overlay">
        </div>
    </div>
    <div class="dataContainer" runat="server" id="divSteps">
        <div class="stepWizBox">
            <Elita:UserControlWizard runat="server" ID="WizardControl">
                <Steps>
                    <Elita:StepDefinition StepNumber="1" StepName="DATE_OF_INCIDENT"></Elita:StepDefinition>
                    <Elita:StepDefinition StepNumber="2" StepName="COVERAGE_DETAILS"></Elita:StepDefinition>
                    <Elita:StepDefinition StepNumber="3" StepName="LOCATE_SERVICE_CENTER" IsSelected="true"></Elita:StepDefinition>
                    <Elita:StepDefinition StepNumber="4" StepName="CLAIM_DETAILS"></Elita:StepDefinition>
                    <Elita:StepDefinition StepNumber="5" StepName="SUBMIT_CLAIM"></Elita:StepDefinition>
                </Steps>
            </Elita:UserControlWizard>
        </div>
    </div>
    <div class="dataContainer">
        <h2 class="dataGridHeader" runat="server" id="searchServiceCenterH2">
            <asp:Label runat="server" ID="moSearchServiceCenterLabel" Text="SEARCH_SERVICE_CENTER"></asp:Label></h2>
        <div class="stepformZone">
            <table class="formGrid" border="0" cellpadding="0" cellspacing="0">
                <tbody>
                    <tr>
                        <td align="right" nowrap="nowrap">
                            <asp:Label runat="server" ID="moSearchByLabel" Text="SEARCH_BY"></asp:Label>
                            :
                        </td>
                        <td align="left" nowrap="nowrap">
                            <asp:RadioButton ID="RadioButtonByZip" runat="server" AutoPostBack="True" Text="BY_ZIP" GroupName="SEARCH_TYPE"></asp:RadioButton>
                        </td>
                        <td align="left" nowrap="nowrap">
                            <asp:RadioButton ID="RadioButtonByCity" runat="server" AutoPostBack="True" Text="BY_CITY" GroupName="SEARCH_TYPE"></asp:RadioButton>
                        </td>
                        <td align="left" nowrap="nowrap">
                            <asp:RadioButton ID="RadioButtonAll" runat="server" AutoPostBack="True" Text="ALL" GroupName="SEARCH_TYPE"></asp:RadioButton>
                        </td>
                        <td align="left" nowrap="nowrap">
                             <asp:RadioButton ID="RadioButtonNO_SVC_OPTION" runat="server" AutoPostBack="True" Text="NO_SVC_OPTION" GroupName="SEARCH_TYPE"></asp:RadioButton>
                        </td>
                        <td class="padLeft60" nowrap="nowrap" runat="server" id="tdCountryLabel">
                            <asp:Label runat="server" ID="moCountryLabel" Text="COUNTRY"></asp:Label>
                            :
                        </td>
                        <td nowrap="nowrap">
                            <asp:DropDownList ID="moCountryDrop" runat="server" SkinID="SmallDropDown" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td nowrap="nowrap" runat="server" id="tdCityLabel">
                            <asp:Label runat="server" ID="moCityLabel" Text="CITY"></asp:Label>
                            :
                        </td>
                        <td nowrap="nowrap" runat="server" id="tdCityTextBox">
                            <asp:TextBox ID="TextboxCity" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>

                        <td runat="server" id="tdClearButton">
                            <asp:Button ID="btnClearSearch" runat="server" SkinID="AlternateLeftButton" Text="Clear">
                            </asp:Button>
                        </td>
                        <td runat="server" id="tdSearchButton">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" SkinID="SearchButton"></asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td runat="server" id="tdServiceCenterLabel" colspan="11" align="left">
                            <table>
                                <tbody>
                                    <Elita:MultipleColumnDDLabelControl runat="server" ID="moMultipleColumnDrop"></Elita:MultipleColumnDDLabelControl>
                                </tbody>
                            </table>
                        </td>                    
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    <div class="dataContainer" runat="server" id="DataGridPlaceHolder">
        <asp:Xml ID="xmlSource" runat="server"></asp:Xml>
    </div>
    <div class="btnZone">
        <!--START   DEF-2539-->
        <asp:Button ID="btnContinue" Text="Continue" SkinID="PrimaryRightButton" runat="server"></asp:Button>
        <asp:LinkButton ID="lnkCancel" Text="Cancel" SkinID="AlternateRightButton" runat="server" OnClientClick="return revealModal('ModalCancel');"></asp:LinkButton>
        <asp:Button ID="btnBack" Text="Back" SkinID="AlternateLeftButton" runat="server"></asp:Button>
        <!--END   DEF-2539-->
        <asp:HiddenField ID="selectedServiceCenterId" runat="server" Value="XXXX"></asp:HiddenField>
        <asp:HiddenField ID="CurrentPage" runat="server" Value="1"></asp:HiddenField>
    </div>
    <script language="jscript" type="text/jscript">
        function SelectServiceCenter(theID) {
            var selectedServiceCenterId = '<%=selectedServiceCenterId.ClientID%>'; ;
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

    </script>
</asp:Content>
