<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DealerProductDefinitionForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.DealerProductDefinitionForm" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Dealer Product Definition Report</title>
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
        <table style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid"
            cellspacing="0" cellpadding="0" width="98%" bgcolor="#d5d6e4" border="0">
            <tr>
                <td valign="top">
                    <table width="100%" border="0">
                        <tr>
                            <td height="20">
                                <asp:Label ID="LabelReports" runat="server" CssClass="TITLELABEL">Reports</asp:Label>:<asp:Label ID="Label7" runat="server" CssClass="TITLELABELTEXT">Dealer_Product_Definition</asp:Label></td>
                            <td height="20" align="right">*&nbsp;
									<asp:Label ID="moIndicatesLabel" runat="server" EnableViewState="true">INDICATES_REQUIRED_FIELDS</asp:Label></td>
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
                                            <td>
                                                <table id="tblSearch" style="border-right: #999999 1px solid; border-top: #999999 1px solid; border-left: #999999 1px solid; width: 628px; border-bottom: #999999 1px solid; height: 64px"
                                                    cellspacing="2" cellpadding="8" rules="cols" width="628" align="center" bgcolor="#fef9ea"
                                                    border="0">
                                                    <tbody>
                                                        <tr>
                                                            <td>
                                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                    <tr>
                                                                        <td align="center">
                                                                            <uc1:ErrorController ID="ErrorCtrl" runat="server"></uc1:ErrorController>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td nowrap align="right">&nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <!--<TR>
																		<TD noWrap width="450" colSpan="2">&nbsp;</TD>
																		<TD noWrap align="right">
																			<asp:label id="lblDistChannel" runat="server">Sort By:</asp:label>&nbsp;
																			<asp:dropdownlist id="cboDistChannel" runat="server" Width="154px" AutoPostBack="False"></asp:dropdownlist></TD>
																	</TR>-->

                                                                </table>
                                                                <uc1:ReportCeInputControl ID="moReportCeInputControl" runat="server"></uc1:ReportCeInputControl>
                                                            </td>
                                                        </tr>
                                                    </tbody>
                                                </table>

                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td>&nbsp;
										<table id="table1" cellspacing="2" cellpadding="0" width="75%" border="0" align="center">
                                            <tr>
                                                <td nowrap align="center">
                                                    <uc1:MultipleColumnDDLabelControl ID="multipleDropControl" runat="server"></uc1:MultipleColumnDDLabelControl>
                                                </td>
                                            </tr>
                                        </table>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <hr>
                                    &nbsp;</td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td style="height: 11px"></td>
            </tr>
        <tr>
            <td align="left">&nbsp;
						<asp:Button ID="btnGenRpt" Style="background-image: url(../Navigation/images/viewIcon2.gif); cursor: hand; background-repeat: no-repeat"
                            runat="server" Width="100px" Text="View" Height="20px" CssClass="FLATBUTTON"></asp:Button></td>
        </tr>
        </table>

    </form>
    <script language="JavaScript" src="../Navigation/Scripts/ReportCeMainScripts.js"></script>
</body>
</html>
