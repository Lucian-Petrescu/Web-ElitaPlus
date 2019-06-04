<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Reports/content_Report.Master"  CodeBehind="AMLComplianceReportForm.aspx.vb" 
Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.AMLComplianceReportForm" %>
<%@ Register TagPrefix="uc3" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server" >

    <table cellpadding="0" cellspacing="0" border="0" width="80%" align="center">
       
        <tr>
            <td class="BLANKROW" colspan="4" style="vertical-align: top;">
            </td>
        </tr>  
        <tr>
            <td align="right" style="text-align: right; vertical-align: middle;">
                <asp:Label ID="moBaseReportOnLabel" runat="server">BASE_REPORT_ON</asp:Label>:
            </td>
            <td colspan="3" align="right" valign ="middle">
                <asp:RadioButtonList ID="rdPeriod" runat="server" RepeatDirection="Horizontal" CellPadding="4" CellSpacing="4">
                    <asp:ListItem Value="W" Selected="True">Warranty_Sales_Date</asp:ListItem>
                    <asp:ListItem Value="I">IN_FORCE_PERIOD</asp:ListItem> 
                    <asp:ListItem Value="P">PEP_FLAGGED_DATE_PERIOD</asp:ListItem> 
                    <asp:ListItem Value="T">TERRORIST_FLAGGED_DATE_PERIOD</asp:ListItem> 
                </asp:RadioButtonList>
            </td>
          </tr>
          <tr><td colspan="4">&nbsp;</td></tr>
          <tr>
                <td style="text-align: right; vertical-align: middle;" align="right">
                    *<asp:Label ID="moBeginDateLabel" runat="server" Font-Bold="false">BEGIN_DATE</asp:Label>:
                </td>
                <td nowrap width="20%">
                    &nbsp;
                    <asp:TextBox ID="moBeginDateText" TabIndex="1" runat="server" CssClass="FLATTEXTBOX"></asp:TextBox>
                    <asp:ImageButton ID="btnBeginDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg"
                        Width="20px" ImageAlign="AbsMiddle"></asp:ImageButton>
                </td>
                <td  colspan="2">
                    &nbsp;&nbsp; *<asp:Label ID="moEndDateLabel" runat="server" Font-Bold="false">END_DATE</asp:Label>:
                     &nbsp;
                    <asp:TextBox ID="moEndDateText" TabIndex="1" runat="server" CssClass="FLATTEXTBOX"
                        Width="125px"></asp:TextBox>
                    <asp:ImageButton ID="btnEndDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg"
                        Width="20px" ImageAlign="AbsMiddle"></asp:ImageButton>
                </td>
             </tr>
             <tr><td colspan="4">&nbsp;</td></tr>
             <tr>
                    <td class="BLANKROW" colspan="4">
                        <hr />
                    </td>
             </tr> 
       <tr><td colspan="4">&nbsp;</td></tr>
       <tr>
          <td align="right" style="text-align: right; vertical-align: middle;">
             <asp:Label ID="moReportTypeLabel" runat="server">REPORT_TYPE</asp:Label>:
          </td>
          <td style="text-align: right; ">
               <asp:CheckBox ID="chkAllCerts" Text="ALL_CERTIFICATES" AutoPostBack="false" runat="server" TextAlign="Left" Checked="True" onclick="RptTypeSelection('ALL');"></asp:CheckBox><br />
               <asp:CheckBox ID="chkPepReport" Text="PEP_REPORT" AutoPostBack="false" runat="server" TextAlign="Left" onclick="RptTypeSelection('PEP');"></asp:CheckBox><br /> 
               <asp:CheckBox ID="chkTerrReport" Text="TERRORIST_REPORT" AutoPostBack="false" runat="server" TextAlign="Left" onclick="RptTypeSelection('TER');"></asp:CheckBox>
          </td>
          <td align="right" colspan="1" style="text-align: center; vertical-align: middle;" >
               &nbsp;&nbsp;&nbsp;&nbsp;<asp:CheckBox ID="chkSusOpReport" Text="SUSPICIOUS_OPERATIONS_REPORT" AutoPostBack="false" runat="server" TextAlign="Left"></asp:CheckBox><br /><br /><br /><br />
          </td><td></td>
     </tr>
     <tr><td colspan="4">&nbsp;</td></tr>
     <tr>
            <td class="BLANKROW" colspan="4">
                <hr />
            </td>
     </tr>
     <tr>
        <td align="right" style="text-align: right; vertical-align: middle;">
            <asp:Label ID="moSortByLabel" runat="server">SELECT_REPORT_SORT_ORDER</asp:Label>:
        </td>
        <td colspan="2" align="center" style="text-align: right; vertical-align: middle;">&nbsp;&nbsp; &nbsp;&nbsp; 
            <asp:RadioButtonList ID="rdReportSortOrder" runat="server" RepeatDirection="VERTICAL" CellPadding="2" CellSpacing="2">
                <asp:ListItem Value="TXI" Selected="True">TAX_ID</asp:ListItem>                             
                <asp:ListItem Value="WSD">WARRANTY_SALES_DATE</asp:ListItem>
                <asp:ListItem Value="PEP">PEP_FLAGGED_DATE</asp:ListItem>
                <asp:ListItem Value="TRR">TERRORIST_FLAGGED_DATE</asp:ListItem>
            </asp:RadioButtonList>
         </td>
         <td></td>
     </tr>
 </table>

 </asp:Content>      
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">    
        <asp:Button ID="btnGenRpt" Style="background-image: url(../Navigation/images/viewIcon2.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Text="View" Width="100px"
        CssClass="FLATBUTTON" Height="20px" CausesValidation="False"></asp:Button>

		<script type="text/javascript" language="javascript">

		    function RptTypeSelection(rptType) {
		        var objAllCerts = document.getElementById('<%=chkAllCerts.ClientID%>');
		        var objPepReport = document.getElementById('<%=chkPepReport.ClientID%>');
		        var objTerrReport = document.getElementById('<%=chkTerrReport.ClientID%>');

		        if (rptType == 'PEP' || rptType == 'TER') {
		            if (objAllCerts.checked && (objPepReport.checked || objTerrReport.checked)) {
		                objAllCerts.checked = false;
		            }

		        }
		        else {
		            if (rptType == 'ALL') {
		                if (objAllCerts.checked) {
		                    objPepReport.checked = false;
		                    objTerrReport.checked = false;
		                }
		            }
		        }
		    }
		</script>
</asp:Content>   
