<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CertificateForm.aspx.vb"
    Theme="Default" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Certificates.CertificateForm"
    EnableSessionState="True" MasterPageFile="../Navigation/masters/ElitaBase.Master" %>

<%@ Register Assembly="Microsoft.Web.UI.WebControls" Namespace="Microsoft.Web.UI.WebControls"
    TagPrefix="iewc" %>
<%@ Register TagPrefix="Elita" TagName="UserControlPaymentOrderInfo" Src="../Common/UserControlPaymentOrderInfo.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlBankInfo" Src="../Common/UserControlBankInfo_New.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlCertificateInfo" Src="UserControlCertificateInfo_New.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlAddress" Src="../Common/UserControlAddress_New.ascx" %>

<asp:Content ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <style type="text/css">
        .style1 {
            height: 20px;
        }

        /* commented because IE 11 doesn't care about :not() and therefore has a different behaviour than Firefox, Chrome and others */
        /*li.ui-state-default.ui-state-hidden[role=tab]:not(.ui-tabs-active) {
            display: none;
        }*/
    </style>

    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.6.1.min.js"></script>
    <script type="text/javascript">
        function fillTB(tb, value) {
            document.getElementById(value).value = tb.value;
        }
    </script>


    <link type="text/css" href="../Navigation/styles/jquery-ui.min.css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.12.4.min.js"> </script>
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-ui.min.js"> </script>
    <link type="text/css" href="../Navigation/styles/jquery-ui.elita.css" rel="stylesheet" />



    <script type="text/javascript">    

        $(function () {

            var initDisabledTabs = $("input[id$='hdnInitDisabledTabs']").val().split(',');
            $.each(initDisabledTabs, function () {
                var ind = parseInt(this);
                if (ind != NaN) {
                    $($("#tabs").find("li")[ind]).addClass('ui-state-hidden');
                }
            });

            var disabledTabs = $("input[id$='hdnDisabledTabs']").val().split(',');
            var disabledTabsIndexArr = [];
            var isCertHistLoaded = 0;
            $.each(disabledTabs, function () {
                var tabIndex = parseInt(this);
                if (tabIndex != NaN) {
                    disabledTabsIndexArr.push(tabIndex);
                }
            });
            $("#tabs").tabs({
                activate: function () {
                    var selectedTab = $('#tabs').tabs('option', 'active');
                    $("#<%= hdnSelectedTab.ClientID %>").val(selectedTab);


                    if (selectedTab == 9 && isCertHistLoaded == 0 && _isSubmitting == false) {

                        var prm = Sys.WebForms.PageRequestManager.getInstance();
                        if (prm != null) {
                            prm.add_endRequest(UpdatePanelLoaded);
                        };
                        isCertHistLoaded = 1;
                        document.getElementById("<%=btnGetCertHistory.ClientID %>").click();
                    };
                },
                active: document.getElementById('<%= hdnSelectedTab.ClientID %>').value,
                disabled: disabledTabsIndexArr,

            });
            $("#tabs").removeAttr('style');



        });

        function UpdatePanelLoaded(sender, args) {
            if (_isSubmitting) {
                _isSubmitting = false;
                document.body.style.cursor = '';
            }
        }



        function Count(text, length) {
            var maxlength = length;
            var object = document.getElementById(text.id);
            if (object.value.length > maxlength) {

                object.focus();
                object.value = text.value.substring(0, maxlength);
                object.scrollTop = object.scrollHeight;
                return false;
            }
            return true;
        }
    </script>
    <style type="text/css">
        .verticalPanal {
            vertical-align: top;
        }
    </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="SummaryPlaceHolder" runat="server">


    <table id="TableFixed" cellspacing="0" cellpadding="0" border="0" width="80%" class="summaryGrid">
        <tbody>
            <Elita:UserControlCertificateInfo ID="moCertificateInfoController" runat="server"
                align="center" />
            <Elita:UserControlCertificateInfo ID="moCertificateInfoCtrlCancel" runat="server"
                align="center" />
            <tr>
                <td align="right" id="moCancelDueDateInformation">
                    <asp:Label ID="lblFutureCancelationDate" runat="server" Visible="false" SkinID="SummaryLabel">FUTURE_CANCELATION_DATE</asp:Label>
                </td>
                <td align="left" colspan="5">
                    <asp:TextBox ID="txtFutureCancelationDate" runat="server" Visible="false" ReadOnly="true"
                        SkinID="SmallTextBox"></asp:TextBox>
                    <asp:LinkButton ID="btnRemoveCancelDueDate_WRITE" runat="server" Visible="False">Remove</asp:LinkButton>
                    <input id="HiddenRemoveCancelDueDatePromptResponse" type="hidden" name="HiddenRemoveCancelDueDatePromptResponse"
                        runat="server" />
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
<asp:Content ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto">
        <Scripts>
            <asp:ScriptReference Path="~/Navigation/scripts/ComunaSuggest.js" />
        </Scripts>
    </asp:ScriptManager>
    <asp:Panel runat="server" ID="WorkingPanel">
        <div class="dataContainer">
            <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0" />
            <asp:HiddenField ID="hdnDisabledTabs" runat="server" Value="" />
            <asp:HiddenField ID="hdnInitDisabledTabs" runat="server" Value="NA" />
            <div id="tabs" class="style-tabs" style="display: none;">
                <ul>
                    <li><a href="#tabsCertDetail">
                        <asp:Label ID="Label6" runat="server" CssClass="tabHeaderText">CERTIFICATE_DETAIL</asp:Label></a></li>
                    <li><a href="#tabsGeneralInfo">
                        <asp:Label ID="Label7" runat="server" CssClass="tabHeaderText">General_Information</asp:Label></a></li>
                    <li><a href="#tabsItemInfo">
                        <asp:Label ID="Label8" runat="server" CssClass="tabHeaderText">ITEMS</asp:Label></a></li>
                    <li><a href="#tabsPremiumInfo">
                        <asp:Label ID="Label11" runat="server" CssClass="tabHeaderText">Premium_Information</asp:Label></a></li>
                    <li><a href="#tabsCancelRequest">
                        <asp:Label ID="Label12" runat="server" CssClass="tabHeaderText">REQUEST_CANCELLATION</asp:Label></a></li>
                    <li><a href="#tabsCancelInfo">
                        <asp:Label ID="Label13" runat="server" CssClass="tabHeaderText">Cancellation_Information</asp:Label></a></li>
                    <li><a href="#tabsComments">
                        <asp:Label ID="lblTabCommentHeader" runat="server" CssClass="tabHeaderText">COMMENTS</asp:Label></a></li>
                    <li><a href="#tabsEndorsement">
                        <asp:Label ID="Label15" runat="server" CssClass="tabHeaderText">ENDORSEMENTS</asp:Label></a></li>
                    <li><a href="#tabsTaxId">
                        <asp:Label ID="Label16" runat="server" CssClass="tabHeaderText">Tax ID</asp:Label></a></li>
                    <li><a href="#tabsCertHistory">
                        <asp:Label ID="Label17" runat="server" CssClass="tabHeaderText">CERTIFICATE_HISTORY</asp:Label></a></li>
                    <li><a href="#tabsCovHistory">
                        <asp:Label ID="Label18" runat="server" CssClass="tabHeaderText">Coverage_History</asp:Label></a></li>
                    <li><a href="#tabsFinanceInfo">
                        <asp:Label ID="Label19" runat="server" CssClass="tabHeaderText">Finance_Information</asp:Label></a></li>
                    <li><a href="#tabsReprice">
                        <asp:Label ID="Label20" runat="server" CssClass="tabHeaderText">REPRICE</asp:Label></a></li>
                    <li><a href="#tabsDataProtection">
                        <asp:Label ID="Label22" runat="server" CssClass="tabHeaderText">Data_Protection</asp:Label></a></li>
                    <li><a href="#tabsMigratedCertificateLink">
                        <asp:Label ID="Label23" runat="server" CssClass="tabHeaderText">CERTIFICATE_LINKS_TAB</asp:Label></a></li>
                    <li><a href="#tabsCertificateExtendedFields">
                        <asp:Label ID="lblCertificateExtendedFields" runat="server" CssClass="tabHeaderText">CERT_EXT_FIELDS</asp:Label></a></li>
                </ul>
                <div id="tabsCertDetail">
                    <div class="Page" runat="server" id="moCertificateDetailPanel" style="display: block; height: 300px; overflow: auto">
                        <table width="100%" class="formGrid" border="0">
                            <tbody>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moSalutationLabel" runat="server">SALUTATION</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moSalutationText" TabIndex="1" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                        <asp:DropDownList ID="cboSalutationId" runat="server" SkinID="SmallDropDown">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" id="moCustName01" runat="server">
                                        <asp:Label ID="moCustomerNameLabel" runat="server">CUSTOMER_NAME</asp:Label>
                                    </td>
                                    <td align="left" id="moCustName02" runat="server">
                                        <asp:TextBox ID="moCustomerNameText" TabIndex="1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moLangPrefLabel" runat="server">LANGUAGE_PREF</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moLangPrefText" TabIndex="1" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                        <asp:DropDownList ID="cboLangPref" TabIndex="1" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr runat="server" id="moCustName1">
                                    <td align="right">
                                        <asp:Label ID="moCustomerFirstNameLabel" runat="server">CUSTOMER_FIRST_NAME</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moCustomerFirstNameText" TabIndex="1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moCustomerMiddleNameLabel" runat="server">CUSTOMER_MIDDLE_NAME</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moCustomerMiddleNameText" TabIndex="1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr runat="server" id="moCustName2">
                                    <td align="right">
                                        <asp:Label ID="moCustomerLastNameLabel" runat="server">CUSTOMER_LAST_NAME</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moCustomerLastNameText" TabIndex="1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moCorporateNameLabel" runat="server">CORPORATE_NAME</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moCorporateNameText" TabIndex="1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr runat="server" id="moAltCustFirstName">
                                    <td align="right">
                                        <asp:Label ID="moAlternativeFirstNameLabel" runat="server">ALTERNATIVE_FIRST_NAME</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moAlternativeFirstNameText" TabIndex="1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td colspan="2"></td>
                                </tr>

                                <tr runat="server" id="moAltCustLasName">
                                    <td align="right">
                                        <asp:Label ID="moAlternativeLastNameLabel" runat="server">ALTERNATIVE_LAST_NAME</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moAlternativeLastNameText" TabIndex="1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td colspan="2"></td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moEmailAddressLabel" runat="server">EMAIL</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moEmailAddressText" TabIndex="1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moHomePhoneLabel" runat="server">HOME_PHONE</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moHomePhoneText" TabIndex="1" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moTaxIdLabel" runat="server">TAX_ID</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moTaxIdText" TabIndex="1" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moWorkPhoneLabel" runat="server">WORK_PHONE</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moWorkPhoneText" TabIndex="1" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr runat="server" id="moCustLegalInfo1">
                                    <td align="right">
                                        <asp:Label ID="moOccupationLabel" runat="server">OCCUPATION</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moOccupationText" TabIndex="1" runat="server" CssClass="FLATTEXTBOX_TAB"
                                            Width="95%"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moPoliticallyExposedLabel" runat="server">POLITICALLY_EXPOSED</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moPoliticallyExposedText" TabIndex="1" runat="server" CssClass="FLATTEXTBOX_TAB"></asp:TextBox>
                                        <asp:DropDownList ID="cboPoliticallyExposedId" TabIndex="1" runat="server">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr runat="server" id="moCustLegalInfo2">
                                    <td align="right">
                                        <asp:Label ID="moIncomeRangeLabel" runat="server">INCOME_RANGE</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moIncomeRangeText" TabIndex="1" runat="server" CssClass="FLATTEXTBOX_TAB"
                                            Width="95%"></asp:TextBox>
                                        <asp:DropDownList ID="cboIncomeRangeId" TabIndex="1" runat="server" Width="50%">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right" id="tdDateOfBirthTag" runat="server">
                                        <asp:Label ID="moDateOfBirthLabel" runat="server">DATE_OF_BIRTH</asp:Label>
                                    </td>
                                    <td align="left" id="tdDateOfBirthCalTag">
                                        <asp:TextBox ID="moDateOfBirthText" TabIndex="1" runat="server" CssClass="FLATTEXTBOX_TAB"></asp:TextBox>
                                        <asp:ImageButton ID="BtnDateOfBirth" runat="server" Width="20px" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:ImageButton>
                                    </td>
                                </tr>
                                <Elita:UserControlAddress ID="moAddressController" runat="server"></Elita:UserControlAddress>
                                <tr runat="server" id="moAMLRegulations0">
                                    <td align="right">
                                        <asp:Label ID="moMaritalStatusLabel" runat="server">MARITAL_STATUS</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlMaritalStatus" runat="server" TabIndex="1" SkinID="MediumDropDown">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="moMaritalStatusText" TabIndex="2" runat="server" SkinID="MediumTextBox" CssClass="FLATTEXTBOX_TAB"></asp:TextBox>
                                    </td>
                                    <td align="right"></td>
                                    <td align="left"></td>
                                </tr>
                                <tr runat="server" id="moAMLRegulations1">
                                    <td align="right">
                                        <asp:Label ID="moNationalityLabel" runat="server">NATIONALITY</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlNationality" runat="server" TabIndex="1" SkinID="MediumDropDown" CssClass="FLATTEXTBOX_TAB">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="moNationalityText" TabIndex="2" runat="server" SkinID="MediumTextBox" CssClass="FLATTEXTBOX_TAB"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moPlaceOfBirthLabel" runat="server">PLACE_OF_BIRTH</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlPlaceOfBirth" runat="server" TabIndex="3" SkinID="MediumDropDown">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="moPlaceOfBirthText" TabIndex="4" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr runat="server" id="moAMLRegulations4">
                                    <td align="right"></td>
                                    <td align="left"></td>
                                    <td align="right">
                                        <asp:Label ID="mocityOfBirthLabel" runat="server">CITY_OF_BIRTH</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moCityOfBirthText" TabIndex="4" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr runat="server" id="moAMLRegulations2">
                                    <td align="right">
                                        <asp:Label ID="moGenderLabel" runat="server">GENDER</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlGender" runat="server" TabIndex="5" SkinID="ExtraSmallDropDown">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="moGenderText" TabIndex="6" runat="server" SkinID="exSmallTextBox"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moCUIT_CUILLabel" runat="server">CUIT_CUIL</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moCUIT_CUILText" TabIndex="7" runat="server" SkinID="exSmallTextBox"
                                            MaxLength="11"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr runat="server" id="moAMLRegulations3">
                                    <td align="right">
                                        <asp:Label ID="moPerson_typeLabel" runat="server">PERSON_TYPE</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="ddlPersonType" runat="server" TabIndex="50" SkinID="MediumDropDown">
                                        </asp:DropDownList>
                                        <asp:TextBox ID="moPersonTypeText" TabIndex="60" runat="server" SkinID="exSmallTextBox"></asp:TextBox>
                                    </td>
                                    <td align="right"></td>
                                    <td align="left"></td>
                                </tr>
                                <tr style="background-color: #f2f2f2">
                                    <td>
                                        <asp:Button ID="btnCustProfileHistory_Write" runat="server" Text="Customer_Profile_History" class="primaryBtn" SkinID="PrimaryleftButton"></asp:Button>
                                    </td>
                                    <td>
                                        <asp:Button ID="btnOutboundCommHistory" runat="server" Text="Outbound_Comm_History" class="primaryBtn" SkinID="PrimaryleftButton"></asp:Button>
                                    </td>
                                    <td colspan="2">
                                        <asp:Button ID="btnUndoCertDetail_Write" runat="server" Text="Undo" SkinID="AlternateRightButton"></asp:Button>
                                        <asp:Button ID="btnSaveCertDetail_WRITE" TabIndex="11" runat="server" SkinID="PrimaryRightButton"
                                            Text="Save"></asp:Button>
                                        <asp:Button ID="btnEditCertDetail_WRITE" TabIndex="13" runat="server" Text="Edit"
                                            SkinID="PrimaryRightButton"></asp:Button>
                                    </td>
                                </tr>

                                <tr id="AdditionalCustomer" runat="server">
                                    <td align="left" colspan="4">
                                        <h2 class="dataGridHeader">
                                            <a id="OthCustExpander" href="#">
                                                <img src="../App_Themes/Default/Images/sort_indicator_des.png" /></a>
                                            <asp:Label ID="OtherCustomer" runat="server">OTHER_CUSTOMER</asp:Label>
                                            <asp:Label ID="CustomerCount" runat="server"></asp:Label>
                                        </h2>
                                    </td>
                                </tr>

                                <tr id="OtherCustomerInfo">
                                    <td colspan="4" style="border-bottom: none">

                                        <asp:GridView ID="CertOtherCustomers" runat="server" AutoGenerateColumns="false"
                                            DataKeyNames="customer_id" SkinID="DetailPageGridView" AllowPaging="false" AllowSorting="false" Width="100%">
                                            <Columns>
                                                <asp:TemplateField>
                                                    <ItemTemplate>
                                                        <span style="cursor: pointer" id='<%# "Expand" & Container.DataItemIndex %>' onclick="ShowHideCustomerDetails(<%# Container.DataItemIndex %>, '<%# New Guid(CType(Eval("customer_id"), Byte())).ToString() %>', '<%# Eval("cust_info_exclude").ToString() %>', '<%# Eval("cust_salutation_exclude").ToString() %>', '<%# Eval("lang_id").ToString()%>', '<%# Eval("identification_number_type").ToString() %>');">+</span>
                                                    </ItemTemplate>
                                                </asp:TemplateField>

                                                <asp:BoundField DataField="customer_name" HeaderText="CUSTOMER_NAME" />
                                                <asp:BoundField DataField="identification_number" HeaderText="TAX_ID" />
                                                <asp:BoundField DataField="work_phone" HeaderText="WORK_PHONE" />
                                                <asp:BoundField DataField="home_phone" HeaderText="HOME_PHONE" />
                                                <asp:BoundField DataField="cust_info_exclude" Visible="false" />
                                                <asp:BoundField DataField="cust_salutation_exclude" Visible="false" />
                                                <asp:BoundField DataField="lang_id" Visible="false" />
                                                <asp:BoundField DataField="identification_number_type" Visible="false" />
                                                <asp:TemplateField ItemStyle-BorderStyle="None">
                                                    <ItemTemplate>
                                                        <tr id='<%# "Child" & Container.DataItemIndex %>'>
                                                            <td colspan="5" id='<%# "Cell" & Container.DataItemIndex %>' style="border-bottom: none"></td>
                                                        </tr>
                                                    </ItemTemplate>
                                                </asp:TemplateField>
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>



                            </tbody>
                        </table>
                    </div>
                </div>
                <div id="tabsGeneralInfo">
                    <div class="Page" runat="server" id="moGeneral_InformationTabPanel_WRITE" style="display: block; height: 300px; overflow: auto">
                        <table width="100%" class="formGrid" border="0">
                            <tbody>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moProductSalesDateLabel" runat="server">PRODUCT_SALES_DATE</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moProductSalesDateText" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                        <asp:ImageButton ID="BtnProductSalesDate" runat="server" Style="vertical-align: bottom"
                                            ImageUrl="~/App_Themes/Default/Images/calendar.png" />
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moRetailerLabel" runat="server">RETAILER</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moRetailerText" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moWarrantySoldLabel" runat="server">WARRANTY_SALES_DATE</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moWarrantySoldText" runat="server" SkinID="SmallTextBox" ReadOnly="True"></asp:TextBox>
                                        <asp:ImageButton ID="BtnWarrantySoldDate" runat="server" Style="vertical-align: bottom"
                                            ImageUrl="~/App_Themes/Default/Images/calendar.png" />
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moInvoiceNumberLabel" runat="server">INVOICE_NUMBER</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moInvoiceNumberText" runat="server" SkinID="SmallTextBox" MaxLength="30"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moActivationDateLabel" runat="server">ACTIVATION_DATE</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moActivationDateText" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moSalesPriceLabel" runat="server">SALES_PRICE</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moSalesPriceText" runat="server" SkinID="SmallTextBox" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moOriginalRetailPriceLabel" runat="server">ORIGINAL_RETAIL_PRICE</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moOriginalRetailPriceText" ReadOnly="true" SkinID="SmallTextBox"
                                            runat="server"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moCurrencyPurchaseLabel" runat="server">CURRENCY_OF_PURCHASE</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moCurrencyPurchaseText" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moDateAddedLabel" runat="server">DATE_ADDED</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moDateAddedText" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moCountryOfPurchaseLabel" runat="server">COUNTRY_OF_PURCHASE</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moCountryOfPurchaseTextBox" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblSource" runat="server">SOURCE</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtSource" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="Label14" runat="server">DEALER_UPDATE_REASON:</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtDealerUpdateReason" ReadOnly="true" runat="server" SkinID="SmallTextBox" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moLastMaintainedLabel" runat="server">DATE_LAST_MAINTAINED</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moLastMaintainedTextbox" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moPaymentByLabel" runat="server">PAYMENT_DESCRIPTION</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moPaymentByText" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" />
                                    <td runat="server" id="tdVehicleLicenseTag" align="right">
                                        <asp:Label ID="moVehicleLicenseTagLabel" runat="server">VEHICLE_LICENSE_TAG</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moVehicleLicenseTagText" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moRegionLabel" runat="server">REGION</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moRegionText" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moServiceLineNumberLabel" runat="server">SERVICE_LINE_NUMBER</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moServiceLineNumberText" ReadOnly="true" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moProductCodeLabel" runat="server">PRODUCT_CODE</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moProductCodeText" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moAccountNumberLabel" runat="server">ACCOUNT_NUMBER</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moAccountNumberText" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moDescriptionLabel" runat="server">DESCRIPTION</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moDescriptionText" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moCampaignNumberLabel" runat="server">CAMPAIGN_NUMBER</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moCampaignNumberText" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moTypeOfEquipmentLabel" runat="server">TYPE_OF_EQUIPMENT</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moTypeOfEquipmentText" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moAccount_TypeLabel" runat="server">ACCOUNT_TYPE</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="cboAccountType" runat="server" SkinID="SmallDropDown">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moDealerProductCodeLabel" runat="server">DEALER_PRODUCT_CODE</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moDealerProductCodeText" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moOldNumberLabel" runat="server">OLD_NUMBER</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moOldNumberText" runat="server" SkinID="SmallTextBox" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moDealerItemLabel" runat="server">DEALER_ITEM</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moDealerItemText" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="LabelRatingPlan" runat="server">RATING_PLAN</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextboxRatingPlan" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moDealerBranchCodeLabel" runat="server">DEALER_BRANCH_CODE</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moDealerBranchCodeText" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moVATNumLabel" runat="server">VAT_NUM</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moVATNumText" runat="server" SkinID="SmallTextBox" MaxLength="50"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td id="Td1" runat="server" align="right">
                                        <asp:Label ID="moSalesRepNumberLabel" runat="server">SALES_REP_NUMBER</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moSalesRepNumberText" MaxLength="30" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td runat="server" id="tdBillingDate" align="right">
                                        <asp:Label ID="moBillingDateLabel" runat="server">DATE_PAID_FOR</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moBillingDateText" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moPostPaidLabel" runat="server">POSTPRE_PAID</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moPostPaidText" runat="server" SkinID="SmallTextBox" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td runat="server" id="tdDatePaid" align="right">
                                        <asp:Label ID="moDatePaidLabel" runat="server">DATE_PAID</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moDatePaidText" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moCapSeriesLabel" runat="server">CAPITALIZATION_SERIES</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moCapSeriesText" runat="server" SkinID="SmallTextBox" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moCapNumberLabel" runat="server">CAPITALIZATION_NUMBER</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moCapNumberText" runat="server" SkinID="SmallTextBox" ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moSubStatusChangeDateLabel" runat="server">SUBSCRIBER_STATUS_CHANGE_DATE</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moSubStatusChangeDateText" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moLinesOnAccountLabel" runat="server">LINES_ON_ACCOUNT</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moLinesOnAccountText" runat="server" SkinID="SmallTextBox" ReadOnly="True"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moReinsuranceStatusLabel" runat="server">REINSURANCE_STATUS</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moReinsuranceStatusText" runat="server" SkinID="SmallTextBox" ReadOnly="True"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moReinsRejectReasonLabel" runat="server">REINS_REJECT_REASON</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moReinsRejectReasonText" runat="server" SkinID="SmallTextBox" ReadOnly="True" Visible="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moDeviceOrderNumberLabel" runat="server">DEVICE_ORDER_NUMBER</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moDeviceOrderNumberText" runat="server" SkinID="SmallTextBox" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moInsuranceOrderNumberLabel" runat="server">INSURANCE_ORDER_NUMBER</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moInsuranceOrderNumberText" runat="server" SkinID="SmallTextBox" ReadOnly="true"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moUpgradeTypeLabel" runat="server">UPGRADE_TYPE</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moUpgradeTypeText" runat="server" SkinID="SmallTextBox" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moFulfillmentConsentAction" runat="server">FULFILLMENT_CONSENT_ACTION</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="moFulfillmentConsentActionDrop" runat="server" ReadOnly="true" SkinID="mediumdropdown" AutoPostBack="False">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moPlanTypeLabel" runat="server">PLAN_TYPE</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moPlanTypeText" runat="server" SkinID="SmallTextBox" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td align="right"></td>
                                    <td align="left"></td>
                                </tr>

                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moServiceIdLabel" runat="server">SERVICE_ID</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moServiceIdText" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moServiceStartDateLabel" runat="server">SERVICE_START_DATE</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moServiceStartDateText" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                        <asp:ImageButton ID="BtnServiceStartDate" runat="server" Style="vertical-align: bottom"
                                            ImageUrl="~/App_Themes/Default/Images/calendar.png" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moClaimWaitingPeriodEndDateLabel" runat="server">CLAIM_WAITING_PERIOD_END_DATE</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moClaimWaitingPeriodEndDateText" runat="server" ReadOnly="true" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </tr>

                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moCertificateSignedLabel" runat="server" Visible="false">CERTIFICATE_SIGNED</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="moCertificateSigneddrop" runat="server" SkinID="mediumdropdown" AutoPostBack="False" Visible="false">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moCertificateVerificationDateLabel" runat="server" Visible="false">CERTIFICATE_VERIFICATION_DATE</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moCertificateVerificationDateText" runat="server" SkinID="SmallTextBox" Visible="false"></asp:TextBox>
                                        <asp:ImageButton ID="BtnCertificateVerificationDate" runat="server" Style="vertical-align: bottom"
                                            ImageUrl="~/App_Themes/Default/Images/calendar.png" Visible="false" />
                                    </td>

                                </tr>
                                <tr>

                                    <td align="right">
                                        <asp:Label ID="moSEPAMandateSignedLabel" runat="server" Visible="false">SEPA_MANDATE_SIGNED</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="moSEPAMandateSignedDrop" runat="server" SkinID="mediumdropdown" AutoPostBack="False" Visible="false">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moSEPAMandateDateLabel" runat="server" Visible="false">SEPA_MANDATE_DATE</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moSEPAMandateDateText" runat="server" SkinID="SmallTextBox" Visible="false"></asp:TextBox>
                                        <asp:ImageButton ID="BtnSEPAMandateDate" runat="server" Style="vertical-align: bottom"
                                            ImageUrl="~/App_Themes/Default/Images/calendar.png" Visible="false" />
                                    </td>
                                </tr>
                                <tr>

                                    <td align="right">
                                        <asp:Label ID="moCheckSignedLabel" runat="server" Visible="false">CHECK_SIGNED</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="moCheckSignedDrop" runat="server" SkinID="mediumdropdown" AutoPostBack="False" Visible="false">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moCheckVerificationDateLabel" runat="server" Visible="false">CHECK_VERIFICATION_DATE</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moCheckVerificationDateText" runat="server" SkinID="SmallTextBox" Visible="false"></asp:TextBox>
                                        <asp:ImageButton ID="BtnCheckVerificationDate" runat="server" Style="vertical-align: bottom"
                                            ImageUrl="~/App_Themes/Default/Images/calendar.png" Visible="false" />
                                    </td>
                                </tr>
                                <tr>

                                    <td align="right">
                                        <asp:Label ID="moContractCheckCompleteLabel" runat="server" Visible="false">CONTRACT_CHECK_COMPLETE</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="moContractCheckCompleteDrop" runat="server" SkinID="mediumdropdown" AutoPostBack="False" Visible="false">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moContractCheckCompleteDateLabel" runat="server" Visible="false">CONTRACT_CHECK_COMPLETE_DATE</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moContractCheckCompleteDateText" runat="server" SkinID="SmallTextBox" Visible="false"></asp:TextBox>
                                        <asp:ImageButton ID="BtnContractCheckCompleteDate" runat="server" Style="vertical-align: bottom"
                                            ImageUrl="~/App_Themes/Default/Images/calendar.png" Visible="false" />
                                    </td>
                                </tr>
                               <%-- <tr id="trmfgdate" runat="server">

                                    <td align="right">
                                        <asp:Label ID="lblMfgBeginDate" runat="server" >MANUFACTURING_BEGIN_DATE</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtMfgBeginDate" runat="server" SkinID="SmallTextBox" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblMfgEndDate" runat="server" >MANUFACTURING_END_DATE</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtMfgEndDate" runat="server" SkinID="SmallTextBox" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr id="trmfgkm" runat="server">

                                    <td align="right">
                                        <asp:Label ID="lblMfgBeginKm" runat="server" >MANUFACTURING_BEGIN_KM</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtMfgBeginKm" runat="server" SkinID="SmallTextBox" Enabled="false"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblMfgEndKm" runat="server" >MANUFACTURING_END_KM</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtMfgEndKm" runat="server" SkinID="SmallTextBox" Enabled="false"></asp:TextBox>
                                    </td>
                                </tr>--%>
                                <tr id="trCertExtn" runat="server">
                                    <td colspan="4">
                                        <div class="Page" runat="server" id="Div2" style="display: block; height: 300px; overflow: auto">
                                            <table width="100%" class="dataGrid">
                                                <tr id="tr3" runat="server">
                                                    <td class="bor" align="left">
                                                        <asp:Label ID="Label3" runat="server">Page_Size</asp:Label><asp:Label
                                                            ID="Label4" runat="server">:</asp:Label>
                                                        &nbsp;
                                                        <asp:DropDownList ID="moCertExtn_cboPageSize" runat="server" Width="50px"
                                                            AutoPostBack="true" SkinID="SmallDropDown">
                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                            <asp:ListItem Value="15">15</asp:ListItem>
                                                            <asp:ListItem Value="20">20</asp:ListItem>
                                                            <asp:ListItem Value="25">25</asp:ListItem>
                                                            <asp:ListItem Value="30">30</asp:ListItem>
                                                            <asp:ListItem Value="35">35</asp:ListItem>
                                                            <asp:ListItem Value="40">40</asp:ListItem>
                                                            <asp:ListItem Value="45">45</asp:ListItem>
                                                            <asp:ListItem Value="50">50</asp:ListItem>
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td class="bor" align="right">
                                                        <asp:Label ID="Label5" runat="server"></asp:Label>
                                                    </td>
                                                </tr>
                                            </table>
                                            <div>
                                                <asp:GridView ID="CertExtnGrid" runat="server" Width="50%" AutoGenerateColumns="False"
                                                    AllowPaging="True" SkinID="DetailPageGridView" AllowSorting="False">
                                                    <SelectedRowStyle Wrap="True" />
                                                    <EditRowStyle Wrap="True" />
                                                    <AlternatingRowStyle Wrap="True" />
                                                    <HeaderStyle Wrap="false" />
                                                    <RowStyle Wrap="True" />
                                                    <Columns>
                                                        <asp:BoundField DataField="FIELD_NAME"
                                                            HeaderText="Consent Type" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                                        <asp:BoundField DataField="FIELD_VALUE"
                                                            HeaderText="Consent Value" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                                    </Columns>
                                                    <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                                                    <PagerStyle />
                                                </asp:GridView>
                                            </div>
                                        </div>
                                    </td>
                                </tr>
                                <tr style="background-color: #f2f2f2">
                                    <td colspan="4">
                                        <asp:Button ID="btnUndoCertInfo_Write" runat="server" Text="Undo" SkinID="AlternateRightButton"></asp:Button>
                                        <asp:Button ID="btnSaveCertInfo_WRITE" runat="server" Text="Save" SkinID="PrimaryRightButton"></asp:Button>
                                        <asp:Button ID="btnEditCertInfo_WRITE" runat="server" Text="Edit" SkinID="PrimaryRightButton"></asp:Button>
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div id="tabsItemInfo">
                    <div class="Page" runat="server" id="moItemsTabPanel_WRITE" style="display: block; height: 300px; overflow: auto">
                        <asp:GridView ID="ItemsGrid" runat="server" Width="100%" OnRowCreated="ItemsGrid_ItemCreated"
                            OnRowCommand="ItemsGrid_ItemCommand" AllowPaging="false" AllowSorting="false"
                            SkinID="DetailPageGridView">
                            <Columns>
                                <asp:TemplateField SortExpression="item_number" HeaderText="item_number" />
                                <asp:TemplateField SortExpression="risk_type" HeaderText="Risk_Type">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEditItem" runat="server" CommandName="SelectAction" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="item_desc" HeaderText="Item_Desc" />
                                <asp:TemplateField SortExpression="make" HeaderText="Make" />
                                <asp:TemplateField SortExpression="model" HeaderText="Model" />
                                <asp:TemplateField SortExpression="expiration_date" HeaderText="Expiration_Date" />
                                <asp:TemplateField SortExpression="benefit_status" HeaderText="Benefit_Status" />
                            </Columns>
                            <PagerSettings PageButtonCount="15" Mode="Numeric" />
                        </asp:GridView>
                        <br />
                        <asp:GridView ID="RegisteredItemsGrid" runat="server" Width="100%" OnRowCreated="RegisteredItemsGrid_ItemCreated"
                            OnRowCommand="RegisteredItemsGrid_ItemCommand" AllowPaging="false" AllowSorting="false"
                            SkinID="DetailPageGridView">
                            <Columns>
                                <asp:TemplateField SortExpression="registered_item_name" HeaderText="registered_item_name">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnRegEditItem" runat="server" CommandName="SelectAction" />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="device_type" HeaderText="device_Type" />
                                <asp:TemplateField SortExpression="item_desc" HeaderText="Item_Desc" />
                                <asp:TemplateField SortExpression="make" HeaderText="Make" />
                                <asp:TemplateField SortExpression="model" HeaderText="Model" />
                                <asp:TemplateField SortExpression="purchased_date" HeaderText="purchased_date" />
                                <asp:TemplateField SortExpression="purchase_price" HeaderText="purchase_price" />
                                <asp:TemplateField SortExpression="serial_number" HeaderText="serial_number" />
                                <asp:TemplateField SortExpression="registration_date" HeaderText="registration_date" />
                                <asp:TemplateField SortExpression="retail_price" HeaderText="retail_price" />
                                <asp:TemplateField SortExpression="expiration_date" HeaderText="expiration_date" />
                                <asp:TemplateField SortExpression="registered_item_status" HeaderText="registered_item_status" />
                            </Columns>
                            <PagerSettings PageButtonCount="15" Mode="Numeric" />
                        </asp:GridView>
                        <br />
                        <table>
                            <tbody>
                                <tr style="background-color: #f2f2f2">
                                    <td>
                                        <asp:Button ID="btnNewCertItem_WRITE" runat="server" SkinID="PrimaryRightButton"
                                            Text="New" />
                                        <asp:Button ID="btnNewCertRegItem_WRITE" runat="server" SkinID="PrimaryRightButton"
                                            Text="Register_New_Item" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div id="tabsPremiumInfo">
                    <div class="Page" runat="server" id="moPremiumInformationTabPanel_WRITE" style="display: block; height: 300px; overflow: auto">
                        <table width="100%" class="formGrid" border="0">
                            <tbody>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="Label2" runat="server">CURRENCY:</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="TextboxCURRENCY_OF_CERT" runat="server" SkinID="SmallTextBox" />
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblBillingDocType" runat="server">BILLING_DOCUMENT_TYPE:</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="txtBillingDocType" ReadOnly="true" runat="server" SkinID="SmallTextBox" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moBillingPlanLabel" runat="server">BILLING_PLAN:</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moBillingPlanText" ReadOnly="true" runat="server" SkinID="SmallTextBox" />
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moBillingCycleLabel" runat="server">BILLING_CYCLE:</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moBillingCycleText" ReadOnly="true" runat="server" SkinID="SmallTextBox" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moGrossAmtReceivedLabel" runat="server">GROSS_AMT_RECEIVED</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moGrossAmtReceivedText" runat="server" SkinID="SmallTextBox" />
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moMarketingExpenseLabel" runat="server">MARKETING_EXPENSE</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moMarketingExpenseText" runat="server" SkinID="SmallTextBox" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moPremiumWrittenLabel" runat="server">PREMIUM_WRITTEN</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moPremiumWrittenText" runat="server" SkinID="SmallTextBox" />
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moOtherLabel" runat="server">OTHER</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moOtherText" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moOriginalPremiumLabel" runat="server">ORIGINAL_PREMIUM</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moOriginalPremiumText" runat="server" SkinID="SmallTextBox" />
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moSalesTaxLabel" runat="server">SALES_TAX</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moSalesTaxText" runat="server" SkinID="SmallTextBox" />
                                        <asp:Label ID="lblTaxDetails" runat="server" ForeColor ="Blue"  Text="Tax_Details"> </asp:Label>
                                    </td>
                                    <ajaxToolkit:HoverMenuExtender  ID="HoverMenuExtender1" runat="server" TargetControlID="lblTaxDetails" 
                                         PopupControlID="PanTaxDetails" PopupPosition="right" PopDelay="25" HoverCssClass="popupBtnHover"
                                         DynamicControlID="PanTaxDetails"  DynamicServiceMethod="GetSalesTaxDetails">
                                     </ajaxToolkit:HoverMenuExtender>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moLossCostLabel" runat="server">LOSS_COST</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moLossCostText" runat="server" SkinID="SmallTextBox" />
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moMTDPaymentsLabel" runat="server">MTD_PAYMENTS</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moMTDPaymentsText" runat="server" SkinID="SmallTextBox" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moComissionsLabel" runat="server">COMMISSIONS</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moComissionsText" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moYTDPaymentsLabel" runat="server">YTD_PAYMENTS</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moYTDPaymentsText" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moAdminExpensesLabel" runat="server">ADMIN_EXPENSE</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moAdminExpensesText" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td colspan="2" />
                                </tr>
                                <tr id="moCustPaymentInfo1" runat="server">
                                    <td align="right">
                                        <asp:Label ID="moPaymentRcvdFromCustLabel" runat="server">PAYMENT_RCVD_FROM_CUST</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moPaymentRcvdFromCustText" runat="server" SkinID="SmallTextBox" />
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moMTDPaymentFromCustLabel" runat="server">MTD_PAYMENT_FROM_CUST</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moMTDPaymentFromCustText" runat="server" SkinID="SmallTextBox" />
                                    </td>
                                </tr>
                                <tr id="moCustPaymentInfo2" runat="server">
                                    <td colspan="2" />
                                    <td align="right">
                                        <asp:Label ID="moYTDPaymentFromCustLabel" runat="server">YTD_PAYMENT_FROM_CUST</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moYTDPaymentFromCustText" runat="server" SkinID="SmallTextBox" />
                                    </td>
                                </tr>
                                <tr id="moDirectDebitInformation1" runat="server">
                                    <td align="right">
                                        <asp:Label ID="moBilling_StatusLabel" runat="server">BILLING_STATUS</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="moBillingStatusId" runat="server" SkinID="SmallDropDown" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moInstallAmountLabel" runat="server">INSTALLMENT_AMOUNT</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moInstallAmountText" runat="server" SkinID="SmallTextBox" />
                                    </td>
                                </tr>
                                <tr id="moDirectDebitInformation2" runat="server">
                                    <td align="right">
                                        <asp:Label ID="moNumberOfInstallmentLabel" runat="server" Text="NUMBER_OF_INSTALLMENT"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moNumberOfInstallmentText" runat="server" SkinID="SmallTextBox" />
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moTotalAmountCollectedLabel" runat="server">TOTAL_AMOUNT_COLLECTED</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moTotalAmountCollectedText" runat="server" SkinID="SmallTextBox" />
                                    </td>
                                </tr>
                                <tr id="moDirectDebitInformation3" runat="server">
                                    <td align="right">
                                        <asp:Label ID="moNumberOfInstallmentCollectedLabel" runat="server" Text="NUMBER_OF_INSTALLMENT_COLLECTED"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moNumberOfInstallmentCollectedText" runat="server" SkinID="SmallTextBox" />
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moBalanceRemainingLabel" runat="server">BALANCE_REMAINING</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moBalanceRemainingText" runat="server" SkinID="SmallTextBox" />
                                    </td>
                                </tr>
                                <tr id="moDirectDebitInformation4" runat="server">
                                    <td align="right">
                                        <asp:Label ID="moNumberOfInstallmentRemainingLabel" runat="server" Text="NUMBER_OF_INSTALLMENT_REMAINING"></asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moNumberOfInstallmentRemainingLText" runat="server" SkinID="SmallTextBox" />
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moNextDueDateLabel" runat="server">NEXT_DUE_DATE</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moNextDueDateText" runat="server" SkinID="SmallTextBox" />
                                    </td>
                                </tr>
                                <tr id="moDirectDebitInformation4A" runat="server">
                                    <td colspan="2"></td>
                                    <td align="right">
                                        <asp:Label ID="moNextBillingDateLabel" runat="server">NEXT_BILLING_DATE</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moNextBillingDateText" runat="server" SkinID="SmallTextBox" />
                                    </td>
                                </tr>
                                <tr id="moDirectDebitInformation5" runat="server">
                                    <td align="right">
                                        <asp:Label ID="moPaymentTypeIdLabel" runat="server">Payment_Type</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="moPaymentTypeId" runat="server" SkinID="SmallDropDown" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moSendLetterIdLabel" runat="server">SEND_LETTER</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:CheckBox ID="CheckBoxSendLetter" runat="server"></asp:CheckBox>
                                    </td>
                                </tr>
                                <tr id="moDirectDebitInformation5A" runat="server">
                                    <td align="right">
                                        <asp:Label ID="moPaymentInstrumentLabel" runat="server">PAYMENT_INSTRUMENT:</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="moPaymentInstrument" runat="server" SkinID="SmallDropDown"
                                            Enabled="false">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moDateLetterSentLabel" runat="server">DATE_LETTER_SENT</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moDateLetterSentText" runat="server" SkinID="SmallTextBox" />
                                    </td>
                                </tr>
                                <tr id="moDirectDebitInformation6" runat="server">
                                    <td align="right">
                                        <asp:Label ID="moBankAccountNumberLabel" runat="server">BANK_ACCOUNT_NUMBER</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moBankAccountNumberText" runat="server" SkinID="SmallTextBox" />
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moBankRoutingNumberLabel" runat="server">BANK_ROUTING_NUMBER</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moBankRoutingNumberText" runat="server" SkinID="SmallTextBox" />
                                    </td>
                                </tr>
                                <tr id="moDirectDebitInformation7" runat="server">
                                    <td align="right">
                                        <asp:Label ID="moBankAccountOwnerLabel" runat="server">BANK_ACCOUNT_OWNER</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moBankAccountOwnerText" runat="server" SkinID="SmallTextBox" />
                                    </td>
                                    <td colspan="2" />
                                </tr>
                                <tr id="moCreditCardInformation1" runat="server">
                                    <td align="right">
                                        <asp:Label ID="moCreditCardTypeIDLabel" runat="server">CREDIT_CARD_TYPE</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="moCreditCardTypeIDDropDown" runat="server" SkinID="SmallDropDown"
                                            AutoPostBack="false">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moCreditCardNumberLabel" runat="server">CREDIT_CARD_NUMBER</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moCreditCardNumberText" runat="server" SkinID="SmallTextBox" />
                                    </td>
                                </tr>
                                <tr id="moCreditCardInformation2" runat="server">
                                    <td align="right">
                                        <asp:Label ID="moNameOnCreditCardLabel" runat="server">NAME_ON_CREDIT_CARD</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moNameOnCreditCardText" runat="server" SkinID="SmallTextBox" />
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moExpirationDateLabel" runat="server">Expiration_Date</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moExpirationDateText" runat="server" SkinID="SmallTextBox" />
                                    </td>
                                </tr>
                                <tr id="moCommEntityInformationLine" runat="server">
                                    <td colspan="4">
                                        <hr style="width: 100%; height: 1px" size="1" />
                                    </td>
                                </tr>
                                <tr id="moCommEntityInformation" runat="server">
                                    <td colspan="4">
                                        <asp:GridView ID="moCommEntityGrid" runat="server" Width="70%" AutoGenerateColumns="False"
                                            CellPadding="1" AllowPaging="false" AllowSorting="false" CssClass="DATAGRID"
                                            Visible="true" HorizontalAlign="Center">
                                            <SelectedRowStyle Wrap="False" CssClass="SELECTED" />
                                            <EditRowStyle Wrap="False" CssClass="EDITROW" />
                                            <AlternatingRowStyle Wrap="False" CssClass="ALTROW" />
                                            <RowStyle Wrap="False" CssClass="ROW" />
                                            <HeaderStyle CssClass="HEADER" />
                                            <Columns>
                                                <asp:BoundField HeaderText="Commission Entity" DataField="payee_type_or_entity_name"
                                                    HeaderStyle-CssClass="CenteredTD" ItemStyle-Width="1%" />
                                                <asp:BoundField HeaderText="Total Commission Paid" DataField="comm_paid_as_of_date"
                                                    HeaderStyle-CssClass="CenteredTD" ItemStyle-Width="1%" />
                                            </Columns>
                                        </asp:GridView>
                                    </td>
                                </tr>
                                <tr style="background-color: #f2f2f2">
                                    <td colspan="4">
                                        <asp:Button ID="btnBankInfo" runat="server" Text="BANK_INFO" SkinID="PrimaryRightButton" />
                                        <asp:Button ID="btnDebitHistory" runat="server" Text="BILLING_HISTORY" SkinID="PrimaryRightButton" />
                                        <asp:Button ID="btnPaymentHistory" runat="server" Text="PAYMENT_HISTORY" SkinID="PrimaryRightButton" />
                                        <asp:Button ID="btnBillPayHist" runat="server" Text="BILLING_COLLECTION_HISTORY" SkinID="PrimaryRightButton" />
                                        <asp:Button ID="btnDebitEdit_WRITE" runat="server" Text="Edit" SkinID="PrimaryRightButton" />
                                        <asp:Button ID="btnUndoDebit_WRITE" runat="server" Text="Undo" SkinID="AlternateRightButton" />
                                        <asp:Button ID="btnDebitSave_WRITE" runat="server" Text="Save" SkinID="PrimaryRightButton" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div id="tabsCancelRequest">
                    <div class="Page" runat="server" id="moCancelRequestInfoTabPanel_WRITE" style="display: block; height: 300px; overflow: auto">
                        <table width="100%" class="formGrid" border="0">
                            <tbody>
                                <tr valign="top">
                                    <td align="right">
                                        <asp:Label ID="moCancelRequestReasonLabel" runat="server">CANCELLATION_REASON</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="moCancelRequestReasonDrop" runat="server" SkinID="SmallDropDown" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>

                                <tr valign="top">
                                    <td align="right">
                                        <asp:Label ID="moProofOfDocumentationLabel" Visible="false" runat="server">PROOF_OF_DOCUMENTATION</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="moProofOfDocumentationDrop" Visible="false" Width="200px" runat="server" SkinID="SmallDropDown"
                                            AutoPostBack="false">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moCancelRequestDateLabel" runat="server">CANCELLATION_REQUEST_DATE</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moCancelRequestDateTextBox" AutoPostBack="true" runat="server" SkinID="SmallTextBox" />
                                        <asp:ImageButton ID="moCancelRequestDateImagebutton" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png" ImageAlign="AbsMiddle" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moCancelDateLabel" runat="server">CANCELLATION_DATE</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moCancelDateTextBox" runat="server" SkinID="SmallTextBox" />
                                        <asp:ImageButton ID="moCancelDateImageButton" runat="server" ImageAlign="AbsMiddle" ImageUrl="~/App_Themes/Default/Images/calendar.png" />
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td align="right">
                                        <asp:Label ID="moUseExistingBankDetailsLabel" Visible="true" runat="server">USE_EXISTING_BANK_DETAILS</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="moUseExistingBankDetailsDrop" Visible="true" Width="200px" runat="server" AutoPostBack="True" SkinID="SmallDropDown">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moCRIBANNumberLabel" runat="server" Visible="false">IBAN_NUMBER</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moCRIBANNumberText" runat="server" Width="200px" Visible="false" SkinID="MediumTextBox" />
                                    </td>
                                </tr>
                                <tr valign="top">
                                    <td align="right">
                                        <asp:Label ID="moCanReqJustificationLabel" runat="server">JUSTIFICATION</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="moCancelRequestJustificationDrop" runat="server" AutoPostBack="False" SkinID="mediumdropdown">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moCallerNameLabel" runat="server">NAME_OF_CALLER</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moCallerNameTextBox" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moCancelRequestStatusLabel" runat="server">STATUS</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moCancelRequestStatusText" runat="server" SkinID="SmallTextBox" ReadOnly="True" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moCommentsLabel" runat="server">COMMENTS</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moCommentsTextbox" runat="server" Rows="8" SkinID="LargeTextBox" TextMode="MultiLine" Visible="true" />
                                    </td>
                                </tr>
                                <tr style="background-color: #f2f2f2">
                                    <td colspan="2">
                                        <asp:Button ID="btnCancelRequestEdit_WRITE" runat="server" SkinID="PrimaryRightButton" Text="Edit" />
                                        <asp:Button ID="btnCancelRequestUndo_WRITE" runat="server" SkinID="AlternateRightButton" Text="Undo" />
                                        <asp:Button ID="btnCancelRequestSave_WRITE" runat="server" SkinID="PrimaryRightButton" Text="Save" />
                                        <asp:Button ID="btnCreateNewRequest_WRITE" runat="server" SkinID="PrimaryRightButton" Visible="false" Text="CREATE_NEW_REQUEST" />
                                    </td>
                                </tr>

                            </tbody>
                        </table>
                    </div>
                </div>
                <div id="tabsCancelInfo">
                    <div id="moCancellationInfoTabPanel_WRITE" runat="server" class="Page" style="display: block; height: 300px; overflow: auto">
                        <table width="100%" class="formGrid" border="0">
                            <tbody>
                                <tr>
                                    <td colspan="2" align="left" valign="bottom" class="GroupHeader">
                                        <asp:Label ID="moCanGralInfoLabel" runat="server">GENERAL INFORMATION</asp:Label>
                                    </td>
                                    <td colspan="2" align="left" valign="bottom" class="GroupHeader">
                                        <asp:Label ID="moCanFinImpactLabel" runat="server">FINANCIAL IMPACT</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moCancellationReasonLabel" runat="server">CANCELLATION_REASON</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moCancellationReasonTextbox" runat="server" SkinID="MediumTextBox"
                                            CssClass="FLATTEXTBOX"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moCanGrossAmtReceivedLabel" runat="server">GROSS_AMT_RECEIVED</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moCanGrossAmtReceivedTextbox" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moCancellationDateLabel" runat="server">CANCELLATION_DATE</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moCancellationDateTextbox" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moCanOriginalPremiumLabel" runat="server">ORIGINAL_PREMIUM</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moCanOriginalPremiumTextbox" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moSourceLabel" runat="server">SOURCE</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moSourceTextbox" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moCanPremiumWrittenLabel" runat="server">PREMIUM_WRITTEN</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moCanPremiumWrittenTextbox" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moProcessedDateLabel" runat="server">PROCESSED</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moProcessedDateTextbox" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moCanLossCostLabel" runat="server">LOSS_COST</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moCanLossCostTextbox" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moOriginalStateProvinceLabel" runat="server">ORIGINAL_STATE_PROVINCE</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moOriginalStateProvinceTextbox" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moCanComissionsLabel" runat="server">COMMISSIONS</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moCanComissionsTextbox" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2" align="left" valign="bottom" class="GroupHeader">
                                        <asp:Label ID="moRefundComputationLabel" runat="server">REFUND COMPUTATION</asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moCanAdminExpensesLabel" runat="server">ADMIN_EXPENSE</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moCanAdminExpensesTextbox" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moRefundMethodMeaningLabel" runat="server">REFUND_METHOD_MEANING</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moRefundMethodMeaningTextbox" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moCanMarketingExpensesLabel" runat="server">MARKETING_EXPENSE</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moCanMarketingExpensesTextbox" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moPolicyCostLabel" runat="server">POLICY_COST</asp:Label>:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="moPolicyCostTextbox" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moCanOtherLabel" runat="server">OTHER</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moCanOtherTextbox" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moComputedRefundLabel" runat="server">COMPUTED_REFUND</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moComputedRefundTextbox" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moCanSalesTaxLabel" runat="server">SALES_TAX</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moCanSalesTaxTextbox" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moCostRetainedLabel" runat="server">COST_RETAINED</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moCostRetainedTextbox" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td colspan="2" align="left" valign="bottom" class="GroupHeader">
                                        <asp:Label ID="Label1" runat="server">PAYMENT INFORMATION</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moRefundAmtLabel" runat="server">REFUND_AMT</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moRefundAmtTextbox" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moAcctStatusLabel" runat="server">ACCT_STATUS</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moAcctStatusTextbox" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moIssuedToLabel" runat="server">ISSUED TO</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moIssuedToTextbox" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moAcctStatusDateLabel" runat="server">ACCT_STATUS_DATE</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moAcctStatusDateTextbox" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moPaymentMethodLabel" runat="server">PAYMENT_METHOD</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moPaymentMethodTextbox" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moAcctTrackNumLabel" runat="server">ACCT_TRACK_NUMBER</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moAcctTrackNumTextbox" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moRefundRejectStatusLabel" runat="server">REFUND_STATUS</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moRefundRejectStatusText" ReadOnly="true" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td colspan="2"></td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moRefundRejectCodeLabel" runat="server">REFUND_REJECT_CODE</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moRefundRejectCodeText" ReadOnly="true" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                    <td colspan="2"></td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moRfIBANNumberLabel" runat="server">IBAN_NUMBER</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moRfIBANNumberText" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td colspan="2"></td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moRfAccountNumberLabel" runat="server">Account_NUMBER</asp:Label>:
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moRfAccountNumberText" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td colspan="2"></td>
                                </tr>
                                <tr runat="server" id="trBankInfo">
                                    <td colspan="2" align="left" valign="bottom" class="GroupHeader">
                                        <asp:Label ID="moBankInfoLabel" runat="server">BANK_INFO</asp:Label>
                                    </td>
                                    <td colspan="2"></td>
                                </tr>
                            </tbody>
                        </table>
                        <div id="div1" runat="server">
                            <table width="100%" class="formGrid" border="0">
                                <tbody>
                                    <Elita:UserControlBankInfo ID="moCancBankInfoController" runat="server"></Elita:UserControlBankInfo>
                                </tbody>
                            </table>
                            <table width="100%" class="formGrid" border="0">
                                <tbody>
                                    <Elita:UserControlPaymentOrderInfo ID="moCancPaymentOrderInfoCtrl" runat="server"></Elita:UserControlPaymentOrderInfo>
                                </tbody>
                            </table>
                        </div>
                        <div class="btnRevCancZone">
                            <div class="">
                                <table width="100%" class="formGrid" border="0">
                                    <tbody>
                                        <tr>
                                            <td>
                                                <asp:Button ID="ReverseCancellationButton_WRITE" runat="server" Text="REINSTATE_CERTIFICATE"
                                                    class="primaryBtn" SkinID="PrimaryleftButton" />
                                                <asp:Button ID="UpdateBankInfoButton_WRITE" runat="server" Text="UPDATE_BANK_INFO"
                                                    class="primaryBtn" SkinID="PrimaryleftButton" />
                                            </td>
                                        </tr>
                                    </tbody>
                                </table>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="tabsComments">
                    <div class="Page" runat="server" id="moCommentsInformationTabPanel_WRITE" style="display: block; height: 300px; overflow: auto">
                        <asp:DataGrid ID="CommentsGrid" runat="server" Width="100%" OnItemCommand="CommentsGrid_ItemCommand"
                            OnItemCreated="CommentsGrid_ItemCreated" AllowSorting="True" AllowPaging="True"
                            AutoGenerateColumns="False" SkinID="DetailPageDataGrid">
                            <Columns>
                                <asp:TemplateColumn HeaderText="Time_Stamp">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEditItem" runat="server" CommandName="SelectAction" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn HeaderText="Name_of_Caller" />
                                <asp:BoundColumn HeaderText="User_Name" />
                                <asp:BoundColumn HeaderText="Comments" />
                            </Columns>
                            <PagerStyle Position="TopAndBottom" PageButtonCount="15" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                        <table width="100%" class="formGrid" border="0">
                            <tbody>
                                <tr style="background-color: #f2f2f2">
                                    <td>
                                        <asp:Button ID="btnAddComment_WRITE" SkinID="PrimaryRightButton" runat="server" Text="Add" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div id="tabsEndorsement">
                    <div class="Page" runat="server" id="moEndorsementsTabPanel_WRITE" style="display: block; height: 300px; overflow: auto">
                        <asp:DataGrid ID="EndorsementsGrid" runat="server" Width="100%" OnItemCommand="EndorsementsGrid_ItemCommand"
                            OnItemCreated="EndorsementsGrid_ItemCreated" AllowSorting="False" AllowPaging="True"
                            AutoGenerateColumns="False" SkinID="DetailPageDataGrid">
                            <Columns>
                                <asp:TemplateColumn HeaderText="Endorsement_Number">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEditItem" runat="server" CommandName="SelectAction" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn HeaderText="Created_by" />
                                <asp:BoundColumn HeaderText="Created_Date" />
                                <asp:BoundColumn HeaderText="Type" />
                                <asp:BoundColumn HeaderText="Endorsement_Reason" />
                                <asp:BoundColumn HeaderText="Effective_Date" />
                                <asp:BoundColumn HeaderText="Expiration_Date" />
                            </Columns>
                            <PagerStyle Position="TopAndBottom" PageButtonCount="15" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                        <table width="100%" class="formGrid" border="0">
                            <tbody>
                                <tr style="background-color: #f2f2f2">
                                    <td>
                                        <asp:Button ID="btnAddEndorsement_WRITE" runat="server" Text="Add" SkinID="PrimaryRightButton" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div id="tabsTaxId">
                    <div class="Page" runat="server" id="moTaxIdTabPanel_WRITE" style="display: block; height: 300px; overflow: auto">
                        <table width="100%" class="formGrid" border="0">
                            <tbody>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moDocumentTypeLabel" runat="server">DOCUMENT_TYPE</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moDocumentTypeText" runat="server" SkinID="SmallTextBox" />
                                        <asp:DropDownList ID="cboDocumentTypeId" runat="server" SkinID="SmallDropDown" />
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moNewTaxIdLabel" runat="server">DOCUMENT_NUMBER</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moNewTaxIdText" runat="server" SkinID="MediumTextBox" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moIDTypeLabel" runat="server">ID_TYPE</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moIDTypeText" runat="server" SkinID="MediumTextBox" />
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moRGNumberLabel" runat="server">RG_NUMBER</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moRGNumberText" runat="server" SkinID="SmallTextBox" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moDocumentAgencyLabel" runat="server">DOCUMENT_AGENCY</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moDocumentAgencyText" runat="server" SkinID="MediumTextBox" />
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moDocumentIssueDateLabel" runat="server" Width="100%">DOCUMENT_ISSUE_DATE</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moDocumentIssueDateText" runat="server" SkinID="SmallTextBox" />
                                        <asp:ImageButton ID="BtnDocumentIssueDate" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png"
                                            Style="vertical-align: bottom" />
                                    </td>
                                </tr>
                                <tr style="background-color: #f2f2f2">
                                    <td>
                                        <asp:Button ID="btnEditTaxID_WRITE" runat="server" Text="Edit" SkinID="PrimaryRightButton" />
                                        <asp:Button ID="btnUndoTaxID_WRITE" runat="server" Text="Undo" SkinID="AlternateRightButton" />
                                        <asp:Button ID="btnSaveTaxID_WRITE" runat="server" Text="Save" SkinID="PrimaryRightButton" />
                                    </td>
                                </tr>
                            </tbody>
                        </table>
                    </div>
                </div>
                <div id="tabsCertHistory">
                    <div class="Page" runat="server" id="moCertificateHistoryPanel" style="display: block; height: 300px; overflow: auto">
                        <div class="dataContainer">
                            <h2 class="dataGridHeader"></h2>
                            <div>
                                <asp:UpdatePanel ID="uplCertHistory" runat="server" UpdateMode="Always">
                                    <ContentTemplate>
                                        <asp:Button ID="btnGetCertHistory" runat="server" Style="display: none;" />
                                        <table width="100%" class="dataGrid">
                                            <tr id="trPageSize" runat="server">
                                                <td class="bor" align="left">
                                                    <asp:Label ID="moCertificateHistory_lblPageSize" runat="server">Page_Size</asp:Label><asp:Label
                                                        ID="colonSepertor" runat="server">:</asp:Label>
                                                    &nbsp;
                                                        <asp:DropDownList ID="moCertificateHistory_cboPageSize" runat="server" Width="50px"
                                                            AutoPostBack="true" SkinID="SmallDropDown">
                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                            <asp:ListItem Value="15">15</asp:ListItem>
                                                            <asp:ListItem Value="20">20</asp:ListItem>
                                                            <asp:ListItem Value="25">25</asp:ListItem>
                                                            <asp:ListItem Value="30">30</asp:ListItem>
                                                            <asp:ListItem Value="35">35</asp:ListItem>
                                                            <asp:ListItem Value="40">40</asp:ListItem>
                                                            <asp:ListItem Value="45">45</asp:ListItem>
                                                            <asp:ListItem Value="50">50</asp:ListItem>
                                                        </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:CheckBox ID="chbShowUpdates" Text="SHOW_PREMIUM_CHANGES_ONLY" runat="server"
                                                        TextAlign="Right" AutoPostBack="true" />
                                                </td>
                                                <td class="bor" align="right">
                                                    <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                                                </td>
                                            </tr>
                                        </table>
                                        <div style="width: 100%">
                                            <asp:GridView ID="CertHistoryGrid" runat="server" Width="100%" AutoGenerateColumns="False"
                                                AllowPaging="True" SkinID="DetailPageGridView" AllowSorting="true">
                                                <SelectedRowStyle Wrap="True" />
                                                <EditRowStyle Wrap="True" />
                                                <AlternatingRowStyle Wrap="True" />
                                                <HeaderStyle Wrap="false" />
                                                <RowStyle Wrap="True" />
                                                <Columns>
                                                    <asp:BoundField DataField="Record_Type" SortExpression="Record_Type" ReadOnly="true"
                                                        HeaderText="Record_Type" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                                    <asp:BoundField DataField="Description" SortExpression="Description" ReadOnly="true"
                                                        HeaderText="Description" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                                    <asp:BoundField DataField="product_code" SortExpression="product_code" ReadOnly="true"
                                                        HeaderText="product_code" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                                    <asp:BoundField DataField="source" SortExpression="source" ReadOnly="true" HeaderText="source"
                                                        HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                                    <asp:BoundField DataField="Inforce_Date" SortExpression="Inforce_Date" ReadOnly="true"
                                                        HeaderText="Inforce_Date" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:d}"
                                                        HtmlEncode="false" />
                                                    <asp:BoundField DataField="Processed_date" SortExpression="Processed_date" ReadOnly="true"
                                                        HeaderText="Processed_date" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:d}"
                                                        HtmlEncode="false" />
                                                    <asp:BoundField DataField="Customer_Name" SortExpression="Customer_Name" ReadOnly="true"
                                                        HeaderText="Customer_Name" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                                    <asp:BoundField DataField="Address1" SortExpression="Address1" ReadOnly="true" HeaderText="Address1"
                                                        HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                                    <asp:BoundField DataField="City" SortExpression="City" ReadOnly="true" HeaderText="City"
                                                        HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                                    <asp:BoundField DataField="State_Province" SortExpression="State_Province" ReadOnly="true"
                                                        HeaderText="State_Province" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                                    <asp:BoundField DataField="Zip" SortExpression="Zip" ReadOnly="true" HeaderText="Zip"
                                                        HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                                    <asp:BoundField DataField="Manufacturer" SortExpression="Manufacturer" ReadOnly="true"
                                                        HeaderText="Manufacturer" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                                    <asp:BoundField DataField="Model" SortExpression="Model" ReadOnly="true" HeaderText="Model"
                                                        HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                                    <asp:BoundField DataField="IMEI_Number" SortExpression="IMEI_Number"
                                                        ReadOnly="true" HeaderText="IMEI_Number" HeaderStyle-HorizontalAlign="Center"
                                                        HtmlEncode="false" />
                                                    <asp:BoundField DataField="Serial_Number" SortExpression="Serial_Number" ReadOnly="true"
                                                        HeaderText="Serial_No_LABEL" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                                    <asp:BoundField DataField="SKU_Number" SortExpression="SKU_Number" ReadOnly="true"
                                                        HeaderText="SKU_Number" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                                    <asp:BoundField DataField="Membership_Type" SortExpression="Membership_Type" ReadOnly="true"
                                                        HeaderText="Membership_Type" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                                    <asp:BoundField DataField="Identification_Number" SortExpression="Identification_Number"
                                                        ReadOnly="true" HeaderText="Identification_Number" HeaderStyle-HorizontalAlign="Center"
                                                        HtmlEncode="false" />
                                                    <asp:BoundField DataField="Subscriber_Status" SortExpression="Subscriber_Status"
                                                        ReadOnly="true" HeaderText="Subscriber_Status" HeaderStyle-HorizontalAlign="Center"
                                                        HtmlEncode="false" />
                                                    <asp:BoundField DataField="Home_Phone" SortExpression="Home_Phone" ReadOnly="true"
                                                        HeaderText="Home_Phone" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                                    <asp:BoundField DataField="Work_Phone" SortExpression="Work_Phone" ReadOnly="true"
                                                        HeaderText="Work_Phone" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                                    <asp:BoundField DataField="Email" SortExpression="Email" ReadOnly="true" HeaderText="Email"
                                                        HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                                    <asp:BoundField DataField="Status_Change_Date" SortExpression="Status_Change_Date" ReadOnly="true"
                                                        HeaderText="Status_Change_Date" HeaderStyle-HorizontalAlign="Center" DataFormatString="{0:d}"
                                                        HtmlEncode="false" />
                                                    <asp:BoundField DataField="bank_account_number" SortExpression="Email" ReadOnly="true" HeaderText="bank_account_number"
                                                                    HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                                    <asp:BoundField DataField="bank_name" SortExpression="Email" ReadOnly="true" HeaderText="bank_name"
                                                                    HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                                    <asp:BoundField DataField="bank_acct_owner_name" SortExpression="Email" ReadOnly="true" HeaderText="bank_acct_owner_name"
                                                                    HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                                    <asp:BoundField DataField="bank_sort_code" SortExpression="Email" ReadOnly="true" HeaderText="bank_sort_code"
                                                                    HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                                </Columns>
                                                <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                                                <PagerStyle />
                                            </asp:GridView>
                                        </div>
                                    </ContentTemplate>
                                </asp:UpdatePanel>
                            </div>
                        </div>
                    </div>
                </div>
                <div id="tabsCovHistory">
                    <div class="Page" runat="server" id="moCoverageHistory_WRITE" style="display: block; height: 300px; overflow: auto">
                        <asp:DataGrid ID="CoverageHistoryGrid" runat="server" Width="100%" AutoGenerateColumns="False"
                            AllowPaging="false" AllowSorting="True" ShowFooter="false" SkinID="DetailPageDataGrid">
                            <Columns>
                                <asp:BoundColumn SortExpression="Risk_Type" HeaderText="Risk_type" />
                                <asp:TemplateColumn HeaderText="Coverage_Type" SortExpression="Coverage_Type">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="btnEditCoverage" runat="server" CommandName="SelectAction" />
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn SortExpression="Sequence" HeaderText="Sequence" />
                                <asp:BoundColumn SortExpression="Begin_Date" HeaderText="Begin_Date" />
                                <asp:BoundColumn SortExpression="End_Date" HeaderText="End_Date" />
                            </Columns>
                            <PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7"
                                PageButtonCount="15" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                    </div>
                </div>
                <div id="tabsFinanceInfo">
                    <div id="moFinanceInfo_WRITE" class="Page" runat="server" style="display: block; height: 300px; overflow: auto">
                        <table class="formGrid" border="0">
                            <tbody>

                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moFinanceAmountLabel" runat="server">Finance_Amount</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moFinanceAmount" runat="server" SkinID="SmallTextBox" ReadOnly="true" />
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moCurrentOutstandingBalanceLabel" runat="server">CURRENT_OUTSTANDING_BALANCE</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moCurrentOutstandingBalanceText" runat="server" SkinID="SmallTextBox" ReadOnly="true" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moFinanceTermLabel" runat="server">Finance_Term</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moFinanceTerm" runat="server" SkinID="SmallTextBox" ReadOnly="true" />
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moOutstandingBalanceDueDateLabel" runat="server">OUTSTANDING_BALANCE_DUE_DATE</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moOutstandingBalanceDueDateText" runat="server" ReadOnly="true" SkinID="SmallTextBox" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moFinanceFrequencyLabel" runat="server">Finance_Frequency</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moFinanceFrequency" runat="server" SkinID="SmallTextBox" ReadOnly="true" />
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moFinanceDateLabel" runat="server">FINANCE_DATE</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moFinanceDateText" runat="server" ReadOnly="true" SkinID="SmallTextBox" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moFinanceInstallmentNumLabel" runat="server">Finance_Number_of_Installments</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moFinanceInstallmentNum" runat="server" SkinID="SmallTextBox" ReadOnly="true" />
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moDownPaymentLabel" runat="server">DOWN_PAYMENT</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moDownPaymentText" runat="server" ReadOnly="true" SkinID="SmallTextBox" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moFinanceInstallmentAmountLabel" runat="server">Finance_Installment_Amount</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moFinanceInstallmentAmount" runat="server" SkinID="SmallTextBox"
                                            ReadOnly="true" />
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moAdvancePaymentLabel" runat="server">ADVANCE_PAYMENT</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moAdvancePaymentText" runat="server" ReadOnly="true" SkinID="SmallTextBox" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moBillingAccountNumberLabel" runat="server">BILLING_ACCOUNT_NUMBER</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moBillingAccountNumberText" runat="server" SkinID="SmallTextBox"
                                            ReadOnly="true" />
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moNumOfConsecutivePaymentsLabel" runat="server">NUM_OF_CONSECUTIVE_PAYMENTS</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moNumOfConsecutivePaymentsText" runat="server" ReadOnly="true" SkinID="SmallTextBox" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moDealerCurrentPlanCodeLabel" runat="server">DEALER_CURRENT_PLAN_CODE</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moDealerCurrentPlanCodeText" runat="server" SkinID="SmallTextBox"
                                            ReadOnly="true" />
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moDealerRewardPointsLabel" runat="server">DEALER_REWARD_POINTS</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moDealerRewardPointsText" runat="server" ReadOnly="true" SkinID="SmallTextBox" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" id="moUpgradeTermUOMTD1" runat="server">
                                        <asp:Label ID="lblUpgradeTermUnitOfMeasure" runat="server">Upgrade_Term_Unit_Of_Measure</asp:Label>
                                    </td>
                                    <td align="right" id="moUpgradeTermUOMTD2" runat="server">
                                        <asp:TextBox ID="moUpgradeTermUnitOfMeasureText" runat="server" SkinID="SmallTextBox"
                                            ReadOnly="true" />
                                    </td>
                                    <td align="right" id="moUpgradeFixedTermTD1" runat="server">
                                        <asp:Label ID="moDealerScheduledPlanCodeLabel" runat="server">DEALER_SCHEDULED_PLAN_CODE</asp:Label>
                                    </td>
                                    <td align="left" id="moUpgradeFixedTermTD2" runat="server">
                                        <asp:TextBox ID="moDealerScheduledPlanCodeText" runat="server" ReadOnly="true" SkinID="SmallTextBox" />
                                    </td>
                                </tr>
                                <tr id="moUpgradeFixedTermTR" runat="server">
                                    <td align="right">
                                        <asp:Label ID="moUpgradeTermLabelFrom" runat="server">UPGRADE_TERM_FROM</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moUpgradeTermTextFROM" runat="server" SkinID="SmallTextBox"
                                            ReadOnly="true" />
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moUpgradeFixedTermLabel" runat="server">UPGRADE_TERM</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moUpgradeFixedTermText" runat="server" SkinID="SmallTextBox"
                                            ReadOnly="true" />
                                    </td>
                                </tr>
                                <tr id="Tr2" runat="server">
                                    <td align="right">
                                        <asp:Label ID="moLoanCodeLabel" runat="server">LOAN_CODE</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moLoanCodeText" runat="server" SkinID="SmallTextBox"
                                            ReadOnly="true" />
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moUpgradeTermLabelTo" runat="server">UPGRADE_TERM_TO</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moUpgradeTermTextTo" runat="server" ReadOnly="true" SkinID="SmallTextBox" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moPenaltyFeeLabel" runat="server">PENALTY_FEE</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moPenaltyFeeText" runat="server" SkinID="SmallTextBox"
                                            ReadOnly="true" />
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moPaymentShiftNumberLabel" runat="server">PAYMENT_SHIFT_NUMBER</asp:Label>
                                    </td>
                                    <td align="right">
                                        <asp:TextBox ID="moPaymentShiftNumberText" runat="server" ReadOnly="true" SkinID="SmallTextBox" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="moAppleCareFeeLabel" runat="server">APPLECARE_FEE</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moAppleCareFeeText" runat="server" SkinID="SmallTextBox"
                                            ReadOnly="true" />
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="moUpgradeProgramLabel" runat="server">UPGRADE_PROGRAM</asp:Label>
                                    </td>
                                    <td align="left">
                                        <asp:TextBox ID="moUpgradeProgramText" runat="server" SkinID="SmallTextBox"
                                            ReadOnly="true" />
                                    </td>
                                </tr>
                                <tr id="UpgradeDataGridtr" runat="server">
                                    <td colspan="4">
                                        <div style="width: 100%">
                                            <asp:GridView ID="CertUpgradeDatagrid" runat="server" Width="90%" AutoGenerateColumns="False"
                                                AllowPaging="True" SkinID="DetailPageGridView" AllowSorting="False">
                                                <SelectedRowStyle Wrap="True" />
                                                <EditRowStyle Wrap="True" />
                                                <AlternatingRowStyle Wrap="True" />
                                                <HeaderStyle Wrap="false" />
                                                <RowStyle Wrap="True" />
                                                <Columns>
                                                    <asp:BoundField DataField="Sequence_Number" HeaderText="Sequence Number" ReadOnly="true" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                                    <asp:BoundField DataField="Upgrade_Date" HeaderText="Upgrade Date" ReadOnly="true" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                                    <asp:BoundField DataField="Voucher_Number" HeaderText="Voucher Number" ReadOnly="true" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                                    <asp:BoundField DataField="Upgrade_Fee" HeaderText="Upgrade Fee" ReadOnly="true" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                                    <asp:BoundField DataField="RMA" HeaderText="RMA" ReadOnly="true" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                                    <asp:BoundField DataField="UPGRADE_APR_AMOUNT" HeaderText="Upgrade APR Amount" ReadOnly="true" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                                    <asp:BoundField DataField="UNEARNED_PREMIUM_CUSTOMER" HeaderText="Unearned Premium Customer" ReadOnly="true" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                                    <asp:BoundField DataField="UPGRADE_TOTAL_AMOUNT" HeaderText="Upgrade Total Amount" ReadOnly="true" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                                </Columns>
                                            </asp:GridView>
                                        </div>
                                    </td>
                                </tr>

                            </tbody>
                        </table>
                    </div>
                </div>
                <div id="tabsReprice">
                    <div class="Page" runat="server" id="moInstallmentHistoryPanel" style="display: block; height: 300px; overflow: auto">

                        <table width="100%" class="dataGrid">
                            <tr id="tr1" runat="server">
                                <td class="bor" align="left">
                                    <asp:Label ID="moCertInstallmentHistory_lblPageSize" runat="server">Page_Size</asp:Label><asp:Label
                                        ID="Label9" runat="server">:</asp:Label>
                                    &nbsp;
                                                        <asp:DropDownList ID="moCertInstallmentHistory_cboPageSize" runat="server" Width="50px"
                                                            AutoPostBack="true" SkinID="SmallDropDown">
                                                            <asp:ListItem Value="5">5</asp:ListItem>
                                                            <asp:ListItem Value="10">10</asp:ListItem>
                                                            <asp:ListItem Value="15">15</asp:ListItem>
                                                            <asp:ListItem Value="20">20</asp:ListItem>
                                                            <asp:ListItem Value="25">25</asp:ListItem>
                                                            <asp:ListItem Value="30">30</asp:ListItem>
                                                            <asp:ListItem Value="35">35</asp:ListItem>
                                                            <asp:ListItem Value="40">40</asp:ListItem>
                                                            <asp:ListItem Value="45">45</asp:ListItem>
                                                            <asp:ListItem Value="50">50</asp:ListItem>
                                                        </asp:DropDownList>
                                </td>
                                <td class="bor" align="right">
                                    <asp:Label ID="Label10" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <div style="width: 100%">
                            <asp:GridView ID="CertInstallmentHistoryGrid" runat="server" Width="100%" AutoGenerateColumns="False"
                                AllowPaging="True" SkinID="DetailPageGridView" AllowSorting="true">
                                <SelectedRowStyle Wrap="True" />
                                <EditRowStyle Wrap="True" />
                                <AlternatingRowStyle Wrap="True" />
                                <HeaderStyle Wrap="false" />
                                <RowStyle Wrap="True" />
                                <Columns>
                                    <asp:BoundField DataField="START_DATE" SortExpression="START_DATE"
                                        HeaderText="START_DATE" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                    <asp:BoundField DataField="END_DATE" SortExpression="END_DATE"
                                        HeaderText="END_DATE" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                    <asp:BoundField DataField="INSTALLMENT_AMOUNT" SortExpression="INSTALLMENT_AMOUNT" ReadOnly="true"
                                        HeaderText="INSTALLMENT_AMOUNT" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                </Columns>
                                <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                                <PagerStyle />
                            </asp:GridView>
                        </div>
                    </div>
                </div>

                <div id="tabsDataProtection">
                    <div class="Page" id="tabs_Data_Protection_WRITE" visible="false" style="height: 300px; overflow: auto" runat="server">

                        <div class="dataContainer" style="width: 100%">
                            <asp:GridView ID="GridDataProtection" runat="server" Width="100%" ShowFooter="false" ShowHeaderWhenEmpty="true" AutoGenerateColumns="False" CellPadding="1" AllowPaging="false" SkinID="DetailPageGridView">
                                <SelectedRowStyle Wrap="False" CssClass="SELECTED" />
                                <EditRowStyle Wrap="False" CssClass="EDITROW" />
                                <AlternatingRowStyle Wrap="False" CssClass="ALTROW" />
                                <RowStyle Wrap="False" CssClass="ROW" />
                                <HeaderStyle CssClass="HEADER" />
                                <Columns>
                                    <asp:TemplateField SortExpression="REQUEST_ID">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblRequestHeader" runat="server" Visible="True" Text="REQUEST_ID"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemStyle CssClass="CenteredTD" />
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtRequestID" runat="server" onKeyUp="javascript:Count(this,100);" onChange="javascript:Count(this,100);" Visible="True" Columns="35" MaxLength="100" />
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="COMMENTS">
                                        <HeaderTemplate>
                                            <asp:Label ID="lblCommentHeader" runat="server" Visible="True" Text="COMMENTS"></asp:Label>
                                        </HeaderTemplate>
                                        <ItemStyle CssClass="CenteredTD" />
                                        <FooterTemplate>
                                            <asp:TextBox ID="txtComment" runat="server" Visible="True" onKeyUp="javascript:Count(this,1000);" onChange="javascript:Count(this,1000);" TextMode="MultiLine" Rows="3" ForeColor="black" MaxLength="1000" Columns="35" SkinID="MediumTextBox"></asp:TextBox>
                                        </FooterTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField HeaderText="ADDED_BY" />
                                    <asp:BoundField HeaderText="TIME_STAMP" />
                                    <asp:BoundField HeaderText="STATUS" />
                                </Columns>
                            </asp:GridView>
                        </div>

                        <table width="100%" class="formGrid" border="0">
                            <tbody>
                                <tr style="background-color: #f2f2f2">

                                    <td colspan="2">
                                        <asp:Button ID="btnRightToForgotten" Visible="false" runat="server" SkinID="PrimaryRightButton" Text="RIGHT_TO_BE_FORGOTTEN"></asp:Button>&nbsp;
                                   <asp:Button ID="btnRestrict" runat="server" SkinID="PrimaryRightButton" Text="RESTRICT"></asp:Button>&nbsp;
                                      <asp:Button ID="btnUnRestrict" runat="server" Visible="false" SkinID="PrimaryRightButton" Text="UNRESTRICT"></asp:Button>&nbsp;                                       
                                    <asp:Button ID="btnCancel" runat="server" Text="Cancel" SkinID="AlternateRightButton" Visible="false"></asp:Button>&nbsp;
                                     <asp:Button ID="btnSave" runat="server" SkinID="PrimaryRightButton" Text="SAVE" Visible="false"></asp:Button>&nbsp;
                                  
                                    </td>
                                </tr>
                            </tbody>
                        </table>

                    </div>
                </div>

                <div id="tabsMigratedCertificateLink">
                    <div class="Page" runat="server" id="moCertificatesLinkPanel" style="display: block; height: 300px; overflow: auto">

                        <table width="20%" class="dataGrid">
                            <tr id="tr4" runat="server">
                                <td class="bor" align="left">
                                    <asp:Label ID="moCertificatesLinkPanel_PrevCertId" runat="server">PREVIOUS_CERTIFICATE_ID</asp:Label>
                                </td>
                                <td class="bor" align="right">
                                    <asp:LinkButton ID="linkPrevCertId" runat="server"></asp:LinkButton>
                                </td>
                            </tr>
                            <tr id="tr5" runat="server">
                                <td class="bor" align="left">
                                    <asp:Label ID="moCertificatesLinkPanel_OrigCertId" runat="server">ORIGINAL_CERTIFICATE_ID</asp:Label>
                                    &nbsp;
                                </td>
                                <td class="bor" align="right">
                                    <asp:LinkButton ID="linkOrigCertId" runat="server"></asp:LinkButton>
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
                
                <div id ="tabsCertificateExtendedFields">
                    <div class="dataContainer" style="width: 100%">
                        <asp:DataGrid ID="GridCertExtFields" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
                            OnItemCreated="GridCertExtFields_ItemCreated"
                                      SkinID="DetailPageDataGrid" AllowSorting="true">
                            <HeaderStyle />
                            <Columns>
                                <asp:BoundColumn DataField="CERT_EXT_ID" Visible="False"/>
                                <asp:BoundColumn DataField="CERT_ID" Visible="False"/>
                                <asp:BoundColumn DataField="FIELD_NAME" SortExpression="FIELD_NAME" ReadOnly="true" HeaderStyle-CssClass="FIELD_NAME" ItemStyle-CssClass="FIELD_NAME"
                                                HeaderText="FIELD_NAME" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundColumn DataField="FIELD_VALUE" SortExpression="FIELD_VALUE" ReadOnly="true" HeaderStyle-CssClass="FIELD_VALUE" ItemStyle-CssClass="FIELD_VALUE"
                                                HeaderText="FIELD_VALUE" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundColumn DataField="CREATED_BY" SortExpression="CREATED_BY" ReadOnly="true" HeaderStyle-CssClass="CREATED_BY" ItemStyle-CssClass="CREATED_BY"
                                                HeaderText="CREATED_BY" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundColumn DataField="CREATED_DATE" SortExpression="CREATED_DATE" ReadOnly="true" HeaderStyle-CssClass="CREATED_DATE" ItemStyle-CssClass="CREATED_DATE"
                                                 HeaderText="CREATED_DATE" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundColumn DataField="MODIFIED_BY" SortExpression="MODIFIED_BY" ReadOnly="true" HeaderStyle-CssClass="MODIFIED_BY" ItemStyle-CssClass="MODIFIED_BY"
                                                HeaderText="MODIFIED_BY" HeaderStyle-HorizontalAlign="Center" />
                                <asp:BoundColumn DataField="MODIFIED_DATE" SortExpression="MODIFIED_DATE" ReadOnly="true" HeaderStyle-CssClass="MODIFIED_DATE" ItemStyle-CssClass="MODIFIED_DATE"
                                                 HeaderText="MODIFIED_DATE" HeaderStyle-HorizontalAlign="Center" />
                            </Columns>
                            <PagerStyle Position="TopAndBottom" PageButtonCount="15" Mode="NumericPages"></PagerStyle>
                            <PagerStyle />
                        </asp:DataGrid>
                        </div>
                </div>
            </div>
        </div>
        <div class="dataContainer">
            <h2 class="dataGridHeader">
                <asp:Label runat="server" ID="LabelCvgInfoHdr">COVERAGE INFORMATION</asp:Label></h2>
            <asp:DataGrid ID="Grid" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="false"
                AllowSorting="True" ShowFooter="false" SkinID="DetailPageDataGrid">
                <Columns>
                    <asp:BoundColumn SortExpression="risk_type_description" HeaderText="Risk_type" />
                    <asp:TemplateColumn HeaderText="Coverage_Type" SortExpression="coverage_type_description" HeaderStyle-Wrap="false">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEditCoverage" runat="server" CommandName="SelectAction" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn SortExpression="Sequence" HeaderText="Sequence" />
                    <asp:BoundColumn SortExpression="Begin_Date" HeaderText="Begin_Date" ItemStyle-Wrap="false" />
                    <asp:BoundColumn SortExpression="End_Date" HeaderText="End_Date" ItemStyle-Wrap="false" />
                    <asp:BoundColumn SortExpression="Coverage_duration" HeaderText="Coverage_Term" HeaderStyle-Wrap="false" />
                    <asp:BoundColumn SortExpression="Coverage_Expiration_Date" HeaderText="Coverage_Expiration_Date" ItemStyle-Wrap="false" />
                    <asp:BoundColumn SortExpression="Max_Renewal_Duration" HeaderText="Max_Renewal_Duration" HeaderStyle-Wrap="false" />
                    <asp:BoundColumn SortExpression="No_of_Renewals" HeaderText="No_Of_Renewals" HeaderStyle-Wrap="false" />
                    <asp:BoundColumn SortExpression="No_of_Renewals_Remaining" HeaderText="No_of_Renewals_Remaining" HeaderStyle-Wrap="false" />
                    <asp:BoundColumn SortExpression="Renewal_Date" HeaderText="Renewal_Date" HeaderStyle-Wrap="false" ItemStyle-Wrap="false" />
                    <asp:BoundColumn HeaderText="COVERAGE_TOTAL_PAID_AMOUNT" HeaderStyle-Width="150" />
                    <asp:BoundColumn HeaderText="COVERAGE_REMAIN_LIABILITY_LIMIT" HeaderStyle-Width="150" />
<%--                    <asp:BoundColumn SortExpression="Ext_Begin_KM_MI" HeaderText="Begin_KM" ItemStyle-Wrap="false" />
                    <asp:BoundColumn SortExpression="Ext_End_KM_MI" HeaderText="End_KM" ItemStyle-Wrap="false" />--%>
                </Columns>
                <PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7"
                    PageButtonCount="15" Mode="NumericPages"></PagerStyle>
            </asp:DataGrid>
        </div>
        <div class="dataContainer">
            <h2 class="dataGridHeader">
                <asp:Label runat="server" ID="LabelClmInfoHdr">CLAIM_INFORMATION</asp:Label>
                <asp:LinkButton ID="btnNewClaim" runat="server" Text="NEW_CLAIM"></asp:LinkButton>&nbsp;
                <asp:LinkButton ID="btnNewClaimDcm" runat="server" Text="NEW_CLAIM_DCM"></asp:LinkButton>
            </h2>
            <asp:DataGrid ID="moClaimsDatagrid" runat="server" Width="100%" AllowPaging="False"
                AllowSorting="True" ShowFooter="false" SkinID="DetailPageDataGrid">
                <Columns>
                    <asp:TemplateColumn HeaderText="CLAIM_NUMBER" SortExpression="Claim_Number">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEditClaim" runat="server" CommandName="SelectAction" />
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn SortExpression="created_date" HeaderText="Date_Added" />
                    <asp:BoundColumn SortExpression="Status_Code" HeaderText="Status" />
                    <asp:BoundColumn SortExpression="Authorized_Amount" HeaderText="Auth_Amt" />
                    <asp:BoundColumn SortExpression="Total_Paid" HeaderText="Inv. Total" />
                    <asp:BoundColumn SortExpression="Extended_status" HeaderText="Extended_Status" />
                </Columns>
                <PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7"
                    PageButtonCount="15" Mode="NumericPages"></PagerStyle>
            </asp:DataGrid>
        </div>
        <div class="btnZone">
            <div class="">
                <asp:Button ID="TNCButton_WRITE" runat="server" Text="TERMANDCONDITION" SkinID="AlternateRightButton" />
                <asp:Button ID="btnCancelCertificate_WRITE" runat="server" Text="Cancel Certificate"
                    SkinID="AlternateRightButton" />
                <asp:Button ID="DocumentsButton" runat="server" Text="DOCUMENTS"
                    SkinID="AlternateRightButton" />
                <asp:Button ID="btnBack" runat="server" Text="Back" SkinID="AlternateLeftButton"></asp:Button>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel runat="server" ID="CancellationPanel">
        <div class="dataContainer Page" style="border-color: #999; border-style: solid; border-width: 1px">
            <table width="100%" class="formGrid" border="0">
                <tbody>
                    <tr>
                        <td align="right">
                            <asp:Label ID="moCancelCallerNameLabel" runat="server">NAME_OF_CALLER</asp:Label>
                        </td>
                        <td align="left" colspan="2">
                            <asp:TextBox ID="CancelCallerNameTextbox" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="moCancellationReasonDrpLabel" runat="server">CANCELLATION_REASON</asp:Label>
                        </td>
                        <td align="left" colspan="2">
                            <asp:DropDownList ID="moCancellationReasonDrop" runat="server" SkinID="MediumDropDown"
                                OnChange="return DisableControls();" />
                        </td>
                    </tr>
                    <tr id="CancelCertReqDateRowHeader" runat="server">
                        <td align="right">
                            <asp:Label ID="Label21" runat="server">REQUESTED_CANCELLATION_DATE</asp:Label>
                        </td>
                        <td align="left" colspan="2">
                            <asp:TextBox ID="CancelCertReqDateTextbox" runat="server" AutoPostBack="true" SkinID="MediumTextBox"></asp:TextBox>
                            <asp:ImageButton ID="CancelCertReqDateImageButton" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png" Style="vertical-align: bottom" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="CancelCertDateLabel" runat="server">CANCELLATION_DATE</asp:Label>
                        </td>
                        <td align="left" colspan="2">
                            <asp:TextBox ID="CancelCertDateTextbox" runat="server" SkinID="MediumTextBox" OnChange="return  DisableControls();"></asp:TextBox>
                            <asp:ImageButton ID="CancelCertDateImagebutton" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png" Style="vertical-align: bottom" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="CancelCommentType" runat="server">JUSTIFICATION</asp:Label>
                        </td>
                        <td align="left" colspan="2">
                            <asp:DropDownList ID="moCancelCommentType" runat="server" AutoPostBack="False" SkinID="MediumDropDown" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="RefundAmtLabel" runat="server">REFUND_AMOUNT</asp:Label>
                        </td>
                        <td align="left" colspan="2">
                            <asp:TextBox ID="RefundAmtTextbox" runat="server" SkinID="MediumTextBox" />
                            <asp:Label ID="LabelWarningRefundAmtBelowTolerance" runat="server" ForeColor="Red"
                                Visible="False">REFUND_AMT_BELOW_TOLERANCE</asp:Label>
                            <asp:TextBox ID="txtInstallmentsPaid" runat="server" SkinID="MediumTextBox" Visible="false" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="moCanelCommentsLabel" runat="server">COMMENTS</asp:Label>
                        </td>
                        <td align="left" colspan="2">
                            <asp:TextBox ID="moCancelCommentsTextbox" runat="server" TextMode="MultiLine" Rows="8"
                                Visible="true" SkinID="LargeTextBox" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="PaymentMethodDrpLabel" runat="server">PAYMENT_METHOD</asp:Label>
                        </td>
                        <td align="left" colspan="2">
                            <asp:DropDownList ID="PaymentMethodDrop" runat="server" SkinID="MediumDropDown" AutoPostBack="False" />
                        </td>
                    </tr>
                </tbody>
            </table>
            <div id="divbankctl" runat="server">
                <table width="100%" class="formGrid" border="0">
                    <tbody>
                        <Elita:UserControlBankInfo ID="moBankInfoController" runat="server"></Elita:UserControlBankInfo>
                    </tbody>
                </table>
                <table width="100%" class="formGrid" border="0">
                    <tbody>
                        <Elita:UserControlPaymentOrderInfo ID="moPaymentOrderInfoCtrl" runat="server"></Elita:UserControlPaymentOrderInfo>
                    </tbody>
                </table>
            </div>
            <div class="btnZone">
                <div class="">
                    <asp:Button ID="ProcessCancellationButton_WRITE" runat="server" Text="Process Cancellation"
                        SkinID="PrimaryRightButton" />
                    <asp:Button ID="QuoteButton_WRITE" runat="server" Text="Quote" SkinID="PrimaryRightButton" />
                    <asp:Button ID="BackCancelCertButton" runat="server" Text="Back" SkinID="AlternateRightButton" />
                </div>
            </div>
        </div>
    </asp:Panel>
    <asp:Panel ID="PanTaxDetails" runat="server"  BackColor="#99ccff" ScrollBars="Auto" >
         
    </asp:Panel>
    <script type="text/javascript" language="javascript">

        $(document).ready(function () {
            $('#OtherCustomerInfo').slideUp();

        });

        $("#OthCustExpander").click(function () {
            //open up the content needed - toggle the slide- if visible, slide up, if not slidedown.
            $('#OtherCustomerInfo').slideToggle(500, function () {
                //execute this after slideToggle is done
                //change text of header based on visibility of content div
                $('#OthCustExpander').html(function () {
                    //change text based on condition
                    return $('#OtherCustomerInfo').is(":visible") ? "<img src='../App_Themes/Default/Images/sort_indicator_asc.png'>" : "<img src='../App_Themes/Default/Images/sort_indicator_des.png'>";
                });
            });

        });

        function DisableControls() {
            //  alert('1');
        }

        function LoadXmlDoc(fileName) {
            if (window.XMLHttpRequest) {
                xhttp = new XMLHttpRequest();
            }
            else {
                xhttp = new ActiveXObject("Microsoft.XMLHTTP");
            }
            xhttp.open("GET", fileName, false);
            xhttp.send();
            return xhttp.responseXML;
        }

        function GetXmlDoc(xmlString) {
            if (window.DOMParser) {
                parser = new DOMParser();
                xmlDoc = parser.parseFromString(xmlString, "text/xml");
            }
            else // Internet Explorer
            {
                xmlDoc = new ActiveXObject("Microsoft.XMLDOM");
                xmlDoc.async = false;
                xmlDoc.loadXML(xmlString);
            }
            return xmlDoc;
        }

        function TransformToHtmlText(xmlDoc, xsltDoc) {

            //FOR ie11 only
            var xslt = new ActiveXObject("Msxml2.XSLTemplate");
            var xslDoc = new ActiveXObject("Msxml2.FreeThreadedDOMDocument");
            var serializer = new XMLSerializer();
            strXSLT = serializer.serializeToString(xsltDoc);
            xslDoc.loadXML(strXSLT);
            xslt.stylesheet = xslDoc;
            var xslProc = xslt.createProcessor();
            xslProc.input = xmlDoc;
            xslProc.transform();
            return xslProc.output;
        }

        function ShowHideCustomerDetails(index, customerId, custInfoExclude, cust_salutation_exclude, lang_id, identification_number_type) {

            if ($('#Expand' + index).text() == '+') {
                $.ajax({
                    type: "POST",
                    url: "CertificateForm.aspx/GetOtherCustomerDetails",
                    data: '{ customerId: "' + customerId + '" , custInfoExclude: "' + custInfoExclude + '" , cust_salutation_exclude: "' + cust_salutation_exclude + '" , lang_id: "' + lang_id + '" , identification_number_type: "' + identification_number_type + '"}',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        xml = GetXmlDoc(msg.d);
                        xsl = LoadXmlDoc("CustomerDetail.xslt");

                        // code for IE
                        if (window.ActiveXObject) {
                            ex = xml.transformNode(xsl);
                            $('#Cell' + index).html(ex);

                        }
                        //IE 11 only
                        else if (!(window.ActiveXObject) && "ActiveXObject" in window) {
                            resultHTML = TransformToHtmlText(xml, xsl);
                            $('#Cell' + index).html(resultHTML);
                        }
                        // code for Mozilla, Firefox, Opera, etc.
                        else if (document.implementation && document.implementation.createDocument) {
                            xsltProcessor = new XSLTProcessor();
                            xsltProcessor.importStylesheet(xsl);
                            resultDocument = xsltProcessor.transformToFragment(xml, document);
                            $('#Cell' + index).html(resultDocument);
                        }
                        $('#Child' + index).slideDown('slow');
                        $('#Expand' + index).text('-');
                        $('#Expand' + index).parent().attr('RowSpan', '2');
                    }
                });
            }
            else {
                $('#Child' + index).slideUp();
                $('#Expand' + index).text('+');
                $('#Expand' + index).parent().removeAttr('RowSpan');
            }
        }
    </script>
    <input id="HiddenSavePromptResponse" type="hidden" name="HiddenSavePromptResponse"
        runat="server" />
    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
        runat="server" />
    <input id="HiddenTransferOfOwnershipPromptResponse" type="hidden" name="HiddenTransferOfOwnershipPromptResponse"
        runat="server" />
</asp:Content>
