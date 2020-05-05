<%@ Page Language="vb" ValidateRequest="false" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master"
    CodeBehind="AdminLabelTransListForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Translation.AdminLabelTransListForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td valign="top" colspan="2">
                <table id="tblSearch" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                    border-left: #999999 1px solid; border-bottom: #999999 1px solid; height: 24px"
                    cellspacing="0" cellpadding="4" rules="cols" width="100%" align="center" bgcolor="#f1f1f1"
                    border="0">
                    <tr>
                        <td>
                            <table cellspacing="2" cellpadding="2" width="100%" align="center" border="0">
                                <tr>
                                    <td align="center" colspan="2">
                                        <table id="TABLE1" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                                            border-left: #999999 1px solid; width: 98%; border-bottom: #999999 1px solid"
                                            cellspacing="0" cellpadding="6" width="98%" align="center" bgcolor="#f1f1f1"
                                            border="0">
                                            <!--fef9ea-->
                                            <tr style="vertical-align: top;">
                                                <td style="width: 22%; vertical-align: middle;" align="right">
                                                    <asp:Label ID="Label4" runat="server" Visible="true">Search On</asp:Label>:
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="rdbViewType" runat="server" Width="60%" Font-Bold="false"
                                                        OnSelectedIndexChanged="Index_Changed" AutoPostBack="true" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="UI_PROG_CODE" Selected="True">UI_PROG_CODE</asp:ListItem>
                                                        <asp:ListItem Value=" English">ENGLISH</asp:ListItem>
                                                    </asp:RadioButtonList>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <%--<td style="width: 22%">
                                                </td>--%>
                                                <td style="width: 95%" align="left" colspan="2">
                                                    <asp:TextBox ID="txtSearchTrans" runat="server" Width="95%" CssClass="FLATTEXTBOX"></asp:TextBox>&nbsp;
                                                </td>
                                                <td nowrap>
                                                    <asp:Button ID="btnClear" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
                                                        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                                        Width="90px" CssClass="FLATBUTTON" ToolTip="Clear selection" Text="Clear" Height="20px"
                                                        TabIndex="2"></asp:Button>&nbsp;
                                                    <asp:Button ID="btnSearch" Style="background-image: url(../Navigation/images/icons/search_icon.gif);
                                                        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                                        Width="90px" CssClass="FLATBUTTON" ToolTip="Search for translation" Text="Search"
                                                        Height="20px" TabIndex="1"></asp:Button>
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="height: 7px;" colspan="2">
            </td>
        </tr>
        <tr id="trPageSize" runat="SERVER" visible="False">
            <td align="left">
                <asp:Label ID="lblPageSize" runat="server"  >Page_Size:</asp:Label>&nbsp;
                <asp:DropDownList ID="cboPageSize" runat="server" Width="50px" AutoPostBack="true"
                     >
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
                </asp:DropDownList>
                <input id="HiddenSavePagePromptResponse" style="width: 8px;" type="hidden" size="1"
                       name="HiddenSavePagePromptResponse" runat="server" />
            </td>
            <td style="text-align: right">
                <asp:Label ID="lblRecordCount" runat="server"  ></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:DataGrid ID="moDictionaryGrid" runat="server" Width="100%" AutoGenerateColumns="False"
                    BackColor="#DEE3E7" BorderColor="#999999" BorderStyle="Solid" CellPadding="1"
                    BorderWidth="1px" AllowPaging="True" AllowSorting="True" OnItemCreated="ItemCreated"
                    OnItemCommand="ItemCommand" CssClass="DATAGRID">
                    <SelectedItemStyle Wrap="False" BorderStyle="Solid" BorderColor="Orange" BackColor="LightSteelBlue" />
                    <EditItemStyle Wrap="False"></EditItemStyle>
                    <AlternatingItemStyle Wrap="False" BackColor="#F1F1F1" />
                    <ItemStyle Wrap="True" BackColor="White" HorizontalAlign="Center" />
                    <HeaderStyle  HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateColumn>
                            <HeaderStyle ForeColor="#12135B"></HeaderStyle>
                            <ItemStyle Width="1px"></ItemStyle>
                            <ItemTemplate>
                                <asp:ImageButton Style="cursor: hand;" ID="EditButton_WRITE" ImageUrl="../Navigation/images/icons/edit2.gif"
                                    runat="server" CommandName="SelectAction"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn Visible="False">
                            <HeaderStyle Width="5%"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="moDictItemIdLabel" runat="server" Text='<%# GetGuidStringFromByteArray(Container.DataItem("DICT_ITEM_ID"))%>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn SortExpression="UI_PROG_CODE" HeaderText="UI_PROG_CODE" ItemStyle-HorizontalAlign="Justify">
                            <HeaderStyle></HeaderStyle>
                            <ItemStyle></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="moUIProgcodeLabelGrid" runat="server" Width="98%" Height="15px" Visible="True">
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn SortExpression="ENGLISH" HeaderText="ENGLISH" ItemStyle-HorizontalAlign="Justify">
                            <HeaderStyle></HeaderStyle>
                            <ItemStyle></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="moEnglishLabelGrid" runat="server" Width="98%" Height="15px" Visible="True">
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                    <PagerStyle ForeColor="DarkSlateBlue" BackColor="#DEE3E7" PageButtonCount="15" Mode="NumericPages">
                    </PagerStyle>
                </asp:DataGrid>
            </td>
        </tr>
        <asp:Literal runat="server" ID="spanFiller"></asp:Literal>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <asp:Button ID="btnAdd_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
        Width="100px" CssClass="FLATBUTTON" Text="New" Height="20px"></asp:Button>&nbsp;
</asp:Content>
