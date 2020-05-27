<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="ClaimDeviceInformationController.ascx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.ClaimDeviceInformationController" %>

<div id="dvClaimDeviceInformation" runat="server" class="dataContainer">
    <h2 class="dataGridHeader">
        <asp:Label runat="server" ID="lblClaimDeviceInformation">CLAIM_DEVICE_INFORMATION</asp:Label>
    </h2>

    <asp:GridView ID="GvClaimDeviceInformation" runat="server" Width="100%" AutoGenerateColumns="False"
        CellPadding="1" AllowPaging="False" SkinID="DetailPageGridView" CssClass="grid-view"  AllowSorting="False"
        DataKeyNames="Id" OnRowEditing="GvClaimDeviceInformation_RowEditing" OnRowCommand="RowCommand">
        <Columns>
            <asp:TemplateField Visible="False" HeaderText="Id" >
                    <ItemTemplate>
                        <asp:Label ID="lblID" Text='<%# Eval("Id") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            <asp:TemplateField Visible="True" HeaderText="MAKE"  HeaderStyle-HorizontalAlign="Center" >
                <ItemTemplate>
                    <asp:Label ID="lblMake" Text='<%# Eval("Manufacturer") %>' runat="server"></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtMake" Text='<%# Eval("Manufacturer") %>' runat="server" Visible="True" Width="100%" SkinID="SmallTextBox" MaxLength="225"></asp:TextBox>
                    <asp:DropDownList ID="ddlMake" runat="server" Visible="True" SkinID="SmallDropDown" Width="100%"></asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField Visible="True" HeaderText="MODEL"  HeaderStyle-HorizontalAlign="Center" >
                <ItemTemplate>
                    <asp:Label ID="lblModel" Text='<%# Eval("Model") %>' runat="server"></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtModel" Text='<%# Eval("Model") %>' runat="server" Width="100%" Visible="True" SkinID="SmallTextBox" MaxLength="100"></asp:TextBox>
                    <asp:DropDownList ID="ddlModel" runat="server" Visible="True" SkinID="SmallDropDown" Width="100%"></asp:DropDownList>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField Visible="True" HeaderText="DEVICE_TYPE"  HeaderStyle-HorizontalAlign="Center">
                    <ItemTemplate>
                        <asp:Label ID="lblDeviceType" Text='<%# Eval("DeviceType") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
             <asp:TemplateField Visible="True" HeaderText="PURCHASED_DATE"  HeaderStyle-HorizontalAlign="Center" >
                    <ItemTemplate>
                        <asp:Label ID="lblPurchasedDate" Text='<%# Eval("PurchasedDate") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
             <asp:TemplateField Visible="True" HeaderText="PURCHASE_PRICE"  HeaderStyle-HorizontalAlign="Center" >
                    <ItemTemplate>
                        <asp:Label ID="lblPurchasedPrice" Text='<%# Eval("PurchasedPrice") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
             <asp:TemplateField Visible="True" HeaderText="IMEI_NUM"  HeaderStyle-HorizontalAlign="Center" >
                <ItemTemplate>
                    <asp:Label ID="lblImeiNumber" Text='<%# Eval("ImeiNumber") %>' runat="server"></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtImeiNumber" Text='<%# Eval("ImeiNumber") %>' runat="server" Width="100%" Visible="True" SkinID="exSmallTextBox" MaxLength="30"></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField Visible="True" HeaderText="SERIAL_NUM"  HeaderStyle-HorizontalAlign="Center" >
                <ItemTemplate>
                    <asp:Label ID="lblSerialNumber" Text='<%# Eval("SerialNumber") %>' runat="server" MaxLength="30"></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtSerialNumber" Text='<%# Eval("SerialNumber") %>'  runat="server" Width="100%" Visible="True" SkinID="exSmallTextBox"></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField Visible="True" HeaderText="REGISTERED_ITEM"  HeaderStyle-HorizontalAlign="Center" >
                    <ItemTemplate>
                        <asp:Label ID="lblRegisteredItemName" Text='<%# Eval("RegisteredItemName") %>' runat="server"></asp:Label>
                    </ItemTemplate>
                </asp:TemplateField>
            <asp:TemplateField Visible="True" HeaderText="COLOR"  HeaderStyle-HorizontalAlign="Center" >
                <ItemTemplate>
                    <asp:Label ID="lblColor" Text='<%# Eval("Color") %>' runat="server"></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtColor" Text='<%# Eval("Color") %>' runat="server" Visible="True" Width="100%" SkinID="exSmallTextBox" MaxLength="200"></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField Visible="True" HeaderText="CAPACITY"  HeaderStyle-HorizontalAlign="Center" >
                <ItemTemplate>
                    <asp:Label ID="lblCapacity" Text='<%# Eval("Memory") %>' runat="server"></asp:Label>
                </ItemTemplate>
                <EditItemTemplate>
                    <asp:TextBox ID="txtCapacity" Text='<%# Eval("Memory") %>' runat="server" Width="100%" Visible="True" SkinID="exSmallTextBox" MaxLength="200"></asp:TextBox>
                </EditItemTemplate>
            </asp:TemplateField>
            <asp:TemplateField Visible="True" HeaderText="EQUIPMENT_TYPE">
                <ItemTemplate>
                    <asp:Label ID="lblEquipmentType" Text='<%# Eval("EquipmentType") %>' runat="server"></asp:Label>
                </ItemTemplate>
           </asp:TemplateField>
            <asp:TemplateField ShowHeader="false" ItemStyle-HorizontalAlign="Right">
                    <ItemTemplate>
                         <asp:ImageButton ID="ImgBtnEdit" runat="server" CausesValidation="False" CommandName="Edit" Visible="false"
                            ImageUrl="~/App_Themes/Default/Images/edit.png"  />
                        
                    </ItemTemplate>
                    <EditItemTemplate>
                        <table>
                    <tr><td>
                        <asp:Button ID="btnSave" runat="server" Text="Save" CommandName="SaveRecord" SkinID="PrimaryRightButton"></asp:Button></td><td>
                        <asp:LinkButton ID="btnCancel" runat="server" Text="Cancel" OnClick="btnCancel_Click" SkinID="AlternateRightButton"></asp:LinkButton></td>
                    </tr>
                    </table>
                    </EditItemTemplate>
                </asp:TemplateField>
        </Columns>
    </asp:GridView>
  
</div>
