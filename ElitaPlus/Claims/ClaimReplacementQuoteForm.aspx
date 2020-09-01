<%@ Page Language="VB" AutoEventWireup="false" CodeBehind="ClaimReplacementQuoteForm.aspx.vb"
    Theme="Default" MasterPageFile="~/Navigation/masters/ElitaBase.Master"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Claims.ClaimReplacementQuoteForm" %>

<%@ Register TagPrefix="Elita" TagName="ProtectionAndEventDetails" Src="~/Common/ProtectionAndEventDetails.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlSelectServiceCenter" Src="../Common/UserControlServiceCenterSelection.ascx" %>

<asp:Content ID="content1" ContentPlaceHolderID="SummaryPlaceHolder" runat="server" >
    <div>
        <asp:Label ID="lblNewSCError" runat="server" Visible="false" ForeColor="Red"></asp:Label>
    </div>
    <div>
        <Elita:ProtectionAndEventDetails ID="moProtectionEvtDtl" runat="server" align="center" />
    </div>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div >
        <Elita:UserControlSelectServiceCenter ID="ucSelectServiceCenter" runat="server"/>
    </div>

    <div class="btnZone">
        <asp:Button runat="server" ID="btnBack" Text="Back" SkinID="AlternateLeftButton" />
        <asp:Button runat="server" ID="btnSend" Text="SEND_QUOTE" SkinID="PrimaryRightButton" />
    </div>
</asp:Content>
