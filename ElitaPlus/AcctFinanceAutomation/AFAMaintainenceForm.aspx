<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AFAMaintainenceForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.AFAMaintainenceForm" EnableSessionState="True"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" Theme="Default" %>

<asp:Content ID="Header" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.6.1.min.js"> </script>
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
                <asp:DropDownList ID="ddlDealer" runat="server" SkinID="SmallDropDown" style="width: 250px;">
                </asp:DropDownList>
            </td>
            <td align="left">
                <asp:Label runat="server" ID="lblAcctPeriod">SELECT_MONTH_AND_YEAR</asp:Label>
                <br />
                <asp:DropDownList ID="ddlAcctPeriodYear" runat="server" SkinID="SmallDropDown"></asp:DropDownList>
                <asp:DropDownList ID="ddlAcctPeriodMonth" runat="server" SkinID="SmallDropDown"></asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td colspan="2"></td>
            <td align="left">
                <asp:Button ID="btnClearSearch" runat="server" SkinID="AlternateLeftButton" Text="Clear"></asp:Button>
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
            <asp:Label ID="lblActiveSearchResults" runat="server" Text="SEARCH_RESULTS_FOR_PROCESS_STATUS" Visible="true"></asp:Label>
        </h2>
        <div style="width: 100%">
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
                    </tr>
                </tbody>
            </table>

            <asp:GridView ID="GridProcessStatus" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
                SkinID="DetailPageGridView" AllowSorting="false" CellPadding="1" CssClass="DATAGRID">
                <selectedrowstyle cssclass="SELECTED" />
                <editrowstyle cssclass="EDITROW" />
                <alternatingrowstyle wrap="False" cssclass="ALTROW" />
                <rowstyle cssclass="ROW" />
                <headerstyle cssclass="HEADER" />
                <columns>
                    <asp:TemplateField HeaderText="PROCESS_TYPE">
                        <ItemTemplate>
                            <asp:Label ID="lblProcessType" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.PROCESS_TYPE") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="STATUS">
                        <ItemTemplate>
                            <asp:Label ID="lblStatus" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.STATUS") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="START_DATE_TIME">
                        <ItemTemplate>
                           <%-- <asp:Label ID="lblStartDateTime" runat="server" Text='<%# ConvertDateAsString(DataBinder.Eval(Container, "DataItem.START_DATE_TIME")) %>' />--%>
                            <asp:Label ID="lblStartDateTime" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.START_DATE_TIME") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                     <asp:TemplateField HeaderText="COMPLETION_DATE_TIME">
                        <ItemTemplate>
                            <asp:Label ID="lblCompletionDateTime" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COMPLETION_DATE_TIME") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="COMMENTS">
                        <ItemTemplate>
                            <asp:Label ID="lblComments" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.COMMENTS") %>' />
                        </ItemTemplate>
                    </asp:TemplateField>
                </columns>
                <pagerstyle horizontalalign="Center" cssclass="PAGER"></pagerstyle>
            </asp:GridView>
        </div>
    </div>
    <div class="btnZone">
        <asp:Button ID="BtnReRunRecon" runat="server" Text="RERUN_RECON" SkinID="AlternateLeftButton"></asp:Button>
        <asp:Button ID="BtnReRunInvoice" runat="server" Text="RERUN_INVOICE" SkinID="AlternateLeftButton"></asp:Button>
    </div>
</asp:Content>
