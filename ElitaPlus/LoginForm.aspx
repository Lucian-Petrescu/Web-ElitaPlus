<%@ Page Language="vb" AutoEventWireup="false" Codebehind="LoginForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.LoginForm"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Login - Elita+ System</title>
		<asp:literal id="Literal1" runat="server"></asp:literal>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="Styles.css" type="text/css" rel="STYLESHEET">
		<SCRIPT language="JavaScript" src="./Navigation/scripts/GlobalHeader.js"></SCRIPT>
		<script language="javascript">
			function doHandleOnLoad()
			{
				window.focus();
				document.forms[0].TextBoxUserId.focus();
			}
			
			function SwapMyImage(objectid,NuImage)
			{
				document.all[objectid].src = NuImage
			}
			
		</script>
	</HEAD>
	<body oncontextmenu="return false" bgColor="#ffffff" leftMargin="0" topMargin="0" onload="doHandleOnLoad();"
		rightMargin="0">
		<form id="Form2" method="post" runat="server">
			<table cellSpacing="0" cellPadding="0" width="100%" border="0">
				<tr>
					<td nowrap vAlign="top" bgcolor="#d5d7e4" width="100%" background="Navigation/images/login_spacer.gif"
						height="92"><asp:Label id="lblApplicationInstanceName" style="LEFT: 100px; POSITION: absolute; TOP: 20px"
							runat="server"  ForeColor="#63648d">(Development Environment)</asp:Label><IMG height="92" src="Navigation/images/trans_spacer.gif" width="100%"></td>
					<td nowrap vAlign="top" align="right"><IMG height="92" src="Navigation/images/login_header.gif"></td>
				</tr>
				<tr>
					<td nowrap width="100%" bgColor="#666666" colSpan="2" height="1"></td>
				</tr>
				<tr>
					<td valign="top" colspan="2" align="center">
						<table cellSpacing="0" cellPadding="0" width="100%" border="0">
							<!--<tr>
								<td valign="top" colSpan="2">&nbsp;</td>
							</tr>-->
							<tr>
								<td nowrap background="Navigation/images/login_back.gif" colspan="2" valign="top" align="center"
									height="290"><br>
									<br>
									<br>
									<br>
									<TABLE id="tblMain" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid"
										cellSpacing="0" cellPadding="4" rules="cols" width="400" align="center" bgColor="#f1f1f1"
										border="1">
										<TR>
											<TD>
												<table cellSpacing="0" cellPadding="0" width="100%" border="0">
													<tr>
														<td><asp:label id="lblLogin" runat="server"  >Development Environment Login</asp:label></td>
														<td align="right">&nbsp;</td>
													</tr>
												</table>
											</TD>
										</TR>
										<tr>
											<td align="center">
												<table cellSpacing="2" cellPadding="2" width="50%" border="0">
													<tr>
														<td align="right" nowrap><asp:label id="lblUserName" runat="server">User Id:&nbsp;&nbsp;</asp:label></td>
														<td><asp:textbox id="TextBoxUserId" runat="server" Width="165px"></asp:textbox></td>
													</tr>
													<!--<tr>
														<td align="right"><asp:label id="lblPassword" runat="server">Password:&nbsp;&nbsp;</asp:label></td>
														<td><asp:textbox id="txtPassword" runat="server" Width="165px" TextMode="Password"></asp:textbox></td>
													</tr>-->
													<tr>
														<td>&nbsp;</td>
														<td align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:imagebutton id="ButtonOk" runat="server" style="cursor:hand;" ImageUrl="Navigation/images/loginBtnUp.gif"></asp:imagebutton></td>
													</tr>
													<tr>
														<td>&nbsp;</td>
														<td><asp:label id="lblMessage" runat="server" ForeColor="red" ></asp:label></td>
													</tr>
												</table>
											</td>
										</tr>
									</TABLE>
									<br>
									<table cellSpacing="0" cellPadding="0" align="center" border="0">
										<tr>
											<td align="center">
												<p><font color="gray">* This site has been optimized for Internet Explorer.</font> 
													<!--<br>
													<br>
													<img alt="IE" src="images/IElogo.gif" border="0"></p>
												<p align="center">&nbsp;</p>--></p>
											</td>
										</tr>
									</table>
								</td>
							</tr>
							<!--<tr>
								<td COLSPAN="2">
									<div align="center">
										<p>&nbsp;</p>
										<p>Forgot your password?<br>
											<a href="/forgotPassword.asp"><font color="#0055aa">Click here</font></a> to 
											have your password emailed to you.
										</p>
									</div>
								</td>
							</tr>-->
						</table>
					</td>
				</tr>
				<tr>
					<td nowrap width="100%" bgColor="#666666" colSpan="2" height="1"></td>
				</tr>
				<tr>
					<td colspan="2" height="100%" background="Navigation/images/login_spacer.gif"><IMG src="Navigation/images/trans_spacer.gif" height="100%"></td>
				</tr>
				<tr>
					<td align="right" valign="bottom" colspan="2">
						<img src="Navigation/images/trans_spacer.gif" width="1"> <font color="gray">All 
							rights reserved. ©2018 Assurant Solutions.</font>
					</td>
				</tr>
			</table>
			<br>
			<br>
		</form>
	</body>
</HTML>
