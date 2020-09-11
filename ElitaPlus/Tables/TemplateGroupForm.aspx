<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="TemplateGroupForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.TemplateGroupForm" EnableSessionState="True" Theme="Default"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" %>
<%@ Register TagPrefix="Elita" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl_New.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlAvailableSelected" Src="../Common/UserControlAvailableSelected_new.ascx" %>
<%@ Register TagPrefix="mytab" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register Assembly="Microsoft.Web.UI.WebControls" Namespace="Microsoft.Web.UI.WebControls" TagPrefix="iewc" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.6.1.min.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto">
        <Scripts>
            <asp:ScriptReference Path="~/Navigation/scripts/ComunaSuggest.js"></asp:ScriptReference>
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
                                    <asp:Label ID="lblTemplateGroupCode" runat="server">TEMPLATE_GROUP_CODE</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="txtTemplateGroupCode" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblTemplateGroupDescription" runat="server">TEMPLATE_GROUP_DESCRIPTION</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="txtTemplateGroupDescription" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblGroupAccountUserName" runat="server">GROUP_ACCOUNT_USER_NAME</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="txtGroupAccountUserName" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="lblGroupAccountPassword" runat="server">GROUP_ACCOUNT_PASSWORD</asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:TextBox ID="txtGroupAccountPassword" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                                </td>
                            </tr>
                            <tr>
                                <td align="right" nowrap="nowrap">
                                    <asp:Label ID="moIsNewBillingCycleLabel" runat="server" Visible="False"></asp:Label>
                                </td>
                                <td align="left" nowrap="nowrap">
                                    <asp:Label ID="moBillingCycleIdLabel" runat="server" Visible="False"></asp:Label>
                                    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse" runat="server">
                                </td>
                            </tr>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
    </div>
    <div class="dataContainer">
        <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0"></asp:HiddenField>
        <asp:HiddenField ID="hdnDisabledTab" runat="server"></asp:HiddenField>
        <div id="tabs" class="style-tabs">
	        <ul>
		        <li><a href="#tab_Dealers" rel="noopener noreferrer"><asp:Label ID="lblDealersTab" runat="server" CssClass="tabHeaderText">DEALERS_TAB</asp:Label></a></li>
		        <li><a href="#tab_Templates" rel="noopener noreferrer"><asp:Label ID="lblTemplatesTab" runat="server" CssClass="tabHeaderText">TEMPLATES_TAB</asp:Label></a></li>
	        </ul>
            <div id="tab_Dealers">
                <div class="Page" runat="server" style="height: 100%; overflow: auto">
                    <Elita:UserControlAvailableSelected ID="UserControlAvailableSelectedDealers" runat="server"></Elita:UserControlAvailableSelected>
                </div>
            </div>
            <div id="tab_Templates">
                <div class="Page" runat="server" style="height: 100%; overflow: auto">
                    <asp:GridView ID="TemplatesGrid" runat="server" Width="100%" OnRowCreated="Grid_RowCreated" OnRowCommand="Grid_RowCommand" AllowPaging="True" AllowSorting="False" CellPadding="1" AutoGenerateColumns="False" SkinID="DetailPageGridView">
                        <SelectedRowStyle Wrap="False"></SelectedRowStyle>
                        <EditRowStyle Wrap="False"></EditRowStyle>
                        <AlternatingRowStyle Wrap="False"></AlternatingRowStyle>
                        <RowStyle Wrap="False"></RowStyle>
                        <Columns>
                            <asp:TemplateField HeaderText="TEMPLATE_CODE">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="SelectAction" Style="cursor: hand" runat="server" Visible="true" CommandName="SelectAction" CommandArgument="<%#Container.DisplayIndex%>">
                                    </asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="TEMPLATE_DESCRIPTION">
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                <ItemTemplate>
                                    <asp:Label ID="lblDescription" runat="server" Visible="True">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField Visible="False" HeaderText="TEMPLATE_ID">
                                <ItemTemplate>
                                    <asp:Label ID="IdLabel" runat="server">
                                    </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom"></PagerSettings>
                        <PagerStyle></PagerStyle>
                    </asp:GridView>
                </div>
                <br>
                <asp:Button ID="btnNewTemplate_WRITE" runat="server" CausesValidation="False" Text="NEW_TEMPLATE" SkinID="PrimaryLeftButton"></asp:Button>
            </div>
        </div>
    </div>
    <div class="btnZone">
        <asp:Button ID="btnApply_WRITE" runat="server" CausesValidation="False" Text="SAVE" SkinID="PrimaryRightButton"></asp:Button>
        <asp:Button ID="btnUndo_WRITE" runat="server" CausesValidation="False" Text="UNDO" SkinID="AlternateRightButton"></asp:Button>
        <asp:Button ID="btnBack" runat="server" CausesValidation="False" Text="BACK" SkinID="AlternateLeftButton"></asp:Button>
        <asp:Button ID="btnNew_WRITE" runat="server" CausesValidation="False" Text="New" SkinID="AlternateLeftButton"></asp:Button>
        <asp:Button ID="btnCopy_WRITE" runat="server" CausesValidation="False" Text="New_With_Copy" SkinID="AlternateRightButton"></asp:Button>
        <asp:Button ID="btnDelete_WRITE" runat="server" CausesValidation="False" Text="Delete" SkinID="CenterButton"></asp:Button>
    </div>
</asp:Content>
          SkinID="CenterButton"></asp:Button>
    </div>
</asp:Content>
