<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master"
    CodeBehind="InvoiceListForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.InvoiceListForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <style type="text/css">
        tr.HEADER th
        {
            text-align: center;
        }
    </style>
    <table cellspacing="2" cellpadding="2" width="100%" align="center" border="0">
        <tr>
            <td align="center" colspan="2">
                <table id="tblSearch" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                    border-left: #999999 1px solid; width: 98%; border-bottom: #999999 1px solid;
                    height: 76px" cellspacing="0" cellpadding="6" width="98%" align="center" bgcolor="#f1f1f1"
                    border="0">
                    <!--fef9ea-->
                    <tr>
                        <td align="center">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td nowrap align="left" width="1%">
                                        <asp:Label ID="LabelSearchInvoiceNumber"  runat="server">INVOICE_NUMBER</asp:Label>:
                                    </td>
                                    <td nowrap align="left" width="1%" colspan="2">
                                        <asp:Label ID="LabelSearchPayeeName" runat="server">PAYEE</asp:Label>:
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap align="left" width="275">
                                        <asp:TextBox ID="TextBoxSearchInvoiceNumber" Width="75%"  runat="server" CssClass="FLATTEXTBOX"
                                            AutoPostBack="False"></asp:TextBox>
                                    </td>
                                    <td nowrap align="left" width="75%" colspan="2">
                                        <asp:TextBox ID="TextBoxSearchPayeeName" Width="80%"  runat="server" CssClass="FLATTEXTBOX"
                                            AutoPostBack="False"></asp:TextBox>
                                    </td>
                                    <tr>
                                        <td nowrap align="left">
                                            <asp:Label ID="LabelSearchCreatedDate" runat="server">DATE_CREATED</asp:Label>:
                                        </td>
                                        <td nowrap align="left">
                                            <asp:Label ID="LabelSearchClaimNumber" runat="server">CLAIM_NUMBER</asp:Label>:
                                        </td>
                                        <td nowrap align="left">
                                            <asp:Label ID="LabelSearchInvoiceAmount" runat="server">INVOICE_AMT</asp:Label>:
                                        </td>
                                    </tr>
                                 <tr>
                                    <td style="height: 12px" nowrap align="left" width="50%">
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td style="width: 118px">
                                                    <asp:TextBox ID="TextBoxSearchCreatedDate" runat="server" CssClass="FLATTEXTBOX"
                                                        AutoPostBack="False"></asp:TextBox>
                                                </td>
                                                <td>
                                                    <asp:ImageButton ID="ImageButtonSearchCreatedDate" runat="server" Visible="True"
                                                        ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:ImageButton>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                    <td nowrap align="left" width="30%">
                                        <asp:TextBox ID="TextBoxSearchClaimNumber" runat="server" CssClass="FLATTEXTBOX"
                                            Width="95%" AutoPostBack="False"></asp:TextBox>
                                    </td>
                                    <td nowrap align="left" width="20%">
                                        <asp:TextBox ID="TextBoxSearchInvoiceAmount" runat="server" CssClass="FLATTEXTBOX"
                                            Width="75%" AutoPostBack="False"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 12px" colspan="3" align="left">
                                        <asp:Label ID="lblSortBy" runat="server">Sort By</asp:Label>:
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap align="left">
                                        <asp:DropDownList ID="cboSortBy" runat="server" Width="75%" AutoPostBack="False">
                                        </asp:DropDownList>
                                    </td>
                                    <td nowrap style="text-align:right;" colspan="3">
                                        <asp:Button ID="btnClearSearch" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
                                            cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                            CssClass="FLATBUTTON" Width="90px" Height="20px" Text="Clear"></asp:Button>&nbsp;&nbsp;
                                        <asp:Button ID="btnSearch" Style="background-image: url(../Navigation/images/icons/search_icon.gif);
                                            cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                            CssClass="FLATBUTTON" Width="90px" Height="20px" Text="Search"></asp:Button>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <hr style="height: 1px">
            </td>
        </tr>
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
            <td  style="text-align: right;" >
                <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="Grid" runat="server" Width="100%" AllowPaging="True" AllowSorting="False"
                    CellPadding="1" AutoGenerateColumns="False" CssClass="DATAGRID">
                    <SelectedRowStyle CssClass="SELECTED"></SelectedRowStyle>
                    <EditRowStyle CssClass="EDITROW"></EditRowStyle>
                    <AlternatingRowStyle Wrap="False" CssClass="ALTROW"></AlternatingRowStyle>
                    <RowStyle CssClass="ROW"></RowStyle>
                    <HeaderStyle CssClass="HEADER"></HeaderStyle>
                    <Columns>
                        <asp:TemplateField>
                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="3%"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                            <ItemTemplate>
                                <asp:ImageButton Style="cursor: hand;" ID="btnEdit" ImageUrl="../Navigation/images/icons/edit2.gif"
                                    runat="server" CommandName="SelectAction"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:BoundField HeaderText="Invoice_Number">
                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="15%"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Payee">
                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Authorization_Number">
                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Claim_Number">
                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="DATE_CREATED">
                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField HeaderText="Invoice_Amt">
                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundField>
                        <asp:BoundField Visible="False" HeaderText="Claim_Invoice_Id"></asp:BoundField>
                    </Columns>
                    <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
