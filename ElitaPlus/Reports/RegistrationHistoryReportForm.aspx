<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RegistrationHistoryReportForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.RegistrationHistoryReportForm" %>

<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title id="rptWindowTitle" runat="server">REGISTRATION_HISTORY</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Styles.css" type="text/css" rel="STYLESHEET">


    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>

    <style type="text/css">
        .style1 {
            width: 20%;
        }
    </style>

</head>
<body leftmargin="0" topmargin="0" onload="changeScrollbarColor();" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <!--Start Header-->
        <input id="rptTitle" type="hidden" name="rptTitle">
        <input id="rptSrc" type="hidden" name="rptSrc">
        <table style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid"
            cellspacing="0"
            cellpadding="0" width="98%" bgcolor="#d5d6e4" border="0">
            <tr>
                <td valign="top">
                    <table width="100%" border="0">
                        <tr>
                            <td height="20">
                                <asp:Label ID="LabelReports" CssClass="TITLELABEL" runat="server">Reports</asp:Label>:
                            <asp:Label ID="Label7" runat="server" CssClass="TITLELABELTEXT">REGISTRATION_HISTORY</asp:Label>
                            </td>
                            <td height="20" align="right">*&nbsp;
                            <asp:Label ID="moIndicatesLabel" runat="server" EnableViewState="true">INDICATES_REQUIRED_FIELDS</asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table id="tblOuter2" style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid"
            height="93%"
            cellspacing="0" cellpadding="0" rules="none" width="98%" bgcolor="#d5d6e4" border="0">
            <!--d5d6e4-->
            <tr>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td valign="top" align="center" height="100%">
                    <asp:Panel ID="WorkingPanel" runat="server">
                        <table id="tblMain1" style="border-right: #999999 1px solid; border-top: #999999 1px solid; border-left: #999999 1px solid; border-bottom: #999999 1px solid"
                            cellspacing="0"
                            cellpadding="6" rules="cols" width="98%" align="center" bgcolor="#fef9ea" height="95%"
                            border="0">
                            <tr>
                                <td height="1"></td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td colspan="3">
                                                <table id="tblSearch" style="border-right: #999999 1px solid; border-top: #999999 1px solid; border-left: #999999 1px solid; border-bottom: #999999 1px solid; height: 64px"
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
                                                                    <td nowrap align="right">&nbsp;
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
                                            <td style="width: 99.43%; height: 1px"></td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="3" valign="top">
                                                <table border="0" cellpadding="0" cellspacing="2" width="95%">
                                                    <tr>
                                                        <td align="center" colspan="3" rowspan="1" valign="middle" width="50%">
                                                            <table border="0" cellpadding="0" cellspacing="0" width="484">
                                                                <tr>
                                                                    <td align="right" nowrap valign="middle">*
                                                                        <asp:Label ID="moBeginDateLabel" runat="server">BEGIN_DATE</asp:Label>:
                                                                    </td>
                                                                    <td nowrap style="width: 110px">&#160;
                                                                        <asp:TextBox ID="moBeginDateText" runat="server" CssClass="FLATTEXTBOX" TabIndex="1"
                                                                            Width="95px"></asp:TextBox>
                                                                    </td>
                                                                    <td align="center" style="width: 15px" valign="middle">
                                                                        <asp:ImageButton ID="BtnBeginDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                                                            Width="20px"></asp:ImageButton>
                                                                    </td>
                                                                    <td align="right" colspan="1" nowrap rowspan="1" valign="middle" width="15"></td>
                                                                    <td align="right" nowrap valign="middle">*
                                                                        <asp:Label ID="moEndDateLabel" runat="server">END_DATE</asp:Label>:
                                                                    </td>
                                                                    <td nowrap style="width: 110px">&#160;
                                                                        <asp:TextBox ID="moEndDateText" runat="server" CssClass="FLATTEXTBOX" TabIndex="1"
                                                                            Width="95px"></asp:TextBox>
                                                                    </td>
                                                                    <td align="center" style="width: 15px" valign="middle">
                                                                        <asp:ImageButton ID="BtnEndDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                                                            Width="20px"></asp:ImageButton>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td align="center" colspan="3">
                                                            <hr style="width: 99.43%; height: 1px">
                                                            <table id="Table2" border="0" cellpadding="1" cellspacing="1" width="100%">
                                                                <tr>
                                                                    <td colspan="4">
                                                                        <table border="0" cellpadding="1" cellspacing="1" width="100%">
                                                                            <tr>
                                                                                <td align="center" valign="top" colspan="4" nowrap style="width: 100%; height: 0.01%">
                                                                                    <uc1:MultipleColumnDDLabelControl ID="moUserCompanyMultipleDrop" runat="server"></uc1:MultipleColumnDDLabelControl>
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
                                                                    <td align="right" valign="bottom" colspan="1">*
                                                                            <asp:RadioButton ID="rdealer" runat="server" AutoPostBack="false"
                                                                                Checked="False"
                                                                                onclick="ToggleDualDropDownsSelection('moDealerMultipleDrop_moMultipleColumnDrop', 'moDealerMultipleDrop_moMultipleColumnDropDesc', 'rdealer', false, ''); document.all.item('moDealerMultipleDrop_lb_DropDown').style.color = '';"
                                                                                Text="SELECT_ALL_DEALERS" TextAlign="left" />
                                                                        &nbsp;
                                                                    </td>
                                                                    <td nowrap valign="baseline" colspan="3" align="left">
                                                                        <uc1:MultipleColumnDDLabelControl ID="moDealerMultipleDrop" runat="server" />
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4" height="20px"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td nowrap align="right" width="25%" colspan="1"></td>
                                                                    <td nowrap align="left" width="25%"></td>
                                                                    <td nowrap align="left" width="25%" colspan="2"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td colspan="4"></td>
                                                                </tr>
                                                                <tr>
                                                                    <td align="right" colspan="1" nowrap class="style1" colspan="2"></td>
                                                                    <td align="left" nowrap colspan="2"></td>
                                                                </tr>
                                                            </table>
                                                    </tr>
                                                </table>
                                                <hr />
                                            </td>
                                        </tr>

                                        <tr>
                                            <td colspan="3"></td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="3" nowrap width="25%"></td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td style="height: 85%; width: 100%;"></td>
            </tr>
            <tr>
                <td>
                    <hr>
                </td>
            </tr>
            <tr>
                <td align="left">
                    <asp:Button ID="btnGenRpt" Style="background-image: url(../Navigation/images/viewIcon2.gif); cursor: hand; background-repeat: no-repeat"
                        runat="server"
                        CssClass="FLATBUTTON" Width="100px" Text="View" Height="20px"></asp:Button>
                </td>
            </tr>
        </table>
    </form>
    <script language="JavaScript" type="text/javascript" src="../Navigation/Scripts/ReportCeMainScripts.js"></script>

</body>
</html>
