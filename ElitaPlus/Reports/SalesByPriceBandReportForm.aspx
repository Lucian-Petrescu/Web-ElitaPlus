<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SalesByPriceBandReportForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.SalesByPriceBandReportForm" %>

<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title id="rptWindowTitle" runat="server">SALES_BY_PRICE_BAND</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Styles.css" type="text/css" rel="STYLESHEET">
    

    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>
    <script language="JavaScript">
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
                            <asp:Label ID="Label7" runat="server"  CssClass="TITLELABELTEXT">SALES_BY_PRICE_BAND</asp:Label>
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
                            <td>
                                <table cellspacing="0" cellpadding="0" width="100%" border="0" height="95%">
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
                                        <td style="width: 99.43%; height: 1px" colspan="3">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3" valign="top">
                                            <table border="0" cellpadding="0" cellspacing="1" width="75%">
                                                <tr>
                                                    <td colspan="4" align="center">
                                                        <table id="Table2" border="0" cellpadding="1" cellspacing="1" width="75%">
                                                            <tr>
                                                                <td align="center" colspan="5">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap align="left" width="1%" colspan="1">
                                                                </td>
                                                                <td align="left" nowrap valign="baseline" colspan="1">
                                                                    *
                                                                    <asp:Label ID="moBeginDateLabel" runat="server" >BEGIN_DATE</asp:Label>:
                                                                    <asp:TextBox ID="moBeginDateText" runat="server" CssClass="FLATTEXTBOX"
                                                                        TabIndex="1"></asp:TextBox>
                                                                    <asp:ImageButton ID="BtnBeginDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                                                        Width="20px" />
                                                                </td>
                                                                <td nowrap align="left" width="1%" colspan="1">
                                                                </td>
                                                                <td align="left" nowrap valign="baseline" colspan="2">
                                                                    *
                                                                    <asp:Label ID="moEndDateLabel" runat="server" >END_DATE</asp:Label>:
                                                                    
                                                                    <asp:TextBox ID="moEndDateText" runat="server"  CssClass="FLATTEXTBOX"
                                                                        TabIndex="1"></asp:TextBox>
                                                                    <asp:ImageButton ID="BtnEndDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                                                        Width="20px" />
                                                                </td>
                                                            </tr>
                                                          <tr>
                                                                <td colspan="4" height="10px" >                                                                 
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap align="left" width="1%" colspan="1">
                                                                </td>
                                                                <td  align="left" nowrap colspan="1">&nbsp;<asp:RadioButton ID="rSalesDate" runat="server" AutoPostBack="false" groupname ="Dates"
                                                                        Text="WARR_SALES_DATE" TextAlign="left"></asp:RadioButton>
                                                                </td>
                                                                <td nowrap align="left" width="1%" colspan="1">
                                                                </td>
                                                                <td align="left" nowrap colspan="2">
                                                                    <asp:RadioButton ID="rDateAdded" runat="server" AutoPostBack="false" groupname ="Dates"
                                                                        Text="DATE_ADDED" TextAlign="left"></asp:RadioButton>
                                                                 </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5" height="10px">
                                                                    <hr style="width: 100%; height: 1px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap align="left" width="1%" colspan="1">
                                                                </td>
                                                                <td nowrap valign="baseline" colspan="4" align="left" width="60%">
                                                                    <uc1:MultipleColumnDDLabelControl ID="moUserCompanyMultipleDrop" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td id="ddSeparator" align="center" colspan="5" width="100%">
                                                                    <hr style="width: 100%; height: 1px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap align="center" colspan="5">
                                                                    <table id="Table1" cellspacing="2" cellpadding="0" width="75%" border="0">
                                                                        <tr>
                                                                            <td nowrap align="center" colspan="5">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td nowrap align="left" width="1%" colspan="1">
                                                                            </td>
                                                                            <td style="height: 12px" nowrap align="right" colspan="1" valign="bottom">
                                                                                *
                                                                                <asp:RadioButton ID="rdealer" onclick=" document.all.item('moUserDealerMultipleDrop_lb_DropDown').style.color = ''; ToggleExt(this, arrDealerGroupCtr); fncEnable1(2); "
                                                                                    AutoPostBack="false" type="radio" GroupName="Dealer" Text="SELECT_ALL_DEALERS"
                                                                                    TextAlign="left" runat="server" Checked="False"></asp:RadioButton>
                                                                                <td nowrap align="left" style="height: 12px" colspan="3" width="60%" >
                                                                                    <uc1:MultipleColumnDDLabelControl ID="moUserDealerMultipleDrop" runat="server"></uc1:MultipleColumnDDLabelControl>
                                                                                </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5" height="10px" >                                                                    
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="1%">
                                                                </td>
                                                                <td align="left">
                                                                    <asp:RadioButton ID="rGroup" onclick="ToggleExt(this, arrDealerGroupCtr); fncEnable1(1);"
                                                                        Width="197px" AutoPostBack="false" type="radio"  Text="SELECT_ALL_GROUPS"
                                                                        TextAlign="left" runat="server" GroupName="Dealer" Checked="False"></asp:RadioButton>
                                                                </td>
                                                                <td nowrap align="left" width="1%" colspan="1">
                                                                </td>
                                                                <td nowrap align="left" colspan="2">
                                                                    <asp:Label ID="GroupLabel" runat="server">OR_A_SINGLE_GROUP</asp:Label>:
                                                                    <asp:DropDownList ID="cboDealerGroup" runat="server" Width="190px" type="DropDown" AutoPostBack="false"
                                                                        onchange="ToggleExt(this, arrDealerGroupCtr); fncEnable1(2); document.all.item('moUserDealerMultipleDrop_lb_DropDown').style.color = '';">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5" height="10px">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap align="left" width="1%" colspan="1">
                                                                </td>
                                                                <td  align="left" nowrap colspan="1">&nbsp;<asp:RadioButton ID="rIncludeDealer" runat="server" autopostback ="false" 
                                                                        Text="INCLUDE_OTHER_SINGLE_DEALERS" TextAlign="left" groupname ="Dealer1"></asp:RadioButton>
                                                                </td>
                                                                <td nowrap align="left" width="1%" colspan="1">
                                                                </td>
                                                                <td align="left" nowrap colspan="2">
                                                                    <asp:RadioButton ID="rExcludeDealer" runat="server" autopostback ="false" 
                                                                        Text="EXCLUDE_OTHER_DEALERS" TextAlign="left" groupname ="Dealer1"></asp:RadioButton>
                                                                 </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="5" >
                                                                    <hr style="width: 100%; height: 1px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="1%">
                                                                </td>
                                                                <td align="left" nowrap colspan="1" valign="top">
                                                                    *
                                                                    <asp:Label ID="lbldisplay" runat="server">DISPLAY</asp:Label>&nbsp;
                                                                </td>
                                                                <td nowrap align="left" width="1%" colspan="1">
                                                                </td>
                                                                <td align="left" nowrap colspan="2">
                                                                    <asp:CheckBoxList ID="chkdisplay" runat="server" RepeatDirection="VERTICAL">
                                                                        <asp:ListItem Value="PRODUCT_GROUP" >PRODUCT_GROUP</asp:ListItem>
                                                                        <asp:ListItem Value="BRANCH_NAME">BRANCH</asp:ListItem>
                                                                        <asp:ListItem Value="RISK_TYPE">RISK_TYPE</asp:ListItem>
                                                                        <asp:ListItem Value="PRODUCT_CODE">PRODUCT_CODE</asp:ListItem>
                                                                    </asp:CheckBoxList>
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
	   	
   function fncEnable1(chknum)
   {
            if(chknum == 1)  // for all Dealer Group
            {
              //  alert(document.getElementById('rIncludeDealer').disabled );
                document.getElementById('rIncludeDealer').disabled = false;
                document.getElementById('rIncludeDealer').parentElement.removeAttribute('disabled');                
                document.getElementById('rExcludeDealer').disabled = false;      
                document.getElementById('rExcludeDealer').parentElement.removeAttribute('disabled');
            } 
            else
            {
            // alert(chknum);
                document.getElementById('rIncludeDealer').checked = false;
                document.getElementById('rIncludeDealer').disabled = true;
                document.getElementById('rIncludeDealer').parentElement.setAttribute('disabled','true');
                document.getElementById('rExcludeDealer').checked = false;
                document.getElementById('rExcludeDealer').disabled = true;      
                document.getElementById('rExcludeDealer').parentElement.setAttribute('disabled','true');
            }           
   }
   </script>


  
</body>
</html>
