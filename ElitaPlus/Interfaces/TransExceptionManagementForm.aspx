<%@ Page Language="vb" AutoEventWireup="false" MasterPageFile="~/Navigation/masters/content_default.Master"
    CodeBehind="TransExceptionManagementForm.aspx.vb" Inherits="Assurant.ElitaPlus.ElitaPlusWebApp.TransExceptionManagementForm"
    Title="Untitled Page" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPanelMainContentBody" runat="server">
    <style type="text/css">
        tr.HEADER th
        {
            text-align: center;
        }
        tr.LeftAlign td
        {
            text-align: left;
            padding-right: 25px;
        }
        INPUT.BUTTONSTYLE_EDIT
        {
            background-image: url(../Navigation/images/icons/edit2.gif);
            width: 165px;
        }
    </style>
    <table cellpadding="0" cellspacing="0" border="0" width="100%">
        <tr>
            <td colspan="2" style="text-align: center; padding-left: 8px; padding-right: 8px;">
                <table cellpadding="2" cellspacing="0" border="0" style="border: #999999 1px solid;
                    background-color: #f1f1f1; height: 40px; width: 100%; padding: 0px;">
                    <tr valign="bottom">
                        <td style="padding-top: 8px;width: 100%;" colspan="3" valign="bottom">
                            <%= lastSuccessfulElitaMessage%>
                        </td>
                    </tr>
                    <tr valign="bottom">
                        <td style="padding-top: 8px;width: 100%;" colspan="3" valign="bottom">
                            <%= lastSuccessfulGVSMessage%>
                        </td>
                    </tr>
                    <tr align="center" valign="top">
                        <td style="width:100%;" colspan="3">
                            <hr style="height: 1px" />
                        </td>
                    </tr>
                    <tr class="LeftAlign">
                        <td style="width: 25%;">
                            <asp:Label ID="lblClaimNumber" runat="server">Claim_Number</asp:Label>:<br />
                            <asp:TextBox ID="txtClaimNumber" runat="server" CssClass="FLATTEXTBOX_TAB" AutoPostBack="False"
                                Width="80%" TabIndex="1"></asp:TextBox>
                        </td>
                        <td style="width: 25%;">
                            <asp:Label ID="lblAuthNumber" runat="server">Authorization_Number</asp:Label>:<br />
                            <asp:TextBox ID="txtAuthNumber" runat="server" CssClass="FLATTEXTBOX_TAB" AutoPostBack="False"
                                Width="80%" TabIndex="2"></asp:TextBox>
                        </td>
                        <td style="padding-right: 9px; vertical-align: bottom; width: 31%;">
                            <asp:Label ID="lblFrom" runat="server">FROM</asp:Label>:
                            <asp:TextBox ID="txtFrom" runat="server" Width="90px" CssClass="FLATTEXTBOX_TAB"
                                onFocus="setHighlighter(this)" onMouseover="setHighlighter(this)" Visible="True"></asp:TextBox>
                            <asp:ImageButton ID="imgBtnFrom" runat="server" 
                                ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="AbsMiddle" />
                            &nbsp;&nbsp;
                            <asp:Label ID="lblTo" runat="server">TO</asp:Label>:
                            <asp:TextBox ID="txtTo" runat="server" Width="90px" onFocus="setHighlighter(this)"
                                onMouseover="setHighlighter(this)" CssClass="FLATTEXTBOX_TAB" Visible="True"></asp:TextBox>
                            <asp:ImageButton ID="imgBtnTo" runat="server" 
                                ImageUrl="../Common/Images/calendarIcon2.jpg" ImageAlign="AbsMiddle" />
                        </td>
                    </tr>
                    <tr class="LeftAlign">
                        <td style="width: 25%;">
                            <asp:Label ID="lblServiceCenterName" runat="server">Service_Center_Code</asp:Label>:<br />
                            <asp:TextBox ID="txtServiceCenterName" runat="server" CssClass="FLATTEXTBOX_TAB"
                                AutoPostBack="False" Width="80%" TabIndex="5"></asp:TextBox>
                        </td>
                        <td style="width: 25%;">
                            <asp:Label ID="lblErrorCode" runat="server">Error_Code</asp:Label>:<br />
                            <asp:TextBox ID="txtErrorCode" runat="server" CssClass="FLATTEXTBOX_TAB" AutoPostBack="False"
                                Width="80%" TabIndex="5"></asp:TextBox>
                        </td>
                        <td style="width: 31%;">
                            &nbsp;
                        </td>
                    </tr>
                    <tr>
                        <td style="text-align: right; vertical-align: text-bottom; padding-right: 4px; padding-bottom: 5px"
                            colspan="3">
                            <asp:Button ID="btnClearSearch" CssClass="FLATBUTTON BUTTONSTYLE_CLEAR" runat="server"
                                Text="Clear" TabIndex="5" />&nbsp;&nbsp;
                            <asp:Button ID="btnSearch" CssClass="FLATBUTTON BUTTONSTYLE_SEARCH" runat="server"
                                Text="Search" TabIndex="4" />
                        </td>
                    </tr>
                </table>
            </td>
        </tr>
        <tr>
            <td style="padding-top: 5px; padding-bottom: 5px;" colspan="2">
                <hr style="height: 1px" />
            </td>
        </tr>
        <tr id="trPageSize" runat="server" visible="False">
            <td align="left">
                <asp:Label ID="lblPageSize" runat="server">Page_Size:</asp:Label>&nbsp;
                <asp:DropDownList ID="cboPageSize" runat="server" Width="50px" AutoPostBack="true">
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
                </asp:DropDownList>
            </td>
            <td style="text-align: right">
                <asp:Label ID="lblRecordCount" runat="server"></asp:Label>
            </td>
        </tr>
        <tr class="LeftAlign">
            <td colspan="2">
                &nbsp;
            </td>
        </tr>
        <tr>
            <td colspan="2">
                <asp:GridView ID="Grid" runat="server" Width="100%" AutoGenerateColumns="False" CellPadding="1"
                    AllowPaging="True" AllowSorting="True" CssClass="DATAGRID">
                    <SelectedRowStyle Wrap="False" CssClass="SELECTED" />
                    <EditRowStyle Wrap="False" CssClass="EDITROW" />
                    <AlternatingRowStyle Wrap="False" CssClass="ALTROW" />
                    <RowStyle Wrap="False" CssClass="ROW" />
                    <HeaderStyle CssClass="HEADER" />
                    <Columns>
                        <asp:TemplateField HeaderStyle-Width="18px">
                            <ItemStyle Width="18px" CssClass="CenteredTD" />
                            <ItemTemplate>
                                <asp:ImageButton Style="cursor: hand;" ID="EditButton_WRITE" ImageUrl="../Navigation/images/icons/edit2.gif"
                                    runat="server" CommandName="SelectAction"></asp:ImageButton>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField>
                            <HeaderTemplate>
                                <asp:CheckBox runat="server" ID="HeaderLevelCheckBox" />
                            </HeaderTemplate>
                            <ItemTemplate>
                                <asp:CheckBox ID="CheckBoxItemSel" runat="server" Checked="false">
                                </asp:CheckBox>
                            </ItemTemplate>
                        </asp:TemplateField>                        
                        <asp:TemplateField Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblShowCheckboxID" runat="server" Text='<%# Container.DataItem("show_checkbox")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblTransactionLogHeaderID" runat="server" Text='<%# GetGuidStringFromByteArray(Container.DataItem("transaction_log_header_id"))%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Claim_Number" SortExpression="claim_number">
                            <ItemStyle CssClass="CenteredTD" />
                            <ItemTemplate>
                                <asp:Label ID="lblClaimNumber" runat="server" Visible="True" Text='<%#Container.DataItem("claim_number")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Transaction_Date" SortExpression="transaction_date">
                            <ItemStyle CssClass="CenteredTD" Width="180px" />
                            <ItemTemplate>
                                <asp:Label ID="lblTransactionDate" runat="server" Text='<%#Container.DataItem("transaction_date")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Customer_Name" SortExpression="customer_name">
                            <ItemStyle CssClass="CenteredTD" />
                            <ItemTemplate>
                                <asp:Label ID="lblCustomerName" runat="server" Text='<%#Container.DataItem("customer_name")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="SERVICE_CENTER_NAME" SortExpression="service_center_code">
                            <ItemTemplate>
                                <asp:Label ID="lblSvcName" runat="server" Text='<%#Container.DataItem("service_center_code")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Authorization_Number" SortExpression="authorization_number">
                            <ItemTemplate>
                                <asp:Label ID="lblAuthNumber" runat="server" Text='<%#Container.DataItem("authorization_number")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Error_Code" SortExpression="error_code">
                            <ItemStyle Width="400px" />
                            <ItemTemplate>
                                <asp:Label ID="lblErrCode" runat="server" Text='<%#Container.DataItem("error_code")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Originator" SortExpression="originator">
                            <ItemTemplate>
                                <asp:Label ID="lblOriginator" runat="server" Text='<%#Container.DataItem("originator")%>' />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField Visible="False">
                            <ItemTemplate>
                                <asp:Label ID="lblFunctionType" runat="server" Text='<%#Container.DataItem("function_type")%>'></asp:Label>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                    <PagerSettings PageButtonCount="15" Mode="Numeric" />
                    <PagerStyle HorizontalAlign="Center" CssClass="PAGER" />
                </asp:GridView>
            </td>
        </tr>        
    </table>
    <p><asp:Literal ID="CheckBoxIDsArray" runat="server"></asp:Literal>&nbsp;</p>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPanelButtons" runat="server">
    <input id="HiddenSaveChangesPromptResponse" type="hidden" name="HiddenSaveChangesPromptResponse"
        runat="server" designtimedragdrop="261" />
    <asp:Button ID="btnProcessRecords" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false" Enabled=false
        Width="140px" CssClass="FLATBUTTON" Height="20px" Text="ProcessRecords"></asp:Button>
    &nbsp;
    <asp:Button ID="btnExportResults" Style="background-image: url(../Navigation/images/icons/add_icon.gif);
        cursor: hand; background-repeat: no-repeat" runat="server" Font-Bold="false" Enabled=true
        Width="140px" CssClass="FLATBUTTON" Height="20px" Text="ExportResults"></asp:Button>
        <input type="hidden" id="checkRecords" value="" runat="server" />
    <script>
		    function CheckboxAction(cbValue, cbClientId, btnClientId, hiddenClientId) {
		        var objCbId = document.getElementById(cbClientId);
		        var objbtnId = document.getElementById(btnClientId);
		        var objCheckRecords = document.getElementById(hiddenClientId);
		        var tempValue = "";

		        if (objCbId != null && cbValue != null) 
		        {
		            if (objCbId.checked)
		            {
		                objCheckRecords.value = cbValue + ':' + objCheckRecords.value;
		            }
		            else
		            {
		                objCheckRecords.value = objCheckRecords.value.replace(cbValue, '');
		            }

		            tempValue = objCheckRecords.value;
		            tempValue = tempValue.replace(/:/g, '');

		            if (tempValue != null && tempValue != '') {
		                objbtnId.disabled = false;
		            }
		            else {
		                objbtnId.disabled = true;
		            }

		            var intCheckedCount = 0;
		            for (var i = 0; i < CheckBoxIDs.length; i++) {
		                objCbId = document.getElementById(CheckBoxIDs[i]);
		                if (objCbId.checked == true) {
		                    intCheckedCount = intCheckedCount + 1;
		                    break;
		                }
		            }
		            if (intCheckedCount > 0) {
		                objbtnId.disabled = false;
		            }
		            else {
		                objbtnId.disabled = true;
		            }
		            
                    
                }
		    }

		    function ChangeCheckBoxState(id, checkState) {
		        var cb = document.getElementById(id);
		        if (cb != null)
		            cb.checked = checkState;
		    }

		    function ChangeAllCheckBoxStates(checkState, btnClientId) {
		        // Toggles through all of the checkboxes defined in the CheckBoxIDs array
		        // and updates their value to the checkState input parameter
		        if (CheckBoxIDs != null) {
		            for (var i = 0; i < CheckBoxIDs.length; i++)
		                ChangeCheckBoxState(CheckBoxIDs[i], checkState);
		        }

		        var objbtnId = document.getElementById(btnClientId);
		        if (checkState == true) {
		            objbtnId.disabled = false;
		        }
		        else {		            
		            objbtnId.disabled = true;
		        }

               
		    }

		    function ChangeHeaderAsNeeded() {
		        // Whenever a checkbox in the GridView is toggled, we need to
		        // check the Header checkbox if ALL of the GridView checkboxes are
		        // checked, and uncheck it otherwise
		        if (CheckBoxIDs != null) {
		            // check to see if all other checkboxes are checked
		            for (var i = 1; i < CheckBoxIDs.length; i++) {
		                var cb = document.getElementById(CheckBoxIDs[i]);
		                if (!cb.checked) {
		                    // Whoops, there is an unchecked checkbox, make sure
		                    // that the header checkbox is unchecked
		                    ChangeCheckBoxState(CheckBoxIDs[0], false);
		                    return;
		                }
		            }

		            // If we reach here, ALL GridView checkboxes are checked
		            ChangeCheckBoxState(CheckBoxIDs[0], true);
		        }
		    }	 	    
    </script>

</asp:Content>
