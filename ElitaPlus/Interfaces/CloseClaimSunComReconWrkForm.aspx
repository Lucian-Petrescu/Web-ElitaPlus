<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CloseClaimSunComReconWrkForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Interfaces.CloseClaimSunComReconWrkForm"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>CloseClaimSunComReconWrkFormForm</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="STYLESHEET">
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>
		<script language="javascript">

    var arColumnMap= new Array();
    var n = 0;
	arColumnMap[":moRejectReasonTextGrid"] = ++n;
	arColumnMap["col"+n] = ":moRejectReasonTextGrid"
	arColumnMap[":moClaimTextGrid"] = ++n;
	arColumnMap["col"+n] = ":moClaimTextGrid"
	arColumnMap[":moReplacementDateTextGrid"] = ++n;
	arColumnMap["col"+n] = ":moReplacementDateTextGrid"
	arColumnMap[":moCertificateTextGrid"] = ++n;
	arColumnMap["col"+n] = ":moCertificateTextGrid"
	arColumnMap[":moAmountTextGrid"] = ++n;
	arColumnMap["col"+n] = ":moAmountTextGrid"

		</script>
	</HEAD>
	<body onresize="resizeForm(document.getElementById('scroller'));" leftMargin="0" topMargin="0"
		onload="changeScrollbarColor();" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--Start Header-->
			<TABLE style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE width="100%" border="0">
							<TR>
								<TD height="20">&nbsp;<asp:label id="Label7" runat="server" CssClass="TITLELABEL">INTERFACES</asp:label>:
									<asp:label id="Label3" runat="server"  CssClass="TITLELABELTEXT">CLOSE_CLAIMS_SUNCOM</asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="tblOuter2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				height="93%" cellSpacing="0" cellPadding="0" rules="none" width="98%" bgColor="#d5d6e4"
				border="0"> <!--d5d6e4-->
				<TR>
					<TD height="5"></TD>
				</TR>
				<tr>
					<td vAlign="top" align="center" height="100%"><asp:panel id="WorkingPanel" runat="server" Width="98%" Height="100px">
							<TABLE id="tblMain1" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid"
								cellSpacing="0" cellPadding="6" rules="cols" width="100%" align="center" bgColor="#fef9ea"
								border="0">
								<TR>
									<TD height="1"></TD>
								</TR>
								<TR>
									<TD style="HEIGHT: 30px" align="center" width="75%" colSpan="2">
										<uc1:ErrorController id="moErrorController" runat="server"></uc1:ErrorController>&nbsp;&nbsp;
									</TD>
								</TR>
								<TR>
									<TD style="HEIGHT: 30px" align="center" width="75%" colSpan="2">
										<asp:Label id="moDealerNameLabel" runat="server">CLAIM_INTERFACE:</asp:Label>
										<asp:TextBox id="moDealerNameText" runat="server" Width="200px" visible="True" ReadOnly="True"
											Enabled="False"></asp:TextBox>
										<asp:Label id="moFileNameLabel" runat="server">FILENAME:</asp:Label>
										<asp:TextBox id="moFileNameText" runat="server" Width="200px" visible="True" ReadOnly="True"
											Enabled="False"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD>
										<TABLE style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
											cellSpacing="1" cellPadding="1" width="100%" bgColor="#d5d6e4" border="0">
											<TR>
												<TD style="WIDTH: 435px; HEIGHT: 19px" align="right" height="19"></TD>
											</TR>
											<TR id="trPageSize" runat="server">
												<TD style="WIDTH: 435px" vAlign="top">
													<asp:label id="lblPageSize" runat="server">Page_Size</asp:label>:&nbsp;
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
                                                    </asp:dropdownlist><INPUT id="HiddenSavePagePromptResponse" style="WIDTH: 8px; HEIGHT: 18px" type="hidden"
                                                                              size="1" name="HiddenSavePagePromptResponse" Runat="server"/>
                                                    <INPUT id="HiddenIsPageDirty" style="WIDTH: 8px; HEIGHT: 18px" type="hidden" size="1" name="HiddenIsPageDirty"
                                                           Runat="server"/></TD>
												<TD align="right">
													<asp:label id="lblRecordCount" Runat="server"></asp:label></TD>
											</TR>
											<TR>
												<TD colSpan="3">
													<DIV id="scroller" onkeydown="arrowKeyHandler()" style="OVERFLOW: auto; WIDTH: 710px; HEIGHT: 365px"
														onclick="ClickHandler()" align="center">
														<TABLE id="Table1" cellSpacing="0" cellPadding="0" border="0">
															<TR>
																<TD noWrap>
																	<asp:datagrid id="moDataGrid" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
																		BorderWidth="1px" AllowSorting="True" CellPadding="1" BorderColor="#999999" BackColor="#DEE3E7"
																		BorderStyle="Solid" OnItemCreated="ItemCreated" OnItemCommand="ItemCommand">
																		<SelectedItemStyle Wrap="False" BackColor="Transparent"></SelectedItemStyle>
																		<EditItemStyle Wrap="False" BackColor="AliceBlue"></EditItemStyle>
																		<AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
																		<ItemStyle Wrap="False" BackColor="White"></ItemStyle>
																		<HeaderStyle  Wrap="False" ForeColor="#003399"></HeaderStyle>
																		<Columns>
																			<asp:TemplateColumn Visible="False">
																				<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Center" Width="30px" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:ImageButton id="EditButton_WRITE" style="CURSOR: hand" runat="server" ImageUrl="../Navigation/images/icons/edit2.gif"
																						CommandName="EditRecord"></asp:ImageButton>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																			<asp:TemplateColumn Visible="False">
																				<ItemStyle HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:Label id="moDealerReconWrkIdLabel" runat="server" text='<%# GetGuidStringFromByteArray(Container.DataItem("claim_recon_wrk_id"))%>'>
																					</asp:Label>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																			<asp:TemplateColumn SortExpression="REJECT_REASON" HeaderText="REJECT_REASON">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Left" Width="214px" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moRejectReasonTextGrid" onmouseover="setHighlighter(this)" onfocus="setHighlighter(this)"
																						runat="server" Width="214px" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																			<asp:TemplateColumn SortExpression="CLAIM" HeaderText="CLAIM">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moClaimTextGrid" runat="server" align="right" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																			<asp:TemplateColumn SortExpression="REPLACEMENT_DATE" HeaderText="REPLACEMENT_DATE">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moReplacementDateTextGrid" runat="server"  onFocus="setHighlighter(this)" onMouseover="setHighlighter(this)"
																						visible="True"></asp:TextBox>
																					<asp:ImageButton id="ImgReplacementDateTextGrid" runat="server" TabIndex="-1" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:ImageButton>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																			<asp:TemplateColumn SortExpression="CERTIFICATE" HeaderText="CERTIFICATE">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Left" Width="150px" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moCertificateTextGrid" runat="server"  Width="150px" align="right" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																			<asp:TemplateColumn SortExpression="AMOUNT" HeaderText="AMOUNT">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Left" Width="100%" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moAmountTextGrid" runat="server" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																			<asp:TemplateColumn Visible="False">
																				<ItemStyle HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:Label id="moModifiedDateLabel" runat="server" text='<%# Container.DataItem("modified_date")%>'>
																					</asp:Label>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																		</Columns>
																		<PagerStyle HorizontalAlign="Left" ForeColor="DarkSlateBlue" BackColor="#DEE3E7" PageButtonCount="15"
																			Mode="NumericPages"></PagerStyle>
																	</asp:datagrid></TD>
															</TR>
														</TABLE>
													</DIV>
												</TD>
											</TR>
											<TR>
												<TD style="WIDTH: 435px" align="left" height="28">
													<asp:button id="btnSave_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/save_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
														runat="server" Font-Bold="false" Width="100px" CssClass="FLATBUTTON" height="20px" Text="Save"></asp:button>&nbsp;
													<asp:button id="btnUndo_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/cancel_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
														tabIndex="195" runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" height="20px"
														Text="Undo" CausesValidation="False"></asp:button></TD>
												<TD align="right">
													<asp:Label id="LbGridNavigation" runat="server" Width="371px">GRID_NAVIGATION</asp:Label></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD vAlign="bottom" align="left" height="50">
										<asp:button id="btnBack" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/back_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											tabIndex="185" runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" height="20px"
											Text="Back"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
									</TD>
								</TR>
							</TABLE>
						</asp:panel></td>
				</tr>
			</TABLE>
		</form>
		<script>
		
			function setDirty(){
				document.getElementById("HiddenIsPageDirty").value = "YES"
			}
			
			function resizeForm(item)
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
					newHeight = browseHeight - 280;
				}
				else
				{
					newHeight = browseHeight - 260;
				}
					
				item.style.height = String(newHeight) + "px";
				
				item.style.width = String(browseWidth - 50) + "px";
				
			}	
			
			resizeForm(document.getElementById("scroller"));
			
		</script>
	</body>
</HTML>
