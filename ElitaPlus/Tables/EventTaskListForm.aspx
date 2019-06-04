<%@ Page Title="" Language="vb" AutoEventWireup="false" Theme="Default" MasterPageFile="../Navigation/masters/ElitaBase.Master" CodeBehind="EventTaskListForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.EventTaskListForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table class="searchGrid" border="0" cellpadding="0" cellspacing="0">
        <tbody>
            <tr>
                <td>
                    <asp:Label ID="lblSearchCompanyGroup" runat="server">COMPANY_GROUP:</asp:Label><br />
                    <asp:DropDownList ID="ddlSearchCompanyGroup" runat="server" SkinID="MediumDropDown" AutoPostBack="False"></asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblSearchCompany" runat="server">COMPANY:</asp:Label><br />
                    <asp:DropDownList ID="ddlSearchCompany" runat="server" SkinID="MediumDropDown" AutoPostBack="False"></asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblSearchCountry" runat="server">COUNTRY:</asp:Label><br />
                    <asp:DropDownList ID="ddlSearchCountry" runat="server" SkinID="MediumDropDown" AutoPostBack="False"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSearchDealerGroup" runat="server">DEALER_GROUP:</asp:Label><br />
                    <asp:DropDownList ID="ddlSearchDealerGroup" runat="server" SkinID="MediumDropDown" AutoPostBack="False" TabIndex="3"></asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblSearchDealer" runat="server">DEALER:</asp:Label><br />
                    <asp:DropDownList ID="ddlSearchDealer" runat="server" SkinID="MediumDropDown" AutoPostBack="False"></asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblSearchProdCode" runat="server">PRODUCT_CODE</asp:Label>:<br />
                    <asp:TextBox ID="txtSearchProdCode" runat="server" SkinID="MediumTextBox" />
                </td>
                <td>
                    <asp:Label ID="lblSearchEventType" runat="server">EVENT_TYPE:</asp:Label><br />
                    <asp:DropDownList ID="ddlSearchEventType" runat="server" SkinID="MediumDropDown" AutoPostBack="False"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSearchTask" runat="server">TASK:</asp:Label><br />
                    <asp:DropDownList ID="ddlSearchTask" runat="server" SkinID="MediumDropDown" AutoPostBack="False"></asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblSearchCoverageType" runat="server">COVERAGE_TYPE:</asp:Label><br />
                    <asp:DropDownList ID="ddlSearchCoverageType" runat="server" SkinID="MediumDropDown" AutoPostBack="False"></asp:DropDownList>
                </td>
                <td>&nbsp;<br />
                    <asp:Button ID="btnSearch" runat="server" Text="Search" SkinID="SearchRightButton" />
                    <asp:Button ID="btnClearSearch" runat="server" Text="Clear" SkinID="AlternateRightButton" />
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
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
        <asp:GridView ID="Grid" runat="server" Width="100%" OnRowCreated="RowCreated" OnRowCommand="RowCommand"
            AllowPaging="True" AllowSorting="True" CellPadding="1" AutoGenerateColumns="False"
            SkinID="DetailPageGridView">
            <Columns>
                <asp:TemplateField Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblEventTaskID" Text='<%# GetGuidStringFromByteArray(Container.DataItem("EVENT_TASK_ID")) %>'
                            runat="server">
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:ImageButton ID="EditButton_WRITE" runat="server" CausesValidation="False" CommandName="SelectAction"
                            ImageUrl="~/App_Themes/Default/Images/edit.png" CommandArgument="<%#Container.DisplayIndex %>" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="COMPANY_GROUP" SortExpression="COMPANY_GROUP_DESC">
                    <ItemTemplate>
                        <asp:Label ID="lblCompGrp" Text='<%#Container.DataItem("COMPANY_GROUP_DESC")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="COMPANY" SortExpression="COMPANY_DESC">
                    <ItemTemplate>
                        <asp:Label ID="lblComp" Text='<%#Container.DataItem("COMPANY_DESC")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="COUNTRY" SortExpression="COUNTRY_DESC">
                    <ItemTemplate>
                        <asp:Label ID="lblCountry" Text='<%#Container.DataItem("COUNTRY_DESC")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="DEALER_GROUP">
                    <ItemTemplate>
                        <asp:Label ID="lblDealerGroup" Text='<%#Container.DataItem("DEALER_GROUP_DESC")%>' runat="server">
                        </asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList runat="server" ID="ddlDealerGroup" SkinID="SmallDropDown" TabIndex="13" />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="DEALER" SortExpression="DEALER_DESC">
                    <ItemTemplate>
                        <asp:Label ID="lblDealer" Text='<%#Container.DataItem("DEALER_DESC")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="PRODUCT_CODE" SortExpression="PRODUCT_CODE">
                    <ItemTemplate>
                        <asp:Label ID="lblProdCode" Text='<%#Container.DataItem("PRODUCT_CODE")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="EVENT_TYPE" SortExpression="EVENT_TYPE_DESC">
                    <ItemTemplate>
                        <asp:Label ID="lblEventType" Text='<%#Container.DataItem("EVENT_TYPE_DESC")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="EVENT_ARGUMENT" SortExpression="EVENT_ARGUMENT_DESC">
                    <ItemTemplate>
                        <asp:Label ID="lblEventArgument" Text='<%#Container.DataItem("EVENT_ARGUMENT_DESC")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="TASK" SortExpression="Task_Desc">
                    <ItemTemplate>
                        <asp:Label ID="lblTask" Text='<%#Container.DataItem("Task_Desc")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="COVERAGE_TYPE" SortExpression="COVERAGE_TYPE_DESC">
                    <ItemTemplate>
                        <asp:Label ID="lblCoverageType" Text='<%#Container.DataItem("COVERAGE_TYPE_DESC")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="EVENT_TASK_PARAMETERS">
                    <ItemTemplate>
                        <asp:Label ID="lblEventTaskParameter" Text='<%#Container.DataItem("EVENT_TASK_PARAMETERS")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="INIT_DELAY_MINUTES" SortExpression="INIT_DELAY_MINUTES">
                    <ItemTemplate>
                        <asp:Label ID="lblInitDelayMinutes" Text='<%#Container.DataItem("INIT_DELAY_MINUTES")%>' runat="server" />
                    </ItemTemplate>
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
