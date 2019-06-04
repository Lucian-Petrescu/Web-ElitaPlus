<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ClaimFulfillmentOrderListForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.ClaimFulfillmentOrderListForm" MasterPageFile="../Navigation/masters/ElitaBase.Master"
    Theme="Default" %>
<%@ Register Src="../Common/MultipleColumnDDLabelControl_new.ascx" TagName="MultipleColumnDDLabelControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js">        function TABLE1_onclick() {

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table width="100%" border="0" class="searchGrid">
        <tr>
            <td>
                <asp:Label ID="CodeLabel" runat="server">CODE</asp:Label><br />
                <asp:TextBox ID="CodeText" runat="server" SkinID="MediumTextBox" 
                    AutoPostBack="False"></asp:TextBox>
            </td>
            <td>
                <asp:Label ID="DescriptionLabel" runat="server">DESCRIPTION</asp:Label><br />
                <asp:TextBox ID="DescriptionText" runat="server" SkinID="MediumTextBox" 
                    AutoPostBack="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td>
                <asp:Label ID="PriceListSourceLabel" runat="server">PRICE_LIST_SOURCE</asp:Label><br />
                <asp:DropDownList ID="PriceListSourceDropDown" runat="server" SkinID="MediumDropDown" AutoPostBack="False">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td style="height: 40px" valign="middle" align="left" width="40%" nowrap="nowrap">
                <asp:Button ID="moBtnClearSearch" runat="server" SkinID="AlternateLeftButton" Text="Clear">
                </asp:Button>
                <asp:Button ID="moBtnSearch" runat="server" Text="Search" SkinID="SearchLeftButton">
                </asp:Button>
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
            <asp:Label runat="server" ID="moSearchResultsHeader">SEARCH_RESULTS_FOR_CLAIM_FULFILLMENT_ORDER_DETAILS</asp:Label>
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
            <asp:GridView ID="Grid" runat="server" Width="80%" OnRowCreated="RowCreated" OnRowCommand="RowCommand"
                AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" SkinID="DetailPageGridView" PageSize="30">
                <SelectedRowStyle Wrap="True"></SelectedRowStyle>
                <EditRowStyle Wrap="True"></EditRowStyle>
                <AlternatingRowStyle Wrap="True"></AlternatingRowStyle>
                <RowStyle Wrap="True"></RowStyle>
                <Columns>
                    <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton ID="EditButton_WRITE" runat="server" CausesValidation="False" CommandName="SelectAction"
                                    ImageUrl="~/Navigation/images/icons/edit2.gif" CommandArgument="<%#Container.DisplayIndex %>" />
                            </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="CODE" HeaderText="CODE">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                     </asp:TemplateField>
                    <asp:TemplateField SortExpression="DESCRIPTION" HeaderText="DESCRIPTION">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="PRICE_LIST_SOURCE" HeaderText="PRICE_LIST_SOURCE">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="COUNTRY" HeaderText="COUNTRY">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="PRICE_LIST_CODE" HeaderText="PRICE_LIST_CODE">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="EQUIPMENT_TYPE" HeaderText="EQUIPMENT_TYPE">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="False" HeaderText="CF_ORDER_DETAIL_ID"></asp:TemplateField>
                </Columns>
                <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                <PagerStyle  />
            </asp:GridView>
        </div>
    </div>
    <div class="btnZone">
        <asp:Button ID="btnNew_WRITE" runat="server" SkinID="PrimaryLeftButton" Text="New"
            CommandName="WRITE"></asp:Button>
    </div>
</asp:Content>
