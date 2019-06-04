<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="IssueTypeForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.IssueTypeForm" MasterPageFile="~/Navigation/masters/content_default.Master" %>

<asp:Content ID="cntMain" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">    
    <script type="text/javascript" language="javascript">
        changeScrollbarColor();
    </script>
    <table class="TABLESEARCH" id="tblMain" width="100%" style="top: 0;">
        <tr>
            <td style="width: 200px; padding-left: 10px; text-align: left;" nowrap="nowrap">
                <div style="height: 20px;">
                    <asp:Label ID="SearchCodeLabel" runat="server">CODE</asp:Label>:</div>
                <asp:TextBox ID="moCodeText" runat="server" AutoPostBack="False" Height="15px" Width="180px"
                    CssClass="FLATTEXTBOX"></asp:TextBox>
            </td>
            <td style="width: 150px; text-align: left;" nowrap="nowrap">
                <div style="height: 20px;">
                    <asp:Label ID="Label1" runat="server">DESCRIPTION</asp:Label>:</div>
                <asp:TextBox ID="moDescriptionText" runat="server" AutoPostBack="False" Height="15px"
                    Width="150px" CssClass="FLATTEXTBOX"></asp:TextBox>
            </td>
            <td style="text-align: right; padding-right: 10px;" nowrap="nowrap">
                <asp:Button ID="ClearButton" Style="background-image: url(~/Navigation/images/icons/clear_icon.gif); cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false" Width="90px" Text="Clear" Height="20px" CssClass="FLATBUTTON"></asp:Button>
                &nbsp;
                <asp:Button ID="SearchButton" Style="background-image: url(~/Navigation/images/icons/search_icon.gif); cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false" Width="90px" Text="Search" Height="20px" CssClass="FLATBUTTON"></asp:Button>
            </td>
        </tr>
    </table>
    <hr style="width: 100%; height: 1px; text-align: center; height: 1px;" />
    <table width="100%">
        <tr id="trPageSize" runat="server">
            <td style="vertical-align: top; text-align: left;">
                <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>: &nbsp;
                <asp:DropDownList ID="cboPageSize" runat="server" AutoPostBack="true" Width="50px">
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
            <td style="text-align: right;">
                <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:DataGrid ID="Grid" runat="server" Width="100%" AutoGenerateColumns="False" OnItemCommand="ItemCommand"
                    OnItemCreated="ItemCreated" CellPadding="1" AllowSorting="True" AllowPaging="True"
                    CssClass="DATAGRID">
                    <ItemStyle BackColor="White"></ItemStyle>
                    <AlternatingItemStyle BackColor="#F1F1F1"></AlternatingItemStyle>
                    <Columns>
                        <asp:TemplateColumn>
                            <ItemStyle Width="30" Height="15px" />
                            <ItemTemplate>
                                <asp:ImageButton ID="EditButton_WRITE" Style="cursor: hand;" runat="server" CommandName="EditRecord"
                                    ImageUrl="~/Navigation/images/icons/edit2.gif"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn>
                            <ItemStyle Width="30" Height="15px" />
                            <ItemTemplate>
                                <asp:ImageButton ID="DeleteButton_WRITE" Style="cursor: hand;" runat="server" CommandName="DeleteRecord"
                                    ImageUrl="~/Navigation/images/icons/trash.gif"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn Visible="False" HeaderText="Id">
                            <ItemTemplate>
                                &gt;
                                <asp:Label ID="IdLabel" runat="server">
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn SortExpression="CODE" HeaderText="CODE">
                            <ItemTemplate>
                                <asp:Label ID="CodeLabel" runat="server" Visible="True">
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="CodeTextBox" runat="server" Visible="True"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn SortExpression="DESCRIPTION" HeaderText="DESCRIPTION">
                            <ItemTemplate>
                                <asp:Label ID="DescriptionLabel" runat="server" Visible="True">
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="DescriptionTextBox" runat="server" Visible="True"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn SortExpression="ISSYSTEMGENERATED" HeaderText="ISSYSTEMGENERATED">
                            <ItemTemplate>
                                <asp:Label ID="IsSystemGenLabel" runat="server" Visible="True">
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="IsSystemGenDropdown" runat="server" Visible="True">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn SortExpression="ISSELFCLEANING" HeaderText="ISSELFCLEANING">
                            <ItemTemplate>
                                <asp:Label ID="IsSelfCleaningLabel" runat="server" Visible="True">
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="IsSelfCleaningDropDown" runat="server" Visible="True">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                    <PagerStyle HorizontalAlign="Center" CssClass="PAGER" PageButtonCount="15" Mode="NumericPages">
                    </PagerStyle>
                </asp:DataGrid>
            </td>
        </tr>
    </table>
    <input id="HiddenDeletePromptResponse" type="hidden" runat="server" designtimedragdrop="261" />
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <asp:Button ID="NewButton_WRITE" Style="background-image: url(~/Navigation/images/icons/add_icon.gif);
                                                                                                                                                                                                                                   	                                             	                                                                                                                                                                                                                                                                                  	cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
        Width="100px" Text="New" Height="20px" CssClass="FLATBUTTON"></asp:Button>
    <asp:Button ID="SaveButton_WRITE" Style="background-image: url(~/Navigation/images/icons/save_icon.gif);
                                                                                                                                                                                                                                   	                                             	                                                                                                                                                                                                                                                                                  	                                             	cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
        Width="100px" Text="Save" Height="20px" CssClass="FLATBUTTON"></asp:Button>&nbsp;
    <asp:Button ID="CancelButton" Style="background-image: url(~/Navigation/images/icons/cancel_icon.gif);
                                                                                                                                                                                                                                   	                                             	                                                                                                                                                                                                                                                                                  	                                             	                                             	cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
        Width="100px" Text="Cancel" Height="20px" CssClass="FLATBUTTON"></asp:Button>&nbsp;
</asp:Content>
