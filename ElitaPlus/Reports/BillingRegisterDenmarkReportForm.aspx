<%@ Page Language="vb" AutoEventWireup="false" Codebehind="BillingRegisterDenmarkReportForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.BillingRegisterDenmarkReportForm" %>

<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title id="rptWindowTitle" runat="server">Billing Register Denmark</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Styles.css" type="text/css" rel="STYLESHEET">
    
    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>
    <script language="JavaScript" src="../Navigation/Scripts/ReportceScripts.js"></script>

   	<SCRIPT language="JavaScript">
	    //var arrDealerGroupCtr = [[['cboDealer'],['cboDealerCode']],['rdealer'],['rGroup'],['cboDealerGroup']]
	    var arrDealerGroupCtr = [[['multipleDropControl_moMultipleColumnDropDesc'],['multipleDropControl_moMultipleColumnDrop']],['rdealer']]
	    
		</SCRIPT>

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
                                    ID="Label7" runat="server"  CssClass="TITLELABELTEXT">BILLING_REGISTER_DENMARK</asp:Label></td>
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
                            cellpadding="6" rules="cols" width="98%" align="center" bgcolor="#fef9ea" border="0" height="95%">
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
                                            <td colspan="3">
                                                <img height="15" src="../Navigation/images/trans_spacer.gif"></td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="4">
                                                <table cellspacing="2" cellpadding="0" width="75%" border="0">
                                                    <tr>
                                                        <td colspan="4" align="center">
                                                            <table border="0" cellpadding="0" cellspacing="0" width="65%">
                                                                <tr>
                                                                    <td align="left" colspan="2" nowrap valign="top">
                                                                        <uc1:MultipleColumnDDLabelControl ID="moUserCompanyMultipleDrop" runat="server" />
                                                                    </td>
                                                                    <td align="right" colspan="2">
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td id="ddSeparator" align="center" colspan="4" width="100%">
                                                            <hr style="width: 100%; height: 1px"></hr>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 1px" colspan="4" valign=top>
                                                           
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td nowrap align="right" width="25%" colspan="2">
                                                            <asp:Label ID="lblPeriodReport" runat="server">Base Report On Reporting Period:</asp:Label>&nbsp;&nbsp;
                                                        </td>
                                                        <td nowrap align="left" width="25%">
                                                            &nbsp;&nbsp;&nbsp;</td>
                                                        <td nowrap align="left" width="40%">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <img height="1" src="../Navigation/images/trans_spacer.gif"></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" width="20%" colspan="2"></td>
                                                        <td nowrap align="left" width="25%" colspan="2">
                                                            <table cellspacing="0" cellpadding="0" width="55%" border="0">
                                                                <tr>
                                                                     &nbsp;&nbsp;<td valign="middle" nowrap align="left" style="height: 19px">
                                                                        <asp:Label ID="moBeginDateLabel" runat="server" >BEGIN_DATE</asp:Label>:</td>
                                                                    <td>
                                                                        &nbsp;
                                                                        <asp:TextBox ID="moBeginDateText" TabIndex="1" runat="server" CssClass="FLATTEXTBOX"></asp:TextBox>&nbsp;</td>
                                                                    <td>
                                                                        <asp:ImageButton ID="BtnBeginDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                                                                        </asp:ImageButton>&nbsp;&nbsp;</td>
                                                                    <td valign="middle" nowrap align="right">
                                                                        &nbsp;&nbsp;
                                                                        <asp:Label ID="moEndDateLabel" runat="server" >END_DATE</asp:Label>:</td>
                                                                    <td>
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
                                                            <img height="5" src="../Navigation/images/trans_spacer.gif"></td>
                                                    </tr>                                                                                                   
                                                    <tr>
                                                        <td nowrap align="right" width="25%" colspan="2" style="height: 12px" valign =bottom>
                                                              <asp:RadioButton ID="rdealer" onclick="toggleSelection('rdealer',false);"
                                                                Checked="false" TextAlign="left" Text="SELECT_ALL_DEALERS" runat="server" AutoPostBack="false">
                                                            </asp:RadioButton></td>
                                                        <td nowrap align="left" style="height: 12px" colspan="2">
                                                            <uc1:MultipleColumnDDLabelControl ID="multipleDropControl" runat="server"></uc1:MultipleColumnDDLabelControl>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4" >
                                                            &nbsp;&nbsp;
                                                            <hr style="height: 1px" />
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td nowrap align="right" width="25%" colspan="2" style="height: 13px">
                                                            <asp:Label ID="lblInvoiceReport" runat="server">Base Report On An Invoice Number:</asp:Label>&nbsp;&nbsp;
                                                        </td>
                                                        <td nowrap align="left" width="25%" style="height: 13px">
                                                            &nbsp;&nbsp;&nbsp;</td>
                                                        <td nowrap align="left" width="40%" style="height: 13px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <img height="1" src="../Navigation/images/trans_spacer.gif"></td>
                                                    </tr>
                                                    <tr>
                                                        <td nowrap align="right" width="25%" colspan="2" style="height: 20px">
                                                            </td>
                                                        <td nowrap align="left" width="50%" colspan=2 style="height: 20px">
                                                            <asp:Label ID="InvoiceNumberLabel" runat="server" >INVOICE_NUMBER</asp:Label>:                                                       
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:TextBox ID="InvoiceNumberTextbox" TabIndex="1" runat="server"
                                                                AutoPostBack="false" OnFocus="toggleSelection('InvoiceNumberTextbox',true);" MaxLength="8" CssClass="FLATTEXTBOX" ></asp:TextBox></td>
                                                    </tr>
                                                      <tr>
                                                        <td colspan="4">
                                                            &nbsp;&nbsp;
                                                            <hr style="height: 1px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <img height="1" src="../Navigation/images/trans_spacer.gif"></td>
                                                    </tr>
                                                    <tr>
                                                        <td nowrap align="right" width="25%" colspan="2" style="height: 13px">
                                                            <asp:Label ID="lblCreditReport" runat="server">Base_Report_On_A_Credit_Note_Number:</asp:Label>&nbsp;&nbsp;                                                        </td>
                                                        <td nowrap align="left" width="25%" style="height: 13px">
                                                            &nbsp;&nbsp;&nbsp;</td>
                                                        <td nowrap align="left" width="40%" style="height: 13px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <img height="1" src="../Navigation/images/trans_spacer.gif"></td>
                                                    </tr>
                                                    <tr>
                                                        <td nowrap align="right" width="25%" colspan="2">
                                                            </td>
                                                        <td nowrap align="left" width="50%" colspan=2>
                                                            <asp:Label ID="CreditNoteLabel" runat="server" >CREDIT_NOTE_NUMBER</asp:Label>:
                                                                &nbsp;
                                                                     <asp:TextBox ID="CreditNoteTextbox" TabIndex="1" runat="server"
                                                                AutoPostBack="false" OnFocus="toggleSelection('CreditNoteTextbox',true);" MaxLength="8" CssClass="FLATTEXTBOX" ></asp:TextBox></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            &nbsp;&nbsp;
                                                            <hr style="height: 1px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="right" colspan="2" nowrap style="height: 20px" width="25%">
                                                            <asp:RadioButton ID="rdealerlevel" runat="server" AutoPostBack="false" GroupName="rlevel"
                                                                Text="DEALER_LEVEL" TextAlign="left" />
                                                        </td>
                                                        <td align="right" nowrap style="height: 20px" width="25%">
                                                            <asp:RadioButton ID="rbranchlevel" runat="server" AutoPostBack="false" GroupName="rlevel"
                                                                Text="BRANCH_LEVEL" TextAlign="Left" />
                                                        </td>
                                                        <td align="left" nowrap style="height: 20px" width="25%">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            &nbsp;&nbsp;
                                                            <hr style="height: 1px"></hr>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td nowrap align="right" width="25%" colspan="2" style="height: 20px">
                                                            <asp:RadioButton ID="RadiobuttonTotalsOnly" onclick="toggleDetailSelection(false);"
                                                                AutoPostBack="false" Text="SHOW TOTALS ONLY" TextAlign="left" runat="server" GroupName="rtotal">
                                                            </asp:RadioButton>
                                                        </td>
                                                        <td nowrap align="right" width="25%" style="height: 20px">
                                                            <asp:RadioButton ID="RadiobuttonDetail" onclick="toggleDetailSelection(true);" AutoPostBack="false"
                                                                Text="OR SHOW DETAIL WITH TOTALS" TextAlign="Left" runat="server" GroupName="rtotal"></asp:RadioButton></td>
                                                        <td nowrap align="left" width="25%" style="height: 20px">
                                                        </td>
                                                    </tr>                                                                                              
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 85%; width: 100%;">
                                </td>
                            </tr>
                            <tr>
                                <td width="100%">
                                    <hr/>
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
    
    function toggleSelection(ctl,isSingleDealer)
	{		
	    var objcontrol = ctl;	    
	    var objCodeDropDown = document.getElementById('multipleDropControl_moMultipleColumnDrop'); // "By Code" DropDown control
	    var objDecDropDown = document.getElementById('multipleDropControl_moMultipleColumnDropDesc');   // "By Description" DropDown control 
	    //alert(objcontrol);
		if(isSingleDealer)
		{
		    if (objcontrol== 'rdealer')
		    {
    		    if (document.forms[0].rdealer.checked = true)
    		    {
    		        //alert("rdealer");
    		            debugger;
    			    objCodeDropDown.selectedIndex = -1;	
        			objDecDropDown.selectedIndex = -1;		
    			    document.forms[0].InvoiceNumberTextbox.value ="";
    			    document.forms[0].CreditNoteTextbox.value ="";
                    //return ;		
    		    }
            }                
            if (objcontrol== 'InvoiceNumberTextbox')
		    {
		        if 	(document.forms[0].InvoiceNumberTextbox.value  != null)
		        {
		           //alert("InvoiceNumberTextbox");
		           document.forms[0].rdealer.checked = false;
			       objCodeDropDown.selectedIndex = -1;	
        		   objDecDropDown.selectedIndex = -1;
    			   document.forms[0].CreditNoteTextbox.value ="";	
			       //return;
		        }		
		     }   
		     if (objcontrol== 'CreditNoteTextbox')
		    {
		        if 	(document.forms[0].CreditNoteTextbox.value  != null)
		        {
		            //alert("CreditNoteTextbox");
		           document.forms[0].rdealer.checked = false;
			       objCodeDropDown.selectedIndex = -1;	
        		   objDecDropDown.selectedIndex = -1;
    			   document.forms[0].InvoiceNumberTextbox.value ="";	
			       //return;
		        }		
		     }   
		}   
		else
		{
		        //   alert(objCodeDropDown);
		            objCodeDropDown.selectedIndex = -1;	
        			objDecDropDown.selectedIndex = -1;		
		            document.forms[0].InvoiceNumberTextbox.value ="";
    			    document.forms[0].CreditNoteTextbox.value ="";
			    //   return;
		}
	}
	
    </script>
</body>
</html>
