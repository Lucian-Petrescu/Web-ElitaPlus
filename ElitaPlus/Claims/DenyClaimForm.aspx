<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DenyClaimForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.DenyClaimForm" %>

<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Deny_Claim</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Styles.css" type="text/css" rel="STYLESHEET">
    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>
</head>
<body onresize="resizeForm(document.getElementById('scroller'));" leftmargin="0" topmargin="0"
    onload="changeScrollbarColor();" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <!--Start Header-->
        <table style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid"
            cellspacing="0" cellpadding="0" width="98%" bgcolor="#d5d6e4" border="0">
            <tr>
                <td valign="top">
                    <table width="100%" border="0">
                        <tr>
                            <td height="20">
                                <asp:Label ID="moTitleLabel1" runat="server" CssClass="TITLELABEL">Tables</asp:Label>:
									<asp:Label ID="moTitleLabel2" runat="server" CssClass="TITLELABELTEXT">Deny_Claim</asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table id="tblOuter2" style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid"
            height="93%" cellspacing="0" cellpadding="0" rules="none" width="98%" bgcolor="#d5d6e4"
            border="1">
            <tr>
                <td>&nbsp;</td>
            </tr>
            <tr>
                <td valign="top" align="center">
                    <table id="moOutTable" style="border-right: #999999 1px solid; border-top: #999999 1px solid; border-left: #999999 1px solid; border-bottom: #999999 1px solid"
                        height="98%" cellspacing="0" cellpadding="6" width="98%" align="center" bgcolor="#fef9ea"
                        border="0">
                        <tr>
                            <td valign="top" align="center" width="98%" colspan="1" rowspan="1">
                                <uc1:ErrorController ID="ErrorCtrl" runat="server"></uc1:ErrorController>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td valign="top" align="center" height="100%">
                                <div id="scroller" style="overflow: auto; width: 100%; height: 365px" align="center">
                                    <asp:Panel ID="EditPanel_WRITE" runat="server">
                                        <table id="Table1" style="width: 98%; height: 98%" cellspacing="1" cellpadding="0"
                                            border="0">
                                            <tr>
                                                <td></td>
                                            </tr>
                                            <tr valign="top">
                                                <td align="right" width="30%" height="20">*
                                                    <asp:Label ID="lblDeniedReason" runat="server" Font-Bold="false">Denied_Reason</asp:Label></td>
                                                <td>&nbsp; 
                                                <asp:DropDownList ID="cboDeniedReason" runat="server" Width="280px" AutoPostBack="False">
                                                </asp:DropDownList></td>
                                            </tr>
                                            <tr height="5">
                                                <td colspan="2"></td>
                                            </tr>
                                            <tr valign="top">
                                                <td align="right" height="20">
                                                    <asp:Label ID="lblInvoiceNumber" runat="server">Invoice_Numb</asp:Label></td>
                                                <td>&nbsp;
												<asp:TextBox ID="txtInvoiceNumber" runat="server" Width="280" TabIndex="2"></asp:TextBox>&nbsp;
                                                </td>
                                            </tr>

                                            <tr valign="top">
                                                <td align="right" width="30%" height="20">
                                                    <asp:Label ID="lblPotFraudulent" runat="server" Font-Bold="false">Potentially_Fraudulent</asp:Label></td>
                                                <td>&nbsp; 
                                                <asp:DropDownList ID="cboFraudulent" runat="server" Width="280px" AutoPostBack="False">
                                                </asp:DropDownList></td>
                                            </tr>
                                            <tr height="5">
                                                <td colspan="2"></td>
                                            </tr>
                                            <tr valign="top">
                                                <td align="right" height="20">
                                                    <asp:Label ID="lblComplaint" runat="server">Complaint</asp:Label></td>
                                                <td>&nbsp;
												<asp:DropDownList ID="cboComplaint" runat="server" Width="280px" AutoPostBack="False">
                                                </asp:DropDownList></td>
                                            </tr></table>
                                    </asp:Panel>
                                </div>
                            </td>
                        </tr>
                        <tr valign="top">
                            <td align="left" colspan="2" height="20">
                                <hr style="width: 100%; height: 1px" size="1">
                            </td>
                        </tr>
                        <tr valign="top">
                            <td nowrap align="left">
                                <asp:Button ID="btnCancel" Style="background-image: url(../Navigation/images/icons/back_icon.gif); cursor: hand; background-repeat: no-repeat"
                                    runat="server" Text="BACK" CssClass="FLATBUTTON" Height="20px" CausesValidation="False"
                                    Width="100px"></asp:Button>
                                &nbsp;
							<asp:Button ID="btnApply_WRITE" Style="background-image: url(../Navigation/images/icons/save_trans_icon.gif); cursor: hand; background-repeat: no-repeat"
                                runat="server" Text="SAVE" CssClass="FLATBUTTON" Height="20px" CausesValidation="False"
                                Width="100px"></asp:Button></td>
                            <%--<asp:button id="btnCancel" style="BACKGROUND-IMAGE: url(../Navigation/images/icons/back_icon.gif); CURSOR: hand; BACKGROUND-REPEAT: no-repeat"
									runat="server" Text="BACK" CssClass="FLATBUTTON" height="20px" CausesValidation="False"
									Width="100px"></asp:button></TD>--%>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <script>
            resizeForm(document.getElementById("scroller"));
        </script>
    </form>
</body>
</html>
