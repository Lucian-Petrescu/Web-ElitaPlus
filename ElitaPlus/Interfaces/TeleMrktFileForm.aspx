<%@ Page Language="vb" AutoEventWireup="false" Codebehind="TeleMrktFileForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Interfaces.TeleMrktFileForm" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>TeleMrktFileForm</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="STYLESHEET"/>
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>
		<script language="javascript">

		    var arColumnMap = new Array();
		    var n = 0;
		    arColumnMap[":moRejectReasonTextGrid"] = ++n;
		    arColumnMap["col" + n] = ":moRejectReasonTextGrid"
		    arColumnMap[":moDealerCodeTextGrid"] = ++n;
		    arColumnMap["col" + n] = ":moDealerCodeTextGrid"
		    arColumnMap[":moCertificateTextGrid"] = ++n;
		    arColumnMap["col" + n] = ":moCertificateTextGrid"
		    arColumnMap[":moCertificateSalesDateTextGrid"] = ++n;
		    arColumnMap["col" + n] = ":moFirstNameTextGrid"
		    arColumnMap[":moFirstNameTextGrid"] = ++n;
		    arColumnMap["col" + n] = ":moLastNameTextGrid"
		    arColumnMap[":moLastNameTextGrid"] = ++n;
		    arColumnMap["col" + n] = ":moSalesDateTextGrid"
		    arColumnMap[":moSalesDateTextGrid"] = ++n;
		    arColumnMap["col" + n] = ":moCampaignNumberTextGrid"
		    arColumnMap[":moCampaignNumberTextGrid"] = ++n;
		    arColumnMap["col" + n] = ":moTMKLoadedTextGrid"
		    arColumnMap[":moTMKLoadedTextGrid"] = ++n;

		</script>
	</HEAD>
	<body onresize="resizeForm(document.getElementById('scroller'));" leftMargin="0" topMargin="0"
		onload="changeScrollbarColor();" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--Start Header-->
			<table style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				cellspacing="0" cellpadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<tr>
					<td vAlign="top">
						<table width="100%" border="0">
							<tr>
								<td height="20">&nbsp;<asp:label id="Label7" runat="server" CssClass="TITLELABEL">INTERFACES</asp:label>:
									<asp:label id="Label3" runat="server" CssClass="TITLELABELTEXT">TELEMARKETING_FILE</asp:label></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<table id="tblOuter2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				height="65%" cellspacing="0" cellpadding="0" rules="none" width="98%" bgColor="#d5d6e4"
				border="0"> <!--d5d6e4-->
				<tr>
					<td height="3"></td>
				</tr>
				<tr>
					<td vAlign="top" align="center" height="100%"><asp:panel id="WorkingPanel" runat="server" Width="98%" Height="100px">
							<table id="tblMain1" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid"
								cellspacing="0" cellpadding="3" rules="cols" width="100%" align="center" bgColor="#fef9ea"
								border="0">
			
								<tr>
									<td style="HEIGHT: 30px" align="center" width="80%" colSpan="2">
										<uc1:ErrorController id="moErrorController" runat="server"></uc1:ErrorController>&nbsp;&nbsp;</td>
								</tr>
							    <tr>
									<td style="HEIGHT: 30px" align="center" width="80%" colSpan="2">
										<asp:Label id="moCertNumberLabel" runat="server">Certificate_number:</asp:Label>
										<asp:TextBox id="moCertNumberText" runat="server" Width="200px" visible="True" Enabled="true"></asp:TextBox>&nbsp;&nbsp;
										<asp:Label id="moCampaignNumberLabel" runat="server">Campaign_number:</asp:Label>
										<asp:TextBox id="moCampaignNumberText" runat="server" Width="200px" visible="True" Enabled="true"></asp:TextBox>&nbsp;&nbsp;
										<asp:Label id="moStatusLabel" runat="server">Status:</asp:Label>
										<asp:dropdownlist id="cboStatus" runat="server" Width="150px" ></asp:dropdownlist>
										</td>
								</tr>		
				                <tr>
                                    <td colspan="2">
                                        <hr size="1" />
                                    </td>
                                </tr>						
								<tr>
									<td style="HEIGHT: 30px" align="center" width="80%" colSpan="1">
										<asp:Label id="moDealerNameLabel" runat="server">DEALER_NAME:</asp:Label>
										<asp:TextBox id="moDealerNameText" runat="server" Width="200px" visible="True" Enabled="False"
											ReadOnly="True"></asp:TextBox>&nbsp;&nbsp;
										<asp:Label id="moFileNameLabel" runat="server">FILENAME:</asp:Label>
										<asp:TextBox id="moFileNameText" runat="server" Width="200px" visible="True" Enabled="False"
											ReadOnly="True"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
										<asp:button id="btnClearSearch" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/clear_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server" Font-Bold="false" Width="90px" Text="Clear" CssClass="FLATBUTTON" height="20px"></asp:button>&nbsp;
										<asp:button id="btnSearch" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/search_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server" Font-Bold="false" Width="90px" Text="Search" CssClass="FLATBUTTON" height="20px"></asp:button></td>
                                        
								</tr>
								<tr>
									<td height="60%">
										<table style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
											cellspacing="1" cellpadding="1" width="100%"  bgColor="#d5d6e4" border="0">
											<tr>
												<td style="HEIGHT: 19px" height="19"></td>
											</tr>
											<tr id="trPageSize" runat="server">
												<td style="WIDTH: 627px" vAlign="top" align="left">
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
														Runat="server"/></td>
												<td align="right">
													<asp:label id="lblRecordCount" Runat="server"></asp:label></td>
											</tr>
											<tr>
												<td colSpan="3">
											    	<DIV id="scroller" onkeydown="arrowKeyHandler()" style="OVERFLOW: auto; WIDTH: 680px; HEIGHT: 215px"
														onclick="ClickHandler()" align="center">
														<table id="Table1" cellspacing="0" cellpadding="0" border="0">
															<tr>
																<td noWrap>
					                                            <asp:GridView ID="moDataGrid" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
                                                                BorderWidth="1px" AllowSorting="True" CellPadding="1" BackColor="#DEE3E7" OnRowCreated="ItemCreated"
                                                                BorderColor="#999999" BorderStyle="Solid">
                                                                <SelectedRowStyle Wrap="False" BackColor="Transparent"></SelectedRowStyle>
                                                                <EditRowStyle Wrap="False" BackColor="AliceBlue"></EditRowStyle>
                                                                <AlternatingRowStyle Wrap="False" BackColor="#F1F1F1"></AlternatingRowStyle>
                                                                <RowStyle Wrap="False" BackColor="White"></RowStyle>
                                                                <HeaderStyle  Wrap="False" ForeColor="#003399"></HeaderStyle>
																		<Columns>
																			<asp:TemplateField Visible="False">
																				<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Center" Width="30px" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:ImageButton id="EditButton_WRITE" style="CURSOR: hand" runat="server" ImageUrl="../Navigation/images/icons/edit2.gif"
																						CommandName="EditRecord"></asp:ImageButton>
																				</ItemTemplate>
																			</asp:TemplateField>
																			<asp:TemplateField Visible="False">
																				<ItemStyle HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:Label id="moDealerTmkReconWrkIdLabel" runat="server" text='<%# GetGuidStringFromByteArray(Container.DataItem("dealer_tmk_recon_wrk_id"))%>'>
																					</asp:Label>
																				</ItemTemplate>
																			</asp:TemplateField>
																			<asp:TemplateField SortExpression="REJECT_REASON" HeaderText="Reject Reason">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Left" Width="214px" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moRejectReasonTextGrid" onmouseover="setHighlighter(this)" onfocus="setHighlighter(this)"
																						runat="server" Width="214px" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateField>
																			<asp:TemplateField SortExpression="DEALERCODE" HeaderText="Dealer Code">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Left" Width="100px" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moDealerCodeTextGrid" runat="server" onFocus="setHighlighter(this)" onMouseover="setHighlighter(this)"
																						visible="True" Width="100px"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateField>
																			<asp:TemplateField SortExpression="CERTIFICATE" HeaderText="Certificate Number">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Left" Width="150px" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moCertificateTextGrid" runat="server" Width="150px" align="right" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateField>
																			<asp:TemplateField SortExpression="FIRSTNAME" HeaderText="First Name">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Left" Width="180px" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moFirstNameTextGrid" runat="server" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True" Width="180px"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateField>
																			<asp:TemplateField SortExpression="LASTNAME" HeaderText="Last Name">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Left" Width="180px" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moLastNameTextGrid" runat="server" Width="180px" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateField>
																			<asp:TemplateField SortExpression="SALESDATE" HeaderText="Sales Date">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Left" Width="90px" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moSalesDateTextGrid" runat="server" Width="90px" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateField>
																			<asp:TemplateField SortExpression="CAMPAIGN_NUMBER" HeaderText="Campaign Number">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Right" Width="150px" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moCampaignNumberTextGrid" runat="server" Width="150px" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateField>
																			<asp:TemplateField SortExpression="TMK_LOADED" HeaderText="Status">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Left" Width="120px" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moTMKLoadedTextGrid" runat="server" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True" Width="120px"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateField>
																			<asp:TemplateField Visible="False">
																				<ItemStyle HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:Label id="moModifiedDateLabel" runat="server" text='<%# Container.DataItem("modified_date")%>'>
																					</asp:Label>
																				</ItemTemplate>
																			</asp:TemplateField>
																		</Columns>
                                                                        <PagerSettings PageButtonCount="15" Mode="Numeric" />
                                                                        <PagerStyle HorizontalAlign="Left" ForeColor="DarkSlateBlue" BackColor="#DEE3E7"
                                                                            CssClass="PAGER_LEFT"></PagerStyle>
																	</asp:GridView></td>
															</tr>
														</table>
													</DIV>
												</td>
											</tr>
											<tr>
												<td align="left" height="28">
													<asp:button id="btnSave_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/save_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
														runat="server" Font-Bold="false" Width="100px" Text="Save" height="20px" CssClass="FLATBUTTON"></asp:button>&nbsp;
													<asp:button id="btnUndo_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/cancel_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
														tabIndex="195" runat="server" Font-Bold="false" Width="90px" Text="Undo" height="20px" CssClass="FLATBUTTON"
														CausesValidation="False"></asp:button></td>
												<td align="right">
													<asp:Label id="LbGridNavigation" runat="server" Width="371px">GRID_NAVIGATION</asp:Label></td>
											</tr>
										</table>
									</td>
								</tr>
								<tr>
									<td valign="bottom" align="left" height="25">
										<asp:button id="btnBack" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/back_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											tabIndex="185" runat="server" Font-Bold="false" Width="90px" Text="Back" height="20px" CssClass="FLATBUTTON"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
								</tr>
				                <tr>
					                <td height="3"></td>
				                </tr>
								
							</table>
						</asp:panel></td>
				</tr>
                <tr>
	                <td height="3"></td>
                </tr>

			</table>
		</form>
		<script>

		    function setDirty() {
		        document.getElementById("HiddenIsPageDirty").value = "YES"
		    }


		    function resizeForm(item) {
		        var browseWidth, browseHeight;

		        if (document.layers) {
		            browseWidth = window.outerWidth;
		            browseHeight = window.outerHeight;
		        }
		        if (document.all) {
		            browseWidth = document.body.clientWidth;
		            browseHeight = document.body.clientHeight;
		        }

		        if (screen.width == "800" && screen.height == "600") {
		            newHeight = browseHeight - 280;
		        }
		        else {
		            newHeight = browseHeight - 260;
		        }

		        item.style.height = String(newHeight) + "px";

		        item.style.width = String(browseWidth - 50) + "px";

		    }

		    resizeForm(document.getElementById("scroller"));
		</script>
	</body>
</HTML>
