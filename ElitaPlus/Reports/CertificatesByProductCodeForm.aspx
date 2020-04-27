<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CertificatesByProductCodeForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.CertificatesByProductCodeForm" %>

<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Certificate By Product Code</title>
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
        <table style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid; height: 22px"
            cellspacing="0" cellpadding="0" width="98%" bgcolor="#d5d6e4" border="0">
            <tr>
                <td valign="top">
                    <table width="100%" border="0">
                        <tr>
                            <td height="20">
                                <asp:Label ID="LabelReports" runat="server" CssClass="TITLELABEL">Reports</asp:Label>:
									<asp:Label ID="Label7" runat="server" CssClass="TITLELABELTEXT">CERTIFICATES BY PRODUCT CODE REPORT CRITERIA</asp:Label></td>
                            <td align="right" height="20">*&nbsp;
									<asp:Label ID="moIndicatesLabel" runat="server" EnableViewState="true">INDICATES_REQUIRED_FIELDS</asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table id="tblOuter2" style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid"
            height="93%" cellspacing="0" cellpadding="0" rules="none" width="98%" bgcolor="#d5d6e4"
            border="0">
            <!--d5d6e4-->
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td valign="top" align="center" height="100%">
                    <asp:Panel ID="WorkingPanel" runat="server">
                        <table id="tblMain1" style="border-right: #999999 1px solid; border-top: #999999 1px solid; border-left: #999999 1px solid; border-bottom: #999999 1px solid"
                            cellspacing="0" cellpadding="6" rules="cols" width="98%" align="center" bgcolor="#fef9ea"
                            border="0">
                            <tr>
                                <td valign="top" height="1"></td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td colspan="3">
                                                <table id="tblSearch" style="border-right: #999999 1px solid; border-top: #999999 1px solid; border-left: #999999 1px solid; width: 100%; border-bottom: #999999 1px solid; height: 64px"
                                                    cellspacing="2" cellpadding="8" rules="cols" width="100%" align="center" bgcolor="#fef9ea"
                                                    border="0">
                                                    <tr>
                                                        <td>
                                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                <tr>
                                                                    <td nowrap align="center">
                                                                        <uc1:ErrorController ID="ErrorCtrl" runat="server"></uc1:ErrorController>
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
                                            <td align="center" colspan="3">
                                                <table cellspacing="2" cellpadding="0" width="75%" border="0">
                                                    <tr>
                                                        <td colspan="3"></td>
                                                    </tr>
                                                    <tr>
                                                        <td valign="bottom" nowrap align="right">*
																<asp:RadioButton ID="rdealer" onclick="ToggleDualDropDownsSelection('multipleDropControl_moMultipleColumnDrop', 'multipleDropControl_moMultipleColumnDropDesc', 'rdealer', false, ''); document.all.item('multipleDropControl_lb_DropDown').style.color = '';"
                                                                    TextAlign="left" Text="SELECT_ALL_DEALERS" runat="server" Checked="False" AutoPostBack="false"></asp:RadioButton>&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                        <td style="width: 20px; height: 18px" width="20"></td>
                                                        <td nowrap align="left">
                                                            <uc1:MultipleColumnDDLabelControl ID="multipleDropControl" runat="server"></uc1:MultipleColumnDDLabelControl>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 18px" nowrap align="center" width="50%" colspan="3">
                                                            <hr style="width: 95%; height: 1px">
                                                            &nbsp;</td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 18px" nowrap align="center" width="50%" colspan="3">
                                                            <table id="Table3" cellspacing="1" cellpadding="1" width="90%" border="0">
                                                                <tr>
                                                                    <td align="right">*
																			<asp:RadioButton ID="rbProduct" onclick="toggleAllProductsSelection(false); document.all.item('moProductLabel').style.color = '';"
                                                                                AutoPostBack="false" Checked="False" runat="server" Text="SELECT_ALL_PRODUCT_CODES" TextAlign="left"></asp:RadioButton></td>
                                                                    <td style="width: 20px; height: 18px"></td>
                                                                    <td>
                                                                        <asp:Label ID="moProductLabel" runat="server">OR A SINGLE PRODUCT CODE</asp:Label>:
																			<asp:DropDownList ID="cboProduct" runat="server" AutoPostBack="false" onchange="toggleAllProductsSelection(true); document.all.item('moProductLabel').style.color = '';"
                                                                                Width="212px">
                                                                            </asp:DropDownList></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 14px" colspan="3">
                                                            <hr style="width: 95%; height: 1px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 18px" nowrap align="center" width="50%" colspan="3">
                                                            <table id="Table4" cellspacing="1" cellpadding="1" width="90%" border="0">
                                                                <tr>
                                                                    <td align="right">*
																			<asp:RadioButton ID="rbCampaignNumber" onclick="toggleAllCampainNoSelection(false); document.all.item('moCampaignNumberLabel').style.color = '';"
                                                                                AutoPostBack="false" Checked="False" runat="server" Text="SELECT_ALL_CAMPAIGN_NUMBERS" TextAlign="left"></asp:RadioButton></td>
                                                                    <td style="width: 20px; height: 18px"></td>
                                                                    <td>
                                                                        <asp:Label ID="moCampaignNumberLabel" runat="server">OR A SINGLE CAMPAIGN NUMBER</asp:Label>:
																			<asp:DropDownList ID="cboCampaignNumber" runat="server" AutoPostBack="false" onchange="toggleAllCampainNoSelection(true); document.all.item('moCampaignNumberLabel').style.color = '';"
                                                                                Width="212px">
                                                                            </asp:DropDownList></td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 14px" colspan="3">
                                                            <hr style="width: 95%; height: 1px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 14px" colspan="3">
                                                            <table id="Table1" cellspacing="1" cellpadding="1" width="90%" align="center" border="0">
                                                                <tr>
                                                                    <td valign="top">
                                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                            <tbody>
                                                                                <tr>
                                                                                    <!--<TD style="WIDTH: 274px"><IMG height="7" src="../images/trans_spacer.gif" width="1"></TD>-->
                                                                                    <td>
                                                                                        <img height="7" src="../Navigation/images/trans_spacer.gif" width="1"></td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td nowrap align="right"></td>
                                                                                    <td nowrap align="right">*
																							<asp:Label ID="Label5" runat="server">SELECT_REPORT_SORT_ORDER</asp:Label>:</td>
                                                                                </tr></table>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                        <td>
                                                            <asp:RadioButtonList ID="rdReportSortOrder" runat="server" Width="224px" Height="40px" RepeatDirection="VERTICAL">
                                                                <asp:ListItem Value="0" Selected="True">DEALER CODE</asp:ListItem>
                                                                <asp:ListItem Value="1">PRODUCT CODE</asp:ListItem>
                                                            </asp:RadioButtonList></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top" nowrap align="left"></td>
                                            <td style="width: 20px" valign="top"></td>
                                            <td valign="top">
                                            <tr>
                                                <td style="height: 25px" colspan="3">
                                                    <hr style="width: 95%; height: 1px">
                                                </td>
                                            </tr>
                                        <tr>
                                            <td style="height: 25px" align="center" colspan="3">
                                                <table id="Table2" cellspacing="1" cellpadding="1" width="50%" border="0">
                                                    <tr>
                                                        <td>*
																<asp:RadioButton ID="RadiobuttonTotalsOnly" onclick="toggleDetailSelection(false);" AutoPostBack="false"
                                                                    runat="server" Text="SHOW TOTALS ONLY" TextAlign="left"></asp:RadioButton></td>
                                                        <td>
                                                            <asp:RadioButton ID="RadiobuttonDetail" onclick="toggleDetailSelection(true);" AutoPostBack="false"
                                                                runat="server" Text="OR SHOW DETAIL WITH TOTALS" TextAlign="left"></asp:RadioButton></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:panel>
                </td>
            </tr>
            <tr>
                <td style="height: 24px"></td>
            </tr>
            <tr>
                <td>
                    <hr style="height: 1px">
                </td>
            </tr>
            <tr>
                <td align="left">&nbsp;
						<asp:Button ID="btnGenRpt" Style="background-image: url(../Navigation/images/viewIcon2.gif); cursor: hand; background-repeat: no-repeat"
                            runat="server" Text="View" Width="100px" Height="20px" CssClass="FLATBUTTON"></asp:Button></td>
            </tr>
        </table>
      
    </form>
    <script language="JavaScript" src="../Navigation/Scripts/ReportCeMainScripts.js"></script>

</body>
</html>

