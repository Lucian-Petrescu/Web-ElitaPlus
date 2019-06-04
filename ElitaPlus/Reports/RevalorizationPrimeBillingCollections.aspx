<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RevalorizationPrimeBillingCollections.aspx.vb"
    MasterPageFile="~/Reports/content_Report.Master" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.RevalorizationPrimeBillingCollections" %>

<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <script language="JavaScript">
        //var arrDealerGroupCtr = [[['cboDealer'],['cboDealerCode']],['rdealer'],['rGroup'],['cboDealerGroup']]
        var arrDealerGroupCtr = [[['moUserDealerMultipleDrop_moMultipleColumnDropDesc'], ['moUserDealerMultipleDrop_moMultipleColumnDrop']], ['rdealer']]
        var arrDealerCtr = [[['moUserDealerMultipleDrop_moMultipleColumnDropDesc'], ['moUserDealerMultipleDrop_moMultipleColumnDrop']], ['rdealer']]
    </script>
    <table border="0" cellpadding="2" cellspacing="2" align="center" width="750px">
        <tr>
            <td align="right" colspan="2">
                <asp:Label ID="moBeginDateLabel" runat="server">BEGIN_DATE</asp:Label>
                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                <asp:Label ID="moEndDateLabel" runat="server">END_DATE</asp:Label>
            </td>
        </tr>
        <tr>
            <td align="right" colspan="2">
                <asp:TextBox ID="moBeginDateText" runat="server" TabIndex="1"></asp:TextBox>
                <asp:ImageButton ID="BtnBeginDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg" />
                &nbsp;&nbsp;
                <asp:TextBox ID="moEndDateText" runat="server" TabIndex="1"></asp:TextBox>
                <asp:ImageButton ID="BtnEndDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg" />
            </td>
        </tr>
        <tr>
            <td class="BLANKROW" colspan="2">
                <hr />
            </td>
        </tr>
        <tr>
            <td align="center" colspan="2">
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
            <td class="LABELCOLUMN" style="vertical-align: bottom;" height="10px" align="right">    
                <asp:RadioButton ID="rdealer" AutoPostBack="false" Text="SELECT_ALL_DEALERS" TextAlign="left"
                    runat="server" Checked="True">
                </asp:RadioButton>
            </td>
            <td align="center">
                &nbsp;&nbsp;<uc1:MultipleColumnDDLabelControl ID="moUserDealerMultipleDrop" runat="server">
                </uc1:MultipleColumnDDLabelControl>
            </td>
        </tr>
        
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <asp:Button ID="btnGenRpt" Style="background-image: url(../Navigation/images/viewIcon2.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Text="View" Width="100px"
        CssClass="FLATBUTTON" Height="20px" CausesValidation="False"></asp:Button>
</asp:Content>
