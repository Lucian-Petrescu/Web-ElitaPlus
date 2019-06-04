<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="Elita" TagName="MessageController" Src="~/Common/MessageController.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CountryTaxList.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.CountryTaxList" MasterPageFile="../Navigation/masters/ElitaBase.Master"
    Theme="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="container">
        <div class="contentZoneHome">
            <div class="dataContainer">
                <div>
                    <asp:GridView ID="grdResults" runat="server" Width="100%" AllowPaging="true" AllowSorting="False"
                        CellPadding="1" AutoGenerateColumns="False" SkinID="DetailPageGridView">
                        <SelectedRowStyle Wrap="true"></SelectedRowStyle>
                        <EditRowStyle Wrap="true"></EditRowStyle>
                        <AlternatingRowStyle Wrap="False"></AlternatingRowStyle>
                        <RowStyle></RowStyle>
                        <HeaderStyle></HeaderStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="Edit">
                                <ItemStyle HorizontalAlign="Center" Width="3%" Height="15px" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="False" CommandName="SelectAction"
                                        ImageUrl="~/App_Themes/Default/Images/edit.png" CommandArgument="<%#Container.DisplayIndex %>" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TAX_TYPE">
                                <ItemTemplate>
                                    <%#Container.DataItem("TTAX")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="product_tax_type">
                                <ItemTemplate>
                                    <%#Container.DataItem("product_tax_type")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="COMPANY_TYPE" HeaderText="COMPANY_TYPE">
                                <ItemTemplate>
                                    <%#Container.DataItem("COMPANY_TYPE")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="EFFECTIVE_DATE" HeaderText="EFFECTIVE_DATE">
                                <ItemTemplate>
                                    <%#Container.DataItem("EFFECTIVE_DATE")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="EXPIRATION_DATE" HeaderText="EXPIRATION_DATE">
                                <ItemTemplate>
                                    <%#Container.DataItem("EXPIRATION_DATE")%>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="COUNtrY_NAME" HeaderText="COUNTRY">
                                <ItemTemplate>
                                    <%# Container.DataItem("COUNtrY_NAME")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="DEALER" HeaderText="DEALER">
                                <ItemTemplate>
                                    <%# Container.DataItem("DEALER")%>
                                </ItemTemplate>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="lblCountryTaxID" runat="server" Text='<%# GetGuidStringFromByteArray(Container.DataItem("COUNtrY_TAX_ID"))%>'>
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerSettings PageButtonCount="15" Mode="Numeric" Position="TopAndBottom" />
                        <PagerStyle HorizontalAlign="Center"></PagerStyle>
                    </asp:GridView>
                </div>
                <div class="btnZone">
                    <asp:Button ID="btnAdd_WRITE" runat="server" SkinID="AlternateLeftButton" Text="New"
                        CommandName="WRITE"></asp:Button>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
