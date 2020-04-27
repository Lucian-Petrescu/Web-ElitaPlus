<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BillingAndCollectionReportForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.BillingAndCollectionReportForm" %>

<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>


<!doctype html public "-//w3c//dtd html 4.0 transitional//en">
<html>
	<head>
		<title id="rptWindowTitle" runat="server">BILLING_AND_COLLECTION_RPT</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR" />
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE" />
		<meta content="JavaScript" name="vs_defaultClientScript" />
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" /> 
		<link href="../Styles.css" type="text/css" rel="STYLESHEET" />
		<script type="text/jscript"  language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>
	</head>

	<body leftMargin="0" topMargin="0" onload="changeScrollbarColor();" MS_POSITIONING="GridLayout">
		<form id="form1" method="post" runat="server">
			<!--start header-->
            <input id="rpttitle" type="hidden" name="rpttitle" />
            <input id="rptsrc" type="hidden" name="rptsrc" />

			<table style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid"
				cellspacing="0" cellpadding="0" width="98%" bgcolor="#d5d6e4" border="0">
				<tr>
					<td valign="top">
						<table width="100%" border="0">
							<tr>
								<td height="20">
									<asp:label id="LabelReports" runat="server" cssclass="TITLELABEL">Reports</asp:label>:
									<asp:label id="LabelReportHeader" runat="server" cssclass="TITLELABELTEXT"></asp:label>
                                </td>
								<td height="20" align="right">*&nbsp;
									<asp:label id="moIndicatesLabel" runat="server" Font-Bold="false" EnableViewState="true">INDICATES_REQUIRED_FIELDS</asp:label>
                                </td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<table id="tblouter2" style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid"
				height="93%" cellspacing="0" cellpadding="0" rules="none" width="98%" bgcolor="#d5d6e4"
				border="0"> <!--d5d6e4-->
				<tr><td>&nbsp;</td></tr>
				<tr>
					<td valign="top" align="center" height="100%">
                        <asp:panel id="workingpanel" runat="server">
							<table id="tblmain1" style="border-right: #999999 1px solid; border-top: #999999 1px solid; border-left: #999999 1px solid; border-bottom: #999999 1px solid"
								cellspacing="0" cellpadding="6" rules="cols" width="98%" align="center" bgcolor="#fef9ea"
								border="0">
								<tr><td height="1"></td></tr>
								<tr>
									<td>
										<table cellspacing="0" cellpadding="0" width="100%" border="0">
											<tr>
												<td colspan="3">
													<table id="tblsearch" style="border-right: #999999 1px solid; border-top: #999999 1px solid; border-left: #999999 1px solid; width: 628px; border-bottom: #999999 1px solid; height: 64px"
														cellspacing="2" cellpadding="8" rules="cols" width="628" align="center" bgcolor="#fef9ea"
														border="0">
														<tr>
															<td>
																<table cellspacing="0" cellpadding="0" width="100%" border="0">
																	<tr>
																		<td align="center" colspan="3">
																			<uc1:errorcontroller id="errorctrl" runat="server"></uc1:errorcontroller>
                                                                        </td>
																	</tr>
																	<tr><td nowrap="nowrap" align="right">&nbsp;</td></tr>
																</table>
																<uc1:ReportCeInputControl id="moReportCeInputControl" runat="server"></uc1:ReportCeInputControl>
                                                            </td>
														</tr>
													</table>
												</td>
											</tr>
											<tr><td colspan="3"><img height="15" src="../navigation/images/trans_spacer.gif" /></td></tr>
											<tr><td style="width: 99.43%; height: 1px"></td></tr>
											<tr>
												<td align="center" colspan="3">
													<table cellspacing="2" cellpadding="0" width="95%" border="0">
                                                        <tr align='center' >
                                                            <td colspan="3" valign ="middle">
                                                                <asp:RadioButtonList ID="rdDate_type" runat="server" RepeatDirection="Horizontal" CellPadding="4" CellSpacing="4">
                                                                    <asp:ListItem Value="ProcessDate" Selected="True">PROCESS_DATE</asp:ListItem>
                                                                    <asp:ListItem Value="BillingDate">BILLING_DATE</asp:ListItem> 
                                                                    <asp:ListItem Value="PayFromDate">PAID_FROM_DATE</asp:ListItem> 
                                                                </asp:RadioButtonList>
                                                            </td>
                                                        </tr>
														<tr>
															<td style="height: 10px" valign="middle" align="center" width="50%" colspan="3" rowspan="1">
																<table style="width: 484px; height: 18px" cellspacing="0" cellpadding="0" width="484" border="0"  >
																	<tr>
																		<td valign="middle" nowrap="nowrap" align="right">*
																			<asp:label id="mobegindatelabel" runat="server" font-bold="False">BEGIN_DATE</asp:label>:
                                                                        </td>
																		<td style="width: 110px" nowrap="nowrap">&nbsp;
																			<asp:textbox id="moBeginDateText" tabindex="1" runat="server" width="95px" cssclass="flattextbox"></asp:textbox>
                                                                        </td>
																		<td style="width: 15px" valign="middle" align="center">
																			<asp:imagebutton id="btnBeginDate" runat="server" width="20px" imageurl="../common/images/calendaricon2.jpg"
																				height="17px"></asp:imagebutton>
                                                                        </td>
																		<td valign="middle" nowrap="nowrap" align="right" width="15" colspan="1" rowspan="1"></td>
																		<td valign="middle" nowrap="nowrap" align="right">*
																			<asp:label id="moenddatelabel" runat="server" font-bold="False">END_DATE</asp:label>:
                                                                        </td>
																		<td style="width: 110px" nowrap="nowrap">&nbsp;
																			<asp:textbox id="moEndDateText" tabindex="1" runat="server" width="95px" cssclass="flattextbox"></asp:textbox>
                                                                        </td>
																		<td style="width: 15px" valign="middle" align="center">
																			<asp:imagebutton id="btnEndDate" runat="server" width="20px" imageurl="../common/images/calendaricon2.jpg"
																				height="17px"></asp:imagebutton>
                                                                        </td>
																	</tr>
																</table>
															</td>
														</tr>
														<tr><td><hr style="width: 99.43%; height: 1px" /></td></tr>
                                                        <tr>
                                                            <td align='center'>
                                                                <table border='0' width="60%">

														            <tr style="height: 40px">
        													            <td align="left" nowrap="nowrap"> 
																            <table border="0" cellspacing="0" cellpadding="0" >
																	            <tr>
																		            <td valign="bottom" nowrap="nowrap" >*
																			            <asp:radiobutton id="rdealer" Text="SELECT_ALL_DEALERS" AutoPostBack="false" 
                                                                                            Checked="False" Runat="server" TextAlign="left" 
                                                                                            
                                                                                            
                                                                                            onclick="ToggleDualDropDownsSelection('multipleDropControl_moMultipleColumnDrop', 'multipleDropControl_moMultipleColumnDropDesc', 'rdealer', false, ''); document.all.item('multipleDropControl_lb_DropDown').style.color = '';" />
                                                                                        &nbsp;
																		            </td>
																		            <td align='left' valign="baseline" nowrap="nowrap" >
																			            <uc1:MultipleColumnDDLabelControl id="multipleDropControl" runat="server"></uc1:MultipleColumnDDLabelControl>
                                                                                    </td>
																	            </tr>
																            </table>
                                                                        </td>
														            </tr>
                                                                </table>
                                                            </td>
														</tr>
													</table>
												</td>
											</tr>
										</table>
									</td>
								</tr>
								
                                <tr><td style="height: 20px"></td></tr>

								<tr><td><hr /></td></tr>
								
                                <tr>
									<td align="left">
										<asp:button id="btngenrpt" style="background-image: url(../navigation/images/viewicon2.gif); cursor: hand; background-repeat: no-repeat"
											runat="server" font-bold="false" cssclass="flatbutton" width="100px" text="view" height="20px" />
                                    </td>
								</tr>
							</table>
					    </asp:panel>
                    </td>
				</tr>
			</table>
		</form>

		<script language="javascript" src="../navigation/scripts/ReportCEMainScripts.js"></script>
	</body>
</html>
