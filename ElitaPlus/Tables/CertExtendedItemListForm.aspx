<%@ Page Title="" Language="vb" AutoEventWireup="false" Theme="Default" MasterPageFile="../Navigation/masters/ElitaBase.Master" CodeBehind="CertExtendedItemListForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.CertExtendedItemListForm" %>

<%@ Register Src="../Common/MultipleColumnDDLabelControl_new.ascx" TagName="MultipleColumnDDLabelControl"
    TagPrefix="uc2" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table class="searchGrid" border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px;"
        width="100%">
        <tr>
            <td id="Td1" runat="server" colspan="2">
                <table>
                    <tbody>
                        <uc2:MultipleColumnDDLabelControl ID="moCompanyMultipleDrop" runat="server" />
                    </tbody>
                </table>
            </td>
            <td>
                <table>
                    <td nowrap="nowrap" align="right">&nbsp;
                    </td>
                    <tr>
                        <td nowrap="nowrap">
                            <asp:Label ID="lblSearchConfigCode" runat="server">CERT_EXT_ITEM_CONFIG_CODE:</asp:Label><br />
                        </td>
                        <td nowrap="nowrap" align="left">
                            <asp:DropDownList ID="ddlSearchConfigCode" runat="server" SkinID="MediumDropDown" TabIndex="3" AutoPostBack="true"></asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>&nbsp;</td>
            <td id="Td2" runat="server">
                <table>
                    <tbody>
                        <uc2:MultipleColumnDDLabelControl ID="moDealerMultipleDrop" runat="server" />
                    </tbody>
                </table>
            </td>
            <td></td>
            <td>
                <table>
                    <tr>
                        <td nowrap="nowrap" align="right">&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap" align="left">
                            <asp:Button ID="btnSearch" runat="server" Text="Search" SkinID="SearchRightButton" />
                            <asp:Button ID="btnClearSearch" runat="server" Text="Clear" SkinID="AlternateRightButton" />
                        </td>
                    </tr>

                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer" runat="server" id="moSearchResults">
        <h2 class="dataGridHeader">
            <asp:Label runat="server" ID="moSearchResultsHeader">SEARCH_RESULTS_FOR_CERTIFICATE_EXTENDED_ITEM_CONFIGURATION</asp:Label>
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
        <asp:GridView ID="Grid" runat="server" Width="100%" OnRowCreated="RowCreated" OnRowCommand="RowCommand"
            AllowPaging="True" AllowSorting="True" CellPadding="1" AutoGenerateColumns="False"
            SkinID="DetailPageGridView">
            <Columns>
                <asp:TemplateField SortExpression="CODE" HeaderText="CERT_EXT_ITEM_CONFIG_CODE">
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    <ItemTemplate>
                        <asp:LinkButton ID="SelectAction" Style="cursor: hand" runat="server" Visible="true"
                            OnClientClick="document.body.style.cursor = 'wait'; if(isNotDblClick()) {return true;} else return false;" CommandName="SelectAction" CommandArgument="<%#Container.DisplayIndex %>">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField SortExpression="DESCRIPTION" HeaderText="DESCRIPTION">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField SortExpression="CREATED_DATE" HeaderText="CREATED_DATE">
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                </asp:TemplateField>
                <asp:TemplateField SortExpression="MODIFIED_DATE" HeaderText="MODIFIED_DATE">
                    <HeaderStyle></HeaderStyle>
                </asp:TemplateField>

            </Columns>
            <PagerSettings PageButtonCount="10" Mode="Numeric" Position="TopAndBottom" />
        </asp:GridView>
        <input id="HiddenDeletePromptResponse" type="hidden" runat="server" designtimedragdrop="261" />
    </div>
    <div class="btnZone">
        <asp:Button runat="server" ID="btnNew" Text="New" SkinID="AlternateLeftButton" />
    </div>
</asp:Content>
