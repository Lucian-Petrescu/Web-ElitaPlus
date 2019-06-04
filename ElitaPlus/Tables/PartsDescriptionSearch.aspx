<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master"
    CodeBehind="PartsDescriptionSearch.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.PartsDescriptionSearch" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <!--d5d6e4-->
        <tr>
            <td class="BLANKROW">
            </td>
            <td>
            </td>
        </tr>
        <tr id="trPageSize" runat="server">
            <td style="height: 22px" valign="top" align="left">
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
            <td style="height: 22px; text-align: right">
                <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td class="BLANKROW">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="Grid" runat="server" Width="100%" OnRowCommand="ItemCommand" OnRowCreated="ItemCreated"
                    AllowSorting="True" AllowPaging="True" CellPadding="1" AutoGenerateColumns="False"
                    CssClass="DATAGRID">
                    <SelectedRowStyle CssClass="SELECTED"></SelectedRowStyle>
                    <EditRowStyle CssClass="EDITROW"></EditRowStyle>
                    <AlternatingRowStyle Wrap="False" CssClass="ALTROW"></AlternatingRowStyle>
                    <RowStyle CssClass="ROW"></RowStyle>
                    <HeaderStyle CssClass="HEADER"></HeaderStyle>
                    <Columns>
                        <asp:TemplateField>
                            <ItemStyle HorizontalAlign="Center" Width="3%" Height="15px" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEdit" ImageUrl="../Navigation/images/icons/edit2.gif" runat="server"
                                    CommandName="SelectAction" CausesValidation="false" CommandArgument="<%#Container.DisplayIndex %>">
                                </asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="False" HeaderText="ID"></asp:TemplateField>
                        <asp:TemplateField SortExpression="2" HeaderText="Risk_Group">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings PageButtonCount="15" Mode="Numeric" />
                    <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <asp:Button ID="NewButton_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
        Width="100px" CssClass="FLATBUTTON" Height="20px" Text="New"></asp:Button>
</asp:Content>
