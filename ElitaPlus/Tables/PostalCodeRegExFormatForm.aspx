<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PostalCodeRegExFormatForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.PostalCodeRegExFormatForm" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>PostalCodeRegExFormatForm</title>
    <meta content="Microsoft Visual Studio .NET 7.1" name="GENERATOR">
    <meta content="Visual Basic .NET 7.1" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Styles.css" type="text/css" rel="STYLESHEET">

    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>

</head>
<body oncontextmenu="return false" style="width: 555px; background-repeat: repeat;
    height: 360px" bottommargin="0" leftmargin="0" background="../Common/images/back_spacer.jpg"
    topmargin="0" scroll="no" onload="changeScrollbarColor();" rightmargin="0" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <input id="imgURL" type="hidden" name="imgURL" runat="server">
    <asp:Panel ID="panelForm" runat="server" Width="704px" Height="504px">
        <table style="width: 699px; height: 468px" cellspacing="2" cellpadding="2" bgcolor="#f4f3f8"
            border="0">
            <tr>
                <td style="width: 301px; height: 317px" valign="top">
                    <asp:Panel ID="ListPanel" runat="server" Height="444px" Width="208px">
                        <table id="ListTabel" style="height: 270px" cellspacing="2" cellpadding="2" border="0">
                            <tr>
                                <td style="height: 25px" colspan="2">
                                    <asp:Label ID="Label4" runat="server">RegEx Segments</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="height: 155px" valign="top" colspan="2">
                                    <asp:ListBox ID="SegmentList" runat="server" Height="160px" Width="304px"></asp:ListBox>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top" colspan="2">
                                    <asp:Button ID="AddRegex" TabIndex="1" runat="server" Width="55px" Text="Add"></asp:Button>
                                    <asp:Button ID="EditRegex" runat="server" Width="55px" Text="Edit"></asp:Button>
                                    <asp:Button ID="DeleteRegex" runat="server" Width="55px" Text="Delete"></asp:Button>
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Label ID="Label5" runat="server">Regular Expression</asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 301px" align="left" colspan="2">
                                    <asp:TextBox ID="RegExBox" runat="server" Height="40px" Width="304px" TextMode="MultiLine"
                                        ReadOnly="True" BackColor="#E0E0E0"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 301px" align="center" colspan="2">
                                    <asp:Button ID="ClearRegEx" runat="server" Text="Clear Regex" Visible="False"></asp:Button>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 301px; height: 22px" colspan="2">
                                    ___________________________________________
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Label ID="Label3" runat="server">Preview</asp:Label>
                                </td>
                                <td>
                                    <asp:Label ID="lblPreview" runat="server"></asp:Label>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 301px; height: 23px" colspan="2">
                                    ___________________________________________
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 64px">
                                    <asp:Label ID="Label2" runat="server">Test Input</asp:Label>
                                </td>
                                <td>
                                    <asp:TextBox ID="TestInput" runat="server" Width="100%"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td style="width: 64px" align="center">
                                    <asp:Label ID="lblTest" runat="server" Width="100%"></asp:Label>
                                </td>
                                <td align="right">
                                    <asp:Button ID="btnTest" runat="server" Text="Test Input"></asp:Button>
                                </td>
                            </tr>
                            <tr>
                                <td valign="bottom" nowrap align="left" colspan="2" height="25">
                                    <asp:Button ID="btnSave_WRITE" Style="background-position: left center; background-image: url(../Navigation/images/icons/save_icon.gif);
                                        cursor: hand; background-repeat: no-repeat" runat="server" Height="21" Text="&nbsp;&nbsp;Save"
                                        Font-Bold="false" ForeColor="#000000" Width="100" CssClass="FLATBUTTON" ToolTip="Save changes to database">
                                    </asp:Button>&nbsp;
                                    <input class="FLATBUTTON" id="btnUndo_Write" title="Cancel" style="background-position: left center;
                                        background-image: url(../Navigation/images/icons/cancel_icon.gif); width: 100px;
                                        cursor: hand; color: #000000; background-repeat: no-repeat; height: 21px" onclick="parent.ClosePostalCodeFormat();"
                                        type="button" value=" Cancel" height="21" width="100">
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
                <td style="height: 317px" valign="top">
                    <asp:Panel ID="DetailPanel" runat="server" Height="302px" Width="368px">
                        <table cellspacing="2" cellpadding="2" border="0">
                            <tbody>
                                <tr>
                                    <td align="center" colspan="2">
                                        <asp:Label ID="EditPanelTitle" runat="server" Width="100%" Font-Underline="True"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 25px">
                                        <asp:Label ID="Label1" runat="server">PostalCode Char Type</asp:Label>
                                    </td>
                                    <td style="height: 25px">
                                        <asp:DropDownList ID="RegExTypeList" runat="server" AutoPostBack="True">
                                            <asp:ListItem Value="Numeric" Selected="True">Numeric</asp:ListItem>
                                            <asp:ListItem Value="Alpha">Alpha</asp:ListItem>
                                            <asp:ListItem Value="AlphaNumeric">AlphaNumeric</asp:ListItem>
                                            <asp:ListItem Value="Space">Space</asp:ListItem>
                                            <asp:ListItem Value="SpecialChar">SpecialChar(s)</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:CheckBox ID="chkOptional" runat="server" Text=" Optional ?" Visible="False"
                                            AutoPostBack="True"></asp:CheckBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="MinLabel" runat="server" Visible="False">Minimum Length</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="MinLen" runat="server" Width="45px" Visible="False">0</asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="MaxLabel" runat="server"> Length</asp:Label>
                                    </td>
                                    <td>
                                        <asp:TextBox ID="MaxLen" runat="server" Width="45px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        <asp:Label ID="CaseLabel" runat="server">Case</asp:Label>
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="CaseList" runat="server">
                                            <asp:ListItem Value="Both">Both</asp:ListItem>
                                            <asp:ListItem Value="Upper" Selected="True">Upper</asp:ListItem>
                                            <asp:ListItem Value="Lower">Lower</asp:ListItem>
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 26px">
                                        <asp:Label ID="DisallowLabel" runat="server">Disallowed Values</asp:Label>
                                    </td>
                                    <td style="height: 26px">
                                        <asp:TextBox ID="DisallowedValues" runat="server" Width="208px"></asp:TextBox>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                    </td>
                                    <td>
                                        <asp:Label ID="ExampleLabel" runat="server" Width="200px">e.g.  0,9  if 0 and 9 are not allowed in the RegEx</asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <asp:Label ID="ErrorLabel" runat="server" Width="100%" Visible="False" ForeColor="Red"></asp:Label>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="left">
                                        <asp:Button ID="btnBack" runat="server" Width="100px" Text="Cancel"></asp:Button>
                                    </td>
                                    <td align="right">
                                        <asp:Button ID="btnGenerate" runat="server" Width="100px" Text="Save Regex"></asp:Button>
                                    </td>
                                </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </asp:Panel>
    </form>
</body>
</html>
