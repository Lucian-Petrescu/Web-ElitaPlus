<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AccountingReversalListForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.AccountingReversalListForm" MasterPageFile="~/Navigation/masters/content_default.Master" %>

<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<asp:Content ID="Content1" runat="server" ContentPlaceHolderID="ContentPanelMainContentBody">
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
                        <td class="LABELCOLUMN" style="vertical-align: bottom">
                            *&nbsp;<asp:Label ID="moAccountingCompanyLABEL" runat="server">COMPANY:</asp:Label>
                        </td>
                        <td style="vertical-align: bottom; text-align: left">
                            <uc1:MultipleColumnDDLabelControl ID="moUserCompanyMultipleDrop" runat="server">
                            </uc1:MultipleColumnDDLabelControl>
                        </td>
                    </tr>
                    <tr>
                        <td class="BLANKROW" colspan="2">
                            <hr />
                        </td>
                    </tr>
                    <tr>
                        <td class="LABELCOLUMN">
                            <asp:Label ID="Label1" runat="server">START_DATE:</asp:Label>
                        </td>
                        <td>
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="moTxtStartDate" runat="server" Style="width: 150px;"></asp:TextBox>
                            <asp:ImageButton ID="btnStartDate" runat="server" Visible="True" ImageUrl="~/Common/Images/calendarIcon2.jpg">
                            </asp:ImageButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="LABELCOLUMN">
                            <asp:Label ID="Label2" runat="server">END_DATE:</asp:Label>
                        </td>
                        <td>
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="moTxtEndDate" runat="server" Style="width: 150px;"></asp:TextBox>
                            <asp:ImageButton ID="btnEndDate" runat="server" Visible="True" ImageUrl="~/Common/Images/calendarIcon2.jpg">
                            </asp:ImageButton>
                        </td>
                    </tr>
                    <tr>
                        <td class="LABELCOLUMN">
                            <asp:Label ID="Label3" runat="server">BATCH_NUMBER:</asp:Label>
                        </td>
                        <td>
                            &nbsp;&nbsp;&nbsp;<asp:TextBox ID="moTxtBatchNumber" runat="server" Style="width: 150px;"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td class="BLANKROW" colspan="2">
                            <hr />
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
        <tr id="trPageSize" runat="server" style="display: none">
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
            <td colspan="2">
                <asp:DataGrid ID="moDataGrid" runat="server" Width="100%" CssClass="DATAGRID_NOWRAP"
                    BorderStyle="Solid" BackColor="#DEE3E7" BorderColor="#999999" CellPadding="1"
                    AllowSorting="True" BorderWidth="1px" AutoGenerateColumns="False" AllowPaging="True">
                    <SelectedItemStyle Wrap="False" BackColor="Transparent"></SelectedItemStyle>
                    <EditItemStyle Wrap="False" BackColor="AliceBlue"></EditItemStyle>
                    <AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
                    <ItemStyle Wrap="False" BackColor="White"></ItemStyle>
                    <HeaderStyle  Wrap="False" ForeColor="#003399"></HeaderStyle>
                    <Columns>
                        <asp:BoundColumn DataField="acct_transmission_id" Visible="false"></asp:BoundColumn>
                        <asp:TemplateColumn>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle Wrap="False" HorizontalAlign="Center" Width="30px" BackColor="White">
                            </ItemStyle>
                            <ItemTemplate>
                                <asp:ImageButton ID="EditButton_WRITE" Style="cursor: hand" runat="server" ImageUrl="../Navigation/images/icons/edit2.gif"
                                    CommandName="EditRecord"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="file_name" HeaderText="FILE_NAME"></asp:BoundColumn>
                        <asp:BoundColumn DataField="created_date" HeaderText="CREATED_DATE" DataFormatString="{0:d}">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="credit_amount" HeaderText="CREDIT_AMOUNT" DataFormatString="{0:c}">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="debit_amount" HeaderText="DEBIT_AMOUNT" DataFormatString="{0:c}">
                        </asp:BoundColumn>
                    </Columns>
                    <PagerStyle HorizontalAlign="Left" ForeColor="DarkSlateBlue" BackColor="#DEE3E7"
                        PageButtonCount="15" Mode="NumericPages"></PagerStyle>
                </asp:DataGrid>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" runat="server" ContentPlaceHolderID="ContentPanelButtons">
    <asp:Button ID="btnBack" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_BACK" Text="Back">
    </asp:Button>
</asp:Content>
