<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ReportCeInputControl" Src="ReportCeInputControl.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="InvoicesToBePaidFreeZoneForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Reports.InvoicesToBePaidFreeZoneForm" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title id="rptWindowTitle" runat="server">INVOICES TO BE PAID INCLUDING FREE ZONE</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Styles.css" type="text/css" rel="STYLESHEET">


    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>
    <script>
        function DisplayPayeeRow() {
            document.getElementById("PayeeRow").style.display = 'block';
        }

        function toggleOptionSelection(isByReportingPeriod) {
            //debugger;
            if (isByReportingPeriod) {
                document.getElementById("RadiobuttonByInvoiceNumber").checked = false;
                document.getElementById("InvoiceRow").style.display = 'none';
                document.getElementById("PayeeRow").style.display = 'none';
                document.getElementById("DateRow").style.display = 'block';
            }
            else {
                document.getElementById("RadiobuttonByReportingPeriod").checked = false;
                document.getElementById("InvoiceNumberTextbox").innerText = "";
                document.getElementById("InvoiceRow").style.display = 'block';
                document.getElementById("DateRow").style.display = 'none';
                document.getElementById("PayeeRow").style.display = 'none';
            }
        }
    </script>
</head>
<body leftmargin="0" topmargin="0" onload="changeScrollbarColor();" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <!--Start Header-->
        <input id="rptTitle" type="hidden" name="rptTitle">
        <input id="rptSrc" type="hidden" name="rptSrc">
        <table style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid"
            cellspacing="0" cellpadding="0" width="98%" bgcolor="#d5d6e4" border="0">
            <tr>
                <td valign="top">
                    <table width="100%" border="0">
                        <tr>
                            <td height="20">
                                <asp:Label ID="LabelReports" CssClass="TITLELABEL" runat="server">Reports</asp:Label>:&nbsp;<asp:Label ID="Label7" runat="server" CssClass="TITLELABELTEXT">INVOICES_TO_BE_PAID_INCLUDING_FREE_ZONE</asp:Label></td>
                            <td height="20" align="right">*&nbsp;
                                <asp:Label ID="moIndicatesLabel" runat="server" EnableViewState="true">INDICATES_REQUIRED_FIELDS</asp:Label></td>
                        </tr>
                    </table>
            </tr>
        </table>
      
			<table id="tblOuter2" style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid"
                height="93%" cellspacing="0" cellpadding="0" rules="none" width="98%" bgcolor="#d5d6e4"
                border="0">
                <!--ededd5-->
                <tr>
                    <td>&nbsp;</td>
                </tr>
                <tr>
                    <td valign="top" align="center" height="100%">
                        <asp:Panel ID="WorkingPanel" runat="server">
                            <table id="tblMain1" style="border-right: #999999 1px solid; border-top: #999999 1px solid; border-left: #999999 1px solid; border-bottom: #999999 1px solid"
                                cellspacing="0" cellpadding="6" rules="cols" width="98%" height="95%" align="center" bgcolor="#fef9ea"
                                border="0">
                                <tr>
                                    <td height="1"></td>
                                </tr>
                                <tr>
                                    <td>
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td colspan="3">
                                                    <table id="tblSearch" style="border-right: #999999 1px solid; border-top: #999999 1px solid; border-left: #999999 1px solid; width: 100%; border-bottom: #999999 1px solid; height: 64px"
                                                        cellspacing="2" cellpadding="8" rules="cols" width="100%" align="center" bgcolor="#fef9ea"
                                                        border="0">
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
                                                <td colspan="3">
                                                    <img height="15" src="../Navigation/images/trans_spacer.gif"></td>
                                            </tr>
                                            <tr>
                                                <td align="center" colspan="3">
                                                    <table cellspacing="2" cellpadding="0" width="75%" border="0">
                                                        <tr>
                                                            <td nowrap align="right">
                                                                <asp:RadioButton ID="RadiobuttonByReportingPeriod" onclick="toggleOptionSelection(true);" TextAlign="left"
                                                                    Text="Base Report On Reporting Period" runat="server" AutoPostBack="false"></asp:RadioButton></td>
                                                            <td nowrap align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
																<asp:RadioButton ID="RadiobuttonByInvoiceNumber" onclick="toggleOptionSelection(false);" TextAlign="left"
                                                                    Text="Base Report On An Invoice Number" runat="server" AutoPostBack="false"></asp:RadioButton></td>
                                                            <td></td>

                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td align="center" width="50%" colspan="3">
                                                                <table cellspacing="0" cellpadding="0" width="75%" border="0">
                                                                    <tr id="DateRow" <%=toggleDisplay("period")%>>
                                                                        <td width="50%">&nbsp;</td>
                                                                        <td valign="middle" nowrap align="right">
                                                                            <asp:Label ID="BeginDateLabel" runat="server">BEGIN_DATE</asp:Label>:</td>
                                                                        <td nowrap>&nbsp;
																			<asp:TextBox ID="BeginDateText" TabIndex="1" runat="server" CssClass="FLATTEXTBOX"></asp:TextBox>&nbsp;</td>
                                                                        <td>
                                                                            <asp:ImageButton ID="BtnBeginDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" Height="17px"
                                                                                Width="20px"></asp:ImageButton></td>
                                                                        <td valign="middle" nowrap align="right">&nbsp;&nbsp;
																			<asp:Label ID="EndDateLabel" runat="server">END_DATE</asp:Label>:</td>
                                                                        <td nowrap>&nbsp;
																			<asp:TextBox ID="EndDateText" TabIndex="1" runat="server" CssClass="FLATTEXTBOX" Width="125px"></asp:TextBox>&nbsp;
                                                                        </td>
                                                                        <td>
                                                                            <asp:ImageButton ID="BtnEndDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" Height="17px"
                                                                                Width="20px"></asp:ImageButton></td>
                                                                        <td width="50%">&nbsp;</td>
                                                                    </tr>
                                                                    <tr id="InvoiceRow" <%=toggleDisplay("invoice")%>>
                                                                        <td width="50%">&nbsp;</td>
                                                                        <td valign="middle" nowrap align="right">
                                                                            <asp:Label ID="InvoiceNumberLabel" runat="server">INVOICE_NUMBER</asp:Label>:</td>
                                                                        <td nowrap>&nbsp;
																			<asp:TextBox ID="InvoiceNumberTextbox" TabIndex="1" runat="server" AutoPostBack="True" CssClass="FLATTEXTBOX"
                                                                                onblur="DisplayPayeeRow()"></asp:TextBox>&nbsp;</td>
                                                                        <td width="50%">&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td>&nbsp;</td>
                                                                    </tr>
                                                                    <tr id="PayeeRow" <%=toggleDisplay("payee")%>>
                                                                        <td width="50%">&nbsp;</td>
                                                                        <td valign="middle" nowrap align="right">
                                                                            <asp:Label ID="PayeeLabel" runat="server">Payee</asp:Label>:</td>
                                                                        <td>&nbsp;
																			<asp:DropDownList ID="cboPayee" runat="server" Width="300px"></asp:DropDownList></td>
                                                                        <td width="50%">&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="50%">&nbsp;</td>
                                                                        <td valign="middle" nowrap align="right" colspan="2">
                                                                            <asp:CheckBox ID="chkSvcCode" Text="INCLUDE_SERVICE_CENTER_CODE" AutoPostBack="false"
                                                                                runat="server" TextAlign="Left"></asp:CheckBox>
                                                                        <td width="50%">&nbsp;</td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="50%">&nbsp;</td>
                                                                        <td valign="middle" nowrap align="right" colspan="2">
                                                                            <asp:CheckBox ID="chkCustomerAddress" Text="INCLUDE_CUSTOMER_ADDRESS" AutoPostBack="false"
                                                                                runat="server" TextAlign="Left"></asp:CheckBox>
                                                                        <td width="50%">&nbsp;</td>
                                                                    </tr>

                                                                    <tr>
                                                                        <td nowrap align="right" width="25%" colspan="2">
                                                                            <asp:RadioButton ID="rSvcCtr" Checked="False"
                                                                                TextAlign="left" Text="PLEASE SELECT ALL SERVICE CENTERS" runat="server" AutoPostBack="false"></asp:RadioButton></td>
                                                                        <td nowrap align="left" width="25%">&nbsp;&nbsp;
                                                                        </td>
                                                                        <td nowrap align="left" width="40%">&nbsp;&nbsp;
                                                                        </td>
                                                                    </tr>
                                                                    <tr>
                                                                        <td width="50%">&nbsp;</td>
                                                                        <td valign="middle" nowrap align="right" colspan="2">
                                                                            <asp:CheckBox ID="CheckBoxNoFreeZone" onclick="toggleCheckBox('CheckBoxNoFreeZone','CheckBoxFreeZone');" Text="NO_FREE_ZONE" AutoPostBack="false" Checked="False" TextAlign="left" runat="server"></asp:CheckBox>
                                                                            <asp:CheckBox ID="CheckBoxFreeZone" onclick="toggleCheckBox('CheckBoxFreeZone','CheckBoxNoFreeZone');" Text="FREE_ZONE" AutoPostBack="false" Checked="False" TextAlign="left" runat="server"></asp:CheckBox>
                                                                        </td>
                                                                        <td width="50%">&nbsp;</td>
                                                                    </tr>
                                                                </table>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                            <td>&nbsp;</td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="3">
                                                                <hr>
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 85%; width: 100%;"></td>
                                </tr>
                                <tr>
                                    <td width="100%">
                                        <hr>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">&nbsp;
										<asp:Button ID="btnGenRpt" Style="background-image: url(../Navigation/images/viewIcon2.gif); cursor: hand; background-repeat: no-repeat"
                                            runat="server" Text="View" CssClass="FLATBUTTON" Width="100px" Height="20px"></asp:Button></td>
                                </tr>
                            </table>
                        </asp:Panel>
                    </td>
                </tr>
            </table>
    </form>
    <script language="JavaScript" src="../Navigation/Scripts/ReportCeMainScripts.js"></script>
</body>
</html>
