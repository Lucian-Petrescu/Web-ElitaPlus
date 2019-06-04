<%@ Page Language="vb" AutoEventWireup="false" Codebehind="TestRegionTax.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.TestRegionTax" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>WebForm1</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
	</HEAD>
	<body class="NormalText" bottomMargin="0" bgColor="#fef9ea" leftMargin="0" topMargin="0"
		rightMargin="0" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<br>
			</TR>
			<TABLE style="Z-INDEX: 102; LEFT: 3px; WIDTH: 643px; POSITION: absolute; TOP: 4px; HEIGHT: 455px">
				<TR>
					<TD style="HEIGHT: 390px" align="center">
						<TABLE id="Table3" style="WIDTH: 620px; HEIGHT: 459px" cellSpacing="1" cellPadding="1"
							width="620" border="1">
							<TR>
								<TD style="HEIGHT: 5px">
									<TABLE id="Table1" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; WIDTH: 602px; BORDER-BOTTOM: black 1px solid; HEIGHT: 39px"
										cellSpacing="0" cellPadding="0" width="602" bgColor="#d5d6e4" border="0">
										<TR>
											<TD vAlign="top">
												<TABLE id="Table2" width="100%" border="0">
													<TR>
														<TD height="20">&nbsp;
															<asp:label id="Label7" runat="server" Font-Bold="false">
																<font color="black">Table: <b><font color="12135b">COUNTRY TAX LIST</font></b></font></asp:label></TD>
													</TR>
												</TABLE>
											</TD>
										</TR>
									</TABLE>
								</TD>
							</TR>
							<TR>
								<TD style="HEIGHT: 354px"><asp:datagrid id="dgrResults" runat="server" Height="210px" Width="611px" AllowSorting="True"
										BorderColor="#999999" BackColor="#DEE3E7" borderwidth="1px" PagerStyle-Mode="NumericPages" AutoGenerateColumns="False"
										AllowPaging="True" CellPadding="1" BorderStyle="Solid">
										<SelectedItemStyle Wrap="False"></SelectedItemStyle>
										<EditItemStyle Wrap="False"></EditItemStyle>
										<AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
										<ItemStyle Wrap="False" BackColor="White"></ItemStyle>
										<HeaderStyle  HorizontalAlign="Center" ForeColor="#12135B"></HeaderStyle>
										<Columns>
											<asp:TemplateColumn HeaderText="Show">
												<ItemStyle HorizontalAlign="Center"></ItemStyle>
												<ItemTemplate>
													<asp:ImageButton id="btnEdit" style="CURSOR: hand" runat="server" CommandName="SelectAction" ImageUrl="../Navigation/images/icons/edit2.gif"></asp:ImageButton>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn Visible="False">
												<ItemTemplate>
												</ItemTemplate>
											</asp:TemplateColumn>
											<asp:TemplateColumn HeaderText="Country Tax Information">
												<ItemTemplate>
													<table>
														<tr>
															<td><%# DataBinder.Eval(Container.DataItem, "TTAX") %></td>
															<td><%# DataBinder.Eval(Container.DataItem, "EFFECTIVE_DATE" ) %></td>
															<td><%# DataBinder.Eval(Container.DataItem, "EXPIRATION_DATE" ) %></td>
														</tr>
													</table>
												</ItemTemplate>
											</asp:TemplateColumn>
										</Columns>
										<PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7" PageButtonCount="15"
											Mode="NumericPages"></PagerStyle>
									</asp:datagrid></TD>
							</TR>
							<TR>
								<TD></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
		</form>
	</body>
</HTML>
