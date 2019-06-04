<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ClaimListByCommentType.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ClaimListByCommentType" %>
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
        <style type="text/css">
            .wrapText{white-space:pre-wrap;}
        </style>
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
								<TD height="20"><asp:label id="LabelTables" runat="server"  CssClass="TITLELABEL">Claims</asp:label>:
								<asp:label id="Label7" runat="server" CssClass="TITLELABELTEXT">SEARCH_BY_COMMENT_TYPE</asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="tblOuter2" style="BORDER: black 1px solid; MARGIN: 5px;"
				height="93%" cellSpacing="0" cellPadding="0" rules="none" width="98%" bgColor="#d5d6e4"> 
				<tr>
					<td style="HEIGHT: 8px"></td>
				</tr>
				<tr>
					<td vAlign="top" align="center" height="100%">
                        <%--<asp:panel id="WorkingPanel" runat="server">--%>
							<TABLE id="tblMain1" style="BORDER: #999999 1px solid;"
								height="98%" cellSpacing="0" cellPadding="6" width="98%" align="center" bgColor="#fef9ea"
								border="0">
								<TR>
									<TD vAlign="top" height="1">
										<uc1:ErrorController id="ErrorCtrl" runat="server"></uc1:ErrorController></TD>
								</TR>
								<TR>
									<TD vAlign="top" align="center">
										<TABLE cellSpacing="2" cellPadding="2" width="98%" align="center" border="0">
											<TR>
												<TD align="center" colSpan="2">
													<TABLE id="tblSearch" style="BORDER: #999999 1px solid; WIDTH: 100%; HEIGHT: 76px"
														cellSpacing="0" cellPadding="6" width="100%" align="center" bgColor="#f1f1f1" border="0"> <!--fef9ea-->
														<TR>
															<TD align="left">
																<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
																	<TR>
																		<TD noWrap align="left" width="1%">&nbsp;
																			<asp:label id="LabelSearchClaimNumber" runat="server">CLAIM_NUMBER</asp:label>:</TD>
																		<TD noWrap align="left" width="1%" colSpan="2">
																			<asp:label id="LabelSearchCustomerName" runat="server">CUSTOMER_NAME</asp:label>:
																		</TD>
                                                                        <TD noWrap align="left">
																		    <asp:label id="LabelSearchAuthorizationNumber" runat="server">AUTHORIZATION_NUMBER</asp:label>:</TD>
																	</TR>
																	<TR>
																		<TD noWrap align="left" width="25%">&nbsp;
																			<asp:textbox id="TextBoxSearchClaimNumber" runat="server" CssClass="FLATTEXTBOX" AutoPostBack="False"
																				Width="90%"></asp:textbox></TD>
																		<TD noWrap align="left" width="50%" colSpan="2">
																			<asp:textbox id="TextBoxSearchCustomerName" runat="server" CssClass="FLATTEXTBOX" AutoPostBack="False"
																				Width="90%"></asp:textbox></TD>                
                                                                        <TD noWrap align="left" width="25%">
																				<asp:textbox id="TextBoxSearchAuthorizationNumber" runat="server" CssClass="FLATTEXTBOX" AutoPostBack="False"
																					Width="98%"></asp:textbox></TD>
																	</TR>
																	<asp:panel id="serviceCenterPanel" runat="server">
																		<TR>
																			<TD noWrap align="left">&nbsp;<asp:label id="lblCommentType" runat="server">COMMENT_TYPE</asp:label>:</TD>																			
                                                                            <TD noWrap align="left"><asp:label id="lblClaimStatus" runat="server">CLAIM_STATUS</asp:label>:</TD> 
                                                                            <TD style="HEIGHT: 12px" colSpan="3">
																			<asp:label id="lblSortBy" runat="server">Sort By</asp:label>:</TD>   																			
																		</TR>
																		<TR>
																			<TD noWrap align="left">&nbsp;
																			<asp:dropdownlist id="cboCommentType" runat="server" AutoPostBack="False" Width="90%"></asp:dropdownlist></TD>
																			<TD noWrap align="left" width="25%">
																			<asp:dropdownlist id="cboClaimStatus" runat="server" AutoPostBack="False" Width="92%"></asp:dropdownlist></TD>  
                                                                            <TD noWrap align="left">
																			<asp:dropdownlist id="cboSortBy" runat="server" AutoPostBack="False" Width="85%"></asp:dropdownlist></TD>  
                                                                            <TD noWrap align="right">
																			<asp:button id="btnClearSearch" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/clear_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
																				runat="server" Font-Bold="false" CssClass="FLATBUTTON" Width="90px" Text="Clear" height="20px"></asp:button>&nbsp;&nbsp;
																			<asp:button id="btnSearch" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/search_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
																				runat="server" Font-Bold="false" CssClass="FLATBUTTON" Width="90px" Text="Search" height="20px"></asp:button>&nbsp;&nbsp;</TD>    																			
																		</TR>
																	</asp:panel>																	
																	<!--<TR>
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
														AllowSorting="false" OnItemCreated="ItemCreated" OnItemCommand="ItemCommand">
														<SelectedItemStyle Wrap="False" BorderStyle="Solid" BorderColor="Orange" BackColor="LightSteelBlue"></SelectedItemStyle>
														<EditItemStyle CssClass="EDITROW" Wrap="False"></EditItemStyle>
														<AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
														<ItemStyle Wrap="False" BackColor="White"></ItemStyle>
														<HeaderStyle></HeaderStyle>
														<Columns>
															<asp:TemplateColumn>
																<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="2%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
																<ItemTemplate>
																	<asp:ImageButton style="cursor:hand;" id="btnEdit" ImageUrl="../Navigation/images/icons/edit2.gif"
																		runat="server" CommandName="EditRecord"></asp:ImageButton>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="1" HeaderText="Claim_Number">
																<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="8%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center" ></ItemStyle>
                                                                <ItemTemplate>
                                                                    <asp:LinkButton style="cursor:hand; color:Blue;" id="btnClaimNum" runat="server" CommandName="Select"></asp:LinkButton>
                                                                </ItemTemplate>
															</asp:TemplateColumn>
                                                            <asp:TemplateColumn SortExpression="2" HeaderText="Authorization_Number">
																<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="8%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="6" HeaderText="Status">
																<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="5%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="3" HeaderText="Customer_Name">
																<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="15%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:TemplateColumn>															
                                                            <asp:TemplateColumn SortExpression="4" HeaderText="Comment_Type">
                                                                <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="15%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                                <EditItemTemplate>
																	<asp:DropDownList id="moCommentType" runat="server" visible="True"></asp:DropDownList>
																</EditItemTemplate>                                                               
                                                            </asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="5" HeaderText="Comment_Detail">
																<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center" Wrap="true" Width="47%" CssClass="wrapText"></ItemStyle>
                                                                 <EditItemTemplate>
                                                                    <asp:TextBox ID="moCommentDetail" runat="server" Width="98%" Visible="True"></asp:TextBox>
                                                                </EditItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn Visible="False" HeaderText="Claim_Id"></asp:TemplateColumn>
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
                                <tr valign="bottom">
                                <td align="left">
                                <hr style="width: 100%; height: 1px" size="1"/>                                    
                                    <asp:Button ID="SaveButton_WRITE" Style="background-image: url(../Navigation/images/icons/save_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                    Width="100px" CssClass="FLATBUTTON" Height="20px" Text="Save"></asp:Button>&nbsp;
                                    <asp:Button ID="UndoButton" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                    Width="100px" CssClass="FLATBUTTON" Height="20px" Text="Undo"></asp:Button>&nbsp;
                                </td>
                            </tr>
							</TABLE>
                            <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse" />
						<%--</asp:panel>--%>
					</td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
