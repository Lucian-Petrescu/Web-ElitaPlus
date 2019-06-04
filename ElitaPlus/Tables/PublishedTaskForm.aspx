<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PublishedTaskForm.aspx.vb" Theme="Default"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.PublishedTaskForm" %>

<asp:Content ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table width="100%" border="0" class="searchGrid" id="searchTable" runat="server">
        <tr>
            <td colspan="3">
                <table width="100%" border="0">
                    <tr>
                        <td>
                            <asp:Label ID="lblCompany" runat="server">Company:</asp:Label><br />
                            <asp:DropDownList ID="lstCompany" runat="server" SkinID="MediumDropDown" AutoPostBack="False">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="lblEventType" runat="server">Event_Type:</asp:Label><br />
                            <asp:DropDownList ID="lstEventType" runat="server" SkinID="MediumDropDown" AutoPostBack="False">
                            </asp:DropDownList>

                        </td>
                        <td>
                            <asp:Label ID="lblProduct" runat="server">Product:</asp:Label><br />
                            <asp:TextBox ID="txtProduct" runat="server" SkinID="MediumTextBox" AutoPostBack="False"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblDealer" runat="server">Dealer:</asp:Label><br />
                            <asp:DropDownList ID="lstDealer" runat="server" SkinID="MediumDropDown" AutoPostBack="False">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="lblStatus" runat="server">TASK_STATUS:</asp:Label><br />
                            <asp:DropDownList ID="lstStatus" runat="server" SkinID="SmallDropDown" AutoPostBack="False">
                            </asp:DropDownList>
                        </td>
                        <td>
                            <asp:Label ID="lblDateRange1" runat="server">Event_Date</asp:Label>&nbsp<asp:Label ID="lblDateRange2" runat="server">Range:</asp:Label><br />
                            <asp:TextBox ID="txtStartDate" runat="server" SkinID="exSmallTextBox" width="100px" AutoPostBack="False"></asp:TextBox>
                            <asp:ImageButton ID="BtnStartDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg" Width="20px" />
                            &nbsp<asp:Label ID="lblEventDateTo" runat="server">To</asp:Label>&nbsp
                            <asp:TextBox ID="txtEndDate" runat="server" SkinID="exSmallTextBox" width="100px"  AutoPostBack="False"></asp:TextBox>
                            <asp:ImageButton ID="BtnEndDate" runat="server" Height="17px" ImageUrl="../Common/Images/calendarIcon2.jpg" Width="20px" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                        </td>
                        <td colspan="2" align="right" valign="bottom"><br/>
                                <asp:Button ID="btnClearSearch" runat="server" SkinID="AlternateLeftButton" Text="Clear">
                                </asp:Button>
                                <asp:Button ID="btnSearch" runat="server" SkinID="SearchButton" Text="Search"></asp:Button>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ContentPlaceHolderID="BodyPlaceHolder" runat="server">

    <div class="dataContainer" runat="server" id="moSearchResults">
        <h2 class="dataGridHeader">
            <asp:Label runat="server" ID="moSearchResultsHeader">SEARCH_RESULTS_FOR_PUBLISHED_TASK</asp:Label>
        </h2>
        <table width="100%" class="dataGrid" border="0" cellspacing="0" cellpadding="0">
            <tbody>
                <tr>
                    <td class="bor">
                        <asp:Label ID="Label15" runat="server">Page_Size</asp:Label>
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
        <asp:GridView ID="grdPublishTask" runat="server" Width="100%" AllowPaging="True"
            AllowSorting="False" CellPadding="1" SkinID="DetailPageGridView">
            <RowStyle Wrap="True" />
            <Columns>
                <asp:TemplateField HeaderText="ORIGIN_INFO">
                    <ItemTemplate>
                        <div runat="server" id="OriginInfoDiv" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="EVENT_TASK_INFO">
                    <ItemTemplate>
                        <div runat="server" id="EventTaskInfoDiv" />
                        <asp:LinkButton runat="server" ID="btnDelete" />
                        <asp:LinkButton runat="server" ID="btnReset" />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="EXECUTION_INFO">
                    <ItemStyle Width="30%" />
                    <ItemTemplate>
                        <div runat="server" id="ExecutionInfoDiv" />
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerSettings PageButtonCount="10" Mode="Numeric" Position="TopAndBottom" />
        </asp:GridView>
    </div>
    <div class="btnZone">
        <asp:Button runat="server" ID="btnRefresh" Text="REFRESH" SkinID="AlternateLeftButton" />
    </div>
</asp:Content>
