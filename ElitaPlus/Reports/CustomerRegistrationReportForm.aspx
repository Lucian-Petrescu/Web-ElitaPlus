<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CustomerRegistrationReportForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.CustomerRegistrationReportForm" %>

<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Customer Registration</title>
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
                                <asp:Label ID="LabelReports" runat="server" CssClass="TITLELABEL">Reports</asp:Label>:
                                <asp:Label ID="Label7" runat="server"  CssClass="TITLELABELTEXT">Customer_Registration</asp:Label></td>
                            <td height="20" align="right">
                                *&nbsp;
                                <asp:Label ID="moIndicatesLabel" runat="server"  EnableViewState="true">INDICATES_REQUIRED_FIELDS</asp:Label></td>
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
                                                    border-left: #999999 1px solid; width: 628px; border-bottom: #999999 1px solid;
                                                    height: 64px" cellspacing="2" cellpadding="8" rules="cols" width="628" align="center"
                                                    bgcolor="#fef9ea" border="0">
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
                                                <img height="15" src="../Navigation/images/trans_spacer.gif"></td>
                                        </tr>
                                        <tr>
                                            <td style="width: 99.43%; height: 1px" colspan="3">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="3">
                                                <table cellspacing="2" cellpadding="0" width="95%" border="0">
                                                    <tr>
                                                        <td style="height: 10px" valign="middle" align="center" width="50%" colspan="3" rowspan="1">
                                                            <table style="width: 484px; height: 18px" cellspacing="0" cellpadding="0" width="484"
                                                                border="0">
                                                                <tr>
                                                                    <td valign="middle" nowrap align="right">
                                                                        *
                                                                        <asp:Label ID="moBeginDateLabel" runat="server" >BEGIN_DATE</asp:Label>:</td>
                                                                    <td style="width: 110px" nowrap>
                                                                        &nbsp;
                                                                        <asp:TextBox ID="moBeginDateText" TabIndex="1" runat="server" Width="95px" CssClass="FLATTEXTBOX"></asp:TextBox></td>
                                                                    <td style="width: 15px" valign="middle" align="center">
                                                                        <asp:ImageButton ID="BtnBeginDate" runat="server" Width="20px" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                                                            Height="17px"></asp:ImageButton></td>
                                                                    <td valign="middle" nowrap align="right" width="15" colspan="1" rowspan="1">
                                                                    </td>
                                                                    <td valign="middle" nowrap align="right">
                                                                        *
                                                                        <asp:Label ID="moEndDateLabel" runat="server" >END_DATE</asp:Label>:</td>
                                                                    <td style="width: 110px" nowrap>
                                                                        &nbsp;
                                                                        <asp:TextBox ID="moEndDateText" TabIndex="1" runat="server" Width="95px" CssClass="FLATTEXTBOX"></asp:TextBox></td>
                                                                    <td style="width: 15px" valign="middle" align="center">
                                                                        <asp:ImageButton ID="BtnEndDate" runat="server" Width="20px" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                                                            Height="17px"></asp:ImageButton></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 94px" align="center" colspan="3">
                                                            <hr style="width: 99.43%; height: 1px">
                                                            <table id="Table2" cellspacing="1" cellpadding="1" width="100%" border="0">
                                                                <tr>
                                                                    <td valign="bottom" align="right" width="23%">
                                                                        *
                                                                        <asp:RadioButton ID="rdealer" onclick="ToggleDualDropDownsSelection('multipleDropControl_moMultipleColumnDrop', 'multipleDropControl_moMultipleColumnDropDesc', 'rdealer', false, ''); document.all.item('multipleDropControl_lb_DropDown').style.color = '';"
                                                                            Text="SELECT_ALL_DEALERS" AutoPostBack="false" Checked="False" runat="server"
                                                                            TextAlign="left"></asp:RadioButton>&nbsp;
                                                                    </td>
                                                                    <td valign="baseline" nowrap width="75%">
                                                                        <uc1:MultipleColumnDDLabelControl ID="multipleDropControl" runat="server"></uc1:MultipleColumnDDLabelControl>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 20px" colspan="2">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td nowrap align="right" width="23%">
                                                                        *
                                                                        <asp:RadioButton ID="rproductcode" onclick="toggleAllProductSelection(false);" Text="SELECT_ALL_PRODUCT_CODES"
                                                                            AutoPostBack="false" runat="server" TextAlign="left"></asp:RadioButton>&nbsp;
                                                                    </td>
                                                                    <td nowrap align="left" width="75%">
                                                                        <asp:Label ID="lblProductcode" runat="server">OR A SINGLE PRODUCT CODE</asp:Label>
                                                                        <asp:TextBox ID="txtProductcode" runat="server" OnFocus="toggleAllProductSelection(true);"
                                                                            AutoPostBack="False"></asp:TextBox></td>
                                                                </tr>
                                                                <tr>
                                                                    <td style="height: 20px" colspan="2">
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td nowrap align="right" width="23%">
                                                                        *
                                                                        <asp:RadioButton ID="rrisktype" onclick="toggleAllRiskTypeSelection(false);" Text="SELECT_ALL_RISK_TYPE"
                                                                            AutoPostBack="false" runat="server" TextAlign="left"></asp:RadioButton>&nbsp;
                                                                    </td>
                                                                    <td nowrap align="left" width="75%">
                                                                        <asp:Label ID="lblrisktype" runat="server">OR A SINGLE RISK TYPE</asp:Label>
                                                                        <asp:DropDownList ID="cborisktype" runat="server"  onchange="toggleAllRiskTypeSelection(true);"
                                                                            AutoPostBack="False">
                                                                        </asp:DropDownList></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 27px" colspan="3">
                                                            <hr style="width: 99.43%; height: 1px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td nowrap align="right" width="25%">
                                                            *
                                                            <asp:RadioButton ID="RadiobuttonTotalsOnly" onclick="toggleDetailSelection(false);"
                                                                Text="SHOW TOTALS ONLY" AutoPostBack="false" runat="server" TextAlign="left"></asp:RadioButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                        <td align="left" width="2%">
                                                        </td>
                                                        <td nowrap align="left" width="25%">
                                                            <asp:RadioButton ID="RadiobuttonDetail" onclick="toggleDetailSelection(true);" Text="OR SHOW DETAIL WITH TOTALS"
                                                                AutoPostBack="false" runat="server" TextAlign="left"></asp:RadioButton></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 20px">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <hr>
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Button ID="btnGenRpt" Style="background-image: url(../Navigation/images/viewIcon2.gif);
                                        cursor: hand; background-repeat: no-repeat" runat="server" 
                                        CssClass="FLATBUTTON" Width="100px" Text="View" Height="20px"></asp:Button></td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </form>

    <script language="JavaScript" src="../Navigation/Scripts/ReportCeMainScripts.js"></script>

    <script>
	function toggleAllProductSelection(isSingleProductCode)
   {
		//debugger;
		if(isSingleProductCode)
		{
			document.forms[0].rproductcode.checked = false;
		}
		else
		{
			document.forms[0].txtProductcode.value = "";
		}
	}	
	function toggleAllRiskTypeSelection(isSinglerisktype)
   {
		//debugger;
		if(isSinglerisktype)
		{
			document.forms[0].rrisktype.checked = false;
		}
		else
		{
			document.forms[0].cborisktype.selectedIndex = -1;
		}
	}	
		
    </script>

</body>
</html>
