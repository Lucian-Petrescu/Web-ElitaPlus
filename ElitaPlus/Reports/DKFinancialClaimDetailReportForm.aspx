<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DKFinancialClaimDetailReportForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.DKFinancialClaimDetailReportForm" %>

<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title id="rptWindowTitle" runat="server">FINANCIAL_CLAIM_DETAIL</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Styles.css" type="text/css" rel="STYLESHEET">
    

    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>

</head>
<body leftmargin="0" topmargin="0" onload="changeScrollbarColor();" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <!--Start Header-->
    <input id="rptTitle" type="hidden" name="rptTitle">
    <input id="rptSrc" type="hidden" name="rptSrc">
    <table style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px;
        border-left: black 1px solid; border-bottom: black 1px solid" cellspacing="0"
        cellpadding="0" width="98%" bgcolor="#d5d6e4" border="0">
        <tr>
            <td valign="top">
                <table width="100%" border="0">
                    <tr>
                        <td height="20">
                            <asp:Label ID="LabelReports"  CssClass="TITLELABEL" runat="server">Reports</asp:Label>:
                            <asp:Label ID="Label7" runat="server"  CssClass="TITLELABELTEXT">DK_FINANCIAL_CLAIM_DETAIL</asp:Label>
                        </td>
                        <td height="20" align="right">
                            *&nbsp;
                            <asp:Label ID="moIndicatesLabel" runat="server"  EnableViewState="true">INDICATES_REQUIRED_FIELDS</asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table id="tblOuter2" style="border-right: black 1px solid; border-top: black 1px solid;
        margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid" height="93%"
        cellspacing="0" cellpadding="0" rules="none" width="98%" bgcolor="#d5d6e4" border="0">
        <!--d5d6e4-->
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td valign="top" align="center" height="100%">
                <asp:Panel ID="WorkingPanel" runat="server">
                    <table id="tblMain1" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                        border-left: #999999 1px solid; border-bottom: #999999 1px solid" cellspacing="0"
                        cellpadding="6" rules="cols" width="98%" align="center" bgcolor="#fef9ea" height="98%"
                        border="0">
                        <tr>
                            <td height="1">
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tr>
                                        <td colspan="3">
                                            <table id="tblSearch" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                                                border-left: #999999 1px solid; border-bottom: #999999 1px solid; height: 64px"
                                                cellspacing="2" cellpadding="8" rules="cols" width="100%" align="center" bgcolor="#fef9ea"
                                                border="0">
                                                <tr>
                                                    <td>
                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                            <tr>
                                                                <td align="center" colspan="3">
                                                                    <uc1:ErrorController ID="ErrorCtrl" runat="server"></uc1:ErrorController>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap align="right">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <uc1:ReportCeInputControl ID="moReportCeInputControl" runat="server"></uc1:ReportCeInputControl>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <img height="15" src="../Navigation/images/trans_spacer.gif">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3" valign="top">
                                            <table cellspacing="2" cellpadding="0" width="100%" border="0">
                                                <tr>
                                                    <td align="center" width="85%" colspan="3">
                                                        <table cellspacing="0" cellpadding="0" width="85%" border="0">
                                                            <tr>
                                                                <td width="10%" align="left">
                                                                </td>
                                                                <td align="right" nowrap valign="middle">
                                                                    &nbsp;
                                                                </td>
                                                                <td nowrap>                                                                   
                                                                    <asp:Label ID="moBeginDateLabel" runat="server" >BEGIN_DATE</asp:Label>
                                                                </td>
                                                                <td nowrap>                                                                   
                                                                    <asp:Label ID="moEndDateLabel" runat="server" >END_DATE</asp:Label>
                                                                </td>
                                                                 <td align="right" nowrap valign="middle" width="10%">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="10%" align="left">                                                                   
                                                                </td>
                                                                <td align="left" nowrap valign="bottom">
                                                                   <asp:RadioButton ID="rSelectDates" runat="server"  Text ="SELECT_DATES" AutoPostBack="false" onclick="toggleDateorMonthYearSelection('rSelectDates',false);"
                                                                       TextAlign="right" />                                                                
                                                                    </td>                                                                 
                                                                <td nowrap>
                                                                  <asp:TextBox ID="moBeginDateText" runat="server" TabIndex="1"></asp:TextBox>
                                                                    <asp:ImageButton ID="BtnBeginDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                                                        Width="20px" />
                                                                </td>
                                                                <td nowrap>
                                                                   <asp:TextBox ID="moEndDateText" runat="server" TabIndex="1"  ></asp:TextBox>
                                                                    <asp:ImageButton ID="BtnEndDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                                                        Width="20px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4">
                                                                    <img height="15" src="../Navigation/images/trans_spacer.gif">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="10%" align="left">
                                                                </td>
                                                                <td align="right" nowrap valign="middle">
                                                                    &nbsp;
                                                                </td>
                                                                <td nowrap>
                                                                    
                                                                    <asp:Label ID="moMonthLabel" runat="server" >MONTH</asp:Label>
                                                                </td>
                                                                <td nowrap>
                                                                    
                                                                    <asp:Label ID="moYearLabel" runat="server" >YEAR</asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="10%" nowrap>
                                                                  
                                                                </td>
                                                                <td align="LEFT" nowrap valign="bottom">
                                                                <asp:RadioButton ID="rMonthYear" runat="server" AutoPostBack="false"  Text = "OR_SELECT_ACCOUNTING_MONTH" 
                                                                        onclick="toggleDateorMonthYearSelection('rMonthYear',true);" TextAlign="right" />                                                                                                                                          
                                                                </td>
                                                                <td nowrap>
                                                                   <asp:DropDownList ID="MonthDropDownList" runat="server" AutoPostBack="false">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td nowrap>
                                                                    <asp:DropDownList ID="YearDropDownList" runat="server"  AutoPostBack="false">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" align="center">
                                                        <table id="Table2" border="0" cellpadding="1" cellspacing="1" width="100%">
                                                            <tr>
                                                                <td id="Td1" align="center" colspan="4">
                                                                    <hr style="height: 1px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap align="left" width="10%" colspan="1">
                                                                </td>
                                                                <td nowrap valign="baseline" colspan="3" align="left" width="60%">
                                                                    <uc1:MultipleColumnDDLabelControl ID="moUserCompanyMultipleDrop" runat="server"></uc1:MultipleColumnDDLabelControl>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td id="ddSeparator" align="center" colspan="4" width="100%">
                                                                    <hr style="width: 100%; height: 1px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap align="left" width="10%" colspan="1">
                                                                </td>
                                                                <td align="left" valign="bottom" nowrap>
                                                                    *
                                                                    <asp:RadioButton ID="rdealer" runat="server" AutoPostBack="false" Checked="False"
                                                                        onclick="ToggleDualDropDownsSelection('moDealerMultipleDrop_moMultipleColumnDrop', 'moDealerMultipleDrop_moMultipleColumnDropDesc', 'rdealer', false, ''); document.all.item('moDealerMultipleDrop_lb_DropDown').style.color = '';"
                                                                        Text="SELECT_ALL_DEALERS" TextAlign="left" />
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap align="left" width="10%" colspan="1">
                                                                </td>
                                                                <td nowrap valign="baseline" colspan="3" align="left" width="60%">
                                                                    <uc1:MultipleColumnDDLabelControl ID="moDealerMultipleDrop" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4" height="20px">
                                                                    <hr style="width: 100%; height: 1px" />
                                                                </td>
                                                            </tr>
                                                            </table>
                                                            </td>
                                                         </tr>   
                                                            
                                                <tr>
                                                    <td colspan="4" align="center">
                                                        <table id="Table1" border="0" cellpadding="1" cellspacing="1" width="100%">
                                                            <tr> 
                                                              <td width="14%">
                                                                </td>                                                              
                                                                <td align="left" nowrap colspan="1">*
                                                                    <asp:RadioButton ID="rCoverageType" onclick="ToggleSingleDropDownSelection('cboCoverageType','rCoverageType',false);"
                                                                        Checked="False" TextAlign="left" Text="PLEASE_SELECT_ALL_COVERAGE_TYPES" runat="server"
                                                                        AutoPostBack="false"></asp:RadioButton>
                                                                </td>
                                                                <td align="left" nowrap colspan="1">
                                                                    <asp:Label ID="lblCoverageType" runat="server">OR_A_SINGLE_COVERAGE_TYPE</asp:Label>
                                                                    <asp:DropDownList ID="cboCoverageType" runat="server" AutoPostBack="false" onchange="ToggleSingleDropDownSelection('cboCoverageType','rCoverageType',true);">
                                                                    </asp:DropDownList>
                                                                </td> 
                                                                  <td align="right" nowrap valign="middle" width="12%">
                                                                    &nbsp;
                                                                </td>                                                           
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4" height="20px">
                                                                    <hr style="width: 100%; height: 1px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="14%">
                                                                </td>
                                                                <td align="left" colspan="1" nowrap>*
                                                                    <asp:RadioButton ID="RadiobuttonTotalsOnly" runat="server" AutoPostBack="false" onclick="toggleDetailSelection(false);"
                                                                        Text="SHOW TOTALS ONLY" TextAlign="left" />
                                                                </td>
                                                                <td align="left" colspan="2" nowrap>
                                                                    <asp:RadioButton ID="RadiobuttonDetail" runat="server" AutoPostBack="false" onclick="toggleDetailSelection(true);"
                                                                        Text="OR SHOW DETAIL WITH TOTALS" TextAlign="left" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="10%">
                                                                </td>
                                                                <td align="left" colspan="1" nowrap>
                                                                <asp:CheckBox ID="chkTotalsPageByCov" runat="server" AutoPostBack="false" Text="SHOW_TOTALS_PAGE_BY_COVERAGE"
                                                                        TextAlign="Left" />
                                                                </td>
                                                                <td align="left" colspan="2" nowrap>                                                                  
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                </td>
                                                            </tr>                                                            
                                                        </table>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 55%; width: 100%;">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <hr style="width: 100%; height: 1px" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Button ID="btnGenRpt" runat="server" CssClass="FLATBUTTON" 
                                    Height="20px" Style="background-image: url(../Navigation/images/viewIcon2.gif);
                                    cursor: hand; background-repeat: no-repeat" Text="View" Width="100px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    </form>

    <script language="JavaScript" src="../Navigation/Scripts/ReportCeMainScripts.js"></script>
    <script language="JavaScript">
    
    function toggleDateorMonthYearSelection(ctl,isSelected)
	{		
	    var objcontrol = ctl;	    
		if(isSelected)
		{	
		   // alert(objcontrol);	        
             if (objcontrol == 'rSelectDates')
		     {		      
		        if(document.forms[0].rSelectDates.checked == true)
		        {		           		          
		           document.forms[0].YearDropDownList.selectedIndex = -1;	           		         
		           document.forms[0].MonthDropDownList.selectedIndex = -1;
		           document.forms[0].rMonthYear.checked = false;
		           document.getElementById('moBeginDateText').disabled = false;
		           document.getElementById('moEndDateText').disabled =false;
		           document.getElementById('MonthDropDownList').disabled =true;
		           document.getElementById('YearDropDownList').disabled =true;
		           //document.getElementById('lblSelectDates').style.color = '';
		           //document.getElementById('MonthYearLabel').style.color = '';
		           //document.getElementById('rSelectDates').style.color = '';	               
	               //document.getElementById('rMonthYear').style.color = '';
	               document.getElementById('moBeginDateLabel').style.color = '';
	               document.getElementById('moEndDateLabel').style.color = ''; 	
	               document.getElementById('moMonthLabel').style.color = '';
	               document.getElementById('moYearLabel').style.color = ''; 		               	               
		        }
		      }   
		       if (objcontrol== 'rMonthYear')
		      {		      
		        if(document.forms[0].rMonthYear.checked == true)
		        {		           
		           document.forms[0].moBeginDateText.value ="";	
		           document.forms[0].moEndDateText.value ="";					           
		           document.forms[0].rSelectDates.checked = false;
		           document.getElementById('moBeginDateText').disabled =true;
		           document.getElementById('moEndDateText').disabled =true;
		           document.getElementById('MonthDropDownList').disabled =false;
		           document.getElementById('YearDropDownList').disabled =false;
		           //document.getElementById('lblSelectDates').style.color = '';
	               //document.getElementById('MonthYearLabel').style.color = ''; 
	               //document.getElementById('rSelectDates').style.color = '';	               
	               //document.getElementById('rMonthYear').style.color = '';
	               document.getElementById('moBeginDateLabel').style.color = '';
	               document.getElementById('moEndDateLabel').style.color = ''; 	
	               document.getElementById('moMonthLabel').style.color = '';
	               document.getElementById('moYearLabel').style.color = ''; 	 	              	              
		        }
	          }
	     }      
	    else
		 {		           
		           document.forms[0].YearDropDownList.selectedIndex = -1;		           		         
		           document.forms[0].MonthDropDownList.selectedIndex = -1;	           		           
		           document.forms[0].rMonthYear.checked = false;
		           document.getElementById('moBeginDateText').disabled=false;
		           document.getElementById('moEndDateText').disabled=false;
		           document.getElementById('MonthDropDownList').disabled=true;
		           document.getElementById('YearDropDownList').disabled=true;
		           //document.getElementById('lblSelectDates').style.color = '';
	               //document.getElementById('MonthYearLabel').style.color = ''; 	
	              // document.getElementById('rSelectDates').style.color = '';	               
	               //document.getElementById('rMonthYear').style.color = '';
	               document.getElementById('moBeginDateLabel').style.color = '';
	               document.getElementById('moEndDateLabel').style.color = ''; 	
	               document.getElementById('moMonthLabel').style.color = '';
	               document.getElementById('moYearLabel').style.color = '';                	                             
		 }
	}
	</script>	      		   
</body>
</html>
