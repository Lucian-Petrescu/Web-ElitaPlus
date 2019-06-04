<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="QuestionSetListForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.GlobalConfig.QuestionSetListForm"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" Theme="Default" %>

<asp:Content ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script type="text/javascript" language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>
</asp:Content>
<asp:Content ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <!--Start Header-->
    <table width="60%" border="0" class="searchGrid">
        <tr>
            <td nowrap="nowrap" align="left">
                <asp:Label ID="CodeLabel" runat="server">Code</asp:Label>
            </td>
            <td nowrap="nowrap" align="left">
                <asp:Label ID="DescriptionLabel" runat="server">Description</asp:Label>
            </td>
            <td nowrap="nowrap" align="right">&nbsp;
            </td>
        </tr>
        <tr>
            <td nowrap="nowrap" align="left">
                <asp:TextBox ID="CodeTextBox" runat="server" AutoPostBack="False" SkinID="SmallTextBox"></asp:TextBox>
            </td>
            <td nowrap="nowrap" align="left">
                <asp:TextBox ID="DescriptionTextBox" runat="server" AutoPostBack="False" SkinID="MediumTextBox"></asp:TextBox>

            </td>
            <td nowrap="nowrap" align="right">
                <asp:Button ID="ClearSearchButton" Text="Clear" SkinID="AlternateLeftButton" runat="server"></asp:Button>&nbsp;
                <asp:Button ID="SearchButton" Text="Search" SkinID="SearchButton" runat="server"></asp:Button>&nbsp;&nbsp;
            </td>
        </tr>

    </table>
</asp:Content>
<asp:Content ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="container">
        <div class="contentZoneHome">
            <div class="dataContainer">
                <h2 class="dataGridHeader">Search Results for Question Sets</h2>
                <table class="dataGrid" border="0" width="100%">
                    <tr id="PageSizeRow" runat="server">
                        <td width="60%">
                            <asp:Label ID="PageSizeLabel" runat="server">Page_Size</asp:Label>:
                            <asp:DropDownList ID="PageSizeCombo" runat="server" AutoPostBack="true" SkinID="ExtraSmallDropDown">
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
                        <td width="40%" align="right">
                            <asp:Label ID="RecordCountLabel" class="bor" runat="server"></asp:Label>
                        </td>
                    </tr>
                </table>
                <div>
                    <div style="overflow: auto; width: 99.53%;" align="center">
                        <asp:GridView ID="QuestionSetGridView" runat="server" Width="100%" AutoGenerateColumns="False"
                            OnRowCommand="QuestionSetGridView_RowCommand" OnRowCreated="QuestionSetGridView_RowCreated"
                            AllowPaging="True" SkinID="DetailPageGridView" EnableViewState="true" AllowSorting="True" SortDirection="Ascending">
                            <SelectedRowStyle Wrap="True" />
                            <EditRowStyle Wrap="True" />
                            <AlternatingRowStyle Wrap="True" />
                            <RowStyle Wrap="True" />
                            <HeaderStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="Code" SortExpression="Code">
                                    <ItemTemplate>
                                        <asp:LinkButton runat="server" CommandName="SelectRecord" CommandArgument='<%# Eval("Code") %>' Text='<%# Eval("Code") %>' />
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="Description" HeaderText="Description">
                                    <ItemTemplate>
                                        <span><%# Eval("Description") %></span>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
                        </asp:GridView>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="btnZone">
        <asp:Button ID="AddButton_WRITE" runat="server" Text="New" SkinID="PrimaryLeftButton"
            CommandName="ADD"></asp:Button>
    </div>
</asp:Content>
