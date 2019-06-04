<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MaintainTabExclusionForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Security.MaintainTabExclusionForm"
    MasterPageFile="~/Navigation/masters/content_default.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td valign="top" >
                <font color="#12135b"><i><b>*</b> Click on +/- to expand or collapse nodes and toggle
                    tab permissions for each user role.</i></font>
            </td>
        </tr>
        <tr>
            <td>
                <hr>
            </td>
        </tr>
        <tr>
          <td style="vertical-align: top; text-align: left; padding-left: 345px;">
                <asp:Panel ID="panelTree" ScrollBars="Auto" runat="server" Width="100%" Height="490px">
			        <asp:TreeView ID="tvFormList" runat="server" ShowExpandCollapse="true" ShowLines="true" NodeStyle-HorizontalPadding="5" NodeStyle-VerticalPadding="0"></asp:TreeView>
			    </asp:Panel>
            </td>
        </tr>
    </table>

</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPanelButtons" runat="server">
    &nbsp;<asp:Button Height="18px" Style="background-image: url(../Navigation/images/icons/save_trans_icon.gif);
        cursor: hand; background-repeat: no-repeat" CssClass="FLATBUTTON" ID="BtnSave_WRITE"
        runat="server" Width="90px" Text="Save" ToolTip='The UnChecked Roles will be in the DataBase tab exclusion table'>
    </asp:Button>&nbsp;
    <asp:Button ID="BtnReset" Height="18px" Style="background-image: url(../Navigation/images/icons/reset_icon.gif);
        cursor: hand; background-repeat: no-repeat" CssClass="FLATBUTTON" runat="server"
        Width="90px" Text="Reset" ToolTip="Load The Tabs and Roles from the DataBase">
    </asp:Button>
</asp:Content>
