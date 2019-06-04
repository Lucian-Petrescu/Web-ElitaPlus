<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CommissionEntityForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.CommissionEntityForm"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" Theme="Default" %>

<%@ Register TagPrefix="uc1" TagName="UserControlBankInfo" Src="../Common/UserControlBankInfo.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UserControlAddress" Src="../Common/UserControlAddress_New.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto">
    </asp:ScriptManager>
    <div class="dataContainer">
        <table style="padding-left: 0px" cellspacing="0" cellpadding="0" border="0" width="50%" class="searchGrid">

            <tr>
                <td align="right" class="borderLeft">
                    <asp:Label ID="lblEntityName" runat="server">Name</asp:Label></td>
                <td>
                    <asp:TextBox ID="txtEntityName" TabIndex="1" runat="server" SkinID="MediumTextBox"></asp:TextBox></td>
                <td align="right">
                    <asp:Label ID="lblPhone" runat="server">Phone</asp:Label></td>
                <td>
                    <asp:TextBox ID="txtPhone" TabIndex="3" runat="server" SkinID="MediumTextBox"></asp:TextBox></td>
            </tr>
            <tr>
                <td align="right" class="borderLeft">
                    <asp:Label ID="lblEmail" runat="server">Email</asp:Label></td>
                <td><asp:TextBox ID="txtEmail" TabIndex="2" runat="server" SkinID="MediumTextBox"></asp:TextBox></td>
                <td>&nbsp;</td>
                <td>&nbsp;</td>
            </tr>

            
                <td align="right" class="borderLeft" style="width: 50%;">
                    <uc1:UserControlAddress ID="moAddressController" runat="server"></uc1:UserControlAddress>
                </td>
           

            <tr>
                <td align="right" class="borderLeft" style="white-space: nowrap;">
                    <asp:Label ID="lblPaymentMethod" runat="server">Payment_Method</asp:Label></td>
                <td>
                    <asp:DropDownList ID="cboPaymentMethodId" TabIndex="10" runat="server" SkinID="MediumDropDown" AutoPostBack="True"></asp:DropDownList></td>
                <td align="right">
                    <asp:Label ID="lblDisplay" runat="server">Display</asp:Label></td>
                <td>
                    <asp:DropDownList ID="cboDisplayId" TabIndex="10" runat="server" SkinID="MediumDropDown"></asp:DropDownList></td>
            </tr>
            <tr>
                <td align="right" class="borderLeft">
                    <asp:Label ID="lblTaxid" runat="server">Tax_id</asp:Label></td>
                <td>
                    <asp:TextBox ID="txtTaxid" TabIndex="3" runat="server" SkinID="MediumTextBox"></asp:TextBox></td>
                <td align="right" style="white-space: nowrap;">
                    <asp:Label ID="lblCommissionEntityTypeId" runat="server">Commission_Entity_Type_id</asp:Label></td>
                <td>
                    <asp:DropDownList ID="cboCommissionEntityTypeId" TabIndex="2" runat="server" SkinID="MediumDropDown" AutoPostBack="True"></asp:DropDownList></td>
            </tr>
            <asp:Panel ID="pnlLine" runat="server">
                <tr>
                    <td align="left" colspan="4">
                        <hr />
                    </td>
                </tr>
            </asp:Panel>
            <tr>
                <td colspan="4">
                    <uc1:UserControlBankInfo ID="moBankInfoController" runat="server"></uc1:UserControlBankInfo>
                </td>
            </tr>
            <tr>
                <td colspan="4">
                    <asp:Label ID="lblEntityId" runat="server" Visible="False"></asp:Label><asp:Label ID="moIsNewEntityLabel" runat="server" Visible="False"></asp:Label><input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
                        runat="server">
                </td>
            </tr>
        </table>
    </div>
    <div class="btnZone">
        <div class="">
            <asp:Button ID="btnApply_WRITE" runat="server" SkinID="PrimaryRightButton" Text="SAVE" />
            <asp:Button ID="btnBack" runat="server" SkinID="AlternateLeftButton" Text="BACK" />
            <asp:Button ID="btnNew_WRITE" runat="server" SkinID="AlternateLeftButton" Text="ADD" />
            <asp:Button ID="btnCopy_WRITE" runat="server" SkinID="AlternateRightButton" Text="NEW_WITH_COPY" />
            <asp:Button ID="btnUndo_WRITE" runat="server" SkinID="AlternateRightButton" Text="UNDO" />
            <asp:Button ID="btnDelete_WRITE" runat="server" SkinID="AlternateRightButton" Text="DELETE" />
        </div>
    </div>

</asp:Content>
