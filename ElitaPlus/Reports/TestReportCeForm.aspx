<%@ Page Language="vb" AutoEventWireup="false" Codebehind="TestReportCeForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.TestReportCeForm"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>TestReportCeForm</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="../Styles.css" type="text/css" rel="STYLESHEET">
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<asp:button id="Button1" style="Z-INDEX: 101; LEFT: 392px; POSITION: absolute; TOP: 128px" runat="server"
				Text="Get Report"></asp:button><input id="isReportCeVisible" type="hidden" name="isReportCeVisible" runat="server">
			<IFRAME id="moCeReportFrame" style="LEFT: 15px; VISIBILITY: hidden; OVERFLOW: hidden; WIDTH: 675px; PADDING-TOP: 0px; POSITION: absolute; TOP: 70px; HEIGHT: 450px"
				name="iframe1" marginWidth="0" src="" frameBorder="no" scrolling="no"></IFRAME>
		</form>
		<script>
		//debugger;
			if (document.all("isReportCeVisible").value != "True")
			{
				document.all.item('iframe1').style.display = 'none';
				document.all.item('iframe1').style.visibility = 'hidden';
				document.all.item('iframe1').src = "";
			}
			else
			{
				document.all.item('iframe1').style.display = '';
				document.all.item('iframe1').style.visibility = 'visible';
				document.all.item('iframe1').src = "ReportCeBaseForm.aspx";
			}
			
			function LoadReportCe()
			{
				//debugger;
				var url = "ReportCeBaseForm.aspx;"
				var frame = document.all.item('iframe1');
				frame.src = url;
				frame.style.display='';
				frame.visibility = 'visible';
				document.all("isReportCeVisible").value = "True";
			}
			
			function SaveReportCe(postalCodeFormat, sampleFormat)
			{
				document.all("TextboxFormat").value = postalCodeFormat;
				document.all("TextboxSampleFormat").value = sampleFormat;
				ClosePostalCodeFormat();
			}

			function CloseReportCe()
			{
				var frame = document.all.item('iframe1');
				frame.src='';
				frame.style.display='none';
				frame.style.visibility = 'hidden';
				document.all("isReportCeVisible").value = "False";
			}
			

		</script>
	</body>
</HTML>
