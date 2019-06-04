<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="MaintainDropdownForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Translation.MaintainDropdownForm" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>

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
	<body leftMargin="0" topMargin="0" onload="changeScrollbarColor();" border="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--Start Header-->
			<TABLE style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE width="100%" border="0">
							<TR>
								<TD height="20">&nbsp;
									<asp:label id="Label5" runat="server" Cssclass="TITLELABEL">Tables</asp:label>:
									<asp:Label id="Label6" runat="server"  Cssclass="TITLELABELTEXT">Maintain Dropdowns</asp:Label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="tblOuter2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				height="93%" cellSpacing="0" cellPadding="0" rules="none" width="98%" bgColor="#d5d6e4"
				border="0" frame="void"> <!--d5d6e4-->
				<tr>
					<td vAlign="top" align="center" ><asp:panel id="WorkingPanel" runat="server" height="98%">
							<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
								<TR>
									<%--<TD>&nbsp;</TD>--%>
								</TR>
								
							</TABLE>
							<TABLE id="tblMain1" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid"
								cellSpacing="0" cellPadding="4" rules="cols" width="98%" height="98%" align="center" bgColor="#fef9ea"
								border="0">
								<TR>
									<TD><%--&nbsp;--%>
										<uc1:ErrorController id="ErrorController" runat="server" Visible="False"></uc1:ErrorController></TD>
								</TR>
								<TR>
									<TD align="center" valign=top >
										<TABLE cellSpacing="0" cellPadding="0" width="98%" border="0">
										 <tr>
                                     <td align="center" colspan="3" valign="top">
                                         <%--&nbsp;--%>
                                         <uc1:MultipleColumnDDLabelControl ID="moCompanyMultipleDrop" runat="server">
                                            </uc1:MultipleColumnDDLabelControl>
                                        
                                     </td>
                                  </tr>
                                  <tr><td valign=top>&nbsp;</td></tr>
											    <caption>
                                                    &#160;
												<tr>
												<td colspan="3">
													<asp:DataGrid ID="DataGridDropdowns" runat="server" AllowPaging="True" 
                                                        AllowSorting="True" AutoGenerateColumns="False" BackColor="#DEE3E7" 
                                                        BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="1" 
                                                        OnItemCommand="DataGridDropdowns_ItemCommand" OnItemCreated="ItemCreated" 
                                                        PageSize="17" Width="100%">
														<SelectedItemStyle Wrap="False"></SelectedItemStyle>
                                                        
                                                        
														<EditItemStyle Wrap="False"></EditItemStyle>
                                                        
                                                        
														<AlternatingItemStyle BackColor="#F1F1F1" Wrap="False"></AlternatingItemStyle>
                                                        
                                                        
														<ItemStyle BackColor="White" Wrap="False"></ItemStyle>
                                                        
                                                        
														<HeaderStyle></HeaderStyle>
                                                        
                                                        
														<Columns>
															<asp:TemplateColumn Visible="False">
																<ItemTemplate>
																	<asp:Label ID="lblListId" runat="server" 
                                                                        Text='<%# GetGuidStringFromByteArray(Container.DataItem("LIST_ID")) %>' 
                                                                        Visible="False">
																	</asp:Label>
																</ItemTemplate>
                                                            
                                                            
															</asp:TemplateColumn>
															<asp:BoundColumn DataField="ENGLISH_TRANSLATION" HeaderText="English">
																<HeaderStyle ForeColor="#12135B" HorizontalAlign="Center"></HeaderStyle>
                                                            
                                                            
															</asp:BoundColumn>
															<asp:TemplateColumn HeaderText="Language Description">
																<HeaderStyle ForeColor="#12135B" HorizontalAlign="Center" Width="50%">
                                                            </HeaderStyle>
                                                            
                                                            
																<ItemTemplate>
																	<asp:TextBox ID="TextBoxLangTrans" runat="server" CssClass="FLATTEXTBOX" 
                                                                        Text='<%# Container.DataItem("LANG_TRANSLATION") %>' width="100%">
																	</asp:TextBox>
																</ItemTemplate>
                                                            
                                                            
															</asp:TemplateColumn>
															<asp:ButtonColumn ButtonType="PushButton" CommandName="ItemsCMD" 
                                                                HeaderText="Items" Text="View">
																<HeaderStyle ForeColor="#12135B" HorizontalAlign="Center"></HeaderStyle>
                                                            
                                                            
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            
                                                            
															</asp:ButtonColumn>
															<asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
															<asp:BoundColumn DataField="DICT_ITEM_TRANSLATION_ID" 
                                                                SortExpression="LANG_TRANSLATION" Visible="False"></asp:BoundColumn>
																<asp:TemplateColumn Visible="False">
																<ItemTemplate>
																	<asp:Label ID="lblDictItemTransId" runat="server" 
                                                                        Text='<%# GetGuidStringFromByteArray(Container.DataItem("DICT_ITEM_TRANSLATION_ID")) %>' 
                                                                        Visible="False">
																	</asp:Label>
																</ItemTemplate>
                                                            
                                                            
															</asp:TemplateColumn>
														</Columns>
                                                        
                                                        
														<PagerStyle BackColor="#DEE3E7" ForeColor="DarkSlateBlue" 
                                                            HorizontalAlign="Center" Mode="NumericPages" PageButtonCount="15">
                                                        </PagerStyle>
                                                        
                                                        
													</asp:DataGrid></td>
											</tr></caption></TABLE></TD></TR>
							</TABLE>
						    </TD>
                            </TR>
                            <tr>
										<td align="center" valign="bottom" width="100%">
												<hr size="1">
										</hr></td>
								</tr>
                            <tr>
									<td align="left">&#160;
										<asp:Button ID="btnBack" runat="server" CssClass="FLATBUTTON" 
                                Font-Bold="false" height="20px" 
                                style="BACKGROUND-IMAGE: url(../Navigation/images/icons/back_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat" 
                                tabIndex="185" Text="Return" Width="90px"></asp:Button>&#160;
										<asp:Button ID="btnSave_WRITE" runat="server" CssClass="FLATBUTTON" 
                                Font-Bold="false" height="20px" 
                                style="BACKGROUND-IMAGE: url(../Navigation/images/icons/save_trans_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat" 
                                Text="Save" Width="90px"></asp:Button>&#160;
										<asp:Button ID="btnCancel_WRITE" runat="server" 
                                CssClass="FLATBUTTON" Font-Bold="false" height="20px" 
                                style="BACKGROUND-IMAGE: url(../Navigation/images/icons/cancel_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat" 
                                Text="Cancel" Width="90px"></asp:Button></td>
								</tr>
                            </TABLE>
						</asp:panel></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
