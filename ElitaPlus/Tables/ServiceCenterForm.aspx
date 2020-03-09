<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ServiceCenterForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ServiceCenterForm" MasterPageFile="../Navigation/masters/ElitaBase.Master"
    Theme="Default" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>
<%@ Register TagPrefix="uc1" TagName="UserControlAddress" Src="../Common/UserControlAddress_New.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UserControlAvailableSelected" Src="../Common/UserControlAvailableSelected_New.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UserControlBankInfo" Src="../Common/UserControlBankInfo_New.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UserControlContactInfo" Src="../Common/UserControlContactInfo_New.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlAttrtibutes" Src="~/Common/UserControlAttrtibutes.ascx" %>
<%--START   DEF-2818--%>
<%@ Register TagPrefix="Elita" TagName="MessageController" Src="~/Common/MessageController.ascx" %>
<%--END   DEF-2818--%>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <link type="text/css" href="../Navigation/styles/jquery-ui.min.css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.12.4.min.js"> </script>
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-ui.min.js"> </script>
    <link type="text/css" href="../Navigation/styles/jquery-ui.elita.css" rel="stylesheet" />
    <script type="text/javascript">
        $(function () {
            // Disable tabs.
            var disabledTabs = $("input[id$='hdnDisabledTabs']").val().split(',');
            var disabledTabsIndexArr = [];
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
                },
                active: document.getElementById('<%= hdnSelectedTab.ClientID %>').value,
                disabled: disabledTabsIndexArr
            });
        });
    </script>
    <script language="javascript" type="text/javascript">
        function showProcessingFee(obj) {
            var objLabelProcessingFee = document.getElementById('<%=LabelProcessingFee.ClientID%>');
            var objTextboxPROCESSING_FEE = document.getElementById('<%=TextboxPROCESSING_FEE.ClientID%>');
            objTextboxPROCESSING_FEE.value = "";
            if (obj.checked) {
                objLabelProcessingFee.style.display = '';
                objTextboxPROCESSING_FEE.style.display = '';
            }
            else {
                objLabelProcessingFee.style.display = 'none'
                objTextboxPROCESSING_FEE.style.display = 'none';
            }
        }

        function comboSelectedMasterCenter(source, eventArgs) {
            //put the selected value in a hidden textbox - runat server so you can read it on postback
            // alert( " Key : "+ eventArgs.get_text() +"  Value :  "+eventArgs.get_value());
            var inpId = document.getElementById('<%=inpMasterCenterId.ClientID%>');
            var inpDesc = document.getElementById('<%=inpMasterCenterDesc.ClientID%>');
            inpId.value = eventArgs.get_value();
            inpDesc.value = eventArgs.get_text();
        }

        function comboSelectedLoanerCenter(source, eventArgs) {
            //put the selected value in a hidden textbox - runat server so you can read it on postback
            // alert( " Key : "+ eventArgs.get_text() +"  Value :  "+eventArgs.get_value());
            var inpId = document.getElementById('<%=inpLoanerCenterId.ClientID%>');
            var inpDesc = document.getElementById('<%=inpLoanerCenterDesc.ClientID%>');
            inpId.value = eventArgs.get_value();
            inpDesc.value = eventArgs.get_text();
        }
        function SetPayMasterVisible(blnSetDefalt) {
            return;
            var objM = document.getElementById('<%=TextBoxMasterCenter.ClientID%>');
            var objL = document.getElementById('<%=lblPayMaster.ClientID%>');
            var objC = document.getElementById('<%=chkPayMaster.ClientID%>');
            if (objM.value) {
                if (trim(objM.value) == "") {
                    objL.style.display = 'none';
                    objC.style.display = 'none';
                    if (blnSetDefalt)
                        objC.checked = false;
                } else {
                    objL.style.display = '';
                    objC.style.display = '';
                    if (blnSetDefalt) {
                        objC.checked = true;
                    }
                }
            } else {
                objL.style.display = 'none';
                objC.style.display = 'none';
                if (blnSetDefalt)
                    objC.checked = false;
            }

        }
        function MasterCenterChanged() {
            SetPayMasterVisible(true);
        }

        SetPayMasterVisible(false);

        //  document.getElementById('<%=TextBoxMasterCenter.ClientID%>').onchange = MasterCenterChanged;
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
    <%--END   DEF-2818--%>
    <Elita:MessageController runat="server" ID="moMessageController" Visible="false" />
    <%--END   DEF-2818--%>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto">
        <Scripts>
            <asp:ScriptReference Path="~/Navigation/scripts/ComunaSuggest.js" />
        </Scripts>
    </asp:ScriptManager>
    <asp:Panel ID="EditPanel" runat="server" Height="40%" Width="100%">
        <div class="dataContainer">
            <table class="formGrid" border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px;">
                <tr>
                    <td align="right" class="borderLeft" nowrap="nowrap">
                        <asp:Label ID="moCountryLabel" runat="server">Country</asp:Label>:
                    </td>
                    <td align="left" nowrap="nowrap">
                        <asp:TextBox ID="moCountryLabel_NO_TRANSLATE" TabIndex="1" runat="server" SkinID="MediumTextBox"
                            Enabled="False"></asp:TextBox>
                        <asp:DropDownList ID="moCountryDrop" TabIndex="1" runat="server" AutoPostBack="True">
                        </asp:DropDownList>
                    </td>
                    <td align="right" nowrap="nowrap">
                        <asp:Label ID="LabelStatusCode" runat="server">STATUS</asp:Label>
                    </td>
                    <td align="left" nowrap="nowrap">
                        <asp:DropDownList ID="cboStatusCode" TabIndex="2" runat="server" SkinID="MediumDropDown">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="borderLeft" nowrap="nowrap">
                        <asp:Label ID="LabelCode" runat="server">SERVICE_CENTER_CODE</asp:Label>
                    </td>
                    <td align="left" nowrap="nowrap">
                        <asp:TextBox ID="TextboxCode" TabIndex="3" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                    </td>
                    <td align="right" nowrap="nowrap">
                        <asp:Label ID="LabelDescription" runat="server">SERVICE_CENTER_NAME</asp:Label>
                    </td>
                    <td align="left" nowrap="nowrap">
                        <asp:TextBox ID="TextboxDescription" TabIndex="4" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="borderLeft" nowrap="nowrap">
                        <asp:Label ID="LabelDateAdded" runat="server">DATE_ADDED</asp:Label>
                    </td>
                    <td align="left" nowrap="nowrap">
                        <asp:TextBox ID="TextboxDateAdded" TabIndex="5" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                    </td>
                    <td align="right" nowrap="nowrap">
                        <asp:Label ID="lblPriceList" runat="server">PRICE_LIST</asp:Label>
                    </td>
                    <td align="left" nowrap="nowrap">
                        <asp:DropDownList ID="ddlPriceList" TabIndex="6" runat="server" AutoPostBack="False" SkinID="MediumDropDown">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="borderLeft" nowrap="nowrap">
                        <asp:Label ID="LabelDateLastMaintained" runat="server">DATE_LAST_MAINTAINED</asp:Label>
                    </td>
                    <td align="left" nowrap="nowrap">
                        <asp:TextBox ID="TextboxDateLastMaintained" TabIndex="7" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                    </td>
                    <td align="right" nowrap="nowrap">
                        <asp:Label ID="LabelServiceGroupId" runat="server">SERVICE_GROUP</asp:Label>
                    </td>
                    <td align="left" nowrap="nowrap">
                        <asp:DropDownList ID="cboServiceGroupId" TabIndex="8" runat="server" SkinID="MediumDropDown">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="borderLeft" nowrap="nowrap">
                        <asp:Label ID="labelRoute" runat="server">ROUTE</asp:Label>:
                    </td>
                    <td align="left" nowrap="nowrap">
                        <asp:TextBox ID="TextboxRoute" TabIndex="9" Enabled="false" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                    </td>
                    <td align="right" nowrap="nowrap">
                        <asp:Label ID="LabelBusinessHours" runat="server">BUSINESS_HOURS</asp:Label>
                    </td>
                    <td align="left" nowrap="nowrap">
                        <asp:TextBox ID="TextboxBusinessHours" TabIndex="10" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4"></td>
                </tr>
                <tr>
                    <td align="right" class="borderLeft" nowrap="nowrap">
                        <asp:Label ID="LabelFtpAddress" runat="server">FTP_ADDRESS</asp:Label>
                    </td>
                    <td align="left" nowrap="nowrap">
                        <asp:TextBox ID="TextboxFtpAddress" TabIndex="11" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                    </td>
                    <td align="right" nowrap="nowrap">
                        <asp:Label ID="LabelOwnerName" runat="server">OWNER_NAME</asp:Label>
                    </td>
                    <td align="left" nowrap="nowrap">
                        <asp:TextBox ID="TextboxOwnerName" TabIndex="12" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="borderLeft" nowrap="nowrap">
                        <asp:Label ID="LabelTaxId" runat="server">TAX_ID</asp:Label>
                    </td>
                    <td align="left" nowrap="nowrap">
                        <asp:TextBox ID="TextboxTaxId" TabIndex="13" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                    </td>
                    <td align="right" nowrap="nowrap">
                        <asp:Label ID="LabelContactName" runat="server">CONTACT_NAME</asp:Label>
                    </td>
                    <td align="left" nowrap="nowrap">
                        <asp:TextBox ID="TextboxContactName" TabIndex="14" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="borderLeft" nowrap="nowrap">
                        <asp:Label ID="LabelServiceWarrantyDays" runat="server">SERVICE_WARRANTY_DAYS</asp:Label>
                    </td>
                    <td align="left" nowrap="nowrap">
                        <asp:TextBox ID="TextboxServiceWarrantyDays" TabIndex="15" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                    </td>
                    <td align="right" nowrap="nowrap">
                        <asp:Label ID="LabelPhone1" runat="server">PHONE1</asp:Label>
                    </td>
                    <td align="left" nowrap="nowrap">
                        <asp:TextBox ID="TextboxPhone1" TabIndex="16" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="borderLeft" nowrap="nowrap">
                        <asp:Label ID="LabelRatingCode" runat="server">RATING_CODE</asp:Label>
                    </td>
                    <td align="left" nowrap="nowrap">
                        <asp:TextBox ID="TextboxRatingCode" TabIndex="17" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                    </td>
                    <td align="right" nowrap="nowrap">
                        <asp:Label ID="LabelPhone2" runat="server">PHONE2</asp:Label>
                    </td>
                    <td align="left" nowrap="nowrap">
                        <asp:TextBox ID="TextboxPhone2" TabIndex="18" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="borderLeft" nowrap="nowrap">
                        <asp:Label ID="LabelFreeZone" runat="server">Free_Zone</asp:Label>:
                    </td>
                    <td align="left" nowrap="nowrap">
                        <asp:CheckBox ID="CheckBoxFreeZone" TabIndex="19" runat="server" CssClass="disabled"></asp:CheckBox>
                    </td>
                    <td align="right" nowrap="nowrap">
                        <asp:Label ID="LabelFax" runat="server">FAX</asp:Label>
                    </td>
                    <td align="left" nowrap="nowrap">
                        <asp:TextBox ID="TextboxFax" TabIndex="20" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="borderLeft" nowrap="nowrap">
                        <asp:Label ID="LabelIvaResponsible" runat="server">IVA_Responsible</asp:Label>:
                    </td>
                    <td align="left" nowrap="nowrap">
                        <asp:CheckBox ID="CheckBoxIvaResponsible" TabIndex="21" runat="server" CssClass="disabled"></asp:CheckBox>
                    </td>
                    <td align="right" nowrap="nowrap">
                        <asp:Label ID="LabelDefaultToEmail" runat="server">Default_To_Email</asp:Label>:
                    </td>
                    <td align="left" nowrap="nowrap">
                        <asp:CheckBox ID="CheckBoxDefaultToEmail" TabIndex="22" runat="server" CssClass="disabled"></asp:CheckBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="borderLeft" nowrap="nowrap">
                        <asp:Label ID="LabelShipping" runat="server">SHIPPING</asp:Label>:
                    </td>
                    <td align="left" nowrap="nowrap">
                        <asp:CheckBox ID="CheckBoxShipping" TabIndex="23" runat="server" AutoPostBack="true" CssClass="disabled"><%--OnCheckedChanged="CheckBoxShipping_CheckedChanged"--%>
                        </asp:CheckBox>
                    </td>
                    <td align="right" nowrap="nowrap">
                        <asp:Label ID="LabelEmail" runat="server">EMAIL</asp:Label>
                    </td>
                    <td align="left" nowrap="nowrap">
                        <asp:TextBox ID="TextboxEmail" TabIndex="24" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="borderLeft" nowrap="nowrap">
                        <asp:Label ID="LabelProcessingFee" runat="server">PROCESSING_FEE</asp:Label>
                    </td>
                    <td align="left" nowrap="nowrap">
                        <asp:TextBox ID="TextboxPROCESSING_FEE" TabIndex="25" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                    </td>
                    <td align="right" nowrap="nowrap">
                        <asp:Label ID="LabelCcEmail" runat="server">CC_EMAIL</asp:Label>
                    </td>
                    <td align="left" nowrap="nowrap">
                        <asp:TextBox ID="TextboxCcEmail" TabIndex="26" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="borderLeft" nowrap="nowrap">
                        <asp:Label ID="IntegratedWithLabel" runat="server">INTEGRATED_WITH</asp:Label>:
                    </td>
                    <td align="left" nowrap="nowrap">
                        <asp:DropDownList ID="cboIntegratedWithId" TabIndex="27" runat="server" AutoPostBack="False"
                            SkinID="MediumDropDown">
                        </asp:DropDownList>
                    </td>
                    <td align="right" nowrap="nowrap">
                        <asp:Label ID="IntegratedAsOfLabel" runat="server">INTEGRATED_AS_OF</asp:Label>
                    </td>
                    <td align="left" nowrap="nowrap">
                        <asp:TextBox ID="TextboxIntegratedAsOf" TabIndex="28" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4"></td>
                </tr>
                <tr>
                    <td align="right" class="borderLeft" nowrap="nowrap">
                        <asp:Label ID="PaymentMethodDrpLabel" runat="server">PAYMENT_METHOD</asp:Label>
                    </td>
                    <td align="left" nowrap="nowrap">
                        <asp:DropDownList ID="cboPaymentMethodId" TabIndex="29" runat="server" AutoPostBack="true"
                            SkinID="SmallDropDown">
                        </asp:DropDownList>
                    </td>
                    <td align="right" nowrap="nowrap">
                        <asp:Label ID="LabelMasterCenterId" runat="server">MASTER_CENTER</asp:Label>
                    </td>
                    <td align="left" nowrap="nowrap">
                        <asp:TextBox ID="TextBoxMasterCenter" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        <input id="inpMasterCenterId" type="hidden" name="inpMasterCenterId" runat="server" />
                        <input id="inpMasterCenterDesc" type="hidden" name="inpMasterCenterDesc" runat="server" />
                        <cc1:AutoCompleteExtender ID="aCompMasterCenter" OnClientItemSelected="comboSelectedMasterCenter"
                            runat="server" TargetControlID="TextBoxMasterCenter" ServiceMethod="PopulateMasterCenterDrop"
                            MinimumPrefixLength='1' CompletionListCssClass="autocomplete_completionListElement">
                        </cc1:AutoCompleteExtender>
                        <cc1:TextBoxWatermarkExtender ID="tMasterCenter" Enabled="true" runat="server" TargetControlID="TextBoxMasterCenter" WatermarkText="Enter text to Search..">
                        </cc1:TextBoxWatermarkExtender>

                    </td>
                </tr>
                <tr>
                    <td align="right" class="borderLeft" nowrap="nowrap">
                        <asp:Label ID="lblDealer" runat="server">ORIGINAL_DEALER</asp:Label>
                    </td>
                    <td align="left" nowrap="nowrap">
                        <asp:DropDownList ID="cboOriginalDealer_WRITE" TabIndex="30" runat="server" SkinID="MediumDropDown">
                        </asp:DropDownList>
                    </td>
                    <td align="right" nowrap="nowrap">
                        <asp:Label ID="LabelLoanerCenterId" runat="server">LOANER_CENTER</asp:Label>
                    </td>
                    <td align="left" nowrap="nowrap">
                        <asp:TextBox ID="TextBoxLoanerCenter" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        <input id="inpLoanerCenterId" type="hidden" name="inpLoanerCenterId" runat="server" />
                        <input id="inpLoanerCenterDesc" type="hidden" name="inpLoanerCenterDesc" runat="server" />
                        <cc1:AutoCompleteExtender ID="aCompLoanerCenter" OnClientItemSelected="comboSelectedLoanerCenter"
                            runat="server" TargetControlID="TextBoxLoanerCenter" ServiceMethod="PopulateLoanerCenterDrop"
                            MinimumPrefixLength='1' CompletionListCssClass="autocomplete_completionListElement">
                        </cc1:AutoCompleteExtender>
                        <cc1:TextBoxWatermarkExtender ID="tLoanerCenter" Enabled="true" runat="server" TargetControlID="TextBoxLoanerCenter" WatermarkText="Enter text to Search..">
                        </cc1:TextBoxWatermarkExtender>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="borderLeft" nowrap="nowrap"></td>
                    <td align="left" nowrap="nowrap"></td>
                    <td align="right" nowrap="nowrap">
                        <asp:Label ID="lblPayMaster" runat="server">PAY_MASTER:</asp:Label>
                    </td>
                    <td align="left" nowrap="nowrap">
                        <asp:CheckBox ID="chkPayMaster" runat="server" CssClass="disabled" TabIndex="31"></asp:CheckBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="borderLeft" nowrap="nowrap">
                        <asp:Label ID="lblReverseLogistics" runat="server">REVERSE_LOGISTICS</asp:Label>
                    </td>
                    <td align="left" nowrap="nowrap">
                        <asp:DropDownList ID="cboReverseLogisticsId" TabIndex="32" runat="server" AutoPostBack="False"
                            SkinID="MediumDropDown">
                        </asp:DropDownList>
                    </td>
                    <td align="right" nowrap="nowrap">
                        <asp:Label ID="lblWithholdingRate" runat="server">WITHHOLDING_RATE</asp:Label>
                    </td>
                    <td align="left" nowrap="nowrap">
                        <asp:TextBox ID="TextBoxWithholdingRate" runat="server" SkinID="MediumTextBox" TabIndex="14"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td colspan="4" height="5"></td>
                </tr>
                <uc1:UserControlBankInfo ID="moBankInfoController" runat="server"></uc1:UserControlBankInfo>
                <tr>
                    <td colspan="4" height="5"></td>
                </tr>
                <tr>
                    <td align="right" class="borderLeft" nowrap="nowrap">
                        <asp:Label ID="lblDistributionMethod" runat="server">DISTRIBUTION_METHOD</asp:Label>
                    </td>
                    <td align="left" nowrap="nowrap">
                        <asp:DropDownList ID="ddlDistributionMethod" TabIndex="33" runat="server" AutoPostBack="False"
                            SkinID="MediumDropDown">
                        </asp:DropDownList>
                    </td>
                    <td align="right" nowrap="nowrap">
                        <asp:Label ID="lblFulFillingTimeZone" runat="server">FULFILLING_TIME_ZONE</asp:Label>
                    </td>
                    <td align="left" nowrap="nowrap">
                        <asp:DropDownList ID="ddlFulFillingTimeZone" runat="server" AutoPostBack="False"
                            TabIndex="34" SkinID="MediumDropDown">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="borderLeft" nowrap="nowrap">
                        <asp:Label ID="lblDiscountPercent" runat="server">DISCOUNT_PERCENT</asp:Label>
                    </td>
                    <td align="left" nowrap="nowrap">
                        <asp:TextBox ID="TextBoxDiscountPercent" TabIndex="35" runat="server" AutoPostBack="False"
                            SkinID="MediumTextBox"></asp:TextBox>
                    </td>
                    <td align="right" nowrap="nowrap">
                        <asp:Label ID="lblNetDays" runat="server">NET_DAYS</asp:Label>
                    </td>
                    <td align="left" nowrap="nowrap">
                        <asp:TextBox ID="TextBoxNetDays" TabIndex="36" runat="server" AutoPostBack="False"
                            SkinID="MediumTextBox">
                        </asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="borderLeft" nowrap="nowrap">
                        <asp:Label ID="lblDiscountDays" runat="server">DISCOUNT_DAYS</asp:Label>
                    </td>
                    <td align="left" nowrap="nowrap">
                        <asp:TextBox ID="TextBoxDiscountDays" TabIndex="37" runat="server" AutoPostBack="False"
                            SkinID="MediumTextBox"></asp:TextBox>
                    </td>
                    <td align="right" nowrap="nowrap">
                        <asp:Label ID="lblPreInvoice" runat="server">PRE_INVOICE</asp:Label>
                    </td>
                    <td align="left" nowrap="nowrap">
                        <asp:DropDownList ID="ddlPreInvoice" TabIndex="38" runat="server" AutoPostBack="False" SkinID="MediumDropDown"></asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right" class="borderLeft" nowrap="nowrap">
                        <asp:Label ID="lblAutoProcessInventoryFile" runat="server">AUTO_PROCESS_INVENTORY_FILE</asp:Label>
                    </td>
                    <td align="left" nowrap="nowrap">
                        <asp:DropDownList ID="ddlAutoProcessInventoryFile" TabIndex="39" runat="server" AutoPostBack="False"
                            SkinID="MediumDropDown">
                        </asp:DropDownList>
                    </td>
                    <td align="right" nowrap="nowrap"></td>
                    <td align="left" nowrap="nowrap"></td>
                </tr>
                <tr>
                    <td colspan="4" height="5"></td>
                </tr>
            </table>
        </div>
    </asp:Panel>
    <div class="dataContainer">
        <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0" />
        <asp:HiddenField ID="hdnDisabledTabs" runat="server" Value="" />
        <div id="tabs" class="style-tabs">
            <ul>
                <li><a href="#tabsAddress">
                    <asp:Label ID="Label4" runat="server" CssClass="tabHeaderText">Address</asp:Label></a></li>
                <li><a href="#tabsMfgAuthSvcCtr">
                    <asp:Label ID="Label6" runat="server" CssClass="tabHeaderText">Mfg_Auth_Svc_Ctr</asp:Label></a></li>
                <li><a href="#tabsCoveredDist">
                    <asp:Label ID="Label8" runat="server" CssClass="tabHeaderText">Covered_District</asp:Label></a></li>
                <li><a href="#tabsDealerPreferred">
                    <asp:Label ID="Label1" runat="server" CssClass="tabHeaderText">Dealer_Preferred</asp:Label></a></li>
                <li><a href="#tabsSvcNetwork">
                    <asp:Label ID="Label2" runat="server" CssClass="tabHeaderText">Service_Network</asp:Label></a></li>
                <li><a href="#tabsComment">
                    <asp:Label ID="Label3" runat="server" CssClass="tabHeaderText">Comment</asp:Label></a></li>
                <li><a href="#tabsMthOfRepair">
                    <asp:Label ID="Label5" runat="server" CssClass="tabHeaderText">Method_Of_Repair</asp:Label></a></li>
                <li><a href="#tabsContacts">
                    <asp:Label ID="Label7" runat="server" CssClass="tabHeaderText">Contacts</asp:Label></a></li>
                <li><a href="#tabsAttributes">
                    <asp:Label ID="Label9" runat="server" CssClass="tabHeaderText">Attributes</asp:Label></a></li>
                <li><a href="#tabsQuantity">
                    <asp:Label ID="Label10" runat="server" CssClass="tabHeaderText">Quantity</asp:Label></a></li>
                <li><a href="#tabsSchedule">
                    <asp:Label ID="Label11" runat="server" CssClass="tabHeaderText">Schedule</asp:Label></a></li>
                <li><a href="#tabsPriceList">
                    <asp:Label ID="Label12" runat="server" CssClass="tabHeaderText">Price_List</asp:Label></a></li>
            </ul>
            <div id="tabsAddress">
                <asp:Panel ID="moAddressTabPanel_WRITE" runat="server" Width="100%" Height="300px">
                    <table id="tblAddress" class="formGrid" cellspacing="4" cellpadding="4" rules="cols"
                        class="noBor" width="100%" border="0">
                        <tr>
                            <td align="left" colspan="1" height="100%">
                                <uc1:UserControlAddress ID="moAddressController" runat="server"></uc1:UserControlAddress>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </div>
            <div id="tabsMfgAuthSvcCtr">
                <asp:Panel ID="moMfg_Auth_Svc_CtrTabPanel_WRITE" runat="server" Width="100%" Height="300px">
                    <div id="scroller1" style="overflow: auto; width: 99.53%;" align="center">
                        <table id="tblMfgDetail" style="width: 100%; height: 90%" cellspacing="2" cellpadding="2"
                            rules="cols" border="0">
                            <tr>
                                <td valign="top" colspan="2" width="50%">
                                    <table id="TableMfg" cellspacing="1" cellpadding="1" width="100%" border="0">
                                        <tr>
                                            <td align="center" colspan="2">
                                                <uc1:UserControlAvailableSelected ID="UserControlAvailableSelectedManufacturers"
                                                    runat="server"></uc1:UserControlAvailableSelected>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <!-- Tab end -->
                </asp:Panel>
            </div>
            <div id="tabsCoveredDist">
                <asp:Panel ID="moCovered_DistrictTabPanel_WRITE" runat="server" Width="100%" Height="300px">
                    <div id="scroller2" style="overflow: auto; width: 99.53%;" align="center">
                        <table id="tblDstDetail" style="width: 100%; height: 90%" cellspacing="2" cellpadding="2"
                            rules="cols" border="0">
                            <tr>
                                <td valign="top" colspan="2" width="50%">
                                    <table id="TableDst" cellspacing="1" cellpadding="1" width="100%" border="0">
                                        <tr>
                                            <td align="center" colspan="2">
                                                <uc1:UserControlAvailableSelected ID="UsercontrolAvailableSelectedDistricts" runat="server"></uc1:UserControlAvailableSelected>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <!-- Tab end -->
                </asp:Panel>
            </div>
            <div id="tabsDealerPreferred">
                <asp:Panel ID="moDealer_PreferredTabPanel_WRITE" runat="server" Width="100%" Height="300px">
                    <div id="scroller3" style="overflow: auto; width: 99.53%;" align="center">
                        <table id="tblDlrDetail" style="width: 100%; height: 90%" cellspacing="2" cellpadding="2"
                            rules="cols" border="0">
                            <tr>
                                <td valign="top" colspan="2" width="50%">
                                    <table id="TableDlr" cellspacing="1" cellpadding="1" width="100%" border="0">
                                        <tr>
                                            <td align="center" colspan="2">
                                                <uc1:UserControlAvailableSelected ID="UsercontrolAvailableSelectedDealers" runat="server"></uc1:UserControlAvailableSelected>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <!-- Tab end -->
                </asp:Panel>
            </div>
            <div id="tabsSvcNetwork">
                <asp:Panel ID="moServiceNetworkTabPanel_WRITE" runat="server" Width="100%" Height="300px">
                    <div id="scroller4" style="overflow: auto; width: 99.53%;" align="center">
                        <table id="tblExcDlrDetail" style="width: 100%; height: 90%" cellspacing="2" cellpadding="2"
                            rules="cols" border="0">
                            <tr>
                                <td valign="top" width="50%" colspan="2">
                                    <table id="TableExcDlr" cellspacing="1" cellpadding="1" width="100%" border="0">
                                        <tr>
                                            <td align="center" colspan="2">
                                                <uc1:UserControlAvailableSelected ID="UsercontrolavailableselectedServiceNetworks"
                                                    runat="server"></uc1:UserControlAvailableSelected>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <!-- Tab end -->
                </asp:Panel>
            </div>
            <div id="tabsComment">
                <asp:Panel ID="moCommentsInformationTabPanel_WRITE" runat="server" Width="100%" Height="300px">
                    <div id="scroller5" style="overflow: auto; width: 99.53%;" align="center">
                        <table id="tblCommentsInformation" style="width: 100%; height: 90%" cellspacing="2"
                            cellpadding="2" rules="cols" border="0">
                            <tr>
                                <td valign="top" width="50%" colspan="2">
                                    <table id="Table5" cellspacing="1" cellpadding="1" width="100%" border="0" class="formGrid">
                                        <tr>
                                            <td valign="top" align="right" width="10%">
                                                <asp:Label ID="LabelComment1" runat="server">COMMENT</asp:Label>&nbsp;
                                            </td>
                                            <td width="90%">
                                                <asp:TextBox ID="TextboxComment" runat="server" TabIndex="41" Width="99%" TextMode="MultiLine"
                                                    Rows="9"></asp:TextBox>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <!-- Tab end -->
                </asp:Panel>
            </div>
            <div id="tabsMthOfRepair">
                <table id="Table7" align="center" border="0" class="dataGrid" rules="cols" width="98%">
                    <tr>
                        <td align="right">
                            <asp:Label ID="Label13" runat="server">Available</asp:Label>:
                        </td>
                        <td colspan="2">
                            <asp:DropDownList ID="ddlMethodOfRepairAvailable" Width="200px" runat="server" SkinID="SmallDropDown">
                            </asp:DropDownList>
                            &nbsp;
                            <asp:Button ID="btnAddMethodOfRepair" runat="server" Width="41px" Text="Add" SkinID="AlternateLeftButton"></asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="3">
                            <div id="MethodOfRepair" style="overflow: auto; width: 96%; height: 125px" align="center">
                                <asp:GridView ID="GridViewMethodOfRepair" runat="server" Width="100%"
                                    AllowPaging="false" AllowSorting="False" CellPadding="1"
                                    AutoGenerateColumns="False" SkinID="DetailPageGridView" PageSize="50">
                                    <SelectedRowStyle Wrap="True"></SelectedRowStyle>
                                    <EditRowStyle Wrap="True"></EditRowStyle>
                                    <AlternatingRowStyle Wrap="True"></AlternatingRowStyle>
                                    <RowStyle Wrap="True" HorizontalAlign="Left"></RowStyle>
                                    <Columns>
                                        <asp:TemplateField HeaderText="Method_Of_Repair">
                                            <HeaderStyle></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="labelMethodOfRepair"
                                                    runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField HeaderText="SERVICE_WARRANTY_DAYS" ItemStyle-Width="30%">
                                            <HeaderStyle></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="LabelServiceWarrantyDays" runat="server" Visible="True" Text='<%# DataBinder.Eval(Container.DataItem, "ServiceWarrantyDays")%>' />
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="TextBoxServiceWarrantyDays" runat="server" Visible="True"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="30px">
                                            <HeaderStyle ForeColor="#12135B" Wrap="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="EditButton_WRITE" Style="cursor: hand" runat="server" ImageUrl="~/Navigation/images/icons/edit2.gif"
                                                    CommandName="EditRecord" CommandArgument='<%# Databinder.Eval(Container.DataItem, "id") %>'></asp:ImageButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:LinkButton ID="BtnCancel" runat="server" CommandName="CancelRecord" CommandArgument="<%#Container.DisplayIndex %>"
                                                    Text="Cancel"></asp:LinkButton>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ItemStyle-Width="30px">
                                            <HeaderStyle ForeColor="#12135B" Wrap="False"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:ImageButton Style="cursor: hand;" ID="DeleteButton_WRITE" ImageUrl="~/Navigation/images/icons/trash.gif"
                                                    runat="server" CommandName="DeleteRecord" CommandArgument='<%# Databinder.Eval(Container.DataItem, "id") %>'></asp:ImageButton>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:Button ID="BtnSave_WRITE" runat="server" CommandName="SaveRecord" CommandArgument="<%#Container.DisplayIndex %>"
                                                    Text="Save" SkinID="PrimaryRightButton"></asp:Button>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                </table>
            </div>
            <div id="tabsContacts">
                <asp:Panel ID="moContactsTabPanel_WRITE" runat="server" Width="100%" Height="300px">
                    <div id="Div2" style="overflow: auto; width: 99.53%;" align="center">
                        <asp:Panel ID="panelContactGrid" runat="server">
                            <div>
                                <asp:GridView ID="moContactsGridView" runat="server" Width="100%" AutoGenerateColumns="False"
                                    OnRowCommand="moContactsGrid_RowCommand" AllowPaging="True" SkinID="DetailPageGridView"
                                    EnableViewState="true">
                                    <SelectedRowStyle Wrap="True" />
                                    <EditRowStyle Wrap="True" />
                                    <AlternatingRowStyle Wrap="True" />
                                    <RowStyle Wrap="True" />
                                    <HeaderStyle />
                                    <Columns>
                                        <asp:TemplateField Visible="false">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="ID" Visible="false" Text='<%#GetGuidStringFromByteArray(Container.DataItem("ID"))%>' />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField SortExpression="NAME" HeaderText="NAME" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="NameText" Visible="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField SortExpression="JOB_TITLE" HeaderText="JOB_TITLE" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="JobTitleText" Visible="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField SortExpression="COMPANY_NAME" HeaderText="COMPANY_NAME" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="CompanyNameText" Visible="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField SortExpression="EMAIL" HeaderText="EMAIL" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="EmailText" Visible="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField SortExpression="ADDRESS_TYPE" HeaderText="ADDRESS_TYPE" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="AddressTypeText" Visible="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField SortExpression="EFFECTIVE_DATE" HeaderText="EFFECTIVE_DATE" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="EffectiveDateText" Visible="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField SortExpression="EXPIRATION_DATE" HeaderText="EXPIRATION_DATE"
                                            ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:Label runat="server" ID="ExpirationDateText" Visible="true" />
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="AttributeEditButton_WRITE" Style="cursor: hand" runat="server"
                                                    ImageUrl="../App_Themes/Default/Images/edit.png" Visible="true" CommandName="EditRecord"
                                                    CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left">
                                            <ItemTemplate>
                                                <asp:ImageButton ID="AttributeDeleteButton_WRITE" Style="cursor: hand" runat="server"
                                                    ImageUrl="../App_Themes/Default/Images/icon_delete.png" Visible="true" CommandName="DeleteRecord"
                                                    CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerSettings PageButtonCount="10" Mode="Numeric" Position="TopAndBottom" />
                                    <PagerStyle />
                                </asp:GridView>
                                <table width="100%" cellpadding="0" cellspacing="0" class="tabBtnAreaZone">
                                    <tr>
                                        <td></td>
                                        <td align="left">
                                            <asp:Button ID="btnNewContactInfo" Text="ADD_NEW" runat="server" SkinID="PrimaryLeftButton" />
                                            <asp:Button ID="btnSearchContactInfo" runat="server" SkinID="PrimaryRightButton"
                                                Text="SEARCH" Visible="false" /><asp:LinkButton ID="LinkButton1" runat="server"></asp:LinkButton>
                                        </td>
                                    </tr>
                                </table>
                            </div>
                        </asp:Panel>
                    </div>
                    <!-- Tab end -->
                </asp:Panel>
            </div>
            <div id="tabsAttributes">
                <Elita:UserControlAttrtibutes runat="server" ID="AttributeValues" />
            </div>
            <div id="tabsQuantity">
                <asp:Panel ID="moQuantityTabPanel_WRITE" runat="server" Width="100%" Height="300px">
                    <div id="Div4" style="overflow: auto; width: 99.53%;" align="center">
                        <asp:GridView ID="moQuantityGridView" runat="server" Width="100%" AutoGenerateColumns="False"
                            OnRowCommand="moQuantityGrid_RowCommand" AllowPaging="True" SkinID="DetailPageGridView"
                            EnableViewState="true">
                            <SelectedRowStyle Wrap="True" />
                            <EditRowStyle Wrap="True" />
                            <AlternatingRowStyle Wrap="True" />
                            <RowStyle Wrap="True" />
                            <HeaderStyle />
                            <Columns>
                                <asp:TemplateField Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblQuantityID" Text='<%#GetGuidStringFromByteArray(Container.DataItem("ID"))%>'
                                            runat="server"> </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="True" HeaderText="EQUIPMENT_TYPE" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEquipmentType" Text='<%#Container.DataItem("EQUIPMENT_TYPE_ID")%>'
                                            runat="server"> </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="True" HeaderText="MANUFACTURER" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblManufacturer" Text='<%#Container.DataItem("MANUFACTURER_ID")%>'
                                            runat="server"></asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="True" HeaderText="MODEL" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblModel" Text='<%#Container.DataItem("JOB_MODEL")%>' runat="server"> </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="True" HeaderText="SKU_NUMBER" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSKU" Text='<%#Container.DataItem("VENDOR_SKU")%>' runat="server"> </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="True" HeaderText="SKU_DESCRIPTION" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSKUDesc" Text='<%#Container.DataItem("VENDOR_SKU_DESCRIPTION")%>' runat="server"> </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="True" HeaderText="QUANTITY" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblScore" Text='<%#Container.DataItem("QUANTITY")%>' runat="server"> </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtQuantity" runat="server" Visible="True" Text='<%#Container.DataItem("QUANTITY")%>'
                                            SkinID="SmallTextBox"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="True" HeaderText="PRICE" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblPrice" Text='<%#Container.DataItem("PRICE")%>' runat="server"> </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="True" HeaderText="CONDITION" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCondition" Text='<%#Container.DataItem("CONDITION_ID")%>' runat="server"> </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="True" HeaderText="EFFECTIVE_DATE" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEffective" Text='<%#Container.DataItem("EFFECTIVE")%>' runat="server"> </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="True" HeaderText="EXPIRATION_DATE" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblExpiration" Text='<%#Container.DataItem("EXPIRATION")%>' runat="server"> </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="QuantityEditButton_WRITE" Style="cursor: hand" runat="server"
                                            ImageUrl="../App_Themes/Default/Images/edit.png" Visible="true" CommandName="EditRecord"
                                            CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                        <asp:Button Style="cursor: hand;" ID="QuantitySaveButton_WRITE" runat="server" CommandName="QuantitySaveRecord"
                                            Text="Save" SkinID="PrimaryRightButton" CommandArgument="<%#Container.DisplayIndex %>"></asp:Button>
                                        <asp:LinkButton Style="cursor: hand;" ID="QuantityCancelButton_WRITE" runat="server"
                                            SkinID="AlternateRightButton" Text="Cancel" CommandName="QuantityCancelRecord"
                                            CommandArgument="<%#Container.DisplayIndex %>">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
                        </asp:GridView>
                    </div>
                    <!-- Tab end -->
                </asp:Panel>
            </div>
            <div id="tabsSchedule">
                <asp:Panel ID="moScheduleTabPanel_WRITE" runat="server" Width="100%" Height="300px"
                    ScrollBars="Horizontal">
                    <div id="divScheduleDetails" style="overflow: auto; width: 99.53%;" align="center"
                        runat="server" visible="True">
                        <asp:GridView ID="moAddScheduleGridView" runat="server" Width="100%" AutoGenerateColumns="False"
                            OnRowCommand="moAddScheduleGrid_RowCommand" AllowPaging="True" SkinID="DetailPageGridView"
                            EnableViewState="true">
                            <SelectedRowStyle Wrap="True" />
                            <EditRowStyle Wrap="True" />
                            <AlternatingRowStyle Wrap="True" />
                            <RowStyle Wrap="True" />
                            <HeaderStyle />
                            <Columns>
                                <asp:TemplateField Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblScheduleID" runat="server"> </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="True" HeaderText="SERVICE_CLASS" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblServiceClass" runat="server"> </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="cboServiceClass" runat="server" SkinID="SmallDropDown" Visible="True">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="True" HeaderText="SERVICE_TYPE" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCode" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="cboServiceType" runat="server" SkinID="SmallDropDown" Visible="True">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="True" HeaderText="SCHEDULE" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSchedule" runat="server"> </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="cboSchedule" runat="server" SkinID="SmallDropDown" Visible="True">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DAY" ItemStyle-HorizontalAlign="Left" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDay" runat="server"> </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="cboDayOfWeek" runat="server" SkinID="SmallDropDown" Visible="True">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FROM" ItemStyle-HorizontalAlign="Left" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFrom" runat="server"> </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txFrom" runat="server" SkinID="exSmallTextBox" Visible="True"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TO" ItemStyle-HorizontalAlign="Left" Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTo" runat="server"> </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtTo" runat="server" Visible="True" SkinID="exSmallTextBox"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="True" HeaderText="EFFECTIVE_DATE" ItemStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEffective" runat="server"> </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtEffective" runat="server" Visible="True" SkinID="exSmallTextBox"></asp:TextBox>
                                        <asp:ImageButton ID="btnEffectiveDate" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png"></asp:ImageButton>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="True" HeaderText="EXPIRATION_DATE" ItemStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblExpiration" runat="server"> </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtExpiration" runat="server" Visible="True" SkinID="exSmallTextBox"></asp:TextBox><asp:ImageButton
                                            ID="btnExpirationDate" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png"></asp:ImageButton>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ScheduleEditButton_WRITE" Style="cursor: hand" runat="server"
                                            ImageUrl="../App_Themes/Default/Images/edit.png" Visible="true" CommandName="EditRecord"
                                            CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                        <asp:Button Style="cursor: hand;" ID="ScheduleSaveButton_WRITE" runat="server" CommandName="ScheduleSaveRecord"
                                            Text="Save" SkinID="PrimaryRightButton" CommandArgument="<%#Container.DisplayIndex %>"></asp:Button>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:LinkButton Style="cursor: hand;" ID="ScheduleCancelButton_WRITE" runat="server"
                                            Text="Cancel" SkinID="AlternateRightButton" CommandName="ScheduleCancelRecord"
                                            CommandArgument="<%#Container.DisplayIndex %>">
                                        </asp:LinkButton>
                                        <asp:ImageButton ID="ScheduleDeleteButton_WRITE" Style="cursor: hand" runat="server"
                                            ImageUrl="../App_Themes/Default/Images/icon_delete.png" Visible="true" CommandName="DeleteRecord" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
                        </asp:GridView>
                        <table width="100%" cellpadding="0" cellspacing="0" class="tabBtnAreaZone">
                            <tr>
                                <td></td>
                                <td align="left">
                                    <asp:Button ID="btnAddNewSchedule" runat="server" Text="ADD_NEW" SkinID="PrimaryLeftButton" />
                                    <asp:Button ID="btnCancelNewSchedule" Text="Done" runat="server" CssClass="PrimaryRightButton" Visible="false" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div id="divAddNewSchedule" style="overflow: auto; width: 99.53%;" align="center"
                        runat="server" visible="false">
                        <asp:GridView ID="moScheduleGridView" runat="server" Width="100%" AutoGenerateColumns="False"
                            AllowPaging="True" SkinID="DetailPageGridView" EnableViewState="true">
                            <SelectedRowStyle Wrap="True" />
                            <EditRowStyle Wrap="True" />
                            <AlternatingRowStyle Wrap="True" />
                            <RowStyle Wrap="True" />
                            <HeaderStyle />
                            <Columns>
                                <asp:TemplateField Visible="False">
                                    <ItemTemplate>
                                        <asp:Label ID="lblScheduleID" runat="server"> </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="True" HeaderText="SERVICE_CLASS" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblServiceClass" runat="server"> </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="cboServiceClass" runat="server" SkinID="SmallDropDown" Visible="True">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="True" HeaderText="SERVICE_TYPE" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblCode" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="cboServiceType" runat="server" SkinID="SmallDropDown" Visible="True">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="True" HeaderText="SCHEDULE" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblSchedule" runat="server"> </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="cboSchedule" runat="server" SkinID="SmallDropDown" Visible="True">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="DAY" ItemStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblDay" runat="server"> </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:DropDownList ID="cboDayOfWeek" runat="server" SkinID="SmallDropDown" Visible="True">
                                        </asp:DropDownList>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="FROM" ItemStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblFrom" runat="server"> </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txFrom" runat="server" SkinID="exSmallTextBox" Visible="True"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField HeaderText="TO" ItemStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemTemplate>
                                        <asp:Label ID="lblTo" runat="server"> </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtTo" runat="server" Visible="True" SkinID="exSmallTextBox"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="True" HeaderText="EFFECTIVE_DATE" ItemStyle-Wrap="true"
                                    ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblEffective" runat="server"> </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtEffective" runat="server" Visible="True" SkinID="exSmallTextBox"></asp:TextBox>
                                        <asp:ImageButton ID="btnEffectiveDate" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png"></asp:ImageButton>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="True" HeaderText="EXPIRATION_DATE" ItemStyle-Wrap="false"
                                    ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblExpiration" runat="server"> </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtExpiration" runat="server" Visible="True" SkinID="exSmallTextBox"></asp:TextBox><asp:ImageButton
                                            ID="btnExpirationDate" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png"></asp:ImageButton>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="ScheduleEditButton_WRITE" Style="cursor: hand" runat="server"
                                            ImageUrl="../App_Themes/Default/Images/edit.png" Visible="true" CommandName="EditRecord"
                                            CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                        <asp:Button Style="cursor: hand;" ID="ScheduleSaveButton_WRITE" runat="server" CommandName="ScheduleSaveRecord"
                                            Text="Save" SkinID="PrimaryRightButton" CommandArgument="<%#Container.DisplayIndex %>"></asp:Button>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left" Visible="false">
                                    <ItemTemplate>
                                        <asp:LinkButton Style="cursor: hand;" ID="ScheduleCancelButton_WRITE" runat="server"
                                            Text="Cancel" SkinID="AlternateRightButton" CommandName="ScheduleCancelRecord"
                                            CommandArgument="<%#Container.DisplayIndex %>">
                                        </asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
                        </asp:GridView>
                        <table width="100%" cellpadding="0" cellspacing="0" class="tabBtnAreaZone">
                            <tr>
                                <td></td>
                                <td align="left">
                                    <asp:Button ID="btnNewSchedule" Text="ADD_NEW" runat="server" SkinID="PrimaryLeftButton" />
                                </td>
                            </tr>
                        </table>
                    </div>
                    <!-- Tab end -->
                </asp:Panel>
            </div>
            <div id="tabsPriceList">
                <hr style="height: 1px">
                <table width="100%">
                    <tr id="trPageSize" runat="server">
                        <td valign="top" align="left">
                            <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>: &nbsp;
													<asp:DropDownList ID="cboPageSize" runat="server" Width="50px" AutoPostBack="true">
                                                        <asp:ListItem Value="5">5</asp:ListItem>
                                                        <asp:ListItem Selected="True" Value="10">10</asp:ListItem>
                                                        <asp:ListItem Value="15">15</asp:ListItem>
                                                        <asp:ListItem Value="20">20</asp:ListItem>
                                                        <asp:ListItem Value="25">25</asp:ListItem>
                                                        <asp:ListItem Value="30">30</asp:ListItem>
                                                        <asp:ListItem Value="35">35</asp:ListItem>
                                                        <asp:ListItem Value="40">40</asp:ListItem>
                                                        <asp:ListItem Value="45">45</asp:ListItem>
                                                        <asp:ListItem Value="50">50</asp:ListItem>
                                                    </asp:DropDownList></td>
                        <td style="height: 22px" align="right">
                            <asp:Label ID="lblRecordCount" runat="server"></asp:Label></td>
                    </tr>
                    <%--<tr>
                        <td colspan="2" style="vertical-align: top" id="tdGrid">
                            <asp:GridView ID="Grid" runat="server" Width="100%" OnRowCreated="ItemCreated" OnRowCommand="ItemCommand"
                                AllowPaging="True" AllowSorting="True" CellPadding="1" AutoGenerateColumns="False"
                                BorderColor="#999999" BackColor="#DEE3E7" BorderWidth="1px" BorderStyle="Solid">
                                <SelectedRowStyle CssClass="SELECTED" Wrap="false" BorderStyle="Solid" BorderColor="Orange" BackColor="LightSteelBlue"></SelectedRowStyle>
                                <AlternatingRowStyle Wrap="False" BackColor="#F1F1F1"></AlternatingRowStyle>
                                <RowStyle Wrap="false" BackColor="White"></RowStyle>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <Columns>
                                    <asp:TemplateField Visible="true" ShowHeader="false">
                                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="5%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Width="3%" Height="15px" />
                                        <ItemTemplate>
                                            <asp:ImageButton Style="cursor: hand;" ID="SelectButton_WRITE" runat="server" CausesValidation="False" CommandName="SelectRecord"
                                                ImageUrl="../Navigation/images/icons/yes_icon.gif" CommandArgument="<%#Container.DisplayIndex %>" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Price List Recon Id">
                                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="20%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblColPriceListReconId" runat="server" Visible="True">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Price Desc">
                                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="20%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" Width="24%"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblColPriceListId" runat="server" Visible="True">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Requested By">
                                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="20%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" Width="24%"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblColRequestedBy" runat="server">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Status">
                                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="20%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" Width="24%"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblColStatusXcd" runat="server" Visible="True">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Approved By">
                                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="20%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" Width="12%"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblColApprovedBy" runat="server">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Requested Date">
                                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="20%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left" Width="12%"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblColRequestedDate" runat="server">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                                <PagerSettings PageButtonCount="15" Mode="Numeric" />
                                <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
                            </asp:GridView>
                        </td>
                    </tr>--%>
                    <tr>
                        <td valign="top" align="center" colspan="2" height="100%">
                            <asp:DataGrid ID="DataGridPriceList" runat="server" Width="100%" AllowPaging="True" CellPadding="1" BorderColor="#999999"
                                BackColor="#DEE3E7" BorderWidth="1px" BorderStyle="Solid" AutoGenerateColumns="False" OnItemCommand="ItemCommand"
                                OnItemCreated="ItemCreated" Height="100%">
                                <SelectedItemStyle Wrap="False" BorderStyle="Solid" BorderColor="Orange" BackColor="LightSteelBlue"></SelectedItemStyle>
                                <EditItemStyle Wrap="False"></EditItemStyle>
                                <AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
                                <ItemStyle Wrap="False" BackColor="White"></ItemStyle>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <Columns>
                                    <asp:TemplateColumn>
                                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="5%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:ImageButton Style="cursor: hand;" ID="EditButton_WRITE" ImageUrl="../Navigation/images/icons/yes_icon.gif"
                                                runat="server" CommandName="SelectAction"></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn Visible="False" HeaderText="svc_price_list_recon_id">
                                        <HeaderStyle Width="5px"></HeaderStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn SortExpression="PRICE_DESC" HeaderText="Price Desc">
                                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="20%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn SortExpression="REQUESTED_BY" HeaderText="Requested By">
                                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="30px"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn SortExpression="STATUS" HeaderText="Status">
                                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="12%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn SortExpression="APPROVED_BY" HeaderText="Approved By">
                                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="15%"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    </asp:BoundColumn>
                                    <asp:BoundColumn SortExpression="REQUESTED_DATE" HeaderText="Requested Date">
                                        <HeaderStyle Width="15%"></HeaderStyle>
                                    </asp:BoundColumn>
                                </Columns>
                                <PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7" Mode="NumericPages"></PagerStyle>
                            </asp:DataGrid>
                        </td>
                    </tr>
                </table>
                <input id="HiddenDeletePromptResponse" type="hidden" runat="server" designtimedragdrop="261">

                <script type="text/javascript">
                    if (document.getElementById("tdGrid")) {
                        document.getElementById("tdGrid").style.height = parent.document.getElementById("Navigation_Content").clientHeight - 350;
                    }
                </script>

                <div>
                    <table align="center" border="0" class="dataGrid" rules="cols" width="98%">
                        <tr>
                            <td align="left">
                                <asp:Button ID="btnSubmitApproval" runat="server" Width="200px" Text="Submit For Approval" SkinID="AlternateLeftButton"></asp:Button>
                                &nbsp;
                                <asp:Button ID="btnApprove" runat="server" Width="100px" Text="Approve" SkinID="AlternateLeftButton"></asp:Button>
                                &nbsp;
                            <asp:Button ID="btnReject" runat="server" Width="100px" Text="Reject" SkinID="AlternateLeftButton"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </div>
            </div>
        </div>
        <%--END   DEF-2818--%>
    </div>
    <div class="btnZone">
        <asp:Button ID="btnSave_WRITE" runat="server" TabIndex="43" Text="Save" SkinID="PrimaryRightButton"></asp:Button>
        <asp:Button ID="btnUndo_Write" runat="server" TabIndex="44" Text="Undo" SkinID="AlternateRightButton"></asp:Button>
        <asp:Button ID="btnCopy_WRITE" runat="server" TabIndex="46" Text="NEW_WITH_COPY"
            CausesValidation="False" SkinID="AlternateRightButton"></asp:Button>
        <asp:Button ID="btnBack" runat="server" Text="Back" TabIndex="42" SkinID="AlternateLeftButton"></asp:Button>
        <asp:Button ID="btnNew_WRITE" runat="server" TabIndex="45" Text="New" SkinID="AlternateLeftButton"></asp:Button>
        <asp:Button ID="btnDelete_WRITE" runat="server" TabIndex="47" Text="Delete" SkinID="CenterButton"></asp:Button>
    </div>
    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
        runat="server" designtimedragdrop="261" />
    <div id="AddNewContainer">
        <ajaxToolkit:ModalPopupExtender runat="server" ID="mdlPopup" PopupControlID="pnlPopup"
            DropShadow="True" BackgroundCssClass="ModalBackground" TargetControlID="LinkButton1"
            BehaviorID="addNewModal" PopupDragHandleControlID="pnlPopup" RepositionMode="RepositionOnWindowScroll">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlPopup" runat="server" CssClass="modalpopup" Style="display: none; width: 800px;">
            <div id="light" class="overlay_message_content" style="overflow: hidden;">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                    <ContentTemplate>
                        <p class="modalTitle">
                            <asp:Label ID="lblModalTitle" runat="server" Text="ADD_NEW_VENDOR_CONTACT_DETAILS"></asp:Label>
                            <asp:ImageButton ImageUrl="~/App_Themes/Default/Images/icon_modalClose.png" runat="server"
                                ID="modalClose" CssClass="floatR" AlternateText="Close" OnClientClick="$find('addNewModal').hide(); return false;" />
                        </p>
                        <table>
                            <tbody>
                                <uc1:UserControlContactInfo ID="moUserControlContactInfo" runat="server"></uc1:UserControlContactInfo>
                            </tbody>
                        </table>
                        <table border="0"></table>
                        <table width="100%" border="0" class="formGrid" cellpadding="0" cellspacing="0">
                            <tr valign="middle">
                                <td style="text-align: center;" align="right">
                                    <span class="mandatory">*</span>
                                    <asp:Label ID="lblEffectiveDate" runat="server" Text="EFFECTIVE_DATE"></asp:Label>:
                                </td>
                                <td align="right">
                                    <asp:TextBox ID="txtEffective" runat="server" SkinID="SmallTextBox" />
                                </td>
                                <td align="left">
                                    <asp:ImageButton ID="ibtnEffective" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png"
                                        valign="bottom" />
                                </td>
                                <td align="right">
                                    <span class="mandatory">*</span><asp:Label ID="lblExpirationDate" runat="server"
                                        Text="EXPIRATION_DATE"></asp:Label>:
                                </td>
                                <td align="right">
                                    <asp:TextBox ID="txtExpiration" runat="server" SkinID="SmallTextBox" />
                                </td>
                                <td align="left">
                                    <asp:ImageButton ID="ibtnExpiration" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png"
                                        valign="bottom" />
                                </td>
                            </tr>
                        </table>
                    </ContentTemplate>
                    <Triggers>
                    </Triggers>
                </asp:UpdatePanel>
                <table width="100%" border="0" class="formGrid" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="4">&nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnNewItemSave" runat="server" CssClass="primaryBtn floatR" Text="SAVE" />
                            <asp:Button ID="btnNewItemCancel" runat="server" CssClass="popWindowCancelbtn floatR"
                                Text="CANCEL" />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
    </div>
</asp:Content>
<%-- CancelControlID="btnNewItemCancel"--%>
