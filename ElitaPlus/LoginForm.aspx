<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LoginForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.LoginForm" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>Login - Elita+ System</title>
    <asp:Literal ID="Literal1" runat="server"></asp:Literal>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="Styles.css" type="text/css" rel="STYLESHEET">
    <script language="JavaScript" src="./Navigation/scripts/GlobalHeader.js"></script>
    <script language="javascript">
        function doHandleOnLoad() {
            window.focus();
            document.forms[0].TextBoxUserId.focus();
        }

        function SwapMyImage(objectid, NuImage) {
            document.all[objectid].src = NuImage
        }

    </script>
</head>
<body oncontextmenu="return false" bgcolor="#ffffff" leftmargin="0" topmargin="0" onload="doHandleOnLoad();"
    rightmargin="0">
    <form id="Form2" method="post" runat="server">
        <table cellspacing="0" cellpadding="0" width="100%" border="0">
            <tr>
                <td nowrap valign="top" bgcolor="#d5d7e4" width="100%" background="Navigation/images/login_spacer.gif"
                    height="92">
                    <asp:Label ID="lblApplicationInstanceName" Style="left: 100px; position: absolute; top: 20px"
                        runat="server" ForeColor="#63648d">(Development Environment)</asp:Label>
                    <img height="92" src="Navigation/images/trans_spacer.gif" width="100%"></td>
                <td nowrap valign="top" align="right">
                    <img height="92" src="Navigation/images/login_header.gif"></td>
            </tr>
            <tr>
                <td nowrap width="100%" bgcolor="#666666" colspan="2" height="1"></td>
            </tr>
            <tr>
                <td valign="top" colspan="2" align="center">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <!--<tr>
                                <td valign="top" colSpan="2">&nbsp;</td>
                            </tr>-->
                        <tr>
                            <td nowrap background="Navigation/images/login_back.gif" colspan="2" valign="top" align="center"
                                height="290">
                                <br>
                                <br>
                                <br>
                                <br>
                                <table id="tblMain" style="border-right: #999999 1px solid; border-top: #999999 1px solid; border-left: #999999 1px solid; border-bottom: #999999 1px solid"
                                    cellspacing="0" cellpadding="4" rules="cols" width="400" align="center" bgcolor="#f1f1f1"
                                    border="1">
                                    <tr>
                                        <td>
                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                <tr>
                                                    <td>
                                                        <asp:Label ID="lblLogin" runat="server">Development Environment Login</asp:Label></td>
                                                    <td align="right">&nbsp;</td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center">
                                            <table cellspacing="2" cellpadding="2" width="50%" border="0">
                                                <tr>
                                                    <td align="right" nowrap>
                                                        <asp:Label ID="lblUserName" runat="server">User Id:&nbsp;&nbsp;</asp:Label></td>
                                                    <td>
                                                        <asp:TextBox ID="TextBoxUserId" runat="server" Width="165px"></asp:TextBox></td>
                                                </tr>
                                                <!--<tr>
                                                        <td align="right"><asp:label id="lblPassword" runat="server">Password:&nbsp;&nbsp;</asp:label></td>
                                                        <td><asp:textbox id="txtPassword" runat="server" Width="165px" TextMode="Password"></asp:textbox></td>
                                                    </tr>-->
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td align="center">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;<asp:ImageButton ID="ButtonOk" runat="server" Style="cursor: hand;" ImageUrl="Navigation/images/loginBtnUp.gif"></asp:ImageButton></td>
                                                </tr>
                                                <tr>
                                                    <td>&nbsp;</td>
                                                    <td>
                                                        <asp:Label ID="lblMessage" runat="server" ForeColor="red"></asp:Label></td>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                </table>
                                <br>
                            </td>
                        </tr>
                        <!--<tr>
                                <td COLSPAN="2">
                                    <div align="center">
                                        <p>&nbsp;</p>
                                        <p>Forgot your password?<br>
                                            <a href="/forgotPassword.asp"><font color="#0055aa">Click here</font></a> to 
                                            have your password emailed to you.
                                        </p>
                                    </div>
                                </td>
                            </tr>-->
                    </table>
                </td>
            </tr>
            <tr>
                <td nowrap width="100%" bgcolor="#666666" colspan="2" height="1"></td>
            </tr>
            <tr>
                <td colspan="2" height="100%" background="Navigation/images/login_spacer.gif">
                    <img src="Navigation/images/trans_spacer.gif" height="100%"></td>
            </tr>
            <tr>
                <td align="right" valign="bottom" colspan="2">
                    <img src="Navigation/images/trans_spacer.gif" width="1">
                    <font color="gray"><%="&copy;" & DateTime.Now.Year %> Assurant. All rights reserved.</font>
                </td>
            </tr>
        </table>
        <br>
        <br>
    </form>
</body>
</html>