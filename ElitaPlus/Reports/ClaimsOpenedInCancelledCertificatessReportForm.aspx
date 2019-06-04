<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Reports/content_Report.Master" 
 CodeBehind="ClaimsOpenedInCancelledCertificatessReportForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.ClaimsOpenedInCancelledCertificatessReportForm" %>
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
            <td align="center" width="75%" colspan="2">
                <table cellspacing="0" cellpadding="0" width="65%" border="0">
                    <tr>
                        <td valign="middle" nowrap align="right">
                            *<asp:Label ID="BeginDateLabel" runat="server" Font-Bold="false">BEGIN_DATE</asp:Label>:
                        </td>
                        <td nowrap align="left">                       
                            <asp:TextBox ID="BeginDateText" TabIndex="1" runat="server" CssClass="FLATTEXTBOX"></asp:TextBox>
                            <asp:ImageButton ID="BtnBeginDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                Width="20px"></asp:ImageButton>
                        </td>
                        <td valign="middle" nowrap align="right">
                            &nbsp;&nbsp; *<asp:Label ID="EndDateLabel" runat="server" Font-Bold="false">END_DATE</asp:Label>:
                        </td>
                        <td nowrap align="left">                            
                            <asp:TextBox ID="EndDateText" TabIndex="1" runat="server" CssClass="FLATTEXTBOX"
                                Width="125px"></asp:TextBox>
                            <asp:ImageButton ID="BtnEndDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                Width="20px"></asp:ImageButton>
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
        <td nowrap align="right" colspan="1" style="width: 25%">
            </td>
            <td class="LABELCOLUMN" style="vertical-align: bottom;" height="10px">
                <asp:RadioButton ID="rdealer" AutoPostBack="false" Text="SELECT_ALL_DEALERS" TextAlign="left"
                    runat="server" Checked="True"></asp:RadioButton>
            </td>
            <td>
                &nbsp;&nbsp;<uc3:MultipleColumnDDLabelControl ID="moDealerMultipleDrop" runat="server">
                </uc3:MultipleColumnDDLabelControl>
            </td>
        </tr> 
         <tr>
            <td class="BLANKROW" colspan="3">
                <hr />
            </td>
        </tr>  
        <TR> 
            <td nowrap align="right" colspan="1" style="width: 25%">
            </td>
			<td class="LABELCOLUMN" style="vertical-align: bottom;" height="10px">
			    <asp:radiobutton id="rbAllSvcCenters" 
				 AutoPostBack="false" Checked="False" Runat="server" Text="PLEASE SELECT ALL SERVICE CENTERS" TextAlign="left"></asp:radiobutton>
		    </TD>
			<TD>
			    <asp:label id="lblSvcCenter" runat="server">OR A SINGLE SERVICE CENTER</asp:label>:
				<asp:dropdownlist id="cboSvcCenter" runat="server" AutoPostBack="false"></asp:dropdownlist>
			</TD>
		</TR>
		 <tr>
            <td class="BLANKROW" colspan="3">
                <hr />
            </td>
        </tr>                     
        <TR> 
            <td nowrap align="right" colspan="1" style="width: 25%">
            </td>
			<td class="LABELCOLUMN" style="vertical-align: bottom;" height="10px">
			    <asp:radiobutton id="rbAllUsers" 
				 AutoPostBack="false" Checked="False" Runat="server" Text="PLEASE_SELECT_ALL_USERS" TextAlign="left"></asp:radiobutton>
		    </TD>
			<TD>
			    <asp:label id="lblUserId" runat="server">OR_ENTER_A_USER_ID</asp:label>:
				<asp:TextBox id="txtUserId" runat="server" CssClass="FLATTEXTBOX" AutoPostBack="false"
				></asp:TextBox>
			</TD>
		</TR>
		<tr>
            <td class="BLANKROW" colspan="3">
              <hr />            
            </td>
        </tr>  
        <tr>
            <td nowrap align="right" colspan="1" style="width: 25%; vertical-align: top;">
            </td>
            <td style="vertical-align: top;" nowrap >
                <table cellspacing="0" cellpadding="0" border="0" align ="right">
                    <tr>
                        <td style="vertical-align: top;">
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top;">
                            <asp:Label ID="moSortByLabel" runat="server">SELECT_REPORT_SORT_ORDER</asp:Label>:
                        </td>
                    </tr>
                </table>
            </td>
            <td align="left" style="vertical-align: top;">
                <table cellspacing="0" cellpadding="0" border="0">
                    <tr>
                        <td style="vertical-align: top;">
                             <asp:RadioButtonList ID="rdReportSortOrder" runat="server" RepeatDirection="Vertical">
                                <asp:ListItem Value="1" Selected="True">Claim_Opened_By</asp:ListItem>
                                <asp:ListItem Value="2">Claim_Opened_Date</asp:ListItem>
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


