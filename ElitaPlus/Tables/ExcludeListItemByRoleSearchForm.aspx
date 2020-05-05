<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ExcludeListItemByRoleSearchForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.ExcludeListItemByRoleSearchForm" MasterPageFile="../Navigation/masters/ElitaBase.Master"
    Theme="Default" %>

<%@ Register Src="../Common/MultipleColumnDDLabelControl_new.ascx" TagName="MultipleColumnDDLabelControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script type="text/javascript" language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js">  </script>
    
    <script type="text/javascript">
    function TABLE1_onclick() {

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table width="100%" border="0" class="searchGrid">
        <tr>
            <td align="left" style="height: 40px" width="30%" nowrap="nowrap">
                <table width="1%">
                    <uc1:MultipleColumnDDLabelControl ID="CompanyMultipleDrop" runat="server" />
                </table>
            </td>
        </tr>
        <tr>
            <td valign="middle" width="30%" style="height: 40px">
                <table>
                    <tr>
                        <td nowrap align="left" width="1%">
                            <asp:Label ID="moListDescriptionLabel" runat="server">List_Description</asp:Label>
                        </td>
                        <td nowrap align="left" width="1%">
                            <asp:DropDownList ID="moListDescriptionDrop" runat="server" 
                                SkinID="MediumDropDown" AutoPostBack="True">
                            </asp:DropDownList>
                        </td>
                        <td nowrap align="left" width="1%">
                            <asp:Label ID="moListItemDescriptionLabel" runat="server">List_Item_Description</asp:Label>
                        </td>
                        <td nowrap align="left" width="1%">
                            <asp:DropDownList ID="moListItemDescriptionDrop" runat="server" 
                                SkinID="SmallDropDown">
                            </asp:DropDownList>
                        </td>
                        <td nowrap align="left" width="1%">
                            <asp:Label ID="moRoleDescriptionLabel" runat="server">Role_Description</asp:Label>
                        </td>
                        <td nowrap align="left" width="1%">
                            <asp:DropDownList ID="moRoleDescriptionDrop" runat="server" 
                                SkinID="SmallDropDown">
                            </asp:DropDownList>
                        </td>
                        <td style="height: 40px" valign="middle" align="right" width="40%" nowrap="nowrap">
                            <br />
                            <asp:Button ID="moBtnClear" runat="server" SkinID="AlternateLeftButton" Text="Clear">
                            </asp:Button>
                            <asp:Button ID="moBtnSearch" runat="server" Text="Search" SkinID="SearchLeftButton">
                            </asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <!-- new layout start -->
    <div class="dataContainer">
        <h2 class="dataGridHeader">
            <asp:Label runat="server" ID="moSearchResultsHeader">SEARCH_RESULTS_FOR_ExcludeListItemByRole</asp:Label>
        </h2>
        <div>
            <table width="100%" class="dataGrid">
                <tr id="trPageSize" runat="server">
                    <td align="left">
                        <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>: &nbsp;
                        <asp:DropDownList ID="cboPageSize" runat="server" Width="50px" AutoPostBack="true">
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
                        </asp:DropDownList>
                    </td>
                    <td class="bor" align="right">
                        <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 100%">
            <asp:GridView id="Grid" runat="server" Width="100%" OnRowCreated="RowCreated" OnRowCommand="RowCommand"
                AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" SkinID="DetailPageGridView" PageSize="15">
                <SelectedRowStyle Wrap="True"></SelectedRowStyle>
                <EditRowStyle Wrap="True"></EditRowStyle>
                <AlternatingRowStyle Wrap="True"></AlternatingRowStyle>
                <RowStyle Wrap="True"></RowStyle>
                <Columns>
                    <asp:TemplateField SortExpression="LIST_ITEM_DESCRIPTION" HeaderText="LIST_ITEM_DESCRIPTION">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="SelectAction" Style="cursor: hand" runat="server" Visible="true"
                                CommandName="SelectAction" CommandArgument="<%#Container.DisplayIndex %>">
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="COMPANY" HeaderText="COMPANY">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>                    
                    <asp:TemplateField SortExpression="LIST_DESCRIPTION" HeaderText="LIST_DESCRIPTION">
                        <HeaderStyle></HeaderStyle>
                    </asp:TemplateField>
                       <asp:TemplateField SortExpression="ROLE_DESCRIPTION" HeaderText="ROLE_DESCRIPTION">
                        <HeaderStyle></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="False" HeaderText="EXCLUDE_LIST_ITEM_BY_ROLE_ID"></asp:TemplateField>
                </Columns>
               <PagerSettings PageButtonCount="15" Mode="Numeric" Position ="TopAndBottom"></PagerSettings>
               <PagerStyle />
            </asp:GridView>
        </div>
    </div>
    <div class="btnZone">
        <asp:Button ID="btnNew_WRITE" runat="server" SkinID="PrimaryLeftButton" Text="New"
            CommandName="WRITE"></asp:Button>
    </div>
</asp:Content>
