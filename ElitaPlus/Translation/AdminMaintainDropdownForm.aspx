<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AdminMaintainDropdownForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Translation.AdminMaintainDropdownForm" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">
<html>
<head>
    <title>AdminMaintainDropdownForm</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Styles.css" type="text/css" rel="STYLESHEET">

    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>

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
                    <tr>
                        <td height="20">
                            <asp:Label ID="Label5" runat="server"  Cssclass="TITLELABEL">Admin</asp:Label>:
                            <asp:Label ID="Label6" runat="server" Cssclass="TITLELABELTEXT">Maintain Dropdowns</asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
    <table id="tblOuter2" style="border-right: black 1px solid; border-top: black 1px solid;
        margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid" height="93%"
        cellspacing="0" cellpadding="0" rules="none" width="98%" bgcolor="#d5d6e4" border="0"
        frame="void">
        <!--d5d6e4-->
        <tr>
            <td valign="top" align="center">
                <asp:Panel ID="WorkingPanel" runat="server" Height="98%">
                    <table cellspacing="0" cellpadding="0" width="100%" border="0">
                        <tr>
                            <td>
                                &nbsp;
                            </td>
                        </tr>
                    </table>
                    <table id="tblMain1" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                        border-left: #999999 1px solid; border-bottom: #999999 1px solid" cellspacing="0"
                        cellpadding="4" rules="cols" height="98%" width="98%" align="center" bgcolor="#fef9ea"
                        border="0">
                        <tr>
                            <td>
                                &nbsp;
                                <uc1:ErrorController ID="ErrorControl" runat="server" Visible="False"></uc1:ErrorController>
                            </td>
                        </tr>
                        <tr>
                            <td valign="top">
                                <table cellspacing="0" cellpadding="3" width="99%" border="0" align="center">
                                    <tr >
                                        <td align="right" width="20%" style="white-space:nowrap;">
                                            *
                                            <asp:Label ID="LabelNewProgCode" runat="server" Visible="true">Code</asp:Label>:
                                        </td>
                                        <td width="35%" style="white-space:nowrap;">
                                            <asp:TextBox ID="TextBoxNewProgCode" TabIndex="1" runat="server" Width="280"></asp:TextBox>&nbsp;
                                        </td>
                                        <td nowrap align="left" width="30%" rowspan="2" valign="middle">
                                            <asp:Button ID="bntAdd" TabIndex="3" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
                                                cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                                Width="90px" Text="Add" Height="20px" ToolTip="Add" CssClass="FLATBUTTON"></asp:Button>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="right" style="white-space:nowrap;">
                                            *
                                            <asp:Label ID="LabelDescription" runat="server" Visible="true">Description</asp:Label>:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="TextBoxDescription" TabIndex="2" runat="server" Width="280"></asp:TextBox>
                                        </td>
                                        <td nowrap>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" style="height: 14px" width="100%" colspan="3">
                                            <hr size="1" style="height: 1px">
                                        </td>
                                    </tr>
                                    <tr>
                                        <td align="center" valign="top" colspan="3">
                                            <asp:DataGrid ID="DataGridDropdowns" runat="server" Width="99%" AutoGenerateColumns="False"
                                                BorderStyle="Solid" BorderWidth="1px" BackColor="#DEE3E7" BorderColor="#999999"
                                                CellPadding="1" AllowPaging="True" AllowSorting="True" OnItemCreated="ItemCreated"
                                                PageSize="15" OnItemCommand="DataGridDropdowns_ItemCommand">
                                                <SelectedItemStyle Wrap="False"></SelectedItemStyle>
                                                <EditItemStyle Wrap="False"></EditItemStyle>
                                                <AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
                                                <ItemStyle Wrap="False" BackColor="White"></ItemStyle>
                                                <HeaderStyle ForeColor="#12135B"></HeaderStyle>
                                                <Columns>
                                                    <asp:TemplateColumn HeaderImageUrl="../Navigation/images/icons/check.gif">
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CheckBoxItemSel" runat="server"></asp:CheckBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn>
                                                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="5%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:ImageButton Style="cursor: hand;" ID="btnEdit" ImageUrl="../Navigation/images/icons/edit2.gif"
                                                                runat="server" CommandName="SelectAction"></asp:ImageButton>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="Code">
                                                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="30%"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:TextBox Width="100%" ID="TextBoxProgCode" runat="server" Text='<%# Container.DataItem("Code") %>'
                                                                CssClass="FLATTEXTBOX">
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="English">
                                                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="40%"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:TextBox ID="TextBoxEngTrans" runat="server" Text='<%# Container.DataItem("English_Translation") %>'
                                                                CssClass="FLATTEXTBOX" Width="100%">
                                                            </asp:TextBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn HeaderText="MAINTAINABLE_BY_USER">
                                                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="10%"></HeaderStyle>
                                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                        <ItemTemplate>
                                                            <asp:CheckBox ID="CheckBoxMaintainableByUser" runat="server" Checked='<%# Container.DataItem("MAINTAINABLE_BY_USER")="Y" %>'>
                                                            </asp:CheckBox>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" HeaderText="Items">
                                                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="10%"></HeaderStyle>
                                                        <ItemTemplate>
                                                            <asp:Button ID="btnView" runat="server" Text="View" CommandName="ItemsCMD"></asp:Button>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:TemplateColumn Visible="False">
                                                        <ItemTemplate>
                                                            <asp:Label ID="lblListId" runat="server" Visible="False" Text='<%# GetGuidStringFromByteArray(Container.DataItem("LIST_ID")) %>'>
                                                            </asp:Label>
                                                        </ItemTemplate>
                                                    </asp:TemplateColumn>
                                                    <asp:BoundColumn Visible="False" DataField="CODE"></asp:BoundColumn>
                                                    <asp:BoundColumn Visible="False" DataField="MAINTAINABLE_BY_USER"></asp:BoundColumn>
                                                    <asp:BoundColumn Visible="False" DataField="ENGLISH_TRANSLATION" SortExpression="ENGLISH_TRANSLATION">
                                                    </asp:BoundColumn>
                                                </Columns>
                                                <PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7"
                                                    PageButtonCount="15" Mode="NumericPages"></PagerStyle>
                                            </asp:DataGrid>
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td align="center" valign="bottom" width="100%" colspan="3">
                                <hr size="1">
                            </td>
                        </tr>
                        <tr>
                            <td align="left">
                                &nbsp;
                                <asp:Button ID="btnBack" Style="background-image: url(../Navigation/images/icons/back_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                    Width="90px" Text="Return" Height="20px" CssClass="FLATBUTTON"></asp:Button>&nbsp;
                                <asp:Button ID="btnSave" Style="background-image: url(../Navigation/images/icons/save_trans_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                    Width="90px" Text="Save" Height="20px" CssClass="FLATBUTTON"></asp:Button>&nbsp;
                                <asp:Button ID="btnDelete" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                    Width="90px" Text="Delete" Height="20px" CssClass="FLATBUTTON"></asp:Button>&nbsp;
                                <asp:Button ID="btnCancel" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
                                    cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                    Width="90px" Text="Cancel" Height="20px" CssClass="FLATBUTTON"></asp:Button>
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
