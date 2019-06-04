<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WorkQueueUsersForm.aspx.vb" Theme="Default" 
Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.WorkQueueUsersForm" 
EnableSessionState="True" MasterPageFile="../Navigation/masters/ElitaBase.Master" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="MessagePlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="SummaryPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Body" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
  <div>
    <table width="100%" class="dataGrid">
        <tr id="trPageSize" runat="server">
            <td class="bor" align="left">
                <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label><asp:Label ID="colonSepertor" runat="server">:</asp:Label> &nbsp;
                <asp:DropDownList ID="cboPageSize" runat="server" AutoPostBack="true"
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
   <div ID="divDataContainer" class="dataContainer" runat="server">
     <asp:GridView ID="Grid" runat="server" Width="100%" OnRowCreated="RowCreated" AutoGenerateColumns="False" AllowPaging="True"
      SkinID="DetailPageGridView" AllowSorting="true">
      <SelectedRowStyle Wrap="True" />
      <EditRowStyle Wrap="True" />
      <AlternatingRowStyle Wrap="True" />
      <RowStyle Wrap="True" />
      <HeaderStyle   />
        <Columns>
            <asp:TemplateField HeaderText="USER" SortExpression="USER_NAME">
                <ItemTemplate>
                    <asp:Label ID="lblUser" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.USER_NAME") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ITEMS_ACCESSED" SortExpression="ITEMS_ACCESSED">
                <ItemTemplate>
                    <asp:Label ID="lblItemsAccessed" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ITEMS_ACCESSED") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ITEMS_PROCESSED" SortExpression="ITEMS_PROCESSED">
                <ItemTemplate>
                    <asp:Label ID="lblProcessed" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ITEMS_PROCESSED") %>' />
                </ItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ITEMS_REQUEUED" SortExpression="ITEMS_REQUEUED">
                <ItemTemplate>
                    <asp:Label ID="lblRequeued" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ITEMS_REQUEUED") %>' />
                </ItemTemplate>
            </asp:TemplateField>
             <asp:TemplateField HeaderText="ITEMS_REDIRECTED" SortExpression="ITEMS_REDIRECTED">
                <ItemTemplate>
                    <asp:Label ID="lblRedirected" runat="server" Text='<%# DataBinder.Eval(Container, "DataItem.ITEMS_REQUEUED") %>' />
                </ItemTemplate>
            </asp:TemplateField>          
        </Columns>
        <PagerSettings PageButtonCount="10" Mode="Numeric" Position="TopAndBottom" />
        <PagerStyle/>
    </asp:GridView>  
   </div>
   <div class="btnZone">                       
    <asp:Button ID="btnBack" runat="server" Text="BACK" SkinID="AlternateLeftButton"></asp:Button>
    <asp:Button ID="BtnNew_WRITE" runat="server" Text="Add_New" SkinID="AlternateLeftButton"></asp:Button>
   </div>
</asp:Content>