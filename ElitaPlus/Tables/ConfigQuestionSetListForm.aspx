﻿<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="../Navigation/masters/ElitaBase.Master" CodeBehind="ConfigQuestionSetListForm.aspx.vb" 
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.ConfigQuestionSetListForm" Theme="Default" %>

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
                    <asp:DropDownList ID="ddlSearchCompanyGroup" runat="server" SkinID="MediumDropDown" AutoPostBack="False" TabIndex="1"></asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblSearchCompany" runat="server">COMPANY:</asp:Label><br />
                    <asp:DropDownList ID="ddlSearchCompany" runat="server" SkinID="MediumDropDown" AutoPostBack="True" TabIndex="2"></asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblSearchDealerGroup" runat="server">DEALER_GROUP:</asp:Label><br />
                    <asp:DropDownList ID="ddlSearchDealerGroup" runat="server" SkinID="MediumDropDown" AutoPostBack="False" TabIndex="3"></asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblSearchDealer" runat="server">DEALER:</asp:Label><br />
                    <asp:DropDownList ID="ddlSearchDealer" runat="server" SkinID="MediumDropDown" AutoPostBack="True" TabIndex="4"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSearchProdCode" runat="server">PRODUCT_CODE:</asp:Label><br />
                    <asp:TextBox ID="txtSearchProductCode" TabIndex="5" runat="server" SkinID="MediumTextBox" MaxLength="5"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="lblSearchRiskType" runat="server">RISK_TYPE:</asp:Label><br />
                    <asp:DropDownList ID="ddlSearchRiskType" runat="server" SkinID="MediumDropDown" AutoPostBack="False" TabIndex="6"></asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblSearchCoverageType" runat="server">COVERAGE_TYPE:</asp:Label><br />
                    <asp:DropDownList ID="ddlSearchCoverageType" runat="server" SkinID="MediumDropDown" AutoPostBack="False" TabIndex="7"></asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblSearchPurposeCode" runat="server">PURPOSE:</asp:Label><br />
                    <asp:DropDownList ID="ddlSearchPurposeCode" runat="server" SkinID="MediumDropDown" AutoPostBack="False" TabIndex="8"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSearchQuestionSetCode" runat="server">QUESTION_SET:</asp:Label><br />
                    <asp:DropDownList ID="ddlSearchQuestionSetCode" runat="server" SkinID="MediumDropDown" AutoPostBack="False" TabIndex="9"></asp:DropDownList>
                </td>
                <td>&nbsp;<br />
                    <asp:Button ID="btnSearch" runat="server" Text="Search" SkinID="SearchRightButton" TabIndex="11" />
                    <asp:Button ID="btnClearSearch" runat="server" Text="Clear" SkinID="AlternateRightButton" TabIndex="12" />
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
                        <asp:Label ID="lblConfigQuestionSetID" Text='<%# GetGuidStringFromByteArray(Container.DataItem("CONFIG_QUESTION_SET_ID")) %>'
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
                <asp:TemplateField Visible="True" HeaderText="QUESTION_SET" SortExpression="QUESTION_SET_DESC">
                    <ItemTemplate>
                        <asp:Label ID="lblQuestionSet" Text='<%#Container.DataItem("QUESTION_SET_DESC")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="PURPOSE" SortExpression="PURPOSE_XCD_DESC">
                    <ItemTemplate>
                        <asp:Label ID="lblPurpose" Text='<%#Container.DataItem("PURPOSE_XCD_DESC")%>' runat="server" />
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
                <asp:TemplateField Visible="True" HeaderText="DEALER_GROUP" SortExpression="DEALER_GROUP_DESC">
                    <ItemTemplate>
                        <asp:Label ID="lblDealerGrp" Text='<%#Container.DataItem("DEALER_GROUP_DESC")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="DEALER" SortExpression="DEALER_DESC">
                    <ItemTemplate>
                        <asp:Label ID="lblDealer" Text='<%#Container.DataItem("DEALER_DESC")%>' runat="server">
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="PRODUCT_CODE" SortExpression="PRODUCT_CODE">
                    <ItemTemplate>
                        <asp:Label ID="lblProdCode" Text='<%#Container.DataItem("PRODUCT_CODE")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="RISK_TYPE" SortExpression="RISK_TYPE_DESC">
                    <ItemTemplate>
                        <asp:Label ID="lblRiskType" Text='<%#Container.DataItem("RISK_TYPE_DESC")%>' runat="server" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="COVERAGE_TYPE" SortExpression="COVERAGE_TYPE_DESC">
                    <ItemTemplate>
                        <asp:Label ID="lblCoverageType" Text='<%#Container.DataItem("COVERAGE_TYPE_DESC")%>' runat="server" />
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