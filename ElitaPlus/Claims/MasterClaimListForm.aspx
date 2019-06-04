<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="MasterClaimListForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.MasterClaimListForm" %>
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
	<body leftMargin="0" topMargin="0" onload="changeScrollbarColor();" MS_POSITIONING="GridLayout"
		border="0">
		<form id="Form1" method="post" runat="server">
			<!--Start Header-->

			
			<TABLE class="TABLETITLE" cellSpacing="0" cellPadding="0" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE width="100%" border="0">
							<TR>
								<TD height="20">
									<asp:label id="TablesLabel" runat="server" Cssclass="TITLELABEL">Claims</asp:label>:
									<asp:label id="LabelTables" runat="server" Cssclass="TITLELABELTEXT">SEARCH_MASTER_CLAIM</asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			
			<TABLE id="tblOuter2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				height="93%" cellSpacing="0" cellPadding="0" rules="none" width="98%" bgColor="#d5d6e4"
				border="0"> <!--d5d6e4-->
				<tr>
					<td style="HEIGHT: 8px"></td>
				</tr>
				<tr>
					<td vAlign="top" align="center" height="100%"><asp:panel id="WorkingPanel" runat="server">
							<TABLE id="tblMain1" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid"
								height="98%" cellSpacing="0" cellPadding="6" width="98%" align="center" bgColor="#fef9ea"
								border="0">
								<TR>
									<TD vAlign="top" height="1">
										<uc1:ErrorController id="ErrorCtrl" runat="server"></uc1:ErrorController></TD>
								</TR>
								<TR>
									<TD vAlign="top" align="center" width="100%">
										<TABLE cellSpacing="2" cellPadding="2" width="100%" align="center" border="0">
											<TR>
												<TD align="center" colSpan="2">
													<TABLE id="tblSearch" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; WIDTH: 98%; BORDER-BOTTOM: #999999 1px solid; HEIGHT: 76px"
														cellSpacing="0" cellPadding="6" width="98%" align="center" bgColor="#f1f1f1" border="0"> <!--fef9ea-->
														<TR>
															<TD align="center">
																<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
																	<TR>
																		<TD noWrap align="left" width="50%">
																			<asp:label id="LabelSearchMasterClaimNumber" runat="server">MASTER_CLAIM_NUMBER</asp:label>:
																		</TD>
																		<TD noWrap align="left" width="50%">
																			<asp:label id="LabelSearchClaimNumber" runat="server">CLAIM_NUMBER</asp:label>:</TD>
																	</TR>
																	<TR>
																	    <TD noWrap align="left" width="50%">
																			<asp:textbox id="TextBoxSearchMasterClaimNumber" runat="server" CssClass="FLATTEXTBOX" AutoPostBack="False"
																				Width="98%"></asp:textbox></TD>
																		<TD noWrap align="left" width="50%">
																			<asp:textbox id="TextBoxSearchClaimNumber" runat="server" CssClass="FLATTEXTBOX" AutoPostBack="False"
																				Width="100%"></asp:textbox></TD>																		
																	</TR>
																	<asp:panel id="serviceCenterPanel" runat="server">
																		<TR>
																			<TD noWrap align="left">
																				<asp:label id="LabelSearchCustomerName" runat="server">CUSTOMER_NAME</asp:label>:</TD>
																			<TD noWrap align="left">
																				<asp:label id="LabelSearchAuthorizationNumber" runat="server">AUTHORIZATION_NUMBER</asp:label>:</TD>
																		</TR>
																		<TR>
																			<TD noWrap align="left" width="50%">
																				<asp:textbox id="TextBoxSearchCustomerName" runat="server" CssClass="FLATTEXTBOX" AutoPostBack="False"
																					Width="98%"></asp:textbox></TD>
																			<TD noWrap align="left" width="50%">
																				<asp:textbox id="TextBoxSearchAuthorizationNumber" runat="server" CssClass="FLATTEXTBOX" AutoPostBack="False"
																					Width="100%"></asp:textbox></TD>
																		</TR>
																	</asp:panel>
																	<TR>
																		<TD style="HEIGHT: 12px" colSpan="2">&nbsp;</TD>
																	</TR>
																	
																	<TR>
																		<TD noWrap align="right" colSpan="2">
																			<asp:button id="btnClearSearch" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/clear_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
																				runat="server" Font-Bold="false" CssClass="FLATBUTTON" Width="90px" Text="Clear" height="20px"></asp:button>&nbsp;&nbsp;
																			<asp:button id="btnSearch" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/search_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
																				runat="server" Font-Bold="false" CssClass="FLATBUTTON" Width="90px" Text="Search" height="20px"></asp:button></TD>
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
													<asp:datagrid id="Grid" runat="server" Width="100%" AutoGenerateColumns="False" BorderStyle="Solid"
														BorderWidth="1px" BackColor="#DEE3E7" BorderColor="#999999" CellPadding="1" AllowPaging="True"
														AllowSorting="true" OnItemCreated="ItemCreated" OnItemCommand="ItemCommand">
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
															<asp:BoundColumn SortExpression="1" HeaderText="Master_Claim_Number">
																<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="15%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn SortExpression="2"  HeaderText="Claim_Number">
																<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="15%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:BoundColumn>															
															<asp:BoundColumn SortExpression="3" HeaderText="Customer_Name">
																<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="40%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn SortExpression="4" HeaderText="Total_Paid">
																<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="15%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:BoundColumn>															
															<asp:BoundColumn SortExpression="5" HeaderText="Authorized_Amount">
																<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="15%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn Visible="False" HeaderText="Claim_ID"></asp:BoundColumn>
															<asp:BoundColumn Visible="False" HeaderText="Cert_ID"></asp:BoundColumn>
														</Columns>
														<PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7" PageButtonCount="10"
															Mode="NumericPages"></PagerStyle>
													</asp:datagrid></TD>
											</TR>
											<TR>
												<TD></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
							</TABLE>
						</asp:panel></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
