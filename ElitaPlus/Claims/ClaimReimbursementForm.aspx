<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ClaimReimbursementForm.aspx.vb"
    Theme="Default" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ClaimReimbursementForm"
    EnableSessionState="True" MasterPageFile="../Navigation/masters/ElitaBase.Master" %>

<%@ Register Assembly="Microsoft.Web.UI.WebControls" Namespace="Microsoft.Web.UI.WebControls"
    TagPrefix="iewc" %>
<%@ Register TagPrefix="Elita" TagName="UserControlBankInfo" Src="../Common/UserControlBankInfo_New.ascx" %>

<asp:Content ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.6.1.min.js"></script>
    <link type="text/css" href="../Navigation/styles/jquery-ui.min.css" rel="stylesheet" />
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.12.4.min.js"> </script>
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-ui.min.js"> </script>
    <link type="text/css" href="../Navigation/styles/jquery-ui.elita.css" rel="stylesheet" />

</asp:Content>
<asp:Content ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
        <table cellspacing="0" cellpadding="0" width="65%" border="0" class="formGrid" style="padding-left: 0px;">
            <tr>
                <td nowrap="nowrap" align="right">
                    <asp:Label ID="lblSelect" runat="server" Text="SELECT"></asp:Label>&nbsp;
                    <asp:Label ID="LabelPaymentType" runat="server">PAYMENT_INSTRUMENT</asp:Label>:
                </td>
                <td align="left">
                    <asp:DropDownList ID="ddlPaymentList" TabIndex="1" SkinID="SmallDropDown" runat="server" AutoPostBack="true">
                    </asp:DropDownList>
                </td>
                <td align="right"></td>
                <td align="left"></td>
            </tr>
            <tr>
                <td colspan="4"></td>
            </tr>
            <Elita:UserControlBankInfo ID="moBankInfoController" runat="server"></Elita:UserControlBankInfo>
        </table>

    </div>
    <div class="btnZone">
        <div class="">
            <asp:Button ID="btnSave_WRITE" runat="server" Text="Save"
                SkinID="PrimaryRightButton" />
            <asp:Button ID="btnBack" runat="server" Text="Back" SkinID="AlternateRightButton" />
        </div>
    </div>

</asp:Content>
