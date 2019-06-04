<%@ Page Title="" Language="vb" AutoEventWireup="false" Theme="Default" MasterPageFile="../Navigation/masters/ElitaBase.Master" CodeBehind="ClaimStageListForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.ClaimStageListForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table class="searchGrid" border="0" cellpadding="0" cellspacing="0">
        <tbody>
            <tr>
                <td>
                    <asp:Label ID="lblSearchStageName" runat="server">STAGE_NAME:</asp:Label><br />
                    <asp:DropDownList ID="ddlSearchStageName" runat="server" SkinID="MediumDropDown" TabIndex="1" AutoPostBack="False"></asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblSearchCompanyGroup" runat="server">COMPANY_GROUP:</asp:Label><br />
                    <asp:DropDownList ID="ddlSearchCompanyGroup" runat="server" SkinID="MediumDropDown" TabIndex="2" AutoPostBack="False"></asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblSearchCompany" runat="server">COMPANY:</asp:Label><br />
                    <asp:DropDownList ID="ddlSearchCompany" runat="server" SkinID="MediumDropDown" TabIndex="3" AutoPostBack="False"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSearchDealer" runat="server">DEALER:</asp:Label><br />
                    <asp:DropDownList ID="ddlSearchDealer" runat="server" SkinID="MediumDropDown" TabIndex="4" AutoPostBack="False"></asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblSearchCoverageType" runat="server">COVERAGE_TYPE:</asp:Label><br />
                    <asp:DropDownList ID="ddlSearchCoverageType" runat="server" SkinID="MediumDropDown" TabIndex="5" AutoPostBack="False" />
                </td>
                <td>
                    <asp:Label ID="lblSearchSequence" runat="server">SEQUENCE:</asp:Label><br />
                    <asp:TextBox ID="txtSearchSequence" runat="server" SkinID="MediumTextBox" TabIndex="6"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSearchActiveOn" runat="server">ACTIVE_ON:</asp:Label><br />
                    <asp:TextBox ID="txtSearchActiveOn" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                    &nbsp;<asp:ImageButton ID="imgSearchActiveOn" runat="server" Visible="True" TabIndex="7" ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="Top" />
                </td>
                <td>
                    <asp:Label ID="lblSearchScreen" runat="server">SCREEN:</asp:Label><br />
                    <asp:DropDownList ID="ddlSearchScreen" runat="server" SkinID="MediumDropDown" TabIndex="8" AutoPostBack="False"></asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblSearchPortal" runat="server">PORTAL:</asp:Label><br />
                    <asp:DropDownList ID="ddlSearchPortal" runat="server" SkinID="MediumDropDown" TabIndex="9" AutoPostBack="False"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td>
                    &nbsp;<br />
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
                        <asp:Label ID="lblClaimStageID" Text='<%# GetGuidStringFromByteArray(Container.DataItem("STAGE_ID"))%>'
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
                <asp:TemplateField Visible="True" HeaderText="STAGE_NAME" SortExpression="STAGE_NAME_DESC">
                    <ItemTemplate>
                        <asp:Label ID="lblStageName" Text='<%#Container.DataItem("STAGE_NAME_DESC")%>' runat="server" />
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
                <asp:TemplateField Visible="True" HeaderText="DEALER" SortExpression="DEALER_DESC">
                    <ItemTemplate>
                        <asp:Label ID="lblDealer" Text='<%#Container.DataItem("DEALER_DESC")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="COVERAGE_TYPE" SortExpression="COVERAGE_TYPE_DESC">
                    <ItemTemplate>
                        <asp:Label ID="lblCoverageType" Text='<%#Container.DataItem("COVERAGE_TYPE_DESC")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="EFFECTIVE_DATE" SortExpression="EFFECTIVE_DATE">
                    <ItemTemplate>
                        <asp:Label ID="lblEffectiveDate" Text='<%#Container.DataItem("EFFECTIVE_DATE")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="EXPIRATION_DATE" SortExpression="EXPIRATION_DATE">
                    <ItemTemplate>
                        <asp:Label ID="lblExpirationDate" Text='<%#Container.DataItem("EXPIRATION_DATE")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="SEQUENCE" SortExpression="SEQUENCE">
                    <ItemTemplate>
                        <asp:Label ID="lblSequence" Text='<%#Container.DataItem("SEQUENCE")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="SCREEN" SortExpression="SCREEN_DESC">
                    <ItemTemplate>
                        <asp:Label ID="lblScreen" Text='<%#Container.DataItem("SCREEN_DESC")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="PORTAL" SortExpression="PORTAL_DESC">
                    <ItemTemplate>
                        <asp:Label ID="lblPortal" Text='<%#Container.DataItem("PORTAL_DESC")%>' runat="server" />
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