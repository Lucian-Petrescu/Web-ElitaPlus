<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="NewCertificatesForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.NewCertificatesForm"%>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UserControlAvailableSelected" Src="../Common/UserControlAvailableSelected.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
	<HEAD>
		<title id="rptWindowTitle" runat="server">NewCertificates</title>
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<link href="../Styles.css" type="text/css" rel="STYLESHEET">
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>
	</HEAD>
	<body leftMargin="0" topMargin="0" onload="changeScrollbarColor();" MS_POSITIONING="GridLayout">
		<form id="Form1" method="post" runat="server">
			<!--Start Header--><input id="rptTitle" type="hidden" name="rptTitle"> <input id="rptSrc" type="hidden" name="rptSrc">
			<TABLE style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE width="100%" border="0">
							<TR>
								<TD height="20">
									<asp:label id="LabelReports" runat="server" CssClass="TITLELABEL">Reports</asp:label>:
									<asp:label id="Label7" runat="server" CssClass="TITLELABELTEXT">New Certificates</asp:label></TD>
								<TD height="20" align="right">*&nbsp;
									<asp:label id="moIndicatesLabel" runat="server" Font-Bold="false" EnableViewState="true">INDICATES_REQUIRED_FIELDS</asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="tblOuter2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				height="93%" cellSpacing="0" cellPadding="0" rules="none" width="98%" bgColor="#d5d6e4"
				border="0"> <!--d5d6e4-->
				<tr>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td vAlign="top" align="center" height="100%"><asp:panel id="WorkingPanel" runat="server">
							<TABLE id="tblMain1" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid"
								cellSpacing="0" cellPadding="6" rules="cols" width="98%" align="center" bgColor="#fef9ea"
								border="0">
								<TR>
									<TD height="1"></TD>
								</TR>
								<TR>
									<TD>
										<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
											<TR>
												<TD colSpan="3">
													<TABLE id="tblSearch" style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; WIDTH: 628px; BORDER-BOTTOM: #999999 1px solid; HEIGHT: 64px"
														cellSpacing="2" cellPadding="8" rules="cols" width="628" align="center" bgColor="#fef9ea"
														border="0">
														<TR>
															<TD>
																<TABLE cellSpacing="0" cellPadding="0" width="100%" border="0">
																	<TR>
																		<TD align="center" colSpan="3">
																			<uc1:ErrorController id="ErrorCtrl" runat="server"></uc1:ErrorController></TD>
																	</TR>
																	<TR>
																		<TD noWrap align="right">&nbsp;
																		</TD>
																	</TR>
																</TABLE>
																<uc1:ReportCeInputControl id="moReportCeInputControl" runat="server"></uc1:ReportCeInputControl></TD>
														</TR>
													</TABLE>
												</TD>
											</TR>
											<TR>
												<TD colSpan="3"><IMG height="15" src="../Navigation/images/trans_spacer.gif"></TD>
											</TR>
											<TR>
											    <td style="WIDTH: 100%; HEIGHT: 1px">
                                                </td></TR> 
                                                          <tr>
                                                            <td colspan="3" style="HEIGHT: 27px">
                                                                <hr style="WIDTH: 100%; HEIGHT: 1px">
                                                                </hr>
                                                            </td>
                                                        </tr>
                                            <tr>
                                                <td align="center" colspan="3">
                                                    <table border="0" cellpadding="0" cellspacing="2" width="100%">
                                                        <tr>
                                                            <td align="center" colspan="3" rowspan="1" style="HEIGHT: 10px" valign="middle" 
                                                                width="50%">
                                                                <table border="0" cellpadding="0" cellspacing="0" 
                                                                    style="WIDTH: 484px; HEIGHT: 18px" width="484">
                                                                    <tr>
                                                                        <td align="right" nowrap valign="middle">
                                                                            *
                                                                            <asp:Label ID="moBeginDateLabel" runat="server" Font-Bold="false">BEGIN_DATE</asp:Label>
                                                                            :</td>
                                                                        <td nowrap style="WIDTH: 110px">
                                                                            &nbsp;
                                                                            <asp:TextBox ID="moBeginDateText" runat="server" CssClass="FLATTEXTBOX" 
                                                                                tabIndex="1" width="95px"></asp:TextBox>
                                                                        </td>
                                                                        <td align="center" style="WIDTH: 15px" valign="middle">
                                                                            <asp:ImageButton ID="BtnBeginDate" runat="server" Height="17px" 
                                                                                ImageUrl="../Common/Images/calendarIcon2.jpg" Width="20px" />
                                                                        </td>
                                                                        <td align="right" colspan="1" nowrap rowspan="1" valign="middle" width="15">
                                                                        </td>
                                                                        <td align="right" nowrap valign="middle">
                                                                            *
                                                                            <asp:Label ID="moEndDateLabel" runat="server" Font-Bold="false">END_DATE</asp:Label>
                                                                            :</td>
                                                                        <td nowrap style="WIDTH: 110px">
                                                                            &nbsp;
                                                                            <asp:TextBox ID="moEndDateText" runat="server" CssClass="FLATTEXTBOX" 
                                                                                tabIndex="1" width="95px"></asp:TextBox>
                                                                        </td>
                                                                        <td align="center" style="WIDTH: 15px" valign="middle">
                                                                            <asp:ImageButton ID="BtnEndDate" runat="server" Height="17px" 
                                                                                ImageUrl="../Common/Images/calendarIcon2.jpg" Width="20px" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3" style="HEIGHT: 27px">
                                                                <hr style="WIDTH: 100%; HEIGHT: 1px">
                                                                </hr>
                                                            </td>
                                                        </tr>                                                        
                                                        <tr>
                                                            <td align="center" colspan="3" >
                                                                    <table id="Table2" border="0" cellpadding="1" cellspacing="1" width="85%">
                                                                        <tr>
                                                                            <td align="right" valign="bottom" width="30%">
                                                                                *
                                                                                <asp:RadioButton ID="rdealer" Runat="server" AutoPostBack="false" 
                                                                                    Checked="False" 
                                                                                    onClick="return TogglelDropDownsSelectionsForNewCertForm('rdealer');" 
                                                                                    Text="SELECT_ALL_DEALERS" TextAlign="left" />
                                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                            </td>
                                                                            <td nowrap valign="baseline" width="70%">
                                                                                <uc1:MultipleColumnDDLabelControl ID="multipleDropControl" runat="server" />
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="3" >
                                                                    <table id="Table3" border="0" cellpadding="1" cellspacing="1" width="85%">
                                                                        <tr>
                                                                            <td align="right" valign="bottom" width="30%">
                                                                                
                                                                            </td>
                                                                            <td nowrap valign="baseline" width="70%">
                                                                                <asp:Label ID="lblDealerGroup" runat="server">DEALER_GROUP:</asp:Label>
                                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                                                                <asp:DropDownList ID="moDealerGroupList" runat="server" AutoPostBack="false" onChange="return TogglelDropDownsSelectionsForNewCertForm('dealerGroup');">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                         <tr>
                                                            <td align="center" colspan="3" >
                                                                <table id="Table4" border="0" cellpadding="1" cellspacing="1" width="85%">
                                                                        <tr>
                                                                            <td align="right" valign="bottom" width="30%">
                                                                                
                                                                            </td>
                                                                            <td nowrap valign="baseline" width="100%">
                                                                               <asp:Label ID="lblDealer" runat="server">DEALER:</asp:Label> 
                                                                                <uc1:UserControlAvailableSelected ID="UsercontrolAvailableSelectedDealers" runat="server" >
                                                                                </uc1:UserControlAvailableSelected>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                                    
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3" style="HEIGHT: 27px">
                                                                <hr style="WIDTH: 100%; HEIGHT: 1px">
                                                                </hr>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="3">
                                                                    <table id="Table1" border="0" cellpadding="1" cellspacing="1" width="85%">
                                                                        <tr>
                                                                            <td align="right" valign="bottom" width="40%">
                                                                                <asp:RadioButton ID="rbCampaignNumber" Runat="server" AutoPostBack="false" 
                                                                                    Checked="False" 
                                                                                    onclick="toggleAllCampainNoSelection(false); document.all.item('moCampaignNumberLabel').style.color = '';" 
                                                                                    Text="SELECT_ALL_CAMPAIGN_NUMBERS" TextAlign="left" />
                                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                                            <td nowrap valign="baseline" width="60%">
                                                                                <asp:Label ID="moCampaignNumberLabel" runat="server">OR A SINGLE CAMPAIGN NUMBER</asp:Label>
                                                                                :
                                                                                <asp:DropDownList ID="cboCampaignNumber" runat="server" AutoPostBack="false" 
                                                                                    onchange="toggleAllCampainNoSelection(true); document.all.item('moCampaignNumberLabel').style.color = '';" 
                                                                                    Width="212px">
                                                                                </asp:DropDownList>
                                                                            </td>
                                                                        </tr>
                                                                    </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3" style="HEIGHT: 27px">
                                                                <hr style="WIDTH: 100%; HEIGHT: 1px">
                                                                </hr>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                <asp:RadioButton ID="RadiobuttonDateAdded" runat="server" AutoPostBack="false" Checked="True"
                                                                    GroupName="gn" onclick="toggleAddedSoldSelection(false);" Text="DATE_ADDED" TextAlign="left" />
                                                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                            <td align="left" colspan="2">
                                                                <asp:RadioButton ID="RadiobuttonSold" runat="server" AutoPostBack="false" GroupName="gn"
                                                                    onclick="toggleAddedSoldSelection(true);" Text="OR DATE ESC SOLD/CANCELLED" TextAlign="left"
                                                                    Width="211px" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3" style="HEIGHT: 27px">
                                                                <hr style="WIDTH: 100%; HEIGHT: 1px">
                                                                </hr>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right"  nowrap >
                                                                *
                                                                <asp:RadioButton ID="RadiobuttonTotalsOnly" Runat="server" AutoPostBack="false" 
                                                                    onclick="toggleDetailSelection(false);" Text="SHOW TOTALS ONLY" 
                                                                    TextAlign="left" />
                                                                &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                                                            <td align="left" nowrap colspan="2">
                                                                &nbsp;
                                                                <asp:RadioButton ID="RadiobuttonDetail" Runat="server" AutoPostBack="false" 
                                                                    onclick="toggleDetailSelection(true);" Text="OR SHOW DETAIL WITH TOTALS" 
                                                                    TextAlign="left" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <hr style="WIDTH: 100%; HEIGHT: 1px" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" colspan="3" nowrap width="80%">
                                                                <asp:CheckBox ID="chkTotalsPageByCov" Runat="server" AutoPostBack="false" 
                                                                    Text="SHOW_TOTALS_PAGE_BY_COVERAGE" TextAlign="Left" />
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD>
										<HR style="WIDTH: 100%; HEIGHT: 1px"/>
									</TD>
								</TR>
								<TR>
									<TD align="left">
										<asp:button id="btnGenRpt" style="BACKGROUND-IMAGE: url(../Navigation/images/viewIcon2.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
											runat="server" Font-Bold="false" CssClass="FLATBUTTON" Width="100px" Text="View" height="20px"></asp:button></TD>
								</TR>
							</TABLE>
						</asp:panel></td>
				</tr>
			</TABLE>
		</form>
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/ReportCeMainScripts.js" type="text/javascript"></SCRIPT>
        <SCRIPT language='JavaScript' src="../Navigation/Scripts/AvailableSelected.js" type="text/javascript"></SCRIPT>

        <script language="javascript" type="text/javascript">
            function TogglelDropDownsSelectionsForNewCertForm(source) {

                if (source == "rdealer") {
                    document.getElementById("multipleDropControl_moMultipleColumnDrop").selectedIndex = 0;
                    document.getElementById("multipleDropControl_moMultipleColumnDropDesc").selectedIndex = 0;
                    document.getElementById("moDealerGroupList").selectedIndex = 0;   // "Dealers Group" DropDown control
                }
                else if (source == "dealerGroup") {
                    document.getElementById("multipleDropControl_moMultipleColumnDrop").selectedIndex = 0;
                    document.getElementById("multipleDropControl_moMultipleColumnDropDesc").selectedIndex = 0;
                    document.getElementById("rdealer").checked = false;   // "Dealers Group" DropDown control

                }
                RemoveAllSelectedDealersForReports("UserControlAvailableSelectedDealers");
            }

    </script>
	</body>
</HTML>
