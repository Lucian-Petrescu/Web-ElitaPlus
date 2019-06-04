<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master"
    CodeBehind="BillingHistoryListForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.Certificates.BillingHistoryListForm"
    Title="Untitled Page" %>

<%@ Register TagPrefix="uc1" TagName="UserControlCertificateInfo" Src="UserControlCertificateInfo.ascx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td style="text-align: right; vertical-align: top;" width="100%" colspan="2">
                <uc1:UserControlCertificateInfo ID="moCertificateInfoController" runat="server">
                </uc1:UserControlCertificateInfo>
            </td>
        </tr>
        <tr>
            <td valign="middle" width="100%" colspan="2">
                <hr style="height: 1px"/>
            </td>
        </tr>
        <tr id="trPageSize" runat="server" visible="false" >
            <td valign="top" style="text-align: left">
                <asp:Label ID="lblPageSize" runat="server">Page_Size</asp:Label>: &nbsp;
                <asp:DropDownList ID="cboPageSize" runat="server" Width="50px" AutoPostBack="true">
                    <asp:ListItem Value="5">5</asp:ListItem>
                    <asp:ListItem Selected="true" Value="10">10</asp:ListItem>
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
            <td style="text-align: right;">
                <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
            </td>
        </tr>
        <tr id="moESCBillingInformation" runat="server">
            <td style="text-align: center" width="100%" colspan="2">
                <asp:DataGrid ID="Grid" runat="server" Width="100%" AllowPaging="false" AllowSorting="False"
                    CellSpacing="0" CellPadding="1" BorderColor="#999999" BackColor="#DEE3E7" BorderStyle="Solid"
                    OnItemCommand="ItemCommand" BorderWidth="1px" AutoGenerateColumns="False" ShowFooter="true"
                    Height="16px">
                    <SelectedItemStyle Wrap="False" BackColor="LavenderBlush"></SelectedItemStyle>
                    <EditItemStyle Wrap="False" BackColor="AliceBlue"></EditItemStyle>
                    <AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
                    <ItemStyle Wrap="False" BackColor="White"></ItemStyle>
                    <FooterStyle BorderStyle="Double" />
                    <Columns>
        			    <asp:TemplateColumn>
						<HeaderStyle HorizontalAlign="Center" Width="3%" ForeColor="#12135B"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:ImageButton style="cursor:hand;" id="btnEdit" ImageUrl="../Navigation/images/icons/edit2.gif"
								runat="server" CommandName="SelectAction"></asp:ImageButton>
						</ItemTemplate>
					    </asp:TemplateColumn>
                        <asp:BoundColumn Visible="False" HeaderText="Id"></asp:BoundColumn>
   					    <asp:TemplateColumn HeaderText="INSTALLMENT_NUMB">
						<HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center" ></ItemStyle>
						<ItemTemplate>
						</ItemTemplate>
						<EditItemTemplate>
                               <asp:TextBox ID="txtInstallmentNumb" runat="server" Visible="True" Enabled="false">
                                </asp:TextBox>
						</EditItemTemplate>
					    </asp:TemplateColumn>   
   					    <asp:TemplateColumn HeaderText="BILLED_AMOUNT">
						<HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center" ></ItemStyle>
						<ItemTemplate>
						</ItemTemplate>
						<EditItemTemplate>
                               <asp:TextBox ID="txtBilledAmount" runat="server" Visible="True" Enabled="false">
                                </asp:TextBox>
						</EditItemTemplate>
					    </asp:TemplateColumn>
   					    <asp:TemplateColumn HeaderText="DATE_ADDED" >
						<HeaderStyle HorizontalAlign="Center" Width="15%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center" ></ItemStyle>
						<ItemTemplate>
						</ItemTemplate>
						<EditItemTemplate>
                               <asp:TextBox ID="txtDateAdded" runat="server" Visible="True" Enabled="false">
                                </asp:TextBox>
						</EditItemTemplate>
					    </asp:TemplateColumn>   
    					<asp:TemplateColumn HeaderText="Billing_Status">
						<HeaderStyle HorizontalAlign="Center" Width="12%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center" ></ItemStyle>
						<ItemTemplate>
						</ItemTemplate>
						<EditItemTemplate>
                               <asp:DropDownList ID="BillingStatusDropdown" AutoPostBack="true" OnSelectedIndexChanged="EnableDisableRejectCodeDD" runat="server" Visible="True">
                                </asp:DropDownList>
						</EditItemTemplate>
					    </asp:TemplateColumn>   
    					<asp:TemplateColumn HeaderText="REJECT_CODE">
						<HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
						</ItemTemplate>
						<EditItemTemplate>
                               <asp:DropDownList ID="RejectCodesDropdown" runat="server" Width="275px" Visible="True" Enabled="true" >
                               </asp:DropDownList>
						</EditItemTemplate>
					    </asp:TemplateColumn> 
   					    <asp:TemplateColumn HeaderText="REJECT_DATE">
						<HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center" ></ItemStyle>
						<ItemTemplate>
						</ItemTemplate>
						<EditItemTemplate>
                               <asp:TextBox ID="txtRejectDate" runat="server" Visible="True" Enabled="false">
                                </asp:TextBox>
						</EditItemTemplate>
					    </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="PAID?">
						<HeaderStyle HorizontalAlign="Center" Width="5%" ForeColor="#12135B"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center" ></ItemStyle>
						<ItemTemplate>
    						  <asp:Button ID="btnPaid" runat="server" Text="Pay" Width="40px" visible="false" CommandName="PaidAction"></asp:Button>
    						    <asp:Label ID="lblPaid" runat="server" Width="40px" visible="false"></asp:Label>
						</ItemTemplate>
					    </asp:TemplateColumn>	
                        <asp:BoundColumn DataField="processor_reject_code" HeaderText="PROCESSOR_REJECT_CODE" ItemStyle-HorizontalAlign="Center" ReadOnly="true"></asp:BoundColumn>
                     </Columns>
                </asp:DataGrid>
            </td>
        </tr>
        <tr id="moVSCBillingInformation" runat="server">
            <td style="text-align: center" width="100%" colspan="2">
                <asp:DataGrid ID="moVSCGrid" runat="server" Width="100%" AllowPaging="false" AllowSorting="False"
                    CellSpacing="0" CellPadding="1" BorderColor="#999999" BackColor="#DEE3E7" BorderStyle="Solid"
                    OnItemCommand="ItemCommand_VSC" BorderWidth="1px" AutoGenerateColumns="False" ShowFooter="true"
                    Height="16px">
                    <SelectedItemStyle Wrap="False" BackColor="LavenderBlush"></SelectedItemStyle>
                    <EditItemStyle Wrap="False" BackColor="AliceBlue"></EditItemStyle>
                    <AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
                    <ItemStyle Wrap="False" BackColor="White"></ItemStyle>
                    <FooterStyle BorderStyle="Double" />
                    <Columns>
        			    <asp:TemplateColumn>
						<HeaderStyle HorizontalAlign="Center" Width="3%" ForeColor="#12135B"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
							<asp:ImageButton style="cursor:hand;" id="btnEdit" ImageUrl="../Navigation/images/icons/edit2.gif"
								runat="server" CommandName="SelectAction"></asp:ImageButton>
						</ItemTemplate>
					    </asp:TemplateColumn>
                        <asp:BoundColumn Visible="False" HeaderText="Id"></asp:BoundColumn>
   					    <asp:TemplateColumn HeaderText="INSTALLMENT_NUMB">
						<HeaderStyle HorizontalAlign="Center" Width="12%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center" ></ItemStyle>
						<ItemTemplate>
						</ItemTemplate>
						<EditItemTemplate>
                               <asp:TextBox ID="txtInstallmentNumb" runat="server" Visible="True" Enabled="false">
                                </asp:TextBox>
						</EditItemTemplate>
					    </asp:TemplateColumn>   
   					    <asp:TemplateColumn HeaderText="BILLED_AMOUNT">
						<HeaderStyle HorizontalAlign="Center" Width="8%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center" ></ItemStyle>
						<ItemTemplate>
						</ItemTemplate>
						<EditItemTemplate>
                               <asp:TextBox ID="txtBilledAmount" runat="server" Visible="True" Enabled="false">
                                </asp:TextBox>
						</EditItemTemplate>
					    </asp:TemplateColumn>
   					    <asp:TemplateColumn HeaderText="INSTALLMENT_DUE_DATE">
						<HeaderStyle HorizontalAlign="Center" Width="12%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center" ></ItemStyle>
						<ItemTemplate>
						</ItemTemplate>
						<EditItemTemplate>
                               <asp:TextBox ID="txtInstallmentDueDate" runat="server" Visible="True" Enabled="false">
                                </asp:TextBox>
						</EditItemTemplate>
					    </asp:TemplateColumn>    
   					    <asp:TemplateColumn HeaderText="DATE_ADDED" >
						<HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center" ></ItemStyle>
						<ItemTemplate>
						</ItemTemplate>
						<EditItemTemplate>
                               <asp:TextBox ID="txtDateAdded" runat="server" Visible="True" Enabled="false">
                                </asp:TextBox>
						</EditItemTemplate>
					    </asp:TemplateColumn>   
    					<asp:TemplateColumn HeaderText="Billing_Status">
						<HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center" ></ItemStyle>
						<ItemTemplate>
						</ItemTemplate>
						<EditItemTemplate>
                               <asp:DropDownList ID="BillingStatusDropdown" AutoPostBack="true" OnSelectedIndexChanged="EnableDisableRejectCodeDD" runat="server" Visible="True">
                                </asp:DropDownList>
						</EditItemTemplate>
					    </asp:TemplateColumn>   
    					<asp:TemplateColumn HeaderText="REJECT_CODE">
						<HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center"></ItemStyle>
						<ItemTemplate>
						</ItemTemplate>
						<EditItemTemplate>
                               <asp:DropDownList ID="RejectCodesDropdown" runat="server" Width="150px" Visible="True" Enabled="true" >
                               </asp:DropDownList>
						</EditItemTemplate>
					    </asp:TemplateColumn> 
   					    <asp:TemplateColumn HeaderText="RE_ATTEMPT_COUNT">
						<HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center" ></ItemStyle>
						<ItemTemplate>
						</ItemTemplate>
						<EditItemTemplate>
                               <asp:TextBox ID="txtReAttemptCount" runat="server" Visible="True" Enabled="false">
                                </asp:TextBox>
						</EditItemTemplate>
					    </asp:TemplateColumn>
   					    <asp:TemplateColumn HeaderText="REJECT_DATE">
						<HeaderStyle HorizontalAlign="Center" Width="10%"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center" ></ItemStyle>
						<ItemTemplate>
						</ItemTemplate>
						<EditItemTemplate>
                               <asp:TextBox ID="txtRejectDate" runat="server" Visible="True" Enabled="false">
                                </asp:TextBox>
						</EditItemTemplate>
					    </asp:TemplateColumn>
                        <asp:TemplateColumn HeaderText="PAID?">
						<HeaderStyle HorizontalAlign="Center" Width="5%" ForeColor="#12135B"></HeaderStyle>
						<ItemStyle HorizontalAlign="Center" ></ItemStyle>
						<ItemTemplate>
    						  <asp:Button ID="btnPaid" runat="server" Text="Pay" Width="40px" visible="false" CommandName="PaidAction"></asp:Button>
    						  <asp:Label ID="lblPaid" runat="server" Width="40px" visible="false"></asp:Label>
						</ItemTemplate>
					    </asp:TemplateColumn>				      					                 
                     </Columns>
                </asp:DataGrid>
            </td>
        </tr>
    </table>
    <input id="HiddenPayPromptResponse" type="hidden" name="HiddenPayPromptResponse" runat="server" />

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <asp:Button ID="btnBack" runat="server" CssClass="FLATBUTTON BUTTONSTYLE_BACK" Text="BACK">
    </asp:Button>
   <asp:Button ID="btnSave_WRITE" Style="background-image: url(../Navigation/images/icons/save_trans_icon.gif);
        cursor: hand; background-repeat: no-repeat" TabIndex="15" runat="server" Width="80px"
        CausesValidation="False" Height="20px" CssClass="FLATBUTTON" Text="SAVE"></asp:Button>
    <asp:Button ID="btnUndo_WRITE" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
        cursor: hand; background-repeat: no-repeat" TabIndex="16" runat="server" Width="90px"
        CausesValidation="False" Height="20px" CssClass="FLATBUTTON" Text="UNDO"></asp:Button>    
</asp:Content>
