<%@ Page Language="vb" AutoEventWireup="false" Codebehind="RepairCodeForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.RepairCodeForm" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>RepairCodeForm</title>
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
								<TD height="20">&nbsp;<asp:label id="TablesLabel" runat="server"  Cssclass="TITLELABEL">Tables</asp:label>:
									<asp:label id="MaintainRepairCodeLabel" runat="server" Cssclass="TITLELABELTEXT">REPAIR_CODE</asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<!--d5d6e4-->
			<TABLE id="tblOuter2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				height="93%" cellSpacing="0" cellPadding="0" rules="none" width="98%" bgColor="#d5d6e4"
				border="0">
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
									<TD style="HEIGHT: 1px" align="center" colSpan="2">
										<uc1:ErrorController id="ErrController" runat="server"></uc1:ErrorController></TD>
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
															<TD vAlign="top">
																<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
																	<TR>
																		<TD style="WIDTH: 150px" align="left">
																			<asp:label id="SearchCodeLabel" runat="server">CODE</asp:label>:</TD>
																		<TD style="HEIGHT: 13px">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
																		<TD style="WIDTH: 200px" align="left">
																			<asp:label id="Label1" runat="server">DESCRIPTION</asp:label>:</TD>
																		<TD></TD>
																	</TR>
																	<TR>
																		<TD style="WIDTH: 150px" noWrap align="left">
																			<asp:textbox id="SearchCodeTextBox" runat="server" Width="150px" AutoPostBack="False" cssclass="FLATTEXTBOX"></asp:textbox></TD>
																		<TD>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
																		<TD style="WIDTH: 95%" noWrap align="left">
																			<asp:textbox id="SearchDescriptionTextBox" runat="server" Width="95%" AutoPostBack="False" cssclass="FLATTEXTBOX"></asp:textbox></TD>
																		<TD align="center">
																			<asp:button id="ClearButton" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/clear_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
																				runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" height="20px" Text="Clear"></asp:button>&nbsp;
																			<asp:button id="SearchButton" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/search_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
																				runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" height="20px" Text="Search"></asp:button></TD>
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
												<TD colSpan="3">
													<asp:datagrid id="Grid" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
														BorderWidth="1px" AllowSorting="True" CellPadding="1" BorderColor="#999999" BackColor="#DEE3E7"
														BorderStyle="Solid" OnItemCreated="ItemCreated" OnItemCommand="ItemCommand">
														<SelectedItemStyle Wrap="False" BackColor="LavenderBlush"></SelectedItemStyle>
														<EditItemStyle Wrap="False" BackColor="AliceBlue"></EditItemStyle>
														<AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
														<ItemStyle Wrap="False" BackColor="White"></ItemStyle>
														<HeaderStyle></HeaderStyle>
														<Columns>
															<asp:TemplateColumn>
																<HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
																<ItemTemplate>
																	<asp:ImageButton id="EditButton_WRITE" style="CURSOR: hand" runat="server" ImageUrl="../Navigation/images/icons/edit2.gif"
																		CommandName="EditRecord"></asp:ImageButton>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn>
																<HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
																<ItemTemplate>
																	<asp:ImageButton style="cursor:hand;" id="DeleteButton_WRITE" ImageUrl="../Navigation/images/icons/trash.gif"
																		runat="server" CommandName="DeleteRecord"></asp:ImageButton>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn Visible="False" SortExpression="Id" HeaderText="Id"></asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="Short_Desc" HeaderText="CODE">
																<HeaderStyle HorizontalAlign="Center" Width="35%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Left" Width="27%"></ItemStyle>
																<ItemTemplate>
																</ItemTemplate>
																<EditItemTemplate>
																	<asp:TextBox id="TextBoxGridCode" cssclass="FLATTEXTBOX_TAB" runat="server"></asp:TextBox>
																</EditItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="Description" HeaderText="DESCRIPTION">
																<HeaderStyle HorizontalAlign="Center" Width="40%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Left" Width="60%"></ItemStyle>
																<ItemTemplate>
																</ItemTemplate>
																<EditItemTemplate>
																	<asp:TextBox id="TextBoxGridDescription" cssclass="FLATTEXTBOX_TAB" runat="server"></asp:TextBox>
																</EditItemTemplate>
															</asp:TemplateColumn>
														</Columns>
														<PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7" PageButtonCount="15"
															Mode="NumericPages"></PagerStyle>
													</asp:datagrid></TD>
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
										<asp:button id="NewButton_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server" Font-Bold="false" Width="100px" CssClass="FLATBUTTON" height="20px" Text="New"></asp:button>
										<asp:button id="SaveButton_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/save_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server" Font-Bold="false" Width="100px" CssClass="FLATBUTTON" height="20px" Text="Save"></asp:button>&nbsp;
										<asp:button id="CancelButton" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/cancel_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server" Font-Bold="false" Width="100px" CssClass="FLATBUTTON" height="20px" Text="Cancel"></asp:button>&nbsp;
									</TD>
								</TR>
							</TABLE>
						</asp:panel></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
