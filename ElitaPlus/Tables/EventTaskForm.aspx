<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="../Navigation/masters/ElitaBase.Master" Theme="Default" CodeBehind="EventTaskForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.EventTaskForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
        <h2 class="dataGridHeader">
            <asp:Label runat="server" ID="lbl1">EVENT_TASK</asp:Label></h2>
        <asp:Panel ID="EditPanel_WRITE" runat="server" Width="100%">
            <div class="stepformZone">
                <table border="0" class="formGrid" cellpadding="0" cellspacing="0">
                    <tr>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="lblCompnayGrp" runat="server" Font-Bold="False">COMPANY_GROUP:</asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:DropDownList ID="ddlCompanyGroup" runat="server" AutoPostBack="true" TabIndex="1" SkinID="MediumDropDown"></asp:DropDownList>
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
                            <asp:Label ID="lblCountry" runat="server" Font-Bold="False">COUNTRY:</asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:DropDownList ID="ddlCountry" runat="server" AutoPostBack="true" TabIndex="3" SkinID="MediumDropDown"></asp:DropDownList>
                        </td>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="lblDealerGroup" runat="server" Font-Bold="False">DEALER_GROUP:</asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:DropDownList ID="ddlDealerGroup" runat="server" AutoPostBack="true" TabIndex="4" SkinID="MediumDropDown"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="lblDealer" runat="server" Font-Bold="False">DEALER:</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlDealer" runat="server" AutoPostBack="true" TabIndex="5" SkinID="MediumDropDown"></asp:DropDownList>
                        </td>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="lblProdCode" runat="server" Font-Bold="False">PRODUCT_CODE:</asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:TextBox ID="txtProdCode" TabIndex="6" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="lblCoverageType" runat="server" Font-Bold="False">COVERAGE_TYPE:</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlCoverageType" runat="server" AutoPostBack="true" SkinID="MediumDropDown" TabIndex="7"></asp:DropDownList>
                        </td>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="lblEventType" runat="server" Font-Bold="False">EVENT_TYPE:</asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:DropDownList ID="ddlEventType" runat="server" AutoPostBack="true" TabIndex="8" SkinID="MediumDropDown"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="lblEventArgument" runat="server" Font-Bold="False">EVENT_ARGUMENT:</asp:Label>
                        </td>
                        <td>
                            <asp:DropDownList ID="ddlEventArgument" runat="server" AutoPostBack="true" SkinID="MediumDropDown" TabIndex="9"></asp:DropDownList>
                        </td>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="lblTask" runat="server" Font-Bold="False">TASK</asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:DropDownList ID="ddlTask" runat="server" SkinID="MediumDropDown" TabIndex="10"></asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="lblTimeout" runat="server" Font-Bold="False">TIMEOUT_SECONDS</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtTimeout" runat="server" SkinID="MediumTextBox" TabIndex="11"></asp:TextBox>
                        </td>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="lblRetryCount" runat="server" Font-Bold="False">RETRY_COUNT:</asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:TextBox ID="txtRetryCount" runat="server" SkinID="MediumTextBox" TabIndex="12"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="lblRetryDelay" runat="server" Font-Bold="False">RETRY_DELAY_SECONDS</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtRetryDelay" runat="server" SkinID="MediumTextBox" TabIndex="13" Width="75%"></asp:TextBox>
                        </td>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="lblEventTaskParameters" runat="server" Font-Bold="False">EVENT_TASK_PARAMETERS:</asp:Label>
                        </td>
                        <td nowrap="nowrap">
                            <asp:TextBox ID="txtEventTaskParameters" runat="server" SkinID="MediumTextBox" TabIndex="14" Width="75%"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap" align="right">
                            <asp:Label ID="lblInitDelayMinutes" runat="server" Font-Bold="False">INIT_DELAY_MINUTES</asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtInitiDelayMinutes" runat="server" SkinID="MediumTextBox" TabIndex="15" Width="75%"></asp:TextBox>
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
