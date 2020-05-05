<%@ Page Language="vb" AutoEventWireup="false" Codebehind="CopyDeleteDealerDefinitionForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.CopyDeleteDealerDefinitionForm" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title>CopyDeleteDealerDefinition</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="STYLESHEET">
		<script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>
		<script language="javascript">
	
	function LoadMessage()
		{
		//debugger;
	     
		 var Message = document.all["txtMessageResponse"].value;
		 var Result = window.confirm(Message);
		 if (Result == true)
		   {	
		   document.all["txtMessageResponse"].value = "Ok"
			Form1.submit();	
		   }
		   else 
		   {
		   document.all["txtMessageResponse"].value ="Cancel" 
		   }
		}
		
	/*function enable_disable_div(rCopyDeleteIndex, mchkboxIndex)
		{
			//alert (rCopyDeleteIndex);
			//alert (document.getElementById('DivCovDates').style.visibility);
			
			if ( (rCopyDeleteIndex == 0) && (mchkboxIndex = false) )
			{
				document.getElementById('DivCovDates').style.visibility = "";
			}
			else
			{	
				document.getElementById('DivCovDates').style.visibility = "hidden";
			}
		}*/
	
/*	function ToggleDualDropDownsSelection(ctlCodeDropDown, ctlDecDropDown, change_Dec_Or_Code)
	{
		var objCodeDropDown = document.getElementById(ctlCodeDropDown); // "By Code" DropDown control
		var objDecDropDown = document.getElementById(ctlDecDropDown);   // "By Description" DropDown control 
				
		if (change_Dec_Or_Code=='C')
			{
				objCodeDropDown.value = objDecDropDown.options[objDecDropDown.selectedIndex].value;
			}
			else
			{
				objDecDropDown.value = objCodeDropDown.options[objCodeDropDown.selectedIndex].value;
			}
	}
*/
		</script>
	</HEAD>
	<body leftMargin="0" topMargin="0" onload="changeScrollbarColor();" border="0" MS_POSITIONING="GridLayout">
		<FORM id="Form1" method="post" runat="server">
			<!--Start Header-->
			<TABLE style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; WIDTH: 98%; BORDER-BOTTOM: black 1px solid; HEIGHT: 20px"
				cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE width="100%" border="0">
							<TR>
								<TD height="20"><asp:label id="TablesLabel" runat="server" Cssclass="TITLELABEL">Tables</asp:label>:
								<asp:label id="NEW_DEALER_DEFINITIONSLabel" runat="server" Cssclass="TITLELABELTEXT">NEW_DEALER_DEFINITIONS</asp:label></TD>
								<TD align="right" height="20">*&nbsp;
									<asp:label id="moIndicatesLabel" runat="server">INDICATES_REQUIRED_FIELDS</asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="tblOuter2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				height="93%" cellSpacing="0" cellPadding="4" rules="none" width="98%" bgColor="#d5d6e4"
				border="0"> <!--d5d6e4-->
				<tr>
					<td vAlign="top" height="90%"><asp:panel id="pnlAllTranslation" runat="server" HorizontalAlign="Center">
							<TABLE id="tblMain1" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid"
								height="98%" cellSpacing="0" cellPadding="6" width="98%" align="center" bgColor="#fef9ea"
								border="0"> <!--fef9ea LightGoldenrodYellow-->
								<TR>
									<TD vAlign="top" height="1">
										<uc1:errorcontroller id="ErrorController" runat="server" Visible="False"></uc1:errorcontroller></TD>
								</TR>
								<TR vAlign="top">
									<TD vAlign="top" height="5%">
										<TABLE id="tblSearch" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid"
											cellSpacing="0" cellPadding="0" width="98%" align="center" bgColor="#f1f1f1" border="0"> <!--fef9ea-->
											<TR>
												<TD vAlign="middle" align="left">
													<asp:radiobuttonlist id="rCopyDelete" runat="server" Font-Bold="false" CellSpacing="0" CellPadding="0"
														Height="0px" OnSelectedIndexChanged="Index_Changed" AutoPostBack="true" RepeatDirection="Horizontal"
														Width="527px">
														<asp:ListItem Value="Copy" Selected="True">Copy</asp:ListItem>
														<asp:ListItem Value="Delete">Delete</asp:ListItem>
														<asp:ListItem Value="Renew">RENEW</asp:ListItem>														
													</asp:radiobuttonlist></TD>
											</TR>
										</TABLE>
										<HR style="WIDTH: 98%; HEIGHT: 1px" SIZE="1">
									</TD>
								</TR>
								<TR vAlign="top">
									<TD style="HEIGHT: 295px" vAlign="top" align="left" width="100%">
										<TABLE id="Table3" cellSpacing="1" cellPadding="1" width="65%" border="0">
											<TR>
												<TD>
													<TABLE id="Table2" cellSpacing="2" cellPadding="0" width="60%" border="0">
														<TR>
															<TD style="HEIGHT: 13px" noWrap align="right" colSpan="3">
																<uc1:MultipleColumnDDLabelControl id="moFromMultipleColumnDropControl" runat="server"></uc1:MultipleColumnDDLabelControl></TD>
														</TR>
														<TR>
															<TD noWrap align="right" colSpan="3"></TD>
														</TR>
														<TR>
															<TD noWrap align="right" colSpan="3">
																<uc1:MultipleColumnDDLabelControl id="moToMultipleColumnDropControl" runat="server"></uc1:MultipleColumnDDLabelControl></TD>
														</TR>
														<TR>
															<TD noWrap align="right" colSpan="3">&nbsp;</TD>
														</TR>
														<TR>
															<TD noWrap align="right" colSpan="2"></TD>
															<TD noWrap align="right"></TD>
														</TR>
														<TR>
															<TD noWrap align="right" colSpan="2"></TD>
															<TD noWrap align="right"></TD>
														</TR>
														<TR>
															<TD noWrap align="right" colSpan="2"></TD>
															<TD noWrap align="right"></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
											<TR>
												<TD>
													<TABLE id="Table1" cellSpacing="1" cellPadding="1" width="60%" border="0">
														<TR> <!--<TD style="WIDTH: 81px"></TD>-->
															<TD noWrap align="left" colSpan="3">
																<asp:CheckBox id="chkExcludeCoveragesAndRates" runat="server" AutoPostBack="true" Text="EXCLUDE_COVERAGES_AND_RATES"></asp:CheckBox></TD> <!--<TD></TD>--></TR>
														<TR>
															<TD noWrap align="right" colSpan="3">&nbsp;</TD>
														</TR>
													</TABLE>
													<DIV id="DivCovDates">
														<TABLE>
															<TR>
																<TD noWrap align="left" colSpan="3">
																	<asp:label id="LabelEnterCovDateHeader" runat="server" >Enter_Coverage_Date_Header</asp:label></TD>
															</TR>
															<TR>
																<TD noWrap align="right" colSpan="3"></TD>
															</TR>
															<TR>
																<TD noWrap align="right" colSpan="3"></TD>
															</TR>
															<TR>
																<TD style="WIDTH: 84px; HEIGHT: 24px" vAlign="baseline" align="right" width="84">&nbsp;
																	<asp:label id="LabelStar1" runat="server" Font-Bold="false">*</asp:label>
																	<asp:label id="LabelEffDate" runat="server" Font-Bold="false">Effective</asp:label></TD>
																<TD style="HEIGHT: 24px" noWrap align="left">
																	<asp:textbox id="TextboxEffDate" runat="server" CssClass="FLATTEXTBOX" width="150px"></asp:textbox>&nbsp;
																	<asp:ImageButton id="ImageButtonEffDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:ImageButton></TD>
																<TD style="HEIGHT: 24px"></TD>
															</TR>
															<TR>
																<TD noWrap align="right" colSpan="3"></TD>
															</TR>
															<TR>
																<TD noWrap align="right" colSpan="3"></TD>
															</TR>
															<TR>
																<TD style="WIDTH: 84px" vAlign="baseline" noWrap align="right">&nbsp;
																	<asp:label id="LabelStar2" runat="server" Font-Bold="false">*</asp:label>
																	<asp:label id="LabelExpDate" runat="server" Font-Bold="false">Expiration</asp:label></TD>
																<TD noWrap align="left">
																	<asp:textbox id="TextboxExpDate" runat="server" CssClass="FLATTEXTBOX" width="150px"></asp:textbox>&nbsp;
																	<asp:ImageButton id="ImagebuttonExpDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:ImageButton></TD>
																<TD></TD>
															</TR>
														</TABLE>
													</DIV>
												</TD>
											</TR>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD vAlign="top" align="left"></TD>
								</TR>
								<TR>
									<TD style="HEIGHT: 22px" vAlign="top" align="center"></TD>
								</TR>
								<TR>
									<TD align="center" width="100%">
										<HR style="WIDTH: 98%; HEIGHT: 1px" SIZE="1">
									</TD>
								</TR>
								<TR>
									<TD align="left">&nbsp;
										<asp:button id="btnBack" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/back_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											tabIndex="185" runat="server" Font-Bold="false" Width="90px" Text="Return" CssClass="FLATBUTTON"
											height="20px"></asp:button>&nbsp;
										<asp:button id="btnSave_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/save_trans_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server" Font-Bold="false" Width="90px" Text="Save" CssClass="FLATBUTTON" height="20px"></asp:button>&nbsp;
										<asp:button id="btnCancel_WRITE" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/cancel_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server" Font-Bold="false" Width="90px" Text="Cancel" CssClass="FLATBUTTON" height="20px"></asp:button></TD>
								</TR>
							</TABLE>
                    </asp:panel><input id="txtMessageResponse" type="hidden" name="txtMessageResponse" runat="server"/></td>
				</tr>
			</TABLE>
		</FORM>
	</body>
</HTML>
