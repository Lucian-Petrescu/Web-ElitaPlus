<%@ Page Language="vb" AutoEventWireup="false" Codebehind="EquipmentListForm.aspx.vb"
    Theme="Default" EnableSessionState="True" MasterPageFile="../Navigation/masters/ElitaBase.Master"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.EquipmentListForm" %>

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
    <table cellspacing="0" class="searchGrid" cellpadding="0" width="100%" border="0">
        <tr>
        <td   align="left" >
            <asp:Label ID="moDescriptionLabel" runat="server">Description</asp:Label>:
        </td>
        <td  align="left" >
            <asp:Label ID="moModelLabel" runat="server">Model</asp:Label>:
        </td>
        <td   align="left" >
            <asp:Label ID="moManufacturerLabel" runat="server">Manufacturer</asp:Label>:
        </td>
        </tr>
        <tr>
            <td  align="left">
                <asp:TextBox ID="moDescriptionText" runat="server" Width="75%" AutoPostBack="False" SkinID="MediumTextBox"></asp:TextBox>
            </td>
            <td  align="left">
                <asp:TextBox ID="moModelText" runat="server" Width="75%" AutoPostBack="False" SkinID="MediumTextBox"></asp:TextBox>
            </td>
            <td  align="left">
                <asp:DropDownList ID="moManufacturerDrop" runat="server" Width="75%" SkinID="SmallDropDown" AutoPostBack="False">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td   align="left" >
                <asp:Label ID="moEquipmentClassLabel" runat="server">EQUIPMENT_CLASS</asp:Label>:
            </td>
            <td align="left" >
                <asp:Label ID="moEquipmentType" runat="server">EQUIPMENT_TYPE</asp:Label>:
            </td>
            <td  align="left" >
                <asp:Label ID="moSKU" runat="server">SKU</asp:Label>:
            </td>
        </tr>
        <tr>
            <td  align="left">
                <asp:DropDownList ID="moEquipmentClassDrop" runat="server" Width="75%" SkinID="SmallDropDown" AutoPostBack="False"/>
            </td>
            <td  align="left">
                <asp:DropDownList ID="moEquipmentTypeDrop" runat="server" Width="75%" SkinID="SmallDropDown" />
            </td>
            <td align="left">
                <asp:TextBox ID="txtSKU" runat="server" Width="75%" AutoPostBack="False" SkinID="MediumTextBox"></asp:TextBox>
            </td>
         </tr>
         <tr>
            <td colspan="3" >&nbsp;</td>
         </tr>
         <tr>
            <td colspan="3" align="right" width="100%">
                <label>
                    <asp:Button ID="btnClearSearch" SkinID="AlternateRightButton" runat="server"  Text="Clear" OnClick="btnClearSearch_Click" />
                    <asp:Button ID="btnSearch" SkinID="SearchRightButton" runat="server"  Text="Search" />
                </label>
            </td>
         </tr>
    </table>
</asp:Content>                               
<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">                                         
    <div class="dataContainer" runat="server" id="moSearchResults">
        <h2 class="dataGridHeader">
            <asp:Label runat="server" ID="moSearchResultsHeader">SEARCH_RESULTS_FOR_EQUIPMENT</asp:Label>
        </h2>
        <table class="dataGrid" cellspacing="0" cellpadding="0" width="98%" border="0">
            <tr id="trPageSize" runat="server">
            <td valign="top" align="left">
                <asp:Label ID="Label3" runat="server">Page_Size</asp:Label>: &nbsp;
                <asp:DropDownList ID="cboPageSize" runat="server" Width="50px" AutoPostBack="true" SkinID="SmallDropDown">
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
                </asp:DropDownList></td>
                <td align="right">
                <asp:Label ID="lblRecordCount" runat="server"></asp:Label></td>
            </tr>
        </table>
        <asp:DataGrid ID="Grid" runat="server" Width="98%" AutoGenerateColumns="False" 
            CellPadding="1" AllowPaging="True" SkinID="DetailPageDataGrid"
            AllowSorting="True" OnItemCreated="ItemCreated" OnItemCommand="ItemCommand">
            <Columns>
                <asp:BoundColumn Visible="False" HeaderText="Id"></asp:BoundColumn>
                <asp:TemplateColumn SortExpression="DESCRIPTION" HeaderText="DESCRIPTION">
                    <ItemTemplate>
                        <asp:LinkButton CommandName="SelectAction" ID="btnEdit" runat="server"
                            OnClientClick="document.body.style.cursor = 'wait'; if(isNotDblClick()) {return true;} else return false;"
                            CommandArgument="">
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateColumn>
                <asp:BoundColumn SortExpression="MODEL" HeaderText="MODEL"/>
                <asp:BoundColumn SortExpression="MANUFACTURER" HeaderText="MANUFACTURER_NAME"/>
                <asp:BoundColumn SortExpression="SKU" HeaderText="SKU"/>
                <asp:BoundColumn SortExpression="EQUIPMENT_CLASS" HeaderText="EQUIPMENT_CLASS"/>
                <asp:BoundColumn SortExpression="EQUIPMENT_TYPE" HeaderText="EQUIPMENT_TYPE"/>
                <asp:BoundColumn SortExpression="RISK_TYPE_ID" HeaderText="RISK_TYPE_ID"/>
                <asp:BoundColumn SortExpression="COLOR" HeaderText="EQUIPMENT_COLOR"/>
                <asp:BoundColumn SortExpression="MEMORY" HeaderText="EQUIPMENT_MEMORY"/>                                
                <asp:BoundColumn SortExpression="CARRIER" HeaderText="EQUIPMENT_CARRIER"/>
            </Columns>
            <PagerStyle PageButtonCount="15" ForeColor="DarkSlateBlue"  Mode="NumericPages" Position="TopAndBottom" />
        </asp:DataGrid>
    </div>
    <div class="btnZone">
        <asp:Button ID="btnAdd_WRITE" SkinID="AlternateLeftButton" runat="server"  Text="New"></asp:Button>&nbsp;
        <asp:Button ID="btnback_WRITE" SkinID="PrimaryLeftButton" Visible="false" runat="server"  Text="Back"></asp:Button>&nbsp;
    </div>
</asp:Content>