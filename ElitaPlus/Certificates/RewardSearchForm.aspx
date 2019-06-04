<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RewardSearchForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Certificates.RewardSearchForm"
    MasterPageFile="../Navigation/masters/ElitaBase.Master"
    Theme="Default" %>


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
                <table width="100%" border="0">

                    <tr>
                        <td>
                            <asp:Label ID="LabelCompany" runat="server">Company</asp:Label><br />
                            <asp:DropDownList ID="ddlCompanyName" runat="server" SkinID="MediumDropDown" AutoPostBack="False">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="LabelDealer" runat="server">Dealer</asp:Label><br />
                            <asp:DropDownList ID="ddlDealerName" runat="server" SkinID="MediumDropDown" AutoPostBack="False">
                            </asp:DropDownList>
                        </td>
                        <%--   <td>
                            <asp:Label ID="LabelCaseNumber" runat="server">CASE_NUMBER</asp:Label><br />
                            <asp:TextBox ID="TextBoxCaseNumber" runat="server" SkinID="MediumTextBox" 
                                AutoPostBack="False"></asp:TextBox>
                        </td>--%>
                        <td>
                            <asp:Label ID="LabelRewardStatus" runat="server">REWARD_STATUS</asp:Label><br />
                            <asp:DropDownList ID="ddlRewardStatus" runat="server" SkinID="MediumDropDown" AutoPostBack="False">
                            </asp:DropDownList>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="LabelCertificateNumber" runat="server">CERTIFICATE_NUMBER</asp:Label><br />
                            <asp:TextBox ID="TextBoxCertificateNumber" runat="server" SkinID="MediumTextBox"
                                AutoPostBack="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td align="left">&nbsp;
                        </td>
                    </tr>

                    <tr>
                        <td colspan="3" align="right">
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
        <h2 class="dataGridHeader">
            <asp:Label ID="lblSearchResults" runat="server" Text="SEARCH_RESULTS_FOR_REWARD" Visible="true"></asp:Label>
        </h2>
        <div>
            <table width="100%" class="dataGrid">
                <tr id="trPageSize" runat="server">
                    <td class="bor" align="left">
                        <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label><asp:Label ID="colonSepertor"
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
                    <asp:TemplateField HeaderText="cert_number" SortExpression="cert_number">
                        <ItemTemplate>
                            <asp:LinkButton CommandName="SelectAction" ID="btnEditCase" runat="server"
                                OnClientClick="document.body.style.cursor = 'wait'; if(isNotDblClick()) {return true;} else return false;"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField HeaderText="reward_status" DataField="reward_status" SortExpression="reward_status" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField HeaderText="reward_amount" DataField="reward_amount" SortExpression="reward_amount" HtmlEncode="false"></asp:BoundField>
                    <asp:BoundField HeaderText="reward_type" DataField="reward_type" SortExpression="reward_type" HtmlEncode="false"></asp:BoundField>
                </Columns>
                <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                <PagerStyle />
            </asp:GridView>
        </div>
    </div>
    <!-- end new layout -->
</asp:Content>
