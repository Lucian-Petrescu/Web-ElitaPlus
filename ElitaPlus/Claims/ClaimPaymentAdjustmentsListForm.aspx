<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ClaimPaymentAdjustmentsListForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ClaimPaymentAdjustmentsListForm" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Claim Payment Adjustments List Form</title>
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
			<TABLE style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; WIDTH: 98%; BORDER-BOTTOM: black 1px solid; HEIGHT: 20px"
				cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE width="100%" border="0">
							<TR>
								<TD height="20"><asp:label id="LabelTables" runat="server"  CssClass="TITLELABEL">Claims</asp:label>:&nbsp;
									<asp:label id="Label2" runat="server"   CssClass="TITLELABELTEXT">CLAIM_PAYMENT_ADJUSTMENTS</asp:label>&nbsp;</TD>
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
																		<TD noWrap align="left" width="1%">
																			<asp:label id="LabelSearchClaimNumber" runat="server">CLAIM_NUMBER</asp:label>:</TD>
																		<TD noWrap align="left" width="1%" colSpan="2">
																			<asp:label id="LabelSearchCustomerName" runat="server">CUSTOMER_NAME</asp:label>:
																		</TD>
																	</TR>
																	<TR>
																		<TD noWrap align="left" width="275">
																			<asp:textbox id="TextBoxSearchClaimNumber" runat="server" Width="75%" AutoPostBack="False" CssClass="FLATTEXTBOX"></asp:textbox></TD>
																		<TD noWrap align="left" width="75%" colSpan="2">
																			<asp:textbox id="TextBoxSearchCustomerName" runat="server" Width="100%" AutoPostBack="False"
																				CssClass="FLATTEXTBOX"></asp:textbox></TD>
																	<TR>
																	<TR>
																		<TD noWrap align="left">
																			<asp:label id="LabelSearchServiceCenter" runat="server">SERVICE_CENTER_NAME</asp:label>:</TD>
																		<TD noWrap align="left">
																			<asp:label id="LabelSearchAuthorizationNumber" runat="server">AUTHORIZATION_NUMBER</asp:label>:</TD>
																		<TD noWrap align="left">
																			<asp:label id="LabelSearchAuthorizedAmount" runat="server">AUTHORIZED_AMOUNT</asp:label>:</TD>
																	</TR>
																	<TR>
																		<TD noWrap align="left" width="50%">
																			<asp:textbox id="moServiceCenterText" runat="server" Width="98%" AutoPostBack="False" CssClass="FLATTEXTBOX"></asp:textbox></TD>
																		<TD noWrap align="left" width="30%">
																			<asp:textbox id="TextBoxSearchAuthorizationNumber" runat="server" Width="95%" AutoPostBack="False"
																				CssClass="FLATTEXTBOX"></asp:textbox></TD>
																		<TD noWrap align="left" width="20%">
																			<asp:textbox id="TextBoxSearchAuthorizedAmount" runat="server" Width="100%" AutoPostBack="False"
																				CssClass="FLATTEXTBOX"></asp:textbox></TD>
																	</TR>
																	<TR>
																		<TD style="HEIGHT: 12px" colSpan="3">
																			<asp:label id="lblSortBy" runat="server">Sort By</asp:label>:</TD>
																	</TR>
																	<TR>
																		<TD noWrap align="left">
																			<asp:dropdownlist id="cboSortBy" runat="server" Width="75%" AutoPostBack="False"></asp:dropdownlist></TD>
																		<TD noWrap align="right" colSpan="3">
																			<asp:button id="btnClearSearch" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/clear_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
																				runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" height="20px" Text="Clear"></asp:button>&nbsp;&nbsp;
																			<asp:button id="btnSearch" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/search_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
																				runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" height="20px" Text="Search"></asp:button></TD>
																	</TR> <!--<TR>
																		<TD noWrap width="450" colSpan="2">&nbsp;</TD>
																		<TD noWrap align="right">
																			<asp:label id="lblDistChannel" runat="server">Sort By:</asp:label>&nbsp;
																			<asp:dropdownlist id="cboDistChannel" runat="server" Width="154px" AutoPostBack="False"></asp:dropdownlist></TD>
																	</TR>--></TABLE>
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
													<asp:datagrid id="Grid" runat="server" Width="100%" OnItemCommand="ItemCommand" OnItemCreated="ItemCreated"
														AllowSorting="false" AllowPaging="True" CellPadding="1" BorderColor="#999999" BackColor="#DEE3E7"
														BorderWidth="1px" BorderStyle="Solid" AutoGenerateColumns="False">
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
															<asp:BoundColumn SortExpression="1" HeaderText="Claim_Number">
																<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="15%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn SortExpression="6" HeaderText="Status">
																<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="6%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn SortExpression="4" HeaderText="Customer_Name">
																<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="27%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn SortExpression="5" HeaderText="Service_Center">
																<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="28%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn SortExpression="2" HeaderText="Authorization_Number">
																<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="12%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn SortExpression="3" HeaderText="Authorized_Amount">
																<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="12%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn Visible="False" HeaderText="Claim_Id"></asp:BoundColumn>
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
