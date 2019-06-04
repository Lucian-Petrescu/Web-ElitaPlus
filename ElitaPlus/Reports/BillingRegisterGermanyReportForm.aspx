<%@ Page Language="vb" AutoEventWireup="false" Codebehind="BillingRegisterGermanyReportForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.BillingRegisterGermanyReportForm" %>

<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title id="rptWindowTitle" runat="server">Billing Register Germany</title>
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
                                    ID="Label7" runat="server"  CssClass="TITLELABELTEXT">BILLING_REGISTER_GERMANY</asp:Label></td>
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
                    &nbsp;</td>
            </tr>
            <tr>
                <td valign="top" align="center" height="100%">
                    <asp:Panel ID="WorkingPanel" runat="server">
                        <table id="tblMain1" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                            border-left: #999999 1px solid; border-bottom: #999999 1px solid" cellspacing="0"
                            cellpadding="6" rules="cols" width="98%" align="center" bgcolor="#fef9ea" border="0">
                            <tr>
                                <td height="1">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
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
                                            <td height="10px">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="4">
                                                <table cellspacing="2" cellpadding="0" width="75%" border="0">
                                                    <tr>
                                                        <td align="center" width="50%" colspan="4">
                                                            <table cellspacing="0" cellpadding="0" width="75%" border="0">
                                                                <tr>
                                                                    <td width="50%">
                                                                        &nbsp;</td>
                                                                    <td valign="middle" nowrap align="right">
                                                                        <asp:Label ID="moBeginDateLabel" runat="server" >BEGIN_DATE</asp:Label>:</td>
                                                                    <td nowrap>
                                                                        &nbsp;
                                                                        <asp:TextBox ID="moBeginDateText" TabIndex="1" runat="server" CssClass="FLATTEXTBOX"></asp:TextBox>&nbsp;</td>
                                                                    <td>
                                                                        <asp:ImageButton ID="BtnBeginDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                                                                        </asp:ImageButton>&nbsp;&nbsp;</td>
                                                                    <td valign="middle" nowrap align="right">
                                                                        &nbsp;&nbsp;
                                                                        <asp:Label ID="moEndDateLabel" runat="server" >END_DATE</asp:Label>:</td>
                                                                    <td nowrap>
                                                                        &nbsp;
                                                                        <asp:TextBox ID="moEndDateText" TabIndex="1" runat="server" CssClass="FLATTEXTBOX"
                                                                            Width="125px"></asp:TextBox>&nbsp;</td>
                                                                    <td>
                                                                        <asp:ImageButton ID="BtnEndDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                                                                        </asp:ImageButton></td>
                                                                    <td width="50%">
                                                                        &nbsp;</td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <hr style="height: 1px"/>
                                                        </td>
                                                    </tr>                                                     
                                                    <tr>
                                                        <td nowrap align="right" width="25%" colspan="2">
                                                            <asp:Label ID="dReport" runat="server">DEALER_LEVEL_REPORT:</asp:Label>&nbsp;&nbsp;
                                                        </td>
                                                        <td nowrap align="left" width="25%">
                                                            &nbsp;&nbsp;&nbsp;</td>
                                                        <td nowrap align="left" width="40%">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            </td>
                                                    </tr>
                                                    <tr>
                                                        <td nowrap align="right" width="25%" colspan="2">
                                                        </td>
                                                        <td nowrap align="left" width="25%">
                                                            &nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rdealer" onclick="toggleDealersSelection('rdealer',false);"
                                                                Checked="false" TextAlign="left" Text="SELECT_ALL_DEALERS" runat="server" AutoPostBack="false">
                                                            </asp:RadioButton></td>
                                                        <td nowrap align="left" width="40%">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="2">
                                                        </td>
                                                        <td nowrap align="left" width="25%">
                                                            &nbsp;&nbsp;
                                                            <asp:Label ID="moDealerLabel" runat="server">OR A SINGLE DEALER</asp:Label>:&nbsp;&nbsp;
                                                        </td>
                                                        <td nowrap align="left" width="40%">
                                                            <asp:DropDownList ID="cbodealer" runat="server" Width="100%" AutoPostBack="false"
                                                                onchange="toggleDealersSelection('cbodealer',true);">
                                                            </asp:DropDownList></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            </td>
                                                    </tr>
                                                    <tr>
                                                        <td nowrap align="right" width="25%" colspan="2">
                                                        </td>
                                                        <td nowrap align="left" width="25%">
                                                            &nbsp;&nbsp;
                                                            <asp:Label ID="lblDealerGrp" runat="server">OR_A_SINGLE_DEALER_GROUP</asp:Label>:&nbsp;&nbsp;
                                                        </td>
                                                        <td nowrap align="left" width="40%">
                                                            <asp:DropDownList ID="cboDealerGrp" runat="server" AutoPostBack="false" Width="100%"
                                                                onchange="toggleDealersSelection('cboDealerGrp',true);">
                                                            </asp:DropDownList></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <hr style="height: 1px"/>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td nowrap align="right" width="25%" colspan="2">
                                                            <asp:Label ID="DgrpReport" runat="server">DEALER_GROUP_LEVEL_REPORT:</asp:Label>&nbsp;&nbsp;
                                                        </td>
                                                        <td nowrap align="left" width="25%">
                                                            &nbsp;&nbsp;&nbsp;</td>
                                                        <td nowrap align="left" width="40%">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            </td>
                                                    </tr>
                                                    <tr>
                                                        <td nowrap align="right" width="25%" colspan="2">
                                                        </td>
                                                        <td nowrap align="left" width="25%">
                                                            &nbsp;&nbsp;&nbsp;<asp:RadioButton ID="rdealergrp" onclick="toggleDealersSelection('rdealergrp',true);"
                                                                AutoPostBack="false" Checked="false" runat="server" Text="SELECT_ALL_DEALER_GROUPS"
                                                                TextAlign="left"></asp:RadioButton></td>
                                                        <td nowrap align="left" width="40%">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                          </td>
                                                    </tr>
                                                    <tr>
                                                        <td nowrap align="right" width="25%" colspan="2">
                                                        </td>
                                                        <td nowrap align="left" width="25%">
                                                            &nbsp;&nbsp;
                                                            <asp:Label ID="moDealergrouplabel" runat="server">OR_A_SINGLE_DEALER_GROUP</asp:Label>:&nbsp;&nbsp;</td>
                                                        <td nowrap align="left" width="25%">
                                                            <asp:DropDownList ID="cboadealergrp" runat="server" AutoPostBack="false" Width="100%"
                                                                onchange="toggleDealersSelection('cboadealergrp',true);">
                                                            </asp:DropDownList></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">                                                          
                                                            <hr style="height: 1px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td nowrap align="right" width="25%" colspan="2">
                                                            <asp:RadioButton ID="RadiobuttonTotalsOnly" onclick="toggleDetailSelection(false);"
                                                                AutoPostBack="false" Text="SHOW TOTALS ONLY" TextAlign="left" runat="server"></asp:RadioButton>
                                                        </td>
                                                        <td nowrap align="left" width="25%">
                                                            &nbsp;&nbsp;&nbsp;<asp:RadioButton ID="RadiobuttonDetail" onclick="toggleDetailSelection(true);"
                                                                AutoPostBack="false" Text="OR SHOW DETAIL WITH TOTALS" TextAlign="left" runat="server">
                                                            </asp:RadioButton></td>
                                                        <td nowrap align="left" width="25%">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <hr style="height: 1px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="top" nowrap align="center" width="25%" colspan="4">
                                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                <tr>
                                                                    <td valign="top" nowrap align="right" width="25%">
                                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                            <tr>
                                                                                <td nowrap align="center">
                                                                                    <asp:Label ID="Label5" runat="server">SELECT_REPORT_SORT_ORDER</asp:Label>:</td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td valign="top" width="55%" colspan="2" align="left">
                                                                        <asp:RadioButtonList ID="rdReportSortOrder" runat="server" RepeatDirection="Vertical">
                                                                            <asp:ListItem Value="0" Selected="True">DEALER_CODE</asp:ListItem>
                                                                            <asp:ListItem Value="1">DEALER_NAME</asp:ListItem>
                                                                        </asp:RadioButtonList></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>                                                 
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" width="100%">
                                                <hr>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    &nbsp;
                                    <asp:Button ID="btnGenRpt" Style="background-image: url(../Navigation/images/viewIcon2.gif);
                                        cursor: hand; background-repeat: no-repeat" runat="server" 
                                        CssClass="FLATBUTTON" Text="View" Height="20px" Width="100px"></asp:Button></td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </form>

    <script language="JavaScript" src="../Navigation/Scripts/ReportCeMainScripts.js"></script>

    <script language="JavaScript">
    
    function toggleDealersSelection(ctl,isSingleDealer)
	{		
	    var objcontrol = ctl;	    
	    //alert(objcontrol);
		if(isSingleDealer)
		{
		    if (objcontrol== 'rdealer')
		    {
    		    if (document.forms[0].rdealer.checked = true)
    		    {
    		        //alert("rdealer");
    			    document.forms[0].cbodealer.selectedIndex = -1;
    			    document.forms[0].cboDealerGrp.selectedIndex = -1;	
    			    document.forms[0].rdealergrp.checked = false;
    			    document.forms[0].cboadealergrp.selectedIndex = -1;		
                    return true;		
    		    }
            }
            if (objcontrol== 'cbodealer')
		    {
    		    if 	(document.forms[0].cbodealer.selectedIndex != -1)
    		    {
    		        //alert("cbodealer");
    			    document.forms[0].rdealer.checked = false;
    			    document.forms[0].cboDealerGrp.selectedIndex = -1;	
    			    document.forms[0].rdealergrp.checked = false;
    			    document.forms[0].cboadealergrp.selectedIndex = -1;				    
    			    return;
    		    }
            }
            if (objcontrol== 'cboDealerGrp')
		    {
    		    if 	(document.forms[0].cboDealerGrp.selectedIndex != -1)
    		    {
    		        //alert("cbodealer");
    			    document.forms[0].rdealer.checked = false;
    			    document.forms[0].cbodealer.selectedIndex = -1;
    			    document.forms[0].rdealergrp.checked = false;
    			    document.forms[0].cboadealergrp.selectedIndex = -1;				    
    			    return;
    		    }
            }
             /*if (objcontrol== 'cboDealerGrp')
		    {
    		    if 	(document.forms[0].cboDealerGrp.selectedIndex != -1)
    		    {
    		      if (document.forms[0].cbodealer.selectedIndex == -1)    		        
    		        {
    		            //alert("cbodealer");
    			        document.forms[0].rdealer.checked = true;
    			        document.forms[0].rdealergrp.checked = false;
    			        document.forms[0].cboadealergrp.selectedIndex = -1;				    
    			        return;
    			     }
    			     else
    			     {
    			        //document.forms[0].rdealer.checked = true;
    			        document.forms[0].rdealergrp.checked = false;
    			        document.forms[0].cboadealergrp.selectedIndex = -1;				    
    			        return;    			        
    			     }   
    		    }
            }*/
            if (objcontrol== 'rdealergrp')
		    {
		        if 	(document.forms[0].rdealergrp.checked = true)
		        {
		            //alert("rdealergrp");
		            document.forms[0].rdealer.checked = false;
			        document.forms[0].cbodealer.selectedIndex = -1;		
			        document.forms[0].cboDealerGrp.selectedIndex = -1	
			        document.forms[0].cboadealergrp.selectedIndex = -1;			    
			        return;
		        }		
		     }   
		    if (objcontrol== 'cboadealergrp')
		    {
		        if 	(document.forms[0].cboadealergrp.selectedIndex != -1)
		        {
		            //alert("cboadealergrp");
		            document.forms[0].rdealer.checked = false;
			        document.forms[0].cbodealer.selectedIndex = -1;	
			        document.forms[0].cboDealerGrp.selectedIndex = -1		
			        document.forms[0].rdealergrp.checked = false;			    
			        return;
		        }
		     }   
		}   
		else
		{
		    document.forms[0].cbodealer.selectedIndex = -1;
		    document.forms[0].cboDealerGrp.selectedIndex = -1;	
			document.forms[0].rdealergrp.checked = false;
			document.forms[0].cboadealergrp.selectedIndex = -1;	
			return;
		}
	}
	
    </script>

</body>
</html>
