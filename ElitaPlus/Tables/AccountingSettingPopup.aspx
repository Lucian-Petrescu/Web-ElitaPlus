<%@ Page Language="vb" AutoEventWireup="false" Codebehind="AccountingSettingPopup.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.AccountingSettingPopup"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>AccountingSettingPopup</title>
		<base target="_self">
		<meta name="GENERATOR" content="Microsoft Visual Studio .NET 7.1">
		<meta name="CODE_LANGUAGE" content="Visual Basic .NET 7.1">
		<meta name="vs_defaultClientScript" content="JavaScript">
		<LINK href="../Styles.css" type="text/css" rel="STYLESHEET">
		<meta name="vs_targetSchema" content="http://schemas.microsoft.com/intellisense/ie5">
		<SCRIPT language="JavaScript" src="../Navigation/scripts/GlobalHeader.js"></SCRIPT>
	</HEAD>
	<body oncontextmenu="return true" style="BACKGROUND-REPEAT: repeat" leftMargin="0" background="../common/Images/back_spacer.jpg" topMargin="0" onload="changeScrollbarColor();" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE cellSpacing="0" cellPadding="0" width="100%" align="right" border="0" style="BACKGROUND-REPEAT: repeat">
				<tr>
					<td colspan="2"><img src="../Navigation/images/trans_spacer.gif" height="8" width="1"></td>
				</tr>
				<TR>
					<td><img src="../Navigation/images/trans_spacer.gif" width="15" height="1"></td>
					<TD align="center" vAlign="middle" width="100%" height="50">
						<TABLE height="50" cellSpacing="0" cellPadding="0" width="100%" background="../common/images/body_mid_back.jpg"
							border="0" style="BACKGROUND-REPEAT: repeat">
							<TR>
								<TD width="2" height="3"><IMG height="3" src="../common/images/body_top_left.jpg" width="2"></TD>
								<TD width="100%" background="../common/images/body_top_back.jpg" height="3"><IMG height="3" src="../Navigation/images/trans_spacer.gif" width="100%"></TD>
								<TD width="2" height="3"><IMG height="3" src="../common/images/body_top_right.jpg" width="2"></TD>
							</TR>
							<TR>
								<TD width="2" style="BACKGROUND-REPEAT: repeat" height="100%"><IMG height="100%" src="../common/Images/body_mid_left.jpg" width="2"></TD>
								<TD vAlign="middle" width="100%" background="../common/images/body_mid_back.jpg">
									<TABLE cellSpacing="2" style="BACKGROUND-REPEAT: repeat" cellPadding="2" width="100%" background=""
										border="0">
										<TR>
											<TD vAlign="top"><!--<div id="scroller" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 368px">-->
												<TABLE id="tblMain2" height="100%" cellSpacing="2" cellPadding="2" width="100%" bgColor="#f4f3f8"
													border="0">
													<TR>
														<TD vAlign="middle" colSpan="4" height="100%">
															<!--<DIV id="scroller" style="OVERFLOW-Y: auto; WIDTH: 100%; HEIGHT: 110px">-->
															<TABLE cellSpacing="0" cellPadding="0" width="100%" background="" border="0">
																<TR>
																	<TD valign="middle" align="center">
																		<TABLE cellSpacing="4" cellPadding="4" width="100%" background="" border="0" height="105">
																			<TR>
																				<td valign="middle" align="center"><img runat="server" name="imgMsgIcon" id="imgMsgIcon" width="37" height="35" src="../Navigation/images/questionIcon.gif"></td>
																				<TD runat="server" valign="middle" align="left" width="100%" height="105">
																					Do you want to create a accounting setting for?
																				</TD>
																			</TR>
																		</TABLE>
																	</TD>
																</TR>
															</TABLE>
															<!--</DIV>-->
														</TD>
													</TR>
												</TABLE> <!--</div>--></TD>
										</TR>
										<TR>
											<TD height="2"></TD>
										</TR>
										<TR>
											<TD width="100%" height="2"><IMG height="2" src="../common/Images/div_spacer2.jpg" width="100%"></TD>
										</TR>
										<TR>
											<TD height="2"></TD>
										</TR>
										<TR>
											<TD runat="server" align="center">
												<asp:button id="btnDealer" runat="server" Text="Dealer" CommandName="WRITE" height="20px" CssClass="FLATBUTTON"
													Width="100px" Font-Bold="false"></asp:button>&nbsp;
												<asp:button id="btnServiceCenter" runat="server" Text="Service_Center" CommandName="WRITE" height="20px"
													CssClass="FLATBUTTON" Width="100px" Font-Bold="false"></asp:button>&nbsp;&nbsp;
											</TD>
										</TR>
									</TABLE>
								</TD>
								<TD width="2" height="100%"><IMG height="100%" src="../common/Images/body_mid_right.jpg" width="2"></TD>
							</TR>
							<TR>
								<TD width="2" height="3"><IMG height="3" src="../common/Images/body_bot_left.jpg" width="2"></TD>
								<TD width="100%" background="../common/Images/body_bot_back.jpg" height="3"><IMG height="3" src="../Navigation/images/trans_spacer.gif" width="100%"></TD>
								<TD width="2" height="3"><IMG height="3" src="../common/Images/body_bot_right.jpg" width="2"></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<table width="100%">
				<tr>
					<td></td>
				</tr>
				<tr>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td>
					</td>
				</tr>
			</table>
		</form>
		</body>
</HTML>
