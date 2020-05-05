<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="mytab" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CoverageByCompanyGroupForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.CoverageByCompanyGroupForm"%>
<%@ Register TagPrefix="uc1" TagName="UserControlAvailableSelected" Src="../Common/UserControlAvailableSelected.ascx" %>
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
			<TABLE style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE width="100%" border="0">
							<TR>
								<TD height="20">
									<P>&nbsp;
										<asp:label id="LabelTables" runat="server" Cssclass="TITLELABEL">Tables</asp:label>:
										<asp:label id="Label40" runat="server" Cssclass="TITLELABELTEXT">Coverage_By_Company_Group</asp:label></P>
								</TD>
								<TD align="right" height="20"><STRONG>*</STRONG>
									<asp:label id="Label9" runat="server" Font-Bold="false">INDICATES_REQUIRED_FIELDS</asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="tblOuter2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				height="93%" cellSpacing="0" cellPadding="0" rules="none" width="98%" bgColor="#d5d6e4"
				border="0"> <!--d5d6e4-->
				<tr>
					<td align="center">&nbsp;</td>
				</tr>
				<tr>
					<td vAlign="top" align="center"><asp:panel id="WorkingPanel" runat="server" Height="98%" Width="100%">
							<TABLE id="tblMain1" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid"
								height="98%" cellSpacing="0" cellPadding="6" rules="cols" width="98%" align="center"
								bgColor="#fef9ea" border="0">
								<TR>
									<TD style="HEIGHT: 34px" vAlign="top" align="center" colSpan="4">
										<uc1:ErrorController id="ErrorCtrl" runat="server"></uc1:ErrorController>&nbsp;</TD>
								</TR>
								<TR>
									<TD align="center" style="vertical-align:top;">
										<asp:Panel id="EditPanel_WRITE" runat="server" Width="100%" Height="11.77%" Wrap="False">
											<TABLE id="Table1" style="HEIGHT: 141px" cellSpacing="1" cellPadding="0" width="100%" border="0">
												<TR>
													<TD style="WIDTH: 162px; HEIGHT: 19px" align="right">
														<asp:label id="LabelCompanyGroup" runat="server" Width="96px">COMPANY_GROUP</asp:label></TD>
													<TD style="WIDTH: 617px; HEIGHT: 19px" align="left">&nbsp;
														<asp:textbox id="txtCompanyGroup" tabIndex="10" runat="server" Width="192px" CssClass="FLATTEXTBOX"></asp:textbox>
														<asp:DropDownList id="cboCompanyGroupId" runat="server" Width="192px" AutoPostBack="True"></asp:DropDownList></TD>
												</TR>
												<TR>
													<TD style="HEIGHT: 55px" vAlign="middle" align="center" colSpan="4">
														<HR width="100%" SIZE="2">
														&nbsp;
														<uc1:UserControlAvailableSelected id="UserControlAvailableSelectedCoverageType" runat="server"></uc1:UserControlAvailableSelected>&nbsp;&nbsp;
														<HR width="100%" SIZE="2">
													</TD>
												</TR>
												<TR>
													<TD style="WIDTH: 192px" align="left" colSpan="2" rowSpan="1">&nbsp;
													</TD>
												</TR>
												<TR>
													<TD style="WIDTH: 162px" align="right"></TD>
													<TD align="left" colSpan="2">&nbsp;
													</TD>
												</TR>
											</TABLE>
										</asp:Panel></TD>
								</TR>
								<TR>
									<TD style="HEIGHT: 17px" align="left" colSpan="4">
										<HR style="WIDTH: 100%; HEIGHT: 1px" SIZE="1">
									</TD>
								</TR>
								<TR>
									<TD vAlign="bottom" noWrap align="left" height="20">
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
									</TD>
								</TR>
							</TABLE>
                        <INPUT id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
                               runat="server"/>
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
		</TR></TABLE>
		<DIV></DIV>
		</TR></TABLE></TR></TABLE></FORM>
	</body>
</HTML>
