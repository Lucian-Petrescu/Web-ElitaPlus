<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DownloadReportData.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.DownloadReportData" %>
<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
            <TABLE style="BORDER-RIGHT: black 1px solid; BORDER-TOP: black 1px solid; MARGIN: 5px; BORDER-LEFT: black 1px solid; BORDER-BOTTOM: black 1px solid"
				cellSpacing="0" cellPadding="0" width="98%" bgColor="#d5d6e4" border="0">
				<TR>
					<TD vAlign="top">
						<TABLE width="100%" border="0">
							<TR>
								<TD height="20"><asp:Label ID="Label1"  CssClass="TITLELABEL" runat="server">Export Report Data</asp:label>:&nbsp;</asp:label></TD>
							</TR>
						</TABLE>
					</TD>
				</TR>
			</TABLE>
            
        &nbsp;
        <div><asp:Label ID="statusLabel" runat="server"></asp:Label></div>
        <uc1:ErrorController id="ErrorCtrl" runat="server"></uc1:ErrorController>
    </div>
    </form>
    <script language="JavaScript" src="../Navigation/Scripts/ReportCeMainScripts.js"></script>
</body>
</html>
