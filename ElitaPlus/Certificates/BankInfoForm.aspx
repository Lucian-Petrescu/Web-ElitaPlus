<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BankInfoForm.aspx.vb"
    Theme="Default" MasterPageFile="../Navigation/masters/ElitaBase.Master" EnableSessionState="True"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Certificates.BankInfoForm" %>

<%@ Register TagPrefix="Elita" TagName="UserControlCertificateInfo" Src="UserControlCertificateInfo_New.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlBankInfo" Src="../Common/UserControlBankInfo_New.ascx" %>

<%@ Import Namespace="System.Web.UI.HtmlControls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table id="TableFixed" cellspacing="0" cellpadding="0" border="0" width="80%" class="summaryGrid">
        <tbody>
            <Elita:UserControlCertificateInfo ID="moCertificateInfoController" runat="server" />
        </tbody>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
        <div class="stepformZone">
            <asp:Panel ID="EditPanel_WRITE" runat="server" Width="100%">
                <div id="div1" runat="server">
                    <table width="100%" class="formGrid" border="0">
                        <tbody>
                            <Elita:UserControlBankInfo ID="moPremiumBankInfoController" runat="server" />
                        </tbody>
                    </table>
                </div>
            </asp:Panel>
        </div>
    </div>
    <div class="btnZone">
        <asp:Button ID="btnUndo_WRITE" runat="server" Text="UNDO" SkinID="PrimaryRightButton"></asp:Button>&nbsp;
        <asp:Button ID="btnBack" runat="server" SkinID="AlternateLeftButton" Text="BACK"></asp:Button>
        <asp:Button ID="btnApply_WRITE" runat="server" Text="SAVE" SkinID="PrimaryRightButton"></asp:Button>&nbsp;
        <asp:Button ID="btnEdit_WRITE" runat="server" Text="EDIT" SkinID="PrimaryRightButton"></asp:Button>&nbsp;
    </div>
    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse" runat="server" />
</asp:Content>
