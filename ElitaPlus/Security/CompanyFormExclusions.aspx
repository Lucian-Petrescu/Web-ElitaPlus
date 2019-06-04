<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CompanyFormExclusions.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Security.CompanyFormExclusions"
    MasterPageFile="~/Navigation/masters/content_default.Master" %>

<asp:Content ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <table width="100%" border="0" cellpadding="0" cellspacing="0">
        <tr>
            <td style="height: 30px; vertical-align: top;">
                <font color="#12135b"><i><b>*</b> Click on +/- to expand or collapse nodes and toggle
                    form exclusions for each company.</i></font>
            </td>
        </tr>
        <tr>
            <td style="height: 1px" valign="top">
                <hr>
            </td>
        </tr>
        <tr>
            <td style="vertical-align: top; text-align: left; padding-left: 345px;">
                <div id="scroller" style="overflow: auto; width: 98%; height:455px" align="left">
                    <asp:TreeView  ID="tvFormList" runat="server" ShowExpandCollapse="true" ShowLines="true" NodeStyle-HorizontalPadding="5" NodeStyle-VerticalPadding="0"></asp:TreeView>
                </div>
            </td>
        </tr>
    </table> 

</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <asp:Button ID="BtnSave_WRITE" Style="background-image: url(../Navigation/images/icons/save_trans_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" DESIGNTIMEDRAGDROP="44"
        ToolTip="The Checked Roles will be in the DataBase company form exclusion table"
        Text="Save" Width="90px" CssClass="FLATBUTTON" Height="18px"></asp:Button>&nbsp;
    <asp:Button ID="BtnReset" Style="background-image: url(../Navigation/images/icons/reset_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" ToolTip="Load The Tabs, Forms and Companies from the DataBase"
        Text="Reset" Width="90px" CssClass="FLATBUTTON" Height="18px"></asp:Button>
    <script type="text/javascript">        
        if (document.getElementById('scroller')) {
            document.getElementById('scroller').style.height = parent.document.getElementById("Navigation_Content").clientHeight - 210;
        }
    </script>
</asp:Content>
