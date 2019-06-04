<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SuspendedReasonsForm.aspx.vb" 
Theme="Default" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.SuspendedReasonsForm" 
MasterPageFile="../Navigation/masters/ElitaBase.Master" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
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
    <table class="searchGrid" border="0" cellpadding="0" cellspacing="0" >
            <tr>
                <td>
                    <asp:Label ID="DealerLabel" runat="server">Dealer:</asp:Label><br />
                    <asp:DropDownList ID="SearchDealerDD" runat="server" SkinID="MediumDropDown" AutoPostBack="False">
                    </asp:DropDownList>
                </td>
                <td>
                    <asp:Label ID="CodeLabel" runat="server">CODE</asp:Label>:<br />
                    <asp:TextBox ID="SearchCodeTxt" runat="server" SkinID="SmallTextBox" />
                </td>
                <td>
                    <asp:Label ID="SearchDescriptionLabel" runat="server">DESCRIPTION</asp:Label>:<br />
                    <asp:TextBox ID="SearchDescriptionTxt" runat="server" SkinID="MediumTextBox" />
                </td>
                <td>
                    <asp:Label ID="ActiveLabel" runat="server">CLAIM_ALLOWED</asp:Label>:<br />
                    <asp:DropDownList ID="SearchClaimAllowDD" runat="server"  AutoPostBack="False" SkinID="SmallDropDown" Width="100px">
                    </asp:DropDownList>
                </td>
                <td align="left">
                    <p style="height:8px"></p>
                    <table>
                        <tr>
                            <td><asp:Button ID="btnSearch" runat="server" Text="Search" SkinID="SearchRightButton" /></td>
                            <td><asp:Button ID="btnClearSearch" runat="server" Text="Clear" SkinID="AlternateRightButton" /></td>
                        </tr>
                    </table>
                </td>
            </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer" runat="server" id="moSearchResults">
        <h2 class="dataGridHeader">
            <asp:Label runat="server" ID="moSearchResultsHeader">SEARCH_RESULTS_FOR_SUSPENDED_REASONS</asp:Label>
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
 
        <asp:GridView ID="Grid" runat="server" Width="100%" OnRowCreated="RowCreated" OnRowCommand="RowCommand"
            AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" SkinID="DetailPageGridView">
            <PagerSettings PageButtonCount="10" Mode="Numeric" Position="TopAndBottom" />
            <Columns>
                <asp:TemplateField Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="moIDLabel" Text='<%#GetGuidStringFromByteArray(Container.DataItem("SUSPENDED_REASON_ID"))%>'
                            runat="server">
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="DEALER" ItemStyle-Width="250px">
                    <ItemTemplate>
                        <asp:Label ID="moDealerLabel" Text='<%#Container.DataItem("DEALER_NAME")%>' runat="server">
                        </asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList runat="server" ID="moDealerDDL" SkinID="MediumDropDown" />
                        <asp:Label ID="moDealerLabel2" Text='' runat="server" />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField Visible="True" HeaderText="CODE" ItemStyle-Width="10%">
                    <ItemTemplate>
                        <asp:Label ID="moCodeLabel" Text='<%#Container.DataItem("CODE")%>' runat="server" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="moCodeText" runat="server" Visible="True" SkinID="SmallTextBox"></asp:TextBox>
                        <asp:Label ID="moCodeLabel2" Text='' runat="server" />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="DESCRIPTION">
                    <ItemTemplate>
                        <asp:Label ID="moDescriptionLabel" Text='<%#Container.DataItem("DESCRIPTION")%>' runat="server">
                        </asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="moDescriptionText" runat="server" Visible="True" SkinID="LargeTextBox" Width="450px"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="CLAIM_ALLOWED" ItemStyle-Width="100px">
                    <ItemTemplate>
                        <asp:Label ID="moClaimAllowedLabel" Text='<%#Container.DataItem("CLAIM_ALLOWED_STR")%>' runat="server">
                        </asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList runat="server" ID="moClaimAllowedDDL" SkinID="SmallDropDown"  Width="100px" />
                    </EditItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField ShowHeader="false" ItemStyle-Width="45px" ItemStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:ImageButton ID="EditButton_WRITE" runat="server" CausesValidation="False" CommandName="SelectAction"
                            ImageUrl="~/App_Themes/Default/Images/edit.png" CommandArgument="<%#Container.DisplayIndex %>" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <table><tr><td>
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" SkinID="PrimaryRightButton"></asp:Button></td><td>
                        <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" SkinID="AlternateRightButton"></asp:LinkButton></td>
                        </tr></table>
                    </EditItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>

        <input id="HiddenDeletePromptResponse" type="hidden" runat="server" designtimedragdrop="261" />
    </div>
    <div class="btnZone">
        <asp:Button runat="server" ID="btnNew" Text="New" SkinID="AlternateLeftButton" />
    </div>
</asp:Content>