<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CertItemDetailForm.aspx.vb"
    Theme="Default" MasterPageFile="../Navigation/masters/ElitaBase.Master" EnableSessionState="True" 
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Certificates.CertItemDetailForm" %>

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
                        <td align="right" >
                            <asp:Label ID="EffectiveDateLabel" runat="server">EFFECTIVE_DATE</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="EffectiveDateText" SkinID="SmallTextBox" runat="server" ></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="ExpirationDateLabel" runat="server">EXPIRATION_DATE</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="ExpirationDateText" SkinID="SmallTextBox" runat="server" ></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="ItemCodeLabel" runat="server">ITEM_CODE</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="ItemCodeText"  runat="server" SkinID="SmallTextBox"></asp:TextBox>
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
                            <asp:Label ID="ItemNumberLabel" runat="server">ITEM_NUMBER:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="ItemNumberText" runat="server"  SkinID="SmallTextBox"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="RiskTypeLabel" runat="server">RISK_TYPE</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="cboRiskTypeId" runat="server"  SkinID="MediumDropDown">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="ManufacturerLabel" runat="server">MANUFACTURER</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="cboManufacturerId" runat="server" SkinID="MediumDropDown">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            <asp:Label ID="ProductCodeLabel" runat="server">PRODUCT CODE:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtProductCode" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="SerialNumberIMEILabel" runat="server">SERIAL_NUMBER</asp:Label>
                             <asp:Label ID="SerialNumberLabel" runat="server">SERIAL_NO_LABEL</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="SerialNumberText" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="MaxReplacementCostLabel" runat="server">MAX_REPLACEMENT_COST</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="MaxReplacementCostText" runat="server" SkinID="SmallTextBox" ></asp:TextBox>
                        </td>
                    </tr>
                   <tr>
                        <td align="right">
                            <asp:Label ID="IMEINumberLabel" runat="server">IMEI_NUMBER</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="IMEINumberText" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                        </td>
                        <td align="right">
                        </td>
                        <td>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="RetailPriceLabel" runat="server">RETAIL_PRICE</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="RetailPriceText" runat="server" SkinID="SmallTextBox" ></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="ModelLabel" runat="server">MODEL</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="ModelText" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="CertProdCodeLabel" runat="server">CERT_PRODUCT_CODE:</asp:Label>
                        </td>                       
                        <td>
                            <asp:TextBox ID="CertProdCodeText" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="ReplaceReturnDateLabel" runat="server">REPLACE_RETURN_DATE</asp:Label>
                        </td>
                        
                        <td>
                            <asp:TextBox ID="ReplaceReturnDateText_WRITE" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                            <asp:ImageButton ID="ImageButtonReturnDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                            </asp:ImageButton>
                        </td>
                    </tr>
                     <tr>
                        <td align="right" >
                            <asp:Label ID="MobileTypeLabel" runat="server">MOBILE_TYPE</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="cboMobileType" runat="server" SkinID="MediumDropDown">
                            </asp:DropDownList>
                        </td>
                        <td align="right">
                            <asp:Label ID="FirstUseDateLabel" runat="server">FIRST_USE_DATE</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="FirstUseDateText" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                            <asp:ImageButton ID="ImageButtonFirstUseDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                            </asp:ImageButton>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="SimCardNumberLabel" runat="server">SIM_CARD_NUMBER</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="SimCardNumberText" SkinID="MediumTextBox" runat="server" ></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="LastUseDateLabel" runat="server">LAST_USE_DATE</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="LastUseDateText" SkinID="SmallTextBox" runat="server"></asp:TextBox>
                            <asp:ImageButton ID="ImageButtonLastUseDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                            </asp:ImageButton>
                        </td>
                    </tr>
                    <tr>
                        <td align="right">
                            <asp:Label ID="SKUNumberLabel" runat="server">SKU_NUMBER</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="SKUNumberText" SkinID="MediumTextBox" runat="server" ></asp:TextBox>
                        </td>
                         <td align="right">
                            <asp:Label ID="OriginalRetailPriceLabel" runat="server">ORIGINAL_RETAIL_PRICE</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="OriginalRetailPriceText" SkinID="MediumTextBox" runat="server" ></asp:TextBox>
                       </td>
                    </tr>
                     <tr id="trVSCOnly" runat="server" visible="false">
                        <td align="right">
                            <asp:Label ID="AllowedEventsLabel" runat="server">Allowed_Events:</asp:Label>
                        </td>                       
                        <td>
                            <asp:TextBox ID="AllowedEventsText" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="MaxInsuredAmountLabel" runat="server">Max_Insured_Amount</asp:Label>
                            </td>
                          <td>
                            <asp:TextBox ID="MaxInsuredAmountText" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                        </td>
                      </tr>
                    <tr id="trBenefitCheck" runat="server" visible="false">
                        <td align="right">
                            <asp:Label ID="Label1" runat="server">BENEFIT_STATUS</asp:Label>
                        </td>                       
                        <td>
                            <asp:TextBox ID="BenefitStatusCheckText" runat="server" SkinID="SmallTextBox" ReadOnly="true"></asp:TextBox>
                        </td>
                        <td align="right">
                            <asp:Label ID="lblIneligibleReason" runat="server">INELIGIBLE_REASON</asp:Label>
                            </td>
                          <td>
                            <asp:TextBox ID="IneligibleReasonText" runat="server" SkinID="LargeTextBox" ReadOnly="true"></asp:TextBox>
                        </td>
                      </tr>
                </table>
             </asp:Panel>
        </div>
    </div>
    <div class="btnZone">
        <asp:Button ID="btnEdit_WRITE" runat="server" CausesValidation="False" Text="EDIT" SkinID="PrimaryRightButton" ></asp:Button>
        <asp:Button ID="btnUndo_WRITE" runat="server" CausesValidation="False" Text="UNDO" SkinID="AlternateRightButton" ></asp:Button>&nbsp;
        <asp:Button ID="btnBack" runat="server" CausesValidation="False" SkinID="AlternateLeftButton" Text="BACK" ></asp:Button>
        <asp:Button ID="btnHistory_WRITE" SkinID="AlternateLeftButton" runat="server" Text="ITEM_HISTORY" ></asp:Button>&nbsp;
        <asp:Button ID="btnChangeEquipment_WRITE" SkinID="AlternateLeftButton" runat="server" Text="CHANGE_EQUIPMENT" Visible="false" ></asp:Button>&nbsp;
        <asp:Button ID="btnApply_WRITE" runat="server" Text="SAVE" SkinID="CenterButton" ></asp:Button>&nbsp;
        
    </div>
    
    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
            runat="server"/>
  </asp:Content>