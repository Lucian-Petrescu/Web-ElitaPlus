<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MaintainControlExclusionForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Security.MaintainControlExclusionForm"
    MasterPageFile="~/Navigation/masters/content_default.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td valign="top">
                <font color="#12135b"><i><b>*</b> Click on +/- to expand or collapse nodes and toggle
                    control permissions per form for each user role.</i></font>
            </td>
        </tr>
        <tr>
            <td style="height: 22px">
                <hr>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; text-align:center; padding-left:150px;">
                <div id="scroller" style="overflow: auto; width: 98%; height: 485px" align="left">
                    <asp:TreeView ID="tvFormList" runat="server" ShowExpandCollapse="true" ShowLines="true" NodeStyle-HorizontalPadding="5" NodeStyle-VerticalPadding="0"></asp:TreeView>
                </div>
            </td>
        </tr>
    </table>
   
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPanelButtons" runat="server">
    &nbsp;<asp:Button ID="BtnSave_WRITE" Style="background-image: url(../Navigation/images/icons/save_trans_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Height="18px" Width="90px"
        CssClass="FLATBUTTON" Text="Save" ToolTip="The UnChecked Roles will be in the DataBase control exclusion table">
    </asp:Button>&nbsp;
    <asp:Button ID="BtnReset" Style="background-image: url(../Navigation/images/icons/reset_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Height="18px" Width="90px"
        CssClass="FLATBUTTON" Text="Reset" ToolTip="Load The Tabs and Forms  from the DataBase">
    </asp:Button>
</asp:Content>
