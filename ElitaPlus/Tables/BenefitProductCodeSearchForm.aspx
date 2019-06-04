<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BenefitProductCodeSearchForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.BenefitProductCodeSearchForm"
    MasterPageFile="../Navigation/masters/ElitaBase.Master"
    Theme="Default" %>

<%@ Register Src="../Common/MultipleColumnDDLabelControl_new.ascx" TagName="MultipleColumnDDLabelControl"
    TagPrefix="uc1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js">        
        function TABLE1_onclick() {
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table width="100%" border="0" class="searchGrid">
        <tr>
            <td align="left" style="height: 40px" width="30%" nowrap="nowrap">
                <table width="1%">
                    <uc1:MultipleColumnDDLabelControl ID="DealerMultipleDrop" runat="server" />
                </table>
            </td>
        </tr>
        <tr>
            <td valign="middle" width="30%" style="height: 40px">
                <table>
                    <tr>
                        <td nowrap align="left" width="1%">
                            <asp:Label ID="moBenfitProductCodeLabel" runat="server">Benefit_Product_Code</asp:Label>
                        </td>
                        <td nowrap align="left" width="1%">
                            <asp:TextBox ID="moBenefitProductCodeText" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td style="height: 40px" valign="middle" align="right" width="40%" nowrap="nowrap">
                            <br />
                            <asp:Button ID="moBtnClear" runat="server" SkinID="AlternateLeftButton" Text="Clear"></asp:Button>
                            <asp:Button ID="moBtnSearch" runat="server" Text="Search" SkinID="SearchLeftButton"></asp:Button>
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
            <asp:Label runat="server" ID="moSearchResultsHeader">SEARCH_RESULTS_FOR_BENEFIT_PRODUCTS</asp:Label>
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
            <asp:GridView ID="Grid" runat="server" Width="100%" OnRowCreated="RowCreated" OnRowCommand="RowCommand"
                AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" SkinID="DetailPageGridView" PageSize="15">
                <SelectedRowStyle Wrap="True"></SelectedRowStyle>
                <EditRowStyle Wrap="True"></EditRowStyle>
                <AlternatingRowStyle Wrap="True"></AlternatingRowStyle>
                <RowStyle Wrap="True"></RowStyle>
                <Columns>
                    <asp:TemplateField SortExpression="BEN_PRODUCT_CODE" HeaderText="BEN_PRODUCT_CODE">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="SelectAction" Style="cursor: hand" runat="server" Visible="true"
                                CommandName="SelectAction" CommandArgument="<%#Container.DisplayIndex %>">
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="DEALER_NAME" HeaderText="DEALER_NAME">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="DESCRIPTION" HeaderText="DESCRIPTION">
                        <HeaderStyle></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="VENDOR_NAME" HeaderText="VENDOR_NAME">
                        <HeaderStyle></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="EFFECTIVE_DATE" HeaderText="EFFECTIVE_DATE">
                        <HeaderStyle></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="EXPIRATION_DATE" HeaderText="EXPIRATION_DATE">
                        <HeaderStyle></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="False" HeaderText="BEN_PRODUCT_CODE_ID"></asp:TemplateField>
                </Columns>
                <PagerSettings PageButtonCount="15" Mode="Numeric" Position="TopAndBottom"></PagerSettings>
                <PagerStyle />
            </asp:GridView>
        </div>
    </div>
    <div class="btnZone">
        <asp:Button ID="btnNew_WRITE" runat="server" SkinID="PrimaryLeftButton" Text="New"
            CommandName="WRITE"></asp:Button>
    </div>
</asp:Content>
