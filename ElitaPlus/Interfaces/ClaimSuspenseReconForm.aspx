<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ClaimSuspenseReconForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ClaimSuspenseReconForm"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ClaimSuspenseReconForm</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="STYLESHEET">
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>
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
									<asp:label id="Label3" runat="server"  CssClass="TITLELABELTEXT">CLAIM_SUSPENSE</asp:label></TD>
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
									<TD align="center" width="75%" colSpan="2">
										<uc1:ErrorController id="moErrorController" runat="server"></uc1:ErrorController></TD>
								</TR>
								<TR>
									<TD align="center" width="75%" colSpan="2">
										<TABLE id="tblSearch" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; WIDTH: 98%; BORDER-BOTTOM: #999999 1px solid; HEIGHT: 76px"
											cellSpacing="0" cellPadding="6" rules="cols" width="98%" align="center" bgColor="#f1f1f1"
											border="0"> <!--fef9ea-->
											<TR>
												<TD align="center">
													<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
														<TR>
															<TD style="HEIGHT: 12px" noWrap align="left" width="1%">
																<asp:label id="moCertificateLabel" runat="server">Certificate</asp:label>:</TD>
															<TD style="HEIGHT: 12px" noWrap align="left" width="1%">
																<asp:label id="moAuthorizationNumberLabel" runat="server">Authorization Number</asp:label>:</TD>
															<TD style="HEIGHT: 12px" noWrap align="left" width="1%">
																<asp:label id="moFileNameLabel" runat="server">File Name</asp:label>:</TD>
														</TR>
														<TR>
															<TD noWrap align="left">
																<asp:textbox id="moCertificateText" runat="server" Width="90%" AutoPostBack="False" CssClass="FLATTEXTBOX_TAB"></asp:textbox></TD>
															<TD noWrap align="left">
																<asp:textbox id="moAuthorizationNumberText" runat="server" Width="90%" AutoPostBack="False" CssClass="FLATTEXTBOX_TAB"></asp:textbox></TD>
															<TD noWrap align="left">
																<asp:textbox id="moFileNameText" runat="server" Width="100%" AutoPostBack="False" CssClass="FLATTEXTBOX_TAB"></asp:textbox></TD>
														<TR>
														<TR>
															<TD height="10"></TD>
														</TR>
														<TR>
															<TD noWrap align="right" colSpan="3">
																<asp:button id="btnClearSearch" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/clear_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
																	runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" height="20px" Text="Clear"></asp:button>&nbsp;&nbsp;
																<asp:button id="btnSearch" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/search_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
																	runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" height="20px" Text="Search"></asp:button></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD>
										<TABLE style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
											cellSpacing="1" cellPadding="1" width="100%" bgColor="#d5d6e4" border="0">
											<TR>
												<TD style="WIDTH: 435px; HEIGHT: 19px" align="right" height="19"></TD>
											</TR>
											<TR id="trPageSize" runat="server" visible="False">
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
														size="1" name="HiddenSavePagePromptResponse" Runat="server"><INPUT id="HiddenIsPageDirty" style="WIDTH: 8px; HEIGHT: 18px" type="hidden" size="1" name="HiddenIsPageDirty"
														Runat="server"></TD>
												<TD align="right">
													<asp:label id="lblRecordCount" Runat="server"></asp:label></TD>
											</TR>
											<TR>
												<TD colSpan="3">
													<DIV id="scroller" style="OVERFLOW: auto; WIDTH: 710px" align="center">
														<TABLE id="Table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
															<TR>
																<TD align="left" width="100%">
																	<asp:datagrid id="moDataGrid" runat="server" Width="100%" AutoGenerateColumns="False" OnItemDataBound="DataGridItemDataBound"
																		BorderWidth="1px" CellPadding="1" BorderColor="#999999" BorderStyle="Solid" AllowPaging="True"
																		 CssClass="DATAGRID_NOWRAP" OnItemCreated="ItemCreated">
																		<SelectedItemStyle Wrap="False" BackColor="Transparent"></SelectedItemStyle>
																		<AlternatingItemStyle Wrap="False" VerticalAlign=Top></AlternatingItemStyle>
																		<ItemStyle Wrap="False" VerticalAlign=Top></ItemStyle>
																		<HeaderStyle  Wrap="False" ForeColor="#003399" BackColor="#DEE3E7"></HeaderStyle>
																		<Columns>
																			<asp:TemplateColumn Visible="FALSE" HeaderText="ID">
																				<ItemStyle HorizontalAlign="Left" BackColor="White" VerticalAlign=Top></ItemStyle>
																				<ItemTemplate>
																					<asp:Label id="moIdLabel" runat="server"></asp:Label>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																			<asp:TemplateColumn HeaderText="CERTIFICATE" ItemStyle-HorizontalAlign="Left" >
																				<ItemTemplate>
																					<asp:Label Runat="server" ID="lblCertificateNumber"></asp:Label>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																			<asp:TemplateColumn Visible="True" HeaderText="FILENAME">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Left" Width="100px" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:Label id="moFileNameLabelGrid" runat="server"></asp:Label>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																			<asp:TemplateColumn Visible="True" HeaderText="">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Left" Width="100px" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:Label id="moClaimActionLabelGrid" runat="server"></asp:Label>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																			<asp:TemplateColumn HeaderText="SEQUENCE">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="center" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moSequenceTextGrid" runat="server" Width="35px" style="text-align:center" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True" onkeydown="oldVal=this.value;" onkeyup="checkNum(this);"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																			<asp:TemplateColumn HeaderText="DO_NOT_PROCESS">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="center" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moDoNotProcessTextGrid" runat="server" Width="35px" style="text-align:center"
																						onkeypress='onlyYesNo(this)' onFocus="setHighlighter(this)" onMouseover="setHighlighter(this)"
																						visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																		
																			<asp:TemplateColumn HeaderText="AMOUNT">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Left" Width="50px" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moAmountTextGrid" runat="server" Width="50px" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																			<asp:TemplateColumn HeaderText="PRODUCT_CODE">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Left" Width="50px" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moProductCodeTextGrid" runat="server" Width="50px" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																			<asp:TemplateColumn HeaderText="MODEL">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Left" Width="80px" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moModelTextGrid" runat="server" Width="80px" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																																						
																			<asp:TemplateColumn HeaderText="AUTHORIZATION_NUMBER">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Left" Width="80px" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moAuthorizationNumberTextGrid" runat="server" Width="80px" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																			
																			
																			<asp:TemplateColumn HeaderText="AUTHORIZATION_CODE">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Left" Width="50px" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moAuthorizationCodeTextGrid" runat="server" Width="50px" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																			<asp:TemplateColumn HeaderText="AUTHORIZATION_CREATION_DATE">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moAuthorizationCreationDateTextGrid" runat="server" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True" Width="80px"></asp:TextBox>
																					<asp:ImageButton id="ImgAuthorizationCreationDateTextGrid" runat="server" TabIndex="-1" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:ImageButton>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																			<asp:TemplateColumn HeaderText="REJECT_REASON">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Left" Width="214px" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moRejectReasonTextGrid" onmouseover="setHighlighter(this)" onfocus="setHighlighter(this)"
																						runat="server" Width="214px" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																			<asp:TemplateColumn HeaderText="DEALER_CODE">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Left" Width="35px" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moDealerCodeTextGrid" runat="server" onFocus="setHighlighter(this)" onMouseover="setHighlighter(this)"
																						visible="True" Width="35px"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																			<asp:TemplateColumn HeaderText="CERTIFICATE_SALES_DATE">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moCertificateSalesDateTextGrid" runat="server" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True" Width="80px"></asp:TextBox>
																					<asp:ImageButton id="ImgCertificateSalesDateTextGrid" runat="server" TabIndex="-1" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:ImageButton>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																			<asp:TemplateColumn HeaderText="DATE_CLAIM_CLOSED">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Left" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moDateClaimClosedTextGrid" runat="server" onFocus="setHighlighter(this)" onMouseover="setHighlighter(this)"
																						visible="True" Width="80px"></asp:TextBox>
																					<asp:ImageButton id="ImgDateClaimClosedTextGrid" runat="server" TabIndex="-1" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:ImageButton>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																			<asp:TemplateColumn HeaderText="SERVICE_CENTER_CODE">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Left" Width="80px" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moServiceCenterCodeTextGrid" runat="server" onFocus="setHighlighter(this)" onMouseover="setHighlighter(this)"
																						visible="True" Width="80px"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																			<asp:TemplateColumn HeaderText="SERIAL_NUMBER">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Left" Width="100px" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moSerialNumberTextGrid" runat="server" onFocus="setHighlighter(this)" onMouseover="setHighlighter(this)"
																						visible="True" Width="100px"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																			<asp:TemplateColumn HeaderText="ADDITIONAL_PRODUCT_CODE">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Left" Width="50px" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moAdditionalProductCodeTextGrid" runat="server" Width="50px" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																			<asp:TemplateColumn HeaderText="MANUFACTURER">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Left" Width="80px" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moManufacturerTextGrid" runat="server" Width="80px" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																			<asp:TemplateColumn HeaderText="STATUS_CODE">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Left" Width="80px" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moStatusCodeTextGrid" runat="server" Width="80px" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																			<asp:TemplateColumn HeaderText="PROBLEM_DESCRIPTION">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Left" Width="50px" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:Label Runat="server" ID="moProblemDescriptionTextGrid" Width="200"></asp:Label>
																				</ItemTemplate>
																			</asp:TemplateColumn>
																		</Columns>
																		<PagerStyle HorizontalAlign="left" ForeColor="DarkSlateBlue" BackColor="#DEE3E7" PageButtonCount="10"
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
														runat="server" Font-Bold="false" Width="135px" CssClass="FLATBUTTON" height="20px" Text="PROCESS_RECORDS"></asp:button>&nbsp;
													<asp:button id="BtnRejectReport" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
														runat="server" Font-Bold="false" Width="161px" CssClass="FLATBUTTON" height="20px" Text="SUSPENSE_REPORT"
														CausesValidation="False"></asp:button></TD>
												<TD align="right">
													<asp:Label id="LbGridNavigation" runat="server" Width="371px">GRID_NAVIGATION</asp:Label></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
							</TABLE>
						</asp:panel>
					</td>
				</tr>
			</TABLE>
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
					newHeight = browseHeight - 230;
				}
				else
				{
					newHeight = browseHeight - 230;
				}
					
				item.style.height = String(newHeight) + "px";
				
				item.style.width = String(browseWidth - 50) + "px";
				
			}	
			
			resizeForm(document.getElementById("scroller"));
			
			var oldVal = 0;
			
			function checkNum(num){
			    
			    if (isNaN(num.value)){
			      num.value = oldVal;
			      return false;
			    } else {
			       return true;
			    }
			    			
			}
		</script>
		</form>
		
	</body>
</HTML>
