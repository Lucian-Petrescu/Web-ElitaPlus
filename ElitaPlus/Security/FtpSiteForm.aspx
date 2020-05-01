<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master"
    CodeBehind="FtpSiteForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Security.FtpSiteForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    
      <table cellspacing="0" cellpadding="0" width="100%" border="0">
        <tr>
            <td class="TD_LABEL" >
                *<asp:Label ID="moCodeLabel" runat="server">CODE</asp:Label>:
            </td>
            <td style="white-space: nowrap;" align="left">
                &nbsp;<asp:TextBox ID="moCodeText" runat="server" Width="300px" CssClass="FLATTEXTBOX"></asp:TextBox>
            </td>
            <td class="TD_LABEL">
                *<asp:Label ID="moDescriptionLabel" runat="server">DESCRIPTION</asp:Label>:
            </td>
            <td style="white-space: nowrap;" align="left">
                &nbsp;<asp:TextBox ID="moDescriptionText" runat="server" Width="300px" CssClass="FLATTEXTBOX"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  colspan="4" style="height: 12px; width: 1%;">   
            </td>  
        </tr>
        <tr>
            <td class="TD_LABEL" >
                *<asp:Label ID="moHostLabel" runat="server">HOST</asp:Label>:
            </td>
            <td style="white-space: nowrap;" align="left">
                &nbsp;<asp:TextBox ID="moHostText" runat="server" Width="300px" CssClass="FLATTEXTBOX"></asp:TextBox>
            </td>
            <td class="TD_LABEL">
                *<asp:Label ID="moPortLabel" runat="server">PORT</asp:Label>:
            </td>
            <td style="white-space: nowrap;" align="left">
                &nbsp;<asp:TextBox ID="moPortText" runat="server" Width="300px" CssClass="FLATTEXTBOX"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  colspan="4" style="height: 12px; width: 1%;">   
            </td>  
        </tr>
        <tr>
            <td class="TD_LABEL" >
                *<asp:Label ID="moUsernameLabel" runat="server">USERNAME</asp:Label>:
            </td>
            <td style="white-space: nowrap;" align="left">
                &nbsp;<asp:TextBox ID="moUsernameText" runat="server" Width="300px" CssClass="FLATTEXTBOX" TextMode="Password"></asp:TextBox>
            </td>
            <td class="TD_LABEL">
                *<asp:Label ID="moPasswordLabel" runat="server">PASSWORD</asp:Label>:
            </td>
            <td style="white-space: nowrap;" align="left">
                &nbsp;<asp:TextBox ID="moPasswordText" runat="server" Width="300px" CssClass="FLATTEXTBOX" TextMode="Password"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  colspan="4" style="height: 12px; width: 1%;">   
            </td>  
        </tr>
        <tr>
            <td class="TD_LABEL" >
                <asp:Label ID="moAccountLabel" runat="server">ACCOUNT</asp:Label>:
            </td>
            <td style="white-space: nowrap;" align="left">
                &nbsp;<asp:TextBox ID="moAccountText" runat="server" Width="300px" CssClass="FLATTEXTBOX"></asp:TextBox>
            </td>
            <td class="TD_LABEL">
                *<asp:Label ID="moDirectoryLabel" runat="server">DIRECTORY</asp:Label>:
            </td>
            <td style="white-space: nowrap;" align="left">
                &nbsp;<asp:TextBox ID="moDirectoryText" runat="server" Width="300px" CssClass="FLATTEXTBOX"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td  colspan="4" style="height: 12px; width: 1%;">
                <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
                       runat="server"/>   
            </td>  
        </tr>
        
    </table>
</asp:Content>
<asp:Content ID="cntButtons" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <asp:Button ID="btnBack" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_BACK" Text="BACK">
    </asp:Button>
    <asp:Button ID="btnApply_WRITE" Style="background-image: url(../Navigation/images/icons/save_trans_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Width="90px" Text="SAVE"
        CssClass="FLATBUTTON" Height="20px"></asp:Button>
    <asp:Button ID="btnUndo_WRITE" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Width="90px" Text="UNDO"
        CssClass="FLATBUTTON" Height="20px" CausesValidation="False"></asp:Button>
    <asp:Button ID="btnNew_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Width="90px" Text="New"
        CssClass="FLATBUTTON" Height="20px" CausesValidation="False"></asp:Button>
    <asp:Button ID="btnCopy_WRITE" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_NEW"
        Text="New_With_Copy" Width="136px"></asp:Button>
    <asp:Button ID="btnDelete_WRITE" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Width="100px" Text="Delete"
        CssClass="FLATBUTTON" Height="20px" CausesValidation="False"></asp:Button>
</asp:Content>