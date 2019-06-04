<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DealerGroupForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.DealerGroupForm" MasterPageFile="../Navigation/masters/ElitaBase.Master"
    Theme="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js">  
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table width="100%" border="0" class="searchGrid">
        <tr>
            <td valign="middle" width="30%" style="height: 40px">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td style="width: 200px; height: 13px" align="left">
                            <asp:Label ID="SearchDescriptionLabel" runat="server">DEALER_GROUP_NAME</asp:Label>:
                        </td>
                        <td style="height: 13px">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td style="width: 150px; height: 13px" align="left">
                            <asp:Label ID="SearchCodeLabel" runat="server">DEALER_GROUP_CODE</asp:Label>:
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 200px" nowrap="nowrap" align="left">
                            <asp:TextBox ID="SearchDescriptionTextBox" runat="server"  SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td style="width: 150px" nowrap="nowrap" align="left">
                            <asp:TextBox ID="SearchCodeTextBox" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                        </td>
                        <td style="height: 40px" valign="middle" align="right" nowrap="nowrap">
                            <asp:Button ID="ClearButton" runat="server" SkinID="AlternateLeftButton" Text="Clear"></asp:Button>
                            <asp:Button ID="SearchButton" runat="server" SkinID="SearchLeftButton" Text="Search"></asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <!-- new layout start -->
    <div class="dataContainer">
        <h2 class="dataGridHeader">
            <asp:Label runat="server" ID="moSearchResultsHeader">SEARCH_RESULTS_FOR_DEALER_GROUP</asp:Label>
        </h2>
        <div>
            <table width="100%" class="dataGrid">
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
                <tr>
            </table>
        </div>
        <div>
            <asp:GridView ID="Grid" runat="server" Width="100%" OnRowCreated="RowCreated" OnRowCommand="RowCommand"
                AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" SkinID="DetailPageGridView" PageSize="30">
                <SelectedRowStyle Wrap="True"></SelectedRowStyle>
                <EditRowStyle Wrap="True"></EditRowStyle>
                <AlternatingRowStyle Wrap="True"></AlternatingRowStyle>
                <RowStyle Wrap="True"></RowStyle>
                <Columns>
                    <asp:TemplateField ShowHeader="false">
                        <ItemStyle HorizontalAlign="Center" Width="3%" Height="15px" />
                        <ItemTemplate>
                            <asp:ImageButton ID="EditButton_WRITE" runat="server" CausesValidation="False" CommandName="EditRecord"
                                ImageUrl="~/Navigation/images/icons/edit2.gif" CommandArgument="<%#Container.DisplayIndex %>" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="false">
                        <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                        <ItemTemplate>
                            <asp:ImageButton ID="DeleteButton_WRITE" ImageUrl="~/Navigation/images/icons/trash.gif"
                                runat="server" CommandName="DeleteRecord" CommandArgument="<%#Container.DisplayIndex %>">
                            </asp:ImageButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="False" HeaderText="Id">
                        <ItemTemplate>
                            <asp:Label ID="IdLabel" runat="server" Text='<%# GetGuidStringFromByteArray(Container.DataItem("DEALER_GROUP_ID"))%>'>
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="Description" HeaderText="DEALER_GROUP_NAME">
                        <ItemStyle HorizontalAlign="Left" Width="50%"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="DescriptionLabel" runat="server" Visible="True" Text='<%# Container.DataItem("DESCRIPTION")%>'>
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="DescriptionTextBox" runat="server" Visible="True"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="Code" HeaderText="DEALER_GROUP_CODE">
                        <ItemStyle HorizontalAlign="Left" Width="17%"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="CodeLabel" runat="server" Text='<%# Container.DataItem("CODE")%>'
                                Visible="True">
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:TextBox ID="CodeTextBox" runat="server" Visible="True"></asp:TextBox>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ACCTING_BY_GROUP">
                        <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="AcctingByGroupLabel" runat="server" Visible="True" Text='<%# Container.DataItem("ACCTING_BY_GROUP_Desc")%>'>
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="AcctingByGroupDropdown" runat="server" Visible="True">
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="USE_CLIENT_DEALER_CODE">
                        <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="UseClientDealerCode" runat="server" Visible="True" Text='<%# Container.DataItem("USE_CLIENT_CODE_YNDESC")%>'>
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="UseClientDealerCodeDropdown" runat="server" Visible="True">
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="AUTO_REJ_ERR_TYPE">
                        <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                        <ItemTemplate>
                            <asp:Label ID="lblAutoRejErrType" runat="server" Visible="True" Text='<%# Container.DataItem("AUTO_REJ_ERR_TYPE_DESC")%>'>
                            </asp:Label>
                        </ItemTemplate>
                        <EditItemTemplate>
                            <asp:DropDownList ID="AutoRejErrTypeDropdown" runat="server" Visible="True">
                            </asp:DropDownList>
                        </EditItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerSettings PageButtonCount="15" Mode="Numeric" />
                <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
            </asp:GridView>
            <asp:GridView ID="Grid1" runat="server" Width="100%" AllowPaging="True" AllowSorting="True"
                CellPadding="1" AutoGenerateColumns="False" Visible="false" CssClass="DATAGRID">
                <SelectedRowStyle CssClass="SELECTED"></SelectedRowStyle>
                <EditRowStyle CssClass="EDITROW"></EditRowStyle>
                <AlternatingRowStyle Wrap="False" CssClass="ALTROW"></AlternatingRowStyle>
                <RowStyle CssClass="ROW"></RowStyle>
                <HeaderStyle CssClass="HEADER"></HeaderStyle>
                <Columns>
                    <asp:TemplateField ShowHeader="false">
                        <ItemStyle HorizontalAlign="Center" Width="3%" Height="15px" />
                        <ItemTemplate>
                            <asp:ImageButton ID="EditButton_WRITE" runat="server" CausesValidation="False" CommandName="EditRecord"
                                ImageUrl="~/Navigation/images/icons/edit2.gif" CommandArgument="<%#Container.DisplayIndex %>" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField ShowHeader="false">
                        <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                        <ItemTemplate>
                            <asp:ImageButton ID="DeleteButton_WRITE" ImageUrl="~/Navigation/images/icons/trash.gif"
                                runat="server" CommandName="DeleteRecord" CommandArgument="<%#Container.DisplayIndex %>">
                            </asp:ImageButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="False" HeaderText="Id">
                        <ItemTemplate>
                            <asp:Label ID="IdLabel" runat="server">
                            </asp:Label>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="Description" HeaderText="Dealer Group Name">
                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="35%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="Code" HeaderText="Dealer Group Code">
                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="30%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Accounting By Group">
                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="35%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                </Columns>
                <PagerSettings PageButtonCount="15" Mode="Numeric" />
                <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
            </asp:GridView>
        </div>
        <div>
            <input id="HiddenDeletePromptResponse" type="hidden" runat="server" designtimedragdrop="261">
        </div>
        <div class="btnZone">
            <asp:Button ID="CancelButton" runat="server" Text="Back" SkinID="PrimaryLeftButton">
            </asp:Button>&nbsp;
            <asp:Button ID="SaveButton_WRITE" runat="server" Text="Save" SkinID="PrimaryRightButton">
            </asp:Button>&nbsp;
            <asp:Button ID="NewButton_WRITE" runat="server" Text="New" SkinID="AlternateLeftButton">
            </asp:Button>&nbsp;
        </div>
</asp:Content>
