<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RoleListForm.aspx.vb"
    Theme="Default" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.RoleListForm"
    MasterPageFile="~/Navigation/masters/ElitaBase.Master" %>

<asp:Content ContentPlaceHolderID="HeadPlaceHolder" runat="server">
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
<asp:Content ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table class="searchGrid" border="0" cellpadding="0" cellspacing="0" width= "100%" runat="server">
        <tbody>
            <tr>
                <td>
                    <asp:Label ID="moRoleCodeLabel" runat="server">CODE</asp:Label>
                    <br />
                    <asp:TextBox ID="moRoleCode" runat="server" SkinID="MediumTextBox" />
                </td>
                <td>
                    <asp:Label ID="moRoleDescriptionLabel" runat="server">DESCRIPTION</asp:Label>
                    <br />
                    <asp:TextBox ID="moRoleDescription" runat="server" SkinID="MediumTextBox" />
                </td>
                <td align="left" colspan="2">
                </td>
            </tr>
            <tr>
                <td colspan="4" align="right">
                    <asp:Button runat="server" ID="btnSearch" SkinID="SearchRightButton" Text="SEARCH" />
                    <asp:Button runat="server" ID="btnClear" SkinID="AlternateRightButton" Text="CLEAR" />
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
<asp:Content ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer" runat="server" id="moSearchResults" visible="false">
        <h2 class="dataGridHeader">
            <asp:Label runat="server" ID="moSearchResultsHeader">SEARCH_RESULTS_FOR_ROLE</asp:Label>
        </h2>
        <table width="100%" class="dataGrid" border="0" cellspacing="0" cellpadding="0">
            <tbody>
                <tr>
                    <td class="bor">
                        <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>
                        <asp:DropDownList ID="cboPageSize" runat="server" SkinID="SmallDropDown" AutoPostBack="true">
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
                    <td align="right" class="bor">
                        <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                    </td>
                </tr>
            </tbody>
        </table>
        <asp:GridView ID="RolesGrid" runat="server" Width="100%" AllowPaging="true" AllowSorting="true" CellPadding="1" AutoGenerateColumns="False" SkinID="DetailPageGridView">
            <Columns>
                <asp:TemplateField Visible="True" HeaderText="CODE" SortExpression="Code">
                    <ItemTemplate>
                        <asp:LinkButton CommandName="SelectAction" ID="LinkButton1" runat="server" OnClientClick="document.body.style.cursor = 'wait'; if(isNotDblClick()) {return true;} else return false;"
                            CommandArgument='<%# Assurant.ElitaPlus.BusinessObjectsNew.Role.RoleSearchDV.RoleId(Container.DataItem) %>'
                            Text='<%#Container.DataItem("CODE")%>'>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="DESCRIPTION" SortExpression="Description">
                    <ItemTemplate>
                        <asp:Label ID="moDescriptionLabel" Text='<%#Container.DataItem("DESCRIPTION")%>'
                            runat="server">
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="IHQ_ONLY">
                    <ItemTemplate>
                        <asp:Label ID="moIHQOnlyLabel" Text='<%# Assurant.ElitaPlus.BusinessObjectsNew.Role.RoleSearchDV.IhqOnly(Container.DataItem) %>' runat="server">
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="ROLE_PROVIDER">
                    <ItemTemplate>
                        <asp:Label ID="moRoleProviderLabel" runat="server" Text='<%# Assurant.ElitaPlus.BusinessObjectsNew.Role.RoleSearchDV.RoleProvider(Container.DataItem) %>'>
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerSettings PageButtonCount="10" Mode="Numeric" Position="TopAndBottom" />
        </asp:GridView>
    </div>
    <div class="btnZone">
        <asp:Button runat="server" ID="btnAdd_WRITE" Text="New" SkinID="AlternateLeftButton" />
    </div>
</asp:Content>
