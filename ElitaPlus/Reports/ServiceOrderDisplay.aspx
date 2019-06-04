<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ServiceOrderDisplay.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ServiceOrderDisplay"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
    <HEAD>
        <title>ServiceOrderDisplay</title>
        <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
        <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
        <meta content="JavaScript" name="vs_defaultClientScript">
        <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
        <LINK href="../Styles.css" type="text/css" rel="STYLESHEET">
        <SCRIPT language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>
    </HEAD>
    <body MS_POSITIONING="GridLayout">
        <form id="Form1" method="post" runat="server">
            <uc1:errorcontroller id="ErrorCtrl" runat="server"></uc1:errorcontroller>
            <TABLE id="Table1" height="100%" cellSpacing="1" cellPadding="0" width="100%" border="0">
                <tr height="95%">
                    <td>
                        <IFRAME id="ReportFrame" style="WIDTH: 100%; HEIGHT: 95%" src="ServiceOrderPreview.aspx?file=file.pdf"
                            scrolling="auto"></IFRAME>
                    </td>
                </tr>
                <TR>
                    <TD>
                        <asp:button id="btnBack" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/back_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
                            tabIndex="37" runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" Text="Back"
                            height="20px"></asp:button>&nbsp; <input type="button" id="btnPrint" runat="server" onclick="ReportFrame.focus();ReportFrame.print();"
                            style="BACKGROUND-IMAGE: url(../Navigation/images/icons/printer_icon.gif); WIDTH: 90px; CURSOR: hand; BACKGROUND-REPEAT: no-repeat; HEIGHT: 20px;display:none;"
                            tabIndex="38" Class="FLATBUTTON" value="Print" title="Print">
                    </TD>
                </TR>
            </TABLE>
        </form>
    </body>
</HTML>
