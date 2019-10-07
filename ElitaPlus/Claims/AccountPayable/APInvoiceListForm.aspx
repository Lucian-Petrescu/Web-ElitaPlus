﻿<%@ Page Language="vb" 
    AutoEventWireup="false" 
    MasterPageFile="~/Navigation/masters/ElitaBase.Master" 
    CodeBehind="APInvoiceListForm.aspx.vb" 
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.APInvoiceListForm"
    Theme="Default"%>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="JavaScript" type="text/javascript">
        window.latestClick = '';
        function isNotDblClick() {
            if (window.latestClick != "clicked") {
                window.latestClick = "clicked";
                return true;
            } else {
                return false;
            }
        }

        var gridViewId = '#<%= Grid.ClientID %>';
        function checkAll(selectAllCheckbox) {
                //get all checkboxes within item rows and select/deselect based on select all checked status
                //:checkbox is jquery selector to get all checkboxes
                $('td :checkbox', gridViewId).prop("checked", selectAllCheckbox.checked);
        }

        function unCheckSelectAll(selectCheckbox) {
            //if any item is unchecked, uncheck header checkbox as well
            if (!selectCheckbox.checked)
                $('th :checkbox', gridViewId).prop("checked", false);
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table width="100%" border="0" class="searchGrid" id="searchTable" runat="server">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td>
                            <asp:Label ID="lblVendorCode" runat="server">VENDOR_CODE</asp:Label>:
                        </td>
                        <td>
                            <asp:Label ID="lblInvoiceNumber" runat="server">INVOICE_NUMBER</asp:Label>:
                        </td>
                        <td align="left">
                            <asp:Label ID="lblInoviceSource" runat="server">SOURCE</asp:Label>:
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:TextBox ID="txtVendorCode" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtInvoiceNumber" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtInvoiceSource" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>

                        <td align="left">
                            <asp:Label ID="lblInvoiceDate" runat="server">INVOICE_DATE</asp:Label>:
                        </td>
                        <td align="left">
                            <asp:Label ID="lblInvoiceDueDateFrom" runat="server">DUE_DATE_FROM</asp:Label>:
                        </td>
                        <td>
                            <asp:Label ID="lblInvoiceDueDateTo" runat="server">DUE_DATE_TO</asp:Label>:
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:TextBox ID="txtInvoiceDate" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                            <asp:ImageButton ID="ImageInvoiceDate" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png" style="vertical-align:bottom"></asp:ImageButton>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="txtInvoiceDueDateFrom" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                            <asp:ImageButton ID="ImageInvoiceDueDateFrom" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png" style="vertical-align:bottom"></asp:ImageButton>                            
                        </td>
                        <td>
                            <asp:TextBox ID="txtInvoiceDueDateTo" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                            <asp:ImageButton ID="ImageInvoiceDueDateTo" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png" style="vertical-align:bottom"></asp:ImageButton>
                        </td>
                    </tr>    
                    <!-- commands -->
                    <tr>
                        <td></td>
                        <td></td>
                        <td align="right" colspan="2">
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
                    <td class="bor" align="left">
                        <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>:&nbsp;
                        <asp:DropDownList ID="cboPageSize" runat="server" AutoPostBack="true" Width="50px" SkinID="SmallDropDown">
                            <asp:ListItem Value="5">5</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="15">15</asp:ListItem>
                            <asp:ListItem Value="20">20</asp:ListItem>
                            <asp:ListItem Value="25" Selected="True">25</asp:ListItem>
                            <asp:ListItem Value="30">30</asp:ListItem>
                            <asp:ListItem Value="35">35</asp:ListItem>
                            <asp:ListItem Value="40">40</asp:ListItem>
                            <asp:ListItem Value="45">45</asp:ListItem>
                            <asp:ListItem Value="50">50</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="bor" align="right">
                        <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 100%">
            <asp:GridView ID="Grid" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
                SkinID="DetailPageGridView" AllowSorting="false">
                <SelectedRowStyle Wrap="True" />
                <EditRowStyle Wrap="True" />
                <AlternatingRowStyle Wrap="True" />
                <RowStyle Wrap="True" />
                <Columns>
                    <asp:TemplateField HeaderText="Check">
                        <HeaderTemplate>
                            <asp:CheckBox ID="checkBoxAll" runat="server" class="checkbox" onclick="checkAll(this);"/>
                        </HeaderTemplate>
                        <ItemTemplate>
                            <asp:CheckBox ID="checkBoxSelected" runat="server" class="checkbox" onclick="unCheckSelectAll(this);" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="VENDOR" HtmlEncode="false" DataField="Vendor" ></asp:BoundField>
                    <asp:TemplateField HeaderText="INVOICE_NUMBER">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnInvoiceDetails" runat="server" OnClientClick="document.body.style.cursor = 'wait'; if(isNotDblClick()) {return true;} else return false;"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="InvoiceDate" HeaderText="INVOICE_DATE" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField DataField="DueDate" HeaderText="DUE_DATE" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField DataField="Source" HeaderText="SOURCE" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField DataField="InvoiceAmt" HeaderText="INVOICE_AMOUNT" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField DataField="MatchedAmount" HeaderText="MATCHED_AMOUNT" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField DataField="PaidAmount" HeaderText="PAID_AMOUNT" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField DataField="PaymentDate" HeaderText="PAYMENT_DATE" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField DataField="UnMatchedLineCnt" HeaderText="UNMATCHED_LINES" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField Visible="False" DataField="ap_invoice_header_id"></asp:BoundField>                    
                </Columns>
                <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                <PagerStyle />
            </asp:GridView>
        </div>
        <div class="btnZone" id="divButtons" runat="server">
            <asp:Button ID="btnDelete_WRITE" runat="server" SkinID="AlternateLeftButton" Text="DELETE_INVOICES"></asp:Button>&nbsp;&nbsp;&nbsp;
            <asp:Label ID="lblBatchNum" runat="server">BATCH_NUMBER</asp:Label>:
            <asp:TextBox ID="txtBatchNum" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
            <asp:Button ID="btnCreatePaymentBatch_WRITE" runat="server" SkinID="AlternateLeftButton" Text="CREATE_PAYMENT"></asp:Button>            
        </div>
    </div>    
</asp:Content>
