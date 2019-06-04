<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CoverageTypeList.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Claims.CoverageTypeList" Theme="Default"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" EnableViewState="true" %>

<%@ Register TagPrefix="uc1" TagName="UserControlCertificateInfo" Src="../certificates/UserControlCertificateInfo.ascx" %>
<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <style type="text/css">
        .dataEditBox
        {
            border-left: 10px solid #0066CC;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <div class="dataEditBox">
        <table border="0" class="formGrid" cellpadding="0" cellspacing="0" width="100%">
            <tr>
                <td nowrap="nowrap" align="right">
                    <asp:Label ID="moClaimNumberLabel" runat="server" Text="Claim_Number"></asp:Label>:&nbsp;
                </td>
                <td nowrap="nowrap">
                    <asp:TextBox ID="moClaimNumberText" runat="server" SkinID="MediumTextBox" ReadOnly="true"
                        Enabled="false"></asp:TextBox>
                </td>
                <td nowrap="nowrap" align="right">
                    <asp:Label ID="moCertificateLabel" runat="server" Text="Certificate"></asp:Label>:&nbsp;
                </td>
                <td>
                    <asp:TextBox ID="moCertificateText" runat="server" SkinID="MediumTextBox" ReadOnly="true"
                        Enabled="false"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td nowrap="nowrap" align="right">
                    <asp:Label ID="moDealerLabel" runat="server" Text="Dealer"></asp:Label>:&nbsp;
                </td>
                <td nowrap="nowrap">
                    <asp:TextBox ID="moDealerText" runat="server" SkinID="MediumTextBox" ReadOnly="true"
                        Enabled="false"></asp:TextBox>
                </td>
            </tr>
        </table>
    </div>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto">
        <Scripts>
            <asp:ScriptReference Path="~/Navigation/scripts/ComunaSuggest.js" />
        </Scripts>
    </asp:ScriptManager>
    <asp:Panel ID="EditPanel" runat="server">
        <div class="dataContainer" style="width: 100%;">
            <div id="tabs" class="style-tabs">
                <ul>
                    <li><a href="#tabsCoverageType">
                        <asp:Label ID="Label4" runat="server" CssClass="tabHeaderText">Coverage_Type</asp:Label></a></li>
                </ul>
               <table width="100%" class="dataGrid" border="0" cellspacing="0" cellpadding="0">
               <tbody>
                <tr>
                    <td class="bor">
                        <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>
                        <asp:DropDownList ID="cboPageSize" runat="server" SkinID="SmallDropDown" AutoPostBack="true">
                            <asp:ListItem Value="5">5</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="15">15</asp:ListItem>
                            <asp:ListItem Value="20">20</asp:ListItem>
                            <asp:ListItem Value="25">25</asp:ListItem>
                            <asp:ListItem Selected="True" Value="30">30</asp:ListItem>
                            <asp:ListItem Value="35">35</asp:ListItem>
                            <asp:ListItem Value="40">40</asp:ListItem>
                            <asp:ListItem Value="45">45</asp:ListItem>
                            <asp:ListItem Value="50">50</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td align="right" class="bor">
                        <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                    </td>
                </tr>
            </tbody>
        </table>
                <div id="tabsCoverageType">
                    <asp:GridView ID="Grid" runat="server" AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" SkinID="DetailPageGridView"
                        PageSize="15" Width="100%">
                        <Columns>
                            <asp:TemplateField Visible="False">
                                <ItemTemplate>
                                    <asp:Label ID="moCertItemCoverageId" runat="server" Text='<%# GetGuidStringFromByteArray(Container.DataItem("CERT_ITEM_COVERAGE_ID")) %>'> </asp:Label>
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Risk_type" Visible="true" SortExpression="risk_type_description" >
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Coverage_Type" Visible="true" SortExpression="coverage_type_description" >
                                <HeaderStyle HorizontalAlign="Center" Wrap="false" />
                                <ItemStyle HorizontalAlign="Center" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="CAUSE_OF_LOSS" Visible="true" >
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle BackColor="White" HorizontalAlign="Left" Width="25%" Wrap="False" />
                                <ItemTemplate>
                                    <asp:Label ID="lblCauseofLoss" runat="server"> </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:DropDownList ID="cboCauseOfLossId" runat="server"
                                        DataValueField="CODE" AutoPostBack="True" Enabled="false" Width="250px" OnSelectedIndexChanged="cboCauseOfLossId_SelectedIndexChanged">
                                    </asp:DropDownList>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="AUTHORIZED_AMOUNT" Visible="true">
                                <HeaderStyle HorizontalAlign="Center" Wrap="False" />
                                <ItemStyle BackColor="White" HorizontalAlign="Left" Width="25%" Wrap="False" />
                                <ItemTemplate>
                                    <asp:Label ID="lblAUTHORIZATION_AMOUNT" runat="server"> </asp:Label>
                                </ItemTemplate>
                                <EditItemTemplate>
                                    <asp:TextBox ID="txtAuthAmt" runat="server" Width="150px" Enabled="false">
                                    </asp:TextBox>
                                </EditItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Begin_Date" Visible="true" SortExpression="begin_date">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="left" />
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="End_Date" Visible="true" SortExpression="end_date">
                                <HeaderStyle HorizontalAlign="Center" />
                                <ItemStyle HorizontalAlign="left" />
                            </asp:TemplateField>
                            <asp:TemplateField>
                                <HeaderStyle HorizontalAlign="Center" Width="5%" />
                                <ItemStyle HorizontalAlign="Center" Width="30px" />
                                <ItemTemplate>
                                    <asp:ImageButton ID="btnSelect" runat="server" CommandName="SelectRecord" ImageUrl="~/App_Themes/Default/Images/edit.png"
                                        Style="cursor: hand" ImageAlign="AbsMiddle" CommandArgument="<%#Container.DisplayIndex %>" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                        <PagerSettings Mode="Numeric" PageButtonCount="15" Position="TopAndBottom" />
                    </asp:GridView>
                    <table width="100%" class="formGrid" border="0">
                        <tbody>
                            <tr style="background-color: #f2f2f2">
                                <td>
                                    <asp:Button ID="btnChangeCoverage" SkinID="PrimaryRightButton" runat="server" Text="Save" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                </div>
            </div>
        </div>
        <div class="btnZone">
            <asp:Button ID="btnBack" runat="server" SkinID="AlternateLeftButton" Text="BACK" />
        </div>
    </asp:Panel>
    <input id="HiddenSaveChangesPromptResponse" runat="server" designtimedragdrop="261"
        name="HiddenSaveChangesPromptResponse" type="hidden" />
</asp:Content>
