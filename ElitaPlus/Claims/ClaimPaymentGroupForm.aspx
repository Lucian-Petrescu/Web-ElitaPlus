<%@ Register TagPrefix="uc1" TagName="UserControlServiceCenterInfo" Src="../Certificates/UserControlServiceCenterInfo.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ClaimPaymentGroupForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ClaimPaymentGroupForm" EnableSessionState="True"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" Theme="Default" %>

<asp:Content ID="Header" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.6.1.min.js" />
    <script type="text/javascript" language="javascript">
        debugger;
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
</asp:Content>
<asp:Content ID="Message" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Summary" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table width="100%" border="0" class="searchGrid" id="searchTable" runat="server">
        <tr>
            <td>
                <asp:Label runat="server" ID="lblPaymentGroupNumber">PAYMENT_GROUP_NUMBER</asp:Label>:
                <asp:TextBox ID="moPaymentGroupNumber" runat="server" Enabled="false" SkinID="SmallTextBox"
                    AutoPostBack="False"></asp:TextBox>
            </td>
            <td align="left">
                <asp:Label runat="server" ID="lblPaymentGroupDate">PAYMENT_GROUP_DATE</asp:Label>:
                <asp:TextBox ID="moPaymentGroupDate" runat="server" Enabled="false" SkinID="SmallTextBox" AutoPostBack="False"></asp:TextBox>
            </td>
            <td align="left">
                <asp:Label runat="server" ID="lblPaymentGroupStatus">PAYMENT_GROUP_STATUS</asp:Label>:
                <asp:TextBox ID="moPaymentGroupStatus" runat="server" Enabled="false" SkinID="SmallTextBox"
                    AutoPostBack="False"></asp:TextBox>                
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label runat="server" ID="lblPaymentGroupTotal">PAYMENT_GROUP_TOTAL</asp:Label>:
                <asp:TextBox ID="moPaymentGroupTotal" runat="server" Enabled="false" SkinID="SmallTextBox"
                    AutoPostBack="False"></asp:TextBox>
            </td>
            <td align="left">
                <asp:Label runat="server" ID="lblExpectedPaymentDate">EXPECTED_PAYMENT_DATE</asp:Label>:
                <asp:TextBox ID="moExpectedPaymentDate" runat="server" SkinID="SmallTextBox" AutoPostBack="False"></asp:TextBox>
                <asp:ImageButton ID="imgExpectedPymntDate" runat="server" Style="vertical-align: bottom"
                    ImageUrl="~/App_Themes/Default/Images/calendar.png" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Body" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer" runat="server" id="moSearchResults" visible="false">
        <h2 class="dataGridHeader">
            <asp:Label runat="server" ID="moSearchResultsHeader">PAYMENT_GROUP_DETAIL</asp:Label>
        </h2>
        <table width="100%" class="dataGrid" border="0" cellspacing="0" cellpadding="0">
            <tbody>
                <tr>
                    <td class="bor">
                        <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>
                        <asp:DropDownList ID="cboPageSize" runat="server" SkinID="SmallDropDown" AutoPostBack="true">
                            <asp:ListItem Value="5">5</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="15">15</asp:ListItem>
                            <asp:ListItem Value="20">20</asp:ListItem>
                            <asp:ListItem Value="25">25</asp:ListItem>
                            <asp:ListItem Selected="True" Value="30">30</asp:ListItem>
                            <asp:ListItem Value="35">35</asp:ListItem>
                            <asp:ListItem Value="40">40</asp:ListItem>
                            <asp:ListItem Value="45">45</asp:ListItem>
                            <asp:ListItem Value="50">50</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="right" class="bor">
                        <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                    </td>
                </tr>
            </tbody>
        </table>
        <table width="100%" class="dataGrid" >
            <tbody>
                <asp:Repeater runat="server" ID="moPaymentRepeater">
                    <HeaderTemplate>
                        <tr id="moPagerRow" runat="server" style="display: none">
                            <td colspan="9" class="gridPager" style="text-align: center;" runat="server" id="moPagerCell">
                            </td>
                        </tr>
                        <tr>
                            <th />
                            <th>
                                <asp:LinkButton runat="server" ID="moSvcCenterCodeSort" Text="SERVICE_CENTER" />
                            </th>
                            <th>
                                <asp:LinkButton runat="server" ID="moClaimNumberSort" Text="CLAIM_NUMBER" />
                            </th>
                            <th>
                                <asp:LinkButton runat="server" ID="moAuthorizationNumberSort" Text="AUTHORIZATION_NUMBER" />
                            </th>
                            <th>
                                <asp:LinkButton runat="server" ID="moInvoiceNumberSort" Text="INVOICE_NUMBER" />
                            </th>
                            <th>
                                <asp:LinkButton runat="server" ID="moInvoiceDateSort" Text="INVOICE_DATE" />
                            </th>
                            <th>
                                <asp:LinkButton runat="server" ID="moTotalAmountSort" Text="RECONCILED_AMOUNT" />
                            </th>
                            <th>
                                <asp:LinkButton runat="server" ID="moItemCountSort" Text="COUNT" />
                            </th>
                            <th>
                                <asp:LinkButton runat="server" ID="moInvoiceDueDateSort" Text="DUE_DATE" />
                            </th>
                           <%-- <th>
                                <asp:Label runat="server" ID="moUpdateAddress" />
                            </th>--%>
                            <th>
                                <asp:Label runat="server" ID="moDeletePayment" />
                            </th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td align="center" rowspan="1">
                                <span style="cursor: pointer" id='<%# "Expand" & Container.ItemIndex %>' onclick="ShowHideInvoiceDetails(<%# Container.ItemIndex %>, '<%# New Guid(CType(Eval("CLAIM_AUTHORIZATION_ID"), Byte())).ToString() %>');">
                                    +</span>
                            </td>
                            <td>
                                <asp:LinkButton runat="server" ID="lblSvcCenterCode" Text="SERVICE_CENTER" />
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblClaimNumber" Text="CLAIM_NUMBER" />
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblAuthorizationNumber" Text="AUTHORIZATION_NUMBER" />
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblInvoiceNumber" Text="INVOICE_NUMBER" />
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblInvoiceDate" Text="INVOICE_DATE" />
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblTotalAmount" Text="RECONCILED_AMOUNT" />
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblItemCount" Text="COUNT" />
                            </td>
                            <td>
                                <asp:Label runat="server" ID="lblInvoiceDueDate" Text="" />
                            </td>
                            <%--<td>
                                <asp:ImageButton ID="btnUpdateAddress" Style="cursor: hand" runat="server" ImageUrl="../Navigation/images/edit.png"
                                    CommandName="UpdateAddress" Tooltip="Update Address" CommandArgument='<%# New Guid(CType(Eval("SERVICE_CENTER_ID"), Byte())).ToString() %>'>
                                </asp:ImageButton>
                            </td>--%>
                            <td>
                                <asp:LinkButton ID="btnDeletePayable" Style="cursor: hand" runat="server" 
                                    CommandName="DeletePayable" Tooltip="Delete Payable" CommandArgument='<%# New Guid(CType(Eval("PAYMENT_GROUP_DETAIL_ID"), Byte())).ToString() %>'>
                                    <asp:Image ID ="imgDelete" runat ="server" ImageUrl="../Navigation/images/icon_delete.png" />
                                </asp:LinkButton>
                            </td>
                        </tr>
                        <tr id='<%# "Child" & Container.ItemIndex %>' style="display: none">
                            <td colspan="10" id='<%# "Cell" & Container.ItemIndex %>'>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr id="moPagerRow" runat="server" style="display: none">
                            <td colspan="9" class="gridPager" style="text-align: center;" runat="server" id="moPagerCell">
                            </td>
                        </tr>
                    </FooterTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
    <div class="btnZone">
        <asp:Button ID="btnBack" runat="server" Text="BACK" SkinID="AlternateLeftButton">
        </asp:Button>
        <asp:Button ID="btnCreatePayment" runat="server" Text="CREATE_PAYMENT" SkinID="PrimaryRightButton">
        </asp:Button>
        <asp:Button ID="btnAddPayables" runat="server" Text="ADD_PAYABLES" SkinID="AlternateRightButton">
        </asp:Button>
    </div>
    <input id="HiddenDeletePromptResponse" type="hidden" runat="server" designtimedragdrop="261" />
    <script type="text/javascript" language="javascript">
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
            debugger;
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

        function ShowHideInvoiceDetails(index, claimAuthId) {
            if ($('#Expand' + index).text() == '+') {
                $.ajax({
                    type: "POST",
                    url: "ClaimPaymentGroupForm.aspx/GetClaimAuthLineItems",
                    data: '{ claimAuthId: "' + claimAuthId + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        xml = GetXmlDoc(msg.d);
                        xsl = LoadXmlDoc("ClaimAuthLineItemAmounts.xslt");

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
</asp:Content>
