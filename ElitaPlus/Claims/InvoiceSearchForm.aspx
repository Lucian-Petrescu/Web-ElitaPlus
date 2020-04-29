<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="InvoiceSearchForm.aspx.vb"
    Theme="Default" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.InvoiceSearchForm"
    EnableSessionState="True" MasterPageFile="~/Navigation/masters/ElitaBase.Master" %>

<%@ Register TagPrefix="Elita" TagName="FieldSearchCriteriaNumber" Src="~/Common/FieldSearchCriteriaControl.ascx" %>
<asp:Content ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.6.1.min.js" />
    <script type="text/javascript" language="javascript">
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
<asp:Content ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table width="100%" class="searchGrid" runat="server" id="searchGrid" border="0"
        cellspacing="0" cellpadding="0">
        <tbody>
            <tr>
                <td style="width: 50%">
                    <asp:Label runat="server" ID="moServiceCenterLabel" Text="SERVICE_CENTER"></asp:Label>
                    <br />
                    <asp:TextBox runat="server" ID="moServiceCenter" SkinID="MediumTextBox" />
                </td>
                <td>
                    <Elita:FieldSearchCriteriaNumber runat="server" ID="moDateCreated" DataType="DateTime"
                        Text="DATE_CREATED" />
                </td>
            </tr>
            <tr>
                <td>
                    <Elita:FieldSearchCriteriaNumber runat="server" ID="moInvoiceNumber" DataType="NumberString"
                        Text="INVOICE_NUMBER" />
                </td>
                <td>
                    <Elita:FieldSearchCriteriaNumber runat="server" ID="moInvoiceAmount" DataType="NumberDouble"
                        Text="INVOICE_AMT" />
                </td>
            </tr>
            <tr>
                <td>
                    <Elita:FieldSearchCriteriaNumber runat="server" ID="moInvoiceDate" DataType="Date"
                        Text="INVOICE_DATE" />
                </td>
                <td>
                    <Elita:FieldSearchCriteriaNumber runat="server" ID="moBatchNumber" DataType="NumberString"
                        Text="BATCH_NUMBER" />
                </td>
            </tr>
            <tr>
                <td>
                    <Elita:FieldSearchCriteriaNumber runat="server" ID="moClaimNumber" DataType="NumberString"
                        Text="CLAIM_NUMBER" />
                </td>
                <td>
                    <Elita:FieldSearchCriteriaNumber runat="server" ID="moAuthorizationNumber" DataType="NumberString"
                        Text="AUTHORIZATION_NUMBER" />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="right">
                    <asp:Button runat="server" ID="btnSearch" SkinID="SearchRightButton" Text="SEARCH" />
                    <asp:Button runat="server" ID="btnClear" SkinID="AlternateRightButton" Text="CLEAR" />
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
<asp:Content ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer" runat="server" id="moSearchResults" visible="false">
        <h2 class="dataGridHeader">
            <asp:Label runat="server" ID="moSearchResultsHeader">SEARCH_RESULTS_FOR_INVOICE</asp:Label>
        </h2>
        <table width="100%" class="dataGrid" border="0" cellspacing="0" cellpadding="0">
            <tbody>
                <tr>
                    <td class="bor">
                        <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>
                        <asp:DropDownList ID="cboPageSize" runat="server" SkinID="ExtraSmallDropDown" AutoPostBack="true" Width="50px">
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
        <table width="100%" class="dataGrid">
            <tbody>
                <asp:Repeater runat="server" ID="moInvoiceRepeater">
                    <HeaderTemplate>
                        <tr id="moPagerRow" runat="server" style="display: none">
                            <td colspan="6" class="gridPager" style="text-align: center;" runat="server" id="moPagerCell">
                            </td>
                        </tr>
                        <tr>
                            <th width="15px" align="center" />
                            <th nowrap="noWrap">
                                <asp:LinkButton runat="server" ID="moInvoiceNumberSort" Text="INVOICE_NUMBER" />
                            </th>
                            <th nowrap="noWrap">
                                <asp:LinkButton runat="server" ID="moServiceCenterSort" Text="SERVICE_CENTER_NAME" />
                            </th>
                            <th nowrap="noWrap" width="100px">
                                <asp:LinkButton runat="server" ID="moInvoiceDateSort" Text="INVOICE_DATE" />
                            </th>
                            <th nowrap="noWrap" width="100px">
                                <asp:LinkButton runat="server" ID="moInvoiceAmountSort" Text="INVOICE_AMOUNT" />
                            </th>
                            <th nowrap="noWrap" width="100px">
                                <asp:LinkButton runat="server" ID="moInvoiceStatusSort" Text="INVOICE_STATUS" />
                            </th>
                        </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td align="center" rowspan="1">
                                <span style="cursor: pointer" id='<%# "Expand" & Container.ItemIndex %>' onclick="ShowHideInvoiceDetails(<%# Container.ItemIndex %>, '<%# New Guid(CType(Eval("INVOICE_ID"), Byte())).ToString() %>');">
                                    +</span>
                            </td>
                            <td nowrap="noWrap">
                                <asp:LinkButton runat="server" ID="moInvoiceNumber" OnClick="moInvoiceNumber_Click" />
                            </td>
                            <td>
                                <asp:Label runat="server" ID="moServiceCenter" />
                            </td>
                            <td nowrap="noWrap">
                                <asp:Label runat="server" ID="moInvoiceDate" />
                            </td>
                            <td align="right" nowrap="noWrap">
                                <asp:Label runat="server" ID="moInvoiceAmount" />
                            </td>
                            <td>
                                <asp:Label runat="server" ID="moInvoiceStatus" />
                            </td>
                        </tr>
                        <tr id='<%# "Child" & Container.ItemIndex %>' style="display: none">
                            <td colspan="5" id='<%# "Cell" & Container.ItemIndex %>'>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate>
                        <tr id="moPagerRow" runat="server" style="display: none">
                            <td colspan="6" class="gridPager" style="text-align: center;" runat="server" id="moPagerCell">
                            </td>
                        </tr>
                    </FooterTemplate>
                </asp:Repeater>
            </tbody>
        </table>
    </div>
    <div class="btnZone">
        <asp:Button runat="server" ID="btnAdd_WRITE" Text="New" SkinID="AlternateLeftButton" />
    </div>
    <div class="modal">
    </div>
    <script type="text/javascript" language="javascript">
        //        $(function () {
        //            debugger;
        //            $("body").on({
        //                ajaxStart: function () {
        //                    debugger;
        //                    $(this).addClass("loading");
        //                },
        //                ajaxStop: function () {
        //                    debugger;
        //                    $(this).removeClass("loading");
        //                }
        //            });
        //        });


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

        function ShowHideInvoiceDetails(index, invoiceId) {
            if ($('#Expand' + index).text() == '+') {
                $.ajax({
                    type: "POST",
                    url: "InvoiceSearchForm.aspx/GetAuthorizations",
                    data: '{ invoiceId: "' + invoiceId + '" }',
                    contentType: "application/json; charset=utf-8",
                    dataType: "json",
                    success: function (msg) {
                        xml = GetXmlDoc(msg.d);
                        xsl = LoadXmlDoc("InvoiceAuthorizations.xslt");

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
                        $('#Expand' + index).text('-');
                        $('#Expand' + index).parent().attr('RowSpan', '2');
                    }
                });
            }
            else {
                $('#Child' + index).hide();
                $('#Expand' + index).text('+');
                $('#Expand' + index).parent().removeAttr('RowSpan');
            }
        }
    </script>
</asp:Content>
