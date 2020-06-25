<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CommissionPlanSearchForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.CommissionPlanSearchForm"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" Theme="Default" %>

<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl_new.ascx" %>

<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="JavaScript" type="text/javascript" src="../Navigation/Scripts/GlobalHeader.js"></script>
    <script language="JavaScript" type="text/javascript" src="../Navigation/Scripts/ReportCeMainScripts.js"></script>
</asp:Content>
<asp:Content ID="MessageContent" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="SummaryContent" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table width="100%" border="0" class="searchGrid" id="searchTable" runat="server" style="padding: 0px; margin: 0px">
        <tr>
            <td style="text-align: left;">
                <table width="100%">
                    <uc1:MultipleColumnDDLabelControl ID="moDealerMultipleDrop" runat="server" AutoPostBackDD="true"></uc1:MultipleColumnDDLabelControl>
                </table>
            </td>
        </tr>
    </table>
    
    <table width="100%" border="0" class="searchGrid" id="Table2" runat="server" style="padding: 0px; margin: 0px">
        <tr>
            <td style="text-align: right" colspan="2">
                <asp:Button ID="btnClear" SkinID="AlternateLeftButton" runat="server" Text="Clear"></asp:Button>&nbsp;
                <asp:Button ID="btnSearch" SkinID="SearchLeftButton" runat="server" Text="Search"></asp:Button>&nbsp;
            </td>
        </tr>
    </table>
</asp:Content>

<asp:Content ID="Body" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
        <h2 class="dataGridHeader">Search results for Commission Plan</h2>
        <div>
            <table width="100%" class="dataGrid">
                <tr id="trPageSize" runat="server">
                    <td valign="top" align="left">
                        <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>: &nbsp;
                        <asp:DropDownList ID="cboPageSize" runat="server" Width="50px" SkinID="SmallDropDown" AutoPostBack="true">
                            <asp:ListItem Value="5">5</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
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
                    <td align="right">
                        <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>

        <div style="width: 100%">
            <asp:GridView ID="GridCommPlan" runat="server" Width="100%" AllowSorting="True" AllowPaging="True"
                CellPadding="1" AutoGenerateColumns="False" SkinID="DetailPageGridView" Visible="False">
                <SelectedRowStyle Wrap="True"></SelectedRowStyle>
                <EditRowStyle Wrap="True"></EditRowStyle>
                <AlternatingRowStyle Wrap="True"></AlternatingRowStyle>
                <RowStyle Wrap="True"></RowStyle>
                <Columns>
                    <asp:TemplateField ShowHeader="false">
                        <ItemStyle HorizontalAlign="Center" Width="5%" Height="15px" />
                        <ItemTemplate>
                            <asp:ImageButton ID="btnEdit" runat="server" CausesValidation="False" CommandName="SelectRecord"
                                ImageUrl="~/Navigation/images/icons/edit2.gif" CommandArgument="<%#Container.DisplayIndex %>" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="False"></asp:TemplateField>
                    <asp:TemplateField HeaderText="COMPANY_CODE">
                        <HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="DEALER_NAME" HeaderText="DEALER_NAME">
                        <HeaderStyle HorizontalAlign="Center" Width="40%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="CODE" HeaderText="CODE">
                        <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="DESCRIPTION" HeaderText="DESCRIPTION">
                        <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="EFFECTIVE_DATE" HeaderText="EFFECTIVE_DATE">
                        <HeaderStyle HorizontalAlign="Center" Width="20%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="EXPIRATION_DATE" HeaderText="EXPIRATION_DATE">
                        <HeaderStyle HorizontalAlign="Center" Width="20%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                    </asp:TemplateField>
                </Columns>
                <PagerSettings PageButtonCount="15" Mode="Numeric" />
                <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
            </asp:GridView>
        </div>
    </div>
    <div class="btnZone">
        <asp:Button ID="btnNew" runat="server" Text="New" SkinID="PrimaryLeftButton"></asp:Button>&nbsp;
    </div>
    <input id="HiddenDeletePromptResponse" type="hidden" runat="server" designtimedragdrop="261">
</asp:Content>
