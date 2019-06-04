<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ExcludeListItemByRoleForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.ExcludeListItemByRoleForm" EnableSessionState="True" Theme="Default"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" %>

<%@ Register TagPrefix="Elita" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl_new.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlAvailableSelected" Src="../Common/UserControlAvailableSelected_new.ascx" %>
<%@ Register TagPrefix="mytab" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
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
        <div class="stepformZone">
            <table class="formGrid" border="0" cellspacing="0" cellpadding="0" style="padding-left: 0px;"
                width="100%">
                <tr>
                    <td id="Td1" runat="server" colspan="2">
                        <table>
                            <tbody>
                                <Elita:MultipleColumnDDLabelControl runat="server" ID="CompanyDropControl" />
                            </tbody>
                        </table>
                    </td>
                </tr>
                <tr>
                    <td>&nbsp;</td>
                    <td id="Td2" runat="server">
                        <table>
                            <tbody>
                                <Elita:MultipleColumnDDLabelControl runat="server" ID="ListDropControl" />
                            </tbody>
                        </table>
                    </td>

                </tr>
                <tr>
                    <td id="TD3" runat="server" colspan="2">
                        <table>
                            <tbody>
                                <Elita:MultipleColumnDDLabelControl runat="server" ID="ListItemDropControl" />
                            </tbody>
                        </table>
                    </td>
                </tr>
            </table>
        </div>
        <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse" runat="server" />
        <input id="HiddenDeletePromptResponse" type="hidden" runat="server" designtimedragdrop="261" />
    </div>
    <div class="dataContainer">
        <%--     changes for tab--%>
        <div id="tabs" class="style-tabs">
            <ul>
                <li><a href="#tabsExcludeRoles">
                    <asp:Label ID="Label4" runat="server" CssClass="tabHeaderText">Exclude_Roles</asp:Label></a></li>
            </ul>

            <div id="tabsExcludeRoles">
                 <div class="Page" runat="server" id="moExcludeRoles1" style="height: 100%; overflow: auto">
                    <Elita:UserControlAvailableSelected tabindex="12" ID="UserControlAvailableSelectedExcludeRoles"
                        runat="server"></Elita:UserControlAvailableSelected>
                </div>
            </div>
        </div>
    </div>
    <div class="btnZone">
        <asp:Button ID="btnApply_WRITE" runat="server" CausesValidation="False" Text="SAVE"
            SkinID="PrimaryRightButton"></asp:Button>
        <asp:Button ID="btnUndo_WRITE" runat="server" CausesValidation="False" Text="UNDO"
            SkinID="AlternateRightButton"></asp:Button>
        <asp:Button ID="btnBack" runat="server" CausesValidation="False" Text="BACK" SkinID="AlternateLeftButton"></asp:Button>
        <asp:Button ID="btnNew_WRITE" runat="server" CausesValidation="False" Text="New"
            SkinID="AlternateLeftButton"></asp:Button>
        <asp:Button ID="btnCopy_WRITE" runat="server" CausesValidation="False" Text="New_With_Copy"
            SkinID="AlternateRightButton"></asp:Button>
        <asp:Button ID="btnDelete_WRITE" runat="server" CausesValidation="False" Text="Delete"
            SkinID="CenterButton"></asp:Button>

    </div>

</asp:Content>
