<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SpecialServiceForm.aspx.vb"
    ValidateRequest="false" MasterPageFile="../Navigation/masters/ElitaBase.Master"
    EnableSessionState="True" Theme="Default" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.SpecialServiceForm" %>

<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl_New.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UserControlAvailableSelected" Src="../Common/UserControlAvailableSelected_New.ascx" %>
<%@ Register TagPrefix="uc1" TagName="UserControlServiceClassType" Src="../Interfaces/ServiceClassServiceTypeControl.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
        <div class="stepformZone">
            <table class="formGrid" id="tblMain1" border="0" cellpadding="0" cellspacing="0">
                <tr>
                    <td>
                    </td>
                    <td align="center" colspan="2" valign="middle">
                        <div align="left">
                            <table cellspacing="0" cellpadding="0" border="0" style="border-left: 0px; margin-left: 0px;
                                font-size: small;">
                                <uc1:MultipleColumnDDLabelControl tabindex="1" ID="multipleDropControl" runat="server">
                                </uc1:MultipleColumnDDLabelControl>
                            </table>
                        </div>
                    </td>
                    <td>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <hr />
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="SplServiceCodeLabel" runat="server">Code</asp:Label>&nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="SplServiceCodeText" TabIndex="2" runat="server" SkinID="MediumTextBox">
                        </asp:TextBox>
                    </td>
                    <td align="right">
                        <asp:Label ID="SplServiceDescLabel" runat="server">Description</asp:Label>&nbsp;
                    </td>
                    <td>
                        <asp:TextBox ID="SplServiceDescText" TabIndex="3" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="CoverageTypeLabel" runat="server">Coverage_Type</asp:Label>&nbsp;
                    </td>
                    <td>
                        <asp:DropDownList ID="cboCoverageTypeDrop" TabIndex="4" runat="server" SkinID="MediumDropDown"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                    <td align="right">
                        <asp:Label ID="AddItemsAllowedLabel" runat="server">Add_Items_Allowed</asp:Label>&nbsp;
                    </td>
                    <td>
                        <asp:DropDownList ID="cboAddItemsAllowedDrop" TabIndex="5" runat="server" SkinID="MediumDropDown"
                            AutoPostBack="False">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="CauseOfLossLabel" runat="server">Cause_Of_Loss</asp:Label>&nbsp;
                    </td>
                    <td>
                        <asp:DropDownList ID="cboCauseOfLossDrop" TabIndex="6" runat="server" SkinID="MediumDropDown"
                            AutoPostBack="False">
                        </asp:DropDownList>
                    </td>
                    <td align="right">
                        <asp:Label ID="AddItemsCertExpLabel" runat="server" Style="white-space: nowrap">ADD_ITEMS_CERT_EXPIRED</asp:Label>&nbsp;
                    </td>
                    <td>
                        <asp:DropDownList ID="cboAddItemsCertExpDrop" TabIndex="7" runat="server" SkinID="MediumDropDown"
                            AutoPostBack="False">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right">
                        <asp:Label ID="OccurAllowedLabel" runat="server">Occurences_Allowed</asp:Label>&nbsp;
                    </td>
                    <td>
                        <asp:DropDownList ID="cboOccurAllowedDrop" TabIndex="8" runat="server" SkinID="MediumDropDown"
                            AutoPostBack="False">
                        </asp:DropDownList>
                    </td>
                    <td align="right">
                        <asp:Label ID="RepairCombinedLabel" runat="server">Combined_With_Repair</asp:Label>&nbsp;
                    </td>
                    <td>
                        <asp:DropDownList ID="cboRepairCombinedDrop" TabIndex="11" runat="server" SkinID="MediumDropDown"
                            AutoPostBack="False">
                        </asp:DropDownList>
                    </td>
                </tr>
                <tr>
                    <td align="right" style="white-space: nowrap">
                        <asp:Label ID="AvailSvcCenterLabel" runat="server">Avail_Svc_Center</asp:Label>&nbsp;
                    </td>
                    <td>
                        <asp:DropDownList ID="cboAvailSvcCenterDrop" TabIndex="10" runat="server" SkinID="MediumDropDown"
                            AutoPostBack="False">
                        </asp:DropDownList>
                    </td>
                    <td style="text-align: right; vertical-align: sub;">
                        <asp:Label ID="AuthAmtFromLabel" runat="server">Authorized_Amount_From</asp:Label>&nbsp;
                    </td>
                    <td valign="top" align="left">
                        <asp:DropDownList ID="cboAuthAmtFromDrop" TabIndex="9" runat="server" SkinID="MediumDropDown"
                            AutoPostBack="true">
                        </asp:DropDownList>
                    </td>
                </tr>
                <uc1:UserControlServiceClassType tabindex="1" ID="ServiceClassServiceTypeControl"
                    runat="server"></uc1:UserControlServiceClassType>

            
                <tr>
                    <td colspan="4">
                        <hr />
                        &nbsp;&nbsp;&nbsp;<asp:Label ID="lblProductCode" runat="server" Font-Bold="false">PRODUCT_CODES</asp:Label>:
                    </td>
                </tr>
                <tr>
                    <td align="center" colspan="4">
                        <uc1:UserControlAvailableSelected tabindex="12" ID="UserControlAvailableSelectedProductCodes"
                            style="background: #d5d6e4;" runat="server"></uc1:UserControlAvailableSelected>
                    </td>
                </tr>
            </table>
            <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
                runat="server" />
        </div>
    </div>
    <div class="btnZone">
        <div class="">
            <asp:Button ID="btnBack" runat="server" SkinID="AlternateLeftButton" Text="BACK"
                CausesValidation="False"></asp:Button>
            <asp:Button ID="btnApply_WRITE" SkinID="PrimaryRightButton" Text="SAVE" runat="server">
            </asp:Button>
            <asp:Button ID="btnUndo_WRITE" SkinID="AlternateRightButton" runat="server" Text="UNDO"
                CausesValidation="False"></asp:Button>
            <asp:Button ID="btnNew_WRITE" runat="server" Text="New" SkinID="AlternateLeftButton"
                CausesValidation="False"></asp:Button>
            <asp:Button ID="btnCopy_WRITE" runat="server" Text="New_With_Copy" SkinID="AlternateRightButton"
                CausesValidation="False"></asp:Button>
            <asp:Button ID="btnDelete_WRITE" runat="server" Text="Delete" SkinID="AlternateRightButton"
                CausesValidation="False"></asp:Button>
        </div>
    </div>
</asp:Content>
