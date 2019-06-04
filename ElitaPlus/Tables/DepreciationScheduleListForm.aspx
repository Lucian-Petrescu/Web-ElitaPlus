<%@ Page Language="vb" AutoEventWireup="false" Codebehind="DepreciationScheduleListForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.DepreciationScheduleListForm" MasterPageFile="../Navigation/masters/ElitaBase.Master"
    Theme="Default" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl_new.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script type="text/javascript" language="JavaScript" src="../Navigation/Scripts/GlobalHeader.js"></script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table width="100%" border="0" class="searchGrid">
        <tr>
            <td align="left" style="height: 40px" width="30%" nowrap="nowrap">
                <table width="1%">
                    <uc1:MultipleColumnDDLabelControl ID="CompanyMultipleDrop" runat="server" />
                </table>
            </td>
            <td style="height: 40px" valign="middle" align="right" width="40%" nowrap="nowrap">
                <br /> <br />
                <asp:Button ID="ClearSearchButton" runat="server" SkinID="AlternateLeftButton" Text="Clear">
                </asp:Button>
                <asp:Button ID="SearchButton" runat="server"  SkinID="SearchButton" Text="Search">
                </asp:Button>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
      <div class="container">
        <div class="contentZoneHome">
            <div class="dataContainer">
                <h2 class="dataGridHeader"><asp:Label ID="Label1" runat="server">SEARCH_RESULTS_FOR_DEPRECIATION_SCHEDULE</asp:Label></h2>
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
                        <asp:GridView ID="DepreciationScheduleGridView" runat="server" Width="100%" AutoGenerateColumns="False"
                              AllowPaging="True" SkinID="DetailPageGridView" EnableViewState="true" AllowSorting="True" SortDirection="Ascending">
                            <SelectedRowStyle Wrap="True" />
                            <EditRowStyle Wrap="True" />
                            <AlternatingRowStyle Wrap="True" />
                            <RowStyle Wrap="True" />
                            <HeaderStyle />
                            <Columns>
                                <asp:TemplateField HeaderText="code" SortExpression="Code">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="SelectAction" runat="server" CommandName="SelectRecord" CommandArgument="<%#Container.DisplayIndex %>"/>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="description" HeaderText="Description">
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="active" HeaderText="DEPRECIATION_SCHEDULE_ACTIVE">
                                </asp:TemplateField>                                
                                <asp:TemplateField Visible="False">
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
        <asp:Button ID="NewButton_WRITE" runat="server" Text="New" SkinID="PrimaryLeftButton"
            CommandName="ADD"></asp:Button>
    </div>
</asp:Content>
