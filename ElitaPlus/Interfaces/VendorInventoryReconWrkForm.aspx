<%@ Page ValidateRequest="false" Language="vb" AutoEventWireup="false"
    CodeBehind="VendorInventoryReconWrkForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Interfaces.VendorInventoryReconWrkForm"
    Theme="Default" MasterPageFile="../Navigation/masters/ElitaBase.Master" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">

    <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.6.1.min.js"></script>
    <script language="JavaScript" type="text/javascript" src="../Navigation/scripts/GlobalHeader.js"></script>

</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
    <table width="100%" border="0" class="searchGrid">
        <tr>
            <td align="right">
                <asp:Label ID="moCountryLabel" runat="server">COUNTRY</asp:Label>:
            </td>
            <td>
                <asp:TextBox ID="moCountryText" runat="server" Visible="True" ReadOnly="True"
                    SkinID="MediumTextBox" Enabled="False"></asp:TextBox>
            </td>
            <td align="right">
                <asp:Label ID="moServiceCenterLabel" runat="server" Visible="True">SERVICE_CENTER</asp:Label>:
            </td>
            <td>
                <asp:TextBox ID="moServiceCenterText" runat="server" SkinID="MediumTextBox" Visible="True"
                    ReadOnly="True" Enabled="False"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="moFileNameLabel" runat="server">FILENAME</asp:Label>:
            </td>
            <td>
                <asp:TextBox ID="moFileNameText" runat="server" SkinID="MediumTextBox" Visible="True"
                    ReadOnly="True" Enabled="False"></asp:TextBox>
            </td>
            <td align="right">
                <asp:Label ID="moRejectReasonLabel" runat="server" Visible="True">REJECT_REASON:</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="moRejectReasonText" runat="server"
                    SkinID="MediumTextBox" Visible="True"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td align="right">
                <asp:Label ID="moRecordTypeLabel" runat="server">RECORD_TYPE:</asp:Label>
            </td>
            <td>
                <asp:DropDownList ID="moRecordTypeSearchDrop" runat="server" Visible="True" Width="55px"
                    SkinID="MediumDropDown">
                </asp:DropDownList>
            </td>
            <td align="right">
                <asp:Label ID="moRejectCodeLabel" runat="server">REJECT_CODE:</asp:Label>
            </td>
            <td>
                <asp:TextBox ID="moRejectCodeText" runat="server" Visible="True" Width="50px"
                    SkinID="MediumTextBox"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td colspan="3"></td>
            <td>
                <asp:Button ID="btnClearSearch" runat="server" SkinID="AlternateLeftButton" Text="Clear"></asp:Button>
                <asp:Button ID="btnSearch" runat="server" SkinID="SearchButton" Text="Search"></asp:Button>
            </td>
        </tr>
    </table>
</asp:Content>

<asp:Content ID="Content4" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <!-- new layout start -->
    <asp:ScriptManager ID="ScriptManager2" runat="server"></asp:ScriptManager>
    <script type="text/javascript">
        function Test(obj) {

        }

        function setDirty() {
            var inpId = document.getElementById('<%= HiddenIsPageDirty.ClientID %>')
             inpId.value = "YES"
         }

         function UpdateDropDownCtr(obj, oField) {
             document.getElementById(oField).value = obj.value
         }

         function UpdateCtr(oDropDown, oField) {
             document.getElementById(oField).value = oDropDown.value
             setDirty()
         }

    </script>
    <div class="dataContainer">
        <h2 class="dataGridHeader">
            <asp:Label runat="server" ID="moSearchResultsHeader">SEARCH_RESULTS</asp:Label>
        </h2>
        <div>
            <table width="100%" class="dataGrid">
                <tr id="tr1" runat="server">
                    <td align="left">
                        <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>: &nbsp;
                        <asp:DropDownList ID="cboPageSize" runat="server" Width="50px" AutoPostBack="true">
                            <asp:ListItem Value="5">5</asp:ListItem>
                            <asp:ListItem Value="10">10</asp:ListItem>
                            <asp:ListItem Value="15">15</asp:ListItem>
                            <asp:ListItem Value="20">20</asp:ListItem>
                            <asp:ListItem Value="25">25</asp:ListItem>
                            <asp:ListItem Value="30">30</asp:ListItem>
                            <asp:ListItem Value="35">35</asp:ListItem>
                        </asp:DropDownList>
                    </td>
                    <td class="bor" align="right">
                        <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
                        <input id="HiddenSavePagePromptResponse" type="hidden" runat="server" />
                        <input id="HiddenIsPageDirty" type="hidden" runat="server" />
                    </td>
                </tr>
            </table>
        </div>
        <div>
            <asp:UpdatePanel ID="updatePanel1" runat="server">
                <ContentTemplate>
                    <div id="div-datagrid" style="overflow: auto; width: 100%; height: 400px;">
                        <asp:GridView ID="moDataGrid" runat="server" OnRowCreated="ItemCreated"
                            AllowPaging="True" AllowCustomPaging="true" AllowSorting="True" AutoGenerateColumns="False"
                            PageSize="30" SkinID="DetailPageGridView">
                            <SelectedRowStyle Wrap="True"></SelectedRowStyle>
                            <EditRowStyle Wrap="True"></EditRowStyle>
                            <AlternatingRowStyle Wrap="True"></AlternatingRowStyle>
                            <RowStyle Wrap="True"></RowStyle>
                            <HeaderStyle Wrap="True" HorizontalAlign="Center"></HeaderStyle>
                            <PagerStyle HorizontalAlign="left" />
                            <Columns>
                                <asp:TemplateField Visible="False">
                                    <ItemStyle></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="moVendorInventoryReconWrkIdLabel" runat="server" Text='<%# GetGuidStringFromByteArray(Container.DataItem("vendorload_inv_recon_wrk_id"))%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="RECORD_TYPE" HeaderText="RECORD_TYPE">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="true"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Left" Width="5%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:DropDownList ID="moRecordTypeDrop" runat="server" Visible="True">
                                        </asp:DropDownList>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="REJECT_CODE" HeaderText="REJECT_CODE">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="true"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="10%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moRejectCode" runat="server" Visible="True" Width="95%" ReadOnly="true"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="REJECT_REASON" HeaderText="REJECT_REASON">
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="50%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="RejectReasonTextGrid" runat="server" Visible="True" Width="95%" ReadOnly="true"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>

                                <asp:TemplateField SortExpression="VENDOR_SKU" HeaderText="VENDOR_SKU">
                                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="20%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moVendorSkuTextGrid" runat="server" Visible="True" Width="95%"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField SortExpression="INVENTORY_QUANTITY" HeaderText="INVENTORY_QUANTITY">
                                    <HeaderStyle HorizontalAlign="Center" Wrap="true"></HeaderStyle>
                                    <ItemStyle Wrap="False" HorizontalAlign="Left" Width="10%"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:TextBox ID="moInventoryQuantityTextGrid" runat="server"
                                            Visible="True" Width="95%"></asp:TextBox>
                                    </ItemTemplate>
                                </asp:TemplateField>
                                <asp:TemplateField Visible="False">
                                    <ItemStyle HorizontalAlign="Left"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:Label ID="moModifiedDateLabel" runat="server" Text='<%# Container.DataItem("modified_date")%>'>
                                        </asp:Label>
                                    </ItemTemplate>
                                </asp:TemplateField>
                            </Columns>
                            <PagerSettings PageButtonCount="15" Mode="Numeric" Position="TopAndBottom" />
                            <PagerStyle HorizontalAlign="left" CssClass="PAGER_LEFT"></PagerStyle>
                        </asp:GridView>
                    </div>
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
    </div>
    <div class="btnZone">
        <asp:Button ID="SaveButton_WRITE" runat="server" Text="Save" CausesValidation="False"
            SkinID="AlternateLeftButton"></asp:Button>&nbsp;
        <asp:Button ID="btnUndo_WRITE" runat="server" Text="Undo" CausesValidation="False"
            SkinID="AlternateLeftButton"></asp:Button>&nbsp;
        <asp:Button ID="btnBack" runat="server" Text="Back" CausesValidation="False"
            SkinID="AlternateLeftButton"></asp:Button>&nbsp;
    </div>
</asp:Content>
