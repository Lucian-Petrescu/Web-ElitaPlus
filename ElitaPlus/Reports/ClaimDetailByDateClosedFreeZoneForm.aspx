<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ClaimDetailByDateClosedFreeZoneForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.ClaimDetailByDateClosedFreeZoneForm" %>

<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>


<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title id="rptWindowTitle" runat="server">CLAIM_DETAIL_BY_DATE_CLOSED_INCLUDING_FREE_ZONE</title>
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
         <input id="rptSrc" type="hidden" name="rptSrc">
        <table style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px;
            border-left: black 1px solid; border-bottom: black 1px solid" cellspacing="0"
            cellpadding="0" width="98%" bgcolor="#d5d6e4" border="0">
            <tr>
                <td valign="top">
                    <table width="100%" border="0">
                        <tr>
                            <td height="20">
                                <asp:Label ID="LabelReports"  CssClass="TITLELABEL" runat="server">Reports</asp:Label>:&nbsp;<asp:Label ID="Label7" runat="server"  CssClass="TITLELABELTEXT">CLAIM_DETAIL_BY_DATE_CLOSED_INCLUDING_FREE_ZONE</asp:Label></td>
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
                                                <img height="15" src="../Navigation/images/trans_spacer.gif"></td>
                                        </tr>
                                        <tr>
                                            <td align="center" colspan="4">
                                                <table cellspacing="2" cellpadding="0" width="75%" border="0">
                                                    <tr>
                                                        <td align="center" width="50%" colspan="4">
                                                            <table cellspacing="0" cellpadding="0" width="75%" border="0">
                                                                <tr>
                                                                    <td width="50%">
                                                                        &nbsp;</td>
                                                                    <td valign="middle" nowrap align="right">
                                                                        <asp:Label ID="moBeginDateLabel" runat="server" >BEGIN_DATE</asp:Label>:</td>
                                                                    <td nowrap>
                                                                        &nbsp;
                                                                        <asp:TextBox ID="moBeginDateText" TabIndex="1" runat="server" CssClass="FLATTEXTBOX"></asp:TextBox>&nbsp;</td>
                                                                    <td>
                                                                        <asp:ImageButton ID="BtnBeginDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                                                                        </asp:ImageButton>&nbsp;&nbsp;</td>
                                                                    <td valign="middle" nowrap align="right">
                                                                        &nbsp;&nbsp;
                                                                        <asp:Label ID="moEndDateLabel" runat="server" >END_DATE</asp:Label>:</td>
                                                                    <td nowrap>
                                                                        &nbsp;
                                                                        <asp:TextBox ID="moEndDateText" TabIndex="1" runat="server" CssClass="FLATTEXTBOX"
                                                                            Width="125px"></asp:TextBox>&nbsp;</td>
                                                                    <td>
                                                                        <asp:ImageButton ID="BtnEndDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                                                                        </asp:ImageButton></td>
                                                                    <td width="50%">
                                                                        &nbsp;</td>
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
                                                            <asp:RadioButton ID="rDealer" onclick="toggleAllDealersSelection(false);" Checked="False"
                                                                TextAlign="left" Text="SELECT_ALL_DEALERS" runat="server" AutoPostBack="false"></asp:RadioButton></td>
                                                        <td nowrap align="left" width="25%">
                                                            &nbsp;&nbsp;
                                                            <asp:Label ID="moDealerLabel" runat="server">OR A SINGLE DEALER</asp:Label>&nbsp;&nbsp;
                                                        </td>
                                                        <td nowrap align="left" width="40%">
                                                            <asp:DropDownList ID="cboDealer" runat="server" Width="100%" AutoPostBack="false"
                                                                onchange="toggleAllDealersSelection(true);">
                                                            </asp:DropDownList></td>
                                                    </tr>                                                   
                                                    <tr>
                                                        <td colspan="4"><hr style="height: 1px">
                                                        </td>
                                                    </tr>
                                                    <tr id="ddHideRow">
                                                        <td nowrap align="right" width="25%" colspan="2">
                                                            <asp:RadioButton ID="rCountry" onclick="toggleAllCountiesSelection(false);" Checked="False"
                                                                TextAlign="left" Text="PLEASE_SELECT_ALL_COUNTRIES" runat="server" AutoPostBack="True">
                                                            </asp:RadioButton></td>
                                                        <td nowrap align="left" width="25%">
                                                            &nbsp;&nbsp;
                                                            <asp:Label ID="moCountryLabel" runat="server">OR_SELECT_SINGLE_COUNTRY</asp:Label>&nbsp;&nbsp;
                                                        </td>
                                                        <td nowrap align="left" width="40%">
                                                            <asp:DropDownList ID="cboCountry" runat="server" Width="100%" AutoPostBack="True"
                                                                onchange="toggleAllCountiesSelection(true);">
                                                            </asp:DropDownList></td>
                                                    </tr>                                                    
                                                    <tr>
                                                        <td nowrap align="right" width="25%" colspan="2">
                                                            <asp:RadioButton ID="rSvcCtr" onclick="toggleAllSvcCtrsSelection(false);" Checked="False"
                                                                TextAlign="left" Text="PLEASE SELECT ALL SERVICE CENTERS" runat="server" AutoPostBack="false">
                                                            </asp:RadioButton></td>
                                                        <td nowrap align="left" width="25%">
                                                            &nbsp;&nbsp;
                                                            <asp:Label ID="SvcCtrLabel" runat="server">OR A SINGLE SERVICE CENTER</asp:Label>&nbsp;&nbsp;</td>
                                                        <td nowrap align="left" width="40%">
                                                            <asp:DropDownList ID="cboSvcCtr" runat="server" Width="100%" AutoPostBack="false"
                                                                onchange="toggleAllSvcCtrsSelection(true);">
                                                            </asp:DropDownList></td>
                                                    </tr>
                                                    <tr>
                                                            <td nowrap align="right" width="25%">
                                                                <asp:CheckBox ID="CheckBoxNoFreeZone" onclick="toggleCheckBox('CheckBoxNoFreeZone','CheckBoxFreeZone');" Text="NO_FREE_ZONE" AutoPostBack="false" Checked="False" TextAlign="left" runat="server"></asp:CheckBox>
                                                                <asp:CheckBox ID="CheckBoxFreeZone" onclick="toggleCheckBox('CheckBoxFreeZone','CheckBoxNoFreeZone');" Text="FREE_ZONE" AutoPostBack="false" Checked="False" TextAlign="left" runat="server"></asp:CheckBox>
                                                            </td>
                                                            <td width="15">
                                                                <img src="../Navigation/images/trans_spacer.gif" width="15">
                                                            </td>
                                                            <td nowrap align="left" width="50%">
                                                            </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">                                     
                                                            <hr style="height: 1px">
                                                        </td>
                                                    </tr>                                                   
                                                    <tr>
                                                        <td nowrap align="right" width="25%" colspan="2">
                                                            <asp:RadioButton ID="rRiskType" onclick="toggleAllRiskTypesSelection(false);" Checked="False"
                                                                TextAlign="left" Text="PLEASE SELECT ALL RISK TYPES" runat="server" AutoPostBack="false">
                                                            </asp:RadioButton></td>
                                                        <td nowrap align="left" width="25%">
                                                            &nbsp;&nbsp;
                                                            <asp:Label ID="RiskTypeLabel" runat="server">OR A SINGLE RISK TYPE</asp:Label>&nbsp;&nbsp;</td>
                                                        <td nowrap align="left" width="25%">
                                                            <asp:DropDownList ID="cboRiskType" runat="server" Width="100%" AutoPostBack="false"
                                                                onchange="toggleAllRiskTypesSelection(true);">
                                                            </asp:DropDownList></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <img height="3" src="../Navigation/images/trans_spacer.gif"></td>
                                                    </tr>
                                                    <tr>
                                                        <td nowrap align="right" width="25%" colspan="2">
                                                            <asp:RadioButton ID="rMethodofRepair" onclick="toggleAllRepairSelection(false);" Checked="False"
                                                                TextAlign="left" Text="PLEASE_SELECT_ALL_METHOD_OF_REPAIR" runat="server" AutoPostBack="false">
                                                            </asp:RadioButton></td>
                                                        <td nowrap align="left" width="25%">
                                                            &nbsp;&nbsp;
                                                            <asp:Label ID="lblMethodofRepair" runat="server">OR_A_SINGLE_METHOD_OF_REPAIR</asp:Label>&nbsp;&nbsp;</td>
                                                        <td nowrap align="left" width="25%">
                                                            <asp:DropDownList ID="cboMethodofRepair" runat="server" Width="100%" AutoPostBack="false"
                                                                onchange="toggleAllRepairSelection(true);">
                                                            </asp:DropDownList></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <img height="3" src="../Navigation/images/trans_spacer.gif"></td>
                                                    </tr>
                                                    <tr>
                                                        <td nowrap align="right" width="25%" colspan="2">
                                                            <asp:RadioButton ID="rCoverageType" onclick="toggleAllCovergeTypesSelection(false);" Checked="False"
                                                                TextAlign="left" Text="PLEASE_SELECT_ALL_COVERAGE_TYPES" runat="server" AutoPostBack="false">
                                                            </asp:RadioButton></td>
                                                        <td nowrap align="left" width="25%">
                                                            &nbsp;&nbsp;
                                                            <asp:Label ID="lblCoverageType" runat="server">OR_A_SINGLE_COVERAGE_TYPE</asp:Label>&nbsp;&nbsp;</td>
                                                        <td nowrap align="left" width="25%">
                                                            <asp:DropDownList ID="cboCoverageType" runat="server" Width="100%" AutoPostBack="false" 
                                                                onchange="toggleAllCovergeTypesSelection(true);">
                                                            </asp:DropDownList></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <img height="3" src="../Navigation/images/trans_spacer.gif"></td>
                                                    </tr>
                                                      <tr>
                                                        <td nowrap align="right" width="25%" colspan="2">
                                                            <asp:RadioButton ID="rReasonClosed" onclick="ToggleSingleDropDownSelection('cboReasonClosed','rReasonClosed',false);" Checked="False"
                                                                TextAlign="left" Text="PLEASE_SELECT_ALL_REASONS_CLOSED" runat="server" AutoPostBack="false">
                                                            </asp:RadioButton></td>
                                                        <td nowrap align="left" width="25%">
                                                            &nbsp;&nbsp;
                                                            <asp:Label ID="lblReasonClosed" runat="server">OR_A_SINGLE_REASON_CLOSED</asp:Label>&nbsp;&nbsp;</td>
                                                        <td nowrap align="left" width="25%">
                                                            <asp:DropDownList ID="cboReasonClosed" runat="server" Width="100%" AutoPostBack="false" 
                                                                onchange="ToggleSingleDropDownSelection('cboReasonClosed','rReasonClosed',true);">
                                                            </asp:DropDownList></td>
                                                    </tr>                                                    
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
                                                                                <td nowrap align="right">
                                                                                    <asp:Label ID="Label5" runat="server">SELECT_REPORT_SORT_ORDER</asp:Label>:</td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td valign="top" width="55%" colspan="2">
                                                                        <asp:RadioButtonList ID="rdReportSortOrder" runat="server" RepeatDirection="Vertical">
                                                                            <asp:ListItem Value="1" Selected="True">Claim_Number</asp:ListItem>
                                                                            <asp:ListItem Value="2">Dealer</asp:ListItem>
                                                                            <asp:ListItem Value="3">Risk_Type</asp:ListItem>
                                                                            <asp:ListItem Value="4">Service_Center_Name</asp:ListItem>
                                                                            <asp:ListItem Value="5">Authorized_Amount</asp:ListItem>
                                                                        </asp:RadioButtonList></td>
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
                                <td>
                                    <hr style="height: 1px">
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    &nbsp;
                                    <asp:Button ID="btnGenRpt" Style="background-image: url(../Navigation/images/viewIcon2.gif);
                                        cursor: hand; background-repeat: no-repeat" runat="server" 
                                        CssClass="FLATBUTTON" Text="View" Height="20px" Width="100px"></asp:Button></td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </form>

    <script language="JavaScript" src="../Navigation/Scripts/ReportCeMainScripts.js"></script>

    <script>

        function toggleAllCountiesSelection(isSingleCountry) {
            //debugger;
            if (isSingleCountry) {
                document.forms[0].rCountry.checked = false;
            }
            else {
                document.forms[0].cboCountry.selectedIndex = -1;
            }
        }

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

        function toggleAllRiskTypesSelection(isSingleRiskType) {
            //debugger;
            if (isSingleRiskType) {
                document.forms[0].rRiskType.checked = false;
            }
            else {
                document.forms[0].cboRiskType.selectedIndex = -1;
            }
        }

        function toggleAllRepairSelection(isSingleRepairType) {
            //debugger;
            if (isSingleRepairType) {
                document.forms[0].rMethodofRepair.checked = false;
            }
            else {
                document.forms[0].cboMethodofRepair.selectedIndex = -1;
            }
        }
        function toggleAllCovergeTypesSelection(isSingleCoverageType) {
            //debugger;
            if (isSingleCoverageType) {
                document.forms[0].rCoverageType.checked = false;
            }
            else {
                document.forms[0].cboCoverageType.selectedIndex = -1;
            }
        }
    </script>

</body>
</html>
