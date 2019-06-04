<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Reports/content_Report.Master"  CodeBehind="BillingRegisterCanada.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.BillingRegisterCanada" %>
<%@ Register TagPrefix="uc3" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server" >

    <table cellpadding="0" cellspacing="0" border="0" style="vertical-align: top;">
        <tr>
            <td class="BLANKROW" colspan="3" style="vertical-align: top;">
            </td>
        </tr>
        <tr>
            <td nowrap align="right" colspan="1" style="width: 25%">
            </td>
            <td align="center" width="50%" colspan="2">
                <table cellspacing="0" cellpadding="0" width="75%" border="0">
                    <tr>
                        <td valign="middle" nowrap align="right">
                            *<asp:Label ID="BeginDateLabel" runat="server" Font-Bold="false">BEGIN_DATE</asp:Label>:
                        </td>
                        <td nowrap>
                            &nbsp;
                            <asp:TextBox ID="BeginDateText" TabIndex="1" runat="server" CssClass="FLATTEXTBOX"></asp:TextBox>
                            <asp:ImageButton ID="BtnBeginDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                Width="20px"></asp:ImageButton>
                        </td>
                        <td valign="middle" nowrap align="right">
                            &nbsp;&nbsp; *<asp:Label ID="EndDateLabel" runat="server" Font-Bold="false">END_DATE</asp:Label>:
                        </td>
                        <td nowrap>
                            &nbsp;
                            <asp:TextBox ID="EndDateText" TabIndex="1" runat="server" CssClass="FLATTEXTBOX"
                                Width="125px"></asp:TextBox>
                            <asp:ImageButton ID="BtnEndDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                Width="20px"></asp:ImageButton>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <asp:Panel ID="pnlComp" runat="server">
        <tr>
            <td class="BLANKROW" colspan="3" style="vertical-align: top;">
            </td>
        </tr>
        <tr>
            <td class="BLANKROW" colspan="3">
                <hr />
            </td>
        </tr>
        <tr id="trcomp" runat="server">
            <td nowrap align="right" colspan="1" style="width: 25%">
            </td>
            <td colspan="2">
                <uc3:MultipleColumnDDLabelControl ID="CompanyDropControl" runat="server">
                </uc3:MultipleColumnDDLabelControl>
            </td>
        </tr>
        </asp:Panel>
        <tr>
            <td class="BLANKROW" colspan="3">
            </td>
        </tr>
        <tr>
            <td class="BLANKROW" colspan="3">
                <hr />
            </td>
        </tr>
        <tr>
            <td nowrap align="right" colspan="1" style="width: 25%">
            </td>            
            <td colspan="2">
                &nbsp;&nbsp;<uc3:MultipleColumnDDLabelControl ID="DealerDropControl" runat="server">
                </uc3:MultipleColumnDDLabelControl>
            </td>
        </tr>
        <tr>
            <td class="BLANKROW" colspan="3">               
            </td>
        </tr>      
        <tr>
            <td nowrap align="right" colspan="1" style="width: 25%">
            </td>            
            <td colspan="2">
                &nbsp;&nbsp;<uc3:MultipleColumnDDLabelControl ID="DealerGrpDropControl" runat="server">
                </uc3:MultipleColumnDDLabelControl>
            </td>
        </tr>
        <tr>
            <td class="BLANKROW" colspan="3">               
            </td>
        </tr>
        <tr>
            <td class="BLANKROW" colspan="3">
                <hr />
            </td>
        </tr>
        <tr>
            <td nowrap align="right" colspan="1" style="width: 25%">
            </td>
            <td nowrap class="LABELCOLUMN" style="vertical-align: bottom;" height="10px">
                <asp:RadioButton ID="RadiobuttonTotalsOnly" GroupName="Total" AutoPostBack="false" runat="server" TextAlign="left"
                    Text="SHOW TOTALS ONLY"></asp:RadioButton>
            </td>
            <td>
                <asp:RadioButton ID="RadiobuttonDetail" GroupName="Total" AutoPostBack="false" runat="server" TextAlign="left"
                    Text="OR SHOW DETAIL WITH TOTALS"></asp:RadioButton>
            </td>
        </tr>
        <tr>
            <td class="BLANKROW" colspan="3">               
            </td>
        </tr>
        <tr>
            <td class="BLANKROW" colspan="3">
                <hr />
            </td>
        </tr>         
        <tr>
            <td nowrap style="vertical-align: top;" colspan="1" style="width: 25%" valign="top">
            </td>
            <td style="vertical-align: top;" nowrap align="left" colspan="2">
                <table cellspacing="0" cellpadding="0" width="100%" border="0" valign = "top">
                    <tr>
                        <td style="vertical-align: top;" nowrap align="right" width="25%">
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td nowrap align="center" valign="top">
                                        <asp:Label ID="Label5" runat="server">SELECT_REPORT_SORT_ORDER</asp:Label>:
                                    </td>
                                </tr>
                            </table>
                        </td>
                        <td style="vertical-align: top;" width="55%" colspan="2" align="left">
                            <asp:RadioButtonList ID="rdReportSortOrder" runat="server" RepeatDirection="Vertical">
                                <asp:ListItem Value="0" Selected="True">DEALER_CODE</asp:ListItem>
                                <asp:ListItem Value="1">DEALER_NAME</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>

 </asp:Content>      
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">    
        <asp:Button ID="btnGenRpt" Style="background-image: url(../Navigation/images/viewIcon2.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Text="View" Width="100px"
        CssClass="FLATBUTTON" Height="20px" CausesValidation="False"></asp:Button>
</asp:Content>   

