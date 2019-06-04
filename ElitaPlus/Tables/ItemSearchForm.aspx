<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ItemSearchForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.ItemSearchForm"%>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>Item Search</title>
<meta content="Microsoft Visual Studio.NET 7.0" name=GENERATOR>
<meta content="Visual Basic 7.0" name=CODE_LANGUAGE>
<meta content=JavaScript name=vs_defaultClientScript>
<meta content=http://schemas.microsoft.com/intellisense/ie5 name=vs_targetSchema>
<SCRIPT language=JavaScript src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>
<LINK href="../Styles.css" type=text/css rel=STYLESHEET >
  </HEAD>
<body leftMargin="0" topMargin="0" onload="changeScrollbarColor();" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!---BEGIN-->
			<!--Start Header-->
			<TABLE style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; WIDTH: 98%; BORDER-BOTTOM: black 1px solid; HEIGHT: 20px"
				cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE width="100%" border="0">
							<TR>
								<TD height="20"><asp:label id="moTitleLabel1" runat="server"  Cssclass="TITLELABEL">Tables</asp:label>:&nbsp;
								<asp:label id="moTitleLabel2" runat="server" Cssclass="TITLELABELTEXT">Item</asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="moTablelOuter" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				height="93%" cellSpacing="0" cellPadding="0" rules="none" width="98%" bgColor="#d5d6e4"
				border="0"> <!--d5d6e4-->
				<tr>
					<td style="HEIGHT: 8px"></td>
				</tr>
				<tr>
					<td vAlign="top" align="center" height="100%"><asp:panel id="moPanel" runat="server">
							<TABLE id="moTablelMain" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid"
								height="98%" cellSpacing="0" cellPadding="6" width="98%" align="center" bgColor="#fef9ea"
								border="0">
								<TR>
									<TD height="1">
										<uc1:ErrorController id="moErrorController" runat="server"></uc1:ErrorController></TD>
								</TR>
								<TR>
									<TD vAlign="top" align="center" width="100%">
										<TABLE cellSpacing="2" cellPadding="2" width="100%" align="center" border="0">
											<TR>
												<TD align="center" colSpan="2">
													<TABLE id="moTableSearch" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; WIDTH: 98%; BORDER-BOTTOM: #999999 1px solid; HEIGHT: 76px"
														cellSpacing="0" cellPadding="6" width="98%" align="center" bgColor="#f1f1f1" border="0">
														<TR>
															<TD>
																<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
																	<TR>
																		<TD align="left" colSpan="4">
																			<DIV style="WIDTH: 65.5%" align="left">
																				<uc1:MultipleColumnDDLabelControl id="multipleDropControl" runat="server"></uc1:MultipleColumnDDLabelControl></DIV>
																		</TD>
																	</TR>
																	<TR>
																		<TD colSpan="4">&nbsp;</TD>
																	</TR>
																	<TR>
																		<TD colSpan="4">
																			<HR style="HEIGHT: 1px">
																			&nbsp;
																		</TD>
																	</TR>
																	<TR>
																		<TD style="WIDTH: 1%" vAlign="middle" noWrap align="left">&nbsp;
																			<asp:label id="moProductCodeLabel" runat="server">Product_Code</asp:label>:</TD>
																		<TD align="left" width="30%" height="21">&nbsp;
																			<asp:DropDownList id="moProductCodeDrop" runat="server" Width="170px" AutoPostBack="True"></asp:DropDownList></TD>
																		<TD noWrap align="left" width="1%">&nbsp;</TD>
																		<TD vAlign="middle" align="left" width="60%">
																			<asp:label id="moItemRiskTypeLabel" runat="server">Risk_Type</asp:label>:
																			<asp:DropDownList id="moItemRiskTypeDrop" runat="server" Width="170px" AutoPostBack="True"></asp:DropDownList></TD>
																	</TR>
																	<TR>
																		<TD align="center" colSpan="4" height="10"></TD>
																	</TR>
																	<TR>
																		<TD align="center"></TD>
																		<TD align="center"></TD>
																		<TD align="right"></TD>
																		<TD align="right" height="18">
																			<asp:button id="moBtnClear" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/clear_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
																				runat="server" Font-Bold="false" Width="90px" height="20px" CssClass="FLATBUTTON" Text="Clear"></asp:button>&nbsp;
																			<asp:button id="moBtnSearch" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/search_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
																				runat="server" Font-Bold="false" Width="90px" height="20px" CssClass="FLATBUTTON" Text="Search"></asp:button></TD>
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
													<asp:label id="lblPageSize" runat="server">Page_Size</asp:label>:&nbsp;
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
													<asp:GridView id="moItemGrid" runat="server" Width="100%" 
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
                                                                    <asp:ImageButton ID="imgbtnEdit" runat="server" CausesValidation="False"
                                                                            CommandName="SelectUser" ImageUrl="~/Navigation/images/icons/edit2.gif" 
                                                                            CommandArgument="<%#Container.DisplayIndex %>" />
                                                                </ItemTemplate>
                                                            </asp:TemplateField>    															
															<asp:TemplateField Visible="False">																
															</asp:TemplateField>
															<asp:TemplateField SortExpression="DEALER_NAME" HeaderText="DEALER_NAME">											       
															</asp:TemplateField>
															<asp:TemplateField SortExpression="PRODUCT_CODE" HeaderText="PRODUCT_CODE">															       
															</asp:TemplateField>
															<asp:TemplateField SortExpression="ITEM_NUMBER" HeaderText="ITEM_NUMBER">															       
															</asp:TemplateField>
															<asp:TemplateField SortExpression="RISK_TYPE" HeaderText="RISK_TYPE">															       
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
									<TD width="98%">
										<HR style="HEIGHT: 1px">
									</TD>
								</TR>
								<TR>
									<TD align="left">
										<asp:button id="btnNew_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server" Font-Bold="false" Width="90px" height="23px" CssClass="FLATBUTTON" Text="New"></asp:button>&nbsp;
									</TD>
								</TR>
							</TABLE>
						</asp:panel></td>
				</tr>
			</TABLE>
		</form>
		<!--END-->
	</body>
</HTML>
