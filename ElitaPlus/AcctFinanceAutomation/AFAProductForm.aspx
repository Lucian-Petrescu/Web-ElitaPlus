<%@ Page Title=""  Language="vb" AutoEventWireup="false" Theme="Default" CodeBehind="AFAProductForm.aspx.vb" 
    MasterPageFile="../Navigation/masters/ElitaBase.Master" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.AFAProductForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
 <script language="javascript" type="text/javascript" src="../Navigation/scripts/jquery-1.6.1.min.js" > </script>
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
    <table width="100%" class="searchGrid" border="0" cellpadding="0" cellspacing="0">
        <tbody>
            <tr>
                <td align="left">
                    <asp:Label runat="server" ID="lblDealer">DEALER</asp:Label>:
                    <br />
                    <asp:DropDownList ID="ddlDealer" runat="server" SkinID="SmallDropDown"  AutoPostBack="true" style="width:250px;">
                    </asp:DropDownList>
                </td>
                <td align="left">
                    <asp:Label ID="lblProductCode" runat="server">PRODUCT_CODE</asp:Label>:<br />
                    <asp:TextBox ID="txtProductCode" runat="server" SkinID="MediumTextBox" />
                </td>
                <td align="left">
                    <br />
                    <asp:Button ID="btnSearch" runat="server" Text="Search" SkinID="SearchRightButton" />
                    <asp:Button ID="btnClearSearch" runat="server" Text="Clear" SkinID="AlternateRightButton" />                    
                </td>
            </tr>
        </tbody>
    </table>
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
    <div class="dataContainer" runat="server" id="moSearchResults">
        <h2 class="dataGridHeader">
            <asp:Label runat="server" ID="moSearchResultsHeader">SEARCH_RESULTS</asp:Label>
        </h2>
        <table width="100%" class="dataGrid" border="0" cellspacing="0" cellpadding="0">
            <tbody>
                <tr>
                    <td class="bor">
                        <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>
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
        <asp:GridView ID="Grid" runat="server" Width="100%" OnRowCreated="RowCreated" OnRowCommand="RowCommand"
            AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" SkinID="DetailPageGridView">
            <Columns>
                <asp:TemplateField Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="lblProductID" Text='<%# GetGuidStringFromByteArray(Container.DataItem("afa_product_id")) %>'
                            runat="server">
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="DEALER">
                    <ItemTemplate>
                        <asp:Label ID="lblDealer" runat="server">
                        </asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <%--<asp:DropDownList id="moDealer" runat="server" visible="True"></asp:DropDownList>--%>
                        <asp:Label ID="lblEditDealer" runat="server"></asp:Label>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="CODE">
                    <ItemTemplate>
                        <asp:LinkButton CommandName="SelectAction" ID="btnEditInvoiceRate" runat="server"
                                OnClientClick="document.body.style.cursor = 'wait'; if(isNotDblClick()) {return true;} else return false;"></asp:LinkButton> 
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtCode" runat="server" Visible="True" SkinID="SmallTextBox"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="DESCRIPTION">
                    <ItemTemplate>
                        <asp:Label ID="lblDesc" runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:TextBox ID="txtDesc" runat="server" Visible="True" SkinID="SmallTextBox"></asp:TextBox>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="True" HeaderText="PRODUCT_TYPE">
                    <ItemTemplate>
                        <asp:Label ID="lblProdType" runat="server"></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList id="ddlProdType" runat="server" visible="True"></asp:DropDownList>
                    </EditItemTemplate>
                </asp:TemplateField>                
                <asp:TemplateField ShowHeader="false" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:ImageButton ID="EditButton_WRITE" runat="server" CausesValidation="False" CommandName="SelectAction"
                            ImageUrl="~/App_Themes/Default/Images/edit.png" CommandArgument="<%#Container.DisplayIndex %>" />                        
                    </ItemTemplate>
                    <EditItemTemplate>                        
                        <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" SkinID="AlternateRightButton"></asp:LinkButton>
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:ImageButton ID="DeleteButton_WRITE" ImageUrl="~/App_Themes/Default/Images/icon_delete.png"
                            runat="server" CommandName="DeleteRecord" CommandArgument="<%#Container.DisplayIndex %>" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" SkinID="PrimaryRightButton"></asp:Button>                        
                    </EditItemTemplate>
                </asp:TemplateField>
            </Columns>
            <PagerSettings PageButtonCount="10" Mode="Numeric" Position="TopAndBottom" />
        </asp:GridView>
        <input id="HiddenDeletePromptResponse" type="hidden" runat="server" designtimedragdrop="261" />
    </div>
    <div class="btnZone">
        <asp:Button runat="server" ID="btnNew" Text="New" SkinID="AlternateLeftButton" />
    </div>
</asp:Content>