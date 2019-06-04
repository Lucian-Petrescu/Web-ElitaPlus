<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ReportContainerForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ReportContainerForm"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<script language="javascript" type="text/javascript">
		function ChangeTitle() {
		    var doc_title = document.getElementById('hiddenTitle').value;
		    document.title = doc_title;
		}

		
	</script>
	<HEAD>
		<title>ReportContainerForm</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
	</HEAD>
	<body leftMargin="0" topmargin="0" MS_POSITIONING="GridLayout" onload="ChangeTitle()">
		<form id="Form1" method="post" runat="server">
			<iframe id="rptContainer" frameborder="0" name="ReportPDFFrame" width="100%" scrolling="auto" 
				runat="server" height="100%" APPLICATION="yes">
			</iframe>
            <INPUT id="hiddenTitle" type="hidden" name="HiddenTitle" runat="server">
		</form>
	</body>
</HTML>
