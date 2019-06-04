<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="NewCertificatesByBranchEnglishUSAReportForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.NewCertificatesByBranchEnglishUSAReportForm" %>
<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UserControlAvailableSelected" Src="../Common/UserControlAvailableSelected.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title id="rptWindowTitle" runat="server">NewCertificatesByBranch</title>
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
                            <asp:Label ID="LabelReports" CssClass="TITLELABEL" runat="server">Reports</asp:Label>:&nbsp;<asp:Label
                                ID="Label7" runat="server" CssClass="TITLELABELTEXT">New Certificates By Branch</asp:Label>
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
                        height: 95%; border-left: #999999 1px solid; border-bottom: #999999 1px solid"
                        cellspacing="0" cellpadding="6" rules="cols" width="98%" align="center" bgcolor="#fef9ea"
                        border="0">
                        <tr>
                            <td height="1">
                                &nbsp;
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
                                        <td align="center" width="25%" colspan="3">
                                            <table cellspacing="0" cellpadding="0" width="75%" border="0">
                                                <tr>
                                                    <td valign="middle" nowrap align="right">
                                                        <asp:Label ID="moBeginDateLabel" runat="server">BEGIN_DATE</asp:Label>:
                                                    </td>
                                                    <td nowrap>
                                                        &nbsp;
                                                        <asp:TextBox ID="moBeginDateText" TabIndex="1" runat="server" CssClass="FLATTEXTBOX"></asp:TextBox>
                                                        <asp:ImageButton ID="BtnBeginDate" runat="server" Width="20px" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                                            Height="17px" ImageAlign="AbsMiddle"></asp:ImageButton>
                                                    </td>
                                                    <td>
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                    </td>
                                                    <td valign="middle" nowrap align="right">
                                                        <asp:Label ID="moEndDateLabel" runat="server">END_DATE</asp:Label>:
                                                    </td>
                                                    <td nowrap>
                                                        &nbsp;
                                                        <asp:TextBox ID="moEndDateText" TabIndex="1" runat="server" CssClass="FLATTEXTBOX"
                                                            Width="125px"></asp:TextBox>
                                                        <asp:ImageButton ID="BtnEndDate" runat="server" Width="20px" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                                            Height="17px" ImageAlign="AbsMiddle"></asp:ImageButton>
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3">
                                            <table border="0" cellpadding="0" cellspacing="2" width="75%">
                                                <tr>
                                                    <td colspan="3">
                                                        <hr style="height: 1px"></hr>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="3">
                                                        <table id="Table1" border="0" cellpadding="1" cellspacing="1" width="100%">
                                                            <tr>
                                                                <td align="right" valign="bottom" width="30%">
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
                                                                <td align="right" valign="bottom" width="30%">
                                                                    *
                                                                    <asp:RadioButton ID="rdealer" runat="server" AutoPostBack="false" Checked="False"
                                                                        onClick="return TogglelDropDownsSelectionsForNewCertForm('rdealer');" Text="SELECT_ALL_DEALERS"
                                                                        TextAlign="left" />
                                                                </td>
                                                                <td nowrap valign="baseline" width="70%">
                                                                    <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                                        <ContentTemplate>
                                                                            <uc1:MultipleColumnDDLabelControl ID="multipleDropControl" runat="server" />
                                                                        </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="UserCompanyMultiDrop" />
                                                                            <asj:AsyncPostBackTrigger ControlID="UserCompanyMultiDrop" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="3">
                                                        <table id="Table3" border="0" cellpadding="1" cellspacing="1" width="100%">
                                                            <tr>
                                                                <td align="right" valign="bottom" width="30%">
                                                                </td>
                                                                <td nowrap valign="baseline" width="75%">
                                                                    <asp:Label ID="lblCompany" runat="server">DEALER_GROUP:</asp:Label>
                                                                    &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                     <asp:UpdatePanel ID="UpdatePanel3" runat="server">
                                                                        <ContentTemplate>
                                                                    <asp:DropDownList ID="moDealerGroupList" runat="server" AutoPostBack="false" onChange="return TogglelDropDownsSelectionsForNewCertForm('dealerGroup');">
                                                                    </asp:DropDownList>
                                                                     </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="UserCompanyMultiDrop" />
                                                                            <asj:AsyncPostBackTrigger ControlID="UserCompanyMultiDrop" />
                                                                         </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                </tr>
                                                <tr>
                                                    <td align="center" colspan="3">
                                                        <table id="Table4" border="0" cellpadding="1" cellspacing="1" width="100%">
                                                            <tr>
                                                                <td align="right" valign="bottom" width="30%">
                                                                </td>
                                                                <td nowrap valign="baseline" width="75%">
                                                                    <asp:Label ID="Label1" runat="server">DEALER:</asp:Label>
                                                                     <asp:UpdatePanel ID="UpdatePanel4" runat="server">
                                                                        <ContentTemplate>
                                                                    <uc1:UserControlAvailableSelected ID="UsercontrolAvailableSelectedDealers" runat="server">
                                                                    </uc1:UserControlAvailableSelected>
                                                                     </ContentTemplate>
                                                                        <Triggers>
                                                                            <asp:AsyncPostBackTrigger ControlID="UserCompanyMultiDrop" />
                                                                            <asj:AsyncPostBackTrigger ControlID="UserCompanyMultiDrop" />
                                                                        </Triggers>
                                                                    </asp:UpdatePanel>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <hr style="height: 1px"></hr>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" valign="bottom" width="30%">
                                                       
                                                    </td>
                                                    <td nowrap valign="baseline" width="70%">
                                                         <asp:RadioButton ID="rbyBranch" runat="server" AutoPostBack="false" Checked="True"
                                                            GroupName="gb" onclick="toggleRadioButtonSelection('rbyBranch','rbySalesRep',false);"
                                                            Text="BRANCH" TextAlign="left" />
                                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                        <asp:RadioButton ID="rbySalesRep" runat="server" AutoPostBack="false"
                                                            GroupName="gb" onclick="toggleRadioButtonSelection('rbyBranch','rbySalesRep',true);"
                                                            Text="SALES_REP" TextAlign="left" Width="211px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <hr style="height: 1px"></hr>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" valign="bottom" width="30%">
                                                       
                                                    </td>
                                                    <td nowrap valign="baseline" width="70%">
                                                         <asp:RadioButton ID="RadiobuttonDateAdded" runat="server" AutoPostBack="false" Checked="True"
                                                            GroupName="gn" onclick="toggleAddedSoldSelection(false);" Text="DATE_ADDED" TextAlign="left" />
                                                       &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:RadioButton ID="RadiobuttonSold" runat="server" AutoPostBack="false"
                                                            GroupName="gn" onclick="toggleAddedSoldSelection(true);" Text="OR DATE ESC SOLD/CANCELLED"
                                                            TextAlign="left" Width="211px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <hr style="height: 1px"></hr>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" valign="bottom" width="30%">
                                                       
                                                    </td>
                                                    <td nowrap valign="baseline" width="70%">
                                                         <asp:RadioButton ID="RadiobuttonTotalsOnly" runat="server" AutoPostBack="false" onclick="toggleDetailSelection(false);"
                                                            Text="SHOW TOTALS ONLY" TextAlign="left" />
                                                        &nbsp;&nbsp;
                                                        <asp:RadioButton ID="RadiobuttonDetail" runat="server" AutoPostBack="false" onclick="toggleDetailSelection(true);"
                                                            Text="OR SHOW DETAIL WITH TOTALS" TextAlign="left" />
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
    <SCRIPT language='JavaScript' src="../Navigation/Scripts/AvailableSelected.js" type="text/javascript"></SCRIPT>

    <script language="javascript" type="text/javascript">
        function TogglelDropDownsSelectionsForNewCertForm(source) {

            if (source == "rdealer") {
                document.getElementById("multipleDropControl_moMultipleColumnDrop").selectedIndex = 0;
                document.getElementById("multipleDropControl_moMultipleColumnDropDesc").selectedIndex = 0;
                document.getElementById("moDealerGroupList").selectedIndex = 0;   // "Dealers Group" DropDown control
            }
            else if (source == "dealerGroup") {
                document.getElementById("multipleDropControl_moMultipleColumnDrop").selectedIndex = 0;
                document.getElementById("multipleDropControl_moMultipleColumnDropDesc").selectedIndex = 0;
                document.getElementById("rdealer").checked = false;   // "Dealers Group" DropDown control

            }
            RemoveAllSelectedDealersForReports("UserControlAvailableSelectedDealers");
        }

    </script>
</body>
</html>
