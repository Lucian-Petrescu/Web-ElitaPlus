<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DirectBillingForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.DirectBillingForm" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//Dtd HTML 4.0 transitional//EN">
<HTML>
	<HEAD>
		<title>Direct Billing Form</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>
		<LINK href="../Styles.css" type="text/css" rel="STYLESHEET">
	</HEAD>
	<body leftMargin="0" topMargin="0" onload="changeScrollbarColor();" MS_POSITIONING="GridLayout"
		border="0">
		<form id="Form1" method="post" runat="server">
			<!--Start Header-->
			<table style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<tr>
					<td vAlign="top">
						<table width="100%" border="0">
							<tr>
								<td height="20">&nbsp;<asp:label id="moTitleLabel1" runat="server" CssClass="TITLELABEL">Interfaces</asp:label>:
									<asp:label id="moTitleLabel2" runat="server" CssClass="TITLELABELTEXT">Direct_Billing</asp:label></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<!--d5d6e4-->
		<table id="tblOuter2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid; HEIGHT: 93%"
		cellSpacing="0" cellPadding="0" rules="none" width="98%" bgColor="#d5d6e4" border="0"> <!--d5d6e4-->
				<tr>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td valign="top" align="center" height="100%"><asp:panel id="moPanel" runat="server">
					<table id="tblMain1" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid"
					height="98%" cellSpacing="0" cellPadding="6" rules="cols" width="98%" align="center"
					bgColor="#fef9ea" border="0">
								<tr>
									<td height="1">
										<uc1:ErrorController id="ErrController" runat="server"></uc1:ErrorController></td>
								</tr>
								<tr>
									<td vAlign="top">
										<table cellSpacing="2" cellPadding="2" width="100%" align="center" border="0">
											<tr>
												<td vAlign="top" colSpan="2" >
													<table id="motableSearch" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; WIDTH: 98%; BORDER-BOTTOM: #999999 1px solid; HEIGHT: 76px"
														cellSpacing="0" cellPadding="6" rules="cols" width="98%" align="center" bgColor="#f1f1f1"
														border="0">
														<tr>
															<td>
																<table cellSpacing="0" cellPadding="0" width="100%" border="0">
                                                                    <tr>
                                                                        <td valign="top" align="left" colspan="6">
                                                                            <table cellspacing="0" cellpadding="0" border="0" width="85%">

                                                                                <tr id="moCompanyInformation" runat="server">
                                                                                    <td align="left" valign="middle">
                                                                                        <uc1:MultipleColumnDDLabelControl ID="companyMultipleDropControl" runat="server"></uc1:MultipleColumnDDLabelControl>
                                                                                    </td>
                                                                                </tr>
                                                                                <tr>
                                                                                    <td align="left" valign="middle">
                                                                                        <uc1:MultipleColumnDDLabelControl ID="multipleDropControl" runat="server"></uc1:MultipleColumnDDLabelControl>
                                                                                    </td>
                                                                                </tr>
                                                                            </table>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
																		<td colspan="6">
																		<hr style="height:1px" />
																		</td>
																	</tr>
																	<tr>
																		<td style="HEIGHT: 11px; width:1%" noWrap align="left" >
																			<asp:label id="moBeginDateLabel" runat="server">Begin_date:</asp:label>&nbsp;</td>
																		<td style="HEIGHT: 11px; width:25%" noWrap align="left">
																		       <asp:TextBox runat="server" Columns="20" ID="txtBeginDate"></asp:TextBox>
                                                                                <asp:imagebutton id="btnBeginDate" runat="server" Visible="true" ImageAlign="AbsMiddle"  ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:imagebutton>
                                                                         </td>
																		<td style="HEIGHT: 11px;width:1%" noWrap align="left" >
																			<asp:label id="moEndDateLabel" runat="server">End_Date:</asp:label>&nbsp;</td>
																		<td style="HEIGHT: 11px" noWrap align="left" width="25%">
									                                        <asp:TextBox runat="server" Columns="20" ID="txtEndDate"></asp:TextBox>
                                                                            <asp:imagebutton id="btnEndDate" runat="server" Visible="true" ImageAlign="AbsMiddle" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:imagebutton>
																		</td>
																		<td style="HEIGHT: 11px" noWrap align="left" colspan="2">
																		   </td>
																	</tr>																	
																	<tr>
																		<td colSpan="6">&nbsp;</td>
																	</tr>
																	<tr>
																		<td noWrap align="center"></td>
																		<td noWrap align="center" colspan="3">&nbsp;</td>
																		<td noWrap align="center" colspan="2">
																			<asp:button id="mobtnClear" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/clear_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
																				runat="server" Font-Bold="false" Width="90px" Text="Clear" CssClass="FLATBUTTON" height="20px"></asp:button>&nbsp;
																			<asp:button id="mobtnSearch" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/search_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
																				runat="server" Font-Bold="false" Width="90px" Text="Search" CssClass="FLATBUTTON" height="20px"></asp:button>&nbsp;
																		</td>
																	</tr>
																</table>
															</td>
														</tr>
													</table>
												</td>
											</tr>
											<tr>
												<td colSpan="2">
													<HR style="HEIGHT: 1px">
												</td>
											<tr id="trPageSize" runat="server">
												<td vAlign="top" align="left">
													<asp:label id="lblPageSize" runat="server">Page_Size</asp:label>: &nbsp;
													<asp:dropdownlist id="cboPageSize" runat="server" Width="50px" AutoPostBack="true">
														<asp:ListItem Value="5">5</asp:ListItem>
														<asp:ListItem Selected="true" Value="10">10</asp:ListItem>
														<asp:ListItem Value="15">15</asp:ListItem>
														<asp:ListItem Value="20">20</asp:ListItem>
														<asp:ListItem Value="25">25</asp:ListItem>
														<asp:ListItem Value="30">30</asp:ListItem>
														<asp:ListItem Value="35">35</asp:ListItem>
														<asp:ListItem Value="40">40</asp:ListItem>
														<asp:ListItem Value="45">45</asp:ListItem>
														<asp:ListItem Value="50">50</asp:ListItem>
													</asp:dropdownlist></td>
												<td style="HEIGHT: 22px" align="right">
													<asp:label id="lblRecordCount" Runat="server"></asp:label></td>
											</tr>
											<tr id="moESCBillingInformation" runat="server">
												<td colSpan="2">
													<asp:datagrid id="moBillingGrid" runat="server" Width="100%" AllowSorting="true" AllowPaging="true"
														BorderWidth="1px" CellPadding="1" BorderStyle="Solid" BorderColor="#999999" BackColor="#DEE3E7"
														AutoGenerateColumns="False"  OnItemCreated="ItemCreated" OnItemCommand="ItemCommand">
														<SelectedItemStyle Wrap="False" BackColor="LightSteelBlue"></SelectedItemStyle>
														<EditItemStyle Wrap="False"></EditItemStyle>
														<AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
														<ItemStyle Wrap="False" BackColor="White"></ItemStyle>
														<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
														<Columns>
															<asp:TemplateColumn>
																<HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
																<ItemTemplate>
																	<asp:ImageButton id="imgbtnEdit" runat="server" CommandName="SelectAction" ImageUrl="../Navigation/images/icons/edit2.gif"></asp:ImageButton>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn Visible="False">
																<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
																<ItemTemplate>
																	<asp:Label id="moBillingHeaderId" text='<%# GetGuidStringFromByteArray(Container.DataItem("billing_header_id")) %>' runat="server">
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
																<asp:TemplateColumn HeaderText="DEALER">
						                                            <HeaderStyle HorizontalAlign="Center" />
						                                            <ItemStyle HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label id="lblDealer" runat="server" text='<%#Container.DataItem("Dealer") & " - " & Container.DataItem("Dealer_name")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="DATE_FILE_SENT">
						                                        <HeaderStyle HorizontalAlign="Center" />
						                                        <ItemStyle HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:Label id="lblDateFileSent" runat="server" text='<%# formatDateTime(Container.DataItem("DATE_FILE_SENT"))%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
															<asp:BoundColumn DataField="FILENAME" ItemStyle-HorizontalAlign="Center" HeaderText="FILE_NAME">
																<HeaderStyle Width="25%"></HeaderStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="TOTAL_BILLED_AMT" DataFormatString="{0:#,0.00}" ItemStyle-HorizontalAlign="Center"  HeaderText="TOTAL_BILLED_AMT">
																<HeaderStyle Width="20%"></HeaderStyle>
															</asp:BoundColumn>
														</Columns>
														<PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7" PageButtonCount="15"
															Mode="NumericPages"></PagerStyle>
													</asp:datagrid></td>
											</tr>
											<tr id="moVSCBillingInformation" runat="server">
												<td colSpan="2">
													<asp:datagrid id="moVSCBillingGrid" runat="server" Width="100%" AllowSorting="true" AllowPaging="true"
														BorderWidth="1px" CellPadding="1" BorderStyle="Solid" BorderColor="#999999" BackColor="#DEE3E7"
														AutoGenerateColumns="False"  OnItemCreated="ItemCreated" OnItemCommand="ItemCommand">
														<SelectedItemStyle Wrap="False" BackColor="LightSteelBlue"></SelectedItemStyle>
														<EditItemStyle Wrap="False"></EditItemStyle>
														<AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
														<ItemStyle Wrap="False" BackColor="White"></ItemStyle>
														<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
														<Columns>
															<asp:TemplateColumn>
																<HeaderStyle HorizontalAlign="Center" Width="3%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
																<ItemTemplate>
																	<asp:ImageButton id="imgbtnEdit" runat="server" CommandName="SelectAction" ImageUrl="../Navigation/images/icons/edit2.gif"></asp:ImageButton>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn Visible="False">
																<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
																<ItemTemplate>
																	<asp:Label id="moBillingHeaderId" text='<%# GetGuidStringFromByteArray(Container.DataItem("billing_header_id")) %>' runat="server">
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
																<asp:TemplateColumn HeaderText="Status">
						                                            <HeaderStyle Width="7%" HorizontalAlign="Center" />
						                                            <ItemStyle HorizontalAlign="Center" />
                                                                    <ItemTemplate>
                                                                        <asp:Label id="lblStatus" runat="server" text='<%#Container.DataItem("Status")%>'></asp:Label>
                                                                    </ItemTemplate>
                                                                </asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="DATE_FILE_SENT">
						                                        <HeaderStyle Width="15%" HorizontalAlign="Center" />
						                                        <ItemStyle HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:Label id="lblDateFileSent" runat="server" text='<%# formatDateTime(Container.DataItem("DATE_FILE_SENT"))%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
															<asp:TemplateColumn HeaderText="DATE_FILE_RECEIVED">
						                                        <HeaderStyle Width="15%" HorizontalAlign="Center" />
						                                        <ItemStyle HorizontalAlign="Center" />
                                                                <ItemTemplate>
                                                                    <asp:Label id="lblDateFileReceived" runat="server" text='<%# formatDateTime(Container.DataItem("DATE_FILE_RECEIVED"))%>'></asp:Label>
                                                                </ItemTemplate>
                                                            </asp:TemplateColumn>
															<asp:BoundColumn DataField="FILENAME" ItemStyle-HorizontalAlign="Center" HeaderText="FILE_NAME">
																<HeaderStyle Width="20%"></HeaderStyle>
															</asp:BoundColumn>
															<asp:BoundColumn DataField="reference_number" ItemStyle-HorizontalAlign="Center" HeaderText="reference_number">
																<HeaderStyle Width="15%"></HeaderStyle>
															</asp:BoundColumn>															
															<asp:BoundColumn DataField="TOTAL_BILLED_AMT" DataFormatString="{0:#,0.00}" ItemStyle-HorizontalAlign="Center"  HeaderText="TOTAL_BILLED_AMT">
																<HeaderStyle Width="15%"></HeaderStyle>
															</asp:BoundColumn>
														</Columns>
														<PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7" PageButtonCount="15"
															Mode="NumericPages"></PagerStyle>
													</asp:datagrid></td>
											</tr>
										</table>
									</td>
								</tr>
						</table>
						</asp:panel></td>
				</tr>
			</table>
		</form>
		<!--END-->
	</body>
</HTML>
