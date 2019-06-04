<%@ Page Language="vb" AutoEventWireup="false" EnableSessionState="True" CodeBehind="QuestionForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.QuestionForm" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="url" TagName="UserControlSearchAvailable" Src="../Interfaces/SearchAvailableSelected.ascx" %>
<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>TemplateForm</title>
    <!-- ************* THIS CODE HAS BEEN GENERATED FROM TEMPLATE BOEditingWebForm.cst (9/20/2004)  ******************** -->
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR"/>
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE" />
    <meta content="JavaScript" name="vs_defaultClientScript" />
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema" />
    <link href="../Styles.css" type="text/css" rel="STYLESHEET"/>
    <script language="JavaScript" type="text/javascript"  src="../Navigation/Scripts/GlobalHeader.js"></script>
    <link type="text/css" href="../Navigation/styles/jquery-ui.min.css" rel="stylesheet"/>
    <link type="text/css" href="../Navigation/styles/jquery-ui.elita.css" rel="stylesheet"/>  
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.12.4.min.js" > </script>
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-ui.min.js" > </script>
    <script type="text/javascript">        
        $(function () {
                        $("#tabs").tabs({
                                        activate: function() {
                                            var selectedTab = $('#tabs').tabs('option', 'active');
                                            $("#<%= hdnSelectedTab.ClientID %>").val(selectedTab);
                                        },
                            active: document.getElementById('<%= hdnSelectedTab.ClientID %>').value,
                            disabled:document.getElementById('<%= hdnDisabledTab.ClientID %>').value
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
                            <p>
                                &nbsp;
                                <asp:Label ID="LabelTables" runat="server" CssClass="TITLELABEL">Admin</asp:Label>:
                                <asp:Label ID="Label40" runat="server" CssClass="TITLELABELTEXT">Question</asp:Label></p>
                        </td>
                        <td align="right" height="20">
                            <strong>*</strong>
                            <asp:Label ID="Label9" runat="server" Font-Bold="false">INDICATES_REQUIRED_FIELDS</asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table id="tblOuter2" style="border-right: black 1px solid; border-top: black 1px solid;
        margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid" height="93%"
        cellspacing="0" cellpadding="0" rules="none" width="98%" bgcolor="#d5d6e4" border="0">
        <!--d5d6e4-->
        <tr>
            <td>
                &nbsp;
            </td>
        </tr>
        <tr>
            <td valign="top" align="center">
                <asp:Panel ID="WorkingPanel" runat="server" Height="98%" Width="100%">
                    <table id="tblMain1" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                        border-left: #999999 1px solid; border-bottom: #999999 1px solid; height: 95%"
                        cellspacing="0" cellpadding="6" rules="cols" width="95%" align="center" bgcolor="#fef9ea"
                        border="0">
                        <tr>
                            <td valign="middle" align="center" colspan="4">
                                <uc1:ErrorController ID="ErrorCtrl" runat="server"></uc1:ErrorController>
                                &nbsp;
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <asp:Panel ID="EditPanel" runat="server" Width="100%">
                                    <table id="Table4" style="width: 100%" cellspacing="1" cellpadding="0" width="710"
                                        border="0">
                                        <tr>
                                            <td>
                                                <asp:Panel ID="PanelMasterFields_Write" runat="server" Width="100%">
                                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                        <tr>
                                                            <td nowrap align="right" colspan="1">
                                                                &nbsp;
                                                                <asp:Label ID="lblCode" runat="server" Font-Bold="False">CODE</asp:Label>&nbsp;
                                                            </td>
                                                            <td colspan="1" rowspan="1" align="left">
                                                                <asp:TextBox ID="txtCode" TabIndex="1" runat="server" Width="210px" CssClass="FLATTEXTBOX" />
                                                            </td>
                                                            <td nowrap align="right" colspan="1">
                                                                &nbsp;
                                                                <asp:Label ID="lblDescription" runat="server" Font-Bold="false">DESCRIPTION</asp:Label>&nbsp;
                                                            </td>
                                                            <td colspan="1" rowspan="1" align="left">
                                                                <asp:TextBox ID="txtDescription" TabIndex="2" runat="server" Width="210px" CssClass="FLATTEXTBOX" />
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                &nbsp;
                                                                <asp:Label ID="lbleffectiveDate" runat="server" Font-Bold="false">EFFECTIVE_DATE</asp:Label>&nbsp;
                                                            </td>
                                                            <td colspan="1" align="left">
                                                                <asp:TextBox ID="txtEffective" TabIndex="3" Width="210px" runat="server" CssClass="FLATTEXTBOX"></asp:TextBox>
                                                                <asp:imagebutton id="btneffective" runat="server" 
                                                                    ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:imagebutton>
                                                            </td>
                                                            <td align="right">
                                                                &nbsp;
                                                                <asp:Label ID="lblExpirationDate" runat="server" Font-Bold="False">EXPIRATION_DATE</asp:Label>&nbsp;
                                                            </td>
                                                            <td colspan="1" align="left">
                                                                <asp:TextBox ID="txtExpirationDate" TabIndex="4" Width="210px" runat="server" CssClass="FLATTEXTBOX"></asp:TextBox>
                                                                <asp:imagebutton id="btnExpiration" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:imagebutton>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td align="right">
                                                                &nbsp;
                                                                <asp:Label ID="lblQuestionType" runat="server" Font-Bold="false">QUESTION_TYPE</asp:Label>&nbsp;
                                                            </td>
                                                            <td colspan="1" align="left">
                                                                <asp:DropDownList ID="ddlQuestionType" TabIndex="5" runat="server" Width="210px" AutoPostBack="true">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="right">
                                                                &nbsp;
                                                                <asp:Label ID="lblAnswerType" runat="server" Font-Bold="false">ANSWER_TYPE</asp:Label>&nbsp;
                                                            </td>
                                                            <td colspan="1" align="left">
                                                                <asp:DropDownList ID="ddlanswerType" runat="server" TabIndex="5" 
                                                                    Width="210px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td nowrap align="right" colspan="1">
                                                                &nbsp;
                                                                <asp:Label ID="lblImpactsClaim" runat="server" Font-Bold="False">IMPACTS_CLAIM</asp:Label>&nbsp;
                                                            </td>
                                                            <td colspan="1" rowspan="1" align="left">
                                                                <asp:DropDownList ID="ddlImpactsClaim" TabIndex="7" runat="server" Width="210px">
                                                                </asp:DropDownList>
                                                            </td>
                                                            <td align="right" colspan="1">
                                                                &nbsp;
                                                                <asp:Label ID="lblAttribute" runat="server" Font-Bold="false">ATTRIBUTE</asp:Label>&nbsp;
                                                            </td>
                                                            <td colspan="1" align="left">
                                                                <asp:DropDownList ID="ddlAttribute" TabIndex="8" runat="server" Width="210px">
                                                                </asp:DropDownList>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td nowrap align="right" colspan="1"  style="vertical-align: top" >
                                                                &nbsp;
                                                                <asp:Label ID="lblCustomerMessage" runat="server" Font-Bold="false">CUSTOMER_MESSAGE</asp:Label>&nbsp;
                                                            </td>
                                                            <td colspan="1" rowspan="1" align="left" style="vertical-align: top" >
                                                                <asp:TextBox ID="txtCustomerMessage" TextMode="MultiLine" Rows="5" TabIndex="9" runat="server" Width="300px" Height="100px"  CssClass="FLATTEXTBOX" >
                                                                </asp:TextBox>
                                                            </td>
                                                            <td align="right" style="vertical-align: top" >
                                                                 <asp:Label ID="lblSearchTags" runat="server" Font-Bold="false">SEARCH_TAGS</asp:Label>&nbsp;
                                                            </td>
                                                            <td align="left" style="vertical-align: top" >
                                                                <asp:TextBox ID="txtSearchTags" TabIndex="9" runat="server" Width="210px" Height="100px" TextMode="MultiLine" Rows="5"
                                                                    CssClass="FLATTEXTBOX"  ></asp:TextBox>
                                                            </td>
                                                        </tr>
                                                        <tr>
                                                            <td colspan="4">
                                                                &nbsp;
                                                            </td>
                                                        </tr>
                                                    </table>
                                                </asp:Panel>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="4" valign="top">
                                                <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0" />
                                                <asp:HiddenField ID="hdnDisabledTab" runat="server" Value="" />
                                                <div id="tabs" class="style-tabs-old" style="border:none;">
                                                        <ul>
                                                        <li style="background:#d5d6e4"><a href="#tabAnswer"><asp:Label ID="Label4" runat="server" CssClass="tabHeaderTextOld">ANSWER</asp:Label></a></li>                                                                
                                                        </ul>
                                                        <div id="tabAnswer" style="background:#d5d6e4;border:1px solid; border-color:black;">

                                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                            <tr>
                                                                <td>&#160;&#160;
                                                                    <asp:Label ID="lblPageSize" runat="server">PAGE_SIZE</asp:Label>&nbsp; 
                                                                    <asp:DropDownList ID="ddlPageSize" TabIndex="8" runat="server" Width="50px" AutoPostBack="true"><asp:ListItem Value="5">5</asp:ListItem><asp:ListItem Selected="True" Value="10">10</asp:ListItem><asp:ListItem Value="15">15</asp:ListItem><asp:ListItem Value="20">20</asp:ListItem><asp:ListItem Value="25">25</asp:ListItem><asp:ListItem Value="30">30</asp:ListItem><asp:ListItem Value="35">35</asp:ListItem><asp:ListItem Value="40">40</asp:ListItem><asp:ListItem Value="45">45</asp:ListItem><asp:ListItem Value="50">50</asp:ListItem></asp:DropDownList>
                                                                </td>
                                                            </tr>
                                                            <tr>
                                                                <td height="250px" valign="top">
                                                                <asp:GridView ID="GVAnswers" runat="server" Width="100%" OnRowCreated="GVAnswers_ItemCreated"
                                                                                OnRowCommand="GVAnswers_RowCommand" AllowPaging="False" PageSize="50" AllowSorting="True"
                                                                                CellPadding="1"  AutoGenerateColumns="False" CssClass="DATAGRID" EmptyDataRowStyle-Wrap="True"><SelectedRowStyle CssClass="SELECTED"></SelectedRowStyle><EditRowStyle CssClass="EDITROW"></EditRowStyle><AlternatingRowStyle Wrap="False" CssClass="ALTROW"></AlternatingRowStyle><RowStyle CssClass="ROW"></RowStyle><HeaderStyle CssClass="HEADER"></HeaderStyle><Columns><asp:TemplateField ShowHeader="false"><ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle><ItemTemplate><asp:ImageButton ID="EditButton_WRITE" Style="cursor: hand" runat="server" ImageUrl="~/Navigation/images/icons/edit2.gif"
                                                                                                visible="true" CommandName="EditRecord" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton></ItemTemplate></asp:TemplateField><asp:TemplateField ShowHeader="false"><ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle><ItemTemplate><asp:ImageButton Style="cursor: hand;" ID="DeleteButton_WRITE" ImageUrl="~/Navigation/images/icons/trash.gif"
                                                                                                runat="server" CommandName="DeleteRecord" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton></ItemTemplate></asp:TemplateField><asp:TemplateField Visible="False"><ItemTemplate><asp:Label ID="lblAnswerID" Text='<%#GetGuidStringFromByteArray(Container.DataItem("ANSWER_ID"))%>'
                                                                                                runat="server"> </asp:Label></ItemTemplate></asp:TemplateField><asp:TemplateField Visible="True" HeaderText="ORDER"><ItemStyle HorizontalAlign="center" Width="6%"></ItemStyle><ItemTemplate><asp:Label ID="lblAnswerOrder" Text='<%#Container.DataItem("ANSWER_ORDER")%>'
                                                                                                runat="server"> </asp:Label></ItemTemplate><EditItemTemplate><asp:TextBox ID="txtAnswerOrder" runat="server" CssClass="FLATTEXTBOX" Visible="True" Width="90"></asp:TextBox></EditItemTemplate></asp:TemplateField>
                                                                                                <asp:TemplateField Visible="True" HeaderText="CODE"><ItemStyle HorizontalAlign="center" Width="8%"></ItemStyle><ItemTemplate><asp:Label ID="lblCode" Text='<%#Container.DataItem("CODE")%>' runat="server"></asp:Label></ItemTemplate>
                                                                                                <EditItemTemplate><asp:Label ID="lblAnswerCode" runat="server"  Visible="True" Width="80%"></asp:Label><asp:ImageButton id="btnClearCode"  Width="20%" OnClick="btnClearCode_Click" runat="server"  ImageUrl="../Navigation/images/icons/clear_icon.gif" ></asp:ImageButton></EditItemTemplate></asp:TemplateField>
                                                                                                <asp:TemplateField Visible="True" HeaderText="VALUE"><ItemStyle HorizontalAlign="center" Width="17%"></ItemStyle><ItemTemplate><asp:Label ID="lblAnswerValue" Text='<%#Container.DataItem("ANSWER_VALUE")%>'
                                                                                                runat="server"> </asp:Label></ItemTemplate><EditItemTemplate><asp:TextBox ID="txtAnswerValue" AutoPostBack="true" OnTextChanged="Answer_Value_TextChanged" runat="server" CssClass="FLATTEXTBOX" Visible="True" Width="95%"></asp:TextBox></EditItemTemplate></asp:TemplateField><asp:TemplateField Visible="True" HeaderText="DESCRIPTION"><ItemStyle HorizontalAlign="center" Width="19%"></ItemStyle><ItemTemplate><asp:Label ID="lblDescription" Text='<%#Container.DataItem("DESCRIPTION")%>'
                                                                                                runat="server"> </asp:Label></ItemTemplate><EditItemTemplate><asp:TextBox ID="txtDescription" runat="server" CssClass="FLATTEXTBOX" Visible="True" Width="95%"></asp:TextBox></EditItemTemplate></asp:TemplateField><asp:TemplateField Visible="True" HeaderText="SUPPORTS_CLAIM"><ItemStyle HorizontalAlign="center" Width="8%"></ItemStyle><ItemTemplate><asp:Label ID="lblSupports_Claim" Text='<%#Container.DataItem("SUPPORTS_CLAIM_ID")%>'
                                                                                                runat="server"> </asp:Label></ItemTemplate><EditItemTemplate><asp:DropDownList ID="ddlSupports_Claim" runat="server"  Visible="True" Width="75"></asp:DropDownList></EditItemTemplate></asp:TemplateField><asp:TemplateField Visible="True" HeaderText="SCORE"><ItemStyle HorizontalAlign="center" Width="4%"></ItemStyle><ItemTemplate><asp:Label ID="lblScore" Text='<%#Container.DataItem("SCORE")%>'
                                                                                                runat="server"> </asp:Label></ItemTemplate><EditItemTemplate><asp:TextBox ID="txtScore" runat="server" Visible="True" CssClass="FLATTEXTBOX" Width="95%"></asp:TextBox></EditItemTemplate></asp:TemplateField><asp:TemplateField Visible="True" HeaderText="EFFECTIVE_DATE"><ItemStyle HorizontalAlign="center" Width="16%"></ItemStyle><ItemTemplate><asp:Label ID="lblEffective" Text='<%#Container.DataItem("EFFECTIVE")%>'
                                                                                                runat="server"> </asp:Label></ItemTemplate><EditItemTemplate><asp:TextBox ID="txtEffective" runat="server" CssClass="FLATTEXTBOX" Visible="True" Width="90%"></asp:TextBox><asp:imagebutton id="btnEffectiveDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:imagebutton></EditItemTemplate></asp:TemplateField><asp:TemplateField Visible="True" HeaderText="EXPIRATION_DATE"><ItemStyle HorizontalAlign="center" Width="16%"></ItemStyle><ItemTemplate><asp:Label ID="lblExpiration" Text='<%#Container.DataItem("EXPIRATION")%>'
                                                                                                runat="server"> </asp:Label></ItemTemplate><EditItemTemplate><asp:TextBox ID="txtExpiration" runat="server" CssClass="FLATTEXTBOX" Visible="True" Width="90%"></asp:TextBox><asp:imagebutton id="btnExpirationDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:imagebutton></EditItemTemplate></asp:TemplateField></Columns><PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle></asp:GridView>
                                                                </td>
                                                                </tr>
                                                                <tr>
                                                                    <td>
                                                                        <asp:Button ID="btnNewAnswer" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                                                        cursor: hand; background-repeat: no-repeat" TabIndex="200" runat="server" Font-Bold="false"
                                                                        Width="81px" CssClass="FLATBUTTON" Height="20px" Text="New"></asp:Button>&nbsp;&nbsp; <asp:Button ID="btnSaveAnswer" Style="background-image: url(../Navigation/images/icons/save_icon.gif);
                                                                        cursor: hand; background-repeat: no-repeat" TabIndex="190" runat="server" Font-Bold="false"
                                                                        Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Save"></asp:Button>&nbsp; <asp:Button ID="btnCancelAnswer" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
                                                                        cursor: hand; background-repeat: no-repeat" TabIndex="195" runat="server" Font-Bold="false"
                                                                        Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Cancel"></asp:Button>&nbsp; 
                                                                    </td>
                                                                </tr>
                                                                </table>
                                                        </div>
                                                 </div>
                                            </td>
                                        </tr>
                                    </table>
                                </asp:Panel>
                            </td>
                        </tr>
                        <tr>
                            <td align="left" colspan="4">
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
                                    Width="81px" CssClass="FLATBUTTON" Height="20px" Text="New"></asp:Button>&nbsp;&nbsp;
                                <asp:Button ID="btnCopy_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" TabIndex="205" runat="server" Width="136px"
                                    Height="20px" CssClass="FLATBUTTON" Text="NEW_WITH_COPY" CausesValidation="False">
                                </asp:Button>
                                <asp:Button ID="btnDelete_WRITE" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" TabIndex="210" runat="server" Font-Bold="false"
                                    Width="100px" CssClass="FLATBUTTON" Height="20px" Text="Delete"></asp:Button>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
                <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
                       runat="server" designtimedragdrop="261"/>
            </td>
        </tr>
    </table>
    </form>
</body>
</html>