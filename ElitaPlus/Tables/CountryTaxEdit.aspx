<%@ Register TagPrefix="uc1" TagName="CountryTaxUserControl" Src="../Common/CountryTaxUserControl.ascx" %>
<%@ Register TagPrefix="uc2" Src="../Common/MultipleColumnDDLabelControl_new.ascx"
    TagName="MultipleColumnDDLabelControl" %>
<%@ Register TagPrefix="Elita" TagName="MessageController" Src="~/Common/MessageController.ascx" %>

<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CountryTaxEdit.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.CountryTaxEdit" MasterPageFile="../Navigation/masters/ElitaBase.Master"
    Theme="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script type="text/javascript">
    function preventBackspace(e) {
        var evt = e || window.event;
        if (evt) {
            var keyCode = evt.charCode || evt.keyCode;
            if (keyCode === 8) {
                if (evt.preventDefault) {
                    evt.preventDefault();
                } else {
                    evt.returnValue = false;
                }
            }
        }
    }
</script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
        <table id="Table13" class="formGrid" width="100%" cellspacing="0" cellpadding="0"
            border="0" style="padding-left: 0px;">
            <tr>
                <td nowrap="nowrap" align="left" class="borderLeft" width="10%">
                    <asp:Label ID="lblTaxType" runat="server" Font-Bold="false">TAX_TYPE</asp:Label>
                </td>
                <td nowrap="nowrap" align="left" width="25%">
                    <asp:DropDownList ID="dlstTaxType_WRITE" runat="server" SkinID="SmallDropDown" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:TextBox ID="dlstTaxType_WRITETextBox" runat="server" Enabled="False" ReadOnly="True" Visible="False" SkinID="SmallTextBox"></asp:TextBox>
                </td>
                <td nowrap="nowrap" align="left" width="10%">
                    <asp:Label ID="lblCompanyType" runat="server" Font-Bold="false">COMPANY_TYPE</asp:Label>
                </td>
                <td nowrap="nowrap" align="left" width="25%">
                    <asp:DropDownList ID="cboCompanyType" runat="server" SkinID="SmallDropDown" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:TextBox ID="cboCompanyTypeTextBox" runat="server" Enabled="False" ReadOnly="True" Visible="False" SkinID="SmallTextBox"></asp:TextBox>
                </td>
                <td id="tdWithholdingCheck" runat="server"  nowrap="nowrap" align="left" width="30%">
                        <asp:Label ID="lblWithholdingRate" runat="server">APPLY_WITHHOLDING</asp:Label>:
                        <asp:CheckBox ID="CheckBoxApplyWithholding" TabIndex="19" runat="server"></asp:CheckBox>
                </td>
            </tr>
            <tr>
                <td nowrap="nowrap" align="left" class="borderLeft" width="10%">
                    <asp:Label ID="lblEffectiveDate" runat="server" Font-Bold="false">EFFECTIVE_DATE</asp:Label>
                </td>
                <td nowrap="nowrap" align="left" width="25%">
                    <asp:TextBox ID="txtEffectiveDate" runat="server" SkinID="SmallTextBox" onKeyDown="preventBackspace();"></asp:TextBox>&nbsp;
                    <asp:ImageButton ID="btnEffectiveDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                    </asp:ImageButton>
                </td>
                <td nowrap="nowrap" align="left" width="10%">
                    <asp:Label ID="lblCountry" runat="server" Font-Bold="false">COUNTRY</asp:Label>
                </td>
                <td nowrap="nowrap" align="left" width="25%">
                    <asp:DropDownList ID="cboCountry" runat="server" SkinID="SmallDropDown" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:TextBox ID="cboCountryTextBox" runat="server" Enabled="False" ReadOnly="True" Visible="False" SkinID="SmallTextBox"></asp:TextBox>
                </td>
                <td nowrap="nowrap" align="left" width="30%">
                </td>
            </tr>
            <tr>
                <td nowrap="nowrap" align="left" class="borderLeft" width="10%">
                    <asp:Label ID="lblExpirationDate" runat="server" Font-Bold="false">EXPIRATION_DATE</asp:Label>
                </td>
                <td nowrap="nowrap" align="left" width="25%">
                    <asp:TextBox ID="txtExpirationDate" runat="server" SkinID="SmallTextBox" onKeyDown="preventBackspace();"></asp:TextBox>&nbsp;
                    <asp:ImageButton ID="btnExpirationDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                    </asp:ImageButton>
                </td>
                <td nowrap="nowrap" align="left" width="10%">
                    <asp:Label ID="lblProductTaxType" runat="server" Font-Bold="false">PRODUCT_TAX_TYPE</asp:Label>
                </td>
                <td nowrap="nowrap" align="left" width="25%">
                    <asp:DropDownList ID="cboProductTaxType" runat="server" SkinID="SmallDropDown" AutoPostBack="True">
                    </asp:DropDownList>
                    <asp:TextBox ID="cboProductTaxTypeTextBox" runat="server" Enabled="False" ReadOnly="True" Visible="False" SkinID="SmallTextBox"></asp:TextBox>
                </td>
                <td nowrap="nowrap" align="left" width="30%">
                </td>
            </tr>
        </table>
        <table id="Table3" class="formGrid" width="100%" cellspacing="0" cellpadding="0" style="padding-left: 0px;">
            <tr>
                <td nowrap="nowrap" align="left" class="borderLeft" width="10%">
                </td>
                <td nowrap="nowrap" align="left" width="25%">
                    <table id="Table2" class="formGrid" border="0" width="50%" cellspacing="0" cellpadding="0"
                        style="padding-left: 0px;">
                        <tr>
                            <td align="center">
                                <uc2:MultipleColumnDDLabelControl ID="moDealerMultipleDrop" runat="server"></uc2:MultipleColumnDDLabelControl>
                            </td>
                        </tr>
                    </table>
                </td>
                <td nowrap="nowrap" align="left" width="10%">
                </td>
                <td nowrap="nowrap" align="left" width="25%">
                </td>
                <td nowrap="nowrap" align="left" width="30%">
                </td>
            </tr>
        </table>
        <hr style="height: 1px" />
        <table id="Table1" class="formGrid" border="0" width="100%" cellspacing="0" cellpadding="0"
            style="padding-left: 0px;">
            <tr>
                <td nowrap="nowrap" align="left" class="borderLeft">
                    &nbsp;
                    <asp:Label ID="Label60" runat="server">TAX1</asp:Label>
                    <uc1:CountryTaxUserControl ID="CountryTaxUserControl1" runat="server"></uc1:CountryTaxUserControl>
                </td>
                <td align="left">
                    &nbsp;
                    <asp:Label ID="Label65" runat="server">TAX4</asp:Label>
                    <uc1:CountryTaxUserControl ID="CountryTaxUserControl4" runat="server"></uc1:CountryTaxUserControl>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr style="height: 1px">
                </td>
            </tr>
            <tr>
                <td nowrap="nowrap" align="left" class="borderLeft">
                    &nbsp;
                    <asp:Label ID="Label61" runat="server">TAX2</asp:Label>
                    <uc1:CountryTaxUserControl ID="CountryTaxUserControl2" runat="server"></uc1:CountryTaxUserControl>
                </td>
                <td nowrap="nowrap" align="left">
                    &nbsp;
                    <asp:Label ID="Label64" runat="server">TAX5</asp:Label>
                    <uc1:CountryTaxUserControl ID="CountryTaxUserControl5" runat="server"></uc1:CountryTaxUserControl>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr style="height: 1px"/>
                </td>
            </tr>
            <tr>
                <td nowrap="nowrap" align="left" class="borderLeft">
                    &nbsp;
                    <asp:Label ID="Label62" runat="server">TAX3</asp:Label>
                    <uc1:CountryTaxUserControl ID="CountryTaxUserControl3" runat="server"></uc1:CountryTaxUserControl>
                </td>
                <td align="left">
                    &nbsp;
                    <asp:Label ID="Label63" runat="server">TAX6</asp:Label>
                    <uc1:CountryTaxUserControl ID="CountryTaxUserControl6" runat="server"></uc1:CountryTaxUserControl>
                </td>
            </tr>
            <tr>
                <td colspan="2">
                    <hr style="height: 1px"/>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left" >
                    <asp:Label ID="lblRegionList" runat="server" Visible="False">REGION</asp:Label>
                </td>
                <td nowrap="nowrap" align="left">
                    <asp:Label ID="lblRegionTax" runat="server" Visible="False">REGION TAX</asp:Label>
                </td>
            </tr>
            <tr>
                <td valign="top" align="left">
                    <br />
                    <asp:ListBox ID="lstRegion" TabIndex="80" runat="server" Height="70px" Width="190px"
                        AutoPostBack="True" Visible="False" CssClass="FLATTEXT"></asp:ListBox>
                </td>
                <td nowrap="nowrap" align="left">
                    <br />
                    <asp:ListBox ID="lstRegionTax" runat="server" Height="70px" Width="190px" AutoPostBack="True"
                        Visible="False"></asp:ListBox>
                    &nbsp;<asp:Button ID="btnNewRegionTax_WRITE" runat="server" Visible="False" CausesValidation="False"
                        Text="New Region Tax" SkinID="ActionButton"></asp:Button>
                </td>
            </tr>
        </table>
        <div>
            <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
                runat="server" />
        </div>
    </div>
    <div class="btnZone">
        <asp:Button ID="btnSave_WRITE" runat="server" Text="Save" CommandName="WRITE" SkinID="PrimaryRightButton"></asp:Button>
        <asp:Button ID="btnUndo_WRITE" runat="server" Text="Undo" SkinID="AlternateRightButton"></asp:Button>
        <asp:Button ID="btnCopy_WRITE" runat="server" CausesValidation="False" Text="NEW_WITH_COPY" CommandName="WRITE" SkinID="AlternateRightButton"></asp:Button>
        <asp:Button ID="btnCancel" runat="server" CausesValidation="False" Text="Back" SkinID="PrimaryLeftButton">
        </asp:Button>
        <asp:Button ID="btnNew_WRITE" runat="server" CausesValidation="False" Text="New"
            CommandName="WRITE" SkinID="AlternateLeftButton"></asp:Button>
        <asp:Button ID="btnDelete_WRITE" runat="server" CausesValidation="False" Text="Delete"
            CommandName="WRITE" SkinID="CenterButton"></asp:Button>
    </div>
</asp:Content>
