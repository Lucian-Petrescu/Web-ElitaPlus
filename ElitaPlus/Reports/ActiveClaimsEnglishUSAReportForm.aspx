<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ActiveClaimsEnglishUSAReportForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.ActiveClaimsEnglishUSAReportForm" %>

<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title id="rptWindowTitle" runat="server">ActiveClaims</title>
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
                            <asp:Label ID="LabelReports" runat="server"  CssClass="TITLELABEL">Reports</asp:Label>:&nbsp;<asp:Label
                                ID="Label7" runat="server" CssClass="TITLELABELTEXT">ACTIVE CLAIMS</asp:Label>
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
                        cellpadding="6" rules="cols" width="98%" align="center" bgcolor="#fef9ea" border="0"
                        height="98%">
                        <tr>
                            <td height="1">
                            </td>
                        </tr>
                        <tr>
                            <td valign ="top">
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
                                        <td align="center" colspan="3" valign="top">
                                            <table cellspacing="2" cellpadding="0" width="75%" border="0">
                                                <tr>
                                                    <td nowrap align="right" width="25%" valign="bottom">
                                                        <asp:RadioButton ID="rdealer" onclick="ToggleDualDropDownsSelection('moDealerMultipleDrop_moMultipleColumnDrop', 'moDealerMultipleDrop_moMultipleColumnDropDesc', 'rdealer', false, ''); document.all.item('moDealerMultipleDrop_lb_DropDown').style.color = '';"
                                                            AutoPostBack="false" Checked="False" runat="server" Text="SELECT_ALL_DEALERS"
                                                            TextAlign="left"></asp:RadioButton>
                                                    </td>                                                  
                                                    <td align="left" width="50%" valign="baseline">
                                                        <uc1:MultipleColumnDDLabelControl ID="moDealerMultipleDrop"  runat="server" />
                                                    </td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>                                    
                                    <tr>
                                        <td align="center" colspan="3" valign="top">
                                            <table cellspacing="2" cellpadding="0" width="75%" border="0">                                              
                                              
                                                <tr>
                                                    <td colspan="3">
                                                        <hr style="height: 1px">
                                                    </td>
                                                </tr>
                                                <tr id="ddHideRow">
                                                    <td nowrap align="right" width="25%">
                                                        <asp:RadioButton ID="rCountry" onclick="ToggleSingleDropDownSelection('cboCountry','rCountry',false);" AutoPostBack="True"
                                                            Checked="false" runat="server" Text="PLEASE_SELECT_ALL_COUNTRIES" TextAlign="left">
                                                        </asp:RadioButton>
                                                    </td>
                                                    <td width="5">
                                                        <img src="../Navigation/images/trans_spacer.gif" width="5">
                                                    </td>
                                                    <td align="left" width="50%">
                                                        <asp:Label ID="moCountryLabel" runat="server">OR_SELECT_SINGLE_COUNTRY</asp:Label>&nbsp;&nbsp;
                                                        <asp:DropDownList ID="cboCountry" runat="server" AutoPostBack="True" Width="250px"
                                                            onchange="ToggleSingleDropDownSelection('cboCountry','rCountry',true);">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td nowrap align="right" width="25%">
                                                        <asp:RadioButton ID="rsvccenter" onclick="ToggleSingleDropDownSelection('cboSvcCenter','rsvccenter',false);"
                                                            AutoPostBack="false" Checked="False" runat="server" Text="PLEASE SELECT ALL SERVICE CENTERS"
                                                            TextAlign="left"></asp:RadioButton>
                                                    </td>
                                                    <td width="15">
                                                        <img src="../Navigation/images/trans_spacer.gif" width="15">
                                                    </td>
                                                    <td nowrap align="left" width="50%">
                                                        <asp:Label ID="moServiceCenterLabel" runat="server">OR A SINGLE SERVICE CENTER</asp:Label>&nbsp;&nbsp;
                                                        <asp:DropDownList ID="cboSvcCenter" runat="server" AutoPostBack="false" Width="250px"
                                                            onchange="ToggleSingleDropDownSelection('cboSvcCenter','rsvccenter',true);">
                                                        </asp:DropDownList>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <hr style="height: 1px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td style="height: 17px" colspan="2">
                                                    </td>
                                                    <td style="height: 17px" nowrap align="left">
                                                        *
                                                        <asp:Label ID="moDaysActiveLabel" runat="server">PLEASE ENTER MINIMUM NUMBER OF DAYS ACTIVE (0 TO 999)</asp:Label>:
                                                        &nbsp;&nbsp;
                                                        <asp:TextBox ID="txtActiveDays" runat="server" Width="71px" CssClass="FLATTEXTBOX"
                                                            MaxLength="3"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                    <td>
                                                        &nbsp;
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td nowrap align="right" width="25%">
                                                        <asp:RadioButton ID="RadiobuttonAllClaims"  GroupName="claim"
                                                            AutoPostBack="false" runat="server" Text="INCLUDE REPAIRED CLAIMS" TextAlign="left">
                                                        </asp:RadioButton>
                                                    </td>
                                                    <td width="15">
                                                        <img src="../Navigation/images/trans_spacer.gif" width="15">
                                                    </td>
                                                    <td nowrap align="left" width="50%">
                                                        <asp:RadioButton ID="RadiobuttonExcludeRepairedClaims" GroupName="claim"
                                                            AutoPostBack="false" runat="server" Text="EXCLUDE REPAIRED CLAIMS" TextAlign="left">
                                                        </asp:RadioButton>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td nowrap align="right" width="25%">
                                                        <asp:RadioButton ID="rAllUsers" onclick="toggleAllUsersSelection(false);" AutoPostBack="false"
                                                            runat="server" Text="PLEASE_SELECT_ALL_USERS" TextAlign="left"></asp:RadioButton>
                                                    </td>
                                                    <td width="15">
                                                        <img src="../Navigation/images/trans_spacer.gif" width="15">
                                                    </td>
                                                    <td nowrap align="left" width="50%">
                                                        <asp:Label ID="lblUserId" runat="server">OR_ENTER_A_USER_ID</asp:Label>:&nbsp;&nbsp;
                                                        <asp:TextBox ID="txtUserId" runat="server" AutoPostBack="false" OnFocus="toggleAllUsersSelection(true);"
                                                            CssClass="FLATTEXTBOX" Width="301px"></asp:TextBox>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="3">
                                                        <hr style="height: 1px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td nowrap align="right" width="25%" valign ="top">
                                                        <asp:Label ID="moSortByLabel" runat="server">SELECT_REPORT_SORT_ORDER</asp:Label>:&nbsp;
                                                    </td>
                                                    <td width="15">
                                                        <img src="../Navigation/images/trans_spacer.gif" width="15">
                                                    </td>
                                                    <td nowrap align="left" width="50%">
                                                        <asp:RadioButtonList ID="rdReportSortOrder" runat="server" RepeatDirection="VERTICAL">
                                                            <asp:ListItem Value="0" Selected="True">NUMBER OF ACTIVE DAYS</asp:ListItem>
                                                            <asp:ListItem Value="1">CLAIM_NUMBER</asp:ListItem>
                                                            <asp:ListItem Value="2">SERVICE_CENTER_NAME</asp:ListItem>
                                                            <asp:ListItem Value="3">DEALER_CODE</asp:ListItem>
                                                        </asp:RadioButtonList>
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
                                    cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                    Text="View" Width="100px" CssClass="FLATBUTTON" Height="20px"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    </form>

    <script language="JavaScript" src="../Navigation/Scripts/ReportCeMainScripts.js"></script>

    <script>
        function toggleAllUsersSelection(isSingleUser) {
            //debugger;
            if (isSingleUser) {
                document.forms[0].rAllUsers.checked = false;
            }
            else {
                document.forms[0].txtUserId.value = "";
            }
        }					
    </script>

</body>
</html>
