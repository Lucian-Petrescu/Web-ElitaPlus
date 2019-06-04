<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="../Navigation/masters/ElitaBase.Master"
    Theme="Default" CodeBehind="EventConfigListForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.EventConfigListForm" %>

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
                    <asp:DropDownList ID="ddlSearchCompany" runat="server" SkinID="MediumDropDown" AutoPostBack="False" TabIndex="2"></asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblSearchCountry" runat="server">COUNTRY:</asp:Label><br />
                    <asp:DropDownList ID="ddlSearchCountry" runat="server" SkinID="MediumDropDown" AutoPostBack="False" TabIndex="3"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblSearchDealerGroup" runat="server">DEALER_GROUP:</asp:Label><br />
                    <asp:DropDownList ID="ddlSearchDealerGroup" runat="server" SkinID="MediumDropDown" AutoPostBack="False" TabIndex="3"></asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblSearchDealer" runat="server">DEALER:</asp:Label><br />
                    <asp:DropDownList ID="ddlSearchDealer" runat="server" SkinID="MediumDropDown" AutoPostBack="False" TabIndex="4"></asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="lblSearchProdCode" runat="server">PRODUCT_CODE</asp:Label>:<br />
                    <asp:TextBox ID="txtSearchProdCode" runat="server" SkinID="MediumTextBox" TabIndex="5" />
                </td>
                <td>
                    <asp:Label ID="lblSearchCoverageType" runat="server">COVERAGE_TYPE</asp:Label>:<br />
                    <asp:DropDownList ID="ddlSearchCoverageType" runat="server" SkinID="MediumDropDown" AutoPostBack="False" TabIndex="6"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td></td>
                <td></td>
                <td>&nbsp;<br />
                    <asp:Button ID="btnSearch" runat="server" Text="Search" SkinID="SearchRightButton" TabIndex="7" />
                    <asp:Button ID="btnClearSearch" runat="server" Text="Clear" SkinID="AlternateRightButton" TabIndex="8" />
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
                        <asp:DropDownList ID="cboPageSize" runat="server" SkinID="SmallDropDown" AutoPostBack="true" TabIndex="9">
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
            AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" SkinID="DetailPageGridView">
            <Columns>
                <asp:TemplateField Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblEventConfigID" Text='<%# GetGuidStringFromByteArray(Container.DataItem("EVENT_CONFIG_ID")) %>'
                            runat="server">
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="COMPANY_GROUP">
                    <ItemTemplate>
                        <asp:Label ID="lblCompanyGroup" Text='<%#Container.DataItem("COMPANY_GROUP_DESC")%>' runat="server">
                        </asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList runat="server" ID="ddlCompanyGroup" SkinID="SmallDropDown" TabIndex="10" />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="COMPANY">
                    <ItemTemplate>
                        <asp:Label ID="lblCompany" Text='<%#Container.DataItem("COMPANY_DESC")%>' runat="server">
                        </asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList runat="server" ID="ddlCompany" SkinID="SmallDropDown" TabIndex="11" />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="COUNTRY">
                    <ItemTemplate>
                        <asp:Label ID="lblCountry" Text='<%#Container.DataItem("COUNTRY_DESC")%>' runat="server">
                        </asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList runat="server" ID="ddlCountry" SkinID="SmallDropDown" TabIndex="12" />
                    </EditItemTemplate>
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
                <asp:TemplateField Visible="True" HeaderText="DEALER">
                    <ItemTemplate>
                        <asp:Label ID="lblDealer" Text='<%#Container.DataItem("DEALER_DESC")%>' runat="server">
                        </asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList runat="server" ID="ddlDealer" SkinID="SmallDropDown" TabIndex="14" />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="PRODUCT_CODE">
                    <ItemTemplate>
                        <asp:Label ID="lblProdCode" Text='<%#Container.DataItem("PRODUCT_CODE")%>' runat="server">
                        </asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtProdCode" runat="server" Visible="True" SkinID="SmallTextBox" TabIndex="15"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="COVERAGE_TYPE">
                    <ItemTemplate>
                        <asp:Label ID="lblCoverageType" runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList runat="server" ID="ddlCoverageType" SkinID="SmallDropDown" TabIndex="16" />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="EVENT_TYPE">
                    <ItemTemplate>
                        <asp:Label ID="lblEventType" runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList runat="server" ID="ddlEventType" SkinID="SmallDropDown" OnSelectedIndexChanged="ddlEventType_SelectedIndexChanged" AutoPostBack="true" TabIndex="17" />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="EVENT_ARGUMENT">
                    <ItemTemplate>
                        <asp:Label ID="lblEventArgument" runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList runat="server" ID="ddlEventArgument" SkinID="SmallDropDown" TabIndex="18" />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:ImageButton ID="EditButton_WRITE" runat="server" CausesValidation="False" CommandName="SelectAction"
                            ImageUrl="~/App_Themes/Default/Images/edit.png" CommandArgument="<%#Container.DisplayIndex %>" />
                        <asp:ImageButton ID="DeleteButton_WRITE" ImageUrl="~/App_Themes/Default/Images/icon_delete.png"
                            runat="server" CommandName="DeleteRecord" CommandArgument="<%#Container.DisplayIndex %>" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <table>
                            <tr>
                                <td>
                                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" SkinID="PrimaryRightButton"></asp:Button></td>
                                <td>
                                    <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" SkinID="AlternateRightButton"></asp:LinkButton></td>
                            </tr>
                        </table>
                    </EditItemTemplate>
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
