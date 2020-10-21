﻿<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CertExtendedItemForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.CertExtendedItemForm" MasterPageFile="../Navigation/masters/ElitaBase.Master"
    EnableSessionState="True" Theme="Default" %>

<%@ Register TagPrefix="Elita" TagName="UserControlAvailableSelected" Src="../Common/UserControlAvailableSelected_new.ascx" %>
<%@ Register TagPrefix="mytab" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register Assembly="Microsoft.Web.UI.WebControls" Namespace="Microsoft.Web.UI.WebControls" TagPrefix="iewc" %>
<asp:Content ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script src="../Navigation/scripts/Common.js" type="text/javascript"></script>
</asp:Content>
<asp:Content ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table id="TableFixed" cellspacing="0" cellpadding="0" border="0" width="60%" class="summaryGrid">
        <tbody>
            <tr>
                <td align="left">
                    <asp:Label ID="LabelCertItemConfigCode" runat="server">CERT_ITEM_CONFIG_CODE</asp:Label>
                </td>
                <td>
                    <asp:TextBox ID="TextboxCertItemConfigCode" TabIndex="1" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                </td>
                <td nowrap="nowrap" align="right">
                    <asp:Label ID="LabelCertItemConfigDesc" runat="server">CERT_ITEM_CONFIG_DESC</asp:Label>
                </td>
                <td nowrap="nowrap">
                    <asp:TextBox ID="TextboxCertItemConfigDesc" TabIndex="2" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
<asp:Content ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:Panel runat="server" ID="WorkingPanel">
        <div class="dataContainer">
            <table width="100%" border="0" class="dataGrid" cellpadding="0" cellspacing="0">
                <tr id="trPageSize" runat="server">
                    <td class="bor">
                        <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>: &nbsp;
                        <asp:DropDownList ID="cboPageSize" runat="server" Width="50px" AutoPostBack="true">
                            <asp:ListItem Value="5">5</asp:ListItem>
                            <asp:ListItem Selected="True" Value="10">10</asp:ListItem>
                            <asp:ListItem Value="15">15</asp:ListItem>
                            <asp:ListItem Value="20">20</asp:ListItem>
                            <asp:ListItem Value="25">25</asp:ListItem>
                            <asp:ListItem Value="30">30</asp:ListItem>
                            <asp:ListItem Value="35">35</asp:ListItem>
                            <asp:ListItem Value="40">40</asp:ListItem>
                            <asp:ListItem Value="45">45</asp:ListItem>
                            <asp:ListItem Value="50">50</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="right" class="bor">
                        <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                        <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
                            runat="server" />
                    </td>
                </tr>
            </table>
            <div>
                <asp:GridView ID="GridViewCertItemConfig" runat="server" Width="100%" AutoGenerateColumns="False"
                    AllowPaging="True" CellPadding="1" SkinID="DetailPageGridView" ShowHeaderWhenEmpty="true">
                    <RowStyle HorizontalAlign="Left" />
                    <Columns>
                        <asp:TemplateField HeaderText="FIELD NAME" SortExpression="FIELD_NAME">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="FieldNameLabel"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" ID="FieldNameTextBox">
                                </asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="IN_ENROLLMENT">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="InEnrollmentLabel"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList runat="server" ID="InEnrollmentDropDown" AutoPostBack="true" OnSelectedIndexChanged="InEnrollment_SelectedIndexChanged">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="DEFAULT_VALUE" SortExpression="DEFAULT_VALUE">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="DefaultValueLabel"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:TextBox runat="server" ID="DefaultValueTextBox">
                                </asp:TextBox>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="ALLOW_UPDATE" SortExpression="ALLOW_UPDATE">
                            <ItemTemplate>
                                <asp:Label runat="server" ID="AllowUpdateLabel"></asp:Label>
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:DropDownList runat="server" ID="AllowUpdateDropDown">
                                </asp:DropDownList>
                            </EditItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:ImageButton runat="server" ID="EditButton" CommandName="EditRecord" AlternateText="Edit" ImageUrl="~/App_Themes/Default/Images/edit.png" />
                                <asp:ImageButton runat="server" ID="DeleteButton" CommandName="DeleteRecord" AlternateText="Delete" ImageUrl="~/App_Themes/Default/Images/icon_delete.png" />
                            </ItemTemplate>
                            <EditItemTemplate>
                                <asp:ImageButton runat="server" ID="SaveButton" AlternateText="Save" CommandName="SaveRecord" ImageUrl="~/App_Themes/Default/Images/save.png" />
                                <asp:ImageButton runat="server" ID="CancelButton" AlternateText="Cancel" CommandName="CancelRecord" ImageUrl="~/App_Themes/Default/Images/cancel.png" />
                            </EditItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings PageButtonCount="10" Mode="Numeric" Position="TopAndBottom" />
                </asp:GridView>
            </div>
        </div>
        <div class="btnZone">
            <asp:Button ID="btnAdd" runat="server" SkinID="AlternateLeftButton" Text="Add_New" />
            <%--<asp:Button runat="server" ID="btnSave" Text="Save" SkinID="AlternateLeftButton" />--%>
        </div>
        <div class="dataContainer">
            <table id="TableConfig" cellspacing="0" cellpadding="0" border="0" width="100%" class="formGrid">
                <tbody>
                    <tr>
                        <td align="left">
                            <asp:Label ID="Label1" runat="server">CONFIGURATION_BY</asp:Label>
                        </td>

                    </tr>
                    <tr>
                        <td align="left">
                            <asp:RadioButton ID="rdoDealers" runat="server" Text="Company" Checked="True" AutoPostBack="false" GroupName="ConfigGroup"></asp:RadioButton>
                            &nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:RadioButton ID="rdoCompanies" runat="server" Text="Dealer" AutoPostBack="false" GroupName="ConfigGroup"></asp:RadioButton>
                        </td>
                    </tr>

                </tbody>
            </table>
        </div>
        <div class="dataContainer">
            <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0" />
            <asp:HiddenField ID="hdnDisabledTab" runat="server" />
            <div id="tabs" class="style-tabs">
                <ul>
                    <li><a href="#tab_Company">
                        <asp:Label ID="lblCompanyTab" runat="server" CssClass="tabHeaderText">COMPANY_TAB</asp:Label></a></li>
                    <li><a href="#tab_Dealer">
                        <asp:Label ID="lblDealerTab" runat="server" CssClass="tabHeaderText">DEALER_TAB</asp:Label></a></li>
                </ul>
                <div id="tab_Company">
                    <div class="Page" runat="server" style="height: 100%; overflow: auto">
                        <Elita:UserControlAvailableSelected ID="UserControlAvailableSelectedCompanies" runat="server" />
                    </div>
                </div>
                <div id="tab_Dealer">
                    <div class="Page" runat="server" style="height: 100%; overflow: auto">
                        <Elita:UserControlAvailableSelected ID="UserControlAvailableSelectedDealers" runat="server" />
                    </div>
                </div>
            </div>
            <div class="btnZone">
                <asp:Button runat="server" ID="btnSaveConfig" Text="Save" SkinID="AlternateLeftButton" />
            </div>
        </div>
    </asp:Panel>
</asp:Content>
