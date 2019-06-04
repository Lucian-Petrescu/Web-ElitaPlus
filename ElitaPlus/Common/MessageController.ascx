<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="MessageController.ascx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.MessageController" %>
<div class="dataContainer" style="margin-bottom:0px">
    <div runat="server" id="MessageBox" class="errorMsg">
        <p>
            <img width="16" height="13" align="middle" runat="server" id="moIconImage" />
            <asp:Literal runat="server" ID="MessageLiteral" />
        </p>
    </div>
</div>
