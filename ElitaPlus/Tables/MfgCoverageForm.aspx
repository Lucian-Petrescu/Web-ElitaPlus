<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master"
    CodeBehind="MfgCoverageForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.MfgCoverageForm"
    Title="Untitled Page" %>

<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td valign="top">
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td valign="top" colspan="2">
                            <table id="tblSearch" style="border-right: #999999 1px solid; border-top: #999999 1px solid;
                                border-left: #999999 1px solid; width: 100%; border-bottom: #999999 1px solid;
                                height: 76px" cellspacing="0" cellpadding="6" rules="cols" width="100%" align="center"
                                bgcolor="#f1f1f1" border="0">
                                <tr>
                                    <td>
                                        <table cellspacing="0" cellpadding="0" width="100%" border="0">
                                            <tr>
                                                <td>
                                                    <asp:Label ID="Label1" runat="server">MANUFACTURER</asp:Label>:
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label2" runat="server">RISK_TYPE</asp:Label>:
                                                </td>
                                                <td>
                                                    <asp:Label ID="Label3" runat="server">MODEL</asp:Label>:
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                    <asp:DropDownList ID="cboManufacturer" runat="server" Width="150px" AutoPostBack="False">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:DropDownList ID="cboRiskType" runat="server" Width="150px">
                                                    </asp:DropDownList>
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtModel" runat="server" Width="150px"></asp:TextBox>
                                                </td>
                                                <td>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td>
                                                </td>
                                                <td style="text-align: right">
                                                    <asp:Button ID="ClearButton" CssClass="FLATBUTTON BUTTONSTYLE_CLEAR" runat="server"
                                                        Text="Clear"></asp:Button>&nbsp;&nbsp;&nbsp;
                                                    <asp:Button ID="SearchButton" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_SEARCH"
                                                        Text="Search"></asp:Button>
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="4" height="10px">
                                                </td>
                                            </tr>
                                        </table>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" colspan="2">
                            <hr style="height: 1px">
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                        </td>
                    </tr>
                    <tr id="trPageSize" runat="server">
                        <td style="height: 22px; text-align: left; vertical-align: top">
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
                        <td valign="top" align="center" colspan="2">
                            <asp:DataGrid ID="Grid" runat="server" Width="100%" OnItemCommand="ItemCommand" OnItemCreated="ItemCreated"
                                BorderStyle="Solid" BackColor="#DEE3E7" BorderColor="#999999" CellPadding="1"
                                AllowSorting="True" BorderWidth="1px" AutoGenerateColumns="False" AllowPaging="True">
                                <SelectedItemStyle Wrap="False" BackColor="LavenderBlush"></SelectedItemStyle>
                                <EditItemStyle Wrap="False" BackColor="AliceBlue"></EditItemStyle>
                                <AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
                                <ItemStyle Wrap="False" BackColor="White"></ItemStyle>
                                <HeaderStyle></HeaderStyle>
                                <Columns>
                                    <asp:TemplateColumn>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="EditButton_WRITE" Style="cursor: hand" runat="server" ImageUrl="../Navigation/images/icons/edit2.gif"
                                                CommandName="EditRecord"></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn>
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:ImageButton Style="cursor: hand;" ID="DeleteButton_WRITE" ImageUrl="../Navigation/images/icons/trash.gif"
                                                runat="server" CommandName="DeleteRecord"></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:BoundColumn Visible="False" HeaderText="Id"></asp:BoundColumn>
                                    <asp:TemplateColumn SortExpression="manufacturer_name" HeaderText="MANUFACTURER">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        <ItemTemplate>
                                            ManufacturerInGridLabel
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="cboManufacturerInGrid" AutoPostBack="true" runat="server" Style="width: 135px;
                                                overflow: hidden">
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="RISK_TYPE" SortExpression="risk_type_english">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        <ItemTemplate>
                                            RiskTypeInGridLabel
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="cboRiskTypeInGrid" runat="server" Style="width: 130px; overflow: hidden">
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn SortExpression="model" HeaderText="MODEL">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        <ItemTemplate>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBoxGridModel" CssClass="FLATTEXTBOX_TAB" runat="server" MaxLength="30"
                                                Style="width: 155px; overflow: hidden"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="MFG_WARRANTY" SortExpression="mfg_warranty">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        <ItemTemplate>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBoxGridMfgWarranty" CssClass="FLATTEXTBOX_TAB" runat="server"
                                                Style="width: 45px; overflow: hidden" MaxLength="2"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="TYPE_OF_EQUIPMENT">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="cboEquipmentType" AutoPostBack="true" runat="server" Style="width: 90%; overflow: hidden">
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="EQUIPMENT">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:DropDownList ID="cboEquipment" runat="server" Style="width: 90%; overflow: hidden">
                                            </asp:DropDownList>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>
                                   <asp:TemplateColumn HeaderText="MFG_MAIN_PARTS_WARRANTY" SortExpression="mfg_main_parts_warranty">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                        <ItemTemplate>
                                        </ItemTemplate>
                                        <EditItemTemplate>
                                            <asp:TextBox ID="TextBoxGridMfgMainPartsWarranty" CssClass="FLATTEXTBOX_TAB" runat="server"
                                                Style="width: 45px; overflow: hidden" MaxLength="2"></asp:TextBox>
                                        </EditItemTemplate>
                                    </asp:TemplateColumn>
                                    <asp:TemplateColumn HeaderText="EXT_WARRANTY">
                                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                        <ItemTemplate>
                                            <asp:ImageButton ID="ExtWarrantyButton_WRITE" Style="cursor: hand" runat="server"
                                                ImageUrl="../Navigation/images/icons/edit2.gif" CommandName="ExtWarranty"></asp:ImageButton>
                                        </ItemTemplate>
                                    </asp:TemplateColumn>
                                </Columns>
                                <PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7"
                                    PageButtonCount="15" Mode="NumericPages"></PagerStyle>
                            </asp:DataGrid>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <asp:Button ID="NewButton_WRITE" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_NEW"
        Text="NEW"></asp:Button>
    <asp:Button ID="SaveButton_WRITE" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_SAVE"
        Text="SAVE" TabIndex="600"></asp:Button>
    <asp:Button ID="CancelButton" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_UNDO"
        Text="Cancel"></asp:Button>
</asp:Content>
