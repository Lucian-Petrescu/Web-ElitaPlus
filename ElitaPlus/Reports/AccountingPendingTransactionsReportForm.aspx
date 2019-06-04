<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Reports/content_Report.Master"
    CodeBehind="AccountingPendingTransactionsReportForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.AccountingPendingTransactionsReportForm" %>

<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" style="vertical-align: top;">
        <tr>
            <td class="BLANKROW" colspan="3" style="vertical-align: top;">
            </td>
        </tr>
        <tr>
            <td nowrap align="right" style="width: 25%">
            </td>
            <td align="center" width="50%">
                <table style="width: 75%; height: 50px" cellspacing="2" cellpadding="0" border="0">
                    <tr>
                        <td valign="bottom" align="center" width="100%" colspan="2">
                            <uc1:MultipleColumnDDLabelControl ID="multipleDropControl" runat="server"></uc1:MultipleColumnDDLabelControl>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" width="100%" colspan="2">
                            <hr id="ddSeparator1" style="width: 100%; height: 1px">
                        </td>
                    </tr>
                    <tr>
                        <td nowrap align="right" width="35%">
                            *
                            <asp:RadioButton ID="rEventType" AutoPostBack="false" Checked="False" runat="server" TextAlign="left" Text="SELECT_ALL_EVENTS">
                            </asp:RadioButton>
                        </td>
                        <td style="height: 24px" nowrap align="center" width="65%">
                            <asp:Label ID="EventTypeLabel" runat="server">OR A SINGLE EVENT</asp:Label>:
                            <asp:DropDownList ID="cboEventType" runat="server" AutoPostBack="false" Width="150px">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" width="100%" colspan="2">
                            <hr style="width: 100%; height: 1px">
                        </td>
                    </tr>
                </table>
            </td>
            <td nowrap align="right"  style="width: 25%">
            </td>
        </tr>
         <tr>
            <td class="BLANKROW" colspan="3">
            </td>
        </tr>       
          <tr>
            <td class="BLANKROW" colspan="3">
            </td>
        </tr>     
          <tr>
            <td class="BLANKROW" colspan="3">
            </td>
        </tr>     
          <tr>
            <td class="BLANKROW" colspan="3">
            </td>
        </tr>     
          <tr>
            <td class="BLANKROW" colspan="3">
            </td>
        </tr>     
          <tr>
            <td class="BLANKROW" colspan="3">
            </td>
        </tr>     
          <tr>
            <td class="BLANKROW" colspan="3">
            </td>
        </tr>      
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <asp:Button ID="btnGenRpt" Style="background-image: url(../Navigation/images/viewIcon2.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
        Width="100px" Text="View" Height="20px" CssClass="FLATBUTTON"></asp:Button>
</asp:Content>
