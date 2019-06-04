<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="../Navigation/masters/ElitaBase.Master" Theme="Default" CodeBehind="ClaimStageForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.ClaimStageForm" %>
<%@ Register TagPrefix="ur3" TagName="UCAvailableSelected" Src="../common/UserControlAvailableSelected_New.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
        <h2 class="dataGridHeader"><asp:Label runat="server" ID="lbl1">CLAIM_STAGE</asp:Label></h2>
        <asp:Panel ID="EditPanel_WRITE" runat="server" Width="100%">
            <div class="stepformZone">
                <table border="0" class="formGrid" cellpadding="0" cellspacing="0">
                    <tr>
                        <td nowrap="nowrap" align="right">
                            <span class="mandatory">*</span><asp:Label ID="lblStageName" runat="server" Font-Bold="False">STAGE_NAME:</asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:DropDownList ID="ddlStageName" runat="server" AutoPostBack="true" TabIndex="1" SkinID="MediumDropDown"></asp:DropDownList>
                        </td>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="lblCompnayGrp" runat="server" Font-Bold="False">COMPANY_GROUP:</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCompanyGroup" runat="server" AutoPostBack="true" TabIndex="2" SkinID="MediumDropDown"></asp:DropDownList>
                        </td>

                    </tr>
                    <tr>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="lblCompany" runat="server" Font-Bold="False">COMPANY:</asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="true" TabIndex="3" SkinID="MediumDropDown"></asp:DropDownList>
                        </td>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="lblDealer" runat="server" Font-Bold="False">DEALER:</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDealer" runat="server" AutoPostBack="true" TabIndex="4" SkinID="MediumDropDown"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="lblProdCode" runat="server" Font-Bold="False">PRODUCT_CODE:</asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:TextBox ID="txtProdCode" TabIndex="5" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="lblCoverageType" runat="server" Font-Bold="False">COVERAGE_TYPE:</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCoverageType" runat="server" AutoPostBack="true" SkinID="MediumDropDown" TabIndex="6"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap" align="right">
                            <span class="mandatory">*</span><asp:Label ID="lblEffectiveDate" runat="server" Font-Bold="False">EFFECTIVE_DATE:</asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:TextBox ID="txtEffectiveDate" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                            <asp:ImageButton ID="imgEffectiveDate" runat="server" Visible="True" TabIndex="7" ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="Top" />
                        </td>
                        <td nowrap="nowrap" align="right">
                            <span class="mandatory">*</span><asp:Label ID="lblExpirationDate" runat="server" Font-Bold="False">EXPIRATION_DATE:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtExpirationDate" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                            <asp:ImageButton ID="imgExpirationDate" runat="server" Visible="True" TabIndex="8" ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="Top" />
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap" align="right">
                            <span class="mandatory">*</span><asp:Label ID="lblScreen" runat="server" Font-Bold="False">SCREEN:</asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:DropDownList ID="ddlScreen" runat="server" SkinID="MediumDropDown" TabIndex="9"></asp:DropDownList>
                        </td>
                        <td nowrap="nowrap" align="right">
                            <span class="mandatory">*</span><asp:Label ID="lblPortal" runat="server" Font-Bold="False">PORTAL:</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPortal" runat="server" SkinID="MediumDropDown" TabIndex="10"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap" align="right">
                            <span class="mandatory">*</span><asp:Label ID="lblStartStatus" runat="server" Font-Bold="False">START_STATUS:</asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:DropDownList ID="ddlStartStatus" runat="server" SkinID="MediumDropDown" TabIndex="11"></asp:DropDownList>
                        </td>
                        <td nowrap="nowrap" align="right">
                            <span class="mandatory">*</span><asp:Label ID="lblSequence" runat="server" Font-Bold="False">SEQUENCE:</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtSequence" runat="server" SkinID="MediumTextBox" TabIndex="12"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="vertical-align: top; text-align: left;" colspan="4">
                            <ur3:UCAvailableSelected ID="UC_END_STATUS_AVASEL"  runat="server" tabindex="13" ShowDownButton="false" ShowUpButton="false" AvailableDesc="AVAILABLE_END_STATUS" SelectedDesc="SELECTED_END_STATUS"></ur3:UCAvailableSelected>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="btnZone">
                <asp:Button runat="server" ID="btnSave_WRITE" Text="Save" SkinID="PrimaryRightButton" />
                <asp:Button runat="server" ID="btnUndo_Write" Text="Undo" SkinID="AlternateRightButton" />
                <asp:Button runat="server" ID="btnCopy_WRITE" Text="NEW_WITH_COPY" SkinID="AlternateRightButton" />
                <asp:Button runat="server" ID="btnNew_WRITE" Text="New" SkinID="AlternateRightButton" />
                <asp:Button runat="server" ID="btnBack" Text="Back" SkinID="AlternateLeftButton" />
            </div>
            <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse" runat="server"/>
        </asp:Panel>
    </div>
</asp:Content>