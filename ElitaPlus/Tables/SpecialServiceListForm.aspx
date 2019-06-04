<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SpecialServiceListForm.aspx.vb"
    Theme="Default" MasterPageFile="~/Navigation/masters/ElitaBase.Master" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.SpecialServiceListForm" %>

<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="../Common/ErrorController.ascx" %>
<%@ Register TagPrefix="uc1" TagName="MultipleColumnDDLabelControl" Src="../Common/MultipleColumnDDLabelControl_New.ascx" %>
<%@ Import Namespace="System.Web.UI.HtmlControls" %>
<asp:Content ID="Content3" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content1" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
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
    <table width="100%" align="center" border="0" class="searchGrid">
        <tr>
            <td valign="top" colspan="3">
                <uc1:ErrorController ID="moErrorController" runat="server"></uc1:ErrorController>
            </td>
        </tr>
    </table>
    <table width="30%" class="searchGrid" border="0">
        <tr>
            <td style="white-space: nowrap;">
                <asp:Label ID="CoverageTypeLabel" runat="server" Font-Size="Small">Coverage_Type:</asp:Label>
            </td>
            <td align="left" colspan="2">
                <asp:DropDownList ID="cboCoverageType" runat="server" SkinID="MediumDropDown">
                </asp:DropDownList>
            </td>
            <td>
                &nbsp;
            </td>
        </tr>
        <uc1:MultipleColumnDDLabelControl ID="multipleDropControl" runat="server"></uc1:MultipleColumnDDLabelControl>
        <tr><td colspan="4">&nbsp;</td></tr>

        <tr>
            <td align="right" colspan="4">
                <asp:Button ID="btnClear" runat="server" CausesValidation="False" SkinID="AlternateLeftButton"
                    Text="CLEAR"></asp:Button>
                <asp:Button ID="btnSearch" runat="server" CausesValidation="False" SkinID="SearchButton"
                    Text="SEARCH"></asp:Button>
            </td>
        </tr>
    </table>
    
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer">
        <h2 class="dataGridHeader">
            Search results for Special Service</h2>
        <div>
            <table width="100%" class="dataGrid">
                <tr id="trPageSize" runat="server">
                    <td class="bor" align="left">
                        <asp:Label ID="PageSizeLabel" runat="server">Page_Size</asp:Label><asp:Label ID="colonSepertor"
                            runat="server">:</asp:Label>&nbsp;
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
                        <asp:Label ID="RecordCountLabel" runat="server"></asp:Label>
                    </td>
                </tr>
            </table>
        </div>
        <div style="width: 100%">
            <asp:GridView ID="moSplServiceGrid" runat="server" Width="100%" AutoGenerateColumns="False"
                AllowPaging="True" SkinID="DetailPageGridView" AllowSorting="true">
                <SelectedRowStyle Wrap="True" />
                <EditRowStyle Wrap="True" />
                <AlternatingRowStyle Wrap="True" />
                <RowStyle Wrap="True" />
                <Columns>
                    <asp:TemplateField HeaderText="DEALER_NAME" SortExpression="DEALER_NAME">
                        <ItemTemplate>
                            <asp:LinkButton Text='<%# Eval("DEALER_NAME") %>' CommandName="SelectUser" ID="imgbtnEdit"
                                runat="server" CommandArgument='<%# GetGuidStringFromByteArray(Container.DataItem("SPECIAL_SERVICE_ID")) %>'
                                OnClientClick="document.body.style.cursor = 'wait'; if(isNotDblClick()) {return true;} else return false;"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:BoundField DataField="COVERAGE_TYPE" SortExpression="COVERAGE_TYPE" ReadOnly="True"
                        HeaderText="COVERAGE_TYPE" HeaderStyle-HorizontalAlign="Center" HtmlEncode="false">
                        <HeaderStyle Width="10%"></HeaderStyle>
                    </asp:BoundField>
                    <asp:BoundField DataField="CAUSE_OF_LOSS" SortExpression="CAUSE_OF_LOSS" HeaderText="CAUSE_OF_LOSS"
                        HeaderStyle-HorizontalAlign="Center" HtmlEncode="false">
                        <HeaderStyle Width="15%"></HeaderStyle>
                    </asp:BoundField>
                </Columns>
                <PagerSettings PageButtonCount="30" Mode="Numeric" Position="TopAndBottom" />
                <PagerStyle />
            </asp:GridView>
        </div>
        <div class="btnZone">
            <asp:Button ID="btnNew" runat="server" CausesValidation="False" SkinID="AlternateLeftButton"
                Text="NEW"></asp:Button>&nbsp;
        </div>
    </div>
</asp:Content>
