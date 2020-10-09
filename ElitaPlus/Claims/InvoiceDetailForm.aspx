 <%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/ElitaBase.Master"
    CodeBehind="InvoiceDetailForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.InvoiceDetailForm"
    Theme="Default" %>

<%@ Register TagPrefix="Elita" TagName="MessageController" Src="~/Common/MessageController.ascx" %>
 <%@ Import Namespace="System.Globalization" %>
 <%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="Ajax" %>
<%@ Register TagPrefix="Microsoft" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<asp:Content ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="javascript" type="text/javascript">   
        (function ($) {
            
            $(document).ready(function () {
                $('#overlay').hide();
                $('#aspnetForm').submit(function () {
                $('#overlay').show();
                });
                
            });
        })(jQuery);
       
            
            
        window.latestClick = '';
        function isNotDblClick() {
            if (window.latestClick != "clicked") {
                window.latestClick = "clicked";
                return true;
            } else {
                return false;
            }
        }        
    </script>
    <style type="text/css">
        #overlay {
            position: fixed;
            
            width: 100%;
            height: 100%;
            top: 0;
            left: 0;
            right: 0;
            bottom: 0;
            background-color: rgba(0,0,0,0.5);
            z-index: 2;
            cursor: pointer;
            margin: 0 auto;
        }

        #loader {
            position: absolute;
             top: 50%;
            left: 49%;
        }
</style>
</asp:Content>
<asp:Content ContentPlaceHolderID="MessagePlaceHolder" runat="server">
    <Elita:MessageController runat="server" ID="moInfoMessageController" />
</asp:Content>
<asp:Content ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="BodyPlaceHolder" runat="server">
     <div id="overlay">
        <img id="loader" src="../App_Themes/Default/Images/ajax-loader.gif"  />
    </div>
    <asp:ScriptManager ID="moScriptManager" runat="server" EnablePageMethods="true" ScriptMode="Auto">
        <Scripts>
        </Scripts>
    </asp:ScriptManager>
    <div class="dataContainer">
        <div class="stepformZone">
            <table width="100%" class="formGrid" border="0" cellspacing="0" cellpadding="0">
                <tbody>
                    <tr>
                        <td align="right" nowrap="noWrap">
                            <asp:Label runat="server" ID="moInvoiceNumberLabel" Text="INVOICE_NUMBER" />
                        </td>
                        <td nowrap="noWrap">
                            <asp:TextBox runat="server" ID="moInvoiceNumber" SkinID="MediumTextBox" />
                        </td>
                        <td align="right" nowrap="noWrap">
                            <asp:Label runat="server" ID="moInvoiceDateLabel" Text="INVOICE_DATE" />
                        </td>
                        <td nowrap="noWrap">
                            <asp:TextBox ID="moInvoiceDate" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                            <asp:ImageButton ID="btnInvoiceDate" runat="server" Style="vertical-align: bottom"
                                ImageUrl="~/App_Themes/Default/Images/calendar.png" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="noWrap">
                            <asp:Label runat="server" ID="moServiceCenterLabel" Text="SERVICE CENTER" />
                        </td>
                        <td nowrap="noWrap">
                            <input id="inputServiceCenterId" type="hidden" name="inputServiceCenterId" runat="server" />
                            <asp:TextBox runat="server" ID="moServiceCenter" SkinID="MediumTextBox" />
                            <Ajax:AutoCompleteExtender ID="aCompServiceCenter" runat="server" TargetControlID="moServiceCenter"
                                 OnClientItemSelected="comboSelectedServiceCenter" ServiceMethod="PopulateServiceCenterDrop"
                                MinimumPrefixLength='1' CompletionListCssClass="completionList" CompletionListHighlightedItemCssClass="itemHighlighted"
                                CompletionListItemCssClass="listItem">
                            </Ajax:AutoCompleteExtender>
                        </td>
                        <td align="right" nowrap="noWrap">
                            <asp:Label runat="server" ID="moDueDateLabel" Text="DUE_DATE" />
                        </td>
                        <td nowrap="noWrap">
                            <asp:TextBox ID="moDueDate" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                            <asp:ImageButton ID="btnDueDate" runat="server" Style="vertical-align: bottom" ImageUrl="~/App_Themes/Default/Images/calendar.png" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="noWrap">
                            <asp:Label runat="server" ID="moInvoiceStatusLabel" Text="INVOICE_STATUS" />
                        </td>
                        <td nowrap="noWrap">
                            <asp:TextBox runat="server" ID="moInvoiceStatus" Font-Bold="true" SkinID="MediumTextBox"
                                ReadOnly="true" />
                        </td>
                        <td align="right" nowrap="noWrap">
                            <asp:Label runat="server" ID="moInvoiceAmountLabel" Text="INVOICE_AMOUNT" />
                        </td>
                        <td nowrap="noWrap">
                            <asp:TextBox runat="server" ID="moInvoiceAmount" SkinID="SmallTextBox" onchange="UpdateInvoice()" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="noWrap">
                            <asp:Label runat="server" ID="moRepairDateLabel" Text="REPAIR_DATE" />:
                        </td>
                        <td nowrap="noWrap">
                            <asp:TextBox ID="moRepairDate" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                            <asp:ImageButton ID="btnRepairDate" runat="server" Style="vertical-align: bottom"
                                ImageUrl="~/App_Themes/Default/Images/calendar.png" />
                            <asp:Button runat="server" ID="btnAddRepairDate" Text="ADD_REPAIR_DATE" SkinID="AlternateLeftButton" />
                        </td>
                        <td align="right" nowrap="noWrap">
                            <asp:Label runat="server" ID="moDifferenceAmountLabel" Text="DIFFERENCE" />
                        </td>
                        <td nowrap="noWrap">
                            <asp:TextBox runat="server" ID="moDifferenceAmount" SkinID="SmallTextBox" ReadOnly="true" />
                        </td>
                    </tr>
                    <tr>
                        <td align="right" nowrap="noWrap">
                            <asp:Label runat="server" ID="moSourceLabel" Text="SOURCE" />
                        </td>
                        <td nowrap="noWrap">
                            <asp:TextBox runat="server" ID="moSource" SkinID="MediumTextBox" ReadOnly="true" />
                        </td>
                        <td align="right" nowrap="noWrap">
                            <asp:Label runat="server" ID="moIsCompleteLabel" Text="COMPLETE" />
                        </td>
                        <td nowrap="noWrap">
                            <asp:TextBox runat="server" ID="moIsComplete" SkinID="SmallTextBox" ReadOnly="true" />
                        </td>
                        <td nowrap="nowrap">
                            <asp:Button ID="mosearch" runat="server" SkinID="SearchButton" Text="Search"></asp:Button>
                        </td>
                    </tr>
                    <tr runat="server" id="trInvoiceTaxes1">
                        <td align="right" nowrap="noWrap">
                            <asp:Label runat="server" ID="moBatchNumberLabel" Text="BATCH_NUMBER" />
                        </td>
                        <td nowrap="noWrap">
                            <asp:TextBox runat="server" ID="moBatchNumber" SkinID="MediumTextBox" AutoPostBack="true" />
                        </td>
                        <td align="right" nowrap="noWrap">
                            <asp:Label runat="server" ID="moPerceptionIvaLabel" Text="PERCEPTION_IVA" />
                        </td>
                        <td nowrap="noWrap">
                            <asp:TextBox runat="server" ID="moPerceptionIva" SkinID="SmallTextBox" onchange="UpdateInvoice()" />
                        </td>
                    </tr>
                    <tr runat="server" id="trInvoiceTaxes2">
                        <td align="right" nowrap="noWrap">
                            <asp:Label runat="server" ID="moPerceptionIibbProvinceLabel" Text="PERCEPTION_IIBB_PROVINCE" />
                        </td>
                        <td nowrap="noWrap">
                            <asp:DropDownList runat="server" ID="moPerceptionIibbProvince" SkinID="MediumDropDown">
                            </asp:DropDownList>
                        </td>
                        <td align="right" nowrap="noWrap">
                            <asp:Label runat="server" ID="moPerceptionIibbLabel" Text="PERCEPTION_IIBB" />
                        </td>
                        <td nowrap="noWrap">
                            <asp:TextBox runat="server" ID="moPerceptionIibb" SkinID="SmallTextBox" onchange="UpdateInvoice()" />
                        </td>
                    </tr>
                </tbody>
            </table>
        </div>
    </div>
    <div class="dataContainer">
        <div class="Page" runat="server" id="moLineItems" style="height: 300px; overflow: auto">
            <table width="100%" class="dataGrid">
                <tbody>
                    <asp:Repeater runat="server" ID="moInvoiceRepeater">
                        <HeaderTemplate>
                            <tr>
                                <th>
                                </th>
                                <th align="center">
                                    <asp:CheckBox runat="server" ID="moSelectAll" onclick="moSelectAllClick(this);" />
                                </th>
                                <th>
                                    <asp:LinkButton runat="server" ID="moClaimNumberSort" Text="CLAIM_NUMBER" />
                                </th>
                                <th>
                                    <asp:LinkButton runat="server" ID="moAuthorizationNumberSort" Text="AUTHORIZATION_NUMBER" />
                                </th>
                                <th>
                                    <asp:LinkButton runat="server" ID="moBatchNumberSort" Text="BATCH_NUMBER" />
                                </th>
                                <th>
                                    <asp:LinkButton runat="server" ID="moVerificationNumberSort" Text="VERIFICATION_NUMBER" />
                                </th>
                                <th>
                                    <asp:LinkButton runat="server" ID="moCustomerNameSort" Text="CUSTOMER_NAME" />
                                </th>
                                <th>
                                    <asp:LinkButton runat="server" ID="moReserveAmountSort" Text="RESERVE_AMOUNT" />
                                </th>
                                <th>
                                    <asp:LinkButton runat="server" ID="moDeductibleSort" Text="DEDUCTIBLE" />
                                </th>
                                <th>
                                    <asp:LinkButton runat="server" ID="moInvoiceAuthorizationAmountSort" Text="AMOUNT" />
                                </th>
                                <th style="width: 100px">
                                    <asp:LinkButton runat="server" ID="moRepairDateSort" Text="REPAIR DATE" />
                                </th>
                                <th style="width: 100px">
                                    <asp:LinkButton runat="server" ID="moPickupDateSort" Text="PICKUP_DATE" />
                                </th>
                                <th>
                                    &nbsp;
                                </th>
                            </tr>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <tr onmouseover="this.className='over'" onmouseout="this.className='out'">
                                <td rowspan="1" align="center">
                                    <asp:Label runat="server" ID="moExpandCollapse" Style="cursor: pointer"></asp:Label>
                                </td>
                                <td align="center">
                                    <asp:CheckBox ID="moSelect" runat="server" />
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="moClaimNumber" />
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="moAuthorizationNumber" />
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="moBatchNumber" />
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="moVerificationNumber" />
                                </td>
                                <td>
                                    <asp:Label runat="server" ID="moCustomerName" />
                                </td>
                                <td align="right">
                                    <asp:Label runat="server" ID="moReserveAmount" />
                                </td>
                                <td align="right">
                                    <asp:Label runat="server" ID="moDeductible" />
                                </td>
                                <td align="right">
                                    <asp:Label runat="server" ID="moInvoiceAuthorizationAmount" />
                                </td>
                                <td style="white-space: nowrap">
                                    <asp:TextBox runat="server" ID="moRepairDate" SkinID="exSmallTextBox" /><asp:ImageButton
                                        ID="btnRepairDate" runat="server" Style="vertical-align: bottom" ImageUrl="~/App_Themes/Default/Images/calendar.png" />
                                </td>
                                <td style="white-space: nowrap">
                                    <asp:TextBox runat="server" ID="moPickupDate" SkinID="exSmallTextBox" /><asp:ImageButton
                                        ID="btnPickupDate" runat="server" Style="vertical-align: bottom" ImageUrl="~/App_Themes/Default/Images/calendar.png" />
                                </td>
                                <td align="left">
                                    <asp:LinkButton runat="server" ID="moAddInvoiceLineItems">
                                        <asp:Image ID="btnAddLineItem" runat="server" Style="vertical-align: bottom" ImageUrl="~/App_Themes/Default/Images/icon_add.png" />
                                    </asp:LinkButton>
                                </td>
                            </tr>
                            <tr id='<%# "Child" & Container.ItemIndex %>' style="display: none">
                                <td colspan="12" id='<%# "Cell" & Container.ItemIndex %>'>
                                </td>
                            </tr>
                        </ItemTemplate>
                    </asp:Repeater>
                </tbody>
            </table>
        </div>
    </div>
    <div class="btnZone">
        <asp:Button runat="server" ID="btnBack" Text="Back" SkinID="AlternateLeftButton" />
        <asp:Button runat="server" ID="btnSave_WRITE" Text="Save" SkinID="PrimaryRightButton" />
        <asp:Button runat="server" ID="btnUndoBalance_WRITE" Text="UNBALANCE" SkinID="AlternateRightButton" />
        <asp:Button runat="server" ID="btnBalance_WRITE" Text="BALANCE" SkinID="AlternateRightButton" />
        <asp:Button runat="server" ID="btnNew_WRITE" Text="New" SkinID="AlternateRightButton" />
        <asp:Button runat="server" ID="btnUndo_Write" Text="Undo" SkinID="AlternateRightButton" />
        <asp:Button runat="server" ID="btnDelete_WRITE" Text="Delete" SkinID="CenterButton" />
    </div>
    <ajaxToolkit:ModalPopupExtender runat="server" ID="mdlPopup" DropShadow="true" BackgroundCssClass="ModalBackground"
        BehaviorID="addNewInvoiceItem" PopupDragHandleControlID="BodyPlaceHolder" RepositionMode="None"
        TargetControlID="btnClearSearch" CancelControlID="btnClearSearch" PopupControlID="pnlPopup">
    </ajaxToolkit:ModalPopupExtender>
    <asp:Panel ID="pnlPopup" runat="server">
        <div id="lightRedirect" class="overlay_message_content" style="width: 1000px; height: 600px">
            <p class="modalTitle">
                <asp:Label ID="lblModalTitle" runat="server" Text="ADD_NEW_INVOICE_ITEM"></asp:Label>                
            </p>
            <input type="hidden" id="AuthorizationId" />
            <input type="hidden" id="InvoiceAuthorizationAmountId" />
            <input type="hidden" id="senderId" />
            <input type="hidden" id="index" />
            <div class="dataContainer" id="PriceListPopupSearchInfoBox" style="margin-bottom: 0px;
                display: none">
                <div class="infoMsg">
                    <p>
                        <img width="16" height="13" align="middle" src="../App_Themes/Default/Images/dialogue_inform.png"
                            alt="info" />
                        <span id="PriceListPopupSearchInfo"></span>
                    </p>
                </div>
            </div>
            <table width="100%" border="0" cellpadding="0" cellspacing="0" class="dataGrid">
                <tr>
                    <td align="left">
                        <asp:Label ID="moSkuLabel" runat="server">VENDOR_SKU</asp:Label>:<br />
                        <asp:TextBox runat="server" ID="moSku" SkinID="MediumTextBox" />
                    </td>
                    <td align="left">
                        <asp:Label ID="moSkuDescriptionLabel" runat="server">VENDOR_SKU_DESCRIPTION</asp:Label>:<br />
                        <asp:TextBox runat="server" ID="moSkuDescription" SkinID="MediumTextBox" />
                    </td>
                    <td align="left">
                        <asp:Label ID="moServiceClassLabel" runat="server">SERVICE_CLASS</asp:Label>:<br />
                        <asp:DropDownList runat="server" SkinID="MediumDropDown" ID="moServiceClass" />
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="moServiceTypeLabel" runat="server">SERVICE_TYPE</asp:Label>:<br />
                        <asp:DropDownList runat="server" SkinID="MediumDropDown" ID="moServiceType" />
                    </td>
                    <td align="left">
                        <asp:Label ID="moRiskTypeLabel" runat="server">RISK_TYPE</asp:Label>:<br />
                        <asp:DropDownList runat="server" SkinID="MediumDropDown" ID="moRiskType" />
                    </td>
                    <td align="left">
                        <asp:Label ID="moEquipmentClassLabel" runat="server">EQUIPMENT_CLASS</asp:Label>:<br />
                        <asp:DropDownList runat="server" SkinID="MediumDropDown" ID="moEquipmentClass" />
                    </td>
                </tr>
                <tr>
                    <td align="left">
                        <asp:Label ID="moEquipmentLabel" runat="server">EQUIPMENT</asp:Label>:<br />
                        <asp:DropDownList runat="server" SkinID="MediumDropDown" ID="moEquipment" />
                    </td>
                    <td align="left">
                        <asp:Label ID="moConditionLabel" runat="server">CONDITION</asp:Label>:<br />
                        <asp:DropDownList runat="server" SkinID="MediumDropDown" ID="moCondition" />
                    </td>
                    <td id="Td1" align="right" runat="server">
                        <asp:Button ID="btnCancelSearch" runat="server" SkinID="AlternateRightButton" Text="Cancel"
                            OnClientClick="javascript:cancelAddInvoiceItemSearch()"></asp:Button>
                        <asp:Button ID="btnClearSearch" runat="server" SkinID="AlternateRightButton" Text="Clear"
                            OnClientClick="javascript:clearAddInvoiceItemSearch()"></asp:Button>
                        <asp:Button ID="btnSearch" runat="server" SkinID="PrimaryRightButton" Text="Search"
                            OnClientClick="javascript:searchAddInvoiceItems()"></asp:Button>
                    </td>
                </tr>
            </table>
            <input type="hidden" id="selectedPage" />
            <div id="dvInvoicePriceListResults" style="overflow: auto;">
            </div>
        </div>
    </asp:Panel>
    <script type="text/javascript" language="javascript">
        var decSep = '<%=CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator%>';
        var groupSep = '<%=CultureInfo.CurrentCulture.NumberFormat.CurrencyGroupSeparator%>';
        var differenceAmountId = '<%= moDifferenceAmount.ClientId %>';
        var invoiceAmountId = '<%=moInvoiceAmount.ClientId%>';
        var perceptionIvaId = '<%=moPerceptionIva.ClientId%>';
        var perceptionIibbId = '<%=moPerceptionIibb.ClientId%>';
        var selectAllId = '<%=SelectAllCheckBoxId%>';

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

        function IncludeExcludeAuthorization(sender, index, claimAuthorizationId, expandId, repairDateId, repairDateImageId, pickupDateId, pickupDateImageId, moAddInvoiceLineItemsId) {
            if (sender.checked) {
                $('#overlay').show()
                $.ajax({
                    type: "POST",
                    url: "InvoiceDetailForm.aspx/IncludeAuthorization",
                    data: '{ claimAuthorizationId: "' + claimAuthorizationId + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        $('#overlay').hide()
                        UpdateDifferenceAmount(msg.d.DifferenceAmount);
                        BatchNumberReadOnly(msg.d.BatchNumberReadOnly);
                        $('#' + expandId).text('+');
                        $('#' + expandId).removeAttr('display');
                        $('#' + repairDateId).removeAttr('readOnly');
                        $('#' + repairDateImageId)[0].style.display = 'block';
                        $('#' + pickupDateId).removeAttr('readOnly');
                        $('#' + pickupDateImageId)[0].style.display = 'block';
                        $('#' + moAddInvoiceLineItemsId)[0].style.display = 'block';
                        $('#' + selectAllId)[0].checked = msg.d.SelectAllChecked;
                    }
                });
            }
            else {
                $('#overlay').show()
                $.ajax({
                    type: "POST",
                    url: "InvoiceDetailForm.aspx/ExcludeAuthorization",
                    data: '{ claimAuthorizationId: "' + claimAuthorizationId + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        $('#overlay').hide()
                        UpdateDifferenceAmount(msg.d.DifferenceAmount);
                        BatchNumberReadOnly(msg.d.BatchNumberReadOnly);
                        $('#' + expandId).text('');
                        $('#' + expandId).attr('display', 'none');
                        $('#Child' + index).hide();
                        $('#' + expandId).parent().removeAttr('RowSpan');
                        $('#' + repairDateId).attr('readOnly', 'readOnly');
                        $('#' + repairDateImageId)[0].style.display = 'none';
                        $('#' + pickupDateId).attr('readOnly', 'readOnly');
                        $('#' + pickupDateImageId)[0].style.display = 'none';
                        $('#' + moAddInvoiceLineItemsId)[0].style.display = 'none';
                        $('#' + selectAllId)[0].checked = msg.d.SelectAllChecked;
                    }
                });
            }
            $('#' + sender.id).parent().removeAttr('RowSpan');
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

        function ShowHideInvoiceAuthorizationDetails(sender, index, claimAuthorizationId, invoiceAuthorizationAmountId) {
            if ($('#' + sender.id).text() == '+') {
                $('#overlay').show()
                $.ajax({
                    type: "POST",
                    url: "InvoiceDetailForm.aspx/GetInvoiceAuthorizationDetails",
                    data: '{ claimAuthorizationId: "' + claimAuthorizationId + '", senderId: "' + sender.id + '", index: "' + index + '",invoiceAuthorizationAmountId: "' + invoiceAuthorizationAmountId + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        $('#overlay').hide()
                        xml = GetXmlDoc(msg.d);
                        xsl = LoadXmlDoc("InvoiceAuthorizationItems.xslt");

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

                        $('#Child' + index).show();
                        $('#' + sender.id).text('-');
                        $('#' + sender.id).parent().attr('RowSpan', '2');
                    }
                });
            }
            else {
                
                if ($('#' + sender.id).text() == '-') {
                    $('#overlay').display = "block";
                    $('#Child' + index).hide();
                    $('#' + sender.id).text('+');
                    $('#' + sender.id).parent().removeAttr('RowSpan');
                }
            }
        }

        function comboSelectedServiceCenter(source, eventArgs) {
            var inpId = document.getElementById('<%=inputServiceCenterId.ClientID%>');
            if (inpId.value != eventArgs.get_value()) {
                inpId.value = eventArgs.get_value();
               <%-- __doPostBack('', '<%=SERVICE_CENTER_CHANGE_EVENT_NAME%>');--%>
            }
        }

        function showAddInvoiceItemSearch(authorizationId, senderId, index, invoiceAuthorizationAmountId) {
            $('#AuthorizationId').val(authorizationId);
            $('#senderId').val(senderId);
            $('#index').val(index);
            $('#dvInvoicePriceListResults').html('');
            $('#InvoiceAuthorizationAmountId').val(invoiceAuthorizationAmountId)
            $find('addNewInvoiceItem').show();
            clearAddInvoiceItemSearch();
            return false;
        }

        function searchAddInvoiceItems() {
            $('#PriceListPopupSearchInfoBox')[0].style.display = 'none';
            $.ajax({
                type: "POST",
                url: "InvoiceDetailForm.aspx/GetAddInvoiceItemSearchResults",
                data: '{ claimAuthorizationId: "' + $('#AuthorizationId').val() + '",serviceClassId: "' + $('#<%=moServiceClass.ClientID %>').find('":selected').val() +
                    '",serviceTypeId: "' + $('#<%=moServiceType.ClientID %>').find('":selected').val() + '",riskTypeId: "' + $('#<%=moRiskType.ClientID %>').find('":selected').val() +
                    '", equipmentClassId: "' + $('#<%=moEquipmentClass.ClientID %>').find('":selected').val() + '",equipmentId: "' +
                    $('#<%=moEquipment.ClientID %>').find('":selected').val() + '",conditionId: "' + $('#<%=moCondition.ClientID %>').find('":selected').val() + '", sku: "' +
                    $('#<%=moSku.ClientID %>').val().trim() + '",skuDescription: "' + $('#<%=moSkuDescription.ClientID %>').val().trim() + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    xml = GetXmlDoc(msg.d.xml);
                    if (msg.d.message != null && msg.d.message.length > 0) {
                        $('#PriceListPopupSearchInfo').text(msg.d.message);
                        $('#PriceListPopupSearchInfoBox')[0].style.display = 'block';
                    }
                    xsl = LoadXmlDoc("InvoicePriceListDetails.xslt");

                    // code for IE
                    if (window.ActiveXObject) {
                        ex = xml.transformNode(xsl);
                        $('#dvInvoicePriceListResults').html(ex);
                    }
                    // code for Mozilla, Firefox, Opera, etc.
                    else if (document.implementation && document.implementation.createDocument) {
                        xsltProcessor = new XSLTProcessor();
                        xsltProcessor.importStylesheet(xsl);
                        resultDocument = xsltProcessor.transformToFragment(xml, document);
                        $('#dvInvoicePriceListResults').html(resultDocument);
                    }
                    $('#selectedPage').val('1');
                    $('#dvInvoicePriceListResults').show();
                }
            });
        }

        function clearAddInvoiceItemSearch() {
            $('#<%=moServiceClass.ClientID %> option:nth(0)').attr("selected", "selected");
            $('#<%=moServiceType.ClientID %> option:nth(0)').attr("selected", "selected");
            $('#<%=moRiskType.ClientID %> option:nth(0)').attr("selected", "selected");
            $('#<%=moEquipmentClass.ClientID %> option:nth(0)').attr("selected", "selected");
            $('#<%=moEquipment.ClientID %> option:nth(0)').attr("selected", "selected");
            $('#<%=moCondition.ClientID %> option:nth(0)').attr("selected", "selected");
            $('#<%=moSku.ClientID %>').val('');
            $('#<%=moSkuDescription.ClientID %>').val('');
        }

        function cancelAddInvoiceItemSearch() {
            $('#AuthorizationId').val('');
            $('#senderId').val('');
            $('#index').val('');
            $find('addNewInvoiceItem').hide();
        }

        function AddInvoiceLineItem(serviceClassId, serviceTypeId, vendorSku, vendorSkuDescription, price) {
            $.ajax({
                type: "POST",
                url: "InvoiceDetailForm.aspx/AddInvoiceLineItem",
                data: '{ claimAuthorizationId: "' + $('#AuthorizationId').val() + '", serviceClassId: "' + serviceClassId + '", serviceTypeId: "' +
                    serviceTypeId + '", sku: "' + vendorSku + '", skuDescription: "' + vendorSkuDescription + '", price: "' +
                    price + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    UpdateDifferenceAmount(msg.d.DifferenceAmount);
                    $('#' + $('#InvoiceAuthorizationAmountId').val()).text(convertNumberToCulture(msg.d.AuthorizationAmount, decSep, groupSep));
                    var senderId = $('#senderId').val();
                    if ($('#' + senderId).text() == '+') {
                        $('#' + senderId).text('-');
                    }
                    else {
                        $('#' + senderId).text('+');
                    }
                    ShowHideInvoiceAuthorizationDetails($('#' + senderId)[0], $('#index').val(), msg.d.ClaimAuthorizationId, $('#InvoiceAuthorizationAmountId').val());
                    cancelAddInvoiceItemSearch();
                }
            });
        }

        function RemoveInvoiceLineItem(invoiceItemId, senderId, index, invoiceAuthorizationAmountId) {
            $.ajax({
                type: "POST",
                url: "InvoiceDetailForm.aspx/RemoveInvoiceLineItem",
                data: '{ invoiceItemId: "' + invoiceItemId + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    UpdateDifferenceAmount(msg.d.DifferenceAmount);
                    $('#' + invoiceAuthorizationAmountId).text(convertNumberToCulture(msg.d.AuthorizationAmount, decSep, groupSep));
                    if ($('#' + senderId).text() == '+') {
                        $('#' + senderId).text('-');
                    }
                    else {
                        $('#' + senderId).text('+');
                    }
                    $('#AuthorizationId').val('');
                    ShowHideInvoiceAuthorizationDetails($('#' + senderId)[0], index, msg.d.ClaimAuthorizationId, invoiceAuthorizationAmountId);
                }
            });
        }

        function UpdateInvoice() {
            var invoiceAmount = $('#' + invoiceAmountId).val();
            var perceptionIvaAmount = null;
            if ($('#' + perceptionIvaId).length == 1) {
                perceptionIvaAmount = $('#' + perceptionIvaId).val();
            }
            var perceptionIibbAmount
            if ($('#' + perceptionIibbId).length == 1) {
                perceptionIibbAmount = $('#' + perceptionIibbId).val();
            }

            $.ajax({
                type: "POST",
                url: "InvoiceDetailForm.aspx/UpdateInvoice",
                data: '{ invoiceAmount: "' + invoiceAmount + '", perceptionIvaAmount: "' + perceptionIvaAmount + '", perceptionIibbAmount: "' + perceptionIibbAmount + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    UpdateDifferenceAmount(msg.d.DifferenceAmount);
                }
            });
        }

        function BatchNumberReadOnly(isReadOnly) {
            var moBatchNumber = $('#' + '<%=moBatchNumber.ClientId%>');
            if (moBatchNumber.length > 0) {
                if (isReadOnly) {
                    moBatchNumber.attr('readOnly', 'readOnly');
                }
                else {
                    moBatchNumber.removeAttr('readOnly');
                }
            }
        }

        function UpdateDifferenceAmount(differenceAmount) {
            if (differenceAmount >= 0) {
                $('#' + differenceAmountId).val(convertNumberToCulture(differenceAmount, decSep, groupSep));
            }
            else {
                $('#' + differenceAmountId).val('-' + convertNumberToCulture(differenceAmount * -1, decSep, groupSep));
            }
        }

        function UpdateAuthorization(claimAuthorizationId, repairDateId, pickupDateId) {
            $.ajax({
                type: "POST",
                url: "InvoiceDetailForm.aspx/UpdateAuthorization",
                data: '{ claimAuthorizationId: "' + claimAuthorizationId + '", repairDate: "' + $('#' + repairDateId).val() + '", pickupDate: "' + $('#' + pickupDateId).val() + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json"
            });
        }

        function UpdateInvoiceLineItemAmount(invoiceItemId, lineItemAmount, invoiceAuthorizationAmountId) {
            $.ajax({
                type: "POST",
                url: "InvoiceDetailForm.aspx/UpdateInvoiceItem",
                data: '{ invoiceItemId: "' + invoiceItemId + '", lineItemAmount: "' + lineItemAmount + '" }',
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (msg) {
                    UpdateDifferenceAmount(msg.d.DifferenceAmount);
                    $('#' + invoiceAuthorizationAmountId).text(convertNumberToCulture(msg.d.AuthorizationAmount, decSep, groupSep));
                }
            });
        }

        function showHidePage(newPageNumber, recordCount, pageSize) {
            var newPage = parseInt(newPageNumber);
            var currentPage = parseInt($('#selectedPage').val());
            var tempLocation;
            if (newPageNumber == currentPage) { return; }
            $('#pg1a_' + currentPage).show();
            $('#pg1s_' + currentPage).hide();
            $('#pg2a_' + currentPage).show();
            $('#pg2s_' + currentPage).hide();
            $('#selectedPage').val(newPageNumber);
            $('#pg1a_' + newPageNumber).hide();
            $('#pg1s_' + newPageNumber).show();
            $('#pg2a_' + newPageNumber).hide();
            $('#pg2s_' + newPageNumber).show();
            for (i = 1; i <= pageSize; i++) {
                tempLocation = (((currentPage - 1) * pageSize) + i);
                if (tempLocation <= recordCount) {
                    document.getElementById('tr_' + tempLocation).style.display = "none";
                }
                tempLocation = (((newPage - 1) * pageSize) + i);
                if (tempLocation <= recordCount) {
                    document.getElementById('tr_' + tempLocation).style.display = "block";
                }
            }
        }

        function moSelectAllClick(moSelectAll) {
            __doPostBack('', '<%=SELECT_ALL_EVENT_NAME%>');
        }

    </script>
    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
        runat="server" />
</asp:Content>
