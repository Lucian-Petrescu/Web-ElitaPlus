<%@ Page Language="vb" AutoEventWireup="false" Codebehind="VSCCertificateListForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Certificates.VSCCertificateListForm"%>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
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
	<body onresize="" leftMargin="0" topMargin="0" onload="changeScrollbarColor();" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--Start Header-->
			<TABLE style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; WIDTH: 98%; BORDER-BOTTOM: black 1px solid; HEIGHT: 20px"
				cellSpacing="0" cellPadding="0" width="869" bgColor="#d5d6e4" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE width="100%" border="0">
							<TR>
								<TD height="20"><asp:label id="moTitleLabel" runat="server" CssClass="TITLELABEL">Certificates</asp:label>:&nbsp;
								<asp:label id="moTitle2Label" runat="server" CssClass="TITLELABELTEXT">VEHICLE_LICENSE_TAG</asp:label>&nbsp;<asp:label id="Label1a" runat="server"  CssClass="TITLELABELTEXT">Search</asp:label></TD>
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
					<td vAlign="top" align="center" width="98%" height="100%"><asp:panel id="WorkingPanel" runat="server" Height="100%" Width="100%">
							<TABLE id="tblMain1" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid"
								height="98%" cellSpacing="0" cellPadding="6" rules="cols" width="98%" align="center"
								bgColor="#fef9ea" border="0">
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
														cellSpacing="0" cellPadding="6" rules="cols" width="98%" align="center" bgColor="#f1f1f1"
														border="0"> <!--fef9ea-->
														<TR>
															<TD align="center">
																<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
																	<TR>
																		<TD style="HEIGHT: 12px" noWrap align="left" width="1%">
																			<asp:label id="moVehicleLicenceTagLabel" runat="server">VEHICLE_LICENSE_TAG</asp:label>:</TD>
																		<TD style="HEIGHT: 12px" noWrap align="left" width="1%"></TD>
																		<TD style="HEIGHT: 12px" noWrap align="left" width="1%"></TD>
																	</TR>
																	<TR>
																		<TD noWrap align="left">
																			<asp:textbox id="moVehicleLicenceTagText" runat="server" Width="100%" AutoPostBack="False" CssClass="FLATTEXTBOX_TAB"></asp:textbox></TD>
																		<TD noWrap align="left"></TD>
																		<TD noWrap align="left"></TD>
																	<TR>
																		<td align="left" nowrap></td>
																		<td align="right" colspan="3" nowrap>
																			<asp:Button ID="btnClearSearch" 
                                                                            runat="server" CssClass="FLATBUTTON" Font-Bold="false" height="20px" 
                                                                            style="BACKGROUND-IMAGE: url(../Navigation/images/icons/clear_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat" 
                                                                            Text="Clear" Width="90px"></asp:Button>&#160;&#160;
																			<asp:Button ID="btnSearch" 
                                                                            runat="server" CssClass="FLATBUTTON" Font-Bold="false" height="20px" 
                                                                            style="BACKGROUND-IMAGE: url(../Navigation/images/icons/search_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat" 
                                                                            Text="Search" Width="90px"></asp:Button></td>
																	
																</TABLE>
															</TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
											<TR>
												<TD colSpan="2">
													<hr style="HEIGHT: 1px"/>
												</TD>
											</TR>
											<TR id="trPageSize" runat="server">
												<TD style="HEIGHT: 22px" vAlign="top" align="left">
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
												<TD style="HEIGHT: 22px" align="right">
													<asp:label id="lblRecordCount" Runat="server"></asp:label></TD>
											</TR>
											<TR>
												<TD vAlign="top" align="center" colSpan="2" height="100%"><!--<DIV id="scroller" style="OVERFLOW: auto; WIDTH: 100%; HEIGHT: 400px" align="center">-->
													<asp:datagrid id="Grid" runat="server" Width="100%" height="100%" OnItemCreated="ItemCreated"
														OnItemCommand="ItemCommand" AutoGenerateColumns="False" BorderStyle="Solid" BorderWidth="1px"
														BackColor="#DEE3E7" BorderColor="#999999" CellPadding="1" AllowPaging="true" AllowSorting="false"
														showheader="true">
														<SelectedItemStyle Wrap="False" BorderStyle="Solid" BorderColor="Orange" BackColor="LightSteelBlue"></SelectedItemStyle>
														<EditItemStyle Wrap="False"></EditItemStyle>
														<AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
														<ItemStyle Wrap="False" BackColor="White"></ItemStyle>
														<HeaderStyle></HeaderStyle>
														<Columns>
															<asp:TemplateColumn>
																<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="30px"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
																<ItemTemplate>
																	<asp:ImageButton id="btnEdit" style="CURSOR: hand" runat="server" CommandName="SelectAction" ImageUrl="../Navigation/images/icons/edit2.gif"></asp:ImageButton>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:BoundColumn Visible="False" HeaderText="Certificate_Id"></asp:BoundColumn>
															<asp:BoundColumn SortExpression="vehicle_licence_tag" HeaderText="vehicle_license_tag">
																<HeaderStyle Wrap="False" HorizontalAlign="Center" ForeColor="#12135B" Width="25%"></HeaderStyle>
																<ItemStyle Wrap="False" HorizontalAlign="Left" Width="25%"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn SortExpression="Certificate" HeaderText="Certificate">
																<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="30%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Left" Width="30%"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn>
																<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="15px"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center" Width="15px"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn SortExpression="customer_name" HeaderText="customer_name">
																<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="40%"></HeaderStyle>
																<ItemStyle Wrap="False" HorizontalAlign="Center" Width="40%"></ItemStyle>
															</asp:BoundColumn>
														</Columns>
														<PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7" PageButtonCount="10"
															Mode="NumericPages"></PagerStyle>
													</asp:datagrid></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
							</TABLE>
						</asp:panel>
					</td>
				</tr>
			</TABLE>
		</form>
		<script>
	function resizeScroller(item)
			{
				var browseWidth, browseHeight;
				
				if (document.layers)
				{
					browseWidth=window.outerWidth;
					browseHeight=window.outerHeight;
				}
				if (document.all)
				{
					browseWidth=document.body.clientWidth;
					browseHeight=document.body.clientHeight;
				}
				
				if (screen.width == "800" && screen.height == "600") 
				{
					newHeight = browseHeight - 220;
				}
				else
				{
					newHeight = browseHeight - 975;
				}
				
				if (newHeight < 470)
				{
					newHeight = 470;
				}
					
				item.style.height = String(newHeight) + "px";
				
				return newHeight;
			}
			
			//resizeScroller(document.getElementById("scroller"));
		</script>
	</body>
</HTML>
