<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DirectBillingDetailForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.DirectBillingDetailForm" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>

<!DOCTYPE HTML PUBLIC "-//W3C//Dtd HTML 4.0 transitional//EN">
<HTML>
	<HEAD>
		<title>Direct Billing Detail Form</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>
		<LINK href="../Styles.css" type="text/css" rel="STYLESHEET">
	    <style type="text/css">
            .style1
            {
                height: 42px;
            }
        </style>
	</HEAD>
	<body leftMargin="0" topMargin="0" onload="changeScrollbarColor();" MS_POSITIONING="GridLayout"
		border="0">
		<form id="Form1" method="post" runat="server">
			<!--Start Header-->
			<table style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<tr>
					<td valign="top">
						<table width="100%" border="0">
							<tr>
								<td height="20">&nbsp;<asp:label id="moTitleLabel1" runat="server"  CssClass="TITLELABEL">Interfaces</asp:label>:
									<asp:label id="moTitleLabel2" runat="server"  CssClass="TITLELABELTEXT">Direct_Billing_Detail</asp:label></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<!--d5d6e4-->
		<table id="tblOuter2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid; HEIGHT: 93%"
		cellSpacing="0" cellPadding="0" rules="none" width="98%" bgColor="#d5d6e4" border="0"> <!--d5d6e4-->
				<tr>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td valign="top" align="center" height="100%"><asp:panel id="moPanel" runat="server">
					<table id="tblMain1" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid"
					height="98%" cellSpacing="0" cellPadding="6" rules="cols" width="98%" align="center"
					bgColor="#fef9ea" border="0">
								<tr>
									<td height="1" colspan="2" valign="top">
										<uc1:ErrorController id="ErrController" runat="server"></uc1:ErrorController></td>
								</tr>
					            <tr>
                                    <td  align="center" width="98%" colspan="2" valign="top" class="style1">
                                        <asp:Label ID="moDealerNameLabel" runat="server" Visible="True">DEALER_NAME:</asp:Label>
                                        <asp:TextBox ID="moDealerNameText" runat="server" Width="200px" Visible="True" ReadOnly="True"
                                            Enabled="False"></asp:TextBox>&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:Label ID="moFileNameLabel" runat="server" Visible="True">FILE_NAME</asp:Label>:
                                        <asp:TextBox ID="moFileNameText" runat="server" Width="200px" Visible="True" ReadOnly="True"
                                            Enabled="False"></asp:TextBox>
                                        <hr style="width: 100%; height: 1px" size="1" />
                                    </td>
                                </tr>
	
								<tr id="trPageSize" runat="server">
									<td valign="top" align="left">
										<asp:label id="lblPageSize" runat="server">Page_Size</asp:label>: &nbsp;
										<asp:dropdownlist id="cboPageSize" runat="server" Width="50px" AutoPostBack="true">
											<asp:ListItem Value="5">5</asp:ListItem>
											<asp:ListItem Selected="true" Value="10">10</asp:ListItem>
											<asp:ListItem Value="15">15</asp:ListItem>
											<asp:ListItem Value="20">20</asp:ListItem>
											<asp:ListItem Value="25">25</asp:ListItem>
											<asp:ListItem Value="30">30</asp:ListItem>
											<asp:ListItem Value="35">35</asp:ListItem>
											<asp:ListItem Value="40">40</asp:ListItem>
											<asp:ListItem Value="45">45</asp:ListItem>
											<asp:ListItem Value="50">50</asp:ListItem>
										</asp:dropdownlist></td>
									<td style="HEIGHT: 11px" align="right">
										<asp:label id="lblRecordCount" Runat="server"></asp:label></td>
								</tr>
								<tr id="moESCBillingInformation" runat="server">
									<td colspan="2" valign="top">
											<asp:datagrid id="moBillingGrid" runat="server" Width="100%" AllowSorting="true" AllowPaging="true"
											BorderWidth="1px" CellPadding="1" BorderStyle="Solid" BorderColor="#999999" BackColor="#DEE3E7"
											AutoGenerateColumns="False" >
											<SelectedItemStyle Wrap="False" BackColor="LightSteelBlue"></SelectedItemStyle>
											<EditItemStyle Wrap="False"></EditItemStyle>
											<AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
											<ItemStyle Wrap="False" BackColor="White"></ItemStyle>
											<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn Visible="False">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="moBillingDetailId" text='<%# GetGuidStringFromByteArray(Container.DataItem("billing_detail_id")) %>' runat="server">
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
												<asp:BoundColumn DataField="Cert_Number" ItemStyle-HorizontalAlign="Center" SortExpression="Cert_Number" ReadOnly="true" HeaderText="Certificate">
													<HeaderStyle Width="10%"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Installment_Number" ItemStyle-HorizontalAlign="Center" SortExpression="Installment_Number" HeaderText="Installment_Number">
													<HeaderStyle Width="10%"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Bank_Owner_Name" SortExpression="Bank_Owner_Name" ItemStyle-HorizontalAlign="Center"  HeaderText="BANK_ACCT_OWNER_NAME">
													<HeaderStyle Width="15%"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Bank_acct_number" SortExpression="Bank_acct_number" ItemStyle-HorizontalAlign="Center" HeaderText="BANK_ACCOUNT_NUMBER">
													<HeaderStyle Width="10%"></HeaderStyle>
												</asp:BoundColumn>
														<asp:BoundColumn DataField="Bank_rtn_number" SortExpression="Bank_rtn_number" ItemStyle-HorizontalAlign="Center" HeaderText="Bank_rtn_number">
													<HeaderStyle Width="10%"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Billed_Amount" SortExpression="Billed_Amount" DataFormatString="{0:#,0.00}" ItemStyle-HorizontalAlign="Center" HeaderText="Billed_Amount">
													<HeaderStyle Width="10%"></HeaderStyle>
												</asp:BoundColumn>
														<asp:BoundColumn DataField="Reason" SortExpression="Reason" ItemStyle-HorizontalAlign="Center" HeaderText="Payment_Reason">
													<HeaderStyle Width="20%"></HeaderStyle>
												</asp:BoundColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7" PageButtonCount="15"
															Mode="NumericPages"></PagerStyle>
										</asp:datagrid></td>
										</tr>
                                <tr id="moVSCBillingInformation" runat="server">
									<td colspan="2" valign="top">
											<%--Start DEF-2771--%>
											<asp:datagrid id="moVSCBillingGrid" runat="server" Width="100%" AllowSorting="true" AllowPaging="true"
											BorderWidth="1px" CellPadding="1" BorderStyle="Solid" BorderColor="#999999" BackColor="#DEE3E7"
											AutoGenerateColumns="False" OnItemCreated="ItemCreated"> 
                                           <%-- End DEF-2771--%>
											<SelectedItemStyle Wrap="False" BackColor="LightSteelBlue"></SelectedItemStyle>
											<EditItemStyle Wrap="False"></EditItemStyle>
											<AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
											<ItemStyle Wrap="False" BackColor="White"></ItemStyle>
											<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
											<Columns>
												<asp:TemplateColumn Visible="False">
													<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
													<ItemStyle HorizontalAlign="Center"></ItemStyle>
													<ItemTemplate>
														<asp:Label id="moBillingDetailId" text='<%# GetGuidStringFromByteArray(Container.DataItem("billing_detail_id")) %>' runat="server">
														</asp:Label>
													</ItemTemplate>
												</asp:TemplateColumn>
                                                <asp:BoundColumn DataField="account_type" ItemStyle-HorizontalAlign="Center" SortExpression="Account_Type" ReadOnly="true" HeaderText="Account_Type">
													<HeaderStyle Width="10%"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Cert_Number" ItemStyle-HorizontalAlign="Center" SortExpression="Cert_Number" ReadOnly="true" HeaderText="Certificate">
													<HeaderStyle Width="10%"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Installment_Number" ItemStyle-HorizontalAlign="Center" SortExpression="Installment_Number" HeaderText="Installment_Number">
													<HeaderStyle Width="10%"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Bank_Owner_Name" SortExpression="Bank_Owner_Name" ItemStyle-HorizontalAlign="Center"  HeaderText="BANK_ACCT_OWNER_NAME">
													<HeaderStyle Width="15%"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Bank_acct_number" SortExpression="Bank_acct_number" ItemStyle-HorizontalAlign="Center" HeaderText="BANK_ACCOUNT_NUMBER">
													<HeaderStyle Width="10%"></HeaderStyle>
												</asp:BoundColumn>
														<asp:BoundColumn DataField="Bank_rtn_number" SortExpression="Bank_rtn_number" ItemStyle-HorizontalAlign="Center" HeaderText="Bank_rtn_number">
													<HeaderStyle Width="10%"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Billed_Amount" SortExpression="Billed_Amount" DataFormatString="{0:#,0.00}" ItemStyle-HorizontalAlign="Center" HeaderText="Billed_Amount">
													<HeaderStyle Width="10%"></HeaderStyle>
												</asp:BoundColumn>
												<asp:BoundColumn DataField="Status" SortExpression="Status" ItemStyle-HorizontalAlign="Center" HeaderText="Status">
													<HeaderStyle Width="20%"></HeaderStyle>
												</asp:BoundColumn>
                                                <asp:BoundColumn DataField="Status_Description" SortExpression="Status_Description" ItemStyle-HorizontalAlign="Center" HeaderText="Status_Description">
													<HeaderStyle Width="20%"></HeaderStyle>
												</asp:BoundColumn>
											</Columns>
											<PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7" PageButtonCount="15"
															Mode="NumericPages"></PagerStyle>
										</asp:datagrid></td>
										</tr>
			                            <tr>
		                                    <td align="left" valign="bottom" colspan="2" >
                                            <hr style="width: 100%; height: 1px" size="1" />
						                        <asp:button id="btnBack" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
							                        runat="server" Font-Bold="false" Width="90px" Text="Back" CssClass="FLATBUTTON" height="23px"></asp:button>&nbsp;&nbsp;
                                                    <asp:button id="btnExportResults" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
													runat="server" Width="140px" CausesValidation="False" height="23px" CssClass="FLATBUTTON"
													Text="ExportResults" tabIndex="15"></asp:button></TD>
				                        </tr>											
										</table></asp:panel>
									</td>
								</tr>
						</table>
		             </form>
		<!--END-->
	</body>
</HTML>
