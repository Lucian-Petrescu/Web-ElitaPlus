<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="vscCoverageLimitForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.VSC.vscCoverageLimitForm"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>TemplateForm</title>
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
								<TD height="20"><asp:label id="Label1" runat="server" Cssclass="TITLELABEL">Tables</asp:label>:&nbsp;&nbsp;
								<asp:label id="Label2" runat="server"  Cssclass="TITLELABELTEXT" Width="184px">VSC_COVERAGE_LIMIT</asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<!--Search-List-->
			<TABLE id="moTableOuter" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				height="93%" cellSpacing="0" cellPadding="0" rules="none" width="98%" bgColor="#d5d6e4"
				border="0">
				<tr>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td vAlign="top" align="center" height="100%"><asp:panel id="moPanel" runat="server">
							<TABLE id="moTablelMain" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid"
								height="98%" cellSpacing="0" cellPadding="6" rules="cols" width="98%" align="center"
								bgColor="#fef9ea" border="0">
								<TR>
									<TD height="1"></TD>
								</TR>
								<TR>
									<TD vAlign="top" height="1">
										<uc1:ErrorController id="moErrorController" runat="server"></uc1:ErrorController></TD>
								</TR>
								<TR>
									<TD vAlign="top" colSpan="2">
										<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
											<TR>
												<TD vAlign="top" colSpan="2">
													<TABLE id="moTableSearch" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; WIDTH: 98%; BORDER-BOTTOM: #999999 1px solid; HEIGHT: 81px"
														cellSpacing="0" cellPadding="6" rules="cols" width="98%" align="center" bgColor="#f1f1f1"
														border="0">
														<TR>
															<TD>
																<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
																	<TR>
																		<TD style="WIDTH: 182px" align="left">
																			<asp:Label id="moLimitCodeLabel" runat="server">COVERAGE_LIMIT_CODE</asp:Label>:</TD>
																		<TD style="WIDTH: 300px" align="left">
																			<asp:Label id="moCoveTypeLabel" runat="server">COVERAGE_TYPE</asp:Label>:</TD>
																		<TD style="WIDTH: 200px" align="left">
																			<asp:label id="moMonthLabel" runat="server">MONTHS</asp:label>:</TD>
																		<TD style="WIDTH: 200px" align="left">
																			<asp:label id="moMileLabel" runat="server">KM_MILE</asp:label>:</TD>
																	</TR>
																	<TR>
																		<TD style="WIDTH: 182px" noWrap align="left" width="182">
																			<asp:TextBox id="moLimitCodeTx" runat="server" Width="90%"></asp:TextBox></TD>
																		<TD style="WIDTH: 300px" align="left" width="221">
																			<asp:dropdownlist id="moCoverageTypeDrop" runat="server" Width="90%" AutoPostBack="True"></asp:dropdownlist></TD>
																		<TD style="WIDTH: 200px" align="left">
																			<asp:TextBox id="moMonthTx" runat="server" Width="90%"></asp:TextBox></TD>
																		<TD style="WIDTH: 200px" noWrap align="left">
																			<asp:TextBox id="moKmTx" runat="server" Width="90%"></asp:TextBox></TD>
																	</TR>
																	<TR>
																		<TD style="WIDTH: 177px">&nbsp;</TD>
																	</TR>
																	<TR>
																		<TD style="WIDTH: 177px">&nbsp;</TD>
																		<TD style="WIDTH: 177px">&nbsp;</TD>
																		<TD align="right" colSpan="2">&nbsp;&nbsp;
																			<asp:button id="ClearButton" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/clear_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
																				runat="server" Font-Bold="false" Width="90px" Text="Clear" height="20px" CssClass="FLATBUTTON"></asp:button>&nbsp;&nbsp;
																			<asp:button id="SearchButton" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/search_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
																				runat="server" Font-Bold="false" Width="90px" Text="Search" height="20px" CssClass="FLATBUTTON"></asp:button></TD>
																	</TR>
																</TABLE>
															</TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
											<TR>
												<TD colSpan="2" height="5"></TD>
											</TR>
											<TR>
												<TD colSpan="2">
													<HR style="HEIGHT: 1px">
												</TD>
											</TR>
											<TR id="trPageSize" runat="server">
												<TD vAlign="top" align="left">
													<asp:label id="lblPageSize" runat="server">Page_Size</asp:label>: &nbsp;
													<asp:dropdownlist id="cboPageSize" runat="server" Width="50px" AutoPostBack="true">
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
												<TD align="right">
													<asp:label id="lblRecordCount" Runat="server"></asp:label></TD>
											</TR>
											<TR>
												<TD colSpan="2">
													<asp:DataGrid id="moDataGrid" runat="server" Width="100%" BorderStyle="Solid" BorderWidth="1px" OnItemCreated="ItemCreated"
														OnItemCommand="ItemCommand" BackColor="#DEE3E7" BorderColor="#999999" CellPadding="1" AllowPaging="True"
														AllowSorting="True" AutoGenerateColumns="False">
														<SelectedItemStyle Wrap="False" BackColor="LavenderBlush"></SelectedItemStyle>
														<EditItemStyle Wrap="False" BackColor="AliceBlue"></EditItemStyle>
														<AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
														<ItemStyle Wrap="False" BackColor="White"></ItemStyle>
														<HeaderStyle></HeaderStyle>
														<Columns>
															<asp:TemplateColumn>
																<HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
																<ItemTemplate>
																	<asp:ImageButton style="cursor:hand;" id="EditButton_WRITE" CommandName="EditRecord" ImageUrl="../Navigation/images/icons/edit2.gif"
																		runat="server"></asp:ImageButton>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:BoundColumn Visible="False" HeaderText="COVERAGE_LIMIT_ID"></asp:BoundColumn>
															<asp:BoundColumn ReadOnly="True"  SortExpression="coverage_limit_code"  HeaderText="COVERAGE_LIMIT_CODE">
																<HeaderStyle Width="150px"></HeaderStyle>
															</asp:BoundColumn>
															<asp:BoundColumn Visible="False" HeaderText="COVERAGE_TYPE_ID"></asp:BoundColumn>
															<asp:BoundColumn ReadOnly="True" SortExpression="description" HeaderText="COVERAGE_TYPE"></asp:BoundColumn>
															<asp:TemplateColumn HeaderText="MONTHS">
																<ItemTemplate>
																</ItemTemplate>
																<EditItemTemplate>
																	<asp:TextBox id="moMonthsTextGrid" cssclass="FLATTEXTBOX_TAB" runat="server" visible="True"></asp:TextBox>
																</EditItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="KM_MILE">
																<ItemTemplate>
																</ItemTemplate>
																<EditItemTemplate>
																	<asp:TextBox id="moKmTextGrid" cssclass="FLATTEXTBOX_TAB" runat="server" visible="True"></asp:TextBox>
																</EditItemTemplate>
															</asp:TemplateColumn>
														</Columns>
														<PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7" PageButtonCount="15"
															Mode="NumericPages"></PagerStyle>
													</asp:DataGrid></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD>
										<HR style="HEIGHT: 1px">
									</TD>
								</TR>
								<TR>
									<TD align="left">
										<asp:button id="SaveButton_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/save_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server" Font-Bold="false" Width="100px" Text="Save" height="20px" CssClass="FLATBUTTON"
											Visible="False"></asp:button>&nbsp;
										<asp:button id="CancelButton" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/cancel_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server" Font-Bold="false" Width="100px" Text="Cancel" height="20px" CssClass="FLATBUTTON"
											Visible="False"></asp:button>&nbsp;
									</TD>
								</TR>
							</TABLE>
						</asp:panel></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
