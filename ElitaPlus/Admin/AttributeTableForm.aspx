<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="cc1" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AttributeTableForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.AttributeTableForm" MasterPageFile="../Navigation/masters/ElitaBase.Master"
    Theme="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto">
        <Scripts>
            <%--<asp:ScriptReference Path="~/Navigation/scripts/ComunaSuggest.js" />--%>
        </Scripts>
    </asp:ScriptManager>
    <table id="TableFixed" cellspacing="0" cellpadding="0" border="0" width="100%" class="searchGrid">
        <tbody>
            <tr>
                <td>
                    <asp:Label ID="moTableNameLabel" runat="server">TABLE_NAME</asp:Label><br />
                    <asp:TextBox ID="moTableNameText" runat="server" AutoPostBack="False" TabIndex="0"
                        SkinID="MediumTextBox"></asp:TextBox>
                    <cc1:AutoCompleteExtender ID="TableNameAutoCompleye" runat="server" TargetControlID="moTableNameText"
                        ServiceMethod="PopulateTableDrop" MinimumPrefixLength='1'>
                    </cc1:AutoCompleteExtender>
                    <asp:Button ID="btnClearSearch" runat="server" Text="Clear" SkinID="AlternateLeftButton" OnClick="btnClearSearch_Click" />
                    <asp:Button ID="btnSearch" runat="server" Text="Search" SkinID="SearchButton" OnClick="btnSearch_Click" />
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="container">
        <div class="contentZoneHome">
            <div class="dataContainer">
                <h2 class="dataGridHeader">
                    Search results for table names</h2>
                <table width="100%" border="0" class="dataGrid" cellpadding="0" cellspacing="0">
                    <tr id="trPageSize" runat="server">
                        <td class="bor">
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
                        <td align="right" class="bor">
                            <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <div>
                    <asp:GridView ID="DataGridTables" runat="server" Width="100%" AutoGenerateColumns="False"
                        AllowPaging="True" SkinID="DetailPageGridView">
                        <SelectedRowStyle Wrap="True" />
                        <EditRowStyle Wrap="True" />
                        <AlternatingRowStyle Wrap="True" />
                        <RowStyle Wrap="True" />
                        <HeaderStyle />
                        <Columns>
                            <asp:TemplateField SortExpression="TABLE_NAME" HeaderText="TABLE_NAME">
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEditClaim" runat="server" CommandName="AttributesCMD" CommandArgument='<%# DataBinder.Eval(Container.DataItem, "TABLE_NAME").ToString() %>'
                                        CausesValidation="false"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField SortExpression="ATTRIBUTE_COUNT" HeaderText="ATTRIBUTE_COUNT" />
                        </Columns>
                        <PagerSettings PageButtonCount="10" Mode="Numeric" Position="TopAndBottom" />
                        <PagerStyle />
                    </asp:GridView>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
