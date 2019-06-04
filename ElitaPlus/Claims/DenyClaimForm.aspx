<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DenyClaimForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.DenyClaimForm" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Deny_Claim</title>
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
			<!--Start Header-->
			<TABLE style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE width="100%" border="0">
							<TR>
								<TD height="20"><asp:label id="moTitleLabel1" runat="server" CssClass="TITLELABEL">Tables</asp:label>:
									<asp:label id="moTitleLabel2" runat="server"  CssClass="TITLELABELTEXT">Deny_Claim</asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="tblOuter2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				height="93%" cellSpacing="0" cellPadding="0" rules="none" width="98%" bgColor="#d5d6e4"
				border="1">
				<tr>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td vAlign="top" align="center">
						<TABLE id="moOutTable" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid"
							height="98%" cellSpacing="0" cellPadding="6" width="98%" align="center" bgColor="#fef9ea"
							border="0">
							<tr>
								 <td valign="top" align="center" width="98%" colspan="1" rowspan="1">
									<uc1:errorcontroller id="ErrorCtrl" runat="server"></uc1:errorcontroller></td>
							</tr>
							<TR valign =top >
								<TD valign="top" align="center" height="100%">
									<DIV id="scroller" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 365px" align="center">
									<asp:panel id="EditPanel_WRITE" runat="server">
											<TABLE id="Table1" style="WIDTH: 98%; HEIGHT: 98%" cellSpacing="1" cellPadding="0" 
												border="0">
												<tr><td></td></tr>
                                                 <tr valign="top">
                                                <td align="right" width="30%" height="20">*
                                                    <asp:Label ID="lblDeniedReason" runat="server" Font-Bold="false">Denied_Reason</asp:Label></td>
                                                <td>
                                                &nbsp; 
                                                <asp:DropDownList ID="cboDeniedReason" runat="server" Width="280px" AutoPostBack="False">
                                                </asp:DropDownList></td>
                                                 </tr>												
                                                 <tr height="5">
                                                 <td colspan=2>
                                                 </td>
                                                 </tr>
                                                <tr valign="top" >
                                                <td align="right" height="20">
														<asp:label id="lblInvoiceNumber" runat="server">Invoice_Numb</asp:label></td>
                                                <td>&nbsp;
												<asp:textbox id="txtInvoiceNumber" runat="server" Width="280" TabIndex="2"></asp:textbox>&nbsp;
													</td>
												</tr>
                                           
                                                 <tr valign="top">
                                                <td align="right" width="30%" height="20">
                                                    <asp:Label ID="lblPotFraudulent" runat="server" Font-Bold="false">Potentially_Fraudulent</asp:Label></td>
                                                <td>
                                                &nbsp; 
                                                <asp:DropDownList ID="cboFraudulent" runat="server" Width="280px" AutoPostBack="False">
                                                </asp:DropDownList></td>
                                                 </tr>												
                                                 <tr height="5">
                                                 <td colspan=2>
                                                 </td>
                                                 </tr>
                                                <tr valign="top" >
                                                <td align="right" height="20">
														<asp:label id="lblComplaint" runat="server">Complaint</asp:label></td>
                                                <td>&nbsp;
												<asp:DropDownList ID="cboComplaint" runat="server" Width="280px" AutoPostBack="False">
                                                </asp:DropDownList></td>
												</tr>
										</asp:panel></DIV>
								</TD>
							</TR>
							<TR valign="top">
								<TD align="left" colSpan="2" height="20">
									<HR style="WIDTH: 100%; HEIGHT: 1px" SIZE="1">
								</TD>
							</TR>
							<TR valign="top">
                            <td nowrap align="left" >
							<asp:button id="btnCancel" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/back_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
									runat="server" Text="BACK" CssClass="FLATBUTTON" height="20px" CausesValidation="False"
									Width="100px"></asp:button> &nbsp;
							<asp:button id="btnApply_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/save_trans_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
									runat="server" Text="SAVE" CssClass="FLATBUTTON" height="20px" CausesValidation="False"
									Width="100px"></asp:button></TD> 
							<%--<asp:button id="btnCancel" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/back_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
									runat="server" Text="BACK" CssClass="FLATBUTTON" height="20px" CausesValidation="False"
									Width="100px"></asp:button></TD>--%>
						</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
			<script>
			resizeForm(document.getElementById("scroller"));
			</script>
		</form>
	</body>
</HTML>
