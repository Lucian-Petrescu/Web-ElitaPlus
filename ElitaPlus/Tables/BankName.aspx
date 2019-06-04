<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="BankName.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.BankNameForm"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" Theme="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js">        function TABLE1_onclick() {

        }

    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <!--Start Header-->
    <table id="tblOuter2" border="0" class="searchGrid">
        <tr>
            <td valign="top" align="center" height="100%">
                <asp:Panel ID="WorkingPanel" runat="server">
                    <table id="tblSearch" border="0">
                        <tr>
                            <td valign="top">
                                <table cellspacing="2" cellpadding="2" width="100%" border="0">
                                    <tr>
                                        <td nowrap align="left">
                                            <asp:Label ID="lblDescription" runat="server">Bank Name</asp:Label>:
                                        </td>
                                        <td nowrap align="left">
                                            <asp:Label ID="lblCode" runat="server">Code</asp:Label>:
                                        </td>
                                        <td nowrap align="right">
                                            &nbsp;
                                        </td>
                                    </tr>
                                    <tr>
                                        <td nowrap align="left">
                                            <asp:DropDownList ID="ddlBankNameDropDown" runat="server" SkinID="MediumDropDown"
                                                AutoPostBack="false">
                                            </asp:DropDownList>
                                        </td>
                                        <td nowrap align="left">
                                            <asp:TextBox ID="txtSearchBankCodeTextBox" runat="server" AutoPostBack="False" SkinID="SmallTextBox"></asp:TextBox>
                                        </td>
                                        <td nowrap align="right">
                                            <asp:Button ID="btnClearSearch" Text="Clear" SkinID="AlternateLeftButton" runat="server">
                                            </asp:Button>&nbsp;
                                            <asp:Button ID="btnSearch" Text="Search" SkinID="SearchButton" runat="server"></asp:Button>&nbsp;&nbsp;
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                    </table>
                </asp:Panel>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="container">
        <div class="contentZoneHome">
            <div class="dataContainer">
                <h2 class="dataGridHeader">
                    Search results for Bank Name</h2>
                <table class="dataGrid" border="0" width="100%">
                    <tr id="trPageSize" runat="server">
                        <td width="60%">
                            <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>:
                            <asp:DropDownList ID="cboPageSize" runat="server" AutoPostBack="true" SkinID="ExtraSmallDropDown">
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
                        <td width="40%" align="right">
                            <asp:Label ID="lblRecordCount" class="bor" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <div>
                    <div id="divBankName" style="overflow: auto; width: 99.53%;" align="center" runat="server">
                        <asp:GridView ID="moBankNamesGridView" runat="server" Width="100%" AutoGenerateColumns="False"
                            OnRowCommand="moBankNamesGridView_RowCommand" OnRowCreated="moBankNamesGridView_RowCreated"
                            AllowPaging="True" SkinID="DetailPageGridView" EnableViewState="true" AllowSorting="True">
                            <SelectedRowStyle Wrap="True" />
                            <EditRowStyle Wrap="True" />
                            <AlternatingRowStyle Wrap="True" />
                            <RowStyle Wrap="True" />
                            <HeaderStyle />
                            <Columns>
                                <asp:BoundField Visible="False" />
                                <asp:TemplateField Visible="True" HeaderText="BANK_NAME" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBankName" runat="server"> </asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtBankName" runat="server" Visible="True" SkinID="MediumTextBox"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="True" HeaderText="BANK_CODE" ItemStyle-HorizontalAlign="Left">
                                    <ItemTemplate>
                                        <asp:Label ID="lblBankCode" runat="server"></asp:Label>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:TextBox ID="txtBankCode" runat="server" Visible="True" SkinID="MediumTextBox"></asp:TextBox>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left"
                                    Visible="True">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="EditButton_WRITE" Style="cursor: hand" runat="server" ImageUrl="../App_Themes/Default/Images/edit.png"
                                            Visible="true" CommandName="EditRecord" CommandArgument="<%#Container.DisplayIndex %>">
                                        </asp:ImageButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Button Style="cursor: hand;" ID="SaveButton_WRITE" runat="server" CommandName="SaveRecord"
                                            Text="Save" SkinID="PrimaryRightButton" CommandArgument="<%#Container.DisplayIndex %>">
                                        </asp:Button></EditItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField ShowHeader="false" ItemStyle-Wrap="false" ItemStyle-HorizontalAlign="Left"
                                    Visible="True">
                                    <ItemTemplate>
                                        <asp:ImageButton ID="DeleteButton_WRITE" Style="cursor: hand" runat="server" ImageUrl="../App_Themes/Default/Images/icon_delete.png"
                                            Visible="true" CommandName="DeleteRecord" CommandArgument="<%#Container.DisplayIndex %>">
                                        </asp:ImageButton>
                                    </ItemTemplate>
                                    <EditItemTemplate>
                                        <asp:Button Style="cursor: hand;" ID="CancelButton_WRITE" runat="server" Text="Cancel"
                                            SkinID="AlternateRightButton" CommandName="CancelRecord" CommandArgument="<%#Container.DisplayIndex %>">
                                        </asp:Button>
                                    </EditItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>
    <table>
        <tr>
            <td align="left" class="btnZone" colspan="2">
                <asp:Button ID="btnAdd_WRITE" runat="server" Text="New" SkinID="PrimaryLeftButton"
                    CommandName="WRITE"></asp:Button>
            </td>
        </tr>
    </table>
</asp:Content>
