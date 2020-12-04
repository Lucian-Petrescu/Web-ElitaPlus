<%@ Page Title="AR Invoice" 
    Language="vb" 
    AutoEventWireup="false" 
    MasterPageFile="~/Navigation/masters/ElitaBase.Master" 
    CodeBehind="ARInvoiceListForm.aspx.vb" 
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ArInvoiceListForm" 
    Theme="Default"%>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.12.4.min.js"> </script>    
    <style type="text/css">
        .checkboxLine { 
            margin-left:auto;
        }

        .checkboxHeader { 
            margin-left:auto;
        }

        .formFont{
            font-size:13px;
        }

        .wrapText{white-space:pre-wrap;}
    </style>
    
    <script language="JavaScript" type="text/javascript">
        window.latestClick = '';
        function isNotDblClick() {
            if (window.latestClick !== "clicked") {
                window.latestClick = "clicked";
                return true;
            } else {
                return false;
            }
        }

        jQuery.noConflict();            

        function checkAll(selectAllCheckbox) {
            (function( $jq ) {
                $jq('.checkboxLine').prop("checked", selectAllCheckbox.checked);
            })(jQuery);           
        }

        function unCheckSelectAll(selectCheckbox) {
            if (!selectCheckbox.checked) {
                (function( $jq ) {
                    $jq('.checkboxHeader').prop("checked", false);
                })(jQuery);     
                
            }
                
        }

        var referenceNumCtlId = '<%=txtReferenceNumber.ClientId%>';

        function EnableDisableReferenceNum(referenceSelection) {
            if (referenceSelection !== "") {
                $('#' + referenceNumCtlId).removeAttr("disabled");
                return;
            } else {
                $('#' + referenceNumCtlId).attr('disabled', 'disabled');
                $('#' + referenceNumCtlId).val('');
            }
        }

        $(function () {
            EnableDisableReferenceNum('<%=ddlReference.SelectedValue%>');
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table width="100%" border="0" class="searchGrid" id="searchTable" runat="server">
        <tr>
            <td>
                <table style="border-spacing: 0; border-collapse: collapse; border: none; padding: 0;" width="100%">
                    <tr>
                        <td style="text-align: left;">
                            <asp:Label ID="lblVendorCode" runat="server">COMPANY</asp:Label>:
                        </td>
                        <td>
                            <asp:Label ID="lblDealer" runat="server">DEALER</asp:Label>:
                        </td>
                        <td >
                            <asp:Label ID="lblSource" runat="server">SOURCE</asp:Label>:
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList runat="server" id="ddlCompany" AutoPostBack="True" SkinID="MediumDropDown"/>
                        </td>
                        <td>
                            <asp:DropDownList runat="server" id="ddlDealer" AutoPostBack="False" SkinID="MediumDropDown"/>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSource" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>

                        <td style="text-align: left;">
                            <asp:Label ID="lblInvoiceNumber" runat="server">INVOICE_NUMBER</asp:Label>:
                        </td>
                        <td >
                            <asp:Label ID="lblInvoiceDate" runat="server">INVOICE_DATE</asp:Label>:
                        </td>
                        <td>
                            <asp:Label ID="lblStatus" runat="server">STATUS</asp:Label>:
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;">
                            <asp:TextBox ID="txtInvoiceNumber" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="txtInvoiceDate" runat="server" SkinID="SmallTextBox" AutoPostBack="False"></asp:TextBox>
                            <asp:ImageButton ID="ImageInvoiceDate" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png" style="vertical-align:bottom"></asp:ImageButton>
                        </td>
                        <td>
                            <asp:DropDownList runat="server" id="ddlStatus" AutoPostBack="False" SkinID="SmallDropDown"/>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;">
                            <asp:Label ID="lblReference" runat="server">REFERENCE</asp:Label>:
                        </td>
                        <td style="text-align: left;">
                            <asp:Label ID="lblReferenceNumber" runat="server">REFERENCE_NUMBER</asp:Label>:
                        </td>
                        <td>
                            <asp:Label ID="lblDocType" runat="server">DOCUMENT_TYPE</asp:Label>:
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: left;">
                            <asp:DropDownList runat="server" id="ddlReference" AutoPostBack="False" SkinID="SmallDropDown" onchange="EnableDisableReferenceNum(this.value);">
                            </asp:DropDownList>
                        </td>
                        <td >
                            <asp:TextBox ID="txtReferenceNumber" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:DropDownList runat="server" id="ddlDocType" AutoPostBack="False" SkinID="SmallDropDown">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDocumentUniqueId" runat="server">DOCUMENT_UNIQUE_ID</asp:Label>:
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="txtDocumentUniqueId" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <!-- commands -->
                    <tr>
                        <td></td>
                        <td></td>
                        <td colspan="2" style="text-align: right;">
                            <label>
                                <asp:Button ID="btnClearSearch" runat="server" SkinID="AlternateLeftButton" Text="Clear"></asp:Button>
                                <asp:Button ID="btnSearch" runat="server" SkinID="SearchButton" Text="Search"></asp:Button>
                            </label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
        <h2 class="dataGridHeader">
            <asp:Label runat="server" ID="moSearchResultsHeader">SEARCH_RESULTS</asp:Label>
        </h2>       
        <div>
            <table width="100%" class="dataGrid">
                <tr id="trPageSize" runat="server">
                    <td class="bor" style="text-align: left;">
                        <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>:&nbsp;
                        <asp:DropDownList ID="cboPageSize" runat="server" AutoPostBack="true" Width="60px" SkinID="SmallDropDown">
                            <asp:ListItem Value="20">20</asp:ListItem>
                            <asp:ListItem Value="50" Selected="True">50</asp:ListItem>
                            <asp:ListItem Value="100">100</asp:ListItem>
                            <asp:ListItem Value="200">200</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="bor" style="text-align: right;">
                        <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 100%">
            <asp:GridView ID="Grid" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
                SkinID="DetailPageGridView" AllowSorting="True">
                <SelectedRowStyle Wrap="True" />
                <EditRowStyle Wrap="True" />
                <AlternatingRowStyle Wrap="True" />
                <RowStyle Wrap="True" />
                <Columns>
                    <asp:TemplateField>
                        <HeaderTemplate>
                            <asp:CheckBox ID="checkBoxAll" runat="server" onclick="checkAll(this);"/>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="checkBoxSelected" runat="server" onclick="unCheckSelectAll(this);" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="DEALER" SortExpression="dealer_code">
                        <ItemTemplate>
                            <%# Eval("dealer_code") + " - " + Eval("dealer_name")%>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="INVOICE_NUMBER">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnInvoiceDetails" runat="server" OnClientClick="document.body.style.cursor = 'wait'; if(isNotDblClick()) {return true;} else return false;"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField SortExpression="invoice_date" DataField="invoice_date" HeaderText="INVOICE_DATE" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField DataField="invoice_amount" HeaderText="INVOICE_AMOUNT" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField DataField="doc_type" HeaderText="DOCUMENT_TYPE" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField DataField="reference" HeaderText="REFERENCE" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField DataField="status_xcd" HeaderText="STATUS" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField DataField="source" HeaderText="SOURCE" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField DataField="doc_unique_identifier" HeaderText="DOCUMENT_UNIQUE_ID" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField Visible="False" DataField="invoice_header_id"></asp:BoundField> 
                    <asp:BoundField Visible="False"></asp:BoundField> 
                </Columns>
                <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                <PagerStyle />
            </asp:GridView>
        </div>
        <div class="btnZone" id="divButtons" runat="server" style="text-align:left">
            <asp:Button ID="btnReviewDecision_WRITE" runat="server" SkinID="AlternateLeftButton" Text="INVOICE_REVIEW" />
        </div>
    </div>
    
    <asp:HiddenField ID="HiddenFieldReviewDecision" runat="server" Value="N" />
    
    

    <div id="ModalReviewDecision" class="overlay">
        <div id="light" class="overlay_message_content" style="left: 20%; top: 5%; width: 60%; max-height: 80%">
          
            <h2 class="dataGridHeader" runat="server">
                <asp:Label runat="server" ID="moReviewDecisionLabel" Text="INVOICE_REVIEW" />
            </h2>
            
            <table width="95%" style="border:none;margin:10px; padding: 10px;">
                <tr>
                    <td colspan="2">
                        <asp:Label ID="lblReviewDecisionResult" runat="server" Visible="False"></asp:Label>
                    </td>
                </tr>
                <tr>
                    <td nowrap="nowrap" style="width: 20%;text-align:right;">
                        <asp:Label runat="server" ID="lblReviewDecision" Text="REVIEW_DECISION" />*:
                    </td>   
                    <td nowrap="nowrap" style="width: 80%; text-align: left;">
                        <asp:DropDownList runat="server" id="ddlReviewDecision" SkinID="MediumDropDown" Width="250px" />
                    </td>
                </tr>
                <tr>
                    <td nowrap="nowrap" style="text-align: right;">
                        <asp:Label runat="server" ID="lblReviewComments" Text="COMMENTS" />*:
                    </td>   
                    <td nowrap="nowrap" style="text-align:left">
                        <asp:TextBox ID="txtReviewComments" runat="server" CssClass="formFont wrapText" Wrap="True" MaxLength="500" ForeColor="black" TextMode="MultiLine" Rows="5" Columns="100" Width="850px" ></asp:TextBox>
                    </td>
                </tr>
                <tr style="padding-bottom:20px;">
                    <td colspan="2" style="text-align: right;margin-top:20px;">
                        <asp:Button ID="btnReviewDecisionSave" runat="server" SkinID="SearchButton" Text="Save" /> &nbsp;
                        <asp:Button ID="btnReviewDecisionCancel" runat="server" SkinID="SearchButton" Text="Cancel"
                                    OnClientClick="SetShowReviewDecision('N'); hideModal('ModalReviewDecision'); return false;"/> &nbsp;
                    </td>
                </tr>                
            </table>
                
        </div>
        <div id="fade" class="black_overlay">
        </div>
    </div>
    <script type="text/javascript">
        function SetShowReviewDecision(newValue) {
            $('#<% =HiddenFieldReviewDecision.ClientID%>').val(newValue);
        }    
        //debugger;
        if ($('#<% =HiddenFieldReviewDecision.ClientID%>').val() === "Y") {
            revealModal("ModalReviewDecision");
        } else {
            hideModal('ModalReviewDecision');
        };
    </script>

</asp:Content>
