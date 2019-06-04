
<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Reports/content_Report.Master" 
CodeBehind="MXClaimInfoReportForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.MXClaimInfoReportForm" %>

<%@ Register TagPrefix="uc3" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" style="vertical-align: top;">
        <tr>
            <td nowrap align="right" colspan="1" style="width: 15%">
            </td>
            <td align="left" width="85%" colspan="2">
                <table cellspacing="0" cellpadding="0" width="85%" border="0">
                    <tr>
                        <td align="left" width="85%" colspan="3">
                            <table cellspacing="0" cellpadding="0" width="85%" border="0">
                                <tr>
                                    <td align="right" nowrap valign="middle">
                                        &nbsp;
                                    </td>
                                    <td nowrap>
                                        <asp:Label ID="moBeginDateLabel" runat="server">BEGIN_DATE</asp:Label>
                                    </td>
                                    <td nowrap>
                                        <asp:Label ID="moEndDateLabel" runat="server">END_DATE</asp:Label>
                                    </td>
                                    <td align="right" nowrap valign="middle" width="10%">
                                        &nbsp;
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left" nowrap valign="bottom">
                                        <asp:RadioButton ID="rSelectDates" runat="server" Text="SELECT_DATES" AutoPostBack="false"
                                            onclick="toggleDateorMonthYearSelection('rSelectDates',false);" TextAlign="right" />
                                    </td>
                                    <td nowrap>
                                        <asp:TextBox ID="moBeginDateText" runat="server" TabIndex="1"></asp:TextBox>
                                        <asp:ImageButton ID="BtnBeginDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                            Width="20px" />
                                    </td>
                                    <td nowrap>
                                        <asp:TextBox ID="moEndDateText" runat="server" TabIndex="1"></asp:TextBox>
                                        <asp:ImageButton ID="BtnEndDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                            Width="20px" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="3">
                                        <img height="15" src="../Navigation/images/trans_spacer.gif">
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right" nowrap valign="middle">
                                        &nbsp;
                                    </td>
                                    <td nowrap>
                                        <asp:Label ID="moMonthLabel" runat="server">MONTH</asp:Label>
                                    </td>
                                    <td nowrap>
                                        <asp:Label ID="moYearLabel" runat="server">YEAR</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="LEFT" nowrap valign="bottom">
                                        <asp:RadioButton ID="rMonthYear" runat="server" AutoPostBack="false" Text="OR_SELECT_ACCOUNTING_MONTH"
                                            onclick="toggleDateorMonthYearSelection('rMonthYear',true);" TextAlign="right" />
                                    </td>
                                    <td nowrap>
                                        <asp:DropDownList ID="MonthDropDownList" runat="server" AutoPostBack="false">
                                        </asp:DropDownList>
                                    </td>
                                    <td nowrap>
                                        <asp:DropDownList ID="YearDropDownList" runat="server" AutoPostBack="false">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
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
    </table>    
    <script>
    function toggleDateorMonthYearSelection(ctl,isSelected)
	{		
	    var objcontrol = ctl;	    
		if(isSelected)
		{	
		   // alert(objcontrol);	        
             if (objcontrol == 'rSelectDates')
		     {
		         if (document.getElementById('<%=rSelectDates.ClientID%>').checked == true) 
		        {

		           document.getElementById('<%=YearDropDownList.ClientID%>').selectedIndex = -1;
		           document.getElementById('<%=MonthDropDownList.ClientID%>').selectedIndex = -1;
		           document.getElementById('<%=rMonthYear.ClientID%>').checked = false;
		           document.getElementById('<%=moBeginDateText.ClientID%>').disabled = false;
		           document.getElementById('<%=moEndDateText.ClientID%>').disabled = false;
		           document.getElementById('<%=MonthDropDownList.ClientID%>').disabled = true;
		           document.getElementById('<%=YearDropDownList.ClientID%>').disabled = true;
		           document.getElementById('<%=moBeginDateLabel.ClientID%>').style.color = '';
		           document.getElementById('<%=moEndDateLabel.ClientID%>').style.color = '';
		           document.getElementById('<%=moMonthLabel.ClientID%>').style.color = '';
		           document.getElementById('<%=moYearLabel.ClientID%>').style.color = ''; 		               	               
		        }
		      }   
		       if (objcontrol== 'rMonthYear')
		      {
		          if (document.getElementById('<%=rMonthYear.ClientID%>').checked == true)
		        {		           
		           document.getElementById('<%=moBeginDateText.ClientID%>').value ="";	
		           document.getElementById('<%=moEndDateText.ClientID%>').value ="";
		           document.getElementById('<%=rSelectDates.ClientID%>').checked = false;
		           document.getElementById('<%=moBeginDateText.ClientID%>').disabled = true;
		           document.getElementById('<%=moEndDateText.ClientID%>').disabled = true;
		           document.getElementById('<%=MonthDropDownList.ClientID%>').disabled = false;
		           document.getElementById('<%=YearDropDownList.ClientID%>').disabled = false;
		           document.getElementById('<%=moBeginDateLabel.ClientID%>').style.color = '';
		           document.getElementById('<%=moEndDateLabel.ClientID%>').style.color = '';
		           document.getElementById('<%=moMonthLabel.ClientID%>').style.color = '';
		           document.getElementById('<%=moYearLabel.ClientID%>').style.color = ''; 	 	              	              
		        }
	          }
	     }      
	    else
		 {
		             document.getElementById('<%=YearDropDownList.ClientID%>').selectedIndex = -1;
		             document.getElementById('<%=MonthDropDownList.ClientID%>').selectedIndex = -1;
		             document.getElementById('<%=rMonthYear.ClientID%>').checked = false;
		             document.getElementById('<%=moBeginDateText.ClientID%>').disabled = false;
		             document.getElementById('<%=moEndDateText.ClientID%>').disabled = false;
		             document.getElementById('<%=MonthDropDownList.ClientID%>').disabled = true;
		             document.getElementById('<%=YearDropDownList.ClientID%>').disabled = true;
		             document.getElementById('<%=moBeginDateLabel.ClientID%>').style.color = '';
		             document.getElementById('<%=moEndDateLabel.ClientID%>').style.color = '';
		             document.getElementById('<%=moMonthLabel.ClientID%>').style.color = '';
		             document.getElementById('<%=moYearLabel.ClientID%>').style.color = ''; 		             	                             
		 }
	}
	</script>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <asp:Button ID="btnGenRpt" Style="background-image: url(../Navigation/images/viewIcon2.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Text="View" Width="100px"
        CssClass="FLATBUTTON" Height="20px" CausesValidation="False"></asp:Button>
</asp:Content>


