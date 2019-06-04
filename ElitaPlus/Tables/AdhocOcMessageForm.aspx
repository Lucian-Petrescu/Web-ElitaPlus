<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AdhocOcMessageForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.AdhocOcMessageForm" EnableSessionState="True" Theme="Default"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" %>
<%@ Register TagPrefix="Elita" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl_New.ascx" %>
<%@ Register TagPrefix="mytab" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register Assembly="Microsoft.Web.UI.WebControls" Namespace="Microsoft.Web.UI.WebControls" TagPrefix="iewc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.6.1.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table width="100%" border="0" class="searchGrid">
        <tr>
            <td align="left" style="height: 40px" width="30%" nowrap="nowrap">
                <table width="1%">
                    <Elita:MultipleColumnDDLabelControl ID="DealerMultipleDrop" runat="server" OnSelectedDropChanged="DealerMultipleDrop_SelectedDropChanged" />
                </table>
            </td>
        </tr>
        <tr>
            <td align="left" style="height: 40px" width="30%" nowrap="nowrap">
                <table width="1%">
                    <Elita:MultipleColumnDDLabelControl ID="TemplateMultipleDrop" runat="server" />
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto">
        <Scripts>
            <asp:ScriptReference Path="~/Navigation/scripts/ComunaSuggest.js" />
        </Scripts>
    </asp:ScriptManager>
    <div class="dataContainer">
        <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse" runat="server" />
        <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0" />
        <asp:HiddenField ID="hdnDisabledTab" runat="server" />
        <div id="tabs" class="style-tabs">
	        <ul>
		        <li><a href="#tab_Parameters"><asp:Label ID="lblParametersTab" runat="server" CssClass="tabHeaderText">PARAMETERS_TAB</asp:Label></a></li>
		        <li><a href="#tab_Recipients"><asp:Label ID="lblRecipientsTab" runat="server" CssClass="tabHeaderText">RECIPIENTS_TAB</asp:Label></a></li>
	        </ul>
            <div id="tab_Parameters">
                <div class="Page" runat="server" style="height: 100%; overflow: auto">
                    <asp:GridView ID="ParametersGrid" runat="server" Width="100%" OnRowCreated="ParametersGrid_RowCreated"
                        OnRowCommand="ParametersGrid_RowCommand" AllowPaging="True" AllowSorting="False" CellPadding="1"
                        AutoGenerateColumns="False" SkinID="DetailPageGridView">
                        <SelectedRowStyle Wrap="False"></SelectedRowStyle>
                        <EditRowStyle Wrap="False"></EditRowStyle>
                        <AlternatingRowStyle Wrap="False"></AlternatingRowStyle>
                        <RowStyle Wrap="False"></RowStyle>
                        <Columns>
                            <asp:TemplateField Visible="False" HeaderText="OC_TEMPLATE_PARAMS_ID">
                                <ItemTemplate>
                                    <asp:Label ID="IdLabel" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PARAM_NAME">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblParamName" runat="server" Visible="True">
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtParamName" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PARAM_VALUE_SOURCE_XCD">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblParamValueSource" runat="server" Visible="True">
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="cboParamValueSource" runat="server"></asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PARAM_VALUE">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblParamValue" runat="server" Visible="True">
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtParamValue" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PARAM_DATA_TYPE_XCD">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblParamDataType" runat="server" Visible="True">
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="cboParamDataType" runat="server"></asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DATE_FORMAT_STRING">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblDateFormatString" runat="server" Visible="True">
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDateFormatString" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ALLOW_EMPTY_VALUE_XCD">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblAllowEmptyValue" runat="server" Visible="True">
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="cboAllowEmptyValue" runat="server"></asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="30px" Wrap="False"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                <ItemTemplate>
                                    <asp:ImageButton ID="EditButton_WRITE" Style="cursor: hand" runat="server" ImageUrl="../Navigation/images/edit.png"
                                        CommandName="EditRecord" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="BtnCancel" runat="server" CommandName="CancelRecord" CommandArgument="<%#Container.DisplayIndex%>"
                                        Text="Cancel"></asp:LinkButton>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="30px" Wrap="False"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                <ItemTemplate>
                                    <asp:ImageButton Style="cursor: hand;" ID="DeleteButton_WRITE" ImageUrl="../Navigation/images/icon_delete.png"
                                        runat="server" CommandName="DeleteRecord" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Button ID="BtnSave_WRITE" runat="server" CommandName="SaveRecord" CommandArgument="<%#Container.DisplayIndex %>"
                                        Text="Save" SkinID="PrimaryRightButton"></asp:Button>
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                        <PagerStyle />
                    </asp:GridView>
                    <br />
                    <input id="HiddenDeletePromptResponse" type="hidden" runat="server" designtimedragdrop="261" />
                    <asp:Button ID="btnNewParameter_WRITE" runat="server" CausesValidation="False" Text="NEW_PARAMETER"
                        SkinID="PrimaryLeftButton"></asp:Button>
                </div>
            </div>
            <div id="tab_Recipients">
                <div class="Page" runat="server" style="height: 100%; overflow: auto">
                    <asp:GridView ID="RecipientsGrid" runat="server" Width="100%" OnRowCreated="RecipientsGrid_RowCreated"
                        OnRowCommand="RecipientsGrid_RowCommand" AllowPaging="True" AllowSorting="False" CellPadding="1"
                        AutoGenerateColumns="False" SkinID="DetailPageGridView">
                        <SelectedRowStyle Wrap="False"></SelectedRowStyle>
                        <EditRowStyle Wrap="False"></EditRowStyle>
                        <AlternatingRowStyle Wrap="False"></AlternatingRowStyle>
                        <RowStyle Wrap="False"></RowStyle>
                        <Columns>
                            <asp:TemplateField Visible="False" HeaderText="OC_TEMPLATE_RECIPIENT_ID">
                                <ItemTemplate>
                                    <asp:Label ID="IdLabel" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="RECIPIENT_SOURCE_FIELD_XCD">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblRecipientSourceField" runat="server" Visible="True">
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="cboRecipientSourceField" runat="server"></asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="RECIPIENT_ADDRESS">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblRecipientAddress" runat="server" Visible="True">
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtRecipientAddress" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="DESCRIPTION">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblDescription" runat="server" Visible="True">
                                    </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtDescription" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="30px" Wrap="False"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                <ItemTemplate>
                                    <asp:ImageButton ID="EditButton_WRITE" Style="cursor: hand" runat="server" ImageUrl="../Navigation/images/edit.png"
                                        CommandName="EditRecord" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:LinkButton ID="BtnCancel" runat="server" CommandName="CancelRecord" CommandArgument="<%#Container.DisplayIndex%>"
                                        Text="Cancel"></asp:LinkButton>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="30px" Wrap="False"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                <ItemTemplate>
                                    <asp:ImageButton Style="cursor: hand;" ID="DeleteButton_WRITE" ImageUrl="../Navigation/images/icon_delete.png"
                                        runat="server" CommandName="DeleteRecord" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:Button ID="BtnSave_WRITE" runat="server" CommandName="SaveRecord" CommandArgument="<%#Container.DisplayIndex %>"
                                        Text="Save" SkinID="PrimaryRightButton"></asp:Button>
                                </EditItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                        <PagerStyle />
                    </asp:GridView>
                    <br />
                    <asp:Button ID="btnNewRecipient_WRITE" runat="server" CausesValidation="False" Text="NEW_RECIPIENT"
                        SkinID="PrimaryLeftButton"></asp:Button>
                </div>
            </div>
        </div>
    </div>
    <div class="btnZone">
        <asp:Button ID="btnApply_WRITE" runat="server" CausesValidation="False" Text="SEND"
            SkinID="PrimaryRightButton"></asp:Button>
        <asp:Button ID="btnBack" runat="server" CausesValidation="False" Text="BACK" SkinID="AlternateLeftButton"></asp:Button>
    </div>
</asp:Content>
