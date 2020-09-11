<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UserControlAvailableSelected" Src="../Common/UserControlAvailableSelected.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RuleForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.RuleForm" %>

<%@ Register Src="../Common/UserControlAvailableSelected.ascx" TagName="UserControlAvailableSelected" TagPrefix="uc2" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>TemplateForm</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Styles.css" type="text/css" rel="STYLESHEET">
    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>

    <link type="text/css" href="../Navigation/styles/jquery-ui.elita.css" rel="stylesheet">
    <link type="text/css" href="../Navigation/styles/jquery-ui.min.css" rel="stylesheet">
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.12.4.min.js"> </script>
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-ui.min.js"> </script>
    <script type="text/javascript">
        $(function () {
            $("#tabs").tabs({
                activate: function () {
                    var selectedTab = $('#tabs').tabs('option', 'active');
                    $("#<%= hdnSelectedTab.ClientID %>").val(selectedTab);
                },
                active: document.getElementById('<%= hdnSelectedTab.ClientID %>').value
            });
        });
    </script>

    <style type="text/css">
        .FLATTEXTBOX {
        }
    </style>
</head>
<body leftmargin="0" topmargin="0" onload="changeScrollbarColor();" border="0" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <!--Start Header-->
        <table style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid" cellspacing="0" cellpadding="0" width="98%" bgcolor="#d5d6e4" border="0">
            <tr>
                <td valign="top">
                    <table width="100%" border="0">
                        <tr>
                            <td height="20">
                                <asp:Label ID="LabelTables" runat="server" CssClass="TITLELABEL">Tables</asp:Label>:&nbsp;
                            <asp:Label ID="Label7" runat="server" CssClass="TITLELABELTEXT">RULE</asp:Label>
                            </td>
                            <td align="right" height="20">
                                <strong>*</strong> <span id="Label9" style="font-weight: normal;">Indicates Required
                                Fields</span>
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <!--d5d6e4-->
        <table id="tblOuter2" style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid" height="93%" cellspacing="0" cellpadding="0" rules="none" width="98%" bgcolor="#d5d6e4" border="0">
            <tr>
                <td>&nbsp;
                </td>
            </tr>
            <tr>
                <td valign="top" align="center" height="100%">
                    <asp:Panel ID="WorkingPanel" runat="server">
                        <table id="tblMain1" style="border: 1px solid #999999; height: 95%;" cellspacing="0" cellpadding="6" rules="cols" width="98%" align="center" bgcolor="#fef9ea" border="0">
                            <tr>
                                <td height="1"></td>
                            </tr>
                            <tr>
                                <td style="height: 1px" align="center">
                                    <uc1:ErrorController ID="ErrorCtrl" runat="server"></uc1:ErrorController>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td valign="top" colspan="2">
                                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                    <tr>
                                                        <td style="width: 10%;"></td>
                                                        <td style="width: 10%;"></td>
                                                        <td style="width: 30%;"></td>
                                                        <td style="width: 10%;"></td>
                                                        <td style="width: 30%;"></td>
                                                        <td style="width: 10%;"></td>
                                                    </tr>
                                                    <tr>
                                                        <td></td>
                                                        <td nowrap="" align="right">
                                                            <asp:Label ID="moCodeLabel" runat="server">CODE</asp:Label>&nbsp;
                                                        </td>
                                                        <td>&nbsp;
                                                        <asp:TextBox ID="moCodeText" runat="server" Width="55%" CssClass="FLATTEXTBOX_TAB" AutoPostBack="False"></asp:TextBox>
                                                        </td>
                                                        <td nowrap="" align="right">&nbsp;<asp:Label ID="moRuleTypeLabel" runat="server">RULE_TYPE</asp:Label>
                                                            &nbsp;</td>
                                                        <td>
                                                            <asp:DropDownList ID="ddlRuleType" runat="server" AutoPostBack="True" CssClass="FLATTEXTBOX_TAB" Width="80%">
                                                            </asp:DropDownList>
                                                            &nbsp;</td>
                                                        <td></td>
                                                    </tr>
                                                    <tr>
                                                        <td>
                                                            <br>
                                                        </td>
                                                        <td></td>
                                                        </tr><tr>
                                                            <td></td>
                                                            <td nowrap="" align="right">
                                                                <asp:Label ID="moDescriptionLabel" runat="server">DESCRIPTION</asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>&nbsp;<asp:TextBox ID="moDescriptionText" runat="server" AutoPostBack="False" CssClass="FLATTEXTBOX_TAB" Width="80%"></asp:TextBox>
                                                            </td>
                                                            <td nowrap="" align="right">
                                                                <asp:Label ID="moEffectiveDateLabel" runat="server">EFFECTIVE_DATE</asp:Label>
                                                                &nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="moEffectiveDateText" runat="server" CssClass="FLATTEXTBOX" TabIndex="2" Width="170px"></asp:TextBox>
                                                                &nbsp;<asp:ImageButton ID="imgEffectiveDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" TabIndex="2" Visible="True"></asp:ImageButton>
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td nowrap="" align="right">
                                                                <asp:Label ID="moRuleCategoryLabel" runat="server">RULE_CATEGORY</asp:Label>
                                                            </td>
                                                            <td>&nbsp;<asp:DropDownList ID="ddlRuleCategory" runat="server" AutoPostBack="True" CssClass="FLATTEXTBOX_TAB" Width="80%">
                                                            </asp:DropDownList>
                                                                &nbsp;</td>
                                                            <td nowrap="" align="right">
                                                                <asp:Label ID="moExpirationDateLabel" runat="server">EXPIRATION_DATE</asp:Label>&nbsp;
                                                            </td>
                                                            <td>
                                                                <asp:TextBox ID="moExpirationDateText" runat="server" CssClass="FLATTEXTBOX" TabIndex="2" Width="170px"></asp:TextBox>
                                                                &nbsp;<asp:ImageButton ID="imgExpirationDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg" TabIndex="2" Visible="True"></asp:ImageButton>
                                                            </td>
                                                            <td></td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">&nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td></td>
                                                            <td nowrap="" align="right">
                                                                <asp:Label ID="moRuleExecutionPointLabel" runat="server">RULE_EXECUTION_POINT</asp:Label>
                                                            </td>
                                                            <td>&nbsp;<asp:TextBox ID="moRuleExecutionPointText" runat="server" AutoPostBack="False" CssClass="FLATTEXTBOX_TAB" Width="80%"></asp:TextBox>
                                                                &nbsp;</td>
                                                            <td nowrap="" align="right">&nbsp;
                                                            </td>
                                                            <td>&nbsp;</td>
                                                            <td></td>
                                                        </tr>
                                                    </table></td></tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <hr style="height: 1px">
                                            </td>
                                        </tr>
                                        <tr id="trPageSize" runat="server">
                                            <td valign="top" align="left">&nbsp;
                                            </td>
                                            <td align="right">&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="middle" align="left" colspan="2">
                                                <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0"></asp:HiddenField>
                                                <div id="tabs" class="style-tabs-old" style="border: none;">
                                                    <ul>
                                                        <li style="background: #d5d6e4"><a href="#tabIssues" rel="noopener noreferrer">
                                                            <asp:Label ID="Label4" runat="server" CssClass="tabHeaderTextOld">Issues</asp:Label></a></li>
                                                        <li style="background: #d5d6e4"><a href="#tabProcesses" rel="noopener noreferrer">
                                                            <asp:Label ID="Label6" runat="server" CssClass="tabHeaderTextOld">Processes</asp:Label></a></li>

                                                    </ul>
                                                    <div id="tabIssues" style="background: #d5d6e4">
                                                        <table id="tblCommentDetail" border="0" cellpadding="2" cellspacing="2" rules="cols" style="width: 97%; height: 100%">
                                                            <tr>
                                                                <td align="left" valign="top">
                                                                    <uc1:UserControlAvailableSelected ID="IssueAvailableSelected" runat="server"></uc1:UserControlAvailableSelected>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                    <div id="tabProcesses" style="background: #d5d6e4">
                                                        <table id="Table2" border="0" cellpadding="2" cellspacing="2" rules="cols" style="width: 97%; height: 100%">
                                                            <tr>
                                                                <td align="left" valign="top">
                                                                    <uc1:UserControlAvailableSelected ID="ProcessAvailableSelected" runat="server"></uc1:UserControlAvailableSelected>
                                                                </td>
                                                            </tr>
                                                        </table>
                                                    </div>
                                                </div>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td></td>
                                        </tr>
                                    </table>
                                </asp:Panel></td>
                            </tr>
                            <tr>
                                <td>
                                    <hr style="height: 1px">
                                </td>
                            </tr>
                            <tr>
                                <td valign="bottom" nowrap="" align="left" height="20">
                                    <asp:Button ID="btnBack" Style="background-image: url(../Navigation/images/icons/back_icon.gif); cursor: hand; background-repeat: no-repeat" TabIndex="185" runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Back"></asp:Button>&nbsp;
                                <asp:Button ID="btnSave_WRITE" Style="background-image: url(../Navigation/images/icons/save_icon.gif); cursor: hand; background-repeat: no-repeat" TabIndex="190" runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Save"></asp:Button>&nbsp;
                                <asp:Button ID="btnUndo_Write" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif); cursor: hand; background-repeat: no-repeat" TabIndex="195" runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Undo"></asp:Button>&nbsp;
                                <asp:Button ID="btnNew_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif); cursor: hand; background-repeat: no-repeat" TabIndex="200" runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" Height="20px" Text="New"></asp:Button>&nbsp;
                                <asp:Button ID="btnCopy_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif); cursor: hand; background-repeat: no-repeat" TabIndex="205" runat="server" Width="90px" Height="20px" CssClass="FLATBUTTON" Text="NEW_WITH_COPY" CausesValidation="False"></asp:Button>&nbsp;
                                <asp:Button ID="btnDelete_WRITE" Style="background-image: url(../Navigation/images/icons/clear_icon.gif); cursor: hand; background-repeat: no-repeat" TabIndex="210" runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Delete"></asp:Button>
                                </td>
                            </tr>
                        </table>
                    
                
            
        
        <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse" runat="server" designtimedragdrop="261">
    </form>
</body>
</html>
(../Navigation/images/icons/add_icon.gif); cursor: hand; background-repeat: no-repeat"
                                    TabIndex="205" runat="server" Width="90px"
                                    Height="20px" CssClass="FLATBUTTON" Text="NEW_WITH_COPY" CausesValidation="False"></asp:Button>&nbsp;
                                <asp:Button ID="btnDelete_WRITE" Style="background-image: url(../Navigation/images/icons/clear_icon.gif); cursor: hand; background-repeat: no-repeat"
                                    TabIndex="210" runat="server" Font-Bold="false"
                                    Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Delete"></asp:Button>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
        <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
            runat="server" designtimedragdrop="261" />
    </form>
</body>
</html>
