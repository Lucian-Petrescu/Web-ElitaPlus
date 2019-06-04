<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="EncrypValues.aspx.vb" Inherits="ElitaEncrypt.EncrypValues"  ValidateRequest="false"%>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>Assurant Solutions - Elita Encryting Tool</title>
    <link rel="stylesheet" href="Default.aspx" type="text/css" title="MainStyles" />
</head>
<body>
    <form id="form1" runat="server">
    <table border=0  cellpadding="0" cellspacing="0" width="100%">

    <!--*** HEADER AREA**-->    
        <tr style="width=100%">
            <td class="logo">
                <asp:Image ID="Image1" ImageUrl="~/images/logo.png"  runat="server" />
            </td>
        </tr>
        <tr style="background-color:#666;height:25px;""  ><td></td></tr>
        <tr><td style="height:2px;"></td></tr>
        <tr><td class="navSeparator"></td></tr>
        <tr><td style="height:20px;"></td></tr>

    <!--*** ENTRY DATA AREA**-->    
        <tr><td>
            <div class="dataContainer">
                <h2 class="searchGridHeader">Elita Security Encrypting Tool</h2>
                <table border="0" class="searchGrid">
                    <tr>
                        <td colspan=2>
                            <table border="0">
                                <tr>
                                    <td nowrap><asp:Label ID="Label1" runat="server">Init Vector:</asp:Label></td>
                                    <td><asp:TextBox ID="txtIV" runat="server" MaxLength="16" SkinID="MediumTextBox"  AutoPostBack="False"></asp:TextBox></td>
                                </tr>
                                <tr style="height:10px;"><td></td></tr>
                                <tr style="height:30px;">
                                    <td nowrap colspan=2 valign="bottom"><asp:Label ID="Label7" runat="server"><b>Database Security Info</b></asp:Label></td>
                                </tr>
                                <tr>
                                    <td nowrap><asp:Label ID="Label2" runat="server">User ID:</asp:Label></td>
                                    <td><asp:TextBox ID="txtUserId" runat="server" SkinID="MediumTextBox"  AutoPostBack="False"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td nowrap><asp:Label ID="Label3" runat="server">Pasword:</asp:Label></td>
                                    <td><asp:TextBox ID="txtPassword" runat="server" SkinID="MediumTextBox"  AutoPostBack="False"></asp:TextBox></td>
                                </tr>
                                <tr style="height:10px;"><td></td></tr>
                                <tr>
                                    <td nowrap><asp:Label ID="Label4" runat="server">User ID EU:</asp:Label></td>
                                    <td><asp:TextBox ID="txtUserIdEU" runat="server" SkinID="MediumTextBox"  AutoPostBack="False"></asp:TextBox></td>
                                </tr>
                                <tr>
                                    <td nowrap><asp:Label ID="Label5" runat="server">Pasword EU:</asp:Label></td>
                                    <td><asp:TextBox ID="txtPasswordEU" runat="server" SkinID="MediumTextBox"  AutoPostBack="False"></asp:TextBox></td>
                                </tr>
                                <tr style="height:10px;"><td></td></tr>
                                <tr>
                                    <td colspan="2" align="right"><asp:Button ID="btEncrypValues" runat="server" Text="Encryp Values" /></td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </div>

        <!--*** RETURN VALUES AREA**-->
            <div class="dataContainer">
                    <h2 class="dataGridHeader">Encrypted Elita Settings</h2>
                    <table border="0" class="dataGrid" width="100%">
                        <tr><td style="border-bottom:none" id=>
                            <asp:TextBox ID="txtEncryptedValue"  ForeColor="#333"  runat="server" TextMode="MultiLine" Rows="10" Width="550px"></asp:TextBox>
                        </td></tr>
                    </table>
             </div>    
        </td></tr>

        <tr><td class="navSeparator"></td></tr>
        </table>
    </form>
</body>
</html>
