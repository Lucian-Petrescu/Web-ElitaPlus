﻿<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Page Language="vb" AutoEventWireup="false" Codebehind="QuestionListForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.QuestionListForm" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>TemplateForm</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR"/>
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE"/>
    <meta content="JavaScript" name="vs_defaultClientScript"/>
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema"/>
    <link href="../Styles.css" type="text/css" rel="STYLESHEET"/>

    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>

</head>
<body leftmargin="0" topmargin="0" onload="changeScrollbarColor();" border="0" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <!--Start Header-->
        <table style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px;
            border-left: black 1px solid; border-bottom: black 1px solid" cellspacing="0"
            cellpadding="0" width="98%" bgcolor="#d5d6e4" border="0">
            <tr>
                <td valign="top">
                                                <asp:Label ID="LabelTables0" runat="server" 
                        Cssclass="TITLELABEL">Admin</asp:Label>
                                                &nbsp;:<asp:Label ID="Label7" runat="server" 
                        Cssclass="TITLELABELTEXT">Question</asp:Label>
                                            </td>
            </tr>
        </table>
        <!--d5d6e4-->
        <table id="tblOuter2" style="border-right: black 1px solid; border-top: black 1px solid;
            margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid" height="93%"
            cellspacing="0" cellpadding="0" rules="none" width="98%" bgcolor="#d5d6e4" border="0">
            <tr>
                <td>
                    &nbsp;</td>
            </tr>
            <tr>
                <td valign="top" align="center" height="100%">
                    <asp:Panel ID="WorkingPanel" runat="server">
                        <table id="tblMain1" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                            border-left: #999999 1px solid; border-bottom: #999999 1px solid" height="98%"
                            cellspacing="0" cellpadding="6" rules="cols" width="98%" align="center" bgcolor="#fef9ea"
                            border="0">
                            <tr>
                                <td height="1">
                                    &nbsp;</td>
                            </tr>
                            <tr>
                                <td style="height: 1px" align="center">
                                    <table border="0" width="100%">
                                        <tr>
                                            <td height="20">
                                                &nbsp;</td>
                                        </tr>
                                    </table>
                                    <uc1:ErrorController ID="ErrorCtrl" runat="server"></uc1:ErrorController>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                        <tr>
                                            <td valign="top" colspan="2">
                                                <table id="tblSearch" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                                                    border-left: #999999 1px solid; width: 98%; border-bottom: #999999 1px solid;
                                                    height: 76px" cellspacing="0" cellpadding="6" width="98%" align="center" bgcolor="#f1f1f1"
                                                    border="0">
                                                    <tr>
                                                        <td valign="top">
                                                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                                                <tr>
                                                                    <td style="height: 12px" nowrap align="left" width="10%">
                                                                        <asp:Label ID="lblCode" runat="server">CODE</asp:Label>:
                                                                    </td>
                                                                    <td style="height: 12px" nowrap align="left" width="15%">
                                                                        <asp:Label ID="lblDescription" runat="server">DESCRIPTION</asp:Label>:
                                                                    </td>
                                                                    <td style="height: 12px" nowrap align="left" width="15%">
                                                                        <asp:Label ID="lblSearchTags" runat="server">SEARCH_TAGS</asp:Label>:
                                                                    </td>
                                                                    <td style="height: 12px" nowrap align="left" width="15%">
                                                                        <asp:Label ID="lblQuestionType" runat="server">QUESTION_TYPE</asp:Label>:
                                                                    </td>
                                                                    <td style="height: 12px" nowrap align="left" width="15%">
                                                                        <asp:Label ID="lblIssue" runat="server">ISSUE</asp:Label>:
                                                                    </td>
                                                                    <td style="height: 12px" nowrap align="left" width="15%">
                                                                        <asp:Label ID="lblExpirationdate" runat="server">ACTIVE_ON</asp:Label>:
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td nowrap align="left">
                                                                        <asp:TextBox ID="txtCode" runat="server" Width="75%" CssClass="FLATTEXTBOX_TAB"
                                                                            AutoPostBack="False"></asp:TextBox>
                                                                    </td>
                                                                    <td nowrap align="left">
                                                                        <asp:TextBox ID="txtDescription" runat="server" Width="75%" CssClass="FLATTEXTBOX_TAB"
                                                                            AutoPostBack="False"></asp:TextBox>
                                                                    </td>
                                                                    <td nowrap align="left">
                                                                        <asp:TextBox ID="txtSearchTags" runat="server" Width="75%" CssClass="FLATTEXTBOX_TAB"
                                                                            AutoPostBack="False"></asp:TextBox>
                                                                    </td>
                                                                    <td nowrap align="left">
                                                                         <asp:DropDownList ID="ddlQuestionType" runat="server" Width="80%" CssClass="FLATTEXTBOX_TAB"
                                                                            AutoPostBack="True">
                                                                        </asp:DropDownList>
                                                                    </td>
                                                                    <td nowrap align="left">
                                                                        <asp:TextBox ID="txtIssue" runat="server" Width="75%" CssClass="FLATTEXTBOX_TAB"
                                                                            AutoPostBack="False"></asp:TextBox>
                                                                    </td>
                                                                    <td nowrap align="left">
                                                                        <asp:TextBox ID="txtActiveOn" runat="server" Width="75%" CssClass="FLATTEXTBOX_TAB"
                                                                            AutoPostBack="False"></asp:TextBox>
                                                                              &nbsp;
                                                                            <asp:ImageButton ID="imgActiveOn" TabIndex="2" runat="server" Visible="True"
                                                                                ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:ImageButton>
                                                                    </td>
                                                                </tr>
                                                                <tr>
                                                                    <td nowrap align="left">
                                                                    </td>
                                                                    <td nowrap align="left">
                                                                    </td>
                                                                    <td nowrap align="left">
                                                                    </td>
                                                                    <td nowrap align="left">
                                                                    </td>
                                                                    <td nowrap align="left">
                                                                    </td>
                                                                    <td nowrap align="right">
                                                                        <asp:Button ID="btnClearSearch" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
                                                                            cursor: hand; background-repeat: no-repeat" runat="server" Width="90px" CssClass="FLATBUTTON"
                                                                            Height="20px" Text="Clear" OnClick="btnClearSearch_Click"></asp:Button>&nbsp;&nbsp;
                                                                        <asp:Button ID="btnSearch" Style="background-image: url(../Navigation/images/icons/search_icon.gif);
                                                                            cursor: hand; background-repeat: no-repeat" runat="server" Width="90px" CssClass="FLATBUTTON"
                                                                            Height="20px" Text="Search"></asp:Button>
                                                                    </td>
                                                                </tr>
                                                            </table>
                                                        </td>
                                                    </tr>
                                                </table>
                                            </td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <hr style="height: 1px">
                                            </td>
                                        </tr>
                                        <tr id="trPageSize" runat="server">
                                            <td valign="top" align="left">
                                                <asp:Label ID="Label3" runat="server">Page_Size</asp:Label>: &nbsp;
                                                <asp:DropDownList ID="cboPageSize" runat="server" Width="50px" AutoPostBack="true">
                                                    <asp:ListItem Value="5">5</asp:ListItem>
                                                    <asp:ListItem Selected="True" Value="10">10</asp:ListItem>
                                                    <asp:ListItem Value="15">15</asp:ListItem>
                                                    <asp:ListItem Value="20">20</asp:ListItem>
                                                    <asp:ListItem Value="25">25</asp:ListItem>
                                                    <asp:ListItem Value="30">30</asp:ListItem>
                                                    <asp:ListItem Value="35">35</asp:ListItem>
                                                    <asp:ListItem Value="40">40</asp:ListItem>
                                                    <asp:ListItem Value="45">45</asp:ListItem>
                                                    <asp:ListItem Value="50">50</asp:ListItem>
                                                </asp:DropDownList></td>
                                            <td align="right">
                                                <asp:Label ID="lblRecordCount" runat="server"></asp:Label></td>
                                        </tr>
                                        <tr>
                                            <td colspan="2">
                                                <asp:DataGrid ID="Grid" runat="server" Width="100%" AutoGenerateColumns="False" BorderStyle="Solid"
                                                    BorderWidth="1px" BackColor="#DEE3E7" BorderColor="#999999" CellPadding="1" AllowPaging="True"
                                                    AllowSorting="True" OnItemCreated="ItemCreated" OnItemCommand="ItemCommand">
                                                    <SelectedItemStyle Wrap="False" BackColor="LavenderBlush"></SelectedItemStyle>
                                                    <EditItemStyle Wrap="False" BackColor="AliceBlue"></EditItemStyle>
                                                    <AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
                                                    <ItemStyle Wrap="False" BackColor="White"></ItemStyle>
                                                    <HeaderStyle></HeaderStyle>
                                                    <Columns>
                                                        <asp:TemplateColumn>
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="5%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:ImageButton Style="cursor: hand;" ID="btnEdit" ImageUrl="../Navigation/images/icons/edit2.gif"
                                                                    runat="server" CommandName="SelectAction"></asp:ImageButton>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:BoundColumn Visible="False" HeaderText="Id"></asp:BoundColumn>
                                                        <asp:BoundColumn SortExpression="CODE" HeaderText="CODE">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="10%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn SortExpression="DESCRIPTION" HeaderText="DESCRIPTION">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="20%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:BoundColumn>                                                        
                                                        <asp:BoundColumn SortExpression="QUESTION_TYPE" HeaderText="QUESTION_TYPE">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="15%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:BoundColumn>                                                        
                                                        <asp:BoundColumn SortExpression="EFFECTIVE_DATE" HeaderText="EFFECTIVE_DATE">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="15%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:BoundColumn>
                                                        <asp:BoundColumn SortExpression="EXPIRATION_DATE" HeaderText="EXPIRATION_DATE">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="15%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                        </asp:BoundColumn>
                                                    </Columns>
                                                    <PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7"
                                                        PageButtonCount="15" Mode="NumericPages"></PagerStyle>
                                                </asp:DataGrid></td>
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
                                    
                                </td>
                            </tr>
                            <tr>
                                <td align="left" valign="bottom">
                                                                <hr /><br />
                                    <asp:Button ID="btnAdd_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                        Width="100px" CssClass="FLATBUTTON" Height="20px" Text="New"></asp:Button>&nbsp;
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>