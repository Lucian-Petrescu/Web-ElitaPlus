<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master"
    CodeBehind="CurrencyForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.CurrencyForm" %>

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
                            <asp:Label ID="CodeLabel" runat="server">CODE</asp:Label>:&nbsp;
                        </td>
                        <td style="text-align: Left; white-space: nowrap; vertical-align: middle; width: 20%">
                            <asp:TextBox ID="SearchCodeTextBox" runat="server" Width="99px" AutoPostBack="False"
                                CssClass="FLATTEXTBOX"></asp:TextBox>
                        </td>
                        <td style="text-align: right; white-space: nowrap; vertical-align: middle; width: 1%">
                            <asp:Label ID="SearchDescriptionLabel" runat="server">DESCRIPTION</asp:Label>:
                        </td>
                        <td style="text-align: left; white-space: nowrap; vertical-align: middle; width: 40%">
                            <asp:TextBox ID="SearchDescriptionTextBox" runat="server" Width="65%" AutoPostBack="False"
                                CssClass="FLATTEXTBOX"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; white-space: nowrap; vertical-align: middle; width: 1%">
                            <asp:Label ID="NotationLabel" runat="server">NOTATION</asp:Label>:&nbsp;
                        </td>
                        <td style="text-align: Left; white-space: nowrap; vertical-align: middle; width: 20%">
                            <asp:TextBox ID="SearchNotationTextBox" runat="server" Width="99px" AutoPostBack="False"
                                CssClass="FLATTEXTBOX"></asp:TextBox>
                        </td>
                        <td style="text-align: right; white-space: nowrap; vertical-align: middle; width: 1%">
                            <asp:Label ID="ISOCodeLabel" runat="server">ISO_CODE</asp:Label>:
                        </td>
                        <td style="text-align: left; white-space: nowrap; vertical-align: middle; width: 40%">
                            <asp:TextBox ID="SearchISOCodeTextBox" runat="server" Width="65%" AutoPostBack="False"
                                CssClass="FLATTEXTBOX"></asp:TextBox>
                        </td>
                    </tr>
                    <%--<tr>
                        <td colspan="2">
                            &nbsp;
                        </td>
                    </tr>--%>
                    <tr>
                        <td nowrap align="right">
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
            <td style="HEIGHT: 22px; vertical-align: middle" align="left">
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
                        <asp:TemplateField ShowHeader="false">
                            <ItemStyle HorizontalAlign="Center" Width="3%" Height="15px" />
                            <ItemTemplate>
                                <asp:ImageButton ID="EditButton_WRITE" runat="server" CausesValidation="False" CommandName="SelectAction"
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
                        <asp:TemplateField Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="moCurrencyId" Text='<%# GetGuidStringFromByteArray(Container.DataItem("CURRENCY_ID")) %>'
                                    runat="server">
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="True" HeaderText="CODE">
                            <ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="moCodeLabel" Text='<%#Container.DataItem("CODE")%>' runat="server">
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="moCodeText" runat="server" Visible="True"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="True" HeaderText="DESCRIPTION">
                            <ItemStyle HorizontalAlign="Left" Width="45%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="moDescriptionLabel" Text='<%#Container.DataItem("DESCRIPTION")%>'
                                    runat="server">
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="moDescriptionText" runat="server" Visible="True" Width="75%"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="True" HeaderText="NOTATION">
                            <ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="moNotationLabel" Text='<%#Container.DataItem("NOTATION")%>' runat="server">
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="moNotationText" runat="server" Visible="True" Width="35%"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="True" HeaderText="ISO_CODE">
                            <ItemStyle HorizontalAlign="Left" Width="30%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="moISOCodeLabel" Text='<%#Container.DataItem("ISO_CODE")%>' runat="server">
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="moISOCodeText" runat="server" Visible="True" Width="75%"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
                </asp:GridView>
            </td>
        </tr>
    </table>
    <input id="HiddenDeletePromptResponse" type="hidden" runat="server" designtimedragdrop="261"/>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <asp:Button ID="btnNew" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_NEW" Text="NEW">
    </asp:Button>
    <asp:Button ID="btnSave" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_SAVE" Text="SAVE"
        Visible="false"></asp:Button>
    <asp:Button ID="btnCancel" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_UNDO"
        Text="Cancel" Visible="false"></asp:Button>
</asp:Content>
