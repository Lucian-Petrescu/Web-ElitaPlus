<%@ Page Title="" Language="vb" 
    AutoEventWireup="false" 
    MasterPageFile="~/Navigation/masters/ElitaBase.Master" 
    CodeBehind="APInvoiceDetailForm.aspx.vb" 
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.APInvoiceDetailForm" 
    Theme="Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <script src="https://code.jquery.com/jquery-1.12.4.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.js"></script>
    <script type="text/javascript">
      $( function() {
        $('#tabs').tabs();
      } );
    </script>
    <div class="dataContainer">
        <table class="formGrid" border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px;" width="100%">
            <tr>
                <td align="right" class="borderLeft" nowrap="nowrap">
                    <asp:Label ID="lblVendor" runat="server">VENDOR</asp:Label>:
                </td>
                <td align="left" nowrap="nowrap">
                    <asp:TextBox ID="txtVendor" runat="server" ReadOnly="true" SkinID="MediumTextBox" AutoPostBack="False" ></asp:TextBox>
                </td>
                <td align="right" nowrap="nowrap">
                    <asp:Label ID="lblVendorAddress" runat="server">VENDOR_ADDRESS</asp:Label>:
                </td>
                <td align="left" nowrap="nowrap">
                    <asp:TextBox ID="txtVendorAddress" runat="server" ReadOnly="true" SkinID="LargeTextBox" AutoPostBack="False" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="borderLeft" nowrap="nowrap">
                    <asp:Label ID="lblInvoiceNumber" runat="server">INVOICE_NUMBER</asp:Label>:
                </td>
                <td align="left" nowrap="nowrap">
                    <asp:TextBox ID="txtInvoiceNumber" runat="server" ReadOnly="true"  SkinID="MediumTextBox" AutoPostBack="False" ></asp:TextBox>
                </td>
                <td align="right" nowrap="nowrap">
                    <asp:Label ID="lblSource" runat="server">SOURCE</asp:Label>:
                </td>
                <td align="left" nowrap="nowrap">
                    <asp:TextBox ID="txtSource" runat="server" ReadOnly="true" SkinID="LargeTextBox" AutoPostBack="False" ></asp:TextBox>
                </td>
                
            </tr>
            <tr>
                <td align="right" class="borderLeft" nowrap="nowrap">
                    <asp:Label ID="lblInvoiceDate" runat="server">INVOICE_DATE</asp:Label>:
                </td>
                <td align="left" nowrap="nowrap">
                    <asp:TextBox ID="txtInvoiceDate" runat="server" ReadOnly="true" SkinID="MediumTextBox" AutoPostBack="False" ></asp:TextBox>
                </td>
                <td align="right" nowrap="nowrap">
                    <asp:Label ID="lblInvoiceAmount" runat="server">INVOICE_AMOUNT</asp:Label>:
                </td>
                <td align="left" nowrap="nowrap">
                    <asp:TextBox ID="txtInvoiceAmount" runat="server" ReadOnly="true"  SkinID="MediumTextBox" AutoPostBack="False" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="borderLeft" nowrap="nowrap">
                    <asp:Label ID="lblAccountingPeriod" runat="server">ACCOUNTING_PERIOD</asp:Label>:
                </td>
                <td align="left" nowrap="nowrap">
                    <asp:TextBox ID="txtAccountingPeriod" runat="server" ReadOnly="true" SkinID="MediumTextBox" AutoPostBack="False" ></asp:TextBox>
                </td>
                <td align="right" nowrap="nowrap">
                    <asp:Label ID="lblMatchedAmount" runat="server">MATCHED_AMOUNT</asp:Label>:
                </td>
                <td align="left" nowrap="nowrap">
                    <asp:TextBox ID="txtMatchedAmount" runat="server" ReadOnly="true" SkinID="MediumTextBox" AutoPostBack="False" ></asp:TextBox>
                </td>                
                
            </tr>
            <tr>
                <td align="right" nowrap="nowrap" class="borderLeft">
                    <asp:Label ID="lblTerm" runat="server">TERM</asp:Label>:
                </td>
                <td align="left" nowrap="nowrap">
                    <asp:TextBox ID="txtTerm" runat="server" ReadOnly="true" SkinID="MediumTextBox" AutoPostBack="False" ></asp:TextBox>
                </td>
                <td align="right" nowrap="nowrap">
                    <asp:Label ID="lblPaidAmount" runat="server">PAID AMOUNT</asp:Label>:
                </td>
                <td align="left" nowrap="nowrap">
                    <asp:TextBox ID="txtPaidAmount" runat="server" ReadOnly="true" SkinID="MediumTextBox" AutoPostBack="False" ></asp:TextBox>
                </td>                
                
                
            </tr>
            <tr>
                <td align="right" class="borderLeft" nowrap="nowrap">
                    <asp:Label ID="lblCurrency" runat="server">CURRENCY</asp:Label>:
                </td>
                <td align="left" nowrap="nowrap">
                    <asp:TextBox ID="txtCurrency" runat="server" ReadOnly="true" SkinID="MediumTextBox" AutoPostBack="False" ></asp:TextBox>
                </td>
                <td align="right" nowrap="nowrap">
                    <asp:Label ID="lblTotalLineCnt" runat="server">TOTAL_LINE_COUNT</asp:Label>:
                </td>
                <td align="left" nowrap="nowrap">
                    <asp:TextBox ID="txtTotalLineCnt" runat="server" ReadOnly="true" SkinID="MediumTextBox" AutoPostBack="False" ></asp:TextBox>
                </td>
            </tr>
            <tr>                
                <td align="right" nowrap="nowrap" class="borderLeft">
                    <asp:Label ID="lblApproved" runat="server">APPROVED</asp:Label>:
                </td>
                <td align="left" nowrap="nowrap">
                    <asp:TextBox ID="txtApproved" runat="server" ReadOnly="true" SkinID="MediumTextBox" AutoPostBack="False" ></asp:TextBox>
                </td>
                <td align="right" nowrap="nowrap">
                    <asp:Label ID="lblUnMatchedLineCnt" runat="server">UNMATCHED_LINE_COUNT</asp:Label>:
                </td>
                <td align="left" nowrap="nowrap">
                    <asp:TextBox ID="txtUnMatchedLineCnt" runat="server" ReadOnly="true" SkinID="MediumTextBox" AutoPostBack="False" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" class="borderLeft" nowrap="nowrap">
                    <asp:Label ID="lblDistributed" runat="server">DISTRIBUTED</asp:Label>:
                </td>
                <td align="left" nowrap="nowrap">
                    <asp:TextBox ID="txtDistributed" runat="server" ReadOnly="true" SkinID="MediumTextBox" AutoPostBack="False" ></asp:TextBox>
                </td>
                <td align="right" nowrap="nowrap">
                    <asp:Label ID="lblPosted" runat="server">POSTED</asp:Label>:
                </td>
                <td align="left" nowrap="nowrap">
                    <asp:TextBox ID="txtPosted" runat="server" ReadOnly="true" SkinID="MediumTextBox" AutoPostBack="False" ></asp:TextBox>
                </td>
            </tr>
            <tr> 
                <td colspan="4" class="borderLeft"> 
                    <%--<div class="dataContainer">            --%>
                        <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0" />
                        <asp:HiddenField ID="hdnDisabledTab" runat="server" />
                        <div id="tabs" class="style-tabs">
                            <ul>
                                <li><a href="#tabInvoiceLines"><asp:Label ID="Label4" runat="server" CssClass="tabHeaderText">INVOICE_LINE_ITEMS</asp:Label></a></li>
                            </ul>
                            <div id="tabInvoiceLines">
                                <table width="100%" class="dataGrid" border="0" cellspacing="0" cellpadding="0" style="margin-bottom:5px;">
                                    <tr>
                                        <td>
                                            <asp:RadioButton ID="rbGetAll" runat="server" AutoPostBack="true" Checked="true" Text="ALL" GroupName="InvoiceLineType" />&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                            <asp:RadioButton ID="rbGetUnmatched" runat="server" AutoPostBack="true" Checked="false" Text="UNMATCHED_LINES_ONLY" GroupName="InvoiceLineType"/>
                                        </td>
                                    </tr>
                                    <tr id="trPageSize" runat="server">
                                        <td class="bor">
                                            <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>
                                            <asp:DropDownList ID="cboPageSize" runat="server" SkinID="SmallDropDown" AutoPostBack="true">
                                                <asp:ListItem Value="5">5</asp:ListItem>
                                                <asp:ListItem Value="10">10</asp:ListItem>
                                                <asp:ListItem Value="15" Selected="True">15</asp:ListItem>
                                                <asp:ListItem Value="20">20</asp:ListItem>
                                                <asp:ListItem Value="25">25</asp:ListItem>
                                                <asp:ListItem Value="30">30</asp:ListItem>
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
                                </table>
                                <asp:GridView ID="Grid" runat="server" Width="100%" OnRowCreated="RowCreated" 
                                        AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" SkinID="DetailPageGridView">
                                        <SelectedRowStyle Wrap="True"></SelectedRowStyle>
                                        <EditRowStyle Wrap="True"></EditRowStyle>
                                        <AlternatingRowStyle Wrap="True"></AlternatingRowStyle>
                                        <RowStyle Wrap="True"></RowStyle>
                                        <HeaderStyle Wrap="True" HorizontalAlign="Center"></HeaderStyle>
                                        <PagerStyle HorizontalAlign ="left" />
                                        <Columns>
                                            <asp:BoundField DataField="line_number" HeaderText="LINE_NUMBER" HtmlEncode="false"></asp:BoundField>
                                            <asp:BoundField DataField="line_type" HeaderText="LINE_TYPE" HtmlEncode="false"></asp:BoundField>
                                            <asp:BoundField DataField="vendor_item_code" HeaderText="ITEM CODE" HtmlEncode="false"></asp:BoundField>
                                            <asp:BoundField DataField="description" HeaderText="DESCRIPTION" HtmlEncode="false"></asp:BoundField>
                                            <asp:BoundField DataField="quantity" HeaderText="QUANTITY" HtmlEncode="false"></asp:BoundField>
                                            <asp:BoundField DataField="unit_price" HeaderText="UNIT_PRICE" HtmlEncode="false"></asp:BoundField>
                                            <asp:BoundField DataField="total_price" HeaderText="TOTAL_PRICE" HtmlEncode="false"></asp:BoundField>
                                            <asp:BoundField DataField="matched_quantity" HeaderText="MATCHED_QUANTITY" HtmlEncode="false"></asp:BoundField>
                                            <asp:BoundField DataField="paid_quantity" HeaderText="PAID_QUANTITY" HtmlEncode="false"></asp:BoundField>
                                            <asp:BoundField DataField="po_number" HeaderText="PURCHASE_ORDER_NUMBER" HtmlEncode="false"></asp:BoundField>
                                            <asp:BoundField DataField="po_line_quantity" HeaderText="PURCHASE_ORDER_QUANTITY" HtmlEncode="false"></asp:BoundField>
                                        </Columns>
                                    <PagerSettings PageButtonCount="10" Mode="Numeric" Position="TopAndBottom" />
                                </asp:GridView>
                                <table width="100%" border="0" cellspacing="0" cellpadding="0">
                                    <tr>
                                        <td>
                                            <asp:Button ID="btnPreviousBatch" runat="server" SkinID="CenterButton" Text="PREVIOUS_BATCH"></asp:Button>&nbsp;&nbsp;&nbsp;
                                            <asp:Button ID="btnGetNextBatch" runat="server" SkinID="CenterButton" Text="Next_BATCH"></asp:Button>
                                        </td>
                                    </tr>
                                </table>
                                        
                            </div>                          
                        </div>
                    <%--</div>--%>
                </td>
            </tr>
        </table>
    </div>
    <div class="btnZone">
        <asp:Button ID="btnBack" runat="server" CausesValidation="False" Text="BACK" SkinID="AlternateLeftButton"></asp:Button>        
        <asp:Button ID="btnRunMatch_WRITE" runat="server" CausesValidation="False" Text="RUN_MATCH" SkinID="PrimaryRightButton"></asp:Button>        
    </div>
    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
</asp:Content>
