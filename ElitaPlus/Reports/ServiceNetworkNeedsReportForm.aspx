﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ServiceNetworkNeedsReportForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.ServiceNetworkNeedsReportForm" %>
<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
	<head>
		<title id="rptWindowTitle" runat="server">Service_Network_Needs</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR"/>
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE"/>
		<meta content="JavaScript" name="vs_defaultClientScript"/>
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
		<link href="../Styles.css" type="text/css" rel="STYLESHEET"/>
		<script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>
	    </head>
	<body leftMargin="0" topMargin="0" onload="changeScrollbarColor();" border="0" ms_positioning="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--Start Header--><input id="rptTitle" type="hidden" name="rptTitle"/> <input id="rptSrc" type="hidden" name="rptSrc"/>
			<table style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<tr>
					<td vAlign="top">
						<table width="100%" border="0">
							<tr>
								<td height="20"><asp:label id="LabelReports" runat="server" CssClass="TITLELABEL">Reports</asp:label>:&nbsp;
								<asp:label id="Label7" runat="server"  CssClass="TITLELABELTEXT">Service_Network_Needs</asp:label></td>
							</tr>
						</table>
					</td>
				</tr>
			</table>
			<table id="tblOuter2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				height="93%" cellSpacing="0" cellPadding="0" rules="none" width="98%" bgColor="#d5d6e4"
				border="0"> <!--d5d6e4-->
				<tr>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td vAlign="top" align="center" height="100%"><asp:panel id="WorkingPanel" runat="server">
							<table id="tblMain1" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid"
								cellSpacing="0" cellPadding="6" rules="cols" width="98%" align="center" bgColor="#fef9ea" height="98%"
								border="0">
								<tr>
									<td height="1"></td>
								</tr>
								<tr>
									<td>
										<table cellSpacing="0" cellPadding="0" width="100%" border="0">
											<tr>
												<td colSpan="3">
													 <table id="tblSearch" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                                                border-left: #999999 1px solid; width: 100%; border-bottom: #999999 1px solid;
                                                height: 76px" cellspacing="2" cellpadding="8" rules="cols" width="100%" align="center"
                                                bgcolor="#fef9ea" border="0">
                                                <tr>
                                                    <td>
                                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                            <tr>
                                                                <td align="center" colspan="3">
                                                                    <uc1:ErrorController ID="ErrorCtrl" runat="server"></uc1:ErrorController>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                        <uc1:ReportCeInputControl ID="moReportCeInputControl" runat="server"></uc1:ReportCeInputControl>
                                                    </td>
                                                </tr>
                                            </table>
												</td>
											</tr>
											<tr>
												<td colSpan="3"><img height="15" src="../Navigation/images/trans_spacer.gif"/></td>
											</tr>
											<tr>
												<td align="center" colSpan="3">
													<table cellSpacing="2" cellPadding="0" width="85%" border="0">
														<tr>
															<td colSpan="2">&nbsp;</td>
														</tr>
														<tr>
															<td noWrap align="right" width="25%" colSpan="2">
																<asp:RadioButton id="OptSelectAllCities" 
                                                                    onclick="toggleAllCitiesSelection(false);" Text="SELECT_ALL_CITIES"
																	AutoPostBack="false" Runat="server" TextAlign="left"></asp:RadioButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																<asp:label id="moSelectACityLabel" runat="server">OR_A_SINGLE_CITY</asp:label>:</td>
															</td>
															<td noWrap align="left" width="25%">&nbsp;
																<asp:textbox id="moCityText" tabIndex="1" onclick="toggleAllCitiesSelection(true);" runat="server" CssClass="FLATTEXTBOX" width="200px"></asp:textbox></TD>
														</tr>
														<tr>
															<td colspan="3">&nbsp;</td>
														</tr>
														<tr>
															<td colspan="3">&nbsp;</td>
														</tr>
													
														<tr>
															<td noWrap align="right" width="25%" colSpan="2" valign="top">
					                                                <TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
																				<tr>
																					<TD><IMG height="7" src="../Navigation/images/trans_spacer.gif" width="1"/></TD>
																				</tr>
																				<tr>
																					<TD noWrap align="right">
																						<asp:label id="Label2" runat="server">SELECT_REPORT_SORT_ORDER</asp:label>:</TD>
																				</tr>
																	</TABLE>                                                            
															</td>
															<td noWrap align="left" valign="top" width="25%">
                                                                <asp:RadioButtonList ID="rdReportSortOrder" runat="server">
																    <asp:ListItem Value="1">#_of_Items</asp:ListItem>
                                                                    <asp:ListItem Value="2">City</asp:ListItem>
                                                                    <asp:ListItem Value="3">Zip_Code</asp:ListItem>
                                                                </asp:RadioButtonList></td>
														</tr>
														<tr>
															<td colspan="3">&nbsp;</td>
														</tr>
														<tr>
															<td colspan="3"><hr style="height:1px"/></td>
														</tr>
														<tr>
															<td colspan="3">&nbsp;</td>
														</tr>
				                                         <tr>
															<td noWrap align="right" width="25%" colSpan="1">*
																<asp:RadioButton id="RadiobuttonTotalsOnly" onclick="toggleDetailSelection(false);" Text="SHOW TOTALS ONLY"
																	AutoPostBack="false" Runat="server" TextAlign="left"></asp:RadioButton>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
															</td>
																<td vAlign="middle" noWrap align="right" width="5%" colSpan="1"></td>
															<td noWrap align="left" width="25%">&nbsp;
																<asp:RadioButton id="RadiobuttonDetail" onclick="toggleDetailSelection(true);" Text="OR SHOW DETAIL WITH TOTALS"
																	AutoPostBack="false" Runat="server" TextAlign="left"></asp:RadioButton></td>
														</tr>														
                                        				<tr>
															<td colSpan="2">&nbsp;</td>
														</tr>
													</table>
												</td>
										</tr>
                                        <tr>
									        <td>
										        &nbsp;
									        </td>
								        </tr>
								</table>
								</td>
                                </tr>
                                <tr>
                                    <td style="height: 95%; width: 100%;">
                                    </td>
                                </tr>
                                <tr>
									<td>
										<hr style="height:1px"/>
									</td>
								</tr>
								<tr>
									<td align="left">&nbsp;
										<asp:button id="btnGenRpt" style="BACKGROUND-IMAGE: url(../Navigation/images/viewIcon2.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server"  Width="100px" Text="View" CssClass="FLATBUTTON" height="20px"></asp:button></td>
								</tr>
							</table>
						</asp:panel></td>
				</tr>
			</table>
		</form>
		<script language="JavaScript" src="../Navigation/Scripts/ReportCeMainScripts.js"></script>
		<script language="JavaScript">
        function toggleAllCitiesSelection(isSingleCity)
	        {
		        if(isSingleCity)
		        {
			        document.forms[0].OptSelectAllCities.checked = false;
		        }
		        else
		        {
			        document.forms[0].moCityText.value = "";
		        }
	        }
     
      
	</script>	
	</body>
</html>
