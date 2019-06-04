<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master"
    CodeBehind="MfgCoverageExtForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.MfgCoverageExtForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td valign="top">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
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
                        <td valign="top" align="center" colspan="2">
                            <asp:GridView ID="Grid" runat="server" Width="100%" OnRowCreated="ItemCreated" OnRowCommand="ItemCommand"
                                AllowPaging="True" AllowSorting="True" CellPadding="1" AutoGenerateColumns="False"
                                CssClass="DATAGRID">
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
                                    <asp:TemplateField Visible="False">
                                        <ItemTemplate>
                                            &gt;
                                            <asp:Label ID="lblMfgCoverageExtID" runat="server">
                                            </asp:Label>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField SortExpression="dealer_name" HeaderText="DEALER">
                                        <ItemStyle HorizontalAlign="Left" Width="47%"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblDealer" runat="server">
                                            </asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="ddlstDealer" runat="server" Width="216px">
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="ext_warranty">
                                        <ItemStyle HorizontalAlign="Left" Width="47%"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:Label ID="lblExtWarranty" runat="server" Visible="True">
                                            </asp:Label>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="txtExtWarranty" CssClass="FLATTEXTBOX_TAB" runat="server" Visible="True"></asp:TextBox>
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
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <asp:Button ID="BackButton_WRITE" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_BACK"
        Text="BACK"></asp:Button>
    <asp:Button ID="NewButton_WRITE" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_NEW"
        Text="NEW"></asp:Button>
    <asp:Button ID="SaveButton_WRITE" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_SAVE"
        Text="SAVE" TabIndex="600"></asp:Button>
    <asp:Button ID="CancelButton" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_UNDO"
        Text="Cancel"></asp:Button>
</asp:Content>
