<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="OcMessageSearchForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.OcMessageSearchForm" MasterPageFile="../Navigation/masters/ElitaBase.Master"
    Theme="Default" %>

<%@ Register Src="../Common/MultipleColumnDDLabelControl_new.ascx" TagName="MultipleColumnDDLabelControl"
    TagPrefix="uc1" %>
<%@ Register TagPrefix="Elita" TagName="MessageController" Src="~/Common/MessageController.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table width="100%" border="0" class="searchGrid">
        <tr>
            <td align="left" style="height: 40px" width="30%" nowrap="nowrap">
                <table width="1%">
                    <uc1:MultipleColumnDDLabelControl ID="DealerMultipleDrop" runat="server" />
                </table>
            </td>
        </tr>
        <tr>
            <td valign="middle" width="30%" style="height: 40px">
                <table>
                    <tr>
                        <td nowrap align="left" width="1%">
                            <asp:Label ID="lblMsgSearchBy" runat="server">OC_MESSAGE_SEARCH_BY</asp:Label>
                        </td>
                        <td nowrap align="left" width="1%">
                            <asp:DropDownList ID="OcMessageSearchByDropDown" runat="server" SkinID="SmallDropDown" AutoPostBack="false">
                            </asp:DropDownList>
                        </td>
                        <td nowrap align="left" width="1%">
                            <asp:Label ID="lblSearchConditionOn" runat="server"></asp:Label>
                        </td>
                        <td nowrap align="left" width="1%">
                            <asp:TextBox ID="txtSearchValue" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                        </td>
                        <td style="height: 40px" valign="middle" align="right" width="40%" nowrap="nowrap">
                            <br />
                            <asp:Button ID="moBtnClear" runat="server" SkinID="AlternateLeftButton" Text="Clear">
                            </asp:Button>
                            <asp:Button ID="moBtnSearch" runat="server" Text="Search" SkinID="SearchLeftButton">
                            </asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
    <Elita:MessageController runat="server" ID="moMessageController" Visible="false" />
</asp:Content>

<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <!-- new layout start -->
    <div class="dataContainer">
        <h2 class="dataGridHeader">
            <asp:Label runat="server" ID="moSearchResultsHeader">SEARCH_RESULTS_FOR_OUTBOUND_COMM_MSG</asp:Label>
        </h2>
        <div>
            <table width="100%" class="dataGrid">
                <tr id="trPageSize" runat="server">
                    <td align="left">
                        <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>: &nbsp;
                        <asp:DropDownList ID="cboPageSize" runat="server" Width="50px" AutoPostBack="true">
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
                    <td class="bor" align="right">
                        <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 100%">
            <asp:GridView ID="Grid" runat="server" Width="100%" OnRowCreated="RowCreated" OnRowCommand="RowCommand"
                AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" SkinID="DetailPageGridView" PageSize="15">
                <SelectedRowStyle BackColor="lightsteelblue"></SelectedRowStyle>
                <EditRowStyle Wrap="True"></EditRowStyle>
                <AlternatingRowStyle Wrap="True"></AlternatingRowStyle>
                <RowStyle Wrap="True"></RowStyle>
                <Columns>
                    <asp:TemplateField>
                        <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Center"></ItemStyle>
                        <ItemTemplate>
                            <asp:ImageButton Style="cursor: hand;" ID="btnSelect" CommandName="SelectRecord"
                                ImageUrl="../Navigation/images/icons/yes_icon.gif" runat="server" CommandArgument="<%#Container.DisplayIndex %>"
                                CausesValidation="false"></asp:ImageButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="TEMPLATE_DESCRIPTION" HeaderText="MESSAGE_DESCRIPTION">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                        <ItemTemplate>
                            <asp:LinkButton ID="SelectAction" Style="cursor: hand" runat="server" Visible="true"
                                CommandName="SelectAction" CommandArgument="<%#Container.DisplayIndex%>">
                            </asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="SENDER_REASON" HeaderText="SENDER">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="RECIPIENT_ADDRESS" HeaderText="RECIPIENT_ADDRESS">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="LAST_ATTEMPTED_ON" HeaderText="LAST_ATTEMPTED_ON">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="LAST_ATTEMPTED_STATUS" HeaderText="LAST_ATTEMPTED_STATUS">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="CERT_NUMBER" HeaderText="CERT_NUMBER">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="CLAIM_NUMBER" HeaderText="CLAIM_NUMBER">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="CASE_NUMBER" HeaderText="CASE_NUMBER">
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="False" HeaderText="OC_TEMPLATE_ID"></asp:TemplateField>
                    <asp:TemplateField Visible="False" HeaderText="OC_MESSAGE_ID"></asp:TemplateField>
                    <asp:TemplateField Visible="False" HeaderText="CERTIFICATE_ID"></asp:TemplateField>
                    <asp:TemplateField Visible="False" HeaderText="CLAIM_ID"></asp:TemplateField>
                    <asp:TemplateField Visible="False" HeaderText="CASE_ID"></asp:TemplateField>
                </Columns>
                <PagerSettings PageButtonCount="15" Mode="Numeric" Position="TopAndBottom"></PagerSettings>
                <PagerStyle />
            </asp:GridView>
        </div>
    </div>
    <div class="btnZone">
        <asp:Button ID="btnBack" runat="server" CausesValidation="False" Text="BACK" SkinID="AlternateLeftButton" />
        <asp:Button ID="btnSendAdhocMsg" runat="server" CausesValidation="False" Text="SEND_ADHOC_MESSAGE" SkinID="AlternateLeftButton" />
    </div>
</asp:Content>