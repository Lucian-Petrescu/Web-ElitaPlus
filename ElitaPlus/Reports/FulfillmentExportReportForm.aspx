<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FulfillmentExportReportForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.FulfillmentExportReportForm" %>
<%@ Register TagPrefix="uc1" TagName="ReportExtractInputControl" Src="ReportExtractInputControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title id="rptWindowTitle" runat="server">Actuarial Claims</title>
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
                                ID="Label7" runat="server" CssClass="TITLELABELTEXT">FULFILLMENT_EXPORT</asp:Label>
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
                        cellpadding="6" rules="cols" width="98%" height="95%" align="center" bgcolor="#fef9ea"
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
                                        <td colspan="3">
                                            <img height="15" src="../Navigation/images/trans_spacer.gif">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3">
                                            <table cellspacing="2" cellpadding="0" width="75%" border="0">
                                                <tr>
                                                    <td style="height: 35px" align="center" width="50%" colspan="3">
                                                        <table cellspacing="0" cellpadding="0" width="75%" border="0">
                                                            <tr>
                                                                <td valign="middle" nowrap align="right">
                                                                    <asp:Label ID="moBeginDateLabel" runat="server" Font-Bold="false">BEGIN_DATE</asp:Label>:
                                                                </td>
                                                                <td nowrap>
                                                                    &nbsp;
                                                                    <asp:TextBox ID="moBeginDateText" TabIndex="1" runat="server" CssClass="FLATTEXTBOX" onchange="enableReport()"
                                                                        ></asp:TextBox>
                                                                        <asp:ImageButton ID="BtnBeginDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                                                                    </asp:ImageButton>
                                                                </td>
                                                                <td>
                                                                    &nbsp;&nbsp;
                                                                </td>
                                                                <td valign="middle" nowrap align="right">
                                                                    <asp:Label ID="moEndDateLabel" runat="server" Font-Bold="false">END_DATE</asp:Label>:
                                                                </td>
                                                                <td nowrap>
                                                                    &nbsp;
                                                                    <asp:TextBox ID="moEndDateText" TabIndex="1" runat="server" CssClass="FLATTEXTBOX" onchange="enableReport()"
                                                                        Width="125px"></asp:TextBox>
                                                                    <asp:ImageButton ID="BtnEndDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                                                                    </asp:ImageButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <hr style="width: 84.3%; height: 1px">
                                                        &nbsp;&nbsp;&nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width: 77px" align="center">
                                                    </td>
                                                    <!--<td align="center" colspan="2">
                                                        <uc1:MultipleColumnDDLabelControl ID="multipleDropControl" runat="server"></uc1:MultipleColumnDDLabelControl>
                                                    </td>-->
                                                    <td align="center" colspan="2">
                                                                    <uc1:MultipleColumnDDLabelControl ID="moDealerMultipleDrop" runat="server" />
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
