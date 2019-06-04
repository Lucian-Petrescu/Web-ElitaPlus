<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ClaimFollowUpReportForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.ClaimFollowUpReportForm" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title id="rptWindowTitle" runat="server">ClaimFollowUp</title>
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
                            <asp:Label ID="LabelReports" CssClass="TITLELABEL" runat="server">Reports</asp:Label>:&nbsp;<asp:Label
                                ID="Label7" runat="server" CssClass="TITLELABELTEXT">Claim_Follow_Up</asp:Label>
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
                                        <td colspan="3">
                                            <img height="15" src="../Navigation/images/trans_spacer.gif">
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
                                                                    &nbsp;
                                                                </td>
                                                                <td valign="middle" nowrap align="right">
                                                                    <asp:Label ID="moBeginFollowUpDateLabel" runat="server">BEGIN_FOLLOW_UP_DATE</asp:Label>:
                                                                </td>
                                                                <td nowrap>
                                                                    &nbsp;
                                                                    <asp:TextBox ID="moBeginFollowUpDateText" TabIndex="1" runat="server" CssClass="FLATTEXTBOX"></asp:TextBox>&nbsp;
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton ID="BtnBeginFollowUpDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                                                                    </asp:ImageButton>&nbsp;&nbsp;
                                                                </td>
                                                                <td valign="middle" nowrap align="right">
                                                                    &nbsp;&nbsp;
                                                                    <asp:Label ID="moEndFollowUpDateLabel" runat="server">END_FOLLOW_UP_DATE</asp:Label>:
                                                                </td>
                                                                <td nowrap>
                                                                    &nbsp;
                                                                    <asp:TextBox ID="moEndFollowUpDateText" TabIndex="1" runat="server" CssClass="FLATTEXTBOX"
                                                                        Width="125px"></asp:TextBox>&nbsp;
                                                                </td>
                                                                <td>
                                                                    <asp:ImageButton ID="BtnEndFollowUpDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                                                                    </asp:ImageButton>
                                                                </td>
                                                                <td width="50%">
                                                                    &nbsp;
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <hr style="height: 1px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td nowrap align="right" width="25%" colspan="2">
                                                        <asp:RadioButton ID="rDealer" onclick="toggleAllDealersSelection(false);" AutoPostBack="false"
                                                            runat="server" Text="SELECT_ALL_DEALERS" TextAlign="left" Checked="False"></asp:RadioButton>
                                                    </td>
                                                    <td nowrap align="left" width="25%">
                                                        &nbsp;&nbsp;
                                                        <asp:Label ID="moDealerLabel" runat="server">OR A SINGLE DEALER</asp:Label>&nbsp;&nbsp;
                                                    </td>
                                                    <td nowrap align="left" width="40%">
                                                        <asp:DropDownList ID="cboDealer" runat="server" Width="100%" AutoPostBack="false"
                                                            onchange="toggleAllDealersSelection(true);">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <!--<TR>
															<TD colSpan="4"><IMG height="3" src="../Navigation/images/trans_spacer.gif"></TD>
														</TR>-->
                                                <tr>
                                                    <td colspan="4">
                                                        <hr style="height: 1px">
                                                    </td>
                                                </tr>
                                                <!--<TR>
															<TD colSpan="4"><IMG height="3" src="../Navigation/images/trans_spacer.gif"></TD>
														</TR>-->
                                                <tr>
                                                    <td nowrap align="right" width="25%" colspan="2">
                                                        <asp:RadioButton ID="rSvcCtr" onclick="toggleAllSvcCtrsSelection(false);" AutoPostBack="false"
                                                            runat="server" Text="PLEASE SELECT ALL SERVICE CENTERS" TextAlign="left" Checked="False">
                                                        </asp:RadioButton>
                                                    </td>
                                                    <td nowrap align="left" width="25%">
                                                        &nbsp;&nbsp;
                                                        <asp:Label ID="SvcCtrLabel" runat="server">OR A SINGLE SERVICE CENTER</asp:Label>&nbsp;&nbsp;
                                                    </td>
                                                    <td nowrap align="left" width="40%">
                                                        <asp:DropDownList ID="cboSvcCtr" runat="server" Width="100%" AutoPostBack="false"
                                                            onchange="toggleAllSvcCtrsSelection(true);">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <!--<TR>
															<TD colSpan="4"><IMG height="3" src="../Navigation/images/trans_spacer.gif"></TD>
														</TR>-->
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
                                                                            <td>
                                                                                <img height="7" src="../Navigation/images/trans_spacer.gif" width="1">
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td nowrap align="right">
                                                                                <asp:Label ID="Label5" runat="server">SELECT_REPORT_SORT_ORDER</asp:Label>:
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                                <td valign="top" width="55%" colspan="2">
                                                                    <asp:RadioButtonList ID="rdReportSortOrder" runat="server" RepeatDirection="Vertical">
                                                                        <asp:ListItem Value="1" Selected="True">Claim_Number</asp:ListItem>
                                                                        <asp:ListItem Value="2">Follow_Up_Date</asp:ListItem>
                                                                        <asp:ListItem Value="3">Last_Operator</asp:ListItem>
                                                                        <asp:ListItem Value="4">Date Claim Opened</asp:ListItem>
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
                            <td style="height: 3px">
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
                                    cursor: hand; background-repeat: no-repeat" runat="server" CssClass="FLATBUTTON"
                                    Text="View" Width="100px" Height="20px"></asp:Button>
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
        function toggleAllDealersSelection(isSingleDealer) {
            //debugger;
            if (isSingleDealer) {
                document.forms[0].rDealer.checked = false;
            }
            else {
                document.forms[0].cboDealer.selectedIndex = -1;
            }
        }

        function toggleAllSvcCtrsSelection(isSingleSvcCtr) {
            //debugger;
            if (isSingleSvcCtr) {
                document.forms[0].rSvcCtr.checked = false;
            }
            else {
                document.forms[0].cboSvcCtr.selectedIndex = -1;
            }
        }
    </script>
</body>
</html>
