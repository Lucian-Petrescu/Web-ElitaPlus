<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="InterfaceSplitRuleListForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.InterfaceSplitRuleListForm" MasterPageFile="../Navigation/masters/ElitaBase.Master"
    Theme="Default" %>

<asp:Content ID="HeadContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script type="text/javascript" language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js">
    </script>
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
<asp:Content ID="Content1" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:content id="SummaryContent" contentplaceholderid="SummaryPlaceHolder" runat="server">
    <table width="100%" class="searchGrid" runat="server" id="searchGrid" border="0" cellspacing="0" cellpadding="0">
        <tbody>
            <tr>
                <td>
                    <asp:Label runat="server" ID="moSourceLabel">SOURCE</asp:Label>
                    <br />
                    <asp:DropDownList runat="server" ID="moSource" SkinID="MediumDropDown">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label runat="server" ID="moSourceCodeLable">SOURCE_CODE</asp:Label>
                    <br />
                    <asp:TextBox runat="server" ID="moSourceCode" SkinID="MediumTextBox" />
                </td>
            </tr>
            <tr>
                <td colspan="2" align="right">
                    <asp:Button runat="server" ID="btnSearch" SkinID="SearchRightButton" Text="SEARCH" />
                    <asp:Button runat="server" ID="btnClear" SkinID="AlternateRightButton" Text="CLEAR" />
                </td>
            </tr>
        </tbody>
    </table>
</asp:content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer" runat="server" id="moSearchResults" visible="false">
        <h2 class="dataGridHeader">
            <asp:Label runat="server" ID="moSearchResultsHeader">SEARCH_RESULTS_FOR_INTERFACE_SPLIT_RULES</asp:Label>
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
        <asp:GridView ID="InterfaceSplitRuleGrid" runat="server" Width="100%" AllowPaging="True" AllowSorting="True"
            CellPadding="1" SkinID="DetailPageGridView">
            <RowStyle HorizontalAlign="Center" />
            <Columns>
                <asp:TemplateField HeaderText="Name" SortExpression="Name">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemTemplate>
                        <asp:LinkButton CommandName="SelectAction" ID="btnEditInterfaceSplitRule" runat="server" OnClientClick="document.body.style.cursor = 'wait'; if(isNotDblClick()) {return true;} else return false;"
                            CommandArgument='<%# Assurant.ElitaPlus.ElitaPlusWebApp.ElitaPlusPage.GetGuidStringFromByteArray(DataBinder.Eval(Container, "DataItem.interface_split_rule_id")) %>'
                            Text='<%# DataBinder.Eval(Container, "DataItem.Source") %>'>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="SOURCE_CODE" SortExpression="SOURCE_CODE">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SOURCE_CODE") %>' />
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