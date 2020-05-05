<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ZipDistrictForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ZipDistrictForm" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>TemplateForm</title> <!-- ************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BOEditingWebForm.cst (10/22/2004)  ******************** -->
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="STYLESHEET">
		<SCRIPT type="text/javascript" language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>

        <link type="text/css" href="../Navigation/styles/jquery-ui.min.css" rel="stylesheet"/>
        <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.12.4.min.js" > </script>
        <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-ui.min.js" > </script>
        <link type="text/css" href="../Navigation/styles/jquery-ui.elita.css" rel="stylesheet"/>
        <script type="text/javascript">
        $(function () {
            $("#tabs").tabs({
                activate: function() {
                    var selectedTab = $('#tabs').tabs('option', 'active');
                    $("#<%= hdnSelectedTab.ClientID %>").val(selectedTab);
                },
                active: document.getElementById('<%= hdnSelectedTab.ClientID %>').value
            });
        });
        </script>         
	</HEAD>
	<body onresize="resizeForm(document.getElementById('scroller'));" leftMargin="0" topMargin="0"
		onload="changeScrollbarColor();" MS_POSITIONING="GridLayout" border="0">
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
										<asp:label id="Label40" runat="server"  Cssclass="TITLELABELTEXT">Zip_District</asp:label></P>
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
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td vAlign="middle" align="center"><asp:panel id="WorkingPanel" runat="server" Width="98%" Height="98%">
							<TABLE id="tblMain1" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; WIDTH: 100%; BORDER-BOTTOM: #999999 1px solid; HEIGHT: 95%"
								cellSpacing="0" cellPadding="6" rules="cols" width="100%" align="center" bgColor="#fef9ea"
								border="0">
								<TR>
									<TD style="HEIGHT: 4px" vAlign="middle" align="center" colSpan="4">
										<uc1:ErrorController id="ErrorCtrl" runat="server"></uc1:ErrorController>&nbsp;</TD>
								</TR>
								<TR>
									<TD height="1">
										<DIV id="scroller" style="OVERFLOW: auto; WIDTH: 98.44%; HEIGHT: 300px" align="center">
											<asp:Panel id="EditPanel_WRITE" runat="server" Width="98%">
												<TABLE id="Table1" style="WIDTH: 100%" cellSpacing="1" cellPadding="0" border="0">
													<TR>
														<TD vAlign="middle" align="right" width="15%">
															<asp:label id="moCountryLabel" runat="server" Font-Bold="false" Width="90%">Country</asp:label>:</TD>
														<TD vAlign="middle" align="left" width="75%" colSpan="3">&nbsp;
															<asp:label id="moCountryLabel_NO_TRANSLATE" runat="server" Width="134px"></asp:label>
															<asp:DropDownList id="moCountryDrop" runat="server"></asp:DropDownList></TD>
													</TR>
													<TR>
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
														<TD vAlign="middle" align="center" colSpan="4">
															<HR>
														</TD>
													</TR>
													<TR>
														<TD vAlign="middle" align="left" colSpan="4" style="background-color:#fef9ea;">
															<asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0" />
                                                    <div id="tabs" class="style-tabs-old style-tabs-oldBG">
                                                        <ul>
                                                        <li style="background:#d5d6e4"><a href="#tabZipCodes"><asp:Label ID="Label4" runat="server" CssClass="tabHeaderTextOld">Zip_Codes</asp:Label></a></li>                                                                
                                                        </ul>
                                                        <div id="tabZipCodes" style="background:#d5d6e4">
                                                            <TABLE id="Table2" cellSpacing="1" cellPadding="0" width="100%" border="0">
																<TR>
																	<TD vAlign="top" align="right" width="25%">
																		<asp:label id="LabelZipCodes" runat="server" Font-Bold="false" Width="100%">ZIP_CODES</asp:label></TD>
																	<TD width="25%" align="center">
																		<asp:ListBox id="ListBoxZipCodes" SelectionMode="Multiple" runat="server" Width="100%" Rows="15"></asp:ListBox>
																		<asp:button id="btnDeleteZipCode" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/clear_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
																			tabIndex="210" runat="server" Font-Bold="false" Width="100px" CssClass="FLATBUTTON" Text="Delete"
																			height="20px"></asp:button>
																	</TD>
																	<TD vAlign="top" width="50%">
																		<TABLE id="Table3" cellSpacing="1" cellPadding="0" width="100%" border="0">
																			<TR>
																				<TD width="80%">
																					<asp:RadioButtonList id="RadioButtonListSingleOrRange" runat="server" Height="15px" RepeatDirection="Horizontal"
																						AutoPostBack="true">
																						<asp:ListItem Value="Single" Selected="True">Single</asp:ListItem>
																						<asp:ListItem Value="Range">Range</asp:ListItem>
																						<asp:ListItem Value="Any">Any</asp:ListItem>
																					</asp:RadioButtonList></TD>
																				<TD width="20%"></TD>
																			</TR>
																			<TR>
																				<TD width="80%">
																					<asp:textbox id="TextboxFrom" tabIndex="15" runat="server" Width="60px" CssClass="FLATTEXTBOX"></asp:textbox>
																					<asp:Label id="LabelSeparator" runat="server">-</asp:Label>
																					<asp:textbox id="TextboxTo" tabIndex="15" runat="server" Width="60px" CssClass="FLATTEXTBOX"></asp:textbox>&nbsp;
																					<asp:button id="ButtonAddZipCode" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
																						tabIndex="200" runat="server" Font-Bold="false" Width="70px" CssClass="FLATBUTTON" Text="Add"
																						height="20px"></asp:button></TD>
																				<TD width="20%"></TD>
																			</TR>
																		</TABLE>
																	</TD>
																</TR>
															</TABLE>
                                                        </div>
                                                    </div>
														</TD>
													</TR>
												</TABLE>
											</asp:Panel></DIV>
									</TD>
								</TR>
								<TR>
									<TD align="left" colSpan="4">
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
			resizeForm(document.getElementById("scroller"));
		</script>
	</body>
</HTML>
