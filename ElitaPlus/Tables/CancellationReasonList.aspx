<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CancellationReasonListForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.CancellationReasonListForm" MasterPageFile="../Navigation/masters/ElitaBase.Master"
    Theme="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"> </script>
        <script type="text/javascript">
            function TABLE1_onclick() {

            }

    </script>
</asp:Content>


<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table width="100%" border="0" class="searchGrid">
        <tr>            
            <td nowrap align="left" width="1%">
                <asp:Label ID="lblCode" runat="server">Code:</asp:Label>
            </td>
            <td nowrap align="left" width="1%">
                <asp:TextBox ID="SearchCodeTextBox" runat="server" SkinID="MediumTextBox"></asp:TextBox>
            </td>
            <td nowrap align="left" width="1%">
                <asp:Label ID="lblDescription" runat="server">Description:</asp:Label>
            </td>
            <td nowrap align="left" width="1%">
                 <asp:TextBox ID="SearchDescriptionTextBox" runat="server" SkinID="MediumTextBox"></asp:TextBox>
            </td>
            <td style="height: 40px" valign="middle" align="right" width="40%" nowrap="nowrap">
                <br />
                <asp:Button ID="moBtnClear" runat="server" SkinID="AlternateLeftButton" Text="Clear"></asp:Button>
                
                <asp:Button ID="moBtnSearch" runat="server" Text="Search" SkinID="SearchLeftButton"></asp:button>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <!-- new layout start -->
    <div class="dataContainer">
        <h2 class="dataGridHeader">
            <asp:Label runat="server" ID="moSearchResultsHeader">SEARCH_RESULTS_FOR_CANCELLATION_REASONS</asp:Label>
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
            <asp:GridView id= "Grid" runat="server" Width="100%" OnRowCreated="RowCreated" OnRowCommand="RowCommand"
                AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" SkinID="DetailPageGridView" PageSize="30">
                <SelectedRowStyle Wrap="True"></SelectedRowStyle>
                <EditRowStyle Wrap="True"></EditRowStyle>
                <AlternatingRowStyle Wrap="True"></AlternatingRowStyle>
                <RowStyle Wrap="True"></RowStyle>
                <Columns>
                    <asp:TemplateField SortExpression="Code" HeaderText="Code">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="SelectAction" Style="cursor: hand" runat="server" Visible="true"
                                CommandName="SelectAction" CommandArgument="<%#Container.DisplayIndex %>">
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="DESCRIPTION" HeaderText="DESCRIPTION">
                        <HeaderStyle></HeaderStyle>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="Company_Code" HeaderText="Company_Code">
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>                                                            
                    <asp:TemplateField Visible="False" HeaderText="cancellation_id"></asp:TemplateField>
                </Columns>
               <PagerSettings PageButtonCount="30" Mode="Numeric" Position ="TopAndBottom"></PagerSettings>
               <PagerStyle />
            </asp:GridView>
        </div>
    </div>
    <div class="btnZone">
        <asp:Button ID="btnAdd_WRITE" runat="server" SkinID="PrimaryLeftButton" Text="New"
            CommandName="WRITE"></asp:Button>
    </div>
</asp:Content>