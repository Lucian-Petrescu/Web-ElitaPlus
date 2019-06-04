<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ClaimListForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ClaimListForm" MasterPageFile="../Navigation/masters/ElitaBase.Master"
    Theme="Default" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
<asp:Content ID="HeaderContent" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
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
<asp:Content ID="MessageContent" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="SummaryContent" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto">
        <Scripts>
            <asp:ScriptReference Path="~/Navigation/scripts/ComunaSuggest.js" />
        </Scripts>
    </asp:ScriptManager>
    <table width="100%" border="0" class="searchGrid" id="searchTable" runat="server">
        <tr>
            <td>
                <table cellspacing="0" cellpadding="0" width="100%" border="0">
                    <tr>
                        <td>
                            <asp:Label ID="LabelSearchClaimNumber" runat="server">CLAIM_NUMBER</asp:Label>:
                        </td>
                        <td>
                            <asp:Label ID="LabelSearchCustomerName" runat="server">CUSTOMER_NAME</asp:Label>:
                        </td>
                        <td align="left">
                            <asp:Label ID="LabelSearchServiceCenter" runat="server">SERVICE_CENTER_NAME</asp:Label>:
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:TextBox ID="TextBoxSearchClaimNumber" runat="server" SkinID="MediumTextBox"
                                AutoPostBack="False"></asp:TextBox>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="TextBoxSearchCustomerName" runat="server" SkinID="MediumTextBox"
                                AutoPostBack="False"></asp:TextBox>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="moServiceCenterText" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>

                        <td align="left">
                            <asp:Label ID="LabelSearchAuthorizationNumber" runat="server">SVC_REFERENCE_NUMBER</asp:Label>:
                        </td>
                        <td align="left">
                            <asp:Label ID="LabelSearchAuthorizedAmount" runat="server">AUTHORIZED_AMOUNT</asp:Label>:
                        </td>
                        <td>
                            <asp:Label ID="LabeLSearchHasPendingAuth" runat="server">HAS_PENDING_AUTHORIZATIONS</asp:Label>:
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:TextBox ID="TextBoxSearchSvcRefNumber" runat="server" SkinID="MediumTextBox"
                                AutoPostBack="False"></asp:TextBox>
                        </td>
                        <td align="left">
                            <asp:TextBox ID="TextBoxSearchAuthorizedAmount" runat="server" SkinID="MediumTextBox"
                                AutoPostBack="False"></asp:TextBox>
                        </td>
                        <td>
                            <asp:DropDownList ID="cboSearchHasPendingAuth" runat="server" SkinID="MediumDropDown">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LabelSearchServiceDealer" runat="server">Dealer</asp:Label>:
                        </td>
                        <td>
                            <asp:Label ID="LabelSearchCustomerCertificate" runat="server">Certificate</asp:Label>:
                        </td>
                        <td>
                            <asp:Label ID="LabeLSearchClaimStatus" runat="server">ClaimStatus</asp:Label>:
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:DropDownList ID="cboSearchDealer" runat="server" SkinID="MediumDropDown">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxSearchCertificate" runat="server" AutoPostBack="False" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td>
                            <asp:DropDownList ID="cboSearchClaimStatus" runat="server" SkinID="MediumDropDown">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblTrackingNumber" runat="server">Tracking_Number</asp:Label>:
                        </td>
                        <td>
                            <asp:Label ID="lblAuthorizationNumber" runat="server">AUTHORIZATION_NUMBER</asp:Label>:
                        </td>
                        <td colspan="2">
                            <asp:Label ID="lblAuthorizationStatus" runat="server">AUTHORIZATION_STATUS</asp:Label>:
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:TextBox ID="TextBoxSearchTrackingNumber" runat="server" AutoPostBack="False" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td>
                            <asp:TextBox ID="TextBoxSearchAuthorizationNumber" runat="server" AutoPostBack="False" SkinID="MediumTextBox"></asp:TextBox>
                        </td>
                        <td>
                            <asp:DropDownList ID="cboAuthorizationStatus" runat="server" SkinID="MediumDropDown">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="3">
                            <asp:Label ID="lblSortBy" runat="server">Sort By</asp:Label>:
                        </td>
                    </tr>
                    <tr>
                        <td align="left">
                            <asp:DropDownList ID="cboSortBy" runat="server" AutoPostBack="False" SkinID="MediumDropDown">
                            </asp:DropDownList>
                        </td>
                        <td align="right" colspan="2">
                            <label>
                                <asp:Button ID="btnClearSearch" runat="server" SkinID="AlternateLeftButton" Text="Clear"></asp:Button>
                                <asp:Button ID="btnSearch" runat="server" SkinID="SearchButton" Text="Search"></asp:Button>
                            </label>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Body" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <!-- new layout start -->
    <div class="dataContainer">
        <h2 class="dataGridHeader">Search results for claims</h2>
        <div>
            <table width="100%" class="dataGrid">
                <tr id="trPageSize" runat="server">
                    <td class="bor" align="left">
                        <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label><asp:Label ID="Label3"
                            runat="server">:</asp:Label>
                        &nbsp;
                        <asp:DropDownList ID="cboPageSize" runat="server" AutoPostBack="true" Width="50px"
                            SkinID="SmallDropDown">
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
                    <td class="bor" align="right">
                        <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 100%">
            <asp:GridView ID="Grid" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
                SkinID="DetailPageGridView" AllowSorting="true">
                <SelectedRowStyle Wrap="True" />
                <EditRowStyle Wrap="True" />
                <AlternatingRowStyle Wrap="True" />
                <RowStyle Wrap="True" />
                <Columns>
                    <asp:TemplateField HeaderText="Claim_Number" SortExpression="clnum">
                        <ItemTemplate>
                            <asp:LinkButton ID="btnEditClaim" runat="server" OnClientClick="document.body.style.cursor = 'wait'; if(isNotDblClick()) {return true;} else return false;"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField SortExpression="status_code" HeaderText="Status" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField SortExpression="custn" HeaderText="Customer_Name" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField SortExpression="svcna" HeaderText="Service_Center" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField SortExpression="dlrcd" HeaderText="DEALER" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField SortExpression="certn" HeaderText="CERTIFICATE" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField SortExpression="created_date" HeaderText="DATE_ADDED" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField SortExpression="product_code" HeaderText="PRODUCT_CODE" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField SortExpression="tracking_number" HeaderText="tracking_Number" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField SortExpression="auth" HeaderText="Authorized_Amount" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField SortExpression="authn" HeaderText="Authorization_Number" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField SortExpression="auth_status" HeaderText="Authorization_Status" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField Visible="False"></asp:BoundField>
                    <asp:BoundField Visible="False"></asp:BoundField>
                    <asp:BoundField Visible="False"></asp:BoundField>
                </Columns>
                <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                <PagerStyle />
            </asp:GridView>
        </div>
    </div>
    <!-- end new layout -->
</asp:Content>
