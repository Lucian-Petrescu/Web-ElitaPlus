<%@ Control Language="vb" 
AutoEventWireup="false" 
CodeBehind="UserControlApInvoiceLinesSearch.ascx.vb" 
Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.UserControlApInvoiceLinesSearch" %>

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

    function checkAllLines(selectAllLinesCheckbox) {
        (function ($jq) {
            $jq('.checkboxLine').prop("checked", selectAllLinesCheckbox.checked);
        })(jQuery);
    }

    function unCheckSelectAllLines(selectCheckboxLine) {
        if (!selectCheckboxLine.checked) {
            (function ($jq) {
                $jq('.checkboxHeader').prop("checked", false);
            })(jQuery);

        }
                
    }
</script>

<div class="dataContainer">
    <div class="dataGridHeader">
        <table border="0" class="searchGrid" runat="server" width="100%">
            <tbody>
                <tr>
                    <td align="right" nowrap="noWrap" style="width: 20%">
                        <asp:Label runat="server" ID="moServiceCenterLabel" Text="SERVICE_CENTER" />
                    </td>
                    <td nowrap="noWrap" style="width: 80%">
                        <asp:TextBox runat="server" ID="txtServiceCenter" SkinID="MediumTextBox" />
                    </td>
                    >
                </tr>
                <tr>

                </tr>
                    <td align="right" nowrap="noWrap" style="width: 20%">
                        <asp:Label runat="server" ID="lblClaimNumber" Text="CLAIM_NUMBER" />
                    </td>
                    <td nowrap="noWrap" style="width: 80%">
                        <asp:TextBox runat="server" ID="txtClaimNumber" SkinID="MediumTextBox" />
                    </td>

                <tr>
                    <td align="right" nowrap="noWrap"  style="width: 20%">
                        <asp:Label runat="server" ID="Label1" Text="AUTHORIZATION_NUMBER" />
                    </td>
                    <td nowrap="noWrap" colspan="5" style="width: 80%">
                        <asp:TextBox runat="server" ID="txtAuthorizationNumber" SkinID="MediumTextBox" />
                    </td>

                </tr>
                <tr>
                    <td runat="server" id="tdSearchButton" colspan="6"  align="right">
                        <asp:Button ID="btnClearSearch" runat="server" SkinID="AlternateLeftButton" Text="Clear"></asp:Button>&nbsp;&nbsp;
                        <asp:Button ID="btnSearch" runat="server" Text="Search" SkinID="SearchButton"></asp:Button>
                        
                        
                    </td>
                </tr>
            </tbody>
        </table>
    </div>
    <div>
        <h2 class="dataGridHeader"> Authorizations </h2>

    </div>
    <div class="dataGridHeader">
        <table width="100%" class="dataGrid">
            <tr id="trPageSize" runat="server">
                <td class="bor" align="left">
                    <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label><asp:Label ID="colonSepertor" runat="server">:</asp:Label>
                    &nbsp;
                    <asp:DropDownList ID="cboPageSize" runat="server" Width="50px" AutoPostBack="true" SkinID="SmallDropDown">
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
                    <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                </td>
            </tr>
        </table>
    </div>
    <div id="divAuthorizationList" style="width: 100%">
        <asp:GridView ID="GridAuth" runat="server" Width="50%"  Height = "150px" AutoGenerateColumns="False" AllowPaging="True"
                SkinID="DetailPageGridView" AllowSorting="false">
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
                    <asp:BoundField DataField="AuthorizationNumber" HeaderText="Authorization_Number" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField DataField="AuthorizedAmount" HeaderText="Authorized_Amount" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField DataField="AuthorizationStatus" HeaderText="Authorization_Status" HtmlEncode="false"></asp:BoundField>
                    
                </Columns>
                <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                <PagerStyle />

        </asp:GridView>
        
    </div>
    <div >
        <table width="100%">
            <tr>
                <td width="30%" align="right">
                    <asp:Button ID="btnSearchLines" runat="server" SkinID="SearchButton" Text="Search"  />&nbsp;
                </td>
            </tr>
        </table>
    </div>
    <div>
        <h2 class="dataGridHeader" runat="server" id="linesGridHeader"> Purchase Order  Lines </h2>

    </div>
    <div id="divPoLinesList" style="width: 100%">
        <asp:GridView ID="GridPoLines" runat="server" Width="100%"  Height = "250px" AutoGenerateColumns="False" AllowPaging="True"
                      SkinID="DetailPageGridView" AllowSorting="false">
            <SelectedRowStyle Wrap="True" />
            <EditRowStyle Wrap="True" />
            <AlternatingRowStyle Wrap="True" />
            <RowStyle Wrap="True" />
            <Columns>
                <asp:TemplateField>
                    <HeaderTemplate>
                        <asp:CheckBox ID="checkBoxLinesAll" runat="server" onclick="checkAllLines(this);"/>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <asp:CheckBox ID="checkBoxLinesSelected" runat="server" onclick="unCheckSelectAllLines(this);" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="LINE_TYPE" HeaderText="LINE_TYPE" HtmlEncode="false"></asp:BoundField>
                <asp:BoundField DataField="VENDOR_ITEM_CODE" HeaderText="item_code" HtmlEncode="false"></asp:BoundField>
                <asp:BoundField DataField="DESCRIPTION" HeaderText="description" HtmlEncode="false"></asp:BoundField>
                <asp:BoundField DataField="QTY" HeaderText="quantity" HtmlEncode="false"></asp:BoundField>
                <asp:BoundField DataField="UNIT_PRICE" HeaderText="UNIT_PRICE" HtmlEncode="false"></asp:BoundField>
                <asp:BoundField DataField="UOM_XCD" HeaderText="unit_of_measurement" HtmlEncode="false"></asp:BoundField>
                <asp:BoundField DataField="PO_Number" HeaderText="PO_Number" HtmlEncode="false"></asp:BoundField>
            </Columns>
            <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
            <PagerStyle />

        </asp:GridView>
        
    </div>
</div>