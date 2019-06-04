<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="VSCPlanListForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.VSCPlanListForm" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>VSC_Plan</title>
		<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
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
								<TD height="20"><asp:label id="LabelTables" runat="server" CssClass="TITLELABEL">Tables</asp:label>:&nbsp;<asp:label id="Label7" runat="server" CssClass="TITLELABELTEXT">VSC_PLAN</asp:label></TD>
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
									<TD align="center">
										<uc1:ErrorController id="ErrorCtrl" runat="server"></uc1:ErrorController></TD>
								</TR>
								<TR>
									<TD vAlign="top">
										<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
											<TR>
												<TD colSpan="2">
													<TABLE id="tblSearch" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; WIDTH: 98%; BORDER-BOTTOM: #999999 1px solid; HEIGHT: 50px"
														cellSpacing="0" cellPadding="6" rules="cols" width="98%" align="center" bgColor="#f1f1f1"
														border="0">
														<TR>
															<TD vAlign="top">
																<TABLE cellSpacing="2" cellPadding="2" width="100%" border="0">
																	<TBODY>
																		<TR>
																			<TD noWrap align="left" colSpan="4">&nbsp;
																				<DIV style="WIDTH: 73.3%; HEIGHT: 39px" align="center">
																					<uc1:MultipleColumnDDLabelControl id="multipleDropControl" runat="server"></uc1:MultipleColumnDDLabelControl></DIV>
																			</TD>
																		</TR>
																		<tr>
																			<TD noWrap align="right">
																				<asp:button id="btnClearSearch" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/clear_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
																					runat="server" CssClass="FLATBUTTON" Font-Bold="false" Width="90px" Text="Clear"></asp:button>&nbsp;
																				<asp:button id="btnSearch" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/search_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
																					runat="server" CssClass="FLATBUTTON" Font-Bold="false" Width="90px" Text="Search"></asp:button></TD>
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
										<asp:datagrid id="Grid" runat="server" Width="100%" AutoGenerateColumns="False" BackColor="#DEE3E7"
											BorderColor="#999999" BorderStyle="Solid" CellPadding="1" BorderWidth="1px" AllowPaging="True"
											AllowSorting="True" OnItemCreated="ItemCreated" OnItemCommand="ItemCommand">
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
												<asp:BoundColumn SortExpression="CODE" HeaderText="Code">
													<HeaderStyle HorizontalAlign="Center" Width="30%"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn SortExpression="DESCRIPTION" HeaderText="Description">
													<HeaderStyle HorizontalAlign="Center" Width="65%"></HeaderStyle>
													<ItemStyle HorizontalAlign="Left"></ItemStyle>
												</asp:BoundColumn>
												<asp:BoundColumn Visible="False" HeaderText="id"></asp:BoundColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7" PageButtonCount="15"
												Mode="NumericPages"></PagerStyle>
										</asp:datagrid></TD>
								</TR>
								<TR>
									<TD></TD>
								</TR>
							</TABLE>
					</td>
				</tr>
				<TR>
					<TD>
						<HR>
					</TD>
				</TR>
				<TR>
					<TD align="left">
						<asp:button id="btnAdd_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
							runat="server" CssClass="FLATBUTTON" Font-Bold="false" Width="100px" Text="New" height="20px"
							CommandName="WRITE"></asp:button>&nbsp;
					</TD>
				</TR>
			</TABLE>
			</asp:panel></TD></TR></TBODY></TABLE>
		</form>
		</TD></TR></TBODY></TABLE>
	</body>
</HTML>
