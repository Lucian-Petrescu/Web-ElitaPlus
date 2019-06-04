<%@ Page Language="vb" AutoEventWireup="false" Codebehind="LogOutForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.LogOutForm"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>LogOutForm</title>
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<SCRIPT>
    function SMLogout()
	{
		document.cookie = "SMSESSION=;expires=Wednesday,1-Jan-1970 09:00:01 GMT;domain=.assurant.com;path=/";
		document.cookie = "SMONDENIEDREDIR=;expires=Wednesday,1-Jan-1970 09:00:01 GMT;domain=.assurant.com;path=/";
		document.cookie = "SMTRYNO=;expires=Wednesday,1-Jan-1970 09:00:01 GMT;domain=.assurant.com;path=/";
		document.cookie = "FORMCRED=;expires=Wednesday,1-Jan-1970 09:00:01 GMT;domain=.assurant.com;path=/";

		document.cookie = "SMSESSION=;expires=Wednesday,1-Jan-1970 09:00:01 GMT;domain=.atl0.assurant.com;path=/";
		document.cookie = "SMONDENIEDREDIR=;expires=Wednesday,1-Jan-1970 09:00:01 GMT;domain=.atl0.assurant.com;path=/";
		document.cookie = "SMTRYNO=;expires=Wednesday,1-Jan-1970 09:00:01 GMT;domain=.atl0.assurant.com;path=/";
		document.cookie = "FORMCRED=;expires=Wednesday,1-Jan-1970 09:00:01 GMT;domain=.atl0.assurant.com;path=/";

		document.cookie = "SMSESSION=;expires=Wednesday,1-Jan-1970 09:00:01 GMT;domain=.mia0.assurant.com;path=/";
		document.cookie = "SMONDENIEDREDIR=;expires=Wednesday,1-Jan-1970 09:00:01 GMT;domain=.mia0.assurant.com;path=/";
		document.cookie = "SMTRYNO=;expires=Wednesday,1-Jan-1970 09:00:01 GMT;domain=.mia0.assurant.com;path=/";
		document.cookie = "FORMCRED=;expires=Wednesday,1-Jan-1970 09:00:01 GMT;domain=.mia0.assurant.com;path=/";

		document.cookie = "SMSESSION=;expires=Wednesday,1-Jan-1970 09:00:01 GMT;domain=.assurant.net;path=/";
		document.cookie = "SMONDENIEDREDIR=;expires=Wednesday,1-Jan-1970 09:00:01 GMT;domain=.assurant.net;path=/";
		document.cookie = "SMTRYNO=;expires=Wednesday,1-Jan-1970 09:00:01 GMT;domain=.assurant.net;path=/";
		document.cookie = "FORMCRED=;expires=Wednesday,1-Jan-1970 09:00:01 GMT;domain=.assurant.net;path=/";

		document.cookie = "SMSESSION=;expires=Wednesday,1-Jan-1970 09:00:01 GMT;domain=.assurantpartners.com;path=/";
		document.cookie = "SMONDENIEDREDIR=;expires=Wednesday,1-Jan-1970 09:00:01 GMT;domain=.assurantpartners.com;path=/";
		document.cookie = "SMTRYNO=;expires=Wednesday,1-Jan-1970 09:00:01 GMT;domain=.assurantpartners.com;path=/";
		document.cookie = "FORMCRED=;expires=Wednesday,1-Jan-1970 09:00:01 GMT;domain=.assurantpartners.com;path=/";		

		return true;
	}	
		</SCRIPT>
	</HEAD>
	<body MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
		</form>
	</body>
</HTML>
