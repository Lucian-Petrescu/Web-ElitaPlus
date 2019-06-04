<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="InventorySearchForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.InventorySearchForm"
    MasterPageFile="../Navigation/masters/ElitaBase.Master"
    Theme="Default" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
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
    </script>
</asp:Content>
<asp:Content ID="MessageContent" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="SummaryContent" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto">
        <Scripts>
            <asp:ScriptReference Path="~/Navigation/scripts/ComunaSuggest.js" />
        </Scripts>
    </asp:ScriptManager>
    <table width="100%" border="0" class="searchGrid" id="searchTable" runat="server">
        <tr>

            <td>
                <table width="100%" border="0">

                    <tr>
                        <td align="left">
                            <asp:Label ID="Label2" runat="server">DEALER</asp:Label><br />
                            <asp:DropDownList ID="ddlDealer" runat="server" SkinID="MediumDropDown" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="LabelServiceCenter" runat="server">SERVICE_CENTER</asp:Label><br />
                            <asp:DropDownList ID="ddlServiceCenter" runat="server" SkinID="MediumDropDown" AutoPostBack="False">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="LabelVendorSkuNumber" runat="server">VENDOR_SKU_NUMBER</asp:Label><br />
                            <asp:TextBox ID="TextBoxVendorSkuNumber" runat="server" SkinID="MediumTextBox"
                                AutoPostBack="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LabelDeviceMake" runat="server">MAKE</asp:Label><br />
                            <asp:DropDownList ID="ddlDeviceMake" runat="server" SkinID="MediumDropDown" AutoPostBack="False">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="LabelDeviceModel" runat="server">MODEL</asp:Label><br />
                            <asp:TextBox ID="TextBoxDeviceModel" runat="server" SkinID="MediumTextBox"
                                AutoPostBack="False"></asp:TextBox>
                        </td>
                        <td align="left">&nbsp;
                        </td>
                    </tr>
                    <tr id="trElitaInventory" runat="server">
                        <td>
                            <asp:Label ID="LabelDeviceMemory" runat="server">MEMORY</asp:Label><br />
                            <asp:TextBox ID="TextBoxDeviceMemory" runat="server" SkinID="MediumTextBox"
                                AutoPostBack="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:Label ID="LabelDeviceColor" runat="server">COLOR</asp:Label><br />
                            <asp:TextBox ID="TextBoxDeviceColor" runat="server" SkinID="MediumTextBox"
                                AutoPostBack="False"></asp:TextBox>
                        </td>
                        <td align="left">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:Label ID="Label1" runat="server">Sort By</asp:Label><br />
                            <asp:DropDownList ID="ddlSortBy" runat="server" SkinID="SmallDropDown" AutoPostBack="False">
                            </asp:DropDownList>
                        </td>
                        <td colspan="2">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3" align="right">
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
<asp:Content ID="Body" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <!-- new layout start -->
    <div class="dataContainer">
        <h2 class="dataGridHeader">
            <asp:Label ID="lblSearchResults" runat="server" Text="SEARCH_RESULTS_FOR_INVENTORY" Visible="true"></asp:Label>
        </h2>
        <div>
            <table width="100%" class="dataGrid">
                <tr id="trPageSize" runat="server">
                    <td class="bor" align="left">
                        <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label><asp:Label ID="colonSepertor"
                            runat="server">:</asp:Label>
                        &nbsp;
                        <asp:DropDownList ID="cboPageSize" runat="server" AutoPostBack="true" Width="50px"
                            SkinID="SmallDropDown">
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
                    <td class="bor" align="right">
                        <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 100%">
            <asp:GridView ID="Grid" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
                SkinID="DetailPageGridView" AllowSorting="true">
                <SelectedRowStyle Wrap="True" />
                <EditRowStyle Wrap="True" />
                <AlternatingRowStyle Wrap="True" />
                <RowStyle Wrap="True" />
                <Columns>
                    <asp:BoundField HeaderText="VENDOR_SKU_NUMBER" DataField="VendorSku" SortExpression="VENDORSKU" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField HeaderText="VENDOR_SKU_DESCRIPTION" DataField="VendorSkuDescription" SortExpression="VENDORSKUDESCRIPTION" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField HeaderText="MAKE" DataField="Make" SortExpression="MAKE" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField HeaderText="MODEL" DataField="Model" SortExpression="MODEL" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField HeaderText="MEMORY" DataField="Memory" SortExpression="MEMORY" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField HeaderText="COLOR" DataField="Color" SortExpression="COLOR" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField HeaderText="INVENTORY_QUANTITY" DataField="inventoryQuantity" SortExpression="INVENTORYQUANTITY" HtmlEncode="false"></asp:BoundField>
                </Columns>
                <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                <PagerStyle />
            </asp:GridView>
        </div>
    </div>
    <!-- end new layout -->
</asp:Content>
