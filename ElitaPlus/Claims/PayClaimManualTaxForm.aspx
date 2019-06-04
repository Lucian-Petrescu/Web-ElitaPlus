<%@ Page Title="" Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master" CodeBehind="PayClaimManualTaxForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.PayClaimManualTaxForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <style type="text/css">
        tr.HEADER th { text-align:center}        
    </style>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td style="text-align:left; padding-left:10px">
                <asp:GridView id="Grid" runat="server" Width="70%" AutoGenerateColumns="False" CellPadding="1" AllowPaging="False" AllowSorting="False" 
                    CssClass="DATAGRID">
                    <SelectedRowStyle Wrap="False" CssClass="SELECTED" />
                    <EditRowStyle Wrap="False" CssClass="EDITROW" />
                    <AlternatingRowStyle Wrap="False" CssClass="ALTROW" />
                    <RowStyle Wrap="False" CssClass="ROW" />
					<HeaderStyle CssClass="HEADER"/>
                    <Columns>
                        <asp:TemplateField HeaderText="DESCRIPTION">
                            <ItemStyle CssClass="CenteredTD" Width="60%" />
                            <ItemTemplate>
                                <asp:Label ID="lblDesc" runat="server" Visible="True" Text='<%# Bind("Description") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>  
						<asp:TemplateField HeaderText="AMOUNT">
						    <ItemStyle CssClass="CenteredTD" />
                            <ItemTemplate>
                                <asp:textbox ID="txtAmount" runat="server" Visible="True" Width="95%" MaxLength="35" text='<%# Bind("Amount") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>                        
					</Columns>					
                </asp:GridView>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">
        <asp:Button ID="btnBack" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_BACK" Text="BACK" />
        <asp:Button ID="btnSave_WRITE" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_SAVE" Text="SAVE" />
        <asp:Button ID="btnCancel_WRITE" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_UNDO" Text="Cancel" />
</asp:Content>
