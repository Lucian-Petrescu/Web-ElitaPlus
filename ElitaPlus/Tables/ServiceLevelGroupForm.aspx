<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ServiceLevelGroupForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ServiceLevelGroupForm" %>
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
			<TABLE style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE width="100%" border="0">
							<TR>
								<TD height="20"><asp:label id="LabelTables" runat="server" Cssclass="TITLELABEL">Tables</asp:label>:&nbsp;
									<asp:label id="Label7" runat="server" Cssclass="TITLELABELTEXT">Service_Level_Group</asp:label></TD>
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
									<TD height="1"></TD>
								</TR>
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
																	<TR>
																		<TD noWrap align="right" width="1%">
																			<asp:label id="moCountryLabel" runat="server">Country:</asp:label>
																			</TD>
																		<TD noWrap align="left" width="25%">
																			<asp:DropDownList id="moCountryDrop" runat="server" Width="95%"></asp:DropDownList></TD>
																		<TD noWrap align="left" width="1%"></TD>
																		<TD width="50%"></TD>
																		<TD noWrap align="right" width="25%"></TD>
																	</TR>
																	<TR>
																		<TD noWrap align="right" width="1%">
																			<asp:label id="SearchCodeLabel" runat="server">CODE</asp:label>:</TD>
																		<TD noWrap align="left" width="25%">
																			<asp:textbox id="TextBoxSearchCode" runat="server" Width="95%" cssclass="FLATTEXTBOX" AutoPostBack="False"></asp:textbox></TD>
																		<TD noWrap align="right" width="1%">
																			<asp:label id="LabelSearchDescription" runat="server">DESCRIPTION</asp:label>:</TD>
																		<TD width="50%">
																			<asp:textbox id="TextBoxSearchDescription" runat="server" Width="95%" cssclass="FLATTEXTBOX"
																				AutoPostBack="False"></asp:textbox></TD>

																		<td width="25%"></td>
																	</TR>
                                                                    <tr>
                                                                   
                                                                      <td valign="middle" align="right" width="1%">
                                                                       
                                                                        <asp:Label ID="lblDateFrom" runat="server" Height="4px">FROM</asp:Label>:
                                                                        </td>
                                                                        <td valign="middle" align="left" width="25%">
                                                                        <asp:TextBox ID="txtDateFrom" runat="server" Wrap="False"></asp:TextBox>
                                                                        <asp:ImageButton ID="btnDateFrom" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                                                                        </asp:ImageButton>
                                                                        </td>                                                                                                              
                                                                       
                                                                        <td valign="middle" align="right" width="1%">
                                                                       
                                                                        <asp:Label ID="LabelDateTo" runat="server" Height="4px">TO</asp:Label>:
                                                                        </td>
                                                                        <td valign="middle" align="left" width="50%">
                                                                        <asp:TextBox ID="txtDateTo" runat="server" Wrap="False"></asp:TextBox>
                                                                        <asp:ImageButton ID="btnDateTo" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                                                                        </asp:ImageButton>
                                                                        </td>
                                                                     <TD noWrap align="right" width="25%">
																			<asp:button id="btnClearSearch" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/clear_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
																				runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" Text="Clear"></asp:button>&nbsp;
																			<asp:button id="btnSearch" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/search_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
																				runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" Text="Search"></asp:button></TD>       
                                                                </tr> 
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
													<asp:GridView id="Grid" runat="server" Width="100%" 
														AllowSorting="True" AllowPaging="True" CellPadding="1" BorderColor="#999999" BackColor="#DEE3E7"
														BorderWidth="1px" BorderStyle="Solid" AutoGenerateColumns="False"
														CssClass="DATAGRID">
                                                        <SelectedRowStyle CssClass="SELECTED"></SelectedRowStyle>
                                                        <EditRowStyle CssClass="EDITROW"></EditRowStyle>
                                                        <AlternatingRowStyle Wrap="False" CssClass="ALTROW"></AlternatingRowStyle>
                                                        <RowStyle CssClass="ROW"></RowStyle>
                                                        <HeaderStyle CssClass="HEADER"></HeaderStyle>
														<Columns>
														<asp:TemplateField>
																<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="5%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
																<ItemTemplate>
																	<asp:ImageButton style="cursor:hand;" id="EditButton_WRITE" ImageUrl="../Navigation/images/icons/edit2.gif"
																		runat="server" CommandName="SelectAction" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
																</ItemTemplate>
														</asp:TemplateField>
                                                       <asp:TemplateField SortExpression="Country" HeaderText="COUNTRY">
                                                            <HeaderStyle HorizontalAlign="Center" Width="20%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="CountryLabel" runat="server" Visible="True">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:DropDownList ID="CountryDropdown"  Width="110PX" runat="server"></asp:DropDownList>
                                                            </EditItemTemplate>                                                          
                                                        </asp:TemplateField>
                                                        <asp:TemplateField SortExpression="Code" HeaderText="CODE">
                                                            <HeaderStyle HorizontalAlign="Center" Width="30%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="CodeLabel" runat="server" Visible="True">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="SLGCodeTextBox" runat="server" Visible="True"></asp:TextBox>
                                                            </EditItemTemplate>                                                          
                                                        </asp:TemplateField>  
                                                        <asp:TemplateField SortExpression="Description" HeaderText="DESCRIPTION">
                                                            <HeaderStyle HorizontalAlign="Center" Width="40%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="DescriptionLabel" runat="server" Visible="True">
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                            <EditItemTemplate>
                                                                <asp:TextBox ID="SLGDescTextBox" runat="server" Visible="True"></asp:TextBox>
                                                            </EditItemTemplate>                                                          
                                                        </asp:TemplateField>    
														<asp:TemplateField SortExpression="ID" Visible="False" >
                                                           <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" ></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="SLGIdLabel" runat="server" Visible="True" >
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateField>
														</Columns>
														<PagerSettings PageButtonCount="15" Mode="Numeric" />
                                                        <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
													</asp:GridView></TD>
											</TR>
											<TR>
												<TD></TD>
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
										<asp:button id="btnAdd_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server" Font-Bold="false" Width="100px" CssClass="FLATBUTTON" Text="New" height="20px"></asp:button>&nbsp;
                                             <asp:Button ID="SaveButton_WRITE" Style="background-image: url(../Navigation/images/icons/save_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                    Width="100px" CssClass="FLATBUTTON" Height="20px" Text="Save"></asp:Button>&nbsp;
                                    <asp:Button ID="CancelButton" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                    Width="100px" CssClass="FLATBUTTON" Height="20px" Text="Cancel"></asp:Button>&nbsp;
									</TD>
								</TR>
							</TABLE>
						</asp:panel></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
