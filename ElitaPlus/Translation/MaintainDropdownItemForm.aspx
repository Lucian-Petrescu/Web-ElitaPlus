<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="MaintainDropdownItemForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Translation.MaintainDropdownItemForm" %>
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
					<TD vAlign="top" align="center" >
						<TABLE width="100%" border="0">
							<TR>
								<TD>
									<asp:label id="Label5" runat="server" Cssclass="TITLELABEL">Tables</asp:label>:
									<asp:Label id="Label6" runat="server"  Cssclass="TITLELABELTEXT">Maintain Dropdown Items</asp:Label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
					<TABLE id="tblOuter2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				height="93%" cellSpacing="0" cellPadding="0" rules="none" width="98%" bgColor="#d5d6e4"
				border="1" frame="void"> <!--d5d6e4-->
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
									<TD valign="top" height="80%">
										<TABLE cellSpacing="0" cellPadding="0" width="99%" border="0">
											<TR>
												<TD noWrap align="right">
													<asp:Label id="Label2" runat="server">Dropdown Name</asp:Label>:&nbsp;
												</TD>
												<TD colSpan="2" >
													<asp:Label id="DropdownName" runat="server"  ForeColor="#12135B" Width="272px">DropdownName</asp:Label></TD>
											</TR> 		
											<TR>
												<TD align="center" width="100%" colSpan="3">
													<HR size="1">
												</TD>
											</TR> 	
											<TR>
												<TD colSpan="3" valign="top" align="center" >
													<asp:datagrid id="DataGridDropdownItems" runat="server" Width="90%" PageSize="20" OnItemCreated="ItemCreated"
														AllowSorting="True" CellPadding="1" BorderColor="#999999" BackColor="#DEE3E7" BorderWidth="1px"
														BorderStyle="Solid" AutoGenerateColumns="False">
														<SelectedItemStyle Wrap="False"></SelectedItemStyle>
														<EditItemStyle Wrap="False"></EditItemStyle>
														<AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
														<ItemStyle Wrap="False" BackColor="White"></ItemStyle>
														<HeaderStyle></HeaderStyle>
														<Columns>
															<asp:TemplateColumn Visible="False">
																<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B"></HeaderStyle>
																<ItemTemplate>
																	<asp:Label id="lblListItemId" runat="server" Visible="False" Text='<%# GetGuidStringFromByteArray(Container.DataItem("LIST_ITEM_ID")) %>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:BoundColumn DataField="ENGLISH_TRANSLATION" HeaderText="English">
																<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B"></HeaderStyle>
															</asp:BoundColumn>
															<asp:TemplateColumn HeaderText="Language Description">
																<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B"></HeaderStyle>
																<ItemTemplate>
																	<asp:TextBox id="TextBoxLangTrans" runat="server" CssClass="FLATTEXTBOX" Text='<%# Container.DataItem("LANG_TRANSLATION") %>' width="100%">
																	</asp:TextBox>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:BoundColumn Visible="False" DataField="CODE"></asp:BoundColumn>
															<asp:BoundColumn Visible="False" DataField="LANG_TRANSLATION" SortExpression="LANG_TRANSLATION"></asp:BoundColumn>
		                                                    <asp:BoundColumn Visible="False" DataField="DICT_ITEM_TRANSLATION_ID" SortExpression="LANG_TRANSLATION"></asp:BoundColumn>
																<asp:TemplateColumn Visible="False">
																<ItemTemplate>
																	<asp:Label id="lblDictItemTransId" runat="server" Visible="False" Text='<%# GetGuidStringFromByteArray(Container.DataItem("DICT_ITEM_TRANSLATION_ID")) %>'>
																	</asp:Label>
																</ItemTemplate>
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
										<TD align="center" width="100%" valign="bottom" >
											<HR SIZE="1" >
										</TD>
								</TR>									
								<TR>
									<TD align="left">
										<asp:button id="btnSave_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/save_trans_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" height="20px" Text="Save"></asp:button>&nbsp;&nbsp;&nbsp;
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
