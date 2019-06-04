<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="url" TagName="UserControlDealerAvailable" Src="../Interfaces/SearchAvailableDealer.ascx" %>
<%@ Register TagPrefix="ur2" TagName="UserControlQuestionsAvailable" Src="../Interfaces/SearchAvailableQuestions.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="QuestionListDetailForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.QuestionListDetailForm" %>

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
    
    <link type="text/css" href="../Navigation/styles/jquery-ui.min.css" rel="stylesheet"/>
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.12.4.min.js" > </script>
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-ui.min.js" > </script>
    <link type="text/css" href="../Navigation/styles/jquery-ui.elita.css" rel="stylesheet"/>
    <script type="text/javascript">
    $(function () {
        $("#tabs").tabs({
            activate: function() {
                var selectedTab = $('#tabs').tabs('option', 'active');
                $("#<%= hdnSelectedTab.ClientID %>").val(selectedTab);
            },
            active: document.getElementById('<%= hdnSelectedTab.ClientID %>').value
        });
    });
    </script>
    
</head>
<body leftmargin="0" topmargin="0" onload="changeScrollbarColor();" border="0" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
    <!--Start Header-->
    <table style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px;
        border-left: black 1px solid; border-bottom: black 1px solid" cellspacing="0"
        cellpadding="0" width="98%" bgcolor="#d5d6e4" border="0">
        <tr>
            <td valign="top">
                <table width="100%" border="0">
                    <tr>
                        <td height="20">
                            <asp:Label ID="LabelTables" runat="server" CssClass="TITLELABEL">Tables</asp:Label>:&nbsp;
                            <asp:Label ID="Label7" runat="server" CssClass="TITLELABELTEXT">QUESTION_LIST</asp:Label>
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
    <table id="tblOuter2" style="border-right: black 1px solid; border-top: black 1px solid;
        margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid" height="93%"
        cellspacing="0" cellpadding="0" rules="none" width="98%" bgcolor="#d5d6e4" border="0">
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td valign="top" align="center" height="100%">
                <asp:Panel ID="WorkingPanel" runat="server">
                    <table id="tblMain1" style="border: 1px solid #999999; height: 95%;" cellspacing="0"
                        cellpadding="6" rules="cols" width="98%" align="center" bgcolor="#fef9ea" border="0">
                        <tr>
                            <td height="1">
                            </td>
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
                                                    <td style="width: 10%;">
                                                    </td>
                                                    <td style="width: 10%;">
                                                    </td>
                                                    <td style="width: 30%;">
                                                    </td>
                                                    <td style="width: 10%;">
                                                    </td>
                                                    <td style="width: 30%;">
                                                    </td>
                                                    <td style="width: 10%;">
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td nowrap align="right">
                                                        <asp:Label ID="moCodeLabel" runat="server">CODE</asp:Label>&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="moCodeText" runat="server" Width="55%" CssClass="FLATTEXTBOX_TAB"
                                                            AutoPostBack="False"></asp:TextBox>
                                                    </td>
                                                    <td nowrap align="right">
                                                        <asp:Label ID="moDescriptionLabel" runat="server">DESCRIPTION</asp:Label>&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="moDescriptionText" runat="server" Width="80%" CssClass="FLATTEXTBOX_TAB"
                                                            AutoPostBack="False"></asp:TextBox>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <br />
                                                    </td>
                                                    <td>
                                                    </td>
                                                    <tr>
                                                        <td>
                                                        </td>
                                                        <td nowrap align="right">
                                                            <asp:Label ID="moEffectiveDateLabel" runat="server">EFFECTIVE_DATE</asp:Label>&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="moEffectiveDateText" runat="server" CssClass="FLATTEXTBOX" TabIndex="2"
                                                                Width="176px"></asp:TextBox>
                                                            &nbsp;
                                                            <asp:ImageButton ID="imgEffectiveDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                                                TabIndex="2" Visible="True" />
                                                        </td>
                                                        <td nowrap align="right">
                                                            <asp:Label ID="moExpirationDateLabel" runat="server">EXPIRATION_DATE</asp:Label>&nbsp;
                                                        </td>
                                                        <td>
                                                            <asp:TextBox ID="moExpirationDateText" runat="server" CssClass="FLATTEXTBOX" TabIndex="2"
                                                                Width="170px"></asp:TextBox>
                                                            &nbsp;
                                                            <asp:ImageButton ID="imgExpirationDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                                                TabIndex="2" Visible="True" />
                                                        </td>
                                                        <td>
                                                        </td>
                                                    </tr>
                                                    <tr>
                                                        <td colspan="4">
                                                            &nbsp;
                                                        </td>
                                                    </tr>
                                                </tr>
                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="2">
                                            <hr style="height: 1px" />
                                        </td>
                                    </tr>
                                    <tr id="trPageSize" runat="server">
                                        <td valign="top" align="left">
                                            &nbsp;
                                        </td>
                                        <td align="right">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="middle" align="left" colspan="2">
                                            <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0" />
                                            <div id="tabs" class="style-tabs-old" style="border:none;">
                                              <ul>
                                                <li><a href="#tabQuestion"><asp:Label ID="Label4" runat="server" CssClass="tabHeaderTextOld">Question</asp:Label></a></li>
                                                <li><a href="#tabDealer"><asp:Label ID="Label6" runat="server" CssClass="tabHeaderTextOld">Dealer</asp:Label></a></li>
                                              </ul>
                                              <div id="tabQuestion" style="background:#d5d6e4">
                                                  <asp:Panel ID="PanelQuestionsEditDetail" runat="server" Width="100%" Height="100%">
                                                        <div id="commentScroller" align="center" style="overflow: auto; width: 99.53%; height: 100%">
                                                            <table id="tblCommentDetail" border="0" cellpadding="2" cellspacing="2"
                                                                rules="cols" style="width: 100%; height: 100%">
                                                                <tr>
                                                                    <td align="left" valign="top">
                                                                        <ur2:UserControlQuestionsAvailable ID="UserControlQuestionsAvailable" runat="server"
                                                                            tabindex="12">
                                                                        </ur2:UserControlQuestionsAvailable>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </asp:Panel>
                                              </div>
                                              <div id="tabDealer" style="background:#d5d6e4">
                                                  <asp:Panel ID="PanelDealerEditDetail" runat="server" Width="100%" Height="100%">
                                                        <div id="Div1" align="center" style="overflow: auto; width: 99.53%; height: 100%">
                                                            <table id="Table2" border="0" cellpadding="2" cellspacing="2" rules="cols"
                                                                style="width: 100%; height: 100%">
                                                                <tr>
                                                                    <td align="left" valign="top">
                                                                        <url:UserControlDealerAvailable ID="UserControlDealerAvailable" runat="server" tabindex="12"></url:UserControlDealerAvailable>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </div>
                                                    </asp:Panel>
                                              </div>
                                            </div>                                             
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <hr style="height: 1px" />
                            </td>
                        </tr>
                        <tr>
                            <td valign="bottom" nowrap align="left" height="20">
                                <asp:Button ID="btnBack" Style="background-image: url(../Navigation/images/icons/back_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" TabIndex="185" runat="server" Font-Bold="false"
                                    Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Back"></asp:Button>&nbsp;
                                <asp:Button ID="btnSave_WRITE" Style="background-image: url(../Navigation/images/icons/save_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" TabIndex="190" runat="server" Font-Bold="false"
                                    Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Save"></asp:Button>&nbsp;
                                <asp:Button ID="btnUndo_Write" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" TabIndex="195" runat="server" Font-Bold="false"
                                    Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Undo"></asp:Button>&nbsp;
                                <asp:Button ID="btnNew_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" TabIndex="200" runat="server" Font-Bold="false"
                                    Width="90px" CssClass="FLATBUTTON" Height="20px" Text="New"></asp:Button>&nbsp;
                                <asp:Button ID="btnCopy_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" TabIndex="205" runat="server" Width="90px"
                                    Height="20px" CssClass="FLATBUTTON" Text="NEW_WITH_COPY" CausesValidation="False">
                                </asp:Button>&nbsp;
                                <asp:Button ID="btnDelete_WRITE" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" TabIndex="210" runat="server" Font-Bold="false"
                                    Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Delete"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
        runat="server" designtimedragdrop="261"/>
    </form>
</body>
</html>
