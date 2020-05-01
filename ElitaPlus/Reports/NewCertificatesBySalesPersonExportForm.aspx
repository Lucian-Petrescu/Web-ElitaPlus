<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="NewCertificatesBySalesPersonExportForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.NewCertificatesBySalesPersonExportForm" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ReportExtractInputControl" Src="ReportExtractInputControl.ascx" %>

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
			<asp:ScriptManager ID="ScriptManager1" runat="server" />
			<input id="rptTitle" type="hidden" name="rptTitle"> 
			<input id="rptSrc" type="hidden" name="rptSrc">
			<TABLE style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE width="100%" border="0">
							<TR>
								<TD height="20">
									<asp:label id="LabelReports" runat="server" CssClass="TITLELABEL">Reports</asp:label>:
									<asp:label id="Label7" runat="server" CssClass="TITLELABELTEXT">New Certificates BY SALES REP</asp:label></TD>
								<TD height="20" align="right">*&nbsp;
									<asp:label id="moIndicatesLabel" runat="server" Font-Bold="false" EnableViewState="true">INDICATES_REQUIRED_FIELDS</asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="tblOuter2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid;"
				cellSpacing="0" cellPadding="0" rules="none" width="98%" bgColor="#d5d6e4" border="0">
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
															<uc1:ReportExtractInputControl ID="moReportExtractInputControl" runat="server"></uc1:ReportExtractInputControl>
														</TR>
													</TABLE>
												</TD>
											</TR>
											<TR>
												<TD colSpan="3"><IMG height="15" src="../Navigation/images/trans_spacer.gif"></TD>
											</TR>
											<TR>
											    <td  colspan="3" style="WIDTH: 99.43%; HEIGHT: 1px"></td>
											</TR>
                                            <tr>
                                                <td align="center" colspan="3">
                                                    <table border="0" cellpadding="0" cellspacing="2" width="95%">
                                                        <tr>
                                                            <td align="center" colspan="3" rowspan="1" style="HEIGHT:10px" valign="middle" 
                                                                width="50%">
                                                                <table border="0" cellpadding="0" cellspacing="0" 
                                                                    style="WIDTH: 484px; HEIGHT: 18px" width="484">
                                                                    <tr>
                                                                        <td align="right" style="white-space:nowrap" valign="middle">
                                                                            *<asp:Label ID="moBeginDateLabel" runat="server" Font-Bold="false">BEGIN_DATE</asp:Label>:
                                                                        </td>
                                                                        <td style="white-space:nowrap; text-align:left">
                                                                            &nbsp;
                                                                            <asp:TextBox ID="moBeginDateText" runat="server" CssClass="FLATTEXTBOX" width="95px" onchange="enableReport()"></asp:TextBox>
                                                                            <asp:ImageButton ID="BtnBeginDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg" Width="20px" />
                                                                        </td>
                                                                        <td align="right" style="white-space:nowrap" valign="middle">
                                                                            *<asp:Label ID="moEndDateLabel" runat="server" Font-Bold="false">END_DATE</asp:Label>:
                                                                        </td>
                                                                        <td style="white-space:nowrap;text-align:left">
                                                                            &nbsp;<asp:TextBox ID="moEndDateText" runat="server" CssClass="FLATTEXTBOX" width="95px" onchange="enableReport()"></asp:TextBox>
                                                                            <asp:ImageButton ID="BtnEndDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg" Width="20px" />
                                                                        </td>                                                                        
                                                                    </tr>
                                                                    <tr id="CompTRsprt">
                                                                        <td colspan="4" style="padding-bottom:10px; padding-top:10px">
                                                                            <hr style="WIDTH: 99.43%; HEIGHT: 1px"  />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>&nbsp;</td>
                                                                        <td colspan="3">
                                                                            <asp:UpdatePanel ID="UpdatePanel2" runat="server">
                                                                                <ContentTemplate>
                                                                                    <uc1:MultipleColumnDDLabelControl ID="UserCompanyMultiDrop" runat="server" />
                                                                                </ContentTemplate>
                                                                            </asp:UpdatePanel>
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td colspan="4" style="padding-bottom:10px; padding-top:10px">
                                                                            <hr style="WIDTH: 99.43%; HEIGHT: 1px"  />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        
                                                                        <td style="white-space:nowrap; vertical-align:bottom; text-align:right">
                                                                            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Conditional">
                                                                                <ContentTemplate>
                                                                                    <asp:RadioButton id="rdealer" onclick="ToggleDualDropDownsSelection('multipleDropControl_moMultipleColumnDrop', 'multipleDropControl_moMultipleColumnDropDesc', 'rdealer', false, ''); document.all.item('multipleDropControl_lb_DropDown').style.color = '';"
																				     Text="SELECT_ALL_DEALERS" AutoPostBack="false" Checked="False" Runat="server" TextAlign="left"></asp:RadioButton>&nbsp;
																		        </ContentTemplate>
																		        <Triggers>
                                                                                    <asj:AsyncPostBackTrigger ControlID="UserCompanyMultiDrop" />                                                                                    
                                                                                </Triggers>
                                                                            </asp:UpdatePanel>
                                                                        </td>
                                                                        <td colspan="3" style="white-space:nowrap; padding-left:10px;text-align:left">
                                                                            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                                                                                <ContentTemplate>
                                                                                    <uc1:MultipleColumnDDLabelControl id="multipleDropControl" runat="server" />
                                                                                </ContentTemplate>
                                                                            <Triggers>
                                                                                <asj:AsyncPostBackTrigger ControlID="UserCompanyMultiDrop" />                                                                                    
                                                                            </Triggers>
                                                                        </asp:UpdatePanel>
                                                                        </td>
                                                                        
                                                                    </tr>                                                                   
                                                                    <tr>
                                                                        <td colspan="4" style="padding-bottom:10px; padding-top:10px">
                                                                            <hr style="WIDTH: 99.43%; HEIGHT: 1px" />
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td style="white-space:nowrap; vertical-align:text-bottom; text-align:right;vertical-align:text-bottom;">
                                                                            <asp:RadioButton ID="RadiobuttonDateAdded" runat="server" AutoPostBack="false" Checked="True"
                                                                                GroupName="gn" onclick="toggleAddedSoldSelection(false);" Text="DATE_ADDED" TextAlign="left" />
                                                                        </td>
                                                                        <td colspan="3" style="white-space:nowrap; padding-left:10px; text-align:left;vertical-align:text-bottom;">
                                                                            <asp:RadioButton ID="RadiobuttonSold" runat="server" AutoPostBack="false" GroupName="gn"
                                                                                onclick="toggleAddedSoldSelection(true);" Text="OR DATE ESC SOLD/CANCELLED" TextAlign="left" Width="211px" />
                                                                        </td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>                                                                                                                
                                                        <tr>
                                                            <td colspan="3" style="HEIGHT: 27px">
                                                                <hr style="WIDTH: 99.43%; HEIGHT: 1px">
                                                                </hr>
                                                            </td>
                                                        </tr>                                                                                                                
                                                    </table>
                                                </td>
                                            </tr>
										</TABLE>
									</TD>
								</TR>
								<TR>
									<TD style="HEIGHT: 20px"></TD>
								</TR>
								<TR>
									<TD>
										<HR>
									</TD>
								</TR>
								<TR>
									<TD style="text-align:left; padding-bottom:10px">
									<asp:Button ID="btnGenRpt" runat="server" CssClass="FLATBUTTON" height="20px" style="BACKGROUND-IMAGE: url(../Navigation/images/viewIcon2.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat" Text="Generate Report Request" Width="200px" />
								</TR>
							</TABLE>
						</asp:panel>
					</td>					
				</tr>
			</TABLE>
		</form>
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/ReportCeMainScripts.js"></SCRIPT>
		<script>
	        var newHeight = document.documentElement.clientHeight - 80;
	        document.getElementById("tblOuter2").style.height = String(newHeight) + "px";
        </script>
	<script type="text/javascript">

            
	    $(document).ready(function(){
	        $("form > *").change(function() {
	            enableReport();
	        });
	    });


	    function enableReport() {
	        //debugger
	        var btnGenReport =  document.getElementById("<%=btnGenRpt.ClientID%>");
	        btnGenReport.disabled = false;
	    }

           
	</script>
	</body>
</HTML>
