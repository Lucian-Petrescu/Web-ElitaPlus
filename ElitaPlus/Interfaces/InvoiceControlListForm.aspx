<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master" CodeBehind="InvoiceControlListForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.InvoiceControlListForm" 
    title="Untitled Page" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td colspan="2" style="text-align:center">
                <table cellpadding="2" cellspacing="0" border="0" style="BORDER: #999999 1px solid;background-color:#f1f1f1; height:98%; width:100%">
                    <tr><td style="height:7px;" colspan="2"></td></tr>
                    <tr>
                        <td style="text-align:right;white-space:nowrap; width:20%;vertical-align:middle" rowspan="2">
                            &nbsp;<br /><asp:Label ID="Label1" runat="server">DEALER</asp:Label>:
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>
                            <table cellspacing="0" cellpadding="0" border="0">
                                <tr>
		                            <td style="white-space: nowrap;"><asp:Label ID="Label2" runat="server">By_Code</asp:Label></td>
		                            <td>&nbsp;&nbsp;</td>
                                    <td style="white-space: nowrap;"><asp:Label ID="Label4" runat="server">By_Description</asp:Label></td>
	                            </tr>
	                            <tr>
	                                <td><asp:DropDownList ID="ddlDealerCode" runat="server" Width="103px"></asp:DropDownList></td>
	                                <td>&nbsp;&nbsp;</td>
                                    <td><asp:DropDownList ID="ddlDealer" runat="server" Width="250px"></asp:DropDownList></td>
	                            </tr>
                            </table>               
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;white-space:nowrap; vertical-align:middle">
                            <asp:Label ID="lblInvoice" runat="server">INVOICE_NUMB</asp:Label>:
                        </td>
                        <td>
                            <asp:TextBox runat="server" Columns="35" ID="txtInvNum"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;white-space:nowrap; vertical-align:middle">
                            <asp:Label ID="lblInvDate" runat="server">INVOICE_DATE_GREATER_THAN</asp:Label>:
                        </td>
                        <td style="white-space:nowrap">
                            <asp:TextBox runat="server" Columns="20" ID="txtInvDateStart"></asp:TextBox>
                            <asp:imagebutton id="btnInvDateStart" runat="server" Visible="True" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:imagebutton>
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align:right;white-space:nowrap; vertical-align:middle">
                            <asp:Label ID="Label3" runat="server">INVOICE_DATE_LESS_THAN</asp:Label>:
                        </td>
                        <td style="white-space:nowrap">
                            <asp:TextBox runat="server" Columns="20" ID="txtInvDateEnd"></asp:TextBox>
                            <asp:imagebutton id="btnInvDateEnd" runat="server" Visible="True" ImageUrl="../Common/Images/calendarIcon2.jpg"></asp:imagebutton>
                        </td>
                    </tr>
                    <tr><td colspan="2"><hr style="HEIGHT: 1px"/></td></tr>
                    <tr>
                        <td style="text-align:right;" colspan="4">
                            <asp:button id="btnClearSearch" CssClass="FLATBUTTON BUTTONSTYLE_CLEAR" runat="server" Text="Clear"></asp:button>&nbsp;
			                <asp:button id="btnSearch" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_SEARCH" Text="Search"></asp:button>
                        </td>
                    </tr>
                    <tr><td style="height:5px;" colspan="2"></td></tr>
                </table>
            </td>
        </tr>
        <tr><td style="height:7px;" colspan="2"></td></tr>
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
                    AllowSorting="True" OnItemCreated="ItemCreated" OnItemCommand="ItemCommand" CssClass="DATAGRID">
					<SelectedItemStyle Wrap="False" BorderStyle="Solid" BorderColor="Orange" BackColor="LightSteelBlue" />
					<EditItemStyle Wrap="False"></EditItemStyle>
					<AlternatingItemStyle Wrap="False" BackColor="#F1F1F1" />
					<ItemStyle Wrap="False" BackColor="White" HorizontalAlign="Center" />
					<HeaderStyle  HorizontalAlign="Center" />
                    <Columns>
						<asp:TemplateColumn>
							<HeaderStyle ForeColor="#12135B"></HeaderStyle>
							<ItemStyle CssClass="CenteredTD" Width="30px"></ItemStyle>
							<ItemTemplate>
								<asp:ImageButton style="cursor:hand;" id="EditButton_WRITE" ImageUrl="../Navigation/images/icons/edit2.gif"
									runat="server" CommandName="SelectAction"></asp:ImageButton>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn Visible="False">
						    <ItemTemplate>
								<asp:Label id="InvCtlID" runat="server" text='<%# GetGuidStringFromByteArray(Container.DataItem("ACCT_PREM_INVOICE_ID"))%>'></asp:Label>
							</ItemTemplate>
						</asp:TemplateColumn>
						<asp:TemplateColumn HeaderText="DEALER">
						    <HeaderStyle CssClass="CenteredTD" />
						    <ItemStyle CssClass="CenteredTD" />
                            <ItemTemplate>
                                <asp:Label id="lblDealer" runat="server" text='<%#Container.DataItem("CompanyCode") & " - " & Container.DataItem("Dealer_name")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
						<asp:BoundColumn DataField="Branch_Name" HeaderText="BRANCH" HeaderStyle-CssClass="CenteredTD"></asp:BoundColumn>
						<asp:TemplateColumn HeaderText="INVOICE_DATE">
						    <HeaderStyle CssClass="CenteredTD" />
						    <ItemStyle CssClass="CenteredTD" />
                            <ItemTemplate>
                                <asp:Label id="lblInvDate" runat="server" text='<%# formatDateTime(Container.DataItem("CREATED_DATE"))%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateColumn>
						<asp:BoundColumn DataField="Invoice_Number" HeaderText="INVOICE_NUMB" ItemStyle-CssClass="CenteredTD" HeaderStyle-CssClass="CenteredTD"></asp:BoundColumn>
						<asp:BoundColumn DataField="Credit_Note_Number" HeaderText="Credit_Note_Number" ItemStyle-CssClass="CenteredTD" HeaderStyle-CssClass="CenteredTD"></asp:BoundColumn>
						<asp:BoundColumn DataField="NEW_PREMIUM_TOTAL" DataFormatString="{0:#,0.00}"  HeaderText="NEW_PREMIUM_TOTAL" ItemStyle-CssClass="CenteredTD" HeaderStyle-CssClass="CenteredTD"></asp:BoundColumn>
						<asp:BoundColumn DataField="CANCEL_PREMIUM_TOTAL" DataFormatString="{0:#,0.00}"  HeaderText="CANCEL_PREMIUM_TOTAL" ItemStyle-CssClass="CenteredTD" HeaderStyle-CssClass="CenteredTD"></asp:BoundColumn>
					</Columns>
					<PagerStyle ForeColor="DarkSlateBlue" BackColor="#DEE3E7" PageButtonCount="15" Mode="NumericPages"></PagerStyle>					
                </asp:datagrid>
            </td>
        </tr>
        <asp:Literal runat="server" ID="spanFiller"></asp:Literal>
    </table>
    <script type="text/javascript">
        function UpdateList(dest){
            var objS = event.srcElement
            var val = objS.options[objS.selectedIndex].value
            var objD = document.getElementById(dest)
            for(i=0; i<objD.options.length; i++){
                if (objD.options[i].value == val){
                    objD.selectedIndex = i;
                    break;
                }
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <asp:Button ID="btnNew" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_NEW" Text="NEW"></asp:Button>
</asp:Content>
