<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="url" TagName="UserControlRulesAvailable" Src="../Interfaces/SearchAvailableDealer.ascx" %>
<%@ Register TagPrefix="ur2" TagName="UserControlQuestionsAvailable" Src="../Interfaces/SearchAvailableQuestions.ascx" %>
<%@ Register TagPrefix="ur3" TagName="UCAvailableSelected" Src="../common/UserControlAvailableSelected.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="IssueDetailForm.aspx.vb"
    EnableSessionState="True" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.IssueDetailForm" %>

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
                            <asp:Label ID="Label7" runat="server" CssClass="TITLELABELTEXT">ISSUE</asp:Label>
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
                            <td style="height: 1px" align="center">
                                <uc1:ErrorController ID="ErrorCtrl" runat="server"></uc1:ErrorController>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tr>
                                        <td valign="top" >
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
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td nowrap align="right">
                                                        <asp:Label ID="moCodeLabel" runat="server">CODE</asp:Label>&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="moCodeText" runat="server" Width="80%" CssClass="FLATTEXTBOX_TAB"
                                                            AutoPostBack="False"></asp:TextBox>
                                                    </td>
                                                    <td nowrap align="right">
                                                        <asp:Label ID="moIssueTypeLabel" runat="server">ISSUE_TYPE</asp:Label>&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="cboIssueTypeText" runat="server" Width="60%" AutoPostBack="False">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td nowrap align="right">
                                                        <asp:Label ID="moDescriptionLabel" runat="server">DESCRIPTION</asp:Label>&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="moDescriptionText" runat="server" Width="80%" CssClass="FLATTEXTBOX_TAB"
                                                            AutoPostBack="False"></asp:TextBox>
                                                    </td>
                                                    <td nowrap align="right">
                                                        <asp:Label ID="moEffectiveDateLabel" runat="server">EFFECTIVE_DATE</asp:Label>&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="moEffectiveDateText" runat="server" CssClass="FLATTEXTBOX" TabIndex="2"
                                                            Width="175px"></asp:TextBox>
                                                        &nbsp;
                                                        <asp:ImageButton ID="imgEffectiveDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                                            TabIndex="2" Visible="True" />
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td nowrap align="right">
                                                        <asp:Label ID="moAsOfDateLabel" runat="server">AS_OF_DATE</asp:Label>&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="moAsOfDateText" runat="server" Width="80%" CssClass="FLATTEXTBOX_TAB"
                                                            AutoPostBack="False" Enabled="false"></asp:TextBox>
                                                    </td>
                                                    <td nowrap align="right">
                                                        <asp:Label ID="moExpirationDateLabel" runat="server">EXPIRATION_DATE</asp:Label>&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="moExpirationDateText" runat="server" CssClass="FLATTEXTBOX" TabIndex="2"
                                                            Width="175px"></asp:TextBox>
                                                        &nbsp;
                                                        <asp:ImageButton ID="imgExpirationDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"
                                                            TabIndex="2" Visible="True" />
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td nowrap align="right">
                                                        <asp:Label ID="moPreConditionsLabel" runat="server">PRE_CONDITIONS</asp:Label>&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="moPreConditionsTextBox" runat="server" Width="80%" CssClass="FLATTEXTBOX_TAB"
                                                            AutoPostBack="False"></asp:TextBox>
                                                    </td>
                                                      <td nowrap align="right">
                                                        <asp:Label ID="moIssueProcessorLabel" runat="server">ISSUE_PROCESSOR</asp:Label>&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="cboIssueProcessor" runat="server" Width="60%" AutoPostBack="true">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>
                                                 <tr>
                                                    <td>
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td nowrap align="right">
                                                        <asp:Label ID="moClaimTypeLabel" runat="server">SECURITY_CLAIM_TYPE</asp:Label>&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="cboClaimType" runat="server" Width="60%" AutoPostBack="False">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td nowrap align="right">
                                                        <asp:Label ID="moClaimValueLabel" runat="server">SECURITY_CLAIM_VALUE</asp:Label>&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:TextBox ID="moClaimValueTextBox" runat="server" Width="80%" CssClass="FLATTEXTBOX_TAB"
                                                            AutoPostBack="False"></asp:TextBox>
                                                    </td>

                                                    <td>
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                        <br />
                                                    </td>
                                                </tr>
                                                <tr>
                                                    <td>
                                                    </td>
                                                    <td nowrap align="right">
                                                        <asp:Label ID="moClaimDeniedRsnLabel" runat="server">DENIED_REASON</asp:Label>&nbsp;
                                                    </td>
                                                    <td>
                                                        <asp:DropDownList ID="cboClaimDeniedRsn" runat="server" Width="60%" AutoPostBack="False">
                                                        </asp:DropDownList>
                                                    </td>
                                                    <td>                                                       
                                                    </td>
                                                    <td>                                                        
                                                    </td>
                                                    <td>
                                                    </td>
                                                </tr>

                                            </table>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td >
                                            <br />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td >
                                            <hr style="height: 1px" />
                                        </td>
                                    </tr>
                                    <tr id="trPageSize" runat="server">
                                        <td valign="top" align="left">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td valign="middle" align="left">
                                            <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0" />
                                            <div id="tabs" class="style-tabs-old" style="border:none;">
                                                <ul>
                                                    <li><a href="#tabNotes"><asp:Label ID="Label4" runat="server" CssClass="tabHeaderTextOld">Notes</asp:Label></a></li>
                                                    <li><a href="#tabQuestions"><asp:Label ID="Label6" runat="server" CssClass="tabHeaderTextOld">Questions</asp:Label></a></li>
                                                    <li><a href="#tabRules"><asp:Label ID="Label1" runat="server" CssClass="tabHeaderTextOld">Rules</asp:Label></a></li>
                                                    <li><a href="#tabWorkQueue"><asp:Label ID="Label2" runat="server" CssClass="tabHeaderTextOld">WORK_QUEUE</asp:Label></a></li>
                                                </ul>
                                                <div id="tabNotes" style="background:#d5d6e4">
                                                    <asp:Panel ID="PanelNotesEditDetail" runat="server" Width="100%" Height="100%">
                                                        <table id="Table1" border="0" cellpadding="2" cellspacing="2" rules="cols"
                                                            style="width: 100%; height: 100%; border: 1px solid #999999;">
                                                            <tr id="tr1" runat="server">
                                                                <td style="vertical-align: top; text-align: left;"><asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>: &nbsp; <asp:DropDownList ID="cboPageSize" runat="server" AutoPostBack="true" Width="50px"><asp:ListItem Value="5">5</asp:ListItem><asp:ListItem Selected="True" Value="10">10</asp:ListItem><asp:ListItem Value="15">15</asp:ListItem><asp:ListItem Value="20">20</asp:ListItem><asp:ListItem Value="25">25</asp:ListItem><asp:ListItem Value="30">30</asp:ListItem><asp:ListItem Value="35">35</asp:ListItem><asp:ListItem Value="40">40</asp:ListItem><asp:ListItem Value="45">45</asp:ListItem><asp:ListItem Value="50">50</asp:ListItem></asp:DropDownList></td><td style="text-align: right;"><asp:Label ID="lblRecordCount" runat="server"></asp:Label></td></tr><tr><td align="middle" colspan="2"><div id="scroller1" style="overflow: auto; width: 98%; height: 125px" align="center"
                                                                        runat="server"><asp:DataGrid ID="GVNotes" runat="server" Width="100%" AutoGenerateColumns="False"
                                                                            Visible="true" OnItemCommand="ItemCommand" OnItemCreated="ItemCreated" CellPadding="1"
                                                                            AllowSorting="True" AllowPaging="True" CssClass="DATAGRID"><ItemStyle BackColor="White"></ItemStyle><AlternatingItemStyle BackColor="#F1F1F1"></AlternatingItemStyle><Columns><asp:TemplateColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="5%"><ItemStyle /><ItemTemplate><asp:ImageButton ID="EditButton_WRITE" Style="cursor: hand;" runat="server" CommandName="EditRecord"
                                                                                            ImageUrl="~/Navigation/images/icons/edit2.gif"></asp:ImageButton></ItemTemplate></asp:TemplateColumn><asp:TemplateColumn ItemStyle-HorizontalAlign="Center" HeaderStyle-Width="5%"><ItemStyle /><ItemTemplate><asp:ImageButton ID="DeleteButton_WRITE" Style="cursor: hand;" runat="server" CommandName="DeleteRecord"
                                                                                            ImageUrl="~/Navigation/images/icons/trash.gif"></asp:ImageButton></ItemTemplate></asp:TemplateColumn><asp:TemplateColumn SortExpression="issue_comment_id" Visible="False" HeaderText="Id" HeaderStyle-Width="0%"><ItemTemplate><asp:Label ID="IdLabel" runat="server"> </asp:Label></ItemTemplate></asp:TemplateColumn><asp:TemplateColumn SortExpression="issue_comment_type_id" HeaderText="NOTE_TYPE" HeaderStyle-Width="40%"><ItemTemplate><asp:Label ID="NoteTypeLabel" runat="server" Visible="True"> </asp:Label></ItemTemplate><EditItemTemplate><asp:DropDownList ID="cboNoteType" runat="server" Visible="True" Width="90%"></asp:DropDownList></EditItemTemplate></asp:TemplateColumn><asp:TemplateColumn SortExpression="code" HeaderText="CODE" HeaderStyle-Width="25%"><ItemTemplate><asp:Label ID="CodeLabel" runat="server" Visible="True"> </asp:Label></ItemTemplate><EditItemTemplate><asp:TextBox ID="moCode" runat="server" Visible="True" Width="90%"> </asp:TextBox></EditItemTemplate></asp:TemplateColumn><asp:TemplateColumn SortExpression="text" HeaderText="NOTE" HeaderStyle-Width="25%"><ItemTemplate><asp:Label ID="TextLabel" runat="server" Visible="True"> </asp:Label></ItemTemplate><EditItemTemplate><asp:TextBox ID="moText" runat="server" Visible="True" Width="90%"> </asp:TextBox></EditItemTemplate></asp:TemplateColumn></Columns><PagerStyle HorizontalAlign="Center" CssClass="PAGER" PageButtonCount="15" Mode="NumericPages"></PagerStyle></asp:DataGrid></div></td></tr><tr><td colspan="2">&#160;&#160;&#160;&#160;&#160; <asp:Button ID="btnNew_Comment" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                                                        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                                                        Width="100px" CssClass="FLATBUTTON" Height="20px" Text="New"></asp:Button>&nbsp; <asp:Button ID="btnSave_Comment" Style="background-image: url(../Navigation/images/icons/save_icon.gif);
                                                                        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                                                        Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Save"></asp:Button>&nbsp; <asp:Button ID="btnCancel_Comment" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
                                                                        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                                                        Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Cancel"></asp:Button></td></tr>

                                                        </table>
                                                    </asp:Panel>
                                                </div>
                                                <div id="tabQuestions" style="background:#d5d6e4">
                                                    <asp:Panel ID="PanelQuestionsEditDetail" runat="server" Width="100%" Height="100%">
                                                        <div id="commentScroller" align="center" style="overflow: auto; width: 99.53%; height: 100%">
                                                            <table id="tblCommentDetail" border="0" cellpadding="2" cellspacing="2"
                                                                rules="cols" style="width: 100%; height: 100%"><tr><td align="left" valign="top"><ur2:UserControlQuestionsAvailable ID="UserControlQuestionsAvailable" runat="server"
                                                                            tabindex="12"></ur2:UserControlQuestionsAvailable></td></tr>
                                                            </table>
                                                        </div>
                                                    </asp:Panel>

                                                </div>
                                                <div id="tabRules" style="background:#d5d6e4">
                                                    <asp:Panel ID="PanelRulesEditDetail" runat="server" Width="100%" Height="100%"><div id="Div1" align="center" style="overflow: auto; width: 99.53%; height: 100%"><table id="Table2" background="" border="0" cellpadding="2" cellspacing="2" rules="cols"
                                                                style="width: 100%; height: 100%"><tr><td align="left" valign="top"><url:UserControlRulesAvailable ID="UserControlRulesAvailable" runat="server" background= "#d5d6e4" tabindex="12"></url:UserControlRulesAvailable></td></tr></table></div>

                                                    </asp:Panel>
                                                </div>
                                                <div id="tabWorkQueue" style="background:#d5d6e4">
                                                    <asp:Panel ID="plnQueue" runat="server" Width="100%" Height="100%"><div id="Div2" align="center" style="overflow: auto; width: 99.53%; height: 100%">
                                                                <table  border="0" cellpadding="2" cellspacing="2" rules="cols" style="width: 100%; height: 100%; border: 1px solid #999999;">
                                                                <tr><td height="20px"></td></tr>
                                                                <tr><td style="vertical-align: top; text-align: left;">
                                                                <ur3:UCAvailableSelected ID="UC_QUEUE_AVASEL"  runat="server" tabindex="12"></ur3:UCAvailableSelected>
                                                                        </td>
                                                                        </tr>
                                                                        <tr><td style="vertical-align: top; text-align: left;">&nbsp;&nbsp;
                                                                            <asp:Button ID="btnSave_WQ" Style="background-image: url(../Navigation/images/icons/save_icon.gif);
                                                                            cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                                                            Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Save"></asp:Button>&nbsp;&nbsp;<asp:Button ID="btnCancel_WQ" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
                                                                            cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                                                            Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Cancel"></asp:Button>
                                                                        </td></tr>
                                                                        </table>
                                                                </div></asp:Panel>
                                                </div>
                                            </div>
                                            <%--<mytab:TabStrip ID="tsQuestions" runat="server" TargetID="mpIssues" SepDefaultStyle="border-bottom:solid 1px #000000;"
                                                TabSelectedStyle="border:solid 1px black;border-bottom:none;background:#d5d6e4;padding-left:7px;padding-right:7px;"
                                                TabHoverStyle="background:#faecc2" TabDefaultStyle="border:solid 1px black;background:#f1f1f1;padding-top:2px;padding-bottom:2px;padding-left:7px;padding-right:7px;">
                                                <mytab:Tab ID="tabNotes" Text="Notes" DefaultImageUrl=""></mytab:Tab>
                                                <mytab:Tab ID="tabQuestion" Text="Questions" DefaultImageUrl=""></mytab:Tab>
                                                <mytab:Tab ID="tabRule" Text="Rules" DefaultImageUrl=""></mytab:Tab>
                                                <mytab:Tab ID="tabQueue" Text="WORK_QUEUE" DefaultImageUrl=""></mytab:Tab>
                                            </mytab:TabStrip>--%>
                                            <%--<mytab:MultiPage ID="mpIssues" Style="border-right: #000000 1px solid; padding-right: 5px;
                                                border-top: #000000 1px solid; padding-left: 5px; background: #d5d6e4; padding-bottom: 5px;
                                                border-left: #000000 1px solid; padding-top: 5px; border-bottom: #000000 1px solid"
                                                runat="server" Width="99%" Height="267px">
                                                <mytab:PageView>
                                                </mytab:PageView>
                                                <mytab:PageView>
                                                </mytab:PageView>
                                                <mytab:PageView>
                                                </mytab:PageView>
                                               <mytab:PageView>
                                               </mytab:PageView>
                                            </mytab:MultiPage>--%>
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
        runat="server" designtimedragdrop="261" />
    </form>
</body>
</html>
