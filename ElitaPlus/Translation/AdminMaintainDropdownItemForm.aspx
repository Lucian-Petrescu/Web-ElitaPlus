<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="AdminMaintainDropdownItemForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Translation.AdminMaintainDropdownItemForm" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>AdminMaintainDropdownForm</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="STYLESHEET">
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>
	</HEAD>
	<body leftMargin="0" topMargin="0" ms_positioning="GridLayout"  onload="changeScrollbarColor();" border="0">
		<form id="Form1" method="post" runat="server">
		<!--Start Header-->
			<TABLE style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
					<TR>
					<TD vAlign="top">
						<TABLE width="100%" border="0">
							<TR>
								<TD><asp:label id="Label5" runat="server" Cssclass="TITLELABEL">Admin</asp:label>:
									<asp:label id="Label6" runat="server"  Cssclass="TITLELABELTEXT">Maintain Dropdown Items</asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
				<TABLE id="tblOuter2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				height="93%" cellSpacing="0" cellPadding="0" rules="none" width="98%" bgColor="#d5d6e4"
				border="0" frame="void"> <!--d5d6e4-->
				<tr>
					<td vAlign="top" align="center"><asp:panel id="WorkingPanel" runat="server" Height="98%">
							<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<TD>&nbsp;</TD>
								</TR>
							</TABLE>
							<TABLE id="tblMain1" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid"
								cellSpacing="0" cellPadding="4" rules="cols" height="98%" width="98%" align="center" bgColor="#fef9ea"
								border="0">
								<TR>
									<TD valign="top">&nbsp;
										<uc1:ErrorController id="ErrorControl" runat="server" Visible="False"></uc1:ErrorController></TD>
								</TR>
								<TR>
									<TD valign="top" >
										<TABLE cellSpacing="0" cellPadding="0" width="99%" border="0">
											<TR>
												<TD noWrap align="right">
													<asp:Label id="Label2" runat="server" Width="9px">Dropdown Name</asp:Label>:</TD>
												<TD colSpan="2">
													<asp:Label id="DropdownName" runat="server"  ForeColor="#12135B" Width="386px">DropdownName</asp:Label></TD>
											<TR>
												<TD colSpan="3">&nbsp;</TD>
											</TR>
											<TR>
												<TD noWrap align="right" width="20%">
                                                    *<asp:label id="LabelNewProgCode" runat="server" Width="9px">Code</asp:label>:&nbsp;
												</TD>
												<TD width="35%" >
													<asp:TextBox id="TextBoxNewProgCode" runat="server" Width="80px" TabIndex="1"></asp:TextBox>&nbsp;
												</TD>
												<TD noWrap align="left" width="30%" rowspan="2" valign="middle"  >
													<asp:Button id="bntAdd" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
														runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" ToolTip="Add" height="20px"
														Text="Add" TabIndex="3"></asp:Button></TD>
											</TR>
											<TR>
												<TD noWrap align="right">
                                                    *<asp:label id="LabelDescription" runat="server" Width="9px">Description</asp:label>:&nbsp;
												</TD>
												<TD>
													<asp:TextBox id="TextBoxDescription" runat="server" Width="280" TabIndex="2"></asp:TextBox>&nbsp;
												</TD>
											</TR>
											<TR>
												<TD align="center" width="100%" colSpan="3">
													<HR size="1">
												</TD>
											</TR>	
											<TR>
												<TD align="center" valign ="top" colSpan="3">
													<asp:datagrid id="DataGridDropdownItems" runat="server" Width="99%" AutoGenerateColumns="False"
													BorderStyle="Solid" BorderWidth="1px" BackColor="#DEE3E7" BorderColor="#999999" CellPadding="1"
													AllowPaging="True" AllowSorting="True" OnItemCreated="ItemCreated" PageSize="15" OnItemCommand="DataGridDropdownItems_ItemCommand">
														<SelectedItemStyle Wrap="False"></SelectedItemStyle>
														<EditItemStyle Wrap="False"></EditItemStyle>
														<AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
														<ItemStyle Wrap="False" BackColor="White"></ItemStyle>
														<HeaderStyle></HeaderStyle>
														<Columns>
															<asp:TemplateColumn HeaderImageUrl="../Navigation/images/icons/check.gif">
																<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B"></HeaderStyle>
																<ItemTemplate>
																	<asp:CheckBox id="CheckBoxItemSel" runat="server"></asp:CheckBox>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="Code">
																<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B"  Width="28%"></HeaderStyle>
																<ItemTemplate>
																	<asp:TextBox width="100%" id="TextBoxProgCode" runat="server" Text='<%# Container.DataItem("Code") %>' CssClass="FLATTEXTBOX">
																	</asp:TextBox>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="English">
																<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="40%"></HeaderStyle>
																<ItemTemplate>
																	<asp:TextBox width="100%" CssClass="FLATTEXTBOX" id="TextBoxEngTrans" runat="server" Text='<%# Container.DataItem("English_Translation") %>'>
																	</asp:TextBox>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="MAINTAINABLE_BY_USER" >
																<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B"  Width="12%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
																<ItemTemplate>
																	<asp:CheckBox id="CheckBoxMaintainableByUser" runat="server" Checked='<%# Container.DataItem("Maintainable_by_user")="Y" %>'>
																	</asp:CheckBox>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="DISPLAY_TO_USER" >
																<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B"  Width="12%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
																<ItemTemplate>
																	<asp:CheckBox id="CheckBoxDisplayToUser" runat="server" Checked='<%# Container.DataItem("Display_to_user")="Y" %>'>
																	</asp:CheckBox>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn  ItemStyle-HorizontalAlign="Center" HeaderText="Items">
															<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="10%"></HeaderStyle>
																<ItemStyle width="5%" HorizontalAlign="Center"></ItemStyle>
																<ItemTemplate>
																	<asp:Button ID="btnView" runat="server" Text="View" CommandName="ItemsCMD"></asp:Button>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn Visible="False">
																<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label id="lblListItemId" runat="server" Visible="False" Text='<%# GetGuidStringFromByteArray(Container.DataItem("LIST_ITEM_ID")) %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:BoundColumn Visible="False" DataField="CODE"></asp:BoundColumn>
															<asp:BoundColumn Visible="False" DataField="MAINTAINABLE_BY_USER"></asp:BoundColumn>
															<asp:BoundColumn Visible="False" DataField="DISPLAY_TO_USER"></asp:BoundColumn>
															<asp:BoundColumn Visible="False" DataField="ENGLISH_TRANSLATION" SortExpression="ENGLISH_TRANSLATION"></asp:BoundColumn>
														</Columns>
														<PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7" PageButtonCount="15"
															Mode="NumericPages"></PagerStyle>
													</asp:datagrid>
												</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
			                       <TR>
										<TD align="center" width="100%" valign="bottom" >
											<HR SIZE="1" >
										</TD>
								</TR>	
								<TR>
									<TD align="left" >
										<asp:button id="btnSave" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/save_trans_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" height="20px" Text="Save"></asp:button>&nbsp;
										<asp:button id="btnDelete" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/clear_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" height="20px" Text="Delete"></asp:button>&nbsp;
										<asp:button id="btnCancel" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/cancel_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" height="20px" Text="Cancel"></asp:button></TD>
								</TR>
							</TABLE>
						</asp:panel></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
