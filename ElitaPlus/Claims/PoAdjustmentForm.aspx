<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PoAdjustmentForm.aspx.vb" EnableSessionState="True"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Claims.AccountPayable.POAdjustmentForm" MasterPageFile="../Navigation/masters/ElitaBase.Master" 
    Theme="Default" %>

<%@ Register TagPrefix="asp" Namespace="AjaxControlToolkit" Assembly="AjaxControlToolkit" %>
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
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="true" ScriptMode="Auto">
        <Scripts>
            <asp:ScriptReference Path="~/Navigation/scripts/ComunaSuggest.js" />
        </Scripts>
    </asp:ScriptManager>
     <table width="100%" border="0" class="searchGrid" id="searchTable" runat="server" >
            <tr>
                <td style="width: 200px; text-align: left;" >
                    <asp:Label ID="lblvendorcode" runat="server">Vendor_Code</asp:Label>:
                </td>
                <td style="width: 200px; text-align: left;">
                    <asp:Label ID="lblponumber" runat="server">PO_Number</asp:Label>:
                </td>
                <td style="width: 200px; text-align: left;"> </td>
            </tr>
            <tr>
                <td style="width: 200px; text-align: left;" >
                    <asp:TextBox ID="txtVendorcode" runat="server" Width="50%" AutoPostBack="False"
                        CssClass="FLATTEXTBOX"></asp:TextBox>
                </td>
                <td style="width: 200px; text-align: left;">
                    <asp:TextBox ID="txtPoNumber" runat="server" Width="50%" AutoPostBack="False"
                        CssClass="FLATTEXTBOX"></asp:TextBox>
                </td>
                <td style="width: 200px" > </td>
            </tr>
            <tr>
                <td>
                    &nbsp;
                </td>
                <td style="text-align: right;" colspan="2">
                    &nbsp;&nbsp;
                    <asp:Button ID="ClearButton" Style="background-image: url('../Navigation/images/icons/clear_icon.gif');
                        cursor: hand ; background-repeat: no-repeat" runat="server" Font-Bold="false"
                        Width="90px" Text="Clear" Height="20px" CssClass="FLATBUTTON"></asp:Button>&nbsp;&nbsp;
                    <asp:Button ID="SearchButton" Style="background-image: url('../Navigation/images/icons/search_icon.gif');
                        cursor: hand ; background-repeat: no-repeat" runat="server" Font-Bold="false"
                        Width="90px" Text="Search" Height="20px" CssClass="FLATBUTTON"></asp:Button>
                </td>
            </tr>

     </table>

</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
<!-- new layout start -->
<div class="dataContainer">
<h2 class="dataGridHeader">
    Search results for PO Adjustment </h2>
<div>
    <table width="100%" class="dataGrid">
    <tr id="tr1" runat="server">
        <td class="bor" style="text-align: left;">
            <asp:Label ID="Label1" runat="server">Page_Size</asp:Label><asp:Label ID="Label2" runat="server">:</asp:Label>
      
            <asp:Label ID="Label4" runat="server"></asp:Label> &nbsp;
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
        <td style="height: 22px; text-align: right;">
            <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
        </td>
    </tr>
    </table>
    
    <div style="width: 100%">
        <asp:GridView ID="PoGrid" runat="server" Width="100%" OnRowCommand="ItemCommand"
            OnRowCreated="ItemCreated" CellPadding="1" AllowSorting="True" AutoGenerateColumns="False"
            AllowPaging="True" SkinID="DetailPageGridView">
            <SelectedRowStyle CssClass="SELECTED"></SelectedRowStyle>
            <EditRowStyle CssClass="EDITROW"></EditRowStyle>
            <AlternatingRowStyle Wrap="False" CssClass="ALTROW"></AlternatingRowStyle>
            <RowStyle CssClass="ROW"></RowStyle>
            <HeaderStyle CssClass="HEADER"></HeaderStyle>
            <Columns>
                <asp:TemplateField ShowHeader="false">
                    <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                    <ItemTemplate>
                        <asp:ImageButton ID="EditButton_WRITE" Style="cursor: hand" runat="server" ImageUrl="~/Navigation/images/icons/edit2.gif"
                                         CommandName="EditRecord" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField Visible="False" HeaderText="po_line_id">
                <ItemTemplate>
                    &gt;
                    <asp:Label ID="lblPoLineId" runat="server">
                    </asp:Label>
                </ItemTemplate>
                </asp:TemplateField>
               <asp:TemplateField SortExpression="Vendor" HeaderText="Vendor">
                    <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblVendor" runat="server" Visible="True">
                        </asp:Label>
                    </ItemTemplate>
                   </asp:TemplateField>
                <asp:TemplateField SortExpression="PO_Number" HeaderText="PO_Number">
                    <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblPoNumber" runat="server" Visible="True" >
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField SortExpression="line_number" HeaderText="line_number">
                    <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblLineNumber" runat="server" Visible="True" >
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField SortExpression="Item_Code" HeaderText="Item_Code">
                    <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblItemCode" runat="server" Visible="True" >
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField SortExpression="Description" HeaderText="Description">
                    <ItemStyle HorizontalAlign="Left" Width="20%"></ItemStyle>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblDescription" runat="server" Visible="True" >
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField SortExpression="Quantity" HeaderText="Quantity">
                    <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblQuantity" runat="server" Visible="True" >
                        </asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox CssClass="FLATTEXTBOX_TAB" ID="txtQuantity" runat="server" Visible="True"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField SortExpression="Unit_Price" HeaderText="Unit_Price">
                    <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblUnitprice" runat="server" Visible="True" >
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField SortExpression="Extende_Price" HeaderText="Extende_Price">
                    <ItemStyle HorizontalAlign="Left" Width="10%"></ItemStyle>
                    <HeaderStyle HorizontalAlign="Center"></HeaderStyle>
                    <ItemTemplate>
                        <asp:Label ID="lblExtendePrice" runat="server" Visible="True" >
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerSettings PageButtonCount="15" Mode="Numeric" />
            <PagerStyle HorizontalAlign="Center" CssClass="PAGER"/>
        </asp:GridView>
    </div>
    <div>
        <table width="100%">
        <tr>
            <td style="text-align: left;">
                <asp:Button ID="SaveButton_WRITE" Style="background-image: url('../Navigation/images/icons/save_icon.gif');
                 cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                 Width="100px" Text="Save" Height="20px" CssClass="FLATBUTTON"></asp:Button>&nbsp;
                
                <asp:Button ID="CancelButton" Style="background-image: url('../Navigation/images/icons/cancel_icon.gif');
                 cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false"
                 Width="100px" Text="Cancel" Height="20px" CssClass="FLATBUTTON"></asp:Button>&nbsp;
            </td>
        </tr>
        </table>

    </div>
</div>
</div>
    <input id="HiddenDeletePromptResponse" type="hidden" runat="server" designtimedragdrop="261"/>
</asp:Content>

