<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master" CodeBehind="vscPlanCoveragesRateForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.vscPlanCoveragesRateForm" 
    title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
     <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td colspan="2" style="text-align:center">
                <table cellpadding="2" cellspacing="0" border="0" style="BORDER: #999999 1px solid;WIDTH:100%; background-color:#f1f1f1;">
                    <tr>
                        <td style="text-align:right">
                            <asp:Label ID="label1" runat="server">PLAN</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;"  align="left">  
                            <asp:textbox id="txtPlan" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="40"></asp:textbox>
                        </td>
                        <td style="text-align:right">
                            <asp:Label ID="label2" runat="server">VERSION</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:textbox id="txtVersion" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="18"></asp:textbox>
                        </td>                         
                    </tr>
                    <tr>
                        <td style="text-align:right;white-space: nowrap;">
                            <asp:Label ID="label3" runat="server">DEALER_GROUP</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:textbox id="txtDealerGroup" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="40"></asp:textbox>
                        </td>                         
                        <td style="text-align:right;white-space: nowrap;">
                            <asp:Label ID="label5" runat="server">EFFECTIVE_DATE</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left"> 
                            <asp:textbox id="txtEffectiveDate" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="18"></asp:textbox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right">
                            <asp:Label ID="label4" runat="server">DEALER</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">  
                            <asp:textbox id="txtDealer" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="40"></asp:textbox>
                        </td>                         
                        <td style="text-align:right;white-space: nowrap;">
                            <asp:Label ID="label6" runat="server">EXPIRATION_DATE</asp:Label>: 
                        </td>
                        <td style="white-space: nowrap;"  align="left"> 
                            <asp:textbox id="txtExpirationDate" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="18"></asp:textbox>
                        </td>                         
                    </tr>
                    <tr><td colspan="4"><hr style="HEIGHT:1px"/></td></tr>
                    <tr>
                        <td style="text-align:right;white-space: nowrap;">
                            <asp:Label ID="label7" runat="server">COVERAGE_TYPE</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:textbox id="txtCoverageType" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="40"></asp:textbox>
                        </td>
                        <td style="text-align:right;white-space: nowrap;">
                            <asp:Label ID="label12" runat="server">ADD_TO_PLAN</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:textbox id="txtAddToPlan" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="18"></asp:textbox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;white-space: nowrap;">
                            <asp:Label ID="label8" runat="server">%_ALLOCATION_NEW</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:textbox id="txtAllocPctNew" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="40"></asp:textbox>
                        </td>
                        <td style="text-align:right;white-space: nowrap;">
                            <asp:Label ID="label11" runat="server">DEALER_DISCOUNT</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:textbox id="txtDealerDiscount" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="18"></asp:textbox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;white-space: nowrap;">
                            <asp:Label ID="label9" runat="server">%_ALLOCATION_USED</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:textbox id="txtAllocPctUsed" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="40"></asp:textbox>
                        </td>
                        <td style="text-align:right;white-space: nowrap;">
                            <asp:Label ID="label13" runat="server">CLAIM_ALLOWED</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;" align="left">
                            <asp:textbox id="txtClaimAllowed" runat="server" CssClass="FLATTEXTBOX" ReadOnly="True" Columns="18"></asp:textbox>
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr><td style="height:8px" colspan="2"></td></tr>
        <tr>
            <td colspan="2" style="text-align:center">
                <table cellpadding="2" cellspacing="0" border="0" style="BORDER: #999999 1px solid;WIDTH:100%; background-color:#f1f1f1;">
                    <tr><td style="height:6px" colspan="6"></td></tr>
                    <tr>
                        <td style="text-align:right;white-space: nowrap;">
                            <asp:Label ID="label18" runat="server">COVERAGE_SUPPORT_FOR</asp:Label>:
                        </td>
                        <td style="text-align:left">
                            <asp:RadioButton runat="Server" Text="NEW" ID="rdoAllocNew" Checked="true" GroupName="rdoAllocPctGroup"/>
                            <asp:RadioButton runat="Server" Text="USED" ID="rdoAllocUsed" Checked="false" GroupName="rdoAllocPctGroup"/>
                        </td>
                        <td style="text-align:right;white-space: nowrap;">
                            <asp:Label ID="label10" runat="server">CLASS_CODE</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;">
                             <asp:TextBox ID="txtClassCode" runat="Server" CssClass="FLATTEXTBOX" Columns="25" MaxLength="25"></asp:TextBox> 
                        </td>
                        <td style="text-align:right;white-space: nowrap;">
                            <asp:Label ID="label14" runat="server">TERM</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;">
                             <asp:TextBox ID="txtTermMon" runat="Server" CssClass="FLATTEXTBOX" Columns="25" MaxLength="4"></asp:TextBox> 
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;white-space: nowrap;">
                            <asp:Label ID="label17" runat="server">ENGINE_MONTHS_KM-MI</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;">
                             <asp:DropDownList ID="ddlEngineWarranty" runat="server" Width="103px"></asp:DropDownList>
                        </td>
                        <td style="text-align:right;white-space: nowrap;">
                            <asp:Label ID="label15" runat="server">DEDUCTIBLE</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;">
                             <asp:TextBox ID="txtDeductible" runat="Server" CssClass="FLATTEXTBOX" Columns="25" MaxLength="10"></asp:TextBox> 
                        </td>
                        <td style="text-align:right;white-space: nowrap;">
                            <asp:Label ID="label16" runat="server">ODOMETER</asp:Label>:
                        </td>
                         <td style="white-space: nowrap;">
                             <asp:TextBox ID="txtOdometer" runat="Server" CssClass="FLATTEXTBOX" Columns="25" MaxLength="10"></asp:TextBox> 
                        </td>
                    </tr>
                    <tr>
                        <%--'start--%>
                        <td style="text-align:right;white-space: nowrap;">
                            <asp:Label ID="label19" runat="server">VEHICLE_VALUE</asp:Label>:
                        </td>
                        <td style="white-space: nowrap;">
                             <asp:TextBox ID="txtVehiclevalue" runat="Server" CssClass="FLATTEXTBOX" Columns="25" MaxLength="10"></asp:TextBox> 
                        </td>
                        
                        <%--'start--%>
                         <td align="right" nowrap="nowrap" style="margin-left: 80px">
                    <%--empyt--%>
                </td>
                <td align="left" nowrap="nowrap">
                    <%--empyt--%>
                </td>
                    </tr>
                    <tr><td colspan="6"><hr style="HEIGHT: 1px"/></td></tr>
                    <tr>
                        <td style="text-align:right" colspan="6">
                            <asp:button id="btnClearSearch" CssClass="FLATBUTTON BUTTONSTYLE_CLEAR" runat="server" Text="Clear"></asp:button>&nbsp;
			                <asp:button id="btnSearch" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_SEARCH" Text="Search"></asp:button>
                        </td>
                     </tr>
                     <tr><td colspan="6" style="height:5px;"></td></tr>
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
                    AllowSorting="False" OnItemCreated="ItemCreated">
					<SelectedItemStyle Wrap="False" BorderStyle="Solid" BorderColor="Orange" BackColor="LightSteelBlue"></SelectedItemStyle>
					<EditItemStyle Wrap="False"></EditItemStyle>
					<AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
					<ItemStyle Wrap="False" BackColor="White" HorizontalAlign="Center"></ItemStyle>
					<HeaderStyle  HorizontalAlign="Center"></HeaderStyle>
                    <Columns>
						<asp:BoundColumn DataField="CLASS_CODE" HeaderText="CLASS_CODE" ItemStyle-CssClass="CenteredTD" HeaderStyle-CssClass="CenteredTD"></asp:BoundColumn>
						<asp:BoundColumn DataField="TERM_MONTHS" HeaderText="TERM" ItemStyle-CssClass="CenteredTD" HeaderStyle-CssClass="CenteredTD"></asp:BoundColumn>
                        <%--<asp:BoundColumn DataField="TERM_KM_MI" HeaderText="TERM_KM_MI" ItemStyle-CssClass="CenteredTD" HeaderStyle-CssClass="CenteredTD"></asp:BoundColumn>--%>
						<asp:BoundColumn DataField="DEDUCTIBLE" HeaderText="DEDUCTIBLE" DataFormatString="{0:#,#.00}" ItemStyle-CssClass="CenteredTD" HeaderStyle-CssClass="CenteredTD"></asp:BoundColumn>
						<asp:TemplateColumn HeaderText="ODOMETER">
						    <ItemStyle CssClass="CenteredTD" />
						    <HeaderStyle CssClass="CenteredTD" />
                            <ItemTemplate>
                                <asp:Label id="lblGridOdometer" runat="server" text='<%# String.Format("{0}-{1}", Container.DataItem("ODOMETER_LOW_RANGE"), Container.DataItem("ODOMETER_HIGH_RANGE"))%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="ENGINE_MONTHS_KM-MI">
						    <ItemStyle CssClass="CenteredTD" />
						    <HeaderStyle CssClass="CenteredTD" />
                            <ItemTemplate>
                                <asp:Label id="lblEngineWarranty" runat="server" text='<%# String.Format("{0}/{1}", Container.DataItem("ENGINE_MANUF_WARR_MONTHS"), Container.DataItem("ENGINE_MANUF_WARR_KM_MI"))%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="GWP" HeaderText="PLAN_GWP" DataFormatString="{0:#,0.00}" ItemStyle-CssClass="CenteredTD" HeaderStyle-CssClass="CenteredTD"></asp:BoundColumn>
                        <asp:BoundColumn DataField="WP" HeaderText="PLAN_WP" DataFormatString="{0:#,0.00}" ItemStyle-CssClass="CenteredTD" HeaderStyle-CssClass="CenteredTD"></asp:BoundColumn>
						<asp:TemplateColumn HeaderText="GWP">
						    <ItemStyle CssClass="CenteredTD" />
						    <HeaderStyle CssClass="CenteredTD" />
                            <ItemTemplate>
                                <asp:Label id="lblGridGWP" runat="server" text='<%# Convert.ToDecimal(Container.DataItem("GWP") * AllocationPercent /100).ToString("#,##0.00") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="WP">
						    <ItemStyle CssClass="CenteredTD" />
						    <HeaderStyle CssClass="CenteredTD" />
                            <ItemTemplate>
                                <asp:Label id="lblGridWP" runat="server" text='<%# Convert.ToDecimal(Container.DataItem("WP") * AllocationPercent /100).ToString("#,##0.00") %>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
						<asp:BoundColumn DataField="COMMISSIONS_PERCENT" HeaderText="COMMISSION_PERCENT" DataFormatString="{0:#,0.0000}" ItemStyle-CssClass="CenteredTD" HeaderStyle-CssClass="CenteredTD"></asp:BoundColumn>
						<asp:BoundColumn DataField="MARKETING_PERCENT" HeaderText="MARKETING_PERCENT" DataFormatString="{0:#,0.0000}" ItemStyle-CssClass="CenteredTD" HeaderStyle-CssClass="CenteredTD"></asp:BoundColumn>
						<asp:BoundColumn DataField="ADMIN_EXPENSE" HeaderText="ADMINISTRATIVE_EXPENSE" DataFormatString="{0:#,0.0000}" ItemStyle-CssClass="CenteredTD" HeaderStyle-CssClass="CenteredTD"></asp:BoundColumn>
						<asp:BoundColumn DataField="LOSS_COST_PERCENT" HeaderText="LOSS_COST_PERCENT" DataFormatString="{0:#,0.0000}" ItemStyle-CssClass="CenteredTD" HeaderStyle-CssClass="CenteredTD"></asp:BoundColumn>
						<asp:BoundColumn DataField="PROFIT_EXPENSE" HeaderText="PROFIT_PERCENT" DataFormatString="{0:#,0.0000}" ItemStyle-CssClass="CenteredTD" HeaderStyle-CssClass="CenteredTD"></asp:BoundColumn>
					    <asp:TemplateColumn HeaderText="VEHICLE_VALUE">
						    <ItemStyle CssClass="CenteredTD" />
						    <HeaderStyle CssClass="CenteredTD" />
                            <ItemTemplate>
                                <asp:Label id="lblGridVehiclevalue" runat="server" text='<%# String.Format("{0}-{1}", Container.DataItem("VEHICLE_PURCHASE_PRICE_FROM"), Container.DataItem("VEHICLE_PURCHASE_PRICE_TO"))%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
                        <asp:BoundColumn DataField="ALLOWED_EVENTS" HeaderText="ALLOWED_EVENTS" ItemStyle-CssClass="CenteredTD" HeaderStyle-CssClass="CenteredTD"></asp:BoundColumn>
                        <asp:BoundColumn DataField="MAX_INSURED_AMOUNT" HeaderText="MAX_INSURED_AMOUNT" DataFormatString="{0:#,0.00}" ItemStyle-CssClass="CenteredTD" HeaderStyle-CssClass="CenteredTD"></asp:BoundColumn>
                        <%--<asp:BoundColumn DataField="PERIOD_LOWER_LIMIT" HeaderText="PERIOD_LOWER_LIMIT" ItemStyle-CssClass="CenteredTD" HeaderStyle-CssClass="CenteredTD"></asp:BoundColumn>
                        <asp:BoundColumn DataField="PERIOD_UPPER_LIMIT" HeaderText="PERIOD_UPPER_LIMIT" ItemStyle-CssClass="CenteredTD" HeaderStyle-CssClass="CenteredTD"></asp:BoundColumn>
                        <asp:BoundColumn DataField="MAX_WARRANTY_KM" HeaderText="MAX_WARRANTY_KM" ItemStyle-CssClass="CenteredTD" HeaderStyle-CssClass="CenteredTD"></asp:BoundColumn>--%>
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
