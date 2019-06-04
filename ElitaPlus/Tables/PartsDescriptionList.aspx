<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master"
    CodeBehind="PartsDescriptionList.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.PartsDescriptionList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <!--d5d6e4-->
        <tr>
            <td class="BLANKROW">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td nowrap style="width: 10%" align="left">
                <asp:Label ID="SearchDescriptionLabel" runat="server">RISK_GROUP</asp:Label>:
            </td>
            <td align="left">
                &nbsp;
                <asp:DropDownList ID="RiskGroupDropdown" runat="server" Visible="True" AutoPostBack="True">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="BLANKROW">
            </td>
            <td>
            </td>
        </tr>        
        <tr>
            <td colspan="2">
                <asp:GridView ID="Grid" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                    AllowSorting="True" CellPadding="1" OnRowCreated="ItemCreated" OnRowCommand="ItemCommand"
                    CssClass="DATAGRID">
                    <SelectedRowStyle CssClass="SELECTED"></SelectedRowStyle>
                    <EditRowStyle CssClass="EDITROW"></EditRowStyle>
                    <AlternatingRowStyle Wrap="False" CssClass="ALTROW"></AlternatingRowStyle>
                    <RowStyle CssClass="ROW"></RowStyle>
                    <HeaderStyle CssClass="HEADER"></HeaderStyle>
                    <Columns>
                        <asp:TemplateField ShowHeader="false">
                            <ItemStyle Width="3%" Height="15px" />
                            <ItemTemplate>
                                <asp:ImageButton ID="EditButton_WRITE" CausesValidation="False" runat="server" CommandName="EditRecord"
                                    ImageUrl="../Navigation/images/icons/edit2.gif" CommandArgument="<%#Container.DisplayIndex %>">
                                </asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemStyle Width="5%"></ItemStyle>
                            <ItemTemplate>
                                <asp:ImageButton ID="DeleteButton_WRITE" runat="server" CommandName="DeleteRecord"
                                    ImageUrl="../Navigation/images/icons/trash.gif" CommandArgument="<%#Container.DisplayIndex %>">
                                </asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="False" HeaderText="Id">
                            <ItemTemplate>
                                &gt;
                                <asp:Label ID="IdLabel" runat="server" Text='<%# GetGuidStringFromByteArray(Container.DataItem("parts_description_id"))%>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="description" HeaderText="DESCRIPTION">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Width="41%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="DescriptionLabel" runat="server" Text='<%# Container.DataItem("DESCRIPTION")%>'
                                    Visible="True">
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="DescriptionTextBox" CssClass="FLATTEXTBOX_TAB" runat="server" Visible="True"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="description_english" HeaderText="DESCRIPTION_ENGLISH">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Width="41%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="DescEnglishLabel" runat="server" Text='<%# Container.DataItem("DESCRIPTION_ENGLISH")%>'
                                    Visible="True">
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="DescEnglishTextbox" runat="server" CssClass="FLATTEXTBOX_TAB" Visible="True"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField SortExpression="code" HeaderText="CODE">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Width="10%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="CodeLabel" runat="server" Text='<%# Container.DataItem("CODE")%>'
                                    Visible="True">
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="CodeTextbox" runat="server" CssClass="FLATTEXTBOX_TAB" Visible="True"></asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings PageButtonCount="15" Mode="Numeric" />
                    <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <asp:Button ID="btnBack" Style="background-image: url(../Navigation/images/icons/back_icon.gif);
        cursor: hand; background-repeat: no-repeat" TabIndex="185" runat="server" Font-Bold="false"
        Width="90px" CssClass="FLATBUTTON" Text="Back" Height="20px"></asp:Button>&nbsp;
    <asp:Button ID="NewButton_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
        Width="100px" CssClass="FLATBUTTON" Height="20px" Text="New"></asp:Button>
    <asp:Button ID="SaveButton_WRITE" Style="background-image: url(../Navigation/images/icons/save_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
        Width="100px" CssClass="FLATBUTTON" Height="20px" Text="Save"></asp:Button>&nbsp;
    <asp:Button ID="CancelButton" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
        Width="100px" CssClass="FLATBUTTON" Height="20px" Text="Cancel"></asp:Button>&nbsp;
</asp:Content>
