<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="UserControlAttrtibutes.ascx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.UserControlAttrtibutes" %>
<asp:GridView ID="AttributeValueGridView" runat="server" Width="100%" AutoGenerateColumns="false"
    AllowPaging="false" CellPadding="1" SkinID="DetailPageGridView">
    <RowStyle HorizontalAlign="Left" />
    <Columns>
        <asp:TemplateField HeaderText="ATTRIBUTE NAME">
            <ItemTemplate>
                <asp:Label runat="server" ID="UiProgCodeLabel"></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:DropDownList runat="server" ID="UiProgCodeDropDown" AutoPostBack="true" OnSelectedIndexChanged="UiProgCodeDropDown_SelectedIndexChanged">
                </asp:DropDownList>
                <asp:Label runat="server" ID="UiProgCodeLabel"></asp:Label>
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="VALUE">
            <ItemTemplate>
                <asp:Label runat="server" ID="AttributeValueLabel"></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:DropDownList runat="server" ID="AttributeValueDropDown">
                </asp:DropDownList>
                <asp:TextBox runat="server" ID="AttributeValueTextBox"></asp:TextBox>
                <asp:ImageButton ID="AttributeValueImageButton" runat="server" Style="vertical-align: bottom"
                    ImageUrl="~/App_Themes/Default/Images/calendar.png" Visible="false" />
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="EFFECTIVE_DATE" ItemStyle-Width="200px">
            <ItemStyle HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:Label runat="server" ID="EffectiveDateLabel"></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox runat="server" ID="EffectiveDateTextBox" Visible="false" SkinID="SmallTextBox" />
                <asp:ImageButton ID="EffectiveDateImageButton" runat="server" Style="vertical-align: bottom"
                    ImageUrl="~/App_Themes/Default/Images/calendar.png" Visible="false" />
                <asp:Label runat="server" ID="EffectiveDateLabel" Visible="false" />
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField HeaderText="EXPIRATION_DATE" ItemStyle-Width="200px">
            <ItemStyle HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:Label runat="server" ID="ExpirationDateLabel"></asp:Label>
            </ItemTemplate>
            <EditItemTemplate>
                <asp:TextBox runat="server" ID="ExpirationDateTextBox" Visible="false" SkinID="SmallTextBox" />
                <asp:ImageButton ID="ExpirationDateImageButton" runat="server" Style="vertical-align: bottom"
                    ImageUrl="~/App_Themes/Default/Images/calendar.png" Visible="false" />
                <asp:Label runat="server" ID="ExpirationDateLabel" Visible="false" />
            </EditItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField ItemStyle-Width="50px">
            <ItemStyle HorizontalAlign="Center" />
            <ItemTemplate>
                <asp:ImageButton runat="server" ID="EditButton" AlternateText="Edit" CommandName="EditRecord"
                    ImageUrl="~/App_Themes/Default/Images/edit.png" />
                <asp:ImageButton runat="server" ID="DeleteButton" AlternateText="Delete" CommandName="DeleteRecord"
                    ImageUrl="~/App_Themes/Default/Images/icon_delete.png" />
            </ItemTemplate>
            <EditItemTemplate>
           
                 <table>
                   <tr>
                       <td>
                <asp:LinkButton ID="CancelButton" runat="server" CommandName="CancelRecord" Text="Cancel"></asp:LinkButton>
                       </td>
                     <td>              
                <asp:Button ID="SaveButton" runat="server" CommandName="SaveRecord"
                                           Text="Save" SkinID="PrimaryRightButton"></asp:Button>
                     </td>
                   </tr>
               </table>
                   
            </EditItemTemplate>

        </asp:TemplateField>
    </Columns>
</asp:GridView>
<asp:Button runat="server" ID="NewButton" SkinID="PrimaryLeftButton"
    Text="ADD_NEW" />