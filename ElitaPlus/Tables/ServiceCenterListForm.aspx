<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ServiceCenterListForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ServiceCenterListForm" MasterPageFile="../Navigation/masters/ElitaBase.Master"
    Theme="Default" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table style="width: 100%" cellspacing="0" cellpadding="0" border="0" class="searchGrid">
        <tr runat="server" id="TRlblCountry">
            <td align="left">
                <asp:Label ID="moCountryLabel" runat="server">Country:</asp:Label>
            </td>
            <td align="left">
            </td>
            <td align="left">
            </td>
        </tr>
        <tr runat="server" id="TRddlCountry">
            <td align="left">
                <asp:DropDownList ID="moCountryDrop" runat="server" Width="160px" SkinID="SmallDropDown">
                </asp:DropDownList>
            </td>
            <td align="left">
            </td>
            <td align="left">
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="LabelSearchCode" runat="server">Code:</asp:Label>
            </td>
            <td align="left">
                <asp:Label ID="LabelSearchDescription" runat="server">Name:</asp:Label>
            </td>
            <td align="left">
                <asp:Label ID="LabelAddress" runat="server">Address:</asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:TextBox ID="TextBoxSearchCode" runat="server" AutoPostBack="False" SkinID="MediumTextBox"></asp:TextBox>
            </td>
            <td align="left">
                <asp:TextBox ID="TextBoxSearchDescription" runat="server" AutoPostBack="False" SkinID="MediumTextBox"></asp:TextBox>
            </td>
            <td align="left">
                <asp:TextBox ID="TextBoxSearchAddress" runat="server" AutoPostBack="False" SkinID="MediumTextBox"></asp:TextBox>&nbsp;
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:Label ID="LabelSearchCity" runat="server">City:</asp:Label>
            </td>
            <td align="left">
                <asp:Label ID="LabelSearchZip" runat="server">Zip:</asp:Label>
            </td>
        </tr>
        <tr>
            <td align="left">
                <asp:TextBox ID="TextBoxSearchCity" runat="server" AutoPostBack="False" SkinID="MediumTextBox"></asp:TextBox>
            </td>
            <td align="left">
                <asp:TextBox ID="TextBoxSearchZip" runat="server" AutoPostBack="False" SkinID="MediumTextBox"></asp:TextBox>
            </td>
            <td align="left">
                <asp:Button ID="btnClearSearch" runat="server" Text="Clear" SkinID="AlternateLeftButton" />
                <asp:Button ID="btnSearch" runat="server" Text="Search" SkinID="SearchButton" />
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="container">
        <div class="contentZoneHome">
            <div class="dataContainer">
                <h2 class="dataGridHeader">
                    Search results for service center</h2>
                <table width="100%;" class="dataGrid">
                    <tr id="trPageSize" runat="server">
                        <td>
                            <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>: &nbsp;
                            <asp:DropDownList ID="cboPageSize" runat="server" Width="50px" AutoPostBack="true">
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
                        <td align="right">
                            <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <div>
                    <asp:DataGrid ID="Grid" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
                        SkinID="DetailPageDataGrid" AllowSorting="True" OnItemCreated="ItemCreated" OnItemCommand="ItemCommand">
                        <SelectedItemStyle Wrap="true" />
                        <EditItemStyle Wrap="true" />
                        <AlternatingItemStyle Wrap="true" />
                        <ItemStyle Wrap="true" />
                        <HeaderStyle />
                        <Columns>
                            <asp:TemplateColumn Visible="false">
                                <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                <ItemTemplate>
                                    <asp:ImageButton Style="cursor: hand;" ID="btnEdit" ImageUrl="../Navigation/images/icons/edit2.gif"
                                    OnClientClick ="document.body.style.cursor = 'wait'; if(isNotDblClick()) {return true;} else return false;"  runat="server" CommandName="SelectAction"></asp:ImageButton>
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:TemplateColumn SortExpression="2" HeaderText="Code">
                                <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                <ItemTemplate>
                                    <asp:LinkButton ID="btnEditCode" runat="server" CommandName="SelectAction"
                                    OnClientClick="document.body.style.cursor = 'wait'; if(isNotDblClick()) {return true;} else return false;" />
                                </ItemTemplate>
                            </asp:TemplateColumn>
                            <asp:BoundColumn SortExpression="2" HeaderText="Service Group">
                                <HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn SortExpression="1" HeaderText="Name">
                                <HeaderStyle HorizontalAlign="Center" Width="20%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn SortExpression="3" HeaderText="Address">
                                <HeaderStyle HorizontalAlign="Center" Width="25%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn SortExpression="4" HeaderText="City">
                                <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn SortExpression="5" HeaderText="Zip">
                                <HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn SortExpression="2" HeaderText="Country">
                                <HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
                                <ItemStyle HorizontalAlign="Left"></ItemStyle>
                            </asp:BoundColumn>
                            <asp:BoundColumn Visible="False" HeaderText="Service_Center_Id"></asp:BoundColumn>
                        </Columns>
                        <PagerStyle HorizontalAlign="Center" PageButtonCount="15" Mode="NumericPages" Position="TopAndBottom" />
                    </asp:DataGrid>
                </div>
            </div>
            <div class="btnZone">
                <asp:Button ID="btnAdd_WRITE" runat="server" Text="ADD_NEW" SkinID="AlternateLeftButton">
                </asp:Button>
            </div>
        </div>
    </div>
</asp:Content>
