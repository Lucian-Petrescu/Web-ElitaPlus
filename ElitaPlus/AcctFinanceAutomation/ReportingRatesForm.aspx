<%@ Page Title="" Language="vb" AutoEventWireup="false" Theme="Default" CodeBehind="ReportingRatesForm.aspx.vb" 
     MasterPageFile="../Navigation/masters/ElitaBase.Master"  Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ReportingRatesForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table  width="100%" class="searchGrid" border="0" cellpadding="0" cellspacing="0">
        <tbody>
            <tr>
                <td align="left">
                    <asp:Label runat="server" ID="lblProdCode">PRODUCT_CODE</asp:Label>:
                    <br />
                    <asp:TextBox ID="txtProductCode" runat="server" Enabled="false" SkinID="SmallTextBox"
                    AutoPostBack="False"></asp:TextBox>
                </td>
                <td align="left">
                    <asp:Label runat="server" ID="lblInsCode">INSURANCE_CODE</asp:Label>:
                    <br />
                    <asp:TextBox ID="txtInsCode" runat="server" Enabled="false" SkinID="SmallTextBox"
                    AutoPostBack="False"></asp:TextBox>
                </td>
                <td align="left">
                    <asp:Label runat="server" ID="lblHandsetTier">HANDSET_TIER</asp:Label>:
                    <br />
                    <asp:TextBox ID="txtHandsetTier" runat="server" Enabled="false" SkinID="SmallTextBox"
                    AutoPostBack="False"></asp:TextBox>
                </td>
                <td align="left">
                    <asp:Label ID="lblLossType" runat="server">LOSS_TYPE</asp:Label>:
                    <br />
                    <asp:TextBox ID="txtLossType" runat="server" Enabled="false" SkinID="SmallTextBox"
                    AutoPostBack="False"></asp:TextBox>
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
  <div class="dataContainer" runat="server" id="moSearchResults">
        <h2 class="dataGridHeader">
            <asp:Label runat="server" ID="moSearchResultsHeader">SEARCH_RESULTS</asp:Label>
        </h2>
        <table width="100%" class="dataGrid" border="0" cellspacing="0" cellpadding="0">
            <tbody>
                <tr>
                    <td class="bor">
                        <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>
                        <asp:DropDownList ID="cboPageSize" runat="server" SkinID="SmallDropDown" AutoPostBack="true">
                            <asp:ListItem Value="5">5</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Selected="True" Value="15">15</asp:ListItem>
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
                    </td>
                </tr>
            </tbody>
        </table>
        <div id="Div1" class="Page" runat="server" style="OVERFLOW: auto; height:500px; width:100%" >
        <asp:GridView ID="Grid" runat="server" Width="100%" OnRowCreated="RowCreated" onRowCommand="RowCommand" 
                      AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" SkinID="DetailPageGridView">
            <SelectedRowStyle Wrap="True"></SelectedRowStyle>
            <EditRowStyle Wrap="True"></EditRowStyle>
            <AlternatingRowStyle Wrap="True"></AlternatingRowStyle>
            <RowStyle Wrap="True"></RowStyle>
            <HeaderStyle Wrap="True" HorizontalAlign="Center"></HeaderStyle>
            <PagerStyle HorizontalAlign ="left" />
            <Columns>
                <asp:TemplateField Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblReportingRateID" Text='<%# GetGuidStringFromByteArray(Container.DataItem("afa_reporting_rate_id")) %>'
                            runat="server">
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="RISK_FEE">
                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblRiskFee" runat="server">
                        </asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEditRiskFee" runat="server" Visible="True" SkinID="SmallTextBox" ></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="SPM_COE">
                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblSPMCOE" runat="server">
                        </asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtSPMCOE" runat="server" Visible="True" SkinID="SmallTextBox" ></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="FULLFILLMENT_NOTIFICATION">
                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblFullfillmentNotification" runat="server">
                        </asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtFullfillmentNotification" runat="server" Visible="True" SkinID="SmallTextBox" style="width:200px;" ></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="MARKETING_EXPENSES">
                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblMarketingExpenses" runat="server">
                        </asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtMarketingExpenses" runat="server" Visible="True" SkinID="SmallTextBox" ></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="PREMIUM_TAXES">
                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblPremiumTaxes" runat="server">
                        </asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtPremiumTaxes" runat="server" Visible="True" SkinID="SmallTextBox" ></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="LOSS_RESERVE_COST">
                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblLossReserveCost" runat="server">
                        </asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtLossReserveCost" runat="server" Visible="True" SkinID="SmallTextBox" ></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="OVERHEAD">
                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblOverhead" runat="server">
                        </asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtOverhead" runat="server" Visible="True" SkinID="SmallTextBox" ></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="GENERAL_EXPENSES">
                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblGeneralExpenses" runat="server">
                        </asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtGeneralExpenses" runat="server" Visible="True" SkinID="SmallTextBox" ></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="ASSESSMENTS">
                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblAssessments" runat="server">
                        </asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtAssessments" runat="server" Visible="True" SkinID="SmallTextBox" ></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="LAE">
                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblLAE" runat="server">
                        </asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtLAE" runat="server" Visible="True" SkinID="SmallTextBox" ></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign = "Middle">
                    <ItemTemplate>
                        <asp:ImageButton ID="EditButton_WRITE" runat="server" CausesValidation="False" CommandName="SelectRecord"
                            ImageUrl="~/App_Themes/Default/Images/edit.png" CommandArgument="<%#Container.DisplayIndex %>" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" SkinID="AlternateRightButton"></asp:LinkButton>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false" ItemStyle-HorizontalAlign="Right" ItemStyle-VerticalAlign = "Middle">
                    <ItemTemplate>
                        <asp:ImageButton ID="DeleteButton_WRITE" ImageUrl="~/App_Themes/Default/Images/icon_delete.png"
                            runat="server" CommandName="DeleteRecord"  CommandArgument="<%#Container.DisplayIndex %>" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" SkinID="PrimaryRightButton"></asp:Button>
                    </EditItemTemplate>
                </asp:TemplateField>
                </Columns>
            <PagerSettings PageButtonCount="10" Mode="Numeric" Position="TopAndBottom" />
        </asp:GridView>
        </div>
        <input id="HiddenDeletePromptResponse" type="hidden" runat="server" designtimedragdrop="261" />
  </div>
  <div class="btnZone">
        <asp:Button ID="btnBack" runat="server" Text="BACK" SkinID="AlternateLeftButton">
        </asp:Button>
        <asp:Button runat="server" ID="btnNew" Text="New" SkinID="AlternateLeftButton" />       
   </div>
</asp:Content>