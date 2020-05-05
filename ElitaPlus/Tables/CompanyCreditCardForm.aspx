<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CompanyCreditCardForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.CompanyCreditCardForm"%>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="mytab" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
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
	<body leftMargin="0" topMargin="0" onload="changeScrollbarColor();" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--Start Header-->
			<TABLE style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE width="100%" border="0">
							<TR>
								<TD height="20">&nbsp;<asp:label id="LabelTables" runat="server" Cssclass="TITLELABEL">Company</asp:label>:
									<asp:label id="moTitle2Label" runat="server"  Cssclass="TITLELABELTEXT" >Credit_Card</asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<!--d5d6e4-->
			<TABLE id="moTableOuter" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				height="93%" cellSpacing="0" cellPadding="4" rules="none" width="98%" bgColor="#d5d6e4"
				border="0">
				<tr><td height="1%"></td></tr>
				<tr>
				
					<td vAlign=middle align="center" height="99%">
						<TABLE id="moTableMain" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid"
								height="97%" cellSpacing="0" cellPadding="6" rules="cols" width="98%" align="center"
								bgColor="#fef9ea" border="0">
							<TR>
								<TD colspan="2" vAlign="top" height="1%"><uc1:errorcontroller id="moErrorController" runat="server"></uc1:errorcontroller></TD>
							</TR>
							<TR id="trPageSize" runat="server">
								<TD height="1%" vAlign="top" align="left">
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
								<TD align="right" nowrap>
									<asp:label id="lblRecordCount" Runat="server"></asp:label></TD>
							</TR>
							<tr>
								<TD colspan="2" vAlign="top">
								         <asp:GridView ID="Grid" runat="server" Width="100%" OnRowCreated="RowCreated" OnRowCommand="RowCommand" 
                                            AllowPaging="True" AllowSorting="True" CellPadding="1" AutoGenerateColumns="False"
                                            CssClass="DATAGRID">
                                            <SelectedRowStyle CssClass="SELECTED"></SelectedRowStyle>
                                            <EditRowStyle CssClass="EDITROW"></EditRowStyle>
                                            <AlternatingRowStyle Wrap="False" CssClass="ALTROW"></AlternatingRowStyle>
                                            <RowStyle CssClass="ROW"></RowStyle>
                                            <HeaderStyle CssClass="HEADER"></HeaderStyle>
										<Columns>
											<asp:TemplateField HeaderText=" ">
												<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
												<ItemTemplate>
													<asp:ImageButton style="cursor:hand;" id="EditButton_WRITE" ImageUrl="../Navigation/images/icons/edit2.gif"
														runat="server" CommandName="EditRecord" CommandArgument="<%#Container.DisplayIndex %>" ></asp:ImageButton>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField>
												<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
												<ItemTemplate>
													<asp:ImageButton id="DeleteButton_WRITE" style="CURSOR:hand" runat="server" CommandName="DeleteRecord"
														ImageUrl="../Navigation/images/icons/trash.gif" CommandArgument="<%#Container.DisplayIndex %>" ></asp:ImageButton>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField Visible="False" HeaderText="COMPANY_CREDIT_CARD_ID">
												<ItemTemplate>
													<asp:Label id="IdLabel" text='<%# GetGuidStringFromByteArray(Container.DataItem("COMPANY_CREDIT_CARD_ID"))%>' runat="server">
													</asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField Visible="False" HeaderText="COMPANY_ID">
												<ItemTemplate>
													<asp:Label id="CompanyLabel" text='<%# GetGuidStringFromByteArray(Container.DataItem("COMPANY_ID"))%>' runat="server">
													</asp:Label>
												</ItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField Visible="False" HeaderText="credit_card_format_id">
												<ItemTemplate>
													<asp:Label id="CreditCardFormatLabel" text='<%# GetGuidStringFromByteArray(Container.DataItem("credit_card_format_id"))%>' runat="server">
													</asp:Label>
												</ItemTemplate>
											</asp:TemplateField>											

											<asp:TemplateField SortExpression="COMPANY" HeaderText="COMPANY" >
                                                <ItemStyle HorizontalAlign="Center" ></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="CompanyCodeLabel" runat="server" Text='<%# Container.DataItem("company_code")%>'
                                                        Visible="True">
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="cboCompanyInGrid" runat="server"></asp:DropDownList>
                                                </EditItemTemplate>											
											</asp:TemplateField>
										<asp:TemplateField SortExpression="Credit_Card" HeaderText="Credit_Card" >
                                                <ItemStyle HorizontalAlign="Center" ></ItemStyle>
                                                <ItemTemplate>
                                                    <asp:Label ID="Credit_CardLabel" runat="server" Text='<%# Container.DataItem("Credit_Card_Type")%>'
                                                        Visible="True">
                                                    </asp:Label>
                                                </ItemTemplate>
                                                <EditItemTemplate>
                                                    <asp:DropDownList ID="cboCreditCardInGrid" runat="server"></asp:DropDownList>
                                                </EditItemTemplate>											
											</asp:TemplateField>											
											<asp:TemplateField SortExpression="Billing_Date" HeaderText="Billing_Date">
								                   <ItemStyle HorizontalAlign="Left" Width="15%"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:Label ID="BillingDateLabel" runat="server" Visible="True" Text='<%# Container.DataItem("Billing_Date")%>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                        <EditItemTemplate>
                                                            <asp:TextBox ID="BillingDateTextBox" runat="server" Visible="True"></asp:TextBox>
                                                        </EditItemTemplate>
											</asp:TemplateField>
											<asp:TemplateField HeaderText="Billing_Schedule">
												<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B"></HeaderStyle>
												<ItemStyle HorizontalAlign="Center" Width="15%"></ItemStyle>
												<ItemTemplate>
													<asp:ImageButton id="btnScheduleInGrid" style="CURSOR:hand" runat="server" CommandName="SelectRecord"
														ImageUrl="../Common/Images/calendarIcon2.jpg" CommandArgument="<%#Container.DisplayIndex %>" ></asp:ImageButton>
												</ItemTemplate>
											</asp:TemplateField>																						
										</Columns>
                                        <PagerSettings PageButtonCount="15" Mode="Numeric" />
                                        <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
									</asp:GridView></TD>
							</tr>
                            <input id="HiddenDeletePromptResponse" type="hidden" runat="server" designtimedragdrop="261"/>
							<tr valign="bottom">
								<td colspan="2" align="left" width="1%">
								<hr style="HEIGHT: 1px"/>
									<asp:button id="BtnNew_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
										runat="server" Font-Bold="false" Width="100px" Text="New" height="20px" CssClass="FLATBUTTON"></asp:button><asp:button id="BtnSave_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/save_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
										runat="server" Font-Bold="false" Width="90px" Text="Save" height="20px" CssClass="FLATBUTTON"></asp:button>&nbsp;&nbsp;
									<asp:button id="BtnCancel" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/cancel_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
										runat="server" Font-Bold="false" Width="89px" Text="Cancel" height="20px" CssClass="FLATBUTTON"></asp:button></td>
							</tr>
							<TR>
								<TD></TD>
							</TR>
						</TABLE>
					</td>
				</tr>
			</TABLE>
			</form>
	</body>
</HTML>
