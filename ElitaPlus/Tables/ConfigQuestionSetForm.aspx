﻿<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="../Navigation/masters/ElitaBase.Master" Theme="Default" CodeBehind="ConfigQuestionSetForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.ConfigQuestionSetForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
        <h2 class="dataGridHeader">
            <asp:Label runat="server" ID="lbl1">CONFIG_QUESTION_SET</asp:Label></h2>
        <asp:Panel ID="EditPanel_WRITE" runat="server" Width="100%">
            <div class="stepformZone">
                <table border="0" class="formGrid" cellpadding="0" cellspacing="0">
                    <tr>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="lblCompnayGrp" runat="server" Font-Bold="False">COMPANY_GROUP:</asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:DropDownList ID="ddlCompanyGroup" runat="server" AutoPostBack="false" TabIndex="1" SkinID="MediumDropDown"></asp:DropDownList>
                        </td>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="lblCompany" runat="server" Font-Bold="False">COMPANY:</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCompany" runat="server" AutoPostBack="true" TabIndex="2" SkinID="MediumDropDown"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="lblDealerGroup" runat="server" Font-Bold="False">DEALER_GROUP:</asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:DropDownList ID="ddlDealerGroup" runat="server" AutoPostBack="true" TabIndex="4" SkinID="MediumDropDown"></asp:DropDownList>
                        </td>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="lblDealer" runat="server" Font-Bold="False">DEALER:</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDealer" runat="server" AutoPostBack="true" TabIndex="5" SkinID="MediumDropDown"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="lblProdCode" runat="server" Font-Bold="False">PRODUCT_CODE:</asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:TextBox ID="txtProductCode" runat="server" TabIndex="6" SkinID="MediumTextBox" MaxLength="5"></asp:TextBox>
                            <asp:DropDownList ID="ddlProductCode" runat="server" AutoPostBack="false" TabIndex="6" SkinID="MediumDropDown"></asp:DropDownList>
                        </td>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="lblDeviceType" runat="server" Font-Bold="False">DEVICE_TYPE:</asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:DropDownList ID="ddlDeviceType" runat="server" AutoPostBack="false" TabIndex="7" SkinID="MediumDropDown"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="lblCoverageType" runat="server" Font-Bold="False">COVERAGE_TYPE:</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCoverageType" runat="server" AutoPostBack="false" SkinID="MediumDropDown" TabIndex="7"></asp:DropDownList>
                        </td>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="lblRiskType" runat="server" Font-Bold="False">RISK_TYPE:</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlRiskType" runat="server" AutoPostBack="false" SkinID="MediumDropDown" TabIndex="9"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="lblPurposeCode" runat="server" Font-Bold="False">PURPOSE:</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlPurpose" runat="server" AutoPostBack="false" SkinID="MediumDropDown" TabIndex="10"></asp:DropDownList>
                        </td>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="lblQuestionSetCode" runat="server" Font-Bold="False">QUESTION_SET:</asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:DropDownList ID="ddlQuestionSetCode" runat="server" AutoPostBack="false" SkinID="MediumDropDown" TabIndex="11"></asp:DropDownList>
                        </td>
                    </tr>
                </table>
            </div>
            <div class="btnZone">
                <asp:Button runat="server" ID="btnSave_WRITE" Text="Save" SkinID="PrimaryRightButton" />
                <asp:Button runat="server" ID="btnUndo_Write" Text="Undo" SkinID="AlternateRightButton" />
                <asp:Button runat="server" ID="btnCopy_WRITE" Text="NEW_WITH_COPY" SkinID="AlternateRightButton" />
                <asp:Button runat="server" ID="btnNew_WRITE" Text="New" SkinID="AlternateRightButton" />
                <asp:Button runat="server" ID="btnDelete_WRITE" Text="Delete" SkinID="AlternateRightButton" />
                <asp:Button runat="server" ID="btnBack" Text="Back" SkinID="AlternateLeftButton" />
            </div>
            <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse" runat="server" />
        </asp:Panel>
    </div>
</asp:Content>
