<%@ Page Language="vb" ValidateRequest="false" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master"
    CodeBehind="RouteMaintenanceDetailForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.RouteMaintenanceDetailForm" %>

<%@ Register Src="../Common/UserControlAvailableSelected.ascx" TagName="UserControlAvailableSelected"
    TagPrefix="uc1" %>
<%@ Register TagPrefix="uc2" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <table cellspacing="3" cellpadding="2" width="100%" border="0">
        <input id="HiddenSaveChangesPromptResponse" style="width: 116px; height: 10px" type="hidden"
            size="14" name="HiddenSaveChangesPromptResponse" runat="server"></input>
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td style="vertical-align: middle">
                            <asp:Label ID="LabelShortDesc" runat="server">CODE</asp:Label>
                        </td>
                        <td style="vertical-align: middle">&nbsp;&nbsp;
                            <asp:Label ID="LabelDescription" runat="server">DESCRIPTION</asp:Label>
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: middle;" nowrap width="1%">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="Label1" runat="server">ROUTE</asp:Label>:&nbsp;
                        </td>
                        <td style="vertical-align: middle;" align="left" width="4%">
                            <asp:TextBox ID="TextboxShortDesc" TabIndex="10" runat="server" Width="85%"></asp:TextBox>
                        </td>
                        <td style="vertical-align: middle;" align="left" width="15%">&nbsp;&nbsp;
                            <asp:TextBox ID="TextboxDescription" TabIndex="11" runat="server" Width="57%"></asp:TextBox>
                        </td>
                        <td>
                        </td>
                    </tr>
                    </table>
            </td>
        </tr>
        <td colspan="6">
            <hr>
        </td>
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td valign="top" align="center" colspan="6">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <uc2:MultipleColumnDDLabelControl ID="moServiceNetworkMultipleDrop" runat="server">
                            </uc2:MultipleColumnDDLabelControl>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td colspan="6">
                <hr style="height: 1px; width: 100%">
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td>
                        </td>
                        <td valign="middle" align="center" colspan="6">
                            <uc1:UserControlAvailableSelected ID="UserControlAvailableSelectedServiceCenters"
                                runat="server"></uc1:UserControlAvailableSelected>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPanelButtons" runat="server" ID="cntButtons">
    <span ></span>
    <asp:Button ID="btnBack" Style="background-image: url(../Navigation/images/icons/back_icon.gif);
        cursor: hand; background-repeat: no-repeat" TabIndex="185" runat="server" Font-Bold="false"
        Width="90px" Text="Return" Height="20px" CssClass="FLATBUTTON"></asp:Button>&nbsp;
    <asp:Button ID="moBtnSave_WRITE" Style="background-image: url(../Navigation/images/icons/save_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
        Width="100px" Text="Save" Height="20px" CssClass="FLATBUTTON" TabIndex="4"></asp:Button><span>&nbsp; </span>
    <asp:Button ID="moBtnCancel" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
        Width="100px" Text="Undo" Height="20px" CssClass="FLATBUTTON" TabIndex="5"></asp:Button><span>&nbsp;</span>
    <asp:Button ID="btnNew_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
        cursor: hand; background-repeat: no-repeat" TabIndex="200" runat="server" Font-Bold="false"
        Width="81px" CssClass="FLATBUTTON" Text="New" Height="20px"></asp:Button>&nbsp;&nbsp;
    <asp:Button ID="btnDelete_WRITE" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
        cursor: hand; background-repeat: no-repeat" TabIndex="210" runat="server" Font-Bold="false"
        Width="100px" CssClass="FLATBUTTON" Text="Delete" Height="20px"></asp:Button></TD>
</asp:Content>
