<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ReportCeProgressForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.ReportCeProgressForm" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>ReportCeProgressForm</title>
    <base target="_self" />
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Styles.css" type="text/css" rel="STYLESHEET">

    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>    
    <script language="JavaScript" src="../Navigation/Scripts/ReportCeScripts.js"></script> 

    <script>
		
		var intTimer;

		function LoadWaitMsg()
		{
			try
			{
				window.clearInterval(intTimer);
				elapsedTime = 0;
				document.body.style.cursor = 'default';
					
				intTimer = window.setInterval('ShowWaitMsg()',1000);
				document.body.style.cursor = 'wait';
				
			}
			catch(exception){}
			
		}
		
	//	LoadWaitMsg();
	//	ShowReportCeContainer();

		function ClearWaitMessage()
		{
			try
			{
				if(intTimer != undefined)
				{
					window.clearInterval(intTimer);
					elapsedTime = 0;
					document.body.style.cursor = 'default';
					
					var objDiv = document.all.item("divMessage");
					document.body.removeChild(objDiv);
				}
			}
			catch(exception){}
			
		}
		
		function cancel()
		{
			//alert("Cancel");
			parent.document.all('moReportCeInputControl_moReportCeStatus').value = "SUCCESS";
		}
    </script>

</head>
<body oncontextmenu="return true" style="background-repeat: repeat" leftmargin="0"
    topmargin="0" onload="" ms_positioning="GridLayout">
    <form id="Form2" method="post" runat="server">
    
       <asp:ScriptManager ID="ScriptManager2" runat="server" EnablePageMethods="true" ScriptMode="Auto" AsyncPostBackTimeout="100">
        <Scripts>
            <asp:ScriptReference Path="~/Navigation/scripts/AsyncTimer.js" />            
        </Scripts>
    </asp:ScriptManager> 
    
    <script language = "javascript">

       ReportInstanceStatus();
            
        </script>
        <table style="background-repeat: repeat" cellspacing="0" cellpadding="0" width="450"
            align="left" border="0">
            <tr>
                <td valign="middle" align="center" width="100%" height="10">
                    <table style="background-repeat: repeat" height="50" cellspacing="0" cellpadding="0"
                        width="100%" background="../Common/images/body_mid_back.jpg" border="0">
                        <tr>
                            <td width="2" height="3">
                                <img height="3" src="../Common/images/body_top_left.jpg" width="2"></td>
                            <td width="100%" background="../Common/images/body_top_back.jpg" height="3">
                                <img height="3" src="../Navigation/images/trans_spacer.gif" width="100%"></td>
                            <td width="2" height="3">
                                <img height="3" src="../Common/images/body_top_right.jpg" width="2"></td>
                        </tr>
                        <tr>
                            <td style="background-repeat: repeat" width="2" height="100%">
                                <img height="100%" src="../Common/images/body_mid_left.jpg" width="2"></td>
                            <td valign="middle" width="100%" background="../Common/images/body_mid_back.jpg">
                                <table style="background-repeat: repeat" cellspacing="2" cellpadding="2" width="100%"
                                    background="" border="0">
                                    <tr>
                                        <td valign="top">
                                            <!--<div id="scroller" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 368px">-->
                                            <table id="tblMain2" height="100" cellspacing="2" cellpadding="2" width="100%" bgcolor="#f4f3f8"
                                                border="0">
                                                <tr>
                                                    <td valign="middle" colspan="4" height="100">
                                                        <!--<DIV id="scroller" style="OVERFLOW-Y: auto; WIDTH: 100%; HEIGHT: 110px">-->
                                                        <table cellspacing="0" cellpadding="0" width="100%" background="" border="0">
                                                            <tr>
                                                                <td valign="middle" align="center">
                                                                    <table height="30" cellspacing="4" cellpadding="4" width="100%" background="" border="0">
                                                                        <tr>
                                                                            <td align="left">
                                                                                <img src="../Common/images/counterIcon.jpg"></td>
                                                                            <td align="right">
                                                                                <asp:RadioButton ID="RCancel" onclick="cancel();" Checked="false" runat="server"
                                                                                    Text="Cancel" TextAlign="right" AutoPostBack="false" Visible="False"></asp:RadioButton></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td id="tdBody" valign="middle" nowrap align="center" width="75%" colspan="2" height="15">
                                                                                <asp:Label ID="lblMessage" runat="server"></asp:Label></td>
                                                                        </tr>
                                                                         <tr>
                                                                            <td id="td1" valign="middle" nowrap align="center" width="75%" colspan="2" height="15">
                                                                                <asp:Label ID="lblStatus" runat="server"></asp:Label></td>
                                                                        </tr>
                                                                        <tr>
                                                                            <td valign="middle" nowrap align="center" colspan="2">
                                                                                <img id="ProgressBarImg" height="7" src="../Common/images/loading_pbar.gif" width="78"
                                                                                    name="ProgressBarImg"><br>
                                                                                <br>
                                                                                <asp:Label ID="lblCounter" runat="server" Height="11"></asp:Label></td>
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
                                </table>
                            </td>
                            <td width="2" height="100%">
                                <img height="100%" src="../Common/images/body_mid_right.jpg" width="2"></td>
                        </tr>
                        <tr>
                            <td width="2" height="3">
                                <img height="3" src="../Common/images/body_bot_left.jpg" width="2"></td>
                            <td width="100%" background="../Common/Images/body_bot_back.jpg" height="3">
                                <img height="3" src="../Navigation/images/trans_spacer.gif" width="100%"></td>
                            <td width="2" height="3">
                                <img height="3" src="../Common/images/body_bot_right.jpg" width="2"></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <iframe id="moCeReportFrame" style="left: 15px; visibility: hidden; overflow: hidden;
            width: 1px; padding-top: 0px; position: absolute; top: 1px; height: 1px" name="iframe1"
            marginwidth="0" src="" scrolling="no"></iframe>      
           <input id="moReportErrorButton" type="hidden" name="moReportErrorButton" runat="server"/>
           <input id="moReportStatus" type="hidden" name="moReportStatus" runat="server"/>
           <input id="moReportErrorMsg" type="hidden" name="moReportErrorMsg" runat="server"/>
    </form>
</body>
</html>

