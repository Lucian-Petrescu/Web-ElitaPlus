<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TermAndConditionsForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.TermAndConditionsForm" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<HEAD>
		<title>TermAndConditionsForm</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="STYLESHEET">
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>
	</HEAD>
	<body leftMargin="0" topMargin="0" onload="changeScrollbarColor();ChangeTextBoxSize();" MS_POSITIONING="GridLayout">
    <form id="Form1" method="post" runat="server">
			<TABLE style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE width="100%" border="0">
							<TR>
								<TD height="20"><asp:label id="moTitleLabel1" runat="server" Cssclass="TITLELABEL">Tables:</asp:label>&nbsp;<asp:label id="moTitleLabel2" runat="server" Cssclass="TITLELABELTEXT">TERMS_AND_CONDITIONS</asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE class="TABLEOUTER" id="tblOuter2" height="93%" cellSpacing="0" cellPadding="0" rules="none"
				border="0">
				<tr>
					<td vAlign="top" align="center" height="100%">
						<TABLE class="TABLEMAIN" id="tblMain1" height="98%" cellSpacing="0" cellPadding="6" align="center"
							border="0">
								<TR>
									<TD height="1"></TD>
								</TR>							
							<TR>
								<TD align="center" colSpan="2">&nbsp;&nbsp;
									<uc1:errorcontroller id="ErrorCtrl" runat="server"></uc1:errorcontroller></TD>
							</TR>
							<TR>
								<TD vAlign="top" align="center" height="100%"><asp:panel id="EditPanel_WRITE" runat="server" Height="296px">
										<TABLE id="Table1" cellSpacing="1" cellPadding="0" width="100%" border="0">
											<TR>
												<TD align="left" colSpan="4">
													<TABLE id="Table5" cellSpacing="1" cellPadding="1" width="100%" border="0">
														<TR>
															<TD vAlign="top" noWrap align="right" width="10%">
																<asp:label id="LabelComment1" runat="server" Font-Bold="false">COMMENT</asp:label>&nbsp;
															</TD>
															<TD width="90%" valign="top">
																<%=urlStr%>
																<asp:textbox id="TextboxComment" tabIndex="44" runat="server" Height="280px" MaxLength="1000"
																	  TextMode="MultiLine" Rows="20" Width="99%"></asp:textbox>
															</TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
										</TABLE>
									</asp:panel></TD>
							</TR>
							<TR>
								<TD colSpan="2">
									<asp:label id="lblContractID" runat="server" Visible="False"></asp:label>
									<INPUT id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse" runat="server">
								</TD>
							</TR>
							<TR>
								<TD align="right" colSpan="2">
									<HR style="HEIGHT: 1px">
									<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="300" align="left" border="0">
										<TR>
											<TD><asp:button id="btnBack" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/back_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
													runat="server" Width="90px" CausesValidation="False" height="20px" CssClass="FLATBUTTON"
													Text="BACK"></asp:button></TD>
											<TD><asp:button id="btnApply_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/save_trans_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
													runat="server" Width="90px" height="20px" CssClass="FLATBUTTON" Text="SAVE"></asp:button></TD>
											<TD><asp:button id="btnUndo_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/cancel_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
													runat="server" Width="90px" CausesValidation="False" height="20px" CssClass="FLATBUTTON"
													Text="UNDO"></asp:button></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
    </form>
<script type="text/javascript">
    function ChangeTextBoxSize() 
    {
        var textbox = document.getElementById('TextboxComment');

        if (document.all) 
        {
            textbox.style.cssText = 'height:280px;width:' + (document.body.offsetWidth - 175) + 'px;';
        }
        else if (top.window.innerWidth) 
        {
            textbox.style.cssText = 'height:280px;width:' + (top.window.innerWidth - 175) + 'px;';
        }
    }
</script> 
</body>
</html>

