<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Reports/content_Report.Master" 
 CodeBehind="ClaimsPendingPaymentReportForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.ClaimsPendingPaymentReportForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" style="vertical-align: top;">
        <tr>
            <td class="BLANKROW" colspan="4" style="vertical-align: top;" width="100%">
            </td>
        </tr>       
        <tr id="Tr1" runat="server">
            <td nowrap align="right" colspan="1" style="width: 25%">
            </td>
            <td colspan="3">
                <table cellspacing="0" cellpadding="0" width="75%" border="0">                    
                   
                    <tr>
                        <td class="BLANKROW" colspan="4" style="vertical-align: top;">
                        </td>
                    </tr>
                    <tr id="BatchRow" runat="server">
                        <td   valign="middle" nowrap align="right">
                            *<asp:Label ID="lblbeginbatch" runat="server">Begin_Batch_Number</asp:Label>:
                        </td>
                        <td  nowrap align="left">
                            &nbsp;
                            <asp:TextBox ID="txtBeginbatch" TabIndex="1" runat="server" AutoPostBack="false" CssClass="FLATTEXTBOX"></asp:TextBox>&nbsp;
                        </td>
                        <td valign="middle" nowrap align="left">
                            &nbsp;&nbsp;
                           *<asp:Label ID="lblendbatch" runat="server">End_Batch_Number</asp:Label>:
                        </td>
                        <td nowrap>
                            &nbsp;
                            <asp:TextBox ID="txtEndbatch" TabIndex="1" runat="server" AutoPostBack="false" CssClass="FLATTEXTBOX"></asp:TextBox>&nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td class="BLANKROW" colspan="4" style="vertical-align: top;">
                        </td>
                    </tr>
                    <tr>                                                                     
                         <td valign="middle" nowrap align="left" colspan="2">
                            <asp:CheckBox ID="chkSvcCode" Text="INCLUDE_SERVICE_CENTER_CODE" AutoPostBack="false"
                                runat="server" TextAlign="Left"></asp:CheckBox>
                        </td>
                         <td>
                        </td>
                        <td>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

    <script>
   
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <asp:Button ID="btnGenRpt" Style="background-image: url(../Navigation/images/viewIcon2.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Text="View" Width="100px"
        CssClass="FLATBUTTON" Height="20px" CausesValidation="False"></asp:Button>
</asp:Content>
