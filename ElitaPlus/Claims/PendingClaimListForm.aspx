<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PendingClaimListForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.PendingClaimListForm" MasterPageFile="~/Navigation/masters/content_default.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <table cellspacing="2" width="100%" align="center" border="0">
        <tr>
            <td align="center">
                <table id="tblSearch" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                    border-left: #999999 1px solid; width: 100%; border-bottom: #999999 1px solid;
                    height: 76px" cellspacing="0" cellpadding="6" width="100%" align="center" bgcolor="#f1f1f1"
                    border="0">
                    <!--fef9ea-->
                    <tr>
                        <td align="center">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td nowrap align="left" width="1%">
                                        <asp:Label ID="LabelSearchClaimNumber" runat="server">CLAIM_NUMBER</asp:Label>:
                                    </td>
                                    <td nowrap align="left" width="1%" colspan="2">
                                        <asp:Label ID="LabelSearchCustomerCertificate" runat="server">Certificate</asp:Label>:
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap align="left" width="275px">
                                        <asp:TextBox ID="TextBoxSearchClaimNumber" runat="server" Width="75%" AutoPostBack="False"
                                            CssClass="FLATTEXTBOX"></asp:TextBox>
                                    </td>
                                    <td nowrap align="left" width="75%" colspan="2">
                                        <asp:TextBox ID="TextBoxSearchCertificate" runat="server" Width="70%" AutoPostBack="False"
                                            CssClass="FLATTEXTBOX"></asp:TextBox>
                                    </td>
                                   </tr> 
                                   <tr><td height="1">&nbsp;</td></tr>
                                    <tr>
                                        <td nowrap align="left">
                                            <asp:Label ID="LabelSearchServiceDealer" runat="server">Dealer</asp:Label>:
                                        </td>
                                        <td nowrap align="left">
                                        </td>
                                        <td nowrap align="left">
                                        </td>
                                    </tr>
                                <tr>
                                    <td style="height: 3px" nowrap align="left" width="50%">
                                        <asp:DropDownList ID="cboSearchDealer" runat="server" Width="76%" CssClass="FLATTEXTBOX">
                                        </asp:DropDownList>
                                    </td>
                                    <td style="height: 3px" nowrap align="left" width="40%">
                                    </td>
                                   <td nowrap align="right">
                                        <asp:Button ID="btnClearSearch" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
                                            cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                            Width="90px" CssClass="FLATBUTTON" Text="Clear" Height="20px"></asp:Button>&nbsp;&nbsp;
                                        <asp:Button ID="btnSearch" Style="background-image: url(../Navigation/images/icons/search_icon.gif);
                                            cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                            Width="90px" CssClass="FLATBUTTON" Text="Search" Height="20px"></asp:Button>
                                    </td>
                                </tr>
                             </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
		<tr>
			<td colSpan="2">
				 <hr style="height: 1px;"/>
			</td>
		</tr>
        <tr>
            <td align="center" width="100%">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr id="trPageSize" runat="server">
                        <td valign="top" align="left" width="85%">
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
                        <td nowrap align="right" width="8%">
                            <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td align="center">
                            <asp:GridView ID="PendingGrid" runat="server" Width="100%" OnRowCommand="RowCommand"
                                OnRowCreated="ItemCreated" CellPadding="1" AllowSorting="True" AutoGenerateColumns="False"
                                AllowPaging="True" CssClass="DATAGRID">
                                <SelectedRowStyle CssClass="SELECTED"></SelectedRowStyle>
                                <EditRowStyle CssClass="EDITROW"></EditRowStyle>
                                <AlternatingRowStyle Wrap="False" CssClass="ALTROW"></AlternatingRowStyle>
                                <RowStyle CssClass="ROW"></RowStyle>
                                <HeaderStyle CssClass="HEADER"></HeaderStyle>
                                <Columns>
                                    <asp:TemplateField ShowHeader="false">
                                        <ItemStyle HorizontalAlign="Center" Width="3%" Height="15px" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnView" runat="server" CausesValidation="False"
                                                CommandName="Select" ImageUrl="~/Navigation/images/icons/edit2.gif" CommandArgument="<%# Container.DisplayIndex %>" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DEALER">
                                        <HeaderStyle HorizontalAlign="Center" Width="12%"></HeaderStyle>
                                        <%-- <ItemStyle HorizontalAlign="Center"></ItemStyle>--%>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CERTIFICATE">
                                        <HeaderStyle HorizontalAlign="Center" Width="25%"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CLAIM_NUMBER">
                                        <HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DATE_ADDED">
                                        <HeaderStyle HorizontalAlign="Center" Width="20%"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="AUTHORIZED_AMOUNT">
                                        <HeaderStyle HorizontalAlign="Center" Width="14%"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="PRODUCT_CODE">
                                        <HeaderStyle HorizontalAlign="Center" Width="14%"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField Visible="False" HeaderText="Claim_Id"></asp:TemplateField>
                                </Columns>
                                <PagerSettings PageButtonCount="15" Mode="Numeric" />
                                <PagerStyle HorizontalAlign="Center" CssClass="PAGER"/>
                                
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
