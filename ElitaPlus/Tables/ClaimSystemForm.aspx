<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ClaimSystemForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.ClaimSystemForm" MasterPageFile="~/Navigation/masters/content_default.Master" %>

<asp:Content ID="cntMain" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <table class="TABLESEARCH" id="tblSearch" cellspacing="0" cellpadding="8" align="center"
        id="tblMain" width="100%" style="top: 0">
        <tr>
            <td>
                <asp:Label ID="SearchDescriptionLabel" runat="server" CssClass="TITLELABEL">Description:</asp:Label>
                &nbsp;&nbsp;&nbsp;<asp:TextBox ID="SearchDescriptionTextBox" Width="200" runat="server" CssClass="FLATTEXTBOX"></asp:TextBox>
            </td>
            <td style="text-align: right">
                <asp:Button ID="ClearButton" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
                    cursor: hand; background-repeat: no-repeat" runat="server" Width="90px" Text="Clear"
                    Height="20px" CssClass="FLATBUTTON" Font-Bold="false"></asp:Button>&nbsp;
                <asp:Button ID="SearchButton" Style="background-image: url(../Navigation/images/icons/search_icon.gif);
                    cursor: hand; background-repeat: no-repeat" runat="server" Width="90px" Text="Search"
                    Height="20px" CssClass="FLATBUTTON" Font-Bold="false"></asp:Button>
            </td>
        </tr>
    </table>
    <hr style="width: 100%; height: 1px" align="center" size="1">
    <table width="100%">
        <tr id="trPageSize" runat="server">
            <td valign="top" align="left">
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
            <td style="text-align: right">
                <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="vertical-align: top" id="tdGrid">
                <asp:GridView ID="grdView" runat="server" Width="100%" OnRowCreated="ItemCreated"
                    OnRowCommand="ItemCommand" AllowPaging="True" AllowSorting="True" CellPadding="1"
                    AutoGenerateColumns="False" CssClass="DATAGRID">
                    <SelectedRowStyle CssClass="SELECTED"></SelectedRowStyle>
                    <EditRowStyle CssClass="EDITROW"></EditRowStyle>
                    <AlternatingRowStyle Wrap="False" CssClass="ALTROW"></AlternatingRowStyle>
                    <RowStyle CssClass="ROW"></RowStyle>
                    <HeaderStyle CssClass="HEADER"></HeaderStyle>
                    <Columns>
                        <asp:TemplateField Visible="true" ShowHeader="false">
                            <ItemStyle HorizontalAlign="Center" Width="3%" Height="15px" />
                            <ItemTemplate>
                                <asp:ImageButton ID="EditButton_WRITE" runat="server" CausesValidation="False" CommandName="EditRecord"
                                    ImageUrl="~/Navigation/images/icons/edit2.gif" CommandArgument="<%#Container.DisplayIndex %>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField ShowHeader="false">
                            <ItemStyle HorizontalAlign="Center" Width="3%" Height="15px" />
                            <ItemTemplate>
                                <asp:ImageButton ID="DeleteButton_WRITE" runat="server" CausesValidation="False"
                                    CommandName="DeleteRecord" ImageUrl="../Navigation/images/icons/trash.gif" CommandArgument="<%#Container.DisplayIndex %>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="False" HeaderText="Id">
                            <ItemTemplate>
                                &gt;
                                <asp:Label ID="IdLabel" runat="server">
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="Code" HeaderText="CODE">
                            <ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="CodeLabel" runat="server" Visible="True">
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="CodeTextBox" CssClass="FLATTEXTBOX_TAB" runat="server" Visible="True"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                         <asp:TemplateField SortExpression="Description" HeaderText="Description">
                            <ItemStyle HorizontalAlign="Left" Width="35%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="DescriptionLabel" runat="server" Visible="True">
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="DescriptionTextBox" CssClass="FLATTEXTBOX_TAB" runat="server" Visible="True"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="NEW_CLAIM" HeaderText="NEW_CLAIM">
                            <ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="NewClaimLabel" runat="server" Visible="True">
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="NewClaimDropdown" runat="server" Visible="True" Width="60">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="PAY_CLAIM" HeaderText="PAY_CLAIM">
                            <ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="PayClaimLabel" runat="server" Visible="True">
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="PayClaimDropdown" runat="server" Visible="True" Width="60">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="MAINTAIN_CLAIM" HeaderText="MAINTAIN_CLAIM">
                            <ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="MaintainClaimLabel" runat="server" Visible="True">
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="MaintainClaimDropDown" runat="server" Visible="True" Width="60">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings PageButtonCount="15" Mode="Numeric" />
                    <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <input id="HiddenDeletePromptResponse" type="hidden" runat="server" designtimedragdrop="261">

    <script type="text/javascript">
        if (document.getElementById("tdGrid")) {
            document.getElementById("tdGrid").style.height = parent.document.getElementById("Navigation_Content").clientHeight - 350;
        }
    </script>

</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <asp:Button ID="NewButton_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Width="100px" Text="New"
        Height="20px" CssClass="FLATBUTTON" Font-Bold="false"></asp:Button>
    <asp:Button ID="SaveButton_WRITE" Style="background-image: url(../Navigation/images/icons/save_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Width="100px" Text="Save"
        Height="20px" CssClass="FLATBUTTON" Font-Bold="false"></asp:Button>&nbsp;
    <asp:Button ID="CancelButton" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Width="100px" Text="Cancel"
        Height="20px" CssClass="FLATBUTTON" Font-Bold="false"></asp:Button>&nbsp;
</asp:Content>
