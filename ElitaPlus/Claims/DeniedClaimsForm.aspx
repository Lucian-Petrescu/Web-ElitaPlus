<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master"
    CodeBehind="DeniedClaimsForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Claims.DeniedClaimsForm" %>

<%@ Register Src="../Common/UserControlAvailableSelected.ascx" TagName="UserControlAvailableSelected"
    TagPrefix="uc1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <table cellspacing="2" width="100%" align="center" border="0">
        <tr>
            <td>
                <input id="HiddenSaveChangesPromptResponse" style="width: 116px; height: 10px" type="hidden"
                    size="14" name="HiddenSaveChangesPromptResponse" runat="server"></input>
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: middle;" nowrap width="1%">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="LabelCertificate" runat="server">Certificate</asp:Label>:&nbsp;
                        </td>
                        <td style="vertical-align: middle;" align="left" width="25%">
                            <asp:TextBox ID="TextboxShortDesc" TabIndex="10" runat="server" Width="45%"></asp:TextBox>
                        </td>
                        <td style="vertical-align: middle;" nowrap width="1%">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="LabelClaim" runat="server">Claim</asp:Label>:&nbsp;
                        </td>
                        <td style="vertical-align: middle;" align="left" width="25%">
                            <asp:TextBox ID="TextboxDescription" TabIndex="11" runat="server" Width="45%"></asp:TextBox>
                        </td>
                        <td style="vertical-align: middle;" nowrap width="1%">
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Label ID="LabelDate" runat="server">Date</asp:Label>:&nbsp;
                        </td>
                        <td style="vertical-align: middle;" align="left" width="30%">
                            <asp:TextBox ID="TextboxDate" TabIndex="11" runat="server" Width="45%"></asp:TextBox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td>
                <hr>
            </td>
        </tr>
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td style="height: 10px" valign="top" align="left">
                            <uc1:UserControlAvailableSelected ID="UserControlAvailableSelectedDeniedReasosns"
                                runat="server"></uc1:UserControlAvailableSelected>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <hr>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 1%" valign="middle" align="left">
                            &nbsp;
                            <asp:Label ID="LabelStandardEditing" runat="server">STANDARD_EDITING</asp:Label>:
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 84%" valign="middle" align="center">
                            &nbsp;
                            <asp:TextBox ID="TextboxStandardEditing1" TabIndex="4" runat="server" CssClass="FLATTEXTBOX"
                                Style="border: #999999 1px solid; width: 1160px; white-space: normal; height: 64px;"
                                TextMode="MultiLine"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 84%" valign="middle" align="center">
                            &nbsp;
                            <asp:TextBox BorderWidth="100 px" ID="TextboxStandardEditing2" Style="border-right: #999999 1px solid;
                                border-top: #999999 1px solid; border-left: #999999 1px solid; border-bottom: #999999 1px solid"
                                TabIndex="4" runat="server" CssClass="FLATTEXTBOX" TextMode="MultiLine" Width="1160px"
                                Height="64px" nowrap></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <hr>
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 1%" valign="middle" align="left">
                            &nbsp;
                            <asp:Label ID="LabelSpecialEditing" runat="server">SPECIAL_EDITING</asp:Label>:
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 84%" valign="middle" align="center">
                            &nbsp;
                            <asp:TextBox BorderWidth="100 px" ID="TextboxSpecialEditing" Style="border-right: #999999 1px solid;
                                border-top: #999999 1px solid; border-left: #999999 1px solid; border-bottom: #999999 1px solid"
                                TabIndex="4" runat="server" CssClass="FLATTEXTBOX" TextMode="MultiLine" Width="1160px"
                                Height="64px"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <table cellpadding="0" cellspacing="0" width="100%">
                                <tr>
                                    <td style="vertical-align: middle;"  width="1%">
                                        <asp:Label ID="LabelAuthorizedApproverDrop" runat="server">AUTHORIZED_APPROVER</asp:Label>
                                        &nbsp;&nbsp;<asp:DropDownList ID="moAuthorizedApproverDrop" TabIndex="1" runat="server" Width="25%">
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPanelButtons" runat="server" ID="cntButtons">
    <span></span>
    <asp:Button ID="btnBack" Style="background-image: url(../Navigation/images/icons/back_icon.gif);
        cursor: hand; background-repeat: no-repeat" TabIndex="185" runat="server" Font-Bold="false"
        Width="90px" Text="Back" Height="20px" CssClass="FLATBUTTON"></asp:Button>&nbsp;
    <asp:Button ID="moBtnSave_WRITE" Style="background-image: url(../Navigation/images/icons/save_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
        Width="100px" Text="Save" Height="20px" CssClass="FLATBUTTON" TabIndex="4"></asp:Button><span>&nbsp;
        </span>
    <asp:Button ID="moBtnCancel" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
        Width="100px" Text="Undo" Height="20px" CssClass="FLATBUTTON" TabIndex="5"></asp:Button><span>&nbsp;</span>
    <%--<asp:Button ID="btnNew_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
        cursor: hand; background-repeat: no-repeat" TabIndex="200" runat="server" Font-Bold="false"
        Width="81px" CssClass="FLATBUTTON" Text="New" Height="20px"></asp:Button>&nbsp;&nbsp;
    <asp:Button ID="btnDelete_WRITE" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
        cursor: hand; background-repeat: no-repeat" TabIndex="210" runat="server" Font-Bold="false"
        Width="100px" CssClass="FLATBUTTON" Text="Delete" Height="20px"></asp:Button></TD>--%>
</asp:Content>
