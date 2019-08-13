<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ReconciliationForm.aspx.vb" 
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ReconciliationForm" EnableSessionState="True"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" Theme="Default" %>

<asp:Content ID="Header" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
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
<asp:Content ID="Message" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Summary" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto">
    </asp:ScriptManager>
    <table width="100%" border="0" class="searchGrid" id="searchTable" runat="server">
        <tr>
            <td align="left">
                <asp:Label runat="server" ID="lblDealer">DEALER</asp:Label>
                <br />
                <asp:DropDownList ID="ddlDealer" runat="server" SkinID="SmallDropDown"  AutoPostBack="true" style="width:250px;">
                </asp:DropDownList>
            </td>
            <td align="left">
                <asp:Label runat="server" ID="lblAcctPeriod">SELECT_MONTH_AND_YEAR</asp:Label>
                <br />
                <asp:DropDownList ID="ddlAcctPeriodYear" runat="server" SkinID="SmallDropDown"  AutoPostBack="true">
                </asp:DropDownList>
                <asp:DropDownList ID="ddlAcctPeriodMonth" runat="server" SkinID="SmallDropDown"  AutoPostBack="true">                    
                </asp:DropDownList>
            </td>                    
         </tr>
         <tr>
         </tr>
         <tr>
            <td colspan="2">
            </td>
            <td align="left">
                <asp:Button ID="btnClearSearch" runat="server" SkinID="AlternateLeftButton" Text="Clear">
                </asp:Button>
                <asp:Button ID="btnSearch" runat="server" SkinID="SearchButton" Text="Search"></asp:Button>
            </td>
         </tr>
    </table>
</asp:Content>
<asp:Content ID="Body" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
   <div id="divDataContainer" class="dataContainer" runat="server">
        <%--<h2 class="dataGridHeader">
            <asp:Label ID="lblActiveGrdi" runat="server" Text="ACTIVE_GRID" Visible="true" ></asp:Label>
        </h2>--%>
        <h2 class="dataGridHeader">
            <asp:Label ID="lblActiveSearchResults" runat="server" Text="SEARCH_RESULTS_FOR_RECONCILIATION" Visible="true" ></asp:Label>
        </h2>
        <div style="width: 100%">
            <table width="100%" class="dataGrid" border="0" cellspacing="0" cellpadding="0">
            <tbody>
                <tr>
                    <td class="bor">
                        <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>
                        <asp:DropDownList ID="cboPageSize" runat="server" SkinID="SmallDropDown" AutoPostBack="true" >
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
                        <asp:Label ID="lblshowDiscrepancyOnly" runat="server">SHOW_DISCREP_ONLY</asp:Label>
                        <asp:CheckBox ID = "chkDiscrepOnly" runat ="server" AutoPostBack="true"></asp:CheckBox>
                    </td>
                </tr>
            </tbody>
            </table>
             <asp:GridView ID="Grid" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
                SkinID="DetailPageGridView" AllowSorting="true" CellPadding="1" CssClass="DATAGRID">
                <SelectedRowStyle CssClass="SELECTED" />
                <EditRowStyle CssClass="EDITROW" />
                <AlternatingRowStyle Wrap="False" CssClass="ALTROW" />
                <RowStyle  CssClass="ROW" />
                <HeaderStyle CssClass="HEADER" />
                <Columns>
                     <asp:TemplateField HeaderText="BILLING_DATE" SortExpression="BILLING_DATE">
                        <ItemTemplate>
                            <asp:Label ID="lblBillingDate" runat="server" Text='<%# GetDateFormattedString(DataBinder.Eval(Container, "DataItem.BILLING_DATE")) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SOC_TYPE" SortExpression="SOC_TYPE">
                        <ItemTemplate>
                            <asp:Label ID="lblSocType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SOC_TYPE") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="ACCOUNT_STATUS" SortExpression="ACCOUNT_STATUS">
                        <ItemTemplate>
                            <asp:Label ID="lblAccountStatus" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ACCOUNT_STATUS") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="BILLABLE_COUNT" SortExpression="BILLABLE_COUNT">
                        <ItemTemplate>
                            <asp:Label ID="lblBillableCount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BILLABLE_COUNT") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CARRIER_COUNT" SortExpression="CARRIER_COUNT">
                        <ItemTemplate>
                            <asp:Label ID="lblCarrierCount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CARRIER_COUNT") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
             </asp:GridView>
             <asp:GridView ID="GridViewBLGS" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
                SkinID="DetailPageGridView" AllowSorting="true" CellPadding="1" CssClass="DATAGRID">
                <SelectedRowStyle CssClass="SELECTED" />
                <EditRowStyle CssClass="EDITROW" />
                <AlternatingRowStyle Wrap="False" CssClass="ALTROW" />
                <RowStyle  CssClass="ROW" />
                <HeaderStyle CssClass="HEADER" />
                <Columns>
                     <asp:TemplateField HeaderText="BILLING_DATE" SortExpression="BILLING_DATE">
                        <ItemTemplate>
                            <asp:Label ID="lblBillingDate" runat="server" Text='<%# GetDateFormattedString(DataBinder.Eval(Container, "DataItem.BILLING_DATE")) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SOC_TYPE" SortExpression="SOC_TYPE">
                        <ItemTemplate>
                            <asp:Label ID="lblSocType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SOC_TYPE") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="EPRISM_COUNT" SortExpression="EPRISM_COUNT">
                        <ItemTemplate>
                            <asp:Label ID="lblBillableCount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BILLABLE_COUNT") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="BI_COUNT" SortExpression="BI_COUNT">
                        <ItemTemplate>
                            <asp:Label ID="lblCarrierCount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CARRIER_COUNT") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
             </asp:GridView>
             <asp:GridView ID="GridViewMHP" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
                SkinID="DetailPageGridView" AllowSorting="false" CellPadding="1" CssClass="DATAGRID">
                <SelectedRowStyle CssClass="SELECTED" />
                <EditRowStyle CssClass="EDITROW" />
                <AlternatingRowStyle Wrap="False" CssClass="ALTROW" />
                <RowStyle  CssClass="ROW" />
                <HeaderStyle CssClass="HEADER" />
                <Columns>
                     <asp:TemplateField HeaderText="BILLING_DATE" SortExpression="BILLING_DATE">
                        <ItemTemplate>
                            <asp:Label ID="lblBillingDate" runat="server" Text='<%# GetDateFormattedString(DataBinder.Eval(Container, "DataItem.BILLING_DATE")) %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CATEGORY" SortExpression="SOC_TYPE">
                        <ItemTemplate>
                            <asp:Label ID="lblSocType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.SOC_TYPE") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="SUB_CATEGORY" SortExpression="ACCOUNT_STATUS">
                        <ItemTemplate>
                            <asp:Label ID="lblAccountStatus" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ACCOUNT_STATUS") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="BILLABLE_COUNT" SortExpression="BILLABLE_COUNT">
                        <ItemTemplate>
                            <asp:Label ID="lblBillableCount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BILLABLE_COUNT") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CARRIER_COUNT" SortExpression="CARRIER_COUNT">
                        <ItemTemplate>
                            <asp:Label ID="lblCarrierCount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CARRIER_COUNT") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="BILLABLE_AMOUNT" SortExpression="BILLABLE_AMOUNT">
                        <ItemTemplate>
                            <asp:Label ID="lblBillableAmount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.BILLABLE_AMOUNT") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="CARRIER_AMOUNT" SortExpression="CARRIER_AMOUNT">
                        <ItemTemplate>
                            <asp:Label ID="lblCarrierAmount" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.CARRIER_AMOUNT") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
                <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
             </asp:GridView>
         </div>
   </div>
   <div class="btnZone">
        <asp:Button ID="BtnOverRideRecon" runat="server" Text="OverRide_Recon" SkinID="AlternateLeftButton">
        </asp:Button>
        <asp:Button ID="BtnReRunRecon" runat="server" Text="RERUN_RECON" SkinID="AlternateLeftButton">
        </asp:Button>
   </div>
</asp:Content>
