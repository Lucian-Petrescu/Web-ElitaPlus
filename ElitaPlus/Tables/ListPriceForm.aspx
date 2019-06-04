<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master"
    CodeBehind="ListPriceForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.ListPriceForm" %>

<%@ Register Src="../Common/MultipleColumnDDLabelControl.ascx" TagName="MultipleColumnDDLabelControl"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
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
                                                <td align="left" valign="middle" colspan="4">
                                                    <uc1:MultipleColumnDDLabelControl ID="multipleDropControl" runat="server" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <hr style="height: 1px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: middle">
                                                    <asp:Label ID="lblSearchSKU" runat="server">SKU_NUMBER</asp:Label>:
                                                </td>
                                                <td style="vertical-align: middle">
                                                    <asp:Label ID="lblSearchManufacturer" runat="server">MANUFACTURER_NAME</asp:Label>:
                                                </td>
                                                <td style="vertical-align: middle;" colspan="2">
                                                    <asp:Label ID="lblSearchModel" runat="server">MODEL</asp:Label>:
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: middle;" align="left" width="20%">
                                                    <asp:TextBox ID="tbSearchSKU" runat="server" Columns="25" Width="65%" AutoPostBack="False"></asp:TextBox>
                                                </td>
                                                <td style="vertical-align: middle;" align="left" width="30%">
                                                    <asp:TextBox ID="tbSearchManufacturer" runat="server" Columns="35" AutoPostBack="False"></asp:TextBox>
                                                </td>
                                                <td style="vertical-align: middle; text-align: left" width="30%" align="left" colspan="2">
                                                    <asp:TextBox ID="tbSearchModel" runat="server" Columns="35" AutoPostBack="False"></asp:TextBox>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="vertical-align: middle" colspan="1">
                                                    <asp:Label ID="lblFromDate" runat="server">FROM_DATE</asp:Label>:
                                                </td>
                                                <td style="vertical-align: middle;" colspan="1">
                                                    <asp:Label ID="lblToDate" runat="server">TO_DATE</asp:Label>:
                                                </td>
                                                <td colspan="2">
                                                    <asp:Label ID="lblAmountType" runat="server">AMOUNT_TYPE</asp:Label>:
                                                </td>
                                            </tr>
                                            <tr>
                                                <td style="height: 18px;white-space:nowrap" align="left" colspan="1">
                                                    <asp:TextBox ID="txtFromDate" runat="server" Wrap="False"></asp:TextBox>
                                                    <asp:ImageButton ID="btnFromDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                                                    </asp:ImageButton>
                                                </td>
                                                <td style="height: 18px;white-space:nowrap" align="left" colspan="1">
                                                    <asp:TextBox ID="txtToDate" runat="server" Wrap="False"></asp:TextBox>
                                                    <asp:ImageButton ID="btnToDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                                                    </asp:ImageButton>
                                                </td>
                                                <td colspan="2" style="white-space:nowrap">
                                                    <asp:DropDownList ID="ddlAmountType" runat="server"></asp:DropDownList>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4">
                                                    <hr style="height: 1px" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td nowrap align="left">
                                                    &nbsp;
                                                </td>
                                                <td style="text-align: right" colspan="3">
                                                    <asp:Button ID="moBtnClear" runat="server" Width="90px" Text="Clear" CssClass="FLATBUTTON BUTTONSTYLE_CLEAR"
                                                        Height="20px"></asp:Button>&nbsp;
                                                    <asp:Button ID="moBtnSearch" runat="server" Width="90px" Text="Search" CssClass="FLATBUTTON BUTTONSTYLE_SEARCH"
                                                        Height="20px"></asp:Button>&nbsp;
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
                <asp:DataGrid ID="moListPriceGrid" runat="server" Width="100%" AllowPaging="True"
                    AutoGenerateColumns="False" AllowSorting="True" CssClass="DATAGRID">
                    <SelectedItemStyle CssClass="SELECTED"></SelectedItemStyle>
                    <EditItemStyle CssClass="EDITROW_WRAP"></EditItemStyle>
                    <AlternatingItemStyle CssClass="ALTROW"></AlternatingItemStyle>
                    <ItemStyle BackColor="White" CssClass="ROW"></ItemStyle>
                    <HeaderStyle CssClass="HEADER" HorizontalAlign="Center"></HeaderStyle>
                    <Columns>
                        <asp:TemplateColumn Visible="False" HeaderText="Id">
                            <ItemTemplate>
                                &gt;
                                <asp:Label ID="IdLabel" runat="server" Text='<%# GetGuidStringFromByteArray(Container.DataItem("LIST_PRICE_ID"))%>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="DEALER" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center">
                            <ItemTemplate>
                                <asp:Label ID="lblDealer" runat="server" Visible="True" Text='<%# Container.DataItem("DEALER_CODE")%>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="DEALER_NAME" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="lblDealerName" runat="server" Visible="True" Text='<%# Container.DataItem("DEALER_NAME")%>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="SKU_NUMBER" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-Wrap="false" SortExpression="SKU_NUMBER">
                            <ItemTemplate>
                                <asp:Label ID="lblSKUNum" runat="server" Visible="True" Text='<%# Container.DataItem("SKU_NUMBER")%>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="MANUFACTURER_NAME" ItemStyle-HorizontalAlign="Left"
                            HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false" SortExpression="MANUFACTURER_NAME">
                            <ItemTemplate>
                                <asp:Label ID="lblMfgName" runat="server" Visible="True" Text='<%# Container.DataItem("MANUFACTURER_NAME")%>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="MODEL" ItemStyle-HorizontalAlign="Left" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-Wrap="false" SortExpression="MODEL_NUMBER">
                            <ItemTemplate>
                                <asp:Label ID="lblMode" runat="server" Visible="True" Text='<%# Container.DataItem("MODEL_NUMBER")%>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="AMOUNT_TYPE" ItemStyle-HorizontalAlign="Center" HeaderStyle-HorizontalAlign="Center"
                            HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="lblAmountType" runat="server" Visible="True" Text='<%# Container.DataItem("amount_type_desc")%>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="AMOUNT" ItemStyle-HorizontalAlign="Right" HeaderStyle-HorizontalAlign="Center" HeaderStyle-Wrap="false">
                            <ItemTemplate>
                                <asp:Label ID="lblPrice" runat="server" Visible="True" Text='<%# Container.DataItem("AMOUNT")%>'>
                                </asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn HeaderText="Effective_Date">
                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="10%"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundColumn>
                        <asp:BoundColumn HeaderText="EXPIRATION_DATE">
                            <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="10%"></HeaderStyle>
                            <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        </asp:BoundColumn>
                    </Columns>
                    <PagerStyle CssClass="PAGER" PageButtonCount="15" Mode="NumericPages"></PagerStyle>
                </asp:DataGrid>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="cntButtons" runat="server" ContentPlaceHolderID="ContentPanelButtons">
</asp:Content>
