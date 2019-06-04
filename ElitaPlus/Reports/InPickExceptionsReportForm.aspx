<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="InPickExceptionsReportForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.InPickExceptionsReportForm" %>
<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title id="rptWindowTitle" runat="server">CLAIMS_INPICK_EXCEPTIONS</title>
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
                            <asp:Label ID="Label7" runat="server"  CssClass="TITLELABELTEXT">CLAIMS_INPICK_EXCEPTIONS</asp:Label>
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
                        cellpadding="6" rules="cols" width="98%" align="center" bgcolor="#fef9ea" height="98%"
                        border="0">
                        <tr>
                            <td height="1">
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tr>
                                        <td colspan="3" valign ="top">
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
                                        <td style="width: 99.43%; height: 1px" colspan="3" valign="top">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" colspan="3" valign="top">
                                            <table border="0" cellpadding="0" cellspacing="1" width="75%">
                                                <tr>
                                                    <td colspan="4" align="center">
                                                        <table id="Table2" border="0" cellpadding="1" cellspacing="1" width="75%">                                                           
                                                            <tr>
                                                                <td colspan="4" height="20px">                                                                   
                                                                </td>
                                                            </tr>                                                           
                                                              <tr>                                                               
                                                                <td nowrap valign="baseline" colspan="4" align="left">
                                                                     <uc1:MultipleColumnDDLabelControl ID="moUserCompanyMultipleDrop" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4" height="20px">  
                                                                <hr style="width: 100%; height: 1px" />                                                                 
                                                                </td>
                                                            </tr> 
                                                            <tr>                                                                
                                                                <td align="LEFT" valign="bottom" nowrap colspan="1">
                                                                    *
                                                                    <asp:RadioButton ID="rdealer" runat="server" AutoPostBack="false" Checked="False"
                                                                        onclick="ToggleDualDropDownsSelection('moDealerMultipleDrop_moMultipleColumnDrop', 'moDealerMultipleDrop_moMultipleColumnDropDesc', 'rdealer', false, ''); document.all.item('moDealerMultipleDrop_lb_DropDown').style.color = '';"
                                                                        Text="SELECT_ALL_DEALERS" TextAlign="left" />                                                                   
                                                                </td>                                                            
                                                                <td nowrap valign="baseline" colspan="3" align="CENTER" width="60%">
                                                                    <uc1:MultipleColumnDDLabelControl ID="moDealerMultipleDrop" runat="server" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="4" height="20px">
                                                                    <hr style="width: 100%; height: 1px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
															<td colspan="4" align="left">&nbsp;
															 <asp:label id="moDaysActiveLabel" runat="server">PLEASE ENTER MINIMUM NUMBER OF DAYS ACTIVE (0 TO 999)</asp:label>: 
																&nbsp;&nbsp;
																<asp:TextBox id="txtActiveDays" Runat="server" Width="71px" CssClass="FLATTEXTBOX" MaxLength="3"></asp:TextBox></td>
														    </tr>
														     <tr>
                                                                <td colspan="4" height="20px">
                                                                    <hr style="width: 100%; height: 1px" />
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                 <td align="LEFT" valign="bottom" nowrap colspan="1">&nbsp;    
                                                                <asp:RadioButton id="rSvcenter" onclick="document.forms[0].txtActiveDays.value='';document.forms[0].txtActiveDays.disabled=true;" AutoPostBack="false"
																	Runat="server" TextAlign="left" GroupName="type"  Checked="True" Text="BY_SERVICE_CENTER"></asp:RadioButton>                                                      
                                                                </td>
                                                                 <td nowrap valign="baseline" colspan="3" align="center" width="60%">
                                                                 <asp:RadioButton id="rStore"  AutoPostBack="false" onclick="document.forms[0].txtActiveDays.disabled=false;"
																	Runat="server" TextAlign="left" GroupName="type" Text="OR_BY_STORE"></asp:RadioButton>    
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
                            <td valign=bottom>
                                <hr style="width: 99.43%; height: 1px" />
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
