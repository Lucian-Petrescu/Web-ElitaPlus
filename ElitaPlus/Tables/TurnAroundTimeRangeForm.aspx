<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="TurnAroundTimeRangeForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.TurnAroundTimeRangeForm" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>TurnAroundTimeRangeForm</title>
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
								<TD height="20">&nbsp;<asp:label id="TablesLabel" runat="server" Cssclass="TITLELABEL">Tables</asp:label>:
									<asp:label id="MaintainTATLabel" runat="server"  Cssclass="TITLELABELTEXT">TURN_AROUND_TIME_RANGE</asp:label></TD>
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
					<td vAlign="top" align="center" height="100%">
						<asp:panel id="WorkingPanel" runat="server">
							<TABLE id="tblMain1" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid"
								height="98%" cellSpacing="0" cellPadding="6" rules="cols" width="98%" align="center"
								bgColor="#fef9ea" border="0">
								<TR>
									<TD height="1"></TD>
								</TR>
								<TR>
									<TD vAlign="top" height="1">
										<uc1:ErrorController id="ErrController" runat="server"></uc1:ErrorController></TD>
								</TR>
								<TR>
									<TD vAlign="top" colSpan="2">
										<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
											<TR>
												<TD vAlign="top" colspan = "2">
													<TABLE id="tblSearch" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; WIDTH: 100%; BORDER-BOTTOM: #999999 1px solid; HEIGHT: 76px"
														cellSpacing="0" cellPadding="6" rules="cols" width="100%" align="center" bgColor="#f1f1f1"
														border="0">
														<TR>
															<TD>
																<TABLE cellSpacing="6" cellPadding="0" width="100%" border="0">
																	<TR>
																		<TD noWrap align="left">
																			<asp:RadioButton ID="rdoCompanyGroup" runat="server" AutoPostBack="true" 
                                                                                Checked="true" GroupName="rdoSearchOnGroup" 
                                                                                 Style="margin-right: 10px;" 
                                                                                Text="COMPANY_GROUP" />
                                                                        </TD>
																		<TD style="WIDTH: 200px" noWrap align="center">
																			&nbsp;</TD>
																		<TD align="right" colSpan="2">
																			&nbsp;
																			<asp:button id="SearchButton" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/search_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
																				runat="server"  Width="90px" Text="Search" height="20px" CssClass="FLATBUTTON"></asp:button></TD>
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
											<tr>
											<td colspan="2" style="height:2px;"></td></tr>
											<TR>
												<TD colSpan="2">
													<asp:GridView id="Grid" runat="server" Width="100%" OnRowCreated="ItemCreated" OnRowCommand="ItemCommand"
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
                                                                    <asp:ImageButton ID="EditButton_WRITE" runat="server" CausesValidation="False"
                                                                            CommandName="EditRecord" ImageUrl="~/Navigation/images/icons/edit2.gif" 
                                                                            CommandArgument="<%#Container.DisplayIndex %>" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField> 
                                                            <asp:TemplateField ShowHeader="false">
                                                                <ItemStyle HorizontalAlign="Center" Width="3%" Height="15px" />
                                                                <ItemTemplate>
                                                                    <asp:ImageButton ID="DeleteButton_WRITE" runat="server" CausesValidation="False"
                                                                            CommandName="DeleteRecord" ImageUrl="~/Navigation/images/icons/trash.gif" 
                                                                            CommandArgument="<%#Container.DisplayIndex %>" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField> 
															<asp:TemplateField Visible="False" HeaderText="Id">
																<ItemTemplate>
																	&gt;
																	<asp:Label id="IdLabel" runat="server">
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateField>
                                                            <asp:TemplateField SortExpression="CODE" HeaderText="CODE">
																<ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
																<ItemTemplate>
																	<asp:Label id="CodeLabel" runat="server" visible="True">
																	</asp:Label>
																</ItemTemplate>
																<EditItemTemplate>
																	<asp:TextBox id="CodeTextBox" cssclass="FLATTEXTBOX_TAB" runat="server" visible="True"></asp:TextBox>
																</EditItemTemplate>
															</asp:TemplateField>
                                                            <asp:TemplateField SortExpression="Description" HeaderText="Description">
																<ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>
																<ItemTemplate>
																	<asp:Label id="DescriptionLabel" runat="server" visible="True">
																	</asp:Label>
																</ItemTemplate>
																<EditItemTemplate>
																	<asp:TextBox id="DescriptionTextBox" cssclass="FLATTEXTBOX_TAB" runat="server" visible="True"></asp:TextBox>
																</EditItemTemplate>
															</asp:TemplateField>
                                                            <asp:TemplateField SortExpression="MIN_DAYS" HeaderText="MIN_DAYS">
																<ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
																<ItemTemplate>
																	<asp:Label id="MinDaysLabel" runat="server" visible="True">
																	</asp:Label>
																</ItemTemplate>
																<EditItemTemplate>
																	<asp:TextBox id="MinDaysTextBox" cssclass="FLATTEXTBOX_TAB" runat="server" visible="True" Enabled="false"></asp:TextBox>
																</EditItemTemplate>
															</asp:TemplateField>
                                                            <asp:TemplateField SortExpression="MAX_DAYS" HeaderText="MAX_DAYS">
																<ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
																<ItemTemplate>
																	<asp:Label id="MaxDaysLabel" runat="server" visible="True">
																	</asp:Label>
																</ItemTemplate>
																<EditItemTemplate>
																	<asp:TextBox id="MaxDaysTextBox" cssclass="FLATTEXTBOX_TAB" runat="server" visible="True" Enabled="false"></asp:TextBox>
																</EditItemTemplate>
															</asp:TemplateField>
															<asp:TemplateField SortExpression="COLOR" HeaderText="Color">
																<ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
																<ItemTemplate>
																	<asp:Label id="ColorLabel" runat="server" visible="True">
																	</asp:Label>
																</ItemTemplate>
																<EditItemTemplate>
																	<asp:DropDownList id="cboColor" runat="server" visible="True" AutoPostBack="False"></asp:DropDownList>
																</EditItemTemplate>
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
									<TD>
										<HR style="HEIGHT: 1px">
									</TD>
								</TR>
								<TR>
									<TD align="left">
										<asp:button id="NewButton_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server"  Width="100px" Text="New" height="20px" CssClass="FLATBUTTON"></asp:button>
										<asp:button id="SaveButton_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/save_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server"  Width="100px" Text="Save" height="20px" CssClass="FLATBUTTON"></asp:button>&nbsp;
										<asp:button id="CancelButton" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/cancel_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server"  Width="100px" Text="Cancel" height="20px" CssClass="FLATBUTTON"></asp:button>&nbsp;
									</TD>
								</TR>
							</TABLE>
						</asp:panel>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
