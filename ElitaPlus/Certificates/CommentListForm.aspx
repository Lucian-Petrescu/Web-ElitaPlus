<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CommentListForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.CommentListForm" %>

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
    <style type="text/css">
            .wrapText{white-space:pre-wrap;}
        </style>
</head>
<body leftmargin="0" topmargin="0" onload="changeScrollbarColor();" ms_positioning="GridLayout"
    border="0">
    <form id="Form1" method="post" runat="server">
    <!--Start Header-->
    <table style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px;
        border-left: black 1px solid; border-bottom: black 1px solid" cellspacing="0"
        cellpadding="0" width="98%" bgcolor="#d5d6e4" border="0">
        <tr>
            <td valign="top">
                <table width="100%" border="0">
                    <tr><td>
                        &nbsp;&nbsp;
                        <asp:Label ID="Label7" runat="server" CssClass="TITLELABELTEXT">Comments</asp:Label>
                     </td> 

                    </tr> </table>
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
            <td valign="top" align="center" height="100%">
                <asp:Panel ID="WorkingPanel" runat="server" Height="100px" Width="100%">
                    <table id="tblMain1" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                        border-left: #999999 1px solid; border-bottom: #999999 1px solid" cellspacing="0"
                        cellpadding="6" rules="cols" width="100%" align="center" bgcolor="#fef9ea" border="0">
                        <tr>
                            <td height="1">
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                    <tr>
                                        <td align="center" height="5">
                                            <uc1:ErrorController ID="ErrorCtrl" runat="server"></uc1:ErrorController>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="5">
                                            <asp:Label ID="LabelCertificate" runat="server">Certificate</asp:Label>:
                                            <asp:TextBox ID="TextboxCertificate" TabIndex="35" runat="server" CssClass="FLATTEXTBOX"></asp:TextBox>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="5">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
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
                                                    <asp:BoundColumn HeaderText="Time_Stamp">
                                                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="15%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn HeaderText="NAME_OF_CALLER">
                                                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="15%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn HeaderText="USER_NAME">
                                                        <HeaderStyle ForeColor="#12135B" Width="15%"></HeaderStyle>
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn HeaderText="Comments">
                                                        <HeaderStyle ForeColor="#12135B"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="wrapText"></ItemStyle>
                                                    </asp:BoundColumn>
                                                </Columns>
                                                <PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7"
                                                    PageButtonCount="15" Mode="NumericPages"></PagerStyle>
                                            </asp:DataGrid>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="5">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td height="5">
                                            <br/><asp:Label ID="LabelExtendedStatusComments" runat="server">EXTENDED_STATUS_COMMENTS</asp:Label>:
                                        </td>
                                    </tr>
                                    <tr>
                                        <td colspan="3">
                                            <asp:DataGrid ID="ExtGrid" runat="server" Width="100%" AutoGenerateColumns="False"
                                                BorderStyle="Solid" BorderWidth="1px" BackColor="#DEE3E7" BorderColor="#999999"
                                                CellPadding="1" AllowPaging="True" AllowSorting="True" OnItemCreated="ItemCreated"
                                                OnItemCommand="ItemCommand">
                                                <AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
                                                <ItemStyle Wrap="False" BackColor="White"></ItemStyle>
                                                <HeaderStyle></HeaderStyle>
                                                <Columns>
                                                    <asp:BoundColumn Visible="False" HeaderText="Id"></asp:BoundColumn>
                                                    <asp:BoundColumn HeaderText="Time_Stamp" DataField="created_Date">
                                                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="15%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn HeaderText="NAME_OF_CALLER" DataField="caller_name">
                                                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="15%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn HeaderText="USER_NAME" DataField="added_by">
                                                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="15%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                                    </asp:BoundColumn>
                                                    <asp:BoundColumn HeaderText="Comments" DataField="comments">
                                                        <HeaderStyle ForeColor="#12135B"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Left" Wrap="true" CssClass="wrapText"></ItemStyle>
                                                    </asp:BoundColumn>
                                                </Columns>
                                                <PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7"
                                                    PageButtonCount="15" Mode="NumericPages"></PagerStyle>
                                            </asp:DataGrid>
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
                                <hr>
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                <asp:Button ID="btnBack" Style="background-image: url(../Navigation/images/icons/back_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" TabIndex="185" runat="server" Font-Bold="false"
                                    Width="90px" CssClass="FLATBUTTON" Text="Back" Height="20px"></asp:Button>&nbsp;
                                <asp:Button ID="btnAdd_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                    Width="100px" CssClass="FLATBUTTON" Text="New" Height="20px"></asp:Button>&nbsp;
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
