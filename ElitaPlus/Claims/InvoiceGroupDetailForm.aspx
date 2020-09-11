<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="InvoiceGroupDetailForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.InvoiceGroupDetailForm" MasterPageFile="../Navigation/masters/ElitaBase.Master"
    Theme="Default" EnableViewState="true" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="asp" %>
<%@ Register TagPrefix="Elita" TagName="MessageController" Src="~/Common/MessageController.ascx" %>
<asp:Content  ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content  ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
</asp:Content>
<asp:Content  ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true">
        <Scripts>
            <asp:ScriptReference Path="~/Navigation/scripts/ComunaSuggest.js" />
        </Scripts>
       
    </asp:ScriptManager>
           
    
    <div class="dataContainer">
        <table width="100%" border="0" class="formGrid" id="invgrpdetailtable" runat="server"
            cellspacing="0" cellpadding="0" style="padding-left: 0px;">
            <tr style="margin-bottom: 1px auto;">
                <td class="borderLeft">
                    <asp:Label ID="lblreceiptdate" runat="server">RECEIPT_DATE</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtReceiptdate" runat="server" SkinID="MediumTextBox" ReadOnly="true" AutoPostBack="False"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblgrpnumber" runat="server">GROUP_NUMBER</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtgrpnumber" runat="server" SkinID="MediumTextBox" ReadOnly="true"
                        AutoPostBack="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td class="borderLeft">
                    <asp:Label ID="lbltotalamount" runat="server">TOTAL_AMOUNT</asp:Label>:
                </td>
                <td>
                    <asp:TextBox ID="txttotalamount" runat="server" SkinID="MediumTextBox" ReadOnly="true"
                        AutoPostBack="False"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblgroupcount" runat="server">GROUP_COUNT</asp:Label>:
                </td>
                <td>
                    <asp:TextBox ID="txtgroupcount" runat="server" SkinID="SmallTextBox" AutoPostBack="False"
                        ReadOnly="true"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <asp:Panel ID="pnlDetailView" runat="server">
        <div class="dataContainer">
               <div class="Pages">
                <div id="tabs" class="style-tabs">
                    <ul>
                        <li><a href="#tabsInvoices" rel="noopener noreferrer">
                            <asp:Label ID="Label4" runat="server" CssClass="tabHeaderText">INVOICES</asp:Label></a></li>
                    </ul>
                    <div id="tabsInvoices">
                        <table width="100%" class="dataGrid">
                            <tr id="trPageSize" runat="server">
                                <td class="bor" align="left">
                                    <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>: &nbsp;
                                        <asp:DropDownList ID="cboPageSize" runat="server" Width="50px" AutoPostBack="true"
                                            SkinID="SmallDropDown">
                                            <asp:ListItem Value="5">5</asp:ListItem>
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="25">25</asp:ListItem>
                                            <asp:ListItem Value="30" Selected="True">30</asp:ListItem>
                                            <asp:ListItem Value="35">35</asp:ListItem>
                                            <asp:ListItem Value="40">40</asp:ListItem>
                                            <asp:ListItem Value="45">45</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                        </asp:DropDownList>
                                </td>
                                <td class="bor" align="right">
                                    <asp:Label ID="lblRecordCounts" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                        <div class="Page" runat="server" id="InvoiceTabPanel_WRITE" style="display: block; overflow: hidden">
                            <asp:GridView ID="InvoicesGrid" runat="server" Width="100%" AllowPaging="true" AllowSorting="true"
                                SkinID="DetailPageGridView" OnItemCommand="ItemCommand">
                                <SelectedRowStyle Wrap="True" />
                                <EditRowStyle Wrap="True" />
                                <AlternatingRowStyle Wrap="True" />
                                <RowStyle Wrap="True" />
                                <HeaderStyle />
                                <Columns>

                                    <asp:BoundField SortExpression="Service_center_description" HeaderText="REMITTANCE_VENDOR" HtmlEncode="False" />
                                    <asp:BoundField SortExpression="invoice_number" HeaderText="INVOICE_NUMBER" HtmlEncode="False" />
                                    <asp:BoundField SortExpression="invoice_amount" HeaderText="INVOICE_AMOUNT" HtmlEncode="False" />
                                    <asp:TemplateField SortExpression="line_item_amount" HeaderText="LINE_ITEM_TOTAL">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="btnEditItem" runat="server" CommandName="selectAction" CommandArgument="<%#Container.DisplayIndex %>" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField SortExpression="invoice_date" HeaderText="INVOICE_DATE" HtmlEncode="False" />
                                    <asp:BoundField SortExpression="invoice_status" HeaderText="STATUS" HtmlEncode="False" />
                                    <asp:TemplateField ShowHeader="false">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btn_edit" Style="cursor: hand" runat="server" ImageUrl="~/App_Themes/Default/Images/edit.png"
                                                Visible="true" CommandName="EditRecord" ImageAlign="AbsMiddle" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField ShowHeader="false">
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btn_delete" Style="cursor: hand;" ImageUrl="~/App_Themes/Default/Images/icon_delete.png"
                                                runat="server" CommandName="DeleteRecord" CommandArgument="<%#Container.DisplayIndex %>"
                                                ImageAlign="AbsMiddle"></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField SortExpression="Invoice_id" HeaderText="INVOICE_ID" Visible="false" />
                                    <asp:BoundField SortExpression="Invoice_group_detail_id" HeaderText="INVOICE_GROUP_DETAIL_ID"
                                        Visible="false" />
                                </Columns>
                                <PagerSettings PageButtonCount="10" Mode="Numeric" Position="TopAndBottom" />
                            </asp:GridView>
                        </div>
                        <div class="btnZone">
                            <asp:LinkButton runat="server" ID="dummybutton"></asp:LinkButton>
                            <asp:Button Text="ADD" runat="server" ID="addBtnNew" SkinID="AlternateLeftButton"></asp:Button>
                            <asp:LinkButton ID="Dummybtn" runat="server" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
        <div class="btnZone">
       
                <asp:Button ID="btnBack" runat="server" SkinID="AlternateLeftButton" Text="BACK" Visible ="False" />
                <asp:Button ID="btnAdd" runat="server" SkinID="AlternateLeftButton" Text="NEW" />
                <asp:Button ID="btnSave" runat="server" SkinID="PrimaryRightButton" Text="SAVE" />
                <asp:Button ID="btnUndo" runat="server" SkinID="AlternateRightButton" Text="UNDO" />
                <asp:Button ID="btnDelete" runat="server" SkinID="AlternateRightButton" Text="DELETE" />
            </div>
     
        <div id="AddNewContainer" style="width: 80%;">
            <asp:ModalPopupExtender runat="server" ID="mdlPopup" TargetControlID="dummybutton"
                PopupControlID="pnlPopup" DropShadow="True" BackgroundCssClass="ModalBackground"
                CancelControlID="mdlClose" BehaviorID="addNewModal" PopupDragHandleControlID="BodyPlaceHolder"
                RepositionMode="RepositionOnWindowResizeAndScroll" Y="50">
            </asp:ModalPopupExtender>
            <asp:Panel ID="pnlPopup" runat="server" DefaultButton="btnSearch" Style="display: none; width: 75%; overflow: auto">
                <div id="light" class="overlay_message_content" style="width: 75%; top: 25px; overflow: hidden;">
                    <p class="modalTitle">
                        <asp:Label ID="lblModalTitle" runat="server" Text="NEW_INVOICE_RECORD"></asp:Label>
                        <asp:ImageButton ImageUrl="~/App_Themes/Default/Images/icon_modalClose.png" runat="server"
                            ID="mdlClose" CssClass="floatR" AlternateText="Close" />
                    </p>
                    <Elita:MessageController runat="server" ID="moMessageController" />
                    <table width="100%" border="0" cellpadding="0" cellspacing="0" class="formGrid">
                        <tr>
                            <td align="left" class="borderLeft">
                                <asp:Label ID="lblvendorname" runat="server" Text="Service_Center"></asp:Label>:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlVendorName" runat="server" SkinID="MediumDropDown">
                                </asp:DropDownList>
                            </td>
                            <td>
                                <asp:Label ID="Label1" runat="server" Text="INVOICE_NUMBER"></asp:Label>:
                            </td>
                            <td>
                                <asp:TextBox ID="txtInvoiceNumber" runat="server" SkinID="MediumTextBox">
                                </asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="borderLeft">
                                <asp:Label ID="lblinvoiceamount" runat="server" Text="INVOICE_AMOUNT"></asp:Label>:
                            </td>
                            <td>
                                <asp:TextBox ID="txtInvoiceAmount" runat="server" SkinID="MediumTextBox">
                                </asp:TextBox>
                            </td>
                            <td>
                                <asp:Label ID="lblInvoiceDate" runat="server" Text="INVOICE_DATE"></asp:Label>:
                            </td>
                            <td>
                                <asp:TextBox ID="txtInvoiceDate" runat="server" SkinID="smallTextBox">
                                </asp:TextBox>
                                <asp:ImageButton ID="ImgInvoiceDate" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png"
                                    align="absmiddle" alt="" Width="20" Height="20" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left" class="borderLeft">
                                <asp:Label ID="lblInvoiceStatus" runat="server" Text="Invoice_Status"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlInvoicestatus" runat="server" SkinID="MediumDropDown">
                                </asp:DropDownList>
                            </td>
                            <td>
                            </td>
                            <td>
                            </td>
                        </tr>
                        <tr>
                            <td id="Td2" align="right" colspan="4" runat="server">
                                <asp:Button ID="btnSearch" runat="server" TabIndex="3" SkinID="PrimaryRightButton"
                                    Text="Search"></asp:Button>
                                <asp:Button ID="btncancelSearch" runat="server" TabIndex="1" SkinID="AlternateRightButton"
                                    Text="CANCEL_SEARCH"></asp:Button>
                                <asp:Button ID="btnClearSearch" runat="server" TabIndex="2" SkinID="AlternateRightButton"
                                    Text="Clear" CausesValidation="false"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td id="Td1" align="right" colspan="4" runat="server">
                                <asp:Button ID="btnNewInvCancel" runat="server" Text="CANCEL" SkinID="AlternateLeftButton" />
                                &nbsp;
                                <asp:Button ID="btnEditInvSave" runat="server" Text="SAVE" SkinID="PrimaryRightButton"
                                    CausesValidation="false" />
                            </td>
                        </tr>
                    </table>
                    <div id="divpgsize" runat="server" class="dataContainer">
                        <h2 class="dataGridHeader">
                            Search Results For Invoices</h2>
                        <table width="100%" class="dataGrid">
                            <tr id="trpgsize" runat="server" visible="false">
                                <td valign="top" align="left">
                                    <asp:Label ID="lblPgSize" runat="server" Visible="false">Page_Size</asp:Label> &nbsp;
                                    <asp:DropDownList ID="cboPgSize" runat="server" AutoPostBack="true" Width="50px" Visible="false">
                                        <asp:ListItem Selected="True" Value="5">5</asp:ListItem>
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
                                <td align="right" colspan="4">
                                    <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                                </td>
                            </tr>
                        </table>
                    </div>
                    <div class="dataContainer">
                        <div id="dvBottom" runat="server" style="overflow: hidden;">
                            <asp:GridView ID="ReconciledInvoiceSearchgv" runat="server" Width="100%" AutoGenerateColumns="False"
                                AllowPaging="true" SkinID="DetailPageGridView" EnableViewState="true" >
                                <SelectedRowStyle Wrap="True" />
                                <EditRowStyle Wrap="True" />
                                <AlternatingRowStyle Wrap="True" />
                                <RowStyle Wrap="True" />
                                <HeaderStyle />
                                <Columns>
                                    <asp:TemplateField Visible="True" ItemStyle-HorizontalAlign="Left">
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkbxinvoice" Checked="false" runat="server" Onclick = 'check_click()'/>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField SortExpression="remittance_vendor" HeaderText="REMITTANCE_VENDOR" />
                                    <asp:BoundField SortExpression="invoice_num" HeaderText="INVOICE_NUMBER" />
                                    <asp:BoundField SortExpression="inv_amount" HeaderText="INVOICE_AMOUNT" />
                                    <asp:BoundField SortExpression="inv_date" HeaderText="INVOICE_DATE" />
                                    <asp:BoundField SortExpression="status" HeaderText="STATUS" />
                                    <asp:BoundField SortExpression="invoice_id" HeaderText="INVOICE_ID"
                                        Visible="false" />
                                </Columns>
                                <PagerSettings PageButtonCount="5" Mode="Numeric" Position="TopAndBottom" />
                            </asp:GridView>
                        </div>
                    </div>
                    <div id="btnDiv" runat="server" class="btnZone">
                        <asp:Button ID="btnNewItemAdd" runat="server" Text="ADD" SkinID="PrimaryRightButton" style="display:none" />
                    </div>
                </div>
            </asp:Panel>
        </div>
        <div id="AddlineitemContainer" style="width: 80%;">
            <ajaxToolkit:ModalPopupExtender runat="server" ID="mdlLineItem" TargetControlID="Dummybtn"
                PopupControlID="LineItempnlPopup" DropShadow="True" BackgroundCssClass="ModalBackground"
                PopupDragHandleControlID="BodyPlaceHolder" BehaviorID="modalbehaviour" CancelControlID="modlClose"
                RepositionMode="RepositionOnWindowResizeAndScroll">
            </ajaxToolkit:ModalPopupExtender>
            
            <asp:Panel ID="LineItempnlPopup" runat="server" Style="display: none; width: 100%;
                overflow: auto">
                <div id="divlineitems" class="overlay_message_content" style="width: 75%; top: 25px;">
                    <p class="modalTitle" style="overflow: auto;">
                        <asp:Label ID="lblModlTitle" runat="server" Text="INVOICE_LINE_ITEMS"></asp:Label>
                        <asp:ImageButton ImageUrl="~/App_Themes/Default/Images/icon_modalClose.png" runat="server"
                            ID="modlClose" CssClass="floatR" AlternateText="Close" />
                    </p>
                       <Elita:MessageController runat="server" ID="molineitemmsgcontroller" />
                    <div runat="server" id="LineItemsPanel_WRITE" style="display: inline; overflow: auto" class="dataContainer">
                        <asp:LinkButton ID="btnAddstandardLineItems" runat="server" Text="ADD_STANDARD_LINE_ITEMS"></asp:LinkButton>
                        &nbsp;
                       
                                <asp:DataGrid ID="Lineitemsgv" runat="server" Width="100%" AllowPaging="true" AllowSorting="false"
                                    SkinID="DetailPageDataGrid" AutoGenerateColumns="false">
                                    <SelectedItemStyle Wrap="False" />
                                    <EditItemStyle Wrap="False" />
                                    <AlternatingItemStyle Wrap="False" />
                                    <ItemStyle Wrap="False" />
                                    <HeaderStyle />
                                    <Columns>
                                    <asp:TemplateColumn Visible="false">
                                            <HeaderStyle Width="10%"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblInvoiceitemId" runat="server" Visible="true"></asp:Label>
                                               </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn Visible="false">
                                            <HeaderStyle Width="10%"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblClaimAuthId" runat="server" Visible="true"></asp:Label>
                                             </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="LINE_ITEM_TYPE">
                                            <HeaderStyle Width="15%"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lbllineitemtype" runat="server"></asp:Label></ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" RenderMode="inline">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlserviceclass" runat="server" Visible="true" OnSelectedIndexChanged="ddlServiceClass_SelectedIndexChanged"
                                                            AutoPostBack="true">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </EditItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="LINE_ITEM_DESCRIPTION">
                                            <HeaderStyle Width="15%"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lbllineitemdesc" runat="server"></asp:Label></ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always" RenderMode="inline">
                                                    <ContentTemplate>
                                                        <asp:DropDownList ID="ddlservicetype" runat="server" Visible="true">
                                                        </asp:DropDownList>
                                                    </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </EditItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="LINE_ITEM_AMOUNT">
                                            <HeaderStyle Width="10%"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lbllineitemamount" runat="server"></asp:Label></ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="txtlineitemamt" runat="server" Visible="true"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="CLAIM_NUMBER">
                                            <HeaderStyle Width="10%"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblclaimnumber" runat="server"></asp:Label></ItemTemplate>
                                            <EditItemTemplate>
                                               <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Always" RenderMode="inline">
                                                    <ContentTemplate>
                                            <asp:TextBox ID="txtclaimnumber" runat="server" Visible="true" AutoPostBack="true" ></asp:TextBox>
                                                <asp:AutoCompleteExtender ID="AutoCompleteExtender1" runat="server" TargetControlID="txtclaimnumber"
                                                    CompletionSetCount="20" FirstRowSelected="false" MinimumPrefixLength="2" servicemethod="GetCompletionList"
                                                      OnClientItemSelected="getclaimnumber" UseContextKey="true" CompletionInterval="100">
                                                </asp:AutoCompleteExtender>
                                                </ContentTemplate>
                                                </asp:UpdatePanel>
                                               
                                            </EditItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="AUTHORIZATION_NUMBER">
                                            <HeaderStyle Width="10%"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblauthnumber" runat="server"></asp:Label></ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always" RenderMode="inline">
                                                    <ContentTemplate>
                                                <asp:TextBox ID="txtauthnumber" runat="server" Visible="true" ></asp:TextBox>
                                               </ContentTemplate>
                                               </asp:UpdatePanel>
                                            </EditItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="VENDOR_SKU">
                                            <HeaderStyle Width="10%"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblvendorsku" runat="server"></asp:Label></ItemTemplate>
                                            <EditItemTemplate>
                                                 <asp:UpdatePanel ID="UpdatePanel5" runat="server" RenderMode="Block" UpdateMode="Always">
                                                    <ContentTemplate>
                                                     <asp:DropDownList ID="ddlvendorsku" runat="server" Visible="true" AutoPostBack="true" OnSelectedIndexChanged="ddlvendorsku_SelectedIndexChanged"></asp:DropDownList>
                                               </ContentTemplate>
                                                </asp:UpdatePanel>
                                            </EditItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn HeaderText="VENDOR_SKU_DESCRIPTION">
                                            <HeaderStyle Width="10%"></HeaderStyle>
                                            <ItemTemplate>
                                                <asp:Label ID="lblskudesc" runat="server"></asp:Label></ItemTemplate>
                                            <EditItemTemplate>                                                                                                    
                                                    <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Always" RenderMode="Block">
                                                    <ContentTemplate>
                                                <asp:TextBox ID="txtvendorskudesc" runat="server" Visible="true"></asp:TextBox>
                                                    </ContentTemplate>
                                                   
                                                </asp:UpdatePanel>
                                            </EditItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle Width="5%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btn_edit" Style="cursor: hand" runat="server" ImageUrl="~/App_Themes/Default/Images/edit.png"
                                                    Visible="true" CommandName="EditRecord" ImageAlign="AbsMiddle"></asp:ImageButton>
                                            </ItemTemplate>
                                        </asp:TemplateColumn>
                                        <asp:TemplateColumn>
                                            <HeaderStyle Width="5%"></HeaderStyle>
                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="btn_delete" Style="cursor: hand;" ImageUrl="~/App_Themes/Default/Images/icon_delete.png"
                                                    runat="server" CommandName="DeleteRecord" ImageAlign="AbsMiddle"></asp:ImageButton></ItemTemplate>
                                        </asp:TemplateColumn>
                                    </Columns>
                                    <PagerStyle PageButtonCount="5" Mode="Numericpages" Position="TopAndBottom" />
                                </asp:DataGrid>
                            
                        <table width="100%" border="0" cellpadding="0" cellspacing="0" class="dataGrid">
                            <tr>
                                <td>
                                    <asp:Button ID="btnnew_lineitem" runat="server" SkinID="Alternateleftbutton" Text="New" />
                                    <asp:Button ID="btnsave_lineitem" runat="server" SkinID="PrimaryRightButton" Text="Save" />
                                    <asp:Button ID="btnundo_lineitem" runat="server" SkinID="AlternateRightButton" Text="Undo" />
                                </td>
                            </tr>
                        </table>
                    </div>
                </div>
            </asp:Panel>
           
        </div>
    </asp:Panel>
    <asp:HiddenField ID="hdnclaimnum" runat="server" OnValueChanged="populatevendorsku"   />
    <asp:HiddenField ID="hdnrowNumber" runat="server" />
    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
        runat="server" designtimedragdrop="261" />
        <script language="text/javascript" type="text/javascript">
            function getclaimnumber(source, eventArgs) {
                document.getElementById('ctl00_BodyPlaceHolder_hdnclaimnum').value = eventArgs.get_text();

                var obj = source._id;
                var temp = obj.lastIndexOf('ctl');
                var temp1 = obj.lastIndexOf('_');
                var rowno = obj.substring(temp + 3, temp1);
                document.getElementById('ctl00_BodyPlaceHolder_hdnrowNumber').value = rowno;

                //var hdnValueId = "<%= hdnclaimnum.ClientID %>";
                //__doPostBack(hdnValueId, "");
            }

            function check_click() {
                               
                var grid = document.getElementById("<%= ReconciledInvoiceSearchgv.ClientID %>");
                var addbtn = document.getElementById("<%= btnNewItemAdd.ClientID %>");
                //variable to contain the cell of the grid
                var cell;
                var checked = false;

                if (grid.rows.length > 0) {
                    //loop starts from 1. rows[0] points to the header.
                    for (i = 1; i < grid.rows.length; i++) {
                        //get the reference of first column
                        cell = grid.rows[i].cells[0];
                              
                        //if childNode type is CheckBox
                        if (cell.childNodes[0].type == "checkbox") {
                            if (cell.childNodes[0].checked) {
                                checked = true;                               
                            }                                                          
                        }
                        else if(cell.children[0].type == "checkbox"){ //IE 11 logic
                            if (cell.children[0].checked) {
                                checked = true;
                            }
                        }
                    }
                }

                if (checked) {
                    addbtn.style.display = "inline";
                }
                else {
                    addbtn.style.display = "none";
                }
           }
        </script>
 
</asp:Content>
