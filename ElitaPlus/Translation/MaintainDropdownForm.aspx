<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MaintainDropdownForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Translation.MaintainDropdownForm" %>

<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>

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
<body leftmargin="0" topmargin="0" onload="changeScrollbarColor();" border="0" ms_positioning="GridLayout">
    <form id="Form1" method="post" runat="server">
        <!--Start Header-->
        <table style="border-right: black 1px solid; border-top: black 1px solid; margin: 5px; border-left: black 1px solid; border-bottom: black 1px solid"
            cellspacing="0" cellpadding="0" width="98%" bgcolor="#d5d6e4" border="0">
            <tr>
                <td valign="top">
                    <table width="100%" border="0">
                        <tr>
                            <td height="20">&nbsp;
									<asp:Label ID="Label5" runat="server" CssClass="TITLELABEL">Tables</asp:Label>:
									<asp:Label ID="Label6" runat="server" CssClass="TITLELABELTEXT">Maintain Dropdowns</asp:Label></td>
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
                                <%--<TD>&nbsp;</TD>--%>
                            </tr>

                        </table>
                        <table id="tblMain1" style="border-right: #999999 1px solid; border-top: #999999 1px solid; border-left: #999999 1px solid; border-bottom: #999999 1px solid"
                            cellspacing="0" cellpadding="4" rules="cols" width="98%" height="98%" align="center" bgcolor="#fef9ea"
                            border="0">
                            <tr>
                                <td><%--&nbsp;--%>
                                    <uc1:ErrorController ID="ErrorController" runat="server" Visible="False"></uc1:ErrorController>
                                </td>
                            </tr>
                            <tr>
                                <td align="center" valign="top">
                                    <table cellspacing="0" cellpadding="0" width="98%" border="0">
                                        <tr>
                                            <td align="center" valign="top">
                                                <%--&nbsp;--%>
                                                <uc1:MultipleColumnDDLabelControl ID="moCompanyMultipleDrop" runat="server"></uc1:MultipleColumnDDLabelControl>

                                            </td>
                                        </tr>
                                        <tr>
                                            <td valign="top">&nbsp;</td>
                                        </tr>
                                        <caption>
                                            &#160;
												<tr>
                                                    <td>
                                                        <asp:DataGrid ID="DataGridDropdowns" runat="server" AllowPaging="True"
                                                            AllowSorting="True" AutoGenerateColumns="False" BackColor="#DEE3E7"
                                                            BorderColor="#999999" BorderStyle="Solid" BorderWidth="1px" CellPadding="1"
                                                            OnItemCommand="DataGridDropdowns_ItemCommand" OnItemCreated="ItemCreated"
                                                            PageSize="17" Width="100%">
                                                            <SelectedItemStyle Wrap="False"></SelectedItemStyle>


                                                            <EditItemStyle Wrap="False"></EditItemStyle>


                                                            <AlternatingItemStyle BackColor="#F1F1F1" Wrap="False"></AlternatingItemStyle>


                                                            <ItemStyle BackColor="White" Wrap="False"></ItemStyle>


                                                            <HeaderStyle></HeaderStyle>


                                                            <Columns>
                                                                <asp:TemplateColumn Visible="False">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblListId" runat="server"
                                                                            Text='<%# GetGuidStringFromByteArray(Container.DataItem("LIST_ID")) %>'
                                                                            Visible="False">
                                                                        </asp:Label>
                                                                    </ItemTemplate>


                                                                </asp:TemplateColumn>
                                                                <asp:BoundColumn DataField="ENGLISH_TRANSLATION" HeaderText="English">
                                                                    <HeaderStyle ForeColor="#12135B" HorizontalAlign="Center"></HeaderStyle>


                                                                </asp:BoundColumn>
                                                                <asp:TemplateColumn HeaderText="Language Description">
                                                                    <HeaderStyle ForeColor="#12135B" HorizontalAlign="Center" Width="50%"></HeaderStyle>


                                                                    <ItemTemplate>
                                                                        <asp:TextBox ID="TextBoxLangTrans" runat="server" CssClass="FLATTEXTBOX"
                                                                            Text='<%# Container.DataItem("LANG_TRANSLATION") %>' Width="100%">
                                                                        </asp:TextBox>
                                                                    </ItemTemplate>


                                                                </asp:TemplateColumn>
                                                                <asp:ButtonColumn ButtonType="PushButton" CommandName="ItemsCMD"
                                                                    HeaderText="Items" Text="View">
                                                                    <HeaderStyle ForeColor="#12135B" HorizontalAlign="Center"></HeaderStyle>


                                                                    <ItemStyle HorizontalAlign="Center"></ItemStyle>


                                                                </asp:ButtonColumn>
                                                                <asp:BoundColumn DataField="CODE" Visible="False"></asp:BoundColumn>
                                                                <asp:BoundColumn DataField="DICT_ITEM_TRANSLATION_ID"
                                                                    SortExpression="LANG_TRANSLATION" Visible="False"></asp:BoundColumn>
                                                                <asp:TemplateColumn Visible="False">
                                                                    <ItemTemplate>
                                                                        <asp:Label ID="lblDictItemTransId" runat="server"
                                                                            Text='<%# GetGuidStringFromByteArray(Container.DataItem("DICT_ITEM_TRANSLATION_ID")) %>'
                                                                            Visible="False">
                                                                        </asp:Label>
                                                                    </ItemTemplate>


                                                                </asp:TemplateColumn>
                                                            </Columns>


                                                            <PagerStyle BackColor="#DEE3E7" ForeColor="DarkSlateBlue"
                                                                HorizontalAlign="Center" Mode="NumericPages" PageButtonCount="15"></PagerStyle>


                                                        </asp:DataGrid></td>
                                                </tr>
                                        </caption>
                                    </table>
                                </td>
                            </tr>
                        </table>
                    </asp:Panel>
                </td>
            </tr>
            <tr>
                <td align="center" valign="bottom" width="100%">
                    <hr size="1"><hr/>
                </td>
            </tr>
            <tr>
                <td align="left">&#160;
										<asp:Button ID="btnBack" runat="server" CssClass="FLATBUTTON"
                                            Font-Bold="false" Height="20px"
                                            Style="background-image: url(../Navigation/images/icons/back_icon.gif); cursor: hand; background-repeat: no-repeat"
                                            TabIndex="185" Text="Return" Width="90px"></asp:Button>&#160;
										<asp:Button ID="btnSave_WRITE" runat="server" CssClass="FLATBUTTON"
                                            Font-Bold="false" Height="20px"
                                            Style="background-image: url(../Navigation/images/icons/save_trans_icon.gif); cursor: hand; background-repeat: no-repeat"
                                            Text="Save" Width="90px"></asp:Button>&#160;
										<asp:Button ID="btnCancel_WRITE" runat="server"
                                            CssClass="FLATBUTTON" Font-Bold="false" Height="20px"
                                            Style="background-image: url(../Navigation/images/icons/cancel_icon.gif); cursor: hand; background-repeat: no-repeat"
                                            Text="Cancel" Width="90px"></asp:Button></td>
            </tr>
        </table>
     
    </form>
</body>
</html>
