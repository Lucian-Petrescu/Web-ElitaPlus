<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="SoftQuestionGroupList.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.SoftQuestionGroupList" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Soft Questions Group Form</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="STYLESHEET">
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>
	</HEAD>
	<body leftMargin="0" topMargin="0" onload="changeScrollbarColor();" border="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--Start Header-->
			<TABLE style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE width="100%" border="0">
							<TR>
								<TD height="20"><asp:label id="LabelTables" runat="server" Cssclass="TITLELABEL">Tables</asp:label>:&nbsp;
								<asp:label id="Label7" runat="server" Cssclass="TITLELABELTEXT">Soft Questions Group</asp:label></TD>
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
					<td vAlign="top" align="center" height="100%"><asp:panel id="WorkingPanel" runat="server">
							<TABLE id="tblMain1" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid"
								height="98%" cellSpacing="0" cellPadding="6" rules="cols" width="98%" align="center"
								bgColor="#fef9ea" border="0">
								<TR>
									<TD height="1"></TD>
								</TR>
								<TR>
									<TD align="center">
										<uc1:ErrorController id="ErrorCtrl" runat="server"></uc1:ErrorController></TD>
								</TR>
								<TR>
									<TD vAlign="top">
										<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
											<TR>
												<TD vAlign="top" colSpan="2">
													<TABLE id="tblSearch" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; WIDTH: 98%; BORDER-BOTTOM: #999999 1px solid; HEIGHT: 76px"
														cellSpacing="0" cellPadding="6" rules="cols" width="98%" align="center" bgColor="#f1f1f1"
														border="0">
														<TR>
															<TD>
																<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
																	<TR>
																		<TD noWrap align="left">
																			<asp:Label id="lblDescription" Runat="server">Description</asp:Label>:</TD>
																		<TD noWrap align="left" width="100%">&nbsp;
																			<asp:textbox id="SearchDescriptionTextBox" runat="server" cssclass="FLATTEXTBOX" AutoPostBack="False"
																				Width="93%"></asp:textbox></TD>
																		<TD noWrap align="right">
																			<asp:button id="btnClearSearch" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/clear_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
																				runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" height="20px" Text="Clear"></asp:button>&nbsp;
																			<asp:button id="btnSearch" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/search_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
																				runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" height="20px" Text="Search"></asp:button>&nbsp;&nbsp;
																		</TD>
																	</TR>
																</TABLE>
															</TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
											<TR>
												<TD colSpan="2">
													<HR style="HEIGHT: 1px">
												</TD>
											</TR>
											<TR id="trPageSize" runat="server">
												<TD style="HEIGHT: 22px" vAlign="top" align="left">
													<asp:label id="lblPageSize" runat="server">Page_Size</asp:label>: &nbsp;
													<asp:dropdownlist id="cboPageSize" runat="server" AutoPostBack="true" Width="50px">
														<asp:ListItem Value="5">5</asp:ListItem>
														<asp:ListItem Selected="True" Value="10">10</asp:ListItem>
														<asp:ListItem Value="15">15</asp:ListItem>
														<asp:ListItem Value="20">20</asp:ListItem>
														<asp:ListItem Value="25">25</asp:ListItem>
														<asp:ListItem Value="30">30</asp:ListItem>
														<asp:ListItem Value="35">35</asp:ListItem>
														<asp:ListItem Value="40">40</asp:ListItem>
														<asp:ListItem Value="45">45</asp:ListItem>
														<asp:ListItem Value="50">50</asp:ListItem>
													</asp:dropdownlist></TD>
												<TD style="HEIGHT: 22px" align="right">
													<asp:label id="lblRecordCount" Runat="server"></asp:label></TD>
											</TR>
											<TR>
												<TD colSpan="2">
													<asp:datagrid id="Grid" runat="server" Width="100%" OnItemCommand="ItemCommand" OnItemCreated="ItemCreated"
														AllowSorting="True" AllowPaging="True" CellPadding="1" BorderColor="#999999" BackColor="#DEE3E7"
														BorderWidth="1px" BorderStyle="Solid" AutoGenerateColumns="False">
														<SelectedItemStyle Wrap="False" BorderStyle="Solid" BorderColor="Orange" BackColor="LightSteelBlue"></SelectedItemStyle>
														<EditItemStyle Wrap="False"></EditItemStyle>
														<AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
														<ItemStyle Wrap="False" BackColor="White"></ItemStyle>
														<HeaderStyle></HeaderStyle>
														<Columns>
															<asp:TemplateColumn>
																<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="5%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
																<ItemTemplate>
																	<asp:ImageButton style="cursor:hand;" id="btnEdit" ImageUrl="../Navigation/images/icons/edit2.gif"
																		runat="server" CommandName="SelectAction"></asp:ImageButton>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:BoundColumn SortExpression="1" HeaderText="Description">
																<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="55%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Left"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn Visible="False" HeaderText="SOFT_GROUP_ID"></asp:BoundColumn>
														</Columns>
														<PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7" PageButtonCount="15"
															Mode="NumericPages"></PagerStyle>
													</asp:datagrid></TD>
											</TR>
											<TR>
												<TD></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD>
										<HR>
									</TD>
								</TR>
								<TR>
									<TD align="left">
										<asp:button id="btnAdd_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server" Font-Bold="false" Width="100px" CssClass="FLATBUTTON" height="20px" Text="New"
											CommandName="WRITE"></asp:button>&nbsp;
									</TD>
								</TR>
							</TABLE>
						</asp:panel></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
