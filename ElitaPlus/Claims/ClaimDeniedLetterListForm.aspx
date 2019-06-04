<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ClaimDeniedLetterListForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Claims.ClaimDeniedLetterListForm"
    MasterPageFile="~/Navigation/masters/content_default.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <table cellspacing="2" width="100%" align="center" border="0">
           
        <tr>
            <td align="center" width="100%">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr id="trPageSize" runat="server">
                        <td valign="top" align="left" width="85%">
                            <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>: &nbsp;
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
                        <td nowrap align="right" width="15%">
                            <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td align="center">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td align="center">
                            <asp:GridView ID="Grid" runat="server" Width="100%" OnRowCommand="ItemCommand" OnRowCreated="ItemCreated"
                                CellPadding="1" AllowSorting="True" AutoGenerateColumns="False" AllowPaging="True"
                                CssClass="DATAGRID">
                                <SelectedRowStyle CssClass="SELECTED"></SelectedRowStyle>
                                <EditRowStyle CssClass="EDITROW"></EditRowStyle>
                                <AlternatingRowStyle Wrap="False" CssClass="ALTROW"></AlternatingRowStyle>
                                <RowStyle CssClass="ROW"></RowStyle>
                                <HeaderStyle CssClass="HEADER"></HeaderStyle>
                                <Columns>
                                    <asp:TemplateField ShowHeader="false">
                                        <ItemStyle HorizontalAlign="Center" Width="3%" Height="15px" />
                                        <ItemTemplate>
                                            <asp:ImageButton ID="btnView" runat="server" CausesValidation="False" CommandName="Select"
                                                ImageUrl="~/Navigation/images/icons/edit2.gif" CommandArgument="<%# Container.DisplayIndex %>" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                     <asp:TemplateField HeaderText="DENIAL_REASON_CODE">
                                        <HeaderStyle HorizontalAlign="Center" Width="25%"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="DENIAL_REASON">
                                        <HeaderStyle HorizontalAlign="Center" Width="25%"></HeaderStyle>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="CREATED_DATE">
                                        <HeaderStyle HorizontalAlign="Center" Width="25%"></HeaderStyle>
                                    </asp:TemplateField>
                                    <%--<asp:TemplateField HeaderText="STATUS_DATE">
                                        <HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
                                    </asp:TemplateField>--%>
                                    <asp:TemplateField Visible="False" HeaderText="Claim_Id"></asp:TemplateField>
                                </Columns>
                                <PagerSettings PageButtonCount="15" Mode="Numeric" />
                                <PagerStyle HorizontalAlign="Center" CssClass="PAGER" />
                            </asp:GridView>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPanelButtons" runat="server" ID="cntButtons">
    <span ></span>
   <asp:Button ID="btnBack_WRITE" Style="background-image: url(../Navigation/images/icons/back_icon.gif);
        cursor: hand; background-repeat: no-repeat" TabIndex="185" runat="server" Font-Bold="false"
        Width="90px" Text="Back" Height="20px" CssClass="FLATBUTTON"></asp:Button>&nbsp;
    <asp:Button ID="btnNew_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
        cursor: hand; background-repeat: no-repeat" TabIndex="185" runat="server" Font-Bold="false"
        Width="90px" Text="New" Height="20px" CssClass="FLATBUTTON"></asp:Button>&nbsp;
   </asp:Content>
