<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Reports/content_Report.Master" 
CodeBehind="MasterClaimReportForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.MasterClaimReportForm" %>

<%@ Register TagPrefix="uc3" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" style="vertical-align: top;">
        <tr>
            <td nowrap align="right" colspan="1" style="width: 20%">
            </td>
            <td align="center" colspan="2" width="60%">
                <table border="0" cellpadding="0" cellspacing="0" align="center" style="width: 70%">
                    <tr align="center">
                        <td align="right" nowrap valign="baseline">
                            *<asp:Label ID="moBeginDateLabel" runat="server">BEGIN_DATE</asp:Label>:
                        </td>
                        <td align="left" nowrap>
                            &nbsp;
                            <asp:TextBox ID="moBeginDateText" runat="server" AutoPostBack="false" CssClass="FLATTEXTBOX"
                                TabIndex="1"></asp:TextBox>
                            <asp:ImageButton ID="BtnBeginDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg" />
                        </td>
                        <td align="right" nowrap valign="baseline">
                            *
                            <asp:Label ID="moEndDateLabel" runat="server">END_DATE</asp:Label>:
                        </td>
                        <td align="left" nowrap>
                            &nbsp;
                            <asp:TextBox ID="moEndDateText" runat="server" AutoPostBack="false" CssClass="FLATTEXTBOX"
                                TabIndex="1"></asp:TextBox>
                            <asp:ImageButton ID="BtnEndDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td class="BLANKROW" colspan="3">
                <hr />
            </td>
        </tr>
        <tr>
            <td class="BLANKROW" colspan="3" style="vertical-align: top;">
            </td>
        </tr>
        <tr id="trcomp" runat="server">
            <td nowrap align="right" colspan="1" style="width: 15%">
            </td>
            <td class="LABELCOLUMN" style="vertical-align: bottom;">
                *&nbsp;<asp:Label ID="lblCompany" runat="server">SELECT_COMPANY:</asp:Label>
            </td>
            <td>
                <uc3:MultipleColumnDDLabelControl ID="CompanyDropControl" runat="server"></uc3:MultipleColumnDDLabelControl>
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
            <td nowrap align="right" colspan="1" style="width: 15%">
            </td>
            <td class="LABELCOLUMN" style="vertical-align: bottom;" height="10px" align="right">
                <asp:RadioButton ID="rdealer" AutoPostBack="false" Text="SELECT_ALL_DEALERS" TextAlign="left"
                    runat="server" Checked="True"></asp:RadioButton>
            </td>
            <td align="left">
                &nbsp;&nbsp;<uc3:MultipleColumnDDLabelControl ID="DealerDropControl" runat="server">
                </uc3:MultipleColumnDDLabelControl>
            </td>
        </tr>
         <tr>
            <td class="BLANKROW" colspan="3">
            </td>
        </tr>       
       <%-- <tr>
            <td class="BLANKROW" colspan="3">
                <hr />
            </td>
        </tr>
        <tr>
            <td nowrap align="right" colspan="1" style="width: 15%">
            </td>
            <td nowrap align="right" colspan="1">
                <asp:RadioButton ID="RadiobuttonTotalsOnly" groupname = "Detail"
                    Text="SHOW TOTALS ONLY" AutoPostBack="false" runat="server" TextAlign="left">
                </asp:RadioButton>
            </td>
            <td nowrap align="left">
                &nbsp;&nbsp;
                <asp:RadioButton ID="RadiobuttonDetail" groupname = "Detail" Text="OR SHOW DETAIL WITH TOTALS"
                    AutoPostBack="false" runat="server" TextAlign="left"></asp:RadioButton>
            </td>
        </tr>--%>
    </table>    

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <asp:Button ID="btnGenRpt" Style="background-image: url(../Navigation/images/viewIcon2.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Text="View" Width="100px"
        CssClass="FLATBUTTON" Height="20px" CausesValidation="False"></asp:Button>
</asp:Content>

