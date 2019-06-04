<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="FelitaInterfaceDetailForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.FelitaInterfaceDetailForm" %>

<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title id="rptWindowTitle" runat="server">FelitaInterfaceDetail</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Styles.css" type="text/css" rel="STYLESHEET">
    

    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>

    <style type="text/css">
        .style1
        {
            width: 31px;
        }
    </style>
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
                            <asp:Label ID="Label7" runat="server"  CssClass="TITLELABELTEXT">FELITA_INTERFACE_DETAIL</asp:Label>
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
                        cellpadding="6" rules="cols" width="98%" align="center" bgcolor="#fef9ea" border="0"
                        height="95%">
                        <tr>
                            <td height="1">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tr>
                                        <td colspan="4">
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
                                        <td colspan="4" style="height: 15px">
                                            <img height="15" src="../Navigation/images/trans_spacer.gif">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4" style="width: 99.43%; height: 1px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="4">
                                            <hr style="height: 1PX" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" colspan="4" width="100%">
                                            <table border="0" cellpadding="0" cellspacing="0" width="100%">
                                                <tr>
                                                    <td colspan="4" align=center>
                                                        <table border="0" cellpadding="0" cellspacing="0" width="65%">
                                                            <tr>
                                                                <td align="left" colspan="2" nowrap valign="top">
                                                                    <uc1:MultipleColumnDDLabelControl ID="multipleDropControl" runat="server" />
                                                                </td>
                                                                <td align="right" colspan="2">
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" style="height: 15px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4">
                                                        <hr style="height: 1px" />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" colspan="4" width="100%">
                                                        <table border="0" cellpadding="0" cellspacing="0" width="65%" align ="center">
                                                            <tr align="right">
                                                                <td align="right" nowrap valign="baseline">
                                                                    *
                                                                    <asp:Label ID="moBeginDateLabel" runat="server" >BEGIN_DATE</asp:Label>
                                                                    :
                                                                </td>
                                                                <td align="left" nowrap>
                                                                    &nbsp;
                                                                    <asp:TextBox ID="moBeginDateText" runat="server" AutoPostBack="true" CssClass="FLATTEXTBOX"
                                                                        TabIndex="1"></asp:TextBox>
                                                                    <asp:ImageButton ID="BtnBeginDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                                                        Width="20px" />
                                                                </td>
                                                               
                                                                <td align="right" nowrap valign="baseline">
                                                                    *
                                                                    <asp:Label ID="moEndDateLabel" runat="server" >END_DATE</asp:Label>
                                                                    :
                                                                </td>
                                                                <td align="left" nowrap>
                                                                    &nbsp;
                                                                    <asp:TextBox ID="moEndDateText" runat="server" AutoPostBack="true" CssClass="FLATTEXTBOX"
                                                                        TabIndex="1"></asp:TextBox>
                                                                    <asp:ImageButton ID="BtnEndDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                                                        Width="20px" />
                                                                </td>
                                                                <td width = "5%">&nbsp;</td>
                                                            </tr>
                                                        </table>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td colspan="4" style="height: 25px">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td align="right" colspan="4" width="100%">
                                                        <table border="0" cellpadding="0" cellspacing="0" width="65%" align="center">                                                            
                                                             <tr align="center">
                                                                <td align="center" nowrap valign="baseline">
                                                                    <asp:RadioButton ID="rAllFiles" runat="server" AutoPostBack="false" Checked="False"
                                                                        onclick="ToggleSingleDropDownSelection('cboFileName','rAllFiles',false);" Text="ALL_FILES"
                                                                        TextAlign="left" />
                                                                </td>                                                               
                                                                <td align="right" nowrap valign="baseline">
                                                                    <asp:Label ID="lblFileName" runat="server" >FILE_NAME</asp:Label>
                                                                    :
                                                                </td>
                                                                <td align="left" nowrap colspan ="2">
                                                                    &nbsp;
                                                                    <asp:DropDownList ID="cboFileName" runat="server" AutoPostBack="false" onchange="ToggleSingleDropDownSelection('cboFileName','rAllFiles',true);"
                                                                        TabIndex="1" Width="500px"> 
                                                                    </asp:DropDownList>
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
                            <td align="center" style="height: 60%; width: 100%;">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <hr />
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Button ID="btnGenRpt" runat="server" CssClass="FLATBUTTON" 
                                    Height="20px" Style="background-image: url(../Navigation/images/viewIcon2.gif);
                                    cursor: hand; background-repeat: no-repeat" Text="View" Width="100px" />
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
