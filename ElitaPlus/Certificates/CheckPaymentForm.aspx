<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CheckPaymentForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Certificates.CheckPaymentForm"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" Theme="Default" %>

<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="Elita" TagName="UserControlCertificateInfo" Src="UserControlCertificateInfo_New.ascx" %>
<%@ Register TagPrefix="Elita" Assembly="Assurant.ElitaPlus.WebApp" Namespace="Assurant.ElitaPlus.ElitaPlusWebApp" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table id="TableFixed" cellspacing="0" cellpadding="0" border="0" width="80%" class="summaryGrid">
        <tbody>
            <Elita:UserControlCertificateInfo ID="moCertificateInfoController" runat="server" align="center" />
        </tbody>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto">
        <Scripts>
            <asp:ScriptReference Path="~/Navigation/scripts/GlobalHeader.js" />
        </Scripts>
    </asp:ScriptManager>
    <div class="container">
        <div class="contentZoneHome">
            <div class="dataContainer">
                <h2 class="dataGridHeader">
                    <asp:Label ID="LabelTitle" runat="server">NEW_CHECK_PAYMENT</asp:Label></h2>
                <div class="stepformZone">
                    <table class="formGrid" border="0" cellspacing="0" cellpadding="0">
                        <tr>
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblCheckReceivedOn" runat="server" Font-Bold="false" Text="CHECK_RECEIVED_ON"></asp:Label>
                            </td>
                            <td nowrap="nowrap" colspan="2">
                                <asp:TextBox ID="txtCheckReceivedOn" runat="server" SkinID="SmallTextBox" ReadOnly="True"></asp:TextBox>
                                <asp:ImageButton ID="ImageButtonCheckReceivedDate" runat="server" SkinID="ImageButton" ImageUrl="../Common/Images/calendarIcon2.jpg" />
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblCheckCustomerName" runat="server" Font-Bold="false" Text="CHECK_CUSTOMER_NAME"></asp:Label>
                            </td>
                            <td nowrap="nowrap" colspan="2">
                                <asp:TextBox ID="txtCheckCustomerName" runat="server" SkinID="MediumTextBox" MaxLength="60"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblCheckBankName" runat="server" Font-Bold="false" Text="CHECK_BANK_NAME"></asp:Label>
                            </td>
                            <td nowrap="nowrap" colspan="2">
                                <asp:TextBox ID="txtCheckBankName" runat="server" SkinID="MediumTextBox" MaxLength="60"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblCheckNumber" runat="server" Font-Bold="false" Text="CHECK_NUMBER"></asp:Label>
                            </td>
                            <td nowrap="nowrap" colspan="2">
                                <asp:TextBox ID="txtCheckNumber" runat="server" SkinID="MediumTextBox" MaxLength="10"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblCheckAmount" runat="server" Font-Bold="false" Text="CHECK_AMOUNT"></asp:Label>
                            </td>
                            <td nowrap="nowrap" colspan="2">
                                <asp:TextBox ID="txtCheckAmount" runat="server" SkinID="MediumTextBox" MaxLength="13"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap="nowrap">
                                <asp:Label ID="lblCheckComments" runat="server" Font-Bold="false" Text="CHECK_COMMENTS"></asp:Label>
                            </td>
                            <td nowrap="nowrap" colspan="2">
                                <asp:TextBox ID="txtCheckComments" runat="server" TextMode="MultiLine" Rows="5"
                                    Columns="45" SkinID="MediumTextBox" MaxLength="160"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td align="right" nowrap="nowrap" colspan="3" />
                        </tr>
                    </table>
                </div>
            </div>
            <div class="btnZone">
                <asp:Button ID="btnAdd_WRITE" runat="server" SkinID="PrimaryRightButton" Text="ADD_CHECK_PAYMENT" />
                <asp:Button ID="btnBack" runat="server" SkinID="AlternateLeftButton" Text="Back" />
            </div>
        </div>
    </div>
    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
        runat="server" designtimedragdrop="261" />
</asp:Content>