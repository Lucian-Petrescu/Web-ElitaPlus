<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ServiceNotificationReconWrkForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Interfaces.ServiceNotificationReconWrkForm"%>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>ServiceNotificationReconWrkFormForm</title>
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
	arColumnMap[":moRequestEndDateTextGrid"] = ++n;
	arColumnMap["col"+n] = ":moRequestEndDateTextGrid"
	arColumnMap[":moDescriptionTextGrid"] = ++n;
	arColumnMap["col"+n] = ":moDescriptionTextGrid"
	arColumnMap[":moAmountTextGrid"] = ++n;
	arColumnMap["col"+n] = ":moAmountTextGrid"
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
									<asp:label id="Label3" runat="server"  CssClass="TITLELABELTEXT">SERVICE_NOTIFICATION</asp:label></TD>
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
										<uc1:ErrorController id="moErrorController" runat="server"></uc1:ErrorController>&nbsp;&nbsp;</TD>
								</TR>
								<TR>
									<TD style="HEIGHT: 30px" align="center" width="75%" colSpan="2">
										<asp:Label id="moDealerNameLabel" runat="server" visible="True">SERVICE_NOTIFICATION_INTERFACE</asp:Label>:
										<asp:TextBox id="moDealerNameText" runat="server" Width="200px" visible="True" ReadOnly="True"
											Enabled="False"></asp:TextBox>
										<asp:Label id="moFileNameLabel" runat="server" visible="True">FILENAME</asp:Label>:
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
																	<asp:GridView id="moDataGrid" runat="server" Width="100%" AllowPaging="True" AutoGenerateColumns="False"
																		BorderWidth="1px" AllowSorting="True" CellPadding="1" BorderColor="#999999" BackColor="#DEE3E7"
																		BorderStyle="Solid" OnItemCreated="ItemCreated" >
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
																					<asp:Label id="moDealerReconWrkIdLabel" runat="server" text='<%# GetGuidStringFromByteArray(Container.DataItem("svc_notification_recon_wrk_id"))%>'>
																					</asp:Label>
																				</ItemTemplate>
																			</asp:TemplateField>
																			<asp:TemplateField SortExpression="REJECT_REASON" HeaderText="REJECT_REASON">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Left" Width="214px" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:Label id="moRejectReasonTextGrid" onmouseover="setHighlighter(this)" onfocus="setHighlighter(this)"
																						runat="server" Width="214px" visible="True"></asp:Label>
																				</ItemTemplate>
																			</asp:TemplateField>
																			<asp:TemplateField SortExpression="SVC_NOTIFICATION_TYPE" HeaderText="SVC_NOTIFICATION_TYPE">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moServiceNotificationTypeTextGrid" runat="server" align="right" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateField>
																			<asp:TemplateField SortExpression="DESCRIPTION" HeaderText="DESCRIPTION">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Left" Width="150px" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moDescriptionTextGrid" runat="server"  Width="150px" align="right" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateField>
																			<asp:TemplateField SortExpression="PRP_COD_AMT" HeaderText="PRP_COD_AMT">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Left" Width="100%" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moAmountTextGrid" runat="server" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateField>
																			<asp:TemplateField SortExpression="REJECT_CODE" HeaderText="REJECT_CODE">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Left" Width="100%" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:Label id="moRejectcodeTextGrid" runat="server" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:Label>
																				</ItemTemplate>
																			</asp:TemplateField>
																			<asp:TemplateField SortExpression="svc_notification_number" HeaderText="SERVICE_NOTIFICATION_NUMBER">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Left" Width="100%" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moServiceNotificationNumberTextGrid" runat="server" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateField>
																			<asp:TemplateField SortExpression="ARTICLE_NUMBER" HeaderText="ARTICLE_NUMBER">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Left" Width="100%" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moArticleNumberTextGrid" runat="server" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateField>
														
																			<asp:TemplateField SortExpression="CUST_ACCT_NUMBER" HeaderText="CUST_ACCT_NUMBER">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Left" Width="100%" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moAccountCustomerNumberTextGrid" runat="server" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateField>
																			<asp:TemplateField SortExpression="CUST_NAME_1" HeaderText="CUST_NAME_1">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Left" Width="100%" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moCustomerName1TextGrid" runat="server" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateField>
																			<asp:TemplateField SortExpression="CUST_NAME_2" HeaderText="CUST_NAME_2">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Left" Width="100%" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moCustomerName2TextGrid" runat="server" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateField>
																			<asp:TemplateField SortExpression="CUST_CITY" HeaderText="CITY">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moCustomerCityTextGrid" runat="server" align="right" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateField>
																			<asp:TemplateField SortExpression="CUST_POSTAL_CODE" HeaderText="POSTAL_CODE">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moCustomerPostalCode" runat="server" align="right" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateField>
																			<asp:TemplateField SortExpression="CUST_REGION" HeaderText="REGION">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moCustomerRegion" runat="server" align="right" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateField>
																			<asp:TemplateField SortExpression="CUST_ADDRESS" HeaderText="ADDRESS">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moCustomerAddress" runat="server" align="right" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateField>
																			<asp:TemplateField SortExpression="CUST_PHONE_NUMBER" HeaderText="PHONE">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moCustomerPhoneNumber" runat="server" align="right" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateField>
																			<asp:TemplateField SortExpression="CUST_FAX_NUMBER" HeaderText="FAX">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moCustomerFaxNumber" runat="server" align="right" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateField>
																			<asp:TemplateField SortExpression="EQUIPMENT" HeaderText="EQUIPMENT">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moEquipment" runat="server" align="right" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateField>
																			<asp:TemplateField SortExpression="MFG_NAME" HeaderText="MANUFACTURER_NAME">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moMfgName" runat="server" align="right" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateField>
																			<asp:TemplateField SortExpression="MODEL_NUMBER" HeaderText="MODEL">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moModelNumber" runat="server" align="right" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateField>
																			<asp:TemplateField SortExpression="MFG_PART_NUMBER" HeaderText="PART_NUMBER">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moMfgpartNumber" runat="server" align="right" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateField>
																			<asp:TemplateField SortExpression="SERIAL_NUMBER" HeaderText="SERIAL_NUMBER">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moSerialNumber" runat="server" align="right" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateField>
																			<asp:TemplateField SortExpression="SVC_NOTIFICATION_STATUS" HeaderText="SVC_NOTIFICATION_STATUS">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moSvcNotificationStatus" runat="server" align="right" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateField>
																			<asp:TemplateField SortExpression="SEQ_TASK_NUMBER" HeaderText="SEQ_TASK_NUMBER">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moSeqTaskNumber" runat="server" align="right" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateField>
																			<asp:TemplateField SortExpression="SEQ_TASK_DESCRIPTION" HeaderText="SEQ_TASK_DESCRIPTION">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moSeqTaskDescription" runat="server" align="right" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateField>
																			<asp:TemplateField SortExpression="CONSECUTIVE_ACTIVITY_NUMBER" HeaderText="CONSECUTIVE_ACTIVITY_NUMBER">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moConsecutiveActivityNumber" runat="server" align="right" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateField>
																			<asp:TemplateField SortExpression="ACTIVITY_TEXT" HeaderText="ACTIVITY_TEXT">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moActivityText" runat="server" align="right" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateField>
																			<asp:TemplateField SortExpression="PROBLEM_DESCRIPTION" HeaderText="PROBLEM_DESCRIPTION">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moProblemDescription" runat="server" align="right" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateField>
																			<asp:TemplateField SortExpression="SITE" HeaderText="SITE">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moSite" runat="server" align="right" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateField>
																			<asp:TemplateField SortExpression="TRANSACTION_NUMBER" HeaderText="TRANSACTION_NUMBER">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moTransactionNumber" runat="server" align="right" onFocus="setHighlighter(this)"
																						onMouseover="setHighlighter(this)" visible="True"></asp:TextBox>
																				</ItemTemplate>
																			</asp:TemplateField>
																			<asp:TemplateField SortExpression="created_on" HeaderText="CREATED_ON_DATE">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moCreatedOnDate" runat="server"  onFocus="setHighlighter(this)" onMouseover="setHighlighter(this)"
																						visible="True"></asp:TextBox>
																					<asp:ImageButton id="ImgReplacementDateTextGrid" runat="server" TabIndex="-1" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:ImageButton>
																				</ItemTemplate>
																			</asp:TemplateField>
																			<asp:TemplateField SortExpression="changed_on" HeaderText="CHANGED_ON_DATE">
																				<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
																				<ItemStyle Wrap="False" HorizontalAlign="Left" Width="30%" BackColor="White"></ItemStyle>
																				<ItemTemplate>
																					<asp:TextBox id="moChangedOnDate" runat="server"  onFocus="setHighlighter(this)" onMouseover="setHighlighter(this)"
																						visible="True"></asp:TextBox>
																					<%--<asp:ImageButton id="ImgChangedOnDateTextGrid" runat="server" TabIndex="-1" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:ImageButton>--%>
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
																		<PagerStyle HorizontalAlign="Left" ForeColor="DarkSlateBlue" BackColor="#DEE3E7" CssClass="PAGER_LEFT"></PagerStyle>
																	</asp:GridView></TD>
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
											Text="Back"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</TD>
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
