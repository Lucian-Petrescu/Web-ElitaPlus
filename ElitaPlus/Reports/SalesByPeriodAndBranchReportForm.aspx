<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SalesByPeriodAndBranchReportForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.SalesByPeriodAndBranchReportForm" %>

<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title id="rptWindowTitle" runat="server">BRANCH_SALES"</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Styles.css" type="text/css" rel="STYLESHEET">
    

    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>

    <script language="JavaScript" src="../Navigation/Scripts/ReportceScripts.js"></script>

    <script language="JavaScript">
	    
	    var arrDealerGroupCtr = [[['multipleDropControl_moMultipleColumnDropDesc'],['multipleDropControl_moMultipleColumnDrop']],['rdealer']]
	    
    </script>

</head>
<body leftmargin="0" topmargin="0" onload="changeScrollbarColor();" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <!--Start Header-->
    <input id="rptSrc" type="hidden" name="rptSrc">
    <table style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px;
        border-left: black 1px solid; border-bottom: black 1px solid" cellspacing="0"
        cellpadding="0" width="98%" bgcolor="#d5d6e4" border="0">
        <tr>
            <td valign="top">
                <table width="100%" border="0">
                    <tr>
                        <td height="20">
                            <asp:Label ID="LabelReports" runat="server"  CssClass="TITLELABEL">Reports</asp:Label>:&nbsp;<asp:Label
                                ID="Label7" runat="server"  CssClass="TITLELABELTEXT">BRANCH_SALES</asp:Label>
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
            <td valign="top" align="left" height="100%">
                <asp:Panel ID="WorkingPanel" runat="server">
                    <table id="tblMain1" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                        border-left: #999999 1px solid; border-bottom: #999999 1px solid" cellspacing="0"
                        cellpadding="6" rules="cols" width="98%" align="center" bgcolor="#fef9ea" border="0"
                        height="95%">
                        <tr>
                            <td height="1">
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table cellspacing="0" cellpadding="0" width="100%" border="0" align="left">
                                    <tr>
                                        <td colspan="3">
                                            <table id="tblSearch" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                                                border-left: #999999 1px solid; width: 100%; border-bottom: #999999 1px solid;
                                                height: 64px" cellspacing="2" cellpadding="8" rules="cols" width="100%" align="center"
                                                bgcolor="#fef9ea" border="0">
                                                <tr>
                                                    <td>
                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                            <tr>
                                                                <td align="center" colspan="3">
                                                                    <uc1:ErrorController ID="ErrorCtrl" runat="server"></uc1:ErrorController>
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
                                        <td align="center" colspan="3">
                                            <table id="Table2" border="0" cellpadding="1" cellspacing="1" width="100%">
                                                <tr>
                                                    <td colspan="4">
                                                        <table border="0" cellpadding="1" cellspacing="1" width="100%">
                                                            <tr>
                                                                <td align="center" valign="top" colspan="4" nowrap style="width: 100%; height: 0.01%">
                                                                    <uc1:MultipleColumnDDLabelControl ID="moUserCompanyMultipleDrop" runat="server">
                                                                    </uc1:MultipleColumnDDLabelControl>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td id="ddSeparator" align="center" colspan="4" width="100%" runat="server">
                                                        <hr style="width: 100%; height: 1px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="left" valign="bottom" colspan="1" nowrap>
                                                        *
                                                        <asp:RadioButton ID="rdealer" runat="server" AutoPostBack="false" Checked="False"
                                                            onclick="ToggleDualDropDownsSelection('moDealerMultipleDrop_moMultipleColumnDrop', 'moDealerMultipleDrop_moMultipleColumnDropDesc', 'rdealer', false, ''); document.all.item('moDealerMultipleDrop_lb_DropDown').style.color = '';"
                                                            Text="SELECT_ALL_DEALERS" TextAlign="left" />
                                                        &nbsp;
                                                    </td>
                                                    <td nowrap valign="baseline" colspan="3" align="left">
                                                        <uc1:MultipleColumnDDLabelControl ID="moDealerMultipleDrop" runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            &nbsp;&nbsp;
                                            <hr style="height: 1px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="left" colspan="3">
                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                <tr>
                                                    <td nowrap align="left" width="18%">
                                                        &nbsp;
                                                    </td>
                                                    <td nowrap align="left" width="18%">
                                                        &nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td nowrap align="left" width="20%">
                                                    </td>
                                                    <td nowrap align="left" width="25%">
                                                    </td>
                                                    <td nowrap align="left" width="10%">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td nowrap align="right" width="20%">
                                                        <asp:Label ID="lblWeeklySelect" runat="server" >WEEKLY_SELECTION</asp:Label>
                                                    </td>
                                                    <td nowrap align="left" width="20%">
                                                    </td>
                                                    <td nowrap align="left" width="23%">
                                                    </td>
                                                    <td nowrap align="left" width="20%">
                                                    </td>
                                                    <td nowrap align="left">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td nowrap align="left" width="20%">
                                                    </td>
                                                    <td align="right" nowrap style="height: 19px" valign="middle">
                                                        <asp:Label ID="moWBeginDateLabel" runat="server" >SELECT_WEEK_BEGIN_DATE</asp:Label>:
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                        <asp:TextBox ID="moWeekBeginDateText" runat="server" TabIndex="1" Width="100px" AutoPostBack="True"
                                                            OnFocus="togglePeriodSelection('moWeekBeginDateText',true);" OnChange="togglePeriodSelection('moWeekBeginDateText',true);"></asp:TextBox>
                                                        &nbsp;
                                                        <asp:ImageButton ID="BtnBeginDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" />
                                                        &nbsp;&nbsp;
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="lblBeginWeekNum" runat="server" >BEGIN_WEEK_NUMBER:</asp:Label>
                                                        <asp:TextBox ID="txtBeginWeekNum" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True"
                                                            TabIndex="1" Width="50px"></asp:TextBox>
                                                    </td>
                                                    <td nowrap align="left" width="20%">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td nowrap align="left" width="20%">
                                                    </td>
                                                    <td align="right" nowrap valign="middle">
                                                        &nbsp;&nbsp;
                                                        <asp:Label ID="moWEndDateLabel" runat="server" >SELECT_WEEK_END_DATE</asp:Label>:
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                        <asp:TextBox ID="moWeekEndDateText" runat="server" TabIndex="1" Width="100px" AutoPostBack="True"
                                                            OnFocus="togglePeriodSelection('moWeekEndDateText',true);" OnChange="togglePeriodSelection('moWeekEndDateText',true);"></asp:TextBox>
                                                        &nbsp;
                                                        <asp:ImageButton ID="BtnEndDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" />
                                                    </td>
                                                    <td align="left">
                                                        <asp:Label ID="lblEndWeekNum" runat="server" >END_WEEK_NUMBER:</asp:Label>&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox
                                                            ID="txtEndWeekNum" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" TabIndex="1"
                                                            Width="50px"></asp:TextBox>
                                                    </td>
                                                    <td nowrap align="left" width="20%">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5" height="15px">
                                                        &nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5" height="15px">
                                                        &nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td nowrap align="right" width="20%">
                                                        <asp:Label ID="lblMonthlySelect" runat="server" >MONTHLY_SELECTION</asp:Label>&nbsp;
                                                    </td>
                                                    <td nowrap align="left" width="20%">
                                                        &nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td nowrap align="left" width="20%">
                                                    </td>
                                                    <td nowrap align="left" width="20%">
                                                    </td>
                                                    <td nowrap align="left" width="20%">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td nowrap align="left" width="20%">
                                                    </td>
                                                    <td align="right" nowrap style="height: 19px" valign="middle">
                                                        <asp:Label ID="moMBeginDateLabel" runat="server" >SELECT_MONTH_BEGIN_YEAR</asp:Label>:
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                        <asp:TextBox ID="txtBeginMonthNum" runat="server" TabIndex="1" Width="50px" OnFocus="togglePeriodSelection('txtBeginMonthNum',true);"
                                                            MaxLength="2"></asp:TextBox>
                                                        &nbsp;
                                                        <asp:DropDownList ID="cboBeginMonthyear" runat="server" onchange="togglePeriodSelection('cboBeginMonthyear',true);"
                                                            TabIndex="1" Width="60px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td nowrap align="left" width="20%">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td nowrap align="left" width="20%">
                                                    </td>
                                                    <td align="right" nowrap valign="middle">
                                                        &nbsp;&nbsp;
                                                        <asp:Label ID="moMEndDateLabel" runat="server" >SELECT_MONTH_END_YEAR</asp:Label>:
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                        <asp:TextBox ID="txtEndMonthNum" runat="server" TabIndex="1" Width="50px" OnFocus="togglePeriodSelection('txtEndMonthNum',true);"
                                                            MaxLength="2"></asp:TextBox>
                                                        &nbsp;
                                                        <asp:DropDownList ID="cboEndMonthyear" runat="server" onchange="togglePeriodSelection('cboEndMonthyear',true);"
                                                            TabIndex="1" Width="60px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td nowrap align="left" width="20%">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5" height="15px">
                                                        &nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5" height="15px">
                                                        &nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td nowrap align="right" width="20%">
                                                        <asp:Label ID="lblQuaterlySelect" runat="server" >QUARTERLY_SELECTION</asp:Label>&nbsp;
                                                    </td>
                                                    <td nowrap align="left" width="20%">
                                                        &nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td nowrap align="left" width="20%">
                                                    </td>
                                                    <td nowrap align="left" width="20%">
                                                    </td>
                                                    <td nowrap align="left" width="20%">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td nowrap align="left" width="20%">
                                                    </td>
                                                    <td align="right" nowrap style="height: 19px" valign="middle">
                                                        <asp:Label ID="moQBeginDateLabel" runat="server" >SELECT_QUARTER_BEGIN_YEAR</asp:Label>:
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                        <asp:TextBox ID="txtBeginQuatNum" runat="server" TabIndex="1" Width="50px" OnFocus="togglePeriodSelection('txtBeginQuatNum',true);"
                                                            MaxLength="1"></asp:TextBox>
                                                        &nbsp;
                                                        <asp:DropDownList ID="cboBeginQuatYear" runat="server" onchange="togglePeriodSelection('cboBeginQuatYear',true);"
                                                            TabIndex="1" Width="60px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td nowrap align="left" width="20%">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td nowrap align="left" width="20%">
                                                    </td>
                                                    <td align="right" nowrap valign="middle">
                                                        &nbsp;&nbsp;
                                                        <asp:Label ID="moQEndDateLabel" runat="server" >SELECT_QUARTER_END_YEAR</asp:Label>:
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                        <asp:TextBox ID="txtEndQuatNum" runat="server" TabIndex="1" Width="50px" OnFocus="togglePeriodSelection('txtEndQuatNum',true);"
                                                            MaxLength="1"></asp:TextBox>
                                                        &nbsp;
                                                        <asp:DropDownList ID="cboEndQuatYear" runat="server" onchange="togglePeriodSelection('cboEndQuatYear',true);"
                                                            TabIndex="1" Width="60px">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td nowrap align="left" width="20%">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="5">
                                                        &nbsp;&nbsp;
                                                        <hr style="height: 1px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td nowrap align="right" width="20%" valign="top">
                                                        <asp:Label ID="moSortByLabel" runat="server">SELECT_REPORT_SORT_ORDER</asp:Label>&nbsp;
                                                    </td>
                                                    <td nowrap align="left">
                                                    </td>
                                                    <td nowrap align="left" width="20%">
                                                        <asp:RadioButtonList ID="rdReportSortOrder" runat="server" RepeatDirection="Vertical">
                                                            <asp:ListItem Selected="True" Value="0">ZIP_CODE</asp:ListItem>
                                                            <asp:ListItem Value="1">BRANCH_CODE</asp:ListItem>
                                                        </asp:RadioButtonList>
                                                    </td>
                                                    <td nowrap align="left" width="20%">
                                                    </td>
                                                    <td nowrap align="left" width="20%">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td width="100%">
                                <hr>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                &nbsp;
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
    
    function togglePeriodSelection(ctl,isSelected)
	{		
	    var objcontrol = ctl;	    
		if(isSelected)
		{		        
             if (objcontrol== 'moWeekBeginDateText' || objcontrol== 'moWeekEndDateText')
		     {		      
		        if(document.forms[0].moWeekBeginDateText.value  != null || document.forms[0].moWeekEndDateText.value  != null)
		        {
		           //alert("InvoiceNumberTextbox");
		           document.forms[0].txtBeginQuatNum.value ="";			           
		           document.forms[0].cboBeginQuatYear.selectedIndex = -1;
		           document.forms[0].txtEndQuatNum.value ="";			          
		           document.forms[0].cboEndQuatYear.selectedIndex = -1;
		           document.forms[0].txtBeginMonthNum.value ="";	
		           document.forms[0].cboBeginMonthyear.selectedIndex = -1;
		           document.forms[0].txtEndMonthNum.value ="";		          
		           document.forms[0].cboEndMonthyear.selectedIndex = -1;
		           document.getElementById('lblWeeklySelect').style.color='#000000';  
		           document.getElementById('lblQuaterlySelect').style.color='#000000';      
		           document.getElementById('lblMonthlySelect').style.color='#000000';
		           document.getElementById('moWBeginDateLabel').style.color='#000000';      
		           document.getElementById('moWEndDateLabel').style.color='#000000';  
		           document.getElementById('moQBeginDateLabel').style.color='#000000';      
		           document.getElementById('moQEndDateLabel').style.color='#000000';  
		           document.getElementById('moMBeginDateLabel').style.color='#000000';      
		           document.getElementById('moMEndDateLabel').style.color='#000000';   
		          // ShowControls();        		           
			       //return;
		        }
		      }   		    		        
		     if (objcontrol== 'txtBeginQuatNum' || objcontrol== 'cboBeginQuatYear' || objcontrol== 'txtEndQuatNum' || objcontrol== 'cboEndQuatYear' )
		      {
		        if 	(document.forms[0].txtBeginQuatNum.value  != null || document.forms[0].cboBeginQuatYear.selectedIndex != -1
		         || document.forms[0].txtEndQuatNum.value  != null  || document.forms[0].cboEndQuatYear.selectedIndex != -1)
		        {
		            //alert("InvoiceNumberTextbox");
		          
		           document.forms[0].moWeekBeginDateText.value ="";	
		           document.forms[0].txtBeginWeekNum.value ="";	
		           document.forms[0].moWeekEndDateText.value ="";	
		           document.forms[0].txtEndWeekNum.value ="";	
		           document.forms[0].txtBeginMonthNum.value ="";	
		           document.forms[0].cboBeginMonthyear.selectedIndex = -1;
		           document.forms[0].txtEndMonthNum.value ="";
		           document.forms[0].cboEndMonthyear.selectedIndex = -1;	
		           document.getElementById('lblWeeklySelect').style.color='#000000';  
		           document.getElementById('lblQuaterlySelect').style.color='#000000'
		           document.getElementById('lblMonthlySelect').style.color='#000000';
		           document.getElementById('moWBeginDateLabel').style.color='#000000';      
		           document.getElementById('moWEndDateLabel').style.color='#000000'; 
		           document.getElementById('moQBeginDateLabel').style.color='#000000';      
		           document.getElementById('moQEndDateLabel').style.color='#000000';   
		           document.getElementById('moMBeginDateLabel').style.color='#000000';      
		           document.getElementById('moMEndDateLabel').style.color='#000000'; 
		           HideControls();
		           		  		          		           
		           //return;
		        }		
		       }  
		      if (objcontrol== 'txtBeginMonthNum' ||  objcontrol== 'cboBeginMonthyear' || objcontrol== 'txtEndMonthNum' || objcontrol== 'cboEndMonthyear')
		       { 
		        if 	(document.forms[0].txtBeginMonthNum.value  != null || document.forms[0].cboBeginMonthyear.selectedIndex != -1
		         || document.forms[0].txtEndMonthNum.value  != null  || document.forms[0].cboEndMonthyear.selectedIndex != -1)
		        {
		        
		           //alert("InvoiceNumberTextbox");		           
		           document.forms[0].moWeekBeginDateText.value ="";	
		           document.forms[0].txtBeginWeekNum.value ="";	
		           document.forms[0].moWeekEndDateText.value ="";	
		           document.forms[0].txtEndWeekNum.value ="";	
		           document.forms[0].txtBeginQuatNum.value ="";			           
		           document.forms[0].cboBeginQuatYear.selectedIndex = -1;
		           document.forms[0].txtEndQuatNum.value ="";			          
		           document.forms[0].cboEndQuatYear.selectedIndex = -1;
		           document.getElementById('lblWeeklySelect').style.color='#000000';  
		           document.getElementById('lblQuaterlySelect').style.color='#000000';
		           document.getElementById('lblMonthlySelect').style.color='#000000';
		           document.getElementById('moWBeginDateLabel').style.color='#000000';      
		           document.getElementById('moWEndDateLabel').style.color='#000000';  
		           document.getElementById('moQBeginDateLabel').style.color='#000000';      
		           document.getElementById('moQEndDateLabel').style.color='#000000'; 
		           document.getElementById('moMBeginDateLabel').style.color='#000000';      
		           document.getElementById('moMEndDateLabel').style.color='#000000';    
		           HideControls();		             
		           //return;
		        }		
		     }    
		}   
		else
		{
		         // alert("InvoiceNumberTextbox");
		           document.forms[0].txtBeginQuatNum.value ="";			           
		           document.forms[0].cboBeginQuatYear.selectedIndex = -1;
		           document.forms[0].txtEndQuatNum.value ="";			          
		           document.forms[0].cboEndQuatYear.selectedIndex = -1;
		           document.forms[0].txtBeginMonthNum.value ="";	
		           document.forms[0].cboBeginMonthyear.selectedIndex = -1;
		           document.forms[0].txtEndMonthNum.value ="";		          
		           document.forms[0].cboEndMonthyear.selectedIndex = -1;
		           document.getElementById('lblWeeklySelect').style.color='#000000';  
		           document.getElementById('lblQuaterlySelect').style.color='#000000'
		           document.getElementById('lblMonthlySelect').style.color='#000000';
		           document.getElementById('moWBeginDateLabel').style.color='#000000';      
		           document.getElementById('moWEndDateLabel').style.color='#000000'; 
		           document.getElementById('moQBeginDateLabel').style.color='#000000';      
		           document.getElementById('moQEndDateLabel').style.color='#000000';   
		           document.getElementById('moMBeginDateLabel').style.color='#000000';      
		           document.getElementById('moMEndDateLabel').style.color='#000000'; 
		           //ShowControls();	
			       //return;
		}
	}
	function HideControls()
	{    	
       document.getElementById('txtBeginWeekNum').style.display  = "none";		
       document.getElementById('txtEndWeekNum').style.display  = "none";		
       document.getElementById('lblBeginWeekNum').style.display  = "none";	
       document.getElementById('lblEndWeekNum').style.display  = "none";
    }
    
    function ShowControls()
	{    	
       document.getElementById('txtBeginWeekNum').style.display  = "";		
       document.getElementById('txtEndWeekNum').style.display  = "";		
       document.getElementById('lblBeginWeekNum').style.display  = "";	
       document.getElementById('lblEndWeekNum').style.display  = "";
    }

    </script>

</body>
</html>
