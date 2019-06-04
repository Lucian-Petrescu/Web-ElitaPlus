<%@ Page Language="vb" AutoEventWireup="false" Codebehind="MfgStandardizationForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.MfgStandardizationForm"%>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>WebForm1</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="STYLESHEET">
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>
	</HEAD>
	<body leftMargin="0" topMargin="0" onload="changeScrollbarColor();" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<TABLE id="Table1" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE id="Table3" width="100%" border="0">
							<TR>
								<TD height="20"><asp:label id="LabelTables" runat="server" Cssclass="TITLELABEL">Tables</asp:label>:
									<asp:label id="moTitle2Label" runat="server" Cssclass="TITLELABELTEXT">Manufacturer Standardization</asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<!--d5d6e4-->
			<TABLE id="tblOuter2" style="BORDER: black 1px solid; MARGIN: 5px; height:93%;"
				height="93%" cellSpacing="0" cellPadding="0" rules="none" width="98%" bgColor="#d5d6e4"
				border="0">
				<tr>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td vAlign="top" align="center" height="100%"><asp:panel id="moMfgStandardization_Panel" runat="server">
							<TABLE id="tblMain1" style="BORDER: #999999 1px solid; height:98%;"
								cellSpacing="0" cellPadding="6" rules="cols" width="98%" align="center"
								bgColor="#fef9ea" border="0">
								<TR>
									<TD height="1"></TD>
								</TR>
								<TR>
									<TD align="center">
										<uc1:ErrorController id="moErrorController" runat="server"></uc1:ErrorController></TD>
								</TR>
								<TR>
									<TD vAlign="top" colSpan="2">
										<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
											<TR>
												<TD style="WIDTH: 100%; HEIGHT: 52px" colSpan="2">
													<TABLE id="tblMfgStandardization_Search" style="BORDER: #999999 1px solid;WIDTH: 98%; HEIGHT: 76px"
														cellSpacing="2" cellPadding="2" width="98%" align="center" bgColor="#f1f1f1"
														border="0">
														<TR>
															<TD noWrap align="right" width="1%">
																<asp:label id="moLblMfgStandard" runat="server" Font-Bold="false">Manufacturer Alias</asp:label>:</TD>
															<TD width="40%">
																<asp:textbox id="moTxtMfgStandardSearch" runat="server" CssClass="FLATTEXTBOX" width="200px"></asp:textbox></TD>
															<TD noWrap align="right" width="1%">
																<asp:label id="moLblMfg" runat="server" Font-Bold="false">Manufacturer</asp:label>:</TD>
															<TD width="40%">
																<asp:DropDownList id="moDropdownMfg" tabIndex="1" runat="server" Height="20px" Width="280px"></asp:DropDownList></TD>
														</TR>
														<TR>
															<TD align="right" colSpan="4">
																<asp:button id="moBtnClearSearch" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/clear_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
																	tabIndex="2" runat="server" Font-Bold="false" CssClass="FLATBUTTON" Width="90px" Text="Clear"
																	height="20px"></asp:button>&nbsp;&nbsp;
																<asp:button id="moBtnSearch" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/search_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
																	tabIndex="3" runat="server" Font-Bold="false" CssClass="FLATBUTTON" Width="90px" Text="Search"
																	height="20px"></asp:button></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
											<TR>
												<TD colSpan="2" style="padding-top:5px;">
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
												<TD style="WIDTH: 100%" colSpan="2">&nbsp;
													<asp:GridView id="moMfgGrid" runat="server" Width="100%" OnRowCreated="ItemCreated" OnRowCommand="ItemCommand"
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
                                                                            CommandName="DeleteRecord" ImageUrl="../Navigation/images/icons/trash.gif" 
                                                                            CommandArgument="<%#Container.DisplayIndex %>" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField> 															
															<asp:TemplateField Visible="False">
															   <ItemTemplate>
															       <%#Container.DataItem("mfg_standardization_id")%> 
															   </ItemTemplate>
															</asp:TemplateField>
															<asp:TemplateField Visible="False">
															    <ItemTemplate>
															        <%#Container.DataItem("Manufacturer_ID")%> 
															    </ItemTemplate>
															</asp:TemplateField>															
															<asp:TemplateField SortExpression="description" HeaderText="MANUFACTURER ALIAS">																
																<ItemTemplate>
																</ItemTemplate>
																<EditItemTemplate>
																	<asp:TextBox id="TextBoxGridDescription" cssclass="FLATTEXTBOX_TAB" runat="server"></asp:TextBox>
																</EditItemTemplate>
															</asp:TemplateField>
															<asp:TemplateField SortExpression="manufacturer_name" HeaderText="MANUFACTURER">																
																<ItemTemplate>
																	ManufacturerInGridLabel
																</ItemTemplate>
																<EditItemTemplate>
																	<asp:DropDownList id="moMfgDropDown" runat="server"></asp:DropDownList>
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
									<TD style="WIDTH: 100%" align="left">
										<asp:button id="moBtnAdd_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server" Font-Bold="false" CssClass="FLATBUTTON" Width="90px" Text="New" height="20px"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;
										<asp:button id="moBtnSave_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/save_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server" Font-Bold="false" CssClass="FLATBUTTON" Width="90px" Text="Save" height="20px"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;
										<asp:button id="moBtnCancel" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/cancel_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server" Font-Bold="false" CssClass="FLATBUTTON" Width="90px" Text="Cancel" height="20px"></asp:button></TD>
								</TR>
							</TABLE>
						</asp:panel></td>
				</tr>
			</TABLE>
			</form>
	</body>
</HTML>
