<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PoliceStationForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.PoliceStationForm"%>
<%@ Register TagPrefix="uc1" TagName="UserControlAddress" Src="../Common/UserControlAddress.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Police_Station</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="STYLESHEET">
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>
	</HEAD>
	<body onresize="resizeForm(document.getElementById('scroller'));" leftMargin="0" topMargin="0"
		onload="changeScrollbarColor();" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
		<asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true"  ScriptMode="Auto">        
            <Scripts>
                <asp:ScriptReference Path="~/Navigation/scripts/ComunaSuggest.js" /> 
            </Scripts>
        </asp:ScriptManager>
			<!--Start Header-->
			<TABLE style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE width="100%" border="0">
							<TR>
								<TD height="20"><asp:label id="moTitleLabel1" runat="server" Cssclass="TITLELABEL">Tables</asp:label>:
									<asp:label id="moTitleLabel2" runat="server" Cssclass="TITLELABELTEXT">Police_Station</asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="tblOuter2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				height="93%" cellSpacing="0" cellPadding="0" rules="none" width="98%" bgColor="#d5d6e4"
				border="0">
				<tr>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td vAlign="top" align="center">
						<TABLE id="moOutTable" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid"
							height="98%" cellSpacing="0" cellPadding="6" width="98%" align="center" bgColor="#fef9ea"
							border="0">
							<tr>
								<td style="HEIGHT: 36px" align="center" colSpan="2">&nbsp;&nbsp;
									<uc1:errorcontroller id="ErrorCtrl" runat="server"></uc1:errorcontroller></td>
								<TD style="HEIGHT: 36px" align="center"></TD>
							</tr>
							<TR>
								<TD>
									<DIV id="scroller" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 365px" align="center"><asp:panel id="EditPanel_WRITE" runat="server" Height="320px">
											<TABLE id="Table1" style="WIDTH: 100%; HEIGHT: 168px" cellSpacing="1" cellPadding="0" width="100%"
												border="0">
												<TR>
													<TD style="HEIGHT: 3px" noWrap align="right" width="1%">&nbsp;
														<asp:label id="lblPoliceStationCode" runat="server">Police_Station_Code</asp:label></TD>
													<TD style="HEIGHT: 3px" width="20%">&nbsp;
														<asp:textbox id="txtPoliceStationCode" runat="server" Width="40%"></asp:textbox></TD>
													<TD style="HEIGHT: 3px" noWrap align="right" width="1%">
														<asp:label id="lblPoliceStationName" runat="server">Police_Station_Name</asp:label></TD>
													<TD style="HEIGHT: 3px" width="20%">&nbsp;
														<asp:textbox id="txtPoliceStationName" runat="server" Width="90%"></asp:textbox>&nbsp;
													</TD>
												</TR>
												<uc1:UserControlAddress id="moAddressController" runat="server"></uc1:UserControlAddress></TABLE>
										</asp:panel></DIV>
								</TD>
							</TR>
							<TR>
								<TD align="left" colSpan="2">
									<HR style="WIDTH: 100%; HEIGHT: 1px" SIZE="1">
								</TD>
							</TR>
							<TR>
                                <TD colSpan="4"><asp:label id="lblPoliceStationID" runat="server" Visible="False"></asp:label><asp:label id="moIsNewPoliceStationLabel" runat="server" Visible="False"></asp:label><INPUT id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
                                                                                                                                                                                                                          runat="server"/>
								</TD>
							</TR>
							<TR>
								<TD align="right" colSpan="2">
									<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="300" align="left" border="0">
										<TR>
											<TD><asp:button id="btnBack" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/back_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
													runat="server" Text="BACK" CssClass="FLATBUTTON" height="20px" CausesValidation="False"
													Width="90px"></asp:button></TD>
											<TD><asp:button id="btnApply_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/save_trans_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
													runat="server" Text="SAVE" CssClass="FLATBUTTON" height="20px" CausesValidation="False"
													Width="100px"></asp:button></TD>
											<TD><asp:button id="btnUndo_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/cancel_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
													runat="server" Text="UNDO" CssClass="FLATBUTTON" height="20px" CausesValidation="False"
													Width="90px"></asp:button></TD>
											<TD><asp:button id="btnNew_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
													runat="server" Text="New" CssClass="FLATBUTTON" height="20px" CausesValidation="False" Width="100px"></asp:button></TD>
											<TD><asp:button id="btnCopy_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
													runat="server" Text="New_With_Copy" CssClass="FLATBUTTON" height="20px" CausesValidation="False"
													Width="135px"></asp:button></TD>
											<TD><asp:button id="btnDelete_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/clear_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
													runat="server" Text="Delete" CssClass="FLATBUTTON" height="20px" CausesValidation="False"
													Width="100px"></asp:button></TD>
										</TR>
									</TABLE>
								</TD>
								<TD align="right"></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
				<TR>
					<TD vAlign="top" align="center"></TD>
				</TR>
			</TABLE>
			<script>
			resizeForm(document.getElementById("scroller"));
			</script>
		</form>
	</body>
</HTML>
