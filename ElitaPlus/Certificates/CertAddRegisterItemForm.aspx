<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CertAddRegisterItemForm.aspx.vb"
    Theme="Default" MasterPageFile="../Navigation/masters/ElitaBase.Master" EnableSessionState="True"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Certificates.CertAddRegisterItemForm" %>

<%@ Register TagPrefix="Elita" TagName="UserControlCertificateInfo" Src="UserControlCertificateInfo_New.ascx" %>
<%@ Import Namespace="System.Web.UI.HtmlControls" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table id="TableFixed"  cellspacing="0" cellpadding="0" border="0" width="80%" class="summaryGrid">
        <tbody>
            <Elita:UserControlCertificateInfo ID="moCertificateInfoController" runat="server" >
            </Elita:UserControlCertificateInfo>
        </tbody>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
        <div class="stepformZone">
            <asp:Panel ID="EditPanel_WRITE" runat="server" Width="100%">
                <table id="Table1" class="formGrid">
                     <tr>
                        <td align="right">
                            <asp:Label ID="RegItemNameLabel" runat="server">REGISTERED_ITEM_NAME</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="RegItemNameText"  runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="ItemDescLabel" runat="server">ITEM_DESCRIPTION</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="ItemDescText" runat="server"  SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="SerialNumberLabel" runat="server">SERIAL_NUMBER</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="SerialNumberText" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="DeviceTypeLabel" runat="server">DEVICE_TYPE</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="cboDeviceType" runat="server" SkinID="MediumDropDown">
                            </asp:DropDownList>
                       </td>
                    </tr>
                    <tr>
                      <td align="right">
                            <asp:Label ID="ModelLabel" runat="server">MODEL</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="ModelText" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="ManufacturerLabel" runat="server">MANUFACTURER</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="cboManufacturerId" AutoPostBack="true" runat="server" SkinID="MediumDropDown">
                            </asp:DropDownList>
                            <asp:TextBox ID="ManufacturerTextBox" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                         </td>
    
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="PurchasedDateLabel" runat="server">PURCHASED_DATE</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="PurchasedDateText_WRITE" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                            <asp:ImageButton ID="ImageButtonPurchasedDate" ImageAlign="AbsMiddle" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                            </asp:ImageButton>
                        </td>
                        <td align="right">
                            <asp:Label ID="PurchasePriceLabel" runat="server">PURCHASE_PRICE</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="PurchasePriceText" runat="server" SkinID="SmallTextBox" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="RegistrationDateLabel" runat="server">REGISTRATION_DATE</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="RegistrationDateText" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                            <asp:ImageButton ID="RegistrationDateImageButton" ImageAlign="AbsMiddle" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                            </asp:ImageButton>
                        </td>
                        <td align="right">
                            <asp:Label ID="RetailPriceLabel" runat="server">RETAIL_PRICE</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="RetailPriceText" runat="server" SkinID="MediumTextBox" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="IndixIDLabel" runat="server">INDIXID</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="IndixIDText" runat="server" SkinID="MediumTextBox" ></asp:TextBox>
                        </td>
                    </tr>
                </table>
             </asp:Panel>
        </div>
    </div>
    <div class="btnZone">
        <asp:Button ID="btnUndo_WRITE" runat="server" CausesValidation="False" Text="UNDO" SkinID="AlternateRightButton" ></asp:Button>&nbsp;
        <asp:Button ID="btnBack" runat="server" CausesValidation="False" SkinID="AlternateLeftButton" Text="BACK" ></asp:Button>
       <asp:Button ID="btnApply_WRITE" runat="server" Text="SAVE" SkinID="CenterButton" ></asp:Button>&nbsp;
        
    </div>
    
    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
            runat="server"/>
  </asp:Content>