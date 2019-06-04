<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PaymentTypeForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Tables.PaymentTypeForm"
     MasterPageFile="../Navigation/masters/ElitaBase.Master" theme="Default" %>
<%@ Register TagPrefix="mytab" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<%@ Register TagPrefix="Elita" TagName="MultipleColumnDDLabelControl" Src="~/Common/MultipleColumnDDLabelControl_New.ascx" %>
<%@ Register TagPrefix="uc1" TagName="ErrorController" Src="~/Common/ErrorController.ascx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content4" ContentPlaceHolderID="BodyPlaceHolder" runat="server">
<div class="dataContainer" runat="server" id="moDetailSection">
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
        <asp:GridView ID="Grid" runat="server" Width="100%" OnRowCreated="RowCreated"  OnRowCommand="RowCommand"
            AllowPaging="True" AllowSorting="True" AutoGenerateColumns="False" SkinID="DetailPageGridView">
            <Columns>
                <asp:TemplateField Visible="False">
                    <ItemTemplate>
                        <asp:Label ID="IdLabel" Text='<%# GetGuidStringFromByteArray(Container.DataItem("Payment_Type_Id")) %>'
                            runat="server">
                        </asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="true" SortExpression="collection_method" HeaderText="collection_method" >
                    <ItemTemplate>
                        <asp:Label ID="Collection_MethodLabel" Text='<%# Container.DataItem("collection_method")%>' runat="server">
                        </asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:DropDownList runat="server" ID="cboCollection_MethodInGrid" SkinID="SmallDropDown" />
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField Visible="true" SortExpression="payment_instrument" HeaderText="payment_instrument">
                    <ItemTemplate>
                        <asp:Label ID="Payment_InstrumentLabel" runat="server" Visible="True" Text='<%# Container.DataItem("payment_instrument")%>'></asp:Label>
                    </ItemTemplate>
                    <EditItemTemplate>
                         <asp:DropDownList ID="cboPayment_InstrumentInGrid" runat="server"></asp:DropDownList> 
                    </EditItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField ShowHeader="false" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                        <asp:ImageButton ID="EditButton_WRITE" runat="server" CausesValidation="False" CommandName="SelectAction"
                            ImageUrl="~/App_Themes/Default/Images/edit.png" CommandArgument="<%#Container.DisplayIndex %>" />
                        <asp:ImageButton ID="DeleteButton_WRITE" ImageUrl="~/App_Themes/Default/Images/icon_delete.png"
                            runat="server" CommandName="DeleteRecord" CommandArgument="<%#Container.DisplayIndex %>" />
                    </ItemTemplate>
                    <EditItemTemplate>
                        <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" SkinID="PrimaryRightButton"></asp:Button>
                        <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" SkinID="AlternateRightButton"></asp:LinkButton>
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