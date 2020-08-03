<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PoliceStationListForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.PoliceStationListForm"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Police_Station</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="STYLESHEET">
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>
	</HEAD>
	<body leftMargin="0" topMargin="0" onload="changeScrollbarColor();" MS_POSITIONING="GridLayout"
		border="0">
		<form id="Form1" method="post" runat="server">
			<!--Start Header-->
			<TABLE style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE width="100%" border="0">
							<TR>
								<TD height="20"><asp:label id="LabelTables" runat="server"  Cssclass="TITLELABEL">Tables</asp:label>:&nbsp;
								<asp:label id="Label7" runat="server" Cssclass="TITLELABELTEXT">POLICE_STATION</asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="tblOuter2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid; HEIGHT: 93%"
				cellSpacing="0" cellPadding="0" rules="none" width="98%" bgColor="#d5d6e4" border="0"> <!--d5d6e4-->
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
									<TD style="HEIGHT: 1px" align="center">
										<uc1:ErrorController id="ErrorCtrl" runat="server"></uc1:ErrorController></TD>
								</TR>
								<TR>
									<TD vAlign="top">
										<TABLE style="HEIGHT: 100%" cellSpacing="0" cellPadding="0" width="100%" align="center"
											border="0">
											<TR>
												<TD style="HEIGHT: 84px" vAlign="top" colSpan="2">
													<TABLE id="tblSearch" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid; HEIGHT: 84px"
														cellSpacing="0" cellPadding="4" rules="cols" width="100%" align="center" bgColor="#f1f1f1"
														border="0">
														<TR>
															<TD vAlign="top" align="left" colSpan="1" rowSpan="1">
																<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
																	<TR>
																		<TD noWrap align="left">
																			<asp:Label id="moCountryLabel" runat="server">Country:</asp:Label></TD>
																		<TD noWrap align="left" width="30%">
																			<asp:Label id="lblDescription" Runat="server">Police_Station_Name:</asp:Label></TD>
																		<TD noWrap align="left" width="5%"></TD>
																		<TD noWrap align="left" width="25%">
																			<asp:Label id="lblCode" Runat="server">Police_Station_Code:</asp:Label></TD>
																		<TD noWrap align="left"></TD>
																	</TR>
																	<TR>
																		<TD noWrap align="left" colSpan="1" rowSpan="1">
																			<asp:DropDownList id="moCountryDrop" runat="server" Width="120px"></asp:DropDownList>
																		<TD noWrap align="left">
																			<asp:textbox id="SearchDescriptionTextBox" runat="server" Width="304px" Height="20px" AutoPostBack="False"></asp:textbox></TD>
																		<TD noWrap align="left"></TD>
																		<TD noWrap align="left">
																			<asp:textbox id="SearchCodeTextBox" runat="server" Width="150px" Height="20px" AutoPostBack="False"></asp:textbox></TD>
																		<TD noWrap align="left"></TD>
															</TD>
														</TR>
														<TR>
															<TD noWrap align="left"></TD>
															<TD noWrap align="left"></TD>
															<TD noWrap align="right"></TD>
															<TD noWrap align="right" colSpan="2" rowSpan="1">
																<asp:button id="btnClearSearch" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/clear_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
																	runat="server" Font-Bold="false" Width="90px" Text="Clear" CssClass="FLATBUTTON" height="20px"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;
																<asp:button id="btnSearch" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/search_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
																	runat="server" Font-Bold="false" Width="90px" Text="Search" CssClass="FLATBUTTON" height="20px"></asp:button></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD style="HEIGHT: 1px" colSpan="2"></TD>
								</TR>
								<TR>
									<TD style="HEIGHT: 14px" colSpan="2">&nbsp;
										<HR style="HEIGHT: 1px">
									</TD>
								</TR>
								<TR id="trPageSize" runat="server">
									<TD style="HEIGHT: 22px" vAlign="top" align="left">
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
									<TD style="HEIGHT: 22px" align="right">
										<asp:label id="lblRecordCount" Runat="server"></asp:label></TD>
								</TR>
								<TR>
									<TD vAlign="top" align="center" colSpan="2">
										<asp:datagrid id="Grid" runat="server" Width="100%" AllowSorting="True" AllowPaging="True" BorderWidth="1px"
											CellPadding="1" BorderStyle="Solid" BorderColor="#999999" BackColor="#DEE3E7" AutoGenerateColumns="False"
											OnItemCreated="ItemCreated" OnItemCommand="ItemCommand">
											<SelectedItemStyle Wrap="False" BorderStyle="Solid" BorderColor="Orange" BackColor="LightSteelBlue"></SelectedItemStyle>
											<EditItemStyle Wrap="False"></EditItemStyle>
											<AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
											<ItemStyle Wrap="False" BackColor="White"></ItemStyle>
											<HeaderStyle></HeaderStyle>
											<Columns>
												<asp:TemplateColumn>
													<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
													<ItemTemplate>
														<asp:ImageButton style="cursor:hand;" id="btnEdit" ImageUrl="../Navigation/images/icons/edit2.gif"
															runat="server" CommandName="SelectAction"></asp:ImageButton>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn SortExpression="POLICE_STATION_NAME" HeaderText="Police_Station_Name">
													<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="35%"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn SortExpression="POLICE_STATION_CODE" HeaderText="Police_Station_Code">
													<HeaderStyle HorizontalAlign="Center" Width="13%"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
                                                <asp:BoundColumn SortExpression="POLICE_STATION_DISTRICT_NAME" HeaderText="Police_Station_District_Name">
													<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B"  Width="35%"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
												</asp:BoundColumn>
                                                <asp:BoundColumn SortExpression="POLICE_STATION_DISTRICT_CODE" HeaderText="Police_Station_District_Code">
													<HeaderStyle HorizontalAlign="Center" Width="12%"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn Visible="False" HeaderText="police_station_id"></asp:BoundColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7" PageButtonCount="15"
												Mode="NumericPages"></PagerStyle>
										</asp:datagrid></TD>
								</TR>
								<TR>
									<TD></TD>
								</TR>
							</TABLE></td>
				</tr>
				<TR>
					<TD>
						<HR>
					</TD>
				</TR>
				<TR>
					<TD align="left">
						<asp:button id="btnAdd_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
							runat="server" Font-Bold="false" Width="100px" Text="New" CssClass="FLATBUTTON" height="20px"
							CommandName="WRITE"></asp:button>&nbsp;
					</TD>
				</TR>
			</TABLE>
			</asp:panel></TD></TR></TABLE></form>
	</body>
</HTML>
