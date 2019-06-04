<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ExportWMRiskTypeReportForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.ExportWMRiskTypeReportForm" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ReportExtractInputControl" Src="ReportExtractInputControl.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title id="rptWindowTitle" runat="server">WM RISK TYPE</title>
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
                            <asp:Label ID="Label7" runat="server" CssClass="TITLELABELTEXT">WM_RISK_TYPE</asp:Label>
                        </td>
                        <td height="20" align="right">
                            *&nbsp;
                            <asp:Label ID="moIndicatesLabel" runat="server" EnableViewState="true">INDICATES_REQUIRED_FIELDS</asp:Label>
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
            <td valign="top" align="left" height="100%">
                <asp:Panel ID="WorkingPanel" runat="server">
                    <table id="tblMain1" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                        border-left: #999999 1px solid; border-bottom: #999999 1px solid" cellspacing="0"
                        cellpadding="6" rules="cols" width="98%" height="98%" align="center" bgcolor="#fef9ea"
                        border="0">
                        <tr>
                            <td height="1">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table cellspacing="0" cellpadding="0" width="100%" border="0" height="95%">
                                    <tr>
                                        <td colspan="3" valign="top">
                                            <table id="tblSearch" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                                                border-left: #999999 1px solid; border-bottom: #999999 1px solid; height: 76px"
                                                cellspacing="2" cellpadding="8" width="100%" rules="cols" align="center" bgcolor="#fef9ea"
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
                                                        <uc1:ReportExtractInputControl ID="moReportExtractInputControl" runat="server"></uc1:ReportExtractInputControl>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3"  valign= "top">
                                            <img height="15" src="../Navigation/images/trans_spacer.gif">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3">
                                            <table cellspacing="2" cellpadding="0" width="90%" border="0">
                                                <tr>
                                                    <td style="width: 90%; height: 10px" colspan="3">
                                                    </td>
                                                </tr>                                                                                              
                                                <tr>
                                                    <td style="height: 25px" align="center" colspan="3">                                                       
                                                        <table id="Table2" cellspacing="1" cellpadding="1" width="100%" border="0">
                                                            <tr>
                                                                <td colspan="2">
                                                                    <table border="0" cellpadding="1" cellspacing="1" width="100%">
                                                                        <tr>
                                                                            <td nowrap align="left" width="15%">
                                                                            </td>
                                                                            <td nowrap valign="baseline" style="text-align:right">
                                                                                <uc1:MultipleColumnDDLabelControl ID="moUserCompanyMultipleDrop" runat="server">
                                                                                </uc1:MultipleColumnDDLabelControl>
                                                                            </td>
                                                                            <td style="width:25%">&nbsp;</td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td id="ddSeparator" align="center" colspan="3" width="100%" runat="server">
                                                                                <hr style="width: 100%; height: 1px"></hr>
                                                                            </td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td nowrap align="left" width="15%">
                                                                            </td>
                                                                            <td nowrap valign="baseline" style="text-align:right">
                                                                                <uc1:MultipleColumnDDLabelControl ID="multipleDropControl" runat="server"></uc1:MultipleColumnDDLabelControl>
                                                                            </td>
                                                                            <td style="width:25%">&nbsp;</td>
                                                                        </tr>
                                                                    </table>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td style="height: 10px" colspan="2">
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
                            <td style="height: 95%; width: 100%;">
                            </td>
                        </tr>
                        <tr>
                            <td width="100%">
                                <hr>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
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
<script type="text/javascript"">

            
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
