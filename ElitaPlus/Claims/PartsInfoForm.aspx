<%@ Page Language="vb" AutoEventWireup="false" Codebehind="PartsInfoForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.PartsInfoForm"%>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>RegionForm</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="STYLESHEET">
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>
	</HEAD>
	<body leftMargin="0" topMargin="0" onload="changeScrollbarColor();" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--Start Header-->
			<TABLE style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE width="100%" border="0">
							<TR>
								<TD height="20">&nbsp;<asp:label id="TablesLabel" runat="server"  CssClass="TITLELABEL">Claims</asp:label>:
									<asp:label id="MaintainPartsInfoLabel" runat="server"  CssClass="TITLELABELTEXT">PARTS   INFO</asp:label></TD>
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
					<td vAlign="top" align="center" height="100%"><asp:panel id="WorkingPanel" runat="server" Height="98%" Width="98%">
							<TABLE id="tblMain1" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid"
								cellSpacing="0" cellPadding="6" rules="cols" width="100%" align="center" bgColor="#fef9ea"
								border="0">
								<TR>
									<TD align="center" width="75%" colSpan="2">&nbsp;&nbsp;
										<uc1:ErrorController id="ErrController" runat="server"></uc1:ErrorController></TD>
								</TR>
								<TR>
									<TD>
										<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
											<TR>
												<TD>
													<TABLE id="tblHeader" cellSpacing="0" cellPadding="0" rules="cols" width="98%" align="center"
														bgColor="#fef9ea" border="0">
														<TR>
															<TD>
																<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
																	<TR>
																		<TD style="WIDTH: 45%" align="left">
																			<asp:label id="LabelCustomerName" runat="server">Customer_Name</asp:label>:</TD>
																		<TD style="WIDTH: 20%" align="left">
																			<asp:label id="LabelClaimNumber" runat="server">Claim_Number</asp:label>:</TD>
																		<TD style="WIDTH: 35%" align="left">
																			<asp:label id="LabelRiskGroup" runat="server">RISK_GROUP</asp:label>:
																		</TD>
																	</TR>
																	<TR>
																		<TD align="left">
																			<asp:textbox id="TextboxCustomerName" style="BACKGROUND-COLOR: whitesmoke" tabIndex="1" runat="server"
																				ReadOnly="True" width="95%" CssClass="FLATTEXTBOX"></asp:textbox></TD>
																		<TD align="left">
																			<asp:textbox id="TextboxClaimNumber" style="BACKGROUND-COLOR: whitesmoke" tabIndex="1" runat="server"
																				ReadOnly="True" width="95%" CssClass="FLATTEXTBOX"></asp:textbox></TD>
																		<TD align="left">
																			<asp:textbox id="TextboxRiskGroup" style="BACKGROUND-COLOR: whitesmoke" tabIndex="1" runat="server"
																				ReadOnly="True" width="95%" CssClass="FLATTEXTBOX"></asp:textbox></TD>
																	</TR>
																</TABLE>
															</TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
											<TR>
												<TD vAlign="bottom" noWrap align="left">
													<HR style="WIDTH: 100%; HEIGHT: 1px" SIZE="1">
												</TD>
											</TR>
											<TR>
												<TD colSpan="3">
													<asp:datagrid id="Grid" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
														BorderWidth="1px" AllowSorting="True" CellPadding="1" BorderColor="#999999" BackColor="#DEE3E7"
														BorderStyle="Solid" OnItemCreated="ItemCreated" OnItemCommand="ItemCommand">
														<SelectedItemStyle Wrap="False" BackColor="LavenderBlush"></SelectedItemStyle>
														<EditItemStyle Wrap="False" BackColor="AliceBlue"></EditItemStyle>
														<AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
														<ItemStyle Wrap="False" BackColor="White"></ItemStyle>
														<HeaderStyle></HeaderStyle>
														<Columns>
															<asp:TemplateColumn>
																<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
																<ItemTemplate>
																	<asp:ImageButton id="EditButton_WRITE" style="CURSOR: hand" runat="server" CommandName="EditRecord"
																		ImageUrl="../Navigation/images/icons/edit2.gif"></asp:ImageButton>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn>
																<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
																<ItemTemplate>
																	<asp:ImageButton id="DeleteButton_WRITE" style="CURSOR: hand" runat="server" CommandName="DeleteRecord"
																		ImageUrl="../Navigation/images/icons/trash.gif"></asp:ImageButton>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn Visible="False" HeaderText="Id">
																<ItemTemplate>
																	&gt;
																	<asp:Label id=IdLabel runat="server" text='<%# GetGuidStringFromByteArray(Container.DataItem("parts_info_id"))%>'>
																	</asp:Label>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="Description" HeaderText="DESCRIPTION">
																<ItemStyle HorizontalAlign="Left" Width="60%"></ItemStyle>
																<ItemTemplate>
																	<asp:Label id=DescriptionLabel runat="server" visible="True" text='<%# Container.DataItem("description")%>'>
																	</asp:Label>
																</ItemTemplate>
																<EditItemTemplate>
																	<asp:DropDownList id="DescriptionDropDownList" runat="server" visible="True"></asp:DropDownList>
																</EditItemTemplate>
															</asp:TemplateColumn>
															<asp:TemplateColumn SortExpression="COST" HeaderText="COST">
																<ItemStyle HorizontalAlign="Left" Width="30%"></ItemStyle>
																<ItemTemplate>
																	<asp:Label id=CostLabel runat="server" visible="True" text='<%# Container.DataItem("cost")%>'>
																	</asp:Label>
																</ItemTemplate>
																<EditItemTemplate>
																	<asp:TextBox id="CostTextBox" runat="server" visible="True"></asp:TextBox>
																</EditItemTemplate>
															</asp:TemplateColumn>
														</Columns>
														<PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7" PageButtonCount="15"
															Mode="NumericPages"></PagerStyle>
													</asp:datagrid></TD>
											</TR>
											<TR>
												<TD style="WIDTH: 45%" align="right" colSpan="3">
													<asp:label id="LabelTotalCost" runat="server">Total_Cost</asp:label>:&nbsp;
													<asp:TextBox id="TextTotalCost" style="BACKGROUND-COLOR: whitesmoke" runat="server" ReadOnly="True"
														CssClass="FLATTEXTBOX">Claim_Number</asp:TextBox></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD align="left">
										<asp:button id="btnBack" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/back_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											tabIndex="37" runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" Text="Back"
											height="20px" CausesValidation="False"></asp:button>&nbsp;
										<asp:button id="NewButton_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server" Font-Bold="false" Width="100px" CssClass="FLATBUTTON" Text="New" height="20px"></asp:button>
										<asp:button id="SaveButton_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/save_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server" Font-Bold="false" Width="100px" CssClass="FLATBUTTON" Text="Save" height="20px"></asp:button>&nbsp;
										<asp:button id="CancelButton" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/cancel_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server" Font-Bold="false" Width="100px" CssClass="FLATBUTTON" Text="Cancel" height="20px"></asp:button>&nbsp;
									</TD>
								</TR>
							</TABLE>
						</asp:panel></td>
				</tr>
			</TABLE>
		</form>
	</body>
</HTML>
