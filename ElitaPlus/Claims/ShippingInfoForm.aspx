<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ShippingInfoForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ShippingInfoForm"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ShippingInfoForm</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="STYLESHEET">
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>
	</HEAD>
	<body leftMargin="0" topMargin="0" onload="changeScrollbarColor();" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--Start Header-->
			<TABLE style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE width="100%" border="0">
							<TR>
								<TD height="20">&nbsp;<asp:label id="TablesLabel" runat="server" CssClass="TITLELABEL">Claims</asp:label>:
									<asp:label id="MaintainShippingInfoLabel" runat="server"  CssClass="TITLELABELTEXT">SHIPPING_INFO</asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="tblOuter2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				height="93%" cellSpacing="0" cellPadding="0" rules="none" width="98%" bgColor="#d5d6e4"
				border="0"> <!--d5d6e4-->
				<tr>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td vAlign="top" align="center" height="100%"><asp:panel id="WorkingPanel" runat="server" Width="98%" Height="98%">
							<TABLE id="tblMain1" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid"
								cellSpacing="0" cellPadding="6" rules="cols" width="100%" align="center" bgColor="#fef9ea"
								border="0">
								<TR>
									<TD align="center" width="75%" colSpan="2">&nbsp;&nbsp;
										<uc1:ErrorController id="ErrController" runat="server"></uc1:ErrorController></TD>
								</TR>
								<TR>
									<TD style="HEIGHT: 198px">
										<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
											<TR>
												<TD>
													<TABLE id="tblHeader" cellSpacing="0" cellPadding="0" rules="cols" width="98%" align="center"
														bgColor="#fef9ea" border="0">
														<TR>
															<TD>
																<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
																	<TR>
																		<TD style="WIDTH: 15%; HEIGHT: 19px" align="right">
																			<asp:label id="LabelCredit_Card_Number" runat="server">CREDIT_CARD_NUMBER</asp:label></TD>
																		<TD style="WIDTH: 35%; HEIGHT: 19px" align="left">&nbsp;
																			<asp:textbox id="TextboxCredit_Card_Number" tabIndex="1" runat="server" Width="80px" MaxLength="4"></asp:textbox>
																			<asp:label id="Label1" runat="server">LAST_FOUR_DIGITS</asp:label></TD>
																		<TD style="WIDTH: 15%; HEIGHT: 19px" align="right">
																			<asp:label id="LabelAuthorization_Number" runat="server">AUTHORIZATION_NUMBER</asp:label></TD>
																		<TD style="WIDTH: 35%; HEIGHT: 19px" align="left">&nbsp;
																			<asp:textbox id="TextboxAuthorization_Number" tabIndex="2" runat="server" Width="90%" MaxLength="10"></asp:textbox></TD>
																	</TR>
																	<TR>
																		<TD align="right" width="1%">
																			<asp:label id="moAddress1Label" runat="server">Address1</asp:label></TD>
																		<TD style="WIDTH: 35%" align="left">&nbsp;
																			<asp:textbox id="moAddress1Text" tabIndex="3" runat="server" Width="90%"></asp:textbox></TD>
																		<TD align="right">
																			<asp:label id="moAddress2Label" runat="server">Address2</asp:label></TD>
																		<TD style="WIDTH: 35%" align="left">&nbsp;
																			<asp:textbox id="moAddress2Text" tabIndex="4" runat="server" Width="90%"></asp:textbox></TD>
																	</TR>
																	<TR>
																		<TD align="right" width="1%">
																			<asp:label id="moCityLabel" runat="server">City</asp:label></TD>
																		<TD style="WIDTH: 35%" align="left">&nbsp;
																			<asp:textbox id="moCityText" tabIndex="5" runat="server" Width="90%"></asp:textbox></TD>
																		<TD align="right">
																			<asp:label id="moRegionLabel" runat="server">State_Province</asp:label></TD>
																		<TD style="WIDTH: 35%" align="left">&nbsp;
																			<asp:dropdownlist id="moRegionDrop_WRITE" tabIndex="6" runat="server" Width="90%"></asp:dropdownlist></TD>
																	</TR>
																	<TR>
																		<TD align="right" width="1%">
																			<asp:label id="moPostalLabel" runat="server">Zip</asp:label></TD>
																		<TD align="left">&nbsp;
																			<asp:textbox id="moPostalText" tabIndex="7" runat="server" Width="80px"></asp:textbox></TD>
																		<TD align="right">
																			<asp:label id="moCountryLabel" runat="server">Country</asp:label></TD>
																		<TD align="left">&nbsp;
																			<asp:dropdownlist id="moCountryDrop_WRITE" tabIndex="8" runat="server" Width="90%" AutoPostBack="True"></asp:dropdownlist></TD>
																	</TR>
																	<TR>
																		<TD style="WIDTH: 15%" align="right">
																			<asp:label id="LabelPROCESSING_FEE" runat="server">PROCESSING_FEE</asp:label></TD>
																		<TD style="WIDTH: 35%" align="left">&nbsp;
																			<asp:textbox id="TextboxPROCESSING_FEE" tabIndex="9" runat="server" Width="80px" ReadOnly="True"></asp:textbox></TD>
																		<TD style="WIDTH: 15%" align="right">
																			<asp:label id="LabelTOTAL_CHARGE" runat="server">TOTAL_CHARGE</asp:label></TD>
																		<TD style="WIDTH: 35%" align="left">&nbsp;
																			<asp:textbox id="TextboxTOTAL_CHARGE" tabIndex="10" runat="server" Width="80px" ReadOnly="True"></asp:textbox></TD>
																	</TR>
																</TABLE>
															</TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
											<TR>
												<TD style="HEIGHT: 11px" vAlign="bottom" noWrap align="left"></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD align="left">
										<asp:button id="btnBack" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/back_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											tabIndex="37" runat="server" Font-Bold="false" Width="90px" height="20px" CssClass="FLATBUTTON"
											Text="Back" CausesValidation="False"></asp:button>&nbsp;
										<asp:button id="SaveButton_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/save_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server" Font-Bold="false" Width="100px" height="20px" CssClass="FLATBUTTON" Text="Save"></asp:button>&nbsp;
										<asp:button id="btnUndo_Write" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/cancel_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server" Font-Bold="false" Width="100px" height="20px" CssClass="FLATBUTTON" Text="Undo"></asp:button>&nbsp;
									</TD>
								</TR>
							</TABLE>
							<INPUT id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
								runat="server" DESIGNTIMEDRAGDROP="261"> <INPUT id="HiddenUserAuthorization" type="hidden" name="HiddenUserAuthorization" runat="server">
						</asp:panel></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
