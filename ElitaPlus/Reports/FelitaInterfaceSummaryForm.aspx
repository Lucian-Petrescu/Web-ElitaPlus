<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FelitaInterfaceSummaryForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.FelitaInterfaceSummaryForm" %>

<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title id="rptWindowTitle" runat="server">FelitaInterfaceSummary</title>
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
                            <asp:Label ID="LabelReports"  CssClass="TITLELABEL" runat="server">Reports</asp:Label>:
                            <asp:Label ID="Label7" runat="server"  CssClass="TITLELABELTEXT">FELITA_INTERFACE_SUMMARY</asp:Label>
                        </td>
                        <td height="20" align="right">
                            *&nbsp;
                            <asp:Label ID="moIndicatesLabel" runat="server"  EnableViewState="true">INDICATES_REQUIRED_FIELDS</asp:Label>
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
                        cellpadding="6" rules="cols" width="98%" align="center" bgcolor="#fef9ea" border="0" height ="95%">
                        <tr>
                            <td height="1">
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tr>
                                        <td colspan="3">
                                            <table id="tblSearch" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                                                border-left: #999999 1px solid; width: 100%; border-bottom: #999999 1px solid;
                                                height: 98%" cellspacing="2" cellpadding="8" rules="cols" width="100%" align="center"
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
                                                        <uc1:ReportCeInputControl ID="moReportCeInputControl" runat="server"></uc1:ReportCeInputControl>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3" style="height: 15px">
                                            <img height="15" src="../Navigation/images/trans_spacer.gif">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td style="width: 99.43%; height: 1px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3">
                                            <table id="Table2" style="width: 100%; height: 84px" cellspacing="1" cellpadding="1"
                                                 border="0">
                                                <tr>
                                                    <td style="width: 100%" valign="top" nowrap align="center" colspan="4" rowspan="1">
                                                        <uc1:MultipleColumnDDLabelControl ID="multipleDropControl" runat="server"></uc1:MultipleColumnDDLabelControl>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width 100%" colspan=4>
                                                        <hr style="height: 1PX" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" width="50%" colspan="4">
                                                        <table cellspacing="0" cellpadding="0" width="85%" border="0">
                                                            <tr>
                                                                <td valign="baseline" nowrap align="right">
                                                                    <asp:Label ID="moBeginDateLabel" runat="server" >BEGIN_DATE</asp:Label>:
                                                                </td>
                                                                <td nowrap>
                                                                    &nbsp;
                                                                    <asp:TextBox ID="moBeginDateText" TabIndex="1" runat="server" CssClass="FLATTEXTBOX"></asp:TextBox>
                                                                    <asp:ImageButton ID="BtnBeginDate" runat="server" Width="20px" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                                                        Height="17px"></asp:ImageButton>
                                                                </td>
                                                                <td width="5%">
                                                                    &nbsp;&nbsp;
                                                                </td>
                                                                <td valign="baseline" nowrap align="right">
                                                                    <asp:Label ID="moEndDateLabel" runat="server" >END_DATE</asp:Label>:
                                                                </td>
                                                                <td nowrap>
                                                                    &nbsp;
                                                                    <asp:TextBox ID="moEndDateText" TabIndex="1" runat="server" CssClass="FLATTEXTBOX"
                                                                        Width="125px"></asp:TextBox>
                                                                    <asp:ImageButton ID="BtnEndDate" runat="server" Width="20px" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                                                        Height="17px"></asp:ImageButton>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="width 99.43%" colspan=4>
                                                        <hr style="height: 1px"/>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="3">
                                                        <table border="0" cellpadding="0" cellspacing="2" width="85%">                                                          
                                                            <tr>
                                                                <td align="right">
                                                                    <asp:RadioButton ID="rAllFiles" runat="server" AutoPostBack="false" Checked="True"
                                                                        GroupName="gb" onclick="toggleRadioButtonSelection('rAllFiles','rJournalFiles',true);"
                                                                        Text="All_Files" TextAlign="left" />
                                                                </td>
                                                                <td align="center" width="25%">
                                                                </td>
                                                                <td align="left">
                                                                    <asp:RadioButton ID="rJournalFiles" runat="server" AutoPostBack="false" GroupName="gb"
                                                                        onclick="toggleRadioButtonSelection('rAllFiles','rJournalFiles',false);" Text="Or_Journal_Files_Only"
                                                                        TextAlign="left" />
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                               
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="4" style="height: 85%; width: 100%;">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <hr>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Button ID="btnGenRpt" Style="background-image: url(../Navigation/images/viewIcon2.gif);
                                    cursor: hand; background-repeat: no-repeat" runat="server" 
                                    CssClass="FLATBUTTON" Width="100px" Text="View" Height="20px"></asp:Button>
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
