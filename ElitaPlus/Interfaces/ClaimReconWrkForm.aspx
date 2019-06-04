<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ClaimReconWrkForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Interfaces.ClaimReconWrkForm" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ProductConversionForm</title>
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
	arColumnMap[":moDoNotProcessTextGrid"] = ++n;
	arColumnMap["col"+n] = ":moDoNotProcessTextGrid"
	arColumnMap[":moDealerCodeTextGrid"] = ++n;
	arColumnMap["col"+n] = ":moDealerCodeTextGrid"
	arColumnMap[":moCertificateTextGrid"] = ++n;
	arColumnMap["col"+n] = ":moCertificateTextGrid"
	arColumnMap[":moCertificateSalesDateTextGrid"] = ++n;
	arColumnMap["col"+n] = ":moCertificateSalesDateTextGrid"
	arColumnMap[":moAuthorizationNumberTextGrid"] = ++n;
	arColumnMap["col"+n] = ":moAuthorizationNumberTextGrid"
	arColumnMap[":moAuthorizationCreationDateTextGrid"] = ++n;
	arColumnMap["col"+n] = ":moAuthorizationCreationDateTextGrid"
	arColumnMap[":moAuthorizationCodeTextGrid"] = ++n;
	arColumnMap["col"+n] = ":moAuthorizationCodeTextGrid"
	arColumnMap[":moProblemDescriptionTextGrid"] = ++n;
	arColumnMap["col"+n] = ":moProblemDescriptionTextGrid"
	arColumnMap[":moProductCodeTextGrid"] = ++n;
	arColumnMap["col"+n] = ":moProductCodeTextGrid"
    arColumnMap[":moAdditionalProductCodeTextGrid"] = ++n;
	arColumnMap["col"+n] = ":moAdditionalProductCodeTextGrid"
	arColumnMap[":moManufacturerTextGrid"] = ++n;
	arColumnMap["col"+n] = ":moManufacturerTextGrid"
    arColumnMap[":moModelTextGrid"] = ++n;
	arColumnMap["col"+n] = ":moModelTextGrid"
    arColumnMap[":moSerialNumberTextGrid"] = ++n;
	arColumnMap["col"+n] = ":moSerialNumberTextGrid"
    arColumnMap[":moServiceCenterCodeTextGrid"] = ++n;
	arColumnMap["col"+n] = ":moServiceCenterCodeTextGrid"
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
									<asp:label id="Label3" runat="server" CssClass="TITLELABELTEXT">NEW_CLAIMS</asp:label></TD>
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
										<asp:TextBox id="moDealerNameText" runat="server" Width="200px" visible="True" Enabled="False"
											ReadOnly="True"></asp:TextBox>
										<asp:Label id="moFileNameLabel" runat="server">FILENAME:</asp:Label>
										<asp:TextBox id="moFileNameText" runat="server" Width="200px" visible="True" Enabled="False"
											ReadOnly="True"></asp:TextBox></TD>
								</TR>
								<TR>
									<TD>
										<TABLE style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
											cellSpacing="1" cellPadding="1" width="100%" bgColor="#d5d6e4" border="0">
											<TR>
												<TD style="HEIGHT: 19px" height="19"></TD>
											</TR>
											<TR id="trPageSize" runat="server">
												<TD style="WIDTH: 627px" vAlign="top" align="left">
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
														size="1" name="HiddenSavePagePromptResponse" Runat="server">
                                                    <INPUT id="HiddenIsPageDirty" style="WIDTH: 8px; HEIGHT: 18px" type="hidden" size="1" name="HiddenIsPageDirty"
														Runat="server"></TD>
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
																	<asp:datagrid id="moDataGrid" runat="server" Width="100%" CssClass="DATAGRID_NOWRAP" OnItemCommand="ItemCommand" OnItemCreated="ItemCreated"
																		BorderStyle="Solid" BackColor="#DEE3E7" BorderColor="#999999" CellPadding="1" AllowSorting="True"
																		BorderWidth="1px" AutoGenerateColumns="False" AllowPaging="True">
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
																			<asp:TemplateColumn SortExpression="DO_NOT_PROCESS" HeaderText="DO_NOT_PROCESS">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Center" Width="60px" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moDoNotProcessTextGrid" runat="server" HorizontalAlign="Center" Width="60px"
																						onkeypress='onlyYesNo(this)' onFocus="setHighlighter(this)" onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																				<FooterStyle HorizontalAlign="Center"></FooterStyle>
																			</asp:TemplateColumn>
																			<asp:TemplateColumn SortExpression="DEALER_CODE" HeaderText="DEALER_CODE">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Left" Width="35px" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moDealerCodeTextGrid" runat="server" onFocus="setHighlighter(this)" onMouseover="setHighlighter(this)"
																						visible="True" Width="35px"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																			<asp:TemplateColumn SortExpression="CERTIFICATE" HeaderText="CERTIFICATE">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moCertificateTextGrid" runat="server" Width="100px" align="right" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																			<asp:TemplateColumn SortExpression="CERTIFICATE_SALES_DATE" HeaderText="CERTIFICATE_SALES_DATE">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Left" Width="110px" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moCertificateSalesDateTextGrid" runat="server" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True" Width="80px"></asp:TextBox>
																					<asp:ImageButton id="ImgCertificateSalesDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:ImageButton>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																			<asp:TemplateColumn SortExpression="AUTHORIZATION_NUMBER" HeaderText="AUTHORIZATION_NUMBER">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Left" Width="80px" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moAuthorizationNumberTextGrid" runat="server" Width="80px" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																			<asp:TemplateColumn SortExpression="AUTHORIZATION_CREATION_DATE" HeaderText="AUTHORIZATION_CREATION_DATE">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Left" Width="110px" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moAuthorizationCreationDateTextGrid" runat="server" Width="80px" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																					<asp:ImageButton id="ImgAuthorizationCreationDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:ImageButton>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																			<asp:TemplateColumn SortExpression="AUTHORIZATION_CODE" HeaderText="AUTHORIZATION_CODE">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Right" Width="100px" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moAuthorizationCodeTextGrid" runat="server" Width="100px" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																				<FooterStyle HorizontalAlign="Right"></FooterStyle>
																			</asp:TemplateColumn>
																			<asp:TemplateColumn SortExpression="PROBLEM_DESCRIPTION" HeaderText="PROBLEM_DESCRIPTION">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Left" Width="400px" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moProblemDescriptionTextGrid" runat="server" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True" Width="400px"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																			<asp:TemplateColumn SortExpression="PRODUCT_CODE" HeaderText="PRODUCT_CODE">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Left" Width="45px" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moProductCodeTextGrid" Width="45px" runat="server" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																			<asp:TemplateColumn SortExpression="ADDITIONAL_PRODUCT_CODE" HeaderText="ADDITIONAL_PRODUCT_CODE">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Left" Width="45px" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moAdditionalProductCodeTextGrid" Width="80px" runat="server" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																			<asp:TemplateColumn SortExpression="MANUFACTURER" HeaderText="MANUFACTURER">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Right" Width="100px" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moManufacturerTextGrid" runat="server" Width="100px" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																			<asp:TemplateColumn SortExpression="MODEL" HeaderText="MODEL">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Left" Width="160px" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moModelTextGrid" runat="server" Width="160px" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																			<asp:TemplateColumn SortExpression="SERIAL_NUMBER" HeaderText="SERIAL_NUMBER">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moSerialNumberTextGrid" runat="server" onFocus="setHighlighter(this)" onMouseover="setHighlighter(this)"
																						visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																			<asp:TemplateColumn SortExpression="SERVICE_CENTER_CODE" HeaderText="SERVICE_CENTER_CODE">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Left" Width="100%" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moServiceCenterCodeTextGrid" runat="server" Width="100%" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																			<asp:TemplateColumn SortExpression="AMOUNT" HeaderText="AMOUNT">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Left" Width="100%" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moAmountTextGrid" runat="server" Width="100%" onFocus="setHighlighter(this)"
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
												<TD align="left" height="28">
													<asp:button id="btnSave_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/save_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
														runat="server" Font-Bold="false" Width="100px" Text="Save" height="20px" CssClass="FLATBUTTON"></asp:button>&nbsp;
													<asp:button id="btnUndo_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/cancel_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
														tabIndex="195" runat="server" Font-Bold="false" Width="90px" Text="Undo" height="20px" CssClass="FLATBUTTON"
														CausesValidation="False"></asp:button></TD>
												<TD align="right">
													<asp:Label id="LbGridNavigation" runat="server" Width="371px">GRID_NAVIGATION</asp:Label></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD vAlign="bottom" align="left" height="50">
										<asp:button id="btnBack" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/back_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											tabIndex="185" runat="server" Font-Bold="false" Width="90px" Text="Back" height="20px" CssClass="FLATBUTTON"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
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
