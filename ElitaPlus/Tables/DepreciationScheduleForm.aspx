<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="DepreciationScheduleForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.DepreciationScheduleForm" Theme="Default"
    MasterPageFile="../Navigation/masters/ElitaBase.Master" %>

<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl_new.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
    <script type="text/javascript" src="../Navigation/Scripts/GlobalHeader.js"></script>
    <script type="text/javascript" src="https://code.jquery.com/jquery-1.11.3.js"></script>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="SummaryContent" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table id="moTableOuter" border="0" align="left">
        <tr>
            <td>
                <table class="searchGrid" border="0">
                    <tr>
                        <td style="width: 1px"></td>
                        <td nowrap="nowrap" align="center" colspan="2">
                            <uc1:MultipleColumnDDLabelControl ID="CompanyMultipleDrop" runat="server"></uc1:MultipleColumnDDLabelControl>
                        </td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap" align="right" colspan="1">&nbsp;
                            <asp:Label ID="lblDepreciationScheduleCode" runat="server">DEPRECIATION_SCHEDULE_CODE</asp:Label>&nbsp;
                        </td>
                        <td nowrap="nowrap" colspan="1" align="left">
                            <asp:TextBox ID="txtDepreciationScheduleCode" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                        </td>
                        <td colspan="1"></td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap" align="right" colspan="1">&nbsp;
                            <asp:Label ID="lblDepreciationScheduleDescription" runat="server">DEPRECIATION_SCHEDULE_DESCRIPTION</asp:Label>&nbsp;
                        </td>
                        <td nowrap="nowrap" colspan="1" align="left">
                            <asp:TextBox ID="txtDepreciationScheduleDescription" runat="server" SkinID="SmallTextBox"></asp:TextBox>
                        </td>
                        <td colspan="1"></td>
                    </tr>
                    <tr>
                        <td nowrap="nowrap" align="right" colspan="1">&nbsp;
                            <asp:Label ID="lblDepreciationScheduleActive" runat="server">DEPRECIATION_SCHEDULE_ACTIVE</asp:Label>&nbsp;
                        </td>
                        <td nowrap="nowrap" colspan="1" align="left">
                            <asp:DropDownList ID="ddlDepreciationScheduleActive" runat="server" SkinID="SmallDropDown" AutoPostBack="true"></asp:DropDownList>
                        </td>
                        <td colspan="1"></td>
                    </tr>
                </table>
            </td>
        </tr>
    </table>
</asp:Content>

<asp:Content ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
        <asp:HiddenField ID="hdnSelectedTab" runat="server" Value="0"></asp:HiddenField>
        <div id="tabs" class="style-tabs">
            <ul>
                <li><a href="#tabsDepreciationSchedule" rel="noopener noreferrer">
                    <asp:Label ID="Label4" runat="server" CssClass="tabHeaderText">DEPRECIATION_SCHEDULE</asp:Label></a>
                </li>
            </ul>

            <div id="tabsDepreciationSchedule">
                <table id="tblDepreciationSchedule" class="dataGrid" width="98%" border="0" rules="cols">
                    <tr>
                        <td colspan="2">
                            <uc1:ErrorController ID="ErrorControllerDS" runat="server"></uc1:ErrorController>
                        </td>
                    </tr>
                    <tr>
                        <td align="center" colspan="2">
                            <div id="scroller" style="overflow: auto; width: 96%; height: 125px" align="center">
                                <asp:GridView ID="DepSchDetailsGridView" runat="server" OnRowCreated="DepSchDetailsGridView_RowCreated" OnRowCommand="DepSchDetailsGridView_RowCommand" AllowPaging="False" AllowSorting="false" PageSize="50" CellPadding="1" AutoGenerateColumns="False" SkinID="DetailPageGridView">
                                    <SelectedRowStyle Wrap="False"></SelectedRowStyle>
                                    <EditRowStyle Wrap="False"></EditRowStyle>
                                    <AlternatingRowStyle Wrap="False"></AlternatingRowStyle>
                                    <RowStyle Wrap="False"></RowStyle>
                                    <HeaderStyle Wrap="False"></HeaderStyle>
                                    <Columns>
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:ImageButton ID="EditButton_WRITE" Style="cursor: hand" runat="server" ImageUrl="../Navigation/images/icons/edit2.gif" CommandName="EditRecord" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField ShowHeader="false">
                                            <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                                            <ItemTemplate>
                                                <asp:ImageButton Style="cursor: hand;" ID="DeleteButton_WRITE" ImageUrl="../Navigation/images/icons/trash.gif" runat="server" CommandName="DeleteRecord" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="moDepreciationScheduleItemIDlabel" Text='<%# GetGuidStringFromByteArray(Container.DataItem("DEPRECIATION_SCHEDULE_ITEM_ID"))%>' runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="True" HeaderText="Low_Month">
                                            <ItemTemplate>
                                                <asp:Label ID="moLowMonthLabel" Text='<%# Container.DataItem("LOW_MONTH")%>' runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="moLowMonthText" runat="server" Visible="true" Width="75"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="True" HeaderText="High_Month">
                                            <ItemTemplate>
                                                <asp:Label ID="moHighMonthLabel" Text='<%# Container.DataItem("HIGH_MONTH")%>' runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="moHighMonthText" runat="server" Visible="True" Width="75"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="True" HeaderText="Percent">
                                            <ItemTemplate>
                                                <asp:Label ID="moPercentLabel" Text='<%# Container.DataItem("PERCENT")%>' runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="moPercentText" runat="server" Visible="True" Width="75"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="True" HeaderText="Amount">
                                            <ItemTemplate>
                                                <asp:Label ID="moAmountLabel" Text='<%# Container.DataItem("AMOUNT")%>' runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                            <EditItemTemplate>
                                                <asp:TextBox ID="moAmountText" runat="server" Visible="True" Width="75"></asp:TextBox>
                                            </EditItemTemplate>
                                        </asp:TemplateField>
                                        <asp:TemplateField Visible="False">
                                            <ItemTemplate>
                                                <asp:Label ID="moDepreciationScheduleIDlabel" Text='<%# GetGuidStringFromByteArray(Container.DataItem("DEPRECIATION_SCHEDULE_ID"))%>' runat="server">
                                                </asp:Label>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                    <PagerStyle HorizontalAlign="Center" CssClass="PAGER"></PagerStyle>
                                </asp:GridView>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2" width="96%" align="left">&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;
                            <asp:Button ID="btnNewDepScheduleItem" runat="server" SkinID="AlternateLeftButton" Text="New"></asp:Button>&nbsp;
                            <asp:Button ID="btnSaveDepScheduleItem" runat="server" SkinID="PrimaryRightButton" Text="Save"></asp:Button>&nbsp;
                            <asp:Button ID="btnCancelDepScheduleItem" runat="server" SkinID="AlternateLeftButton" Text="Cancel"></asp:Button>
                        </td>
                    </tr>
                </table>
            </div>
        </div>
    </div>
    <div class="btnZone">
        <asp:Button ID="btnBack" runat="server" SkinID="AlternateLeftButton" Text="BACK"></asp:Button>
        <asp:Button ID="btnNew" runat="server" SkinID="AlternateLeftButton" Text="New"></asp:Button>
        <asp:Button ID="btnCopy" runat="server" SkinID="AlternateLeftButton" Text="New_With_Copy"></asp:Button>
        <asp:Button ID="btnApply" runat="server" SkinID="PrimaryRightButton" Text="SAVE"></asp:Button>
    </div>
</asp:Content>



ver" SkinID="AlternateLeftButton" Text="New_With_Copy"></asp:Button>
        <asp:Button ID="btnApply" runat="server" SkinID="PrimaryRightButton" Text="SAVE" />
    </div>
</asp:Content>



