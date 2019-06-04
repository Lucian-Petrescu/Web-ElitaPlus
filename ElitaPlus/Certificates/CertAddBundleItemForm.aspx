<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master" CodeBehind="CertAddBundleItemForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.CertAddBundleItemForm" 
    title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td style="text-align:center">
                <asp:DataGrid id="Grid" runat="server" Width="100%" AllowPaging="False" AllowSorting="False" CellSpacing="0" CellPadding="1" BorderColor="#999999" BackColor="#DEE3E7" BorderStyle="Solid" OnItemCreated="ItemCreated" OnItemCommand="ItemCommand" borderwidth="1px" autogeneratecolumns="False">
				    <SelectedItemStyle Wrap="False" BackColor="LavenderBlush"></SelectedItemStyle>
				    <EditItemStyle Wrap="False" BackColor="AliceBlue"></EditItemStyle>
				    <AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
				    <ItemStyle Wrap="False" BackColor="White"></ItemStyle>
				    <HeaderStyle></HeaderStyle>
					<Columns>
						<asp:TemplateColumn>
							<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center" Width="1px"></ItemStyle>
							<ItemTemplate>
								<asp:ImageButton id="EditButton_WRITE" style="CURSOR: hand" runat="server" ImageUrl="../Navigation/images/icons/edit2.gif" CommandName="EditRecord"></asp:ImageButton>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn>
							<HeaderStyle HorizontalAlign="Center"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center" Width="1px"></ItemStyle>
							<ItemTemplate>							    
								<asp:ImageButton style="cursor:hand;" id="DeleteButton_WRITE" ImageUrl="../Navigation/images/icons/trash.gif" runat="server" CommandName="DeleteRecord"></asp:ImageButton>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="MAKE">
						    <HeaderStyle CssClass="CenteredTD"></HeaderStyle>
						    <ItemStyle HorizontalAlign="Center"></ItemStyle>
							<ItemTemplate>
								<asp:Label id="lblMake" runat="server" visible="True" text='<%# CType(Container.DataItem, ASSURANT.ElitaPlus.BusinessObjectsNew.CertAddController.BundledItem).Make%>'></asp:Label>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:DropDownList ID="ddlMake" runat="server" Width="150px"></asp:DropDownList>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="MODEL">
						    <HeaderStyle CssClass="CenteredTD"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<ItemTemplate>
								<asp:Label id="lblModel" runat="server" visible="True" text='<%# CType(Container.DataItem, ASSURANT.ElitaPlus.BusinessObjectsNew.CertAddController.BundledItem).Model%>'></asp:Label>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox id="txtModel" runat="server" visible="True" Columns="28" MaxLength="30" text='<%# CType(Container.DataItem, ASSURANT.ElitaPlus.BusinessObjectsNew.CertAddController.BundledItem).Model%>'></asp:TextBox>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="SERIAL_NUMBER">
						    <HeaderStyle CssClass="CenteredTD"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<ItemTemplate>
								<asp:Label id="lblSerialNum" runat="server" visible="True" text='<%# CType(Container.DataItem, ASSURANT.ElitaPlus.BusinessObjectsNew.CertAddController.BundledItem).SerialNumber%>'></asp:Label>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox id="txtSerialNum" runat="server" visible="True" Columns="20" MaxLength="30" text='<%# CType(Container.DataItem, ASSURANT.ElitaPlus.BusinessObjectsNew.CertAddController.BundledItem).SerialNumber%>'></asp:TextBox>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="DESCRIPTION">
						    <HeaderStyle CssClass="CenteredTD"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<ItemTemplate>
								<asp:Label id="lblDesc" runat="server" visible="True" text='<%# CType(Container.DataItem, ASSURANT.ElitaPlus.BusinessObjectsNew.CertAddController.BundledItem).Description%>'></asp:Label>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox id="txtDesc" runat="server" visible="True" Columns="28" MaxLength="50" text='<%# CType(Container.DataItem, ASSURANT.ElitaPlus.BusinessObjectsNew.CertAddController.BundledItem).Description%>'></asp:TextBox>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="PRICE">
						    <HeaderStyle CssClass="CenteredTD"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<ItemTemplate>
								<asp:Label id="lblPrice" runat="server" visible="True" text='<%# CType(Container.DataItem, ASSURANT.ElitaPlus.BusinessObjectsNew.CertAddController.BundledItem).Price.ToString("#,0.00") %>'></asp:Label>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox id="txtPrice" runat="server" visible="True" Columns="10" MaxLength="11" text='<%# CType(Container.DataItem, ASSURANT.ElitaPlus.BusinessObjectsNew.CertAddController.BundledItem).Price.ToString("#,0.00")%>'></asp:TextBox>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="PRODUCT CODE">
						    <HeaderStyle CssClass="CenteredTD"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<ItemTemplate>
								<asp:Label id="lblProdCode" runat="server" visible="True" text='<%# CType(Container.DataItem, ASSURANT.ElitaPlus.BusinessObjectsNew.CertAddController.BundledItem).ProductCode%>'></asp:Label>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox id="txtProdCode" runat="server" visible="True" Columns="6" MaxLength="5" text='<%# CType(Container.DataItem, ASSURANT.ElitaPlus.BusinessObjectsNew.CertAddController.BundledItem).ProductCode%>'></asp:TextBox>
							</EditItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="MFG_WARRANTY">
						    <HeaderStyle CssClass="CenteredTD"></HeaderStyle>
							<ItemStyle HorizontalAlign="Center"></ItemStyle>
							<ItemTemplate>
								<asp:Label id="lblMfgWarranty" runat="server" visible="True" text='<%# CType(Container.DataItem, ASSURANT.ElitaPlus.BusinessObjectsNew.CertAddController.BundledItem).MfgWarranty.ToString %>'></asp:Label>
							</ItemTemplate>
							<EditItemTemplate>
								<asp:TextBox id="txtMfgWarranty" runat="server" visible="True" Columns="6" MaxLength="5" text='<%# CType(Container.DataItem, ASSURANT.ElitaPlus.BusinessObjectsNew.CertAddController.BundledItem).MfgWarranty.ToString%>'></asp:TextBox>
							</EditItemTemplate>
						</asp:TemplateColumn>
					</Columns>
                </asp:DataGrid>
            </td>
        </tr>
        <asp:Literal runat="server" ID="spanFiller"></asp:Literal>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">    
    <asp:Button ID="btnBack" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_BACK" Text="BACK"></asp:Button>
    <asp:Button ID="btnNew" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_NEW" Text="NEW"></asp:Button>
    <asp:Button ID="btnSave" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_SAVE" Text="SAVE"></asp:Button>
    <asp:Button ID="btnCancel" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_UNDO" Text="Cancel"></asp:Button>
</asp:Content>
