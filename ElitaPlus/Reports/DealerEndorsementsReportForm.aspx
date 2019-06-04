<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DealerEndorsementsReportForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.DealerEndorsementsReportForm" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Dealer Endorsements</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
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
                            <asp:Label ID="LabelReports" runat="server" CssClass="TITLELABEL">Reports</asp:Label>:<asp:Label
                                ID="Label7" runat="server" CssClass="TITLELABELTEXT">DEALER_ENDORSEMENTS</asp:Label>
                        </td>
                        <td align="right" height="20">
                            *
                            <asp:Label ID="moIndicatesLabel" runat="server" Font-Bold="false" EnableViewState="true">INDICATES_REQUIRED_FIELDS</asp:Label>
                        </td>
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
                &nbsp;
            </td>
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
                                            <table cellspacing="0" cellpadding="0" width="75%" border="0">
                                                <tr>
                                                    <td style="height: 72px" align="right" width="100%" colspan="4">
                                                        <table id="Table2" cellspacing="1" cellpadding="1" align="center" border="0">
                                                            <tr>
                                                                <td align="right">
                                                                </td>
                                                                <td style="width: 274px" align="right">
                                                                    <asp:Label ID="Label8" runat="server" Width="2px">MONTH</asp:Label>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:Label ID="Label9" runat="server" Width="2px">YEAR</asp:Label>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="right" colspan="2">
                                                                    *
                                                                    <asp:Label ID="MonthYearLabel" runat="server" Font-Bold="false">SELECT_MONTH_AND_YEAR</asp:Label>
                                                                    <asp:DropDownList ID="MonthDropDownList" runat="server" Width="128px" AutoPostBack="false">
                                                                    </asp:DropDownList>
                                                                </td>
                                                                <td align="right">
                                                                    <asp:DropDownList ID="YearDropDownList" runat="server" Width="84px" AutoPostBack="false">
                                                                    </asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="border-top: gray thin solid; border-bottom: gray thin" nowrap align="right"
                                                        width="100%" colspan="4">
                                                        <table id="Table1" cellspacing="2" cellpadding="0" width="75%" align="center" border="0">
                                                            <tr>
                                                                <td style="height: 1px" nowrap align="right" width="100%">
                                                                </td>
                                                                <td style="height: 1px" nowrap align="left" width="25%">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 18px" nowrap align="right" width="100%">
                                                                    *
                                                                    <asp:RadioButton ID="rEndorsementNumber" onclick="toggleAllEndorsementSelection(false); document.all.item('moEndorsmentNumberLabel').style.color = '';"
                                                                        AutoPostBack="false" Text="PLEASE_SELECT_ALL_ENDORSEMENTS_NUMBERS" TextAlign="left"
                                                                        runat="server" Checked="False"></asp:RadioButton>
                                                                </td>
                                                                <td style="height: 18px" nowrap align="left" width="25%">
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                    <asp:Label ID="moEndorsmentNumberLabel" runat="server">OR_A_SINGLE_ENDORSEMENT_NUMBER</asp:Label>&nbsp;&nbsp;
                                                                    <asp:TextBox ID="txtEndorsmentNumber" onclick="toggleAllEndorsementSelection(true); document.all.item('moEndorsmentNumberLabel').style.color = '';"
                                                                        runat="server" AutoPostBack="false"></asp:TextBox>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 2px" nowrap align="right" width="100%">
                                                                </td>
                                                                <td style="height: 2px" nowrap align="left" width="25%">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="border-top: gray thin solid; border-bottom: gray thin" nowrap align="center"
                                                        width="100%" colspan="4">
                                                        <table id="Table1" cellspacing="2" cellpadding="0" width="75%" border="0">
                                                            <tr>
                                                                <td nowrap align="center" colspan="2">
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td valign="bottom" nowrap align="right">
                                                                    *
                                                                    <asp:RadioButton ID="rdealer" onclick="ToggleDualDropDownsSelection('multipleDropControl_moMultipleColumnDrop', 'multipleDropControl_moMultipleColumnDropDesc', 'rdealer', false, ''); document.all.item('multipleDropControl_lb_DropDown').style.color = '';"
                                                                        AutoPostBack="false" Text="SELECT_ALL_DEALERS" TextAlign="left" runat="server"
                                                                        Checked="False"></asp:RadioButton>
                                                                </td>
                                                                <td nowrap align="left">
                                                                    <uc1:MultipleColumnDDLabelControl ID="multipleDropControl" runat="server"></uc1:MultipleColumnDDLabelControl>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100%" nowrap align="right" width="100%">
                                                    </td>
                                                    <td style="width: 100%" nowrap align="right" width="100%">
                                                    </td>
                                                    <td nowrap align="left" width="25%" colspan="2">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 100%" colspan="4">
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 11px">
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
                                    cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                    Width="100px" Text="View" CssClass="FLATBUTTON" Height="20px"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    </form>

    <script language="JavaScript" src="../Navigation/Scripts/ReportCeMainScripts.js"></script>

    <script>
		function toggleAllEndorsementSelection(isSingleEndorsement)
		{
			//debugger;
			if(isSingleEndorsement)
			{
				document.forms[0].rEndorsementNumber.checked = false;
			}
			else
			{
				document.forms[0].txtEndorsmentNumber.value = "";
			}
		}
    </script>

</body>
</html>
