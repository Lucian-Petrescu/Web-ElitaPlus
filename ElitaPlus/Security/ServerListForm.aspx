<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master"
    CodeBehind="ServerListForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Security.ServerListForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <style type="text/css">
        tr.HEADER th
        {
            text-align: center;
        }
    </style>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td colspan="4" style="text-align: center; width: 100%">
                <table cellpadding="4" cellspacing="0" border="0" style="border: #999999 1px solid;
                    background-color: #f1f1f1; height: 98%; width: 100%">
                    <tr>
                        <td style="height: 7px;" colspan="4">
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; white-space: nowrap; vertical-align: middle; width: 1%">
                            <asp:Label ID="SearchEnvironmentLabel" runat="server">ENVIRONMENT</asp:Label>:
                        </td>
                        <td style="text-align: Left; white-space: nowrap; vertical-align: middle; width: 20%">
                            <asp:TextBox ID="SearchEnvironmentTextBox" runat="server" Width="99px" AutoPostBack="False"
                                CssClass="FLATTEXTBOX"></asp:TextBox>
                        </td>
                        <td style="text-align: right; white-space: nowrap; vertical-align: middle; width: 1%">
                            <asp:Label ID="SearchDescriptionLabel" runat="server">DESCRIPTION</asp:Label>:&nbsp;
                        </td>
                        <td style="text-align: left; white-space: nowrap; vertical-align: middle; width: 40%">
                            <asp:TextBox ID="SearchDescriptionTextBox" runat="server" Width="65%" AutoPostBack="False"
                                CssClass="FLATTEXTBOX"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap align="right">
                            &nbsp;
                        </td>
                        <td nowrap style="text-align: right" colspan="4">
                            <asp:Button ID="btnClearSearch" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
                                cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                Width="90px" Text="Clear" CssClass="FLATBUTTON" Height="20px"></asp:Button>&nbsp;
                            <asp:Button ID="btnSearch" Style="background-image: url(../Navigation/images/icons/search_icon.gif);
                                cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                Width="90px" Text="Search" CssClass="FLATBUTTON" Height="20px"></asp:Button>&nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <hr style="height: 1px" />
            </td>
        </tr>
        <tr id="trPageSize" runat="server">
            <td style="height: 22px; vertical-align: middle" align="left">
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
            <td style="height: 22px; text-align: right; vertical-align: middle">
                <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="Grid" runat="server" Width="100%" OnRowCreated="RowCreated" OnRowCommand="RowCommand"
                    AllowPaging="True" AllowSorting="True" CellPadding="1" AutoGenerateColumns="False"
                    CssClass="DATAGRID">
                    <SelectedRowStyle CssClass="SELECTED"></SelectedRowStyle>
                    <EditRowStyle CssClass="EDITROW"></EditRowStyle>
                    <AlternatingRowStyle Wrap="False" CssClass="ALTROW"></AlternatingRowStyle>
                    <RowStyle CssClass="ROW"></RowStyle>
                    <HeaderStyle CssClass="HEADER"></HeaderStyle>
                    <Columns>
                        <asp:TemplateField HeaderStyle-Width="18px">
                            <ItemStyle Width="18px" CssClass="CenteredTD" />
                            <ItemTemplate>
                                <asp:ImageButton Style="cursor: hand;" ID="EditButton_WRITE" ImageUrl="../Navigation/images/icons/edit2.gif"
                                    runat="server" CommandName="Select"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblServerID" runat="server" Text='<%# GetGuidStringFromByteArray(Container.DataItem("Server_Id"))%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField DataField="ENVIRONMENT" SortExpression="ENVIRONMENT" ReadOnly="true"
                            HeaderText="ENVIRONMENT" HeaderStyle-HorizontalAlign="Center" />
                        <asp:BoundField DataField="DESCRIPTION" SortExpression="DESCRIPTION" ReadOnly="true"
                            HeaderText="DESCRIPTION" HeaderStyle-HorizontalAlign="Center" />
                    </Columns>
                    <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <input id="HiddenDeletePromptResponse" type="hidden" runat="server" designtimedragdrop="261">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <asp:Button ID="btnNew" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_NEW" Text="NEW">
    </asp:Button>
</asp:Content>
