<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="AccountingEventForm.aspx.vb"
    Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.AccountingEventForm" MasterPageFile="~/Navigation/masters/content_default.Master" %>

<%@ Register TagPrefix="mytab" Namespace="Microsoft.Web.UI.WebControls" Assembly="Microsoft.Web.UI.WebControls, Version=1.0.2.226, Culture=neutral, PublicKeyToken=31bf3856ad364e35" %>
<asp:Content ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <link type="text/css" href="../Navigation/styles/jquery-ui.elita.css" rel="stylesheet"/>
    <table id="Table1" style="width: 100%; height: 168px" cellspacing="1" cellpadding="0"
        width="100%" border="0">
        <tr>
            <td class="LABELCOLUMN">
                <asp:Label ID="moAccountingCompanyLABEL" runat="server">ACCOUNTING_COMPANY</asp:Label>
            </td>
            <td>
                &nbsp;
                <asp:DropDownList ID="moAccountingCompanyDropDown" Width="246px" AutoPostBack="false"
                    runat="server">
                </asp:DropDownList>
            </td>
            <td class="LABELCOLUMN">
            </td>
            <td>
            </td>
        </tr>
        <tr>
            <td class="LABELCOLUMN">
                <asp:Label ID="moEventTypeLABEL" runat="server">EVENT_TYPE</asp:Label>
            </td>
            <td>
                &nbsp;
                <asp:DropDownList ID="moEventTypeDDL" Width="246px" AutoPostBack="True" runat="server">
                </asp:DropDownList>
            </td>
            <td class="LABELCOLUMN">
                <asp:Label ID="moEventDescriptionLABEL" runat="server">EVENT_DESCRIPTION</asp:Label>
            </td>
            <td>
                &nbsp;
                <asp:TextBox ID="moEventDescriptionTEXT" runat="server" Width="240px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="LABELCOLUMN">
                <asp:Label ID="moJournalTypeLABEL" runat="server">JOURNAL_TYPE</asp:Label>
            </td>
            <td>
                &nbsp;
                <asp:TextBox ID="moJournalTypeTEXT" runat="server" Width="240px"></asp:TextBox>
            </td>
            <td class="LABELCOLUMN">
                <asp:Label ID="moAllowBalTranLABEL" runat="server">ALLOW_BAL_TRAN</asp:Label>
            </td>
            <td>
                &nbsp;
                <asp:DropDownList ID="moAllowBalTranDDL" Width="246px" AutoPostBack="false" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="LABELCOLUMN">
                <asp:Label ID="moAllowOverBudgetLABEL" runat="server">ALLOW_OVER_BUDGET</asp:Label>
            </td>
            <td>
                &nbsp;
                <asp:DropDownList ID="moAllowOverBudgetDDL" Width="246px" AutoPostBack="false" runat="server">
                </asp:DropDownList>
            </td>
            <td class="LABELCOLUMN">
                <asp:Label ID="moAllowPostToSuspendedLABEL" runat="server">ALLOW_POST_TO_SUSPENDED</asp:Label>
            </td>
            <td>
                &nbsp;
                <asp:DropDownList ID="moAllowPostToSuspendedDDL" Width="246px" AutoPostBack="false"
                    runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="LABELCOLUMN">
                <asp:Label ID="moBalancingOptionsLABEL" runat="server">BALANCING_OPTIONS</asp:Label>
            </td>
            <td>
                &nbsp;
                <asp:DropDownList ID="moBalancingOptionsDDL" Width="246px" AutoPostBack="false" runat="server">
                </asp:DropDownList>
            </td>
            <td class="LABELCOLUMN">
                <asp:Label ID="moLayoutCodeLABEL" runat="server">LAYOUT_CODE</asp:Label>
            </td>
            <td>
                &nbsp;
                <asp:TextBox ID="moLayoutCodeTEXT" runat="server" Width="240px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="LABELCOLUMN">
                <asp:Label ID="moLoadOnlyLABEL" runat="server">LOAD_ONLY</asp:Label>
            </td>
            <td>
                &nbsp;
                <asp:DropDownList ID="moLoadOnlyDDL" Width="246px" AutoPostBack="false" runat="server">
                </asp:DropDownList>
            </td>
            <td class="LABELCOLUMN">
                <asp:Label ID="moPostingTypeLABEL" runat="server">POSTING_TYPE</asp:Label>
            </td>
            <td>
                &nbsp;
                <asp:DropDownList ID="moPostingTypeDDL" Width="246px" AutoPostBack="false" runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="LABELCOLUMN">
                <asp:Label ID="moPostProvisionalLABEL" runat="server">POST_PROVISIONAL</asp:Label>
            </td>
            <td>
                &nbsp;
                <asp:DropDownList ID="moPostProvisionalDDL" Width="246px" AutoPostBack="false" runat="server">
                </asp:DropDownList>
            </td>
            <td class="LABELCOLUMN">
                <asp:Label ID="moPostToHoldLABEL" runat="server">POST_TO_HOLD</asp:Label>
            </td>
            <td>
                &nbsp;
                <asp:TextBox ID="moPostToHoldTEXT" runat="server" Width="240px"></asp:TextBox>
            </td>
        </tr>
        <tr>
            <td class="LABELCOLUMN">
                <asp:Label ID="moReportingAccountLABEL" runat="server">REPORTING_ACCOUNT</asp:Label>
            </td>
            <td>
                &nbsp;
                <asp:TextBox ID="moReportingAccountTEXT" runat="server" Width="240px"></asp:TextBox>
            </td>
            <td class="LABELCOLUMN">
                <asp:Label ID="moSuppressSubstitutedMessagesLABEL" runat="server">SUPPRESS_SUB_MESSAGES</asp:Label>
            </td>
            <td>
                &nbsp;
                <asp:DropDownList ID="moSuppressSubstitutedMessagesDDL" Width="246px" AutoPostBack="false"
                    runat="server">
                </asp:DropDownList>
            </td>
        </tr>
        <tr>
            <td class="LABELCOLUMN">
                <asp:Label ID="moSuspenseAccountLABEL" runat="server">SUSPENSE_ACCOUNT</asp:Label>
            </td>
            <td>
                &nbsp;
                <asp:TextBox ID="moSuspenseAccountTEXT" runat="server" Width="240px"></asp:TextBox>
            </td>
            <td class="LABELCOLUMN">
                <asp:Label ID="moTransactionAmountAccountLABEL" runat="server">TRANSACTION_ACCOUNT</asp:Label>
            </td>
            <td>
                &nbsp;
                <asp:TextBox ID="moTransactionAmountAccountTEXT" runat="server" Width="240px"></asp:TextBox>
            </td>
        </tr>

          <tr>
            <td class="LABELCOLUMN">
                <asp:Label ID="moJournalLevelLABEL" runat="server">JOURNAL_LEVEL</asp:Label>
            </td>
            <td>
                &nbsp;
                 <asp:DropDownList ID="moJournalLevelDDL" Width="246px" AutoPostBack="false" runat="server"></asp:DropDownList>
            </td>
            <td colspan="2">&nbsp;</td>            
        </tr>
        <tr>
            <td colspan="4">
                &nbsp;<input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
                             runat="server"/>
            </td>
        </tr>
        <tr>
            <td colspan="4">
                <div id="tabs" class="style-tabs-old" style="border:none;">
                  <ul>
                    <li style="background:#d5d6e4"><a href="#tabsLineItems"><asp:Label ID="Label4" runat="server" CssClass="tabHeaderTextOld">LINE_ITEMS</asp:Label></a></li>
                  </ul>
          
                  <div id="tabsLineItems" style="background:#d5d6e4">
                    <asp:DataGrid ID="moAccountingEventGrid" runat="server" AllowSorting="True" AllowPaging="True"
                            CellPadding="1" BorderColor="#999999" BackColor="#DEE3E7" BorderWidth="1px" BorderStyle="Solid"
                            AutoGenerateColumns="False" Width="100%">
                            <SelectedItemStyle Wrap="False" BorderStyle="Solid" BorderColor="Orange" BackColor="LightSteelBlue">
                            </SelectedItemStyle>
                            <EditItemStyle Wrap="False"></EditItemStyle>
                            <AlternatingItemStyle Wrap="False" BackColor="#F1F1F1"></AlternatingItemStyle>
                            <ItemStyle Wrap="False" BackColor="White"></ItemStyle>
                            <HeaderStyle></HeaderStyle>
                            <Columns>
                                <asp:TemplateColumn>
                                    <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="5%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:ImageButton Style="cursor: hand;" ID="btnEdit" ImageUrl="../Navigation/images/icons/edit2.gif"
                                            runat="server" CommandName="SelectAction"></asp:ImageButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:TemplateColumn>
                                    <HeaderStyle HorizontalAlign="Center" ForeColor="#12135B" Width="3%"></HeaderStyle>
                                    <ItemStyle HorizontalAlign="Center" Width="30px"></ItemStyle>
                                    <ItemTemplate>
                                        <asp:ImageButton Style="cursor: hand;" ID="DeleteButton_WRITE" ImageUrl="../Navigation/images/icons/trash.gif"
                                            runat="server" CommandName="DeleteRecord"></asp:ImageButton>
                                    </ItemTemplate>
                                </asp:TemplateColumn>
                                <asp:BoundColumn HeaderText="ACCOUNTING_BUSINESS_UNIT" HeaderStyle-Width="10%"></asp:BoundColumn>
                                <asp:BoundColumn HeaderText="ACCOUNT_CODE"></asp:BoundColumn>
                                <asp:BoundColumn HeaderText="DEBIT_CREDIT"></asp:BoundColumn>
                                <asp:BoundColumn HeaderText="FIELD_TYPE"></asp:BoundColumn>
                                <asp:BoundColumn Visible="False"></asp:BoundColumn>
                            </Columns>
                            <PagerStyle HorizontalAlign="Center" ForeColor="DarkSlateBlue" BackColor="#DEE3E7"
                                PageButtonCount="15" Mode="NumericPages"></PagerStyle>
                        </asp:DataGrid>
                        <br>
                        <asp:Button ID="btnNew_WRITE" Style="background-image: url(../Navigation/images/icons/new_icon.gif);
                            cursor: hand; background-repeat: no-repeat" TabIndex="185" runat="server" Width="90px"
                            Font-Bold="false" CssClass="FLATBUTTON" Height="20px" Text="New"></asp:Button>&nbsp;
                  </div>
                </div>
            </td>
        </tr>
    </table>
</asp:Content>
<asp:Content ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <asp:Button ID="btnBack" Style="background-image: url(../Navigation/images/icons/back_icon.gif);
        cursor: hand; background-repeat: no-repeat" TabIndex="185" runat="server" Width="90px"
        Font-Bold="false" CssClass="FLATBUTTON" Height="20px" Text="Back"></asp:Button>&nbsp;
    <asp:Button ID="btnSave_WRITE" Style="background-image: url(../Navigation/images/icons/save_icon.gif);
        cursor: hand; background-repeat: no-repeat" TabIndex="190" runat="server" Width="90px"
        Font-Bold="false" CssClass="FLATBUTTON" Height="20px" Text="Save"></asp:Button>&nbsp;
    <asp:Button ID="btnUndo_Write" Style="background-image: url(../Navigation/images/icons/cancel_icon.gif);
        cursor: hand; background-repeat: no-repeat" TabIndex="195" runat="server" Width="90px"
        Font-Bold="false" CssClass="FLATBUTTON" Height="20px" Text="Undo"></asp:Button>&nbsp;
    <asp:Button ID="btnDelete_WRITE" Style="background-image: url(../Navigation/images/icons/clear_icon.gif);
        cursor: hand; background-repeat: no-repeat" TabIndex="210" runat="server" Width="116px"
        Font-Bold="false" CssClass="FLATBUTTON" Height="20px" Text="Delete"></asp:Button>
    <asp:Button ID="btnCopy_WRITE" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
        cursor: hand; background-repeat: no-repeat" TabIndex="205" runat="server" Width="136px"
        Height="20px" CssClass="FLATBUTTON" Text="NEW_WITH_COPY" CausesValidation="False">
    </asp:Button>
</asp:Content>
