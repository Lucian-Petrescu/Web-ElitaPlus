<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SpainFulfillmentReportForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.SpainFulfillmentReportForm" %>

<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title id="rptWindowTitle" runat="server">EXPORT_FULFILLMENT</title>
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
                            <asp:Label ID="Label7" runat="server"  CssClass="TITLELABELTEXT">EXPORT_FULFILLMENT</asp:Label>
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
                                        <td style="width: 99.43%; height: 1px" colspan="3">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3" valign="top" height="95%">
                                            <table id="Table2" border="0" cellpadding="1" cellspacing="1" width="75%">
                                                <tr>
                                                    <td align="center" colspan="4" valign="top">
                                                        <table cellspacing="1" cellpadding="0" width="85%" border="0">
                                                            <tr>
                                                                <td id="Td1" align="center" colspan="5">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="10%" align="left">
                                                                </td>
                                                                <td align="right" nowrap valign="middle">
                                                                    &nbsp;
                                                                </td>
                                                                <td nowrap>
                                                                    <asp:Label ID="lblbegindate" runat="server" >BEGIN_DATE</asp:Label>
                                                                </td>
                                                                <td nowrap>
                                                                    <asp:Label ID="lblenddate" runat="server" >END_DATE</asp:Label>
                                                                </td>
                                                                <td align="right" nowrap  width="15%">
                                                                    &nbsp;
                                                                </td>                                                              
                                                            </tr>
                                                            <tr>
                                                                <td width="10%" align="left">
                                                                </td>
                                                                <td align="left" nowrap valign="bottom">
                                                                    <asp:RadioButton ID="rSelectDates" runat="server" Text="SELECT_DATES" AutoPostBack="false"
                                                                        onclick="toggleDateorCertSelection('rSelectDates',false);" TextAlign="right" />
                                                                </td>
                                                                <td nowrap>
                                                                    <asp:TextBox ID="txtbegindate" runat="server" TabIndex="1" ></asp:TextBox>
                                                                    <asp:ImageButton ID="BtnBeginDate" runat="server"   ImageUrl="../Common/Images/calendarIcon2.jpg"
                                                                        Width="20px" />
                                                                </td>
                                                                <td nowrap>
                                                                    <asp:TextBox ID="txtenddate" runat="server" TabIndex="1" ></asp:TextBox>
                                                                    <asp:ImageButton ID="BtnEndDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                                                        Width="20px" />
                                                                </td>
                                                                <td align="right" nowrap  width="15%">
                                                                    &nbsp;
                                                                </td> 
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5">
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
                                                                    <asp:Label ID="lblbegincertnum" runat="server" >BEGIN_NUMBER</asp:Label>
                                                                </td>
                                                                <td nowrap>
                                                                    <asp:Label ID="lblendcertnum" runat="server" >END_NUMBER</asp:Label>
                                                                </td>
                                                                <td align="right" nowrap width="15%">
                                                                    &nbsp;
                                                                </td> 
                                                            </tr>
                                                            <tr>
                                                                <td width="10%" nowrap>
                                                                </td>
                                                                <td align="LEFT" nowrap valign="bottom">
                                                                    <asp:RadioButton ID="rSelectCertNum" runat="server" AutoPostBack="false" Text="OR_ENTER_CERT_NUMBER"
                                                                        onclick="toggleDateorCertSelection('rSelectCertNum',true);" TextAlign="right"   />&nbsp;
                                                                </td>
                                                                <td nowrap>
                                                                    <asp:TextBox ID="txtbegincertnum" runat="server" TabIndex="1"    width = "180px"></asp:TextBox>
                                                                </td>
                                                                <td nowrap>
                                                                    <asp:TextBox ID="txtendcertnum" runat="server" TabIndex="1"   width = "180px"></asp:TextBox>
                                                                </td>
                                                                <td align="right" nowrap width="15%">
                                                                    &nbsp;
                                                                </td> 
                                                            </tr>
                                                            <tr>
                                                                <td align="center" colspan="5" width="100%">
                                                                    <hr style="width: 100%; height: 1px" />
                                                                </td>
                                                            </tr>                                                                                                                       
                                                            <tr>
                                                                <td nowrap align="left" width="10%" colspan="1">
                                                                </td>
                                                                <td align="left" valign="bottom" nowrap colspan="1">
                                                                    *
                                                                    <asp:RadioButton ID="rdealer" runat="server" AutoPostBack="false" Checked="False"
                                                                        onclick="ToggleDualDropDownsSelection('moDealerMultipleDrop_moMultipleColumnDrop', 'moDealerMultipleDrop_moMultipleColumnDropDesc', 'rdealer', false, ''); document.all.item('moDealerMultipleDrop_lb_DropDown').style.color = '';"
                                                                        Text="SELECT_ALL_DEALERS" TextAlign="left"  />
                                                                    &nbsp;
                                                                </td>                                                           
                                                                <td nowrap valign="baseline" colspan="3" align="left" width = "65%">
                                                                    <uc1:MultipleColumnDDLabelControl ID="moDealerMultipleDrop" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5" height="20px">
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
                            <td>
                                <hr style="width: 99.43%; height: 1px" />
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Button ID="btnGenRpt" Style="background-image: url(../Navigation/images/viewIcon2.gif);
                                    cursor: hand; background-repeat: no-repeat" runat="server" 
                                    CssClass="FLATBUTTON" Width="100px" Text="View" Height="20px"></asp:Button>
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
    
    function toggleDateorCertSelection(ctl,isSelected)
	{		
	    var objcontrol = ctl;	    
		if(isSelected)
		{	
		   // alert(objcontrol);	        
             if (objcontrol == 'rSelectDates')
		     {		      
		        if(document.forms[0].rSelectDates.checked == true)
		        {		           		          
		           document.forms[0].txtbegincertnum.value ="";	           		         
		           document.forms[0].txtendcertnum.value ="";	       
		           document.forms[0].rSelectCertNum.checked = false;
		           document.getElementById('txtbegindate').disabled = false;
		           document.getElementById('txtenddate').disabled =false;
		           document.getElementById('txtbegincertnum').disabled =true;
		           document.getElementById('txtendcertnum').disabled =true;		           
	               document.getElementById('lblbegindate').style.color = '';
	               document.getElementById('lblenddate').style.color = ''; 	
	               document.getElementById('lblbegincertnum').style.color = '';
	               document.getElementById('lblendcertnum').style.color = ''; 		               	               
		        }
		      }   
		       if (objcontrol== 'rSelectCertNum')
		      {		      
		        if(document.forms[0].rSelectCertNum.checked == true)
		        {		           
		           document.forms[0].txtbegindate.value ="";	
		           document.forms[0].txtenddate.value ="";					           
		           document.forms[0].rSelectDates.checked = false;
		           document.getElementById('txtbegindate').disabled =true;
		           document.getElementById('txtenddate').disabled =true;
		           document.getElementById('txtbegincertnum').disabled =false;
		           document.getElementById('txtendcertnum').disabled =false;		           
	               document.getElementById('lblbegindate').style.color = '';
	               document.getElementById('lblenddate').style.color = ''; 	
	               document.getElementById('lblbegincertnum').style.color = '';
	               document.getElementById('lblendcertnum').style.color = ''; 	 	              	              
		        }
	          }
	     }      
	    else
		 {		           
		           document.forms[0].txtbegincertnum.value ="";	           		         
		           document.forms[0].txtendcertnum.value ="";	       
		           document.forms[0].rSelectCertNum.checked = false;
		           document.getElementById('txtbegindate').disabled=false;
		           document.getElementById('txtenddate').disabled=false;
		           document.getElementById('txtbegincertnum').disabled=true;
		           document.getElementById('txtendcertnum').disabled=true;		           
	               document.getElementById('lblbegindate').style.color = '';
	               document.getElementById('lblenddate').style.color = ''; 	
	               document.getElementById('lblbegincertnum').style.color = '';
	               document.getElementById('lblendcertnum').style.color = '';                	                             
		 }
	}
	</script>	
</body>
</html>
