<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PriceGroupsReportForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.PriceGroupsReportForm" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title id="rptWindowTitle" runat="server">Price List</title>
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
                                    ID="Label7" runat="server" CssClass="TITLELABELTEXT">PRICE_LISTS</asp:Label></td>
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
                                            <td colspan="3" style="height: 1px">
                                                <img height="15" src="../Navigation/images/trans_spacer.gif"></td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="3">
                                                <table cellspacing="2" cellpadding="0" width="75%" border="0">                                                   <tr>
                                                        <td align="center" width="50%" colspan="3">
                                                            <table cellspacing="0" cellpadding="0" width="75%" border="0">
                                                                <tr>
                                                                    <td valign="bottom" nowrap align="right">
                                                                        *
                                                                       <asp:RadioButton ID="rPriceLists" onclick="ToggleDualDropDownsSelection('multipleDropControl_moMultipleColumnDrop', 'multipleDropControl_moMultipleColumnDropDesc', 'rPriceGroups', false, ''); document.all.item('multipleDropControl_lb_DropDown').style.color = '';"
                                                                            Text="ALL Price Lists" TextAlign="left" runat="server" Checked="True" AutoPostBack="false">
                                                                       </asp:RadioButton>&nbsp;&nbsp;
                                                                    </td>
                                                                    <td nowrap align="left">
                                                                        <uc1:MultipleColumnDDLabelControl ID="multipleDropControl" runat="server"></uc1:MultipleColumnDDLabelControl>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td style="height: 11px" class =3></td>
                                        </tr>
                                        <tr>
                                            <td colspan=3>
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
