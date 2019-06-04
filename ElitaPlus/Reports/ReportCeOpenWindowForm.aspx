<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ReportCeOpenWindowForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.ReportCeOpenWindowForm" %>

<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%--<%@ Register assembly="Microsoft.ReportViewer.WebForms, Version=12.0.0.0, Culture=neutral, PublicKeyToken=89845dcd8080cc91" namespace="Microsoft.Reporting.WebForms" tagprefix="rsweb" %>--%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>ReportCeOpenWindowForm</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="tblMain1" style="border-right: #999999 1px solid; border-top: #999999 1px solid; border-left: #999999 1px solid; border-bottom: #999999 1px solid"
            height="98%" cellspacing="0" cellpadding="6" width="98%" align="center" bgcolor="#fef9ea"
            border="0">
            <tr>
                <td valign="top" height="1">
                    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" AsyncPostBackTimeout="1200000"></asp:ScriptManager>
                    <RSWEB:ReportViewer ID="SSRSReportViewer" runat="server" Width="100%"/>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
