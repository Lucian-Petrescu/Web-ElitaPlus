<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="OcMessageForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.OcMessageForm" EnableSessionState="True" Theme="Default"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" %>
<%@ Register TagPrefix="Elita" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl_New.ascx" %>
<%@ Register TagPrefix="Elita" TagName="MessageController" Src="~/Common/MessageController.ascx" %>
<%@ Register TagPrefix="mytab" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register Assembly="Microsoft.Web.UI.WebControls" Namespace="Microsoft.Web.UI.WebControls" TagPrefix="iewc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.6.1.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
    <Elita:MessageController runat="server" ID="moMessageController" Visible="false" />
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto">
        <Scripts>
            <asp:ScriptReference Path="~/Navigation/scripts/ComunaSuggest.js" />
        </Scripts>
    </asp:ScriptManager>
    <div class="dataContainer">
        <div class="stepformZone">
            <table class="formGrid" border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px;" width="100%">
                <tr>
                    <td>
                        <table class="formGrid">
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblTemplateCode" runat="server">TEMPLATE_CODE</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="txtTemplateCode" runat="server" SkinID="MediumTextBox" Enabled="false"></asp:TextBox>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblTemplateDescription" runat="server">TEMPLATE_DESCRIPTION</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="txtTemplateDescription" runat="server" SkinID="MediumTextBox" Enabled="false"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblSenderReason" runat="server">SENDER_REASON</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="txtSenderReason" runat="server" SkinID="MediumTextBox" Enabled="false"></asp:TextBox>
                                </td>
                                <td colspan="2">&nbsp;</td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="dataContainer">
        <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0" />
        <asp:HiddenField ID="hdnDisabledTab" runat="server" />
        <div id="tabs" class="style-tabs">
	        <ul>
		        <li><a href="#tab_MessageParameters"><asp:Label ID="lblParametersTab" runat="server" CssClass="tabHeaderText">PARAMETERS_TAB</asp:Label></a></li>
		        <li><a href="#tab_MessageAttempts"><asp:Label ID="lblMessageAttemptsTab" runat="server" CssClass="tabHeaderText">MESSAGEATTEMPTS_TAB</asp:Label></a></li>
	        </ul>
            <div id="tab_MessageParameters">
                <div class="Page" runat="server" style="height: 100%; overflow: auto">
                    <asp:GridView ID="MessageParametersGrid" runat="server" Width="100%" OnRowCreated="ParametersGrid_RowCreated"
                        AllowPaging="False" AllowSorting="False" CellPadding="1" AutoGenerateColumns="False" SkinID="DetailPageGridView">
                        <SelectedRowStyle Wrap="False"></SelectedRowStyle>
                        <EditRowStyle Wrap="False"></EditRowStyle>
                        <AlternatingRowStyle Wrap="False"></AlternatingRowStyle>
                        <RowStyle Wrap="False"></RowStyle>
                        <Columns>
                            <asp:TemplateField Visible="False" HeaderText="OC_MESSAGE_PARAMETERS_ID">
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
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="PARAM_VALUE">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblParamValue" runat="server" Visible="True">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                        <PagerStyle />
                    </asp:GridView>
                    <br />
                </div>
            </div>
            <div id="tab_MessageAttempts">
                <div class="Page" runat="server" style="height: 100%; overflow: auto">
                    <asp:GridView ID="MessageAttemptsGrid" runat="server" Width="100%" OnRowCreated="MessageAttemptsGrid_RowCreated"
                        AllowPaging="False" AllowSorting="False" CellPadding="1" AutoGenerateColumns="False" SkinID="DetailPageGridView">
                        <SelectedRowStyle BackColor="lightsteelblue"></SelectedRowStyle>
                        <EditRowStyle Wrap="False"></EditRowStyle>
                        <AlternatingRowStyle Wrap="False"></AlternatingRowStyle>
                        <RowStyle Wrap="False"></RowStyle>
                        <Columns>
                            <asp:TemplateField Visible="False" HeaderText="OC_MESSAGE_ATTEMPT_ID">
                                <ItemTemplate>
                                    <asp:Label ID="IdLabel" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Center"></ItemStyle>
                                <ItemTemplate>
                                    <asp:ImageButton Style="cursor: hand;" ID="btnSelect" CommandName="SelectRecord"
                                        ImageUrl="../Navigation/images/icons/yes_icon.gif" runat="server" CommandArgument="<%#Container.DisplayIndex %>"
                                        CausesValidation="false"></asp:ImageButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="RECIPIENT_ADDRESS">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblRecipientAddress" runat="server"></asp:Label> 
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtRecipientAddress" runat="server"></asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="RECIPIENT_DESCRIPTION">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblRecipientDescription" runat="server"></asp:Label> 
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtRecipientDescription" runat="server"></asp:TextBox>
                                </EditItemTemplate> 
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ATTEMPTED_ON">
                                <ItemStyle HorizontalAlign="Right"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblAttemptedOn" runat="server" Visible="True">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ATTEMPTED_BY">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblAttemptedBy" runat="server" Visible="True">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="STATUS">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblStatus" runat="server" Visible="True">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="ERROR_MESSAGE">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblErrorMessage" runat="server" Visible="True">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                        <PagerStyle />
                    </asp:GridView>
                </div>
                <div class="btnZone">
                    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
                                        runat="server" />
                    <asp:Button ID="btnResend_WRITE" runat="server" CausesValidation="False" Text="MESSAGE_RESEND"
                        SkinID="AlternateRightButton"></asp:Button>
                    <asp:Button ID="btnResendDiffEmail_WRITE" runat="server" CausesValidation="False" Text="MESSAGE_RESEND_DIFF_EMAIL"
                        SkinID="AlternateRightButton"></asp:Button>
                </div>
            </div>
        </div>
    </div>
    <div class="btnZone">
        <asp:Button ID="btnBack" runat="server" CausesValidation="False" Text="BACK" SkinID="AlternateLeftButton"></asp:Button>
    </div>
</asp:Content>