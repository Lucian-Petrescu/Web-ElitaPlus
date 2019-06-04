<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master" CodeBehind="vscPlanCoverageForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.vscPlanCoverageForm" 
    title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
     <table cellpadding="0" cellspacing="0" border="0" width="100%">
         <tr>
            <td colspan="2" style="text-align:center">
                <table cellpadding="2" cellspacing="0" border="0" style="BORDER: #999999 1px solid;WIDTH: 100%; background-color:#f1f1f1;">
                    <tr><td style="height:8px;" colspan="6"></td></tr>
                    <tr>
                        <td style="text-align:right;white-space: nowrap;">
                            <asp:Label ID="label1" runat="server">PLAN</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">  
                            <asp:textbox id="txtPlan" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="35"></asp:textbox>
                        </td>
                        <td style="text-align:right;white-space: nowrap;">
                            <asp:Label ID="label5" runat="server">EFFECTIVE_DATE</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left"> 
                            <asp:textbox id="txtEffectiveDate" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="15"></asp:textbox>
                        </td>
                        <td style="text-align:right;white-space: nowrap;">
                            <asp:Label ID="label3" runat="server">DEALER_GROUP</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:textbox id="txtDealerGroup" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="40"></asp:textbox>
                        </td>                         
                    </tr>
                    <tr>
                        <td style="text-align:right;white-space: nowrap;">
                            <asp:Label ID="label2" runat="server">VERSION</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:textbox id="txtVersion" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="35"></asp:textbox>
                        </td>                         
                        <td style="text-align:right;white-space: nowrap;">
                            <asp:Label ID="label6" runat="server">EXPIRATION_DATE</asp:Label>: 
                        </td>
                        <td style="white-space: nowrap;" align="left"> 
                            <asp:textbox id="txtExpirationDate" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="15"></asp:textbox>
                        </td>                         
                        <td style="text-align:right;white-space: nowrap;">
                            <asp:Label ID="label4" runat="server">DEALER</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">  
                            <asp:textbox id="txtDealer" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="40"></asp:textbox>
                        </td>                         
                    </tr>
                    <tr><td style="height:7px;" colspan="6"></td></tr>
                </table>            
            </td>
        </tr>
        <tr>
            <td colspan="2">&nbsp;<br /><hr style="HEIGHT: 1px"/></td>
        </tr>
        <tr id="trPageSize" runat="SERVER" visible="False">
            <td align="left">
                <asp:label id="lblPageSize" runat="server">Page_Size:</asp:label>&nbsp;
                <asp:dropdownlist id="cboPageSize" runat="server" Width="50px" AutoPostBack="true">
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
                </asp:dropdownlist>
             </td>
             <td style="text-align:right">
                <asp:label id="lblRecordCount" Runat="server"></asp:label>
             </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:datagrid id="Grid" runat="server" Width="100%" AutoGenerateColumns="False" BackColor="#DEE3E7"
                    BorderColor="#999999" BorderStyle="Solid" CellPadding="1" BorderWidth="1px" AllowPaging="True"
                    AllowSorting="False" OnItemCreated="ItemCreated" OnItemCommand="ItemCommand">
					<SelectedItemStyle Wrap="False" BorderStyle="Solid" BorderColor="Orange" BackColor="LightSteelBlue"></SelectedItemStyle>
					<EditItemStyle Wrap="False"></EditItemStyle>
					<AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
					<ItemStyle Wrap="False" BackColor="White" HorizontalAlign="Center"></ItemStyle>
					<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                    <Columns>
						<asp:TemplateColumn>
							<HeaderStyle ForeColor="#12135B"></HeaderStyle>
							<ItemStyle CssClass="CenteredTD" Width="30px" />
							<ItemTemplate>
								<asp:ImageButton style="cursor:hand;" id="EditButton_WRITE" ImageUrl="../Navigation/images/icons/edit2.gif"
									runat="server" CommandName="SelectAction"></asp:ImageButton>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn Visible="False">
						    <ItemStyle CssClass="CenteredTD" />
							<ItemTemplate>
								<asp:Label id="VSCCoverageId" runat="server" text='<%# Container.DataItem("ROWNUM")%>'></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:BoundColumn DataField="Coverage_Type" HeaderText="COVERAGE_TYPE" ItemStyle-CssClass="CenteredTD" HeaderStyle-CssClass="CenteredTD"></asp:BoundColumn>
						<asp:BoundColumn DataField="ALLOCATION_PERCENT_USED" HeaderText="%_ALLOCATION_USED" DataFormatString="{0:#,0.0000}" ItemStyle-CssClass="CenteredTD" HeaderStyle-CssClass="CenteredTD"></asp:BoundColumn>
						<asp:BoundColumn DataField="ALLOCATION_PERCENT_NEW" HeaderText="%_ALLOCATION_NEW" DataFormatString="{0:#,#.0000}" ItemStyle-CssClass="CenteredTD" HeaderStyle-CssClass="CenteredTD"></asp:BoundColumn>
						<asp:BoundColumn DataField="IS_DEALER_DISCOUNT" HeaderText="DEALER_DISCOUNT" ItemStyle-CssClass="CenteredTD" HeaderStyle-CssClass="CenteredTD"></asp:BoundColumn>
						<asp:BoundColumn DataField="ADD_TO_PLAN" HeaderText="ADD_TO_PLAN" ItemStyle-CssClass="CenteredTD" HeaderStyle-CssClass="CenteredTD"></asp:BoundColumn>
						<asp:BoundColumn DataField="IS_CLAIM_ALLOWED" HeaderText="CLAIM_ALLOWED" ItemStyle-CssClass="CenteredTD" HeaderStyle-CssClass="CenteredTD"></asp:BoundColumn>
					</Columns>
					<PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7" PageButtonCount="15" Mode="NumericPages"></PagerStyle>					
                </asp:datagrid>
            </td>
        </tr>
     </table>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <asp:Button ID="btnBACK" TabIndex="185" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_BACK" Text="BACK"></asp:Button>
</asp:Content>
