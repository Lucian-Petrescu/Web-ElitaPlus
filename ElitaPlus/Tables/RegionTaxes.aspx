<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RegionTaxes.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.RegionTaxes"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" Theme="Default" %>

<%@ Register TagPrefix="uc1" TagName="RegionTaxUserControl" Src="../Common/RegionTaxUserControl.ascx" %>
<%@ Register TagPrefix="Elita" TagName="MessageController" Src="~/Common/MessageController.ascx" %>
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
    <table id="table2" class="formGrid" width="100%" border="0" cellspacing="0" cellpadding="0"
        style="padding-left: 0px;">
        <tr>
            <td nowrap="nowrap" align="left" class="borderLeft">
                <asp:Label ID="Label9" runat="server">REGION</asp:Label>: 
                <asp:Label ID="lblRegion" runat="server" ForeColor="Black"></asp:Label>
            </td>  
        </tr>
        <tr>
            <td nowrap="nowrap" align="left" class="borderLeft">
            
                <asp:Label ID="Label10" runat="server" >DEALER</asp:Label>
                <asp:Label ID="Label11" runat="server" >CODE:</asp:Label>
                <asp:Label ID="lblDealer" runat="server" ForeColor="Black" SkinID="SummaryLabel"></asp:Label>&nbsp;&nbsp;&nbsp;
                <asp:Label ID="Label12" runat="server" >DESCRIPTION:</asp:Label>
                 <asp:Label ID="lblDealerDesc" runat="server" ForeColor="Black" SkinID="SummaryLabel"></asp:Label>
            </td>
        </tr>
        <tr>
            <td nowrap="nowrap" align="left" class="borderLeft">
                <asp:Label ID="Label66" runat="server">COMPANY_TYPE</asp:Label>: 
 		        <asp:Label ID="lblCompanyType" runat="server" ForeColor="Black"></asp:Label>
            </td>  
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
        <table id="tableGrid" class="formGrid" width="100%" border="0" cellspacing="0" cellpadding="0"
            style="padding-left: 0px;">
            <tr>
                <td nowrap="nowrap" align="right" class="borderLeft" style="width:10%">
                    <asp:Label ID="Label4" runat="server">*</asp:Label>
                    <asp:Label ID="Label56" runat="server">TAX_TYPE</asp:Label>:
                </td>
                <td nowrap="nowrap" align="left" style="width:30%">
                    <asp:TextBox ID="txtTaxType" runat="server" SkinID="SmallTextBox"  Enabled="False"></asp:TextBox>
                </td>
                <td nowrap="nowrap" align="right" style="width:10%">
                    <asp:Label ID="Label8" runat="server">*</asp:Label>
                    <asp:Label ID="moProductTaxTypeLabel" runat="server">PRODUCT_TAX_TYPE</asp:Label>:
                </td>
                <td nowrap="nowrap" align="left" style="width:50%">
                    <asp:TextBox ID="txtProductTaxType" runat="server" SkinID="SmallTextBox" Enabled="False"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td nowrap="nowrap" align="right" class="borderLeft" style="width:10%">
                    <asp:Label ID="Label57" runat="server">*</asp:Label>
                    <asp:Label ID="Label26" runat="server">EFFECTIVE_DATE</asp:Label>:
                </td>
                <td nowrap="nowrap" align="left" style="width:30%">
                    <asp:TextBox ID="txtEffectiveDate" runat="server" SkinID="SmallTextBox" onKeyDown="preventBackspace();"></asp:TextBox>
                    <asp:ImageButton ID="btnEffectiveDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                    </asp:ImageButton>
                </td>
                <td nowrap="nowrap" align="right" colspan="1" rowspan="1" style="width:10%">
                    <asp:Label ID="Label59" runat="server">*</asp:Label>
                    <asp:Label ID="Label27" runat="server">EXPIRATION_DATE</asp:Label>:
                </td>
                <td nowrap="nowrap" align="left" style="width:50%">
                    <asp:TextBox ID="txtExpirationDate" runat="server" SkinID="SmallTextBox" onKeyDown="preventBackspace();"></asp:TextBox>
                    <asp:ImageButton ID="btnExpirationDate" runat="server" ImageUrl="../Common/Images/calendarIcon2.jpg">
                    </asp:ImageButton>
                </td>
            </tr>
            <tr>
                <td style="height: 1px" colspan="4">
                    <hr style="height: 1px">
                </td>
            </tr>
        </table>
        <table id="table1" class="formGrid" width="100%" border="0" cellspacing="0" cellpadding="0"
            style="padding-left: 0px;">
            <tr>
                <td nowrap="nowrap" align="left" class="borderLeft">
                    &nbsp;
                    <asp:Label ID="Label7" runat="server">TAX1</asp:Label>:
                    <uc1:RegionTaxUserControl ID="RegionTaxUserControl1" runat="server"></uc1:RegionTaxUserControl>
                </td>
                <td nowrap="nowrap" align="left">
                    &nbsp;
                    <asp:Label ID="Label6" runat="server">TAX4</asp:Label>:
                    <uc1:RegionTaxUserControl ID="RegionTaxUserControl4" runat="server"></uc1:RegionTaxUserControl>
                </td>
            </tr>
            <tr>
                <td style="height: 1px" colspan="2">
                    <hr style="height: 1px">
                </td>
            </tr>
            <tr>
                <td nowrap="nowrap" align="left" class="borderLeft">
                    &nbsp;
                    <asp:Label ID="Label5" runat="server">TAX2</asp:Label>:
                    <uc1:RegionTaxUserControl ID="RegionTaxUserControl2" runat="server"></uc1:RegionTaxUserControl>
                </td>
                <td nowrap="nowrap" align="left">
                    &nbsp;
                    <asp:Label ID="Label3" runat="server">TAX5</asp:Label>:
                    <uc1:RegionTaxUserControl ID="RegionTaxUserControl5" runat="server"></uc1:RegionTaxUserControl>
                </td>
            </tr>
            <tr>
                <td style="height: 1px" colspan="2">
                    <hr style="height: 1px">
                </td>
            </tr>
            <tr>
                <td nowrap="nowrap" align="left" class="borderLeft">
                    &nbsp;
                    <asp:Label ID="Label2" runat="server">TAX3</asp:Label>:
                    <uc1:RegionTaxUserControl ID="RegionTaxUserControl3" runat="server"></uc1:RegionTaxUserControl>
                </td>
                <td nowrap="nowrap" align="left">
                    &nbsp;
                    <asp:Label ID="Label1" runat="server">TAX6</asp:Label>:
                    <uc1:RegionTaxUserControl ID="RegionTaxUserControl6" runat="server"></uc1:RegionTaxUserControl>
                </td>
            </tr>
        </table>
        <div class="btnZone">
            <asp:Button ID="btnBack" runat="server" CausesValidation="False" Text="Back" SkinID="AlternateLeftButton">
            </asp:Button>&nbsp;
            <asp:Button ID="btnSave_WRITE" runat="server" Text="Save" SkinID="PrimaryRightButton">
            </asp:Button>&nbsp;
            <asp:Button ID="btnUndo_WRITE" runat="server" Text="Undo" SkinID="AlternateRightButton">
            </asp:Button>&nbsp;
            <asp:Button ID="btnNew_WRITE" runat="server" Text="New" SkinID="AlternateLeftButton">
            </asp:Button>&nbsp;
            <asp:Button ID="btnCopy_WRITE" runat="server" CausesValidation="False" Text="NEW_WITH_COPY" SkinID="AlternateRightButton">
            </asp:Button>&nbsp;
            <asp:Button ID="btnDelete_WRITE" runat="server" CausesValidation="False" Text="Delete" SkinID="CenterButton">
            </asp:Button>
        </div>
        <div>
            <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
                runat="server" />
        </div>
    </div>
</asp:Content>
