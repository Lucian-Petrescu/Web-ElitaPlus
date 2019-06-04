<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" Codebehind="DealerLinxTestForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.DealerLinxTestForm" ValidateRequest="false" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>DealerLinxTestForm</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
</head>
<body ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <table id="Table1" style="z-index: 104; left: 8px; position: absolute; top: 8px"
            cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td nowrap align="center" width="25%" colspan="4">
                    <uc1:ErrorController ID="ErrorCtrl" runat="server"></uc1:ErrorController>
                </td>
            </tr>
            <tr>
                <td style="height: 77px" colspan="4">
                    <table id="Table2" cellspacing="0" cellpadding="0" width="70%" border="0">
                        <tr valign="bottom" height="30">
                            <td width="10%">
                                Make:</td>
                            <td width="10%">
                                Model:</td>
                            <td width="10%">
                                Engine/Version:</td>
                            <td width="10%">
                                Year:</td>
                        </tr>
                        <tr valign="middle">
                            <td style="height: 23px" width="10%">
                                <asp:TextBox ID="txtMake" TabIndex="5" Width="128px" runat="server"></asp:TextBox></td>
                            <td width="10%">
                                <asp:TextBox ID="txtModel" TabIndex="6" Width="128px" runat="server"></asp:TextBox></td>
                            <td width="10%">
                                <asp:TextBox ID="txtEV" TabIndex="7" Width="128px" runat="server"></asp:TextBox></td>
                            <td width="10%">
                                <asp:TextBox ID="txtYear" TabIndex="8" Width="128px" runat="server"></asp:TextBox></td>
                        </tr>
                        <tr valign="bottom" height="30">
                            <td width="10%">
                                VIN:</td>
                            <td width="10%">
                                KM/MI:</td>
                            <td width="10%">
                            </td>
                            <td width="10%">
                                In Service Date:</td>
                        </tr>
                        <tr valign="middle">
                            <td width="10%">
                                <asp:TextBox ID="txtVIN" TabIndex="9" Width="128px" runat="server"></asp:TextBox></td>
                            <td width="10%">
                                <asp:TextBox ID="txtKM" TabIndex="10" Width="128px" runat="server"></asp:TextBox></td>
                            <td width="15%">
                                New/Used:&nbsp;&nbsp;
                                <asp:RadioButton ID="rdbNew" TabIndex="11" Text="New" runat="server" GroupName="NewUsed"
                                    Checked="True"></asp:RadioButton>&nbsp;
                                <asp:RadioButton ID="rdbUsed" TabIndex="12" Text="Used" runat="server" GroupName="NewUsed">
                                </asp:RadioButton></td>
                            <td width="10%">
                                <asp:TextBox ID="txtISD" TabIndex="8" Width="128px" runat="server"></asp:TextBox>
                                <asp:ImageButton ID="ImageButtonISDate" TabIndex="2" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                                </asp:ImageButton></td>
                        </tr>
                        <tr valign="bottom" height="30">
                            <td nowrap width="10%">
                                Dealer Code:</td>
                            <td width="10%">
                                Vehicle Tag:</td>
                            <td valign="middle" align="center" colspan="2" rowspan="2">
                                <asp:Button ID="btnGetQuote" TabIndex="2" runat="server" Text="Get Quote"></asp:Button>&nbsp;&nbsp;
                                <asp:Button ID="btnEnroll" TabIndex="5" runat="server" Text="Enroll" Enabled="False">
                                </asp:Button>&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;</td>
                        </tr>
                        <tr valign="middle">
                            <td width="10%">
                                <asp:TextBox ID="txtDealerCode" TabIndex="14" Width="128px" runat="server"></asp:TextBox></td>
                            <td width="10%">
                                <asp:TextBox ID="txtTag" TabIndex="15" Width="128px" runat="server"></asp:TextBox></td>
                        </tr>
                    </table>
                </td>
            </tr>
            <tr>
                <td style="height: 77px">
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Panel ID="Panel1" runat="server" Width="100%" Height="100%">
                        <div id="scroller1" style="overflow: auto; width: 99.53%; height: 100%" align="left"
                            runat="server">
                        </div>
                        <!-- Tab end -->
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </form>
    <div>
    </div>
</body>
</html>
