<%@ Page Language="vb" AutoEventWireup="false" Codebehind="HomeForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.HomeForm"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Navigation_AdminSplash</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="STYLESHEET">
		<style type="text/css">
			.hiddenPic { DISPLAY: none }
		</style>
		<SCRIPT language="JavaScript" src="../Navigation/scripts/GlobalHeader.js"></SCRIPT>
		<SCRIPT language="JavaScript">
		function SwapMyImage(objectid,NuImage)
		{
			document.all[objectid].src = NuImage
		}
		
		function Reload_Header()
		 { 
			
		//	window.parent.document.frames["Navigation_Header"].location.href = "Navigation_Header.aspx"
		//	window.parent.document.frames["Navigation_Side"].location.href = "Navigation_Side.aspx?nTabId=" +  document.all["txtNextPageID"].value
			
		}
		
		function Remove_Parent_Scrollbar()
		{
			//debugger;
			window.parent.document.frames['Navigation_Content'].frameElement.scrolling='no';
		}		
		</SCRIPT>
        <%--Preload the script and style files for Jquery UI control to speed up the page render--%>
        <link type="text/css" href="../Navigation/styles/jquery-ui.min.css" rel="stylesheet">
        <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.12.4.min.js"> </script>
        <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-ui.min.js"> </script>
        <link type="text/css" href="../Navigation/styles/jquery-ui.elita.css" rel="stylesheet">
	</HEAD>
	<body style="BACKGROUND-REPEAT: no-repeat" bgColor="white" leftMargin="0" topMargin="0" MS_POSITIONING="GridLayout" onload="changeScrollbarColor();">
		<form id="Form1" method="post" runat="server">
			<input id="txtNextPageID" type="hidden" name="txtNextPageID" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" height="100%">
				<tr>
					<td width="209" nowrap="" valign="top" height="100%">
						<table cellSpacing="0" cellPadding="0" height="100%" width="209">
							<tr>
								<td style="HEIGHT:2px" Width="209"></td>
							</tr>
							<tr>
								<td height="46" nowrap=""><asp:image id="imgCountryFlag" height="46" Width="209" runat="server" Visible="true"></asp:image></td>
							</tr>
							<tr>
								<td style="HEIGHT:2px" Width="209"></td>
							</tr>
							<tr>
								<td height="464" width="209" background="images/home_left_back.gif" style="text-align:center">
									<asp:button id="btnExit" CssClass="HOMEFLATBUTTON" style="BACKGROUND-POSITION: left center; Z-INDEX: 101; LEFT: 55px; BACKGROUND-IMAGE: url(./images/icons/exitIcon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat; POSITION: absolute; TOP: 288px; BACKGROUND-COLOR: #d3d5e2; border-style:outset" runat="server" Width="89" Height="25" Text="EXIT"></asp:button>
									<asp:dropdownlist id="cboLanguage" style="Z-INDEX: 102; LEFT: 30px; POSITION: absolute; TOP: 128px" runat="server" AutoPostBack="True" ForeColor="#5d7ba5"></asp:dropdownlist>
									<asp:label id="lblLanguageTitle" style="Z-INDEX: 103; LEFT: 35px; POSITION: absolute; TOP: 104px" runat="server" Visible="True">CHANGE MY LANGUAGE</asp:label>
									<asp:label id="lblEnvTitle" style="Z-INDEX: 123; LEFT: 13px; POSITION: absolute; TOP: 210px" runat="server" Visible="True">Environment:</asp:label>
									<asp:label id="lblEnvValue" style="Z-INDEX: 113; LEFT: 110px; POSITION: absolute; TOP: 210px" runat="server" Visible="True"></asp:label>
                                    <asp:label id="lblBuildTitle" style="Z-INDEX: 123; LEFT: 13px; POSITION: absolute; TOP: 230px" runat="server" Visible="True">Build:</asp:label>
									<asp:label id="lblBuildValue" style="Z-INDEX: 113; LEFT: 110px; POSITION: absolute; TOP: 230px" runat="server" Visible="True"></asp:label>
									<hr style="Z-INDEX:113;LEFT:19px;WIDTH:170px;POSITION:absolute;TOP:254px;HEIGHT:1px">
									<hr color="white" style="Z-INDEX:113;LEFT:19px;WIDTH:170px;POSITION:absolute;TOP:255px;HEIGHT:1px">
								</td>
							</tr>
							<tr>
								<td height="100%" width="209"><img width="209" height="100%" src="images/home_left_spacer.gif"></td>
							</tr>
							<tr>
								<td height="3" width="209"><img height="3" width="209" src="images/home_left_bottom.gif"></td>
							</tr>
						</table>
					</td>
					<td width="100%" nowrap="" valign="top">
						<table cellSpacing="0" cellPadding="0" border="0" width="100%">
							<tr>
								<td colspan="2" style="HEIGHT:50px">&nbsp;</td>
							</tr>
							<tr>
								<td colspan="2" align="center"><asp:image id="imgHomeSplash" runat="server" ImageUrl="images/countries/new_ArgentinaSplash.jpg"></asp:image></td>
							</tr>
							<tr>
								<td width="100%" align="center" nowrap=""><img src="./images/trans_spacer.gif" height="1" width="210"><asp:label width="123" id="Label12" style="color:#000;Z-INDEX:101;BACKGROUND-COLOR:white;" Runat="server">Extended Service Contract System</asp:label></td>
							</tr>
							<tr>
								<td colspan="2" style="HEIGHT:100px">&nbsp;</td>
							</tr>
							<tr height="1">
								<td colspan="2" width="100%" bgcolor="gray"></td>
							</tr>
							<tr height="1">
								<td colspan="2" width="100%" bgcolor="whitesmoke"></td>
							</tr>
						</table>
						<table cellSpacing="0" cellPadding="0" width="100%">
							<tr>
								<td align="center"><div style="MARGIN-TOP: 5px; MARGIN-BOTTOM: 15px; MARGIN-LEFT: 10px"><br>
										<asp:Label id="lblCopyright" runat="server" Visible="True"></asp:Label>
                                    <a target="_blank" href="http://www.assurant.com/inc/assurant/notice.html" rel="noopener noreferrer">
											<font color="#0066cc">Notice</font></a> | <a target="_blank" href="http://www.assurant.com/inc/assurant/privacy.html" rel="noopener noreferrer">
											<font color="#0066cc">Privacy Policy</font></a></div>
									<DIV></DIV>
								</td>
							</tr>
						</table>
						<asp:label width="123" id="Label1" ForeColor="#5b467f" style="FONT-WEIGHT:900;FONT-SIZE:18px;Z-INDEX:101;LEFT:-78px;POSITION:absolute;TOP:-45px;" Runat="server"></asp:label>
					</td>
				</tr>
			</table>
			<!--<table height="10%" width="95%" style="Z-INDEX: 125; POSITION: relative; TOP: 430px" cellSpacing="0"
				cellPadding="0" align="center" border="0">
				
			</table>-->
		</form>
	</body>
</HTML>
</table>-->
		</form>
	</body>
</HTML>
