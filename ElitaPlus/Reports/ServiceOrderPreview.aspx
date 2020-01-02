<%@Import Namespace="System.Xml" %>
<%@Import Namespace="System.Xml.Xsl"%>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ServiceOrderPreview.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ServiceOrderPreview"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
    <HEAD>
        <title></title>
        <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
        <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
        <meta content="JavaScript" name="vs_defaultClientScript">
        <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
        <SCRIPT language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>
    </HEAD>
    <body MS_POSITIONING="GridLayout">
        <form id="Form1" method="post" runat="server">
            <asp:Xml ID="xmlSource" Runat="server"></asp:Xml>
            <script language="javascript">
                parent.document.getElementById('btnPrint').style.display='';
            </script>
        </form>
    </body>
</HTML>
