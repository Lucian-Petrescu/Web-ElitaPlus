<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master"
    CodeBehind="TransAllMappingListForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.TransAllMappingListForm" %>

<%@ Register Src="../Common/MultipleColumnDDLabelControl.ascx" TagName="MultipleColumnDDLabelControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td colspan="2">
                <table cellpadding="2" cellspacing="0" border="0" style="border: #999999 1px solid;
                    background-color: #f1f1f1; height: 98%; width: 100%">
                    <tr>
                        <td class="BLANKROW">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" style="text-align: left">
                            <uc1:MultipleColumnDDLabelControl ID="ddlDealer" runat="server" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td style="text-align: right">
                            <asp:Button ID="btnClearSearch" runat="server" Text="Clear" CssClass="FLATBUTTON BUTTONSTYLE_CLEAR">
                            </asp:Button>&nbsp;&nbsp;
                            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="FLATBUTTON BUTTONSTYLE_SEARCH">
                            </asp:Button>
                        </td>
                    </tr>
                    <tr>
                        <td class="BLANKROW" colspan="2">
                            &nbsp;
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="BLANKROW">
            </td>
            <td>
            </td>
        </tr>
        <tr id="trPageSize" runat="server">
            <td>
                <asp:Label ID="lblPageSize" runat="server">Page_Size:</asp:Label>&nbsp;
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
            <td style="height: 22px; text-align: right">
                <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
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
                <asp:GridView ID="grdResults" runat="server" Width="100%" AllowPaging="True" AllowSorting="False"
                    CellPadding="1" AutoGenerateColumns="False" CssClass="DATAGRID" OnRowCreated="ItemCreated">
                    <SelectedRowStyle CssClass="SELECTED"></SelectedRowStyle>
                    <EditRowStyle CssClass="EDITROW"></EditRowStyle>
                    <AlternatingRowStyle Wrap="False" CssClass="ALTROW"></AlternatingRowStyle>
                    <RowStyle CssClass="ROW"></RowStyle>
                    <HeaderStyle CssClass="HEADER"></HeaderStyle>
                    <Columns>
                        <asp:TemplateField Visible="False">
                            <ItemTemplate>
                                <%#Container.DataItem("TRANSALL_MAPPING_ID")%>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="">
                            <ItemStyle HorizontalAlign="Center" Width="3%" Height="15px" />
                            <ItemTemplate>
                                <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="False" CommandName="SelectAction"
                                    ImageUrl="~/Navigation/images/icons/edit2.gif" CommandArgument="<%#Container.DisplayIndex %>" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DEALER" SortExpression="DEALER_NAME">
                            <ItemTemplate>
                                <%#Container.DataItem("DEALER_NAME")%>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="PACKAGE_NAME">
                            <ItemTemplate>
                                <%#Container.DataItem("TRANSALL_PACKAGE")%>
                            </ItemTemplate>
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
    <asp:Button ID="btnAdd" TabIndex="185" runat="server"  Style="background-image: url(../Navigation/images/icons/add_icon.gif);
     cursor: hand; background-repeat: no-repeat" Font-Bold="false"
     Width="120px" Text="New" Height="20px" CssClass="FLATBUTTON BUTTONSTYLE_NEW"></asp:Button>
</asp:Content>
