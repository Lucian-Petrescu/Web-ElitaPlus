<%@ Page Language="vb" AutoEventWireup="false" Codebehind="EarningPatternCodeForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.EarningPatternCodeForm"%>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>EarningPatternCodeForm</title>
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
			<TABLE style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; WIDTH: 98%; BORDER-BOTTOM: black 1px solid; HEIGHT: 20px"
				cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE width="100%" border="0">
							<TR>
								<TD height="20"><asp:label id="TablesLabel" runat="server" Font-Bold="false">Tables</asp:label>:&nbsp;<asp:label id="MaintainEarningCodeLabel" runat="server"  ForeColor="#12135B">EARNING_PATTERN</asp:label></TD>
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
								height="98%" cellSpacing="0" cellPadding="6" width="98%" align="center" bgColor="#fef9ea"
								border="0">
								<TR>
									<TD vAlign="top" height="1">
										<uc1:ErrorController id="ErrController" runat="server"></uc1:ErrorController></TD>
								</TR>
								<TR>
									<TD vAlign="top" align="center" width="100%">
										<TABLE cellSpacing="2" cellPadding="2" width="100%" align="center" border="0">
											<TR>
												<TD align="center" colSpan="2">
													<TABLE id="tblSearch" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; WIDTH: 98%; BORDER-BOTTOM: #999999 1px solid; HEIGHT: 76px"
														cellSpacing="0" cellPadding="6" width="98%" align="center" bgColor="#f1f1f1" border="0">
														<TR>
															<TD>
																<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
																	<TR>
																		<TD noWrap align="left"></TD>
																		<TD noWrap align="left"></TD>
																		<TD noWrap align="left"></TD>
																		<TD noWrap align="left"></TD>
																	</TR>
																	<TR>
																		<TD style="HEIGHT: 16px" noWrap align="left">
																			<asp:label id="searchCodeLabel" runat="server">Code</asp:label>:</TD>
																		<TD style="HEIGHT: 16px" noWrap align="left">
																			<asp:label id="searchDescriptionLabel" runat="server">Description</asp:label>:</TD>
																		<TD style="HEIGHT: 16px" noWrap align="left"></TD>
																		<TD style="HEIGHT: 16px" noWrap align="left"></TD>
																	</TR>
																	<TR>
																		<TD noWrap align="left" width="30%">
																			<asp:textbox id="SearchCodeTextBox" runat="server" AutoPostBack="False" Height="20px" Width="120px"></asp:textbox></TD>
																		<TD noWrap align="left" width="40%">
																			<asp:textbox id="SearchDescriptionTextBox" runat="server" AutoPostBack="False" Height="20px"
																				Width="220px"></asp:textbox></TD>
																		<TD noWrap align="right" width="15%">
																			<asp:button id="ClearButton" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/clear_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
																				runat="server" Font-Bold="false" Width="90px" Text="Clear" height="20px" CssClass="FLATBUTTON"></asp:button></TD>
																		<TD noWrap align="right" width="15%">
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
												<TD colSpan="2">
													<HR style="HEIGHT: 1px">
												</TD>
											</TR>
											<TR id="trPageSize" runat="server">
												<TD vAlign="top" align="left">
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
												<TD align="right">
													<asp:label id="lblRecordCount" Runat="server"></asp:label></TD>
											</TR>
											<TR>
												<TD colSpan="2">
													<asp:DataGrid id="Grid" runat="server" Width="100%" autogeneratecolumns="False" borderwidth="1px"
														OnItemCommand="ItemCommand" OnItemCreated="ItemCreated" BorderStyle="Solid" BackColor="#DEE3E7"
														BorderColor="#999999" CellPadding="1" AllowSorting="True" AllowPaging="True">
														<SelectedItemStyle Wrap="False" BackColor="LavenderBlush"></SelectedItemStyle>
														<EditItemStyle Wrap="False" BackColor="AliceBlue"></EditItemStyle>
														<AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
														<ItemStyle Wrap="False" BackColor="White"></ItemStyle>
														<HeaderStyle></HeaderStyle>
														<Columns>
															<asp:TemplateColumn>
																<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
																<ItemTemplate>
																	<asp:ImageButton id="EditButton_WRITE" style="CURSOR: hand" runat="server" ImageUrl="../Navigation/images/icons/edit2.gif"
																		CommandName="EditRecord"></asp:ImageButton>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn>
																<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
																<ItemTemplate>
																	<asp:ImageButton style="cursor:hand;" id="DeleteButton_WRITE" ImageUrl="../Navigation/images/icons/trash.gif"
																		runat="server" CommandName="DeleteRecord"></asp:ImageButton>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn Visible="False" HeaderText="Id">
																<ItemTemplate>
																	&gt;
																	<asp:Label id=IdLabel runat="server" text='<%# GetGuidStringFromByteArray(Container.DataItem("EARNING_CODE_ID"))%>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="Code" HeaderText="CODE">
																<ItemStyle HorizontalAlign="Left" Width="27%"></ItemStyle>
																<ItemTemplate>
																	<asp:Label id=CodeLabel runat="server" visible="True" text='<%# Container.DataItem("CODE")%>'>
																	</asp:Label>
																</ItemTemplate>
																<EditItemTemplate>
																	<asp:TextBox id="CodeTextBox" runat="server" visible="True"></asp:TextBox>
																</EditItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="Description" HeaderText="DESCRIPTION">
																<ItemStyle HorizontalAlign="Left" Width="60%"></ItemStyle>
																<ItemTemplate>
																	<asp:Label id=DescriptionLabel runat="server" visible="True" text='<%# Container.DataItem("DESCRIPTION")%>'>
																	</asp:Label>
																</ItemTemplate>
																<EditItemTemplate>
																	<asp:TextBox id="DescriptionTextBox" runat="server" visible="True"></asp:TextBox>
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
									<TD align="left">
										<HR style="WIDTH: 100%; HEIGHT: 1px" SIZE="1">
									</TD>
								</TR>
								<TR>
									<TD align="left">
										<asp:button id="NewButton_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server" Font-Bold="false" Width="100px" Text="New" height="20px" CssClass="FLATBUTTON"></asp:button>
										<asp:button id="SaveButton_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/save_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server" Font-Bold="false" Width="100px" Text="Save" height="20px" CssClass="FLATBUTTON"></asp:button>&nbsp;
										<asp:button id="CancelButton" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/cancel_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server" Font-Bold="false" Width="100px" Text="Cancel" height="20px" CssClass="FLATBUTTON"></asp:button>&nbsp;
									</TD>
								</TR>
							</TABLE>
						</asp:panel></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
