<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RewardDetailsForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Certificates.RewardDetailsForm" EnableSessionState="True" Theme="Default"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" %>

<%@ Register TagPrefix="Elita" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl_new.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlAvailableSelected" Src="../Common/UserControlAvailableSelected_new.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlAttrtibutes" Src="~/Common/UserControlAttrtibutes.ascx" %>
<%@ Register TagPrefix="mytab" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>

<%@ Register TagPrefix="ElitaUC" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register Assembly="Microsoft.Web.UI.WebControls" Namespace="Microsoft.Web.UI.WebControls" TagPrefix="iewc" %>


<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.6.1.min.js"> </script>
    <script type="text/javascript">
        $("[src*=\sort_indicator_des]").live("click", function () {
            $(this).closest("tr").after("<tr><td></td><td colspan = '999'>" + $(this).next().html() + "</td></tr>")
            $(this).attr("src", "..\\App_Themes\\Default\\Images\\sort_indicator_asc.png");
        });
        $("[src*=sort_indicator_asc]").live("click", function () {
            $(this).attr("src", "..\\App_Themes\\Default\\Images\\sort_indicator_des.png");
            $(this).closest("tr").next().remove();
        });
    </script>
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

    <div class="dataEditBox">
        <table border="0" class="formGrid" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td nowrap="nowrap" align="right">
                    <asp:Label ID="moRewardTypelbl" runat="server" Text="REWARD_TYPE"></asp:Label>
                </td>
                <td nowrap="nowrap">
                    <asp:DropDownList ID="moRewardTypeDD" runat="server" SkinID="MediumDropDown" AutoPostBack="true">
                    </asp:DropDownList>
                </td>              
                <td nowrap="nowrap" align="right">
                    <asp:Label ID="moCertNumberlbl" runat="server" Text="Certificate_Number" ></asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="moCertNumberTxt" runat="server" SkinID="MediumTextBox" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td nowrap="nowrap" align="right">
                    <asp:Label ID="moRewardStatuslbl" runat="server" Text="REWARD_STATUS"></asp:Label>
                </td>
                <td nowrap="nowrap">
                    <asp:DropDownList ID="moRewardStatusDD" runat="server" SkinID="MediumDropDown" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td nowrap="nowrap" align="right">
                    <asp:Label ID="moRewardPayModelbl" runat="server" Text="REWARD_PAY_MODE"></asp:Label>
                </td>
                <td nowrap="nowrap">
                    <asp:DropDownList ID="moRewardPayModeDD" runat="server" SkinID="MediumDropDown" AutoPostBack="true"></asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" nowrap="nowrap">
                    <asp:Label ID="moSwiftCodelbl" runat="server" Font-Bold="false">Swift_Code</asp:Label>&nbsp;
                </td>
                <td align="left" nowrap="nowrap">
                    <asp:TextBox ID="moSwiftCodeTxt" runat="server" SkinID="SmallTextBox" Enabled="false"></asp:TextBox>
                </td>
                <td align="right" nowrap="nowrap">
                    <asp:Label ID="moFormSignedlbl" runat="server">Form_Signed</asp:Label>
                </td>
                <td align="left" nowrap="nowrap">
                    <asp:DropDownList ID="moFormSignedDD" runat="server" SkinID="MediumDropDown">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" nowrap="nowrap">
                    <asp:Label ID="moSubscFormSignedlbl" runat="server">Subs_Form_Signed</asp:Label>
                </td>
                <td align="left" nowrap="nowrap">
                    <asp:DropDownList ID="moSubscFormSignedDD" runat="server" SkinID="MediumDropDown">
                    </asp:DropDownList>
                </td>
                <td align="right" nowrap="nowrap">
                    <asp:Label ID="moInvoiceSignedlbl" runat="server">Invoice_Signed</asp:Label>
                </td>
                <td align="left" nowrap="nowrap">
                    <asp:DropDownList ID="moInvoiceSignedDD" runat="server" SkinID="MediumDropDown">
                    </asp:DropDownList>
                </td>
            </tr>
            <tr>
                <td align="right" nowrap="nowrap">
                    <asp:Label ID="moRibSignedlbl" runat="server">Rib_Signed</asp:Label>
                </td>
                <td align="left" nowrap="nowrap">
                    <asp:DropDownList ID="moRibSignedDD" AutoPostBack="true" runat="server" SkinID="MediumDropDown">
                    </asp:DropDownList>
                </td>
                <td align="right" nowrap="nowrap">
                    <asp:Label ID="moSeqNumberlbl" runat="server">Sequence_Number</asp:Label>
                </td>
                <td align="left" nowrap="nowrap">
                    <asp:TextBox ID="moSeqNumberTxt" runat="server" SkinID="MediumTextBox" Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td align="right" nowrap="nowrap">
                    <asp:Label ID="moIbanNumberlbl" runat="server" Font-Bold="false">Iban_Number</asp:Label>&nbsp;
                </td>
                <td align="left" nowrap="nowrap">
                    <asp:TextBox ID="moIbanNumberTxt" TabIndex="12" runat="server" SkinID="SmallTextBox" Enabled="false"></asp:TextBox>
                </td>
                <td align="right" nowrap="nowrap">
                    <asp:Label ID="moRewardAmountlbl" runat="server" Font-Bold="false">Reward_Amount</asp:Label>&nbsp;
                </td>
                <td align="left" nowrap="nowrap">
                    <asp:TextBox ID="moRewardAmountTxt" TabIndex="12" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
    <div class="btnZone">
        <asp:Button ID="btnApply_WRITE" runat="server" CausesValidation="False" Text="SAVE"
            SkinID="PrimaryRightButton"></asp:Button>
        <asp:Button ID="btnUndo_WRITE" runat="server" CausesValidation="False" Text="UNDO"
            SkinID="AlternateRightButton"></asp:Button>
        <asp:Button ID="btnBack" runat="server" CausesValidation="False" Text="BACK" SkinID="AlternateLeftButton"></asp:Button>
    </div>

</asp:Content>
