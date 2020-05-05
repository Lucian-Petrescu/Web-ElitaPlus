<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UserControlServiceCenterInfo" Src="UserControlServiceCenterInfo.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="LocateServiceCenterDetailForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.LocateServiceCenterDetailForm"%>
<%@ Register TagPrefix="uc1" TagName="UserControlCertificateInfo" Src="UserControlCertificateInfo.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<HTML>
  <HEAD>
		<title>TemplateForm</title> <!-- ************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BOEditingWebForm.cst (10/7/2004)  ******************** -->
		<meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
		<meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
		<meta content="JavaScript" name="vs_defaultClientScript">
		<meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
		<LINK href="../Styles.css" type="text/css" rel="STYLESHEET">
        <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.12.4.min.js" > </script>
		<SCRIPT language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></SCRIPT>
        <SCRIPT type="text/javascript" language="JavaScript">
            $(document).ready(function () {
                $('#UserControlCertificateInfo1_TextboxCustomerName').parent().css('text-align', 'center');
                $('#UserControlCertificateInfo1_LabelCustomerName').parent().css('text-align', 'right');
            });
        </SCRIPT>

        <style type="text/css">
          hr {
            border: #999999 0.5px solid; 
            margin-bottom:4px;           
          }
          
          .certificateInfo {
             width: 35%; 
             height: 6px;
          }

           .certificateInfo hr {           
            height: 0.2px !important;
          }

           .customerName
            {
               text-align:center;
            }
         </style>
</HEAD>
	<body leftMargin="0" topMargin="0" onload="changeScrollbarColor();" onresize="resizeForm(document.getElementById('scroller'));"
		MS_POSITIONING="GridLayout" border="0">
		<form id="Form1" method="post" runat="server">
			<!--Start Header-->
			<TABLE style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE width="100%" border="0">
							<TR>
								<TD height="20">
									<P>&nbsp;
										<asp:label id="Label40" runat="server" CssClass="TITLELABELTEXT">Service_Center</asp:label></P>
								</TD>
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
					<td vAlign="top" align="center"><asp:panel id="WorkingPanel" runat="server" Width="98%" Height="98%">
      <TABLE id=tblMain1 
      style="BORDER-RIGHT: #999999 1px solid; BORDER-TOP: #999999 1px solid; BORDER-LEFT: #999999 1px solid; WIDTH: 100%; BORDER-BOTTOM: #999999 1px solid; HEIGHT: 100%" 
      cellSpacing=0 cellPadding=0 rules=cols width="100%" align=center 
      bgColor=#fef9ea border=0>
        <TR>
          <TD style="HEIGHT: 4px" vAlign=top align=center colSpan=4>
<uc1:ErrorController id=ErrorCtrl runat="server"></uc1:ErrorController>&nbsp;</TD></TR>
        <TR>
          <TD style="HEIGHT: 4px" vAlign=top align=center 
        colSpan=4>&nbsp;</TD></TR>
        <TR>
          <TD vAlign=top align=center>
            <DIV id=scroller style="WIDTH: 98%; HEIGHT: 365px" 
            align=center>
<asp:Panel id=EditPanel_WRITE runat="server" Height="80%" Width="98%">
            <TABLE id=Table1 style="WIDTH: 100%" cellSpacing=1 cellPadding=0 
            width="100%" border=0>
              <TR>
                <TD class="certificateInfo" vAlign=middle align=center 
                colSpan=4 >
<uc1:UserControlCertificateInfo id=UserControlCertificateInfo1 runat="server"></uc1:UserControlCertificateInfo></TD></TR>
              <TR>
                <TD>
                  <HR />               
              <TR>
                <TD class="certificateInfo" vAlign=middle align=center 
                colSpan=4>
<uc1:UserControlServiceCenterInfo id=UserControlServiceCenterInfo runat="server"></uc1:UserControlServiceCenterInfo></TD></TR>
              <TR>
                <TD style="WIDTH: 35%; HEIGHT: 6px" vAlign=middle align=center 
                colSpan=4></TD></TR>
              <TR>
                <TD style="WIDTH: 35%; HEIGHT: 11px" vAlign=middle align=center 
                colSpan=4>
<asp:Label id=LabelLoanerCenterHeader runat="server" >LOANER_CENTER</asp:Label></TD></TR>
              <TR>
                <TD style="WIDTH: 35%; HEIGHT: 6px" vAlign=middle align=center 
                colSpan=4>
<uc1:UserControlServiceCenterInfo id=UserControlLoanerCenterInfo runat="server"></uc1:UserControlServiceCenterInfo></TD></TR></TABLE></asp:Panel></DIV></TD></TR>
        <TR>
          <TD>
            <HR />
          </TD></TR>
        <TR>
          <TD vAlign=top noWrap align=left height=20>&nbsp; 
<asp:button id=btnBack style="BACKGROUND-IMAGE: url(../Navigation/images/icons/back_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat" tabIndex=185 runat="server" Font-Bold="false" Width="90px" height="20px" Text="Back" CssClass="FLATBUTTON"></asp:button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp; 
<asp:button id=btnAccept_Write style="BACKGROUND-IMAGE: url(../Navigation/images/icons/clear_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat" tabIndex=210 runat="server" Font-Bold="false" Width="190px" height="20px" Text="ACCEPT_SERVICE_CENTER" CssClass="FLATBUTTON"></asp:button></TD></TR>
        <TR>
                                <TD height=5></TD></TR></TABLE><INPUT 
                                                                   id=HiddenSaveChangesPromptResponse type=hidden 
                                                                   name=HiddenSaveChangesPromptResponse runat="server" 
                                                                   DESIGNTIMEDRAGDROP="261" />
						</asp:panel></td>
				</tr>
			</TABLE>
		</form>
		<script>
			resizeForm(document.getElementById("scroller"));	
		</script>
	</body>
</HTML>
