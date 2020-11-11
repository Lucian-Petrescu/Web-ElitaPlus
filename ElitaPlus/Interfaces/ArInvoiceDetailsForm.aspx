<%@ Page 
    Title="" 
    Language="vb" 
    AutoEventWireup="false" 
    MasterPageFile="~/Navigation/masters/ElitaBase.Master" 
    CodeBehind="ArInvoiceDetailsForm.aspx.vb" 
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ArInvoiceDetailsForm" 
    Theme="Default"
%>
<%@ Register TagPrefix="Elita" TagName="UserControlBankInfo" Src="../Common/UserControlBankInfo_New.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlAddress" Src="../Common/UserControlAddress_New.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <link rel="stylesheet" href="//code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css">
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.12.4.min.js"> </script>    
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-ui.min.js"> </script>    
    <script type="text/javascript">
        $( function() {
            $('#tabs').tabs();
        } );
    </script>
    <div class="dataContainer">
        <table class="formGrid" style="border-spacing: 0; border-collapse: collapse; border: none; padding: 0;" width="100%">
            <tr>
                <td class="borderLeft" nowrap="nowrap" style="text-align: right">
                    <asp:Label ID="lblCompany" runat="server">COMPANY</asp:Label>:
                </td>
                <td nowrap="nowrap" style="text-align: left">
                    <asp:DropDownList runat="server" id="ddlCompany" AutoPostBack="False" SkinID="MediumDropDown" Enabled="False"/>
                </td>
                <td style="text-align: right" nowrap="nowrap">
                    <asp:Label ID="lblDealer" runat="server">DEALER</asp:Label>:
                </td>
                <td style="text-align: left" nowrap="nowrap">
                    <asp:DropDownList runat="server" id="ddlDealer" AutoPostBack="False" SkinID="MediumDropDown" Enabled="False"/>
                </td>
            </tr>
            <tr>
                <td class="borderLeft" nowrap="nowrap" style="text-align: right">
                    <asp:Label ID="lblReference" runat="server">REFERENCE</asp:Label>:
                </td>
                <td nowrap="nowrap" style="text-align: left">
                    <asp:DropDownList runat="server" id="ddlReference" AutoPostBack="False" SkinID="SmallDropDown" Enabled="False" />
                </td>
                <td style="text-align: right" nowrap="nowrap">
                    <asp:Label ID="lblReferenceNum" runat="server">REFERENCE_NUMBER</asp:Label>:
                </td>
                <td style="text-align: left" nowrap="nowrap">
                    <asp:TextBox ID="txtReferenceNum" runat="server" ReadOnly="true" SkinID="MediumTextBox" AutoPostBack="False" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="borderLeft" style="text-align: right" nowrap="nowrap">
                    <asp:Label ID="lblDocType" runat="server">DOCUMENT_TYPE</asp:Label>:
                </td>
                <td style="text-align: left" nowrap="nowrap">
                    <asp:DropDownList runat="server" id="ddlDocType" AutoPostBack="False" SkinID="SmallDropDown" Enabled="False"/>
                </td>
                <td style="text-align: right" nowrap="nowrap">
                    <asp:Label ID="lblInvoiceNumber" runat="server">INVOICE_NUMBER</asp:Label>:
                </td>
                <td style="text-align: left" nowrap="nowrap">
                    <asp:TextBox ID="txtInvoiceNumber" runat="server" ReadOnly="true"  SkinID="MediumTextBox" AutoPostBack="False" ></asp:TextBox>
                </td>
                
            </tr>
        <tr>
            <td class="borderLeft" style="text-align: right" nowrap="nowrap">
                <asp:Label ID="lblSource" runat="server">SOURCE</asp:Label>:
            </td>
            <td style="text-align: left" nowrap="nowrap">
                <asp:TextBox ID="txtSource" runat="server" ReadOnly="true" SkinID="MediumTextBox" AutoPostBack="False" ></asp:TextBox>
            </td>
            <td style="text-align: right" nowrap="nowrap">
                <asp:Label ID="lblStatus" runat="server">STATUS</asp:Label>:
            </td>
            <td style="text-align: left" nowrap="nowrap">
                <asp:DropDownList runat="server" id="ddlStatus" AutoPostBack="False" SkinID="SmallDropDown" Enabled="False"/>
            </td>
        </tr>
            <tr>
                <td style="text-align: right" class="borderLeft" nowrap="nowrap">
                    <asp:Label ID="lblInvoiceDate" runat="server">INVOICE_DATE</asp:Label>:
                </td>
                <td style="text-align: left" nowrap="nowrap">
                    <asp:TextBox ID="txtInvoiceDate" runat="server" ReadOnly="true" SkinID="MediumTextBox" AutoPostBack="False" ></asp:TextBox>
                </td>
                <td style="text-align: right" nowrap="nowrap">
                    <asp:Label ID="lblInvoiceAmount" runat="server">INVOICE_AMOUNT</asp:Label>:
                </td>
                <td style="text-align: left" nowrap="nowrap">
                    <asp:TextBox ID="txtInvoiceAmount" runat="server" ReadOnly="true"  SkinID="MediumTextBox" AutoPostBack="False" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right" class="borderLeft" nowrap="nowrap">
                    <asp:Label ID="lblInvoiceDueDate" runat="server">INVOICE_DUE_DATE</asp:Label>:
                </td>
                <td style="text-align: left" nowrap="nowrap">
                    <asp:TextBox ID="txtInvoiceDueDate" runat="server" ReadOnly="true" SkinID="MediumTextBox" AutoPostBack="False" ></asp:TextBox>
                </td>
                <td style="text-align: right" nowrap="nowrap">
                    <asp:Label ID="lblInvoiceOpenAmt" runat="server">OPEN_AMOUNT</asp:Label>:
                </td>
                <td style="text-align: left" nowrap="nowrap">
                    <asp:TextBox ID="txtInvoiceOpenAmt" runat="server" ReadOnly="true" SkinID="MediumTextBox" AutoPostBack="False" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="borderLeft" style="text-align: right" nowrap="nowrap">
                    <asp:Label ID="lblPaymentMethod" runat="server">PAYMENT_METHOD</asp:Label>:
                </td>
                <td style="text-align: left" nowrap="nowrap">
                    <asp:DropDownList runat="server" id="ddlPaymentMethod" AutoPostBack="False" SkinID="SmallDropDown" Enabled="False"/>
                </td>
                <td style="text-align: right" nowrap="nowrap">
                    <asp:Label ID="lblNumOfPymtRejected" runat="server">NO_OF_PYMT_REJECTED</asp:Label>:
                </td>
                <td style="text-align: left" nowrap="nowrap">
                    <asp:TextBox ID="txtNumOfPymtRejected" runat="server" ReadOnly="true" SkinID="MediumTextBox" AutoPostBack="False" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right" class="borderLeft" nowrap="nowrap">
                    <asp:Label ID="lblAccountingPeriod" runat="server">ACCOUNTING_PERIOD</asp:Label>:
                </td>
                <td style="text-align: left" nowrap="nowrap">
                    <asp:TextBox ID="txtAccountingPeriod" runat="server" ReadOnly="true" SkinID="MediumTextBox" AutoPostBack="False" ></asp:TextBox>
                </td>
                <td style="text-align: right" nowrap="nowrap">
                    <asp:Label ID="lblInstallmentNum" runat="server">INSTALLMENT_NUMBER</asp:Label>:
                </td>
                <td style="text-align: left" nowrap="nowrap">
                    <asp:TextBox ID="txtInstallmentNum" runat="server" ReadOnly="true"  SkinID="MediumTextBox" AutoPostBack="False" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right" nowrap="nowrap" class="borderLeft">
                    <asp:Label ID="lblCurrencyCode" runat="server">CURRENCY_CODE</asp:Label>:
                </td>
                <td style="text-align: left" nowrap="nowrap">
                    <asp:TextBox ID="txtCurrencyCode" runat="server" ReadOnly="true" SkinID="MediumTextBox" AutoPostBack="False" ></asp:TextBox>
                </td>
                <td style="text-align: right" nowrap="nowrap" >
                    <asp:Label ID="lblExchangeRate" runat="server">EXCHANGE_RATE</asp:Label>:
                </td>
                <td style="text-align: left" nowrap="nowrap">
                    <asp:TextBox ID="txtExchangeRate" runat="server" ReadOnly="true" SkinID="MediumTextBox" AutoPostBack="False" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="borderLeft" style="text-align: right" nowrap="nowrap">
                    <asp:Label ID="lblCreatedBy" runat="server">CREATED_BY</asp:Label>:
                </td>
                <td style="text-align: left" nowrap="nowrap">
                    <asp:TextBox ID="txtCreatedBy" runat="server" ReadOnly="true" SkinID="MediumTextBox" AutoPostBack="False" ></asp:TextBox>
                </td>
                <td style="text-align: right" nowrap="nowrap">
                    <asp:Label ID="lblCreatedDate" runat="server">CREATED_DATE</asp:Label>:
                </td>
                <td style="text-align: left" nowrap="nowrap">
                    <asp:TextBox ID="txtCreatedDate" runat="server" ReadOnly="true" SkinID="MediumTextBox" AutoPostBack="False" ></asp:TextBox>
                </td>                
            </tr>
            <tr>
                <td style="text-align: right" class="borderLeft" nowrap="nowrap">
                    <asp:Label ID="lblDistributed" runat="server">DISTRIBUTED</asp:Label>:
                </td>
                <td style="text-align: left" nowrap="nowrap">
                    <asp:TextBox ID="txtDistributed" runat="server" ReadOnly="true" SkinID="MediumTextBox" AutoPostBack="False" ></asp:TextBox>
                </td>
                <td style="text-align: right" nowrap="nowrap">
                    <asp:Label ID="lblPosted" runat="server">POSTED</asp:Label>:
                </td>
                <td style="text-align: left" nowrap="nowrap">
                    <asp:TextBox ID="txtPosted" runat="server" ReadOnly="true" SkinID="MediumTextBox" AutoPostBack="False" ></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td style="text-align: right" class="borderLeft" nowrap="nowrap">
                    <asp:Label ID="lblDocUniqueId" runat="server">DOCUMENT_UNIQUE_ID</asp:Label>:
                </td>
                <td style="text-align: left" nowrap="nowrap">
                    <asp:TextBox ID="txtDocUniqueId" runat="server" ReadOnly="true" SkinID="MediumTextBox" AutoPostBack="False" ></asp:TextBox>
                </td>
                <td style="text-align: right" nowrap="nowrap">
                    <asp:Label ID="lblComments" runat="server">COMMENTS</asp:Label>:
                </td>
                <td style="text-align: left" nowrap="nowrap">
                    <asp:TextBox ID="txtComments" runat="server" ReadOnly="true" SkinID="LargeTextBox" AutoPostBack="False" TextMode="MultiLine" Rows="3"></asp:TextBox>
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
                                <li><a href="#tabBankInfo"><asp:Label ID="Label1" runat="server" CssClass="tabHeaderText">BANK_INFO</asp:Label></a></li>
                                <li><a href="#tabBillToAddress"><asp:Label ID="Label2" runat="server" CssClass="tabHeaderText">BILL_TO_ADDRESS</asp:Label></a></li>
                                <li><a href="#tabShipToAddress"><asp:Label ID="Label6" runat="server" CssClass="tabHeaderText">SHIP_TO_ADDRESS</asp:Label></a></li>
                            </ul>
                            <div id="tabInvoiceLines">
                                <table width="100%" class="dataGrid" style="border-spacing: 0; border-collapse: collapse; border: none; padding: 0; margin-bottom:5px;">
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
                                        <td style="text-align: right" class="bor">
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
                                            <asp:BoundField DataField="item_code" HeaderText="ITEM CODE" HtmlEncode="false"></asp:BoundField>
                                            <asp:BoundField DataField="parent_line_number" HeaderText="PARENT_LINE_NUMBER" HtmlEncode="false"></asp:BoundField>
                                            <asp:BoundField DataField="description" HeaderText="DESCRIPTION" HtmlEncode="false"></asp:BoundField>
                                            <asp:BoundField DataField="amount" HeaderText="AMOUNT" HtmlEncode="false"></asp:BoundField>
                                            <asp:BoundField DataField="earning_parter" HeaderText="EARNING_PARTER" HtmlEncode="false"></asp:BoundField>
                                            <asp:BoundField DataField="invoice_period_start_date" HeaderText="INVOICE_PERIOD_START_DATE" HtmlEncode="false"></asp:BoundField>
                                            <asp:BoundField DataField="invoice_period_end_date" HeaderText="INVOICE_PERIOD_END_DATE" HtmlEncode="false"></asp:BoundField>
                                            <asp:BoundField DataField="incoming_amount" HeaderText="INCOMING_AMOUNT" HtmlEncode="false"></asp:BoundField>
                                        </Columns>
                                    <PagerSettings PageButtonCount="10" Mode="Numeric" Position="TopAndBottom" />
                                </asp:GridView>
                            </div>
                            <div id="tabBankInfo">
                                <table width="100%" class="formGrid" style="border-spacing: 0; border-collapse: collapse; border: none; padding: 0;">
                                    <tbody>
                                    <Elita:UserControlBankInfo ID="ucBankInfo" runat="server"></Elita:UserControlBankInfo>
                                    </tbody>
                                </table>
                            </div>
                            <div id="tabBillToAddress">
                                <table width="100%" class="formGrid" style="border-spacing: 0; border-collapse: collapse; border: none; padding: 0;">
                                    <Elita:UserControlAddress ID="ucAddressBillTo" runat="server"></Elita:UserControlAddress>
                                </table>
                            </div>
                            <div id="tabShipToAddress">
                                <table width="100%" class="formGrid" style="border-spacing: 0; border-collapse: collapse; border: none; padding: 0;">
                                    <Elita:UserControlAddress ID="ucAddressShipTo" runat="server"></Elita:UserControlAddress>
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
    </div>
    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse" runat="server" />
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
</asp:Content>
