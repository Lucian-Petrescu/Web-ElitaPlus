<%@ Page Language="vb" AutoEventWireup="false" Codebehind="EarningPatternForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.EarningPatternForm"%>
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

        <link type="text/css" href="../Navigation/styles/jquery-ui.min.css" rel="stylesheet">
        <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.12.4.min.js"> </script>
        <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-ui.min.js"> </script>
        <script language="javascript" type="text/javascript" src="../Navigation/scripts/Tabs.js"> </script>
        <link type="text/css" href="../Navigation/styles/jquery-ui.elita.css" rel="stylesheet">  
	</HEAD>
	<body onresize="resizeForm(document.getElementById('scroller'));" leftMargin="0" topMargin="0" onload="changeScrollbarColor();" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--Start Header-->
			<TABLE style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid" cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE width="100%" border="0">
							<TR>
								<TD height="20"><asp:label id="moTitleLabel1" runat="server" Cssclass="TITLELABEL">Tables</asp:label>:
									<asp:label id="moTitleLabel2" runat="server" Cssclass="TITLELABELTEXT">Earning_Pattern_Detail</asp:label></TD>
								<TD align="right" height="20"><STRONG>*</STRONG>
									<asp:label id="Label9" runat="server" Font-Bold="false">INDICATES_REQUIRED_FIELDS</asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="moTableOuter" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid" height="93%" cellSpacing="0" cellPadding="0" rules="none" width="98%" bgColor="#d5d6e4" border="0">
				<TR>
					<TD style="HEIGHT: 8px" vAlign="top" align="center"></TD>
				</TR>
				<tr>
					<td vAlign="top" align="center" height="100%">
						<TABLE id="moTableMain" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid" height="98%" cellSpacing="0" cellPadding="6" rules="cols" width="98%" align="center" bgColor="#fef9ea" border="0">
							<TR>
								<TD vAlign="top" align="center" height="1"><uc1:errorcontroller id="moErrorController" runat="server"></uc1:errorcontroller></TD>
							</TR>
							<TR>
								<TD style="vertical-align:top;"><asp:panel id="EditPanel" runat="server">
										<TABLE style="WIDTH: 100%" height="60%" cellSpacing="2" cellPadding="0" width="100%" border="0">
											<asp:panel id="moEarningPatternEditPanel_WRITE" runat="server" Width="100%" Height="98%">
												<TBODY>
													<TR>
														<TD colSpan="4" height="1">
															<asp:label id="moEarningPatternIdLabel" runat="server" Visible="False"></asp:label>
															<asp:Label id="moIsNewPatternLabel" runat="server" Visible="False"></asp:Label>
															<asp:Label id="moIsNewPercentLabel" runat="server" Visible="False"></asp:Label><INPUT id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse" runat="server">
															<asp:label id="moEarningPercentIDLabel" runat="server" Visible="False"></asp:label></TD>
													</TR>
													<TR>
														<TD align="right">
															<asp:label id="moCodeLabel1" runat="server" Font-Bold="false" Visible="False">Code</asp:label>&nbsp;
														</TD>
														<TD noWrap="">
															<asp:textbox id="moCodeText" tabIndex="10" runat="server" Visible="False" CssClass="FLATTEXTBOX" width="210px"></asp:textbox></TD>
														<TD align="right" colSpan="1">
															<asp:label id="moDescriptionLabel1" runat="server" Font-Bold="false" Visible="False">Description</asp:label>&nbsp;</TD>
														<TD colSpan="1">
															<asp:textbox id="moDescriptionText1" tabIndex="10" runat="server" Visible="False" CssClass="FLATTEXTBOX" width="210px"></asp:textbox></TD>
													</TR>
													<TR>
														<TD align="right">
															<asp:label id="moCodeLabel" runat="server" Font-Bold="false">Code</asp:label>&nbsp;
														</TD>
														<TD noWrap="">
															<asp:dropdownlist id="moCodeDrop" tabIndex="40" runat="server" Width="210px" AutoPostBack="True"></asp:dropdownlist></TD>
														<TD align="right" colSpan="1">
															<asp:label id="moDescriptionLabel" runat="server" Font-Bold="false">Description</asp:label>&nbsp;</TD>
														<TD colSpan="1">
															<asp:textbox id="moDescriptionText" tabIndex="10" runat="server" CssClass="FLATTEXTBOX" width="210px" Enabled="False" ReadOnly="True"></asp:textbox></TD>
													</TR>
													<TR>
														<TD align="right" colSpan="1">
															<asp:label id="moEffectiveLabel" runat="server" Font-Bold="false">Effective</asp:label>&nbsp;</TD>
														<TD noWrap="" colSpan="1">
															<asp:textbox id="moEffectiveText" tabIndex="10" runat="server" CssClass="FLATTEXTBOX" width="210px"></asp:textbox>
															<asp:imagebutton id="BtnEffectiveDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="AbsMiddle"></asp:imagebutton></TD>
														<TD align="right" colSpan="1">
															<asp:label id="moExpirationLabel" runat="server" Font-Bold="false">Expiration</asp:label>&nbsp;</TD>
														<TD noWrap="" colSpan="1">
															<asp:textbox id="moExpirationText" tabIndex="10" runat="server" CssClass="FLATTEXTBOX" width="210px"></asp:textbox>
															<asp:imagebutton id="BtnExpirationDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="AbsMiddle"></asp:imagebutton></TD>
													</TR>
                                                    <TR>
														<TD align="right">
															<asp:label id="EPStartsOnLabel" runat="server" Font-Bold="false">EARNING_PATTERN_STARTS_ON</asp:label>&nbsp;
														</TD>
														<TD>
															<asp:dropdownlist id="moEPStartsOnDrop" tabIndex="40" runat="server" Width="210px" AutoPostBack="false"></asp:dropdownlist></TD>
													<TD align="right" colSpan="1">
															<asp:label id="Label1" runat="server" Font-Bold="false" Visible="False"></asp:label>&nbsp;</TD>
														<TD colSpan="1">
															<asp:label id="Label2" runat="server" Font-Bold="false" Visible="False"></asp:label>&nbsp;</TD>
													</TR>
													<TR>
														<TD colSpan="5">
															<HR>
														</TD>
													</TR>
											</TBODY></asp:panel>
                                        </TABLE>
                                        <TABLE style="WIDTH: 100%" height="60%" cellSpacing="2" cellPadding="0" width="100%" border="0">
											<TR>
												<TD align="left" width="100%">
													<div id="tabs" class="style-tabs-old style-tabs-oldBG">
                                                    <ul>
                                                        <li style="background:#d5d6e4"><a href="#tabsPercentages" rel="noopener noreferrer">
                                                            <asp:Label ID="Label4" runat="server" CssClass="tabHeaderTextOld">Percentages</asp:Label></a></li>
                                                    </ul>

                                                    <div id="tabsPercentages" style="background:#d5d6e4">
                                                        <table id="tblOpportunities" style="border-right: #999999 1px solid; border-top: #999999 1px solid; border-left: #999999 1px solid; border-bottom: #999999 1px solid" cellspacing="2" cellpadding="2" rules="cols" width="100%" background="" border="0">
                                                            <tr>
                                                                <td colspan="2">
                                                                    <uc1:ErrorController ID="moErrorControllerPercent" runat="server"></uc1:ErrorController>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td align="middle" colspan="2">
                                                                    <div id="scroller" style="width: 98%" align="center">
                                                                        <asp:GridView ID="moDataGrid" runat="server" Width="100%" OnRowCreated="ItemCreated" OnRowCommand="ItemCommand" AllowPaging="True" AllowSorting="True" CellPadding="1" AutoGenerateColumns="False" CssClass="DATAGRID">
                                                                            <SelectedRowStyle CssClass="SELECTED"></SelectedRowStyle>
                                                                            <EditRowStyle CssClass="EDITROW"></EditRowStyle>
                                                                            <AlternatingRowStyle Wrap="False" CssClass="ALTROW"></AlternatingRowStyle>
                                                                            <RowStyle CssClass="ROW"></RowStyle>
                                                                            <HeaderStyle CssClass="HEADER"></HeaderStyle>
                                                                            <Columns>
                                                                                <asp:TemplateField ShowHeader="false">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="3%" Height="15px"></ItemStyle>
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="EditButton_WRITE" runat="server" CausesValidation="False" CommandName="EditRecord" ImageUrl="~/Navigation/images/icons/edit2.gif" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField ShowHeader="false">
                                                                                    <ItemStyle HorizontalAlign="Center" Width="3%" Height="15px"></ItemStyle>
                                                                                    <ItemTemplate>
                                                                                        <asp:ImageButton ID="DeleteButton_WRITE" runat="server" CausesValidation="False" CommandName="DeleteRecord" ImageUrl="~/Navigation/images/icons/trash.gif" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField Visible="False">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="moEARNING_PERCENT_ID" runat="server">
                                                                                        </asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField SortExpression="EARNING_TERM" Visible="True" HeaderText="Earning_Term">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="moEarningTermLabel" runat="server">
                                                                                        </asp:Label>
                                                                                    </ItemTemplate>
                                                                                </asp:TemplateField>
                                                                                <asp:TemplateField SortExpression="Earning_Percent" Visible="True" HeaderText="Earning_Percent">
                                                                                    <ItemTemplate>
                                                                                        <asp:Label ID="moEarningPercentLabel" runat="server">
                                                                                        </asp:Label>
                                                                                    </ItemTemplate>
                                                                                    <EditItemTemplate>
                                                                                        <asp:TextBox ID="moEarningPercentText" runat="server" Visible="True" Width="75"></asp:TextBox>
                                                                                    </EditItemTemplate>
                                                                                </asp:TemplateField>
                                                                            </Columns>
                                                                            <PagerSettings PageButtonCount="15" Mode="Numeric"></PagerSettings>
                                                                            <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
                                                                        </asp:GridView>
                                                                    </div>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td colspan="2" style="padding-top:20px;">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																		<asp:Button ID="BtnNewPercent_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif); cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false" Width="100px" CssClass="FLATBUTTON" Height="20px" Text="New"></asp:Button>&nbsp;
																		<asp:Button ID="BtnSavePercent_WRITE" Style="background-image: url(../Navigation/images/icons/save_icon.gif); cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Save"></asp:Button>&nbsp;
																		<asp:Button ID="BtnCancelPercent" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif); cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Cancel"></asp:Button>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>

												</TD>
											</TR>
										</TABLE>
									</asp:panel>
								</TD>
							</TR>
							<TR>
								<TD align="left">
									<HR style="WIDTH: 100%; HEIGHT: 1px" SIZE="1">
								</TD>
							</TR>
							<TR>
								<TD align="right" colSpan="2">
									<TABLE id="Table2" style="WIDTH: 678px; HEIGHT: 24px" cellSpacing="1" cellPadding="1" width="678" align="left" border="0">
										<TR>
											<TD><asp:button id="btnBack" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/back_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat" runat="server" Width="90px" CssClass="FLATBUTTON" height="20px" Text="BACK"></asp:button></TD>
											<TD><asp:button id="btnApply_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/save_trans_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat" runat="server" Width="90px" CssClass="FLATBUTTON" height="20px" Text="SAVE"></asp:button></TD>
											<TD><asp:button id="btnUndo_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/cancel_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat" runat="server" Width="110px" CssClass="FLATBUTTON" height="20px" Text="UNDO"></asp:button></TD>
											<TD><asp:button id="btnNew_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat" runat="server" Width="100px" CssClass="FLATBUTTON" height="20px" Text="New"></asp:button></TD>
											<TD><asp:button id="btnCopy_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat" runat="server" Width="140px" CssClass="FLATBUTTON" height="20px" Text="New_With_Copy"></asp:button></TD>
											<TD><asp:button id="btnDelete_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/clear_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat" runat="server" Width="100px" CssClass="FLATBUTTON" height="20px" Text="Delete"></asp:button></TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
						</TABLE>
					</td>
				</tr>
				</TABLE>
			<script>
		
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
					newHeight = browseHeight - 350;
				}
				else
				{
					newHeight = browseHeight - 570;
				}
				
				if (newHeight < 75)
				{
					newHeight = 75;
				}
					
				item.style.height = String(newHeight) + "px";
				
				item.style.width = String(browseWidth - 80) + "px";
		}
		
		resizeForm(document.getElementById("scroller"));
		//document.getElementById("scroller").scrollBy(0,-50); // horizontal and vertical scroll increments

			</script>
		</form>
	</body>
</HTML>
Width, browseHeight;
			
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
					newHeight = browseHeight - 350;
				}
				else
				{
					newHeight = browseHeight - 570;
				}
				
				if (newHeight < 75)
				{
					newHeight = 75;
				}
					
				item.style.height = String(newHeight) + "px";
				
				item.style.width = String(browseWidth - 80) + "px";
		}
		
		resizeForm(document.getElementById("scroller"));
		//document.getElementById("scroller").scrollBy(0,-50); // horizontal and vertical scroll increments

			</script>
		</form>
	</body>
</HTML>
