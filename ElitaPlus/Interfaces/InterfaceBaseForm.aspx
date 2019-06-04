<%@ Page Language="vb" AutoEventWireup="false" Codebehind="InterfaceBaseForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Interfaces.InterfaceBaseForm"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>InterfaceBaseForm</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="STYLESHEET">
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>
	</HEAD>
	<body leftMargin="0" topMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--Start Header-->
			<TABLE style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE width="100%" border="0">
							<TR>
								<TD height="20">&nbsp;<asp:label id="moTitleLabel" runat="server"  CssClass="TITLELABEL">Reports</asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="moTablelOuter" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				height="93%" cellSpacing="0" cellPadding="0" rules="none" width="98%" bgColor="#d5d6e4"
				border="0"> <!--d5d6e4-->
				<tr>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td vAlign="top" align="center" height="100%"><asp:panel id="moPanel" runat="server" Height="100px" Width="98%">
							<TABLE id="moTablelMain" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid"
								cellSpacing="0" cellPadding="6" width="100%" align="center" bgColor="#fef9ea" border="0">
								<TR>
									<TD align="center" height="1">
										<asp:Button id="btnHtmlHidden" style="BACKGROUND-COLOR: #fef9ea" runat="server" Height="2" Width="2"
											Text="btnHtmlHidden"></asp:Button>
										<asp:Button id="btnContinue" style="BACKGROUND-COLOR: #fef9ea" runat="server" Height="2" Width="2"
											Text="btnContinue"></asp:Button></TD>
								</TR>
							</TABLE>
						</asp:panel></td>
				</tr>
			</TABLE>
			<script>

			function buttonClick()
			{
				var btn = document.all.item('btnHTMLHidden');
				btn.click();
				return true;
			}
			
			function buttonContinueClick()
			{
				var btn = document.all.item("btnContinue");
		
				btn.click();
				return true;
            }

            function updateStatus(statusControlId, errmsgControlId, status, errmsg) {
                parent.parent.document.getElementById(statusControlId).value = status;
                parent.parent.document.getElementById(errmsgControlId).value = errmsg;
            }
			</script>
		</form>
	</body>
</HTML>
