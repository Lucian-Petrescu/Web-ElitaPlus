<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master"
    CodeBehind="FtpSiteListForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Security.FtpSiteListForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <style type="text/css">
        tr.HEADER th
        {
            text-align: center;
        }
    </style>
    <table cellspacing="2" cellpadding="2" width="100%" align="center" border="0">
        <tr>
            <td valign="top" colspan="2">
                <table id="moTableSearch" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                    border-left: #999999 1px solid; width: 98%; border-bottom: #999999 1px solid;
                    height: 76px" cellspacing="0" cellpadding="6" rules="cols" width="98%" align="center"
                    bgcolor="#f1f1f1" border="0">
                    <tr>
                        <td>
                            <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                <tr>
                                    <td valign="top" align="left">
                                        <table cellspacing="0" cellpadding="0" border="0" width="100%">
                                            <tr>
                                                <td align="left" valign="middle" colspan="2">
                                                    <asp:Label ID="moCodeLabel" runat="server">CODE</asp:Label>:&nbsp;
                                                    <asp:TextBox ID="moCodeText" runat="server" Width="300px" CssClass="FLATTEXTBOX"></asp:TextBox>
                                                </td>
                                                <td align="left" valign="middle" colspan="2">
                                                    <asp:Label ID="moDescriptionLabel" runat="server">DESCRIPTION</asp:Label>:&nbsp;
                                                    <asp:TextBox ID="moDescriptionText" runat="server" Width="300px" CssClass="FLATTEXTBOX"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    &nbsp;
                                                </td>
                                            </tr>
                                            <tr>
                                                <td nowrap align="right" colspan="2">
                                                </td>
                                                <td nowrap style="text-align: right" colspan="2">
                                                    <asp:Button ID="moBtnClear" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
                                                        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                                        Width="90px" Text="Clear" CssClass="FLATBUTTON" Height="20px" ></asp:Button>&nbsp;
                                                    <asp:Button ID="moBtnSearch" Style="background-image: url(../Navigation/images/icons/search_icon.gif);
                                                        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                                                        Width="90px" Text="Search" CssClass="FLATBUTTON" Height="20px"></asp:Button>&nbsp;
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
            <td colspan="2">
                <hr style="height: 1px" />
            </td>
        </tr>
        <tr id="trPageSize" runat="server">
            <td valign="top" align="left">
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
            <td style="height: 22px; text-align: right">
                <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="Grid" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="1"
                    AllowPaging="True" AllowSorting="True" CssClass="DATAGRID">
                    <SelectedRowStyle Wrap="False" CssClass="SELECTED" />
                    <EditRowStyle Wrap="False" CssClass="EDITROW" />
                    <AlternatingRowStyle Wrap="False" CssClass="ALTROW" />
                    <RowStyle Wrap="False" CssClass="ROW" />
                    <HeaderStyle CssClass="HEADER" />
                    <Columns>
                        <asp:TemplateField>
                            <HeaderStyle HorizontalAlign="Center" Width="5%"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:ImageButton ID="imgbtnEdit" runat="server" CommandName="SelectUser" ImageUrl="../Navigation/images/icons/edit2.gif">
                                </asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="False">
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                            <ItemTemplate>
                                <asp:Label ID="moFtpSiteId" Text='<%# GetGuidStringFromByteArray(Container.DataItem("FTP_SITE_ID")) %>'
                                    runat="server">
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        
                        <asp:TemplateField SortExpression="CODE" HeaderText="CODE">
                            <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        </asp:TemplateField>
                       
                        <asp:TemplateField SortExpression="DESCRIPTION" HeaderText="DESCRIPTION">
                            <ItemStyle HorizontalAlign="Left" Width="40%"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        </asp:TemplateField>
                       
                        <asp:TemplateField SortExpression="HOST" HeaderText="HOST">
                            <ItemStyle HorizontalAlign="Left" Width="45%"></ItemStyle>
                            <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings PageButtonCount="15" Mode="Numeric" />
                    <PagerStyle HorizontalAlign="Center" CssClass="PAGER" />
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <asp:Button ID="btnNew_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Width="90px" Text="New"
        CssClass="FLATBUTTON" Height="20px" CausesValidation="False"></asp:Button>
</asp:Content>
