<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MaintainUserList.aspx.vb"
    Theme="Default" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.MaintainUserList"
    EnableSessionState="True" MasterPageFile="../Navigation/masters/ElitaBase.Master" %>
<%@ Register TagPrefix="Elita" TagName="MessageController" Src="~/Common/MessageController.ascx" %>
<asp:Content ContentPlaceHolderID="HeadPlaceHolder" runat="server">
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
<asp:Content ContentPlaceHolderID="MessagePlaceHolder" runat="server">
    <Elita:MessageController runat="server" ID="moMessageController" />
</asp:Content>
<asp:Content ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
  <asp:PlaceHolder ID="PlaceHolder1" runat="server">
    <table class="searchGrid" border="0" cellpadding="0" cellspacing="0">
        <tbody>
            <tr>
                <td>
                    <asp:Label ID="LabelSearchID" runat="server">Network ID</asp:Label>:<br />
                    <asp:TextBox ID="TextBoxSearchID" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="LabelSearchName" runat="server">Name</asp:Label>:<br />
                    <asp:TextBox ID="TextBoxSearchName" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                </td>
                <td>
                    <asp:Label ID="LabelSearchRole" runat="server">Role</asp:Label>:<br />
                    <asp:TextBox ID="TextBoxSearchRole" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="LabelSearchCompanyCode" runat="server">Company_Code</asp:Label>:<br />
                    <asp:TextBox ID="TextBoxSearchCompanyCode" runat="server" SkinID="MediumTextBox"></asp:TextBox>
                </td>
                <td colspan="2">
                    &nbsp;<br />
                    <asp:Button ID="btnSearch" runat="server" Text="Search" SkinID="SearchRightButton" />
                    <asp:Button ID="btnClearSearch" runat="server" Text="Clear" SkinID="AlternateRightButton" />
                </td>
            </tr>
        </tbody>
    </table>
   </asp:PlaceHolder>
</asp:Content>
<asp:Content ContentPlaceHolderID="BodyPlaceHolder" runat="server">
   <asp:PlaceHolder ID="PlaceHolder2" runat="server">
    <div class="dataContainer" runat="server" id="moSearchResults">
        <h2 class="dataGridHeader">
            <asp:Label runat="server" ID="moSearchResultsHeader">SEARCH_RESULTS_FOR_USER</asp:Label>
        </h2>
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
        <asp:GridView ID="Grid" runat="server" Width="100%" OnRowCreated="ItemCreated" OnRowCommand="ItemCommand"
            AllowPaging="True" AllowSorting="True" CellPadding="1" AutoGenerateColumns="False"
            SkinID="DetailPageGridView">
            <Columns>
                <asp:TemplateField SortExpression="Network_id" HeaderText="Network ID"></asp:TemplateField>
                <asp:TemplateField SortExpression="user_name" HeaderText="Name">
                    <ItemTemplate>
                        <asp:LinkButton CommandName="SelectAction" ID="btnEditCertificate" runat="server"
                            OnClientClick="document.body.style.cursor = 'wait'; if(isNotDblClick()) {return true;} else return false;"
                            CommandArgument="">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField SortExpression="user_roles" HeaderText="Roles"></asp:TemplateField>
                <asp:TemplateField SortExpression="user_companies" HeaderText="Company Code"></asp:TemplateField>
                <asp:TemplateField SortExpression="Active" HeaderText="Active"></asp:TemplateField>
            </Columns>
            <PagerSettings PageButtonCount="10" Mode="Numeric" Position="TopAndBottom" />
        </asp:GridView>
    </div>
    <div class="btnZone">
        <asp:Button ID="btnBack" runat="server" Text="BACK" SkinID="AlternateLeftButton"></asp:Button>
        <asp:Button runat="server" ID="btnAdd_WRITE" Text="New" SkinID="AlternateLeftButton" />
    </div>
   </asp:PlaceHolder>
</asp:Content>
