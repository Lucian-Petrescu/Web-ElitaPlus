<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SalesReconciliationReportForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.SalesReconciliationReportForm" %>

<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title id="rptWindowTitle" runat="server">NewCertificatesByCoverage</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Styles.css" type="text/css" rel="STYLESHEET">

    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>

</head>
<body leftmargin="0" topmargin="0" onload="changeScrollbarColor();" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" />
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
                            <asp:Label ID="LabelReports" CssClass="TITLELABEL" runat="server">Reports</asp:Label>:
                            <asp:Label ID="Label7" runat="server" CssClass="TITLELABELTEXT">SALES_RECONCILIATION</asp:Label>
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
            <td valign="top" align="center" height="100%">
                <asp:Panel ID="WorkingPanel" runat="server">
                    <table id="tblMain1" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                        border-left: #999999 1px solid; border-bottom: #999999 1px solid" cellspacing="0"
                        cellpadding="6" rules="cols" width="98%" align="center" bgcolor="#fef9ea" height="95%"
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
                                        <td style="width: 99.43%; height: 1px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3" valign="top">
                                            <table border="0" cellpadding="0" cellspacing="2" width="95%">
                                                <tr>
                                                    <td align="center" colspan="3" rowspan="1" valign="middle" width="50%">
                                                        <table border="0" cellpadding="0" cellspacing="0" width="484">
                                                            <tr>
                                                                <td align="right" nowrap valign="middle">
                                                                    *
                                                                    <asp:Label ID="moBeginDateLabel" runat="server">BEGIN_DATE</asp:Label>
                                                                    :
                                                                </td>
                                                                <td nowrap style="width: 110px">
                                                                    &nbsp;
                                                                    <asp:TextBox ID="moBeginDateText" runat="server" CssClass="FLATTEXTBOX" TabIndex="1"
                                                                        Width="95px"></asp:TextBox>
                                                                </td>
                                                                <td align="center" style="width: 15px" valign="middle">
                                                                    <asp:ImageButton ID="BtnBeginDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                                                        Width="20px" />
                                                                </td>
                                                                <td align="right" colspan="1" nowrap rowspan="1" valign="middle" width="15">
                                                                </td>
                                                                <td align="right" nowrap valign="middle">
                                                                    *
                                                                    <asp:Label ID="moEndDateLabel" runat="server">END_DATE</asp:Label>
                                                                    :
                                                                </td>
                                                                <td nowrap style="width: 110px">
                                                                    &nbsp;
                                                                    <asp:TextBox ID="moEndDateText" runat="server" CssClass="FLATTEXTBOX" TabIndex="1"
                                                                        Width="95px"></asp:TextBox>
                                                                </td>
                                                                <td align="center" style="width: 15px" valign="middle">
                                                                    <asp:ImageButton ID="BtnEndDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                                                        Width="20px" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <hr style="width: 99.43%; height: 1px"></hr>
                                                    </td>
                                                </tr>
                                               
                                                        <tr>
                                                            <td align="center" colspan="3">
                                                                <table id="Table1" border="0" cellpadding="1" cellspacing="1" width="100%">
                                                                    <tr>
                                                                        <td align="right" valign="bottom" width="23%">
                                                                        </td>
                                                                        <td nowrap valign="baseline" width="75%">
                                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                <ContentTemplate>
                                                                                    <uc1:MultipleColumnDDLabelControl ID="UserCompanyMultiDrop" runat="server" />
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                  
                                                <tr id="trsep" runat="server">
                                                    <td colspan="3">
                                                        <hr style="width: 99.43%; height: 1px"></hr>
                                                    </td>
                                                </tr>
                                               
                                                        <tr>
                                                            <td align="center" colspan="3">
                                                                <table id="Table2" border="0" cellpadding="1" cellspacing="1" width="100%">
                                                                    <tr>
                                                                        <td align="right" valign="bottom" width="23%">
                                                                        </td>
                                                                        <td nowrap valign="baseline" width="75%">
                                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                                                <ContentTemplate>
                                                                                    <uc1:MultipleColumnDDLabelControl ID="multipleDropControl" runat="server" />
                                                                                </ContentTemplate>
                                                                                 <Triggers>
                                                                                <asp:AsyncPostBackTrigger ControlID="UserCompanyMultiDrop" />
                                                                                </Triggers>
                                                                            </asp:UpdatePanel>
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                               
                                                      
                                                <tr>
                                                    <td colspan="3">
                                                        <hr style="width: 99.43%; height: 1px"></hr>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" colspan="1" nowrap width="25%">
                                                        *
                                                        <asp:RadioButton ID="RadiobuttonTotalsOnly" runat="server" AutoPostBack="false" onclick="toggleDetailSelection(false);"
                                                            Text="SHOW TOTALS ONLY" TextAlign="left" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td align="right" colspan="1" nowrap valign="middle" width="5%">
                                                    </td>
                                                    <td align="left" nowrap width="25%">
                                                        &nbsp;
                                                        <asp:RadioButton ID="RadiobuttonDetail" runat="server" AutoPostBack="false" onclick="toggleDetailSelection(true);"
                                                            Text="OR SHOW DETAIL WITH TOTALS" TextAlign="left" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="3" nowrap width="25%">
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
                                <asp:Button ID="btnGenRpt" Style="background-image: url(../Navigation/images/viewIcon2.gif);
                                    cursor: hand; background-repeat: no-repeat" runat="server" CssClass="FLATBUTTON"
                                    Width="100px" Text="View" Height="20px"></asp:Button>
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
