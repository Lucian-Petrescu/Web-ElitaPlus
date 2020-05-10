<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlAuthorizationInfo.ascx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.UserControlAuthorizationInfo" %>
<link href="../App_Themes/Default/Default.css" rel="stylesheet" />
<h2 class="dataGridHeader">
    <asp:Label ID="AuthItems" runat="server">Claim Authorization Items</asp:Label>
    <asp:LinkButton ID="BtnNew_WRITE" runat="server" Text="ADD_NEW_AUTHORIZATION"></asp:LinkButton>
</h2>
<div style="overflow: auto; height: 200px;">
    <asp:GridView ID="AuthorizationGrid" runat="server" Width="100%" AllowPaging="false"
        AllowSorting="false"  CssClass="dataGrid">
        <Columns>
            <asp:BoundField HeaderText="Authorization_Item_Id" Visible="false" ReadOnly="true" />
            <asp:BoundField HeaderText="LINE_ITEM_NUMBER" Visible="true" ReadOnly="true" />
            <asp:TemplateField HeaderText="service_class" HeaderStyle-CssClass="CenteredTD">
                <ItemTemplate>
                    <asp:Label ID="lblService_Class" runat="server"> </asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlService_class" runat="server" OnSelectedIndexChanged="ServiceClassSelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="service_type" HeaderStyle-CssClass="CenteredTD">
                <ItemTemplate>
                    <asp:Label ID="lblService_type" runat="server"> </asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:DropDownList ID="ddlService_type" runat="server" OnSelectedIndexChanged="ServiceTypeSelectedIndexChanged"
                        AutoPostBack="true">
                    </asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="vendor_sku" HeaderStyle-CssClass="CenteredTD">
                <ItemTemplate>
                    <asp:Label ID="lblvendor_sku" runat="server"> </asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:textbox ID="txtvendor_sku" runat="server">
                    </asp:textbox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="vendor_sku_description" HeaderStyle-CssClass="CenteredTD">
                <ItemTemplate>
                    <asp:Label ID="lblvendor_sku_description" runat="server"> </asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtvendor_sku_description" runat="server">
                    </asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="AMOUNT" HeaderStyle-CssClass="CenteredTD">
                <ItemTemplate>
                    <asp:Label ID="lblAMOUNT" runat="server"> </asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtAMOUNT" runat="server">
                    </asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField HeaderText="ADJUSTMENT_REASON" HeaderStyle-CssClass="CenteredTD">
                <ItemTemplate>
                    <asp:Label ID="lbLADJUSTMENT_REASON" runat="server"> </asp:Label>
                </ItemTemplate>  
                <EditItemTemplate>
                    <asp:Label ID="txtADJUSTMENT_REASON" runat="server">
                    </asp:Label>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField ShowHeader="false">
                <ItemStyle HorizontalAlign="Center" Width="3%"></ItemStyle>
                <ItemTemplate>
                    <asp:ImageButton ID="EditButton_WRITE" Style="cursor: hand" runat="server" ImageUrl="~/Navigation/images/edit.png"
                        CommandName="EditRecord" CommandArgument="<%#Container.DisplayIndex %>"></asp:ImageButton>
                    <asp:ImageButton Style="cursor: hand;" ID="DeleteButton_WRITE" ImageUrl="~/Navigation/images/icon_delete.png"
                        runat="server" CommandName="DeleteRecord" CommandArgument="<%#Container.DisplayIndex %>">
                    </asp:ImageButton>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:Button ID="btnSave" runat="server" CommandName="SaveRecord" CommandArgument="<%#Container.DisplayIndex %>"
                        Text="Save" CssClass="primaryBtn floatR"></asp:Button>
                    <asp:LinkButton ID="btnCancel" runat="server" CommandName="CancelRecord" CommandArgument="<%#Container.DisplayIndex %>"
                        Text="Cancel"></asp:LinkButton>
                </EditItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
     <input id="HiddenDeletePromptResponse" type="hidden" runat="server" designtimedragdrop="261" />
   
</div>
