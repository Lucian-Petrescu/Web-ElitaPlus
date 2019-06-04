<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ClaimPaymentAdjustmentsForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ClaimPaymentAdjustmentsForm" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="mytab" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>Claim Payment Adjustments</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="STYLESHEET">
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>
		<script>
/* 
 * This function is used to show/hide payment detail based on the button clicked.
 * 
 * Function arguments:
 * -------------------
 * 1- ctlButton         -->  Button control being clicked
 *
 * Function Use Examples:
 * ----------------------
 * 1- From Adjust Payment button control:
 *     onclick="return ShowPaymentDetail('btnADJUST_PAYMENT');"
 *
 * 2- From Reverse Payment button control:
 *     onchange="return ShowPaymentDetail('btnREVERSE_PAYMENT');"
 *
*/	
	function ShowPaymentDetail(ctlButton)
	{
		var obj = document.getElementById('tblPaymentDetails');	
		obj.style.visibility = 'visible';

		var objtxtADJUSTMENT_AMOUNT = document.getElementById('txtADJUSTMENT_AMOUNT');
		var objlblADJUSTMENT_AMOUNT = document.getElementById('lblADJUSTMENT_AMOUNT');
		var objbtnREVERSE_PAYMENT = document.getElementById('btnREVERSE_PAYMENT');
		var objbtnADJUST_PAYMENT = document.getElementById('btnADJUST_PAYMENT');
		var objbtnNEW_PAYMENT = document.getElementById('btnNEW_PAYMENT');
		var objHiddenButtonClickedValue = document.getElementById('txtButtonClickedHiddenValue');
		
		objbtnREVERSE_PAYMENT.disabled="disabled";
		objbtnADJUST_PAYMENT.disabled="disabled";
		objbtnNEW_PAYMENT.disabled="disabled";
		
		if(ctlButton == 'btnREVERSE_PAYMENT')
		{
			objtxtADJUSTMENT_AMOUNT.style.visibility = 'hidden';
			objlblADJUSTMENT_AMOUNT.style.visibility = 'hidden';
			objHiddenButtonClickedValue.value = "R";
		}
		else
		{
			objtxtADJUSTMENT_AMOUNT.style.visibility = 'visible';
			objlblADJUSTMENT_AMOUNT.style.visibility = 'visible';
			objHiddenButtonClickedValue.value = "A";
		}
		
		return false;
	}				
		
/* 
 * This function is used to hide payment detail when Cancel button clicked.
 * 
 * Function arguments:
 * -------------------
 * 
 * Function Use Examples:
 * ----------------------
 * 1- From Cancel button control:
 *     onclick="return ReastPaymentDetail();"
 *
*/	
	function ReastPaymentDetail()
	{
		var obj = document.getElementById('tblPaymentDetails');	
		obj.style.visibility = 'hidden';

		var objbtnREVERSE_PAYMENT = document.getElementById('btnREVERSE_PAYMENT');
		var objbtnADJUST_PAYMENT = document.getElementById('btnADJUST_PAYMENT');
		var objbtnNEW_PAYMENT = document.getElementById('btnNEW_PAYMENT');
		
		objbtnNEW_PAYMENT.removeAttribute("disabled");
		objbtnADJUST_PAYMENT.removeAttribute("disabled");
		objbtnREVERSE_PAYMENT.removeAttribute("disabled");
			
		var objtxtADJUSTMENT_AMOUNT = document.getElementById('txtADJUSTMENT_AMOUNT');
		var objlblADJUSTMENT_AMOUNT = document.getElementById('lblADJUSTMENT_AMOUNT');
		objtxtADJUSTMENT_AMOUNT.style.visibility = 'hidden';
		objlblADJUSTMENT_AMOUNT.style.visibility = 'hidden';
		return false;
	}				
		
		</script>
	    <style type="text/css">
            .style1
            {
                height: 12px;
                width: 179px;
            }
            .style2
            {
                width: 179px;
            }
        </style>
	</HEAD>
	<body onresize="" leftMargin="0" topMargin="0" onload="changeScrollbarColor();" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--Start Header-->
			<TABLE style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; WIDTH: 98%; BORDER-BOTTOM: black 1px solid; HEIGHT: 20px"
				cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE width="100%" border="0">
							<TR>
								<TD height="20"><asp:label id="LabelTables" runat="server"  CssClass="TITLELABEL">Claims</asp:label>:&nbsp;
								<asp:label id="Label7" runat="server" CssClass="TITLELABELTEXT">CLAIM_PAYMENT_ADJUSTMENTS</asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="tblOuter2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				height="93%" cellSpacing="0" cellPadding="0" rules="none" width="98%" bgColor="#d5d6e4"
				border="0"> <!--d5d6e4-->
				<tr>
					<td style="HEIGHT: 8px"></td>
				</tr>
				<tr>
					<td vAlign="top" align="center" width="98%" height="100%"><asp:panel id="WorkingPanel" runat="server" Height="100%" Width="100%">
							<TABLE id="tblMain1" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid"
								height="98%" cellSpacing="0" cellPadding="6" rules="cols" width="98%" align="center"
								bgColor="#fef9ea" border="0">
								<TR>
									<TD vAlign="top" height="1">
										<uc1:ErrorController id="ErrorCtrl" runat="server"></uc1:ErrorController></TD>
								</TR>
								<TR>
									<TD vAlign="top" align="center" width="100%">
										<TABLE cellSpacing="2" cellPadding="2" width="100%" align="center" border="0">
											<TR>
												<TD vAlign="top" align="left" colSpan="2">
													<TABLE id="Table4" cellSpacing="1" cellPadding="1" width="100%" border="0">
														<TR>
															<TD style="WIDTH: 164px">
																<asp:label id="lblClaimNumber" runat="server">CLAIM_NUMBER</asp:label>:</TD>
															<TD style="WIDTH: 372px">
																<asp:label id="LabelSearchCustomerName" runat="server">CUSTOMER_NAME</asp:label>:</TD>
															<TD>
																<asp:label id="LabelSearchServiceCenter" runat="server">SERVICE_CENTER_NAME</asp:label>:</TD>
														</TR>
														<TR>
															<TD style="WIDTH: 164px">
																<asp:textbox id="txtClaimNumber" runat="server" Width="152px" Enabled="False" CssClass="FLATTEXTBOX"
																	AutoPostBack="False"></asp:textbox></TD>
															<TD style="WIDTH: 372px">
																<asp:textbox id="txtCustomerName" runat="server" Width="100%" Enabled="False" CssClass="FLATTEXTBOX"
																	AutoPostBack="False"></asp:textbox></TD>
															<TD>
																<asp:textbox id="txtServiceCenter" runat="server" Width="328px" Enabled="False" CssClass="FLATTEXTBOX"
																	AutoPostBack="False"></asp:textbox></TD>
														</TR>
														<TR>
															<TD colSpan="3">
																<HR style="HEIGHT: 1px">
																&nbsp;</TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
											<TR id="trPageSize" runat="server">
												<TD vAlign="top" align="left">
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
												<TD vAlign="top" align="center" colSpan="2" height="100%">
													<asp:datagrid id="Grid" runat="server" Width="100%" AllowPaging="True" CellPadding="1" BorderColor="#999999"
														BackColor="#DEE3E7" BorderWidth="1px" BorderStyle="Solid" AutoGenerateColumns="False" OnItemCommand="ItemCommand"
														OnItemCreated="ItemCreated" height="100%">
														<SelectedItemStyle Wrap="False" BorderStyle="Solid" BorderColor="Orange" BackColor="LightSteelBlue"></SelectedItemStyle>
														<EditItemStyle Wrap="False"></EditItemStyle>
														<AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
														<ItemStyle Wrap="False" BackColor="White"></ItemStyle>
														<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
														<Columns>
															<asp:TemplateColumn>
																<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="5%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
																<ItemTemplate>
																	<asp:ImageButton style="cursor:hand;" id="EditButton_WRITE" ImageUrl="../Navigation/images/icons/yes_icon.gif"
																		runat="server" CommandName="SelectAction"></asp:ImageButton>
																</ItemTemplate>
															</asp:TemplateColumn>
															<asp:BoundColumn Visible="False" HeaderText="claim_invoice_id">
																<HeaderStyle Width="5px"></HeaderStyle>
															</asp:BoundColumn>
															<asp:BoundColumn SortExpression="INVOICE_NUMBER" HeaderText="INVOICE_NUMBER">
																<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="20%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Left"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn SortExpression="PAYEE" HeaderText="PAYEE">
																<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="30px"></HeaderStyle>
																<ItemStyle HorizontalAlign="Left"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn SortExpression="DATE_CREATED" HeaderText="DATE_CREATED">
																<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="12%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Left"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn SortExpression="PAYMENT_AMOUNT" HeaderText="PAYMENT_AMOUNT">
																<HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="15%"></HeaderStyle>
																<ItemStyle HorizontalAlign="Center"></ItemStyle>
															</asp:BoundColumn>
															<asp:BoundColumn SortExpression="PAID_BY" HeaderText="PAID_BY">
																<HeaderStyle Width="15%"></HeaderStyle>
															</asp:BoundColumn>
														</Columns>
														<PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7" Mode="NumericPages"></PagerStyle>
													</asp:datagrid></TD>
											</TR>
											<TR id="trTotalPaid" runat="server">
												<TD align="right" colSpan="2">
													<asp:label id="lblTotalPaid" Runat="server">TOTAL_PAID</asp:label>&nbsp;
													<asp:textbox id="txtTotalPaid" style="BORDER-RIGHT: #c6c6c6 1px solid; BORDER-TOP: #c6c6c6 1px solid; BORDER-LEFT: #c6c6c6 1px solid; BORDER-BOTTOM: #c6c6c6 1px solid; TEXT-ALIGN: right"
														runat="server" CssClass="FLATTEXTBOX" AutoPostBack="False" ReadOnly="True" width="95px"></asp:textbox></TD>
											</TR>
											<TR>
												<TD align="center" colSpan="2">&nbsp;</TD>
											</TR>
											<TR>
												<TD align="center" colSpan="2">
													<TABLE id="tblButtons" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; WIDTH: 98%; BORDER-BOTTOM: #999999 1px solid; HEIGHT: 30px"
														cellSpacing="0" cellPadding="6" width="100%" bgColor="#f1f1f1" border="0" runat="server">
														<TR>
															<TD align="center">
																<asp:button id="btnNEW_PAYMENT" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
																	tabIndex="200" runat="server" Font-Bold="false" Width="150px" Enabled="False" CssClass="FLATBUTTON"
																	height="20px" Text="NEW_PAYMENT"></asp:button></TD>
															<TD align="center">
																<asp:button id="btnADJUST_PAYMENT" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
																	tabIndex="200" runat="server" Font-Bold="false" Width="150px" Enabled="False" CssClass="FLATBUTTON"
																	height="20px" Text="ADJUST_PAYMENT"></asp:button></TD>
															<TD align="center">
																<asp:button id="btnREVERSE_PAYMENT" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/add_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
																	tabIndex="200" runat="server" Font-Bold="false" Width="150px" Enabled="False" CssClass="FLATBUTTON"
																	height="20px" Text="REVERSE_PAYMENT"></asp:button></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
											<TR>
												<TD align="center" colSpan="2">&nbsp;
													<TABLE id="tblHR" cellSpacing="1" cellPadding="1" width="100%" border="0" runat="server">
														<TR>
															<TD>
																<HR style="HEIGHT: 1px">
																&nbsp;</TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
											<TR>
												<TD align="center" colSpan="2">
													<TABLE id="tblPaymentDetails" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; WIDTH: 98%; BORDER-BOTTOM: #999999 1px solid; HEIGHT: 30px"
														cellSpacing="1" cellPadding="1" width="100%" bgColor="#f1f1f1" border="0" runat="server">
														<TR>
															<TD>
																<TABLE id="table1" cellSpacing="0" cellPadding="0" width="100%" border="0">
																	<TR>
																	<TD style="WIDTH: 279px; HEIGHT: 12px">&nbsp;*
																			<asp:label id="lblINVOICE_NUMBER" runat="server" Font-Bold="false">INVOICE_NUMBER</asp:label>:</TD>
							                                     	<TD class="style1">&nbsp;
							                                     	        <asp:Label ID="lblInvDateAsterisk" runat="server" Font-Bold="false">*</asp:Label>
																			<asp:label id="lblInvoiceDate" runat="server" Font-Bold="false">INVOICE_DATE</asp:label></TD>
																	<TD style="HEIGHT: 12px">&nbsp;
																	        <asp:Label ID="lblAdjAmtAsterisk" runat="server" Font-Bold="false">*</asp:Label>
																			<asp:label id="lblADJUSTMENT_AMOUNT" runat="server" Font-Bold="false">ADJUSTMENT_AMOUNT</asp:label></TD>
																												</TR>
																	<TR>
																		<TD style="WIDTH: 279px">&nbsp;
																			<asp:textbox id="txtInvoiceNumber" tabIndex="1" runat="server" Width="264px" CssClass="FLATTEXTBOX_TAB"
																				MaxLength="20"></asp:textbox></TD>
										                                <TD class="style2">&nbsp;
																			<asp:textbox id="txtInvoiceDate" tabIndex="1" runat="server" Width="128px" CssClass="FLATTEXTBOX_TAB"></asp:textbox>
																			<asp:ImageButton ID="ImageButtonInvoiceDate" runat="server" CausesValidation="False" Visible="True" ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="AbsMiddle"></asp:ImageButton></TD>
																	    <TD>&nbsp;
																			<asp:textbox id="txtADJUSTMENT_AMOUNT" tabIndex="1" runat="server" Width="128px" CssClass="FLATTEXTBOX_TAB"></asp:textbox></TD>
																   </TR>
																</TABLE>
															</TD>
														</TR>
														<TR>
															<TD align="right">
																<asp:button id="btnSave" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/save_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
																	tabIndex="11" runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" height="20px"
																	Text="Save"></asp:button>&nbsp;
																<asp:button id="btnCancel" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/cancel_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
																	runat="server" Font-Bold="false" Width="100px" CssClass="FLATBUTTON" height="20px" Text="Cancel"></asp:button></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
											<TR>
												<TD align="center" colSpan="2">
													<TABLE id="Table2" cellSpacing="1" cellPadding="1" width="100%" border="0" runat="server">
														<TR>
															<TD>
																<HR style="HEIGHT: 1px">
																&nbsp;</TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
											<TR>
												<TD align="left" colSpan="2">
													<asp:button id="btnBack" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/back_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
														tabIndex="35" runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" height="20px"
														Text="Back" CausesValidation="False"></asp:button><INPUT id="HiddenSaveChangesPromptResponse" style="WIDTH: 5px" type="hidden" name="HiddenSaveChangesPromptResponse"
														runat="server" DESIGNTIMEDRAGDROP="261"/></TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
							</TABLE>
						</asp:panel></td>
				</tr>
			</TABLE>
		</form>
		<script>
/*		
				//Global var
		var objPaymentDetailsTable = document.getElementById('tblPaymentDetails');	
		var objtxtADJUSTMENT_AMOUNT = document.getElementById('txtADJUSTMENT_AMOUNT');
		var objlblADJUSTMENT_AMOUNT = document.getElementById('lblADJUSTMENT_AMOUNT');
		var objbtnREVERSE_PAYMENT = document.getElementById('btnREVERSE_PAYMENT');
		var objbtnADJUST_PAYMENT = document.getElementById('btnADJUST_PAYMENT');
		var objbtnNEW_PAYMENT = document.getElementById('btnNEW_PAYMENT');
		var objHiddenButtonClickedValue = document.getElementById('txtButtonClickedHiddenValue');
		
		if (objHiddenButtonClickedValue.value == "A")
		{
			objPaymentDetailsTable.style.visibility = 'visible';
			objtxtADJUSTMENT_AMOUNT.style.visibility = 'visible';
			objlblADJUSTMENT_AMOUNT.style.visibility = 'visible';
		}
		else if (objHiddenButtonClickedValue.value == "R")
		{
			objPaymentDetailsTable.style.visibility = 'visible';
			objtxtADJUSTMENT_AMOUNT.style.visibility = 'visible';
			objlblADJUSTMENT_AMOUNT.style.visibility = 'visible';
		}

*/

	function resizeScroller(item)
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
					newHeight = browseHeight - 220;
				}
				else
				{
					newHeight = browseHeight - 975;
				}
				
				if (newHeight < 470)
				{
					newHeight = 470;
				}
					
				item.style.height = String(newHeight) + "px";
				
				return newHeight;
			}
			
			//resizeScroller(document.getElementById("scroller"));
		</script>
	</body>
</HTML>
