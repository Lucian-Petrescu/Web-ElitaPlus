<%@ Page Language="vb" ValidateRequest="false" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master"
    CodeBehind="AdminMaintainLabelTranslationForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Translation.AdminMaintainLabelTranslationForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">

    <script language="javascript">

    var arColumnMap= new Array();
    var n = 0;
	arColumnMap[":moTranslationTextGrid"] = ++n;
	arColumnMap["col"+n] = ":moTranslationTextGrid"
    </script>

    <table cellpadding="0" cellspacing="0" border="0" width="100%" style="font-size: 7.5pt;
        font-family: Verdana">
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
                                                <td style="width: 22%; vertical-align: middle; text-align: right;" >
                                                    <asp:Label ID="lblUiProgCode" runat="server" Visible="true">UI_PROG_CODE</asp:Label>:
                                                </td>
                                                <td style="width: 100%" align="left" colspan="2">
                                                    <asp:TextBox ID="txtUiProgCode" runat="server" Width="95%" CssClass="FLATTEXTBOX"></asp:TextBox>&nbsp;
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
            </td>
            <td style="text-align: right">
                <asp:Label ID="lblRecordCount" runat="server"  ></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:DataGrid ID="moDictionaryGrid" runat="server" Width="100%" OnItemCommand="ItemCommand"
                    OnItemCreated="ItemCreated" BorderStyle="Solid" BackColor="#DEE3E7" BorderColor="#999999"
                    CellPadding="1" AllowSorting="True" BorderWidth="1px" AutoGenerateColumns="False"
                    AllowPaging="True"   TabIndex="3">
                    <SelectedItemStyle Wrap="True" BackColor="LavenderBlush"></SelectedItemStyle>
                    <EditItemStyle Wrap="True" BackColor="AliceBlue"></EditItemStyle>
                    <AlternatingItemStyle Wrap="True" BackColor="#F1F1F1"></AlternatingItemStyle>
                    <ItemStyle Wrap="True" VerticalAlign="Bottom" BackColor="White"></ItemStyle>
                    <HeaderStyle></HeaderStyle>
                    <Columns>
                        <asp:TemplateColumn Visible="False">
                            <HeaderStyle Width="5%"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="moDictionaryItemIdLabel" runat="server" Text='<%# GetGuidStringFromByteArray(Container.DataItem("DICT_ITEM_TRANSLATION_ID"))%>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn SortExpression="TRANSLATION" HeaderText="TRANSLATION" ItemStyle-HorizontalAlign="Justify">
                            <HeaderStyle></HeaderStyle>
                            <ItemStyle></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="moTranslationTextGrid" Wrap="true" runat="server" Width="98%" Height="15px"
                                    Visible="True" TextMode="MultiLine" onmouseover="setHighlighter(this)" onfocus="setHighlighter(this)">
                                </asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn SortExpression="ENGLISH" HeaderText="LANGUAGE" ItemStyle-HorizontalAlign="Justify">
                            <HeaderStyle></HeaderStyle>
                            <ItemStyle></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="moLanguageLabelGrid" runat="server" Width="98%" Height="15px" Visible="True">
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                    </Columns>
                    <PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7"
                        PageButtonCount="15" Mode="NumericPages"></PagerStyle>
                </asp:DataGrid>
            </td>
        </tr>
    </table>
    <input id="HiddenSavePagePromptResponse" style="width: 8px;" type="hidden" size="1"
        name="HiddenSavePagePromptResponse" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPanelButtons" runat="server" ID="cntButtons">
    <span ></span>
    <asp:Button ID="btnBack" Style="background-image: url(../Navigation/images/icons/back_icon.gif);
        cursor: hand; background-repeat: no-repeat" TabIndex="185" runat="server" Font-Bold="false"
        Width="90px" Text="Return" Height="20px" CssClass="FLATBUTTON"></asp:Button>&nbsp;
    <asp:Button ID="moBtnSave_WRITE" Style="background-image: url(../Navigation/images/icons/save_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
        Width="100px" Text="Save" Height="20px" CssClass="FLATBUTTON" TabIndex="4"></asp:Button><span>&nbsp; </span>
    <asp:Button ID="moBtnCancel" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
        Width="100px" Text="Undo" Height="20px" CssClass="FLATBUTTON" TabIndex="5"></asp:Button><span >&nbsp;</span>
    <asp:Button ID="btnNew_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Width="100px" CssClass="FLATBUTTON"
        Height="20px" Text="New" Visible="false"></asp:Button>
</asp:Content>
