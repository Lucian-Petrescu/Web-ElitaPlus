<%@ Register TagPrefix="uc1" TagName="UserControlServiceCenterInfo" Src="../Certificates/UserControlServiceCenterInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ServiceCenterInfoForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ServiceCenterInfoForm" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>TemplateForm</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Styles.css" type="text/css" rel="STYLESHEET">
    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>
</head>
<body leftmargin="0" topmargin="0" onload="changeScrollbarColor();" ms_positioning="GridLayout"
    border="0">
    <form id="Form1" method="post" runat="server">
        <!--Start Header-->

        <table style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px; border-left: black 1px solid; width: 98%; border-bottom: black 1px solid; height: 20px"
            cellspacing="0" cellpadding="0" width="98%" bgcolor="#d5d6e4" border="0">
            <tr>
                <td valign="top">
                    <table width="100%" border="0">
                        <tr>
                            <td height="20">
                                <asp:Label ID="LabelTables" runat="server" CssClass="TITLELABEL">CLAIMS</asp:Label>:
									<asp:Label ID="Label1" runat="server" CssClass="TITLELABELTEXT">Service_Center</asp:Label>
                            </td>
                            <td align="right" height="20"><strong>*</strong>
                                <asp:Label ID="moIndicatesLabel" runat="server" Font-Bold="false" EnableViewState="true">INDICATES_REQUIRED_FIELDS</asp:Label>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table id="tblOuter2" style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid"
            cellspacing="0" cellpadding="0" rules="none" width="98%" height="93%" bgcolor="#d5d6e4"
            border="0">
            <!--d5d6e4-->
            <tr>
                <td height="5"></td>
            </tr>
            <tr>
                <td valign="top" align="center">
                    <asp:Panel ID="WorkingPanel" runat="server">
                        <table id="tblMain1" style="border-right: #999999 1px solid; border-top: #999999 1px solid; border-left: #999999 1px solid; border-bottom: #999999 1px solid"
                            height="98%" cellspacing="0" cellpadding="6" width="98%" align="center"
                            bgcolor="#fef9ea" border="0">

                            <tr>
                                <td valign="top" align="center" height="1">
                                    <uc1:ErrorController ID="ErrorCtrl" runat="server"></uc1:ErrorController>
                                </td>
                            </tr>

                            <tr>
                                <td valign="top">
                                    <asp:Panel ID="EditPanel_WRITE" runat="server">
                                        <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td style="width: 35%;" valign="middle" align="center" colspan="4"></td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="4" style="width: 35%;" valign="middle">
                                                    <uc1:UserControlServiceCenterInfo ID="UserControlServiceCenterInfo" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="4" style="width: 35%;" valign="middle"></td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <hr style="width: 100%; height: 1px" size="1">
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" nowrap align="left" height="20">&nbsp;
										<asp:Button ID="btnBack" Style="background-image: url(../Navigation/images/icons/back_icon.gif); cursor: hand; background-repeat: no-repeat"
                                            TabIndex="185" runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" Text="Back"
                                            Height="20px"></asp:Button>&nbsp;
                                </td>
                            </tr>
                            <tr>
                                <td height="5"></td>
                            </tr>
                        </table>
                        <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
                               runat="server" designtimedragdrop="261"/>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
