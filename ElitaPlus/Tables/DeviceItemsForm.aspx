<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DeviceItemsForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.DeviceItemsForm" %>

<!DOCTYPE HTML PUBLIC "-//W3C//DTD HTML 4.0 Transitional//EN">

<html>
<head>
    <title>DeviceItemsForm</title>
    <meta content="Microsoft Visual Studio.NET 7.0" name="GENERATOR">
    <meta content="Visual Basic 7.0" name="CODE_LANGUAGE">
    <meta content="JavaScript" name="vs_defaultClientScript">
    <meta content="http://schemas.microsoft.com/intellisense/ie5" name="vs_targetSchema">
    <link href="../Styles.css" type="text/css" rel="STYLESHEET">
    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>
</head>
<body leftmargin="0" topmargin="0" ms_positioning="GridLayout" onload="changeScrollbarColor();" border="0">
    <form id="Form1" method="post" runat="server">
        <!--Start Header-->
        <table style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid"
            cellspacing="0" cellpadding="0" width="98%" bgcolor="#d5d6e4" border="0">
            <tr>
                <td valign="top">
                    <table width="100%" border="0">
                        <tr>
                            <td>
                                <asp:Label ID="Label5" runat="server" CssClass="TITLELABEL">DEVICE_TYPES</asp:Label>:
									<asp:Label ID="Label6" runat="server" CssClass="TITLELABELTEXT">Dropdown Items</asp:Label></td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <table id="tblOuter2" style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid"
            height="93%" cellspacing="0" cellpadding="0" rules="none" width="98%" bgcolor="#d5d6e4"
            border="0" frame="void">
            <!--d5d6e4-->
            <tr>
                <td valign="top" align="center">
                    <asp:Panel ID="WorkingPanel" runat="server" Height="98%">
                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                            <tr>
                                <td>&nbsp;</td>
                            </tr>
                        </table>
                        <table id="tblMain1" style="border-right: #999999 1px solid; border-top: #999999 1px solid; border-left: #999999 1px solid; border-bottom: #999999 1px solid"
                            cellspacing="0" cellpadding="4" rules="cols" height="98%" width="98%" align="center" bgcolor="#fef9ea"
                            border="0">
                            <tr>
                                <td valign="top">&nbsp;
										<uc1:ErrorController ID="ErrorControl" runat="server" Visible="False"></uc1:ErrorController>
                                </td>
                            </tr>
                            <tr>
                                <td valign="top">
                                    <table cellspacing="0" cellpadding="0" width="99%" border="0">
                                        <%--<TR>
												<TD noWrap align="right">
													<asp:Label id="Label2" runat="server" Width="9px">Dropdown Name</asp:Label>:</TD>
												<TD colSpan="2">
													<asp:Label id="DropdownName" runat="server"  ForeColor="#12135B" Visible="false" Width="386px">DropdownName</asp:Label></TD>
											<TR>
												<TD colSpan="3">&nbsp;</TD>
											</TR>--%>
                                        <tr>
                                            <td nowrap align="right" width="20%">*<asp:Label ID="LabelNewProgCode" runat="server">Code</asp:Label>:&nbsp;
                                            </td>
                                            <td width="35%">
                                                <asp:TextBox ID="TextBoxNewProgCode" runat="server" Width="280px" TabIndex="1"></asp:TextBox>&nbsp;
                                            </td>
                                            <td nowrap align="left" width="30%" rowspan="2" valign="middle">
                                                <asp:Button ID="bntAdd" Style="background-image: url(../Navigation/images/icons/add_icon.gif); cursor: hand; background-repeat: no-repeat"
                                                    runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" ToolTip="Add" Height="20px"
                                                    Text="Add" TabIndex="3"></asp:Button></td>
                                        </tr>
                                        <tr>
                                            <td align="right" style="white-space: nowrap;">*
                                                    <asp:Label ID="LabelDescription" runat="server" >Description</asp:Label>:&nbsp;
                                            </td>
                                            <td>
                                                <asp:TextBox ID="TextBoxDescription" runat="server" Width="280" TabIndex="2"></asp:TextBox>&nbsp;
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" width="100%" colspan="3">
                                                <hr size="1">
                                            </td>
                                        </tr>
                                        <tr>
                                            <td align="center" valign="top" colspan="3">
                                                <asp:DataGrid ID="DataGridDropdownItems" runat="server" Width="99%" AutoGenerateColumns="False"
                                                    BorderStyle="Solid" BorderWidth="1px" BackColor="#DEE3E7" BorderColor="#999999" CellPadding="1"
                                                    AllowPaging="True" AllowSorting="True" OnItemCreated="ItemCreated" PageSize="15" OnItemCommand="DataGridDropdownItems_ItemCommand">
                                                    <SelectedItemStyle Wrap="False"></SelectedItemStyle>
                                                    <EditItemStyle Wrap="False"></EditItemStyle>
                                                    <AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
                                                    <ItemStyle Wrap="False" BackColor="White"></ItemStyle>
                                                    <HeaderStyle></HeaderStyle>
                                                    <Columns>
                                                        <asp:TemplateColumn HeaderImageUrl="../Navigation/images/icons/check.gif">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B"></HeaderStyle>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="CheckBoxItemSel" runat="server"></asp:CheckBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="Code">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="28%"></HeaderStyle>
                                                            <ItemTemplate>
                                                                <asp:TextBox Width="100%" ID="TextBoxProgCode" runat="server" Text='<%# Container.DataItem("Code") %>' CssClass="FLATTEXTBOX">
                                                                </asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="English">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="40%"></HeaderStyle>
                                                            <ItemTemplate>
                                                                <asp:TextBox Width="100%" CssClass="FLATTEXTBOX" ID="TextBoxEngTrans" runat="server" Text='<%# Container.DataItem("English_Translation") %>'>
                                                                </asp:TextBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="MAINTAINABLE_BY_USER">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="12%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="CheckBoxMaintainableByUser" runat="server" Checked='<%# Container.DataItem("Maintainable_by_user")="Y" %>'></asp:CheckBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn HeaderText="DISPLAY_TO_USER">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="12%"></HeaderStyle>
                                                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:CheckBox ID="CheckBoxDisplayToUser" runat="server" Checked='<%# Container.DataItem("Display_to_user")="Y" %>'></asp:CheckBox>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn ItemStyle-HorizontalAlign="Center" HeaderText="Items">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="10%"></HeaderStyle>
                                                            <ItemStyle Width="5%" HorizontalAlign="Center"></ItemStyle>
                                                            <ItemTemplate>
                                                                <asp:Button ID="btnView" runat="server" Text="View" CommandName="ItemsCMD"></asp:Button>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn Visible="False">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B"></HeaderStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblListItemId" runat="server" Visible="False" Text='<%# GetGuidStringFromByteArray(Container.DataItem("LIST_ITEM_ID")) %>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:TemplateColumn Visible="False">
                                                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B"></HeaderStyle>
                                                            <ItemTemplate>
                                                                <asp:Label ID="lblListId" runat="server" Visible="False" Text='<%# GetGuidStringFromByteArray(Container.DataItem("LIST_ID")) %>'>
                                                                </asp:Label>
                                                            </ItemTemplate>
                                                        </asp:TemplateColumn>
                                                        <asp:BoundColumn Visible="False" DataField="CODE"></asp:BoundColumn>
                                                        <asp:BoundColumn Visible="False" DataField="MAINTAINABLE_BY_USER"></asp:BoundColumn>
                                                        <asp:BoundColumn Visible="False" DataField="DISPLAY_TO_USER"></asp:BoundColumn>
                                                        <asp:BoundColumn Visible="False" DataField="ENGLISH_TRANSLATION" SortExpression="ENGLISH_TRANSLATION"></asp:BoundColumn>
                                                    </Columns>
                                                    <PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7" PageButtonCount="15"
                                                        Mode="NumericPages"></PagerStyle>
                                                </asp:DataGrid>
                                            </td>
                                        </tr>
                                    </table>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" width="100%" valign="bottom">
                                    <hr size="1">
                                </td>
                            </tr>
                            <tr>
                                <td align="left">
                                    <asp:Button ID="btnSave" Style="background-image: url(../Navigation/images/icons/save_trans_icon.gif); cursor: hand; background-repeat: no-repeat"
                                        runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Save"></asp:Button>&nbsp;
										<asp:Button ID="btnDelete" Style="background-image: url(../Navigation/images/icons/clear_icon.gif); cursor: hand; background-repeat: no-repeat"
                                            runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Delete"></asp:Button>&nbsp;
										<asp:Button ID="btnCancel" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif); cursor: hand; background-repeat: no-repeat"
                                            runat="server" Font-Bold="false" Width="90px" CssClass="FLATBUTTON" Height="20px" Text="Cancel"></asp:Button></td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
        </table>
    </form>
</body>
</html>
