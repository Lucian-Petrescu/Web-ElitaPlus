<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ConfigureWorkQueueWebService.aspx.vb"
    Theme="Default" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ConfigureWorkQueueWebService"
    EnableSessionState="True" MasterPageFile="~/Navigation/masters/ElitaBase.Master" %>

<asp:Content ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <style type="text/css">
        .success
        {
            color: Green;
        }
        .error
        {
            color: Red;
        }
        .info
        {
            color: Blue;
        }
        .exception
        {
            background-color: Gray;
            color: White;
            margin: 10px;
        }
    </style>
</asp:Content>
<asp:Content ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div runat="server" id="Output" class="dataGrid" style="font-size:small">
    </div>
    <div class="btnZone">
        <asp:Button runat="server" ID="btnFix" Text="CONFIGURE_FIX" OnClick="btnFix_Click"
            SkinID="AlternateLeftButton" />
        <asp:Button runat="server" ID="btnCreateUsers" Text="CREATE_USERS" OnClick="btnCreateUsers_Click"
            SkinID="AlternateLeftButton" />
        <asp:Button runat="server" ID="btnGrantCWQI" Text="GRANT_CWQI" OnClick="btnGrantCWQI_Click"
            SkinID="AlternateLeftButton" />
        <asp:Button runat="server" ID="btnDiagnostic" Text="DIAGNOSTICS" OnClick="btnDiagnostic_Click"
            SkinID="AlternateLeftButton" />
        <asp:Button runat="server" ID="btnRefresh" Text="REFRESH" OnClick="btnRefresh_Click"
            SkinID="AlternateRightButton" />
    </div>
</asp:Content>
