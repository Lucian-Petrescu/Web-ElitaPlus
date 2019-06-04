<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AttributeForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.AttributeForm" MasterPageFile="../Navigation/masters/ElitaBase.Master"
    EnableSessionState="True" Theme="Default" %>

<asp:Content ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script src="../Navigation/scripts/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table id="TableFixed" cellspacing="0" cellpadding="0" border="0" width="80%" class="summaryGrid">
        <tbody>
            <tr>
                <td>
                    <asp:Label ID="moTableNameLabel" runat="server">TABLE_NAME</asp:Label>:&nbsp;
                    <asp:Label ID="moTableName" runat="server">TABLE_NAME</asp:Label>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
<asp:Content ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:Panel runat="server" ID="WorkingPanel">
        <div class="dataContainer">
            <table width="100%" border="0" class="dataGrid" cellpadding="0" cellspacing="0">
                <tr id="trPageSize" runat="server">
                    <td class="bor">
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
                    <td align="right" class="bor">
                        <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                        <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
                            runat="server" />
                    </td>
                </tr>
            </table>
            <div>
                <asp:GridView ID="moAttributeGridView" runat="server" Width="100%" AutoGenerateColumns="False"
                    AllowPaging="True" CellPadding="1" SkinID="DetailPageGridView">
                    <RowStyle HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="Code" SortExpression="UI_PROG_CODE">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="UiProgCodeLabel"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList runat="server" ID="UiProgCodeDropDown">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DESCRIPTION">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="DescriptionLabel"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList runat="server" ID="DescriptionDropDown">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DATA_TYPE" SortExpression="DATA_TYPE">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="DataTypeLabel"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList runat="server" ID="DataTypeDropDown">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="USE_EFFECTIVE_DATE" SortExpression="USE_EFFECTIVE_DATE">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="UseEffectiveDateLabel"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList runat="server" ID="UseEffectiveDateDropDown">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ALLOW_DUPLICATES" SortExpression="ALLOW_DUPLICATES">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="AllowDuplicatesLabel"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList runat="server" ID="AllowDuplicatesDropDown">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="EditButton" CommandName="EditRecord" AlternateText="Edit" ImageUrl="~/App_Themes/Default/Images/edit.png"
                                    CommandArgument='<%# Assurant.ElitaPlus.ElitaPlusWebApp.ElitaPlusPage.GetGuidStringFromByteArray(CType(Container.DataItem("ATTRIBUTE_ID"), Byte())) %>' />
                                <asp:ImageButton runat="server" ID="DeleteButton" CommandName="DeleteRecord" AlternateText="Delete" ImageUrl="~/App_Themes/Default/Images/icon_delete.png"
                                    CommandArgument='<%# Assurant.ElitaPlus.ElitaPlusWebApp.ElitaPlusPage.GetGuidStringFromByteArray(CType(Container.DataItem("ATTRIBUTE_ID"), Byte())) %>' />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:ImageButton runat="server" ID="SaveButton" AlternateText="Save" CommandName="SaveRecord" ImageUrl="~/App_Themes/Default/Images/save.png" />
                                <asp:ImageButton runat="server" ID="CancelButton" AlternateText="Cancel" CommandName="CancelRecord" ImageUrl="~/App_Themes/Default/Images/cancel.png" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings PageButtonCount="10" Mode="Numeric" Position="TopAndBottom" />
                </asp:GridView>
            </div>
        </div>
        <div class="btnZone">
            <asp:Button runat="server" ID="btnBack" Text="Back" SkinID="AlternateLeftButton" />
            <asp:Button ID="btnAdd" runat="server" SkinID="AlternateLeftButton" Text="Add_New" />
        </div>
    </asp:Panel>
</asp:Content>
