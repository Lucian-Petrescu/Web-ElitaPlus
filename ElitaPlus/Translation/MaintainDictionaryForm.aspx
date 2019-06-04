<%@ Page Language="vb" ValidateRequest="false" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master"
    CodeBehind="MaintainDictionaryForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Translation.MaintainDictionaryForm" %>
<%@ Register TagPrefix="uc2" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">

    <script language="javascript">

    var arColumnMap= new Array();
    var n = 0;
	arColumnMap[":moTranslationTextGrid"] = ++n;
	arColumnMap["col"+n] = ":moTranslationTextGrid"
    </script>

    <table cellpadding="0" cellspacing="0" border="0" width="100%" style="font-size: 7.5pt;
        font-family: Verdana">
        <%--<table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td valign="top" align="center" colspan="6">
                            &nbsp;
                            <uc2:MultipleColumnDDLabelControl ID="moCompanyMultipleDrop" runat="server">
                            </uc2:MultipleColumnDDLabelControl>
                        </td>
                    </tr>
                </table>--%>
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
                                         <td valign="top" align="center" colspan="6">
                                            &nbsp;
                                            <uc2:MultipleColumnDDLabelControl ID="moCompanyMultipleDrop" runat="server">
                                            </uc2:MultipleColumnDDLabelControl>
                                        </td>
                                            <!--fef9ea-->
                                            <%--<tr>
                                                <td style="width: 24%; vertical-align: middle;" align="right">
                                                    <asp:Label ID="Label1" runat="server" Visible="true">Company:</asp:Label>
                                                
                                                </td>
                                                <td> <asp:DropDownList ID="cboCompayDropDown" runat="server" Width="200px" AutoPostBack="true" 
                                                        >
                                                    </asp:DropDownList>
                                                 </td>
                                             </tr>    --%>
                                            <tr style="vertical-align: top;">
                                                
                                                <td style="width: 24%; vertical-align: middle;" align="right">
                                                    <asp:Label ID="Label4" runat="server" Visible="true">Search On:</asp:Label>
                                                
                                                </td>
                                                <td align="left">
                                                    <asp:RadioButtonList ID="rdbViewType" runat="server" Width="30%" Font-Bold="false"
                                                        OnSelectedIndexChanged="Index_Changed" AutoPostBack="true" RepeatDirection="Horizontal">
                                                        <asp:ListItem Value="TRANSLATION" Selected="True">TRANSLATION</asp:ListItem>
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
                    name="HiddenSavePagePromptResponse" runat="server"><input id="HiddenIsPageDirty"
                        style="width: 8px;" type="hidden" size="1" name="HiddenIsPageDirty" runat="server">
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
                                <asp:Label ID="moTranslationIdLabel" runat="server" Text='<%# GetGuidStringFromByteArray(Container.DataItem("DICT_ITEM_TRANSLATION_ID"))%>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn SortExpression="TRANSLATION" HeaderText="TRANSLATION" ItemStyle-HorizontalAlign="Justify">
                            <HeaderStyle></HeaderStyle>
                            <ItemStyle Width="50%"></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="moTranslationTextGrid" Wrap="true" runat="server" Width="98%" Height="15px"
                                    Visible="True" TextMode="MultiLine" onmouseover="setHighlighter(this)" onfocus="setHighlighter(this)">
                                </asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn SortExpression="ENGLISH" HeaderText="ENGLISH" ItemStyle-HorizontalAlign="Justify">
                            <HeaderStyle></HeaderStyle>
                            <ItemStyle Width="50%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="moEnglishLabelGrid" runat="server"  Height="15px" Visible="True">
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
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPanelButtons" runat="server" ID="cntButtons">
    <span ></span>
    <asp:Button ID="btnBack" Style="background-image: url(../Navigation/images/icons/back_icon.gif);
        cursor: hand; background-repeat: no-repeat" TabIndex="185" runat="server" Font-Bold="false"
        Width="90px" Text="Return" Height="20px" CssClass="FLATBUTTON"></asp:Button>&nbsp;
    <asp:Button ID="moBtnSave_WRITE" Style="background-image: url(../Navigation/images/icons/save_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
        Width="100px" Text="Save" Height="20px" CssClass="FLATBUTTON" TabIndex="4"></asp:Button>
        <span>&nbsp; </span>
    <asp:Button ID="moBtnCancel" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
        Width="100px" Text="Undo" Height="20px" CssClass="FLATBUTTON" TabIndex="5">
    </asp:Button><span >&nbsp;</span>

    <script>
	function setDirty(){
	    document.getElementById("<%=HiddenIsPageDirty.ClientId %>").value = "YES"
	}
    </script>

</asp:Content>
