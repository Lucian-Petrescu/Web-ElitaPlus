<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DealerProductSalesReportForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.DealerProductSalesReportForm" %>

<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title id="rptWindowTitle" runat="server">DEALER_PRODUCT_SALES</title>
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
                            <asp:Label ID="Label7" runat="server"  CssClass="TITLELABELTEXT">DEALER_PRODUCT_SALES</asp:Label>
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
                                        <td align="center" colspan="3" valign="top">
                                            <table border="0" cellpadding="0" cellspacing="1" width="75%">
                                                <tr>
                                                    <td colspan="4" align="center">
                                                        <table id="Table2" border="0" cellpadding="1" cellspacing="1" width="75%">
                                                            <tr>
                                                                <td id="Td1" align="center" colspan="4">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap align="left" width="10%" colspan="1">
                                                                </td>
                                                                <td align="left" nowrap valign="baseline" colspan="1">
                                                                    *
                                                                    <asp:Label ID="moBeginDateLabel" runat="server" >BEGIN_DATE</asp:Label>:
                                                                    &nbsp;
                                                                    <asp:TextBox ID="moBeginDateText" runat="server" CssClass="FLATTEXTBOX"
                                                                        TabIndex="1"></asp:TextBox>
                                                                    <asp:ImageButton ID="BtnBeginDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                                                        Width="20px" />
                                                                </td>
                                                                <td align="left" nowrap valign="baseline" colspan="2">
                                                                    *
                                                                    <asp:Label ID="moEndDateLabel" runat="server" >END_DATE</asp:Label>:
                                                                    &nbsp;
                                                                    <asp:TextBox ID="moEndDateText" runat="server"  CssClass="FLATTEXTBOX"
                                                                        TabIndex="1"></asp:TextBox>
                                                                    <asp:ImageButton ID="BtnEndDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                                                        Width="20px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4" height="20px">
                                                                    <hr style="width: 100%; height: 1px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td nowrap align="left" width="10%" colspan="1">
                                                                </td>
                                                                <td nowrap valign="baseline" colspan="3" align="left" width="60%">
                                                                    <uc1:MultipleColumnDDLabelControl ID="moUserCompanyMultipleDrop" runat="server" />
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
                                                                <td align="left" valign="bottom" nowrap colspan="3">
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
                                                            <tr>
                                                                <td width="10%">
                                                                </td>
                                                                <td align="left" nowrap colspan="1">
                                                                    *
                                                                    <asp:RadioButton ID="RadiobuttonTotalsOnly" runat="server" AutoPostBack="false" onclick="toggleDetailSelection(false);"
                                                                        Text="SHOW TOTALS ONLY" TextAlign="left"></asp:RadioButton>
                                                                </td>
                                                                <td align="left" nowrap colspan="2">
                                                                    <asp:RadioButton ID="RadiobuttonDetail" runat="server" AutoPostBack="false" onclick="toggleDetailSelection(true);"
                                                                        Text="OR SHOW DETAIL WITH TOTALS" TextAlign="left"></asp:RadioButton>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4" height="20px">
                                                                    <hr style="width: 100%; height: 1px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td width="10%">
                                                                </td>
                                                                <td align="left" nowrap colspan="1" valign="top">
                                                                    *
                                                                    <asp:Label ID="moSortByLabel" runat="server">SELECT_REPORT_SORT_ORDER</asp:Label>:&nbsp;
                                                                </td>
                                                                <td align="left" nowrap colspan="2">
                                                                    <asp:RadioButtonList ID="rdReportSortOrder" runat="server" RepeatDirection="VERTICAL">
                                                                        <asp:ListItem Value="1" Selected="True">Branch_Code</asp:ListItem>
                                                                        <asp:ListItem Value="2">WARR_SALES_DATE</asp:ListItem>
                                                                        <asp:ListItem Value="3">Dealer_product_code</asp:ListItem>
                                                                    </asp:RadioButtonList>
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
                            <td style="height: 85%; width: 100%;">
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

</body>
</html>
