<%@ Page Language="vb" AutoEventWireup="false" Codebehind="ServiceCentersReportForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.ServiceCentersReportForm"%>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>Service Centers Report</title>
		<meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
		<meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
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
								<TD height="20"><asp:label id="LabelReports" runat="server" cssclass="TITLELABEL">Reports</asp:label>:
								<asp:label id="Label7" runat="server"  cssclass="TITLELABELTEXT">SERVICE_CENTERS</asp:label></TD>
								<TD align="right" height="20">*&nbsp;
									<asp:label id="moIndicatesLabel" runat="server" Font-Bold="false" EnableViewState="true">INDICATES_REQUIRED_FIELDS</asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
			<TABLE id="tblOuter2" style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				height="93%" cellSpacing="0" cellPadding="0" rules="none" width="98%" bgColor="#d5d6e4"
				border="0"> <!--ededd5-->
				<tr>
					<td>&nbsp;</td>
				</tr>
				<tr>
					<td vAlign="top" align="center" height="100%"><asp:panel id="WorkingPanel" runat="server">
      <TABLE id=tblMain1 
      style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; BORDER-BOTTOM: #999999 1px solid" 
      cellSpacing=0 cellPadding=6 rules=cols width="98%" align=center 
      bgColor=#fef9ea border=0>
        <TR>
          <TD height=1></TD></TR>
        <TR>
          <TD>
            <TABLE cellSpacing=0 cellPadding=0 width="100%" border=0>
              <TR>
                <TD colSpan=3>
                  <TABLE id=tblSearch 
                  style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; WIDTH: 628px; BORDER-BOTTOM: #999999 1px solid; HEIGHT: 64px" 
                  cellSpacing=2 cellPadding=8 rules=cols width=628 align=center 
                  bgColor=#fef9ea border=0>
                    <TR>
                      <TD>
                        <TABLE cellSpacing=0 cellPadding=0 width="100%" 
border=0>
                          <TR>
                            <TD align=center colSpan=3>
<uc1:ErrorController id=ErrorCtrl runat="server"></uc1:ErrorController></TD></TR></TABLE>
<uc1:ReportCeInputControl id=moReportCeInputControl runat="server"></uc1:ReportCeInputControl></TD></TR></TABLE></TD></TR>
              <TR>
                <TD colSpan=3><IMG height=15 
                  src="../Navigation/images/trans_spacer.gif"></TD></TR>
              <TR>
                <TD align=center colSpan=3>
                  <TABLE cellSpacing=2 cellPadding=0 width="75%" border=0>
                    <TR id=ddRowHide runat="server">
                      <TD noWrap align=right width="25%">*
<asp:label id=moCountryLabel runat="server">PLEASE_SELECT_A_SINGLE_COUNTRY:</asp:label>&nbsp;</TD>
                      <TD noWrap align=left width="75%" colSpan=2>
<asp:dropdownlist id=cboCountry runat="server" width="184px" AutoPostBack="True"></asp:dropdownlist></TD></TR>
                    <TR>
                      <TD align=center width="100%" colSpan=3><!-- <HR style="WIDTH: 100%; HEIGHT: 1px">--></TD></TR>
                    <TR>
                      <TD vAlign=bottom noWrap align=right width="28%" 
                        colSpan=2>&nbsp;* 
<asp:RadioButton id=rServiceCenters onclick="ToggleDualDropDownsSelection('multipleDropControl_moMultipleColumnDrop', 'multipleDropControl_moMultipleColumnDropDesc', 'rServiceCenters', false, ''); document.all.item('multipleDropControl_lb_DropDown').style.color = '';" AutoPostBack="false" Text="ALL_SERVICE_CENTERS" TextAlign="left" Runat="server" Checked="True"></asp:RadioButton></TD>
                      <TD noWrap align=left width="72%"></TD></TR>
                    <TR>
                      <TD align=left colSpan=3>
                        <TABLE id=Table1 cellSpacing=1 cellPadding=1 
                        width="100%" border=0>
                          <TR><!--<TD width="3%"><IMG style="HEIGHT: 15px" height=15 
                              src="../Navigation/images/trans_spacer.gif" 
                              width=20></TD>-->
                            <TD width="100%" colSpan=3>
<uc1:MultipleColumnDDLabelControl id=multipleDropControl runat="server"></uc1:MultipleColumnDDLabelControl></TD></TR></TABLE></TD></TR>
                    <TR>
                      <TD align=left 
              colSpan=3></TD></TR></TABLE></TD></TR></TABLE></TD></TR>
        <TR>
          <TD style="HEIGHT: 11px"></TD></TR>
        <TR>
          <TD>
            <HR>
          </TD></TR>
        <TR>
          <TD align=left>&nbsp; 
<asp:button id=btnGenRpt style="BACKGROUND-IMAGE: url(../Navigation/images/viewIcon2.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat" runat="server" Font-Bold="false" Text="View" CssClass="FLATBUTTON" height="20px" Width="100px"></asp:button></TD></TR></TABLE>
						</asp:panel></td>
				</tr>
			</TABLE>
		</form>
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/ReportCeMainScripts.js"></SCRIPT>
		<script>
			function toggleAllCountriesSelection(isSingleCountry)
			{
				//debugger;
				if(isSingleCountry)
				{
					document.forms[0].rCountry.checked = false;
				}
				else
				{
					document.forms[0].cboCountry.selectedIndex = -1;
				}
			}
		</script>
	</body>
</HTML>
