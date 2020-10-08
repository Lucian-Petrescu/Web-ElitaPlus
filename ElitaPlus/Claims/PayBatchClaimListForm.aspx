<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>

<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="../Navigation/masters/ElitaBase.Master"
    CodeBehind="PayBatchClaimListForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.PayBatchClaimListForm"
    Theme="Default" %>

<%@ Register TagPrefix="Elita" TagName="UserControlInvoiceRegionTaxes" Src="../Common/UserControlInvoiceRegionTaxes.ascx" %>

<asp:Content ID="MessageContent" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="SummaryContent" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <link type="text/css" href="../Navigation/styles/jquery-ui.min.css" rel="stylesheet" />
    <link type="text/css" href="../Navigation/styles/jquery-ui.elita.css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.12.4.min.js"> </script>
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-ui.min.js"> </script>
    <table cellspacing="0" cellpadding="0" width="100%" border="0" class="searchGrid">
        <tr>
            <td style="width: 10%">
                <asp:Label ID="LabelSearchInvoiceNumber" runat="server">INVOICE_NUMBER</asp:Label>:
            </td>
            <td style="width: 5%">&nbsp;&nbsp;
            </td>
            <td style="width: 10%">
                <asp:Label ID="LabelSearchInvoiceAmount" runat="server">INVOICE_AMT</asp:Label>:
            </td>
            <td style="width: 5%">&nbsp;&nbsp;
            </td>
            <td style="width: 10%">
                <asp:Label ID="LabelInvDateAsterisk" runat="server" Font-Bold="false" Visible="False">*</asp:Label>
                <asp:Label ID="LabelInvoiceDate" runat="server" Font-Bold="false">Invoice_Date</asp:Label>:
            </td>
            <td style="width: 5%">&nbsp;&nbsp;
            </td>
            <td style="width: 10%">
                <asp:Label ID="LabelSearchServiceCenter" runat="server">SERVICE_CENTER</asp:Label>:
            </td>
            <td style="width: 5%">&nbsp;&nbsp;
            </td>
            <td style="width: 10%">&nbsp;&nbsp;
            </td>
            <td style="width: 5%">&nbsp;&nbsp;
            </td>
            <td style="width: 25%">&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td nowrap align="left" width="10%">
                <asp:TextBox ID="TextBoxSearchInvoiceNumber" runat="server" SkinID="SmallTextBox"
                    AutoPostBack="False"></asp:TextBox>
            </td>
            <td width="5%">&nbsp;&nbsp;
            </td>
            <td nowrap align="left" width="10%">
                <asp:TextBox ID="TextBoxSearchInvoiceAmount" runat="server" SkinID="SmallTextBox"
                    AutoPostBack="False"></asp:TextBox>
            </td>
            <td width="5%">&nbsp;&nbsp;
            </td>
            <td nowrap align="left" width="10%">
                <asp:TextBox ID="TextBoxSearchInvoiceDate" runat="server" SkinID="SmallTextBox" AutoPostBack="False"></asp:TextBox>
                <asp:ImageButton ID="ImageButtonInvoiceDate" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png"
                    Visible="True" CausesValidation="False" ImageAlign="AbsMiddle"></asp:ImageButton>
            </td>
            <td width="5%">&nbsp;&nbsp;
            </td>
            <td style="width: 10%">
                <asp:DropDownList ID="cboServiceCenter" runat="server" SkinID="LargeDropDown" AutoPostBack="False">
                </asp:DropDownList>
            </td>
            <td style="width: 5%">&nbsp;&nbsp;
            </td>
            <td style="width: 10%">&nbsp;&nbsp;
            </td>
            <td style="width: 5%">&nbsp;&nbsp;
            </td>
            <td style="width: 25%">&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 10%">
                <asp:Label ID="Label1" runat="server">BATCH_NUMBER</asp:Label>:
            </td>
            <td style="width: 5%">&nbsp;&nbsp;
            </td>
            <td style="width: 10%">
                <asp:Label ID="lblInvRecDateAsterisk" runat="server" Font-Bold="false" Visible="False">*</asp:Label>
                <asp:Label ID="lblInvRecDate" runat="server">INVOICE_RECEIVED_DATE:</asp:Label>
            </td>
            <td style="width: 5%">&nbsp;&nbsp;
            </td>
            <td style="width: 10%">
                <asp:Label ID="lblInvTyp" runat="server">INVOICE_TYPE:</asp:Label>
            </td>
            <td style="width: 5%">&nbsp;&nbsp;
            </td>
            <td style="width: 10%">
                <table border="0">
                    <tr>
                        <td style="width: 10%">
                            <asp:Label ID="lblInvStat" runat="server">INVOICE_STATUS:</asp:Label>
                        </td>
                        <td style="width: 10%"></td>
                        <td style="width: 10%">
                            <asp:Label ID="lblInvCreDt" runat="server">INVOICE_CREATED_DATE:</asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 5%">&nbsp;&nbsp;
            </td>
            <td style="width: 10%"></td>
            <td style="width: 5%">&nbsp;&nbsp;
            </td>
            <td style="width: 25%">&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td style="width: 10%" nowrap align="left">
                <asp:TextBox ID="TextBoxBatchNumber" runat="server" SkinID="SmallTextBox" AutoPostBack="False"></asp:TextBox>
            </td>
            <td style="width: 5%">&nbsp;&nbsp;
            </td>
            <td style="width: 10%" nowrap align="left">
                <asp:TextBox ID="txtboxInvRecDt" runat="server" SkinID="SmallTextBox" AutoPostBack="False"></asp:TextBox>
                <asp:ImageButton ID="imgBtnInvRecDt" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png"
                    Visible="True" CausesValidation="False" ImageAlign="AbsMiddle"></asp:ImageButton>
            </td>
            <td style="width: 5%">&nbsp;&nbsp;
            </td>
            <td style="width: 10%" nowrap align="left">
                <asp:DropDownList ID="ddlInvTyp" runat="server" SkinID="SmallDropDown" AutoPostBack="False">
                </asp:DropDownList>
            </td>
            <td style="width: 5%">&nbsp;&nbsp;
            </td>
            <td style="width: 10%" nowrap align="left">
                <table border="0">
                    <tr>
                        <td style="width: 10%">
                            <asp:DropDownList ID="ddlInvStat" runat="server" SkinID="SmallDropDown" AutoPostBack="False">
                            </asp:DropDownList>
                        </td>
                        <td style="width: 10%"></td>
                        <td style="width: 10%">
                            <asp:TextBox ID="txtboxInvCtdDt" runat="server" SkinID="SmallTextBox" AutoPostBack="False"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
            <td style="width: 5%">&nbsp;&nbsp;
            </td>
            <td style="width: 10%" nowrap align="left"></td>
            <td style="width: 5%">&nbsp;&nbsp;
            </td>
            <td style="width: 25%">&nbsp;&nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="11" align="right">
                <table width="85%" border="0">
                    <tr>
                        <td style="width: 20%">&nbsp;&nbsp;
                        </td>
                        <td style="width: 80%; text-align: right;">
                            <asp:Button ID="btnEditBatch_Write" runat="server" SkinID="AlternateLeftButton" Text="Edit"
                                Visible="False"></asp:Button>
                            <asp:Button ID="btnSaveBatch_Write" runat="server" SkinID="AlternateLeftButton" Text="Save"
                                Visible="False"></asp:Button>
                            <asp:Button ID="btnUndo_Write" runat="server" SkinID="AlternateLeftButton" Text="Undo"
                                Visible="False"></asp:Button>
                            <asp:Button ID="btnClear" runat="server" SkinID="AlternateLeftButton" Text="Clear"
                                Visible="true"></asp:Button>
                            <asp:Button ID="btnExisting" runat="server" SkinID="SearchButton" Text="SEARCH"></asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Body" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <script type="text/javascript">
        $(document).ready(function () {
            var chkAllClaims = $("[id$='chkAllClaims']");
            var chkExcludeDedAll = $("[id$='chkExcludeDedAll']");

            var chkAddClaim = $("[id$='chkAddClaim']");
            var chkExcludeDeductible = $("[id$='chkExcludeDeductible']");

            if ($("[id$='chkAddClaim']:disabled").length > 0) {
                chkAllClaims.prop("disabled", "disabled");
            }

            if ($("[id$='chkExcludeDeductible']:disabled").length > 0) {
                chkExcludeDedAll.prop("disabled", "disabled");
            }

            if ($("[id$='chkAddClaim']:not(:checked)").length == 0) {
                chkAllClaims.prop("checked", true);
            }

            if ($("[id$='chkExcludeDeductible']:not(:checked)").length == 0) {
                chkExcludeDedAll.prop("checked", true);
            }

            chkAllClaims.click(function () {
                setCheckboxState(this, chkAddClaim);
            });

            chkAddClaim.click(function () {
                setHeaderCheckboxState(this, chkAllClaims, 'chkAddClaim');
            });

            chkExcludeDedAll.click(function () {
                setCheckboxState(this, chkExcludeDeductible);
            });

            chkExcludeDeductible.click(function () {
                setHeaderCheckboxState(this, chkExcludeDedAll, 'chkExcludeDeductible');
            });

            function setCheckboxState(headerCheckbox, checkboxes) {
                headerCheckbox.checked == true
                    ? checkboxes.prop("checked", true)
                    : checkboxes.prop("checked", false);
            }

            function setHeaderCheckboxState(selectedCheckbox, headerCheckbox, checkboxeId) {
                var unselectedCheckboxCount = $("[id$='" + checkboxeId + "'" + "]:not(:checked)").length;

                if (selectedCheckbox.checked) {
                    unselectedCheckboxCount > 0
                        ? headerCheckbox.prop("checked", false)
                        : headerCheckbox.prop("checked", true);
                }
                else {
                    if (unselectedCheckboxCount > 0) {
                        headerCheckbox.prop("checked", false);
                    }
                }
            }
        });
    </script>
    <div class="dataContainer">
        <h2 class="dataGridHeader">Search results for Pay Invoice</h2>

        <div style="width: 100%">
            <asp:GridView ID="GridInvoices" runat="server" Width="100%" AllowPaging="True" CellPadding="1"
                AutoGenerateColumns="False" SkinID="DetailPageGridView">
                <SelectedRowStyle Wrap="True" />
                <EditRowStyle Wrap="True" />
                <AlternatingRowStyle Wrap="True" />
                <RowStyle Wrap="True" />
                <Columns>
                    <asp:BoundField Visible="false" HeaderText="invoice_trans_id"></asp:BoundField>
                    <asp:BoundField SortExpression="Status" HeaderText="Status" Visible="False">
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundField>
                    <asp:TemplateField Visible="False" HeaderText="Add_Remove_Claims" HeaderStyle-HorizontalAlign="Center"
                        HeaderStyle-Width="1%">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnAddClaims" runat="server" CausesValidation="False" CommandName="SelectAction"
                                ImageUrl="~/Navigation/images/icons/edit2.gif" CommandArgument="<%#Container.DisplayIndex %>" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="1%">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnEditClaims" Style="cursor: hand" runat="server" CommandName="EditClaimsAction"
                                ImageUrl="../Navigation/images/edit.png"></asp:ImageButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderStyle-HorizontalAlign="Center" HeaderStyle-Width="1%">
                        <ItemTemplate>
                            <asp:ImageButton ID="btnDeleteClaims" Style="cursor: hand" runat="server" CommandName="DeleteInvoiceAction"
                                ImageUrl="../Navigation/images/icon_delete.png"></asp:ImageButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField SortExpression="svc_control_number" HeaderText="Invoice_Number"></asp:BoundField>
                    <asp:BoundField SortExpression="description" HeaderText="Service_Center_Name"></asp:BoundField>
                    <asp:BoundField SortExpression="Invoice_Date" HeaderText="Invoice_Date"></asp:BoundField>
                    <asp:BoundField SortExpression="Invoice_Amount" HeaderText="Invoice_Amount"></asp:BoundField>
                    <asp:BoundField SortExpression="Invoice_Status" HeaderText="Invoice_Status"></asp:BoundField>
                    <asp:TemplateField ItemStyle-VerticalAlign="Middle" HeaderStyle-CssClass="NormalCell"
                        HeaderStyle-Wrap="false" ItemStyle-HorizontalAlign="Center" HeaderText="Invoice_Comments"
                        ItemStyle-Width="10px">
                        <ItemTemplate>
                            <asp:Image ID="Image1" runat="server" ImageUrl="../Navigation/images/edit.png" />
                            <asp:PopupControlExtender ID="PopupControlExtender1" runat="server" PopupControlID="Panel1"
                                TargetControlID="Image1" DynamicContextKey='<%# New Guid(CType(Eval("invoice_trans_id"), Byte())).ToString()%>'
                                DynamicControlID="Panel1" DynamicServiceMethod="GetInvoiceComments" Position="Bottom">
                            </asp:PopupControlExtender>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField SortExpression="Batch_number" HeaderText="Batch_number"></asp:BoundField>
                    <asp:BoundField Visible="False" HeaderText="service_center_id"></asp:BoundField>
                    <asp:BoundField Visible="False" HeaderText="svc_control_amount"></asp:BoundField>
                    <asp:BoundField HeaderText="BONUS_PAID"></asp:BoundField>
                </Columns>
                <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                <PagerStyle />
            </asp:GridView>


            <div class="dataContainer" id="InvoiceTabs" visible="false" runat="server" style="width: 100%;">
                <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0" />
                <div id="tabs" class="style-tabs">
                    <ul>
                        <li>
                            <a href="#tabsSearchInvoice" rel="noopener noreferrer">
                                <asp:Label ID="lblsearchInvoice" runat="server" CssClass="tabHeaderText">Claim Details</asp:Label>
                            </a>
                        </li>
                        <li>
                            <a href="#tabsIIBBtaxes" rel="noopener noreferrer">
                                <asp:Label ID="Label6" runat="server" CssClass="tabHeaderText">REGION_TAXES</asp:Label>
                            </a>
                        </li>
                    </ul>
                    <div id="tabsSearchInvoice">
                        <div>
                            <asp:ToolkitScriptManager ID="ScriptManager1" runat="server" CombineScripts="false">
                            </asp:ToolkitScriptManager>
                            <table width="100%" class="dataGrid">
                                <tr id="trPageSize" runat="server" height="1px">
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
                        </asp:DropDownList>
                                    </td>
                                    <td style="text-align: right;">
                                        <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div>
                            <asp:GridView ID="GridClaims" runat="server" Width="100%" AllowPaging="True" AllowSorting="False"
                                CellPadding="1" AutoGenerateColumns="False" SkinID="DetailPageGridView">
                                <SelectedRowStyle Wrap="True" />
                                <EditRowStyle Wrap="True" />
                                <AlternatingRowStyle Wrap="True" />
                                <RowStyle Wrap="True" />
                                <Columns>
                                    <asp:BoundField Visible="false" HeaderText="invoice_trans_id"></asp:BoundField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkAddClaim" runat="server" Checked="False"></asp:CheckBox>
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkAllClaims" runat="server"></asp:CheckBox>
                                        </HeaderTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkExcludeDeductible" runat="server" Checked="False"></asp:CheckBox>
                                        </ItemTemplate>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkExcludeDedAll" runat="server" />
                                        </HeaderTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField Visible="False" HeaderText="claim_id"></asp:BoundField>
                                    <asp:BoundField SortExpression="Authorization_Number" HeaderText="Authorization_Number"></asp:BoundField>
                                    <asp:BoundField SortExpression="claim_number" HeaderText="Claim_number"></asp:BoundField>
                                    <asp:BoundField SortExpression="Customer_name" HeaderText="Customer_name"></asp:BoundField>
                                    <asp:BoundField SortExpression="description" HeaderText="Service_Center_Name"></asp:BoundField>
                                    <asp:BoundField Visible="False" HeaderText="claim_modified_date"></asp:BoundField>
                                    <asp:BoundField SortExpression="Reserve_amount" HeaderText="Reserve_amount"></asp:BoundField>
                                    <asp:BoundField HeaderText="TOTAL_BONUS"></asp:BoundField>
                                    <asp:BoundField SortExpression="Batch_number" HeaderText="Batch_number"></asp:BoundField>
                                    <asp:BoundField Visible="False" HeaderText="payment_amount"></asp:BoundField>
                                    <asp:BoundField Visible="False" HeaderText="repair_date"></asp:BoundField>
                                    <asp:BoundField Visible="False" HeaderText="pickup_date"></asp:BoundField>
                                    <asp:BoundField Visible="False" HeaderText="spare_parts"></asp:BoundField>
                                    <asp:BoundField Visible="False" HeaderText="selected"></asp:BoundField>
                                    <asp:BoundField Visible="False" HeaderText="invoice_trans_detail_id"></asp:BoundField>
                                    <asp:BoundField Visible="False" HeaderText="exclude_deductible"></asp:BoundField>
                                    <asp:BoundField SortExpression="claim_extended_status" HeaderText="claim_extended_status"></asp:BoundField>
                                </Columns>
                                <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                                <PagerStyle />
                            </asp:GridView>
                        </div>
                    </div>
                    <div id="tabsIIBBtaxes">
                        <Elita:UserControlInvoiceRegionTaxes runat="server" ID="IIBBTaxes"
                            RequestIIBBTaxesData="IIBBTaxes_RequestIIBBTaxes">
                        </Elita:UserControlInvoiceRegionTaxes>
                    </div>
                </div>
            </div>


        </div>
        <asp:Panel ID="Panel1" runat="server">
        </asp:Panel>
        <div class="btnZone">
            <asp:Button ID="btnCancelSearch_WRITE" runat="server" SkinID="AlternateLeftButton"
                Text="Back" Visible="false"></asp:Button>
            <asp:Button ID="btnNew" runat="server" SkinID="AlternateLeftButton" Text="NEW"></asp:Button>
            <asp:Button ID="btnCancelEdit_WRITE" runat="server" SkinID="AlternateLeftButton"
                Text="Back" Visible="false"></asp:Button>
            <asp:Button ID="btnSave_WRITE" runat="server" SkinID="AlternateLeftButton" Text="SAVE"
                Visible="false"></asp:Button>
            <asp:Button ID="btnNEXT_WRITE" runat="server" SkinID="AlternateLeftButton" Text="NEXT"
                Visible="false"></asp:Button>
        </div>
    </div>

</asp:Content>
