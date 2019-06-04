<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="../Navigation/masters/ElitaBase.Master" Theme="Default" CodeBehind="TaskListForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.TaskListForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table class="searchGrid" border="0" cellpadding="0" cellspacing="0">
        <tbody>
            <tr>
                <td>
                    <asp:Label ID="lblSearchCode" runat="server">CODE:</asp:Label><br />
                    <asp:TextBox ID="txtSearchCode" runat="server" SkinID="MediumTextBox" />
                </td>
                <td>
                    <asp:Label ID="lblSearchDesc" runat="server">DESCRIPTION:</asp:Label><br />
                    <asp:TextBox ID="txtSearchDesc" runat="server" SkinID="MediumTextBox" />
                </td>
                <td>
                    &nbsp;<br />
                    <asp:Button ID="btnSearch" runat="server" Text="Search" SkinID="SearchRightButton" /></td><td><br />
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
            AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" SkinID="DetailPageGridView">
            <Columns>
                <asp:TemplateField Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblTaskID" Text='<%# GetGuidStringFromByteArray(Container.DataItem("Task_ID")) %>'
                            runat="server">
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="CODE">
                    <ItemTemplate>
                        <asp:Label ID="lblCode" Text='<%#Container.DataItem("CODE")%>' runat="server">
                        </asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtCode" runat="server" Visible="True" SkinID="SmallTextBox"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="DESCRIPTION">
                    <ItemTemplate>
                        <asp:Label ID="lblDesc" runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtDesc" runat="server" Visible="True" SkinID="SmallTextBox"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="RETRY_COUNT">
                    <ItemTemplate>
                        <asp:Label ID="lblRetryCnt" runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtRetryCnt" runat="server" Visible="True" SkinID="SmallTextBox"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="RETRY_DELAY_SECONDS">
                    <ItemTemplate>
                        <asp:Label ID="lblRetryDelay" runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtRetryDelay" runat="server" Visible="True" SkinID="SmallTextBox"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="TIMEOUT_SECONDS">
                    <ItemTemplate>
                        <asp:Label ID="lblTimeOut" runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtTimeOut" runat="server" Visible="True" SkinID="SmallTextBox"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="PARAMETERS">
                    <ItemTemplate>
                        <asp:Label ID="lblParams" runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtParams" runat="server" Visible="True" SkinID="MediumTextBox"></asp:TextBox>
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
                    <tr><td>
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" SkinID="PrimaryRightButton"></asp:Button></td><td>
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
