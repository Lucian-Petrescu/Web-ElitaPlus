<%@ Page Language="vb" AutoEventWireup="false" Codebehind="AccountingSettingListForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.AccountingSettingListForm"%>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>TemplateForm</title>
		<META http-equiv="Content-Type" content="text/html; charset=windows-1252">
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="STYLESHEET">
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>		
	    <style type="text/css">
            .DATAGRID TR.ROW TD.LeftCol{{text-align:left;} 
            .DATAGRID TR.ALTROW TD.LeftCol{{text-align:left;} 
            .DATAGRID TR.SELECTED TD.LeftCol{{text-align:left;} 
        </style>
	</HEAD>
	<body leftMargin="0" topMargin="0" onload="changeScrollbarColor();" MS_POSITIONING="GridLayout">
	    <style type="text/css">
            .TABLEMAIN TD.LeftCol {text-align:left;} 
            .TABLEMAIN_MASTER TD.LeftCol {text-align:left;} 
        </style>
		<form id="Form1" method="post" runat="server">
			<!--Start Header-->
			<TABLE style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE width="100%" border="0">
							<TR>
								<TD height="20"><asp:label id="LabelTables" runat="server" cssclass="TITLELABEL">Tables</asp:label>:&nbsp;<asp:label id="Label7" runat="server" cssclass="TITLELABELTEXT">ACCOUNT_SETTINGS_FORM</asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="tblOuter2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid; HEIGHT: 93%"
				cellSpacing="0" cellPadding="0" rules="none" width="98%" bgColor="#d5d6e4" border="0"> <!--d5d6e4-->
				<tr>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td vAlign="top" align="center" height="100%"><asp:panel id="WorkingPanel" runat="server">
							<TABLE id="tblMain1" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid"
								height="98%" cellSpacing="0" cellPadding="6" rules="cols" width="98%" align="center"
								bgColor="#fef9ea" border="0">
								<TR>
									<TD height="1"></TD>
								</TR>
								<TR>
									<TD style="HEIGHT: 1px" align="center">
										<uc1:ErrorController id="moErrorController" runat="server"></uc1:ErrorController></TD>
								</TR>
								<TR>
									<TD vAlign="top">
										<TABLE style="HEIGHT: 100%" cellSpacing="0" cellPadding="0" width="100%" align="center"
											border="0">
											<TR>
												<TD style="HEIGHT: 84px" vAlign="top" colSpan="2">
													<TABLE id="tblSearch" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid; HEIGHT: 84px"
														cellSpacing="0" cellPadding="4" rules="cols" width="100%" align="center" bgColor="#f1f1f1"
														border="0">
														<TR>
															<TD vAlign="top" align="left" colSpan="1" rowSpan="1">
																<TABLE style="HEIGHT: 62px" cellSpacing="0" cellPadding="0" width="100%" border="0">
																	<TR>
																		<TD width="3%">Select:</TD>
																		<TD width="1%"></TD>
																		<TD noWrap align="left" width="30%">
																			<asp:Label id="lblDGDesc" Runat="server">DEALER_GROUP_NAME</asp:Label>:</TD>
																		<TD noWrap align="left" width="30%">
																			<asp:Label id="lblDGCode" Runat="server">DEALER_GROUP_CODE</asp:Label>:</TD>
																	</TR>
																	<TR>
																		<TD>&nbsp;<asp:RadioButton id="rdoDealerGroup" Runat="server" GroupName="rdos" Checked="True"></asp:RadioButton></TD>
																		<TD></TD>
																		<TD noWrap align="left">
																			<asp:TextBox id="txtDealerGroupName" Runat="server" Width="300px" TabIndex="1"></asp:TextBox></TD>
																		<TD noWrap align="left">
																			<asp:TextBox id="txtDealerGroupCode" Runat="server" Width="150px"></asp:TextBox></TD>
																	</TR>
																	<TR style="padding-top:3px;">
																		<TD></TD>
																		<TD></TD>
																		<TD noWrap align="left">
																			<asp:Label id="lblDDesc" Runat="server">Dealer_Name</asp:Label>:</TD>
																		<TD noWrap align="left">
																			<asp:Label id="lblDCode" Runat="server">Dealer_Code</asp:Label>:</TD>
																	</TR>
																	<TR>
																		<TD>&nbsp;<asp:RadioButton id="rdoDealer" Runat="server" GroupName="rdos"></asp:RadioButton></TD>
																		<TD></TD>
																		<TD noWrap align="left">
																			<asp:TextBox id="txtDealerName" Runat="server" Width="300px"></asp:TextBox></TD>
																		<TD noWrap align="left">
																			<asp:TextBox id="txtDealerCode" Runat="server" Width="150px"></asp:TextBox></TD>
																	</TR>
																	<TR style="padding-top:3px;">
																		<TD></TD>
																		<TD></TD>
																		<TD noWrap align="left">
																			<asp:Label id="lblSCName" Runat="server">Service_Center_Name</asp:Label>:</TD>
																		<TD noWrap align="left">
																			<asp:Label id="lblSCCode" Runat="server">Service_Center_Code</asp:Label>:</TD>
																	</TR>
																	<TR>
																		<TD>&nbsp;<asp:RadioButton id="rdoServiceCenter" Runat="server" GroupName="rdos"></asp:RadioButton></TD>
																		<TD></TD>
																		<TD noWrap align="left">
																			<asp:TextBox id="txtSCName" Runat="server" Width="300px"></asp:TextBox></TD>
																		<TD noWrap align="left">
																			<asp:TextBox id="txtSCCode" Runat="server" Width="150px"></asp:TextBox></TD>
																	</TR>
																	<TR style="padding-top:3px;">
																		<TD></TD>
																		<TD></TD>
																		<TD noWrap align="left">
																			<asp:Label id="lblBranchName" Runat="server">BRANCH_NAME</asp:Label>:</TD>
																		<TD noWrap align="left">
																			<asp:Label id="lblBranchCode" Runat="server">BRANCH_CODE</asp:Label>:</TD>
																	</TR>
																	<TR>
																		<TD>&nbsp;<asp:RadioButton id="rdoBranch" Runat="server" GroupName="rdos"></asp:RadioButton></TD>
																		<TD></TD>
																		<TD noWrap align="left">
																			<asp:TextBox id="txtBranchName" Runat="server" Width="300px"></asp:TextBox></TD>
																		<TD noWrap align="left">
																			<asp:TextBox id="txtBranchCode" Runat="server" Width="150px"></asp:TextBox></TD>
																	</TR>
																	<TR style="padding-top:3px;">
																		<TD></TD>
																		<TD></TD>
																		<TD noWrap align="left">
																			<asp:Label id="Label1" Runat="server">COMMISSION_ENTITY</asp:Label>:</TD>
																		<TD noWrap align="left"></TD>
																	</TR>
																	<TR>
																		<TD>&nbsp;<asp:RadioButton id="rdoCommEntity" Runat="server" GroupName="rdos"></asp:RadioButton></TD>
																		<TD></TD>
																		<TD noWrap align="left"><asp:TextBox id="txtCommEntity" Runat="server" Width="300px"></asp:TextBox></TD>
																		<TD noWrap align="left"></TD>
																	</TR>
																	<TR>
																		<TD align="right" colSpan="4">
																			<asp:button id="moBtnClear" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/clear_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
																				runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" height="20px" Text="Clear"></asp:button>&nbsp;&nbsp;
																			<asp:button id="moBtnSearch" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/search_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
																				runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" height="20px" Text="Search"></asp:button></TD>
																	</TR>
																</TABLE>
															</TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
											<TR>
												<TD style="HEIGHT: 1px" colSpan="2"></TD>
											</TR>
											<TR>
												<TD style="HEIGHT: 14px" colSpan="2">&nbsp;
													<HR style="HEIGHT: 1px">
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
												<TD vAlign="top" align="center" colSpan="2">
												   <asp:GridView ID="Grid" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="1" AllowPaging="True" AllowSorting="True" CssClass="DATAGRID">
                                                        <SelectedRowStyle Wrap="False" CssClass="SELECTED" />
                                                        <EditRowStyle Wrap="False" CssClass="EDITROW" />
                                                        <AlternatingRowStyle Wrap="False" CssClass="ALTROW" />
                                                        <RowStyle Wrap="False" CssClass="ROW" />
					                                    <HeaderStyle CssClass="HEADER"/>
                                                        <Columns>
                                                            <asp:TemplateField HeaderStyle-Width="18px">
                                                                <ItemStyle Width="18px" CssClass="CenteredTD" />
							                                    <ItemTemplate>
								                                    <asp:ImageButton style="cursor:hand;" id="EditButton_WRITE" ImageUrl="../Navigation/images/icons/edit2.gif" runat="server" CommandName="Select"></asp:ImageButton>
								                                    <asp:Label id="lblAcctSettingID" runat="server" text='<%# GetGuidStringFromByteArray(Container.DataItem("acct_settings_id"))%>' Visible=false></asp:Label>
							                                    </ItemTemplate>
						                                    </asp:TemplateField>
						                                    <asp:BoundField ReadOnly="true"/>
						                                    <asp:BoundField ReadOnly="true"/>
						                                    <asp:BoundField ReadOnly="true"/>						                                    						                                    					                                    
                                                        </Columns>
                                                        <PagerSettings PageButtonCount="15" Mode="Numeric" />
                                                        <PagerStyle HorizontalAlign="Center" CssClass="PAGER" />
                                                    </asp:GridView>
													</TD>
											</TR>
											<TR>
												<TD></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR valign=bottom>
									<TD align="left">
									<HR style="HEIGHT: 1px">
										<asp:button id="btnAdd_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server" Font-Bold="false" Width="100px" CssClass="FLATBUTTON" height="20px" Text="New"
											CommandName="WRITE"></asp:button>&nbsp;
									</TD>
								</TR>
							</TABLE>
						</asp:panel></td>
				</tr>
			</TABLE>
        <INPUT id="HiddenASTypePromptResponse" type="hidden" name="HiddenASTypePromptResponse"
               runat="server"/>
		</form>
	</body>
</HTML>
