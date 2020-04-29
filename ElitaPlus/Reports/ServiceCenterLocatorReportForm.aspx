<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDropControl" Src="../Common/MultipleColumnDropControl.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ServiceCenterLocatorReportForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.ServiceCenterLocatorReportForm" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>ServiceCenterLocator</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Styles.css" type="text/css" rel="STYLESHEET">
    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.6.1.min.js"></script>
    <script language="JavaScript">
        $(document).ready(function () {
            $('.fromToInput').find('select').css({ height: '' });
        });

        function MultipleColumnChanged() {
            ToggleDualDropDownsSelection('moFromMultipleColumnDropControl_moMultipleColumnDrop', 'moToMultipleColumnDropControl_moMultipleColumnDrop', 'moAllServiceCentersRadio', false)
            ToggleDualDropDownsSelection('moFromMultipleColumnDropControl_moMultipleColumnDropDesc', 'moToMultipleColumnDropControl_moMultipleColumnDropDesc', 'moAllServiceCentersRadio', false)
            var objFromCodeDropDown = document.getElementById('moFromMultipleColumnDropControl_moMultipleColumnDrop'); // "By Code" DropDown control
            var objFromDecDropDown = document.getElementById('moFromMultipleColumnDropControl_moMultipleColumnDropDesc');   // "By Description" DropDown control 
            var objToCodeDropDown = document.getElementById('moToMultipleColumnDropControl_moMultipleColumnDrop'); // "By Code" DropDown control
            var objToDecDropDown = document.getElementById('moToMultipleColumnDropControl_moMultipleColumnDropDesc');   // "By Description" DropDown control 
            objFromCodeDropDown.selectedIndex = 0;
            objFromDecDropDown.selectedIndex = 0;
            objToCodeDropDown.selectedIndex = 0;
            objToDecDropDown.selectedIndex = 0;
        }
    </script>
</head>
<body leftmargin="0" topmargin="0" onload="changeScrollbarColor();" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <!--Start Header-->
        <input id="rptTitle" type="hidden" name="rptTitle">
        <input id="rptSrc" type="hidden" name="rptSrc">
        <table style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid"
            cellspacing="0" cellpadding="0" width="98%" bgcolor="#d5d6e4" border="0">
            <tr>
                <td valign="top">
                    <table width="100%" border="0">
                        <tr>
                            <td height="20">
                                <asp:Label ID="LabelReports" runat="server" CssClass="TITLELABEL">Reports</asp:Label>:&nbsp;
								<asp:Label ID="moTitleLabel" runat="server" CssClass="TITLELABELTEXT">Service_Center_Locator</asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table id="tblOuter2" style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid"
            height="93%" cellspacing="0" cellpadding="0" rules="none" width="98%" bgcolor="#d5d6e4"
            border="0">
            <!--d5d6e4-->
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td valign="top" align="center" height="100%">
                    <asp:Panel ID="WorkingPanel" runat="server">
                        <table id="tblMain1" style="border-right: #999999 1px solid; border-top: #999999 1px solid; border-left: #999999 1px solid; border-bottom: #999999 1px solid"
                            cellspacing="0" cellpadding="6" rules="cols" width="98%" align="center" bgcolor="#fef9ea"
                            border="0">
                            <tr>
                                <td height="1"></td>
                            </tr>
                            <tr>
                                <td>
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td colspan="3">
                                                <table id="tblSearch" style="border-right: #999999 1px solid; border-top: #999999 1px solid; border-left: #999999 1px solid; width: 100%; border-bottom: #999999 1px solid; height: 64px"
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
                                        <tr id="ddSeparator">
                                            <td align="center" colspan="4">
                                                <table id="tblCountry" cellspacing="2" cellpadding="0" width="75%" border="0">
                                                    <tr>
                                                        <td nowrap align="left" width="15%">&nbsp;&nbsp;
																<asp:Label ID="moCountryLabel" runat="server">SELECT_SINGLE_COUNTRY</asp:Label>&nbsp;&nbsp;</td>
                                                        <td nowrap align="left" width="35%">
                                                            <asp:DropDownList ID="cboCountry" runat="server" AutoPostBack="True" Width="100%"></asp:DropDownList></td>
                                                        <td nowrap align="left" width="50%"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <hr style="height: 1px">
                                                            <img height="1" src="../Navigation/images/trans_spacer.gif"></td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                                <table id="tblSericeCenter" cellspacing="2" cellpadding="8" rules="cols" width="100%" align="center"
                                                    bgcolor="#fef9ea" border="0">
                                                    <tr>
                                                        <td align="center" colspan="4">
                                                            <table cellspacing="2" cellpadding="0" width="75%" border="0">
                                                                <tr>
                                                                    <td nowrap align="right" width="40%" colspan="2">
                                                                        <asp:RadioButton ID="rSvcNetwork" onclick="ToggleSingleDropDownSelection('dpSvcNetwork', 'rSvcNetwork',false);" runat="server"
                                                                            Text="PLEASE_SELECT_ALL_SERVICE_NETWORKS" AutoPostBack="true" TextAlign="left" Checked="False"></asp:RadioButton>
                                                                    </td>
                                                                    <td nowrap align="left" width="25%">
                                                                        <asp:Label ID="lblSvcNetwork" runat="server">or_a_single_service_network</asp:Label>&nbsp;&nbsp;
                                                                    </td>
                                                                </tr></table>
                                                        </td>
                                                        <td nowrap align="left" width="40%">
                                                            <asp:DropDownList ID="dpSvcNetwork" runat="server" AutoPostBack="true" onchange="ToggleSingleDropDownSelection('dpSvcNetwork', 'rSvcNetwork', true);"
                                                                Width="230px">
                                                            </asp:DropDownList>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <hr style="height: 1px">
                                                            <img height="1" src="../Navigation/images/trans_spacer.gif">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td nowrap align="right" width="40%" colspan="2">
                                                            <asp:RadioButton ID="moAllServiceCentersRadio" onclick="MultipleColumnChanged();"
                                                                runat="server" Text="PLEASE SELECT ALL SERVICE CENTERS" TextAlign="left" Checked="False"></asp:RadioButton></td>
                                                        <td nowrap align="left" width="25%">&nbsp;&nbsp; &nbsp;&nbsp;</td>
                                                        <td nowrap align="left" width="40%"></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <img height="3" src="../Navigation/images/trans_spacer.gif"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 2px" nowrap align="right" width="50%" colspan="2">
                                                            <asp:Label ID="moRangeServiceCentersLabel" runat="server">or a range of service centers</asp:Label>:&nbsp;&nbsp;</td>
                                                        <td style="height: 2px" nowrap align="left" width="25%"></td>
                                                        <td style="height: 2px" nowrap align="left" width="25%"></td>
                                                    </tr>
                                                </table>
                                                <table cellspacing="2" cellpadding="0" width="75%" border="0" class="fromToInput">
                                                    <tr>
                                                        <td colspan="4">
                                                            <img height="3" src="../Navigation/images/trans_spacer.gif"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 2px" nowrap align="right" width="25%" colspan="1">
                                                            <asp:Label ID="moFromLabel" runat="server">FROM</asp:Label>:&nbsp;&nbsp;</td>
                                                        <td style="height: 2px" nowrap align="left" width="75%" colspan="3">
                                                            <uc1:MultipleColumnDropControl ID="moFromMultipleColumnDropControl" runat="server"></uc1:MultipleColumnDropControl>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <img height="3" src="../Navigation/images/trans_spacer.gif"></td>
                                                    </tr>
                                                    <tr>
                                                        <td style="height: 2px" nowrap align="right" width="25%" colspan="1">
                                                            <asp:Label ID="moToLabel" runat="server">TO</asp:Label>:&nbsp;&nbsp;</td>
                                                        <td style="height: 2px" nowrap align="left" width="75%" colspan="3">
                                                            <uc1:MultipleColumnDropControl ID="moToMultipleColumnDropControl" runat="server"></uc1:MultipleColumnDropControl>
                                                        </td>
                                                    </tr>
                                                </table>
                                                <table cellspacing="2" cellpadding="0" width="75%" border="0">
                                                    <tr>
                                                        <td colspan="4">
                                                            <hr style="height: 1px">
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td nowrap align="right" width="25%" colspan="2">
                                                            <asp:RadioButton ID="moAllServiceGroupsRadio" onclick="ToggleSingleDropDownSelection('moServiceGroupDrop', 'moAllServiceGroupsRadio',false);"
                                                                AutoPostBack="false" runat="server" Text="Please select all service groups" TextAlign="left"
                                                                Checked="False"></asp:RadioButton></td>
                                                        <td nowrap align="left" width="25%">&nbsp;&nbsp;
																			<asp:Label ID="moServiceGroupLabel" runat="server">or a single service group</asp:Label>&nbsp;&nbsp;</td>
                                                        <td nowrap align="left" width="40%">
                                                            <asp:DropDownList ID="moServiceGroupDrop" runat="server" AutoPostBack="false" onchange="ToggleSingleDropDownSelection('moServiceGroupDrop', 'moAllServiceGroupsRadio', true);"
                                                                Width="230px">
                                                            </asp:DropDownList></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <img height="3" src="../Navigation/images/trans_spacer.gif"></td>
                                                    </tr>
                                                    <tr>
                                                        <td nowrap align="right" width="25%" colspan="2">
                                                            <asp:RadioButton ID="moAllPriceListsRadio" onclick="ToggleSingleDropDownSelection('moPriceListDrop', 'moAllPriceListsRadio',false);"
                                                                AutoPostBack="false" runat="server" Text="Please select all price lists" TextAlign="left" Checked="False"></asp:RadioButton></td>
                                                        <td nowrap align="left" width="25%">&nbsp;&nbsp;
																			<asp:Label ID="moPriceListLabel" runat="server">or a single price list</asp:Label>&nbsp;&nbsp;</td>
                                                        <td nowrap align="left" width="25%">
                                                            <asp:DropDownList ID="moPriceListDrop" runat="server" AutoPostBack="false" onchange="ToggleSingleDropDownSelection('moPriceListDrop', 'moAllPriceListsRadio', true);"
                                                                Width="230px">
                                                            </asp:DropDownList></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <img height="3" src="../Navigation/images/trans_spacer.gif"></td>
                                                    </tr>
                                                    <tr>
                                                        <td nowrap align="right" width="25%" colspan="2">
                                                            <asp:RadioButton ID="moAllMethodofRepairRadio" onclick="ToggleSingleDropDownSelection('moMethodofRepairDrop', 'moAllMethodofRepairRadio',false);"
                                                                AutoPostBack="false" runat="server" Text="Please_select_all_method_of_repair" TextAlign="left"
                                                                Checked="False"></asp:RadioButton></td>
                                                        <td nowrap align="left" width="25%">&nbsp;&nbsp;
																			<asp:Label ID="moMethodofRepairLabel" runat="server">OR_A_SINGLE_METHOD_OF_REPAIR</asp:Label>&nbsp;&nbsp;</td>
                                                        <td nowrap align="left" width="25%">
                                                            <asp:DropDownList ID="moMethodofRepairDrop" runat="server" AutoPostBack="false" onchange="ToggleSingleDropDownSelection('moMethodofRepairDrop', 'moAllMethodofRepairRadio', true);"
                                                                Width="230px">
                                                            </asp:DropDownList></td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            <img height="1" src="../Navigation/images/trans_spacer.gif"></td>
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
                                                                                    <asp:Label ID="moSortOrderLabel" runat="server">SELECT_REPORT_SORT_ORDER</asp:Label>:</td>
                                                                            </tr>
                                                                        </table>
                                                                    </td>
                                                                    <td valign="top" width="65%" colspan="2">
                                                                        <asp:RadioButtonList ID="moSortOrderRadioList" runat="server" RepeatDirection="Vertical">
                                                                            <asp:ListItem Value="1" Selected="True">State</asp:ListItem>
                                                                            <asp:ListItem Value="2">City</asp:ListItem>
                                                                            <asp:ListItem Value="3">Service Group Code</asp:ListItem>
                                                                            <asp:ListItem Value="4">METHOD_OF_REPAIR</asp:ListItem>
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
                                    <hr>
                                </td>
                            </tr>
                        </table>
                    </asp:panel>
                </td>
            </tr>
            <tr>
                <td align="left">&nbsp;
										<asp:Button ID="btnGenRpt" Style="background-image: url(../Navigation/images/viewIcon2.gif); cursor: hand; background-repeat: no-repeat"
                                            runat="server" Font-Bold="false" Text="View" Width="100px" Height="20px" CssClass="FLATBUTTON"></asp:Button></td>
            </tr>
        </table>
     
    </form>
    <script language="JavaScript" src="../Navigation/Scripts/ReportCeMainScripts.js"></script>
</body>
</html>
