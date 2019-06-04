<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" Codebehind="UnearnedPremiumReservesForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.UnearnedPremiumReservesForm" %>

<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title id="rptWindowTitle" runat="server">Unearned Premiums Reserves</title>
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
                                <asp:Label ID="LabelReports"  CssClass="TITLELABEL" runat="server">Reports</asp:Label>:&nbsp;<asp:Label
                                    ID="Label7" runat="server"  CssClass="TITLELABELTEXT">Unearned Premiums Reserves</asp:Label></td>
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
                <td valign="top" align="center" height="100%">
                    <asp:Panel ID="WorkingPanel" runat="server">
                        <table id="tblMain1" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                            border-left: #999999 1px solid; border-bottom: #999999 1px solid" cellspacing="0"
                            cellpadding="6" rules="cols" width="98%" align="center" bgcolor="#fef9ea" border="0" height = "95%">
                            <tr>
                                <td height="1">
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0"  height = "95%">
                                        <tr>
                                            <td colspan="3">
                                                <table id="tblSearch" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                                                    border-left: #999999 1px solid; width: 100%; border-bottom: #999999 1px solid;
                                                    height: 76px" cellspacing="2" cellpadding="8" rules="cols" width="100%" align="center"
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
                                            <td align="center" colspan="3">
                                                <table style="width: 706px; height: 50px" cellspacing="2" cellpadding="0" width="706"
                                                    border="0">
                                                    <tr>
                                                        <td style="width: 100%" align="center" colspan="2">
                                                            <asp:Label ID="MonthYearLabel" runat="server" >SELECT MONTH AND YEAR</asp:Label>:
                                                            <asp:DropDownList ID="MonthDropDownList" runat="server" Width="128px" AutoPostBack="false">
                                                            </asp:DropDownList>&nbsp;
                                                            <asp:DropDownList ID="YearDropDownList" runat="server" Width="84px" AutoPostBack="false">
                                                            </asp:DropDownList></td>
                                                    </tr>
                                                    <tr>
                                                        <td id="ddSeparator" align="center" width="100%" colspan="2">
                                                            <hr style="width: 100%; height: 1px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 24px" valign="middle" align="center" colspan="2">
                                                            <uc1:MultipleColumnDDLabelControl ID="moUserCompanyMultipleDrop" runat="server"></uc1:MultipleColumnDDLabelControl>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" width="100%" colspan="2">
                                                            <hr id="ddSeparator1" style="width: 100%; height: 1px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td nowrap align="right" width="35%">
                                                            <asp:RadioButton ID="rdealer" onclick="toggleAllDealersSelection(false,'DealerLabel');"
                                                                AutoPostBack="false" Text="SELECT_ALL_DEALERS" TextAlign="left" runat="server"
                                                                Checked="False"></asp:RadioButton>&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                        <td nowrap align="left" width="65%">
                                                            &nbsp;
                                                            <asp:Label ID="DealerLabel" runat="server">OR A SINGLE DEALER</asp:Label>:&nbsp;
                                                            <asp:DropDownList ID="cboDealer" runat="server" AutoPostBack="false" onchange="toggleAllDealersSelection(true);"
                                                                Width="267px">
                                                            </asp:DropDownList></td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" width="100%" colspan="2">
                                                            <hr style="width: 100%; height: 1px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td nowrap align="right" width="35%">
                                                            <asp:RadioButton ID="RadiobuttonTotalsOnly" onclick="toggleDetailSelection(false);"
                                                                AutoPostBack="false" Text="SHOW TOTALS ONLY" TextAlign="left" runat="server"></asp:RadioButton>&nbsp;&nbsp;&nbsp;
                                                        </td>
                                                        <td nowrap align="left" width="65%">
                                                            &nbsp;
                                                            <asp:RadioButton ID="RadiobuttonDetail" onclick="toggleDetailSelection(true);" AutoPostBack="false"
                                                                Text="OR SHOW DETAIL WITH TOTALS" TextAlign="left" runat="server"></asp:RadioButton></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                           <TR>
									<TD style="HEIGHT: 85%; width: 100%;"></TD>
								</TR>
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
                                        Width="100px" Text="View" CssClass="FLATBUTTON" Height="20px"></asp:Button></td>
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
