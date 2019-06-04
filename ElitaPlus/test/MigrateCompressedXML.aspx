<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MigrateCompressedXML.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.MigrateCompressedXML" 
Theme="Default" EnableSessionState="True" MasterPageFile="~/Navigation/masters/ElitaBase.Master" %>

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
        <asp:Button runat="server" ID="btnRefresh" Text="REFRESH" OnClick="btnRefresh_Click"
            SkinID="AlternateRightButton" />
        <asp:Button runat="server" ID="btnCheckRemainingRecords" Text="CHECK_RECORDS_REMAINING" OnClick="btnCheckRemainingRecords_Click"
            SkinID="AlternateLeftButton" />
        <asp:Button runat="server" ID="btnCompressXML" Text="COMPRESS_XML" OnClick="btnCompressXML_Click"
            SkinID="AlternateLeftButton" />
        
    </div>
</asp:Content>