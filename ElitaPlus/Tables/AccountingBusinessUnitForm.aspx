<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AccountingBusinessUnitForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.AccountingBusinessUnitForm"
    MasterPageFile="~/Navigation/masters/content_default.Master" %>

<asp:Content runat="server" ContentPlaceHolderID="ContentPanelMainContentBody">
    <table class="TABLESEARCH" id="tblSearch" cellspacing="0" cellpadding="8" align="center"
        width="100%">
        <tr>
            <td valign="top">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td nowrap align="left">
                            <asp:Label ID="SearchBusinessUnitLabel" CssClass="TITLELABEL" runat="server">ACCOUNTING_BUSINESS_UNIT:</asp:Label>
                        </td>
                        <td nowrap align="left">
                            <asp:Label ID="SearchAcctCompanyLabel" CssClass="TITLELABEL" runat="server">ACCOUNTING_COMPANY:</asp:Label>
                        </td>
                        <td nowrap align="right">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td nowrap align="left">
                            <asp:TextBox ID="SearchBusinessUnitTextBox" runat="server" CssClass="FLATTEXTBOX"
                                AutoPostBack="False" Width="180px"></asp:TextBox>
                        </td>
                        <td nowrap align="left">
                            <asp:DropDownList ID="SearchAcctCompanyDropdownList" TabIndex="1" runat="server"
                                Width="280px">
                            </asp:DropDownList>
                        </td>
                        <td nowrap align="right">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td nowrap align="right">
                            &nbsp;
                        </td>
                        <td nowrap align="right">
                            &nbsp;
                        </td>
                        <td nowrap align="right">
                            <asp:Button ID="ClearButton" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
                                cursor: hand; background-repeat: no-repeat" runat="server" Width="90px" Height="20px"
                                Text="Clear" CssClass="FLATBUTTON" Font-Bold="false"></asp:Button>&nbsp;
                            <asp:Button ID="SearchButton" Style="background-image: url(../Navigation/images/icons/search_icon.gif);
                                cursor: hand; background-repeat: no-repeat" runat="server" Width="90px" Height="20px"
                                Text="Search" CssClass="FLATBUTTON" Font-Bold="false"></asp:Button>&nbsp;&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <hr style="height: 1px">
    <table width="100%">
        <tr id="trPageSize" runat="server">
            <td valign="top" align="left">
                <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>: &nbsp;
                <asp:DropDownList ID="cboPageSize" runat="server" AutoPostBack="true" Width="50px">
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
            <td style="text-align: right">
                <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="vertical-align: top" id="tdGrid">
                <asp:GridView ID="Grid" runat="server" Width="100%" OnRowCreated="ItemCreated" OnRowCommand="ItemCommand"
                    AllowPaging="True" AllowSorting="True" CellPadding="1" AutoGenerateColumns="False"
                    CssClass="DATAGRID">
                    <SelectedRowStyle CssClass="SELECTED"></SelectedRowStyle>
                    <EditRowStyle CssClass="EDITROW"></EditRowStyle>
                    <AlternatingRowStyle Wrap="False" CssClass="ALTROW"></AlternatingRowStyle>
                    <RowStyle CssClass="ROW"></RowStyle>
                    <HeaderStyle CssClass="HEADER"></HeaderStyle>
                    <Columns>
                        <asp:TemplateField Visible="true" ShowHeader="false">
                            <ItemStyle HorizontalAlign="Center" Width="3%" Height="15px" />
                            <ItemTemplate>
                                <asp:ImageButton ID="EditButton_WRITE" runat="server" CausesValidation="False" CommandName="EditRecord"
                                    ImageUrl="~/Navigation/images/icons/edit2.gif" CommandArgument="<%#Container.DisplayIndex %>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="false">
                            <ItemStyle HorizontalAlign="Center" Width="3%" Height="15px" />
                            <ItemTemplate>
                                <asp:ImageButton ID="DeleteButton_WRITE" runat="server" CausesValidation="False" CommandName="DeleteRecord"
                                    ImageUrl="../Navigation/images/icons/trash.gif" CommandArgument="<%#Container.DisplayIndex %>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="False" HeaderText="ACCOUNTING_BUSINESS_UNIT">
                            <ItemTemplate>
                                &gt;
                                <asp:Label ID="lblAcctBusinessUnitID" runat="server">
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="False" HeaderText="ACCOUNTING_COMPANY">
                            <ItemTemplate>
                                &gt;
                                <asp:Label ID="lblAcctCompanyID" runat="server">
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="business_unit" HeaderText="ACCOUNTING_BUSINESS_UNIT">
                            <ItemStyle HorizontalAlign="Left" Width="24%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblColBusinessUnit" runat="server" Visible="True">
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtColBusinessUnit" CssClass="FLATTEXTBOX_TAB" runat="server" Visible="True"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="code" HeaderText="CODE">
                            <ItemStyle HorizontalAlign="Left" Width="24%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblColCode" runat="server" Visible="True">
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="txtColCode" CssClass="FLATTEXTBOX_TAB" runat="server" Visible="True"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="Description" HeaderText="ACCOUNTING_COMPANY">
                            <ItemStyle HorizontalAlign="Left" Width="24%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblColAcctCompanyDescription" runat="server">
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlstColAcctCompany" runat="server" Width="216px">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="suppress_vendors" HeaderText="SUPPRESS_VENDORS">
                            <ItemStyle HorizontalAlign="Left" Width="12%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblColSuppressVendors" runat="server">
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlColSuppressVendors" runat="server" Width="216px">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PAYMENT_METHOD">
                            <ItemStyle HorizontalAlign="Left" Width="12%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="lblColPaymentMethod" runat="server">
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="ddlColPaymentMethod" runat="server" Width="216px">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings PageButtonCount="15" Mode="Numeric" />
                    <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <input id="HiddenDeletePromptResponse" type="hidden" runat="server" designtimedragdrop="261"/>

    <script type="text/javascript">
        if (document.getElementById("tdGrid")) {
            document.getElementById("tdGrid").style.height = parent.document.getElementById("Navigation_Content").clientHeight - 350;
        }
    </script>

</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <asp:Button ID="NewButton_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Width="90px" Height="20px"
        Text="New" CssClass="FLATBUTTON" Font-Bold="false"></asp:Button>
    <asp:Button ID="SaveButton_WRITE" Style="background-image: url(../Navigation/images/icons/save_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Width="90px" Height="20px"
        Text="Save" CssClass="FLATBUTTON" Font-Bold="false"></asp:Button>&nbsp;
    <asp:Button ID="CancelButton" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Width="90px" Height="20px"
        Text="Cancel" CssClass="FLATBUTTON" Font-Bold="false"></asp:Button>
</asp:Content>
