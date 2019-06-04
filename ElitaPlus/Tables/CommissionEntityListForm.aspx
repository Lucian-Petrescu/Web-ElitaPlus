<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="CommissionEntityListForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.CommissionEntityListForm" MasterPageFile="../Navigation/masters/ElitaBase.Master" Theme="Default" %>

<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">


    <table style="width: 100%" cellspacing="0" cellpadding="0" border="0" class="searchGrid">

        <tr>
            <td align="left">
                <asp:Label ID="lblDescription" runat="server">Name</asp:Label>:</td>
            <td align="left">
                <asp:Label ID="lblPhone" runat="server">Phone</asp:Label>:</td>
        </tr>
        <tr>
            <td align="left">
                <asp:TextBox ID="SearchDescriptionTextBox" runat="server" AutoPostBack="False" SkinID="MediumTextBox"></asp:TextBox></td>
            <td align="left">
                <asp:TextBox ID="SearchPhoneTextbox" runat="server" SkinID="MediumTextBox"></asp:TextBox></td>
        </tr>


        <tr>
            <td>&nbsp;</td>
            <td align="left">
                <asp:Button ID="btnClearSearch" runat="server" Text="Clear" SkinID="AlternateLeftButton" />
                <asp:Button ID="btnSearch" runat="server" Text="Search" SkinID="SearchButton" />
            </td>
        </tr>
    </table>


</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
        <h2 class="dataGridHeader">Search results for Price List
        </h2>
        <div>
            <table width="100%" class="dataGrid">
                <tr id="trPageSize" runat="server">
                    <td class="bor" align="left">
                        <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>: &nbsp;
                        <asp:DropDownList ID="cboPageSize" runat="server" Width="50px" AutoPostBack="true"
                            SkinID="SmallDropDown">
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
            <asp:GridView ID="Grid" runat="server" Width="100%" AutoGenerateColumns="False" AllowPaging="True"
                AllowSorting="true" SkinID="DetailPageGridView">
                <SelectedRowStyle Wrap="True" />
                <EditRowStyle Wrap="True" />
                <AlternatingRowStyle Wrap="True" />
                <RowStyle Wrap="True" />
                <HeaderStyle />
                <Columns>
                    <asp:TemplateField ShowHeader="false">
                        <ItemStyle HorizontalAlign="Center" Width="3%" Height="15px" />
                        <ItemTemplate>
                            <asp:ImageButton ID="btnView" runat="server" CausesValidation="False"
                                CommandName="SelectAction" ImageUrl="~/Navigation/images/icons/edit2.gif"
                                CommandArgument="<%#Container.DisplayIndex %>" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="ENTITY_NAME" HeaderText="Name">
                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="65%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField SortExpression="PHONE" HeaderText="Phone">
                        <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="65%"></HeaderStyle>
                        <ItemStyle HorizontalAlign="Left"></ItemStyle>
                    </asp:TemplateField>
                    <asp:TemplateField Visible="False" HeaderText="id"></asp:TemplateField>
                </Columns>
                <PagerSettings PageButtonCount="15" Mode="Numeric" />
                <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
            </asp:GridView>
        </div>
        <div class="btnZone">
            <div class="">

                <asp:Button ID="btnAdd_Write" runat="server" CausesValidation="False" SkinID="AlternateLeftButton"
                    Text="NEW" />
            </div>
        </div>
    </div>
</asp:Content>
