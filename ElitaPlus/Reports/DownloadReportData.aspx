<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DownloadReportData.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.DownloadReportData" %>

<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>
</head>
<body>
    <form id="form1" runat="server">
        <div>
            <table style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid"
                cellspacing="0" cellpadding="0" width="98%" bgcolor="#d5d6e4" border="0">
                <tr>
                    <td valign="top">
                        <table width="100%" border="0">
                            <tr>
                                <td height="20">
                                    <asp:Label ID="Label1" CssClass="TITLELABEL" runat="server">Export Report Data</asp:Label>:&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>

            &nbsp;
        <div>
            <asp:Label ID="statusLabel" runat="server"></asp:Label>
            </div>
            <uc1:ErrorController ID="ErrorCtrl" runat="server"></uc1:ErrorController>
        </div>
    </form>
    <script language="JavaScript" src="../Navigation/Scripts/ReportCeMainScripts.js"></script>
</body>
</html>
