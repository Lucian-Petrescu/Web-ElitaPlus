<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PriceListDetailForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.PriceListDetailForm" Theme="Default"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" EnableViewState="true" %>

<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="url" TagName="UserControlVendorAvailable" Src="../common/UserControlAvailableSelected_New.ascx" %>
<asp:Content ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <link type="text/css" href="../Navigation/styles/jquery-ui.min.css" rel="stylesheet" />
    <link type="text/css" href="../Navigation/styles/jquery-ui.elita.css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.12.4.min.js"> </script>
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-ui.min.js"> </script>
    <script type="text/javascript">
        $(function () {
            $("#tabs").tabs({
                activate: function () {
                    var selectedTab = $('#tabs').tabs('option', 'active');
                    $("#<%= hdnSelectedTab.ClientID %>").val(selectedTab);
                },
                active: document.getElementById('<%= hdnSelectedTab.ClientID %>').value
            });

        });
    </script>

    <style type="text/css">
        .dataEditBox {
            border-left: 10px solid #0066CC;
        }
        .auto-style1 {
            width: 223px;
        }
        .auto-style2 {
            width: 95px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <script language="javascript" type="text/javascript">
        function validateEquipmentList() {
            var obj = document.getElementById("ctl00_BodyPlaceHolder_ddlEquipmentlist");
            var btn = document.getElementById("ctl00_BodyPlaceHolder_btnAddEquipment");
            var str = obj.options[obj.selectedIndex].text;
            alert(str);
            if (str == "") {
                btn.disalbed = true;
            }
            else {
                btn.disalbed = false;
            }
            return false;
        }

        function checkAll(objRef) {

            var GridView = objRef.parentNode.parentNode.parentNode;

            var inputList = GridView.getElementsByTagName("input");

            for (var i = 0; i < inputList.length; i++) {

                //Get the Cell To find out ColumnIndex

                var row = inputList[i].parentNode.parentNode;

                if (inputList[i].type == "checkbox" && objRef != inputList[i]) {

                    if (objRef.checked) {

                        inputList[i].checked = true;

                    }

                    else {

                        inputList[i].checked = false;

                    }

                }

            }

        }

    </script>
    <div class="dataEditBox">
        <table border="0" class="formGrid" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td nowrap="nowrap" align="right">
                    <asp:Label ID="lblCode" runat="server" Text="PRICE_LIST_CODE"></asp:Label>
                </td>
                <td nowrap="nowrap">
                    <asp:TextBox ID="txtCode" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                </td>
                <td nowrap="nowrap" align="right">
                    <asp:Label ID="lblDescription" runat="server" Text="PRICE_LIST_DESCRIPTION"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtDescription" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td nowrap="nowrap" align="right">
                    <asp:Label ID="lblCountry" runat="server" Text="COUNTRY"></asp:Label>
                </td>
                <td nowrap="nowrap">
                    <asp:DropDownList ID="ddlCountry" runat="server" SkinID="MediumDropDown" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td nowrap="nowrap" align="right">
                    <asp:Label ID="lblDefaultCurrency" runat="server" Text="DEFAULT_CURRENCY"></asp:Label>
                </td>
                <td nowrap="nowrap">
                    <asp:DropDownList ID="ddlDefaultCurrency" runat="server" SkinID="SmallDropDown" AutoPostBack="true"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td nowrap="nowrap" align="right">
                    <asp:Label ID="lblEffectiveDate" runat="server" Text="EFFECTIVE_DATE"></asp:Label>
                </td>
                <td nowrap="nowrap">
                    <asp:TextBox ID="txtEffective" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                    <asp:ImageButton ID="btneffective" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png"
                        align="absmiddle" alt="" Width="20" Height="20" />
                </td>
                <td nowrap="nowrap" align="right">
                    <asp:Label ID="lblExpirationDate" runat="server" Text="EXPIRATION_DATE"></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="txtExpirationDate" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                    <asp:ImageButton ID="btnExpiration" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png"
                        align="absmiddle" alt="" Width="20" Height="20" />
                </td>
            </tr>
            <tr>
                <td nowrap="nowrap" align="right">
                    <asp:Label ID="lblManageInventory" runat="server" Text="MANAGE_INVENTORY"></asp:Label>
                </td>
                <td>
                    <asp:DropDownList ID="ddlManageInventory" runat="server" SkinID="SmallDropDown" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto">
    </asp:ScriptManager>
    <%--<asp:Panel ID="pnlDetailView" runat="server">--%>
    <div class="dataContainer" style="width: 100%;">
        <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0" />
        <div id="tabs" class="style-tabs">
            <ul>
                <li><a href="#tabsService">
                    <asp:Label ID="Label4" runat="server" CssClass="tabHeaderText">SERVICE</asp:Label></a></li>
                <li><a href="#tabsVendor">
                    <asp:Label ID="Label6" runat="server" CssClass="tabHeaderText">VENDOR</asp:Label></a></li>
                <li><a href="#tabsPendingApprovals">
                    <asp:Label ID="Label2" runat="server" CssClass="tabHeaderText">PENDING_APPROVALS</asp:Label></a></li>
            </ul>
            <div class="Pages">
                <div id="tabsService">
                    <table width="100%" border="1" class="dataGrid dataGridSpacer" cellpadding="0" cellspacing="0"
                        style="padding-top: 5px; padding-bottom: 5px;">
                        <tr>
                            <td align="right">
                                <asp:Label runat="server" ID="lblEquipmentList" Text="EQUIPMENT_LIST"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlEquipmentlist" runat="server" TabIndex="5" Width="210px">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                <asp:Label runat="server" ID="lblServiceClass" Text="SERVICE_CLASS"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlServiceClass" runat="server" TabIndex="5" Width="210px"
                                    AutoPostBack="true">
                                </asp:DropDownList>
                            </td>
                            <td align="right">
                                <asp:Label runat="server" ID="lblServiceType" Text="SERVICE_TYPE"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="ddlServiceType" runat="server" TabIndex="5" Width="210px">
                                </asp:DropDownList>
                            </td>
                            <td align="left">
                                <asp:Button ID="btnAddEquipment" Text="ADD_EQUIPMENT" SkinID="PrimaryRightButton"
                                    runat="server"></asp:Button>
                            </td>
                        </tr>
                        <tr>
                            <td align="right">
                                <asp:Label ID="lblNewEquipVendorSKU" runat="server" Text="VENDOR_SKU"></asp:Label>
                            </td>
                            <td align="left">
                                <asp:TextBox ID="txtNewEquipVendorSKU" runat="server" TabIndex="5" SkinID="SmallTextBox"></asp:TextBox>
                            </td>
                            <td align="right">
                                <asp:Label ID="lblNewEquipSKUDesciption" runat="server" Text="DESCRIPTION"></asp:Label>
                            </td>
                            <td align="left" colspan="4">
                                <asp:TextBox ID="txtNewEquipSKUDescription" runat="server" TabIndex="5" SkinID="MediumTextBox"></asp:TextBox>
                            </td>
                        </tr>
                        <%--</table>
                        <table width="100%" border="0">--%>
                        <tr id="trPageSize" runat="server">
                            <td align="right">
                                <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>: &nbsp;
                                        
                            </td>
                            <td align="left">
                                <asp:DropDownList ID="cboPageSize" runat="server" Width="50px" AutoPostBack="true"
                                    SkinID="SmallDropDown">
                                    <asp:ListItem Value="5">5</asp:ListItem>
                                    <asp:ListItem Value="10">10</asp:ListItem>
                                    <asp:ListItem Value="15">15</asp:ListItem>
                                    <asp:ListItem Value="20">20</asp:ListItem>
                                    <asp:ListItem Value="25">25</asp:ListItem>
                                    <asp:ListItem Value="30">30</asp:ListItem>
                                    <asp:ListItem Value="35">35</asp:ListItem>
                                    <asp:ListItem Value="40">40</asp:ListItem>
                                    <asp:ListItem Value="45">45</asp:ListItem>
                                    <asp:ListItem Value="50">50</asp:ListItem>
                                </asp:DropDownList></td>
                            <td>&nbsp;</td>
                            <td align="right" colspan="4">
                                <asp:Label ID="lblRecordCounts" runat="server"></asp:Label>
                            </td>
                        </tr>
                    </table>
                    <div style="width: 100%; overflow-y: scroll; height: 100%;">
                        <asp:GridView ID="Grid" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
                            DataKeyNames="price_list_detail_id" AllowSorting="true" SkinID="DetailPageGridView">
                            <SelectedRowStyle Wrap="True" />
                            <EditRowStyle Wrap="True" />
                            <AlternatingRowStyle Wrap="True" />
                            <RowStyle Wrap="True" />
                            <Columns>
                                <asp:TemplateField>
        <ItemTemplate>
            <%--<asp:HyperLink ID="lnkViewhistory" runat="server"></asp:HyperLink>--%>
            <%--<asp:LinkButton ID="dummybtnhistory" runat="server"></asp:LinkButton>
            <asp:LinkButton ID="lnkViewhistory" runat="server" Text="View History" CommandName="ViewHistoryRecord" 
                CommandArgument='<%#Eval("price_list_detail_id") %>'></asp:LinkButton>
            --%>
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
                                <asp:TemplateField ShowHeader="false">
                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:ImageButton ID="btn_edit" Style="cursor: hand" runat="server" ImageUrl="~/App_Themes/Default/Images/edit.png"
                                            Visible="true" CommandName="EditRecord" ImageAlign="AbsMiddle" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:BoundField DataField="service_class_code" SortExpression="service_class_code"
                                    ReadOnly="true" HeaderText="SERVICE_CLASS" HeaderStyle-HorizontalAlign="Center"
                                    HtmlEncode="false" />
                                <asp:BoundField DataField="service_type_code" SortExpression="service_type_code"
                                    ReadOnly="true" HeaderText="SERVICE_TYPE" HeaderStyle-HorizontalAlign="Center"
                                    HtmlEncode="false" />
                                <asp:BoundField DataField="service_level_code" SortExpression="service_level_code"
                                    ReadOnly="true" HeaderText="SERVICE_LEVEL" HeaderStyle-HorizontalAlign="Center"
                                    HtmlEncode="false" />
                                <asp:BoundField DataField="risk_type_code" SortExpression="risk_type_code" ReadOnly="true"
                                    HeaderText="RISK_TYPE" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                <asp:BoundField DataField="equipment_code" SortExpression="equipment_code" ReadOnly="true"
                                    HeaderText="EQUIPMENT_CLASS" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                <asp:BoundField DataField="make" SortExpression="make" ReadOnly="true" HeaderText="MAKE"
                                    HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                <asp:BoundField DataField="model" SortExpression="model" ReadOnly="true" HeaderText="MODEL"
                                    HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                <asp:BoundField DataField="part_description" SortExpression="part_description" ReadOnly="true" HeaderText="PART_DESCRIPTION"
                                    HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                <asp:BoundField DataField="condition_type_code" SortExpression="condition_type_code"
                                    ReadOnly="true" HeaderText="CONDITION" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                <asp:BoundField DataField="vendor_sku" SortExpression="vendor_sku" ReadOnly="true"
                                    HeaderText="VENDOR_SKU" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                <asp:BoundField DataField="vendor_sku_description" SortExpression="vendor_sku_description"
                                    ReadOnly="true" HeaderText="DESCRIPTION" HeaderStyle-HorizontalAlign="Center"
                                    HtmlEncode="false" />
                                <asp:BoundField DataField="requested_by" SortExpression="requested_by" ReadOnly="true" HeaderText="REQUESTED_BY"
                                    HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                <asp:BoundField DataField="requested_date" SortExpression="requested_date" ReadOnly="true" HeaderText="REQUESTED_DATE"
                                    HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                <asp:BoundField DataField="status_xcd" SortExpression="status_xcd" ReadOnly="true"
                                    HeaderText="STATUS" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                <asp:BoundField DataField="status_date" SortExpression="status_date"
                                    ReadOnly="true" HeaderText="STATUS_DATE" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                <asp:BoundField DataField="manufacturer_origin_desc" SortExpression="manufacturer_origin_desc" ReadOnly="true" HeaderText="MANUFACTURER_ORIGIN"
                                    HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                <asp:BoundField DataField="price_with_symbol" SortExpression="price_with_symbol" ReadOnly="true" HeaderText="PRICE"
                                    HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                <asp:BoundField DataField="vendor_quantity" SortExpression="vendor_quantity" ReadOnly="true"
                                    HeaderText="QUANTITY" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                <asp:BoundField DataField="price_low_range_with_symbol" SortExpression="price_low_range_with_symbol"
                                    ReadOnly="true" HeaderText="LOW_PRICE" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                <asp:BoundField DataField="price_high_range_with_symbol" SortExpression="price_high_range_with_symbol"
                                    ReadOnly="true" HeaderText="HIGH_PRICE" HeaderStyle-HorizontalAlign="Center"
                                    HtmlEncode="false" />
                               <%-- <asp:BoundField DataField="staus_xcd" SortExpression="staus_xcd" ReadOnly="true"
                                    HeaderText="STATUS" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />--%>
                                <asp:BoundField DataField="effective" SortExpression="effective" ReadOnly="true"
                                    HeaderText="EFFECTIVE" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                <asp:BoundField DataField="expiration" SortExpression="expiration" ReadOnly="true"
                                    HeaderText="EXPIRATION" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                <asp:BoundField DataField="price_list_detail_id" ReadOnly="true" Visible="false" />
                            </Columns>
                            <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                            <PagerStyle />
                        </asp:GridView>

                        <div class="btnZone">
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td>
                                        <asp:LinkButton runat="server" ID="dummybutton"></asp:LinkButton>
                                        <asp:Button ID="addBtnNewListItem" runat="server" SkinID="AlternateLeftButton" Text="NEW" />
                                         </td>
                                    <td>
                                        <asp:LinkButton runat="server" ID="dummybutton1"></asp:LinkButton>
                                        <asp:Button ID="btnSubmitforApproval" runat="server" SkinID="AlternateLeftButton" Text="SUBMIT_FOR_APPROVAL" />
                                    </td>                        
                                </tr>
                                
                            </table>
                        </div>
                </div>
                    </div>
                <div id="tabsVendor">
                    <table id="moOutTable" runat="server" border="0" class="availableSelectBox">
                        <tr>
                            <td align="left" valign="top" width="30%">
                                <strong>
                                    <asp:Label ID="moSelectedTitle" runat="server" Width="100%">SELECTED VENDORS</asp:Label></strong>
                                <asp:ListBox ID="moSelectedList" runat="server" Height="135px" SelectionMode="Multiple"
                                    Rows="6"></asp:ListBox>
                            </td>
                        </tr>
                    </table>
                </div>
                    <div id="tabsPendingApprovals">
                          <div>
                               <table width="100%" border="1" class="dataGrid dataGridSpacer" cellpadding="0" cellspacing="0"
                        style="padding-top: 5px; padding-bottom: 5px;">
                            
                                <tr id="trPageSizePendingApprovals" runat="server">
                                    <td align="right" class="auto-style2">
                                        <asp:Label ID="lblPageSizePendingApprovals" runat="server">Page_Size</asp:Label>: &nbsp;
                                        
                                    </td>
                                    <td align="left">
                                        <asp:DropDownList ID="cboPageSizePendingApproval" runat="server" Width="50px" AutoPostBack="true"
                                            SkinID="SmallDropDown">
                                            <asp:ListItem Value="5">5</asp:ListItem>
                                            <asp:ListItem Value="10">10</asp:ListItem>
                                            <asp:ListItem Value="15">15</asp:ListItem>
                                            <asp:ListItem Value="20">20</asp:ListItem>
                                            <asp:ListItem Value="25">25</asp:ListItem>
                                            <asp:ListItem Value="30">30</asp:ListItem>
                                            <asp:ListItem Value="35">35</asp:ListItem>
                                            <asp:ListItem Value="40">40</asp:ListItem>
                                            <asp:ListItem Value="45">45</asp:ListItem>
                                            <asp:ListItem Value="50">50</asp:ListItem>
                                        </asp:DropDownList></td>
                                    <td>&nbsp;</td>
                                    <td align="right" colspan="4">
                                        <asp:Label ID="lblPendingApprovalRecordCounts" runat="server"></asp:Label>
                                    </td>
                                </tr>
                            </table>
                        </div>
                        <div style="width: 100%; overflow-y: scroll; height: 100%;">
                            <asp:GridView ID="gvPendingApprovals" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
                                DataKeyNames="price_list_detail_id" AllowSorting="true" SkinID="DetailPageGridView">
                                <SelectedRowStyle Wrap="True" />
                                <EditRowStyle Wrap="True" />
                                <AlternatingRowStyle Wrap="True" />
                                <RowStyle Wrap="True" />
                                <Columns>
                                    <asp:TemplateField>
                                        <HeaderTemplate>
                                            <asp:CheckBox ID="chkHeaderApproveOrReject" runat="server" Onclick="checkAll(this)" />
                                        </HeaderTemplate>
                                        <ItemTemplate>
                                            <asp:CheckBox ID="chkApproveOrReject" runat="server" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:BoundField DataField="service_class_code" SortExpression="service_class_code"
                                        ReadOnly="true" HeaderText="SERVICE_CLASS" HeaderStyle-HorizontalAlign="Center"
                                        HtmlEncode="false" />
                                    <asp:BoundField DataField="service_type_code" SortExpression="service_type_code"
                                        ReadOnly="true" HeaderText="SERVICE_TYPE" HeaderStyle-HorizontalAlign="Center"
                                        HtmlEncode="false" />
                                    <asp:BoundField DataField="service_level_code" SortExpression="service_level_code"
                                        ReadOnly="true" HeaderText="SERVICE_LEVEL" HeaderStyle-HorizontalAlign="Center"
                                        HtmlEncode="false" />
                                    <asp:BoundField DataField="risk_type_code" SortExpression="risk_type_code" ReadOnly="true"
                                        HeaderText="RISK_TYPE" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                    <asp:BoundField DataField="equipment_code" SortExpression="equipment_code" ReadOnly="true"
                                        HeaderText="EQUIPMENT_CLASS" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                    <asp:BoundField DataField="make" SortExpression="make" ReadOnly="true" HeaderText="MAKE"
                                        HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                    <asp:BoundField DataField="model" SortExpression="model" ReadOnly="true" HeaderText="MODEL"
                                        HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                    <asp:BoundField DataField="part_description" SortExpression="part_description" ReadOnly="true" HeaderText="PART_DESCRIPTION"
                                        HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                    <asp:BoundField DataField="condition_type_code" SortExpression="condition_type_code"
                                        ReadOnly="true" HeaderText="CONDITION" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                    <asp:BoundField DataField="vendor_sku" SortExpression="vendor_sku" ReadOnly="true"
                                        HeaderText="VENDOR_SKU" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                    <asp:BoundField DataField="vendor_sku_description" SortExpression="vendor_sku_description"
                                        ReadOnly="true" HeaderText="DESCRIPTION" HeaderStyle-HorizontalAlign="Center"
                                        HtmlEncode="false" />
                                    <asp:BoundField DataField="requested_by" SortExpression="requested_by" ReadOnly="true" HeaderText="REQUESTED_BY"
                                        HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                    <asp:BoundField DataField="requested_date" SortExpression="requested_date" ReadOnly="true" HeaderText="REQUESTED_DATE"
                                        HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                    <asp:BoundField DataField="status_xcd" SortExpression="status_xcd" ReadOnly="true"
                                        HeaderText="STATUS" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                    <asp:BoundField DataField="status_date" SortExpression="status_date"
                                        ReadOnly="true" HeaderText="STATUS_DATE" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                    <asp:BoundField DataField="manufacturer_origin_desc" SortExpression="manufacturer_origin_desc" ReadOnly="true" HeaderText="MANUFACTURER_ORIGIN"
                                        HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                    <asp:BoundField DataField="price_with_symbol" SortExpression="price_with_symbol" ReadOnly="true" HeaderText="PRICE"
                                        HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                    <asp:BoundField DataField="vendor_quantity" SortExpression="vendor_quantity" ReadOnly="true"
                                        HeaderText="QUANTITY" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                    <asp:BoundField DataField="price_low_range_with_symbol" SortExpression="price_low_range_with_symbol"
                                        ReadOnly="true" HeaderText="LOW_PRICE" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                    <asp:BoundField DataField="price_high_range_with_symbol" SortExpression="price_high_range_with_symbol"
                                        ReadOnly="true" HeaderText="HIGH_PRICE" HeaderStyle-HorizontalAlign="Center"
                                        HtmlEncode="false" />
                                    <asp:BoundField DataField="effective" SortExpression="effective" ReadOnly="true"
                                        HeaderText="EFFECTIVE" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                    <asp:BoundField DataField="expiration" SortExpression="expiration" ReadOnly="true"
                                        HeaderText="EXPIRATION" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                    <asp:BoundField DataField="price_list_detail_id" ReadOnly="true" Visible="false" />
                                </Columns>
                                <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                                <PagerStyle />
                            </asp:GridView>

                            <div class="btnZone">
                                <table cellpadding="0" cellspacing="0" width="100%">
                                    <tr>

                                        <td class="auto-style1">
                                            <asp:LinkButton runat="server" ID="LinkButton2"></asp:LinkButton>
                                            <asp:Button ID="btnApprove" runat="server" SkinID="AlternateLeftButton" Text="PL_APPROVE" />
                                        </td>
                                        <td>
                                            <asp:LinkButton runat="server" ID="LinkButton1"></asp:LinkButton>
                                            <asp:Button ID="btnReject" runat="server" SkinID="AlternateLeftButton" Text="PL_REJECT" />
                                        </td>

                                    </tr>

                                </table>
                            </div>
                        </div>
                    </div>
            </div>
        </div>
    </div>
    <div class="btnZone">
        <div class="">
            <asp:Button ID="btnSave" runat="server" SkinID="PrimaryRightButton" Text="SAVE" />
            <asp:Button ID="btnBack" runat="server" SkinID="AlternateLeftButton" Text="BACK" />
            <asp:Button ID="btnAdd" runat="server" SkinID="AlternateLeftButton" Text="ADD" />
            <asp:Button ID="btnClone" runat="server" SkinID="AlternateRightButton" Text="NEW_WITH_COPY" />
            <asp:Button ID="btnUndo" runat="server" SkinID="AlternateRightButton" Text="UNDO" />
            <asp:Button ID="btnDelete" runat="server" SkinID="AlternateRightButton" Text="DELETE" />
         </div>
    </div>
    <div id="AddNewContainer">
        <ajaxToolkit:ModalPopupExtender runat="server" ID="mdlPopup" TargetControlID="dummybutton"
            PopupControlID="pnlPopup" DropShadow="True" BackgroundCssClass="ModalBackground"
            BehaviorID="addNewModal" PopupDragHandleControlID="pnlPopup" RepositionMode="RepositionOnWindowScroll">
        </ajaxToolkit:ModalPopupExtender>
        <asp:Panel ID="pnlPopup" runat="server" Style="display: none; width: 500px;">
            <div id="light" class="overlay_message_content">
                <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Always" RenderMode="inline"
                    ChildrenAsTriggers="True">
                    <ContentTemplate>
                        <p class="modalTitle">
                            <asp:Label ID="lblModalTitle" runat="server" Text="PRICE_LIST_DETAIL"></asp:Label>

                        </p>
                        <asp:Panel ID="pnlud" runat="server">
                            <table width="100%" border="0" class="formGrid" cellpadding="0" cellspacing="0">
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblNewItemServiceClass" runat="server" Text="SERVICE_CLASS"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlNewItemServiceClass" runat="server" SkinID="MediumDropDown"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblNewItemVendorSKU" runat="server" Text="VENDOR_SKU"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNewItemVendorSKU" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblNewItemServiceType" runat="server" Text="SERVICE_TYPE"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlNewItemServiceType" runat="server" SkinID="MediumDropDown"
                                            Enabled="false">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblNewItemSKUDesciption" runat="server" Text="DESCRIPTION"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNewItemSKUDescription" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblNewItemPrice" runat="server" Text="PRICE" Visible="true"></asp:Label>
                                        <asp:Label ID="lblcalculation" runat="server" Text="Calculation_Percent" Visible="false"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNewItemPrice" runat="server" SkinID="exSmallTextBox" Width="38%" Visible="true"></asp:TextBox>
                                        <asp:Label ID="lblcurrency" runat="server" Text="" Visible="true"></asp:Label>
                                        <asp:DropDownList ID="ddlcurrency" runat="server" SkinID="SmallDropDown" Width="38%" Visible="true"></asp:DropDownList>
                                        <asp:TextBox ID="txtcalculationpercent" runat="server" Visible="false" Width="50%" ReadOnly="true"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblNewItemQuantity" runat="server" Text="QUANTITY"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNewItemQuantity" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblRiskType" runat="server" Text="RISK_TYPE"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlRiskType" runat="server" SkinID="MediumDropDown" AutoPostBack="True">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblEquipmentClass" runat="server" Text="EQUIPMENT_CLASS"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlEquipmentClass" runat="server" Width="75%" SkinID="SmallDropDown"
                                            AutoPostBack="False" />
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblNewItemEffectiveDate" runat="server" Text="EFFECTIVE_DATE"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNewItemEffectiveDate" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                        <asp:ImageButton ID="imgNewItemEffectiveDate" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png"
                                            align="absmiddle" alt="" Width="20" Height="20" />
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblNewItemMake" runat="server" Text="MAKE"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlNewItemMake" runat="server" SkinID="MediumDropDown" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblNewItemExpirationDate" runat="server" Text="EXPIRATION_DATE"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNewItemExpirationDate" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                                        <asp:ImageButton ID="imgNewItemExpirationDate" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png"
                                            align="absmiddle" alt="" Width="20" Height="20" />
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblNewItemModel" runat="server" Text="MODEL"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlNewItemModel" runat="server" SkinID="MediumDropDown" Enabled="false" AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblNewItemLowPrice" runat="server" Text="LOW_PRICE"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNewItemLowPrice" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblNewItemCondition" runat="server" Text="CONDITION"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlNewItemCondition" runat="server" SkinID="MediumDropDown">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblNewItemHighPrice" runat="server" Text="HIGH_PRICE"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtNewItemHighPrice" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblNewItemServiceLevel" runat="server" Text="SERVICE_LEVEL"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlNewItemServiceLevel" runat="server" SkinID="MediumDropDown"
                                            AutoPostBack="true">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblNewItemSelectTaxType" runat="server" Text="SELECT_TAX_TYPE"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlNewItemReplacementTaxType" runat="server" SkinID="MediumDropDown">
                                        </asp:DropDownList>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lbldetailtype" runat="server" Text="DETAIL_TYPE"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddldetailtype" runat="server" SkinID="MediumDropDown" Enabled="false" CssClass="disabled"></asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        <asp:Label ID="lblNewItemPart" runat="server" Text="PART_DESCRIPTION"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlNewItemPart" runat="server"  SkinID="MediumDropDown" Enabled="false" AutoPostBack="true"></asp:DropDownList>
                                    </td>
                                    <td align="right">
                                        <asp:Label ID="lblNewManOri" runat="server" Text="MANUFACTURER_ORIGIN"></asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlNewManOri" runat="server"  SkinID="MediumDropDown"></asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </ContentTemplate>
                    <Triggers>
                        <asp:AsyncPostBackTrigger ControlID="ddlNewItemMake" EventName="SelectedIndexChanged" />
                        <asp:AsyncPostBackTrigger ControlID="imgNewItemExpirationDate" EventName="Click" />
                        <asp:AsyncPostBackTrigger ControlID="imgNewItemEffectiveDate" EventName="Click" />
                    </Triggers>
                </asp:UpdatePanel>
                <table width="100%" border="0" class="formGrid" cellpadding="0" cellspacing="0">
                    <tr>
                        <td colspan="3">&nbsp;
                        </td>
                        <td>
                            <asp:Button ID="btnNewItemSave" runat="server" CssClass="primaryBtn floatR" Text="SAVE" CausesValidation="false" />
                            <asp:Button ID="btnNewItemCancel" runat="server" CssClass="popWindowCancelbtn floatR" Text="CANCEL" />
                        </td>
                    </tr>
                </table>
            </div>
        </asp:Panel>
    </div>
     <div id="ViewHistoryContainer">
        <%--<ajaxToolkit:ModalPopupExtender runat="server" ID="mdlpopupHistory" TargetControlID="dummybtnhistory"
            PopupControlID="pnlHistoryPopup" DropShadow="True" BackgroundCssClass="ModalBackground"
            BehaviorID="addNewModal" PopupDragHandleControlID="pnlHistoryPopup" RepositionMode="RepositionOnWindowScroll">
        </ajaxToolkit:ModalPopupExtender>--%>
        <asp:Panel ID="pnlHistoryPopup" runat="server" Style="display: none; width: 500px;">
            <div id="light" class="overlay_message_content">
                <asp:GridView ID="gvHistory" runat="server" Width="80%" AutoGenerateColumns="False" AllowPaging="True"
                            DataKeyNames="price_list_detail_id" AllowSorting="true" SkinID="DetailPageGridView">
                            <SelectedRowStyle Wrap="True" />
                            <EditRowStyle Wrap="True" />
                            <AlternatingRowStyle Wrap="True" />
                            <RowStyle Wrap="True" />
                            <Columns>
                                <asp:BoundField DataField="service_class_code" SortExpression="service_class_code"
                                    ReadOnly="true" HeaderText="SERVICE_CLASS" HeaderStyle-HorizontalAlign="Center"
                                    HtmlEncode="false" />
                                <asp:BoundField DataField="service_type_code" SortExpression="service_type_code"
                                    ReadOnly="true" HeaderText="SERVICE_TYPE" HeaderStyle-HorizontalAlign="Center"
                                    HtmlEncode="false" />
                                <asp:BoundField DataField="service_level_code" SortExpression="service_level_code"
                                    ReadOnly="true" HeaderText="SERVICE_LEVEL" HeaderStyle-HorizontalAlign="Center"
                                    HtmlEncode="false" />
                                <asp:BoundField DataField="risk_type_code" SortExpression="risk_type_code" ReadOnly="true"
                                    HeaderText="RISK_TYPE" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                <asp:BoundField DataField="equipment_code" SortExpression="equipment_code" ReadOnly="true"
                                    HeaderText="EQUIPMENT_CLASS" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                <asp:BoundField DataField="make" SortExpression="make" ReadOnly="true" HeaderText="MAKE"
                                    HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                <asp:BoundField DataField="model" SortExpression="model" ReadOnly="true" HeaderText="MODEL"
                                    HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                <asp:BoundField DataField="part_description" SortExpression="part_description" ReadOnly="true" HeaderText="PART_DESCRIPTION"
                                    HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                <asp:BoundField DataField="condition_type_code" SortExpression="condition_type_code"
                                    ReadOnly="true" HeaderText="CONDITION" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                <asp:BoundField DataField="vendor_sku" SortExpression="vendor_sku" ReadOnly="true"
                                    HeaderText="VENDOR_SKU" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                <asp:BoundField DataField="vendor_sku_description" SortExpression="vendor_sku_description"
                                    ReadOnly="true" HeaderText="DESCRIPTION" HeaderStyle-HorizontalAlign="Center"
                                    HtmlEncode="false" />
                                <asp:BoundField DataField="manufacturer_origin_desc" SortExpression="manufacturer_origin_desc" ReadOnly="true" HeaderText="MANUFACTURER_ORIGIN"
                                    HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                <asp:BoundField DataField="price_with_symbol" SortExpression="price_with_symbol" ReadOnly="true" HeaderText="PRICE"
                                    HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                <asp:BoundField DataField="vendor_quantity" SortExpression="vendor_quantity" ReadOnly="true"
                                    HeaderText="QUANTITY" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                <asp:BoundField DataField="price_low_range_with_symbol" SortExpression="price_low_range_with_symbol"
                                    ReadOnly="true" HeaderText="LOW_PRICE" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                <asp:BoundField DataField="price_high_range_with_symbol" SortExpression="price_high_range_with_symbol"
                                    ReadOnly="true" HeaderText="HIGH_PRICE" HeaderStyle-HorizontalAlign="Center"
                                    HtmlEncode="false" />
                               <%-- <asp:BoundField DataField="staus_xcd" SortExpression="staus_xcd" ReadOnly="true"
                                    HeaderText="STATUS" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />--%>
                                <asp:BoundField DataField="effective" SortExpression="effective" ReadOnly="true"
                                    HeaderText="EFFECTIVE" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                <asp:BoundField DataField="expiration" SortExpression="expiration" ReadOnly="true"
                                    HeaderText="EXPIRATION" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false" />
                                <asp:BoundField DataField="price_list_detail_id" ReadOnly="true" Visible="false" />
                            </Columns>
                            <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                            <PagerStyle />
                        </asp:GridView>
                  </div>
        </asp:Panel>
    </div>

    <%--</asp:Panel>--%>
    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
        runat="server" />
</asp:Content>
