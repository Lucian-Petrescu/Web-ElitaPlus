<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ClaimAuthHistoryForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ClaimAuthHistoryForm" Theme="Default"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="Page">
        <div id="dvGridPager" runat="server">
            <table width="100%" class="dataGrid">
                <tr id="trPageSize" runat="server">
                    <td class="bor" align="left">
                        <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label><asp:Label ID="colonSepertor"
                            runat="server">:</asp:Label>
                        &nbsp;
                        <asp:DropDownList ID="cboPageSize" runat="server" Width="50px" AutoPostBack="true"
                            SkinID="SmallDropDown">
                            <asp:ListItem Value="5">5</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="15">15</asp:ListItem>
                            <asp:ListItem Value="20">20</asp:ListItem>
                            <asp:ListItem Value="25">25</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="bor" align="right">
                        <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <asp:GridView ID="GridClaimAuthorization" runat="server" Width="100%" AutoGenerateColumns="False"
            SkinID="DetailPageGridView">
            <SelectedRowStyle Wrap="True" />
            <EditRowStyle Wrap="True" />
            <AlternatingRowStyle Wrap="True" />
            <RowStyle Wrap="True" />
            <HeaderStyle />
            <Columns>
                <asp:TemplateField HeaderText="CLAIM_AUTHORIZATION_NUMBER" SortExpression="AuthorizationNumber">
                    <ItemTemplate>
                        <asp:LinkButton CommandName="Select" ID="EditButton_WRITE" runat="server" CausesValidation="False"
                            Text=""></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:BoundField DataField="ServiceCenterName" SortExpression="ServiceCenterName"
                    ReadOnly="true" HtmlEncode="false" HeaderText="SERVICE_CENTER_NAME" HeaderStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="ClaimAuthStatus" ReadOnly="true" HeaderText="Status" SortExpression="ClaimAuthStatus"
                    HtmlEncode="false" />
                <asp:BoundField DataField="AuthorizedAmount" SortExpression="AuthorizedAmount" ReadOnly="true"
                    HtmlEncode="false" HeaderText="AUTHORIZED_AMOUNT" HeaderStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="HistCreatedDate" SortExpression="AuthorizedAmount" ReadOnly="true"
                    HtmlEncode="false" HeaderText="HISTORY_CREATED_DATE" HeaderStyle-HorizontalAlign="Center" />
                <asp:BoundField DataField="HistoryCreatedByName" SortExpression="CreatedDate" ReadOnly="true"
                    HtmlEncode="false" HeaderText="HISTORY_CREATED_BY" HeaderStyle-HorizontalAlign="Center" />
            </Columns>
            <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
            <PagerStyle />
        </asp:GridView>
    </div>
    <div class="btnZone">
        <div>
            <asp:Button ID="btnBack" TabIndex="185" runat="server" Text="Back" SkinID="AlternateLeftButton" />
        </div>
    </div>
</asp:Content>
