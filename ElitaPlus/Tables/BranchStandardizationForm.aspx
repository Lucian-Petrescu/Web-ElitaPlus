<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master"
    CodeBehind="BranchStandardizationForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.BranchStandardizationForm" %>

<%@ Register Src="../Common/MultipleColumnDDLabelControl.ascx" TagName="MultipleColumnDDLabelControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <style type="text/css">
        tr.HEADER th
        {
            text-align: center;
        }
    </style>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td valign="top" colspan="2">
                <table id="tblSearch" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                    border-left: #999999 1px solid; border-bottom: #999999 1px solid; height: 24px"
                    cellspacing="0" cellpadding="4" rules="cols" width="100%" align="center" bgcolor="#f1f1f1"
                    border="0">
                    <tr>
                        <td>
                            <table id="Table1" cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td>
                                        &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                    </td>
                                    <td nowrap align="left" colspan="3">
                                        <uc1:MultipleColumnDDLabelControl ID="moDealerMultipleDrop" runat="server" />
                                    </td>
                                </tr>
                                <tr>
                                    <td colspan="4">
                                        <hr>
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td nowrap align="left">
                                        &nbsp; &nbsp; &nbsp; &nbsp; &nbsp;&nbsp;
                                        <asp:Label ID="moDealerBranchCodeLabel" runat="server">Dealer_Branch_Code:</asp:Label>&nbsp;
                                    </td>
                                    <td nowrap align="left">
                                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:Label ID="moBranchCodeLabel" runat="server">Assurant_Branch_Code:</asp:Label>
                                    </td>
                                    <td nowrap align="left">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td nowrap align="left">
                                        &nbsp; &nbsp; &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                                        <asp:TextBox ID="moExternalBranchCodeText" runat="server" Width="65%" CssClass="FLATTEXTBOX"></asp:TextBox>&nbsp;
                                    </td>
                                    <td nowrap align="left">
                                        &nbsp;&nbsp;&nbsp;&nbsp;<asp:DropDownList ID="moDropdownBranch" runat="server" Width="85%">
                                        </asp:DropDownList>
                                    </td>
                                    <td nowrap align="left" width="58%">
                                    </td>
                                </tr>
                                <tr>
                                    <td>
                                        &nbsp;
                                    </td>
                                    <td style="text-align: right;" colspan="4">
                                        <asp:Button ID="moBtnClearSearch" CssClass="FLATBUTTON BUTTONSTYLE_CLEAR" runat="server"
                                            Text="Clear"></asp:Button>&nbsp;
                                        <asp:Button ID="moBtnSearch" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_SEARCH"
                                            Text="Search"></asp:Button>
                                    </td>
                                </tr>
                                <tr>
                                    <td style="height: 5px;" colspan="4">
                                    </td>
                                </tr>
                            </table>
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
                <asp:Label ID="lblPageSize" runat="server">Page_Size:</asp:Label>&nbsp;
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
                </asp:DropDownList>
            </td>
            <td style="text-align: right">
                <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="Grid" runat="server" Width="100%" OnRowCreated="RowCreated" OnRowCommand="RowCommand"
                    AllowPaging="True" AllowSorting="True" CellPadding="1" AutoGenerateColumns="False"
                    CssClass="DATAGRID">
                    <SelectedRowStyle CssClass="SELECTED"></SelectedRowStyle>
                    <EditRowStyle CssClass="EDITROW"></EditRowStyle>
                    <AlternatingRowStyle Wrap="False" CssClass="ALTROW"></AlternatingRowStyle>
                    <RowStyle CssClass="ROW"></RowStyle>
                    <HeaderStyle CssClass="HEADER"></HeaderStyle>
                    <Columns>
                        <asp:TemplateField>
                            <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                            <ItemTemplate>
                                <asp:ImageButton ID="EditButton_WRITE" Style="cursor: hand" runat="server" ImageUrl="../Navigation/images/icons/edit2.gif"
                                    CommandName="EditRecord" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemStyle HorizontalAlign="Center" Width="5%"></ItemStyle>
                            <ItemTemplate>
                                <asp:ImageButton ID="DeleteButton_WRITE" Style="cursor: hand" runat="server" ImageUrl="../Navigation/images/icons/trash.gif"
                                    CommandName="DeleteRecord" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="moBranchstandardizationIdLabel" runat="server" Text='<%# GetGuidStringFromByteArray(Container.DataItem("BRANCH_STANDARDIZATION_ID"))%>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="True" HeaderText="DEALER_NAME">
                            <ItemStyle HorizontalAlign="Left" Width="30%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="moDealerNameLabelGrid" runat="server" Text='<%# Container.DataItem("DEALER_NAME")%>'
                                    Visible="True">
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="moDealerNameDropGrid" runat="server" AutoPostBack="True" Visible="True"
                                    OnSelectedIndexChanged="moDealerNameDropGrid_SelectedIndexChanged">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="True" HeaderText="DEALER_BRANCH_CODE">
                            <ItemStyle HorizontalAlign="Left" Width="30%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="moDealerBranchCodeLabelGrid" runat="server" Visible="True" Text='<%# Container.DataItem("DEALER_BRANCH_CODE")%>'>
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox ID="moDealerBranchCodeTextGrid" CssClass="FLATTEXTBOX_TAB" runat="server"
                                    width="80%" Visible="True"></asp:TextBox> <%-- REQ-976 Added width to textbox--%>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="True" HeaderText="ASSURANT_BRANCH_CODE">
                            <ItemStyle HorizontalAlign="Left" Width="30%"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="moAsssurantBranchCodeLabelGrid" runat="server" Visible="True" Text='<%# Container.DataItem("BRANCH_CODE")%>'>
                                </asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList ID="moAsssurantBranchCodeDropGrid" runat="server" Visible="True">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <%--<PagerSettings PageButtonCount="15" Mode="Numeric" />--%>
                    <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
                </asp:GridView>
            </td>
        </tr>
        <%--</table>--%>
        <%--</td>--%>
        <%-- </tr>--%>
    </table>
    <input id="HiddenDeletePromptResponse" type="hidden" runat="server" designtimedragdrop="261"/>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPanelButtons" runat="server" ID="cntButtons">
    <asp:Button ID="moBtnAdd_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
        Width="100px" Text="New" Height="20px" CssClass="FLATBUTTON"></asp:Button><span style="font-size: 7pt;
            font-family: Verdana"> </span>
    <asp:Button ID="moBtnSave_WRITE" Style="background-image: url(../Navigation/images/icons/save_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
        Width="100px" Text="Save" Height="20px" CssClass="FLATBUTTON"></asp:Button><span>&nbsp; </span>
    <asp:Button ID="moBtnCancel" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
        Width="100px" Text="Cancel" Height="20px" CssClass="FLATBUTTON"></asp:Button><span>&nbsp;</span>
</asp:Content>
