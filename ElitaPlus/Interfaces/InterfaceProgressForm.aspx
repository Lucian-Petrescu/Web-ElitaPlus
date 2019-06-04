<%@ Page Language="vb" AutoEventWireup="false" Codebehind="InterfaceProgressForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Interfaces.InterfaceProgressForm"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>InterfaceProgressForm</title>
		<base target="_self">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="STYLESHEET">
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/InterfaceScripts.js"></SCRIPT>
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
	</HEAD>
	<body oncontextmenu="return true" style="BACKGROUND-REPEAT: repeat" leftMargin="0" topMargin="0"
		onload="" MS_POSITIONING="GridLayout">
		<form id="Form2" method="post" runat="server">
			<TABLE style="BACKGROUND-REPEAT: repeat" cellSpacing="0" cellPadding="0" width="300" align="left"
				border="0">
				<TR>
					<TD vAlign="middle" align="center" width="100%" height="10"><TABLE style="BACKGROUND-REPEAT: repeat" height="50" cellSpacing="0" cellPadding="0" width="100%"
							background="../Common/images/body_mid_back.jpg" border="0">
							<TR>
								<TD width="2" height="3"><IMG height="3" src="../Common/images/body_top_left.jpg" width="2"></TD>
								<TD width="100%" background="../Common/images/body_top_back.jpg" height="3"><IMG height="3" src="../Navigation/images/trans_spacer.gif" width="100%"></TD>
								<TD width="2" height="3"><IMG height="3" src="../Common/images/body_top_right.jpg" width="2"></TD>
							</TR>
							<TR>
								<TD style="BACKGROUND-REPEAT: repeat" width="2" height="100%"><IMG height="100%" src="../Common/images/body_mid_left.jpg" width="2"></TD>
								<TD vAlign="middle" width="100%" background="../Common/images/body_mid_back.jpg">
									<TABLE style="BACKGROUND-REPEAT: repeat" cellSpacing="2" cellPadding="2" width="100%" background=""
										border="0">
										<TR>
											<TD vAlign="top"><!--<div id="scroller" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 368px">-->
												<TABLE id="tblMain2" height="100" cellSpacing="2" cellPadding="2" width="100%" bgColor="#f4f3f8"
													border="0">
													<TR>
														<TD vAlign="middle" colSpan="4" height="100">
															<!--<DIV id="scroller" style="OVERFLOW-Y: auto; WIDTH: 100%; HEIGHT: 110px">-->
															<TABLE cellSpacing="0" cellPadding="0" width="100%" background="" border="0">
																<TR>
																	<TD vAlign="middle" align="center"><TABLE height="30" cellSpacing="4" cellPadding="4" width="100%" background="" border="0">
																			<tr>
																				<td align="left"><IMG src="../Common/images/counterIcon.jpg"></td>
																				<td align="right"><asp:RadioButton id="RCancel" onclick="cancel();" Checked="false" Runat="server" Text="Cancel" TextAlign="right"
																						AutoPostBack="false" Visible="False"></asp:RadioButton></td>
																			</tr>
																			<TR>
																				<TD id="tdBody" vAlign="middle" noWrap align="center" width="75%" colSpan="2" height="15"><asp:label id="lblMessage" Runat="server"></asp:label></TD>
																			</TR>
																			<tr>
																				<td vAlign="middle" noWrap align="center" colSpan="2"><IMG id="ProgressBarImg" height="7" src="../Common/images/loading_pbar.gif" width="78"
																						name="ProgressBarImg"><br>
																					<br>
																					<asp:label id="lblCounter" Runat="server" Height="11"></asp:label></td>
																			</tr>
																		</TABLE>
																	</TD>
																</TR>
															</TABLE>
														</TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
									</TABLE>
								</TD>
								<TD width="2" height="100%"><IMG height="100%" src="../Common/images/body_mid_right.jpg" width="2"></TD>
							</TR>
							<TR>
								<TD width="2" height="3"><IMG height="3" src="../Common/images/body_bot_left.jpg" width="2"></TD>
								<TD width="100%" background="../Common/Images/body_bot_back.jpg" height="3"><IMG height="3" src="../Navigation/images/trans_spacer.gif" width="100%"></TD>
								<TD width="2" height="3"><IMG height="3" src="../Common/images/body_bot_right.jpg" width="2"></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<IFRAME id="moInterfaceFrame" style="LEFT: 15px; VISIBILITY: hidden; OVERFLOW: hidden; WIDTH: 1px; PADDING-TOP: 0px; POSITION: absolute; TOP: 1px; HEIGHT: 1px"
				name="iframe1" marginWidth="0" src="" frameBorder="no" scrolling="no"></IFRAME>
		</form>
	</body>
</HTML>
