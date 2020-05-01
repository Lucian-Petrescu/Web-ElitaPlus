<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="IBNRExportReportForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.IBNRExportReportForm" %>

<%@ Register TagPrefix="uc1" TagName="ReportExtractInputControl" Src="ReportExtractInputControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title id="rptWindowTitle" runat="server">IBNR</title>
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
                            <asp:Label ID="Label7" runat="server" CssClass="TITLELABELTEXT">IBNR_Export</asp:Label>
                        </td>
                        <td height="20" align="right">
                            *&nbsp;
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
                        cellpadding="6" rules="cols" width="98%" align="center" bgcolor="#fef9ea" height="98%"
                        border="0">
                        <tr>
                            <td height="1">
                            </td>
                        </tr>
                        <tr>
                            <td  valign="top">
                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
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
                                                        <uc1:ReportExtractInputControl ID="moReportExtractInputControl" runat="server"></uc1:ReportExtractInputControl>
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
                                            <table style="width: 706px; height: 50px" cellspacing="2" cellpadding="0" width="706"
                                                border="0">
                                                <tr>
                                                    <td style="width: 100%; height: 17px" align="center" colspan="2">
                                                        *
                                                        <asp:Label ID="MonthYearLabel" runat="server" Font-Bold="false">SELECT MONTH AND YEAR</asp:Label>:
                                                        <asp:DropDownList ID="MonthDropDownList" runat="server" AutoPostBack="false" Width="128px">
                                                        </asp:DropDownList>
                                                        &nbsp;
                                                        <asp:DropDownList ID="YearDropDownList" runat="server" AutoPostBack="false" Width="84px">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td id="ddSeparator" align="center" width="100%" colspan="2">
                                                        <hr style="width: 100%; height: 1px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td valign="bottom" align="center" width="100%" colspan="2">
                                                        <uc1:MultipleColumnDDLabelControl ID="multipleDropControl" runat="server"></uc1:MultipleColumnDDLabelControl>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" width="100%" colspan="2">
                                                        <hr id="ddSeparator1" style="width: 100%; height: 1px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td nowrap align="right" width="35%">
                                                        *
                                                        <asp:RadioButton ID="rdealer" onclick="toggleAllDealersSelection(false,'DealerLabel');"
                                                            AutoPostBack="false" Checked="False" runat="server" TextAlign="left" Text="SELECT_ALL_DEALERS">
                                                        </asp:RadioButton>
                                                    </td>
                                                    <td style="height: 24px" nowrap align="center" width="65%">
                                                        <asp:Label ID="DealerLabel" runat="server">OR A SINGLE DEALER</asp:Label>:
                                                        <asp:DropDownList ID="cboDealer" runat="server" AutoPostBack="false" Width="267px"
                                                            onchange="toggleAllDealersSelection(true);">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" width="100%" colspan="2">
                                                        <hr style="width: 100%; height: 1px">
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td style="height: 95%; width: 100%;">
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
                                <asp:Button ID="btnGenRpt" runat="server" CssClass="FLATBUTTON" height="20px" style="BACKGROUND-IMAGE: url(../Navigation/images/viewIcon2.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat" Text="Generate Report Request" Width="200px" />
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    </form>

    <script language="JavaScript" src="../Navigation/Scripts/ReportCeMainScripts.js"></script>
    <script type="text/javascript">

            
    $(document).ready(function(){
        $("form > *").change(function() {
            enableReport();
        });
    });


    function enableReport() {
        //debugger
        var btnGenReport =  document.getElementById("<%=btnGenRpt.ClientID%>");
        btnGenReport.disabled = false;
    }

           
</script>
</body>
</html>
