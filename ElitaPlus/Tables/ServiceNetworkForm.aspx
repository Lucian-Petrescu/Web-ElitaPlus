<%@ Register TagPrefix="uc1" TagName="UserControlAvailableSelected" Src="../Common/UserControlAvailableSelected.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ServiceNetworkForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ServiceNetworkForm" %>
<%@ Register TagPrefix="mytab" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>TemplateForm</title> <!-- ************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BOEditingWebForm.cst (9/20/2004)  ******************** -->
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
			<TABLE style="BORDER: black 1px solid; MARGIN: 5px; "
				cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE width="100%" border="0">
							<TR>
								<TD height="20">
									<P>&nbsp;
										<asp:label id="LabelTables" runat="server" Cssclass="TITLELABEL">Tables</asp:label>:
										<asp:label id="Label40" runat="server" Cssclass="TITLELABELTEXT">Service_Network</asp:label></P>
								</TD>
								<TD align="right" height="20"><STRONG>*</STRONG>
									<asp:label id="Label9" runat="server" Font-Bold="false">INDICATES_REQUIRED_FIELDS</asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="tblOuter2" style="BORDER: black 1px solid; MARGIN: 5px;"
				height="93%" cellSpacing="0" cellPadding="0" rules="none" width="98%" bgColor="#d5d6e4"
				border="0"> <!--d5d6e4-->
				<tr>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td vAlign="top" align="center"><asp:panel id="WorkingPanel" runat="server" Width="100%" Height="98%">
							<TABLE id="tblMain1" style="BORDER: #999999 1px solid; WIDTH: 727px;"
								cellSpacing="0" cellPadding="6" rules="cols" width="727" align="center" bgColor="#fef9ea"
								border="0">
								<TR>
									<TD vAlign="middle" align="center" colSpan="4">
										<uc1:ErrorController id="ErrorCtrl" runat="server"></uc1:ErrorController>&nbsp;</TD>
								</TR>
								<TR>
									<TD>
										<asp:Panel id="EditPanel_WRITE" runat="server" Width="100%" Height="100%">
											<TABLE id="Table1" style="WIDTH: 100%" cellSpacing="1" cellPadding="0" width="710" border="0">
												<TR>
													<TD vAlign="middle" align="right" width="15%">
														<asp:label id="LabelShortDesc" runat="server" Font-Bold="false" Width="100%">Code</asp:label></TD>
													<TD vAlign="middle" align="left" width="30%">&nbsp;
														<asp:textbox id="TextboxShortDesc" tabIndex="10" runat="server" Width="134px" CssClass="FLATTEXTBOX"></asp:textbox></TD>
													<TD vAlign="middle" align="right" width="15%">
														<asp:label id="LabelDescription" runat="server" Font-Bold="false" Width="100%">Description</asp:label></TD>
													<TD vAlign="middle" align="left" width="30%">&nbsp;
														<asp:textbox id="TextboxDescription" tabIndex="15" runat="server" Width="189px" CssClass="FLATTEXTBOX"></asp:textbox></TD>
												</TR>
												<TR>
													<TD colSpan="4" style="vertical-align:middle;text-align:center;padding-top:5px;">
														<HR>
														<uc1:UserControlAvailableSelected id="UserControlAvailableSelectedServiceCenters" runat="server"></uc1:UserControlAvailableSelected></TD>
												</TR>
												<TR>
													<TD vAlign="middle" align="left" colSpan="4"></TD>
												</TR>
											</TABLE>
										</asp:Panel></TD>
								</TR>
								<TR>
									<TD align="left" colSpan="4">
										<HR style="WIDTH: 100%; HEIGHT: 1px" SIZE="1">
									</TD>
								</TR>
								<TR>
									<TD vAlign="bottom" style="white-space: nowrap" align="left" height="20">
										<asp:button id="btnBack" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/back_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											tabIndex="185" runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" Text="Back"
											height="20px"></asp:button>&nbsp;
										<asp:button id="btnSave_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/save_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											tabIndex="190" runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" Text="Save"
											height="20px"></asp:button>&nbsp;
										<asp:button id="btnUndo_Write" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/cancel_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											tabIndex="195" runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" Text="Undo"
											height="20px"></asp:button>&nbsp;
										<asp:button id="btnNew_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											tabIndex="200" runat="server" Font-Bold="false" Width="81px" CssClass="FLATBUTTON" Text="New"
											height="20px"></asp:button>&nbsp;&nbsp;
										<asp:button id="btnCopy_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											tabIndex="205" runat="server" Width="136px" Height="20px" CssClass="FLATBUTTON" Text="NEW_WITH_COPY"
											CausesValidation="False"></asp:button>
										<asp:button id="btnDelete_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/clear_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											tabIndex="210" runat="server" Font-Bold="false" Width="100px" CssClass="FLATBUTTON" Text="Delete"
											height="20px"></asp:button></TD>
								</TR>
							</TABLE>
                        <INPUT id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
                               runat="server" DESIGNTIMEDRAGDROP="261"/>
						</asp:panel></td>
				</tr>
			</TABLE>
		</form>
		<script>
			//resizeScroller(document.getElementById("scroller"));
			
			function resizeScroller(item)
			{
				var browseWidth, browseHeight;
				
				if (document.layers)
				{
					browseWidth=window.outerWidth;
					browseHeight=window.outerHeight;
				}
				if (document.all)
				{
					browseWidth=document.body.clientWidth;
					browseHeight=document.body.clientHeight;
				}
				
				if (screen.width == "800" && screen.height == "600") 
				{
					newHeight = browseHeight - 220;
				}
				else
				{
					newHeight = browseHeight - 275;
				}
					
				item.style.height = String(newHeight) + "px";
				
				return newHeight;
			}
		</script>
		
	</body>
</HTML>
