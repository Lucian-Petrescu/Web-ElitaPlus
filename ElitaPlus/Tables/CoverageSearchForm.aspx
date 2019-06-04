<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CoverageSearchForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.CoverageSearchForm" MasterPageFile="../Navigation/masters/ElitaBase.Master"
    Theme="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js">        function TABLE1_onclick() {

        }

    </script>
</asp:Content>
<asp:Content ID="SummaryContent" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <!--Start Header-->
    <asp:Panel ID="moPanel" runat="server">
        <table id="moTableSearch" align="Left" border="0" class="searchGrid" width="100%">
            <tr>
                <td nowrap align="left" colspan="3">
                    &nbsp;&nbsp;
                    <uc1:MultipleColumnDDLabelControl ID="multipleDropControl" runat="server" SkinID="SmallDropdown">
                    </uc1:MultipleColumnDDLabelControl>
                </td>
            </tr>
            <tr>
                <td nowrap align="left">
                    &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="moProductLabel" runat="server">Product_Code</asp:Label>:
                </td>
                <td nowrap align="left">
                    &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="moRiskLabel" runat="server">Risk_Type</asp:Label>:
                </td>
                <td nowrap align="left">
                    &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="moCoverageTypeLabel" runat="server">Coverage_Type</asp:Label>:
                </td>
            </tr>
            <tr>
                <td nowrap align="left">
                    &nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="moProductDrop" runat="server" SkinID="SmallDropDown" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td nowrap align="left">
                    &nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="moRiskDrop" runat="server" SkinID="SmallDropDown" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td nowrap align="left">
                    &nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="moCoverageTypeDrop" runat="server" SkinID="SmallDropDown" AutoPostBack="True">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td nowrap align="left">
                    &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="moCertificateDurationLabel" runat="server">Certificate_Duration</asp:Label>:
                </td>
                <td nowrap align="left">
                    &nbsp;&nbsp;&nbsp;
                    <asp:Label ID="moCoverageDurationLabel" runat="server">Coverage_Duration</asp:Label>:
                </td>
                <td nowrap align="left">
                    &nbsp;&nbsp;&nbsp;
                </td>
            </tr>
            <tr>
                <td nowrap align="left">
                    &nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="moCertificateDurationDrop" runat="server" SkinID="SmallDropDown"
                        AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td nowrap align="left">
                    &nbsp;&nbsp;&nbsp;
                    <asp:DropDownList ID="moCoverageDurationDrop" runat="server" SkinID="SmallDropDown"
                        AutoPostBack="True">
                    </asp:DropDownList>
                </td>
                <td nowrap align="left">&nbsp;&nbsp;
                    <asp:Button ID="moBtnClear" runat="server" Text="Clear" SkinID="AlternateLeftButton">
                    </asp:Button>&nbsp;&nbsp;
                    <asp:Button ID="moBtnSearch" runat="server" Text="Search" SkinID="SearchButton">
                    </asp:Button>
                </td>
            </tr>
        </table>
    </asp:Panel>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="container">
        <div class="contentZoneHome">
            <div class="dataContainer">
                <h2 class="dataGridHeader">
                    Search results for Coverages</h2>
                <table border="0" align="center" width="100%" class="dataGrid">
                    <tr id="trPageSize" runat="server">
                        <td valign="top" align="left">
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
                        <td align="right">
                            <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
          
    <Div>
        
                <asp:DataGrid ID="moDataGrid" runat="server" Width="100%" OnItemCreated="ItemCreated"
                    AutoGenerateColumns="False" SkinID="DetailPageDataGrid" AllowPaging="True" AllowSorting="True"
                    OnItemCommand="ItemCommand">
                    <SelectedItemStyle Wrap="True"></SelectedItemStyle>
                    <EditItemStyle Wrap="True"></EditItemStyle>
                    <AlternatingItemStyle Wrap="True"></AlternatingItemStyle>
                    <ItemStyle Wrap="True"></ItemStyle>
                    <HeaderStyle></HeaderStyle>
                    <Columns>
                        <asp:TemplateColumn>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:ImageButton Style="cursor: hand;" ID="btnEdit" CommandName="BtnEdit" ImageUrl="../Navigation/images/icons/edit2.gif"
                                    runat="server"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="moCoverageId" Text='<%# GetGuidStringFromByteArray(Container.DataItem("COVERAGE_ID"))%>'
                                    runat="server">
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="DEALER" SortExpression="Dealer" HeaderText="Dealer">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="PRODUCT_CODE" SortExpression="Product_Code" HeaderText="Product_Code">
                            <HeaderStyle HorizontalAlign="Center" Width="1%"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="RISK_TYPE" SortExpression="Risk_Type" HeaderText="Risk_Type">
                            <HeaderStyle HorizontalAlign="Center" Width="1%"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="ITEM_NUMBER" SortExpression="ITEM_NUMBER" HeaderText="ITEM_NUMBER">
                            <HeaderStyle Width="1%"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="COVERAGE_TYPE" SortExpression="Coverage_Type" HeaderText="Coverage_Type">
                            <HeaderStyle HorizontalAlign="Center" Width="1%"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="CERTIFICATE_DURATION" SortExpression="Certificate_Duration"
                            HeaderText="Certificate_Duration">
                            <HeaderStyle HorizontalAlign="Center" Width="1%"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="COVERAGE_DURATION" SortExpression="Coverage_Duration"
                            HeaderText="Coverage_Duration">
                            <HeaderStyle Width="25px"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:TemplateColumn HeaderText="EFFECTIVE">
                            <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="moEffectiveLabel" Text='<%# GetDateFormattedString(Assurant.ElitaPlus.Common.DateHelper.GetDateValue(Container.DataItem("EFFECTIVE")))%>'
                                    runat="server">
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="EXPIRATION">
                            <HeaderStyle Wrap="False" HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="Label1" Text='<%# GetDateFormattedString(Assurant.ElitaPlus.Common.DateHelper.GetDateValue(Container.DataItem("EXPIRATION")))%>'
                                    runat="server">
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                    <PagerStyle PageButtonCount="15" Mode="NumericPages" Position="TopAndBottom"></PagerStyle>
                </asp:DataGrid>
           
    </Div>
    </div>
    </div>
    </div>

    <table border="0" class="btnZone">
        <tr>
            <td align="left">
                <asp:Button ID="BtnNew_WRITE" runat="server" Text="New" SkinID="PrimaryLeftButton">
                </asp:Button>&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>
