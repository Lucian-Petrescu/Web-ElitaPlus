<%@ Page Language="vb" ValidateRequest="false" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master"
    CodeBehind="CodeMappingForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.CodeMappingForm" %>

<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">

    <script language="javascript">

    var arColumnMap= new Array();
    var n = 0;
	arColumnMap[":moTranslationTextGrid"] = ++n;
	arColumnMap["col"+n] = ":moTranslationTextGrid"
    </script>

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
                                    <td align="right">
                                        *&nbsp;<asp:Label ID="moCompanyLABEL" runat="server">COMPANY</asp:Label>:
                                    </td>
                                    <td>
                                        <uc1:MultipleColumnDDLabelControl ID="moUserCompanyMultipleDrop" runat="server">
                                        </uc1:MultipleColumnDDLabelControl>
                                    </td>
                                </tr>
                                <tr>
                                    <td align="right">
                                        *&nbsp;<asp:Label ID="lblListItem" runat="server" Text="LIST_ITEM"></asp:Label>:
                                    </td>
                                    <td>
                                        <uc1:MultipleColumnDDLabelControl ID="moMultipleColumnDDListItem" runat="server">
                                        </uc1:MultipleColumnDDLabelControl>
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="2">
                                        <hr />
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <%--</table>--%>
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
                <input id="Hidden1" style="width: 8px;" type="hidden" size="1" name="HiddenSavePagePromptResponse"
                       runat="server"/><input id="HiddenIsPageDirty" style="width: 8px;" type="hidden" size="1"
                                              name="HiddenIsPageDirty" runat="server"/>
            </td>
            <td style="text-align: right">
                <asp:Label ID="lblRecordCount" runat="server"  ></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:DataGrid ID="moListItemGrid" runat="server" runat="server" Width="100%" AutoGenerateColumns="False"
                    BackColor="#DEE3E7" BorderColor="#999999" BorderStyle="Solid" CellPadding="1"
                    BorderWidth="1px" AllowPaging="True"   AllowSorting="True"
                    OnItemCreated="ItemCreated" OnItemCommand="ItemCommand" TabIndex="3">
                 <%--   <SelectedItemStyle Wrap="False" BorderStyle="Solid" BorderColor="Orange" BackColor="LightSteelBlue" />--%>
                    <EditItemStyle Wrap="False"></EditItemStyle>
                    <AlternatingItemStyle Wrap="False" BackColor="#F1F1F1" />
                    <ItemStyle Wrap="True" BackColor="White" HorizontalAlign="Center" />
                    <HeaderStyle  HorizontalAlign="Center" />
                    <Columns>
                        <asp:TemplateColumn Visible="False">
                            <HeaderStyle Width="5%"></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="moListItemIdLabel" runat="server" Text='<%# GetGuidStringFromByteArray(Container.DataItem("LIST_ITEM_ID"))%>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn SortExpression="CODE" HeaderText="CODE" ItemStyle-HorizontalAlign="Justify">
                            <HeaderStyle></HeaderStyle>
                            <ItemStyle></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="moCodeLabelGrid" runat="server" Width="98%" Height="15px" Visible="True">
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn SortExpression="DESCRIPTION" HeaderText="DESCRIPTION" ItemStyle-HorizontalAlign="Justify">
                            <HeaderStyle></HeaderStyle>
                            <ItemStyle></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="moDescriptionLabelGrid" runat="server" Width="98%" Height="15px" Visible="True">
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn SortExpression="NEW_DESCRIPTION" HeaderText="NEW_DESCRIPTION"
                            ItemStyle-HorizontalAlign="Justify">
                            <HeaderStyle></HeaderStyle>
                            <ItemStyle></ItemStyle>
                            <ItemTemplate>
                                <asp:TextBox ID="moNewDescriptionTextGrid" Wrap="true" runat="server" Width="98%"
                                    Height="15px" Visible="True" onmouseover="setHighlighter(this)" onfocus="setHighlighter(this)">
                                </asp:TextBox>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn Visible="False">
                            <HeaderStyle></HeaderStyle>
                            <ItemTemplate>
                                <asp:Label ID="moCodeMappingIdLabel" runat="server" Visible="False">
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
           name="HiddenSavePagePromptResponse" runat="server"/>
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
        Width="100px" Text="Undo" Height="20px" CssClass="FLATBUTTON" TabIndex="5"></asp:Button><span>&nbsp;</span>

    <script>
	function setDirty(){
	    document.getElementById("<%=HiddenIsPageDirty.ClientId %>").value = "YES"
	}
    </script>

</asp:Content>
