<%@ Page Title=""  Language="vb" AutoEventWireup="false" Theme="Default" CodeBehind="InvoiceRateForm.aspx.vb" 
    MasterPageFile="../Navigation/masters/ElitaBase.Master" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.InvoiceRateForm" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
 <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.6.1.min.js" > </script>
 <script language="JavaScript" type="text/javascript">
     window.latestClick = '';

     function isNotDblClick() {
         if (window.latestClick != "clicked") {
             window.latestClick = "clicked";
             return true;
         } else {
             return false;
         }
     }
  </script>
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
                    AutoPostBack="False" ></asp:TextBox>
                </td>
                <td align="left">
                    <asp:Label ID="lblProductDesc" runat="server">PRODUCT_DESCRIPTION</asp:Label>:
                    <br />
                    <asp:TextBox ID="txtProductDesc" runat="server" Enabled="false" SkinID="SmallTextBox"
                    AutoPostBack="False" style="width:250px;"></asp:TextBox>
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
        <div class="Page" runat="server" style="OVERFLOW: auto; height:500px; width:100%" >
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
                        <asp:Label ID="lblInvoiceRateID" Text='<%# GetGuidStringFromByteArray(Container.DataItem("afa_invoice_rate_id")) %>'
                            runat="server">
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
<%--                <asp:TemplateField Visible="True" HeaderText="CODE">
                    <ItemTemplate>
                        <asp:LinkButton CommandName="EditReportingRate"  Text= "RptRate" ID="btnEditReportingRate" runat="server"
                                OnClientClick="document.body.style.cursor = 'wait'; if(isNotDblClick()) {return true;} else return false;"></asp:LinkButton> 
                    </ItemTemplate>
                </asp:TemplateField>--%>
                <asp:TemplateField Visible="True" HeaderText="INSURANCE_CODE">
                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblInsCode" runat="server">
                        </asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEditInsCode" runat="server" Visible="True" SkinID="SmallTextBox" Width="100px"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="HANDSET_TIER">
                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblHandsetTier" runat="server">
                        </asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEditHandsetTier" runat="server" Visible="True" SkinID="SmallTextBox" Width="100px"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="REGULATORY_STATE">
                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblRegulatoryState" runat="server">
                        </asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList id="ddlEditRegulatoryState" runat="server" visible="True" Width="120px"></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="LOSS_TYPE">
                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <asp:LinkButton ID="btnLossType" CommandName="EditReportingRate"  Text= "RptRate" runat="server"
                                OnClientClick="document.body.style.cursor = 'wait'; if(isNotDblClick()) {return true;} else return false;"></asp:LinkButton> 
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList id="ddlEditLossType" runat="server" visible="True" Width="120px"></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="RETAIL_AMT">
                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblRetailAmt" runat="server">
                        </asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEditRetailAmt" runat="server" Visible="True" SkinID="SmallTextBox" Width="90px"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="PREMIUM_AMT">
                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblPremAmt" runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEditPremAmt" runat="server" Visible="True" SkinID="SmallTextBox" Width="90px"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="COMM_AMT">
                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblCommAmt" runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEditCommAmt" runat="server" Visible="True" SkinID="SmallTextBox" Width="90px"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="ADMIN_AMT">
                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblAdminAmt" runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEditAdminAmt" runat="server" Visible="True" SkinID="SmallTextBox" Width="90px"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="ANCILL_AMT">
                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblAncillAmt" runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEditAncillAmt" runat="server" Visible="True" SkinID="SmallTextBox" Width="90px"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="OTHER_AMT">
                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblOtherAmt" runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEditOtherAmt" runat="server" Visible="True" SkinID="SmallTextBox" Width="90px"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="EFFECTIVE_DATE" ItemStyle-VerticalAlign = "Middle">
                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblEffectiveDate" runat="server">
                        </asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtEffectiveDate" runat="server" Width="100px"></asp:TextBox>
                        <asp:ImageButton ID="imgEffectiveDate" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png" />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="EXPIRATION_DATE" ItemStyle-VerticalAlign = "Middle">
                    <HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                    <ItemStyle Wrap="False" HorizontalAlign="Right"></ItemStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblExpirationDate" runat="server">
                        </asp:Label>
                    </ItemTemplate> 
                    <EditItemTemplate>
                        <asp:TextBox ID="txtExpirationDate" runat="server" Width="100px"></asp:TextBox>
                        <asp:ImageButton ID="imgExpirationDate" runat="server" ImageUrl="~/App_Themes/Default/Images/calendar.png" />
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