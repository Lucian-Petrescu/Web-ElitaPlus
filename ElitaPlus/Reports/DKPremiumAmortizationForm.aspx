<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="DKPremiumAmortizationForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.DKPremiumAmortizationForm" %>

<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title id="rptWindowTitle" runat="server">DKPremiumAmortization</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Styles.css" type="text/css" rel="STYLESHEET">
    

    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>

    <script language="JavaScript">
	    //var arrDealerGroupCtr = [[['cboDealer'],['cboDealerCode']],['rdealer'],['rGroup'],['cboDealerGroup']]
	    var arrDealerGroupCtr = [[['moUserDealerMultipleDrop_moMultipleColumnDropDesc'],['moUserDealerMultipleDrop_moMultipleColumnDrop']],['rdealer'],['rGroup'],['cboDealerGroup']]
	    var arrDealerCtr = [[['moUserDealerMultipleDrop_moMultipleColumnDropDesc'],['moUserDealerMultipleDrop_moMultipleColumnDrop']],['rdealer'],['rGroup'],['cboDealerGroup']]
    </script>

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
                                <asp:Label ID="Label7" runat="server" CssClass="TITLELABELTEXT">DK_PREMIUM_AMORTIZATION</asp:Label></td>
                            <td height="20" align="right">
                                *
                                <asp:Label ID="moIndicatesLabel" runat="server"  CssClass="TITLELABEL"  EnableViewState="true">INDICATES_REQUIRED_FIELDS</asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table id="tblOuter2" style="border-right: black 1px solid; border-top: black 1px solid;
            margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid" height="93%"
            cellspacing="0" cellpadding="0" rules="none" width="98%" bgcolor="#d5d6e4" border="0">
            <!--ededd5-->
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td valign="top" align="center" style="height: 100%">
                    <asp:Panel ID="WorkingPanel" runat="server">
                        <table id="tblMain1" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                            border-left: #999999 1px solid; border-bottom: #999999 1px solid" cellspacing="0"
                            cellpadding="6" rules="cols" width="98%" align="center" bgcolor="#fef9ea" border="0" height = "98%">
                            <tr>
                                <td height="1">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0" height="95%">
                                        <tr>
                                            <td colspan="3">
                                                <table id="tblSearch" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                                                    border-left: #999999 1px solid; width: 100%; border-bottom: #999999 1px solid;
                                                    height: 64px" cellspacing="2" cellpadding="8" rules="cols" width="100%" align="center"
                                                    bgcolor="#fef9ea" border="0">
                                                    <tr>
                                                        <td align="left">
                                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                <tr>
                                                                    <td align="center" colspan="2">
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
                                            <td align="center" colspan="3" valign="top">
                                                <table id="Table2" style="width: 596px;" cellspacing="1" cellpadding="1" width="596"
                                                    border="0">
                                                    <tr>
                                                        <td align="left" colspan="4">
                                                            &nbsp;*
                                                            <asp:Label ID="MonthYearLabel" runat="server" >SELECT MONTH AND YEAR</asp:Label>
                                                            <asp:DropDownList ID="MonthDropDownList" runat="server" Height="16px" Width="126px"
                                                                AutoPostBack="false">
                                                            </asp:DropDownList>
                                                            <asp:DropDownList ID="YearDropDownList" runat="server" AutoPostBack="false">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" width="100%" colspan="4">
                                                            <hr style="width: 100%; height: 1px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="width: 100%; height: 0.01%" valign="top" nowrap align="left" colspan="4"
                                                            rowspan="1">
                                                            <uc1:MultipleColumnDDLabelControl ID="moUserCompanyMultipleDrop" runat="server">
                                                            </uc1:MultipleColumnDDLabelControl>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td id="ddSeparator" align="center" width="100%" colspan="4">
                                                            <hr style="width: 100%; height: 1px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 22px" valign="middle" align="left" colspan="4">
                                                            *
                                                            <asp:RadioButton ID="rdealer" onclick=" document.all.item('moUserDealerMultipleDrop_lb_DropDown').style.color = ''; ToggleExt(this, arrDealerCtr); fncEnable(2); "
                                                                AutoPostBack="false" type="radio" GroupName="Dealer" Text="SELECT_ALL_DEALERS"
                                                                TextAlign="left" runat="server" Checked="False"></asp:RadioButton>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 26px" align="left" colspan="4">
                                                            <uc1:MultipleColumnDDLabelControl ID="moUserDealerMultipleDrop" runat="server"></uc1:MultipleColumnDDLabelControl>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 22px" colspan="4">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left">
                                                            <asp:RadioButton ID="rGroup" onclick="ToggleExt(this, arrDealerGroupCtr); fncEnable(1);"
                                                                Width="197px" AutoPostBack="false" type="radio" GroupName="Dealer" Text="SELECT_ALL_GROUPS"
                                                                TextAlign="left" runat="server" Checked="False"></asp:RadioButton>
                                                        </td>
                                                        <td nowrap align="left">
                                                            <asp:Label ID="GroupLabel" runat="server">OR_A_SINGLE_GROUP</asp:Label>
                                                        </td>
                                                        <td align="left" colspan="2">
                                                            <asp:DropDownList ID="cboDealerGroup" runat="server" Width="190px" type="DropDown"
                                                                onchange="ToggleExt(this, arrDealerGroupCtr); fncEnable(1);  document.all.item('moUserDealerMultipleDrop_lb_DropDown').style.color = '';">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" width="100%" colspan="4">
                                                            <hr style="width: 100%; height: 1px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="left" colspan="1">
                                                            <asp:RadioButton ID="RadiobuttonTotalsOnly" onclick="toggleDetailSelection(false);"
                                                                AutoPostBack="false" Text="SHOW TOTALS ONLY" TextAlign="left" runat="server">
                                                            </asp:RadioButton>
                                                        </td>
                                                        <td align="left" colspan="3">
                                                            <asp:RadioButton ID="RadiobuttonDetail" onclick="toggleDetailSelection(true);" AutoPostBack="false"
                                                                Text="OR SHOW DETAIL WITH TOTALS" TextAlign="left" runat="server"></asp:RadioButton>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 22px" colspan="4">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td nowrap align="left" width="25%" colspan="4">
                                                            <asp:Label ID="lblTotalsByCov" runat="server">SHOW_TOTALS_PAGE_BY_COVERAGE</asp:Label>
                                                            <asp:CheckBox ID="chkTotalsPageByCov" runat="server" TextAlign="Left" AutoPostBack="false">
                                                            </asp:CheckBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                            <input type="hidden" id="hidden_dacvalue" runat="server" name="hidden_dacvalue" />
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 5px">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <hr>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    &nbsp;
                                    <asp:Button ID="btnGenRpt" Style="background-image: url(../Navigation/images/viewIcon2.gif);
                                        cursor: hand; background-repeat: no-repeat" runat="server" 
                                        Width="100px" Text="View" CssClass="FLATBUTTON" Height="20px"></asp:Button>&nbsp;</td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </form>

    <script language="JavaScript" src="../Navigation/Scripts/ReportCeMainScripts.js"></script>

    <script language="JavaScript">
	   	
   function fncEnable(chknum)
   {
            if(chknum == 1 )  // for Dealer Group
            {
                 //debugger;
                 if (document.forms[0].hidden_dacvalue.value != "NONE")
                 {                  
                  document.getElementById('chkTotalsPageByCov').disabled = false;
                  document.getElementById('chkTotalsPageByCov').parentElement.removeAttribute('disabled');
                  document.getElementById('lblTotalsByCov').disabled = false;
                  return true;                                           
                 }
                 else
                 {
                  return false;    
                 }                                 
            }
            else    // for Dealer
            {
                 //debugger;
                if (document.forms[0].hidden_dacvalue.value != "NONE")
                  {
                  document.getElementById('chkTotalsPageByCov').parentElement.setAttribute('disabled','true');
                  document.getElementById('chkTotalsPageByCov').checked = false;
                  document.getElementById('lblTotalsByCov').disabled = true;
                  return true;                      
                 }
                 else
                 {
                    return false; 
                 }
            }
   }
    </script>

</body>
</html>
