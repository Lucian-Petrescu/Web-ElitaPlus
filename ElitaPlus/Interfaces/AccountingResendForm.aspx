<%@ Page Language="vb" MasterPageFile="~/Navigation/masters/content_default.Master"
    AutoEventWireup="false" CodeBehind="AccountingResendForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.AccountingResendForm" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>

<asp:Content ID="cntMain" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <table cellpadding="0" cellspacing="0" border="0">
        <tr>
            <td class="BLANKROW">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="LABELCOLUMN" style="vertical-align:bottom">
                *&nbsp;<asp:Label ID="moAccountingCompanyLABEL" runat="server">COMPANY:</asp:Label>
            </td>
            <td  style="vertical-align:bottom;text-align:left">
                <uc1:multiplecolumnddlabelcontrol id="moUserCompanyMultipleDrop" runat="server"></uc1:multiplecolumnddlabelcontrol>
            </td>
        </tr>
        <tr>
            <td class="BLANKROW">
            </td>
            <td>
            </td>
        </tr>
           <tr>
            <td class="BLANKROW" colspan="2">
                <hr />
            </td>
        </tr>
         <tr>
            <td class="BLANKROW">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td colspan=2>
                <asp:DataGrid ID="moDataGrid" runat="server" Width="100%" CssClass="DATAGRID_NOWRAP"
                    BorderStyle="Solid" BackColor="#DEE3E7" BorderColor="#999999" CellPadding="1"
                    AllowSorting="True" BorderWidth="1px" AutoGenerateColumns="False" AllowPaging="True" OnItemCommand="ItemCommand">
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
                        <asp:BoundColumn DataField="batch_number_id" HeaderText="BATCH_NUMBER"></asp:BoundColumn>
                        <asp:BoundColumn DataField="created_date" HeaderText="CREATED_DATE" DataFormatString="{0:d}">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="credit_amount" HeaderText="CREDIT_AMOUNT" DataFormatString="{0:c}">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="debit_amount" HeaderText="DEBIT_AMOUNT" DataFormatString="{0:c}">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="status" HeaderText="Status">
                        </asp:BoundColumn>
                        <asp:BoundColumn DataField="reject_reason" HeaderText="REJECT_REASON">
                        </asp:BoundColumn>
                    </Columns>
                    <PagerStyle HorizontalAlign="Left" ForeColor="DarkSlateBlue" BackColor="#DEE3E7"
                        PageButtonCount="15" Mode="NumericPages"></PagerStyle>
                </asp:DataGrid>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="cntButton" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <asp:Button ID="btnBack"  runat="server" CssClass="FLATBUTTON BUTTONSTYLE_BACK"
        Text="Back" Style="width: 120px; "></asp:Button>
    <asp:Button ID="btnExecute"  runat="server" CssClass="FLATBUTTON BUTTONSTYLE_SAVE"
        Text="PROCESS_RECORDS" Style="width: 140px; "></asp:Button>
    <asp:Button ID="btnReport"  runat="server" CssClass="FLATBUTTON BUTTONSTYLE_SEARCH"
        Text="REPORT" Style="width: 120px;"></asp:Button>
    <asp:Button ID="btnDelete"  runat="server" CssClass="FLATBUTTON BUTTONSTYLE_CLEAR"
        Text="DELETE" Style="width: 120px"></asp:Button>
</asp:Content>
