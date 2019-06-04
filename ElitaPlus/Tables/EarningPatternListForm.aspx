<%@ Page Language="vb" AutoEventWireup="false" Codebehind="EarningPatternListForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.EarningPatternListForm"%>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
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
			<TABLE style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; WIDTH: 98%; BORDER-BOTTOM: black 1px solid; HEIGHT: 20px"
				cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE width="100%" border="0">
							<TR>
								<TD height="20"><asp:label id="moTitleLabel" runat="server" Cssclass="TITLELABEL">Tables</asp:label>:&nbsp;<asp:label id="moTitleLabel2" runat="server" Cssclass="TITLELABELTEXT">Earning_Pattern_Detail</asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<!--d5d6e4-->
			<TABLE id="moTableOuter" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				height="93%" cellSpacing="0" cellPadding="0" rules="none" width="98%" bgColor="#d5d6e4"
				border="0">
				<TR>
					<TD style="HEIGHT: 8px" vAlign="top" align="center"></TD>
				</TR>
				<tr>
					<td vAlign="top" align="center"><asp:panel id="moPanel" runat="server">
							<TABLE id="moTableMain" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid"
								height="98%" cellSpacing="0" cellPadding="6" width="98%" align="center" bgColor="#fef9ea"
								border="0">
								<TR>
									<TD height="1">
										<uc1:ErrorController id="moErrorController" runat="server"></uc1:ErrorController></TD>
								</TR>
								<TR>
									<TD vAlign="top">
										<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
											<TR>
												<TD vAlign="top" colSpan="2">
													<TABLE id="moTableSearch" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid; HEIGHT: 64px"
														cellSpacing="0" cellPadding="6" width="98%" align="center" bgColor="#f1f1f1" border="0">
														<TR>
															<TD vAlign="top" align="center" colSpan="1">
																<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
																	<TR>
																		<TD noWrap align="left"></TD>
																		<TD noWrap align="left"></TD>
																		<TD noWrap align="left"></TD>
																		<TD noWrap align="left"></TD>
																	</TR>
																	<TR>
																		<TD style="HEIGHT: 16px" noWrap align="left">
																			<asp:label id="moCodeLabel" runat="server">Code</asp:label>:</TD>
																		<TD style="HEIGHT: 16px" noWrap align="left">
																			<asp:label id="moDescriptionLabel" runat="server">Description</asp:label>:</TD>
																		<TD style="HEIGHT: 16px" noWrap align="left"></TD>
																		<TD style="HEIGHT: 16px" noWrap align="left"></TD>
																	</TR>
																	<TR>
																		<TD noWrap align="left" width="30%">
																			<asp:textbox id="SearchCodeTextBox" runat="server" Height="20px" AutoPostBack="False" Width="120px"></asp:textbox></TD>
																		<TD noWrap align="left" width="40%">
																			<asp:textbox id="SearchDescriptionTextBox" runat="server" Height="20px" AutoPostBack="False"
																				Width="220px"></asp:textbox></TD>
																		<TD noWrap align="right" width="15%">
																			<asp:button id="moBtnClear" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/clear_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
																				runat="server" Font-Bold="false" Width="90px" Text="Clear" height="20px" CssClass="FLATBUTTON"></asp:button></TD>
																		<TD noWrap align="right" width="15%">
																			<asp:button id="moBtnSearch" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/search_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
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
													<asp:GridView id="moDataGrid" runat="server" Width="100%" OnRowCreated="ItemCreated" OnRowCommand="ItemCommand"
														AllowPaging="True" AllowSorting="True"  CellPadding="1" AutoGenerateColumns="False" CssClass="DATAGRID">
														<SelectedRowStyle CssClass="SELECTED"></SelectedRowStyle>
														 <EditRowStyle CssClass="EDITROW"></EditRowStyle>
														<AlternatingRowStyle Wrap="False" CssClass="ALTROW"></AlternatingRowStyle>
														<RowStyle CssClass="ROW"></RowStyle>
                                                         <HeaderStyle CssClass="HEADER"></HeaderStyle>     
														<Columns>
														  <asp:TemplateField ShowHeader="false">
                                                                <ItemStyle HorizontalAlign="Center" Width="3%" Height="15px" />
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="False"
                                                                            CommandName="BtnEdit" ImageUrl="~/Navigation/images/icons/edit2.gif" 
                                                                            CommandArgument="<%#Container.DisplayIndex %>" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>														
															<asp:TemplateField Visible="False">
																<ItemTemplate>
																	<asp:Label id=moEarningPatternId  runat="server">
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateField>
															<asp:TemplateField SortExpression="Code" HeaderText="Code">																
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:TemplateField>
															<asp:TemplateField SortExpression="Description" HeaderText="Description">																
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:TemplateField>
															<asp:TemplateField SortExpression="Effective" HeaderText="Effective">
																<ItemTemplate>
																	<asp:Label id=moEffectiveLabel runat="server">
																	</asp:Label>
																</ItemTemplate>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:TemplateField>
															<asp:TemplateField SortExpression="Expiration" HeaderText="Expiration">
																<ItemTemplate>
																	<asp:Label id=Label1 runat="server" >
																	</asp:Label>
																</ItemTemplate>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:TemplateField>
														</Columns>
														<PagerSettings PageButtonCount="15" Mode="Numeric" />
														<PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
													</asp:GridView></TD>
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
										<asp:button id="BtnNew_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server" Font-Bold="false" Width="100px" Text="New" height="20px" CssClass="FLATBUTTON"></asp:button>&nbsp;
									</TD>
								</TR>
							</TABLE>
						</asp:panel></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
