<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="../Navigation/masters/ElitaBase.Master"
    Theme="Default" CodeBehind="FileTypeListForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.FileTypeListForm" %>

<asp:Content ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table class="searchGrid" border="0" cellpadding="0" cellspacing="0">
        <tbody>
            <tr>
                <td>
                    <asp:Label ID="CodeLabel" runat="server">CODE</asp:Label><br />
                    <asp:TextBox ID="CodeTextBox" runat="server" SkinID="MediumTextBox" />
                </td>
                <td>
                    <asp:Label ID="DescriptionLabel" runat="server">DESCRIPTION</asp:Label><br />
                    <asp:TextBox ID="DescriptionTextBox" runat="server" SkinID="MediumTextBox" />
                </td>
                <td>
                    <asp:Label ID="ExtensionLabel" runat="server">Extension</asp:Label><br />
                    <asp:TextBox ID="ExtensionTextBox" runat="server" SkinID="MediumTextBox" />
                </td>
            </tr>
            <tr>
                <td colspan="2">
                </td>
                <td>
                    &nbsp;<br />
                    <asp:Button ID="btnSearch" runat="server" Text="Search" SkinID="SearchRightButton" />
                    <asp:Button ID="btnClearSearch" runat="server" Text="Clear" SkinID="AlternateRightButton" />
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
<asp:Content ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer" runat="server" id="moSearchResults">
        <h2 class="dataGridHeader">
            <asp:Label runat="server" ID="moSearchResultsHeader">SEARCH_RESULTS</asp:Label>
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
        <asp:GridView ID="Grid" runat="server" Width="100%" 
            AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" SkinID="DetailPageGridView">
            <Columns>
                <asp:TemplateField Visible="True" HeaderText="CODE" SortExpression="CODE">
                    <ItemTemplate>
                        <asp:Label ID="CodeLabel" runat="server">
                        </asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="CodeTextBox" runat="server" SkinID="MediumTextBox" />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="DESCRIPTION" SortExpression="DESCRIPTION">
                    <ItemTemplate>
                        <asp:Label ID="DescriptionLabel" runat="server" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="DescriptionTextBox" runat="server" SkinID="MediumTextBox" />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="EXTENSION" SortExpression="EXTENSION">
                    <ItemTemplate>
                        <asp:Label ID="ExtensionLabel" runat="server" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="ExtensionTextBox" runat="server" SkinID="MediumTextBox" />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="MIME_TYPE" SortExpression="MIME_TYPE">
                    <ItemTemplate>
                        <asp:Label ID="MimeTypeLabel" runat="server" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="MimeTypeTextBox" runat="server" SkinID="MediumTextBox" />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ItemStyle-Width="50px">
                    <ItemStyle HorizontalAlign="Center" />
                    <ItemTemplate>
                        <asp:ImageButton runat="server" ID="EditButton" AlternateText="Edit" CommandName="EditRecord"
                            ImageUrl="~/App_Themes/Default/Images/edit.png" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:ImageButton runat="server" ID="SaveButton" AlternateText="Save" CommandName="SaveRecord"
                            ImageUrl="~/App_Themes/Default/Images/save.png" />
                        <asp:ImageButton runat="server" ID="CancelButton" AlternateText="Cancel" CommandName="CancelRecord"
                            ImageUrl="~/App_Themes/Default/Images/cancel.png" />
                    </EditItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerSettings PageButtonCount="10" Mode="Numeric" Position="TopAndBottom" />
        </asp:GridView>
    </div>
    <div class="btnZone">
        <asp:Button runat="server" ID="btnNew" Text="New" SkinID="AlternateLeftButton" />
    </div>
</asp:Content>
