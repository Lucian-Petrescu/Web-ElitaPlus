<%@ Page Language="vb" ValidateRequest="false" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master"
    CodeBehind="RouteMaintenanceListForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.RouteMaintenanceListForm" %>

<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%" style="height: 93%">
        <tr style="height:10%;padding-bottom:5px;">
            <td style="vertical-align:top;" colspan="2">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td>
                            <table id="tblSearch" style="border: #999999 1px solid;" cellspacing="0"
                                cellpadding="4" align="center" bgcolor="#f1f1f1" border="0" width="100%">
                                <tr>
                                    <%--<td>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>--%>
                                    <td style="vertical-align:top;text-align:center;" class="CenteredTD">
                                        <uc1:MultipleColumnDDLabelControl ID="moRouteMultipleDrop" runat="server"></uc1:MultipleColumnDDLabelControl>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <hr>
                                    </td>
                                </tr>
                                <tr>
                                    <%--<td>
                                    </td>--%>
                                    <td style="vertical-align:top;text-align:center;" class="CenteredTD">
                                        <uc1:MultipleColumnDDLabelControl ID="moServiceNetworkMultipleDrop" runat="server">
                                        </uc1:MultipleColumnDDLabelControl>
                                    </td>
                                </tr>
                                <tr>
                                    <%--<td>
                                    </td>--%>
                                    <td style="text-align: right;">
                                        <asp:Button ID="btnClearSearch" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
                                            cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                            Width="90px" CssClass="FLATBUTTON" Text="Clear"></asp:Button>&#160;
                                        <asp:Button ID="btnSearch" Style="background-image: url(../Navigation/images/icons/search_icon.gif);
                                            cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                            Width="90px" CssClass="FLATBUTTON" Text="Search"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <hr style="height: 1px">
                        </td>
                    </tr>                    
                </table>
            </td>
        </tr>
        <tr id="trPageSize" runat="server" style="height:1%;">
            <td style="vertical-align:top;text-align:left; padding-bottom:5px;">
                <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>: &#160;
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
            <td style="text-align: right;padding-bottom:5px;">
                <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2" style="vertical-align:top;">
                <asp:DataGrid ID="Grid" runat="server" Width="100%" OnItemCommand="ItemCommand" OnItemCreated="ItemCreated"
                    AllowSorting="True" AllowPaging="True" CellPadding="1" BorderColor="#999999"
                    BackColor="#DEE3E7" BorderWidth="1px" BorderStyle="Solid" AutoGenerateColumns="False">
                    <SelectedItemStyle Wrap="False" BackColor="LavenderBlush"></SelectedItemStyle>
                    <EditItemStyle Wrap="False" BackColor="AliceBlue"></EditItemStyle>
                    <AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
                    <ItemStyle Wrap="False" BackColor="White"></ItemStyle>
                    <HeaderStyle></HeaderStyle>
                    <Columns>
                        <asp:TemplateColumn>
                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="5%"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                            <ItemTemplate>
                                <asp:ImageButton Style="cursor: hand;" ID="btnEdit" ImageUrl="../Navigation/images/icons/edit2.gif"
                                    runat="server" CommandName="SelectAction"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn Visible="False" HeaderText="Id"></asp:BoundColumn>
                        <asp:BoundColumn SortExpression="2" HeaderText="CODE">
                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="20%"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn SortExpression="1" HeaderText="DESCRIPTION">
                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="60%"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn SortExpression="1" HeaderText="SERVICE_NETWORK">
                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="20%"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        </asp:BoundColumn>
                    </Columns>
                    <PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7"
                        PageButtonCount="15" Mode="NumericPages"></PagerStyle>
                </asp:DataGrid>
            </td>
        </tr>
    </table>
    <%--</td> </tr> </table>--%>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPanelButtons" runat="server" ID="cntButtons">
    <asp:Button ID="btnNew" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
        Width="90px" CssClass="FLATBUTTON" Text="New"></asp:Button>
</asp:Content>
