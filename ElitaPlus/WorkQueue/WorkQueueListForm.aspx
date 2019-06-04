<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WorkQueueListForm.aspx.vb"
    Theme="Default" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.WorkQueueListForm"
    EnableSessionState="True" MasterPageFile="~/Navigation/masters/ElitaBase.Master" %>

<%@ Register TagPrefix="Elita" TagName="MessageController" Src="~/Common/MessageController.ascx" %>
<asp:Content ContentPlaceHolderID="HeadPlaceHolder" runat="server">
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
<asp:Content ContentPlaceHolderID="MessagePlaceHolder" runat="server">
    <Elita:MessageController runat="server" ID="moMessageController" />
</asp:Content>
<asp:Content ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
  <asp:PlaceHolder ID="PlaceHolder1" runat="server">
         
    <table width="100%" class="searchGrid" runat="server" id="searchGrid" border="0" cellspacing="0" cellpadding="0">
        <tbody>
            <tr>
                <td>
                    <asp:Label runat="server" ID="moWorkQueueNameLabel">QUEUE_NAME</asp:Label>
                    <br />
                    <asp:TextBox runat="server" ID="moWorkQueueName" SkinID="MediumTextBox" />
                </td>
                <td>
                    <asp:Label runat="server" ID="moCompanyNameLabel">COMPANY</asp:Label>
                    <br />
                    <asp:DropDownList runat="server" ID="moCompanyName" SkinID="MediumDropDown">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label runat="server" ID="moActionLabel">ACTION</asp:Label>
                    <br />
                    <asp:DropDownList runat="server" ID="moAction" SkinID="SmallDropDown">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label runat="server" ID="moActiveObLabel">ACTIVE_ON</asp:Label>
                    <br />
                    <asp:TextBox runat="server" ID="moActiveOn" SkinID="SmallTextBox" />
                    <asp:ImageButton ID="imgActiveOn" runat="server" Style="vertical-align: bottom" ImageUrl="~/App_Themes/Default/Images/calendar.png" />
                </td>
                <td>
                    &nbsp
                </td>
            </tr>
            <tr>
                <td colspan="4" align="right">
                    <asp:Button runat="server" ID="btnSearch" SkinID="SearchRightButton" Text="SEARCH" />
                    <asp:Button runat="server" ID="btnClear" SkinID="AlternateRightButton" Text="CLEAR" />
                </td>
            </tr>
        </tbody>
    </table>
  </asp:PlaceHolder>
</asp:Content>
<asp:Content  ContentPlaceHolderID="BodyPlaceHolder" runat="server">
  <asp:PlaceHolder ID="PlaceHolder2" runat="server">
    <div class="dataContainer" runat="server" id="moSearchResults" visible="false">
        <h2 class="dataGridHeader">
            <asp:Label runat="server" ID="moSearchResultsHeader">SEARCH_RESULTS_FOR_WORK_QUEUE</asp:Label>
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
        <asp:GridView ID="WorkQueueGrid" runat="server" Width="100%" AllowPaging="True" AllowSorting="True"
            CellPadding="1" SkinID="DetailPageGridView">
            <RowStyle HorizontalAlign="Center" />
            <Columns>
                <asp:TemplateField HeaderText="Name" SortExpression="Name">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemTemplate>
                        <asp:LinkButton CommandName="SelectAction" ID="btnEditWorkQueue" runat="server" OnClientClick="document.body.style.cursor = 'wait'; if(isNotDblClick()) {return true;} else return false;"
                            CommandArgument='<%# Assurant.ElitaPlus.ElitaPlusWebApp.ElitaPlusPage.GetGuidStringFromByteArray(CType(DataBinder.Eval(Container, "DataItem.Id"), Guid).ToByteArray()) %>'
                            Text='<%# DataBinder.Eval(Container, "DataItem.Name") %>'>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="COMPANY_CODE" SortExpression="CompanyCode">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CompanyCode") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ACTION" SortExpression="ActionName">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ActionName") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="EFFECTIVE_DATE" SortExpression="Effective">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Assurant.ElitaPlus.ElitaPlusWebApp.ElitaPlusPage.GetLongDateFormattedString(CType(DataBinder.Eval(Container, "DataItem.Effective"), Date)) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="EXPIRATION_DATE" SortExpression="Expiration">
                    <ItemTemplate>
                        <asp:Label runat="server" Text='<%# Assurant.ElitaPlus.ElitaPlusWebApp.ElitaPlusPage.GetLongDateFormattedStringNullable(CType(DataBinder.Eval(Container, "DataItem.Expiration"), Nullable(Of Date))) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerSettings PageButtonCount="10" Mode="Numeric" Position="TopAndBottom" />
        </asp:GridView>
        <asp:GridView ID="WorkQueueStaffingGrid" Visible="false" runat="server" 
            Width="100%" AllowPaging="True" AllowSorting="True" CellPadding="1" SkinID="DetailPageGridView">
            <RowStyle HorizontalAlign="Center" />
            <Columns>
                <asp:TemplateField HeaderText="Name" SortExpression="Name">
                    <ItemStyle HorizontalAlign="Left" />
                    <ItemTemplate>
                        <asp:Label ID="lblName" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.Name") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="OLDEST_ITEM_DATE" SortExpression="OldestItemDatetime">
                    <ItemTemplate>
                        <asp:Label ID="lblOldestItemDate" runat="server" Text='<%# Assurant.ElitaPlus.ElitaPlusWebApp.ElitaPlusPage.GetDateFormattedString(CType(DataBinder.Eval(Container, "DataItem.OldestItemDatetime"), Date)) %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ITEMS_AVAILABLE" SortExpression="AvailableItems">
                    <ItemTemplate>
                        <asp:Label ID="lblItemsAvailable" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.AvailableItems") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="TOTAL_ITEMS" SortExpression="TotalItems">
                    <ItemTemplate>
                        <asp:Label ID="lblTotalItems" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.TotalItems") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="ITEMS_BEING_WORKED" SortExpression="ItemsBeingWorked">
                    <ItemTemplate>
                        <asp:Label ID="lblItemsBeingWorked" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ItemsBeingWorked") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="TOTAL_PEOPLE" SortExpression="TotalAvailableUsers">
                    <ItemTemplate>
                        <asp:LinkButton CommandName="WorkQueueUser" ID="btnWorkQueueUser" runat="server"
                            OnClientClick="document.body.style.cursor = 'wait'; if(isNotDblClick()) {return true;} else return false;"
                            CommandArgument='<%# Assurant.ElitaPlus.ElitaPlusWebApp.ElitaPlusPage.GetGuidStringFromByteArray(CType(DataBinder.Eval(Container, "DataItem.Id"), Guid).ToByteArray()) %>'
                            Text='<%# DataBinder.Eval(Container, "DataItem.TotalAvailableUsers") %>'>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="WORK_QUEUE_HISTORY">
                    <ItemTemplate>
                        <asp:LinkButton CommandName="WorkQueueHistory" ID="btnWorkQueueHistory" runat="server"
                            OnClientClick="document.body.style.cursor = 'wait'; if(isNotDblClick()) {return true;} else return false;"
                            CommandArgument='<%# Assurant.ElitaPlus.ElitaPlusWebApp.ElitaPlusPage.GetGuidStringFromByteArray(CType(DataBinder.Eval(Container, "DataItem.Id"), Guid).ToByteArray()) %>'
                            Text="View">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerSettings PageButtonCount="10" Mode="Numeric" Position="TopAndBottom" />
        </asp:GridView>
    </div>
    <div class="btnZone">
        <asp:Button runat="server" ID="btnAdd_WRITE" Text="New" SkinID="AlternateLeftButton" />
    </div>
  </asp:PlaceHolder>
</asp:Content>
