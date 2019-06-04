<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="vscModelSearchForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.vscModelSearchForm" MasterPageFile="../Navigation/masters/ElitaBase.Master"
    Theme="Default" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<%@ Register TagPrefix="uc1" TagName="MakeModel" Src="../Common/MakeModel.ascx" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="JavaScript" type="text/javascript">
        window.latestClick = '';

        function isNotDblClick() {
            if (window.latestClick != "clicked") {
                window.latestClick = "clicked";
                return true;
            } else {
                return falsecbo
            }
        }
    </script>
</asp:Content>
<asp:Content ID="MessageContent" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="SummaryContent" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <asp:ToolkitScriptManager ID="ToolkitScriptManager1" runat="Server" />
    <table width="100%" border="0" class="searchGrid">
        <tr>
            <td colspan="2">
                <uc1:MakeModel ID="MakeModelCtrl" runat="server"></uc1:MakeModel>
            </td>
        </tr>
        <tr>
            <td align="left">
                &nbsp;&nbsp;
                <asp:Label ID="moRadioTitleLabel" runat="server">COVERAGE_SUPPORT_FOR</asp:Label>:
                <asp:RadioButton ID="rbShowNew" Checked="False" GroupName="CovSupp" Text="New" runat="server">
                </asp:RadioButton>&nbsp;&nbsp;&nbsp;
                <asp:RadioButton ID="rbShowUsed" Checked="False" GroupName="CovSupp" Text="Used"
                    runat="server"></asp:RadioButton>&nbsp;&nbsp;&nbsp;
                <asp:RadioButton ID="rbShowBoth" Checked="true" GroupName="CovSupp" Text="Both" runat="server">
                </asp:RadioButton>
            </td>
            <td align = "right">
                <asp:Button ID="moBtnClear" runat="server" Text="Clear" SkinID="AlternateLeftButton">
                </asp:Button>
                <asp:Button ID="moBtnSearch" runat="server" Text="Search" SkinID="SearchLeftButton">
                </asp:Button>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Body" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <!-- new layout start -->
    <div class="dataContainer">
        <h2 class="dataGridHeader">
            <asp:Label runat="server" ID="moSearchResultsHeader">SEARCH_RESULTS_FOR_VSC_MODEL</asp:Label>
        </h2>
        <div>
            <table width="100%" class="dataGrid">
                <tr id="trPageSize" runat="server">
                    <td valign="top" align="left">
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
            <!--OnItemCreated="ItemCreated"  OnItemCommand="ItemCommand" -->
            <asp:DataGrid ID="Grid" runat="server" Width="100%" AllowSorting="True" AllowPaging="True"
                SkinID="DetailPageDataGrid">
                <SelectedItemStyle Wrap="True"></SelectedItemStyle>
                <EditItemStyle Wrap="True"></EditItemStyle>
                <AlternatingItemStyle Wrap="True"></AlternatingItemStyle>
                <ItemStyle Wrap="True"></ItemStyle>
                <HeaderStyle></HeaderStyle>
                <Columns>
                    <asp:TemplateColumn SortExpression="make" HeaderText="make">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="SelectAction" Style="cursor: hand" runat="server" Visible="true"
                                CommandName="SelectAction" Text='<%# Container.DataItem("make")%>'>
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateColumn>
                    <asp:BoundColumn DataField="model_year" SortExpression="model_year" HeaderText="Year">
                        <HeaderStyle HorizontalAlign="Center" Width="6%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="model" SortExpression="model" HeaderText="Model">
                        <HeaderStyle HorizontalAlign="Center" Width="17%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="description" SortExpression="description" HeaderText="Engine_Version">
                        <HeaderStyle HorizontalAlign="Center" Width="30%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="NEW_CLASS_CODE" SortExpression="New_Class_Code" HeaderText="New_Class_Code">
                        <HeaderStyle HorizontalAlign="Center" Width="9%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="USED_CLASS_CODE" SortExpression="Used_Class_Code" HeaderText="Used_Class_Code">
                        <HeaderStyle HorizontalAlign="Center" Width="9%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn Visible="False" DataField="active_new" SortExpression="acive_new"
                        HeaderText="active_new">
                        <HeaderStyle HorizontalAlign="Center" Width="9%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn Visible="False" DataField="active_used" SortExpression="acive_used"
                        HeaderText="active_used">
                        <HeaderStyle HorizontalAlign="Center" Width="9%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn DataField="engine_months_km_mi" SortExpression="engine_months_km_mi"
                        HeaderText="ENGINE_MONTHS_KM-MI">
                        <HeaderStyle HorizontalAlign="Center" Width="12%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:BoundColumn>
                    <asp:BoundColumn Visible="False" DataField="model_id"></asp:BoundColumn>
                </Columns>
                <PagerStyle HorizontalAlign="Center" Mode="NumericPages" PageButtonCount="30" Position="TopAndBottom">
                </PagerStyle>
            </asp:DataGrid>
        </div>
    </div>
    <div class="btnZone">
        <asp:Button ID="BtnNew_WRITE" runat="server" Text="New" SkinID="PrimaryLeftButton">
        </asp:Button>
    </div>
</asp:Content>
